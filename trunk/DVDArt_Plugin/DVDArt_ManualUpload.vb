Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO

Public Class DVDArt_ManualUpload

    Public this_template_type As Integer = DVDArt_GUI.template_type

    Private _imagename, _title, _type, thumbs As String
    Private _process(3) As Boolean
    Private invchar As String = "[^\w\(){}@'-+. ]"

    Private Function load_image(ByVal path As String) As System.Drawing.Image

        On Error Resume Next

        Dim image As System.Drawing.Image
        Dim fs As System.IO.FileStream
        fs = New System.IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
        image = System.Drawing.Image.FromStream(fs)
        fs.Close()

        Return image

    End Function

    Public Sub New(imagename As String, title As String, type As String)
        InitializeComponent()
        _imagename = imagename
        _title = title
        _type = type
    End Sub

    Private Sub DVDArt_ManualUpload_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim database As String = String.Empty

        If DVDArt_Common.Get_Paths(database, thumbs) Then

            cb_title_and_logos_CheckedChanged(Nothing, Nothing)

            'change window title to reflect movie name
            Me.Text = Me.Text & " - " & _title
            Me.Refresh()

            'change labels depending of type
            If _type = "artist" Then
                l_clearart.Text = "Banner"
            End If

            If _type = "music" Then
                l_dvdart.Text = "CD Art"
            End If

            'enable fields according to selection in setting
            l_dvdart.Visible = (DVDArt_GUI.checked(0, 0) And _type = "movie") Or (DVDArt_GUI.checked(2, 0) And _type = "music")
            tb_dvdart.Visible = (DVDArt_GUI.checked(0, 0) And _type = "movie") Or (DVDArt_GUI.checked(2, 0) And _type = "music")
            b_dvdart.Visible = (DVDArt_GUI.checked(0, 0) And _type = "movie") Or (DVDArt_GUI.checked(2, 0) And _type = "music")
            b_preview_dvdart.Enabled = (DVDArt_GUI.checked(0, 0) And _type = "movie") Or (DVDArt_GUI.checked(2, 0) And _type = "music")

            cb_title.Visible = (_type = "movie")
            cb_logos.Visible = (_type = "movie")
            b_change_layout.Visible = (_type = "movie")

            l_clearart.Visible = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            tb_clearart.Visible = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            b_clearart.Visible = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            b_preview_clearart.Enabled = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            l_clearlogo.Visible = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            tb_clearlogo.Visible = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            b_clearlogo.Visible = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")
            b_preview_clearlogo.Enabled = (DVDArt_GUI.checked(0, 1) And _type = "movie") Or (DVDArt_GUI.checked(1, 1) And _type = "series") Or (DVDArt_GUI.checked(2, 1) And _type = "artist")

            l_backdrop.Visible = (DVDArt_GUI.checked(0, 3) And _type = "movie")
            tb_backdrop.Visible = (DVDArt_GUI.checked(0, 3) And _type = "movie")
            b_backdrop.Visible = (DVDArt_GUI.checked(0, 3) And _type = "movie")
            b_preview_backdrop.Enabled = (DVDArt_GUI.checked(0, 3) And _type = "movie")

            'centre upload button
            If _type <> "movie" Then
                b_upload.Left = (Me.ClientSize.Width / 2) - (b_upload.Width / 2)
            End If

        Else
            Return
        End If

    End Sub

    Private Sub b_dvdart_Click(sender As System.Object, e As System.EventArgs) Handles b_dvdart.Click
        Dim openFileDialog As New OpenFileDialog

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            tb_dvdart.Text = openFileDialog.FileName
            b_process_dvdart.Visible = True
        Else
            b_process_dvdart.Visible = False
        End If

        _process(0) = Not b_process_dvdart.Visible

    End Sub

    Private Sub b_clearart_Click(sender As System.Object, e As System.EventArgs) Handles b_clearart.Click
        Dim openFileDialog As New OpenFileDialog

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            tb_clearart.Text = openFileDialog.FileName
            b_process_clearart.Visible = True
        Else
            b_process_clearart.Visible = False
        End If

        _process(1) = Not b_process_clearart.Visible

    End Sub

    Private Sub b_clearlogo_Click(sender As System.Object, e As System.EventArgs) Handles b_clearlogo.Click
        Dim openFileDialog As New OpenFileDialog

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            tb_clearlogo.Text = openFileDialog.FileName
            b_process_clearlogo.Visible = True
        Else
            b_process_clearlogo.Visible = False
        End If

        _process(2) = Not b_process_clearlogo.Visible

    End Sub

    Private Sub b_backdrop_Click(sender As System.Object, e As System.EventArgs) Handles b_backdrop.Click
        Dim openFileDialog As New OpenFileDialog

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            tb_backdrop.Text = openFileDialog.FileName
            b_process_backdrop.Visible = True
        Else
            b_process_backdrop.Visible = False
        End If

        _process(3) = Not b_process_backdrop.Visible

    End Sub

    Private Function Check_Image(ByVal path As String, ByVal width As Integer, ByVal height As Integer) As Boolean

        Dim imagesize, size As String

        If FileIO.FileSystem.FileExists(path) Then

            imagesize = DVDArt_Common.getSize(IO.Path.GetDirectoryName(path) & "\" & IO.Path.GetFileName(path))
            size = width.ToString & "x" & height.ToString

            If imagesize <> size Then
                DVDArt_Common.Resize(path, width, height)
            End If
            Return True
        Else
            Return False
        End If

    End Function

    Private Sub b_preview_dvdart_Click(sender As System.Object, e As System.EventArgs) Handles b_preview_dvdart.Click
        Dim preview As New DVDArt_Preview(tb_dvdart.Text.Replace(IO.Path.GetExtension(tb_dvdart.Text), ".png"))
        preview.Show()
    End Sub

    Private Sub b_preview_clearart_Click(sender As System.Object, e As System.EventArgs) Handles b_preview_clearart.Click
        If _type <> "artist" Then
            Dim preview As New DVDArt_Preview(tb_clearart.Text.Replace(IO.Path.GetExtension(tb_clearart.Text), ".png"), False)
            preview.Show()
        Else
            Dim preview As New DVDArt_Preview(tb_clearart.Text, False)
            preview.Show()
        End If
    End Sub

    Private Sub b_preview_clearlogo_Click(sender As System.Object, e As System.EventArgs) Handles b_preview_clearlogo.Click
        Dim preview As New DVDArt_Preview(tb_clearlogo.Text.Replace(IO.Path.GetExtension(tb_clearlogo.Text), ".png"), False)
        preview.Show()
    End Sub

    Private Sub b_preview_backdrop_Click(sender As System.Object, e As System.EventArgs) Handles b_preview_backdrop.Click
        Dim preview As New DVDArt_Preview(tb_backdrop.Text.Replace(IO.Path.GetExtension(tb_backdrop.Text), ".jpg"), False)
        preview.Show()
    End Sub

    Private Sub b_process_dvdart_Click(sender As System.Object, e As System.EventArgs) Handles b_process_dvdart.Click

        If Check_Image(tb_dvdart.Text, 500, 500) Then
            b_process_dvdart.Visible = False
            b_preview_dvdart.Visible = True
            _process(0) = True

            If InStr(tb_dvdart.Text, " ") > 0 Then
                FileIO.FileSystem.RenameFile(tb_dvdart.Text, IO.Path.GetFileName(tb_dvdart.Text.Replace(" ", "")))
                tb_dvdart.Text = tb_dvdart.Text.Replace(" ", "")
                tb_dvdart.Refresh()
            End If

            'create image with transparency
            Dim file As String = tb_dvdart.Text.Replace(IO.Path.GetExtension(tb_dvdart.Text), ".png")
            Dim type As String

            If file = tb_dvdart.Text Then
                tb_dvdart.Text = tb_dvdart.Text.Replace(IO.Path.GetExtension(tb_dvdart.Text), ".jpg")
                FileIO.FileSystem.RenameFile(file, IO.Path.GetFileName(tb_dvdart.Text))
            End If

            If _type = "music" Then type = "cdart" Else type = "dvdart"

            DVDArt_Common.create_CoverArt(tb_dvdart.Text, _imagename, _title, cb_title.Checked, cb_logos.Checked, this_template_type, True, file, type)

        End If

    End Sub

    Private Sub b_process_clearart_Click(sender As System.Object, e As System.EventArgs) Handles b_process_clearart.Click

        If _type <> "artist" Then

            If Check_Image(tb_clearart.Text, 500, 281) Then
                b_process_clearart.Visible = False
                b_preview_clearart.Visible = True
                _process(1) = True

                If InStr(tb_clearart.Text, " ") > 0 Then
                    FileIO.FileSystem.RenameFile(tb_clearart.Text, IO.Path.GetFileName(tb_clearart.Text.Replace(" ", "")))
                    tb_clearart.Text = tb_clearart.Text.Replace(" ", "")
                    tb_clearart.Refresh()
                End If

                'create image with transparency
                Dim file As String = tb_clearart.Text.Replace(IO.Path.GetExtension(tb_clearart.Text), ".png")
                Dim params() As String = {"-bordercolor", "white", "-border", "1x1", "-alpha", "set", "-channel", "RGBA", "-fuzz", "1%", "-fill", "none", "-floodfill", "+0+0", "white", "-shave", "1x1"}

                DVDArt_Common.Convert(tb_clearart.Text, file, params)
            End If

        Else

            If Check_Image(tb_clearart.Text, 1000, 185) Then
                b_process_clearart.Visible = False
                b_preview_clearart.Visible = True
                _process(1) = True

                If InStr(tb_clearart.Text, " ") > 0 Then
                    FileIO.FileSystem.RenameFile(tb_clearart.Text, IO.Path.GetFileName(tb_clearart.Text.Replace(" ", "")))
                    tb_clearart.Text = tb_clearart.Text.Replace(" ", "")
                    tb_clearart.Refresh()
                End If
            End If

        End If

    End Sub

    Private Sub b_process_clearlogo_Click(sender As System.Object, e As System.EventArgs) Handles b_process_clearlogo.Click

        If Check_Image(tb_clearlogo.Text, 400, 155) Then
            b_process_clearlogo.Visible = False
            b_preview_clearlogo.Visible = True
            _process(2) = True

            If InStr(tb_clearlogo.Text, " ") > 0 Then
                FileIO.FileSystem.RenameFile(tb_clearlogo.Text, IO.Path.GetFileName(tb_clearlogo.Text.Replace(" ", "")))
                tb_clearlogo.Text = tb_clearlogo.Text.Replace(" ", "")
                tb_clearlogo.Refresh()
            End If

            'create image with transparency
            Dim file As String = tb_clearlogo.Text.Replace(IO.Path.GetExtension(tb_clearlogo.Text), ".png")
            Dim params() As String = {"-bordercolor", "white", "-border", "1x1", "-alpha", "set", "-channel", "RGBA", "-fuzz", "20%", "-fill", "none", "-floodfill", "+0+0", "white", "-shave", "1x1"}

            DVDArt_Common.Convert(tb_clearlogo.Text, file, params)
        End If

    End Sub

    Private Sub b_process_backdrop_Click(sender As System.Object, e As System.EventArgs) Handles b_process_backdrop.Click

        If Check_Image(tb_backdrop.Text, 1920, 1080) Then
            b_process_backdrop.Visible = False
            b_preview_backdrop.Visible = True
            _process(3) = True

            If InStr(tb_backdrop.Text, " ") > 0 Then
                FileIO.FileSystem.RenameFile(tb_backdrop.Text, IO.Path.GetFileName(tb_backdrop.Text.Replace(" ", "")))
                tb_backdrop.Text = tb_backdrop.Text.Replace(" ", "")
                tb_backdrop.Refresh()
            End If

            Dim info As New IO.FileInfo(tb_backdrop.Text)

            DVDArt_Common.reduceSize(tb_backdrop.Text, info.Length)

        End If

    End Sub

    Private Sub b_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_upload.Click

        Me.Cursor = Cursors.WaitCursor

        Dim file As String = Nothing
        Dim fullsize As String = Nothing
        Dim thumb As String = Nothing

        If tb_dvdart.Text <> Nothing Then

            If Not _process(0) Then b_process_dvdart_Click(sender, e)

            file = tb_dvdart.Text.Replace(IO.Path.GetExtension(tb_dvdart.Text), ".png")

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            _imagename = Regex.Replace(_imagename, invchar, "_")

            If _type = "music" Then
                fullsize = thumbs & DVDArt_Common.folder(2, 0, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(2, 0, 1) & _imagename & ".png"
            Else
                fullsize = thumbs & DVDArt_Common.folder(0, 0, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(0, 0, 1) & _imagename & ".png"
            End If

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, fullsize, True)
            'resize to thumb size and copy to Thumbs folder
            DVDArt_Common.Resize(file, 200, 200, True)
            FileIO.FileSystem.MoveFile(file, thumb, True)
            If FileIO.FileSystem.FileExists(tb_dvdart.Text) Then FileIO.FileSystem.DeleteFile(tb_dvdart.Text)

        End If

        If tb_clearart.Text <> Nothing Then

            If Not _process(1) Then b_process_clearart_Click(sender, e)

            If _type <> "artist" Then
                file = tb_clearart.Text.Replace(IO.Path.GetExtension(tb_clearart.Text), ".png")
            Else
                file = tb_clearart.Text
            End If

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            _imagename = Regex.Replace(_imagename, invchar, "_")

            If _type = "artist" Then
                fullsize = thumbs & DVDArt_Common.folder(2, 1, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(2, 1, 1) & _imagename & ".png"
            ElseIf _type = "series" Then
                fullsize = thumbs & DVDArt_Common.folder(1, 1, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(1, 1, 1) & _imagename & ".png"
            Else
                fullsize = thumbs & DVDArt_Common.folder(0, 1, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(0, 1, 1) & _imagename & ".png"
            End If

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, fullsize, True)
            'resize to thumb size and copy to Thumbs folder
            If _type <> "artist" Then
                DVDArt_Common.Resize(file, 200, 112, True)
            Else
                DVDArt_Common.Resize(file, 200, 37, True)
            End If

            FileIO.FileSystem.MoveFile(file, thumb, True)
            If FileIO.FileSystem.FileExists(tb_clearart.Text) Then FileIO.FileSystem.DeleteFile(tb_clearart.Text)

        End If

        If tb_clearlogo.Text <> Nothing Then

            If Not _process(2) Then b_process_clearlogo_Click(sender, e)

            file = tb_clearlogo.Text.Replace(IO.Path.GetExtension(tb_clearlogo.Text), ".png")

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            For Each c As Char In Path.GetInvalidFileNameChars()
                _imagename = _imagename.Replace(c, "_")
            Next

            If _type = "artist" Then
                fullsize = thumbs & DVDArt_Common.folder(2, 2, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(2, 2, 1) & _imagename & ".png"
            ElseIf _type = "series" Then
                fullsize = thumbs & DVDArt_Common.folder(1, 2, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(1, 2, 1) & _imagename & ".png"
            Else
                fullsize = thumbs & DVDArt_Common.folder(0, 2, 0) & _imagename & ".png"
                thumb = thumbs & DVDArt_Common.folder(0, 2, 1) & _imagename & ".png"
            End If

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, fullsize, True)
            'resize to thumb size and copy to Thumbs folder
            DVDArt_Common.Resize(file, 200, 77, True)
            FileIO.FileSystem.MoveFile(file, thumb, True)
            If FileIO.FileSystem.FileExists(tb_clearlogo.Text) Then FileIO.FileSystem.DeleteFile(tb_clearlogo.Text)

        End If

        If tb_backdrop.Text <> Nothing Then

            If Not _process(3) Then b_process_backdrop_Click(sender, e)

            file = tb_backdrop.Text.Replace(IO.Path.GetExtension(tb_backdrop.Text), ".jpg")

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            For Each c As Char In Path.GetInvalidFileNameChars()
                _imagename = _imagename.Replace(c, "_")
            Next

            
            fullsize = thumbs & DVDArt_Common.folder(0, 3, 0) & _imagename & ".jpg"
            thumb = thumbs & DVDArt_Common.folder(0, 3, 1) & _imagename & ".jpg"

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, fullsize, True)
            'resize to thumb size and copy to Thumbs folder
            DVDArt_Common.Resize(file, 200, 112, True)
            FileIO.FileSystem.MoveFile(file, thumb, True)
            If FileIO.FileSystem.FileExists(tb_backdrop.Text) Then FileIO.FileSystem.DeleteFile(tb_backdrop.Text)

            Dim database As String = Nothing
            DVDArt_Common.Get_Paths(database, thumbs)
            DVDArt_Common.updateBackdrop(database, _imagename, fullsize)

        End If

        Me.Cursor = Cursors.Default
        Me.Close()

        Return

    End Sub

    Private Sub b_change_layout_Click(sender As System.Object, e As System.EventArgs) Handles b_change_layout.Click
        Dim layout As New DVDArt_layout
        layout.ChangeLayout(this_template_type, this_template_type)
    End Sub

    Private Sub cb_title_and_logos_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles cb_title.CheckedChanged, cb_logos.CheckedChanged
        b_change_layout.Enabled = cb_title.Checked Or cb_logos.Checked
    End Sub

End Class