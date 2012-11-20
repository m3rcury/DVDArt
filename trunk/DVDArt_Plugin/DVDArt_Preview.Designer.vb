<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_Preview
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
        Me.pb_preview = New System.Windows.Forms.PictureBox()
        Me.l_copyright = New System.Windows.Forms.Label()
        CType(Me.pb_preview, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pb_preview
        '
        Me.pb_preview.Location = New System.Drawing.Point(2, 1)
        Me.pb_preview.Name = "pb_preview"
        Me.pb_preview.Size = New System.Drawing.Size(500, 500)
        Me.pb_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_preview.TabIndex = 0
        Me.pb_preview.TabStop = False
        '
        'l_copyright
        '
        Me.l_copyright.AutoSize = True
        Me.l_copyright.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_copyright.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.l_copyright.Location = New System.Drawing.Point(2, 488)
        Me.l_copyright.Name = "l_copyright"
        Me.l_copyright.Size = New System.Drawing.Size(133, 13)
        Me.l_copyright.TabIndex = 25
        Me.l_copyright.Text = "Copyright © 2012, m3rcury"
        '
        'DVDArt_Preview
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(504, 503)
        Me.Controls.Add(Me.l_copyright)
        Me.Controls.Add(Me.pb_preview)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DVDArt_Preview"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Preview"
        CType(Me.pb_preview, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pb_preview As System.Windows.Forms.PictureBox
    Friend WithEvents l_copyright As System.Windows.Forms.Label
End Class
