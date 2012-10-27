Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library

Imports Microsoft.VisualBasic.FileIO

Imports System.Data.SQLite
Imports System.ComponentModel

Public Class DVDArt_Process

    Private _delay, _scraping, _maxcpu, _missing As Integer
    Private _checked(2) As Boolean
    Private _lastrun, _language As String

    Private database, thumbs As String
    Private lv_import As New ListView
    Private li_import As New ListViewItem
    Private WithEvents bw_import As New BackgroundWorker

    Public Function CPU_Usage_Percent() As String

        Dim cpu As PerformanceCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total")

        Dim dummy As Long

        dummy = cpu.NextValue()
        System.Threading.Thread.Sleep(1000)

        Return cpu.NextValue().ToString("#0") & "%"

    End Function

    Private Sub wait(ByVal seconds As Long)

        Log.Info("DVDArt: process plugin sleeping " & seconds.ToString & " seconds.....")

        System.Threading.Thread.Sleep(seconds * 1000)

    End Sub

    Private Sub bw_import_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_import.DoWork

        If lv_import.Items.Count > 0 Then

            Dim x As Integer
            Dim filenotexist(2), downloaded(2), try2download(2) As Boolean
            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand

            SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

            SQLconnect.Open()

            SQLcommand = SQLconnect.CreateCommand

            For x = 0 To (lv_import.Items.Count - 1)

                For y = 0 To 2
                    filenotexist(y) = lv_import.Items.Item(x).SubItems.Item(y + 2).Text
                    try2download(y) = _checked(y) And filenotexist(y)
                Next

                downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, lv_import.Items.Item(x).SubItems.Item(1).Text, True, try2download, _language)

                If downloaded(0) Or downloaded(1) Or downloaded(2) Then
                    Log.Info("DVDArt: process plugin - artwork found for: """ & lv_import.Items.Item(x).SubItems.Item(0).Text & """")
                Else
                    Log.Info("DVDArt: process plugin - artwork not found for: """ & lv_import.Items.Item(x).SubItems.Item(0).Text & """")
                End If

                If filenotexist(0) And filenotexist(1) And filenotexist(2) Then
                    SQLcommand.CommandText = "INSERT INTO processed_movies (imdb_id) VALUES(""" & lv_import.Items.Item(x).SubItems.Item(1).Text & """)"
                    SQLcommand.ExecuteNonQuery()
                End If

            Next

            SQLconnect.Close()

        End If

    End Sub

    Private Sub Execute_Missing()

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim parm As Object = "queue"
        Dim filenotexist(2) As Boolean

        ' Read movingpictures database

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info ORDER BY sortby"

        SQLreader = SQLcommand.ExecuteReader()

        lv_import.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                For y = 0 To 2
                    filenotexist(y) = _checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(y, 1) & SQLreader(0) & ".png")
                Next

                If filenotexist(0) Or filenotexist(1) Or filenotexist(2) Then
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))

                    For y = 0 To 2
                        li_import.SubItems.Add(filenotexist(y))
                    Next

                    If Not bw_import.IsBusy Then bw_import.RunWorkerAsync(parm)
                End If

            End If

        End While

        SQLconnect.Close()

        Do While bw_import.IsBusy
            wait(200)
        Loop

        If lv_import.Items.Count > 0 Then
            bw_import.RunWorkerAsync(parm)
        End If

        Using XMLwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))
            XMLwriter.SetValue("Scheduler", "lastrun", Now)
        End Using

    End Sub

    Private Sub Execute_Importer()

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader

        ' Read already processed movies to identify newly imported ones in movingpictures

        Dim x As Integer
        Dim parm As Object = "queue"
        Dim processed_movies() As String

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies ORDER BY imdb_id"

        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_movies(x)
            processed_movies(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_movies(0)

        ' Read movingpictures database

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info ORDER BY sortby"

        SQLreader = SQLcommand.ExecuteReader()

        lv_import.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If Not processed_movies.Contains(SQLreader(0)) Then
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))

                    If Not bw_import.IsBusy Then bw_import.RunWorkerAsync(parm)
                End If

            End If

        End While

        SQLconnect.Close()

        Do While bw_import.IsBusy
            wait(200)
        Loop

        If lv_import.Items.Count > 0 Then
            bw_import.RunWorkerAsync(parm)
        End If

    End Sub

    Private Sub Get_Settings()

        Dim cpu, delay_value, scraping_value, missing_value As String

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            _delay = XMLreader.GetValueAsInt("Settings", "delay", 1)
            delay_value = XMLreader.GetValueAsString("Settings", "delay value", "minutes")
            cpu = XMLreader.GetValueAsString("Settings", "CPU utilisation", "30%")
            _scraping = XMLreader.GetValueAsInt("Settings", "scraping", 15)
            scraping_value = XMLreader.GetValueAsString("Settings", "scraping value", "minutes")
            _missing = XMLreader.GetValueAsInt("Settings", "missing", 0)
            missing_value = XMLreader.GetValueAsString("Settings", "missing value", "disabled")
            _checked(0) = XMLreader.GetValueAsBool("Scraper", "dvdart", False)
            _checked(1) = XMLreader.GetValueAsBool("Scraper", "clearart", False)
            _checked(2) = XMLreader.GetValueAsBool("Scraper", "clearlogo", False)
            _language = XMLreader.GetValueAsString("Scraper", "language", "##")

        End Using

        ' calculate actual delay in seconds

        If delay_value = "minutes" Then
            _delay = _delay * 60
        ElseIf delay_value = "hours" Then
            _delay = _delay * 3600
        End If

        ' calculate scraper polling time in seconds

        If scraping_value = "minutes" Then
            _scraping = _scraping * 60
        ElseIf scraping_value = "hours" Then
            _scraping = _scraping * 3600
        End If

        ' get maximum cpu

        _maxcpu = CUInt(Replace(cpu, "%", Nothing))

        ' calculate missing scraper polling time in hours

        If missing_value = "disabled" Then
            _missing = Nothing
        ElseIf missing_value = "days" Then
            _missing = _missing * 24
        ElseIf missing_value = "weeks" Then
            _missing = _missing * 168
        ElseIf missing_value = "months" Then
            _missing = _missing * 672
        End If

    End Sub

    Public Sub DVDArt_Process()

        Log.Info("DVDArt: process plugin initialisation.")

        If DVDArt_Common.Get_Paths(database, thumbs) Then

            Get_Settings()

            'initialize common variables
            DVDArt_Common.Initialize()

            wait(_delay)

            Do

                If CUInt(Replace(CPU_Usage_Percent(), "%", Nothing)) <= _maxcpu Then
                    Log.Info("DVDArt: process plugin starting background importer.")
                    Execute_Importer()
                Else
                    Log.Info("DVDArt: process delayed as HTPC is too busy.....")
                End If

                ' get when last missing run was effected and if it is due, execute

                Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))
                    _lastrun = XMLreader.GetValueAsString("Settings", "lastrun", Nothing)
                End Using

                If DateDiff(DateInterval.Hour, CDate(_lastrun), Now) > _missing Then
                    Execute_Missing()
                End If

                wait(_scraping)

            Loop

            Log.Info("DVDArt: process plugin ending.")

        Else
            Log.Error("DVDArt: process failed to load database & thumb paths.  Process Aborted!")
        End If

        Return

    End Sub

End Class
