<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_CoverArt
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
        Me.components = New System.ComponentModel.Container()
        Dim tp_preview As System.Windows.Forms.TabPage
        Me.pb_coverart = New System.Windows.Forms.PictureBox()
        Me.l_copyright = New System.Windows.Forms.Label()
        Me.b_done = New System.Windows.Forms.Button()
        Me.lv_coverart = New System.Windows.Forms.ListView()
        Me.il_coverart = New System.Windows.Forms.ImageList(Me.components)
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tt_preview = New System.Windows.Forms.ToolTip(Me.components)
        Me.b_preview = New System.Windows.Forms.Button()
        tp_preview = New System.Windows.Forms.TabPage()
        tp_preview.SuspendLayout()
        CType(Me.pb_coverart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tp_preview
        '
        tp_preview.BackColor = System.Drawing.SystemColors.ButtonFace
        tp_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        tp_preview.Controls.Add(Me.pb_coverart)
        tp_preview.Location = New System.Drawing.Point(4, 22)
        tp_preview.Name = "tp_preview"
        tp_preview.Padding = New System.Windows.Forms.Padding(3)
        tp_preview.Size = New System.Drawing.Size(504, 504)
        tp_preview.TabIndex = 1
        '
        'pb_coverart
        '
        Me.pb_coverart.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.pb_coverart.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pb_coverart.Cursor = System.Windows.Forms.Cursors.SizeAll
        Me.pb_coverart.Location = New System.Drawing.Point(2, 2)
        Me.pb_coverart.Name = "pb_coverart"
        Me.pb_coverart.Size = New System.Drawing.Size(500, 500)
        Me.pb_coverart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb_coverart.TabIndex = 1
        Me.pb_coverart.TabStop = False
        Me.tt_preview.SetToolTip(Me.pb_coverart, "Use mouse to position coverart for best visibility." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Image has been resized for" & _
        " best fit, and only vertical" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "movement is allowed.")
        '
        'l_copyright
        '
        Me.l_copyright.AutoSize = True
        Me.l_copyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_copyright.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.l_copyright.Location = New System.Drawing.Point(2, 527)
        Me.l_copyright.Name = "l_copyright"
        Me.l_copyright.Size = New System.Drawing.Size(133, 13)
        Me.l_copyright.TabIndex = 25
        Me.l_copyright.Text = "Copyright © 2012, m3rcury"
        '
        'b_done
        '
        Me.b_done.Location = New System.Drawing.Point(254, 511)
        Me.b_done.Name = "b_done"
        Me.b_done.Size = New System.Drawing.Size(75, 23)
        Me.b_done.TabIndex = 26
        Me.b_done.Text = "Done"
        Me.b_done.UseVisualStyleBackColor = True
        '
        'lv_coverart
        '
        Me.lv_coverart.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.lv_coverart.LargeImageList = Me.il_coverart
        Me.lv_coverart.Location = New System.Drawing.Point(509, 1)
        Me.lv_coverart.MultiSelect = False
        Me.lv_coverart.Name = "lv_coverart"
        Me.lv_coverart.Size = New System.Drawing.Size(197, 539)
        Me.lv_coverart.TabIndex = 27
        Me.lv_coverart.UseCompatibleStateImageBehavior = False
        '
        'il_coverart
        '
        Me.il_coverart.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_coverart.ImageSize = New System.Drawing.Size(128, 192)
        Me.il_coverart.TransparentColor = System.Drawing.Color.Transparent
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(tp_preview)
        Me.TabControl1.Location = New System.Drawing.Point(-1, -21)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(512, 530)
        Me.TabControl1.TabIndex = 28
        '
        'b_preview
        '
        Me.b_preview.Location = New System.Drawing.Point(173, 511)
        Me.b_preview.Name = "b_preview"
        Me.b_preview.Size = New System.Drawing.Size(75, 23)
        Me.b_preview.TabIndex = 29
        Me.b_preview.Text = "Preview"
        Me.b_preview.UseVisualStyleBackColor = True
        '
        'DVDArt_CoverArt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 541)
        Me.Controls.Add(Me.b_preview)
        Me.Controls.Add(Me.lv_coverart)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.b_done)
        Me.Controls.Add(Me.l_copyright)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DVDArt_CoverArt"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Use CoverArt"
        tp_preview.ResumeLayout(False)
        tp_preview.PerformLayout()
        CType(Me.pb_coverart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents l_copyright As System.Windows.Forms.Label
    Friend WithEvents b_done As System.Windows.Forms.Button
    Friend WithEvents lv_coverart As System.Windows.Forms.ListView
    Friend WithEvents il_coverart As System.Windows.Forms.ImageList
    Friend WithEvents pb_coverart As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tt_preview As System.Windows.Forms.ToolTip
    Friend WithEvents b_preview As System.Windows.Forms.Button
End Class
