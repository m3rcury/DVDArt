<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_ChangeMBID
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
        Me.tb_MBID = New System.Windows.Forms.TextBox()
        Me.b_done = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'tb_MBID
        '
        Me.tb_MBID.Location = New System.Drawing.Point(34, 27)
        Me.tb_MBID.Name = "tb_MBID"
        Me.tb_MBID.Size = New System.Drawing.Size(216, 20)
        Me.tb_MBID.TabIndex = 0
        Me.tb_MBID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'b_done
        '
        Me.b_done.Location = New System.Drawing.Point(105, 63)
        Me.b_done.Name = "b_done"
        Me.b_done.Size = New System.Drawing.Size(75, 23)
        Me.b_done.TabIndex = 28
        Me.b_done.Text = "Done"
        Me.b_done.UseVisualStyleBackColor = True
        '
        'DVDArt_ChangeMBID
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 98)
        Me.Controls.Add(Me.b_done)
        Me.Controls.Add(Me.tb_MBID)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DVDArt_ChangeMBID"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Change MBID"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tb_MBID As System.Windows.Forms.TextBox
    Friend WithEvents b_done As System.Windows.Forms.Button
End Class
