<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_SplashScreen
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
        Me.l_plugin = New System.Windows.Forms.Label()
        Me.l_version = New System.Windows.Forms.Label()
        Me.l_copyright = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'l_plugin
        '
        Me.l_plugin.AutoSize = True
        Me.l_plugin.BackColor = System.Drawing.Color.White
        Me.l_plugin.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_plugin.Location = New System.Drawing.Point(148, 13)
        Me.l_plugin.Name = "l_plugin"
        Me.l_plugin.Size = New System.Drawing.Size(247, 37)
        Me.l_plugin.TabIndex = 1
        Me.l_plugin.Text = "DVDArt_Plugin"
        Me.l_plugin.UseWaitCursor = True
        '
        'l_version
        '
        Me.l_version.AutoSize = True
        Me.l_version.BackColor = System.Drawing.Color.White
        Me.l_version.Location = New System.Drawing.Point(155, 54)
        Me.l_version.Name = "l_version"
        Me.l_version.Size = New System.Drawing.Size(46, 13)
        Me.l_version.TabIndex = 2
        Me.l_version.Text = "v1.0.0.8"
        Me.l_version.UseWaitCursor = True
        '
        'l_copyright
        '
        Me.l_copyright.AutoSize = True
        Me.l_copyright.BackColor = System.Drawing.Color.White
        Me.l_copyright.Location = New System.Drawing.Point(370, 128)
        Me.l_copyright.Name = "l_copyright"
        Me.l_copyright.Size = New System.Drawing.Size(139, 13)
        Me.l_copyright.TabIndex = 3
        Me.l_copyright.Text = "Copyright ©  2012 - m3rcury"
        Me.l_copyright.TextAlign = System.Drawing.ContentAlignment.BottomRight
        Me.l_copyright.UseWaitCursor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(0, -6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(526, 152)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.UseWaitCursor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.Image = Global.DVDArt_Plugin.My.Resources.Resources.movies
        Me.PictureBox1.Location = New System.Drawing.Point(13, 13)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(128, 128)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.UseWaitCursor = True
        '
        'DVDArt_SplashScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.WhiteSmoke
        Me.ClientSize = New System.Drawing.Size(529, 147)
        Me.Controls.Add(Me.l_copyright)
        Me.Controls.Add(Me.l_version)
        Me.Controls.Add(Me.l_plugin)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DVDArt_SplashScreen"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Splash"
        Me.UseWaitCursor = True
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents l_version As System.Windows.Forms.Label
    Friend WithEvents l_copyright As System.Windows.Forms.Label
    Friend WithEvents l_plugin As System.Windows.Forms.Label
    Friend WithEvents ShapeContainer1 As Microsoft.VisualBasic.Devices.ServerComputer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
