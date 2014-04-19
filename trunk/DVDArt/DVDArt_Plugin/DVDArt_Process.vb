﻿Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library
Imports MediaPortal.Util

Imports Microsoft.VisualBasic.FileIO

Imports System.Data.SQLite
Imports System.ComponentModel

Public Class DVDArt_Process

    Private _delay, _scraping, _maxcpu, _missing As Integer
    Private _checked(2, 4), _persons, backgroundscraper As Boolean
    Private database, thumbs, _lastrun, _language, _movies, _persons_path, _series, _music As String
    Private l_new_movies As New List(Of String)
    Private lv_import As New ListView
    Private li_import As New ListViewItem

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

    Private Sub Execute_Missing()

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim filenotexist(4) As Boolean

        ' Read movingpictures database

        SQLconnect.ConnectionString = "Data Source=" & database & DVDArt_Common.p_Databases("movingpictures") & ";Read Only=True;"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info WHERE imdb_id is not Null and title is not Null ORDER BY sortby"

        SQLreader = SQLcommand.ExecuteReader()

        lv_import.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                For y = 0 To 4
                    filenotexist(y) = _checked(0, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & SQLreader(0) & ".png")
                Next

                If filenotexist(0) Or filenotexist(1) Or filenotexist(2) Then
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                    li_import.SubItems.Add("movie")

                    For y = 0 To 2
                        li_import.SubItems.Add(filenotexist(y))
                    Next
                End If

            End If

        End While

        SQLconnect.Close()

        ' Read TVSeries database

        SQLconnect.ConnectionString = "Data Source=" & database & DVDArt_Common.p_Databases("tvseries") & ";Read Only=True;"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT id, pretty_name FROM online_series WHERE id is not Null and pretty_name is not Null ORDER BY sortname"

        SQLreader = SQLcommand.ExecuteReader()

        lv_import.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                For y = 1 To 2
                    filenotexist(y) = _checked(1, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & SQLreader(0) & ".png")
                Next

                If filenotexist(1) Or filenotexist(2) Then
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                    li_import.SubItems.Add("series")

                    For y = 1 To 2
                        li_import.SubItems.Add(filenotexist(y))
                    Next
                End If

            End If

        End While

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            DVDArt_Common.logStats("DVDArt: Missing import process started.", "LOG")

            For x = 0 To (lv_import.Items.Count - 1)
                DVDArt_Common.import(database, thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, lv_import.Items.Item(x).SubItems.Item(0).Text, _language, lv_import.Items.Item(x).SubItems.Item(2).Text, Nothing, _checked)
            Next

            DVDArt_Common.logStats("DVDArt: Missing import process complete.", "LOG")
        End If

        Using XMLwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))
            XMLwriter.SetValue("Scheduler", "lastrun", Now)
        End Using

    End Sub

    Private Sub Execute_Importer()

        Dim x As Integer = 0
        Dim enabled As Boolean = False

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader

        SQLconnect.ConnectionString = "Data Source=" & database & DVDArt_Common.p_Databases("dvdart") & ";Read Only=True;"
        SQLconnect.Open()

        ' Read already processed movies to identify newly imported ones in movingpictures

        If IO.File.Exists(database & DVDArt_Common.p_Databases("movingpictures")) Or IO.File.Exists(DVDArt_Common.p_Databases("myfilms")) Or IO.File.Exists(database & DVDArt_Common.p_Databases("myvideos")) Then

            For x = 0 To UBound(_checked, 2)
                enabled = enabled Or _checked(0, x)
            Next

            If enabled Then

                DVDArt_Common.logStats("DVArt: Loading already processed movies.", "DEBUG")

                Dim processed_movies() As String = Nothing

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

                DVDArt_Common.logStats("DVArt: Loading Movie List started.", "DEBUG")

                Dim mymovies As DVDArt_Common.Movies() = DVDArt_Common.loadMovingPictures(database)

                Try
                    mymovies = DVDArt_Common.loadMyFilms(mymovies)
                Catch ex As Exception
                End Try

                Try
                    mymovies = DVDArt_Common.loadMyVideos(database, mymovies)
                Catch ex As Exception
                End Try

                mymovies.Distinct()

                lv_import.Items.Clear()

                ' process loaded movies

                For i As Integer = 0 To UBound(mymovies)

                    If Trim(mymovies(i).imdb_id) <> "" Then

                        If Not processed_movies.Contains(mymovies(i).imdb_id) Then
                            DVDArt_Common.logStats("DVDArt: new movie - """ & mymovies(i).name & """ found.", "LOG")
                            li_import = lv_import.Items.Add(mymovies(i).name)
                            li_import.SubItems.Add(mymovies(i).imdb_id)
                            li_import.SubItems.Add("movie")
                            l_new_movies.Add(mymovies(i).imdb_id)
                        End If

                    End If

                Next

                DVDArt_Common.logStats("DVArt: Loading Movie List complete.", "DEBUG")

                ' Check for any new person images to download

                DVDArt_Common.logStats("DVArt: Loading Persons List started.", "DEBUG")

                Dim mypersons As SortedList = DVDArt_Common.loadMovingPicturesPersons(database)

                Try
                    mypersons = DVDArt_Common.loadMyFilmsPersons(mypersons)
                Catch ex As Exception
                End Try

                Try
                    mypersons = DVDArt_Common.loadMyVideosPersons(database, mypersons)
                Catch ex As Exception
                End Try

                For i As Integer = 0 To mypersons.Count - 1

                    If Not IO.File.Exists(_persons_path & Utils.MakeFileName(mypersons.GetKey(i)) & ".png") Then
                        If l_new_movies.Contains(mypersons.GetByIndex(i)) Then
                            DVDArt_Common.logStats("DVDArt: new person - """ & mypersons.GetKey(i) & """ found.", "LOG")
                            li_import = lv_import.Items.Add(mypersons.GetKey(i))
                            li_import.SubItems.Add("")
                            li_import.SubItems.Add("person")
                        End If
                    End If

                Next

                DVDArt_Common.logStats("DVArt: Loading Persons List complete.", "DEBUG")

            End If

        End If

        ' Read already processed TVSeries to identify newly imported ones in TVSeries

        If IO.File.Exists(database & DVDArt_Common.p_Databases("tvseries")) Then

            enabled = False

            For x = 1 To 2
                enabled = enabled Or _checked(1, x)
            Next

            If enabled Then

                DVDArt_Common.logStats("DVArt: Loading Series List started.", "DEBUG")

                SQLcommand.CommandText = "SELECT thetvdb_id FROM processed_series WHERE thetvdb_id is not Null ORDER BY thetvdb_id"
                SQLreader = SQLcommand.ExecuteReader()

                Dim processed_series() As String = Nothing

                x = 0

                While SQLreader.Read()

                    ReDim Preserve processed_series(x)
                    processed_series(x) = SQLreader(0)
                    x += 1

                End While

                SQLreader.Close()

                If x = 0 Then ReDim Preserve processed_series(0)

                ' load series from respective databases

                Dim myseries As DVDArt_Common.Series() = DVDArt_Common.loadTVSeries(database)

                ' process loaded series

                For i As Integer = 0 To UBound(myseries)

                    If Trim(myseries(i).thetvdb_id) <> "" Then

                        If Not processed_series.Contains(myseries(i).thetvdb_id) Then
                            DVDArt_Common.logStats("DVDArt: new serie - """ & myseries(i).name & """ found.", "LOG")
                            li_import = lv_import.Items.Add(myseries(i).name)
                            li_import.SubItems.Add(myseries(i).thetvdb_id)
                            li_import.SubItems.Add("series")
                        End If

                    End If

                Next

                DVDArt_Common.logStats("DVArt: Loading Series List complete.", "DEBUG")

            End If

        End If

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            DVDArt_Common.logStats("DVDArt: Import process started.", "LOG")

            For x = 0 To (lv_import.Items.Count - 1)
                DVDArt_Common.import(database, thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, lv_import.Items.Item(x).SubItems.Item(0).Text, _language, lv_import.Items.Item(x).SubItems.Item(2).Text, Nothing, _checked)
            Next

            DVDArt_Common.logStats("DVDArt: Import process complete.", "LOG")
        End If

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

        DVDArt_Common.logStats("DVDArt: process plugin initialisation.", "INFO")

        If DVDArt_Common.Get_Paths(database, thumbs) Then

            'pre Initialize
            DVDArt_Common.preInitialize()

            'get plugin settings
            getSettings()

            'initialize common variables
            DVDArt_Common.Initialize(database, thumbs, _movies, _series, _music, _persons_path)

            DVDArt_Common.logStats("DVDArt: process plugin setting property tags.", "INFO")

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

                wait(_delay)

                Do

                    If CUInt(Replace(CPU_Usage_Percent(), "%", Nothing)) <= _maxcpu Then
                        DVDArt_Common.logStats("DVDArt: process plugin starting.", "INFO")
                        Execute_Importer()
                    Else
                        DVDArt_Common.logStats("DVDArt: process delayed as HTPC is too busy!...", "INFO")
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

                DVDArt_Common.logStats("DVDArt: process plugin ending.", "INFO")

            Else
                DVDArt_Common.logStats("DVDArt: background scraping disabled.  Plugin ending.", "INFO")
            End If
        Else
            DVDArt_Common.logStats("DVDArt: process failed to load database & thumb paths.  Process Aborted!", "ERROR")
        End If

        Return

    End Sub

End Class
