Imports Microsoft.VisualBasic.FileIO
Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports MediaPortal.Configuration

Public Class DVDArt

    Private database, thumbs, current_imdb_id, _lastrun As String
    Private checked(2) As Boolean
    Private l_import_queue As New List(Of String)
    Private l_import_index As New List(Of Integer)
    Private lv_url_dvdart, lv_url_clearart, lv_url_clearlogo As New ListView
    Private li_movies, li_import, li_missing As New ListViewItem
    Private WithEvents t_import_timer As New Timer
    Private WithEvents bw_compress As New BackgroundWorker

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

    Friend Const LVM_FIRST As Integer = &H1000
    Friend Const LVM_SETITEMA As Integer = LVM_FIRST + 6
    Friend Const LVM_SETITEMW As Integer = LVM_FIRST + 76
    Friend Shared ReadOnly LVM_SETITEM As Integer = CInt(IIf(Marshal.SystemDefaultCharSize = 1, LVM_SETITEMA, LVM_SETITEMW))
    Friend Const LVM_SETEXTENDEDLISTVIEWSTYLE As Integer = LVM_FIRST + 54
    Friend Const LVIF_IMAGE As Integer = &H2
    Friend Const LVS_EX_SUBITEMIMAGES As Integer = &H2

    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As LVITEM) As Integer
    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

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

    Public Function FileInUse(ByVal sFile As String) As Boolean

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

    Public Function GetSize(ByVal path As String, ByVal image As String) As String

        Dim size As String = Nothing
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

        If columns.ContainsKey("Dimensions") Then size = folder.GetDetailsOf(folder.ParseName(image), columns("Dimensions")).Replace(" ", "")

        Return size

    End Function

    Private Sub import_timer_tick() Handles t_import_timer.Tick

        If l_import_queue.Any And Not bw_import.IsBusy Then
            FTV_api_connector("queue", Nothing, "import")
        End If

    End Sub

    Private Sub bw_compress_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_compress.DoWork

        For Each filePath As String In IO.Directory.GetFiles(thumbs & DVDArt_Common.folder(0, 0))

            If Not FileInUse(filePath) Then
                If GetSize(thumbs & DVDArt_Common.folder(0, 0), IO.Path.GetFileName(filePath)) <> "‪500x500‬" Then DVDArt_Common.CompressImage(filePath)
            End If

        Next

    End Sub

    Private Sub bw_import_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_import.DoWork

        Dim parm As String = e.Argument
        Dim x, y As Integer
        Dim addedmovies, addedmissing, filenotexist(2), downloaded(2) As Boolean
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

                For x = 0 To (lv_import.Items.Count - 1)

                    lv_import.Items(x).StateImageIndex = 0

                    addedmovies = False
                    addedmissing = False
                    lvi = Nothing

                    For y = 0 To 2
                        filenotexist(y) = checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png")
                    Next

                    downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, lv_import.Items.Item(x).SubItems.Item(1).Text, True, filenotexist)

                    For y = 0 To 2

                        If downloaded(y) Then
                            lv_import.Items(x).StateImageIndex = 1

                            addedmovies = addedmovies Or (lv_movies.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text).Text <> Nothing)

                            If Not addedmovies Then
                                li_movies = lv_movies.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                li_movies.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                addedmovies = True
                            End If
                        Else
                            If Not addedmovies Then
                                lv_import.Items(x).StateImageIndex = 2
                            End If

                            If Not addedmissing Then
                                li_missing = lv_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                addedmissing = True
                            End If
                        End If

                        If Not addedmissing Then
                            li_missing = lv_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
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
                        ListView_SetItem(lv_missing.Handle, lvi)

                    Next

                    If addedmissing Then
                        li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                    Else
                        li_missing.SubItems.RemoveAt(li_missing.Index)
                    End If

                    SQLcommand.CommandText = "INSERT INTO processed_movies (imdb_id) VALUES(""" & lv_import.Items.Item(x).SubItems.Item(1).Text & """)"
                    SQLcommand.ExecuteNonQuery()

                Next

                SQLconnect.Close()

            End If

        Else

            Dim endp As Integer
            Dim title, imdb_id As String

            For x = 0 To (l_import_queue.Count - 1)

                If l_import_queue.Count > 0 Then
                    endp = InStr(l_import_queue.Item(x), "|")
                    imdb_id = Microsoft.VisualBasic.Left(l_import_queue.Item(x), endp - 1)
                    title = Microsoft.VisualBasic.Right(l_import_queue.Item(x), Microsoft.VisualBasic.Len(l_import_queue.Item(x)) - endp)

                    lv_import.Items(l_import_index(x)).StateImageIndex = 0

                    addedmovies = False
                    addedmissing = False
                    lvi = Nothing

                    For y = 0 To 2
                        filenotexist(y) = checked(y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(y, 1) & imdb_id & ".png")
                    Next

                    downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, imdb_id, True, filenotexist)

                    For y = 0 To 2

                        If downloaded(y) Then
                            lv_import.Items(l_import_index(x)).StateImageIndex = 1

                            addedmovies = addedmovies Or (lv_movies.FindItemWithText(title).Text <> Nothing)

                            If Not addedmovies Then

                                li_movies = lv_movies.Items.Add(title)
                                li_movies.SubItems.Add(imdb_id)
                                addedmovies = True
                            End If
                        Else
                            If Not addedmovies Then
                                lv_import.Items(l_import_index(x)).StateImageIndex = 2
                            End If

                            If Not addedmissing Then
                                li_missing = lv_missing.Items.Add(title)
                                addedmissing = True
                            End If
                        End If

                If Not addedmissing Then
                    li_missing = lv_missing.Items.Add(title)
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
                ListView_SetItem(lv_missing.Handle, lvi)

            Next

                    If addedmissing Then
                        li_missing.SubItems.Add(imdb_id)
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
            FTV_api_connector(Nothing, Nothing, "import")
        End If

    End Sub

    Private Sub FTV_api_connector(ByVal imdb_id As String, ByVal url() As String, ByVal mode As String)

        On Error Resume Next

        Me.Cursor = Cursors.WaitCursor

        Dim x As Integer
        Dim details(5, 0) As String
        Dim WebClient As New System.Net.WebClient

        x = 0

        If mode = "preview" Then

            Dim jsonresponse As String

            jsonresponse = DVDArt_Common.JSON_request(imdb_id, "2")

            If jsonresponse <> "null" Then

                details = DVDArt_Common.parse(jsonresponse)

                Dim ImageInBytes() As Byte
                Dim stream As System.IO.MemoryStream

                For x = 0 To (details.Length / 6) - 1

                    If cb_DVDArt.Checked = True And details(0, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(0, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_dvdart.Images.Add(imagekey, Image.FromStream(stream))
                        lv_dvdart.Items.Add(details(1, x), imagekey)
                        lv_url_dvdart.Items.Add(details(0, x), imagekey)
                    End If

                    If cb_ClearArt.Checked = True And details(2, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(2, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_clearart.Images.Add(imagekey, Image.FromStream(stream))
                        lv_clearart.Items.Add(details(3, x), imagekey)
                        lv_url_clearart.Items.Add(details(2, x), imagekey)
                    End If

                    If cb_ClearLogo.Checked = True And details(4, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(4, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_clearlogo.Images.Add(imagekey, Image.FromStream(stream))
                        lv_clearlogo.Items.Add(details(5, x), imagekey)
                        lv_url_clearlogo.Items.Add(details(4, x), imagekey)
                    End If

                Next
            End If

        ElseIf mode = "selected" Then

            Dim parm As Object
            Dim fullpath, thumbpath As String

            imdb_id = current_imdb_id

            For x = 0 To 2

                If checked(x) And url(x) <> Nothing Then

                    fullpath = thumbs & DVDArt_Common.folder(x, 0) & imdb_id & ".png"
                    thumbpath = thumbs & DVDArt_Common.folder(x, 1) & imdb_id & ".png"

                    parm = thumbpath & "|" & url(x) & "/preview"

                    Do
                        If Not DVDArt_Common.bw_download0.IsBusy Then
                            DVDArt_Common.bw_download0.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download2.IsBusy Then
                            DVDArt_Common.bw_download2.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download4.IsBusy Then
                            DVDArt_Common.bw_download4.RunWorkerAsync(parm)
                            Exit Do
                        Else
                            wait(250)
                        End If
                    Loop

                    If x = 0 Then parm = fullpath & "|" & url(x) & "|shrink" Else parm = fullpath & "|" & url(x)

                    Do
                        If Not DVDArt_Common.bw_download1.IsBusy Then
                            DVDArt_Common.bw_download1.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download3.IsBusy Then
                            DVDArt_Common.bw_download3.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download5.IsBusy Then
                            DVDArt_Common.bw_download5.RunWorkerAsync(parm)
                            Exit Do
                        Else
                            wait(250)
                        End If
                    Loop

                End If

            Next

        ElseIf mode = "import" Then

            Dim parm As Object = imdb_id

            bw_import.RunWorkerAsync(parm)

        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Load_Movie_List()

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        If Not FileSystem.FileExists(database & "\dvdart.db3") Then

            SQLiteConnection.CreateFile(database & "\dvdart.db3")

            SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

            SQLconnect.Open()

            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = "CREATE TABLE processed_movies(imdb_id TEXT)"
            SQLcommand.ExecuteNonQuery()

            SQLconnect.Close()

        End If

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        ' Read already processed movies to identify newly imported ones in movingpictures

        Dim processed_movies(), imdb_id_in_mp() As String

        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies ORDER BY imdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        Dim x As Integer = 0

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

        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info ORDER BY sortby"

        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If processed_movies.Contains(SQLreader(0)) Then

                    For y = 0 To 2
                        fileexist(y) = FileSystem.FileExists(thumbs & DVDArt_Common.folder(y, 1) & SQLreader(0) & ".png")
                    Next

                    If fileexist(0) Or fileexist(1) Or fileexist(2) Then
                        li_movies = lv_movies.Items.Add(SQLreader(1))
                        li_movies.SubItems.Add(SQLreader(0))
                    End If

                    If Not fileexist(0) Or Not fileexist(1) Or Not fileexist(2) Then

                        li_missing = lv_missing.Items.Add(SQLreader(1))

                        lvi = Nothing

                        For y = 0 To 2

                            li_missing.SubItems.Add("")

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If fileexist(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_missing.Handle, lvi)

                        Next

                        li_missing.SubItems.Add(SQLreader(0))

                    End If

                Else
                    li_import = lv_import.Items.Add(SQLreader(1))
                    li_import.SubItems.Add(SQLreader(0))
                End If

                ReDim Preserve imdb_id_in_mp(x)
                imdb_id_in_mp(x) = SQLreader(0)
                x += 1

            End If

        End While

        If x = 0 Then ReDim Preserve imdb_id_in_mp(0)

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            FTV_api_connector(Nothing, Nothing, "import")
        End If

        ' remove imdb ids from dvdart that no longer exist in movingpictures

        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand

        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies ORDER BY imdb_id"

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

    Private Sub load_image(ByRef pb_image As PictureBox, ByVal path As String)

        On Error Resume Next

        If FileSystem.FileExists(path) Then

            Do Until Not FileInUse(path)
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

    Private Sub lv_movies_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movies.SelectedIndexChanged

        If current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        il_dvdart.Images.Clear()
        lv_dvdart.Items.Clear()
        lv_url_dvdart.Items.Clear()
        il_clearart.Images.Clear()
        lv_clearart.Items.Clear()
        lv_url_clearart.Items.Clear()
        il_clearlogo.Images.Clear()
        lv_clearlogo.Items.Clear()
        lv_url_clearlogo.Items.Clear()

        url = {pb_dvdart.Tag, pb_clearart.Tag, pb_clearlogo.Tag}

        If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Then
            FTV_api_connector(current_imdb_id, url, "selected")
        End If

        current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text
        l_imdb_id.Text = current_imdb_id

        For x = 0 To 2

            If checked(x) Then
                thumbpath = thumbs & DVDArt_Common.folder(x, 1) & current_imdb_id & ".png"
                If x = 0 Then
                    load_image(pb_dvdart, thumbpath)

                    If Not pb_dvdart.Image Is Nothing Then
                        l_size.Text = GetSize(thumbs & DVDArt_Common.folder(x, 0), current_imdb_id & ".png")
                        If l_size.Text = "‪500x500‬" Then b_compress.Visible = False Else b_compress.Visible = True
                    Else
                        l_size.Text = Nothing
                    End If

                ElseIf x = 1 Then
                    load_image(pb_clearart, thumbpath)
                    b_deleteart.Visible = Not (pb_clearart.Image Is Nothing)
                ElseIf x = 2 Then
                    load_image(pb_clearlogo, thumbpath)
                    b_deletelogo.Visible = Not (pb_clearlogo.Image Is Nothing)
                End If
            End If

        Next

    End Sub

    Private Sub cms_movies_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_movies.ItemClicked

        If e.ClickedItem.Text = "Refresh artwork from online" Then
            If lv_movies.SelectedItems.Count > 0 Then
                FTV_api_connector(lv_movies.SelectedItems(0).SubItems.Item(1).Text, Nothing, "preview")
            Else
                MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
            End If
        ElseIf e.ClickedItem.Text = "Compress all DVDArt to 500x500" Then
            bw_compress.WorkerSupportsCancellation = True
            bw_compress.RunWorkerAsync()
        End If

    End Sub

    Private Sub cms_missing_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_missing.ItemClicked

        If e.ClickedItem.Text = "Send to importer" Then
            If lv_missing.SelectedItems.Count > 0 Then
                For x As Integer = 0 To (lv_missing.SelectedItems.Count - 1)
                    li_import = lv_import.Items.Add(lv_missing.SelectedItems(x).SubItems.Item(0).Text)
                    li_import.SubItems.Add(lv_missing.SelectedItems(x).SubItems.Item(4).Text)
                    l_import_queue.Add(lv_missing.SelectedItems(x).SubItems.Item(4).Text & "|" & lv_missing.SelectedItems(x).SubItems.Item(0).Text)
                    l_import_index.Add(lv_import.Items.Count - 1)
                Next

                For x = (lv_missing.SelectedIndices.Count - 1) To 0 Step -1
                    lv_missing.Items.RemoveAt(lv_missing.SelectedIndices(x))
                Next

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "import")
                End If
            Else
                MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
            End If
        ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
            For x = 0 To (lv_missing.Items.Count - 1)
                If lv_missing.Items.Count > 0 Then
                    li_import = lv_import.Items.Add(lv_missing.Items.Item(x).SubItems(0).Text)
                    li_import.SubItems.Add(lv_missing.Items.Item(x).SubItems(4).Text)

                    l_import_queue.Add(lv_missing.Items.Item(x).SubItems(4).Text & "|" & lv_missing.Items.Item(x).SubItems(0).Text)
                    l_import_index.Add(lv_import.Items.Count - 1)

                    lv_missing.Items.RemoveAt(x)

                    x -= 1
                End If
            Next

            If Not bw_import.IsBusy Then
                FTV_api_connector("queue", Nothing, "import")
            End If
        End If

    End Sub

    Private Sub cms_import_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_import.ItemClicked

        Restart_Importer()

    End Sub

    Private Sub lv_dvdart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_dvdart.SelectedIndexChanged

        For Each item In lv_dvdart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_dvdart.Images(itemkey)

            pb_dvdart.Image = image
            pb_dvdart.Tag = lv_url_dvdart.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_clearart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_clearart.SelectedIndexChanged

        For Each item In lv_clearart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearart.Images(itemkey)

            pb_clearart.Image = image
            pb_clearart.Tag = lv_url_clearart.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_clearlogo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_clearlogo.SelectedIndexChanged

        For Each item In lv_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_clearlogo.Image = image
            pb_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

    End Sub

    Private Sub Create_Folder_Structure()

        ' Check and create directory structure

        If Not FileSystem.FileExists(database + "\movingpictures.db3") Then
            Application.Exit()
        End If

        ' DVDArt
        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\DVDArt") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt")

        ' ClearArt
        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\ClearArt") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\ClearArt")

        ' ClearLogo
        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\ClearLogo") Then FileSystem.CreateDirectory(thumbs & "\MovingPictures\ClearLogo")

        For x = 0 To 2
            For y = 0 To 1
                If Not FileSystem.DirectoryExists(thumbs & DVDArt_Common.folder(x, y)) Then
                    FileSystem.CreateDirectory(thumbs & DVDArt_Common.folder(x, y))
                End If
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
            cb_language.Text = DVDArt_Common.lang(Array.IndexOf(DVDArt_Common.langcode, XMLreader.GetValueAsString("Scraper", "language", "EN")))
            _lastrun = XMLreader.GetValueAsString("Settings", "lastrun", Nothing)

        End Using

    End Sub

    Private Sub DVDArt_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Err.Number = 0 Then

            Me.Cursor = Cursors.WaitCursor

            Dim url() As String = {pb_dvdart.Tag, pb_clearart.Tag, pb_clearlogo.Tag}

            t_import_timer.Stop()

            If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Then
                FTV_api_connector(current_imdb_id, url, "selected")
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

            Me.Cursor = Cursors.Default

            Set_Settings()

        End If

        Return

    End Sub

    Private Sub DVDArt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If DVDArt_Common.Get_Paths(database, thumbs) Then

            'show splashscreen
            Dim splash As New DVDArt_SplashScreen
            splash.Show()
            splash.Refresh()
            
            ' initialize timer
            t_import_timer.Interval = 2000
            t_import_timer.Start()

            ' initialize importer state images
            il_state.Images.Add(My.Resources.download)
            il_state.Images.Add(My.Resources.tick)
            il_state.Images.Add(My.Resources.cross)

            ' initialize common variables
            DVDArt_Common.Initialize()

            ' initialize labels
            l_imdb_id.Text = Nothing
            l_size.Text = Nothing

            ' disable tabs that are not selected in settings
            cb_DVDArt_CheckedChanged(Nothing, Nothing)
            cb_ClearArt_CheckedChanged(Nothing, Nothing)
            cb_ClearLogo_CheckedChanged(Nothing, Nothing)

            ' extract System.Data.SQLite.dll from resources to application library
            Dim dll As String = IO.Directory.GetCurrentDirectory() & "\System.Data.SQLite.dll"
            If Not FileSystem.FileExists(dll) Then FileSystem.WriteAllBytes(dll, My.Resources.System_Data_SQLite, False)

            ' extract Interop.Shell32.dll from resources to application library
            dll = IO.Directory.GetCurrentDirectory() & "\Interop.Shell32.dll"
            If Not FileSystem.FileExists(dll) Then FileSystem.WriteAllBytes(dll, My.Resources.Interop_Shell32, False)

            Create_Folder_Structure()
            Get_Settings()
            Load_Movie_List()

            'close splashscreen
            splash.Close()
            splash.Dispose()

        Else
            MsgBox("Unable to load Database & Thumbs paths from MediaPortalDirs.xml", MsgBoxStyle.Critical, "DVDArt Plugin")
            Return
        End If

    End Sub

    Private Sub cb_DVDArt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_DVDArt.CheckedChanged

        checked(0) = cb_DVDArt.Checked

        If cb_DVDArt.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_DVDArt) Then tbc_movies.TabPages.Remove(tp_DVDArt)
            tbc_movies.TabPages.Insert(0, tp_DVDArt)
        Else
            tbc_movies.TabPages.Remove(tp_DVDArt)
        End If

    End Sub

    Private Sub cb_ClearArt_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearArt.CheckedChanged

        checked(1) = cb_ClearArt.Checked

        If cb_ClearArt.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_ClearArt) Then tbc_movies.TabPages.Remove(tp_ClearArt)
            If tbc_movies.TabCount > 0 Then
                If tbc_movies.TabPages.Contains(tp_DVDArt) Then
                    tbc_movies.TabPages.Insert(1, tp_ClearArt)
                Else
                    tbc_movies.TabPages.Insert(tbc_movies.TabCount - 1, tp_ClearArt)
                End If
            Else
                tbc_movies.TabPages.Insert(tbc_movies.TabCount, tp_ClearArt)
            End If
        Else
            tbc_movies.TabPages.Remove(tp_ClearArt)
        End If

    End Sub

    Private Sub cb_ClearLogo_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearLogo.CheckedChanged

        checked(2) = cb_ClearLogo.Checked

        If cb_ClearLogo.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_ClearLogo) Then tbc_movies.TabPages.Remove(tp_ClearLogo)
            tbc_movies.TabPages.Insert(tbc_movies.TabCount, tp_ClearLogo)
        Else
            tbc_movies.TabPages.Remove(tp_ClearLogo)
        End If

    End Sub

    Private Sub b_compress_Click(sender As System.Object, e As System.EventArgs) Handles b_compress.Click

        DVDArt_Common.CompressImage(thumbs & DVDArt_Common.folder(0, 0) & current_imdb_id & ".png")

        l_size.Text = GetSize(thumbs & DVDArt_Common.folder(0, 0), current_imdb_id & ".png")

        If l_size.Text = "‪500x500‬" Then b_compress.Visible = False Else b_compress.Visible = True

    End Sub

    Private Sub b_deleteart_Click(sender As System.Object, e As System.EventArgs) Handles b_deleteart.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(1, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(1, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_clearart.Image = Nothing
            pb_clearart.Tag = Nothing
            b_deleteart.Visible = False
        End If
    End Sub

    Private Sub b_deletelogo_Click(sender As System.Object, e As System.EventArgs) Handles b_deletelogo.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_clearlogo.Image = Nothing
            pb_clearlogo.Tag = Nothing
            b_deletelogo.Visible = False
        End If
    End Sub

End Class