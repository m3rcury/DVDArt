<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt_GUI
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DVDArt_GUI))
        Me.il_dvdart = New System.Windows.Forms.ImageList(Me.components)
        Me.cms_found = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshArtworkFromOnlineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManuallyUploadArtworkToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectCoverArtForDVDArtToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectCoverArtForDVDArtToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.il_clearart = New System.Windows.Forms.ImageList(Me.components)
        Me.il_clearlogo = New System.Windows.Forms.ImageList(Me.components)
        Me.cms_import = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RestartImporterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.il_state = New System.Windows.Forms.ImageList(Me.components)
        Me.cms_missing = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectCoverArtForDVDArtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseCoverArtForDVDArtToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.bw_import = New System.ComponentModel.BackgroundWorker()
        Me.tbc_main = New System.Windows.Forms.TabControl()
        Me.tp_MovingPictures = New System.Windows.Forms.TabPage()
        Me.pb_movingpictures = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tp1 = New System.Windows.Forms.TabPage()
        Me.lv_movies = New System.Windows.Forms.ListView()
        Me.Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_movies = New System.Windows.Forms.TabControl()
        Me.tp_Movie_DVDArt = New System.Windows.Forms.TabPage()
        Me.b_movie_preview = New System.Windows.Forms.Button()
        Me.b_movie_compress = New System.Windows.Forms.Button()
        Me.l_movie_size = New System.Windows.Forms.Label()
        Me.lv_movie_dvdart = New System.Windows.Forms.ListView()
        Me.l_imdb_id = New System.Windows.Forms.Label()
        Me.pb_movie_dvdart = New System.Windows.Forms.PictureBox()
        Me.tp_Movie_ClearArt = New System.Windows.Forms.TabPage()
        Me.b_movie_deleteart = New System.Windows.Forms.Button()
        Me.pb_movie_clearart = New System.Windows.Forms.PictureBox()
        Me.lv_movie_clearart = New System.Windows.Forms.ListView()
        Me.tp_Movie_ClearLogo = New System.Windows.Forms.TabPage()
        Me.b_movie_deletelogo = New System.Windows.Forms.Button()
        Me.pb_movie_clearlogo = New System.Windows.Forms.PictureBox()
        Me.lv_movie_clearlogo = New System.Windows.Forms.ListView()
        Me.tp3 = New System.Windows.Forms.TabPage()
        Me.lv_movies_missing = New System.Windows.Forms.ListView()
        Me.m_Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_DVDArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_ClearArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pbFTV_Logo = New System.Windows.Forms.PictureBox()
        Me.tp_TVSeries = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lv_series = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_series = New System.Windows.Forms.TabControl()
        Me.tp_Serie_ClearArt = New System.Windows.Forms.TabPage()
        Me.l_thetvdb_id = New System.Windows.Forms.Label()
        Me.b_serie_deleteart = New System.Windows.Forms.Button()
        Me.pb_serie_clearart = New System.Windows.Forms.PictureBox()
        Me.lv_serie_clearart = New System.Windows.Forms.ListView()
        Me.tp_Serie_ClearLogo = New System.Windows.Forms.TabPage()
        Me.b_serie_deletelogo = New System.Windows.Forms.Button()
        Me.pb_serie_clearlogo = New System.Windows.Forms.PictureBox()
        Me.lv_serie_clearlogo = New System.Windows.Forms.ListView()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.lv_series_missing = New System.Windows.Forms.ListView()
        Me.c_Serie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_ClearArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.tp_importer = New System.Windows.Forms.TabPage()
        Me.lv_import = New System.Windows.Forms.ListView()
        Me.i_Movie_Series = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tp_Settings = New System.Windows.Forms.TabPage()
        Me.gb2 = New System.Windows.Forms.GroupBox()
        Me.cb_language = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.pb2 = New System.Windows.Forms.PictureBox()
        Me.pb3 = New System.Windows.Forms.PictureBox()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.cb_ClearLogo = New System.Windows.Forms.CheckBox()
        Me.cb_ClearArt = New System.Windows.Forms.CheckBox()
        Me.cb_DVDArt = New System.Windows.Forms.CheckBox()
        Me.gb1 = New System.Windows.Forms.GroupBox()
        Me.cb_missing = New System.Windows.Forms.ComboBox()
        Me.nud_missing = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_scraping = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nud_scraping = New System.Windows.Forms.NumericUpDown()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cb_delay = New System.Windows.Forms.ComboBox()
        Me.nud_delay = New System.Windows.Forms.NumericUpDown()
        Me.mtb_cpu = New System.Windows.Forms.MaskedTextBox()
        Me.il_column = New System.Windows.Forms.ImageList(Me.components)
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MUploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendToImporterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_found.SuspendLayout()
        Me.cms_import.SuspendLayout()
        Me.cms_missing.SuspendLayout()
        Me.tbc_main.SuspendLayout()
        Me.tp_MovingPictures.SuspendLayout()
        CType(Me.pb_movingpictures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tp1.SuspendLayout()
        Me.tbc_movies.SuspendLayout()
        Me.tp_Movie_DVDArt.SuspendLayout()
        CType(Me.pb_movie_dvdart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Movie_ClearArt.SuspendLayout()
        CType(Me.pb_movie_clearart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Movie_ClearLogo.SuspendLayout()
        CType(Me.pb_movie_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp3.SuspendLayout()
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_TVSeries.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.tbc_series.SuspendLayout()
        Me.tp_Serie_ClearArt.SuspendLayout()
        CType(Me.pb_serie_clearart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Serie_ClearLogo.SuspendLayout()
        CType(Me.pb_serie_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage5.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_importer.SuspendLayout()
        Me.tp_Settings.SuspendLayout()
        Me.gb2.SuspendLayout()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb1.SuspendLayout()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'il_dvdart
        '
        Me.il_dvdart.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_dvdart.ImageSize = New System.Drawing.Size(200, 200)
        Me.il_dvdart.TransparentColor = System.Drawing.Color.Transparent
        '
        'cms_found
        '
        Me.cms_found.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshArtworkFromOnlineToolStripMenuItem, Me.ManuallyUploadArtworkToolStripMenuItem, Me.SelectCoverArtForDVDArtToolStripMenuItem2})
        Me.cms_found.Name = "cms_movies"
        Me.cms_found.Size = New System.Drawing.Size(247, 70)
        '
        'RefreshArtworkFromOnlineToolStripMenuItem
        '
        Me.RefreshArtworkFromOnlineToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.movie_search
        Me.RefreshArtworkFromOnlineToolStripMenuItem.Name = "RefreshArtworkFromOnlineToolStripMenuItem"
        Me.RefreshArtworkFromOnlineToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.RefreshArtworkFromOnlineToolStripMenuItem.Text = "Refresh artwork from online"
        Me.RefreshArtworkFromOnlineToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ManuallyUploadArtworkToolStripMenuItem
        '
        Me.ManuallyUploadArtworkToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.ManuallyUploadArtworkToolStripMenuItem.Name = "ManuallyUploadArtworkToolStripMenuItem"
        Me.ManuallyUploadArtworkToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.ManuallyUploadArtworkToolStripMenuItem.Text = "Manually Upload Artwork"
        Me.ManuallyUploadArtworkToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SelectCoverArtForDVDArtToolStripMenuItem2
        '
        Me.SelectCoverArtForDVDArtToolStripMenuItem2.Image = Global.DVDArt_Plugin.My.Resources.Resources.selectcoverart
        Me.SelectCoverArtForDVDArtToolStripMenuItem2.Name = "SelectCoverArtForDVDArtToolStripMenuItem2"
        Me.SelectCoverArtForDVDArtToolStripMenuItem2.Size = New System.Drawing.Size(246, 22)
        Me.SelectCoverArtForDVDArtToolStripMenuItem2.Text = "Select/Edit Cover Art for DVD Art"
        Me.SelectCoverArtForDVDArtToolStripMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SelectCoverArtForDVDArtToolStripMenuItem1
        '
        Me.SelectCoverArtForDVDArtToolStripMenuItem1.Name = "SelectCoverArtForDVDArtToolStripMenuItem1"
        Me.SelectCoverArtForDVDArtToolStripMenuItem1.Size = New System.Drawing.Size(32, 19)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(1, 683)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Copyright © 2012, m3rcury"
        '
        'il_clearart
        '
        Me.il_clearart.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_clearart.ImageSize = New System.Drawing.Size(200, 112)
        Me.il_clearart.TransparentColor = System.Drawing.Color.Transparent
        '
        'il_clearlogo
        '
        Me.il_clearlogo.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_clearlogo.ImageSize = New System.Drawing.Size(200, 77)
        Me.il_clearlogo.TransparentColor = System.Drawing.Color.Transparent
        '
        'cms_import
        '
        Me.cms_import.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartImporterToolStripMenuItem})
        Me.cms_import.Name = "cms_movies"
        Me.cms_import.Size = New System.Drawing.Size(172, 26)
        '
        'RestartImporterToolStripMenuItem
        '
        Me.RestartImporterToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RestartImporterToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.restart
        Me.RestartImporterToolStripMenuItem.Name = "RestartImporterToolStripMenuItem"
        Me.RestartImporterToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.RestartImporterToolStripMenuItem.Text = "Restart Importer"
        Me.RestartImporterToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'il_state
        '
        Me.il_state.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_state.ImageSize = New System.Drawing.Size(16, 16)
        Me.il_state.TransparentColor = System.Drawing.Color.Transparent
        '
        'cms_missing
        '
        Me.cms_missing.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendToolStripMenuItem, Me.RescanToolStripMenuItem, Me.UploadToolStripMenuItem, Me.SelectCoverArtForDVDArtToolStripMenuItem, Me.UseCoverArtForDVDArtToolStripMenuItem})
        Me.cms_missing.Name = "cms_missing"
        Me.cms_missing.Size = New System.Drawing.Size(247, 114)
        '
        'SendToolStripMenuItem
        '
        Me.SendToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.arrow
        Me.SendToolStripMenuItem.Name = "SendToolStripMenuItem"
        Me.SendToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.SendToolStripMenuItem.Text = "Send to importer"
        Me.SendToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RescanToolStripMenuItem
        '
        Me.RescanToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.rescan_movies
        Me.RescanToolStripMenuItem.Name = "RescanToolStripMenuItem"
        Me.RescanToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.RescanToolStripMenuItem.Text = "Rescan ALL missing"
        Me.RescanToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UploadToolStripMenuItem
        '
        Me.UploadToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.UploadToolStripMenuItem.Name = "UploadToolStripMenuItem"
        Me.UploadToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.UploadToolStripMenuItem.Text = "Manually Upload Artwork"
        Me.UploadToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SelectCoverArtForDVDArtToolStripMenuItem
        '
        Me.SelectCoverArtForDVDArtToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.selectcoverart
        Me.SelectCoverArtForDVDArtToolStripMenuItem.Name = "SelectCoverArtForDVDArtToolStripMenuItem"
        Me.SelectCoverArtForDVDArtToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.SelectCoverArtForDVDArtToolStripMenuItem.Text = "Select/Edit Cover Art for DVD Art"
        '
        'UseCoverArtForDVDArtToolStripMenuItem
        '
        Me.UseCoverArtForDVDArtToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.convert1
        Me.UseCoverArtForDVDArtToolStripMenuItem.Name = "UseCoverArtForDVDArtToolStripMenuItem"
        Me.UseCoverArtForDVDArtToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.UseCoverArtForDVDArtToolStripMenuItem.Text = "Use Cover Art for DVD Art"
        '
        'ChangeIMDBTMDBNumberToolStripMenuItem1
        '
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1.Name = "ChangeIMDBTMDBNumberToolStripMenuItem1"
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1.Size = New System.Drawing.Size(232, 22)
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1.Text = "Change IMDB/TMDB Number"
        '
        'bw_import
        '
        '
        'tbc_main
        '
        Me.tbc_main.Controls.Add(Me.tp_MovingPictures)
        Me.tbc_main.Controls.Add(Me.tp_TVSeries)
        Me.tbc_main.Controls.Add(Me.tp_importer)
        Me.tbc_main.Controls.Add(Me.tp_Settings)
        Me.tbc_main.Location = New System.Drawing.Point(12, 4)
        Me.tbc_main.Name = "tbc_main"
        Me.tbc_main.SelectedIndex = 0
        Me.tbc_main.Size = New System.Drawing.Size(664, 676)
        Me.tbc_main.TabIndex = 29
        '
        'tp_MovingPictures
        '
        Me.tp_MovingPictures.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_MovingPictures.Controls.Add(Me.pb_movingpictures)
        Me.tp_MovingPictures.Controls.Add(Me.TabControl1)
        Me.tp_MovingPictures.Controls.Add(Me.pbFTV_Logo)
        Me.tp_MovingPictures.Location = New System.Drawing.Point(4, 22)
        Me.tp_MovingPictures.Name = "tp_MovingPictures"
        Me.tp_MovingPictures.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_MovingPictures.Size = New System.Drawing.Size(656, 650)
        Me.tp_MovingPictures.TabIndex = 0
        Me.tp_MovingPictures.Text = "Movies"
        '
        'pb_movingpictures
        '
        Me.pb_movingpictures.BackColor = System.Drawing.Color.Transparent
        Me.pb_movingpictures.Image = Global.DVDArt_Plugin.My.Resources.Resources.movingpictures
        Me.pb_movingpictures.Location = New System.Drawing.Point(259, 596)
        Me.pb_movingpictures.Name = "pb_movingpictures"
        Me.pb_movingpictures.Size = New System.Drawing.Size(208, 59)
        Me.pb_movingpictures.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_movingpictures.TabIndex = 32
        Me.pb_movingpictures.TabStop = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tp1)
        Me.TabControl1.Controls.Add(Me.tp3)
        Me.TabControl1.Location = New System.Drawing.Point(4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(650, 593)
        Me.TabControl1.TabIndex = 30
        '
        'tp1
        '
        Me.tp1.BackColor = System.Drawing.SystemColors.Control
        Me.tp1.Controls.Add(Me.lv_movies)
        Me.tp1.Controls.Add(Me.tbc_movies)
        Me.tp1.Location = New System.Drawing.Point(4, 22)
        Me.tp1.Name = "tp1"
        Me.tp1.Padding = New System.Windows.Forms.Padding(3)
        Me.tp1.Size = New System.Drawing.Size(642, 567)
        Me.tp1.TabIndex = 0
        Me.tp1.Text = "Movies with Artwork"
        Me.tp1.ToolTipText = "This tab contails all movies that already have a DVD artwork."
        '
        'lv_movies
        '
        Me.lv_movies.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movies.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Movie, Me.IMDb_id})
        Me.lv_movies.ContextMenuStrip = Me.cms_found
        Me.lv_movies.FullRowSelect = True
        Me.lv_movies.GridLines = True
        Me.lv_movies.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_movies.Location = New System.Drawing.Point(8, 8)
        Me.lv_movies.Name = "lv_movies"
        Me.lv_movies.Size = New System.Drawing.Size(340, 550)
        Me.lv_movies.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_movies.TabIndex = 11
        Me.lv_movies.UseCompatibleStateImageBehavior = False
        Me.lv_movies.View = System.Windows.Forms.View.Details
        '
        'Movie
        '
        Me.Movie.Text = "Movie"
        Me.Movie.Width = 319
        '
        'IMDb_id
        '
        Me.IMDb_id.Text = "IMDb ID"
        Me.IMDb_id.Width = 74
        '
        'tbc_movies
        '
        Me.tbc_movies.Controls.Add(Me.tp_Movie_DVDArt)
        Me.tbc_movies.Controls.Add(Me.tp_Movie_ClearArt)
        Me.tbc_movies.Controls.Add(Me.tp_Movie_ClearLogo)
        Me.tbc_movies.Location = New System.Drawing.Point(352, 8)
        Me.tbc_movies.Name = "tbc_movies"
        Me.tbc_movies.SelectedIndex = 0
        Me.tbc_movies.Size = New System.Drawing.Size(284, 553)
        Me.tbc_movies.TabIndex = 12
        '
        'tp_Movie_DVDArt
        '
        Me.tp_Movie_DVDArt.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Movie_DVDArt.Controls.Add(Me.b_movie_preview)
        Me.tp_Movie_DVDArt.Controls.Add(Me.b_movie_compress)
        Me.tp_Movie_DVDArt.Controls.Add(Me.l_movie_size)
        Me.tp_Movie_DVDArt.Controls.Add(Me.lv_movie_dvdart)
        Me.tp_Movie_DVDArt.Controls.Add(Me.l_imdb_id)
        Me.tp_Movie_DVDArt.Controls.Add(Me.pb_movie_dvdart)
        Me.tp_Movie_DVDArt.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_DVDArt.Name = "tp_Movie_DVDArt"
        Me.tp_Movie_DVDArt.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Movie_DVDArt.Size = New System.Drawing.Size(276, 527)
        Me.tp_Movie_DVDArt.TabIndex = 0
        Me.tp_Movie_DVDArt.Text = "DVDArt"
        '
        'b_movie_preview
        '
        Me.b_movie_preview.Image = Global.DVDArt_Plugin.My.Resources.Resources.preview
        Me.b_movie_preview.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_preview.Name = "b_movie_preview"
        Me.b_movie_preview.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_preview.TabIndex = 17
        Me.b_movie_preview.UseVisualStyleBackColor = True
        Me.b_movie_preview.Visible = False
        '
        'b_movie_compress
        '
        Me.b_movie_compress.Image = Global.DVDArt_Plugin.My.Resources.Resources.compress
        Me.b_movie_compress.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_compress.Name = "b_movie_compress"
        Me.b_movie_compress.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_compress.TabIndex = 16
        Me.b_movie_compress.UseVisualStyleBackColor = True
        Me.b_movie_compress.Visible = False
        '
        'l_movie_size
        '
        Me.l_movie_size.AutoSize = True
        Me.l_movie_size.Location = New System.Drawing.Point(9, 195)
        Me.l_movie_size.Name = "l_movie_size"
        Me.l_movie_size.Size = New System.Drawing.Size(24, 13)
        Me.l_movie_size.TabIndex = 14
        Me.l_movie_size.Text = "0x0"
        Me.l_movie_size.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lv_movie_dvdart
        '
        Me.lv_movie_dvdart.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movie_dvdart.BackColor = System.Drawing.SystemColors.Control
        Me.lv_movie_dvdart.ForeColor = System.Drawing.Color.Black
        Me.lv_movie_dvdart.LargeImageList = Me.il_dvdart
        Me.lv_movie_dvdart.Location = New System.Drawing.Point(12, 215)
        Me.lv_movie_dvdart.MultiSelect = False
        Me.lv_movie_dvdart.Name = "lv_movie_dvdart"
        Me.lv_movie_dvdart.Size = New System.Drawing.Size(252, 306)
        Me.lv_movie_dvdart.TabIndex = 13
        Me.lv_movie_dvdart.UseCompatibleStateImageBehavior = False
        '
        'l_imdb_id
        '
        Me.l_imdb_id.AutoSize = True
        Me.l_imdb_id.Location = New System.Drawing.Point(203, 195)
        Me.l_imdb_id.Name = "l_imdb_id"
        Me.l_imdb_id.Size = New System.Drawing.Size(61, 13)
        Me.l_imdb_id.TabIndex = 12
        Me.l_imdb_id.Text = "tt00000000"
        Me.l_imdb_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pb_movie_dvdart
        '
        Me.pb_movie_dvdart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_movie_dvdart.Location = New System.Drawing.Point(34, 8)
        Me.pb_movie_dvdart.Name = "pb_movie_dvdart"
        Me.pb_movie_dvdart.Size = New System.Drawing.Size(200, 200)
        Me.pb_movie_dvdart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_movie_dvdart.TabIndex = 11
        Me.pb_movie_dvdart.TabStop = False
        '
        'tp_Movie_ClearArt
        '
        Me.tp_Movie_ClearArt.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Movie_ClearArt.Controls.Add(Me.b_movie_deleteart)
        Me.tp_Movie_ClearArt.Controls.Add(Me.pb_movie_clearart)
        Me.tp_Movie_ClearArt.Controls.Add(Me.lv_movie_clearart)
        Me.tp_Movie_ClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_ClearArt.Name = "tp_Movie_ClearArt"
        Me.tp_Movie_ClearArt.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Movie_ClearArt.Size = New System.Drawing.Size(276, 527)
        Me.tp_Movie_ClearArt.TabIndex = 1
        Me.tp_Movie_ClearArt.Text = "ClearArt"
        '
        'b_movie_deleteart
        '
        Me.b_movie_deleteart.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_movie_deleteart.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_deleteart.Name = "b_movie_deleteart"
        Me.b_movie_deleteart.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_deleteart.TabIndex = 17
        Me.b_movie_deleteart.UseVisualStyleBackColor = True
        Me.b_movie_deleteart.Visible = False
        '
        'pb_movie_clearart
        '
        Me.pb_movie_clearart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_movie_clearart.Location = New System.Drawing.Point(34, 7)
        Me.pb_movie_clearart.Name = "pb_movie_clearart"
        Me.pb_movie_clearart.Size = New System.Drawing.Size(200, 112)
        Me.pb_movie_clearart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_movie_clearart.TabIndex = 14
        Me.pb_movie_clearart.TabStop = False
        '
        'lv_movie_clearart
        '
        Me.lv_movie_clearart.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movie_clearart.BackColor = System.Drawing.SystemColors.Control
        Me.lv_movie_clearart.ForeColor = System.Drawing.Color.Black
        Me.lv_movie_clearart.LargeImageList = Me.il_clearart
        Me.lv_movie_clearart.Location = New System.Drawing.Point(12, 125)
        Me.lv_movie_clearart.MultiSelect = False
        Me.lv_movie_clearart.Name = "lv_movie_clearart"
        Me.lv_movie_clearart.Size = New System.Drawing.Size(252, 395)
        Me.lv_movie_clearart.TabIndex = 16
        Me.lv_movie_clearart.UseCompatibleStateImageBehavior = False
        '
        'tp_Movie_ClearLogo
        '
        Me.tp_Movie_ClearLogo.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Movie_ClearLogo.Controls.Add(Me.b_movie_deletelogo)
        Me.tp_Movie_ClearLogo.Controls.Add(Me.pb_movie_clearlogo)
        Me.tp_Movie_ClearLogo.Controls.Add(Me.lv_movie_clearlogo)
        Me.tp_Movie_ClearLogo.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_ClearLogo.Name = "tp_Movie_ClearLogo"
        Me.tp_Movie_ClearLogo.Size = New System.Drawing.Size(276, 527)
        Me.tp_Movie_ClearLogo.TabIndex = 2
        Me.tp_Movie_ClearLogo.Text = "ClearLogo"
        '
        'b_movie_deletelogo
        '
        Me.b_movie_deletelogo.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_movie_deletelogo.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_deletelogo.Name = "b_movie_deletelogo"
        Me.b_movie_deletelogo.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_deletelogo.TabIndex = 20
        Me.b_movie_deletelogo.UseVisualStyleBackColor = True
        Me.b_movie_deletelogo.Visible = False
        '
        'pb_movie_clearlogo
        '
        Me.pb_movie_clearlogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_movie_clearlogo.Location = New System.Drawing.Point(34, 7)
        Me.pb_movie_clearlogo.Name = "pb_movie_clearlogo"
        Me.pb_movie_clearlogo.Size = New System.Drawing.Size(200, 77)
        Me.pb_movie_clearlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_movie_clearlogo.TabIndex = 17
        Me.pb_movie_clearlogo.TabStop = False
        '
        'lv_movie_clearlogo
        '
        Me.lv_movie_clearlogo.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movie_clearlogo.BackColor = System.Drawing.SystemColors.Control
        Me.lv_movie_clearlogo.ForeColor = System.Drawing.Color.Black
        Me.lv_movie_clearlogo.LargeImageList = Me.il_clearlogo
        Me.lv_movie_clearlogo.Location = New System.Drawing.Point(12, 90)
        Me.lv_movie_clearlogo.MultiSelect = False
        Me.lv_movie_clearlogo.Name = "lv_movie_clearlogo"
        Me.lv_movie_clearlogo.Size = New System.Drawing.Size(252, 430)
        Me.lv_movie_clearlogo.TabIndex = 19
        Me.lv_movie_clearlogo.UseCompatibleStateImageBehavior = False
        '
        'tp3
        '
        Me.tp3.BackColor = System.Drawing.SystemColors.Control
        Me.tp3.Controls.Add(Me.lv_movies_missing)
        Me.tp3.Location = New System.Drawing.Point(4, 22)
        Me.tp3.Name = "tp3"
        Me.tp3.Padding = New System.Windows.Forms.Padding(3)
        Me.tp3.Size = New System.Drawing.Size(642, 567)
        Me.tp3.TabIndex = 2
        Me.tp3.Text = "Movies missing DVDArt"
        '
        'lv_movies_missing
        '
        Me.lv_movies_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movies_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.m_Movie, Me.m_DVDArt, Me.m_ClearArt, Me.m_ClearLogo, Me.m_IMDb_id})
        Me.lv_movies_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_movies_missing.GridLines = True
        Me.lv_movies_missing.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_movies_missing.Location = New System.Drawing.Point(8, 8)
        Me.lv_movies_missing.Name = "lv_movies_missing"
        Me.lv_movies_missing.Size = New System.Drawing.Size(628, 550)
        Me.lv_movies_missing.SmallImageList = Me.il_state
        Me.lv_movies_missing.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_movies_missing.TabIndex = 10
        Me.lv_movies_missing.UseCompatibleStateImageBehavior = False
        Me.lv_movies_missing.View = System.Windows.Forms.View.Details
        '
        'm_Movie
        '
        Me.m_Movie.Text = "Movie"
        Me.m_Movie.Width = 377
        '
        'm_DVDArt
        '
        Me.m_DVDArt.Text = "DVDArt"
        Me.m_DVDArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.m_DVDArt.Width = 50
        '
        'm_ClearArt
        '
        Me.m_ClearArt.Text = "ClearArt"
        Me.m_ClearArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.m_ClearArt.Width = 50
        '
        'm_ClearLogo
        '
        Me.m_ClearLogo.Text = "ClearLogo"
        Me.m_ClearLogo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_IMDb_id
        '
        Me.m_IMDb_id.Text = "IMDb ID"
        Me.m_IMDb_id.Width = 69
        '
        'pbFTV_Logo
        '
        Me.pbFTV_Logo.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.pbFTV_Logo.Location = New System.Drawing.Point(469, 601)
        Me.pbFTV_Logo.Name = "pbFTV_Logo"
        Me.pbFTV_Logo.Size = New System.Drawing.Size(258, 48)
        Me.pbFTV_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFTV_Logo.TabIndex = 29
        Me.pbFTV_Logo.TabStop = False
        '
        'tp_TVSeries
        '
        Me.tp_TVSeries.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_TVSeries.Controls.Add(Me.TabControl2)
        Me.tp_TVSeries.Controls.Add(Me.PictureBox2)
        Me.tp_TVSeries.Controls.Add(Me.PictureBox1)
        Me.tp_TVSeries.Location = New System.Drawing.Point(4, 22)
        Me.tp_TVSeries.Name = "tp_TVSeries"
        Me.tp_TVSeries.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_TVSeries.Size = New System.Drawing.Size(656, 650)
        Me.tp_TVSeries.TabIndex = 1
        Me.tp_TVSeries.Text = "Series"
        '
        'TabControl2
        '
        Me.TabControl2.Controls.Add(Me.TabPage1)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Location = New System.Drawing.Point(4, 3)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(650, 593)
        Me.TabControl2.TabIndex = 32
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage1.Controls.Add(Me.lv_series)
        Me.TabPage1.Controls.Add(Me.tbc_series)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(642, 567)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Series with Artwork"
        Me.TabPage1.ToolTipText = "This tab contails all movies that already have a DVD artwork."
        '
        'lv_series
        '
        Me.lv_series.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_series.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lv_series.ContextMenuStrip = Me.cms_found
        Me.lv_series.FullRowSelect = True
        Me.lv_series.GridLines = True
        Me.lv_series.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_series.Location = New System.Drawing.Point(8, 8)
        Me.lv_series.Name = "lv_series"
        Me.lv_series.Size = New System.Drawing.Size(340, 550)
        Me.lv_series.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_series.TabIndex = 11
        Me.lv_series.UseCompatibleStateImageBehavior = False
        Me.lv_series.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Serie"
        Me.ColumnHeader1.Width = 319
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "TheTVDB ID"
        Me.ColumnHeader2.Width = 74
        '
        'tbc_series
        '
        Me.tbc_series.Controls.Add(Me.tp_Serie_ClearArt)
        Me.tbc_series.Controls.Add(Me.tp_Serie_ClearLogo)
        Me.tbc_series.Location = New System.Drawing.Point(352, 8)
        Me.tbc_series.Name = "tbc_series"
        Me.tbc_series.SelectedIndex = 0
        Me.tbc_series.Size = New System.Drawing.Size(284, 553)
        Me.tbc_series.TabIndex = 12
        '
        'tp_Serie_ClearArt
        '
        Me.tp_Serie_ClearArt.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Serie_ClearArt.Controls.Add(Me.l_thetvdb_id)
        Me.tp_Serie_ClearArt.Controls.Add(Me.b_serie_deleteart)
        Me.tp_Serie_ClearArt.Controls.Add(Me.pb_serie_clearart)
        Me.tp_Serie_ClearArt.Controls.Add(Me.lv_serie_clearart)
        Me.tp_Serie_ClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tp_Serie_ClearArt.Name = "tp_Serie_ClearArt"
        Me.tp_Serie_ClearArt.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Serie_ClearArt.Size = New System.Drawing.Size(276, 527)
        Me.tp_Serie_ClearArt.TabIndex = 1
        Me.tp_Serie_ClearArt.Text = "ClearArt"
        '
        'l_thetvdb_id
        '
        Me.l_thetvdb_id.AutoSize = True
        Me.l_thetvdb_id.Location = New System.Drawing.Point(227, 112)
        Me.l_thetvdb_id.Name = "l_thetvdb_id"
        Me.l_thetvdb_id.Size = New System.Drawing.Size(43, 13)
        Me.l_thetvdb_id.TabIndex = 18
        Me.l_thetvdb_id.Text = "000000"
        Me.l_thetvdb_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'b_serie_deleteart
        '
        Me.b_serie_deleteart.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_serie_deleteart.Location = New System.Drawing.Point(234, 7)
        Me.b_serie_deleteart.Name = "b_serie_deleteart"
        Me.b_serie_deleteart.Size = New System.Drawing.Size(40, 40)
        Me.b_serie_deleteart.TabIndex = 17
        Me.b_serie_deleteart.UseVisualStyleBackColor = True
        Me.b_serie_deleteart.Visible = False
        '
        'pb_serie_clearart
        '
        Me.pb_serie_clearart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_serie_clearart.Location = New System.Drawing.Point(34, 7)
        Me.pb_serie_clearart.Name = "pb_serie_clearart"
        Me.pb_serie_clearart.Size = New System.Drawing.Size(200, 112)
        Me.pb_serie_clearart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_serie_clearart.TabIndex = 14
        Me.pb_serie_clearart.TabStop = False
        '
        'lv_serie_clearart
        '
        Me.lv_serie_clearart.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_serie_clearart.BackColor = System.Drawing.SystemColors.Control
        Me.lv_serie_clearart.ForeColor = System.Drawing.Color.Black
        Me.lv_serie_clearart.LargeImageList = Me.il_clearart
        Me.lv_serie_clearart.Location = New System.Drawing.Point(12, 125)
        Me.lv_serie_clearart.MultiSelect = False
        Me.lv_serie_clearart.Name = "lv_serie_clearart"
        Me.lv_serie_clearart.Size = New System.Drawing.Size(252, 395)
        Me.lv_serie_clearart.TabIndex = 16
        Me.lv_serie_clearart.UseCompatibleStateImageBehavior = False
        '
        'tp_Serie_ClearLogo
        '
        Me.tp_Serie_ClearLogo.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Serie_ClearLogo.Controls.Add(Me.b_serie_deletelogo)
        Me.tp_Serie_ClearLogo.Controls.Add(Me.pb_serie_clearlogo)
        Me.tp_Serie_ClearLogo.Controls.Add(Me.lv_serie_clearlogo)
        Me.tp_Serie_ClearLogo.Location = New System.Drawing.Point(4, 22)
        Me.tp_Serie_ClearLogo.Name = "tp_Serie_ClearLogo"
        Me.tp_Serie_ClearLogo.Size = New System.Drawing.Size(276, 527)
        Me.tp_Serie_ClearLogo.TabIndex = 2
        Me.tp_Serie_ClearLogo.Text = "ClearLogo"
        '
        'b_serie_deletelogo
        '
        Me.b_serie_deletelogo.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_serie_deletelogo.Location = New System.Drawing.Point(234, 7)
        Me.b_serie_deletelogo.Name = "b_serie_deletelogo"
        Me.b_serie_deletelogo.Size = New System.Drawing.Size(40, 40)
        Me.b_serie_deletelogo.TabIndex = 20
        Me.b_serie_deletelogo.UseVisualStyleBackColor = True
        Me.b_serie_deletelogo.Visible = False
        '
        'pb_serie_clearlogo
        '
        Me.pb_serie_clearlogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_serie_clearlogo.Location = New System.Drawing.Point(34, 7)
        Me.pb_serie_clearlogo.Name = "pb_serie_clearlogo"
        Me.pb_serie_clearlogo.Size = New System.Drawing.Size(200, 77)
        Me.pb_serie_clearlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_serie_clearlogo.TabIndex = 17
        Me.pb_serie_clearlogo.TabStop = False
        '
        'lv_serie_clearlogo
        '
        Me.lv_serie_clearlogo.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_serie_clearlogo.BackColor = System.Drawing.SystemColors.Control
        Me.lv_serie_clearlogo.ForeColor = System.Drawing.Color.Black
        Me.lv_serie_clearlogo.LargeImageList = Me.il_clearlogo
        Me.lv_serie_clearlogo.Location = New System.Drawing.Point(12, 90)
        Me.lv_serie_clearlogo.MultiSelect = False
        Me.lv_serie_clearlogo.Name = "lv_serie_clearlogo"
        Me.lv_serie_clearlogo.Size = New System.Drawing.Size(252, 430)
        Me.lv_serie_clearlogo.TabIndex = 19
        Me.lv_serie_clearlogo.UseCompatibleStateImageBehavior = False
        '
        'TabPage5
        '
        Me.TabPage5.BackColor = System.Drawing.SystemColors.Control
        Me.TabPage5.Controls.Add(Me.lv_series_missing)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(642, 567)
        Me.TabPage5.TabIndex = 2
        Me.TabPage5.Text = "Series missing DVDArt"
        '
        'lv_series_missing
        '
        Me.lv_series_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_series_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c_Serie, Me.c_ClearArt, Me.c_ClearLogo, Me.c_id})
        Me.lv_series_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_series_missing.GridLines = True
        Me.lv_series_missing.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_series_missing.Location = New System.Drawing.Point(8, 8)
        Me.lv_series_missing.Name = "lv_series_missing"
        Me.lv_series_missing.Size = New System.Drawing.Size(628, 550)
        Me.lv_series_missing.SmallImageList = Me.il_state
        Me.lv_series_missing.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_series_missing.TabIndex = 10
        Me.lv_series_missing.UseCompatibleStateImageBehavior = False
        Me.lv_series_missing.View = System.Windows.Forms.View.Details
        '
        'c_Serie
        '
        Me.c_Serie.Text = "Serie"
        Me.c_Serie.Width = 410
        '
        'c_ClearArt
        '
        Me.c_ClearArt.Text = "ClearArt"
        Me.c_ClearArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.c_ClearArt.Width = 50
        '
        'c_ClearLogo
        '
        Me.c_ClearLogo.Text = "ClearLogo"
        Me.c_ClearLogo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'c_id
        '
        Me.c_id.Text = "TheTVDB ID"
        Me.c_id.Width = 89
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.DVDArt_Plugin.My.Resources.Resources.tvseries
        Me.PictureBox2.Location = New System.Drawing.Point(259, 593)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(208, 59)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 31
        Me.PictureBox2.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.PictureBox1.Location = New System.Drawing.Point(469, 601)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(258, 48)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 30
        Me.PictureBox1.TabStop = False
        '
        'tp_importer
        '
        Me.tp_importer.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_importer.Controls.Add(Me.lv_import)
        Me.tp_importer.Location = New System.Drawing.Point(4, 22)
        Me.tp_importer.Name = "tp_importer"
        Me.tp_importer.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_importer.Size = New System.Drawing.Size(656, 650)
        Me.tp_importer.TabIndex = 3
        Me.tp_importer.Text = "Importer"
        '
        'lv_import
        '
        Me.lv_import.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_import.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.i_Movie_Series, Me.i_ID, Me.i_Type})
        Me.lv_import.ContextMenuStrip = Me.cms_import
        Me.lv_import.GridLines = True
        Me.lv_import.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_import.Location = New System.Drawing.Point(14, 16)
        Me.lv_import.Name = "lv_import"
        Me.lv_import.Size = New System.Drawing.Size(628, 619)
        Me.lv_import.StateImageList = Me.il_state
        Me.lv_import.TabIndex = 10
        Me.lv_import.UseCompatibleStateImageBehavior = False
        Me.lv_import.View = System.Windows.Forms.View.Details
        '
        'i_Movie_Series
        '
        Me.i_Movie_Series.Text = "Movie/TVSeries"
        Me.i_Movie_Series.Width = 480
        '
        'i_ID
        '
        Me.i_ID.Text = "ID"
        Me.i_ID.Width = 74
        '
        'i_Type
        '
        Me.i_Type.Text = "Type"
        Me.i_Type.Width = 53
        '
        'tp_Settings
        '
        Me.tp_Settings.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Settings.Controls.Add(Me.gb2)
        Me.tp_Settings.Controls.Add(Me.gb1)
        Me.tp_Settings.Location = New System.Drawing.Point(4, 22)
        Me.tp_Settings.Name = "tp_Settings"
        Me.tp_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Settings.Size = New System.Drawing.Size(656, 650)
        Me.tp_Settings.TabIndex = 2
        Me.tp_Settings.Text = "Settings"
        '
        'gb2
        '
        Me.gb2.Controls.Add(Me.cb_language)
        Me.gb2.Controls.Add(Me.Label6)
        Me.gb2.Controls.Add(Me.pb2)
        Me.gb2.Controls.Add(Me.pb3)
        Me.gb2.Controls.Add(Me.pb1)
        Me.gb2.Controls.Add(Me.cb_ClearLogo)
        Me.gb2.Controls.Add(Me.cb_ClearArt)
        Me.gb2.Controls.Add(Me.cb_DVDArt)
        Me.gb2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb2.Location = New System.Drawing.Point(36, 227)
        Me.gb2.Name = "gb2"
        Me.gb2.Size = New System.Drawing.Size(584, 288)
        Me.gb2.TabIndex = 21
        Me.gb2.TabStop = False
        Me.gb2.Text = " Scrape for "
        '
        'cb_language
        '
        Me.cb_language.FormattingEnabled = True
        Me.cb_language.Location = New System.Drawing.Point(273, 251)
        Me.cb_language.Name = "cb_language"
        Me.cb_language.Size = New System.Drawing.Size(142, 24)
        Me.cb_language.TabIndex = 7
        Me.cb_language.Text = "Any"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 254)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(194, 16)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Preferred language for Importer"
        '
        'pb2
        '
        Me.pb2.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_clearart
        Me.pb2.Location = New System.Drawing.Point(273, 134)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(100, 56)
        Me.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb2.TabIndex = 5
        Me.pb2.TabStop = False
        '
        'pb3
        '
        Me.pb3.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_clearlogo
        Me.pb3.Location = New System.Drawing.Point(273, 196)
        Me.pb3.Name = "pb3"
        Me.pb3.Size = New System.Drawing.Size(100, 39)
        Me.pb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb3.TabIndex = 4
        Me.pb3.TabStop = False
        '
        'pb1
        '
        Me.pb1.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_dvdart
        Me.pb1.Location = New System.Drawing.Point(273, 28)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(100, 100)
        Me.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb1.TabIndex = 3
        Me.pb1.TabStop = False
        '
        'cb_ClearLogo
        '
        Me.cb_ClearLogo.AutoSize = True
        Me.cb_ClearLogo.Location = New System.Drawing.Point(16, 196)
        Me.cb_ClearLogo.Name = "cb_ClearLogo"
        Me.cb_ClearLogo.Size = New System.Drawing.Size(90, 20)
        Me.cb_ClearLogo.TabIndex = 2
        Me.cb_ClearLogo.Text = "ClearLogo"
        Me.cb_ClearLogo.UseVisualStyleBackColor = True
        '
        'cb_ClearArt
        '
        Me.cb_ClearArt.AutoSize = True
        Me.cb_ClearArt.Location = New System.Drawing.Point(14, 134)
        Me.cb_ClearArt.Name = "cb_ClearArt"
        Me.cb_ClearArt.Size = New System.Drawing.Size(178, 20)
        Me.cb_ClearArt.TabIndex = 1
        Me.cb_ClearArt.Text = "ClearArt and HD Clear Art"
        Me.cb_ClearArt.UseVisualStyleBackColor = True
        '
        'cb_DVDArt
        '
        Me.cb_DVDArt.AutoSize = True
        Me.cb_DVDArt.Checked = True
        Me.cb_DVDArt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cb_DVDArt.Location = New System.Drawing.Point(14, 28)
        Me.cb_DVDArt.Name = "cb_DVDArt"
        Me.cb_DVDArt.Size = New System.Drawing.Size(72, 20)
        Me.cb_DVDArt.TabIndex = 0
        Me.cb_DVDArt.Text = "DVDArt"
        Me.cb_DVDArt.UseVisualStyleBackColor = True
        '
        'gb1
        '
        Me.gb1.Controls.Add(Me.cb_missing)
        Me.gb1.Controls.Add(Me.nud_missing)
        Me.gb1.Controls.Add(Me.Label5)
        Me.gb1.Controls.Add(Me.cb_scraping)
        Me.gb1.Controls.Add(Me.Label4)
        Me.gb1.Controls.Add(Me.nud_scraping)
        Me.gb1.Controls.Add(Me.Label3)
        Me.gb1.Controls.Add(Me.Label2)
        Me.gb1.Controls.Add(Me.cb_delay)
        Me.gb1.Controls.Add(Me.nud_delay)
        Me.gb1.Controls.Add(Me.mtb_cpu)
        Me.gb1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb1.Location = New System.Drawing.Point(36, 19)
        Me.gb1.Name = "gb1"
        Me.gb1.Size = New System.Drawing.Size(584, 190)
        Me.gb1.TabIndex = 20
        Me.gb1.TabStop = False
        Me.gb1.Text = " Background scraper settings "
        '
        'cb_missing
        '
        Me.cb_missing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_missing.FormattingEnabled = True
        Me.cb_missing.Items.AddRange(New Object() {"disabled", "hours", "days", "weeks", "months"})
        Me.cb_missing.Location = New System.Drawing.Point(335, 149)
        Me.cb_missing.Name = "cb_missing"
        Me.cb_missing.Size = New System.Drawing.Size(80, 24)
        Me.cb_missing.TabIndex = 18
        Me.cb_missing.Text = "disabled"
        '
        'nud_missing
        '
        Me.nud_missing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_missing.Location = New System.Drawing.Point(295, 150)
        Me.nud_missing.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.nud_missing.Name = "nud_missing"
        Me.nud_missing.Size = New System.Drawing.Size(36, 22)
        Me.nud_missing.TabIndex = 17
        Me.nud_missing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(11, 152)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(281, 16)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "automatically re-scrape missing movies every"
        '
        'cb_scraping
        '
        Me.cb_scraping.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_scraping.FormattingEnabled = True
        Me.cb_scraping.Items.AddRange(New Object() {"minutes", "hours"})
        Me.cb_scraping.Location = New System.Drawing.Point(144, 104)
        Me.cb_scraping.Name = "cb_scraping"
        Me.cb_scraping.Size = New System.Drawing.Size(80, 24)
        Me.cb_scraping.TabIndex = 15
        Me.cb_scraping.Text = "minutes"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(11, 63)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(195, 16)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "scrape when CPU usage below"
        '
        'nud_scraping
        '
        Me.nud_scraping.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_scraping.Location = New System.Drawing.Point(104, 105)
        Me.nud_scraping.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nud_scraping.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_scraping.Name = "nud_scraping"
        Me.nud_scraping.Size = New System.Drawing.Size(36, 22)
        Me.nud_scraping.TabIndex = 14
        Me.nud_scraping.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_scraping.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(11, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(202, 16)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "on MediaPortal Startup, delay by"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(11, 107)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 16)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "scrape every"
        '
        'cb_delay
        '
        Me.cb_delay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_delay.FormattingEnabled = True
        Me.cb_delay.Items.AddRange(New Object() {"seconds", "minutes", "hours"})
        Me.cb_delay.Location = New System.Drawing.Point(259, 20)
        Me.cb_delay.Name = "cb_delay"
        Me.cb_delay.Size = New System.Drawing.Size(80, 24)
        Me.cb_delay.TabIndex = 12
        Me.cb_delay.Text = "minutes"
        '
        'nud_delay
        '
        Me.nud_delay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_delay.Location = New System.Drawing.Point(219, 21)
        Me.nud_delay.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nud_delay.Name = "nud_delay"
        Me.nud_delay.Size = New System.Drawing.Size(36, 22)
        Me.nud_delay.TabIndex = 11
        Me.nud_delay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_delay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'mtb_cpu
        '
        Me.mtb_cpu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtb_cpu.Location = New System.Drawing.Point(208, 60)
        Me.mtb_cpu.Mask = "00%"
        Me.mtb_cpu.Name = "mtb_cpu"
        Me.mtb_cpu.Size = New System.Drawing.Size(34, 22)
        Me.mtb_cpu.TabIndex = 11
        Me.mtb_cpu.Text = "25"
        Me.mtb_cpu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'il_column
        '
        Me.il_column.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_column.ImageSize = New System.Drawing.Size(8, 8)
        Me.il_column.TransparentColor = System.Drawing.Color.Transparent
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.movie_search
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh artwork from online"
        Me.RefreshToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'MUploadToolStripMenuItem
        '
        Me.MUploadToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.MUploadToolStripMenuItem.Name = "MUploadToolStripMenuItem"
        Me.MUploadToolStripMenuItem.Size = New System.Drawing.Size(238, 22)
        Me.MUploadToolStripMenuItem.Text = "Manually Upload Artwork"
        Me.MUploadToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SendToImporterToolStripMenuItem1
        '
        Me.SendToImporterToolStripMenuItem1.Image = Global.DVDArt_Plugin.My.Resources.Resources.arrow
        Me.SendToImporterToolStripMenuItem1.Name = "SendToImporterToolStripMenuItem1"
        Me.SendToImporterToolStripMenuItem1.Size = New System.Drawing.Size(232, 22)
        Me.SendToImporterToolStripMenuItem1.Text = "Send to Importer"
        '
        'RescanAllToolStripMenuItem1
        '
        Me.RescanAllToolStripMenuItem1.Image = Global.DVDArt_Plugin.My.Resources.Resources.rescan_movies
        Me.RescanAllToolStripMenuItem1.Name = "RescanAllToolStripMenuItem1"
        Me.RescanAllToolStripMenuItem1.Size = New System.Drawing.Size(232, 22)
        Me.RescanAllToolStripMenuItem1.Text = "Rescan all"
        '
        'DVDArt_GUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 698)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbc_main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DVDArt_GUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DVDArt "
        Me.cms_found.ResumeLayout(False)
        Me.cms_import.ResumeLayout(False)
        Me.cms_missing.ResumeLayout(False)
        Me.tbc_main.ResumeLayout(False)
        Me.tp_MovingPictures.ResumeLayout(False)
        CType(Me.pb_movingpictures, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.tp1.ResumeLayout(False)
        Me.tbc_movies.ResumeLayout(False)
        Me.tp_Movie_DVDArt.ResumeLayout(False)
        Me.tp_Movie_DVDArt.PerformLayout()
        CType(Me.pb_movie_dvdart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Movie_ClearArt.ResumeLayout(False)
        CType(Me.pb_movie_clearart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Movie_ClearLogo.ResumeLayout(False)
        CType(Me.pb_movie_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp3.ResumeLayout(False)
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_TVSeries.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.tbc_series.ResumeLayout(False)
        Me.tp_Serie_ClearArt.ResumeLayout(False)
        Me.tp_Serie_ClearArt.PerformLayout()
        CType(Me.pb_serie_clearart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Serie_ClearLogo.ResumeLayout(False)
        CType(Me.pb_serie_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage5.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_importer.ResumeLayout(False)
        Me.tp_Settings.ResumeLayout(False)
        Me.gb2.ResumeLayout(False)
        Me.gb2.PerformLayout()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb1.ResumeLayout(False)
        Me.gb1.PerformLayout()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents il_dvdart As System.Windows.Forms.ImageList
    Friend WithEvents cms_found As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cms_missing As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendToImporterToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescanAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeIMDBTMDBNumberToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescanToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bw_import As System.ComponentModel.BackgroundWorker
    Friend WithEvents il_state As System.Windows.Forms.ImageList
    Friend WithEvents cms_import As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RestartImporterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents il_clearart As System.Windows.Forms.ImageList
    Friend WithEvents il_clearlogo As System.Windows.Forms.ImageList
    Friend WithEvents UploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MUploadToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tbc_main As System.Windows.Forms.TabControl
    Friend WithEvents tp_MovingPictures As System.Windows.Forms.TabPage
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tp1 As System.Windows.Forms.TabPage
    Friend WithEvents lv_movies As System.Windows.Forms.ListView
    Friend WithEvents Movie As System.Windows.Forms.ColumnHeader
    Friend WithEvents IMDb_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbc_movies As System.Windows.Forms.TabControl
    Friend WithEvents tp_Movie_DVDArt As System.Windows.Forms.TabPage
    Friend WithEvents b_movie_preview As System.Windows.Forms.Button
    Friend WithEvents b_movie_compress As System.Windows.Forms.Button
    Friend WithEvents l_movie_size As System.Windows.Forms.Label
    Friend WithEvents lv_movie_dvdart As System.Windows.Forms.ListView
    Friend WithEvents l_imdb_id As System.Windows.Forms.Label
    Friend WithEvents pb_movie_dvdart As System.Windows.Forms.PictureBox
    Friend WithEvents tp_Movie_ClearArt As System.Windows.Forms.TabPage
    Friend WithEvents b_movie_deleteart As System.Windows.Forms.Button
    Friend WithEvents pb_movie_clearart As System.Windows.Forms.PictureBox
    Friend WithEvents lv_movie_clearart As System.Windows.Forms.ListView
    Friend WithEvents tp_Movie_ClearLogo As System.Windows.Forms.TabPage
    Friend WithEvents b_movie_deletelogo As System.Windows.Forms.Button
    Friend WithEvents pb_movie_clearlogo As System.Windows.Forms.PictureBox
    Friend WithEvents lv_movie_clearlogo As System.Windows.Forms.ListView
    Friend WithEvents tp3 As System.Windows.Forms.TabPage
    Friend WithEvents lv_movies_missing As System.Windows.Forms.ListView
    Friend WithEvents m_Movie As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_DVDArt As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_ClearArt As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_ClearLogo As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_IMDb_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents pb_movingpictures As System.Windows.Forms.PictureBox
    Friend WithEvents pbFTV_Logo As System.Windows.Forms.PictureBox
    Friend WithEvents tp_TVSeries As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents tp_Settings As System.Windows.Forms.TabPage
    Friend WithEvents gb2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents pb2 As System.Windows.Forms.PictureBox
    Friend WithEvents pb3 As System.Windows.Forms.PictureBox
    Friend WithEvents pb1 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_ClearLogo As System.Windows.Forms.CheckBox
    Friend WithEvents cb_ClearArt As System.Windows.Forms.CheckBox
    Friend WithEvents cb_DVDArt As System.Windows.Forms.CheckBox
    Friend WithEvents gb1 As System.Windows.Forms.GroupBox
    Friend WithEvents cb_missing As System.Windows.Forms.ComboBox
    Friend WithEvents nud_missing As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_scraping As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nud_scraping As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cb_delay As System.Windows.Forms.ComboBox
    Friend WithEvents nud_delay As System.Windows.Forms.NumericUpDown
    Friend WithEvents mtb_cpu As System.Windows.Forms.MaskedTextBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents tp_importer As System.Windows.Forms.TabPage
    Friend WithEvents lv_import As System.Windows.Forms.ListView
    Friend WithEvents i_Movie_Series As System.Windows.Forms.ColumnHeader
    Friend WithEvents i_ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lv_series As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbc_series As System.Windows.Forms.TabControl
    Friend WithEvents tp_Serie_ClearArt As System.Windows.Forms.TabPage
    Friend WithEvents b_serie_deleteart As System.Windows.Forms.Button
    Friend WithEvents pb_serie_clearart As System.Windows.Forms.PictureBox
    Friend WithEvents lv_serie_clearart As System.Windows.Forms.ListView
    Friend WithEvents tp_Serie_ClearLogo As System.Windows.Forms.TabPage
    Friend WithEvents b_serie_deletelogo As System.Windows.Forms.Button
    Friend WithEvents pb_serie_clearlogo As System.Windows.Forms.PictureBox
    Friend WithEvents lv_serie_clearlogo As System.Windows.Forms.ListView
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents lv_series_missing As System.Windows.Forms.ListView
    Friend WithEvents c_ClearArt As System.Windows.Forms.ColumnHeader
    Friend WithEvents c_ClearLogo As System.Windows.Forms.ColumnHeader
    Friend WithEvents c_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents i_Type As System.Windows.Forms.ColumnHeader
    Friend WithEvents l_thetvdb_id As System.Windows.Forms.Label
    Friend WithEvents c_Serie As System.Windows.Forms.ColumnHeader
    Friend WithEvents cb_language As System.Windows.Forms.ComboBox
    Friend WithEvents UseCoverArtForDVDArtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectCoverArtForDVDArtToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectCoverArtForDVDArtToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshArtworkFromOnlineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManuallyUploadArtworkToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectCoverArtForDVDArtToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents il_column As System.Windows.Forms.ImageList

End Class
