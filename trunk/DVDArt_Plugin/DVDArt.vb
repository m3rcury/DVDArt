Imports Microsoft.VisualBasic.FileIO
Imports System.Data.SQLite
Imports MediaPortal.Configuration

Public Class DVDArt

    Private database, thumbs, current_imdb_id, _lastrun As String
    Private l_import_queue As New List(Of String)
    Private l_import_index As New List(Of Integer)
    Private lv_url As New ListView
    Private li_movies, li_import, li_missing As New ListViewItem
    Private WithEvents t_import_timer As New Timer

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

    Private Sub import_timer_tick() Handles t_import_timer.Tick

        If l_import_queue.Any And Not bw_import.IsBusy Then
            FTV_api_connector("queue", Nothing, "import")
        End If

    End Sub

    Private Sub bw_download_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_download_thumb.DoWork, bw_download_fullsize.DoWork

        Dim parm As String = e.Argument
        Dim url, path As String
        Dim endp As Integer
        Dim WebClient As New System.Net.WebClient

        endp = InStr(parm, "|")
        path = Microsoft.VisualBasic.Left(parm, endp - 1)
        url = Microsoft.VisualBasic.Right(parm, Microsoft.VisualBasic.Len(parm) - endp)

        WebClient.DownloadFile(url, path)

    End Sub

    Private Sub bw_import_worker(sender As System.Object, e As System.ComponentModel.DoWorkEventArgs) Handles bw_import.DoWork

        Dim parm As String = e.Argument

        CheckForIllegalCrossThreadCalls = False

        If parm = Nothing Then

            If lv_import.Items.Count > 0 Then

                Dim SQLconnect As New SQLite.SQLiteConnection()
                Dim SQLcommand As SQLiteCommand

                SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

                SQLconnect.Open()

                SQLcommand = SQLconnect.CreateCommand

                For x = 0 To (lv_import.Items.Count - 1)

                    SQLcommand.CommandText = "INSERT INTO processed_movies (imdb_id) VALUES(""" & lv_import.Items.Item(x).SubItems.Item(1).Text & """)"
                    SQLcommand.ExecuteNonQuery()

                    lv_import.Items(x).StateImageIndex = 0

                    If DVDArt_Common.download(thumbs, lv_import.Items.Item(x).SubItems.Item(1).Text, True) Then
                        lv_import.Items(x).StateImageIndex = 1
                        li_movies = lv_movies.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                        li_movies.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                    Else
                        lv_import.Items(x).StateImageIndex = 2
                        li_missing = lv_missing.Items.Add(lv_import.Items.Item(x).SubItems.Item(0).Text)
                        li_missing.SubItems.Add(lv_import.Items.Item(x).SubItems.Item(1).Text)
                    End If

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

                    If DVDArt_Common.download(thumbs, imdb_id, True) Then
                        lv_import.Items(l_import_index(x)).StateImageIndex = 1
                        li_movies = lv_movies.Items.Add(title)
                        li_movies.SubItems.Add(imdb_id)
                    Else
                        lv_import.Items(l_import_index(x)).StateImageIndex = 2
                        li_missing = lv_missing.Items.Add(title)
                        li_missing.SubItems.Add(imdb_id)
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

        Dim SQLconnect As New SQLite.SQLiteConnection()
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

    Private Sub FTV_api_connector(ByVal imdb_id As String, ByVal url As String, ByVal mode As String)

        Me.Cursor = Cursors.WaitCursor

        Dim x As Integer
        Dim moviediscurl(1, 20) As String
        Dim WebClient As New System.Net.WebClient

        x = 0

        If mode = "preview" Then

            Dim jsonresponse As String

            jsonresponse = DVDArt_Common.JSON_request(imdb_id, "2")

            If jsonresponse <> "null" Then

                moviediscurl = DVDArt_Common.parse(jsonresponse)

                Dim ImageInBytes() As Byte
                Dim stream As System.IO.MemoryStream

                Do Until moviediscurl(0, x) = Nothing
                    Dim imagekey As String = Guid.NewGuid().ToString()
                    ImageInBytes = WebClient.DownloadData(moviediscurl(0, x) & "/preview")
                    stream = New System.IO.MemoryStream(ImageInBytes)
                    il_available.Images.Add(imagekey, Image.FromStream(stream))
                    lv_available.Items.Add(moviediscurl(1, x), imagekey)
                    lv_url.Items.Add(moviediscurl(0, x), imagekey)
                    x += 1
                Loop
            End If

        ElseIf mode = "selected" Then

            Dim parm As Object
            Dim fullpath, thumbpath As String

            imdb_id = current_imdb_id
            url = pb_current.Tag

            fullpath = thumbs & "\MovingPictures\DVDArt\FullSize\" & imdb_id & ".png"
            thumbpath = thumbs & "\MovingPictures\DVDArt\Thumbs\" & imdb_id & ".png"

            parm = thumbpath & "|" & url & "/preview"

            bw_download_thumb.RunWorkerAsync(parm)

            parm = fullpath & "|" & url

            bw_download_fullsize.RunWorkerAsync(parm)

        ElseIf mode = "import" Then

            Dim parm As Object = imdb_id

            bw_import.RunWorkerAsync(parm)

        End If

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Load_Movie_List()

        Dim SQLconnect As New SQLite.SQLiteConnection()
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

        ' Read already processed movies to identify newly imported ones in movingpictures

        Dim x As Integer
        Dim processed_movies(), imdb_id_in_mp() As String

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

        x = 0

        While SQLreader.Read()

            If Trim(SQLreader(0)) <> "" Then

                If processed_movies.Contains(SQLreader(0)) Then

                    If FileSystem.FileExists(thumbs & "\MovingPictures\DVDArt\Thumbs\" & SQLreader(0) & ".png") Then
                        li_movies = lv_movies.Items.Add(SQLreader(1))
                        li_movies.SubItems.Add(SQLreader(0))
                    Else
                        li_missing = lv_missing.Items.Add(SQLreader(1))
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

        Dim SQLdelete As SQLiteCommand

        SQLconnect.ConnectionString = "Data Source=" & database & "\dvdart.db3"

        SQLconnect.Open()

        SQLcommand = SQLconnect.CreateCommand
        SQLdelete = SQLconnect.CreateCommand

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

    Private Sub lv_movies_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_movies.SelectedIndexChanged

        If current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text Then Exit Sub

        Dim thumbpath As String

        il_available.Images.Clear()
        lv_available.Items.Clear()
        lv_url.Items.Clear()

        If pb_current.Tag <> Nothing Then
            FTV_api_connector(current_imdb_id, pb_current.Tag, "selected")
        End If

        current_imdb_id = lv_movies.FocusedItem.SubItems.Item(1).Text
        l_imdb_id.Text = current_imdb_id
        thumbpath = thumbs & "\MovingPictures\DVDArt\Thumbs\" & current_imdb_id & ".png"

        If FileSystem.FileExists(thumbpath) Then

            Do Until Not FileInUse(thumbpath)
                wait(200)
            Loop

            Dim fs As System.IO.FileStream
            fs = New System.IO.FileStream(thumbpath, IO.FileMode.Open, IO.FileAccess.Read)
            pb_current.Image = System.Drawing.Image.FromStream(fs)
            fs.Close()

            pb_current.Tag = Nothing
        Else
            pb_current.Image = Nothing
            pb_current.Tag = Nothing
        End If

    End Sub

    Private Sub cms_movies_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_movies.ItemClicked

        If e.ClickedItem.Text = "Refresh DVDArt from online" Then
            If lv_movies.SelectedItems.Count > 0 Then
                FTV_api_connector(lv_movies.SelectedItems(0).SubItems.Item(1).Text, Nothing, "preview")
            Else
                MsgBox("Please select a movie.", MsgBoxStyle.Critical, Nothing)
            End If
        End If

    End Sub

    Private Sub cms_missing_Click(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles cms_missing.ItemClicked

        If e.ClickedItem.Text = "Send to importer" Then
            If lv_missing.SelectedItems.Count > 0 Then
                For x As Integer = 0 To (lv_missing.SelectedItems.Count - 1)
                    li_import = lv_import.Items.Add(lv_missing.SelectedItems(x).SubItems.Item(0).Text)
                    li_import.SubItems.Add(lv_missing.SelectedItems(x).SubItems.Item(1).Text)
                    l_import_queue.Add(lv_missing.SelectedItems(x).SubItems.Item(1).Text & "|" & lv_missing.SelectedItems(x).SubItems.Item(0).Text)
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
                    li_import.SubItems.Add(lv_missing.Items.Item(x).SubItems(1).Text)

                    l_import_queue.Add(lv_missing.Items.Item(x).SubItems(1).Text & "|" & lv_missing.Items.Item(x).SubItems(0).Text)
                    l_import_index.Add(lv_import.Items.Count - 1)

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

    Private Sub lv_available_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_available.SelectedIndexChanged

        For Each item In lv_available.SelectedItems

            Dim itemkey As String = item.ImageKey
            Dim image As Image = il_available.Images(itemkey)

            pb_current.Image = image
            pb_current.Tag = lv_url.Items(item.index).Text

        Next

    End Sub

    Private Sub Create_Folder_Structure()

        ' Check and create directory structure

        If Not FileSystem.FileExists(database + "\movingpictures.db3") Then
            Application.Exit()
        End If

        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\DVDArt") Then
            FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt")
            FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt\FullSize")
            FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt\Thumbs")
        End If

        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\DVDArt\FullSize") Then
            FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt\FullSize")
        End If

        If Not FileSystem.DirectoryExists(thumbs & "\MovingPictures\DVDArt\Thumbs") Then
            FileSystem.CreateDirectory(thumbs & "\MovingPictures\DVDArt\Thumbs")
        End If

    End Sub

    Private Sub Set_Settings()

        Using XMLwriter As MediaPortal.Profile.Settings = New MediaPortal.Profile.Settings(Config.GetFile(Config.Dir.Config, "DVDArt_Plugin.xml"))

            XMLwriter.SetValue("Settings", "delay", nud_delay.Value)
            XMLwriter.SetValue("Settings", "delay value", cb_delay.Text)
            XMLwriter.SetValue("Settings", "CPU utilisation", mtb_cpu.Text)
            XMLwriter.SetValue("Settings", "scraping", nud_scraping.Value)
            XMLwriter.SetValue("Settings", "scraping value", cb_scraping.Text)
            XMLwriter.SetValue("Settings", "missing", nud_missing.Value)
            XMLwriter.SetValue("Settings", "missing value", cb_missing.Text)

            If _lastrun = Nothing Then XMLwriter.SetValue("Scheduler", "lastrun", Now)

        End Using

        MsgBox("Configuration Saved", MsgBoxStyle.Information, "DVDArt Plugin")

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
            _lastrun = XMLreader.GetValueAsString("Settings", "lastrun", Nothing)

        End Using

    End Sub

    Private Sub DVDArt_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        If Err.Number = 0 Then

            Me.Cursor = Cursors.WaitCursor

            If pb_current.Tag <> Nothing Then
                FTV_api_connector(current_imdb_id, pb_current.Tag, "selected")
            End If

            Do While bw_download_thumb.IsBusy Or bw_download_fullsize.IsBusy
                wait(5000)
            Loop

            Me.Cursor = Cursors.Default

            Set_Settings()

        End If

        Return

    End Sub

    Private Sub DVDArt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        If DVDArt_Common.Get_Paths(database, thumbs) Then

            ' initialize timer

            t_import_timer.Interval = 2000
            t_import_timer.Start()

            ' initialize importer state images

            il_state.Images.Add(My.Resources.download)
            il_state.Images.Add(My.Resources.tick)
            il_state.Images.Add(My.Resources.cross)

            ' initialize labels

            l_imdb_id.Text = Nothing

            ' extract System.Data.SQLite.dll from resources to application library
            Dim dll As String = IO.Directory.GetCurrentDirectory() & "\System.Data.SQLite.dll"
            If Not FileSystem.FileExists(dll) Then FileSystem.WriteAllBytes(dll, My.Resources.System_Data_SQLite, False)

            Create_Folder_Structure()
            Get_Settings()
            Load_Movie_List()

        Else
            MsgBox("Unable to load Database & Thumbs paths from MediaPortalDirs.xml", MsgBoxStyle.Critical, "DVDArt Plugin")
            Return
        End If

    End Sub

End Class