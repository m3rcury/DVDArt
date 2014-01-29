Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library

Imports Microsoft.VisualBasic.FileIO

Imports System.Data.SQLite
Imports System.ComponentModel

Public Class DVDArt_Process

    Private _delay, _scraping, _maxcpu, _missing As Integer
    Private _checked(2, 4), _persons, backgroundscraper As Boolean
    Private _lastrun, _language, _movies, _persons_path, _series, _music As String

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

            For x As Integer = 0 To (lv_import.Items.Count - 1)
                DVDArt_Common.import(database, thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, lv_import.Items.Item(x).SubItems.Item(0).Text, _language, lv_import.Items.Item(x).SubItems.Item(2).Text, Nothing, _checked)
            Next

        End If

        e.Result = "Import Complete"

    End Sub

    Private Sub Execute_Missing()

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim parm As Object = "queue"
        Dim filenotexist(2) As Boolean

        ' Read movingpictures database

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3;Read Only=True;"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info WHERE imdb_id is not Null and title is not Null ORDER BY sortby"

        SQLreader = SQLcommand.ExecuteReader()

        lv_import.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                For y = 0 To 2
                    filenotexist(y) = _checked(0, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & SQLreader(0) & ".png")
                Next

                If filenotexist(0) Or filenotexist(1) Or filenotexist(2) Then
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                    li_import.SubItems.Add("movie")

                    For y = 0 To 2
                        li_import.SubItems.Add(filenotexist(y))
                    Next

                    If Not bw_import.IsBusy Then bw_import.RunWorkerAsync(parm)
                End If

            End If

        End While

        SQLconnect.Close()

        ' Read TVSeries database

        SQLconnect.ConnectionString = "Data Source=" & database & "\TVSeriesDatabase4.db3;Read Only=True;"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT id, pretty_name FROM online_series WHERE id is not Null and pretty_name is not Null ORDER BY sortname"

        SQLreader = SQLcommand.ExecuteReader()

        lv_import.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                For y = 0 To 2
                    filenotexist(y) = _checked(1, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & SQLreader(0) & ".png")
                Next

                If filenotexist(1) Or filenotexist(2) Then
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                    li_import.SubItems.Add("series")

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

        Dim x As Integer = 0
        Dim processed_movies() As String = Nothing

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3;Read Only=True;"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies WHERE imdb_id is not Null ORDER BY imdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_movies(x)
            processed_movies(x) = SQLreader(0)
            x += 1

        End While

        SQLreader.Close()

        If x = 0 Then ReDim Preserve processed_movies(0)

        ' load movies from respective databases

        Dim mymovies As DVDArt_Common.Movies() = DVDArt_Common.loadMovingPictures(database)

        Try
            mymovies = DVDArt_Common.loadMyFilms(mymovies)
        Catch ex As Exception
        End Try

        mymovies.Distinct()

        lv_import.Items.Clear()

        ' process loaded movies

        For i As Integer = 0 To UBound(mymovies)

            If Trim(mymovies(i).imdb_id) <> "" Then

                If Not processed_movies.Contains(mymovies(i).imdb_id) Then
                    Log.Info("DVDArt: new movie - """ & mymovies(i).name & """ found.")
                    li_import = lv_import.Items.Add(mymovies(i).name)
                    li_import.SubItems.Add(mymovies(i).imdb_id)
                    li_import.SubItems.Add("movie")
                End If

            End If

        Next

        ' Read already processed TVSeries to identify newly imported ones in TVSeries

        SQLcommand.CommandText = "SELECT thetvdb_id FROM processed_series WHERE thetvdb_id is not Null ORDER BY thetvdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        Dim processed_series() As String = Nothing

        x = 0

        While SQLreader.Read()

            ReDim Preserve processed_series(x)
            processed_series(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_series(0)

        ' Read tvseries database

        ' load series from respective databases

        Dim myseries As DVDArt_Common.Series() = DVDArt_Common.loadTVSeries(database)

        ' process loaded series

        For i As Integer = 0 To UBound(myseries)

            If Trim(myseries(i).thetvdb_id) <> "" Then

                If Not processed_series.Contains(myseries(i).thetvdb_id) Then
                    Log.Info("DVDArt: new serie - """ & myseries(i).name & """ found.")
                    li_import = lv_import.Items.Add(myseries(i).name)
                    li_import.SubItems.Add(myseries(i).thetvdb_id)
                    li_import.SubItems.Add("series")
                End If

            End If

        Next

        If lv_import.Items.Count > 0 Then bw_import.RunWorkerAsync()

        Do While bw_import.IsBusy
            Log.Info("DVDArt: waiting for importer to import found movies/series.")
            wait(20)
        Loop

    End Sub

    Private Sub getSettings()

        Dim cpu, delay_value, scraping_value, missing_value, xml_version As String

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            xml_version = XMLreader.GetValueAsString("Plugin", "version", "0")
            _delay = XMLreader.GetValueAsInt("Settings", "delay", 1)
            delay_value = XMLreader.GetValueAsString("Settings", "delay value", "minutes")
            backgroundscraper = XMLreader.GetValueAsBool("Settings", "backgroundscraper", True)
            cpu = XMLreader.GetValueAsString("Settings", "CPU utilisation", "30%")
            _scraping = XMLreader.GetValueAsInt("Settings", "scraping", 15)
            scraping_value = XMLreader.GetValueAsString("Settings", "scraping value", "minutes")
            _missing = XMLreader.GetValueAsInt("Settings", "missing", 0)
            missing_value = XMLreader.GetValueAsString("Settings", "missing value", "disabled")

            If xml_version > DVDArt_Common._pre_version Then
                _checked(0, 0) = XMLreader.GetValueAsBool("Scraper Movies", "dvdart", False)
                _checked(0, 1) = XMLreader.GetValueAsBool("Scraper Movies", "clearart", False)
                _checked(0, 2) = XMLreader.GetValueAsBool("Scraper Movies", "clearlogo", False)
                _checked(0, 3) = XMLreader.GetValueAsBool("Scraper Movies", "backdrop", False)
                _checked(0, 4) = XMLreader.GetValueAsBool("Scraper Movies", "cover", False)
                _movies = XMLreader.GetValueAsString("Scraper Movies", "path", thumbs & "\MovingPictures")
                _persons = XMLreader.GetValueAsBool("Scraper Movies", "person", False)
                _persons_path = XMLreader.GetValueAsString("Scraper Movies", "person path", thumbs & "\Actors")
                _checked(1, 1) = XMLreader.GetValueAsBool("Scraper Series", "clearart", False)
                _checked(1, 2) = XMLreader.GetValueAsBool("Scraper Series", "clearlogo", False)
                _series = XMLreader.GetValueAsString("Scraper Series", "path", thumbs & "\TVSeries")
                _checked(2, 0) = XMLreader.GetValueAsBool("Scraper Music", "cdart", False)
                _checked(2, 1) = XMLreader.GetValueAsBool("Scraper Music", "banner", False)
                _checked(2, 2) = XMLreader.GetValueAsBool("Scraper Music", "clearlogo", False)
                _music = XMLreader.GetValueAsString("Scraper Music", "path", thumbs & "\Music")
            Else
                _checked(0, 0) = XMLreader.GetValueAsBool("Scraper", "dvdart", False)
                _checked(0, 1) = XMLreader.GetValueAsBool("Scraper", "clearart", False)
                _checked(0, 2) = XMLreader.GetValueAsBool("Scraper", "clearlogo", False)
            End If

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

            'pre Initialize
            DVDArt_Common.preInitialize()

            'get plugin settings
            getSettings()

            Log.Info("DVDArt: Movies artwork - " & _movies)
            Log.Info("DVDArt: Series artwork - " & _series)
            Log.Info("DVDArt: Music artwork  - " & _music)

            'initialize common variables
            DVDArt_Common.Initialize(database, thumbs, _movies, _series, _music)

            Log.Info("DVDArt: process plugin setting property tags.")

            ' intialize plugin properties
            GUIPropertyManager.SetProperty("#MovingPictures.DVDArt", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(0, 0, 0), Len(DVDArt_Common.folder(0, 0, 0)) - 1))
            GUIPropertyManager.SetProperty("#MovingPictures.ClearArt", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(0, 1, 0), Len(DVDArt_Common.folder(0, 1, 0)) - 1))
            GUIPropertyManager.SetProperty("#MovingPictures.ClearLogo", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(0, 2, 0), Len(DVDArt_Common.folder(0, 2, 0)) - 1))
            GUIPropertyManager.SetProperty("#TVSeries.ClearArt", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(1, 1, 0), Len(DVDArt_Common.folder(1, 1, 0)) - 1))
            GUIPropertyManager.SetProperty("#TVSeries.ClearLogo", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(1, 2, 0), Len(DVDArt_Common.folder(1, 2, 0)) - 1))
            GUIPropertyManager.SetProperty("#Music.CDArt", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(2, 0, 0), Len(DVDArt_Common.folder(2, 0, 0)) - 1))
            GUIPropertyManager.SetProperty("#Music.Banner", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(2, 1, 0), Len(DVDArt_Common.folder(2, 1, 0)) - 1))
            GUIPropertyManager.SetProperty("#Music.ClearLogo", thumbs & Microsoft.VisualBasic.Left(DVDArt_Common.folder(2, 2, 0), Len(DVDArt_Common.folder(2, 2, 0)) - 1))
            GUIPropertyManager.SetProperty("#Person.Thumb", _persons_path)

            If backgroundscraper Then

                'create thumb folder structure
                DVDArt_Common.Create_Folder_Structure(database, thumbs)

                wait(_delay)

                Do

                    If CUInt(Replace(CPU_Usage_Percent(), "%", Nothing)) <= _maxcpu Then
                        Log.Info("DVDArt: process plugin starting.")
                        Execute_Importer()
                    Else
                        Log.Info("DVDArt: process delayed as HTPC is too busy!...")
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
                Log.Info("DVDArt: background scraping disabled.  Plugin ending.")
            End If
        Else
            Log.Error("DVDArt: process failed to load database & thumb paths.  Process Aborted!")
        End If

        Return

    End Sub

End Class
