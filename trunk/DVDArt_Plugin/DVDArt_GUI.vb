Imports Microsoft.VisualBasic.FileIO
Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports MediaPortal.Configuration
Imports ICSharpCode.SharpZipLib.Zip

Public Class DVDArt_GUI

    Public Shared checked(2) As Boolean

    Private database, thumbs, current_imdb_id, current_thetvdb_id, _lang, _lastrun As String
    Private l_import_queue As New List(Of String)
    Private l_import_index As New List(Of Integer)
    Private lvwColumnSorter = New ListViewColumnSorter()
    Private lv_url_dvdart, lv_url_clearart, lv_url_clearlogo As New ListView
    Private li_movies, li_series, li_import, li_missing As New ListViewItem
    Private WithEvents t_import_timer As New Timer
    Private WithEvents bw_compress, bw_coverart As New BackgroundWorker

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Friend Structure LVITEM
        Friend mask As Integer
        Friend iItem As Integer
        Friend subItem As Integer
        Friend state As Integer
        Friend stateMask As Integer
        <MarshalAs(UnmanagedType.LPTStr)>
        Friend lpszText As String
        Friend cchTextMax As Integer
        Friend iImage As Integer
        Friend lParam As IntPtr
        Friend iIndent As Integer
    End Structure

    Friend Structure HDITEM
        Friend mask As _mask
        Friend cxy As Long
        Friend pszText As String
        Friend hbm As Long
        Friend cchTextMax As Long
        Friend fmt As _fmt
        Friend lParam As Long
        Friend iImage As Long
        Friend iOrder As Long

        Friend Enum _mask
            format = &H4
        End Enum

        Friend Enum _fmt
            SortDown = &H200
            SortUp = &H400
        End Enum

    End Structure

    Friend Const LVM_FIRST As Integer = &H1000
    Friend Const LVM_SETITEMA As Integer = LVM_FIRST + 6
    Friend Const LVM_SETITEMW As Integer = LVM_FIRST + 76
    Friend Shared ReadOnly LVM_SETITEM As Integer = CInt(IIf(Marshal.SystemDefaultCharSize = 1, LVM_SETITEMA, LVM_SETITEMW))
    Friend Const LVM_SETEXTENDEDLISTVIEWSTYLE As Integer = LVM_FIRST + 54
    Friend Const LVIF_IMAGE As Integer = &H2
    Friend Const LVS_EX_SUBITEMIMAGES As Integer = &H2

    Friend Const LVM_GETHEADER = LVM_FIRST + 31
    Friend Const HDM_FIRST = &H1200
    Friend Const HDM_GETITEM = HDM_FIRST + 11
    Friend Const HDM_SETITEM = HDM_FIRST + 12

    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As LVITEM) As Integer
    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As HDITEM) As Integer
    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Sub SetSortArrow(ByRef lv As ListView, ByVal column As Integer, ByVal order As Integer)

        Dim hHeader As Long
        Dim HD As New HDITEM

        hHeader = SendMessage(lv.Handle, LVM_GETHEADER, 0, 0)

        For col = 0 To (lv.Columns.Count - 1)

            If col = column Then

                With HD
                    .mask = HDITEM._mask.format
                End With

                SendMessage(hHeader, HDM_GETITEM, col, HD)

                If order = SortOrder.Ascending Then
                    With HD
                        .fmt = HDITEM._fmt.SortUp
                    End With

                ElseIf order = SortOrder.Descending Then
                    With HD
                        .fmt = HDITEM._fmt.SortDown
                    End With
                End If

                SendMessage(hHeader, HDM_SETITEM, col, HD)

            End If

        Next

    End Sub

    Friend Shared Function ListView_SetItem(ByVal hwnd As IntPtr, ByRef lvi As LVITEM) As Boolean
        Return CBool(SendMessage(hwnd, LVM_SETITEM, IntPtr.Zero, lvi))
    End Function

    Private Sub wait(ByVal milliseconds As Long)

        Dim oStopWatch As Stopwatch = New Stopwatch()

        oStopWatch.Start()

        Do While oStopWatch.Elapsed.TotalMilliseconds < milliseconds
            Application.DoEvents()
        Loop

        oStopWatch.Stop()

    End Sub

    Private Sub import_timer_tick() Handles t_import_timer.Tick

        If l_import_queue.Any And Not bw_import.IsBusy Then
            FTV_api_connector("queue", Nothing, Nothing, "import")
        End If

    End Sub

    Private Sub bw_coverart_worker(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw_coverart.DoWork

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim imdb_id, images() As String
        Dim x, y, z As Integer

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand

        CheckForIllegalCrossThreadCalls = False

        For x = 0 To (lv_movies_missing.SelectedItems.Count - 1)

            imdb_id = lv_movies_missing.SelectedItems(x).SubItems.Item(4).Text

            If Not FileSystem.FileExists(DVDArt_Common.folder(0, 0, 1)) Then

                SQLcommand.CommandText = "SELECT alternatecovers, coverfullpath FROM movie_info WHERE imdb_id = """ & imdb_id & """"
                SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)

                images = Split(SQLreader(0), "|")

                z = 0

                For y = 0 To (images.Length - 1)
                    If images(y) <> String.Empty Then
                        images(z) = images(y)
                        z += 1
                    End If
                Next

                ReDim Preserve images(z - 1)

                For y = 0 To (images.Length - 1)

                    If images(y) <> SQLreader(1) Or images.Length = 1 Then

                        'create image with transparency from cover art
                        Dim fullsize, thumb As String
                        Dim params() As String = {"-resize", "500", "-gravity", "Center", "-crop", "500x500+0+0", "+repage", DVDArt_Common._temp & "\dvdart_mask.png", "-alpha", "off", "-compose", "copy_opacity", "-composite", DVDArt_Common._temp & "\dvdart.png", "-compose", "over", "-composite"}
                        fullsize = thumbs & DVDArt_Common.folder(0, 0, 0) & imdb_id & ".png"
                        DVDArt_Common.Convert("""" & images(y) & """", """" & fullsize & """", params)

                        Do While Not FileSystem.FileExists(fullsize) Or DVDArt_Common.FileInUse(fullsize)
                            wait(250)
                        Loop

                        'copy to Thumbs folder and resize to thumb size
                        thumb = thumbs & DVDArt_Common.folder(0, 0, 1) & imdb_id & ".png"
                        FileIO.FileSystem.CopyFile(fullsize, thumb, True)
                        DVDArt_Common.Resize(thumb, 200, 200)

                        If lv_movies.FindItemWithText(lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text) Is Nothing Then
                            li_movies = lv_movies.Items.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text)
                            li_movies.SubItems.Add(imdb_id)
                        End If

                        Exit For

                    End If

                Next

                SQLreader.Close()

            End If

        Next

        SQLconnect.Close()

    End Sub

    Private Sub bw_compress_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_compress.DoWork

        For Each filePath As String In IO.Directory.GetFiles(thumbs & DVDArt_Common.folder(0, 0, 0))

            If Not DVDArt_Common.FileInUse(filePath) Then
                If DVDArt_Common.GetSize(thumbs & DVDArt_Common.folder(0, 0, 0), IO.Path.GetFileName(filePath)) <> "500x500" Then DVDArt_Common.Resize(filePath)
            End If

        Next

    End Sub

    Private Sub bw_import_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_import.DoWork

        Dim parm As String = e.Argument
        Dim x, y As Integer
        Dim added, addedmissing, filenotexist(2), downloaded(2) As Boolean
        Dim lvi As LVITEM

        On Error Resume Next

        CheckForIllegalCrossThreadCalls = False

        If parm = Nothing Then

            If lv_import.Items.Count > 0 Then

                Dim SQLconnect As New SQLiteConnection()
                Dim SQLcommand As SQLiteCommand

                SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
                SQLconnect.Open()
                SQLcommand = SQLconnect.CreateCommand

                x = 0

                Do While x < lv_import.Items.Count

                    lv_import.Items(x).StateImageIndex = 0

                    added = False
                    addedmissing = False
                    lvi = Nothing

                    If lv_import.Items.Item(x).SubItems.Item(2).Text = "Movie" Then

                        For y = 0 To 2
                            filenotexist(y) = checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png")
                        Next

                        downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, lv_import.Items.Item(x).SubItems.Item(1).Text, False, filenotexist, "movie", _lang)

                        For y = 0 To 2

                            If downloaded(y) Then
                                lv_import.Items(x).StateImageIndex = 1

                                added = added Or (lv_movies.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text).Text <> Nothing)

                                If Not added Then
                                    li_movies = lv_movies.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_movies.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(x).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_movies_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_movies_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                addedmissing = True
                            End If

                            li_missing.SubItems.Add("")

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_movies_missing.Handle, lvi)

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                        SQLcommand.CommandText = "INSERT INTO processed_movies (imdb_id) VALUES(""" & lv_import.Items.Item(x).SubItems.Item(1).Text & """)"
                        SQLcommand.ExecuteNonQuery()

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Series" Then

                        For y = 1 To 2
                            filenotexist(y) = checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png")
                        Next

                        downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, lv_import.Items.Item(x).SubItems.Item(1).Text, False, filenotexist, "series", _lang)

                        For y = 1 To 2

                            If downloaded(y) Then
                                lv_import.Items(x).StateImageIndex = 1

                                added = added Or (lv_series.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text).Text <> Nothing)

                                If Not added Then
                                    li_series = lv_series.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_series.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(x).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_series_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_series_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                addedmissing = True
                            End If

                            li_missing.SubItems.Add("")

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_series_missing.Handle, lvi)

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                        SQLcommand.CommandText = "INSERT INTO processed_series (thetvdb_id) VALUES(""" & lv_import.Items.Item(x).SubItems.Item(1).Text & """)"
                        SQLcommand.ExecuteNonQuery()

                    End If

                    x += 1

                Loop

                SQLconnect.Close()

            End If

        Else

            Dim endp As Integer
            Dim title, id, type As String

            For x = 0 To (l_import_queue.Count - 1)

                If l_import_queue.Count > 0 Then
                    type = l_import_queue.Item(x)
                    endp = InStr(type, "|") - 1
                    id = Microsoft.VisualBasic.Left(type, endp)
                    endp += 1
                    type = Microsoft.VisualBasic.Right(type, Microsoft.VisualBasic.Len(l_import_queue.Item(x)) - endp)
                    endp = InStr(type, "|") - 1
                    title = Microsoft.VisualBasic.Left(type, endp)
                    endp += 1
                    type = Microsoft.VisualBasic.Right(type, Microsoft.VisualBasic.Len(type) - endp)

                    lv_import.Items(l_import_index(x)).StateImageIndex = 0

                    added = False
                    addedmissing = False
                    lvi = Nothing

                    If type = "movie" Then
                        For y = 0 To 2
                            filenotexist(y) = checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & id & ".png")
                        Next

                        downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, id, False, filenotexist, type, _lang)

                        For y = 0 To 2
                            If downloaded(y) Then
                                lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                added = added Or (lv_movies.FindItemWithText(title).Text <> Nothing)

                                If Not added Then
                                    li_movies = lv_movies.Items.Add(title)
                                    li_movies.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(l_import_index(x)).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_movies_missing.Items.Add(title)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_movies_missing.Items.Add(title)
                                addedmissing = True
                            End If

                            li_missing.SubItems.Add("")

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_movies_missing.Handle, lvi)
                        Next
                    ElseIf type = "series" Then
                        For y = 1 To 2
                            filenotexist(y) = checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & id & ".png")
                        Next

                        downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, id, False, filenotexist, type, _lang)

                        For y = 1 To 2

                            If downloaded(y) Then
                                lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                added = added Or (lv_movies.FindItemWithText(title).Text <> Nothing)

                                If Not added Then
                                    li_series = lv_series.Items.Add(title)
                                    li_series.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(l_import_index(x)).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_series_missing.Items.Add(title)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_series_missing.Items.Add(title)
                                addedmissing = True
                            End If

                            li_missing.SubItems.Add("")

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_series_missing.Handle, lvi)

                        Next
                    End If

                    If addedmissing Then
                        li_missing.SubItems.Add(id)
                    Else
                        li_missing.SubItems.RemoveAt(li_missing.Index)
                    End If

                    l_import_queue.RemoveAt(x)
                    l_import_index.RemoveAt(x)
                    x -= 1
                End If

            Next

        End If

    End Sub

    Private Sub use_coverart(mode As String)

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim imdb_id, images() As String
        Dim x, y, z, count As Integer

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand

        If mode = "movies" Then
            count = lv_movies.SelectedItems.Count - 1
        Else
            count = lv_movies_missing.SelectedItems.Count - 1
        End If

        For x = 0 To count

            If mode = "movies" Then
                imdb_id = lv_movies.SelectedItems(x).SubItems.Item(1).Text
            Else
                imdb_id = lv_movies_missing.SelectedItems(x).SubItems.Item(4).Text
            End If

            If Not FileSystem.FileExists(DVDArt_Common.folder(0, 0, 1)) Then

                SQLcommand.CommandText = "SELECT alternatecovers, coverfullpath FROM movie_info WHERE imdb_id = """ & imdb_id & """"
                SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)

                z = 0

                If Not IsDBNull(SQLreader(0)) Then

                    images = Split(SQLreader(0), "|")

                    For y = 0 To (images.Length - 1)
                        If Trim(images(y)) <> String.Empty Then
                            images(z) = images(y)
                            z += 1
                        End If
                    Next

                End If

                SQLreader.Close()

                If z = 0 Then
                    MsgBox("No CoverArt Available", MsgBoxStyle.Exclamation)
                Else
                    ReDim Preserve images(z - 1)
                    Dim coverart As New DVDArt_CoverArt(images, thumbs, imdb_id)
                    coverart.ShowDialog()
                End If

                If mode = "missing" Then
                    If lv_movies.FindItemWithText(lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text) Is Nothing Then
                        li_movies = lv_movies.Items.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text)
                        li_movies.SubItems.Add(imdb_id)
                    End If
                End If

            End If

        Next

        SQLconnect.Close()

    End Sub

    Private Sub Restart_Importer()

        Me.Cursor = Cursors.WaitCursor

        Do While bw_import.IsBusy
            wait(200)
        Loop

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader

        ' Read already processed movies to identify newly imported ones in movingpictures

        Dim x As Integer
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

        ' Read movingpictures database and populate list box

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
                End If

            End If

        End While

        SQLconnect.Close()

        Me.Cursor = Cursors.Default

        If lv_import.Items.Count > 0 Then
            FTV_api_connector(Nothing, Nothing, "movie", "import")
        End If

    End Sub

    Private Sub FTV_api_connector(ByVal id As String, ByVal url() As String, ByVal type As String, ByVal mode As String)

        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor

        Dim x As Integer
        Dim details(5, 0) As String
        Dim WebClient As New System.Net.WebClient

        x = 0

        If mode = "preview" Then

            Dim jsonresponse As String

            jsonresponse = DVDArt_Common.JSON_request(id, type, "2")

            If jsonresponse <> "null" Then

                details = DVDArt_Common.parse(jsonresponse, type)

                Dim ImageInBytes() As Byte
                Dim stream As System.IO.MemoryStream

                For x = 0 To (details.Length / 6) - 1

                    If cb_DVDArt.Checked = True And details(0, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(0, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_dvdart.Images.Add(imagekey, Image.FromStream(stream))
                        If type = "movie" Then lv_movie_dvdart.Items.Add(details(1, x), imagekey)
                        lv_url_dvdart.Items.Add(details(0, x), imagekey)
                    End If

                    If cb_ClearArt.Checked = True And details(2, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(2, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_clearart.Images.Add(imagekey, Image.FromStream(stream))
                        If type = "movie" Then
                            lv_movie_clearart.Items.Add(details(3, x), imagekey)
                        ElseIf type = "series" Then
                            lv_serie_clearart.Items.Add(details(3, x), imagekey)
                        End If
                        lv_url_clearart.Items.Add(details(2, x), imagekey)
                    End If

                    If cb_ClearLogo.Checked = True And details(4, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(4, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_clearlogo.Images.Add(imagekey, Image.FromStream(stream))
                        If type = "movie" Then
                            lv_movie_clearlogo.Items.Add(details(5, x), imagekey)
                        ElseIf type = "series" Then
                            lv_serie_clearlogo.Items.Add(details(5, x), imagekey)
                        End If
                        lv_url_clearlogo.Items.Add(details(4, x), imagekey)
                    End If

                Next
            End If

        ElseIf mode = "selected" Then

            Dim parm As Object
            Dim fullpath, thumbpath As String

            'id = current_imdb_id

            For x = 0 To 2

                If checked(x) And url(x) <> Nothing Then

                    If type = "movie" Then
                        fullpath = thumbs & DVDArt_Common.folder(0, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & id & ".png"
                    ElseIf type = "series" Then
                        If x = 0 Then Continue For
                        fullpath = thumbs & DVDArt_Common.folder(1, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(1, x, 1) & id & ".png"
                    End If

                    parm = thumbpath & "|" & url(x) & "/preview"

                    Do
                        If Not DVDArt_Common.bw_download0.IsBusy Then
                            DVDArt_Common.bw_download0.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download0.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download2.IsBusy Then
                            DVDArt_Common.bw_download2.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download2.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download4.IsBusy Then
                            DVDArt_Common.bw_download4.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download4.RunWorkerAsync(parm)
                            Exit Do
                        Else
                            wait(250)
                        End If
                    Loop

                    If x = 0 Then parm = fullpath & "|" & url(x) & "|shrink" Else parm = fullpath & "|" & url(x)

                    Do
                        If Not DVDArt_Common.bw_download1.IsBusy Then
                            DVDArt_Common.bw_download1.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download1.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download3.IsBusy Then
                            DVDArt_Common.bw_download3.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download3.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download5.IsBusy Then
                            DVDArt_Common.bw_download5.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download5.RunWorkerAsync(parm)
                            Exit Do
                        Else
                            wait(250)
                        End If
                    Loop

                End If

            Next

        ElseIf mode = "import" Then

            Dim parm As Object = id

            bw_import.WorkerSupportsCancellation = True
            bw_import.RunWorkerAsync(parm)

        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Load_Movie_List()

        If FileSystem.FileExists(database & "\movingpictures.db3") Then
            If tbc_main.TabPages.Contains(tp_MovingPictures) Then tbc_main.TabPages.Remove(tp_MovingPictures)
            tbc_main.TabPages.Insert(0, tp_MovingPictures)
        Else
            tbc_main.TabPages.Remove(tp_MovingPictures)
            Exit Sub
        End If

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_movies(), imdb_id_in_mp() As String
        Dim x As Integer = 0
        Dim found, missing As Boolean

        If Not FileSystem.FileExists(database & "\dvdart.db3") Then SQLiteConnection.CreateFile(database & "\dvdart.db3")

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_movies(imdb_id TEXT)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed movies to identify newly imported ones in movingpictures

        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies WHERE imdb_id is not Null ORDER BY imdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_movies(x)
            processed_movies(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_movies(0)

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3"

        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info WHERE imdb_id is not Null and title is not Null ORDER BY sortby"
        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_movies_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If processed_movies.Contains(SQLreader(0)) Then

                    found = False
                    missing = False

                    For y = 0 To 2
                        fileexist(y) = FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & SQLreader(0) & ".png")
                        If Not found Then found = checked(y) And fileexist(y)
                        If Not missing Then missing = checked(y) And Not fileexist(y)
                    Next

                    If found Then
                        li_movies = lv_movies.Items.Add(SQLreader(1))
                        li_movies.SubItems.Add(SQLreader(0))
                    End If

                    If missing Then

                        li_missing = lv_movies_missing.Items.Add(SQLreader(1))

                        lvi = Nothing

                        li_missing.ForeColor = Color.White

                        For y = 0 To 2

                            li_missing.SubItems.Add("")
                            'If fileexist(y) Then
                            'li_missing.SubItems.Add("1")
                            'Else
                            'li_missing.SubItems.Add("0")
                            'End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(y) Then
                                If fileexist(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_movies_missing.Handle, lvi)

                        Next

                        li_missing.ForeColor = Color.Black

                        li_missing.SubItems.Add(SQLreader(0))

                    End If

                Else
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                    li_import.SubItems.Add("Movie")
                End If

                ReDim Preserve imdb_id_in_mp(x)
                imdb_id_in_mp(x) = SQLreader(0)
                x += 1

            End If

        End While

        If x = 0 Then ReDim Preserve imdb_id_in_mp(0)

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            FTV_api_connector(Nothing, Nothing, "movie", "import")
        End If

        ' remove imdb ids from dvdart that no longer exist in movingpictures

        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies WHERE imdb_id is not Null ORDER BY imdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not imdb_id_in_mp.Contains(SQLreader(0)) Then

                SQLdelete.CommandText = "DELETE FROM processed_movies WHERE imdb_id = """ & SQLreader(0) & """"
                SQLdelete.ExecuteNonQuery()

            End If

        End While

        SQLconnect.Close()

    End Sub

    Private Sub Load_Serie_List()

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_series(), thetvdb_id_in_tv() As String
        Dim x As Integer = 0

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        If Not FileSystem.FileExists(database & "\dvdart.db3") Then SQLiteConnection.CreateFile(database & "\dvdart.db3")

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_series(thetvdb_id TEXT)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed movies to identify newly imported ones in TVSeries

        SQLcommand.CommandText = "SELECT thetvdb_id FROM processed_series WHERE thetvdb_id is not Null ORDER BY thetvdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_series(x)
            processed_series(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_series(0)

        If tbc_main.TabPages.Contains(tp_TVSeries) Then tbc_main.TabPages.Remove(tp_TVSeries)

        If FileSystem.FileExists(database & "\TVSeriesDatabase4.db3") Then
            If tbc_main.TabPages.Contains(tp_MovingPictures) Then tbc_main.TabPages.Insert(1, tp_TVSeries) Else tbc_main.TabPages.Insert(0, tp_TVSeries)
        Else
            Exit Sub
        End If

        SQLconnect.ConnectionString = "Data Source=" & database & "\TVSeriesDatabase4.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT id, pretty_name FROM online_series WHERE id is not Null and pretty_name is not Null ORDER BY sortname"
        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_series_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If processed_series.Contains(SQLreader(0)) Then

                    For y = 1 To 2
                        fileexist(y) = checked(y) And FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & SQLreader(0) & ".png")
                    Next

                    If fileexist(1) Or fileexist(2) Then
                        li_series = lv_series.Items.Add(SQLreader(1))
                        li_series.SubItems.Add(SQLreader(0))
                    End If

                    If Not fileexist(1) Or Not fileexist(2) Then

                        li_missing = lv_series_missing.Items.Add(SQLreader(1))

                        lvi = Nothing

                        For y = 1 To 2

                            li_missing.SubItems.Add("")

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(y) Then
                                If fileexist(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_series_missing.Handle, lvi)

                        Next

                        li_missing.SubItems.Add(SQLreader(0))

                    End If

                Else
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                    li_import.SubItems.Add("Series")
                End If

                ReDim Preserve thetvdb_id_in_tv(x)
                thetvdb_id_in_tv(x) = SQLreader(0)
                x += 1

            End If

        End While

        If x = 0 Then ReDim Preserve thetvdb_id_in_tv(0)

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            FTV_api_connector(Nothing, Nothing, "series", "import")
        End If

        ' remove theTVDB ids from dvdart that no longer exist in TVSeries

        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT thetvdb_id FROM processed_series WHERE thetvdb_id is not Null ORDER BY thetvdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not thetvdb_id_in_tv.Contains(SQLreader(0)) Then

                SQLdelete.CommandText = "DELETE FROM processed_series WHERE thetvdb_id = """ & SQLreader(0) & """"
                SQLdelete.ExecuteNonQuery()

            End If

        End While

        SQLconnect.Close()

    End Sub

    Private Sub load_image(ByRef pb_image As PictureBox, ByVal path As String)

        On Error Resume Next

        If FileSystem.FileExists(path) Then

            Do Until Not DVDArt_Common.FileInUse(path)
                wait(200)
            Loop

            Dim fs As System.IO.FileStream
            fs = New System.IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
            pb_image.Image = System.Drawing.Image.FromStream(fs)
            fs.Close()

            pb_image.Tag = Nothing
        Else
            pb_image.Image = Nothing
            pb_image.Tag = Nothing
        End If

    End Sub

    Private Sub lv_movies_GotFocus(sender As Object, e As System.EventArgs) Handles lv_movies.GotFocus, lv_movies_missing.GotFocus
        cms_found.Items.Item(2).Visible = True
        cms_missing.Items.Item(3).Visible = True
        cms_missing.Items.Item(4).Visible = True
    End Sub

    Private Sub lv_movies_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movies.SelectedIndexChanged

        If current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        il_dvdart.Images.Clear()
        lv_movie_dvdart.Items.Clear()
        lv_url_dvdart.Items.Clear()
        il_clearart.Images.Clear()
        lv_movie_clearart.Items.Clear()
        lv_url_clearart.Items.Clear()
        il_clearlogo.Images.Clear()
        lv_movie_clearlogo.Items.Clear()
        lv_url_clearlogo.Items.Clear()

        url = {pb_movie_dvdart.Tag, pb_movie_clearart.Tag, pb_movie_clearlogo.Tag}

        If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Then
            FTV_api_connector(current_imdb_id, url, "movie", "selected")
        End If

        current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text
        l_imdb_id.Text = current_imdb_id

        For x = 0 To 2

            If checked(x) Then
                thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & current_imdb_id & ".png"
                If x = 0 Then
                    load_image(pb_movie_dvdart, thumbpath)

                    If pb_movie_dvdart.Image IsNot Nothing Then
                        l_movie_size.Text = DVDArt_Common.GetSize(thumbs & DVDArt_Common.folder(0, x, 0), current_imdb_id & ".png")
                        If l_movie_size.Text = "500x500" Then b_movie_compress.Visible = False Else b_movie_compress.Visible = True
                        b_movie_preview.Visible = Not b_movie_compress.Visible
                    Else
                        l_movie_size.Text = Nothing
                        b_movie_compress.Visible = False
                        b_movie_preview.Visible = False
                    End If

                ElseIf x = 1 Then
                    load_image(pb_movie_clearart, thumbpath)
                    b_movie_deleteart.Visible = (pb_movie_clearart.Image IsNot Nothing)
                ElseIf x = 2 Then
                    load_image(pb_movie_clearlogo, thumbpath)
                    b_movie_deletelogo.Visible = (pb_movie_clearlogo.Image IsNot Nothing)
                End If
            End If

        Next

    End Sub

    Private Sub lv_series_GotFocus(sender As Object, e As System.EventArgs) Handles lv_series.GotFocus, lv_series_missing.GotFocus
        cms_found.Items.Item(2).Visible = False
        cms_missing.Items.Item(3).Visible = False
        cms_missing.Items.Item(4).Visible = False
    End Sub

    Private Sub lv_series_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_series.SelectedIndexChanged

        If current_thetvdb_id = lv_series.FocusedItem.SubItems.Item(1).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        il_dvdart.Images.Clear()
        il_clearart.Images.Clear()
        lv_serie_clearart.Items.Clear()
        lv_url_clearart.Items.Clear()
        il_clearlogo.Images.Clear()
        lv_serie_clearlogo.Items.Clear()
        lv_url_clearlogo.Items.Clear()

        url = {Nothing, pb_serie_clearart.Tag, pb_serie_clearlogo.Tag}

        If url(1) <> Nothing Or url(2) <> Nothing Then
            FTV_api_connector(current_thetvdb_id, url, "series", "selected")
        End If

        current_thetvdb_id = lv_series.FocusedItem.SubItems.Item(1).Text
        l_thetvdb_id.Text = current_thetvdb_id

        For x = 1 To 2

            If checked(x) Then
                thumbpath = thumbs & DVDArt_Common.folder(1, x, 1) & current_thetvdb_id & ".png"
                If x = 1 Then
                    load_image(pb_serie_clearart, thumbpath)
                    b_serie_deleteart.Visible = (pb_serie_clearart.Image IsNot Nothing)
                ElseIf x = 2 Then
                    load_image(pb_serie_clearlogo, thumbpath)
                    b_serie_deletelogo.Visible = (pb_serie_clearlogo.Image IsNot Nothing)
                End If
            End If

        Next

    End Sub

    Private Sub lv_movies_missing_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv_movies_missing.GotFocus
        cms_missing.Items.Item(3).Visible = True
    End Sub

    Private Sub lv_movies_missing_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lv_movies_missing.ColumnClick

        lv_movies_missing.ListViewItemSorter = lvwColumnSorter

        ' Determine if the clicked column is already the column that is 
        ' being sorted.
        If (e.Column = lvwColumnSorter.SortColumn) Then
            ' Reverse the current sort direction for this column.
            If (lvwColumnSorter.Order = SortOrder.Ascending) Then
                lvwColumnSorter.Order = SortOrder.Descending
            Else
                lvwColumnSorter.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = e.Column
            lvwColumnSorter.Order = SortOrder.Ascending
        End If

        SetSortArrow(lv_movies_missing, e.Column, lvwColumnSorter.order)

        ' Perform the sort with these new sort options.
        lv_movies_missing.Sort()

    End Sub

    Private Sub lv_series_missing_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv_series_missing.GotFocus
        cms_missing.Items.Item(3).Visible = False
    End Sub

    Private Sub cms_found_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_found.ItemClicked

        If cms_found.SourceControl.Name = "lv_movies" Then

            If e.ClickedItem.Text = "Refresh artwork from online" Then
                If lv_movies.SelectedItems.Count > 0 Then
                    FTV_api_connector(lv_movies.SelectedItems(0).SubItems.Item(1).Text, Nothing, "movie", "preview")
                Else
                    MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                If lv_movies.SelectedItems.Count > 0 Then
                    Dim title As String = lv_movies.SelectedItems(0).SubItems(0).Text
                    Dim imdb_id As String = lv_movies.SelectedItems(0).SubItems.Item(1).Text
                    Dim upload As New DVDArt_ManualUpload(imdb_id, title, "movie")
                    upload.ShowDialog()
                    current_imdb_id = Nothing
                    lv_movies_SelectedIndexChanged(sender, e)
                Else
                    MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Select/Edit Cover Art for DVD Art" Then
                If lv_movies.SelectedItems.Count > 0 Then
                    use_coverart("movies")
                Else
                    MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
                End If
            End If

        ElseIf cms_found.SourceControl.Name = "lv_series" Then

            If e.ClickedItem.Text = "Refresh artwork from online" Then
                If lv_series.SelectedItems.Count > 0 Then
                    FTV_api_connector(lv_series.SelectedItems(0).SubItems.Item(1).Text, Nothing, "series", "preview")
                Else
                    MsgBox("Please select a series.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                If lv_series.SelectedItems.Count > 0 Then
                    Dim title As String = lv_series.SelectedItems(0).SubItems(0).Text
                    Dim thetvdb_id As String = lv_series.SelectedItems(0).SubItems.Item(1).Text
                    Dim upload As New DVDArt_ManualUpload(thetvdb_id, title, "series")
                    upload.ShowDialog()
                    current_thetvdb_id = Nothing
                    lv_series_SelectedIndexChanged(sender, e)
                Else
                    MsgBox("Please select a series.", MsgBoxStyle.Critical, Nothing)
                End If
                'ElseIf e.ClickedItem.Text = "Compress all DVDArt to 500x500" Then
                'bw_compress.WorkerSupportsCancellation = True
                'bw_compress.RunWorkerAsync()
            End If

        End If

    End Sub

    Private Sub cms_missing_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_missing.ItemClicked

        If cms_missing.SourceControl.Name = "lv_movies_missing" Then

            If e.ClickedItem.Text = "Send to importer" Then
                If lv_movies_missing.SelectedItems.Count > 0 Then
                    For x As Integer = 0 To (lv_movies_missing.SelectedItems.Count - 1)
                        li_import = lv_import.Items.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text)
                        li_import.SubItems.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(4).Text)
                        li_import.SubItems.Add("Movie")
                        l_import_queue.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(4).Text & "|" & lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text & "|movie")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    Next

                    For x = (lv_movies_missing.SelectedIndices.Count - 1) To 0 Step -1
                        lv_movies_missing.Items.RemoveAt(lv_movies_missing.SelectedIndices(x))
                    Next

                    If Not bw_import.IsBusy Then
                        FTV_api_connector("queue", Nothing, "movie", "import")
                    End If
                Else
                    MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
                For x = 0 To (lv_movies_missing.Items.Count - 1)
                    If lv_movies_missing.Items.Count > 0 Then
                        li_import = lv_import.Items.Add(lv_movies_missing.Items.Item(x).SubItems(0).Text)
                        li_import.SubItems.Add(lv_movies_missing.Items.Item(x).SubItems(4).Text)
                        li_import.SubItems.Add("Movie")

                        l_import_queue.Add(lv_movies_missing.Items.Item(x).SubItems(4).Text & "|" & lv_movies_missing.Items.Item(x).SubItems(0).Text & "|movie")
                        l_import_index.Add(lv_import.Items.Count - 1)

                        lv_movies_missing.Items.RemoveAt(x)

                        x -= 1
                    End If
                Next

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "movie", "import")
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                For x As Integer = 0 To (lv_movies_missing.SelectedItems.Count - 1)

                    Dim title As String = lv_movies_missing.SelectedItems(x).SubItems(0).Text
                    Dim imdb_id As String = lv_movies_missing.SelectedItems(x).SubItems.Item(4).Text
                    Dim upload As New DVDArt_ManualUpload(imdb_id, title, "movie")
                    upload.ShowDialog()

                    If FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, 0, 1) & imdb_id & ".png") Or _
                       FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, 1, 1) & imdb_id & ".png") Or _
                       FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, 2, 1) & imdb_id & ".png") Then

                        li_import = lv_import.Items.Add(title)
                        li_import.SubItems.Add(imdb_id)
                        li_import.SubItems.Add("Movie")
                        l_import_queue.Add(imdb_id & "|" & title & "|movie")
                        l_import_index.Add(lv_import.Items.Count - 1)

                        lv_movies_missing.Items.Remove(lv_movies_missing.SelectedItems(x))

                        If lv_movies.FindItemWithText(title) Is Nothing Then
                            li_movies = lv_movies.Items.Add(title)
                            li_movies.SubItems.Add(imdb_id)
                            li_movies.SubItems.Add("Movie")
                        End If

                    End If

                Next
            ElseIf e.ClickedItem.Text = "Use Cover Art for DVD Art" Then

                If Not bw_coverart.IsBusy Then
                    bw_coverart.WorkerSupportsCancellation = True
                    bw_coverart.RunWorkerAsync()
                End If

            ElseIf e.ClickedItem.Text = "Select/Edit Cover Art for DVD Art" Then

                use_coverart("missing")

            End If

        ElseIf cms_missing.SourceControl.Name = "lv_series_missing" Then

            If e.ClickedItem.Text = "Send to importer" Then
                If lv_series_missing.SelectedItems.Count > 0 Then
                    For x As Integer = 0 To (lv_series_missing.SelectedItems.Count - 1)
                        li_import = lv_import.Items.Add(lv_series_missing.SelectedItems(x).SubItems.Item(0).Text)
                        li_import.SubItems.Add(lv_series_missing.SelectedItems(x).SubItems.Item(3).Text)
                        li_import.SubItems.Add("Series")
                        l_import_queue.Add(lv_series_missing.SelectedItems(x).SubItems.Item(3).Text & "|" & lv_series_missing.SelectedItems(x).SubItems.Item(0).Text & "|series")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    Next

                    For x = (lv_series_missing.SelectedIndices.Count - 1) To 0 Step -1
                        lv_series_missing.Items.RemoveAt(lv_series_missing.SelectedIndices(x))
                    Next

                    If Not bw_import.IsBusy Then
                        FTV_api_connector("queue", Nothing, "series", "import")
                    End If
                Else
                    MsgBox("Please select a series.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
                For x = 0 To (lv_series_missing.Items.Count - 1)
                    If lv_series_missing.Items.Count > 0 Then
                        li_import = lv_import.Items.Add(lv_series_missing.Items.Item(x).SubItems(0).Text)
                        li_import.SubItems.Add(lv_series_missing.Items.Item(x).SubItems(3).Text)
                        li_import.SubItems.Add("Series")

                        l_import_queue.Add(lv_series_missing.Items.Item(x).SubItems(3).Text & "|" & lv_series_missing.Items.Item(x).SubItems(0).Text & "|series")
                        l_import_index.Add(lv_import.Items.Count - 1)

                        lv_series_missing.Items.RemoveAt(x)

                        x -= 1
                    End If
                Next

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "series", "import")
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                For x As Integer = 0 To (lv_series_missing.SelectedItems.Count - 1)

                    Dim title As String = lv_series_missing.SelectedItems(x).SubItems(0).Text
                    Dim thetvdb_id As String = lv_series_missing.SelectedItems(x).SubItems.Item(3).Text
                    Dim upload As New DVDArt_ManualUpload(thetvdb_id, title, "series")
                    upload.ShowDialog()

                    If FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, 1, 1) & thetvdb_id & ".png") Or _
                       FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, 2, 1) & thetvdb_id & ".png") Then

                        li_import = lv_import.Items.Add(title)
                        li_import.SubItems.Add(thetvdb_id)
                        li_import.SubItems.Add("Series")
                        l_import_queue.Add(thetvdb_id & "|" & title & "|series")
                        l_import_index.Add(lv_import.Items.Count - 1)

                        lv_series_missing.Items.Remove(lv_series_missing.SelectedItems(x))

                        If lv_series.FindItemWithText(title) Is Nothing Then
                            li_series = lv_series.Items.Add(title)
                            li_series.SubItems.Add(thetvdb_id)
                        End If

                    End If

                Next
            End If

        End If

    End Sub

    Private Sub cms_import_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_import.ItemClicked

        If e.ClickedItem.Text = "Restart Importer" Then
            Restart_Importer()
        End If

    End Sub

    Private Sub lv_movie_dvdart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movie_dvdart.SelectedIndexChanged

        For Each item In lv_movie_dvdart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_dvdart.Images(itemkey)

            pb_movie_dvdart.Image = image
            pb_movie_dvdart.Tag = lv_url_dvdart.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_movie_clearart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movie_clearart.SelectedIndexChanged

        For Each item In lv_movie_clearart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearart.Images(itemkey)

            pb_movie_clearart.Image = image
            pb_movie_clearart.Tag = lv_url_clearart.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_movie_clearlogo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movie_clearlogo.SelectedIndexChanged

        For Each item In lv_movie_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_movie_clearlogo.Image = image
            pb_movie_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_serie_clearart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_serie_clearart.SelectedIndexChanged

        For Each item In lv_serie_clearart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearart.Images(itemkey)

            pb_serie_clearart.Image = image
            pb_serie_clearart.Tag = lv_url_clearart.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_serie_clearlogo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_serie_clearlogo.SelectedIndexChanged

        For Each item In lv_serie_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_serie_clearlogo.Image = image
            pb_serie_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

    End Sub

    Private Sub Create_Folder_Structure()

        ' Check and create directory structure

        If Not FileSystem.FileExists(database + "\movingpictures.db3") And Not FileSystem.FileExists(database + "\TVSeriesDatabase4.db3") Then
            Application.Exit()
        End If

        ' DVDArt
        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\DVDArt") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt")

        ' ClearArt
        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\ClearArt") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\ClearArt")
        If Not FileSystem.DirectoryExists(thumbs & "\TVSeries\ClearArt") Then FileSystem.CreateDirectory(thumbs & "\TVSeries\ClearArt")

        ' ClearLogo
        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\ClearLogo") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\ClearLogo")
        If Not FileSystem.DirectoryExists(thumbs & "\TVSeries\ClearLogo") Then FileSystem.CreateDirectory(thumbs & "\TVSeries\ClearLogo")

        For x = 0 To 2
            For y = 0 To 1
                For z = 0 To 1
                    If DVDArt_Common.folder(z, x, y) IsNot Nothing Then
                        If Not FileSystem.DirectoryExists(thumbs & DVDArt_Common.folder(z, x, y)) Then FileSystem.CreateDirectory(thumbs & DVDArt_Common.folder(z, x, y))
                    End If
                Next
            Next
        Next

    End Sub

    Private Sub Set_Settings()

        On Error Resume Next

        Using XMLwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            XMLwriter.SetValue("Settings", "delay", nud_delay.Value)
            XMLwriter.SetValue("Settings", "delay value", cb_delay.Text)
            XMLwriter.SetValue("Settings", "CPU utilisation", mtb_cpu.Text)
            XMLwriter.SetValue("Settings", "scraping", nud_scraping.Value)
            XMLwriter.SetValue("Settings", "scraping value", cb_scraping.Text)
            XMLwriter.SetValue("Settings", "missing", nud_missing.Value)
            XMLwriter.SetValue("Settings", "missing value", cb_missing.Text)
            XMLwriter.SetValueAsBool("Scraper", "dvdart", cb_DVDArt.Checked)
            XMLwriter.SetValueAsBool("Scraper", "clearart", cb_ClearArt.Checked)
            XMLwriter.SetValueAsBool("Scraper", "clearlogo", cb_ClearLogo.Checked)
            XMLwriter.SetValue("Scraper", "language", DVDArt_Common.langcode(Array.IndexOf(DVDArt_Common.lang, cb_language.Text)))

            If _lastrun = Nothing Then XMLwriter.SetValue("Scheduler", "lastrun", Now)

        End Using

        MediaPortal.Profile.Settings.SaveCache()

    End Sub

    Private Sub Get_Settings()

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            nud_delay.Value = XMLreader.GetValueAsInt("Settings", "delay", 1)
            cb_delay.Text = XMLreader.GetValueAsString("Settings", "delay value", "minutes")
            mtb_cpu.Text = XMLreader.GetValueAsString("Settings", "CPU utilisation", 30)
            nud_scraping.Value = XMLreader.GetValueAsInt("Settings", "scraping", 15)
            cb_scraping.Text = XMLreader.GetValueAsString("Settings", "scraping value", "minutes")
            nud_missing.Value = XMLreader.GetValueAsInt("Settings", "missing", 0)
            cb_missing.Text = XMLreader.GetValueAsString("Settings", "missing value", "disabled")
            cb_DVDArt.Checked = XMLreader.GetValueAsBool("Scraper", "dvdart", False)
            cb_ClearArt.Checked = XMLreader.GetValueAsBool("Scraper", "clearart", False)
            cb_ClearLogo.Checked = XMLreader.GetValueAsBool("Scraper", "clearlogo", False)
            _lang = XMLreader.GetValueAsString("Scraper", "language", "##")
            cb_language.Text = DVDArt_Common.lang(Array.IndexOf(DVDArt_Common.langcode, _lang))
            _lastrun = XMLreader.GetValueAsString("Settings", "lastrun", Nothing)

        End Using

    End Sub

    Private Sub cb_DVDArt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_DVDArt.CheckedChanged

        checked(0) = cb_DVDArt.Checked

        If cb_DVDArt.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
            If tbc_movies.TabCount = 0 Then tbc_movies.TabPages.Add(tp_Movie_DVDArt) Else tbc_movies.TabPages.Insert(0, tp_Movie_DVDArt)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
        End If

    End Sub

    Private Sub cb_ClearArt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearArt.CheckedChanged

        checked(1) = cb_ClearArt.Checked

        If cb_ClearArt.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_ClearArt) Then tbc_movies.TabPages.Remove(tp_Movie_ClearArt)
            If tbc_series.TabPages.Contains(tp_Serie_ClearArt) Then tbc_series.TabPages.Remove(tp_Serie_ClearArt)

            If tbc_movies.TabCount > 0 Then
                If tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then
                    tbc_movies.TabPages.Insert(1, tp_Movie_ClearArt)
                Else
                    tbc_movies.TabPages.Insert(tbc_movies.TabCount - 1, tp_Movie_ClearArt)
                End If
            Else
                tbc_movies.TabPages.Add(tp_Movie_ClearArt)
            End If

            If tbc_series.TabCount = 0 Then tbc_series.TabPages.Add(tp_Serie_ClearArt) Else tbc_series.TabPages.Insert(0, tp_Serie_ClearArt)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_ClearArt)
            tbc_series.TabPages.Remove(tp_Serie_ClearArt)
        End If

    End Sub

    Private Sub cb_ClearLogo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearLogo.CheckedChanged

        checked(2) = cb_ClearLogo.Checked

        If cb_ClearLogo.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)
            If tbc_series.TabPages.Contains(tp_Serie_ClearLogo) Then tbc_series.TabPages.Remove(tp_Serie_ClearLogo)

            tbc_movies.TabPages.Add(tp_Movie_ClearLogo)
            tbc_series.TabPages.Add(tp_Serie_ClearLogo)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)
            tbc_series.TabPages.Remove(tp_Serie_ClearLogo)
        End If

    End Sub

    Private Sub b_movie_compress_Click(sender As System.Object, e As System.EventArgs) Handles b_movie_compress.Click

        DVDArt_Common.Resize(thumbs & DVDArt_Common.folder(0, 0, 0) & current_imdb_id & ".png")

        l_movie_size.Text = DVDArt_Common.GetSize(thumbs & DVDArt_Common.folder(0, 0, 0), current_imdb_id & ".png")

        If l_movie_size.Text = "500x500" Then b_movie_compress.Visible = False Else b_movie_compress.Visible = True

        b_movie_preview.Visible = Not b_movie_compress.Visible

    End Sub

    Private Sub b_movie_deleteart_Click(sender As System.Object, e As System.EventArgs) Handles b_movie_deleteart.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(0, 1, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(0, 1, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_movie_clearart.Image = Nothing
            pb_movie_clearart.Tag = Nothing
            b_movie_deleteart.Visible = False
        End If
    End Sub

    Private Sub b_movie_deletelogo_Click(sender As System.Object, e As System.EventArgs) Handles b_movie_deletelogo.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(0, 2, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(0, 2, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_movie_clearlogo.Image = Nothing
            pb_movie_clearlogo.Tag = Nothing
            b_movie_deletelogo.Visible = False
        End If
    End Sub

    Private Sub b_movie_preview_Click(sender As System.Object, e As System.EventArgs) Handles b_movie_preview.Click
        Dim preview As New DVDArt_Preview(thumbs & DVDArt_Common.folder(0, 0, 0) & current_imdb_id & ".png")
        preview.Show()
    End Sub

    Private Sub b_serie_deleteart_Click(sender As System.Object, e As System.EventArgs) Handles b_serie_deleteart.Click
        If lv_series.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(1, 1, 0) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(1, 1, 1) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_serie_clearart.Image = Nothing
            pb_serie_clearart.Tag = Nothing
            b_serie_deleteart.Visible = False
        End If
    End Sub

    Private Sub b_serie_deletelogo_Click(sender As System.Object, e As System.EventArgs) Handles b_serie_deletelogo.Click
        If lv_series.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(1, 2, 0) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(1, 2, 1) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_serie_clearlogo.Image = Nothing
            pb_serie_clearlogo.Tag = Nothing
            b_serie_deletelogo.Visible = False
        End If
    End Sub

    Private Sub DVDArt_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Err.Number = 0 Then

            Me.Cursor = Cursors.WaitCursor

            Dim url() As String = {pb_movie_dvdart.Tag, pb_movie_clearart.Tag, pb_movie_clearlogo.Tag}

            t_import_timer.Stop()

            If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Then
                FTV_api_connector(current_imdb_id, url, "movie", "selected")
            End If

            If DVDArt_Common.bw_download0.IsBusy Or DVDArt_Common.bw_download1.IsBusy Or DVDArt_Common.bw_download2.IsBusy Or DVDArt_Common.bw_download3.IsBusy Or DVDArt_Common.bw_download4.IsBusy Or DVDArt_Common.bw_download5.IsBusy Or bw_compress.IsBusy Then
                wait(5000)
            End If

            If DVDArt_Common.bw_download0.IsBusy Then DVDArt_Common.bw_download0.CancelAsync()
            If DVDArt_Common.bw_download1.IsBusy Then DVDArt_Common.bw_download1.CancelAsync()
            If DVDArt_Common.bw_download2.IsBusy Then DVDArt_Common.bw_download2.CancelAsync()
            If DVDArt_Common.bw_download3.IsBusy Then DVDArt_Common.bw_download3.CancelAsync()
            If DVDArt_Common.bw_download4.IsBusy Then DVDArt_Common.bw_download4.CancelAsync()
            If DVDArt_Common.bw_download5.IsBusy Then DVDArt_Common.bw_download5.CancelAsync()
            If bw_compress.IsBusy Then bw_compress.CancelAsync()
            If bw_coverart.IsBusy Then bw_coverart.CancelAsync()
            If bw_import.IsBusy Then bw_import.CancelAsync()

            Me.Cursor = Cursors.Default

            Set_Settings()

        End If

        Return

    End Sub

    Private Sub DVDArt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If DVDArt_Common.Get_Paths(database, thumbs) Then

            'initialize common variables
            DVDArt_Common.Initialize()

            'show splashscreen
            Dim splash As New DVDArt_SplashScreen
            splash.Show()
            splash.Refresh()

            'initialize version
            Me.Text = Me.Text & DVDArt_Common._version

            'initialize timer
            t_import_timer.Interval = 2000
            t_import_timer.Start()

            'initialize importer state images
            il_state.Images.Add(My.Resources.download)
            il_state.Images.Add(My.Resources.tick)
            il_state.Images.Add(My.Resources.cross)
            il_state.Images.Add(My.Resources.na)

            'initialize column header images
            il_column.Images.Add(My.Resources.sort_none)
            il_column.Images.Add(My.Resources.sort_asc)
            il_column.Images.Add(My.Resources.sort_desc)

            'initialize labels
            l_imdb_id.Text = Nothing
            l_movie_size.Text = Nothing

            'disable tabs that are not selected in settings
            cb_DVDArt_CheckedChanged(Nothing, Nothing)
            cb_ClearArt_CheckedChanged(Nothing, Nothing)
            cb_ClearLogo_CheckedChanged(Nothing, Nothing)

            'populate language dropdown
            cb_language.Items.AddRange(DVDArt_Common.lang)

            'extract System.Data.SQLite.dll from resources to application library
            Dim dll As String = IO.Directory.GetCurrentDirectory() & "\System.Data.SQLite.dll"
            If Not FileSystem.FileExists(dll) Then FileSystem.WriteAllBytes(dll, My.Resources.System_Data_SQLite, False)

            'extract Interop.Shell32.dll from resources to application library
            dll = IO.Directory.GetCurrentDirectory() & "\Interop.Shell32.dll"
            If Not FileSystem.FileExists(dll) Then FileSystem.WriteAllBytes(dll, My.Resources.Interop_Shell32, False)

            'extract ICSharpCode.SharpZipLib.dll from resources to application library
            dll = IO.Directory.GetCurrentDirectory() & "\ICSharpCode.SharpZipLib.dll"
            If Not FileSystem.FileExists(dll) Then FileSystem.WriteAllBytes(dll, My.Resources.ICSharpCode_SharpZipLib, False)

            'extract dvdart.png from resources to temporary folder
            Dim png As String = DVDArt_Common._temp & "\dvdart.png"
            If Not FileSystem.FileExists(png) Then
                Dim image As Image = New Bitmap(My.Resources.dvdart)
                image.Save(png)
            End If

            'extract dvdart_mask.png from resources to temporary folder
            png = DVDArt_Common._temp & "\dvdart_mask.png"
            If Not FileSystem.FileExists(png) Then
                Dim image As Image = New Bitmap(My.Resources.dvdart_mask)
                image.Save(png)
            End If

            'extract convert.zip from resources to temporary folder
            Dim obj As String = DVDArt_Common._temp & "\convert.exe"
            If Not FileSystem.FileExists(obj) Then
                obj = DVDArt_Common._temp & "\convert.zip"
                FileSystem.WriteAllBytes(obj, My.Resources.convert, False)
                Dim unzip As New FastZip
                unzip.ExtractZip(obj, DVDArt_Common._temp, "")
            End If

            Create_Folder_Structure()
            Get_Settings()
            Load_Movie_List()
            Load_Serie_List()

            'set focus to first tab page
            tbc_main.SelectedIndex = 0

            'close splashscreen
            splash.Close()
            splash.Dispose()

        Else
            MsgBox("Unable to load Database & Thumbs paths from MediaPortalDirs.xml", MsgBoxStyle.Critical, "DVDArt Plugin")
            Return
        End If

    End Sub

End Class