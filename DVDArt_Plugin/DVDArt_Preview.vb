Public Class DVDArt_Preview

    Private _path As String
    Private _animate As Boolean
    Private _angle As Single
    Private WithEvents t_import_timer As New Timer

    Private Sub Rotate(ByVal sender As Object, ByVal e As PaintEventArgs) Handles pb_preview.Paint

        With e.Graphics
            'Move the origin to the centre of the PictureBox.
            .TranslateTransform(pb_preview.Width \ 2, pb_preview.Height \ 2)

            'Rotate.
            .RotateTransform(_angle)

            'Draw the image so its centre coincides with the origin.
            .DrawImage(pb_preview.Image, -pb_preview.Width \ 2, -pb_preview.Height \ 2)
        End With

    End Sub

    Private Sub import_timer_tick() Handles t_import_timer.Tick

        _angle += 1

        If _angle > 360 Then _angle = 1

        pb_preview.Refresh()

    End Sub

    Public Sub New(path As String, Optional animate As Boolean = True)
        InitializeComponent()
        _path = path
        _animate = animate
    End Sub

    Private Sub DVDArt_Preview_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Dim imagesize As String = DVDArt_Common.GetSize(IO.Path.GetDirectoryName(_path), IO.Path.GetFileName(_path))

        If imagesize = "500x500" Then
            Me.Width = 510
            Me.Height = 527
            pb_preview.Width = 500
            pb_preview.Height = 500
            l_copyright.Top = 488
        ElseIf imagesize = "500x281" Then
            Me.Width = 510
            Me.Height = 320
            pb_preview.Width = 500
            pb_preview.Height = 281
            l_copyright.Top = 281
        ElseIf imagesize = "400x155" Then
            Me.Width = 410
            Me.Height = 194
            pb_preview.Width = 400
            pb_preview.Height = 155
            l_copyright.Top = 155
        End If

        Me.CenterToScreen()
        Me.Refresh()

        'load image
        Dim fs As System.IO.FileStream
        fs = New System.IO.FileStream(_path, IO.FileMode.Open, IO.FileAccess.Read)
        pb_preview.Image = System.Drawing.Image.FromStream(fs)
        fs.Close()

        If _animate Then
            'initialize timer
            t_import_timer.Interval = 15
            t_import_timer.Start()
        End If

    End Sub

End Class