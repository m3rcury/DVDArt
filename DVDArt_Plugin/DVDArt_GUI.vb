Imports Microsoft.VisualBasic.FileIO

Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Threading

Imports MediaPortal.Configuration

'this is for mymovies.  you need to add movingpictures.dll in the resources
Imports MediaPortal.Plugins.MovingPictures.Database

'this is for myvideos.  you need to add database.dll in the resources
'Imports MediaPortal.Video.Database

'this is for myfilms.  you need to add myfilms.dll in the resources
'Imports MyFilmsPlugin.MyFilms

Public Class DVDArt_GUI

    Public Shared checked(2, 2) As Boolean
    Public Shared template_type As Integer

    Private WithEvents bw_compress, bw_coverart As New BackgroundWorker
    Private WithEvents t_import_timer As New System.Windows.Forms.Timer
    Private database, thumbs, current_imdb_id, current_thetvdb_id, current_artist, current_album, _lang, _lastrun As String
    Private l_import_queue As New List(Of String)
    Private l_import_index As New List(Of Integer)
    Private lvwColumnSorter = New ListViewColumnSorter()
    Private lv_url_dvdart, lv_url_clearart, lv_url_clearlogo As New ListView
    Private li_movies, li_series, li_artist, li_album, li_import, li_missing As New ListViewItem

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

    Public Sub SetSortArrow(ByRef lv As ListView, ByVal column As Integer, ByVal order As Integer)

        For col = 0 To (lv.Columns.Count - 1)

            lv.Columns.Item(col).Text = lv.Columns.Item(col).Text.Replace(" " & ChrW(&H25B2), Nothing)
            lv.Columns.Item(col).Text = lv.Columns.Item(col).Text.Replace(" " & ChrW(&H25BC), Nothing)

            If col = column Then

                If order = SortOrder.Ascending Then
                    lv.Columns.Item(column).Text &= " " & ChrW(&H25B2)
                ElseIf order = SortOrder.Descending Then
                    lv.Columns.Item(column).Text &= " " & ChrW(&H25BC)
                End If

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

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3;Read Only=True;"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand

        CheckForIllegalCrossThreadCalls = False

        For x = 0 To (lv_movies_missing.SelectedItems.Count - 1)

            imdb_id = lv_movies_missing.SelectedItems(x).SubItems.Item(4).Text

            If Not FileSystem.FileExists(DVDArt_Common.folder(0, 0, 1)) Then

                SQLcommand.CommandText = "SELECT alternatecovers, coverfullpath, title FROM movie_info WHERE imdb_id = """ & imdb_id & """"
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

                        DVDArt_Common.create_CoverArt(images(y), imdb_id, SQLreader(2), True, True, template_type)

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
                If DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(0, 0, 0), IO.Path.GetFileName(filePath)) <> "500x500" Then DVDArt_Common.Resize(filePath)
            End If

        Next

    End Sub

    Private Sub bw_import_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_import.DoWork

        Dim parm As String = e.Argument
        Dim x, y As Integer
        Dim added, addedmissing, filenotexist(2), downloaded(2) As Boolean
        Dim MBID As String = Nothing
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
                            filenotexist(y) = checked(0, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png")
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

                            If checked(0, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_movies_missing.Handle, lvi)

                            lv_import.Items(x).EnsureVisible()

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
                            filenotexist(y) = checked(1, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png")
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

                            If checked(1, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If


                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_series_missing.Handle, lvi)

                            lv_import.Items(x).EnsureVisible()

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                        SQLcommand.CommandText = "INSERT INTO processed_series (thetvdb_id) VALUES(""" & lv_import.Items.Item(x).SubItems.Item(1).Text & """)"
                        SQLcommand.ExecuteNonQuery()

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Artist" Then

                        MBID = DVDArt_Common.Get_Artist_MBID(lv_import.Items.Item(x).SubItems.Item(0).Text)

                        If MBID <> Nothing Then

                            lv_import.Items.Item(x).SubItems.Item(1).Text = MBID

                            For y = 0 To 2
                                filenotexist(y) = checked(0, y) And y <> 0 And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & lv_import.Items.Item(x).SubItems.Item(0).Text & ".png")
                            Next

                            downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, lv_import.Items.Item(x).SubItems.Item(1).Text, False, filenotexist, "artist")

                        Else
                            downloaded = {False, False, False}
                            lv_import.Items.Item(x).SubItems.Item(1).Text = Nothing
                        End If

                        For y = 0 To 2

                            If downloaded(y) Then

                                Dim file1 As String = thumbs & DVDArt_Common.folder(2, y, 0) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png"
                                Dim file2 As String = lv_import.Items.Item(x).SubItems.Item(0).Text & ".png"
                                Dim count As Integer = 20

                                Do While Not FileSystem.FileExists(file1)
                                    wait(250)
                                    count -= 1
                                Loop

                                FileSystem.RenameFile(file1, file2)

                                file1 = thumbs & DVDArt_Common.folder(2, y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png"
                                file2 = lv_import.Items.Item(x).SubItems.Item(0).Text & ".png"
                                count = 20

                                Do While Not FileSystem.FileExists(file1)
                                    wait(250)
                                    count -= 1
                                Loop

                                FileSystem.RenameFile(file1, file2)

                                lv_import.Items(x).StateImageIndex = 1

                                added = added Or (lv_album.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text).Text <> Nothing)

                                If Not added Then
                                    li_album = lv_album.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_album.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(x).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                addedmissing = True
                            End If

                            If checked(2, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y > 0 Then
                                If downloaded(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                            lv_import.Items(x).EnsureVisible()

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                        SQLcommand.CommandText = "INSERT INTO processed_artist (artist, MBID) VALUES('" & lv_import.Items.Item(x).SubItems.Item(0).Text.Replace("'", "''") & "','" & lv_import.Items.Item(x).SubItems.Item(1).Text & "')"
                        SQLcommand.ExecuteNonQuery()

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Music" Then

                        MBID = DVDArt_Common.Get_MBID(database, lv_import.Items.Item(x).SubItems.Item(0).Text, lv_import.Items.Item(x).SubItems.Item(3).Text)

                        If MBID <> Nothing Then

                            lv_import.Items.Item(x).SubItems.Item(1).Text = MBID

                            For y = 0 To 2
                                filenotexist(y) = checked(0, y) And y = 0 And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & lv_import.Items.Item(x).SubItems.Item(0).Text & ".png")
                            Next

                            downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, lv_import.Items.Item(x).SubItems.Item(1).Text, False, filenotexist, "music|" & lv_import.Items.Item(x).SubItems.Item(0).Text)
                        Else
                            downloaded = {False, False, False}
                            lv_import.Items.Item(x).SubItems.Item(1).Text = Nothing
                        End If

                        For y = 0 To 2

                            If downloaded(y) Then

                                Dim file1 As String = thumbs & DVDArt_Common.folder(2, y, 0) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png"
                                Dim file2 As String = lv_import.Items.Item(x).SubItems.Item(0).Text & ".png"
                                Dim count As Integer = 20

                                Do While Not FileSystem.FileExists(file1)
                                    wait(250)
                                    count -= 1
                                Loop

                                FileSystem.RenameFile(file1, file2)

                                file1 = thumbs & DVDArt_Common.folder(2, y, 1) & lv_import.Items.Item(x).SubItems.Item(1).Text & ".png"
                                file2 = lv_import.Items.Item(x).SubItems.Item(0).Text & ".png"
                                count = 20

                                Do While Not FileSystem.FileExists(file1)
                                    wait(250)
                                    count -= 1
                                Loop

                                FileSystem.RenameFile(file1, file2)

                                lv_import.Items(x).StateImageIndex = 1

                                added = added Or (lv_album.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text).Text <> Nothing)

                                If Not added Then
                                    li_album = lv_album.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_album.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(x).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                addedmissing = True
                            End If

                            If checked(2, y) And y = 0 Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y = 0 Then
                                If downloaded(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                            lv_import.Items(x).EnsureVisible()

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                        SQLcommand.CommandText = "INSERT INTO processed_music (album, MBID) VALUES('" & lv_import.Items.Item(x).SubItems.Item(0).Text.Replace("'", "''") & "','" & lv_import.Items.Item(x).SubItems.Item(1).Text & "')"
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
                            filenotexist(y) = checked(0, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & id & ".png")
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

                            If checked(0, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_movies_missing.Handle, lvi)

                            lv_import.Items(l_import_index(x)).EnsureVisible()

                        Next
                    ElseIf type = "series" Then
                        For y = 1 To 2
                            filenotexist(y) = checked(1, y) And Not FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & id & ".png")
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

                            If checked(1, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If downloaded(y) Then
                                lvi.iImage = 1
                            Else
                                lvi.iImage = 2
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_series_missing.Handle, lvi)

                            lv_import.Items(l_import_index(x)).EnsureVisible()

                        Next
                    ElseIf type = "artist" Then

                        If id = "" Then
                            lv_import.Items(l_import_index(x)).SubItems.Item(1).Text = DVDArt_Common.Get_Artist_MBID(title)
                        End If

                        For y = 0 To 2
                            Dim fileexist = IO.File.Exists(thumbs & DVDArt_Common.folder(2, y, 1) & title & ".png")
                            filenotexist(y) = checked(0, y) And y <> 0 And Not fileexist
                        Next

                        downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, id, False, filenotexist, type)

                        For y = 0 To 2

                            If downloaded(y) Then

                                If y <> 0 And filenotexist(y) Then
                                    Dim file1 As String = thumbs & DVDArt_Common.folder(2, y, 0) & id & ".png"
                                    Dim file2 As String = title & ".png"
                                    Dim count As Integer = 20

                                    Do While Not FileSystem.FileExists(file1) And count > 0
                                        wait(250)
                                        count -= 1
                                    Loop

                                    FileSystem.RenameFile(file1, file2)

                                    file1 = thumbs & DVDArt_Common.folder(2, y, 1) & id & ".png"
                                    file2 = title & ".png"
                                    count = 20

                                    Do While Not FileSystem.FileExists(file1)
                                        wait(250)
                                        count -= 1
                                    Loop

                                    FileSystem.RenameFile(file1, file2)
                                End If

                                lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                added = added Or (lv_artist.FindItemWithText(title, True, 0, False).Text <> Nothing)

                                If Not added Then
                                    li_artist = lv_artist.Items.Add(title)
                                    li_artist.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(l_import_index(x)).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_music_missing.Items.Add(title)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_music_missing.Items.Add(title)
                                addedmissing = True
                            End If

                            If checked(2, y) And y <> 0 Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y <> 0 Then
                                If downloaded(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                            lv_import.Items(l_import_index(x)).EnsureVisible()

                        Next
                    ElseIf Microsoft.VisualBasic.Left(type, 5) = "music" Then

                        Dim artist = LCase(Microsoft.VisualBasic.Right(type, Len(type) - 6).Replace(" ", "-"))
                        type = Microsoft.VisualBasic.Left(type, 5)

                        If id = "" Then
                            lv_import.Items(l_import_index(x)).SubItems.Item(1).Text = DVDArt_Common.Get_MBID(database, title, artist)
                        End If

                        For y = 0 To 2
                            filenotexist(y) = checked(0, y) And y = 0 And Not IO.File.Exists(thumbs & DVDArt_Common.folder(2, y, 1) & title & ".png")
                        Next

                        downloaded = DVDArt_Common.download(thumbs, DVDArt_Common.folder, id, False, filenotexist, type & "|" & title)

                        For y = 0 To 2

                            If downloaded(y) Then

                                If y = 0 And filenotexist(y) Then
                                    Dim file1 As String = thumbs & DVDArt_Common.folder(2, y, 0) & id & ".png"
                                    Dim file2 As String = title & ".png"
                                    Dim count As Integer = 20

                                    Do While Not FileSystem.FileExists(file1)
                                        wait(250)
                                        count -= 1
                                    Loop

                                    FileSystem.RenameFile(file1, file2)

                                    file1 = thumbs & DVDArt_Common.folder(2, y, 1) & id & ".png"
                                    file2 = title & ".png"
                                    count = 20

                                    Do While Not FileSystem.FileExists(file1)
                                        wait(250)
                                        count -= 1
                                    Loop

                                    FileSystem.RenameFile(file1, file2)
                                End If

                                lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                added = added Or (lv_album.FindItemWithText(title, False, 0, False).Text <> Nothing)

                                If Not added Then
                                    li_album = lv_album.Items.Add(title)
                                    li_album.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then
                                    lv_import.Items(l_import_index(x)).StateImageIndex = 2
                                End If

                                If Not addedmissing Then
                                    li_missing = lv_music_missing.Items.Add(title)
                                    addedmissing = True
                                End If
                            End If

                            If Not addedmissing Then
                                li_missing = lv_music_missing.Items.Add(title)
                                addedmissing = True
                            End If

                            If checked(2, y) And y = 0 Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y = 0 Then
                                If downloaded(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                            lv_import.Items(l_import_index(x)).EnsureVisible()

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

        e.Result = "Import Complete"

    End Sub

    Private Sub use_coverart(mode As String)

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim imdb_id, movie_name, images() As String
        Dim x, y, z, count As Integer

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3;Read Only=True;"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand

        If mode = "movies" Then
            count = lv_movies.SelectedItems.Count - 1
        Else
            count = lv_movies_missing.SelectedItems.Count - 1
        End If

        For x = 0 To count

            If mode = "movies" Then
                movie_name = lv_movies.SelectedItems(x).SubItems.Item(0).Text
                imdb_id = lv_movies.SelectedItems(x).SubItems.Item(1).Text
            Else
                movie_name = lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text
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
                    Dim coverart As New DVDArt_CoverArt(images, thumbs, imdb_id, movie_name)
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

    Private Sub changeMBID(ByVal old_MBID As String, ByVal new_MBID As String)

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "UPDATE processed_artist SET MBID = '" & new_MBID & "' WHERE MBID = '" & old_MBID & "'"
        SQLcommand.ExecuteNonQuery()
        SQLcommand.CommandText = "UPDATE processed_music SET MBID = '" & new_MBID & "' WHERE MBID = '" & old_MBID & "'"
        SQLcommand.ExecuteNonQuery()
        SQLconnect.Close()

        Dim x As Integer

        For x = 0 To lv_artist.Items.Count
            If lv_artist.Items(x).SubItems.Item(1).Text = old_MBID Then lv_artist.Items(x).SubItems.Item(1).Text = new_MBID
        Next

        For x = 0 To lv_album.Items.Count
            If lv_album.Items(x).SubItems.Item(1).Text = old_MBID Then lv_album.Items(x).SubItems.Item(1).Text = new_MBID
        Next

        For x = 0 To lv_music_missing.Items.Count
            If lv_music_missing.Items(x).SubItems.Item(4).Text = old_MBID Then lv_music_missing.Items(x).SubItems.Item(4).Text = new_MBID
        Next

    End Sub

    Private Sub Restart_Importer()

        Me.Cursor = Cursors.WaitCursor

        lv_music_missing.Items.Clear()

        LoadMovieList()
        LoadSerieList()
        LoadArtistList()
        LoadMusicList()

        Me.Cursor = Cursors.Default

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

            If Microsoft.VisualBasic.Left(type, 5) <> "music" Then
                jsonresponse = DVDArt_Common.JSON_request(id, type, "2")
            Else
                jsonresponse = DVDArt_Common.JSON_request(id, "artist", "2")
            End If

            If jsonresponse <> "null" Then

                If Microsoft.VisualBasic.Left(type, 5) <> "music" Then
                    details = DVDArt_Common.parse(jsonresponse, type)
                Else
                    details = DVDArt_Common.parse_music(jsonresponse, LCase(Microsoft.VisualBasic.Right(type, Len(type) - 6).Replace(" ", "-")))
                End If

                Dim ImageInBytes() As Byte
                Dim stream As System.IO.MemoryStream

                For x = 0 To (details.Length / 6) - 1

                    If ((cb_DVDArt_movies.Checked And type = "movie") Or (cb_CDArt_music.Checked And Microsoft.VisualBasic.Left(type, 5) = "music")) And details(0, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(0, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_dvdart.Images.Add(imagekey, Image.FromStream(stream))
                        If type = "movie" Then
                            lv_movie_dvdart.Items.Add(details(1, x), imagekey)
                        ElseIf Microsoft.VisualBasic.Left(type, 5) = "music" Then
                            lv_album_cdart.Items.Add(details(1, x), imagekey)
                        End If
                        lv_url_dvdart.Items.Add(details(0, x), imagekey)
                    End If

                    If ((cb_ClearArt_movies.Checked And type = "movie") Or (cb_ClearArt_series.Checked And type = "series") Or (cb_Banner_artist.Checked And type = "artist")) And details(2, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(2, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        If type = "movie" Then
                            il_clearart.Images.Add(imagekey, Image.FromStream(stream))
                            lv_movie_clearart.Items.Add(details(3, x), imagekey)
                        ElseIf type = "series" Then
                            il_clearart.Images.Add(imagekey, Image.FromStream(stream))
                            lv_serie_clearart.Items.Add(details(3, x), imagekey)
                        ElseIf type = "artist" Then
                            il_banner.Images.Add(imagekey, Image.FromStream(stream))
                            lv_artist_banner.Items.Add(details(3, x), imagekey)
                        End If
                        lv_url_clearart.Items.Add(details(2, x), imagekey)
                    End If

                    If ((cb_ClearLogo_movies.Checked And type = "movie") Or (cb_ClearLogo_series.Checked And type = "series") Or (cb_ClearLogo_artist.Checked And type = "artist")) And details(4, x) <> Nothing Then
                        Dim imagekey As String = Guid.NewGuid().ToString()
                        ImageInBytes = WebClient.DownloadData(details(4, x) & "/preview")
                        stream = New System.IO.MemoryStream(ImageInBytes)
                        il_clearlogo.Images.Add(imagekey, Image.FromStream(stream))
                        If type = "movie" Then
                            lv_movie_clearlogo.Items.Add(details(5, x), imagekey)
                        ElseIf type = "series" Then
                            lv_serie_clearlogo.Items.Add(details(5, x), imagekey)
                        ElseIf type = "artist" Then
                            lv_artist_clearlogo.Items.Add(details(5, x), imagekey)
                        End If
                        lv_url_clearlogo.Items.Add(details(4, x), imagekey)
                    End If

                Next
            End If

        ElseIf mode = "selected" Then

            Dim parm As Object
            Dim fullpath As String = Nothing
            Dim thumbpath As String = Nothing

            For x = 0 To 2

                If (checked(0, x) Or checked(1, x) Or checked(2, x)) And url(x) <> Nothing Then

                    If type = "movie" Then
                        fullpath = thumbs & DVDArt_Common.folder(0, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & id & ".png"
                    ElseIf type = "series" Then
                        If x = 0 Then Continue For
                        fullpath = thumbs & DVDArt_Common.folder(1, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(1, x, 1) & id & ".png"
                    ElseIf type = "artist" Or type = "album" Then
                        If x = 0 Then Continue For
                        fullpath = thumbs & DVDArt_Common.folder(2, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(2, x, 1) & id & ".png"
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

    Private Sub LoadMovieList()

        If Not FileSystem.FileExists(database & "\movingpictures.db3") Then Exit Sub

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_movies() As String = Nothing
        Dim imdb_id_in_mp() As String = Nothing
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

        'this part is for default Videos database
        'Dim myvideos As New ArrayList
        'VideoDatabase.GetMovies(myvideos)

        'this part is for myfilms
        'Dim myfilms As New ArrayList
        'BaseMesFilms.GetMovies(myfilms)

        'this is for MovingPictures
        'Dim allMovies As New List(Of DBMovieInfo)
        'allMovies = DBMovieInfo.GetAll

        SQLconnect.ConnectionString = "Data Source=" & database & "\movingpictures.db3;Read Only=True;"

        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT imdb_id, title FROM movie_info WHERE imdb_id is not Null and title is not Null ORDER BY sortby"
        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_movies_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        lv_movies.Items.Clear()
        lv_movies_missing.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If processed_movies.Contains(SQLreader(0)) Then

                    found = False
                    missing = False

                    For y = 0 To 2
                        fileexist(y) = FileSystem.FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & SQLreader(0) & ".png")
                        If Not found Then found = checked(0, y) And fileexist(y)
                        If Not missing Then missing = checked(0, y) And Not fileexist(y)
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

                            If checked(0, y) Then
                                If fileexist(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If


                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(0, y) Then
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

    Private Sub LoadSerieList()

        If Not FileSystem.FileExists(database & "\TVSeriesDatabase4.db3") Then Exit Sub

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_series() As String = Nothing
        Dim thetvdb_id_in_tv() As String = Nothing
        Dim x As Integer = 0

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        If Not FileSystem.FileExists(database & "\dvdart.db3") Then SQLiteConnection.CreateFile(database & "\dvdart.db3")

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_series(thetvdb_id TEXT)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed series to identify newly imported ones in TVSeries

        SQLcommand.CommandText = "SELECT thetvdb_id FROM processed_series WHERE thetvdb_id is not Null ORDER BY thetvdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_series(x)
            processed_series(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_series(0)

        SQLconnect.ConnectionString = "Data Source=" & database & "\TVSeriesDatabase4.db3;Read Only=True;"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT id, pretty_name FROM online_series WHERE id is not Null and pretty_name is not Null ORDER BY sortname"
        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_series_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        lv_series.Items.Clear()
        lv_series_missing.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If processed_series.Contains(SQLreader(0)) Then

                    For y = 1 To 2
                        fileexist(y) = checked(1, y) And FileSystem.FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & SQLreader(0) & ".png")
                    Next

                    If fileexist(1) Or fileexist(2) Then
                        li_series = lv_series.Items.Add(SQLreader(1))
                        li_series.SubItems.Add(SQLreader(0))
                    End If

                    If Not fileexist(1) Or Not fileexist(2) Then

                        li_missing = lv_series_missing.Items.Add(SQLreader(1))

                        lvi = Nothing

                        For y = 1 To 2

                            If checked(1, y) Then
                                If fileexist(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(1, y) Then
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

    Private Sub LoadArtistList()

        If Not FileSystem.FileExists(database & "\MusicDatabaseV12.db3") Then Exit Sub

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_artist() As String = Nothing
        Dim processed_MBID() As String = Nothing
        Dim artist() As String = Nothing
        Dim x As Integer = 0
        Dim found, missing As Boolean

        If Not FileSystem.FileExists(database & "\dvdart.db3") Then SQLiteConnection.CreateFile(database & "\dvdart.db3")

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_artist(artist TEXT, MBID TEXT)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed movies to identify newly imported ones in music

        SQLcommand.CommandText = "SELECT artist, MBID FROM processed_artist WHERE artist is not Null ORDER BY artist"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_artist(x)
            ReDim Preserve processed_MBID(x)
            processed_artist(x) = SQLreader(0)
            processed_MBID(x) = SQLreader(1)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_artist(0)

        SQLconnect.ConnectionString = "Data Source=" & database & "\MusicDatabaseV12.db3;Read Only=True;"

        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT strArtist FROM artist WHERE strArtist IS NOT NULL AND strArtist <> '' ORDER BY strArtist"
        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_music_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        lv_artist.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                ReDim Preserve artist(x)
                artist(x) = SQLreader(0)
                x += 1

                If processed_artist.Contains(artist(x - 1)) Then

                    found = False
                    missing = False

                    For y = 1 To 2
                        fileexist(y) = FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & artist(x - 1) & ".png") Or FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & artist(x - 1) & ".jpg")
                        If Not found Then found = checked(2, y) And fileexist(y)
                        If Not missing Then missing = checked(2, y) And Not fileexist(y)
                    Next

                    If found Then
                        li_artist = lv_artist.Items.Add(SQLreader(0))
                        li_artist.SubItems.Add(processed_MBID(Array.IndexOf(processed_artist, SQLreader(0))))
                    End If

                    If missing Then

                        li_missing = lv_music_missing.Items.Add(SQLreader(0))

                        lvi = Nothing

                        li_missing.ForeColor = Color.White

                        For y = 0 To 2

                            If checked(2, y) And y > 0 Then
                                If fileexist(y) Then
                                    li_missing.SubItems.Add("Yes")
                                Else
                                    li_missing.SubItems.Add("No")
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(2, y) And y > 0 Then
                                If fileexist(y) Then
                                    lvi.iImage = 1
                                Else
                                    lvi.iImage = 2
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                        Next

                        li_missing.ForeColor = Color.Black

                        li_missing.SubItems.Add(processed_MBID(Array.IndexOf(processed_artist, SQLreader(0))))
                        li_missing.SubItems.Add("")

                    End If

                Else
                    li_import = lv_import.Items.Add(SQLreader(0))
                    li_import.SubItems.Add("*** searching MBID ***")
                    li_import.SubItems.Add("Artist")
                End If

            End If

        End While

        If x = 0 Then ReDim Preserve artist(0)

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            FTV_api_connector(Nothing, Nothing, "artist", "import")
        End If

        ' remove artists from dvdart that no longer exist in music

        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT artist FROM processed_artist WHERE artist is not Null ORDER BY artist"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not artist.Contains(SQLreader(0)) Then

                SQLdelete.CommandText = "DELETE FROM processed_artist WHERE artist = """ & SQLreader(0) & """"
                SQLdelete.ExecuteNonQuery()

            End If

        End While

        SQLconnect.Close()

    End Sub

    Private Sub LoadMusicList()

        If Not FileSystem.FileExists(database & "\MusicDatabaseV12.db3") Then Exit Sub

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_music() As String = Nothing
        Dim processed_MBID() As String = Nothing
        Dim album() As String = Nothing
        Dim x As Integer = 0
        Dim y As Integer = 0
        Dim found, missing As Boolean

        If Not FileSystem.FileExists(database & "\dvdart.db3") Then SQLiteConnection.CreateFile(database & "\dvdart.db3")

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_music(album TEXT, MBID TEXT)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed movies to identify newly imported ones in music

        SQLcommand.CommandText = "SELECT album, MBID FROM processed_music WHERE album is not Null ORDER BY album"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_music(x)
            ReDim Preserve processed_MBID(x)
            processed_music(x) = LCase(SQLreader(0))
            processed_MBID(x) = SQLreader(1)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_music(0)

        SQLconnect.ConnectionString = "Data Source=" & database & "\MusicDatabaseV12.db3;Read Only=True;"

        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT DISTINCT strAlbum, strAlbumArtist FROM tracks WHERE strAlbum IS NOT NULL AND strAlbum <> '' ORDER BY strAlbum"
        SQLreader = SQLcommand.ExecuteReader()

        Dim fileexist(2), add As Boolean
        Dim lvi As LVITEM

        x = -1

        SendMessage(lv_music_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        lv_album.Items.Clear()

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If x = -1 Then
                    add = True
                Else
                    add = Not album.Contains(LCase(SQLreader(0)))
                End If

                If add Then

                    x += 1
                    ReDim Preserve album(x)
                    album(x) = LCase(SQLreader(0))

                    If processed_music.Contains(album(x)) Then

                        found = False
                        missing = False

                        For y = 0 To 0
                            fileexist(y) = FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & album(x) & ".png") Or FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & album(x) & ".jpg")
                            If Not found Then found = checked(2, y) And fileexist(y)
                            If Not missing Then missing = checked(2, y) And Not fileexist(y)
                        Next

                        If found Then
                            li_album = lv_album.Items.Add(SQLreader(0))
                            li_album.SubItems.Add(processed_MBID(Array.IndexOf(processed_music, LCase(SQLreader(0)))))
                        End If

                        If missing Then

                            li_missing = lv_music_missing.Items.Add(SQLreader(0))

                            lvi = Nothing

                            li_missing.ForeColor = Color.White

                            For y = 0 To 2

                                If checked(2, y) And y = 0 Then
                                    If fileexist(y) Then
                                        li_missing.SubItems.Add("Yes")
                                    Else
                                        li_missing.SubItems.Add("No")
                                    End If
                                Else
                                    li_missing.SubItems.Add("")
                                End If

                                lvi.iItem = li_missing.Index
                                lvi.subItem = li_missing.SubItems.Count - 1

                                If checked(2, y) And y = 0 Then
                                    If fileexist(y) Then
                                        lvi.iImage = 1
                                    Else
                                        lvi.iImage = 2
                                    End If
                                Else
                                    lvi.iImage = 3
                                End If

                                lvi.mask = LVIF_IMAGE
                                ListView_SetItem(lv_music_missing.Handle, lvi)

                            Next

                            li_missing.ForeColor = Color.Black

                            y = Array.IndexOf(processed_music, LCase(SQLreader(0)))

                            If y > -1 Then
                                li_missing.SubItems.Add(processed_MBID(y))
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            li_missing.SubItems.Add(Trim(SQLreader(1).Replace("| ", "").Replace(" |", "")))

                        End If

                    Else
                        li_import = lv_import.Items.Add(SQLreader(0))
                        li_import.SubItems.Add("*** searching MBID ***")
                        li_import.SubItems.Add("Music")
                        li_import.SubItems.Add(Trim(SQLreader(1).Replace("| ", "").Replace(" |", "")))
                    End If

                End If

            End If

        End While

        If x = -1 Then ReDim Preserve album(0)

        SQLconnect.Close()

        If lv_import.Items.Count > 0 Then
            FTV_api_connector(Nothing, Nothing, "music", "import")
        End If

        ' remove albums and artists from dvdart that no longer exist in music

        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT album FROM processed_music WHERE album is not Null ORDER BY album"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not album.Contains(LCase(SQLreader(0))) Then

                SQLdelete.CommandText = "DELETE FROM processed_music WHERE album = """ & SQLreader(0) & """"
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

    Private Sub clearLists()

        il_banner.Images.Clear()
        il_clearart.Images.Clear()
        il_clearlogo.Images.Clear()
        il_dvdart.Images.Clear()

        lv_album_cdart.Items.Clear()
        lv_artist_banner.Items.Clear()
        lv_artist_clearlogo.Items.Clear()
        lv_movie_clearart.Items.Clear()
        lv_movie_clearlogo.Items.Clear()
        lv_movie_dvdart.Items.Clear()
        lv_serie_clearart.Items.Clear()
        lv_serie_clearlogo.Items.Clear()

        lv_url_clearart.Items.Clear()
        lv_url_clearlogo.Items.Clear()
        lv_url_dvdart.Items.Clear()

    End Sub

    Private Sub lv_movies_GotFocus(sender As Object, e As System.EventArgs) Handles lv_movies.GotFocus, lv_movies_missing.GotFocus
        cms_found.Items.Item(2).Visible = True
        cms_found.Items.Item(3).Visible = False
        cms_missing.Items.Item(3).Visible = True
        cms_missing.Items.Item(4).Visible = True
        cms_missing.Items.Item(5).Visible = False
    End Sub

    Private Sub lv_movies_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movies.SelectedIndexChanged

        If current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        clearLists()

        url = {pb_movie_dvdart.Tag, pb_movie_clearart.Tag, pb_movie_clearlogo.Tag}

        If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Then
            FTV_api_connector(current_imdb_id, url, "movie", "selected")
        End If

        current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text
        l_imdb_id.Text = current_imdb_id

        For x = 0 To 2

            If checked(0, x) Then
                thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & current_imdb_id & ".png"
                If x = 0 Then
                    load_image(pb_movie_dvdart, thumbpath)

                    If pb_movie_dvdart.Image IsNot Nothing Then
                        l_movie_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(0, x, 0), current_imdb_id & ".png")
                        If l_movie_size.Text = "500x500" Then b_movie_compress.Visible = False Else b_movie_compress.Visible = True
                        b_movie_preview.Visible = Not b_movie_compress.Visible
                        b_movie_delete.Visible = True
                    Else
                        l_movie_size.Text = Nothing
                        b_movie_compress.Visible = False
                        b_movie_preview.Visible = False
                        b_movie_delete.Visible = False
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
        cms_found.Items.Item(3).Visible = False
        cms_missing.Items.Item(3).Visible = False
        cms_missing.Items.Item(4).Visible = False
        cms_missing.Items.Item(5).Visible = False
    End Sub

    Private Sub lv_series_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_series.SelectedIndexChanged

        If current_thetvdb_id = lv_series.FocusedItem.SubItems.Item(1).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        clearLists()

        url = {Nothing, pb_serie_clearart.Tag, pb_serie_clearlogo.Tag}

        If url(1) <> Nothing Or url(2) <> Nothing Then
            FTV_api_connector(current_thetvdb_id, url, "series", "selected")
        End If

        current_thetvdb_id = lv_series.FocusedItem.SubItems.Item(1).Text
        l_thetvdb_id.Text = current_thetvdb_id

        For x = 1 To 2

            If checked(1, x) Then
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

    Private Sub lv_artist_GotFocus(sender As Object, e As System.EventArgs) Handles lv_artist.GotFocus, lv_music_missing.GotFocus, lv_album.GotFocus
        cms_found.Items.Item(2).Visible = False
        cms_found.Items.Item(3).Visible = True
        cms_missing.Items.Item(3).Visible = False
        cms_missing.Items.Item(4).Visible = False
        cms_missing.Items.Item(5).Visible = True
    End Sub

    Private Sub lv_artist_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_artist.SelectedIndexChanged

        If current_artist = lv_artist.FocusedItem.SubItems.Item(0).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        clearLists()

        url = {Nothing, pb_artist_banner.Tag, pb_artist_clearlogo.Tag}

        If url(1) <> Nothing Or url(2) <> Nothing Then
            FTV_api_connector(current_artist, url, "artist", "selected")
        End If

        current_artist = lv_artist.FocusedItem.SubItems.Item(0).Text

        For x = 1 To 2

            If checked(2, x) Then
                thumbpath = thumbs & DVDArt_Common.folder(2, x, 1) & current_artist & ".png"
                If x = 1 Then
                    load_image(pb_artist_banner, thumbpath)
                    b_artist_deletebanner.Visible = (pb_artist_banner.Image IsNot Nothing)
                ElseIf x = 2 Then
                    load_image(pb_artist_clearlogo, thumbpath)
                    b_artist_deletelogo.Visible = (pb_artist_clearlogo.Image IsNot Nothing)
                End If
            End If

        Next

    End Sub

    Private Sub lv_music_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_album.SelectedIndexChanged

        If current_album = lv_album.FocusedItem.SubItems.Item(0).Text Then Exit Sub

        Dim thumbpath, url(2) As String

        clearLists()

        url = {pb_album_cdart.Tag, Nothing, Nothing}

        If url(0) <> Nothing Then
            FTV_api_connector(current_album, url, "album", "selected")
        End If

        current_album = lv_album.FocusedItem.SubItems.Item(0).Text

        If checked(2, 0) Then
            thumbpath = thumbs & DVDArt_Common.folder(2, 0, 1) & current_album & ".png"
            load_image(pb_album_cdart, thumbpath)

            If pb_album_cdart.Image IsNot Nothing Then
                l_music_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(2, 0, 0), current_album & ".png")
                If l_music_size.Text = "500x500" Then b_album_compress.Visible = False Else b_album_compress.Visible = True
                b_album_preview.Visible = Not b_album_compress.Visible
                b_album_delete.Visible = True
            Else
                l_music_size.Text = Nothing
                b_album_compress.Visible = False
                b_album_preview.Visible = False
                b_album_delete.Visible = False
            End If

        End If

    End Sub

    Private Sub lv_movies_missing_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles lv_movies_missing.GotFocus
        cms_missing.Items.Item(3).Visible = False
        cms_missing.Items.Item(4).Visible = False
        cms_missing.Items.Item(5).Visible = True
    End Sub

    Private Sub lv_movies_missing_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lv_movies_missing.ColumnClick, lv_series_missing.ColumnClick, lv_music_missing.ColumnClick

        Select Case sender.name
            Case "lv_movies_missing"
                lv_movies_missing.ListViewItemSorter = lvwColumnSorter
            Case "lv_series_missing"
                lv_series_missing.ListViewItemSorter = lvwColumnSorter
            Case "lv_music_missing"
                lv_music_missing.ListViewItemSorter = lvwColumnSorter
        End Select

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

        ' Perform the sort with these new sort options.
        Select Case sender.name
            Case "lv_movies_missing"
                SetSortArrow(lv_movies_missing, e.Column, lvwColumnSorter.order)
                lv_movies_missing.Sort()
            Case "lv_series_missing"
                SetSortArrow(lv_series_missing, e.Column, lvwColumnSorter.order)
                lv_series_missing.Sort()
            Case "lv_music_missing"
                SetSortArrow(lv_music_missing, e.Column, lvwColumnSorter.order)
                lv_music_missing.Sort()
        End Select

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
                    current_imdb_id = Nothing
                    lv_movies_SelectedIndexChanged(sender, e)
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
            End If

        ElseIf cms_found.SourceControl.Name = "lv_artist" Then

            If e.ClickedItem.Text = "Refresh artwork from online" Then
                If lv_artist.SelectedItems.Count > 0 Then
                    FTV_api_connector(lv_artist.SelectedItems(0).SubItems.Item(1).Text, Nothing, "artist", "preview")
                Else
                    MsgBox("Please select an artist.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                If lv_artist.SelectedItems.Count > 0 Then
                    Dim title As String = lv_artist.SelectedItems(0).SubItems(0).Text
                    Dim upload As New DVDArt_ManualUpload(title, title, "artist")
                    upload.ShowDialog()
                    current_artist = Nothing
                    lv_artist_SelectedIndexChanged(sender, e)
                Else
                    MsgBox("Please select an artist.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Change MBID" Then
                If lv_artist.SelectedItems.Count > 0 Then
                    Dim MBID As String = lv_artist.SelectedItems(0).SubItems.Item(1).Text
                    Dim change As New DVDArt_ChangeMBID
                    change.ChangeMBID(MBID, lv_artist.SelectedItems(0).SubItems.Item(1).Text)
                    If MBID <> lv_artist.SelectedItems(0).SubItems.Item(1).Text Then
                        changeMBID(MBID, lv_artist.SelectedItems(0).SubItems.Item(1).Text)
                    End If
                Else
                    MsgBox("Please select an artist.", MsgBoxStyle.Critical, Nothing)
                End If
            End If

        ElseIf cms_found.SourceControl.Name = "lv_album" Then

            If e.ClickedItem.Text = "Refresh artwork from online" Then
                If lv_album.SelectedItems.Count > 0 Then
                    FTV_api_connector(lv_album.SelectedItems(0).SubItems.Item(1).Text, Nothing, "music|" & lv_album.SelectedItems(0).SubItems.Item(0).Text, "preview")
                Else
                    MsgBox("Please select an album.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                If lv_album.SelectedItems.Count > 0 Then
                    Dim title As String = lv_album.SelectedItems(0).SubItems(0).Text
                    Dim upload As New DVDArt_ManualUpload(title, title, "music")
                    upload.ShowDialog()
                    current_album = Nothing
                    lv_music_SelectedIndexChanged(sender, e)
                Else
                    MsgBox("Please select an album.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Change MBID" Then
                If lv_album.SelectedItems.Count > 0 Then
                    Dim MBID As String = lv_album.SelectedItems(0).SubItems.Item(1).Text
                    Dim change As New DVDArt_ChangeMBID
                    change.ChangeMBID(MBID, lv_album.SelectedItems(0).SubItems.Item(1).Text)
                    If MBID <> lv_album.SelectedItems(0).SubItems.Item(1).Text Then
                        changeMBID(MBID, lv_album.SelectedItems(0).SubItems.Item(1).Text)
                    End If
                Else
                    MsgBox("Please select an album.", MsgBoxStyle.Critical, Nothing)
                End If
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

        ElseIf cms_missing.SourceControl.Name = "lv_music_missing" Then

            If e.ClickedItem.Text = "Send to importer" Then
                If lv_music_missing.SelectedItems.Count > 0 Then
                    Dim type As String = Nothing
                    For x As Integer = 0 To (lv_music_missing.SelectedItems.Count - 1)
                        li_import = lv_import.Items.Add(lv_music_missing.SelectedItems(x).SubItems.Item(0).Text)
                        li_import.SubItems.Add(lv_music_missing.SelectedItems(x).SubItems.Item(4).Text)
                        li_import.SubItems.Add("Music")

                        If lv_music_missing.SelectedItems(x).SubItems.Item(1).Text <> "" Then
                            type = "music"
                        Else
                            type = "artist"
                        End If

                        l_import_queue.Add(lv_music_missing.SelectedItems(x).SubItems.Item(4).Text & "|" & lv_music_missing.SelectedItems(x).SubItems.Item(0).Text & "|" & type)
                        l_import_index.Add(lv_import.Items.Count - 1)
                    Next

                    For x = (lv_music_missing.SelectedIndices.Count - 1) To 0 Step -1
                        lv_music_missing.Items.RemoveAt(lv_music_missing.SelectedIndices(x))
                    Next

                    If Not bw_import.IsBusy Then
                        FTV_api_connector("queue", Nothing, type, "import")
                    End If
                Else
                    MsgBox("Please select an album or artist.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
                For x = 0 To (lv_music_missing.Items.Count - 1)
                    If lv_music_missing.Items.Count > 0 Then
                        li_import = lv_import.Items.Add(lv_music_missing.Items.Item(x).SubItems(0).Text)
                        li_import.SubItems.Add(lv_music_missing.Items.Item(x).SubItems(4).Text)
                        li_import.SubItems.Add("Music")

                        If lv_music_missing.Items.Item(x).SubItems.Item(1).Text <> "" Then
                            l_import_queue.Add(lv_music_missing.Items.Item(x).SubItems.Item(4).Text & "|" & lv_music_missing.Items.Item(x).SubItems.Item(0).Text & "|music|" & lv_music_missing.Items.Item(x).SubItems.Item(5).Text)
                        Else
                            l_import_queue.Add(lv_music_missing.Items.Item(x).SubItems.Item(4).Text & "|" & lv_music_missing.Items.Item(x).SubItems.Item(0).Text & "|artist")
                        End If

                        l_import_index.Add(lv_import.Items.Count - 1)
                        lv_music_missing.Items.RemoveAt(x)

                        x -= 1
                    End If
                Next

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "music", "import")
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                For x As Integer = 0 To (lv_music_missing.SelectedItems.Count - 1)

                    Dim title As String = lv_music_missing.SelectedItems(x).SubItems(0).Text

                    If lv_music_missing.SelectedItems(x).SubItems.Item(1).Text <> "" Then
                        Dim upload As New DVDArt_ManualUpload(title, title, "music")
                        upload.ShowDialog()
                    Else
                        Dim upload As New DVDArt_ManualUpload(title, title, "artist")
                        upload.ShowDialog()
                    End If

                    If FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, 1, 1) & title & ".png") Or _
                       FileSystem.FileExists(thumbs & DVDArt_Common.folder(2, 2, 1) & title & ".png") Then

                        Dim mbid As String = lv_music_missing.SelectedItems(x).SubItems(4).Text

                        li_import = lv_import.Items.Add(title)
                        li_import.SubItems.Add(title)
                        li_import.SubItems.Add("Music")
                        l_import_queue.Add(title & "|" & title & "|music")
                        l_import_index.Add(lv_import.Items.Count - 1)

                        lv_music_missing.Items.Remove(lv_music_missing.SelectedItems(x))

                        If lv_music_missing.SelectedItems(x).SubItems.Item(1).Text <> " " Then
                            If lv_album.FindItemWithText(title) Is Nothing Then
                                li_album = lv_series.Items.Add(title)
                                li_album.SubItems.Add(mbid)
                            End If
                        Else
                            If lv_artist.FindItemWithText(title) Is Nothing Then
                                li_artist = lv_series.Items.Add(title)
                                li_artist.SubItems.Add(mbid)
                            End If
                        End If

                    End If

                Next
            ElseIf e.ClickedItem.Text = "Change MBID" Then
                If lv_music_missing.SelectedItems.Count > 0 Then
                    For x As Integer = 0 To (lv_music_missing.SelectedItems.Count - 1)
                        Dim MBID As String = lv_music_missing.SelectedItems(0).SubItems.Item(4).Text
                        Dim change As New DVDArt_ChangeMBID
                        change.ChangeMBID(MBID, lv_music_missing.SelectedItems(0).SubItems.Item(4).Text)

                        If MBID <> lv_music_missing.SelectedItems(0).SubItems.Item(4).Text Then
                            changeMBID(MBID, lv_music_missing.SelectedItems(0).SubItems.Item(4).Text)
                        End If
                    Next
                Else
                    MsgBox("Please select an album or artist.", MsgBoxStyle.Critical, Nothing)
                End If

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

    Private Sub lv_artist_banner_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_artist_banner.SelectedIndexChanged

        For Each item In lv_artist_banner.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_banner.Images(itemkey)

            pb_artist_banner.Image = image
            pb_artist_banner.Tag = lv_url_clearart.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_artist_clearlogo_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_artist_clearlogo.SelectedIndexChanged

        For Each item In lv_artist_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_artist_clearlogo.Image = image
            pb_artist_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

    End Sub

    Private Sub lv_music_cdart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_album_cdart.SelectedIndexChanged

        For Each item In lv_album_cdart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_dvdart.Images(itemkey)

            pb_album_cdart.Image = image
            pb_album_cdart.Tag = lv_url_dvdart.Items(item.index).Text

        Next

    End Sub

    Private Sub setSettings()

        On Error Resume Next

        Using XMLwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            XMLwriter.SetValue("Plugin", "version", DVDArt_Common._version)
            XMLwriter.SetValue("Settings", "delay", nud_delay.Value)
            XMLwriter.SetValue("Settings", "delay value", cb_delay.Text)
            XMLwriter.SetValueAsBool("Settings", "backgroundscraper", cb_backgroundscraper.Checked)
            XMLwriter.SetValue("Settings", "CPU utilisation", mtb_cpu.Text)
            XMLwriter.SetValue("Settings", "scraping", nud_scraping.Value)
            XMLwriter.SetValue("Settings", "scraping value", cb_scraping.Text)
            XMLwriter.SetValue("Settings", "missing", nud_missing.Value)
            XMLwriter.SetValue("Settings", "missing value", cb_missing.Text)
            XMLwriter.SetValue("Scraper", "language", DVDArt_Common.langcode(Array.IndexOf(DVDArt_Common.lang, cb_language.Text)))
            XMLwriter.SetValueAsBool("Scraper Movies", "dvdart", cb_DVDArt_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "clearart", cb_ClearArt_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "clearlogo", cb_ClearLogo_movies.Checked)
            XMLwriter.SetValue("Scraper Movies", "template", template_type)
            XMLwriter.SetValueAsBool("Scraper Series", "clearart", cb_ClearArt_series.Checked)
            XMLwriter.SetValueAsBool("Scraper Series", "clearlogo", cb_ClearLogo_series.Checked)
            XMLwriter.SetValueAsBool("Scraper Music", "cdart", cb_CDArt_music.Checked)
            XMLwriter.SetValueAsBool("Scraper Music", "banner", cb_Banner_artist.Checked)
            XMLwriter.SetValueAsBool("Scraper Music", "clearlogo", cb_ClearLogo_artist.Checked)

            If _lastrun = Nothing Then XMLwriter.SetValue("Scheduler", "lastrun", Now)

        End Using

        MediaPortal.Profile.Settings.SaveCache()

    End Sub

    Private Sub getSettings()

        Dim xml_version As String

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            xml_version = XMLreader.GetValueAsString("Plugin", "version", "0")
            nud_delay.Value = XMLreader.GetValueAsInt("Settings", "delay", 1)
            cb_delay.Text = XMLreader.GetValueAsString("Settings", "delay value", "minutes")
            cb_backgroundscraper.Checked = XMLreader.GetValueAsBool("Settings", "backgroundscraper", True)
            mtb_cpu.Text = XMLreader.GetValueAsString("Settings", "CPU utilisation", 30)
            nud_scraping.Value = XMLreader.GetValueAsInt("Settings", "scraping", 15)
            cb_scraping.Text = XMLreader.GetValueAsString("Settings", "scraping value", "minutes")
            nud_missing.Value = XMLreader.GetValueAsInt("Settings", "missing", 0)
            cb_missing.Text = XMLreader.GetValueAsString("Settings", "missing value", "disabled")
            _lang = XMLreader.GetValueAsString("Scraper", "language", "##")
            template_type = XMLreader.GetValueAsInt("Scraper Movies", "template", 1)

            If xml_version > DVDArt_Common._pre_version Then
                cb_DVDArt_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "dvdart", False)
                cb_ClearArt_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "clearart", False)
                cb_ClearLogo_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "clearlogo", False)
                cb_ClearArt_series.Checked = XMLreader.GetValueAsBool("Scraper Series", "clearart", False)
                cb_ClearLogo_series.Checked = XMLreader.GetValueAsBool("Scraper Series", "clearlogo", False)
                cb_CDArt_music.Checked = XMLreader.GetValueAsBool("Scraper Music", "cdart", False)
                cb_Banner_artist.Checked = XMLreader.GetValueAsBool("Scraper Music", "banner", False)
                cb_ClearLogo_artist.Checked = XMLreader.GetValueAsBool("Scraper Music", "clearlogo", False)
            Else
                cb_DVDArt_movies.Checked = XMLreader.GetValueAsBool("Scraper", "dvdart", False)
                cb_ClearArt_movies.Checked = XMLreader.GetValueAsBool("Scraper", "clearart", False)
                cb_ClearLogo_movies.Checked = XMLreader.GetValueAsBool("Scraper", "clearlogo", False)
            End If

            cb_language.Text = DVDArt_Common.lang(Array.IndexOf(DVDArt_Common.langcode, _lang))
            _lastrun = XMLreader.GetValueAsString("Scheduler", "lastrun", Nothing)

        End Using

        rb_t1.Checked = (template_type = 1)
        rb_t2.Checked = (template_type = 2)

        If xml_version <> DVDArt_Common._version Then

            cb_ClearArt_series.Checked = cb_ClearArt_movies.Checked
            cb_ClearLogo_series.Checked = cb_ClearLogo_movies.Checked
            cb_CDArt_music.Checked = cb_DVDArt_movies.Checked
            cb_Banner_artist.Checked = cb_ClearArt_movies.Checked
            cb_ClearLogo_artist.Checked = cb_ClearLogo_movies.Checked
            cb_backgroundscraper.Checked = True

            FileIO.FileSystem.DeleteFile(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            MediaPortal.Profile.Settings.ClearCache()

            setSettings()

        End If

    End Sub

    Private Sub cb_backgroundscraper_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_backgroundscraper.CheckedChanged

        cb_delay.Enabled = cb_backgroundscraper.Checked
        nud_delay.Enabled = cb_backgroundscraper.Checked
        mtb_cpu.Enabled = cb_backgroundscraper.Checked
        nud_scraping.Enabled = cb_backgroundscraper.Checked
        cb_scraping.Enabled = cb_backgroundscraper.Checked
        nud_missing.Enabled = cb_backgroundscraper.Checked
        cb_missing.Enabled = cb_backgroundscraper.Checked

    End Sub

    Private Sub cb_DVDArt_movies_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_DVDArt_movies.CheckedChanged

        checked(0, 0) = cb_DVDArt_movies.Checked

        If cb_DVDArt_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
            If tbc_movies.TabCount = 0 Then tbc_movies.TabPages.Add(tp_Movie_DVDArt) Else tbc_movies.TabPages.Insert(0, tp_Movie_DVDArt)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
        End If

    End Sub

    Private Sub cb_ClearArt_movies_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearArt_movies.CheckedChanged

        checked(0, 1) = cb_ClearArt_movies.Checked

        If cb_ClearArt_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_ClearArt) Then tbc_movies.TabPages.Remove(tp_Movie_ClearArt)

            If tbc_movies.TabCount > 0 Then
                If tbc_movies.TabPages.Contains(tp_Movie_DVDArt) And Not tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then
                    tbc_movies.TabPages.Add(tp_Movie_ClearArt)
                ElseIf Not tbc_movies.TabPages.Contains(tp_Movie_DVDArt) And tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabCount - 1, tp_Movie_ClearArt)
                Else
                    tbc_movies.TabPages.Insert(1, tp_Movie_ClearArt)
                End If
            Else
                tbc_movies.TabPages.Add(tp_Movie_ClearArt)
            End If
        Else
            tbc_movies.TabPages.Remove(tp_Movie_ClearArt)
        End If

    End Sub

    Private Sub cb_ClearLogo_movies_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearLogo_movies.CheckedChanged

        checked(0, 2) = cb_ClearLogo_movies.Checked

        If cb_ClearLogo_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)
            tbc_movies.TabPages.Add(tp_Movie_ClearLogo)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)
        End If

    End Sub

    Private Sub cb_ClearArt_series_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearArt_series.CheckedChanged

        checked(1, 1) = cb_ClearArt_series.Checked

        If cb_ClearArt_series.Checked = True Then
            If tbc_series.TabPages.Contains(tp_Serie_ClearArt) Then tbc_series.TabPages.Remove(tp_Serie_ClearArt)
            If tbc_series.TabCount = 0 Then tbc_series.TabPages.Add(tp_Serie_ClearArt) Else tbc_series.TabPages.Insert(0, tp_Serie_ClearArt)
        Else
            tbc_series.TabPages.Remove(tp_Serie_ClearArt)
        End If

    End Sub

    Private Sub cb_ClearLogo_series_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearLogo_series.CheckedChanged

        checked(1, 2) = cb_ClearLogo_series.Checked

        If cb_ClearLogo_series.Checked = True Then
            If tbc_series.TabPages.Contains(tp_Serie_ClearLogo) Then tbc_series.TabPages.Remove(tp_Serie_ClearLogo)
            tbc_series.TabPages.Add(tp_Serie_ClearLogo)
        Else
            tbc_series.TabPages.Remove(tp_Serie_ClearLogo)
        End If

    End Sub

    Private Sub cb_CDArt_music_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_CDArt_music.CheckedChanged

        checked(2, 0) = cb_CDArt_music.Checked

        If cb_CDArt_music.Checked = True Then
            If tbc_album.TabPages.Contains(tp_Music_CDArt) Then tbc_album.TabPages.Remove(tp_Music_CDArt)
            tbc_album.TabPages.Add(tp_Music_CDArt)
        Else
            tbc_album.TabPages.Remove(tp_Music_CDArt)
        End If

    End Sub

    Private Sub cb_Banner_artist_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_Banner_artist.CheckedChanged

        checked(2, 1) = cb_Banner_artist.Checked

        If cb_Banner_artist.Checked = True Then
            If tbc_artist.TabPages.Contains(tp_artist_banner) Then tbc_artist.TabPages.Remove(tp_artist_banner)
            If tbc_artist.TabCount = 0 Then tbc_artist.TabPages.Add(tp_artist_banner) Else tbc_artist.TabPages.Insert(0, tp_artist_banner)
        Else
            tbc_artist.TabPages.Remove(tp_artist_banner)
        End If

    End Sub

    Private Sub cb_ClearLogo_artist_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_ClearLogo_artist.CheckedChanged

        checked(2, 2) = cb_ClearLogo_artist.Checked

        If cb_ClearLogo_artist.Checked = True Then
            If tbc_artist.TabPages.Contains(tp_artist_clearlogo) Then tbc_artist.TabPages.Remove(tp_artist_clearlogo)
            tbc_artist.TabPages.Add(tp_artist_clearlogo)
        Else
            tbc_artist.TabPages.Remove(tp_artist_clearlogo)
        End If

    End Sub

    Private Sub rb_t1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_t1.CheckedChanged
        rb_t1.Image = My.Resources.template_1
        rb_t2.Image = My.Resources.template_2_disabled
        template_type = 1
    End Sub

    Private Sub rb_t2_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles rb_t2.CheckedChanged
        rb_t1.Image = My.Resources.template_1_disabled
        rb_t2.Image = My.Resources.template_2
        template_type = 2
    End Sub

    Private Sub b_movie_compress_Click(sender As System.Object, e As System.EventArgs) Handles b_movie_compress.Click

        DVDArt_Common.Resize(thumbs & DVDArt_Common.folder(0, 0, 0) & current_imdb_id & ".png")

        l_movie_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(0, 0, 0), current_imdb_id & ".png")

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

    Private Sub b_movie_delete_Click(sender As System.Object, e As System.EventArgs) Handles b_movie_delete.Click
        FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(0, 0, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
        FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(0, 0, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
        pb_movie_dvdart.Image = Nothing
        pb_movie_dvdart.Tag = Nothing
        b_movie_compress.Visible = False
        b_movie_preview.Visible = False
        b_movie_delete.Visible = False
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

    Private Sub b_album_compress_Click(sender As System.Object, e As System.EventArgs) Handles b_album_compress.Click

        DVDArt_Common.Resize(thumbs & DVDArt_Common.folder(2, 0, 0) & current_album & ".png")

        l_music_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(2, 0, 0), current_album & ".png")

        If l_music_size.Text = "500x500" Then b_album_compress.Visible = False Else b_album_compress.Visible = True

        b_album_preview.Visible = Not b_album_compress.Visible

    End Sub

    Private Sub b_album_preview_Click(sender As System.Object, e As System.EventArgs) Handles b_album_preview.Click
        Dim preview As New DVDArt_Preview(thumbs & DVDArt_Common.folder(2, 0, 0) & current_album & ".png")
        preview.Show()
    End Sub

    Private Sub b_album_deletecdart_Click(sender As System.Object, e As System.EventArgs) Handles b_album_delete.Click
        FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 0, 0) & lv_movies.SelectedItems(0).SubItems.Item(0).Text & ".png")
        FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 0, 1) & lv_movies.SelectedItems(0).SubItems.Item(0).Text & ".png")
        pb_album_cdart.Image = Nothing
        pb_album_cdart.Tag = Nothing
        b_album_compress.Visible = False
        b_album_preview.Visible = False
        b_album_delete.Visible = False
    End Sub

    Private Sub b_artist_deletebanner_Click(sender As System.Object, e As System.EventArgs) Handles b_artist_deletebanner.Click
        If lv_artist.SelectedItems(0).SubItems.Item(0).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 1, 0) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 1, 1) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            pb_artist_banner.Image = Nothing
            pb_artist_banner.Tag = Nothing
            b_artist_deletebanner.Visible = False
        End If
    End Sub

    Private Sub b_artist_deletelogo_Click(sender As System.Object, e As System.EventArgs) Handles b_artist_deletelogo.Click
        If lv_artist.SelectedItems(0).SubItems.Item(0).Text <> Nothing Then
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 2, 0) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            FileSystem.DeleteFile(thumbs & DVDArt_Common.folder(2, 2, 1) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            pb_artist_clearlogo.Image = Nothing
            pb_artist_clearlogo.Tag = Nothing
            b_artist_deletelogo.Visible = False
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

            setSettings()

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

            'create folder structure
            If DVDArt_Common.Create_Folder_Structure(database, thumbs) = False Then
                Application.Exit()
            End If

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
            cb_DVDArt_movies_CheckedChanged(Nothing, Nothing)
            cb_ClearArt_movies_CheckedChanged(Nothing, Nothing)
            cb_ClearLogo_movies_CheckedChanged(Nothing, Nothing)
            cb_ClearArt_series_CheckedChanged(Nothing, Nothing)
            cb_ClearLogo_series_CheckedChanged(Nothing, Nothing)
            cb_CDArt_music_CheckedChanged(Nothing, Nothing)
            cb_Banner_artist_CheckedChanged(Nothing, Nothing)
            cb_ClearLogo_artist_CheckedChanged(Nothing, Nothing)

            'populate language dropdown
            cb_language.Items.AddRange(DVDArt_Common.lang)

            getSettings()

            'set the tab pages
            If tbc_main.TabPages.Contains(tp_MovingPictures) Then tbc_main.TabPages.Remove(tp_MovingPictures)
            If FileSystem.FileExists(database & "\movingpictures.db3") Then
                tbc_main.TabPages.Insert(0, tp_MovingPictures)
            End If

            If tbc_main.TabPages.Contains(tp_TVSeries) Then tbc_main.TabPages.Remove(tp_TVSeries)
            If FileSystem.FileExists(database & "\TVSeriesDatabase4.db3") Then
                If tbc_main.TabPages.Contains(tp_MovingPictures) Then tbc_main.TabPages.Insert(1, tp_TVSeries) Else tbc_main.TabPages.Insert(0, tp_TVSeries)
            End If

            If tbc_main.TabPages.Contains(tp_Music) Then tbc_main.TabPages.Remove(tp_Music)
            If FileSystem.FileExists(database & "\MusicDatabaseV12.db3") Then
                If tbc_main.TabPages.Contains(tp_MovingPictures) And tbc_main.TabPages.Contains(tp_TVSeries) Then
                    tbc_main.TabPages.Insert(2, tp_Music)
                ElseIf Not tbc_main.TabPages.Contains(tp_MovingPictures) Or Not tbc_main.TabPages.Contains(tp_TVSeries) Then
                    tbc_main.TabPages.Insert(1, tp_Music)
                Else
                    tbc_main.TabPages.Insert(0, tp_Music)
                End If
            End If

            'set focus to first tab page
            tbc_main.SelectedIndex = 0

            ' load the data
            LoadSerieList()
            LoadMovieList()
            LoadArtistList()
            LoadMusicList()
            
            'close splashscreen
            splash.Close()
            splash.Dispose()

            'initialize timer
            t_import_timer.Interval = 5000
            t_import_timer.Start()

        Else
            MsgBox("Unable to load Database & Thumbs paths from MediaPortalDirs.xml", MsgBoxStyle.Critical, "DVDArt Plugin")
            Return
        End If

    End Sub

    End Class