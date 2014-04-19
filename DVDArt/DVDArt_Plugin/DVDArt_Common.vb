Imports Microsoft.VisualBasic.FileIO

Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library
Imports MediaPortal.Util

Imports System.Data.SQLite
Imports System.ComponentModel
Imports System.IO
Imports System.Text
Imports System.Reflection

Imports MyFilmsPlugin.DataBase
Imports MyFilmsPlugin.MyFilms.Utils

Public Class DVDArt_Common

    Public Structure Movies
        Dim imdb_id As String
        Dim name As String
        Dim backdrop As String
        Dim cover As String
    End Structure

    Public Structure Series
        Dim thetvdb_id As String
        Dim name As String
    End Structure

    Public Shared _version, folder(2, 4, 1), lang(4), langcode(4), _coversize As String
    Public Shared WithEvents bw_download0, bw_download1, bw_download2, bw_download3, bw_download4, bw_download5, bw_download6, bw_download7, bw_download8, bw_download9 As New BackgroundWorker
    Public Shared _temp As String = Environ("temp")

    Dim debug As Boolean

    Public Shared ReadOnly Property p_Databases(ByVal item As String) As String
        Get
            Select Case item
                Case "dvdart"
                    Return "\dvdart.db3"
                Case "movingpictures"
                    Return "\movingpictures.db3"
                Case "myfilms"
                    Return Config.GetFile(Config.Dir.Config, "MyFilms.xml")
                Case "tvseries"
                    Return "\TVSeriesDatabase4.db3"
                Case "myvideos"
                    Return "\VideoDatabaseV5.db3"
                Case "music"
                    Return "\MusicDatabaseV13.db3"
                Case Else
                    Return Nothing
            End Select
        End Get
    End Property

    Public Shared ReadOnly Property p_Debug As Boolean
        Get
            Dim debug As Boolean
            Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))
                debug = XMLreader.GetValueAsBool("Settings", "debug", False)
            End Using
            Return debug
        End Get
    End Property

    Public Shared Sub wait(ByVal milliseconds As Long)
        System.Threading.Thread.Sleep(milliseconds)
    End Sub

    Public Shared Sub logStats(ByVal message As String, ByVal msgtype As String)

        If msgtype = "DEBUG" And p_Debug = False Then Exit Sub

        Select Case msgtype
            Case "INFO"
                Log.Info(message)
            Case "ERROR"
                Log.Error(message)
        End Select

        Dim file As String = Config.GetFile(Config.Dir.Log, "dvdart_plugin.log")
        Dim fhandle As System.IO.FileStream

        If msgtype = "LOG" Then msgtype = "INFO"

        Dim info() As Byte = New System.Text.UTF8Encoding(True).GetBytes(DateTime.Now & " - [" & msgtype & "] " & message & vbCrLf)

        Try
            fhandle = IO.File.Open(file, IO.FileMode.Append, IO.FileAccess.Write, IO.FileShare.ReadWrite)
            fhandle.Write(info, 0, info.Length)
            fhandle.Close()
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Function loadMovingPictures(ByVal database As String) As Array

        If Not IO.File.Exists(database & p_Databases("movingpictures")) Then Return Nothing

        Dim movielist() As Movies = Nothing

        Try
            Dim x As Integer = -1
            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
            Dim SQLreader As SQLiteDataReader

            SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("movingpictures") & ";Read Only=True;"

            SQLconnect.Open()
            SQLcommand.CommandText = "SELECT imdb_id, title, backdropfullpath, coverfullpath FROM movie_info WHERE imdb_id IS NOT NULL and title IS NOT NULL ORDER BY sortby"
            SQLreader = SQLcommand.ExecuteReader()

            While SQLreader.Read()

                If Trim(SQLreader(0)) <> "" Then
                    x += 1
                    ReDim Preserve movielist(x)
                    movielist(x).imdb_id = SQLreader(0)
                    movielist(x).name = SQLreader(1)
                    movielist(x).backdrop = SQLreader(2)
                    movielist(x).cover = SQLreader(3)
                End If

            End While

            SQLconnect.Close()
        Catch ex As Exception
        End Try

        Return movielist

    End Function

    Public Shared Function loadMyFilms(ByVal movielist As Movies()) As Array

        If Not IO.File.Exists(p_Databases("myfilms")) Then Return movielist

        Dim x As Integer = -1
        Dim dataExport As AntMovieCatalog = New AntMovieCatalog()

        If movielist Is Nothing Then
            ReDim movielist(0)
        Else
            x = UBound(movielist)
        End If

        Dim lookupbyIMDB = movielist.ToLookup(Function(p) p.imdb_id)

        Using XmlConfig As XmlSettings = New XmlSettings(p_Databases("myfilms"))

            Dim MesFilms_nb_config As Integer = XmlConfig.ReadXmlConfig("MyFilms", "MyFilms", "NbConfig", -1)
            Dim mf_configs As ArrayList = New ArrayList()

            For i As Integer = 0 To MesFilms_nb_config
                mf_configs.Add(XmlConfig.ReadXmlConfig("MyFilms", "MyFilms", "ConfigName" & i.ToString, String.Empty))
            Next

            For Each mf_config As String In mf_configs

                Dim Catalog As String = XmlConfig.ReadXmlConfig("MyFilms", mf_config, "AntCatalog", String.Empty)

                If IO.File.Exists(Catalog) Then

                    dataExport.ReadXml(Catalog)

                    Dim mfmovies As DataRow() = dataExport.Tables("Movie").Select

                    For Each movie As DataRow In mfmovies

                        Dim y As Integer = 0

                        If Not IsDBNull(movie("IMDB_id")) Then

                            If Not movie(("IMDB_id")) = String.Empty Then

                                For Each film In lookupbyIMDB(movie("IMDB_id"))
                                    y += 1
                                    Exit For
                                Next

                                If y = 0 Then

                                    x += 1
                                    ReDim Preserve movielist(x)
                                    Try
                                        movielist(x).imdb_id = movie("IMDB_id")
                                    Catch ex As Exception
                                        movielist(x).imdb_id = String.Empty
                                    End Try
                                    Try
                                        movielist(x).name = movie("OriginalTitle")
                                    Catch ex As Exception
                                        movielist(x).name = String.Empty
                                    End Try
                                    Try
                                        movielist(x).backdrop = movie("Fanart")
                                    Catch ex As Exception
                                        movielist(x).backdrop = String.Empty
                                    End Try
                                    Try
                                        movielist(x).cover = movie("Picture")
                                    Catch ex As Exception
                                        movielist(x).cover = String.Empty
                                    End Try
                                End If

                            End If

                        End If

                    Next

                End If

            Next

        End Using

        Return movielist

    End Function

    Public Shared Function loadMyVideos(ByVal database As String, ByVal movielist As Movies()) As Array

        If Not IO.File.Exists(database & p_Databases("myvideos")) Then Return movielist

        Dim x As Integer = -1
        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader

        If movielist Is Nothing Then
            ReDim movielist(0)
        Else
            x = UBound(movielist)
        End If

        Dim lookupbyIMDB = movielist.ToLookup(Function(p) p.imdb_id)

        SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("myvideos") & ";Read Only=True;"

        SQLconnect.Open()
        SQLcommand.CommandText = "SELECT IMDBID, strTitle, strFanartURL, strPictureURL FROM movieinfo WHERE IMDBID IS NOT NULL and IMDBID <> 'unknown' and strTitle IS NOT NULL ORDER BY strSortTitle"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                Dim y As Integer = 0

                For Each film In lookupbyIMDB(SQLreader(0))
                    y += 1
                    Exit For
                Next

                If y = 0 Then
                    x += 1
                    ReDim Preserve movielist(x)
                    movielist(x).imdb_id = SQLreader(0)
                    movielist(x).name = SQLreader(1)
                    movielist(x).backdrop = SQLreader(2)
                    movielist(x).cover = SQLreader(3)
                End If
            End If

        End While

        SQLconnect.Close()

        Return movielist

    End Function

    Public Shared Function loadTVSeries(ByVal database As String) As Array

        If Not IO.File.Exists(database & p_Databases("tvseries")) Then Return Nothing

        Dim serielist() As Series = Nothing
        Dim x As Integer = -1
        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader

        SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("tvseries") & ";Read Only=True;"

        SQLconnect.Open()
        SQLcommand.CommandText = "SELECT id, pretty_name FROM online_series WHERE id IS NOT NULL and pretty_name IS NOT NULL ORDER BY sortname"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then
                x += 1
                ReDim Preserve serielist(x)
                serielist(x).thetvdb_id = SQLreader(0)
                serielist(x).name = SQLreader(1)
            End If

        End While

        SQLconnect.Close()

        Return serielist

    End Function

    Public Shared Function loadMovingPicturesPersons(ByVal database As String) As SortedList

        If Not IO.File.Exists(database & p_Databases("movingpictures")) Then Return Nothing

        Dim personlist As New SortedList

        Try
            Dim persons As Array
            Dim x As Integer = -1
            Dim y As Integer
            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
            Dim SQLreader As SQLiteDataReader

            SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("movingpictures") & ";Read Only=True;"

            SQLconnect.Open()
            SQLcommand.CommandText = "SELECT directors||writers||actors, imdb_id FROM movie_info WHERE directors||writers||actors IS NOT NULL AND imdb_id IS NOT NULL"
            SQLreader = SQLcommand.ExecuteReader()

            While SQLreader.Read()

                If Trim(SQLreader(0)) <> "" Then

                    persons = Split(Mid(SQLreader(0), 2, Len(SQLreader(0)) - 2).Replace("||", "|"), "|")

                    For y = 0 To UBound(persons)

                        If Not String.IsNullOrEmpty(Trim(persons(y))) Then
                            If Not personlist.ContainsKey(persons(y)) Then personlist.Add(persons(y), SQLreader(1))
                        End If

                    Next

                End If

            End While

            SQLconnect.Close()
        Catch ex As Exception
        End Try

        Return personlist

    End Function

    Public Shared Function loadMyFilmsPersons(ByVal personlist As SortedList) As SortedList

        If Not IO.File.Exists(p_Databases("myfilms")) Then Return personlist

        Dim x As Integer = -1
        Dim y As Integer
        Dim persons As Array
        Dim dataExport As AntMovieCatalog = New AntMovieCatalog()

        If personlist IsNot Nothing Then x = personlist.Count - 1

        Using XmlConfig As XmlSettings = New XmlSettings(p_Databases("myfilms"))

            Dim MesFilms_nb_config As Integer = XmlConfig.ReadXmlConfig("MyFilms", "MyFilms", "NbConfig", -1)
            Dim mf_configs As ArrayList = New ArrayList()

            For i As Integer = 0 To MesFilms_nb_config
                mf_configs.Add(XmlConfig.ReadXmlConfig("MyFilms", "MyFilms", "ConfigName" & i.ToString, String.Empty))
            Next

            For Each mf_config As String In mf_configs

                Dim Catalog As String = XmlConfig.ReadXmlConfig("MyFilms", mf_config, "AntCatalog", String.Empty)

                If IO.File.Exists(Catalog) Then

                    dataExport.ReadXml(Catalog)

                    Dim mfmovies As DataRow() = dataExport.Tables("Movie").Select

                    For Each movie As DataRow In mfmovies

                        If Not IsDBNull(movie("Persons")) Then

                            If Not movie(("Persons")) = String.Empty Then

                                persons = Split(movie(("Persons")), ",")

                                For y = 0 To UBound(persons)

                                    If Not String.IsNullOrEmpty(Trim(persons(y))) Then
                                        If Not personlist.ContainsKey(persons(y)) Then personlist.Add(persons(y), movie("IMDB_id"))
                                    End If

                                Next

                            End If
                        End If
                    Next

                End If

            Next

        End Using

        Return personlist

    End Function

    Public Shared Function loadMyVideosPersons(ByVal database As String, ByVal personlist As SortedList) As SortedList

        If Not IO.File.Exists(database & p_Databases("myvideos")) Then Return personlist

        Dim persons As Array
        Dim x As Integer = -1
        Dim y As Integer
        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader

        If personlist IsNot Nothing Then x = personlist.Count - 1

        SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("myvideos") & ";Read Only=True;"

        SQLconnect.Open()
        SQLcommand.CommandText = "SELECT strDirector||REPLACE(strCast, CHAR(10), '|'), IMDBID FROM movieinfo WHERE strDirector||REPLACE(strCast, CHAR(10), '|') IS NOT NULL AND IMDBID IS NOT NULL"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                persons = Split(Mid(SQLreader(0), 2, Len(SQLreader(0)) - 2).Replace("||", "|"), "|")

                For y = 0 To UBound(persons)

                    If Not String.IsNullOrEmpty(Trim(persons(y))) Then
                        If Not personlist.ContainsKey(persons(y)) Then personlist.Add(persons(y), SQLreader(1))
                    End If

                Next

            End If

        End While

        SQLconnect.Close()

        Return personlist

    End Function

    Public Shared Function get_MBID(ByVal database As String, ByVal album As String, ByVal artist As String) As String

        Dim MBID As String = Nothing

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader

        SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("dvdart") & ";Read Only=True;"
        SQLconnect.Open()
        SQLcommand.CommandText = "SELECT MBID FROM processed_artist WHERE LOWER(artist) = """ & LCase(artist) & """"
        SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)

        MBID = SQLreader(0)

        SQLconnect.Close()

        If MBID = Nothing Then MBID = Last_fm(artist, "artist")
        If MBID = Nothing Then MBID = theAudioDB(artist, "artist")
        If MBID = Nothing Then MBID = MusicBrainz(artist, "artist")

        Return MBID

    End Function

    Public Shared Function get_Artist_MBID(ByVal artist As String) As String

        Dim MBID As String = Nothing

        MBID = Last_fm(artist, "artist")

        If MBID = Nothing Then MBID = theAudioDB(artist, "artist")
        If MBID = Nothing Then MBID = MusicBrainz(artist, "artist")

        Return MBID

    End Function

    Public Shared Function Last_fm(ByVal artist As String, ByVal mode As String, Optional ByVal search As String = Nothing) As String

        Dim WebClient As New System.Net.WebClient
        Dim startp, endp, len As Integer
        Dim Lastfm_XML As String
        Dim url As String = Nothing
        Dim MBID As String = Nothing

        If mode = "artist" Then
            url = Uri.EscapeUriString("http://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist=" & artist & "&lang=en&autocorrect=1&api_key=80c3a2a37a2b6666b38c107759645e48&format=json")
        ElseIf mode = "track" Then
            url = Uri.EscapeUriString("http://ws.audioscrobbler.com/2.0/?method=track.getInfo&autocorrect=1&api_key=80c3a2a37a2b6666b38c107759645e48&artist=" & artist & "&track=" & search.Replace(" ", "%20") & "&format=json")
        ElseIf mode = "album" Then
            url = Uri.EscapeUriString("http://ws.audioscrobbler.com/2.0/?method=album.getinfo&lang=en&autocorrect=1&api_key=80c3a2a37a2b6666b38c107759645e48&artist=" & artist & "&album=" & search & "&format=json")
        Else
            Return Nothing
        End If

        Try

            Lastfm_XML = WebClient.DownloadString(url)

            ' if a match is found
            If Not Lastfm_XML.Contains("error") Then

                If mode = "artist" Or mode = "album" Then
                    startp = InStr(Lastfm_XML, "mbid"":") + 7
                ElseIf mode = "track" Then
                    startp = InStr(Lastfm_XML, "title"":")
                    startp = InStr(startp, Lastfm_XML, "mbid"":") + 7
                End If

                endp = InStr(startp, Lastfm_XML, """,")
                len = endp - startp

                If len > 0 Then
                    MBID = Mid(Lastfm_XML, startp, len)
                Else
                    MBID = Nothing
                End If

            Else
                MBID = Nothing
            End If

        Catch ex As Exception
            MBID = Nothing
        End Try

        Return MBID

    End Function

    Public Shared Function theAudioDB(ByVal artist As String, ByVal mode As String, Optional ByVal search As String = Nothing) As String

        Dim WebClient As New System.Net.WebClient
        Dim startp, endp, len As Integer
        Dim theAudioDB_XML As String
        Dim url As String = Nothing
        Dim MBID As String = Nothing

        If mode = "artist" Then
            url = Uri.EscapeUriString("http://www.theaudiodb.com/api/v1/json/58424d43204d6564696120/search.php?s=" & artist)
        ElseIf mode = "track" Then
            url = Uri.EscapeUriString("http://www.theaudiodb.com/api/v1/json/58424d43204d6564696120/search.php?s=" & artist & "&t=" & search)
        ElseIf mode = "album" Then
            url = Uri.EscapeUriString("http://www.theaudiodb.com/api/v1/json/58424d43204d6564696120/search.php?s=" & artist & "&a=" & search)
        Else
            Return Nothing
        End If

        Try

            theAudioDB_XML = WebClient.DownloadString(url)

            ' if a match is found
            If Not theAudioDB_XML.Contains(":null}") Then

                startp = InStr(theAudioDB_XML, "strMusicBrainzID"":") + 19

                endp = InStr(startp, theAudioDB_XML, """,")
                len = endp - startp

                If len > 0 Then
                    MBID = Mid(theAudioDB_XML, startp, len)
                Else
                    MBID = Nothing
                End If

            Else
                MBID = Nothing
            End If

        Catch ex As Exception
            MBID = Nothing
        End Try

        Return MBID

    End Function

    Public Shared Function MusicBrainz(ByVal artist As String, ByVal mode As String, Optional ByVal track As String = Nothing) As String

        Dim WebClient As New System.Net.WebClient
        Dim startp, endp, len As Integer
        Dim MBz_XML As String
        Dim url As String = Nothing
        Dim MBID As String = Nothing

        If mode = "artist" Then
            url = Uri.EscapeUriString("http://www.musicbrainz.org/ws/2/artist/?query=" & artist)
        ElseIf mode = "track" Then
            url = Uri.EscapeUriString("http://www.musicbrainz.org/ws/2/release?artist=" & artist)
        ElseIf mode = "album" Then
            url = Uri.EscapeUriString("http://www.musicbrainz.org/ws/2/release?artist=" & artist)
        Else
            Return Nothing
        End If

        Try

            MBz_XML = WebClient.DownloadString(url)

            ' if a match is found
            If mode = "artist" Then

                If MBz_XML.Contains("ext:score=""100""") Then
                    startp = InStr(MBz_XML, "<artist id=") + 12
                    endp = InStr(startp, MBz_XML, """")
                    len = endp - startp
                    If len > 0 Then
                        MBID = Mid(MBz_XML, startp, len)
                    Else
                        MBID = Nothing
                    End If
                Else
                    MBID = Nothing
                End If

            Else

                If MBz_XML.Contains("<title>" & track & "</title>") Then
                    startp = InStr(MBz_XML, "<release id=") + 13
                    endp = InStr(startp, MBz_XML, """")
                    len = endp - startp
                    MBID = Mid(MBz_XML, startp, len)
                Else
                    MBID = Nothing
                End If

            End If

        Catch ex As Exception
            MBID = Nothing
        End Try

        Return MBID

    End Function

    Public Shared Function parse(ByVal jsonresponse As String, ByVal type As String, ByVal id As String, Optional ByVal language As String = "##") As Array

        Dim parseHD_l As String = Nothing
        Dim parseHD_c As String = Nothing
        Dim parse_bd As String = Nothing
        Dim parse_ps As String = Nothing
        Dim details(9, 0), returndetails(9, 0), parsestring(4), keyword(8) As String
        Dim starting(4), startHD_l, startHD_c, start_bd, start_ps, startp, endp, len, x, y, i, j As Integer
        Dim invalidimage As String = "http://assets.fanart.tv/fanart/movies/0/movieposter/-5278db34067a7.jpg"

        logStats("DVDArt: " & type & " JSON parsing for " & id & " started.", "LOG")

        If type = "movie" Then
            keyword = {"""hdmovielogo"":", """hdmovieclearart"":", """backdrops"":", "posters"":", """moviedisc"":", """movieart"":", """movielogo"":", """moviebackground"":", "movieposter"":"}
        ElseIf type = "series" Then
            keyword = {"""hdtvlogo"":", """hdclearart"":", "**n/a**", "**n/a**", "**n/a**", """clearart"":", """clearlogo"":", "**n/a**", "**n/a**"}
        ElseIf type = "artist" Then
            keyword = {"""hdmusiclogo"":", "**n/a**", "**n/a**", "**n/a**", "**n/a**", """musicbanner"":", """musiclogo"":", "**n/a**", "**n/a**"}
        ElseIf type = "music" Then
            keyword = {"**n/a**", "**n/a**", "**n/a**", "**n/a**", """cdart"":", "**n/a**", "**n/a**", "**n/a**", "**n/a**"}
        End If

        ' check if there are HD logos and if yes, store in a temporary variable to later on merge with movielogos

        startHD_l = InStr(jsonresponse, keyword(0))
        startHD_c = InStr(jsonresponse, keyword(1))
        start_bd = InStr(jsonresponse, keyword(2))
        start_ps = InStr(jsonresponse, keyword(3))

        If startHD_l > 0 Then
            parseHD_l = Mid(jsonresponse, startHD_l, InStr(startHD_l, jsonresponse, "]") - startHD_l)
        End If

        If startHD_c > 0 Then
            parseHD_c = Mid(jsonresponse, startHD_c, InStr(startHD_c, jsonresponse, "]") - startHD_c)
        End If

        If start_bd > 0 Then
            parse_bd = Mid(jsonresponse, start_bd, InStr(start_bd, jsonresponse, "]") - start_bd)
        End If

        If start_ps > 0 Then
            parse_ps = Mid(jsonresponse, start_ps, InStr(start_ps, jsonresponse, "]") - start_ps)
        End If

        ' find the starting place of the respective sections

        For i = 0 To starting.Count - 1
            starting(i) = InStr(jsonresponse, keyword(i + 4))
        Next

        ' split the jsonresponse to the respective sections

        For i = 0 To parsestring.Count - 1
            If starting(i) > 0 Then
                parsestring(i) = Mid(jsonresponse, starting(i), InStr(starting(i), jsonresponse, "]") - starting(i)).Replace(invalidimage, "")
            End If
        Next

        ' if there are HD images, merge with SD images

        If startHD_l > 0 Then
            parsestring(2) = Trim(parsestring(2)) & parseHD_l.Replace(keyword(0), keyword(6))
            If starting(2) = 0 Then starting(2) = startHD_l
        End If

        If startHD_c > 0 Then
            parsestring(1) = Trim(parsestring(1)) & parseHD_c.Replace(keyword(1), keyword(5))
            If starting(1) = 0 Then starting(1) = startHD_c
        End If

        If start_bd > 0 Then
            parsestring(3) = Trim(parsestring(3)) & parse_bd.Replace(keyword(2), keyword(7))
            If starting(3) = 0 Then starting(3) = start_bd
        End If

        If start_ps > 0 Then
            parsestring(4) = Trim(parsestring(4)) & parse_ps.Replace(keyword(3), keyword(8))
            If starting(4) = 0 Then starting(4) = start_ps
        End If

        For i = 0 To starting.Count - 1

            If starting(i) > 0 Then

                If x > y Then y = x

                x = 0
                j = i * 2
                startp = 1

                Do Until startp = 0

                    startp = InStr(startp, parsestring(i), "id"":")

                    If startp > 0 Then

                        startp = InStr(startp, parsestring(i), "url"":")

                        If startp > 0 Then

                            If x >= y Then ReDim Preserve details(9, x)

                            startp += 6
                            endp = InStr(startp, parsestring(i), """,")
                            len = endp - startp

                            'url
                            details(j, x) = Mid(parsestring(i), startp, len).Replace("\", "")

                            startp = InStr(endp, parsestring(i), "lang"":")

                            If startp > 0 Then

                                endp = InStr(startp, parsestring(i), """,")
                                len = endp - startp

                                'language
                                If InStr(details(j, x), "/hd") Then
                                    details(j + 1, x) = "HD - " & UCase(Mid(parsestring(i), startp, len).Replace("""", ""))
                                Else
                                    details(j + 1, x) = UCase(Mid(parsestring(i), startp, len).Replace("""", ""))
                                End If

                                startp = InStr(endp, parsestring(i), "disc_type"":")

                                If startp > 0 Then

                                    endp = InStr(startp, parsestring(i), """}")
                                    len = endp - startp

                                    'disk type
                                    details(j + 1, x) = Trim(details(j + 1, x)) & " - " & UCase(Mid(parsestring(i), startp, len).Replace("""", ""))
                                    details(j + 1, x) = details(j + 1, x).Replace("_", " ")

                                End If

                            End If

                            x += 1

                            startp = endp

                        Else
                            Exit Do
                        End If
                    Else
                        Exit Do
                    End If

                Loop

            End If

        Next

        If language <> "##" Then

            Dim size_array As Boolean

            j = -1

            For x = 0 To UBound(details, 2)

                size_array = True

                For y = 1 To UBound(details, 1) Step 2

                    If InStr(details(y, x), "LANG:" & language) > 0 Or InStr(details(y, x), "LANG:EN") > 0 Or (details(y - 1, x) <> Nothing And details(y, x) = Nothing) Then

                        If size_array Then
                            j += 1
                            ReDim Preserve returndetails(UBound(details, 1), j)
                            size_array = False
                        End If

                        returndetails(y - 1, j) = details(y - 1, x)
                        returndetails(y, j) = details(y, x)

                    End If

                Next
            Next

        Else
            returndetails = details
        End If

        logStats("DVDArt: " & type & " JSON parsing for " & id & " complete.", "LOG")

        Return returndetails

    End Function

    Public Shared Function parse_music(ByVal jsonresponse As String, ByVal album As String) As Array

        Dim details(5, 0), parsestring As String
        Dim startp, endp, len, x, y, j As Integer

        ' find the starting place of the respective sections

        startp = InStr(jsonresponse, """cdart"":")

        If startp > 0 Then

            If x > y Then y = x

            x = 0
            j = 0

            Do Until startp = 0

                endp = InStr(startp, jsonresponse, "]") - startp + 1
                parsestring = Mid(jsonresponse, startp, endp)
                jsonresponse = Right(jsonresponse, Microsoft.VisualBasic.Len(jsonresponse) - startp + 1)

                startp = InStr(parsestring, "id"":")

                If startp > 0 Then

                    startp = InStr(startp, parsestring, "url"":")

                    If startp > 0 Then

                        startp += 6
                        endp = InStr(startp, parsestring, """,")
                        len = endp - startp

                        'url
                        If Mid(parsestring, startp, len).Contains("/cdart/" & album) Then

                            If x >= y Then ReDim Preserve details(5, x)

                            details(j, x) = Mid(parsestring, startp, len).Replace("\", "")

                            x += 1
                        End If

                        startp = InStr(InStr(jsonresponse, "]"), jsonresponse, "cdart"":")

                    Else
                        Exit Do
                    End If
                Else
                    Exit Do
                End If

            Loop

        End If

        Return details

    End Function

    Public Shared Function JSON_request(ByVal id As String, ByVal type As String, ByVal nbrimages As String) As String

        Dim downloaded As String = Fanart_tv(id, type, nbrimages)

        If type = "movie" Then downloaded += theMovieDB(id)

        Return downloaded

    End Function

    Public Shared Function Fanart_tv(ByVal id As String, ByVal type As String, ByVal nbrimages As String) As String

        Dim WebClient As New System.Net.WebClient
        Dim apikey As String = "bfd6e4e0d4e71237f784b70fc43f8269"

        Dim url As String = "http://fanart.tv/webservice/" & type & "/" & apikey & "/" & id & "/json/all/1/" & nbrimages

        Return WebClient.DownloadString(url)

    End Function

    Public Shared Function theMovieDB(ByVal id As String) As String

        Dim apikey As String = "cc25933c4094ca50635f94574491f320"

        Dim url As String = "http://api.themoviedb.org/3/movie/" & id & "/images?api_key=" & apikey

        Dim WebClient As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
        WebClient.Accept = "application/json"

        Dim response As System.Net.HttpWebResponse = WebClient.GetResponse()
        Dim receiveStream As Stream = response.GetResponseStream()
        Dim readStream As New StreamReader(receiveStream, Encoding.UTF8)
        Dim downstring As String = readStream.ReadToEnd()
        response.Close()
        readStream.Close()

        Return downstring.Replace("/", "http://d3gtl9l2a4fn1j.cloudfront.net/t/p/w1920/").Replace("file_path", "id"":,""url")

    End Function

    Public Shared Function htbackdrops(ByVal id As String) As String

        Dim apikey As String = "02274c29b2cc898a726664b96dcc0e76"

        Dim url As String = "http://htbackdrops.com/api/" & apikey & "/searchXML?" & id

        Dim WebClient As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
        WebClient.Accept = "application/json"

        Dim response As System.Net.HttpWebResponse = WebClient.GetResponse()
        Dim receiveStream As Stream = response.GetResponseStream()
        Dim readStream As New StreamReader(receiveStream, Encoding.UTF8)
        Dim downstring As String = readStream.ReadToEnd()
        response.Close()
        readStream.Close()

        Return downstring.Replace("/", "http://htbackdrops.com/api/" & apikey & "/download/").Replace("file_path", "id"":,""url")

    End Function

    Private Shared Function cotainsNULL(ByVal s As String) As Boolean
        Return s.Contains("null}")
    End Function

    Public Shared Function getImagePath(ByVal database As String, ByVal imdb_id As String, ByVal field As String) As String

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim imagepath As String = String.Empty

        If IO.File.Exists(database & p_Databases("movingpictures")) Then
            Try
                SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("movingpictures") & ";Read Only=True;"
                SQLconnect.Open()
                SQLcommand.CommandText = "SELECT " & field & "fullpath FROM movie_info WHERE imdb_id = '" & imdb_id & "'"
                SQLreader = SQLcommand.ExecuteReader()
                SQLreader.Read()

                imagepath = SQLreader(0)
            Catch ex As Exception
                imagepath = String.Empty
            End Try

            SQLconnect.Close()
        End If

        If IO.File.Exists(database & p_Databases("myvideos")) And imagepath = String.Empty Then
            Try
                SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("myvideos") & ";Read Only=True;"
                SQLconnect.Open()

                If field = "backdrop" Then
                    SQLcommand.CommandText = "SELECT strFanartURL FROM movieinfo WHERE IMDBID = '" & imdb_id & "'"
                ElseIf field = "cover" Then
                    SQLcommand.CommandText = "SELECT strPictureURL FROM movieinfo WHERE IMDBID = '" & imdb_id & "'"
                End If

                SQLreader = SQLcommand.ExecuteReader()
                SQLreader.Read()

                imagepath = SQLreader(0)
            Catch ex As Exception
                imagepath = String.Empty
            End Try

            SQLconnect.Close()
        End If

        Return imagepath

    End Function

    Public Shared Sub get_Person_image(ByVal artist As String, ByVal path As String, ByRef pb_image As PictureBox)

        Try
            Dim apikey As String = "cc25933c4094ca50635f94574491f320"

            Dim url As String = Uri.EscapeUriString("http://api.themoviedb.org/3/search/person?api_key=" & apikey & "&query=" & LCase(artist))

            Dim WebClient As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
            WebClient.Accept = "application/json"

            Dim response As System.Net.HttpWebResponse = WebClient.GetResponse()
            Dim receiveStream As Stream = response.GetResponseStream()
            Dim readStream As New StreamReader(receiveStream, Encoding.UTF8)
            Dim downstring As String = readStream.ReadToEnd()
            response.Close()
            readStream.Close()

            If downstring <> Nothing And InStr(downstring, artist) > 0 Then
                Dim images As New List(Of String)

                images.AddRange(Split(downstring, """profile_path"":"))
                downstring = Nothing

                images.RemoveAll(AddressOf cotainsNULL)

                For Each image In images
                    If InStr(image, "}") > 0 Then
                        downstring = Left(image, InStr(image, "}") - 1).Replace("""", "")
                        Exit For
                    End If
                Next

                If downstring <> Nothing Then
                    Dim filename As String = downstring.Replace("/", "")
                    downstring = "http://d3gtl9l2a4fn1j.cloudfront.net/t/p/w300/" & downstring

                    Dim ImageClient As New System.Net.WebClient
                    Dim ImageInBytes() As Byte
                    Dim stream As System.IO.MemoryStream
                    ImageInBytes = ImageClient.DownloadData(downstring)
                    stream = New System.IO.MemoryStream(ImageInBytes)

                    Dim person As Image = Image.FromStream(stream)

                    person.Save(_temp & "\" & filename)

                    Dim params() As String = {"-compose", "Copy", "-frame", "5x5+2+2"}
                    Convert(_temp & "\" & filename, path, params)

                    load_image(pb_image, path)

                    logStats("DVDArt: person image downloaded to " & path, "LOG")

                    IO.File.Delete(_temp & "\" & filename)
                Else
                    If IO.File.Exists(path) Then
                        load_image(pb_image, path)
                    Else
                        pb_image.Image = Nothing
                    End If
                End If
            Else
                If IO.File.Exists(path) Then
                    load_image(pb_image, path)
                Else
                    pb_image.Image = Nothing
                End If
            End If
        Catch ex As Exception
            pb_image.Image = Nothing
        End Try

    End Sub

    Public Shared Sub load_image(ByRef pb_image As PictureBox, ByVal path As String)

        On Error Resume Next

        If IO.File.Exists(path) Then

            Do Until Not FileInUse(path)
                wait(200)
            Loop

            Dim fs As IO.FileStream = New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
            pb_image.Image = Image.FromStream(fs)
            fs.Close()

            pb_image.Tag = Nothing
        Else
            pb_image.Image = Nothing
            pb_image.Tag = Nothing
        End If

    End Sub

    Public Shared Function import(ByVal database As String, ByVal thumbs As String, ByVal id As String, ByVal title As String, ByVal lang As String, ByVal type As String, ByVal personpath As String, ByVal checked As Array, Optional ByVal backdrop As String = Nothing, Optional ByVal cover As String = Nothing) As Array

        Dim y As Integer
        Dim filenotexist(4) As Boolean
        Dim downloaded(4) As Boolean

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("dvdart")

        logStats("DVDArt: downloading artwork for " & type & " - """ & title & """", "LOG")

        If type = "movie" Then

            For y = 0 To 4
                If y < 3 Then
                    filenotexist(y) = checked(0, y) And Not IO.File.Exists(thumbs & folder(0, y, 1) & id & ".png")
                ElseIf y = 3 Then
                    filenotexist(y) = checked(0, y) And Not IO.File.Exists(backdrop)
                ElseIf y = 4 Then
                    filenotexist(y) = checked(0, y) And Not IO.File.Exists(cover)
                End If
            Next

            downloaded = download(thumbs, folder, id, False, filenotexist, type, lang)

            For y = 0 To 4
                If y < 3 Then
                    downloaded(y) = checked(0, y) And (downloaded(y) Or IO.File.Exists(thumbs & folder(0, y, 1) & id & ".png"))
                ElseIf y = 3 Then
                    downloaded(y) = checked(0, y) And (downloaded(y) Or IO.File.Exists(backdrop))
                ElseIf y = 4 Then
                    downloaded(y) = checked(0, y) And (downloaded(y) Or IO.File.Exists(cover))
                End If
            Next

            SQLcommand.CommandText = "INSERT OR IGNORE INTO processed_movies (imdb_id) VALUES('" & id & "')"

        ElseIf type = "series" Then

            For y = 1 To 2
                filenotexist(y) = checked(1, y) And Not IO.File.Exists(thumbs & folder(1, y, 1) & id & ".png")
            Next

            downloaded = download(thumbs, folder, id, False, filenotexist, type, lang)

            For y = 1 To 2
                downloaded(y) = checked(1, y) And (downloaded(y) Or IO.File.Exists(thumbs & folder(1, y, 1) & id & ".png"))
            Next

            SQLcommand.CommandText = "INSERT OR IGNORE INTO processed_series (thetvdb_id) VALUES('" & id & "')"

        ElseIf Left(type, 6) = "artist" Then

            If title = String.Empty Then title = LCase(Right(type, Len(type) - 7).Replace(" ", "-"))
            type = Left(type, 6)

            If id = "" Then id = get_Artist_MBID(title)

            For y = 0 To 2
                filenotexist(y) = checked(2, y) And y <> 0 And Not IO.File.Exists(thumbs & folder(2, y, 1) & title & ".png")
            Next

            downloaded = download(thumbs, folder, id, False, filenotexist, type & "|" & title)

            For y = 0 To 2
                downloaded(y) = checked(2, y) And (downloaded(y) Or IO.File.Exists(thumbs & folder(2, y, 1) & title & ".png"))
            Next

            SQLcommand.CommandText = "INSERT OR IGNORE INTO processed_artist (artist, MBID) VALUES('" & title.Replace("'", "''") & "','" & id & "')"

        ElseIf Left(type, 5) = "music" Then

            Dim artist = LCase(Right(type, Len(type) - 6).Replace(" ", "-"))
            type = Left(type, 5)

            If id = "" Then id = get_MBID(database, title, artist)

            For y = 0 To 2
                filenotexist(y) = checked(2, y) And y = 0 And Not IO.File.Exists(thumbs & folder(2, y, 1) & title & ".png")
            Next

            downloaded = download(thumbs, folder, id, False, filenotexist, type & "|" & title)

            For y = 0 To 2
                downloaded(y) = checked(2, y) And (downloaded(y) Or IO.File.Exists(thumbs & folder(2, y, 1) & title & ".png"))
            Next

            SQLcommand.CommandText = "INSERT OR IGNORE INTO processed_music (album, MBID) VALUES('" & title.Replace("'", "''") & "','" & id & "')"

        ElseIf type = "person" Then

            Dim pb_image As New PictureBox

            get_Person_image(title, personpath & Utils.MakeFileName(title) & ".png", pb_image)

            downloaded(0) = IO.File.Exists(personpath & Utils.MakeFileName(title) & ".png")

            SQLcommand.CommandText = Nothing

        End If

        If SQLcommand.CommandText <> Nothing Then
            SQLconnect.Open()

            Try
                SQLcommand.ExecuteNonQuery()
            Catch ex As Exception
                logStats("DVDArt: " & SQLcommand.CommandText & " failed with exception - " & ex.Message, "ERROR")
            End Try

            SQLconnect.Close()
        End If

        If downloaded.Contains(True) Then
            logStats("DVDArt: artwork found for: """ & title & """", "LOG")
        Else
            logStats("DVDArt: no artwork found for: """ & title & """", "LOG")
        End If

        Return downloaded

    End Function

    Public Shared Sub bw_download_worker(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw_download0.DoWork, bw_download1.DoWork, bw_download2.DoWork, bw_download3.DoWork, bw_download4.DoWork, bw_download5.DoWork, bw_download6.DoWork, bw_download7.DoWork, bw_download8.DoWork, bw_download9.DoWork

        Try

            Dim parm As String = e.Argument

            logStats("DVDArt: download thread started for [" & parm & "]", "DEBUG")

            Dim url, path As String
            Dim endp As Integer
            Dim factor As Decimal = 1
            Dim shrink As Boolean = False
            Dim WebClient As New System.Net.WebClient

            endp = InStr(parm, "|shrink")

            If endp > 0 Then
                shrink = True
                parm = Left(parm, endp - 1)
            End If

            endp = InStr(parm, "|")
            path = Left(parm, endp - 1)
            url = Right(parm, Microsoft.VisualBasic.Len(parm) - endp)

            'download image and if not preview, reduce size to 500x500

            If url <> String.Empty And url <> "/preview" Then
                Dim image As Image
                Dim ImageInBytes() As Byte
                Dim stream As System.IO.MemoryStream
                Dim imagekey As String = Guid.NewGuid().ToString()
                ImageInBytes = WebClient.DownloadData(url)
                stream = New System.IO.MemoryStream(ImageInBytes)
                image = image.FromStream(stream)

                If shrink Then factor = 500 / image.Size.Height

                image = New Bitmap(image, New Size(image.Size.Width * factor, image.Size.Height * factor))
                image.Save(path)
                image.Dispose()

                logStats("DVDArt: artwork downloaded to " & path, "LOG")

                Dim info As New IO.FileInfo(path)

                If info.Extension = ".jpg" And (InStr(url, "/preview") = 0 Or InStr(url, "/w1920/") > 0) Then

                    reduceSize(path, info.Length)

                    Dim database As String = Nothing
                    Dim t As String = Nothing
                    Get_Paths(database, t)

                    If InStr(url, "/w" & _coversize & "/") > 0 Or InStr(url, "movieposter") > 0 Or InStr(LCase(path), "\covers\") > 0 Then
                        If InStr(LCase(path), "\fullsize\") > 0 Then
                            updateMovingPicturesDB(database, "cover", IO.Path.GetFileNameWithoutExtension(path), path)
                        ElseIf InStr(LCase(path), "covers\thumbs\") > 0 Then
                            updateMovingPicturesDB(database, "coverthumb", IO.Path.GetFileNameWithoutExtension(path), path)
                        End If
                    Else
                        If InStr(LCase(path), "\backdrops\thumbs\") = 0 Then updateMovingPicturesDB(database, "backdrop", IO.Path.GetFileNameWithoutExtension(path), path)
                    End If
                End If

            End If

            logStats("DVDArt: download thread complete for [" & parm & "]", "DEBUG")

        Catch ex As Exception
            logStats("DVDArt: Error downloading with exception " & ex.Message, "ERROR")
        End Try

        e.Result = "DONE"

    End Sub

    Public Shared Function download(ByVal thumbs As String, ByVal folder(,,) As String, ByVal id As String, ByVal overwrite As Boolean, _
                                    ByVal try2download As Array, ByVal type As String, Optional ByVal language As String = "##") As Array

        Dim url(9, 0) As String
        Dim found(4) As Boolean
        Dim parm As Object = Nothing
        Dim y As Integer

        Dim title As String = Nothing
        Dim thumbpath As String = Nothing
        Dim fullpath As String = Nothing
        Dim jsonresponse As String

        Try
            y = InStr(type, "|")

            If y > 0 Then
                title = Right(type, Len(type) - y)
                type = Left(type, y - 1)
            End If

            If type <> "music" Then
                If language = "##" Then
                    jsonresponse = JSON_request(id, type, "1")
                Else
                    jsonresponse = JSON_request(id, type, "2")
                End If
            Else
                jsonresponse = JSON_request(id, "artist", "2")
            End If

            If jsonresponse <> "null" Then

                If Left(type, 5) <> "music" Then
                    url = parse(jsonresponse, type, id, language)
                Else
                    url = parse_music(jsonresponse, LCase(title.Replace(" ", "-")))
                End If

                For y = 0 To 4

                    If type = "movie" Then
                        If y < 3 Then
                            fullpath = thumbs & folder(0, y, 0) & id & ".png"
                            thumbpath = thumbs & folder(0, y, 1) & id & ".png"
                        Else
                            fullpath = thumbs & folder(0, y, 0) & id & ".jpg"
                            thumbpath = thumbs & folder(0, y, 1) & id & ".jpg"
                        End If
                    ElseIf type = "series" Then
                        If y = 0 Or y > 2 Then Continue For
                        fullpath = thumbs & folder(1, y, 0) & id & ".png"
                        thumbpath = thumbs & folder(1, y, 1) & id & ".png"
                    ElseIf type = "artist" Then
                        If y = 0 Or y > 2 Then Continue For
                        fullpath = thumbs & folder(2, y, 0) & title & ".png"
                        thumbpath = thumbs & folder(2, y, 1) & title & ".png"
                    ElseIf Left(type, 5) = "music" Then
                        If y <> 0 Then Continue For
                        fullpath = thumbs & folder(2, y, 0) & title & ".png"
                        thumbpath = thumbs & folder(2, y, 1) & title & ".png"
                    End If

                    If (try2download(y) Or overwrite) And url(y * 2, 0) <> Nothing Then

                        If InStr(url(y * 2, 0), "/w1920/") > 0 Then
                            If y = 3 Then
                                parm = thumbpath & "|" & url(y * 2, 0).Replace("/w1920/", "/w300/")
                            ElseIf y = 4 Then
                                parm = thumbpath & "|" & url(y * 2, 0).Replace("/w1920/", "/w" & _coversize & "/")
                            End If
                        Else
                            parm = thumbpath & "|" & url(y * 2, 0) & "/preview"
                        End If

                        found(y) = True

                        Do
                            If Not bw_download0.IsBusy Then
                                bw_download0.WorkerSupportsCancellation = True
                                bw_download0.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download2.IsBusy Then
                                bw_download2.WorkerSupportsCancellation = True
                                bw_download2.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download4.IsBusy Then
                                bw_download4.WorkerSupportsCancellation = True
                                bw_download4.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download6.IsBusy Then
                                bw_download6.WorkerSupportsCancellation = True
                                bw_download6.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download8.IsBusy Then
                                bw_download8.WorkerSupportsCancellation = True
                                bw_download8.RunWorkerAsync(parm)
                                Exit Do
                            Else
                                wait(250)
                            End If
                        Loop
                    End If

                    found(y) = found(y) Or FileSystem.FileExists(thumbpath) Or (url(y * 2, 0) <> Nothing)

                    If (try2download(y) Or overwrite) And url(y * 2, 0) <> Nothing Then

                        parm = fullpath & "|" & url(y * 2, 0)
                        If y = 0 Then parm += "|shrink"

                        Do
                            If Not bw_download1.IsBusy Then
                                bw_download1.WorkerSupportsCancellation = True
                                bw_download1.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download3.IsBusy Then
                                bw_download3.WorkerSupportsCancellation = True
                                bw_download3.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download5.IsBusy Then
                                bw_download5.WorkerSupportsCancellation = True
                                bw_download5.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download7.IsBusy Then
                                bw_download7.WorkerSupportsCancellation = True
                                bw_download7.RunWorkerAsync(parm)
                                Exit Do
                            ElseIf Not bw_download9.IsBusy Then
                                bw_download9.WorkerSupportsCancellation = True
                                bw_download9.RunWorkerAsync(parm)
                                Exit Do
                            Else
                                wait(250)
                            End If
                        Loop
                    End If

                Next

            End If

        Catch ex As Exception
            Log.Error("DVDArt: download failed with exception - " & ex.Message)
        End Try

        Return found

    End Function

    Public Shared Function getSize(ByVal path As String) As String

        Dim size As String = String.Empty

        If IO.File.Exists(path) Then
            Dim img As Image = Image.FromFile(path)
            size = img.Width.ToString & "x" & img.Height.ToString
            img.Dispose()
        End If

        Return size

    End Function

    Public Shared Sub reduceSize(ByVal path As String, ByVal size As Integer)

        If size > 1048576 Then
            Dim ratio As Integer = 100 - (1048576 / size) * 100
            Dim params() As String = {"-quality", ratio.ToString}
            Convert(path, path, params)
        End If

    End Sub

    Public Shared Sub Resize(ByVal path As String, Optional ByVal width As Integer = 500, Optional ByVal height As Integer = 500, Optional ByVal thumb As Boolean = False, Optional ByVal retainaspect As Boolean = False)

        If thumb Then
            Dim hfactor, wfactor As Decimal
            Dim image As Image
            Dim ImageInBytes() As Byte
            Dim stream As System.IO.MemoryStream
            Dim imagekey As String = Guid.NewGuid().ToString()
            ImageInBytes = FileSystem.ReadAllBytes(path)
            stream = New System.IO.MemoryStream(ImageInBytes)
            image = image.FromStream(stream)

            hfactor = height / image.Size.Height
            wfactor = width / image.Size.Width

            image = New Bitmap(image, New Size(image.Size.Width * wfactor, image.Size.Height * hfactor))
            image.Save(path)
            image.Dispose()
        Else
            Dim aspect As String = "! "
            Dim dimension As String = width.ToString & "x" & height.ToString

            If retainaspect Then
                aspect = String.Empty
                dimension = width.ToString
            End If

            Dim params() As String = {"-format", "PNG32", "-background", "transparent", "-adaptive-resize", dimension & aspect, "-gravity", "Center", "-extent", dimension}

            Convert(path, path, params)
        End If

        logStats("DVDArt: artwork in " & path & " reseized.", "LOG")

    End Sub

    Public Shared Sub Convert(ByVal source As String, ByVal destination As String, ByVal ParamArray params() As String)

        If InStr(source, " ") And Left(source, 1) <> """" Then source = """" & source & """"
        If InStr(destination, " ") And Left(destination, 1) <> """" Then destination = """" & destination & """"

        Dim cmd As String = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\MediaPortal", "InstallPath", Nothing) & "\convert" & " " & source
        Dim x As Integer

        For x = 0 To params.LongLength - 1
            cmd = cmd & " " & params(x)
        Next

        cmd = cmd & " " & destination

        Shell(cmd, vbHide, True)

    End Sub

    Public Shared Sub create_CoverArt(ByVal file As String, ByVal imagename As String, ByVal name As String, ByVal _title As Integer, ByVal _logos As Boolean, ByVal template As Integer, Optional ByVal preview As Boolean = False, Optional ByVal previewfile As String = Nothing, Optional ByVal type As String = "dvdart")

        If Trim(file) = "" Then Exit Sub

        'create image with transparency from cover art
        Dim fullsize, discart, thumb, tempfile As String
        Dim url As System.Uri
        Dim objDL As New System.Net.WebClient
        Dim file2 As String = _temp & "\" & IO.Path.GetFileName(file.Replace(IO.Path.GetExtension(file), ".png"))
        Dim title() As String = Nothing
        Dim logos() As String = Nothing
        Dim database As String = Nothing
        Dim thumbs As String = Nothing

        Get_Paths(database, thumbs)

        If _title = 2 And Not IO.File.Exists(thumbs & folder(0, 2, 0) & imagename & ".png") Then
            _title = 1
        ElseIf _title = 2 And IO.File.Exists(thumbs & folder(0, 2, 0) & imagename & ".png") Then
            If template = 2 Then template = 1
        End If

        If _title = 1 Then discart = _temp & "\" & type & "_title.png" Else discart = _temp & "\" & type & ".png"

        Dim params() As String = {"-resize", "500", "-gravity", "Center", "-crop", "500x500+0+0", "+repage", _temp & "\" & type & "_mask.png", "-alpha", "off", "-compose", "copy_opacity", "-composite", discart, "-compose", "over", "-composite"}

        If _logos Then

            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
            Dim SQLreader As SQLiteDataReader

            If IO.File.Exists(database & p_Databases("movingpictures")) Then
                SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("movingpictures") & ";Read Only=True;"
                SQLconnect.Open()
                SQLcommand.CommandText = "SELECT l.videoresolution, m.certification, m.studios, l.is3d FROM local_media l, movie_info m, local_media__movie_info lm WHERE lm.movie_info_id = m.id AND l.id = lm.local_media_id AND m.imdb_id = '" & imagename & "'"
            ElseIf IO.File.Exists(database & p_Databases("myvideos")) Then
                SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("myvideos") & ";Read Only=True;"
                SQLconnect.Open()
                SQLcommand.CommandText = "SELECT i.videoResolution, m.mpaa, m.studios, '0' FROM movieinfo m, files f, filesmediainfo i WHERE m.idMovie = f.idMovie AND f.idFile = i.idFile AND m.IMDBID = '" & imagename & "'"
            End If

            SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)
            SQLreader.Read()

            Dim videoresolution As String = "sd"
            Dim certification As String = Nothing
            Dim studios() As String = Nothing
            Dim is3d As Boolean = False

            If Not IsDBNull(SQLreader(0)) Then videoresolution = LCase(SQLreader(0))
            If Not IsDBNull(SQLreader(1)) Then certification = LCase(SQLreader(1))
            If Not IsDBNull(SQLreader(2)) Then studios = Split(LCase(SQLreader(2).replace(" ", "_")), "|")
            If Not IsDBNull(SQLreader(3)) Then is3d = SQLreader(3)

            SQLreader.Close()

            ' Video Resolution logo, DVD or BlueRay
            Dim image As Image = Nothing

            If videoresolution = "720p" Or videoresolution = "1080p" Or videoresolution = "1080i" Or videoresolution = "hd" Then
                If Not is3d Then
                    videoresolution = "hd"
                    image = New Bitmap(My.Resources.hd)
                Else
                    videoresolution = "hd_3d"
                    image = New Bitmap(My.Resources.hd_3d)
                End If
            ElseIf videoresolution = "240" Or videoresolution = "480" Or videoresolution = "540" Or videoresolution = "576" Or videoresolution = "dvd" Or videoresolution = "sd" Then
                If Not is3d Then
                    videoresolution = "sd"
                    image = New Bitmap(My.Resources.sd)
                Else
                    videoresolution = "sd_3d"
                    image = New Bitmap(My.Resources.sd_3d)
                End If
            End If

            tempfile = _temp & "\" & videoresolution & ".png"
            If Not FileSystem.FileExists(tempfile) Then
                image.Save(tempfile)
            End If

            image.Dispose()

            If template = 1 Then
                logos = {tempfile, "-geometry", "-148+60", "-composite"}
            Else
                logos = {tempfile, "-geometry", "+0+200", "-composite"}
            End If

            For x = 0 To UBound(logos)
                ReDim Preserve params(UBound(params) + 1)
                params(UBound(params)) = logos(x)
            Next

            ' Studios Logo
            If studios IsNot Nothing Then
                For x = 0 To UBound(studios)

                    If studios(x) <> "" And studios(x) <> "_" And studios(x) <> Nothing Then
                        tempfile = _temp & "\" & studios(x) & ".png"
                        If Not FileSystem.FileExists(tempfile) Then
                            Try
                                url = New Uri("https://dvdart.googlecode.com/svn/trunk/Studio/logos/" + studios(x) + ".png")
                                objDL.DownloadFile(url, tempfile)
                            Catch ex As Exception
                                tempfile = ""
                            End Try
                        End If
                        studios(x) = tempfile
                    Else
                        studios(x) = ""
                    End If

                Next

                Dim studio = (From str In studios Where Not {""}.Contains(str)).ToArray()

                If studio.Length > 0 Then

                    Select Case studio.Length
                        Case 1
                            logos = {"""" & studio(0) & """", "-geometry", "+155", "-composite"}
                        Case 2
                            logos = {"""" & studio(0) & """", "-geometry", "+148-50", "-composite", """" & studio(1) & """", "-geometry", "+148+50", "-composite"}
                        Case 3
                            logos = {"""" & studio(0) & """", "-geometry", "+130-85", "-composite", """" & studio(1) & """", "-geometry", "+155", "-composite", """" & studio(2) & """", "-geometry", "+130+85", "-composite"}
                    End Select

                    For x = 0 To UBound(logos)
                        ReDim Preserve params(UBound(params) + 1)
                        params(UBound(params)) = logos(x)
                    Next
                End If
            End If

            ' Certification logo
            If certification <> "" And certification <> Nothing Then

                If Not FileSystem.FileExists(certification) Then
                    Try
                        url = New Uri("https://dvdart.googlecode.com/svn/trunk/Certification/logos/" + certification + ".png")
                        certification = _temp & "\" & certification & ".png"
                        objDL.DownloadFile(url, certification)
                    Catch ex As Exception
                    End Try
                End If

                If FileSystem.FileExists(certification) Then

                    If template = 1 Then
                        logos = {certification, "-geometry", "-148-60", "-composite"}
                    Else
                        logos = {certification, "-geometry", "-155", "-composite"}
                    End If

                    For x = 0 To UBound(logos)
                        ReDim Preserve params(UBound(params) + 1)
                        params(UBound(params)) = logos(x)
                    Next

                End If

            End If

        End If

        If _title > 0 Then

            If _title = 1 Then
                Dim pointsize As Integer = 36
                If Len(name) > 20 Then pointsize = (20 / Len(name)) * pointsize
                title = {"-background", "transparent", "-fill white", "-font", "segoe-ui-bold", "-pointsize", pointsize.ToString, "-gravity", "center", "-size", "500x49", "label:""" & name & """", "-geometry", "+0+362", "-gravity", "north", "-composite"}
            ElseIf _title = 2 Then
                Dim temp_params() As String = {"-resize", "300", "( +clone", "-background", "black", "-shadow", "100x4+0+0 )", "+swap", "-background", "none", "-mosaic"}
                Convert("""" & thumbs & folder(0, 2, 0) & imagename & ".png""", _temp & "\" & imagename & "#.png", temp_params)
                title = {_temp & "\" & imagename & "#.png", "-geometry", "+0+135", "-composite"}
            End If

            For x = 0 To UBound(title)
                ReDim Preserve params(UBound(params) + 1)
                params(UBound(params)) = title(x)
            Next

        End If

        If Not preview Then
            If type = "dvdart" Then
                fullsize = thumbs & folder(0, 0, 0) & imagename & ".png"
            Else
                fullsize = thumbs & folder(2, 0, 0) & imagename & ".png"
            End If
        Else
            fullsize = previewfile
        End If

        If IO.File.Exists(fullsize) Then
            Do While FileInUse(fullsize)
                wait(250)
            Loop
            IO.File.Delete(fullsize)
        End If

        Convert(file, file2, params)

        If IO.File.Exists(_temp & "\" & imagename & "#.png") Then IO.File.Delete(_temp & "\" & imagename & "#.png")

        Dim counter As Integer = 0

        Do While (Not IO.File.Exists(file2) Or FileInUse(file2)) And counter < 5
            wait(250)
            counter += 1
        Loop

        If counter < 5 Then

            'move to Thumbs folder
            If file2 <> fullsize Then FileIO.FileSystem.MoveFile(file2, fullsize, True)

            If Not preview Then
                'copy to Thumbs folder and resize to thumb size
                If type = "dvdart" Then
                    thumb = thumbs & folder(0, 0, 1) & imagename & ".png"
                Else
                    thumb = thumbs & folder(2, 0, 1) & imagename & ".png"
                End If
                FileIO.FileSystem.CopyFile(fullsize, thumb, True)
                Resize(thumb, 200, 200, True)
            End If

        End If

    End Sub

    Public Shared Function FileInUse(ByVal sFile As String) As Boolean

        Dim inuse As Boolean = False

        If System.IO.File.Exists(sFile) Then
            Try
                Dim F As Short = FreeFile()
                FileOpen(F, sFile, OpenMode.Binary, OpenAccess.ReadWrite, OpenShare.LockReadWrite)
                FileClose(F)
            Catch
                inuse = True
            End Try
        End If

        Return inuse

    End Function

    Public Shared Function Get_Paths(ByRef database As String, ByRef thumbs As String) As Boolean

        database = Nothing
        thumbs = Nothing

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Base, "MediaPortalDirs.xml"))

            database = XMLreader.GetValueAsString("Path", "Database", Config.GetFolder(Config.Dir.Database))
            thumbs = XMLreader.GetValueAsString("Path", "Thumbs", Config.GetFolder(Config.Dir.Thumbs))

        End Using

        Return database <> Nothing And thumbs <> Nothing

    End Function

    Public Shared Function FieldExist(ByVal table As String, ByVal fields() As String) As Array

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim columns() As String = Nothing
        Dim exists() As Boolean
        Dim x As Integer = 0

        SQLconnect.ConnectionString = "Data Source=" & Config.GetFile(Config.Dir.Database, "dvdart.db3") & ";Read Only=True;"
        SQLconnect.Open()
        SQLcommand.CommandText = "PRAGMA table_info (" & table & ")"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()
            ReDim Preserve columns(x)
            columns(x) = SQLreader(1)
            x += 1
        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve columns(0)

        ReDim exists(fields.Count - 1)

        For x = 0 To fields.Count - 1
            exists(x) = columns.Contains(fields(x))
        Next

        Return exists

    End Function

    Private Shared Sub Create_Folder_Structure(ByVal database As String, ByVal thumbs As String)

        Dim db_exist(2) As Boolean

        ' Check and create directory structure
        db_exist(0) = IO.File.Exists(database + p_Databases("movingpictures")) Or IO.File.Exists(p_Databases("myfilms")) Or IO.File.Exists(database & p_Databases("myvideos"))
        db_exist(1) = IO.File.Exists(database + p_Databases("tvseries"))
        db_exist(2) = IO.File.Exists(database + p_Databases("music"))

        For x = 0 To 2
            If db_exist(x) Then
                For y = 0 To 2
                    For z = 0 To 1
                        If folder(x, y, z) IsNot Nothing Then
                            If Not IO.Directory.Exists(thumbs & folder(x, y, z)) Then IO.Directory.CreateDirectory(thumbs & folder(x, y, z))
                        End If
                    Next
                Next
            End If
        Next

    End Sub

    Private Shared Function readMovingPicturesDB(ByVal database As String, ByVal thumbs As String, ByVal key As String) As String

        Dim value As String = Nothing

        Try
            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
            Dim SQLreader As SQLiteDataReader

            SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("movingpictures") & ";Read Only=True;"
            SQLconnect.Open()
            SQLcommand.CommandText = "SELECT value FROM settings WHERE key = '" & key & "'"
            SQLreader = SQLcommand.ExecuteReader()
            SQLreader.Read()

            If InStr(key, "_folder") > 0 Then
                value = Right(SQLreader(0), Len(SQLreader(0)) - Len(thumbs)) & "\"
            Else
                value = SQLreader(0)
            End If

            SQLconnect.Close()
        Catch ex As Exception
        End Try

        Return value

    End Function

    Public Shared Sub updateMovingPicturesDB(ByVal database As String, ByVal field As String, ByVal key As String, ByVal value As String)

        If Not IO.File.Exists(database & p_Databases("movingpictures")) Then Exit Sub

        Try
            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand

            SQLconnect.ConnectionString = "Data Source=" & database & p_Databases("movingpictures") & ";"
            SQLconnect.Open()
            SQLcommand.CommandText = "UPDATE movie_info SET " & field & "fullpath = '" & value & "' WHERE imdb_id = '" & key & "'"
            SQLcommand.ExecuteNonQuery()

            If field = "cover" Then
                SQLcommand.CommandText = "UPDATE movie_info SET alternatecovers = alternatecovers||'" & value & "|' WHERE imdb_id = '" & key & "'"
                SQLcommand.ExecuteNonQuery()
            End If

            SQLconnect.Close()
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub preInitialize()

        ' create log file
        Dim file As String = Config.GetFile(Config.Dir.Log, "dvdart_plugin.log")

        If IO.File.Exists(Config.GetFile(Config.Dir.Log, "dvdart_plugin.bak")) Then IO.File.Delete(Config.GetFile(Config.Dir.Log, "dvdart_plugin.bak"))
        If IO.File.Exists(file) Then FileIO.FileSystem.RenameFile(file, "dvdart_plugin.bak")
        Dim fhandle As System.IO.FileStream = IO.File.Open(file, IO.FileMode.OpenOrCreate)
        fhandle.Close()

        ' initialize version
        _version = "v1.0.2.4"

        logStats("DVDArt: Plugin version " & _version, "LOG")

        'set cover size
        _coversize = "600"

        'initialize language array
        lang = {"English", "Deutsch", "Française", "Italiano", "русский", "Any"}
        langcode = {"EN", "DE", "FR", "IT", "RU", "##"}

        'Build the path of the assembly from where it has to be loaded.
        Dim strTempAssmbPath As String
        strTempAssmbPath = Config.GetFile(Config.Dir.Plugins, "windows\MyFilms.dll")

        Dim MyAssembly As [Assembly]

        'Load the assembly from the specified path. 
        Try
            MyAssembly = [Assembly].LoadFrom(strTempAssmbPath)
        Catch ex As Exception
        End Try

    End Sub

    Public Shared Sub Initialize(ByVal database As String, ByVal thumbs As String, ByVal Movies As String, ByVal Series As String, ByVal Music As String, ByVal Person As String)

        logStats("DVDArt: Initialization in progress.", "LOG")

        logStats("DVDArt: Movies artwork path  - " & Movies, "LOG")
        logStats("DVDArt: Series artwork path  - " & Series, "LOG")
        logStats("DVDArt: Music artwork path   - " & Music, "LOG")
        logStats("DVDArt: Person artwork path  - " & Person, "LOG")

        Movies = Right(Movies, Len(Movies) - Len(thumbs))
        Series = Right(Series, Len(Series) - Len(thumbs))
        Music = Right(Music, Len(Music) - Len(thumbs))

        ' initialize folder paths
        folder(0, 0, 0) = Movies & "\DVDArt\FullSize\"
        folder(0, 0, 1) = Movies & "\DVDArt\Thumbs\"
        folder(0, 1, 0) = Movies & "\ClearArt\FullSize\"
        folder(0, 1, 1) = Movies & "\ClearArt\Thumbs\"
        folder(0, 2, 0) = Movies & "\ClearLogo\FullSize\"
        folder(0, 2, 1) = Movies & "\ClearLogo\Thumbs\"

        If IO.File.Exists(database & p_Databases("movingpictures")) Then
            folder(0, 3, 0) = readMovingPicturesDB(database, thumbs, "backdrop_folder")
            folder(0, 3, 1) = readMovingPicturesDB(database, thumbs, "backdrop_thumbs_folder")
            folder(0, 4, 0) = readMovingPicturesDB(database, thumbs, "cover_art_folder")
            folder(0, 4, 1) = readMovingPicturesDB(database, thumbs, "cover_thumbs_folder")
        ElseIf IO.File.Exists(database & p_Databases("myvideos")) Then
            folder(0, 3, 0) = thumbs & "\Videos\Backdrop\Full\"
            folder(0, 3, 1) = thumbs & "\Videos\Backdrop\Thumbs\"
            folder(0, 4, 0) = thumbs & "\Vidoes\Covers\Full\"
            folder(0, 4, 1) = thumbs & "\Vidoes\Covers\Thumbs\"
        End If

        folder(1, 0, 0) = Nothing
        folder(1, 0, 1) = Nothing
        folder(1, 1, 0) = Series & "\ClearArt\FullSize\"
        folder(1, 1, 1) = Series & "\ClearArt\Thumbs\"
        folder(1, 2, 0) = Series & "\ClearLogo\FullSize\"
        folder(1, 2, 1) = Series & "\ClearLogo\Thumbs\"
        folder(1, 3, 0) = Nothing
        folder(1, 3, 1) = Nothing
        folder(1, 4, 0) = Nothing
        folder(1, 4, 1) = Nothing
        folder(2, 0, 0) = Music & "\CDArt\FullSize\"
        folder(2, 0, 1) = Music & "\CDArt\Thumbs\"
        folder(2, 1, 0) = Music & "\Banner\FullSize\"
        folder(2, 1, 1) = Music & "\Banner\Thumbs\"
        folder(2, 2, 0) = Music & "\ClearLogo\FullSize\"
        folder(2, 2, 1) = Music & "\ClearLogo\Thumbs\"
        folder(2, 3, 0) = Nothing
        folder(2, 3, 1) = Nothing
        folder(2, 4, 0) = Nothing
        folder(2, 4, 1) = Nothing

        'create thumb folder structure
        Create_Folder_Structure(database, thumbs)

        If Not IO.Directory.Exists(Person) Then IO.Directory.CreateDirectory(Person)

        'extract dvdart.png from resources to temporary folder
        Dim png As String = _temp & "\dvdart.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.dvdart)
            image.Save(png)
            image.Dispose()
        End If

        'extract dvdart_title.png from resources to temporary folder
        png = _temp & "\dvdart_title.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.dvdart_title)
            image.Save(png)
            image.Dispose()
        End If

        'extract dvdart_mask.png from resources to temporary folder
        png = _temp & "\dvdart_mask.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.dvdart_mask)
            image.Save(png)
            image.Dispose()
        End If

        'extract cdart.png from resources to temporary folder
        png = _temp & "\cdart.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.cdart)
            image.Save(png)
            image.Dispose()
        End If

        'extract cdart_mask.png from resources to temporary folder
        png = _temp & "\cdart_mask.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.cdart_mask)
            image.Save(png)
            image.Dispose()
        End If

        logStats("DVDArt: Initialization complete.", "LOG")

    End Sub

End Class
