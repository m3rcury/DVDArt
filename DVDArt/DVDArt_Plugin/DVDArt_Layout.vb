Public Class DVDArt_Layout

    Public Sub ChangeLayout(template_type As Integer, ByRef this_template_type As Integer)

        Dim layout As New DVDArt_Layout

        layout.rb_t1.Checked = (template_type = 1)
        layout.rb_t2.Checked = (template_type = 2)

        layout.ShowDialog()

        If layout.rb_t1.Checked Then this_template_type = 1
        If layout.rb_t2.Checked Then this_template_type = 2

    End Sub

    Private Sub rb_t1_CheckedChanged(sender As Object, e As EventArgs) Handles rb_t1.CheckedChanged
        rb_t1.Image = My.Resources.template_1
        rb_t2.Image = My.Resources.template_2_disabled
    End Sub

    Private Sub rb_t2_CheckedChanged(sender As Object, e As EventArgs) Handles rb_t2.CheckedChanged
        rb_t1.Image = My.Resources.template_1_disabled
        rb_t2.Image = My.Resources.template_2
    End Sub

    Private Sub b_done_Click(sender As Object, e As EventArgs) Handles b_done.Click
        Me.Close()
        Return
    End Sub

End Class