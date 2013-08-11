﻿Imports Microsoft.VisualBasic.FileIO

Imports MediaPortal.Configuration
Imports MediaPortal.GUI.Library

Imports System.Data.SQLite
Imports System.ComponentModel

Public Class DVDArt_Common

    Public Shared _version, folder(1, 2, 1), lang(4), langcode(4) As String
    Public Shared WithEvents bw_download0, bw_download1, bw_download2, bw_download3, bw_download4, bw_download5 As New BackgroundWorker
    Public Shared _temp As String = Environ("temp")

    Public Shared Sub wait(ByVal milliseconds As Long)
        System.Threading.Thread.Sleep(milliseconds)
    End Sub

    Public Shared Function parse(ByVal jsonresponse As String, ByVal type As String, Optional ByVal language As String = "##") As Array

        Dim details(5, 0), returndetails(5, 0), parsestring(2), keyword(3) As String
        Dim parseHD As String = Nothing
        Dim starting(2), startHD, startp, endp, len, x, y, i, j As Integer

        If type = "movie" Then
            keyword = {"hdmovielogo", "moviedisc", "movieart", "movielogo"}
        ElseIf type = "series" Then
            keyword = {"hdtvlogo", "**n/a**", "clearart", "clearlogo"}
        End If

        ' check if there are HD logos and if yes, store in a temporary variable to later on merge with movielogos

        startHD = InStr(jsonresponse, keyword(0))

        If startHD > 0 Then
            parseHD = Mid(jsonresponse, startHD, InStr(startHD, jsonresponse, "]") - startHD + 1)
        End If

        ' find the starting place of the respective sections

        For i = 0 To 2
            starting(i) = InStr(jsonresponse, keyword(i + 1))
        Next

        ' split the jsonresponse to the respective sections

        For i = 0 To 2
            If starting(i) > 0 Then
                parsestring(i) = Mid(jsonresponse, starting(i), InStr(starting(i), jsonresponse, "]") - starting(i) + 1)
            End If
        Next

        ' if there are HD logos, merge with movielogos

        If startHD > 0 Then
            parsestring(2) = Trim(parsestring(2)) & parseHD
            If starting(2) = 0 Then starting(2) = startHD
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

    End Sub

    Public Shared Function download(ByVal thumbs As String, ByVal folder(,,) As String, ByVal id As String, ByVal overwrite As Boolean, _
                                    ByVal try2download() As Boolean, ByVal type As String, Optional ByVal language As String = "##") As Array

        Dim WebClient As New System.Net.WebClient
        Dim url(5, 0) As String
        Dim found(2) As Boolean
        Dim parm As Object
        Dim y As Integer

        Dim jsonresponse As String = Nothing
        Dim fullpath As String = Nothing
        Dim thumbpath As String = Nothing

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

    Public Shared Function GetSize(ByVal path As String, ByVal image As String) As String

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

        Dim cmd As String = _temp & "\convert " & source
        Dim x As Integer

        For x = 0 To params.LongLength - 1
            cmd = cmd & " " & params(x)
        Next

        cmd = cmd & " " & destination

        Shell(cmd, vbHide)

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

        SQLconnect.ConnectionString = "Data Source=" & Config.GetFile(Config.Dir.Database, "dvdart.db3")
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

    Public Shared Sub Initialize()

        ' initialize version
        _version = "v1.0.1.2"

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

        ' initialize language array
        lang = {"English", "Deutsch", "Française", "Italiano", "Any"}
        langcode = {"EN", "DE", "FR", "IT", "##"}

    End Sub

End Class