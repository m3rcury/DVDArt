Public Class DVDArt_ChangeMBID

    Public Sub ChangeMBID(MBID As String, ByRef new_MBID As String)

        Dim change As New DVDArt_ChangeMBID

        change.tb_MBID.Text = MBID

        change.ShowDialog()

        new_MBID = change.tb_MBID.Text

    End Sub

    Private Sub b_done_Click(sender As System.Object, e As System.EventArgs) Handles b_done.Click
        Me.Close()
        Return
    End Sub

End Class