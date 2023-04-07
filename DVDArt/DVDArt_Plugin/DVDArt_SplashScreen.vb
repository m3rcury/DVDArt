Public Class DVDArt_SplashScreen

    Private Sub DVDArt_SplashScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        l_version.Text = DVDArt_Common._version
        l_copyright.Text = DVDArt_Common._copyright
    End Sub

End Class