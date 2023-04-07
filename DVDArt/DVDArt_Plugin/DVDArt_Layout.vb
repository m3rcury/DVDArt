Public Class DVDArt_Layout

    Public Sub ChangeLayout(template_type As Integer, size_type As Integer, ByRef this_template_type As Integer, ByRef this_size_type As Integer, ByRef this_title_pos As Integer)

        Dim layout As New DVDArt_Layout

        layout.rb_t1.Checked = (template_type = 1)
        layout.rb_t2.Checked = (template_type = 2)
        layout.rb_s1.Checked = (size_type = 1)
        layout.rb_s2.Checked = (size_type = 2)
        layout.rb_tl1.Checked = True
        layout.rb_tl2.Checked = False

        layout.ShowDialog()

        If layout.rb_t1.Checked Then this_template_type = 1
        If layout.rb_t2.Checked Then this_template_type = 2
        If layout.rb_s1.Checked Then this_size_type = 1
        If layout.rb_s2.Checked Then this_size_type = 2
        If layout.rb_tl1.Checked Then this_title_pos = 1
        If layout.rb_tl2.Checked Then this_title_pos = 2

    End Sub

    Private Sub rb_t1_CheckedChanged(sender As Object, e As EventArgs) Handles rb_t1.CheckedChanged
        rb_t1.Image = My.Resources.template_1
        rb_t2.Image = My.Resources.template_2_disabled
    End Sub

    Private Sub rb_t2_CheckedChanged(sender As Object, e As EventArgs) Handles rb_t2.CheckedChanged
        rb_t1.Image = My.Resources.template_1_disabled
        rb_t2.Image = My.Resources.template_2
    End Sub

    Private Sub rb_s1_CheckedChanged(sender As Object, e As EventArgs) Handles rb_s1.CheckedChanged
        rb_s1.Image = My.Resources.logo_size_80x56
        rb_s2.Image = My.Resources.logo_size_177x125_disabled
    End Sub

    Private Sub rb_s2_CheckedChanged(sender As Object, e As EventArgs) Handles rb_s2.CheckedChanged
        rb_s1.Image = My.Resources.logo_size_80x56_disabled
        rb_s2.Image = My.Resources.logo_size_177x125
    End Sub

    Private Sub rb_tl1_CheckedChanged(sender As Object, e As EventArgs) Handles rb_tl1.CheckedChanged
        rb_tl1.Image = My.Resources.title_1
        rb_tl2.Image = My.Resources.title_2_disabled
    End Sub

    Private Sub rb_tl2_CheckedChanged(sender As Object, e As EventArgs) Handles rb_tl2.CheckedChanged
        rb_tl1.Image = My.Resources.title_1_disabled
        rb_tl2.Image = My.Resources.title_2
    End Sub

    Private Sub b_done_Click(sender As Object, e As EventArgs) Handles b_done.Click
        Me.Close()
        Return
    End Sub

    Private Sub DVDArt_Layout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        l_copyright.Text = DVDArt_Common._copyright
    End Sub

End Class