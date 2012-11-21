Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Public Class DVDArt_ManualUpload

    Private _imdb_id, _title, _type, thumbs As String
    Private _process(2) As Boolean

    Private Function load_image(ByVal path As String) As System.Drawing.Image

        On Error Resume Next

        Dim image As System.Drawing.Image
        Dim fs As System.IO.FileStream
        fs = New System.IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read)
        image = System.Drawing.Image.FromStream(fs)
        fs.Close()

        Return image

    End Function

    Public Sub New(imdb_id As String, title As String, type As String)
        InitializeComponent()
        _imdb_id = imdb_id
        _title = title
        _type = type
    End Sub

    Private Sub DVDArt_ManualUpload_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim database As String = String.Empty

        If DVDArt_Common.Get_Paths(database, thumbs) Then
            'change window title to reflect movie name
            Me.Text = Me.Text & " - " & _title
            Me.Refresh()

            'enable fields according to selection in setting
            l_dvdart.Enabled = DVDArt_GUI.checked(0) And _type <> "series"
            tb_dvdart.Enabled = DVDArt_GUI.checked(0) And _type <> "series"
            b_dvdart.Enabled = DVDArt_GUI.checked(0) And _type <> "series"
            b_preview_dvdart.Enabled = DVDArt_GUI.checked(0) And _type <> "series"
            l_clearart.Enabled = DVDArt_GUI.checked(1)
            tb_clearart.Enabled = DVDArt_GUI.checked(1)
            b_clearart.Enabled = DVDArt_GUI.checked(1)
            b_preview_clearart.Enabled = DVDArt_GUI.checked(1)
            l_clearlogo.Enabled = DVDArt_GUI.checked(2)
            tb_clearlogo.Enabled = DVDArt_GUI.checked(2)
            b_clearlogo.Enabled = DVDArt_GUI.checked(2)
            b_preview_clearlogo.Enabled = DVDArt_GUI.checked(2)
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
    End Sub

    Private Sub b_clearart_Click(sender As System.Object, e As System.EventArgs) Handles b_clearart.Click
        Dim openFileDialog As New OpenFileDialog

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            tb_clearart.Text = openFileDialog.FileName
            b_process_clearart.Visible = True
        Else
            b_process_clearart.Visible = False
        End If
    End Sub

    Private Sub b_clearlogo_Click(sender As System.Object, e As System.EventArgs) Handles b_clearlogo.Click
        Dim openFileDialog As New OpenFileDialog

        If openFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            tb_clearlogo.Text = openFileDialog.FileName
            b_process_clearlogo.Visible = True
        Else
            b_process_clearlogo.Visible = False
        End If
    End Sub

    Private Function Check_Image(ByVal path As String, ByVal width As Integer, ByVal height As Integer) As Boolean

        Dim imagesize, size As String

        If FileIO.FileSystem.FileExists(path) Then

            imagesize = DVDArt_Common.GetSize(IO.Path.GetDirectoryName(path), IO.Path.GetFileName(path))
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
        Dim preview As New DVDArt_Preview(tb_clearart.Text.Replace(IO.Path.GetExtension(tb_clearart.Text), ".png"), False)
        preview.Show()
    End Sub

    Private Sub b_preview_clearlogo_Click(sender As System.Object, e As System.EventArgs) Handles b_preview_clearlogo.Click
        Dim preview As New DVDArt_Preview(tb_clearlogo.Text.Replace(IO.Path.GetExtension(tb_clearlogo.Text), ".png"), False)
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
            Dim params() As String = {"-resize", "500x500", "-gravity", "Center", "-crop", "500x500+0+0", "+repage", DVDArt_Common._temp & "\dvdart_mask.png", "-alpha", "off", "-compose", "copy_opacity", "-composite", DVDArt_Common._temp & "\dvdart.png", "-compose", "over", "-composite"}

            DVDArt_Common.Convert(tb_dvdart.Text, file, params)
        End If

    End Sub

    Private Sub b_process_clearart_Click(sender As System.Object, e As System.EventArgs) Handles b_process_clearart.Click

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
            Dim params() As String = {"-bordercolor", "white", "-border", "1x1", "-alpha", "set", "-channel", "RGBA", "-fuzz", "1%", "-fill", "none", "-floodfill", "+0+0", "white", "-shave", "1x1"}

            DVDArt_Common.Convert(tb_clearlogo.Text, file, params)
        End If

    End Sub

    Private Sub b_upload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles b_upload.Click

        Me.Cursor = Cursors.WaitCursor

        Dim file As String

        If tb_dvdart.Text <> Nothing Then

            If Not _process(0) Then b_process_dvdart_Click(sender, e)

            file = tb_dvdart.Text.Replace(IO.Path.GetExtension(tb_dvdart.Text), ".png")

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, thumbs & DVDArt_Common.folder(0, 0, 0) & _imdb_id & ".png", True)

            'resize to thumb size and copy to Thumbs folder
            DVDArt_Common.Resize(file, 200, 200)
            FileIO.FileSystem.MoveFile(file, thumbs & DVDArt_Common.folder(0, 0, 1) & _imdb_id & ".png", True)
            If FileIO.FileSystem.FileExists(tb_dvdart.Text) Then FileIO.FileSystem.DeleteFile(tb_dvdart.Text)

        End If

        If tb_clearart.Text <> Nothing Then

            If Not _process(1) Then b_process_clearart_Click(sender, e)

            file = tb_clearart.Text.Replace(IO.Path.GetExtension(tb_clearart.Text), ".png")

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, thumbs & DVDArt_Common.folder(0, 1, 0) & _imdb_id & ".png", True)
            'resize to thumb size and copy to Thumbs folder
            DVDArt_Common.Resize(file, 200, 112)
            FileIO.FileSystem.MoveFile(file, thumbs & DVDArt_Common.folder(0, 1, 1) & _imdb_id & ".png", True)
            If FileIO.FileSystem.FileExists(tb_clearart.Text) Then FileIO.FileSystem.DeleteFile(tb_clearart.Text)

        End If

        If tb_clearlogo.Text <> Nothing Then

            If Not _process(2) Then b_process_clearlogo_Click(sender, e)

            file = tb_clearlogo.Text.Replace(IO.Path.GetExtension(tb_clearlogo.Text), ".png")

            Do While Not FileSystem.FileExists(file)
                DVDArt_Common.wait(500)
            Loop

            'copy to FullSize folder
            FileIO.FileSystem.CopyFile(file, """" & thumbs & DVDArt_Common.folder(0, 2, 0) & _imdb_id & ".png""", True)
            'resize to thumb size and copy to Thumbs folder
            DVDArt_Common.Resize(file, 200, 77)
            FileIO.FileSystem.MoveFile(file, """" & thumbs & DVDArt_Common.folder(0, 2, 1) & _imdb_id & ".png""", True)
            If FileIO.FileSystem.FileExists(tb_clearlogo.Text) Then FileIO.FileSystem.DeleteFile(tb_clearlogo.Text)

        End If

        Me.Cursor = Cursors.Default
        Me.Close()

        Return

    End Sub

End Class