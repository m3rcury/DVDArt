﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_ManualUpload
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
        Me.FolderBrowserDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.b_dvdart = New System.Windows.Forms.Button()
        Me.tb_dvdart = New System.Windows.Forms.TextBox()
        Me.l_dvdart = New System.Windows.Forms.Label()
        Me.l_clearart = New System.Windows.Forms.Label()
        Me.l_clearlogo = New System.Windows.Forms.Label()
        Me.tb_clearart = New System.Windows.Forms.TextBox()
        Me.tb_clearlogo = New System.Windows.Forms.TextBox()
        Me.b_clearlogo = New System.Windows.Forms.Button()
        Me.b_clearart = New System.Windows.Forms.Button()
        Me.b_upload = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.b_preview_clearlogo = New System.Windows.Forms.Button()
        Me.b_preview_clearart = New System.Windows.Forms.Button()
        Me.b_preview_dvdart = New System.Windows.Forms.Button()
        Me.b_process_dvdart = New System.Windows.Forms.Button()
        Me.b_process_clearlogo = New System.Windows.Forms.Button()
        Me.b_process_clearart = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'b_dvdart
        '
        Me.b_dvdart.Location = New System.Drawing.Point(416, 26)
        Me.b_dvdart.Name = "b_dvdart"
        Me.b_dvdart.Size = New System.Drawing.Size(75, 23)
        Me.b_dvdart.TabIndex = 0
        Me.b_dvdart.Text = "Browse"
        Me.b_dvdart.UseVisualStyleBackColor = True
        '
        'tb_dvdart
        '
        Me.tb_dvdart.Location = New System.Drawing.Point(80, 28)
        Me.tb_dvdart.Name = "tb_dvdart"
        Me.tb_dvdart.Size = New System.Drawing.Size(320, 20)
        Me.tb_dvdart.TabIndex = 1
        '
        'l_dvdart
        '
        Me.l_dvdart.AutoSize = True
        Me.l_dvdart.Location = New System.Drawing.Point(13, 31)
        Me.l_dvdart.Name = "l_dvdart"
        Me.l_dvdart.Size = New System.Drawing.Size(46, 13)
        Me.l_dvdart.TabIndex = 2
        Me.l_dvdart.Text = "DVD Art"
        '
        'l_clearart
        '
        Me.l_clearart.AutoSize = True
        Me.l_clearart.Location = New System.Drawing.Point(13, 76)
        Me.l_clearart.Name = "l_clearart"
        Me.l_clearart.Size = New System.Drawing.Size(47, 13)
        Me.l_clearart.TabIndex = 3
        Me.l_clearart.Text = "Clear Art"
        '
        'l_clearlogo
        '
        Me.l_clearlogo.AutoSize = True
        Me.l_clearlogo.Location = New System.Drawing.Point(13, 122)
        Me.l_clearlogo.Name = "l_clearlogo"
        Me.l_clearlogo.Size = New System.Drawing.Size(58, 13)
        Me.l_clearlogo.TabIndex = 4
        Me.l_clearlogo.Text = "Clear Logo"
        '
        'tb_clearart
        '
        Me.tb_clearart.Location = New System.Drawing.Point(80, 73)
        Me.tb_clearart.Name = "tb_clearart"
        Me.tb_clearart.Size = New System.Drawing.Size(320, 20)
        Me.tb_clearart.TabIndex = 5
        '
        'tb_clearlogo
        '
        Me.tb_clearlogo.Location = New System.Drawing.Point(80, 119)
        Me.tb_clearlogo.Name = "tb_clearlogo"
        Me.tb_clearlogo.Size = New System.Drawing.Size(320, 20)
        Me.tb_clearlogo.TabIndex = 6
        '
        'b_clearlogo
        '
        Me.b_clearlogo.Location = New System.Drawing.Point(416, 117)
        Me.b_clearlogo.Name = "b_clearlogo"
        Me.b_clearlogo.Size = New System.Drawing.Size(75, 23)
        Me.b_clearlogo.TabIndex = 7
        Me.b_clearlogo.Text = "Browse"
        Me.b_clearlogo.UseVisualStyleBackColor = True
        '
        'b_clearart
        '
        Me.b_clearart.Location = New System.Drawing.Point(416, 71)
        Me.b_clearart.Name = "b_clearart"
        Me.b_clearart.Size = New System.Drawing.Size(75, 23)
        Me.b_clearart.TabIndex = 8
        Me.b_clearart.Text = "Browse"
        Me.b_clearart.UseVisualStyleBackColor = True
        '
        'b_upload
        '
        Me.b_upload.Location = New System.Drawing.Point(248, 189)
        Me.b_upload.Name = "b_upload"
        Me.b_upload.Size = New System.Drawing.Size(75, 23)
        Me.b_upload.TabIndex = 21
        Me.b_upload.Text = "Upload"
        Me.b_upload.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label4.Location = New System.Drawing.Point(2, 213)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Copyright © 2012, m3rcury"
        '
        'b_preview_clearlogo
        '
        Me.b_preview_clearlogo.Image = Global.DVDArt_Plugin.My.Resources.Resources.preview
        Me.b_preview_clearlogo.Location = New System.Drawing.Point(512, 108)
        Me.b_preview_clearlogo.Name = "b_preview_clearlogo"
        Me.b_preview_clearlogo.Size = New System.Drawing.Size(40, 40)
        Me.b_preview_clearlogo.TabIndex = 20
        Me.b_preview_clearlogo.UseVisualStyleBackColor = True
        Me.b_preview_clearlogo.Visible = False
        '
        'b_preview_clearart
        '
        Me.b_preview_clearart.Image = Global.DVDArt_Plugin.My.Resources.Resources.preview
        Me.b_preview_clearart.Location = New System.Drawing.Point(512, 62)
        Me.b_preview_clearart.Name = "b_preview_clearart"
        Me.b_preview_clearart.Size = New System.Drawing.Size(40, 40)
        Me.b_preview_clearart.TabIndex = 19
        Me.b_preview_clearart.UseVisualStyleBackColor = True
        Me.b_preview_clearart.Visible = False
        '
        'b_preview_dvdart
        '
        Me.b_preview_dvdart.Image = Global.DVDArt_Plugin.My.Resources.Resources.preview
        Me.b_preview_dvdart.Location = New System.Drawing.Point(512, 17)
        Me.b_preview_dvdart.Name = "b_preview_dvdart"
        Me.b_preview_dvdart.Size = New System.Drawing.Size(40, 40)
        Me.b_preview_dvdart.TabIndex = 18
        Me.b_preview_dvdart.UseVisualStyleBackColor = True
        Me.b_preview_dvdart.Visible = False
        '
        'b_process_dvdart
        '
        Me.b_process_dvdart.Image = Global.DVDArt_Plugin.My.Resources.Resources.process
        Me.b_process_dvdart.Location = New System.Drawing.Point(512, 17)
        Me.b_process_dvdart.Name = "b_process_dvdart"
        Me.b_process_dvdart.Size = New System.Drawing.Size(40, 40)
        Me.b_process_dvdart.TabIndex = 26
        Me.b_process_dvdart.UseVisualStyleBackColor = True
        Me.b_process_dvdart.Visible = False
        '
        'b_process_clearlogo
        '
        Me.b_process_clearlogo.Image = Global.DVDArt_Plugin.My.Resources.Resources.process
        Me.b_process_clearlogo.Location = New System.Drawing.Point(512, 108)
        Me.b_process_clearlogo.Name = "b_process_clearlogo"
        Me.b_process_clearlogo.Size = New System.Drawing.Size(40, 40)
        Me.b_process_clearlogo.TabIndex = 27
        Me.b_process_clearlogo.UseVisualStyleBackColor = True
        Me.b_process_clearlogo.Visible = False
        '
        'b_process_clearart
        '
        Me.b_process_clearart.Image = Global.DVDArt_Plugin.My.Resources.Resources.process
        Me.b_process_clearart.Location = New System.Drawing.Point(512, 62)
        Me.b_process_clearart.Name = "b_process_clearart"
        Me.b_process_clearart.Size = New System.Drawing.Size(40, 40)
        Me.b_process_clearart.TabIndex = 28
        Me.b_process_clearart.UseVisualStyleBackColor = True
        Me.b_process_clearart.Visible = False
        '
        'DVDArt_ManualUpload
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 228)
        Me.Controls.Add(Me.b_process_clearart)
        Me.Controls.Add(Me.b_process_clearlogo)
        Me.Controls.Add(Me.b_process_dvdart)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.b_upload)
        Me.Controls.Add(Me.b_preview_clearlogo)
        Me.Controls.Add(Me.b_preview_clearart)
        Me.Controls.Add(Me.b_preview_dvdart)
        Me.Controls.Add(Me.b_clearart)
        Me.Controls.Add(Me.b_clearlogo)
        Me.Controls.Add(Me.tb_clearlogo)
        Me.Controls.Add(Me.tb_clearart)
        Me.Controls.Add(Me.l_clearlogo)
        Me.Controls.Add(Me.l_clearart)
        Me.Controls.Add(Me.l_dvdart)
        Me.Controls.Add(Me.tb_dvdart)
        Me.Controls.Add(Me.b_dvdart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "DVDArt_ManualUpload"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Upload Images"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FolderBrowserDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents b_dvdart As System.Windows.Forms.Button
    Friend WithEvents tb_dvdart As System.Windows.Forms.TextBox
    Friend WithEvents l_dvdart As System.Windows.Forms.Label
    Friend WithEvents l_clearart As System.Windows.Forms.Label
    Friend WithEvents l_clearlogo As System.Windows.Forms.Label
    Friend WithEvents tb_clearart As System.Windows.Forms.TextBox
    Friend WithEvents tb_clearlogo As System.Windows.Forms.TextBox
    Friend WithEvents b_clearlogo As System.Windows.Forms.Button
    Friend WithEvents b_clearart As System.Windows.Forms.Button
    Friend WithEvents b_preview_clearart As System.Windows.Forms.Button
    Friend WithEvents b_preview_clearlogo As System.Windows.Forms.Button
    Friend WithEvents b_upload As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents b_preview_dvdart As System.Windows.Forms.Button
    Friend WithEvents b_process_dvdart As System.Windows.Forms.Button
    Friend WithEvents b_process_clearlogo As System.Windows.Forms.Button
    Friend WithEvents b_process_clearart As System.Windows.Forms.Button
End Class