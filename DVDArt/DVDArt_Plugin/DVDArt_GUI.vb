Imports Microsoft.VisualBasic.FileIO.FileSystem

Imports System.Data
Imports System.Data.SQLite
Imports System.Runtime.InteropServices
Imports System.ComponentModel
Imports System.Threading
Imports System.Text.RegularExpressions

Imports MediaPortal.Configuration
Imports MediaPortal.Util

Imports MyFilmsPlugin.DataBase
Imports MyFilmsPlugin.MyFilms
Imports MyFilmsPlugin.MyFilms.Utils

Public Class DVDArt_GUI

    Public Shared checked(2, 5) As Boolean
    Public Shared template_type As Integer
    Public Shared personchecked As Boolean

    Public Shared personpath As String

    Private WithEvents bw_compress, bw_coverart, bw_rescan_persons As New BackgroundWorker
    Private WithEvents t_import_timer As New System.Windows.Forms.Timer
    Private thumbs, current_imdb_id, current_person, current_thetvdb_id, current_artist, current_album, _lang, _lastrun, xml_version As String
    Private l_new_movies As New List(Of String)
    Private l_import_queue As New List(Of String)
    Private l_import_index As New List(Of Integer)
    Private lvwColumnSorter = New ListViewColumnSorter()
    Private lv_url_dvdart, lv_url_clearart, lv_url_clearlogo, lv_url_banner, lv_url_backdrop, lv_url_cover As New ListView
    Private li_movies, li_person, li_series, li_artist, li_album, li_import, li_missing As New ListViewItem

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

        Dim oStopWatch As Diagnostics.Stopwatch = New Diagnostics.Stopwatch()

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

    Private Sub bw_coverart_worker(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bw_coverart.DoWork

        If Not IO.File.Exists(DVDArt_Common.p_Databases("movingpictures")) Then Exit Sub

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim imdb_id, images() As String
        Dim x, y, z As Integer

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("movingpictures") & ";Read Only=True;"
        SQLconnect.Open()

        CheckForIllegalCrossThreadCalls = False

        For x = 0 To (lv_movies_missing.SelectedItems.Count - 1)

            imdb_id = lv_movies_missing.SelectedItems(x).SubItems.Item(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text

            If Not IO.File.Exists(DVDArt_Common.folder(0, 0, 1)) Then

                SQLcommand.CommandText = "SELECT alternatecovers, coverfullpath, title FROM movie_info WHERE imdb_id = """ & imdb_id & """"
                SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)
                SQLreader.Read()

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

    Private Sub bw_compress_worker(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_compress.DoWork

        For Each filePath As String In IO.Directory.GetFiles(thumbs & DVDArt_Common.folder(0, 0, 0))

            If Not DVDArt_Common.FileInUse(filePath) Then
                If DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(0, 0, 0) & IO.Path.GetFileName(filePath)) <> "500x500" Then DVDArt_Common.Resize(filePath)
            End If

        Next

    End Sub

    Private Sub bw_import_worker(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_import.DoWork

        Dim parm As String = e.Argument
        Dim x, y As Integer
        Dim added, addedmissing, filenotexist(4), downloaded(4) As Boolean
        Dim MBID As String = Nothing
        Dim backdrop As String = Nothing
        Dim cover As String = Nothing
        Dim lvi As LVITEM

        CheckForIllegalCrossThreadCalls = False

        If parm = Nothing Then

            If lv_import.Items.Count > 0 Then

                x = 0

                Do While x < lv_import.Items.Count

                    lv_import.Items(x).StateImageIndex = 0

                    added = False
                    addedmissing = False
                    lvi = Nothing

                    If lv_import.Items.Item(x).SubItems.Item(2).Text = "Movie" Then

                        backdrop = lv_import.Items.Item(x).SubItems.Item(3).Text
                        cover = lv_import.Items.Item(x).SubItems.Item(4).Text

                        downloaded = DVDArt_Common.import(thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, lv_import.Items.Item(x).SubItems.Item(0).Text, _lang, "movies", personpath, checked, backdrop, cover)

                        For y = 0 To UBound(downloaded)

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(x).StateImageIndex = 4 Else lv_import.Items(x).StateImageIndex = 1
                                End If

                                added = added Or (lv_movies.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text) IsNot Nothing)

                                If Not added Then
                                    li_movies = lv_movies.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_movies.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    li_movies.SubItems.Add(backdrop)
                                    li_movies.SubItems.Add(cover)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(x).StateImageIndex = 2 Else lv_import.Items(x).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_movies_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_movies_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(0, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(0, y) Then
                                If downloaded(y) Then
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

                        If addedmissing Then
                            'li_missing.SubItems.Add(" ")
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Series" Then

                        downloaded = DVDArt_Common.import(thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, lv_import.Items.Item(x).SubItems.Item(0).Text, _lang, "tv", personpath, checked)

                        For y = 1 To 2

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(x).StateImageIndex = 4 Else lv_import.Items(x).StateImageIndex = 1
                                End If

                                added = added Or (lv_series.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text) IsNot Nothing)

                                If Not added Then
                                    li_series = lv_series.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_series.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(x).StateImageIndex = 2 Else lv_import.Items(x).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_series_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_series_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(1, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(1, y) Then
                                If downloaded(y) Then
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

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Artist" Then

                        MBID = DVDArt_Common.get_Artist_MBID(lv_import.Items.Item(x).SubItems.Item(0).Text)

                        lv_import.Items.Item(x).SubItems.Item(1).Text = MBID

                        If MBID = Nothing Then MBID = "Not found"

                        downloaded = DVDArt_Common.import(thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, Nothing, _lang, "music|" & lv_import.Items.Item(x).SubItems.Item(0).Text, personpath, checked)

                        For y = 0 To 2

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(x).StateImageIndex = 4 Else lv_import.Items(x).StateImageIndex = 1
                                End If

                                added = added Or (lv_album.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text) IsNot Nothing)

                                If Not added Then
                                    li_album = lv_album.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_album.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(x).StateImageIndex = 2 Else lv_import.Items(x).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(2, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y > 0 Then
                                If checked(2, y) Then
                                    If downloaded(y) Then
                                        lvi.iImage = 1
                                    Else
                                        lvi.iImage = 2
                                    End If
                                Else
                                    lvi.iImage = 3
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Music" Then

                        MBID = DVDArt_Common.get_MBID(lv_import.Items.Item(x).SubItems.Item(0).Text, lv_import.Items.Item(x).SubItems.Item(3).Text)

                        lv_import.Items.Item(x).SubItems.Item(1).Text = MBID

                        If MBID = Nothing Then MBID = "Not found"

                        downloaded = DVDArt_Common.import(thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, lv_import.Items.Item(x).SubItems.Item(0).Text, _lang, "music/albums|" & lv_import.Items.Item(x).SubItems.Item(3).Text, personpath, checked)

                        For y = 0 To 2

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(x).StateImageIndex = 4 Else lv_import.Items(x).StateImageIndex = 1
                                End If

                                added = added Or (lv_album.FindItemWithText(lv_import.Items.Item(x).SubItems.Item(0).Text) IsNot Nothing)

                                If Not added Then
                                    li_album = lv_album.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    li_album.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(x).StateImageIndex = 2 Else lv_import.Items(x).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_music_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(2, y) And y = 0 Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y = 0 Then
                                If checked(2, y) Then
                                    If downloaded(y) Then
                                        lvi.iImage = 1
                                    Else
                                        lvi.iImage = 2
                                    End If
                                Else
                                    lvi.iImage = 3
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                        Next

                        If addedmissing Then
                            li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                        Else
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        End If

                    ElseIf lv_import.Items.Item(x).SubItems.Item(2).Text = "Person" Then

                        Dim person As String = lv_import.Items.Item(x).SubItems.Item(0).Text

                        downloaded = DVDArt_Common.import(thumbs, Nothing, person, _lang, "person", personpath, checked)

                        If IO.File.Exists(personpath & Utils.MakeFileName(person) & ".png") Then
                            lv_import.Items(x).StateImageIndex = 4
                            li_person = lv_person.Items.Add(person)
                        Else
                            lv_import.Items(x).StateImageIndex = 2
                            li_missing = lv_persons_missing.Items.Add(person)
                        End If

                    End If

                    Try
                        lv_import.Items(x).EnsureVisible()
                    Catch ex As Exception
                    End Try

                    x += 1

                Loop

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

                    If type = "movies" Then

                        backdrop = DVDArt_Common.getImagePath(id, "backdrop")
                        cover = DVDArt_Common.getImagePath(id, "cover")

                        downloaded = DVDArt_Common.import(thumbs, id, title, _lang, type, personpath, checked, backdrop, cover)

                        For y = 0 To UBound(downloaded)

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(l_import_index(x)).StateImageIndex = 4 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1
                                End If

                                added = added Or (lv_movies.FindItemWithText(title) IsNot Nothing)

                                If Not added Then
                                    li_movies = lv_movies.Items.Add(title)
                                    li_movies.SubItems.Add(id)
                                    li_movies.SubItems.Add(backdrop)
                                    li_movies.SubItems.Add(cover)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(l_import_index(x)).StateImageIndex = 2 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_movies_missing.Items.Add(title)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_movies_missing.Items.Add(title)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(0, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(0, y) Then
                                If downloaded(y) Then
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

                    ElseIf type = "tv" Then

                        downloaded = DVDArt_Common.import(thumbs, id, title, _lang, type, personpath, checked)

                        For y = 1 To 2

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(l_import_index(x)).StateImageIndex = 4 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1
                                End If

                                added = added Or (lv_series.FindItemWithText(title) IsNot Nothing)

                                If Not added Then
                                    li_series = lv_series.Items.Add(title)
                                    li_series.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(l_import_index(x)).StateImageIndex = 2 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_series_missing.Items.Add(title)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_series_missing.Items.Add(title)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(1, y) Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If checked(1, y) Then
                                If downloaded(y) Then
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

                    ElseIf type = "music" Then

                        If id = "" Then
                            id = DVDArt_Common.get_Artist_MBID(title)
                            lv_import.Items(l_import_index(x)).SubItems.Item(1).Text = id
                        End If

                        If id = Nothing Then MBID = "Not found"

                        downloaded = DVDArt_Common.import(thumbs, id, title, _lang, type & "|" & title, personpath, checked)

                        For y = 0 To 2

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(l_import_index(x)).StateImageIndex = 4 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1
                                End If

                                added = added Or (lv_artist.FindItemWithText(title, True, 0, False) IsNot Nothing)

                                If Not added Then
                                    li_artist = lv_artist.Items.Add(title)
                                    li_artist.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(l_import_index(x)).StateImageIndex = 2 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_music_missing.Items.Add(title)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_music_missing.Items.Add(title)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(2, y) And y <> 0 Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y <> 0 Then
                                If checked(2, y) Then
                                    If downloaded(y) Then
                                        lvi.iImage = 1
                                    Else
                                        lvi.iImage = 2
                                    End If
                                Else
                                    lvi.iImage = 3
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                        Next

                    ElseIf Microsoft.VisualBasic.Left(type, 12) = "music/albums" Then

                        downloaded = DVDArt_Common.import(thumbs, id, title, _lang, type & "|" & title, personpath, checked)

                        For y = 0 To 2

                            If downloaded(y) Then
                                If Not added Then
                                    If Not addedmissing Then lv_import.Items(l_import_index(x)).StateImageIndex = 4 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1
                                End If

                                added = added Or (lv_album.FindItemWithText(title, False, 0, False) IsNot Nothing)

                                If Not added Then
                                    li_album = lv_album.Items.Add(title)
                                    li_album.SubItems.Add(id)
                                    added = True
                                End If
                            Else
                                If Not added Then lv_import.Items(l_import_index(x)).StateImageIndex = 2 Else lv_import.Items(l_import_index(x)).StateImageIndex = 1

                                If Not addedmissing Then
                                    Try
                                        addedmissing = True
                                        li_missing = lv_music_missing.Items.Add(title)
                                    Catch ex As Exception
                                    End Try
                                End If
                            End If

                            If Not addedmissing Then
                                Try
                                    addedmissing = True
                                    li_missing = lv_music_missing.Items.Add(title)
                                Catch ex As Exception
                                End Try
                            End If

                            If checked(2, y) And y = 0 Then
                                If downloaded(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
                                End If
                            Else
                                li_missing.SubItems.Add("")
                            End If

                            lvi.iItem = li_missing.Index
                            lvi.subItem = li_missing.SubItems.Count - 1

                            If y = 0 Then
                                If checked(2, y) Then
                                    If downloaded(y) Then
                                        lvi.iImage = 1
                                    Else
                                        lvi.iImage = 2
                                    End If
                                Else
                                    lvi.iImage = 3
                                End If
                            Else
                                lvi.iImage = 3
                            End If

                            lvi.mask = LVIF_IMAGE
                            ListView_SetItem(lv_music_missing.Handle, lvi)

                        Next

                    ElseIf type = "person" Then

                        downloaded = DVDArt_Common.import(thumbs, Nothing, title, _lang, type, personpath, checked)

                        If IO.File.Exists(personpath & Utils.MakeFileName(title) & ".png") Then
                            lv_import.Items(l_import_index(x)).StateImageIndex = 4
                            li_person = lv_person.Items.Add(title)
                        Else
                            lv_import.Items(l_import_index(x)).StateImageIndex = 2
                            li_missing = lv_persons_missing.Items.Add(title)
                        End If

                    End If

                    lv_import.Items(l_import_index(x)).EnsureVisible()

                    If addedmissing Then
                        li_missing.SubItems.Add(id)
                    Else
                        Try
                            li_missing.SubItems.RemoveAt(li_missing.Index)
                        Catch ex As Exception
                        End Try
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

        If Not IO.File.Exists(DVDArt_Common.p_Databases("movingpictures")) Then Exit Sub

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim imdb_id, movie_name, images() As String
        Dim x, y, z, count As Integer

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("movingpictures") & ";Read Only=True;"
        SQLconnect.Open()

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
                imdb_id = lv_movies_missing.SelectedItems(x).SubItems.Item(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text
            End If

            If Not IO.File.Exists(DVDArt_Common.folder(0, 0, 1)) Then

                SQLcommand.CommandText = "SELECT alternatecovers, coverfullpath FROM movie_info WHERE imdb_id = """ & imdb_id & """"
                SQLreader = SQLcommand.ExecuteReader(CommandBehavior.SingleRow)
                SQLreader.Read()

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
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand.CommandText = "UPDATE processed_artist SET MBID = '" & new_MBID & "' WHERE MBID = '" & old_MBID & "'"
        SQLcommand.ExecuteNonQuery()
        SQLcommand.CommandText = "UPDATE processed_music SET MBID = '" & new_MBID & "' WHERE MBID = '" & old_MBID & "'"
        SQLcommand.ExecuteNonQuery()
        SQLconnect.Close()

        Dim x As Integer

        For x = 0 To (lv_artist.Items.Count - 1)
            If lv_artist.Items(x).SubItems.Item(1).Text = old_MBID Then
                lv_artist.Items(x).SubItems.Item(1).Text = new_MBID
                li_import = lv_import.Items.Add(lv_artist.Items(x).SubItems.Item(0).Text)
                li_import.SubItems.Add(new_MBID)
                li_import.SubItems.Add("Music")
                l_import_queue.Add(new_MBID & "|" & lv_artist.Items(x).SubItems.Item(0).Text & "|music")
                l_import_index.Add(lv_import.Items.Count - 1)
            End If
        Next

        For x = 0 To (lv_album.Items.Count - 1)
            If lv_album.Items(x).SubItems.Item(1).Text = old_MBID Then
                lv_album.Items(x).SubItems.Item(1).Text = new_MBID
                li_import = lv_import.Items.Add(lv_album.Items(x).SubItems.Item(0).Text)
                li_import.SubItems.Add(new_MBID)
                li_import.SubItems.Add("Music")
                l_import_queue.Add(new_MBID & "|" & lv_album.Items(x).SubItems.Item(0).Text & "|music/albums|" & lv_album.Items(x).SubItems.Item(2).Text)
                l_import_index.Add(lv_import.Items.Count - 1)
            End If
        Next

        For x = 0 To (lv_music_missing.Items.Count - 1)
            If lv_music_missing.Items(x).SubItems.Item(4).Text = old_MBID Then
                lv_music_missing.Items(x).SubItems.Item(4).Text = new_MBID
                li_import = lv_import.Items.Add(lv_music_missing.Items.Item(x).SubItems(0).Text)
                li_import.SubItems.Add(lv_music_missing.Items.Item(x).SubItems(4).Text)
                li_import.SubItems.Add("Music")

                If lv_music_missing.Items.Item(x).SubItems.Item(1).Text <> "" Then
                    l_import_queue.Add(lv_music_missing.Items.Item(x).SubItems.Item(4).Text & "|" & lv_music_missing.Items.Item(x).SubItems.Item(0).Text & "|music/albums|" & lv_music_missing.Items.Item(x).SubItems.Item(4).Text)
                Else
                    l_import_queue.Add(lv_music_missing.Items.Item(x).SubItems.Item(4).Text & "|" & lv_music_missing.Items.Item(x).SubItems.Item(0).Text & "|music")
                End If

                l_import_index.Add(lv_import.Items.Count - 1)
            End If
        Next

    End Sub

    Private Sub Restart_Importer()

        Me.Cursor = Cursors.WaitCursor

        lv_music_missing.Items.Clear()

        LoadSerieList()
        LoadMovieList()
        loadPersonList()
        LoadMusicList()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub FTV_api_connector(ByVal id As String, ByVal url() As String, ByVal type As String, ByVal mode As String)

        If Not cb_autoimport.Checked And b_import.Enabled Then Exit Sub

        Cursor = Cursors.WaitCursor

        Dim x As Integer
        Dim details As Array = Nothing

        x = 0

        If mode = "preview" Then

            Dim jsonresponse As Object

            If Microsoft.VisualBasic.Left(type, 12) <> "music/albums" Then
                jsonresponse = DVDArt_Common.JSON_request(id, type)
            Else
                jsonresponse = DVDArt_Common.JSON_request(id, "music")
            End If

            If jsonresponse IsNot Nothing Then

                Dim max_download As Integer = nud_downloads.Value - 1

                If Microsoft.VisualBasic.Left(type, 12) <> "music/albums" Then
                    details = DVDArt_Common.parse(jsonresponse, type, id, max_download, _lang)
                Else
                    details = DVDArt_Common.parse_music(jsonresponse, LCase(Microsoft.VisualBasic.Right(type, Len(type) - 6).Replace(" ", "-")), max_download)
                End If

                Dim WebClient As New System.Net.WebClient
                Dim stream As System.IO.MemoryStream
                Dim ImageInBytes() As Byte
                Dim imagekey As String

                clearLists()

                If UBound(details, 2) < (nud_downloads.Value - 1) Then max_download = UBound(details, 2)

                For x = 0 To max_download

                    If ((cb_DVDArt_movies.Checked And type = "movies") Or (cb_CDArt_music.Checked And Microsoft.VisualBasic.Left(type, 12) = "music/albums")) And details(0, x) <> Nothing Then
                        imagekey = Guid.NewGuid().ToString()
                        Try
                            ImageInBytes = WebClient.DownloadData(details(0, x).Replace("/fanart/", "/preview/"))
                            stream = New System.IO.MemoryStream(ImageInBytes)
                            il_dvdart.Images.Add(imagekey, Image.FromStream(stream))
                            If type = "movies" Then
                                lv_movie_dvdart.Items.Add(details(1, x), imagekey)
                            ElseIf Microsoft.VisualBasic.Left(type, 12) = "music/albums" Then
                                lv_album_cdart.Items.Add(details(1, x), imagekey)
                            End If
                            lv_url_dvdart.Items.Add(details(0, x), imagekey)
                        Catch ex As System.Net.WebException
                        End Try
                    End If

                    If ((cb_ClearArt_movies.Checked And type = "movies") Or (cb_ClearArt_series.Checked And type = "tv") Or (cb_Banner_artist.Checked And type = "music")) And details(2, x) <> Nothing Then
                        imagekey = Guid.NewGuid().ToString()
                        Try
                            ImageInBytes = WebClient.DownloadData(details(2, x).Replace("/fanart/", "/preview/"))
                            stream = New System.IO.MemoryStream(ImageInBytes)
                            If type = "movies" Then
                                il_clearart.Images.Add(imagekey, Image.FromStream(stream))
                                lv_movie_clearart.Items.Add(details(3, x), imagekey)
                            ElseIf type = "tv" Then
                                il_clearart.Images.Add(imagekey, Image.FromStream(stream))
                                lv_serie_clearart.Items.Add(details(3, x), imagekey)
                            ElseIf type = "music" Then
                                il_banner.Images.Add(imagekey, Image.FromStream(stream))
                                lv_artist_banner.Items.Add(details(3, x), imagekey)
                            End If
                            lv_url_clearart.Items.Add(details(2, x), imagekey)
                        Catch ex As System.Net.WebException
                        End Try
                    End If

                    If ((cb_ClearLogo_movies.Checked And type = "movies") Or (cb_ClearLogo_series.Checked And type = "tv") Or (cb_ClearLogo_artist.Checked And type = "music")) And details(4, x) <> Nothing Then
                        imagekey = Guid.NewGuid().ToString()
                        Try
                            ImageInBytes = WebClient.DownloadData(details(4, x).Replace("/fanart/", "/preview/"))
                            stream = New System.IO.MemoryStream(ImageInBytes)
                            il_clearlogo.Images.Add(imagekey, Image.FromStream(stream))
                            If type = "movies" Then
                                lv_movie_clearlogo.Items.Add(details(5, x), imagekey)
                            ElseIf type = "tv" Then
                                lv_serie_clearlogo.Items.Add(details(5, x), imagekey)
                            ElseIf type = "music" Then
                                lv_artist_clearlogo.Items.Add(details(5, x), imagekey)
                            End If
                            lv_url_clearlogo.Items.Add(details(4, x), imagekey)
                        Catch ex As System.Net.WebException
                        End Try
                    End If

                    If cb_Banner_movies.Checked And type = "movies" And details(6, x) <> Nothing Then
                        imagekey = Guid.NewGuid().ToString()
                        Try
                            ImageInBytes = WebClient.DownloadData(details(6, x).Replace("/fanart/", "/preview/"))
                            stream = New System.IO.MemoryStream(ImageInBytes)
                            il_banner.Images.Add(imagekey, Image.FromStream(stream))
                            lv_movie_banner.Items.Add(details(7, x), imagekey)
                            lv_url_banner.Items.Add(details(6, x), imagekey)
                        Catch ex As System.Net.WebException
                        End Try
                    End If

                    If cb_Backdrop_movies.Checked And type = "movies" And details(8, x) <> Nothing Then
                        imagekey = Guid.NewGuid().ToString()
                        Try
                            ImageInBytes = WebClient.DownloadData(details(8, x).Replace("/w1920/", "/w300/").Replace("/fanart/", "/preview/"))
                            stream = New System.IO.MemoryStream(ImageInBytes)
                            il_backdrop.Images.Add(imagekey, Image.FromStream(stream))
                            lv_movie_backdrop.Items.Add(details(9, x), imagekey)
                            lv_url_backdrop.Items.Add(details(8, x), imagekey)
                        Catch ex As System.Net.WebException
                        End Try
                    End If

                    If cb_Cover_movies.Checked And type = "movies" And details(10, x) <> Nothing Then
                        imagekey = Guid.NewGuid().ToString()
                        Try
                            ImageInBytes = WebClient.DownloadData(details(10, x).Replace("/w1920/", "/w300/").Replace("/fanart/", "/preview/"))
                            stream = New System.IO.MemoryStream(ImageInBytes)
                            il_cover.Images.Add(imagekey, Image.FromStream(stream))
                            lv_movie_cover.Items.Add(details(11, x), imagekey)
                            lv_url_cover.Items.Add(details(10, x), imagekey)
                        Catch ex As System.Net.WebException
                        End Try
                    End If

                Next
            End If

        ElseIf mode = "selected" Then

            Dim parm As Object = Nothing
            Dim fullpath As String = Nothing
            Dim thumbpath As String = Nothing

            For x = 0 To UBound(url)

                If (checked(0, x) Or checked(1, x) Or checked(2, x)) And url(x) <> Nothing Then

                    If type = "movies" Then
                        If x < 4 Then
                            fullpath = thumbs & DVDArt_Common.folder(0, x, 0) & id & ".png"
                            thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & id & ".png"
                        Else
                            fullpath = thumbs & DVDArt_Common.folder(0, x, 0) & id & ".jpg"
                            thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & id & ".jpg"
                        End If
                    ElseIf type = "tv" Then
                        If x = 0 Or x > 2 Then Continue For
                        fullpath = thumbs & DVDArt_Common.folder(1, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(1, x, 1) & id & ".png"
                    ElseIf type = "music" Or type = "album" Then
                        If x = 0 Or x > 2 Then Continue For
                        fullpath = thumbs & DVDArt_Common.folder(2, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(2, x, 1) & id & ".png"
                    ElseIf type = "music/albums" Then
                        If x <> 0 Then Continue For
                        fullpath = thumbs & DVDArt_Common.folder(2, x, 0) & id & ".png"
                        thumbpath = thumbs & DVDArt_Common.folder(2, x, 1) & id & ".png"
                    End If

                    If InStr(url(x), "/w1920/") > 0 Then
                        If x = 4 Then
                            parm = thumbpath & "|" & url(x).Replace("/w1920/", "/w300/")
                        ElseIf x = 5 Then
                            parm = thumbpath & "|" & url(x).Replace("/w1920/", "/w" & DVDArt_Common._coversize & "/")
                        End If
                    ElseIf x = 5 Then
                        parm = thumbpath & "|" & url(x)
                    Else
                        parm = thumbpath & "|" & url(x).Replace("/fanart/", "/preview/")
                    End If

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
                        ElseIf Not DVDArt_Common.bw_download6.IsBusy Then
                            DVDArt_Common.bw_download6.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download6.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download8.IsBusy Then
                            DVDArt_Common.bw_download8.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download8.RunWorkerAsync(parm)
                            Exit Do
                        Else
                            wait(250)
                        End If
                    Loop

                    If x = 0 Then
                        parm = fullpath & "|" & url(x) & "|shrink"
                    ElseIf x = 5 Then
                        parm = fullpath & "|" & url(x).Replace("/w1920/", "/w" & DVDArt_Common._coversize & "/")
                    Else
                        parm = fullpath & "|" & url(x)
                    End If

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
                        ElseIf Not DVDArt_Common.bw_download7.IsBusy Then
                            DVDArt_Common.bw_download7.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download7.RunWorkerAsync(parm)
                            Exit Do
                        ElseIf Not DVDArt_Common.bw_download9.IsBusy Then
                            DVDArt_Common.bw_download9.WorkerSupportsCancellation = True
                            DVDArt_Common.bw_download9.RunWorkerAsync(parm)
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

        Cursor = Cursors.Default

    End Sub

    Private Sub removeTabPages()

        tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
        tbc_movies.TabPages.Remove(tp_Movie_ClearArt)
        tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)
        tbc_movies.TabPages.Remove(tp_Movie_Banner)
        tbc_movies.TabPages.Remove(tp_Movie_Backdrop)
        tbc_movies.TabPages.Remove(tp_Movie_Cover)
        tbc_series.TabPages.Remove(tp_Serie_ClearArt)
        tbc_series.TabPages.Remove(tp_Serie_ClearLogo)
        tbc_album.TabPages.Remove(tp_Music_CDArt)
        tbc_artist.TabPages.Remove(tp_artist_banner)
        tbc_artist.TabPages.Remove(tp_artist_clearlogo)

    End Sub

    Private Sub setMainTabPages(ByVal modules As String)

        Dim x, y, s, e As Integer
        Dim enabled As Boolean

        Select Case modules

            Case "Movie"
                s = 0
                e = 0

            Case "Series"
                s = 1
                e = 1

            Case "Music"
                s = 2
                e = 2

            Case Else
                s = 0
                e = 2

        End Select

        For x = s To e

            enabled = False

            For y = 0 To 2
                enabled = enabled Or checked(x, y)
            Next

            If x = 0 Then
                If tbc_main.TabPages.Contains(tp_MovingPictures) Then tbc_main.TabPages.Remove(tp_MovingPictures)
                If enabled Then
                    If IO.File.Exists(DVDArt_Common.p_Databases("movingpictures")) Or IO.File.Exists(DVDArt_Common.p_Databases("myfilms")) Or IO.File.Exists(DVDArt_Common.p_Databases("myvideos")) Then
                        tbc_main.TabPages.Insert(0, tp_MovingPictures)
                    Else
                        tbc_scraper.TabPages.Remove(tp_movies_scraper)
                    End If
                End If
            End If

            If x = 1 Then
                If tbc_main.TabPages.Contains(tp_TVSeries) Then tbc_main.TabPages.Remove(tp_TVSeries)
                If enabled Then
                    If IO.File.Exists(DVDArt_Common.p_Databases("tvseries")) Then
                        If tbc_main.TabPages.Contains(tp_MovingPictures) Then tbc_main.TabPages.Insert(1, tp_TVSeries) Else tbc_main.TabPages.Insert(0, tp_TVSeries)
                    Else
                        tbc_scraper.TabPages.Remove(tp_series_scraper)
                    End If
                End If
            End If

            If x = 2 Then
                If tbc_main.TabPages.Contains(tp_Music) Then tbc_main.TabPages.Remove(tp_Music)
                If enabled Then
                    If IO.File.Exists(DVDArt_Common.p_Databases("music")) Then
                        If tbc_main.TabPages.Contains(tp_MovingPictures) And tbc_main.TabPages.Contains(tp_TVSeries) Then
                            tbc_main.TabPages.Insert(2, tp_Music)
                        ElseIf Not tbc_main.TabPages.Contains(tp_MovingPictures) Or Not tbc_main.TabPages.Contains(tp_TVSeries) Then
                            tbc_main.TabPages.Insert(1, tp_Music)
                        Else
                            tbc_main.TabPages.Insert(0, tp_Music)
                        End If
                    Else
                        tbc_scraper.TabPages.Remove(tp_music_scraper)
                    End If
                End If
            End If

        Next

    End Sub

    Private Sub LoadMovieList()

        If Not IO.File.Exists(DVDArt_Common.p_Databases("movingpictures")) And Not IO.File.Exists(DVDArt_Common.p_Databases("myfilms")) And Not IO.File.Exists(DVDArt_Common.p_Databases("myvideos")) Then Exit Sub

        ' check if movie scraping is enabled
        Dim x As Integer
        Dim enabled As Boolean = False

        For x = 0 To UBound(checked, 2)
            enabled = enabled Or checked(0, x)
        Next

        If Not enabled Then Exit Sub

        DVDArt_Common.logStats("DVArt: Loading Movie List started.", "DEBUG")

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect, MP_SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_movies() As String = Nothing
        Dim imdb_id_in_mp() As String = Nothing
        Dim found, missing As Boolean

        ' create processed movies table with a unique index

        If Not IO.File.Exists(DVDArt_Common.p_Databases("dvdart")) Then SQLiteConnection.CreateFile(DVDArt_Common.p_Databases("dvdart"))

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_movies(imdb_id TEXT)"
        SQLcommand.ExecuteNonQuery()

        SQLcommand.CommandText = "DELETE FROM processed_movies WHERE rowid NOT IN (SELECT MIN(rowid) FROM processed_movies GROUP BY imdb_id)"
        SQLcommand.ExecuteNonQuery()

        SQLcommand.CommandText = "CREATE UNIQUE INDEX IF NOT EXISTS movies_index ON processed_movies (imdb_id)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed movies to identify newly imported ones in movingpictures

        SQLcommand.CommandText = "SELECT imdb_id FROM processed_movies WHERE imdb_id is not Null ORDER BY imdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            ReDim Preserve processed_movies(x)
            processed_movies(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_movies(0)

        Dim fileexist(UBound(checked, 2)) As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_movies_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        lv_movies.Items.Clear()
        lv_movies_missing.Items.Clear()
        l_new_movies.Clear()

        ' load movies from respective databases

        Dim mymovies As DVDArt_Common.Movies() = DVDArt_Common.loadMovingPictures()

        Try
            mymovies = DVDArt_Common.loadMyFilms(mymovies)
        Catch ex As Exception
        End Try

        Try
            mymovies = DVDArt_Common.loadMyVideos(mymovies)
        Catch ex As Exception
        End Try

        mymovies.Distinct()

        ' process loaded movies

        For i As Integer = 0 To UBound(mymovies)

            If Trim(mymovies(i).imdb_id) <> "" Then

                If processed_movies.Contains(mymovies(i).imdb_id) Then

                    found = False
                    missing = False

                    For y = 0 To UBound(checked, 2)
                        If y < 4 Then
                            fileexist(y) = FileExists(thumbs & DVDArt_Common.folder(0, y, 1) & mymovies(i).imdb_id & ".png")
                        ElseIf y = 4 Then
                            fileexist(y) = FileExists(mymovies(i).backdrop)
                        ElseIf y = 5 Then
                            fileexist(y) = FileExists(mymovies(i).cover)
                        End If
                        If Not found Then found = checked(0, y) And fileexist(y)
                        If Not missing Then missing = checked(0, y) And Not fileexist(y)
                    Next

                    If found Then
                        li_movies = lv_movies.Items.Add(mymovies(i).name)
                        li_movies.SubItems.Add(mymovies(i).imdb_id)
                        li_movies.SubItems.Add(mymovies(i).backdrop)
                        li_movies.SubItems.Add(mymovies(i).cover)
                        li_movies.SubItems.Add(mymovies(i).sortby)
                    End If

                    If missing Then

                        li_missing = lv_movies_missing.Items.Add(mymovies(i).name)

                        lvi = Nothing

                        For y = 0 To UBound(checked, 2)

                            If checked(0, y) Then
                                If fileexist(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
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

                        li_missing.SubItems.Add(mymovies(i).imdb_id)

                    End If

                Else
                    li_import = lv_import.Items.Add(mymovies(i).name)
                    li_import.SubItems.Add(mymovies(i).imdb_id)
                    li_import.SubItems.Add("Movie")
                    li_import.SubItems.Add(mymovies(i).backdrop)
                    li_import.SubItems.Add(mymovies(i).cover)
                    li_import.SubItems.Add(mymovies(i).sortby)
                    l_new_movies.Add(mymovies(i).imdb_id)
                    If cb_autoimport.Checked = False Then
                        l_import_queue.Add(mymovies(i).imdb_id & "|" & mymovies(i).name & "|movies")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    End If
                End If

                ReDim Preserve imdb_id_in_mp(x)
                imdb_id_in_mp(x) = mymovies(i).imdb_id
                x += 1

            End If

        Next

        If x = 0 Then ReDim Preserve imdb_id_in_mp(0)

        ' remove imdb ids from dvdart that no longer exist in movingpictures

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT imdb_id, rowid FROM processed_movies ORDER BY imdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not imdb_id_in_mp.Contains(SQLreader(0)) Then
                SQLdelete.CommandText = "DELETE FROM processed_movies WHERE rowid = " & SQLreader(1)
                SQLdelete.ExecuteNonQuery()
            End If

        End While

        SQLconnect.Close()
        SQLconnect.Dispose()

        ' Show sort order

        lv_movies.ListViewItemSorter = lvwColumnSorter
        lvwColumnSorter.Order = SortOrder.Ascending
        SetSortArrow(lv_movies, 0, lvwColumnSorter.Order)

        DVDArt_Common.logStats("DVArt: Loading Movie List complete.", "DEBUG")

    End Sub

    Private Sub loadPersonList()

        If Not IO.File.Exists(DVDArt_Common.p_Databases("movingpictures")) And Not IO.File.Exists(DVDArt_Common.p_Databases("mfilms")) And Not IO.File.Exists(DVDArt_Common.p_Databases("myvideos")) Then Exit Sub
        If Not personchecked Then Exit Sub

        DVDArt_Common.logStats("DVArt: Loading Persons List started.", "DEBUG")

        Dim mypersons As SortedList = DVDArt_Common.loadMovingPicturesPersons()

        Try
            mypersons = DVDArt_Common.loadMyFilmsPersons(mypersons)
        Catch ex As Exception
        End Try

        Try
            mypersons = DVDArt_Common.loadMyVideosPersons(mypersons)
        Catch ex As Exception
        End Try

        ' process loaded persons

        For i As Integer = 0 To mypersons.Count - 1

            If IO.File.Exists(personpath & Utils.MakeFileName(mypersons.GetKey(i)) & ".png") Then
                li_person = lv_person.Items.Add(mypersons.GetKey(i))
            Else
                If l_new_movies.Contains(mypersons.GetByIndex(i)) Then
                    li_import = lv_import.Items.Add(mypersons.GetKey(i))
                    li_import.SubItems.Add("")
                    li_import.SubItems.Add("Person")
                    If cb_autoimport.Checked = False Then
                        l_import_queue.Add("|" & mypersons.GetKey(i) & "|person")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    End If

                    lv_persons_missing.Items.RemoveByKey(mypersons.GetKey(i))
                Else
                    li_missing = lv_persons_missing.Items.Add(mypersons.GetKey(i))
                End If
            End If

        Next

        DVDArt_Common.logStats("DVArt: Loading Persons List complete.", "DEBUG")

    End Sub

    Private Sub LoadSerieList()

        If Not IO.File.Exists(DVDArt_Common.p_Databases("tvseries")) Then Exit Sub

        ' check if movie scraping is enabled
        Dim x As Integer
        Dim enabled As Boolean = False

        For x = 1 To 2
            enabled = enabled Or checked(1, x)
        Next

        If Not enabled Then Exit Sub

        DVDArt_Common.logStats("DVArt: Loading Series List started.", "DEBUG")

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_series() As String = Nothing
        Dim thetvdb_id_in_tv() As String = Nothing

        ' create processed series table with a unique index

        If Not FileExists(DVDArt_Common.p_Databases("dvdart")) Then SQLiteConnection.CreateFile(DVDArt_Common.p_Databases("dvdart"))

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_series(thetvdb_id TEXT)"
        SQLcommand.ExecuteNonQuery()

        SQLcommand.CommandText = "DELETE FROM processed_series WHERE rowid NOT IN (SELECT MIN(rowid) FROM processed_series GROUP BY thetvdb_id)"
        SQLcommand.ExecuteNonQuery()

        SQLcommand.CommandText = "CREATE UNIQUE INDEX IF NOT EXISTS series_index ON processed_series (thetvdb_id)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed series to identify newly imported ones in TVSeries

        SQLcommand.CommandText = "SELECT thetvdb_id FROM processed_series WHERE thetvdb_id is not Null ORDER BY thetvdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            ReDim Preserve processed_series(x)
            processed_series(x) = SQLreader(0)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_series(0)

        Dim fileexist(2), found, missing As Boolean
        Dim lvi As LVITEM

        x = 0

        SendMessage(lv_series_missing.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)

        lv_series.Items.Clear()
        lv_series_missing.Items.Clear()

        ' load series from respective databases

        Dim myseries As DVDArt_Common.Series() = DVDArt_Common.loadTVSeries()

        ' process loaded series

        For i As Integer = 0 To UBound(myseries)

            If Trim(myseries(i).thetvdb_id) <> "" Then

                If processed_series.Contains(myseries(i).thetvdb_id) Then

                    found = False
                    missing = False

                    For y = 1 To 2
                        fileexist(y) = checked(1, y) And FileExists(thumbs & DVDArt_Common.folder(1, y, 1) & myseries(i).thetvdb_id & ".png")
                        If Not found Then found = checked(1, y) And fileexist(y)
                        If Not missing Then missing = checked(1, y) And Not fileexist(y)
                    Next

                    If found Then
                        li_series = lv_series.Items.Add(myseries(i).name)
                        li_series.SubItems.Add(myseries(i).thetvdb_id)
                        li_series.SubItems.Add(myseries(i).sortname)
                    End If

                    If missing Then

                        li_missing = lv_series_missing.Items.Add(myseries(i).name)

                        lvi = Nothing

                        For y = 1 To 2

                            If checked(1, y) Then
                                If fileexist(y) Then
                                    li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                Else
                                    li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
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

                        li_missing.SubItems.Add(myseries(i).thetvdb_id)

                    End If

                Else
                    li_import = lv_import.Items.Add(myseries(i).name)
                    li_import.SubItems.Add(myseries(i).thetvdb_id)
                    li_import.SubItems.Add("Series")
                    li_import.SubItems.Add(myseries(i).sortname)
                    If cb_autoimport.Checked = False Then
                        l_import_queue.Add(myseries(i).thetvdb_id & "|" & myseries(i).name & "|tv")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    End If
                End If

                ReDim Preserve thetvdb_id_in_tv(x)
                thetvdb_id_in_tv(x) = myseries(i).thetvdb_id
                x += 1

            End If

        Next

        If x = 0 Then ReDim Preserve thetvdb_id_in_tv(0)

        ' remove theTVDB ids from dvdart that no longer exist in TVSeries

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT thetvdb_id, rowid FROM processed_series ORDER BY thetvdb_id"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not thetvdb_id_in_tv.Contains(SQLreader(0)) Then
                SQLdelete.CommandText = "DELETE FROM processed_series WHERE rowid = " & SQLreader(1)
                SQLdelete.ExecuteNonQuery()
            End If

        End While

        SQLconnect.Close()
        SQLconnect.Dispose()

        ' Show sort order

        lv_series.ListViewItemSorter = lvwColumnSorter
        lvwColumnSorter.Order = SortOrder.Ascending
        SetSortArrow(lv_series, 0, lvwColumnSorter.Order)

        DVDArt_Common.logStats("DVArt: Loading Series List complete.", "DEBUG")

    End Sub

    Private Sub LoadMusicList()

        If Not IO.File.Exists(DVDArt_Common.p_Databases("music")) Then Exit Sub

        Dim x As Integer
        Dim artist_enabled As Boolean = False
        Dim album_enabled As Boolean = False

        For x = 0 To 0
            album_enabled = album_enabled Or checked(2, x)
        Next

        For x = 1 To 2
            artist_enabled = artist_enabled Or checked(2, x)
        Next

        If artist_enabled Then LoadArtistList()
        If album_enabled Then LoadAlbumList()

    End Sub

    Private Sub LoadArtistList()

        DVDArt_Common.logStats("DVArt: Loading Artist List starting.", "DEBUG")

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table

        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_artist() As String = Nothing
        Dim processed_MBID() As String = Nothing
        Dim artist() As String = Nothing
        Dim t_artist As String = Nothing
        Dim found, missing As Boolean
        Dim x As Integer = 0

        If Not IO.File.Exists(DVDArt_Common.p_Databases("dvdart")) Then SQLiteConnection.CreateFile(DVDArt_Common.p_Databases("dvdart"))

        Try
            SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
            SQLconnect.Open()
            SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_artist(artist TEXT, MBID TEXT)"
            SQLcommand.ExecuteNonQuery()

            SQLcommand.CommandText = "DELETE FROM processed_artist WHERE rowid NOT IN (SELECT MIN(rowid) FROM processed_artist GROUP BY artist, MBID)"
            SQLcommand.ExecuteNonQuery()

            SQLcommand.CommandText = "CREATE UNIQUE INDEX IF NOT EXISTS artist_index ON processed_artist (artist, MBID)"
            SQLcommand.ExecuteNonQuery()

            ' Read already processed movies to identify newly imported ones in music
            SQLcommand.CommandText = "SELECT artist, MBID FROM processed_artist WHERE artist is not Null ORDER BY artist"
            SQLreader = SQLcommand.ExecuteReader()

            While SQLreader.Read()

                ReDim Preserve processed_artist(x)
                ReDim Preserve processed_MBID(x)
                processed_artist(x) = "@" & LCase(Utils.MakeFileName(SQLreader(0))) & "@"
                processed_MBID(x) = SQLreader(1)
                x += 1

            End While
        Catch ex As Exception
            DVDArt_Common.logStats("DVArt: Error in Loading Artist [dvdart database].  Failed with exception: " & ex.Message, "ERROR")
        End Try

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_artist(0)

        Try
            SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("music") & ";Read Only=True;"
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
                    artist(x) = LCase(Utils.MakeFileName(SQLreader(0)))
                    x += 1

                    t_artist = "@" & artist(x - 1) & "@"

                    If processed_artist.Contains(t_artist) Then

                        found = False
                        missing = False

                        For y = 1 To 2
                            fileexist(y) = IO.File.Exists(thumbs & DVDArt_Common.folder(2, y, 1) & artist(x - 1) & ".png")
                            If Not found Then found = checked(2, y) And fileexist(y)
                            If Not missing Then missing = checked(2, y) And Not fileexist(y)
                        Next

                        If found Then
                            li_artist = lv_artist.Items.Add(SQLreader(0))
                            li_artist.SubItems.Add(processed_MBID(Array.IndexOf(processed_artist, t_artist)))
                        End If

                        If missing Then

                            li_missing = lv_music_missing.Items.Add(SQLreader(0))

                            lvi = Nothing

                            For y = 0 To 2

                                If checked(2, y) And y > 0 Then
                                    If fileexist(y) Then
                                        li_missing.SubItems.Add("Yes", Color.White, Color.White, DefaultFont)
                                    Else
                                        li_missing.SubItems.Add("No", Color.White, Color.White, DefaultFont)
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

                            li_missing.SubItems.Add(processed_MBID(Array.IndexOf(processed_artist, t_artist)))
                            li_missing.SubItems.Add("")

                        End If

                    Else
                        li_import = lv_import.Items.Add(SQLreader(0))
                        li_import.SubItems.Add("*** searching MBID ***")
                        li_import.SubItems.Add("Artist")
                        If cb_autoimport.Checked = False Then
                            l_import_queue.Add("|" & SQLreader(0) & "|music")
                            l_import_index.Add(lv_import.Items.Count - 1)
                        End If
                    End If

                End If

            End While
        Catch ex As Exception
            DVDArt_Common.logStats("DVArt: Error in Loading Artist [music database].  Failed with exception: " & ex.Message, "ERROR")
        End Try

        If x = 0 Then ReDim Preserve artist(0)

        SQLconnect.Close()

        Try
            ' remove artists from dvdart that no longer exist in music
            SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
            SQLconnect.Open()
            SQLcommand = SQLconnect.CreateCommand
            SQLcommand.CommandText = "SELECT artist, rowid FROM processed_artist ORDER BY artist"
            SQLreader = SQLcommand.ExecuteReader()

            x = 0

            While SQLreader.Read()

                If Not artist.Contains(LCase(Utils.MakeFileName(SQLreader(0)))) Then
                    SQLdelete.CommandText = "DELETE FROM processed_artist WHERE rowid = " & SQLreader(1)
                    SQLdelete.ExecuteNonQuery()
                End If

            End While
        Catch ex As Exception
            DVDArt_Common.logStats("DVArt: Error in Loading Artist housekeeping].  Failed with exception: " & ex.Message, "ERROR")
        End Try

        SQLconnect.Close()
        SQLconnect.Dispose()

        DVDArt_Common.logStats("DVArt: Loading Artist List complete.", "DEBUG")

    End Sub

    Private Sub LoadAlbumList()

        DVDArt_Common.logStats("DVArt: Loading Album List starting.", "DEBUG")

        'On Error Resume Next

        ' check if dvdart.db3 SQLite database exists and if not, create it together with respective table
        Dim SQLconnect As New SQLiteConnection()
        Dim SQLcommand As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLdelete As SQLiteCommand = SQLconnect.CreateCommand
        Dim SQLreader As SQLiteDataReader
        Dim processed_music() As String = Nothing
        Dim processed_MBID() As String = Nothing
        Dim album() As String = Nothing
        Dim t_album As String = Nothing
        Dim y As Integer = 0
        Dim found, missing As Boolean
        Dim x As Integer = 0

        If Not IO.File.Exists(DVDArt_Common.p_Databases("dvdart")) Then SQLiteConnection.CreateFile(DVDArt_Common.p_Databases("dvdart"))

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand.CommandText = "CREATE TABLE IF NOT EXISTS processed_music(album TEXT, MBID TEXT)"
        SQLcommand.ExecuteNonQuery()

        SQLcommand.CommandText = "DELETE FROM processed_music WHERE rowid NOT IN (SELECT MIN(rowid) FROM processed_music GROUP BY album, MBID)"
        SQLcommand.ExecuteNonQuery()

        SQLcommand.CommandText = "CREATE UNIQUE INDEX IF NOT EXISTS music_index ON processed_music (album, MBID)"
        SQLcommand.ExecuteNonQuery()

        ' Read already processed movies to identify newly imported ones in music

        SQLcommand.CommandText = "SELECT album, MBID FROM processed_music WHERE album is not Null ORDER BY album"
        SQLreader = SQLcommand.ExecuteReader()

        While SQLreader.Read()

            ReDim Preserve processed_music(x)
            ReDim Preserve processed_MBID(x)
            processed_music(x) = "@" & LCase(Utils.MakeFileName(SQLreader(0))) & "@"
            processed_MBID(x) = SQLreader(1)
            x += 1

        End While

        SQLconnect.Close()

        If x = 0 Then ReDim Preserve processed_music(0)

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("music") & ";Read Only=True;"
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

                t_album = LCase(Utils.MakeFileName(SQLreader(0)))

                If x = -1 Then
                    add = True
                Else
                    add = Not album.Contains(t_album)
                End If

                If add Then

                    x += 1
                    ReDim Preserve album(x)
                    album(x) = t_album

                    If processed_music.Contains("@" & t_album & "@") Then

                        found = False
                        missing = False

                        For y = 0 To 0
                            fileexist(y) = FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & t_album & ".png") Or FileExists(thumbs & DVDArt_Common.folder(2, y, 1) & t_album & ".jpg")
                            If Not found Then found = checked(2, y) And fileexist(y)
                            If Not missing Then missing = checked(2, y) And Not fileexist(y)
                        Next

                        If found Then
                            li_album = lv_album.Items.Add(SQLreader(0))
                            li_album.SubItems.Add(processed_MBID(Array.IndexOf(processed_music, "@" & t_album & "@")))
                            li_album.SubItems.Add(Trim(SQLreader(1).Replace("| ", "").Replace(" |", "")))
                        End If

                        If missing Then

                            li_missing = lv_music_missing.Items.Add(SQLreader(0))

                            lvi = Nothing

                            For y = 0 To 2

                                If checked(2, y) And y = 0 Then
                                    If fileexist(y) Then
                                        li_missing.SubItems.Add("Yes", Color.Transparent, Color.Transparent, DefaultFont)
                                    Else
                                        li_missing.SubItems.Add("No", Color.Transparent, Color.Transparent, DefaultFont)
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

                            y = Array.IndexOf(processed_music, "@" & t_album & "@")

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
                        If cb_autoimport.Checked = False Then
                            l_import_queue.Add("|" & SQLreader(0) & "|music")
                            l_import_index.Add(lv_import.Items.Count - 1)
                        End If
                    End If

                End If

            End If

        End While

        If x = -1 Then ReDim Preserve album(0)

        SQLconnect.Close()

        ' remove albums and artists from dvdart that no longer exist in music

        SQLconnect.ConnectionString = "Data Source=" & DVDArt_Common.p_Databases("dvdart")
        SQLconnect.Open()
        SQLcommand = SQLconnect.CreateCommand
        SQLcommand.CommandText = "SELECT album, rowid FROM processed_music ORDER BY album"
        SQLreader = SQLcommand.ExecuteReader()

        x = 0

        While SQLreader.Read()

            If Not album.Contains(LCase(Utils.MakeFileName(SQLreader(0)))) Then
                SQLdelete.CommandText = "DELETE FROM processed_music WHERE rowid = " & SQLreader(1)
                SQLdelete.ExecuteNonQuery()
            End If

        End While

        SQLconnect.Close()
        SQLconnect.Dispose()

        DVDArt_Common.logStats("DVArt: Loading Album List complete.", "DEBUG")

    End Sub

    Private Sub clearLists()

        il_banner.Images.Clear()
        il_clearart.Images.Clear()
        il_clearlogo.Images.Clear()
        il_dvdart.Images.Clear()
        il_backdrop.Images.Clear()
        il_cover.Images.Clear()

        lv_album_cdart.Items.Clear()
        lv_artist_banner.Items.Clear()
        lv_artist_clearlogo.Items.Clear()
        lv_movie_clearart.Items.Clear()
        lv_movie_clearlogo.Items.Clear()
        lv_movie_banner.Items.Clear()
        lv_movie_dvdart.Items.Clear()
        lv_movie_backdrop.Items.Clear()
        lv_movie_cover.Items.Clear()
        lv_serie_clearart.Items.Clear()
        lv_serie_clearlogo.Items.Clear()

        lv_url_clearart.Items.Clear()
        lv_url_clearlogo.Items.Clear()
        lv_url_banner.Items.Clear()
        lv_url_dvdart.Items.Clear()
        lv_url_backdrop.Items.Clear()
        lv_url_cover.Items.Clear()

    End Sub

    Private Sub lv_movies_GotFocus(sender As Object, e As EventArgs) Handles lv_movies.GotFocus, lv_movies_missing.GotFocus
        cms_found.Items.Item(cms_found.Items.IndexOfKey("SelectCoverArtForDVDArt_found")).Visible = True
        cms_found.Items.Item(cms_found.Items.IndexOfKey("ChangeMBID_found")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("SelectCoverArtForDVDArt_missing")).Visible = True
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("UseCoverArtForDVDArt_missing")).Visible = True
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("ChangeMBID_missing")).Visible = False
    End Sub

    Private Sub lv_movies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movies.SelectedIndexChanged

        Try

            If current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text Then Exit Sub

            Dim thumbpath As String = Nothing
            Dim url(5) As String

            clearLists()

            url = {pb_movie_dvdart.Tag, pb_movie_clearart.Tag, pb_movie_clearlogo.Tag, pb_movie_banner.Tag, pb_movie_backdrop.Tag, pb_movie_cover.Tag}

            If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Or url(3) <> Nothing Or url(4) <> Nothing Or url(5) <> Nothing Then
                FTV_api_connector(current_imdb_id, url, "movies", "selected")
            End If

            current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text
            l_imdb_id.Text = current_imdb_id

            For x = 0 To 5

                If checked(0, x) Then

                    If x < 4 Then
                        thumbpath = thumbs & DVDArt_Common.folder(0, x, 1) & current_imdb_id & ".png"
                    ElseIf x = 4 Then
                        thumbpath = lv_movies.FocusedItem.SubItems.Item(2).Text

                        If IO.Path.GetFileNameWithoutExtension(thumbpath) <> current_imdb_id Then
                            Dim path As String = Nothing
                            Try
                                path = IO.Path.GetDirectoryName(thumbpath) & "\"
                            Catch ex As Exception
                                path = thumbs & DVDArt_Common.folder(0, 4, 1)
                            End Try

                            If IO.File.Exists(path & current_imdb_id & ".jpg") Then
                                thumbpath = path & current_imdb_id & ".jpg"
                                lv_movies.FocusedItem.SubItems.Item(2).Text = thumbpath
                            End If
                        End If
                    ElseIf x = 5 Then
                        thumbpath = lv_movies.FocusedItem.SubItems.Item(3).Text

                        If IO.Path.GetFileNameWithoutExtension(thumbpath) <> current_imdb_id Then
                            Dim path As String = Nothing
                            Try
                                path = IO.Path.GetDirectoryName(thumbpath) & "\"
                            Catch ex As Exception
                                path = thumbs & DVDArt_Common.folder(0, 5, 1)
                            End Try

                            If IO.File.Exists(path & current_imdb_id & ".jpg") Then
                                thumbpath = path & current_imdb_id & ".jpg"
                                lv_movies.FocusedItem.SubItems.Item(3).Text = thumbpath
                            End If
                        End If
                    End If

                    If x = 0 Then
                        DVDArt_Common.load_image(pb_movie_dvdart, thumbpath)

                        If pb_movie_dvdart.Image IsNot Nothing Then
                            l_movie_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(0, x, 0) & current_imdb_id & ".png")
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
                        DVDArt_Common.load_image(pb_movie_clearart, thumbpath)
                        b_movie_deleteart.Visible = (pb_movie_clearart.Image IsNot Nothing)
                    ElseIf x = 2 Then
                        DVDArt_Common.load_image(pb_movie_clearlogo, thumbpath)
                        b_movie_deletelogo.Visible = (pb_movie_clearlogo.Image IsNot Nothing)
                    ElseIf x = 3 Then
                        DVDArt_Common.load_image(pb_movie_banner, thumbpath)
                        b_movie_deletebanner.Visible = (pb_movie_banner.Image IsNot Nothing)
                    ElseIf x = 4 Then
                        DVDArt_Common.load_image(pb_movie_backdrop, thumbpath)
                        b_movie_deletebackdrop.Visible = (pb_movie_backdrop.Image IsNot Nothing)
                    ElseIf x = 5 Then
                        DVDArt_Common.load_image(pb_movie_cover, thumbpath)
                        b_movie_deletecover.Visible = (pb_movie_cover.Image IsNot Nothing)
                    End If

                End If

            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lv_person_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_person.SelectedIndexChanged

        Try

            If current_person = lv_person.FocusedItem.SubItems.Item(0).Text Then Exit Sub

            Dim thumbpath As String
            Dim current_image As String = Utils.MakeFileName(lv_person.FocusedItem.SubItems.Item(0).Text)

            current_person = lv_person.FocusedItem.SubItems.Item(0).Text
            thumbpath = personpath & current_image & ".png"

            DVDArt_Common.load_image(pb_person, thumbpath)

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lv_series_GotFocus(sender As Object, e As EventArgs) Handles lv_series.GotFocus, lv_series_missing.GotFocus
        cms_found.Items.Item(cms_found.Items.IndexOfKey("SelectCoverArtForDVDArt_found")).Visible = False
        cms_found.Items.Item(cms_found.Items.IndexOfKey("ChangeMBID_found")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("SelectCoverArtForDVDArt_missing")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("UseCoverArtForDVDArt_missing")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("ChangeMBID_missing")).Visible = False
    End Sub

    Private Sub lv_series_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_series.SelectedIndexChanged

        Try

            If current_thetvdb_id = lv_series.FocusedItem.SubItems.Item(1).Text Then Exit Sub

            Dim thumbpath, url(2) As String

            clearLists()

            url = {Nothing, pb_serie_clearart.Tag, pb_serie_clearlogo.Tag}

            If url(1) <> Nothing Or url(2) <> Nothing Then
                FTV_api_connector(current_thetvdb_id, url, "tv", "selected")
            End If

            current_thetvdb_id = lv_series.FocusedItem.SubItems.Item(1).Text
            l_thetvdb_id.Text = current_thetvdb_id

            For x = 1 To 2

                If checked(1, x) Then
                    thumbpath = thumbs & DVDArt_Common.folder(1, x, 1) & current_thetvdb_id & ".png"
                    If x = 1 Then
                        DVDArt_Common.load_image(pb_serie_clearart, thumbpath)
                        b_serie_deleteart.Visible = (pb_serie_clearart.Image IsNot Nothing)
                    ElseIf x = 2 Then
                        DVDArt_Common.load_image(pb_serie_clearlogo, thumbpath)
                        b_serie_deletelogo.Visible = (pb_serie_clearlogo.Image IsNot Nothing)
                    End If
                End If

            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lv_artist_GotFocus(sender As Object, e As EventArgs) Handles lv_artist.GotFocus, lv_music_missing.GotFocus, lv_album.GotFocus
        cms_found.Items.Item(cms_found.Items.IndexOfKey("SelectCoverArtForDVDArt_found")).Visible = False
        cms_found.Items.Item(cms_found.Items.IndexOfKey("ChangeMBID_found")).Visible = True
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("SelectCoverArtForDVDArt_missing")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("UseCoverArtForDVDArt_missing")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("ChangeMBID_missing")).Visible = True
    End Sub

    Private Sub lv_artist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_artist.SelectedIndexChanged

        Try

            If current_artist = lv_artist.FocusedItem.SubItems.Item(0).Text Then Exit Sub

            Dim thumbpath, url(2) As String
            Dim current_image As String = Utils.MakeFileName(lv_artist.FocusedItem.SubItems.Item(0).Text)

            clearLists()

            url = {Nothing, pb_artist_banner.Tag, pb_artist_clearlogo.Tag}

            If url(1) <> Nothing Or url(2) <> Nothing Then
                FTV_api_connector(current_artist, url, "music", "selected")
            End If

            current_artist = lv_artist.FocusedItem.SubItems.Item(0).Text

            For x = 1 To 2

                If checked(2, x) Then
                    thumbpath = thumbs & DVDArt_Common.folder(2, x, 1) & current_image & ".png"

                    If x = 1 Then
                        DVDArt_Common.load_image(pb_artist_banner, thumbpath)
                        b_artist_deletebanner.Visible = (pb_artist_banner.Image IsNot Nothing)
                    ElseIf x = 2 Then
                        DVDArt_Common.load_image(pb_artist_clearlogo, thumbpath)
                        b_artist_deletelogo.Visible = (pb_artist_clearlogo.Image IsNot Nothing)
                    End If
                End If

            Next

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lv_album_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_album.SelectedIndexChanged

        Try

            If current_album = lv_album.FocusedItem.SubItems.Item(0).Text Then Exit Sub

            Dim thumbpath, url(2) As String
            Dim current_image As String = Utils.MakeFileName(lv_album.FocusedItem.SubItems.Item(0).Text)

            clearLists()

            url = {pb_album_cdart.Tag, Nothing, Nothing}

            If url(0) <> Nothing Then
                FTV_api_connector(current_album, url, "album", "selected")
            End If

            current_album = lv_album.FocusedItem.SubItems.Item(0).Text

            If checked(2, 0) Then
                thumbpath = thumbs & DVDArt_Common.folder(2, 0, 1) & current_image & ".png"
                DVDArt_Common.load_image(pb_album_cdart, thumbpath)

                If pb_album_cdart.Image IsNot Nothing Then
                    l_music_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(2, 0, 0) & current_image & ".png")
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

        Catch ex As Exception
        End Try

    End Sub

    Private Sub lv_movies_missing_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles lv_movies_missing.GotFocus
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("SelectCoverArtForDVDArt_missing")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("UseCoverArtForDVDArt_missing")).Visible = False
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("ChangeMBID_missing")).Visible = True
    End Sub

    Private Sub lv_missing_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lv_movies_missing.ColumnClick, lv_series_missing.ColumnClick, lv_music_missing.ColumnClick

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
                SetSortArrow(lv_movies_missing, e.Column, lvwColumnSorter.Order)
                lv_movies_missing.Sort()
            Case "lv_series_missing"
                SetSortArrow(lv_series_missing, e.Column, lvwColumnSorter.Order)
                lv_series_missing.Sort()
            Case "lv_music_missing"
                SetSortArrow(lv_music_missing, e.Column, lvwColumnSorter.Order)
                lv_music_missing.Sort()
        End Select

    End Sub

    Private Sub lv_movies_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lv_movies.ColumnClick

        lv_movies.ListViewItemSorter = lvwColumnSorter

        If Mid(lv_movies.Columns(0).Text, 18, 5) = "title" Then
            lvwColumnSorter.SortColumn = 4
            lv_movies.Columns(0).Text = "Movie - order by sort name"
        Else
            lvwColumnSorter.SortColumn = 0
            lv_movies.Columns(0).Text = "Movie - order by title"
        End If

        lvwColumnSorter.Order = SortOrder.Ascending
        SetSortArrow(lv_movies, 0, lvwColumnSorter.Order)
        lv_movies.Sort()

    End Sub

    Private Sub lv_series_ColumnClick(sender As Object, e As System.Windows.Forms.ColumnClickEventArgs) Handles lv_series.ColumnClick

        lv_series.ListViewItemSorter = lvwColumnSorter

        If Mid(lv_series.Columns(0).Text, 18, 5) = "title" Then
            lvwColumnSorter.SortColumn = 2
            lv_series.Columns(0).Text = "Serie - order by sort name"
        Else
            lvwColumnSorter.SortColumn = 0
            lv_series.Columns(0).Text = "Serie - order by title"
        End If

        lvwColumnSorter.Order = SortOrder.Ascending
        SetSortArrow(lv_series, 0, lvwColumnSorter.Order)
        lv_series.Sort()

    End Sub

    Private Sub lv_series_missing_GotFocus(ByVal sender As Object, ByVal e As EventArgs) Handles lv_series_missing.GotFocus
        cms_missing.Items.Item(cms_missing.Items.IndexOfKey("SelectCoverArtForDVDArt_missing")).Visible = False
    End Sub

    Private Sub cms_found_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_found.ItemClicked

        If cms_found.SourceControl.Name = "lv_movies" Then

            If lv_movies.SelectedItems.Count > 0 Then
                If e.ClickedItem.Text = "Refresh artwork from online" Then
                    FTV_api_connector(lv_movies.SelectedItems(0).SubItems.Item(1).Text, Nothing, "movies", "preview")
                ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                    Dim title As String = lv_movies.SelectedItems(0).SubItems(0).Text
                    Dim imdb_id As String = lv_movies.SelectedItems(0).SubItems.Item(1).Text
                    Dim upload As New DVDArt_ManualUpload(imdb_id, title, "movies")
                    upload.ShowDialog()
                    current_imdb_id = Nothing
                    lv_movies_SelectedIndexChanged(sender, e)
                ElseIf e.ClickedItem.Text = "Select/Edit Cover Art for DVD Art" Then
                    use_coverart("movies")
                    current_imdb_id = Nothing
                    lv_movies_SelectedIndexChanged(sender, e)
                End If
            Else
                MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
            End If

        ElseIf cms_found.SourceControl.Name = "lv_series" Then

            If lv_series.SelectedItems.Count > 0 Then
                If e.ClickedItem.Text = "Refresh artwork from online" Then
                    FTV_api_connector(lv_series.SelectedItems(0).SubItems.Item(1).Text, Nothing, "tv", "preview")
                ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                    Dim title As String = lv_series.SelectedItems(0).SubItems(0).Text
                    Dim thetvdb_id As String = lv_series.SelectedItems(0).SubItems.Item(1).Text
                    Dim upload As New DVDArt_ManualUpload(thetvdb_id, title, "tv")
                    upload.ShowDialog()
                    current_thetvdb_id = Nothing
                    lv_series_SelectedIndexChanged(sender, e)
                End If
            Else
                MsgBox("Please select a series.", MsgBoxStyle.Critical, Nothing)
            End If

        ElseIf cms_found.SourceControl.Name = "lv_artist" Then

            If lv_artist.SelectedItems.Count > 0 Then
                If e.ClickedItem.Text = "Refresh artwork from online" Then
                    FTV_api_connector(lv_artist.SelectedItems(0).SubItems.Item(1).Text, Nothing, "music", "preview")
                ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                    Dim title As String = lv_artist.SelectedItems(0).SubItems(0).Text
                    Dim upload As New DVDArt_ManualUpload(title, title, "music")
                    upload.ShowDialog()
                    current_artist = Nothing
                    lv_artist_SelectedIndexChanged(sender, e)
                ElseIf e.ClickedItem.Text = "Change MBID" Then
                    Dim MBID As String = lv_artist.SelectedItems(0).SubItems.Item(1).Text
                    Dim change As New DVDArt_ChangeMBID
                    change.ChangeMBID(MBID, lv_artist.SelectedItems(0).SubItems.Item(1).Text)
                    If MBID <> lv_artist.SelectedItems(0).SubItems.Item(1).Text Then
                        changeMBID(MBID, lv_artist.SelectedItems(0).SubItems.Item(1).Text)
                    End If
                End If
            Else
                MsgBox("Please select an artist.", MsgBoxStyle.Critical, Nothing)
            End If

        ElseIf cms_found.SourceControl.Name = "lv_album" Then

            If lv_album.SelectedItems.Count > 0 Then
                If e.ClickedItem.Text = "Refresh artwork from online" Then
                    FTV_api_connector(lv_album.SelectedItems(0).SubItems.Item(1).Text, Nothing, "music/albums|" & lv_album.SelectedItems(0).SubItems.Item(0).Text, "preview")
                ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                    Dim title As String = lv_album.SelectedItems(0).SubItems(0).Text
                    Dim upload As New DVDArt_ManualUpload(title, title, "music/albums")
                    upload.ShowDialog()
                    current_album = Nothing
                    lv_album_SelectedIndexChanged(sender, e)
                ElseIf e.ClickedItem.Text = "Change MBID" Then
                    Dim MBID As String = lv_album.SelectedItems(0).SubItems.Item(1).Text
                    Dim change As New DVDArt_ChangeMBID
                    change.ChangeMBID(MBID, lv_album.SelectedItems(0).SubItems.Item(1).Text)
                    If MBID <> lv_album.SelectedItems(0).SubItems.Item(1).Text Then
                        changeMBID(MBID, lv_album.SelectedItems(0).SubItems.Item(1).Text)
                    End If
                End If
            Else
                MsgBox("Please select an album.", MsgBoxStyle.Critical, Nothing)
            End If

        End If

    End Sub

    Private Sub cms_missing_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_missing.ItemClicked

        If cms_missing.SourceControl.Name = "lv_movies_missing" Then

            If e.ClickedItem.Text = "Send to importer" Then
                If lv_movies_missing.SelectedItems.Count > 0 Then
                    For x As Integer = 0 To (lv_movies_missing.SelectedItems.Count - 1)
                        li_import = lv_import.Items.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text)
                        li_import.SubItems.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text)
                        li_import.SubItems.Add("Movie")
                        l_import_queue.Add(lv_movies_missing.SelectedItems(x).SubItems.Item(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text & "|" & lv_movies_missing.SelectedItems(x).SubItems.Item(0).Text & "|movies")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    Next

                    For x = (lv_movies_missing.SelectedIndices.Count - 1) To 0 Step -1
                        lv_movies_missing.Items.RemoveAt(lv_movies_missing.SelectedIndices(x))
                    Next

                    If Not bw_import.IsBusy Then
                        FTV_api_connector("queue", Nothing, "movies", "import")
                    End If
                Else
                    MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
                Do While lv_movies_missing.Items.Count > 0
                    li_import = lv_import.Items.Add(lv_movies_missing.Items.Item(0).SubItems(0).Text)
                    li_import.SubItems.Add(lv_movies_missing.Items.Item(0).SubItems(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text)
                    li_import.SubItems.Add("Movie")
                    l_import_queue.Add(lv_movies_missing.Items.Item(0).SubItems(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text & "|" & lv_movies_missing.Items.Item(0).SubItems(0).Text & "|movies")
                    l_import_index.Add(lv_import.Items.Count - 1)
                    lv_movies_missing.Items.RemoveAt(0)
                Loop

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "movies", "import")
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                For x As Integer = 0 To (lv_movies_missing.SelectedItems.Count - 1)

                    If lv_movies_missing.SelectedItems.Count > 0 Then
                        Dim title As String = lv_movies_missing.SelectedItems(x).SubItems(0).Text
                        Dim imdb_id As String = lv_movies_missing.SelectedItems(x).SubItems.Item(lv_movies_missing.Columns.IndexOf(m_IMDb_id)).Text
                        Dim upload As New DVDArt_ManualUpload(imdb_id, title, "movies")
                        upload.ShowDialog()

                        If FileExists(thumbs & DVDArt_Common.folder(0, 0, 1) & imdb_id & ".png") Or
                           FileExists(thumbs & DVDArt_Common.folder(0, 1, 1) & imdb_id & ".png") Or
                           FileExists(thumbs & DVDArt_Common.folder(0, 2, 1) & imdb_id & ".png") Then

                            li_import = lv_import.Items.Add(title)
                            li_import.SubItems.Add(imdb_id)
                            li_import.SubItems.Add("Movie")
                            l_import_queue.Add(imdb_id & "|" & title & "|movies")
                            l_import_index.Add(lv_import.Items.Count - 1)

                            If lv_movies.FindItemWithText(title) Is Nothing Then
                                li_movies = lv_movies.Items.Add(title)
                                li_movies.SubItems.Add(imdb_id)
                                li_movies.SubItems.Add("Movie")
                            End If

                        End If

                        lv_movies_missing.Items.Remove(lv_movies_missing.SelectedItems(x))
                        x -= 1
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
                        l_import_queue.Add(lv_series_missing.SelectedItems(x).SubItems.Item(3).Text & "|" & lv_series_missing.SelectedItems(x).SubItems.Item(0).Text & "|tv")
                        l_import_index.Add(lv_import.Items.Count - 1)
                    Next

                    For x = (lv_series_missing.SelectedIndices.Count - 1) To 0 Step -1
                        lv_series_missing.Items.RemoveAt(lv_series_missing.SelectedIndices(x))
                    Next

                    If Not bw_import.IsBusy Then
                        FTV_api_connector("queue", Nothing, "tv", "import")
                    End If
                Else
                    MsgBox("Please select a series.", MsgBoxStyle.Critical, Nothing)
                End If
            ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
                Do While lv_series_missing.Items.Count > 0
                    li_import = lv_import.Items.Add(lv_series_missing.Items.Item(0).SubItems(0).Text)
                    li_import.SubItems.Add(lv_series_missing.Items.Item(0).SubItems(3).Text)
                    li_import.SubItems.Add("Series")
                    l_import_queue.Add(lv_series_missing.Items.Item(0).SubItems(3).Text & "|" & lv_series_missing.Items.Item(0).SubItems(0).Text & "|tv")
                    l_import_index.Add(lv_import.Items.Count - 1)
                    lv_series_missing.Items.RemoveAt(0)
                Loop

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "tv", "import")
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                For x As Integer = 0 To (lv_series_missing.SelectedItems.Count - 1)

                    If lv_series_missing.SelectedItems.Count > 0 Then
                        Dim title As String = lv_series_missing.SelectedItems(x).SubItems(0).Text
                        Dim thetvdb_id As String = lv_series_missing.SelectedItems(x).SubItems.Item(3).Text
                        Dim upload As New DVDArt_ManualUpload(thetvdb_id, title, "tv")
                        upload.ShowDialog()

                        If FileExists(thumbs & DVDArt_Common.folder(1, 1, 1) & thetvdb_id & ".png") Or
                           FileExists(thumbs & DVDArt_Common.folder(1, 2, 1) & thetvdb_id & ".png") Then

                            li_import = lv_import.Items.Add(title)
                            li_import.SubItems.Add(thetvdb_id)
                            li_import.SubItems.Add("Series")
                            l_import_queue.Add(thetvdb_id & "|" & title & "|tv")
                            l_import_index.Add(lv_import.Items.Count - 1)

                            If lv_series.FindItemWithText(title) Is Nothing Then
                                li_series = lv_series.Items.Add(title)
                                li_series.SubItems.Add(thetvdb_id)
                            End If

                        End If

                        lv_series_missing.Items.Remove(lv_series_missing.SelectedItems(x))
                        x -= 1
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
                            type = "music/albums"
                        Else
                            type = "music"
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
                Do While lv_music_missing.Items.Count > 0
                    li_import = lv_import.Items.Add(lv_music_missing.Items.Item(0).SubItems(0).Text)
                    li_import.SubItems.Add(lv_music_missing.Items.Item(0).SubItems(4).Text)
                    li_import.SubItems.Add("Music")

                    If lv_music_missing.Items.Item(0).SubItems.Item(1).Text <> "" Then
                        l_import_queue.Add(lv_music_missing.Items.Item(0).SubItems.Item(4).Text & "|" & lv_music_missing.Items.Item(0).SubItems.Item(0).Text & "|music/albums|" & lv_music_missing.Items.Item(0).SubItems.Item(4).Text)
                    Else
                        l_import_queue.Add(lv_music_missing.Items.Item(0).SubItems.Item(4).Text & "|" & lv_music_missing.Items.Item(0).SubItems.Item(0).Text & "|music")
                    End If

                    l_import_index.Add(lv_import.Items.Count - 1)
                    lv_music_missing.Items.RemoveAt(0)
                Loop

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "music/albums", "import")
                End If
            ElseIf e.ClickedItem.Text = "Manually Upload Artwork" Then
                For x As Integer = 0 To (lv_music_missing.SelectedItems.Count - 1)

                    If lv_music_missing.SelectedItems.Count > 0 Then
                        Dim title As String = lv_music_missing.SelectedItems(x).SubItems(0).Text

                        If lv_music_missing.SelectedItems(x).SubItems.Item(1).Text <> "" Then
                            Dim upload As New DVDArt_ManualUpload(title, title, "music/albums")
                            upload.ShowDialog()
                        Else
                            Dim upload As New DVDArt_ManualUpload(title, title, "music")
                            upload.ShowDialog()
                        End If

                        If FileExists(thumbs & DVDArt_Common.folder(2, 0, 1) & title & ".png") Or
                           FileExists(thumbs & DVDArt_Common.folder(2, 1, 1) & title & ".png") Or
                           FileExists(thumbs & DVDArt_Common.folder(2, 2, 1) & title & ".png") Then

                            Dim mbid As String = lv_music_missing.SelectedItems(x).SubItems(4).Text

                            li_import = lv_import.Items.Add(title)
                            li_import.SubItems.Add(title)
                            li_import.SubItems.Add("Music")
                            l_import_queue.Add(title & "|" & title & "|music")
                            l_import_index.Add(lv_import.Items.Count - 1)

                            If lv_music_missing.Items.Item(x).SubItems.Item(1).Text <> " " Then
                                If lv_album.FindItemWithText(title) Is Nothing Then
                                    li_album = lv_album.Items.Add(title)
                                    li_album.SubItems.Add(mbid)
                                End If
                            Else
                                If lv_artist.FindItemWithText(title) Is Nothing Then
                                    li_artist = lv_artist.Items.Add(title)
                                    li_artist.SubItems.Add(mbid)
                                End If
                            End If

                        End If

                        lv_music_missing.Items.Remove(lv_music_missing.SelectedItems(x))
                        x -= 1
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

    Private Sub cms_person_found_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_person_found.ItemClicked

        If lv_person.SelectedItems.Count > 0 Then
            If e.ClickedItem.Text = "Refresh person image from online" Then
                DVDArt_Common.get_Person_image(lv_person.SelectedItems(0).SubItems.Item(0).Text, personpath & Utils.MakeFileName(lv_person.SelectedItems(0).SubItems.Item(0).Text) & ".png", pb_person)
            ElseIf e.ClickedItem.Text = "Manually Upload person image" Then
                For x = 0 To (lv_person.SelectedItems.Count - 1)
                    Dim person As String = lv_person.SelectedItems(x).SubItems(0).Text
                    Dim upload As New DVDArt_ManualUpload(Utils.MakeFileName(person), person, "person")
                    upload.ShowDialog()
                    current_person = String.Empty
                    lv_person_SelectedIndexChanged(sender, e)
                Next
            ElseIf e.ClickedItem.Text = "Delete person image" Then
                IO.File.Delete(personpath & Utils.MakeFileName(lv_person.SelectedItems(0).SubItems.Item(0).Text) & ".png")
            End If
        Else
            MsgBox("Please select a person.", MsgBoxStyle.Critical, Nothing)
        End If

    End Sub

    Private Sub cms_person_missing_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_person_missing.ItemClicked

        If lv_persons_missing.SelectedItems.Count > 0 Then

            If e.ClickedItem.Text = "Manually Upload person image" Then
                For x = 0 To (lv_persons_missing.SelectedItems.Count - 1)
                    Dim person As String = lv_persons_missing.SelectedItems(x).SubItems(0).Text
                    Dim upload As New DVDArt_ManualUpload(Utils.MakeFileName(person), person, "person")
                    upload.ShowDialog()

                    If IO.File.Exists(personpath & Utils.MakeFileName(person) & ".png") Then
                        li_import = lv_import.Items.Add(person)
                        li_import.SubItems.Add(String.Empty)
                        li_import.SubItems.Add("Person")
                        l_import_queue.Add("|" & person & "|person")
                        l_import_index.Add(lv_import.Items.Count - 1)

                        If lv_person.FindItemWithText(person) Is Nothing Then li_person = lv_person.Items.Add(person)
                    End If
                Next

                For x = (lv_persons_missing.SelectedIndices.Count - 1) To 0 Step -1
                    lv_persons_missing.Items.RemoveAt(lv_persons_missing.SelectedIndices(x))
                Next

            ElseIf e.ClickedItem.Text = "Rescan ALL missing" Then
                Do While lv_persons_missing.Items.Count > 0
                    li_import = lv_import.Items.Add(lv_persons_missing.Items.Item(0).SubItems(0).Text)
                    li_import.SubItems.Add(String.Empty)
                    li_import.SubItems.Add("Person")
                    l_import_queue.Add("|" & lv_persons_missing.Items.Item(0).SubItems(0).Text & "|person")
                    l_import_index.Add(lv_import.Items.Count - 1)
                    lv_persons_missing.Items.RemoveAt(0)
                Loop

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "person", "import")
                End If

            ElseIf e.ClickedItem.Text = "Search person image on-line" Then
                For x = 0 To (lv_persons_missing.SelectedItems.Count - 1)
                    li_import = lv_import.Items.Add(lv_persons_missing.SelectedItems(x).SubItems.Item(0).Text)
                    li_import.SubItems.Add(String.Empty)
                    li_import.SubItems.Add("Person")
                    l_import_queue.Add("|" & lv_persons_missing.SelectedItems(x).SubItems.Item(0).Text & "|person")
                    l_import_index.Add(lv_import.Items.Count - 1)
                Next

                For x = (lv_persons_missing.SelectedIndices.Count - 1) To 0 Step -1
                    lv_persons_missing.Items.RemoveAt(lv_persons_missing.SelectedIndices(x))
                Next

                If Not bw_import.IsBusy Then
                    FTV_api_connector("queue", Nothing, "movies", "import")
                End If
            End If

        Else
            MsgBox("Please select a person.", MsgBoxStyle.Critical, Nothing)
        End If

    End Sub

    Private Sub lv_movie_dvdart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movie_dvdart.SelectedIndexChanged

        For Each item In lv_movie_dvdart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_dvdart.Images(itemkey)

            pb_movie_dvdart.Image = image
            pb_movie_dvdart.Tag = lv_url_dvdart.Items(item.index).Text

        Next

        tbc_main.Tag = "movies"

    End Sub

    Private Sub lv_movie_clearart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movie_clearart.SelectedIndexChanged

        For Each item In lv_movie_clearart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearart.Images(itemkey)

            pb_movie_clearart.Image = image
            pb_movie_clearart.Tag = lv_url_clearart.Items(item.index).Text

        Next

        tbc_main.Tag = "movies"

    End Sub

    Private Sub lv_movie_clearlogo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movie_clearlogo.SelectedIndexChanged

        For Each item In lv_movie_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_movie_clearlogo.Image = image
            pb_movie_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

        tbc_main.Tag = "movies"

    End Sub

    Private Sub lv_movie_banner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movie_banner.SelectedIndexChanged

        For Each item In lv_movie_banner.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_banner.Images(itemkey)

            pb_movie_banner.Image = image
            pb_movie_banner.Tag = lv_url_banner.Items(item.index).Text

        Next

        tbc_main.Tag = "movies"

    End Sub

    Private Sub lv_movie_backdrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movie_backdrop.SelectedIndexChanged

        For Each item In lv_movie_backdrop.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_backdrop.Images(itemkey)

            pb_movie_backdrop.Image = image
            pb_movie_backdrop.Tag = lv_url_backdrop.Items(item.index).Text

        Next

        tbc_main.Tag = "movies"

    End Sub

    Private Sub lv_movie_cover_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_movie_cover.SelectedIndexChanged

        For Each item In lv_movie_cover.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_cover.Images(itemkey)

            pb_movie_cover.Image = image
            pb_movie_cover.Tag = lv_url_cover.Items(item.index).Text

        Next

        tbc_main.Tag = "movies"

    End Sub

    Private Sub lv_serie_clearart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_serie_clearart.SelectedIndexChanged

        For Each item In lv_serie_clearart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearart.Images(itemkey)

            pb_serie_clearart.Image = image
            pb_serie_clearart.Tag = lv_url_clearart.Items(item.index).Text

        Next

        tbc_main.Tag = "tv"

    End Sub

    Private Sub lv_serie_clearlogo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_serie_clearlogo.SelectedIndexChanged

        For Each item In lv_serie_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_serie_clearlogo.Image = image
            pb_serie_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

        tbc_main.Tag = "tv"

    End Sub

    Private Sub lv_artist_banner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_artist_banner.SelectedIndexChanged

        For Each item In lv_artist_banner.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_banner.Images(itemkey)

            pb_artist_banner.Image = image
            pb_artist_banner.Tag = lv_url_clearart.Items(item.index).Text

        Next

        tbc_main.Tag = "music"

    End Sub

    Private Sub lv_artist_clearlogo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_artist_clearlogo.SelectedIndexChanged

        For Each item In lv_artist_clearlogo.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_clearlogo.Images(itemkey)

            pb_artist_clearlogo.Image = image
            pb_artist_clearlogo.Tag = lv_url_clearlogo.Items(item.index).Text

        Next

        tbc_main.Tag = "music"

    End Sub

    Private Sub lv_album_cdart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_album_cdart.SelectedIndexChanged

        For Each item In lv_album_cdart.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_dvdart.Images(itemkey)

            pb_album_cdart.Image = image
            pb_album_cdart.Tag = lv_url_dvdart.Items(item.index).Text

        Next

        tbc_main.Tag = "album"

    End Sub

    Private Sub setSettings()

        On Error Resume Next

        Using XMLwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            XMLwriter.SetValue("Plugin", "version", DVDArt_Common._version)
            XMLwriter.SetValueAsBool("Settings", "auto import", cb_autoimport.Checked)
            XMLwriter.SetValueAsBool("Settings", "limit downloads", cb_downloads.Checked)
            XMLwriter.SetValue("Settings", "downloads limit", nud_downloads.Value)
            XMLwriter.SetValue("Settings", "delay", nud_delay.Value)
            XMLwriter.SetValue("Settings", "delay value", cb_delay.Text)
            XMLwriter.SetValueAsBool("Settings", "backgroundscraper", cb_backgroundscraper.Checked)
            XMLwriter.SetValue("Settings", "CPU utilisation", mtb_cpu.Text)
            XMLwriter.SetValue("Settings", "scraping", nud_scraping.Value)
            XMLwriter.SetValue("Settings", "scraping value", cb_scraping.Text)
            XMLwriter.SetValue("Settings", "missing", nud_missing.Value)
            XMLwriter.SetValue("Settings", "missing value", cb_missing.Text)
            XMLwriter.SetValueAsBool("Settings", "debug", cb_debug.Checked)
            XMLwriter.SetValue("Settings", "personal API key", tb_personalapikey.Text)
            XMLwriter.SetValue("Scraper", "language", DVDArt_Common.langcode(Array.IndexOf(DVDArt_Common.lang, cb_language.Text)))
            XMLwriter.SetValueAsBool("Scraper Movies", "dvdart", cb_DVDArt_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "clearart", cb_ClearArt_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "clearlogo", cb_ClearLogo_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "backdrop", cb_Backdrop_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "cover", cb_Cover_movies.Checked)
            XMLwriter.SetValueAsBool("Scraper Movies", "banner", cb_Banner_movies.Checked)
            XMLwriter.SetValue("Scraper Movies", "template", template_type)
            XMLwriter.SetValue("Scraper Movies", "path", tb_movie_path.Text)
            XMLwriter.SetValueAsBool("Scraper Movies", "person", cb_persons.Checked)
            XMLwriter.SetValue("Scraper Movies", "person path", tb_person_path.Text)
            XMLwriter.SetValueAsBool("Scraper Series", "clearart", cb_ClearArt_series.Checked)
            XMLwriter.SetValueAsBool("Scraper Series", "clearlogo", cb_ClearLogo_series.Checked)
            XMLwriter.SetValue("Scraper Series", "path", tb_series_path.Text)
            XMLwriter.SetValueAsBool("Scraper Music", "cdart", cb_CDArt_music.Checked)
            XMLwriter.SetValueAsBool("Scraper Music", "banner", cb_Banner_artist.Checked)
            XMLwriter.SetValueAsBool("Scraper Music", "clearlogo", cb_ClearLogo_artist.Checked)
            XMLwriter.SetValue("Scraper Music", "path", tb_music_path.Text)

            If _lastrun = Nothing Then XMLwriter.SetValue("Scheduler", "lastrun", Now)

        End Using

        MediaPortal.Profile.Settings.SaveCache()

    End Sub

    Private Sub getSettings()

        Using XMLreader As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            xml_version = XMLreader.GetValueAsString("Plugin", "version", "0")
            cb_autoimport.Checked = XMLreader.GetValueAsBool("Settings", "auto import", True)
            cb_downloads.Checked = XMLreader.GetValueAsBool("Settings", "limit downloads", False)
            nud_downloads.Value = XMLreader.GetValueAsInt("Settings", "downloads limit", 99)
            nud_delay.Value = XMLreader.GetValueAsInt("Settings", "delay", 1)
            cb_delay.Text = XMLreader.GetValueAsString("Settings", "delay value", "minutes")
            cb_backgroundscraper.Checked = XMLreader.GetValueAsBool("Settings", "backgroundscraper", True)
            mtb_cpu.Text = XMLreader.GetValueAsString("Settings", "CPU utilisation", 30)
            nud_scraping.Value = XMLreader.GetValueAsInt("Settings", "scraping", 15)
            cb_scraping.Text = XMLreader.GetValueAsString("Settings", "scraping value", "minutes")
            nud_missing.Value = XMLreader.GetValueAsInt("Settings", "missing", 0)
            cb_missing.Text = XMLreader.GetValueAsString("Settings", "missing value", "disabled")
            cb_debug.Checked = XMLreader.GetValueAsBool("Settings", "debug", False)
            tb_personalapikey.Text = XMLreader.GetValueAsString("Settings", "personal API key", Nothing)
            _lang = XMLreader.GetValueAsString("Scraper", "language", "##")
            template_type = XMLreader.GetValueAsInt("Scraper Movies", "template", 1)
            cb_DVDArt_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "dvdart", False)
            cb_ClearArt_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "clearart", False)
            cb_ClearLogo_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "clearlogo", False)
            cb_Backdrop_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "backdrop", False)
            cb_Cover_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "cover", False)
            cb_Banner_movies.Checked = XMLreader.GetValueAsBool("Scraper Movies", "banner", False)
            tb_movie_path.Text = XMLreader.GetValueAsString("Scraper Movies", "path", thumbs & "\MovingPictures")
            cb_persons.Checked = XMLreader.GetValueAsBool("Scraper Movies", "person", False)
            tb_person_path.Text = XMLreader.GetValueAsString("Scraper Movies", "person path", thumbs & "\Actors")
            cb_ClearArt_series.Checked = XMLreader.GetValueAsBool("Scraper Series", "clearart", False)
            cb_ClearLogo_series.Checked = XMLreader.GetValueAsBool("Scraper Series", "clearlogo", False)
            tb_series_path.Text = XMLreader.GetValueAsString("Scraper Series", "path", thumbs & "\TVSeries")
            cb_CDArt_music.Checked = XMLreader.GetValueAsBool("Scraper Music", "cdart", False)
            cb_Banner_artist.Checked = XMLreader.GetValueAsBool("Scraper Music", "banner", False)
            cb_ClearLogo_artist.Checked = XMLreader.GetValueAsBool("Scraper Music", "clearlogo", False)
            tb_music_path.Text = XMLreader.GetValueAsString("Scraper Music", "path", thumbs & "\Music")

            _lastrun = XMLreader.GetValueAsString("Scheduler", "lastrun", Nothing)

        End Using

        rb_t1.Checked = (template_type = 1)
        rb_t2.Checked = (template_type = 2)

        cb_backgroundscraper_CheckedChanged(Nothing, Nothing)

        b_import.Visible = Not cb_autoimport.Checked
        l_downloads.Visible = cb_downloads.Checked
        nud_downloads.Visible = cb_downloads.Checked

        personpath = tb_person_path.Text & "\"
        personchecked = cb_persons.Checked

    End Sub

    Private Sub b_save_Click(sender As Object, e As EventArgs) Handles b_save1.Click, b_save2.Click
        setSettings()
    End Sub

    Private Sub cb_autoimport_CheckedChanged(sender As Object, e As EventArgs) Handles cb_autoimport.CheckedChanged

        If cb_autoimport.Checked Then
            b_import_Click(Nothing, Nothing)
            lv_import.Height = 681
        Else
            t_import_timer.Stop()
            lv_import.Height = 656
        End If

        b_import.Enabled = Not cb_autoimport.Checked
        b_import.Visible = Not cb_autoimport.Checked

    End Sub

    Private Sub cb_downloads_CheckedChanged(sender As Object, e As EventArgs) Handles cb_downloads.CheckedChanged

        l_downloads.Visible = cb_downloads.Checked
        nud_downloads.Visible = cb_downloads.Checked

    End Sub

    Private Sub cb_backgroundscraper_CheckedChanged(sender As Object, e As EventArgs) Handles cb_backgroundscraper.CheckedChanged

        cb_delay.Enabled = cb_backgroundscraper.Checked
        nud_delay.Enabled = cb_backgroundscraper.Checked
        mtb_cpu.Enabled = cb_backgroundscraper.Checked
        nud_scraping.Enabled = cb_backgroundscraper.Checked
        cb_scraping.Enabled = cb_backgroundscraper.Checked
        nud_missing.Enabled = cb_backgroundscraper.Checked
        cb_missing.Enabled = cb_backgroundscraper.Checked

    End Sub

    Private Sub cb_DVDArt_movies_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cb_DVDArt_movies.CheckedChanged

        checked(0, 0) = cb_DVDArt_movies.Checked

        If cb_DVDArt_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
            If tbc_movies.TabCount = 0 Then tbc_movies.TabPages.Add(tp_Movie_DVDArt) Else tbc_movies.TabPages.Insert(0, tp_Movie_DVDArt)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_DVDArt)
        End If

    End Sub

    Private Sub cb_ClearArt_movies_CheckedChanged(sender As Object, e As EventArgs) Handles cb_ClearArt_movies.CheckedChanged

        checked(0, 1) = cb_ClearArt_movies.Checked

        If cb_ClearArt_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_ClearArt) Then tbc_movies.TabPages.Remove(tp_Movie_ClearArt)

            If tbc_movies.TabCount > 0 Then
                If tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then
                    tbc_movies.TabPages.Insert(1, tp_Movie_ClearArt)
                ElseIf Not tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then
                    tbc_movies.TabPages.Insert(0, tp_Movie_ClearArt)
                End If
            Else
                tbc_movies.TabPages.Add(tp_Movie_ClearArt)
            End If
        Else
            tbc_movies.TabPages.Remove(tp_Movie_ClearArt)
        End If

    End Sub

    Private Sub cb_ClearLogo_movies_CheckedChanged(sender As Object, e As EventArgs) Handles cb_ClearLogo_movies.CheckedChanged

        checked(0, 2) = cb_ClearLogo_movies.Checked

        If cb_ClearLogo_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)

            If tbc_movies.TabCount > 0 Then
                If Not tbc_movies.TabPages.Contains(tp_Movie_DVDArt) And Not tbc_movies.TabPages.Contains(tp_Movie_ClearArt) Then
                    tbc_movies.TabPages.Insert(0, tp_Movie_ClearLogo)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_ClearArt) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_ClearArt) + 1, tp_Movie_ClearLogo)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_DVDArt) + 1, tp_Movie_ClearLogo)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_Banner) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_Banner), tp_Movie_ClearLogo)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_Backdrop) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_Backdrop), tp_Movie_ClearLogo)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_Cover) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_Cover), tp_Movie_ClearLogo)
                End If
            Else
                tbc_movies.TabPages.Add(tp_Movie_ClearLogo)
            End If
        Else
            tbc_movies.TabPages.Remove(tp_Movie_ClearLogo)
        End If

    End Sub

    Private Sub cb_Banner_movies_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cb_Banner_movies.CheckedChanged

        checked(0, 3) = cb_Banner_movies.Checked

        If cb_Banner_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_Banner) Then tbc_movies.TabPages.Remove(tp_Movie_Banner)

            If tbc_movies.TabCount > 0 Then

                If Not tbc_movies.TabPages.Contains(tp_Movie_DVDArt) And Not tbc_movies.TabPages.Contains(tp_Movie_ClearArt) And Not tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then
                    tbc_movies.TabPages.Insert(0, tp_Movie_Banner)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_Backdrop) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_Backdrop), tp_Movie_Banner)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_Cover) And Not tbc_movies.TabPages.Contains(tp_Movie_Backdrop) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_Cover), tp_Movie_Banner)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_ClearLogo) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_ClearLogo) + 1, tp_Movie_Banner)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_ClearArt) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_ClearArt) + 1, tp_Movie_Banner)
                ElseIf tbc_movies.TabPages.Contains(tp_Movie_DVDArt) Then
                    tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_DVDArt) + 1, tp_Movie_Banner)
                End If
            Else
                tbc_movies.TabPages.Add(tp_Movie_Banner)
            End If
        Else
            tbc_movies.TabPages.Remove(tp_Movie_Banner)
        End If

    End Sub

    Private Sub cb_Backdrop_movies_CheckedChanged(sender As Object, e As EventArgs) Handles cb_Backdrop_movies.CheckedChanged

        checked(0, 4) = cb_Backdrop_movies.Checked

        If cb_Backdrop_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_Backdrop) Then tbc_movies.TabPages.Remove(tp_Movie_Backdrop)

            If tbc_movies.TabCount > 0 Then
                If tbc_movies.TabPages.Contains(tp_Movie_Cover) Then tbc_movies.TabPages.Insert(tbc_movies.TabPages.IndexOf(tp_Movie_Cover), tp_Movie_Backdrop) Else tbc_movies.TabPages.Add(tp_Movie_Backdrop)
            Else
                tbc_movies.TabPages.Add(tp_Movie_Backdrop)
            End If
        Else
            tbc_movies.TabPages.Remove(tp_Movie_Backdrop)
        End If

    End Sub

    Private Sub cb_Cover_movies_CheckedChanged(sender As Object, e As EventArgs) Handles cb_Cover_movies.CheckedChanged

        checked(0, 5) = cb_Cover_movies.Checked

        If cb_Cover_movies.Checked = True Then
            If tbc_movies.TabPages.Contains(tp_Movie_Cover) Then tbc_movies.TabPages.Remove(tp_Movie_Cover)
            tbc_movies.TabPages.Add(tp_Movie_Cover)
        Else
            tbc_movies.TabPages.Remove(tp_Movie_Cover)
        End If

    End Sub

    Private Sub cb_ClearArt_series_CheckedChanged(sender As Object, e As EventArgs) Handles cb_ClearArt_series.CheckedChanged

        checked(1, 1) = cb_ClearArt_series.Checked

        If cb_ClearArt_series.Checked = True Then
            If tbc_series.TabPages.Contains(tp_Serie_ClearArt) Then tbc_series.TabPages.Remove(tp_Serie_ClearArt)
            If tbc_series.TabCount = 0 Then tbc_series.TabPages.Add(tp_Serie_ClearArt) Else tbc_series.TabPages.Insert(0, tp_Serie_ClearArt)
        Else
            tbc_series.TabPages.Remove(tp_Serie_ClearArt)
        End If

    End Sub

    Private Sub cb_ClearLogo_series_CheckedChanged(sender As Object, e As EventArgs) Handles cb_ClearLogo_series.CheckedChanged

        checked(1, 2) = cb_ClearLogo_series.Checked

        If cb_ClearLogo_series.Checked = True Then
            If tbc_series.TabPages.Contains(tp_Serie_ClearLogo) Then tbc_series.TabPages.Remove(tp_Serie_ClearLogo)
            tbc_series.TabPages.Add(tp_Serie_ClearLogo)
        Else
            tbc_series.TabPages.Remove(tp_Serie_ClearLogo)
        End If

    End Sub

    Private Sub cb_CDArt_music_CheckedChanged(sender As Object, e As EventArgs) Handles cb_CDArt_music.CheckedChanged

        checked(2, 0) = cb_CDArt_music.Checked

        If cb_CDArt_music.Checked = True Then
            If Not tbc_music.TabPages.Contains(tp_albums) Then
                If tbc_music.TabPages.Contains(tp_artists) Then tbc_music.TabPages.Insert(1, tp_albums) Else tbc_music.TabPages.Insert(0, tp_albums)
            End If
            If tbc_album.TabPages.Contains(tp_Music_CDArt) Then tbc_album.TabPages.Remove(tp_Music_CDArt)
            tbc_album.TabPages.Add(tp_Music_CDArt)
        Else
            tbc_album.TabPages.Remove(tp_Music_CDArt)
            tbc_music.TabPages.Remove(tp_albums)
        End If

        setMainTabPages("Music")

    End Sub

    Private Sub cb_Banner_artist_CheckedChanged(sender As Object, e As EventArgs) Handles cb_Banner_artist.CheckedChanged

        checked(2, 1) = cb_Banner_artist.Checked

        If cb_Banner_artist.Checked = True Then
            If Not tbc_music.TabPages.Contains(tp_artists) Then tbc_music.TabPages.Insert(0, tp_artists)
            If tbc_artist.TabPages.Contains(tp_artist_banner) Then tbc_artist.TabPages.Remove(tp_artist_banner)
            If tbc_artist.TabCount = 0 Then tbc_artist.TabPages.Add(tp_artist_banner) Else tbc_artist.TabPages.Insert(0, tp_artist_banner)
        Else
            tbc_artist.TabPages.Remove(tp_artist_banner)
            If Not checked(2, 1) And Not checked(2, 2) Then tbc_music.TabPages.Remove(tp_artists)
        End If

        setMainTabPages("Music")

    End Sub

    Private Sub cb_ClearLogo_artist_CheckedChanged(sender As Object, e As EventArgs) Handles cb_ClearLogo_artist.CheckedChanged

        checked(2, 2) = cb_ClearLogo_artist.Checked

        If cb_ClearLogo_artist.Checked = True Then
            If Not tbc_music.TabPages.Contains(tp_artists) Then tbc_music.TabPages.Insert(0, tp_artists)
            If tbc_artist.TabPages.Contains(tp_artist_clearlogo) Then tbc_artist.TabPages.Remove(tp_artist_clearlogo)
            tbc_artist.TabPages.Add(tp_artist_clearlogo)
        Else
            tbc_artist.TabPages.Remove(tp_artist_clearlogo)
            If Not checked(2, 1) And Not checked(2, 2) Then tbc_music.TabPages.Remove(tp_artists)
        End If

        setMainTabPages("Music")

    End Sub

    Private Sub rb_t1_CheckedChanged(sender As Object, e As EventArgs) Handles rb_t1.CheckedChanged
        rb_t1.Image = My.Resources.template_1
        rb_t2.Image = My.Resources.template_2_disabled
        template_type = 1
    End Sub

    Private Sub rb_t2_CheckedChanged(sender As Object, e As EventArgs) Handles rb_t2.CheckedChanged
        rb_t1.Image = My.Resources.template_1_disabled
        rb_t2.Image = My.Resources.template_2
        template_type = 2
    End Sub

    Private Sub b_import_Click(sender As Object, e As EventArgs) Handles b_import.Click
        'initialize importer timer
        t_import_timer.Interval = 5000
        t_import_timer.Start()
        b_import.Enabled = False
    End Sub

    Private Sub b_movie_compress_Click(sender As Object, e As EventArgs) Handles b_movie_compress.Click

        DVDArt_Common.Resize(thumbs & DVDArt_Common.folder(0, 0, 0) & current_imdb_id & ".png")

        l_movie_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(0, 0, 0) & current_imdb_id & ".png")

        If l_movie_size.Text = "500x500" Then b_movie_compress.Visible = False Else b_movie_compress.Visible = True

        b_movie_preview.Visible = Not b_movie_compress.Visible

    End Sub

    Private Sub b_movie_deleteart_Click(sender As Object, e As EventArgs) Handles b_movie_deleteart.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 1, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 1, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 1, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 1, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_movie_clearart.Image = Nothing
            pb_movie_clearart.Tag = Nothing
            b_movie_deleteart.Visible = False
        End If
    End Sub

    Private Sub b_movie_deletelogo_Click(sender As Object, e As EventArgs) Handles b_movie_deletelogo.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 2, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 2, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 2, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 2, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_movie_clearlogo.Image = Nothing
            pb_movie_clearlogo.Tag = Nothing
            b_movie_deletelogo.Visible = False
        End If
    End Sub

    Private Sub b_movie_preview_Click(sender As Object, e As EventArgs) Handles b_movie_preview.Click
        Dim preview As New DVDArt_Preview(thumbs & DVDArt_Common.folder(0, 0, 0) & current_imdb_id & ".png")
        preview.Show()
    End Sub

    Private Sub b_movie_delete_Click(sender As Object, e As EventArgs) Handles b_movie_delete.Click
        If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 0, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 0, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
        If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 0, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 0, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
        pb_movie_dvdart.Image = Nothing
        pb_movie_dvdart.Tag = Nothing
        b_movie_compress.Visible = False
        b_movie_preview.Visible = False
        b_movie_delete.Visible = False
    End Sub

    Private Sub b_movie_deletebanner_Click(sender As Object, e As EventArgs) Handles b_movie_deletebanner.Click
        If lv_movies.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 3, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 3, 0) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 3, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(0, 3, 1) & lv_movies.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_movie_banner.Image = Nothing
            pb_movie_banner.Tag = Nothing
            b_movie_deletebanner.Visible = False
        End If
    End Sub

    Private Sub b_movie_deletebackdrop_Click(sender As Object, e As EventArgs) Handles b_movie_deletebackdrop.Click
        If IO.File.Exists(lv_movies.SelectedItems(0).SubItems.Item(2).Text) Then DeleteFile(lv_movies.SelectedItems(0).SubItems.Item(2).Text)
        If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 4, 1) & IO.Path.GetFileName(lv_movies.SelectedItems(0).SubItems.Item(2).Text)) Then DeleteFile(thumbs & DVDArt_Common.folder(0, 4, 1) & IO.Path.GetFileName(lv_movies.SelectedItems(0).SubItems.Item(2).Text))
        pb_movie_backdrop.Image = Nothing
        pb_movie_backdrop.Tag = Nothing
        b_movie_deletebackdrop.Visible = False

        DVDArt_Common.updateMovingPicturesDB("backdrop", IO.Path.GetFileNameWithoutExtension(lv_movies.SelectedItems(0).SubItems.Item(1).Text), "")
    End Sub

    Private Sub b_movie_deletecover_Click(sender As Object, e As EventArgs) Handles b_movie_deletecover.Click
        If IO.File.Exists(lv_movies.SelectedItems(0).SubItems.Item(3).Text) Then DeleteFile(lv_movies.SelectedItems(0).SubItems.Item(3).Text)
        If IO.File.Exists(thumbs & DVDArt_Common.folder(0, 5, 1) & IO.Path.GetFileName(lv_movies.SelectedItems(0).SubItems.Item(3).Text)) Then DeleteFile(thumbs & DVDArt_Common.folder(0, 5, 1) & IO.Path.GetFileName(lv_movies.SelectedItems(0).SubItems.Item(3).Text))
        pb_movie_cover.Image = Nothing
        pb_movie_cover.Tag = Nothing
        b_movie_deletecover.Visible = False

        DVDArt_Common.updateMovingPicturesDB("cover", IO.Path.GetFileNameWithoutExtension(lv_movies.SelectedItems(0).SubItems.Item(1).Text), "")
    End Sub

    Private Sub b_serie_deleteart_Click(sender As Object, e As EventArgs) Handles b_serie_deleteart.Click
        If lv_series.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(1, 1, 0) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(1, 1, 0) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(1, 1, 1) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(1, 1, 1) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_serie_clearart.Image = Nothing
            pb_serie_clearart.Tag = Nothing
            b_serie_deleteart.Visible = False
        End If
    End Sub

    Private Sub b_serie_deletelogo_Click(sender As Object, e As EventArgs) Handles b_serie_deletelogo.Click
        If lv_series.SelectedItems(0).SubItems.Item(1).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(1, 2, 0) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(1, 2, 0) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(1, 2, 1) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(1, 2, 1) & lv_series.SelectedItems(0).SubItems.Item(1).Text & ".png")
            pb_serie_clearlogo.Image = Nothing
            pb_serie_clearlogo.Tag = Nothing
            b_serie_deletelogo.Visible = False
        End If
    End Sub

    Private Sub b_album_compress_Click(sender As Object, e As EventArgs) Handles b_album_compress.Click

        Dim album As String = Utils.MakeFileName(current_album)

        DVDArt_Common.Resize(thumbs & DVDArt_Common.folder(2, 0, 0) & album & ".png")

        l_music_size.Text = DVDArt_Common.getSize(thumbs & DVDArt_Common.folder(2, 0, 0) & album & ".png")

        If l_music_size.Text = "500x500" Then b_album_compress.Visible = False Else b_album_compress.Visible = True

        b_album_preview.Visible = Not b_album_compress.Visible

    End Sub

    Private Sub b_album_preview_Click(sender As Object, e As EventArgs) Handles b_album_preview.Click
        Dim preview As New DVDArt_Preview(thumbs & DVDArt_Common.folder(2, 0, 0) & Utils.MakeFileName(current_album) & ".png")
        preview.Show()
    End Sub

    Private Sub b_album_deletecdart_Click(sender As Object, e As EventArgs) Handles b_album_delete.Click
        If IO.File.Exists(thumbs & DVDArt_Common.folder(2, 0, 0) & lv_album.SelectedItems(0).SubItems.Item(0).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(2, 0, 0) & lv_album.SelectedItems(0).SubItems.Item(0).Text & ".png")
        If IO.File.Exists(thumbs & DVDArt_Common.folder(2, 0, 1) & lv_album.SelectedItems(0).SubItems.Item(0).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(2, 0, 1) & lv_album.SelectedItems(0).SubItems.Item(0).Text & ".png")
        pb_album_cdart.Image = Nothing
        pb_album_cdart.Tag = Nothing
        b_album_compress.Visible = False
        b_album_preview.Visible = False
        b_album_delete.Visible = False
    End Sub

    Private Sub b_artist_deletebanner_Click(sender As Object, e As EventArgs) Handles b_artist_deletebanner.Click
        If lv_artist.SelectedItems(0).SubItems.Item(0).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(2, 1, 0) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(2, 1, 0) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(2, 1, 1) & lv_movies.SelectedItems(0).SubItems.Item(0).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(2, 1, 1) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            pb_artist_banner.Image = Nothing
            pb_artist_banner.Tag = Nothing
            b_artist_deletebanner.Visible = False
        End If
    End Sub

    Private Sub b_artist_deletelogo_Click(sender As Object, e As EventArgs) Handles b_artist_deletelogo.Click
        If lv_artist.SelectedItems(0).SubItems.Item(0).Text <> Nothing Then
            If IO.File.Exists(thumbs & DVDArt_Common.folder(2, 2, 0) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(2, 2, 0) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            If IO.File.Exists(thumbs & DVDArt_Common.folder(2, 2, 1) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png") Then DeleteFile(thumbs & DVDArt_Common.folder(2, 2, 1) & lv_artist.SelectedItems(0).SubItems.Item(0).Text & ".png")
            pb_artist_clearlogo.Image = Nothing
            pb_artist_clearlogo.Tag = Nothing
            b_artist_deletelogo.Visible = False
        End If
    End Sub

    Private Sub b_browse_Click(sender As Object, e As EventArgs) Handles b_music_path.Click, b_series_path.Click, b_movie_path.Click, b_person_path.Click

        Dim objShell As Object
        Dim objFolder As Object
        Dim msg As String = Nothing

        If DirectCast(sender, System.Windows.Forms.Button).Name = "b_movie_path" Then
            msg = "Please select folder to put Movie artwork"
        ElseIf DirectCast(sender, System.Windows.Forms.Button).Name = "b_person_path" Then
            msg = "Please select folder to put Artist/Writer/Director image"
        ElseIf DirectCast(sender, System.Windows.Forms.Button).Name = "b_series_path" Then
            msg = "Please select folder to put Series artwork"
        ElseIf DirectCast(sender, System.Windows.Forms.Button).Name = "b_music_path" Then
            msg = "Please select folder to put Music artwork"
        End If

        objShell = CreateObject("Shell.Application")
        objFolder = objShell.BrowseForFolder(0, msg, 0)

        Try
            If DirectCast(sender, System.Windows.Forms.Button).Name = "b_movie_path" Then
                If IsError(objFolder.Items.Item.Path) Then
                    tb_movie_path.Text = CStr(objFolder)
                Else
                    tb_movie_path.Text = objFolder.Items.Item.Path
                End If
            ElseIf DirectCast(sender, System.Windows.Forms.Button).Name = "b_person_path" Then
                If IsError(objFolder.Items.Item.Path) Then
                    tb_person_path.Text = CStr(objFolder)
                Else
                    tb_person_path.Text = objFolder.Items.Item.Path
                End If
            ElseIf DirectCast(sender, System.Windows.Forms.Button).Name = "b_series_path" Then
                If IsError(objFolder.Items.Item.Path) Then
                    tb_series_path.Text = CStr(objFolder)
                Else
                    tb_series_path.Text = objFolder.Items.Item.Path
                End If
            ElseIf DirectCast(sender, System.Windows.Forms.Button).Name = "b_music_path" Then
                If IsError(objFolder.Items.Item.Path) Then
                    tb_music_path.Text = CStr(objFolder)
                Else
                    tb_music_path.Text = objFolder.Items.Item.Path
                End If
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub tb_person_path_TextChanged(sender As Object, e As EventArgs) Handles tb_person_path.TextChanged
        personpath = tb_person_path.Text
    End Sub

    Private Sub pbFTV_Logo_Click(sender As Object, e As EventArgs) Handles pbFTV_Logo.Click, pbFTV_Logo2.Click, pbFTV_Logo3.Click
        Dim url As String = "http://www.fanart.tv"
        Process.Start(url)
    End Sub

    Private Sub pbthemoviedb_Click(sender As Object, e As EventArgs) Handles pbthemoviedb.Click
        Dim url As String = "http://www.themoviedb.org"
        Process.Start(url)
    End Sub

    Private Sub pbLastFM_Click(sender As Object, e As EventArgs) Handles pbLastFM.Click
        Dim url As String = "http://www.last.fm/"
        Process.Start(url)
    End Sub

    Private Sub pbAudioDB_Click(sender As Object, e As EventArgs) Handles pbAudioDB.Click
        Dim url As String = "http://www.theaudiodb.com/"
        Process.Start(url)
    End Sub

    Private Sub pb_donate_Click(sender As Object, e As EventArgs) Handles pb_donate.Click
        Dim url As String = "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=7MDF77KKTGKAJ&lc=MT&item_name=DVDArt%20plugin%20support&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted"
        Process.Start(url)
    End Sub

    Private Sub ll_developer_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ll_developer.LinkClicked
        Dim url As String = "http://www.team-mediaportal.com/extensions/owner/m3rcury"
        Process.Start(url)
    End Sub

    Private Sub ll_project_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ll_project.LinkClicked
        Dim url As String = "http://www.team-mediaportal.com/extensions/movies-videos/dvdart-plugin/visit"
        Process.Start(url)
    End Sub

    Private Sub ll_forum_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ll_forum.LinkClicked
        Dim url As String = "http://forum.team-mediaportal.com/threads/dvdart-plugin-that-scrapes-fanart-tv-for-dvd-cover-art-of-you-movie-collection-in-movingpictures.112847/"
        Process.Start(url)
    End Sub

    Private Sub ll_wiki_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ll_wiki.LinkClicked
        Dim url As String = "http://code.google.com/p/dvdart/w/list"
        Process.Start(url)
    End Sub

    Private Sub DVDArt_FormClosed(ByVal sender As Object, ByVal e As FormClosedEventArgs) Handles Me.FormClosed

        On Error Resume Next

        If Err.Number = 0 Then

            Cursor = Cursors.WaitCursor

            Dim url() As String = Nothing

            If tbc_main.Tag <> Nothing Then

                If tbc_main.Tag = "movies" Then
                    url = {pb_movie_dvdart.Tag, pb_movie_clearart.Tag, pb_movie_clearlogo.Tag, pb_movie_banner.Tag, pb_movie_backdrop.Tag, pb_movie_cover.Tag}
                ElseIf tbc_main.Tag = "tv" Then
                    url = {Nothing, pb_serie_clearart.Tag, pb_serie_clearlogo.Tag, Nothing, Nothing}
                ElseIf tbc_main.Tag = "music" Then
                    url = {Nothing, pb_artist_banner.Tag, pb_artist_clearlogo.Tag, Nothing, Nothing}
                ElseIf tbc_main.Tag = "album" Then
                    url = {pb_album_cdart.Tag, Nothing, Nothing, Nothing, Nothing}
                End If

                t_import_timer.Stop()

                If url(0) <> Nothing Or url(1) <> Nothing Or url(2) <> Nothing Or url(3) <> Nothing Or url(4) <> Nothing Then
                    FTV_api_connector(current_imdb_id, url, tbc_main.Tag, "selected")
                End If

            End If

            If DVDArt_Common.bw_download0.IsBusy Or DVDArt_Common.bw_download1.IsBusy Or DVDArt_Common.bw_download2.IsBusy Or DVDArt_Common.bw_download3.IsBusy Or DVDArt_Common.bw_download4.IsBusy Or DVDArt_Common.bw_download5.IsBusy Or DVDArt_Common.bw_download6.IsBusy Or DVDArt_Common.bw_download8.IsBusy Or DVDArt_Common.bw_download6.IsBusy Or DVDArt_Common.bw_download9.IsBusy Or bw_compress.IsBusy Then
                wait(5000)
            End If

            If DVDArt_Common.bw_download0.IsBusy Then DVDArt_Common.bw_download0.CancelAsync()
            If DVDArt_Common.bw_download1.IsBusy Then DVDArt_Common.bw_download1.CancelAsync()
            If DVDArt_Common.bw_download2.IsBusy Then DVDArt_Common.bw_download2.CancelAsync()
            If DVDArt_Common.bw_download3.IsBusy Then DVDArt_Common.bw_download3.CancelAsync()
            If DVDArt_Common.bw_download4.IsBusy Then DVDArt_Common.bw_download4.CancelAsync()
            If DVDArt_Common.bw_download5.IsBusy Then DVDArt_Common.bw_download5.CancelAsync()
            If DVDArt_Common.bw_download6.IsBusy Then DVDArt_Common.bw_download6.CancelAsync()
            If DVDArt_Common.bw_download7.IsBusy Then DVDArt_Common.bw_download7.CancelAsync()
            If DVDArt_Common.bw_download8.IsBusy Then DVDArt_Common.bw_download8.CancelAsync()
            If DVDArt_Common.bw_download9.IsBusy Then DVDArt_Common.bw_download9.CancelAsync()
            If bw_compress.IsBusy Then bw_compress.CancelAsync()
            If bw_coverart.IsBusy Then bw_coverart.CancelAsync()
            If bw_import.IsBusy Then bw_import.CancelAsync()
            If bw_rescan_persons.IsBusy Then bw_rescan_persons.CancelAsync()

            Cursor = Cursors.Default

            setSettings()

        End If

        DVDArt_Common.logStats("DVDArt: Plugin ended.", "LOG")

        Return

    End Sub

    Private Sub DVDArt_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        DVDArt_Common.logStats("DVDArt: Plugin started.", "LOG")

        If DVDArt_Common.Get_Paths(thumbs) Then

            'pre Initialize
            DVDArt_Common.preInitialize()

            'initialize about tab version
            l_version.Text = DVDArt_Common._version

            'show splashscreen
            Dim splash As New DVDArt_SplashScreen
            splash.Show()
            splash.Refresh()

            'remove all child tabs so that only selected ones will be added
            removeTabPages()

            getSettings()

            'remove any unutilised sub-pages

            If Not checked(2, 1) And Not checked(2, 2) Then tbc_music.TabPages.Remove(tp_artists)
            If Not checked(2, 0) Then tbc_music.TabPages.Remove(tp_albums)

            'initialize common variables
            DVDArt_Common.Initialize(thumbs, tb_movie_path.Text, tb_series_path.Text, tb_music_path.Text, personpath)

            'initialize version
            Me.Text = Me.Text & DVDArt_Common._version

            'initialize importer state images
            il_state.Images.Add(My.Resources.download)
            il_state.Images.Add(My.Resources.tick)
            il_state.Images.Add(My.Resources.cross)
            il_state.Images.Add(My.Resources.na)
            il_state.Images.Add(My.Resources.tick_2)

            'initialize column header images
            il_column.Images.Add(My.Resources.sort_none)
            il_column.Images.Add(My.Resources.sort_asc)
            il_column.Images.Add(My.Resources.sort_desc)

            'initialize labels
            l_imdb_id.Text = Nothing
            l_movie_size.Text = Nothing

            'populate language dropdown
            cb_language.Items.AddRange(DVDArt_Common.lang)
            cb_language.Text = DVDArt_Common.lang(Array.IndexOf(DVDArt_Common.langcode, _lang))

            'set the tab pages
            setMainTabPages("ALL")

            'set focus to first tab page
            tbc_main.SelectedIndex = tbc_main.TabPages.IndexOf(tp_Importer)

            ' load the data
            LoadSerieList()
            LoadMovieList()
            loadPersonList()
            LoadMusicList()

            'start the import process
            If lv_import.Items.Count > 0 And cb_autoimport.Checked Then FTV_api_connector(Nothing, Nothing, Nothing, "import")

            'close splashscreen
            splash.Close()
            splash.Dispose()

        Else
            DVDArt_Common.logStats("DVDArt: Failed to load  thumb paths!", "ERROR")
            MsgBox("Unable to load  Thumbs paths from MediaPortalDirs.xml", MsgBoxStyle.Critical, "DVDArt Plugin")
            Return
        End If

    End Sub

End Class