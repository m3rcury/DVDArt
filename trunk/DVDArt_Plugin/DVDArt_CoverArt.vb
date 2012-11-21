Imports Microsoft.VisualBasic.FileIO

Public Class DVDArt_CoverArt

    Private _images(), _thumbs, _imdb_id As String
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

    Public Sub New(images() As String, thumbs As String, imdb_id As String)
        InitializeComponent()
        _images = images
        _thumbs = thumbs
        _imdb_id = imdb_id
    End Sub

    Private Sub lv_coverart_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lv_coverart.SelectedIndexChanged

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

    Private Sub DVDArt_CoverArt_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

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

    Private Sub b_preview_Click(sender As System.Object, e As System.EventArgs) Handles b_preview.Click

        'Copy the mask onto the main picture.
        Dim coverart As New Bitmap(pb_coverart.Image)

        Dim CropRect As New Rectangle(0, Math.Abs(pb_coverart.Bounds.Top), 500, 500)
        Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)
        Using grp = Graphics.FromImage(CropImage)
            grp.DrawImage(coverart, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
        End Using

        Dim g As Graphics = Graphics.FromImage(CropImage)

        g.DrawImage(CropImage, 0, 0)
        g.DrawImage(dvdart_mask, 0, 0)
        g.DrawImage(dvdart, 0, 0)
        coverart = CropImage
        g.Dispose()

        Dim file As String = DVDArt_Common._temp & "\" & _imdb_id & ".png"
        coverart.MakeTransparent(Color.Black)
        coverart.Save(file)

        Dim preview As New DVDArt_Preview(file)
        preview.Show()

        FileIO.FileSystem.DeleteFile(file)

    End Sub

    Private Sub b_done_Click(sender As System.Object, e As System.EventArgs) Handles b_done.Click

        Dim CropRect As New Rectangle(0, Math.Abs(pb_coverart.Bounds.Top), 500, 500)
        Dim CropImage = New Bitmap(CropRect.Width, CropRect.Height)
        Using grp = Graphics.FromImage(CropImage)
            grp.DrawImage(pb_coverart.Image, New Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel)
        End Using

        Dim file As String = DVDArt_Common._temp & "\" & _imdb_id & ".jpg"

        CropImage.Save(file)

        'create image with transparency from cover art
        Dim fullsize, thumb As String
        Dim file2 As String = file.Replace(IO.Path.GetExtension(file), ".png")
        Dim params() As String = {"-resize", "500", "-gravity", "Center", "-crop", "500x500+0+0", "+repage", DVDArt_Common._temp & "\dvdart_mask.png", "-alpha", "off", "-compose", "copy_opacity", "-composite", DVDArt_Common._temp & "\dvdart.png", "-compose", "over", "-composite"}

        fullsize = _thumbs & DVDArt_Common.folder(0, 0, 0) & _imdb_id & ".png"

        If FileIO.FileSystem.FileExists(fullsize) Then
            If MsgBox("DvdArt already exists.  Overwrite?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                Me.Close()
                Return
            Else
                FileIO.FileSystem.DeleteFile(fullsize)
            End If
        End If

        DVDArt_Common.Convert(file, file2, params)

        Dim counter As Integer = 0

        Do While (Not FileSystem.FileExists(file2) Or DVDArt_Common.FileInUse(file2)) And counter < 5
            DVDArt_Common.wait(250)
            counter += 1
        Loop

        If counter = 5 Then
            MsgBox("Failed to create DVDArt from " & file & " to " & fullsize, MsgBoxStyle.Critical)
        Else
            'move to Thumbs folder
            FileIO.FileSystem.MoveFile(file2, fullsize, True)
            'copy to Thumbs folder and resize to thumb size
            thumb = _thumbs & DVDArt_Common.folder(0, 0, 1) & _imdb_id & ".png"
            FileIO.FileSystem.CopyFile(fullsize, thumb, True)
            DVDArt_Common.Resize(thumb, 200, 200)

            FileIO.FileSystem.DeleteFile(file)
        End If

        Me.Close()

        Return

    End Sub

End Class