﻿Imports Microsoft.VisualBasic.FileIO
Imports System.Data.SQLite

Public Class DVDArt_CoverArt

    Public this_template_type As Integer = DVDArt_GUI.template_type
    Public this_size_type As Integer = DVDArt_GUI.size_type
    Public this_title_pos As Integer = DVDArt_GUI.title_pos

    Private _images(), _thumbs, _imdb_id, _movie_name As String
    Private lv_images As New ListView
    Private dvdart, dvdart_mask As Bitmap

    Private IsDragging As Boolean = False
    Private StartPoint As Point

    Private Sub pb_preview_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_coverart.MouseDown
        StartPoint = pb_coverart.PointToScreen(New Point(e.X, e.Y))
        IsDragging = True
    End Sub

    Private Sub pb_preview_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_coverart.MouseMove

        If IsDragging Then
            Dim EndPoint As Point = pb_coverart.PointToScreen(New Point(e.X, e.Y))
            'pb_preview.Left += (EndPoint.X - StartPoint.X)
            pb_coverart.Top += (EndPoint.Y - StartPoint.Y)
            StartPoint = EndPoint
        End If

    End Sub

    Private Sub pb_preview_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles pb_coverart.MouseUp
        IsDragging = False
    End Sub

    Public Sub New(images() As String, thumbs As String, imdb_id As String, movie_name As String)
        InitializeComponent()
        _images = images
        _thumbs = thumbs
        _imdb_id = imdb_id
        _movie_name = movie_name.Replace("""", Nothing)
    End Sub

    Private Sub lv_coverart_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lv_coverart.SelectedIndexChanged

        Dim path As String
        Dim factor As Decimal
        Dim image As Image
        Dim ImageInBytes() As Byte
        Dim stream As System.IO.MemoryStream

        For Each item In lv_coverart.SelectedItems

            path = lv_images.Items(item.index).Text

            ImageInBytes = FileSystem.ReadAllBytes(path)
            stream = New System.IO.MemoryStream(ImageInBytes)
            image = image.FromStream(stream)

            factor = 500 / image.Size.Width

            pb_coverart.Image = New Bitmap(image, New Size(image.Size.Width * factor, image.Size.Height * factor))
            pb_coverart.Refresh()

        Next

    End Sub

    Private Sub DVDArt_CoverArt_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        l_copyright.Text = DVDArt_Common._copyright

        cb_title_and_logos_CheckedChanged(Nothing, Nothing)

        Dim fs As System.IO.FileStream
        Dim imagekey As String
        Dim x As Integer

        For x = 0 To (_images.Length - 1)

            If _images(x) <> String.Empty Then
                fs = New System.IO.FileStream(_images(x), IO.FileMode.Open, IO.FileAccess.Read)
                imagekey = Guid.NewGuid().ToString()
                il_coverart.Images.Add(imagekey, System.Drawing.Image.FromStream(fs))
                lv_coverart.Items.Add(x.ToString, imagekey)
                lv_images.Items.Add(_images(x), imagekey)
                fs.Close()
            End If

        Next

        'load dvdart template
        fs = New System.IO.FileStream(DVDArt_Common._temp & "\dvdart.png", IO.FileMode.Open, IO.FileAccess.Read)
        dvdart = System.Drawing.Image.FromStream(fs)
        fs.Close()

        'load dvdart mask
        fs = New System.IO.FileStream(DVDArt_Common._temp & "\dvdart_mask.png", IO.FileMode.Open, IO.FileAccess.Read)
        dvdart_mask = System.Drawing.Image.FromStream(fs)
        fs.Close()

        dvdart_mask.MakeTransparent(Color.White)

    End Sub

    Private Sub create_Disc(ByVal tempfile As String, Optional ByVal preview As Boolean = False, Optional ByVal previewfile As String = Nothing)

        Dim CropRect As New Rectangle(0, Math.Abs(pb_coverart.Bounds.Top), 500, 500)
        Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)

        Using grp = Graphics.FromImage(CropImage)
            grp.DrawImage(pb_coverart.Image, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
        End Using

        CropImage.Save(tempfile)
        CropImage.Dispose()

        DVDArt_Common.create_CoverArt(tempfile, _imdb_id, _movie_name, cb_title.SelectedIndex, cb_logos.Checked, this_template_type, this_size_type, this_title_pos, preview, previewfile)

        If FileIO.FileSystem.FileExists(tempfile) Then
            Do While DVDArt_Common.FileInUse(tempfile)
                DVDArt_Common.wait(250)
            Loop
            FileIO.FileSystem.DeleteFile(tempfile)
        End If

    End Sub

    Private Sub b_preview_Click(sender As Object, e As EventArgs) Handles b_preview.Click

        Dim file As String = DVDArt_Common._temp & "\" & _imdb_id & ".jpg"
        Dim file2 As String = DVDArt_Common._temp & "\" & _imdb_id & ".png"

        create_Disc(file, True, file2)

        Dim preview As New DVDArt_Preview(file2)
        preview.Show()

    End Sub

    Private Sub b_change_layout_Click(sender As Object, e As EventArgs) Handles b_change_layout.Click
        Dim layout As New DVDArt_Layout
        layout.ChangeLayout(this_template_type, this_size_type, this_template_type, this_size_type, this_title_pos)
    End Sub

    Private Sub b_done_Click(sender As Object, e As EventArgs) Handles b_done.Click

        Dim file2 As String = _thumbs & DVDArt_Common.folder(0, 0, 0) & _imdb_id & ".png"

        If FileIO.FileSystem.FileExists(file2) Then
            If MsgBox("DvdArt already exists.  Overwrite?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Me.Close()
                Return
            Else
                Do While DVDArt_Common.FileInUse(file2)
                    DVDArt_Common.wait(250)
                Loop
                FileIO.FileSystem.DeleteFile(file2)
            End If
        End If

        Dim file As String = DVDArt_Common._temp & "\" & _imdb_id & ".png"
        Dim thumb As String = _thumbs & DVDArt_Common.folder(0, 0, 1) & _imdb_id & ".png"

        If Not FileIO.FileSystem.FileExists(file) Then
            create_Disc(file, True, file2)
        Else
            Do While DVDArt_Common.FileInUse(file) Or DVDArt_Common.FileInUse(file2)
                DVDArt_Common.wait(250)
            Loop
            FileIO.FileSystem.MoveFile(file, file2)
        End If

        FileIO.FileSystem.CopyFile(file2, thumb, True)
        DVDArt_Common.Resize(thumb, 200, 200, True)

        If Not FileSystem.FileExists(file2) Then
            MsgBox("Failed to create DVDArt from " & file & " to " & file2, MsgBoxStyle.Critical)
        Else
            If FileIO.FileSystem.FileExists(file) Then
                Do While DVDArt_Common.FileInUse(file)
                    DVDArt_Common.wait(250)
                Loop
                FileIO.FileSystem.DeleteFile(file)
            End If
        End If

        Me.Close()

        Return

    End Sub

    Private Sub cb_title_and_logos_CheckedChanged(sender As Object, e As EventArgs) Handles cb_title.TextChanged, cb_logos.CheckedChanged
        b_change_layout.Enabled = cb_title.SelectedIndex > 0 Or cb_logos.Checked
    End Sub

End Class