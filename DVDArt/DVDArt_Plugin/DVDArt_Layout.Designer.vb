<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_Layout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.b_done = New System.Windows.Forms.Button()
        Me.l_copyright = New System.Windows.Forms.Label()
        Me.p_size_type = New System.Windows.Forms.Panel()
        Me.p_title_position = New System.Windows.Forms.Panel()
        Me.rb_tl1 = New System.Windows.Forms.RadioButton()
        Me.rb_tl2 = New System.Windows.Forms.RadioButton()
        Me.rb_s1 = New System.Windows.Forms.RadioButton()
        Me.rb_s2 = New System.Windows.Forms.RadioButton()
        Me.rb_t2 = New System.Windows.Forms.RadioButton()
        Me.rb_t1 = New System.Windows.Forms.RadioButton()
        Me.p_size_type.SuspendLayout()
        Me.p_title_position.SuspendLayout()
        Me.SuspendLayout()
        '
        'b_done
        '
        Me.b_done.Location = New System.Drawing.Point(220, 541)
        Me.b_done.Name = "b_done"
        Me.b_done.Size = New System.Drawing.Size(75, 23)
        Me.b_done.TabIndex = 27
        Me.b_done.Text = "Done"
        Me.b_done.UseVisualStyleBackColor = True
        '
        'l_copyright
        '
        Me.l_copyright.AutoSize = True
        Me.l_copyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_copyright.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.l_copyright.Location = New System.Drawing.Point(2, 563)
        Me.l_copyright.Name = "l_copyright"
        Me.l_copyright.Size = New System.Drawing.Size(138, 12)
        Me.l_copyright.TabIndex = 28
        Me.l_copyright.Text = "Copyright © 2012-2017, m3rcury"
        '
        'p_size_type
        '
        Me.p_size_type.Controls.Add(Me.rb_s1)
        Me.p_size_type.Controls.Add(Me.rb_s2)
        Me.p_size_type.Location = New System.Drawing.Point(13, 218)
        Me.p_size_type.Name = "p_size_type"
        Me.p_size_type.Size = New System.Drawing.Size(483, 100)
        Me.p_size_type.TabIndex = 30
        '
        'p_title_position
        '
        Me.p_title_position.Controls.Add(Me.rb_tl1)
        Me.p_title_position.Controls.Add(Me.rb_tl2)
        Me.p_title_position.Location = New System.Drawing.Point(13, 320)
        Me.p_title_position.Name = "p_title_position"
        Me.p_title_position.Size = New System.Drawing.Size(483, 215)
        Me.p_title_position.TabIndex = 35
        '
        'rb_tl1
        '
        Me.rb_tl1.Image = Global.DVDArt_Plugin.My.Resources.Resources.title_1
        Me.rb_tl1.Location = New System.Drawing.Point(3, 5)
        Me.rb_tl1.Name = "rb_tl1"
        Me.rb_tl1.Size = New System.Drawing.Size(222, 208)
        Me.rb_tl1.TabIndex = 34
        Me.rb_tl1.TabStop = True
        Me.rb_tl1.UseVisualStyleBackColor = True
        '
        'rb_tl2
        '
        Me.rb_tl2.Image = Global.DVDArt_Plugin.My.Resources.Resources.title_2
        Me.rb_tl2.Location = New System.Drawing.Point(258, 3)
        Me.rb_tl2.Name = "rb_tl2"
        Me.rb_tl2.Size = New System.Drawing.Size(222, 208)
        Me.rb_tl2.TabIndex = 33
        Me.rb_tl2.TabStop = True
        Me.rb_tl2.UseVisualStyleBackColor = True
        '
        'rb_s1
        '
        Me.rb_s1.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo_size_80x56
        Me.rb_s1.Location = New System.Drawing.Point(3, 6)
        Me.rb_s1.Name = "rb_s1"
        Me.rb_s1.Size = New System.Drawing.Size(222, 90)
        Me.rb_s1.TabIndex = 34
        Me.rb_s1.TabStop = True
        Me.rb_s1.UseVisualStyleBackColor = True
        '
        'rb_s2
        '
        Me.rb_s2.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo_size_177x125
        Me.rb_s2.Location = New System.Drawing.Point(258, 6)
        Me.rb_s2.Name = "rb_s2"
        Me.rb_s2.Size = New System.Drawing.Size(222, 90)
        Me.rb_s2.TabIndex = 33
        Me.rb_s2.TabStop = True
        Me.rb_s2.UseVisualStyleBackColor = True
        '
        'rb_t2
        '
        Me.rb_t2.Image = Global.DVDArt_Plugin.My.Resources.Resources.template_2
        Me.rb_t2.Location = New System.Drawing.Point(274, 5)
        Me.rb_t2.Name = "rb_t2"
        Me.rb_t2.Size = New System.Drawing.Size(222, 208)
        Me.rb_t2.TabIndex = 5
        Me.rb_t2.TabStop = True
        Me.rb_t2.UseVisualStyleBackColor = True
        '
        'rb_t1
        '
        Me.rb_t1.Image = Global.DVDArt_Plugin.My.Resources.Resources.template_1
        Me.rb_t1.Location = New System.Drawing.Point(19, 5)
        Me.rb_t1.Name = "rb_t1"
        Me.rb_t1.Size = New System.Drawing.Size(222, 208)
        Me.rb_t1.TabIndex = 4
        Me.rb_t1.TabStop = True
        Me.rb_t1.UseVisualStyleBackColor = True
        '
        'DVDArt_Layout
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(515, 578)
        Me.Controls.Add(Me.p_title_position)
        Me.Controls.Add(Me.p_size_type)
        Me.Controls.Add(Me.l_copyright)
        Me.Controls.Add(Me.b_done)
        Me.Controls.Add(Me.rb_t2)
        Me.Controls.Add(Me.rb_t1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DVDArt_Layout"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change DVDArt default layout for this particular creation"
        Me.p_size_type.ResumeLayout(False)
        Me.p_title_position.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rb_t2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_t1 As System.Windows.Forms.RadioButton
    Friend WithEvents b_done As System.Windows.Forms.Button
    Friend WithEvents l_copyright As System.Windows.Forms.Label
    Friend WithEvents p_size_type As Panel
    Friend WithEvents rb_s1 As RadioButton
    Friend WithEvents rb_s2 As RadioButton
    Friend WithEvents p_title_position As Panel
    Friend WithEvents rb_tl1 As RadioButton
    Friend WithEvents rb_tl2 As RadioButton
End Class
