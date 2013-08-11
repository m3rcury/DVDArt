Imports Microsoft.VisualBasic.FileIO

Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library

Imports System.Data.SQLite
Imports System.ComponentModel


Public Class DVDArt_Common

    Public Shared _version, _pre_version, folder(2, 2, 1), lang(4), langcode(4) As String
    Public Shared WithEvents bw_download0, bw_download1, bw_download2, bw_download3, bw_download4, bw_download5 As New BackgroundWorker
    Public Shared _temp As String = Environ("temp")

    Public Shared Sub wait(ByVal milliseconds As Long)
        System.Threading.Thread.Sleep(milliseconds)
    End Sub

    Public Shared Function Get_MBID(ByVal album As String, ByVal artist As String, ByVal database As String) As String

        Dim WebClient As New System.Net.WebClient
        Dim startp, endp, len As Integer
        Dim MBz_XML As String
        Dim MBID As String = Nothing

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT MBID FROM processed_artist WHERE artist = '" & artist & "'"
        SQLreader = SQLcommand.ExecuteReader()
        SQLreader.Read()

        Dim url As String = "http://www.musicbrainz.org/ws/2/release?artist=" & SQLreader(0)

        SQLconnect.Close()

        MBz_XML = WebClient.DownloadString(url)

        ' if a match is found
        If MBz_XML.Contains("<title>" & album & "</title>") Then
            startp = InStr(MBz_XML, "<release id=") + 13
            endp = InStr(startp, MBz_XML, """")
            len = endp - startp
            MBID = Mid(MBz_XML, startp, len)
        End If

        Return MBID

    End Function

    Public Shared Function Get_Artist_MBID(ByVal artist As String) As String

        Dim WebClient As New System.Net.WebClient
        Dim startp, endp, len As Integer
        Dim MBz_XML As String
        Dim MBID As String = Nothing

        Dim url As String = "http://www.musicbrainz.org/ws/2/artist/?query=" & artist.Replace(" ", "%20")

        MBz_XML = WebClient.DownloadString(url)

        ' if a match is found
        If MBz_XML.Contains("ext:score=""100""") Then
            startp = InStr(MBz_XML, "<artist id=") + 12
            endp = InStr(startp, MBz_XML, """")
            len = endp - startp
            MBID = Mid(MBz_XML, startp, len)
        Else
            Return Nothing
        End If

        Return MBID

    End Function

    Public Shared Function parse(ByVal jsonresponse As String, ByVal type As String, Optional ByVal language As String = "##") As Array

        Dim parseHD_l As String = Nothing
        Dim parseHD_c As String = Nothing
        Dim details(5, 0), returndetails(5, 0), parsestring(2), keyword(4) As String
        Dim starting(2), startHD_l, startHD_c, startp, endp, len, x, y, i, j As Integer

        If type = "movie" Then
            keyword = {"hdmovielogo", "**n/a**", "moviedisc", "movieart", "movielogo"}
        ElseIf type = "series" Then
            keyword = {"hdtvlogo", "hdclearart", "**n/a**", "clearart", "clearlogo"}
        ElseIf type = "artist" Then
            keyword = {"hdmusiclogo", "**n/a**", "cdart", "musicbanner", "musiclogo"}
        End If

        ' check if there are HD logos and if yes, store in a temporary variable to later on merge with movielogos

        startHD_l = InStr(jsonresponse, keyword(0))
        startHD_c = InStr(jsonresponse, keyword(1))

        If startHD_l > 0 Then
            parseHD_l = Mid(jsonresponse, startHD_l, InStr(startHD_l, jsonresponse, "]") - startHD_l + 1)
        End If

        If startHD_c > 0 Then
            parseHD_c = Mid(jsonresponse, startHD_c, InStr(startHD_c, jsonresponse, "]") - startHD_c + 1)
        End If

        ' find the starting place of the respective sections

        For i = 0 To 2
            starting(i) = InStr(jsonresponse, keyword(i + 2))
        Next

        ' split the jsonresponse to the respective sections

        For i = 0 To 2
            If starting(i) > 0 Then
                parsestring(i) = Mid(jsonresponse, starting(i), InStr(starting(i), jsonresponse, "]") - starting(i) + 1)
            End If
        Next

        ' if there are HD images, merge with SD images

        If startHD_l > 0 Then
            parsestring(2) = Trim(parsestring(2)) & parseHD_l
            If starting(2) = 0 Then starting(2) = startHD_l
        End If

        If startHD_c > 0 Then
            parsestring(1) = Trim(parsestring(1)) & parseHD_c
            If starting(1) = 0 Then starting(1) = startHD_c
        End If

        For i = 0 To 2

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

                            If x >= y Then ReDim Preserve details(5, x)

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
                                details(j + 1, x) = UCase(Mid(parsestring(i), startp, len).Replace("""", ""))

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

            For x = 0 To (details.Length / 6) - 1
                For y = 1 To 5 Step 2
                    If InStr(details(y, x), "LANG:" & language) > 0 Then

                        For i = 0 To 5
                            returndetails(i, 0) = details(i, x)
                        Next

                        Return returndetails

                    End If
                Next
            Next

            ' if no artwork found for preferred language, check if there is anything in english
            If language <> "EN" Then
                For x = 0 To (details.Length / 6) - 1
                    For y = 1 To 5 Step 2
                        If InStr(details(y, x), "LANG:EN") > 0 Then

                            For i = 0 To 5
                                returndetails(i, 0) = details(i, x)
                            Next

                            Return returndetails

                        End If
                    Next
                Next
            End If

        Else
            returndetails = details
        End If

        Return returndetails

    End Function

    Public Shared Function JSON_request(ByVal imdb_id As String, ByVal type As String, ByVal nbrimages As String) As String

        Dim WebClient As New System.Net.WebClient
        Dim apikey As String = "bfd6e4e0d4e71237f784b70fc43f8269"

        Dim url As String = "http://fanart.tv/webservice/" & type & "/" & apikey & "/" & imdb_id & "/json/all/1/" & nbrimages

        Return WebClient.DownloadString(url)

    End Function

    Public Shared Sub bw_download_worker(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw_download0.DoWork, bw_download1.DoWork, bw_download2.DoWork, bw_download3.DoWork, bw_download4.DoWork, bw_download5.DoWork

        Dim parm As String = e.Argument
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

        e.Result = "DONE"

    End Sub

    Public Shared Function download(ByVal thumbs As String, ByVal folder(,,) As String, ByVal id As String, ByVal overwrite As Boolean, _
                                    ByVal try2download() As Boolean, ByVal type As String, Optional ByVal language As String = "##") As Array

        Dim WebClient As New System.Net.WebClient
        Dim url(5, 0) As String
        Dim found(2) As Boolean
        Dim parm As Object
        Dim y As Integer

        Dim thumbpath As String = Nothing
        Dim fullpath As String = Nothing
        Dim jsonresponse As String

        If language = "##" Then
            jsonresponse = JSON_request(id, type, "1")
        Else
            jsonresponse = JSON_request(id, type, "2")
        End If

        If jsonresponse <> "null" Then

            url = parse(jsonresponse, type, language)

            For y = 0 To 2

                If type = "movie" Then
                    fullpath = thumbs & folder(0, y, 0) & id & ".png"
                    thumbpath = thumbs & folder(0, y, 1) & id & ".png"
                ElseIf type = "series" Then
                    fullpath = thumbs & folder(1, y, 0) & id & ".png"
                    thumbpath = thumbs & folder(1, y, 1) & id & ".png"
                ElseIf type = "artist" Or type = "music" Then
                    fullpath = thumbs & folder(2, y, 0) & id & ".png"
                    thumbpath = thumbs & folder(2, y, 1) & id & ".png"
                End If

                If (try2download(y) Or overwrite) And url(y * 2, 0) <> Nothing Then
                    parm = thumbpath & "|" & url(y * 2, 0) & "/preview"
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
                        Else
                            wait(250)
                        End If
                    Loop
                End If

                found(y) = found(y) Or FileSystem.FileExists(thumbpath)

                If (try2download(y) Or overwrite) And url(y * 2, 0) <> Nothing Then
                    If y = 0 Then parm = fullpath & "|" & url(y * 2, 0) & "|shrink" Else parm = fullpath & "|" & url(y * 2, 0)
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
                        Else
                            wait(250)
                        End If
                    Loop
                End If

            Next

        End If

        Return found

    End Function

    Public Shared Function getSize(ByVal path As String, ByVal image As String) As String

        Dim size As String = String.Empty
        Dim shell As New Shell32.Shell
        Dim folder = shell.NameSpace(path)

        Dim columns As New Dictionary(Of String, Integer)

        For i As Integer = 0 To Short.MaxValue
            Dim header = folder.GetDetailsOf(folder.Items, i)
            If String.IsNullOrEmpty(header) Then
                Exit For 'no more columns
            Else
                columns(header) = i
            End If
        Next

        If columns.ContainsKey("Dimensions") Then
            size = folder.GetDetailsOf(folder.ParseName(image), columns("Dimensions")).Replace(" ", String.Empty)
            size = Mid(size, 2, size.Length - 2)
        End If

        Return size

    End Function

    Public Shared Sub Resize(ByVal path As String, Optional ByVal width As Integer = 500, Optional ByVal height As Integer = 500)

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

        Shell(cmd, vbHide)

    End Sub

    Public Shared Sub create_CoverArt(ByVal file As String, ByVal imdb_id As String, ByVal movie_name As String, ByVal _title As Boolean, ByVal _logos As Boolean)

        If Trim(file) = "" Then Exit Sub

        'create image with transparency from cover art
        Dim fullsize, dvdart, thumb, tempfile As String
        Dim url As System.Uri
        Dim objDL As New System.Net.WebClient
        Dim file2 As String = _temp & "\" & IO.Path.GetFileName(file.Replace(IO.Path.GetExtension(file), ".png"))
        Dim title(), logos() As String
        Dim database As String = Nothing
        Dim thumbs As String = Nothing

        Get_Paths(database, thumbs)

        If _title Then dvdart = _temp & "\dvdart_title.png" Else dvdart = _temp & "\dvdart.png"

        Dim params() As String = {"-resize", "500", "-gravity", "Center", "-crop", "500x500+0+0", "+repage", _temp & "\dvdart_mask.png", "-alpha", "off", "-compose", "copy_opacity", "-composite", dvdart, "-compose", "over", "-composite"}

        If _logos Then

            Dim SQLconnect As New SQLiteConnection()
            Dim SQLcommand As SQLiteCommand
            Dim SQLreader As SQLiteDataReader

            SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3;Read Only=True;"
            SQLconnect.Open()
            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = "SELECT l.videoresolution, m.certification, m.studios FROM local_media l, movie_info m, local_media__movie_info lm WHERE lm.movie_info_id = m.id AND l.id = lm.local_media_id AND m.imdb_id = """ & imdb_id & """"
            SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)

            Dim videoresolution As String = "sd"
            Dim certification As String = Nothing
            Dim studios() As String = Nothing

            If Not IsDBNull(SQLreader(0)) Then videoresolution = LCase(SQLreader(0))
            If Not IsDBNull(SQLreader(1)) Then certification = LCase(SQLreader(1))
            If Not IsDBNull(SQLreader(2)) Then studios = Split(LCase(SQLreader(2).replace(" ", "_")), "|")

            SQLreader.Close()

            ' Video Resolution logo, DVD or BlueRay
            Dim image As Image = Nothing

            If videoresolution = "720p" Or videoresolution = "1080p" Or videoresolution = "1080i" Or videoresolution = "hd" Then
                videoresolution = "hd"
                image = New Bitmap(My.Resources.hd)
            ElseIf videoresolution = "240" Or videoresolution = "480" Or videoresolution = "540" Or videoresolution = "576" Or videoresolution = "dvd" Or videoresolution = "sd" Then
                videoresolution = "sd"
                image = New Bitmap(My.Resources.sd)
            End If

            tempfile = _temp & "\" & videoresolution & ".png"
            If Not FileSystem.FileExists(tempfile) Then
                image.Save(tempfile)
            End If

            logos = {tempfile, "-geometry", "+0+200", "-composite"}
            For x = 0 To UBound(logos)
                ReDim Preserve params(UBound(params) + 1)
                params(UBound(params)) = logos(x)
            Next

            ' Studios Logo
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

            ' Certification logo
            If certification <> "" And certification <> Nothing Then
                certification = _temp & "\" & certification & ".png"

                If Not FileSystem.FileExists(certification) Then
                    Try
                        url = New Uri("https://dvdart.googlecode.com/svn/trunk/Certification/logos/" + certification + ".png")
                        objDL.DownloadFile(url, certification)
                    Catch ex As Exception
                    End Try
                End If

                If FileSystem.FileExists(certification) Then
                    logos = {certification, "-geometry", "-155", "-composite"}
                    For x = 0 To UBound(logos)
                        ReDim Preserve params(UBound(params) + 1)
                        params(UBound(params)) = logos(x)
                    Next
                End If
            End If

        End If

        If _title Then
            Dim pointsize As Integer = 36
            If Len(movie_name) > 20 Then pointsize = (20 / Len(movie_name)) * pointsize
            title = {"-background", "transparent", "-fill white", "-font", "segoe-ui-bold", "-pointsize", pointsize.ToString, "-gravity", "center", "-size", "500x49", "label:""" & movie_name & """", "-geometry", "+0+362", "-gravity", "north", "-composite"}
            For x = 0 To UBound(title)
                ReDim Preserve params(UBound(params) + 1)
                params(UBound(params)) = title(x)
            Next
        End If

        fullsize = thumbs & folder(0, 0, 0) & imdb_id & ".png"

        If FileIO.FileSystem.FileExists(fullsize) Then FileIO.FileSystem.DeleteFile(fullsize)

        Convert(file, file2, params)

        Dim counter As Integer = 0

        Do While (Not FileSystem.FileExists(file2) Or FileInUse(file2)) And counter < 5
            wait(250)
            counter += 1
        Loop

        If counter < 5 Then

            'move to Thumbs folder
            FileIO.FileSystem.MoveFile(file2, fullsize, True)
            'copy to Thumbs folder and resize to thumb size
            thumb = thumbs & folder(0, 0, 1) & imdb_id & ".png"
            FileIO.FileSystem.CopyFile(fullsize, thumb, True)
            Resize(thumb, 200, 200)

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
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim columns() As String = Nothing
        Dim exists() As Boolean
        Dim x As Integer = 0

        SQLconnect.ConnectionString = "Data Source=" & Config.GetFile(Config.Dir.Database, "dvdart.db3") & ";Read Only=True;"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
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

    Public Shared Function Create_Folder_Structure(ByVal database As String, ByVal thumbs As String) As Boolean

        Dim db_exist(2) As Boolean

        ' Check and create directory structure

        db_exist(0) = FileSystem.FileExists(database + "\movingpictures.db3")
        db_exist(1) = FileSystem.FileExists(database + "\TVSeriesDatabase4.db3")
        db_exist(2) = FileSystem.FileExists(database + "\MusicDatabaseV12.db3")

        ' DVDArt
        If db_exist(0) And Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\DVDArt") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt")

        ' CDArt
        If db_exist(2) And Not FileSystem.DirectoryExists(thumbs & "\Music\CDArt") Then FileSystem.CreateDirectory(thumbs & "\Music\CDArt")

        ' ClearArt
        If db_exist(0) And Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\ClearArt") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\ClearArt")
        If db_exist(1) And Not FileSystem.DirectoryExists(thumbs & "\TVSeries\ClearArt") Then FileSystem.CreateDirectory(thumbs & "\TVSeries\ClearArt")

        ' Banner
        If db_exist(2) And Not FileSystem.DirectoryExists(thumbs & "\Music\Banner") Then FileSystem.CreateDirectory(thumbs & "\Music\Banner")

        ' ClearLogo
        If db_exist(0) And Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\ClearLogo") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\ClearLogo")
        If db_exist(1) And Not FileSystem.DirectoryExists(thumbs & "\TVSeries\ClearLogo") Then FileSystem.CreateDirectory(thumbs & "\TVSeries\ClearLogo")
        If db_exist(2) And Not FileSystem.DirectoryExists(thumbs & "\Music\ClearLogo") Then FileSystem.CreateDirectory(thumbs & "\Music\ClearLogo")

        For x = 0 To 2
            If db_exist(x) Then
                For y = 0 To 1
                    For z = 0 To 2
                        If folder(z, x, y) IsNot Nothing Then
                            If Not FileSystem.DirectoryExists(thumbs & folder(z, x, y)) Then FileSystem.CreateDirectory(thumbs & folder(z, x, y))
                        End If
                    Next
                Next
            End If
        Next

        Return db_exist(0) Or db_exist(1) Or db_exist(2)

    End Function

    Public Shared Sub Initialize()

        ' initialize version
        _pre_version = "v1.0.1.2"
        _version = "v1.0.1.3"

        ' initialize folder paths
        folder(0, 0, 0) = "\MovingPictures\DVDArt\FullSize\"
        folder(0, 0, 1) = "\MovingPictures\DVDArt\Thumbs\"
        folder(0, 1, 0) = "\MovingPictures\ClearArt\FullSize\"
        folder(0, 1, 1) = "\MovingPictures\ClearArt\Thumbs\"
        folder(0, 2, 0) = "\MovingPictures\ClearLogo\FullSize\"
        folder(0, 2, 1) = "\MovingPictures\ClearLogo\Thumbs\"
        folder(1, 0, 0) = Nothing
        folder(1, 0, 1) = Nothing
        folder(1, 1, 0) = "\TVSeries\ClearArt\FullSize\"
        folder(1, 1, 1) = "\TVSeries\ClearArt\Thumbs\"
        folder(1, 2, 0) = "\TVSeries\ClearLogo\FullSize\"
        folder(1, 2, 1) = "\TVSeries\ClearLogo\Thumbs\"
        folder(2, 0, 0) = "\Music\CDArt\FullSize\"
        folder(2, 0, 1) = "\Music\CDArt\Thumbs\"
        folder(2, 1, 0) = "\Music\Banner\FullSize\"
        folder(2, 1, 1) = "\Music\Banner\Thumbs\"
        folder(2, 2, 0) = "\Music\ClearLogo\FullSize\"
        folder(2, 2, 1) = "\Music\ClearLogo\Thumbs\"

        ' initialize language array
        lang = {"English", "Deutsch", "Française", "Italiano", "Any"}
        langcode = {"EN", "DE", "FR", "IT", "##"}

        'extract dvdart.png from resources to temporary folder
        Dim png As String = DVDArt_Common._temp & "\dvdart.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.dvdart)
            image.Save(png)
        End If

        'extract dvdart_title.png from resources to temporary folder
        png = DVDArt_Common._temp & "\dvdart_title.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.dvdart_title)
            image.Save(png)
        End If

        'extract dvdart_mask.png from resources to temporary folder
        png = DVDArt_Common._temp & "\dvdart_mask.png"
        If Not FileSystem.FileExists(png) Then
            Dim image As Image = New Bitmap(My.Resources.dvdart_mask)
            image.Save(png)
        End If

    End Sub

End Class
