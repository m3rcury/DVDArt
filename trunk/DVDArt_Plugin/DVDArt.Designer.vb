<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DVDArt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DVDArt))
        Me.il_available = New System.Windows.Forms.ImageList(Me.components)
        Me.cms_movies = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.bw_download_thumb = New System.ComponentModel.BackgroundWorker()
        Me.bw_download_fullsize = New System.ComponentModel.BackgroundWorker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tp1 = New System.Windows.Forms.TabPage()
        Me.lv_movies = New System.Windows.Forms.ListView()
        Me.Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.l_imdb_id = New System.Windows.Forms.Label()
        Me.pb_current = New System.Windows.Forms.PictureBox()
        Me.lv_available = New System.Windows.Forms.ListView()
        Me.tp2 = New System.Windows.Forms.TabPage()
        Me.lv_import = New System.Windows.Forms.ListView()
        Me.i_Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cms_import = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RestartImporterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.il_state = New System.Windows.Forms.ImageList(Me.components)
        Me.tp3 = New System.Windows.Forms.TabPage()
        Me.lv_missing = New System.Windows.Forms.ListView()
        Me.m_Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cms_missing = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.SendToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.tp4 = New System.Windows.Forms.TabPage()
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
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.bw_import = New System.ComponentModel.BackgroundWorker()
        Me.l_movingpictures = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pbFTV_Logo = New System.Windows.Forms.PictureBox()
        Me.SendToImporterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_movies.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tp1.SuspendLayout()
        CType(Me.pb_current, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp2.SuspendLayout()
        Me.cms_import.SuspendLayout()
        Me.tp3.SuspendLayout()
        Me.cms_missing.SuspendLayout()
        Me.tp4.SuspendLayout()
        Me.gb1.SuspendLayout()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'il_available
        '
        Me.il_available.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_available.ImageSize = New System.Drawing.Size(200, 200)
        Me.il_available.TransparentColor = System.Drawing.Color.Transparent
        '
        'cms_movies
        '
        Me.cms_movies.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshToolStripMenuItem})
        Me.cms_movies.Name = "cms_movies"
        Me.cms_movies.Size = New System.Drawing.Size(238, 26)
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RefreshToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.movie_search
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(237, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh DVDArt from online"
        Me.RefreshToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'bw_download_thumb
        '
        '
        'bw_download_fullsize
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Location = New System.Drawing.Point(2, 644)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(133, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Copyright © 2012, m3rcury"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tp1)
        Me.TabControl1.Controls.Add(Me.tp2)
        Me.TabControl1.Controls.Add(Me.tp3)
        Me.TabControl1.Controls.Add(Me.tp4)
        Me.TabControl1.Location = New System.Drawing.Point(12, 13)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(624, 593)
        Me.TabControl1.TabIndex = 26
        '
        'tp1
        '
        Me.tp1.BackColor = System.Drawing.SystemColors.Control
        Me.tp1.Controls.Add(Me.lv_movies)
        Me.tp1.Controls.Add(Me.l_imdb_id)
        Me.tp1.Controls.Add(Me.pb_current)
        Me.tp1.Controls.Add(Me.lv_available)
        Me.tp1.Location = New System.Drawing.Point(4, 22)
        Me.tp1.Name = "tp1"
        Me.tp1.Padding = New System.Windows.Forms.Padding(3)
        Me.tp1.Size = New System.Drawing.Size(616, 567)
        Me.tp1.TabIndex = 0
        Me.tp1.Text = "Movies with DVDArt"
        Me.tp1.ToolTipText = "This tab contails all movies that already have a DVD artwork."
        '
        'lv_movies
        '
        Me.lv_movies.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movies.AllowColumnReorder = True
        Me.lv_movies.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Movie, Me.IMDb_id})
        Me.lv_movies.ContextMenuStrip = Me.cms_movies
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
        'l_imdb_id
        '
        Me.l_imdb_id.AutoSize = True
        Me.l_imdb_id.Location = New System.Drawing.Point(545, 195)
        Me.l_imdb_id.Name = "l_imdb_id"
        Me.l_imdb_id.Size = New System.Drawing.Size(61, 13)
        Me.l_imdb_id.TabIndex = 10
        Me.l_imdb_id.Text = "tt00000000"
        Me.l_imdb_id.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'pb_current
        '
        Me.pb_current.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_current.Location = New System.Drawing.Point(379, 8)
        Me.pb_current.Name = "pb_current"
        Me.pb_current.Size = New System.Drawing.Size(200, 200)
        Me.pb_current.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_current.TabIndex = 9
        Me.pb_current.TabStop = False
        '
        'lv_available
        '
        Me.lv_available.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_available.BackColor = System.Drawing.SystemColors.Control
        Me.lv_available.ForeColor = System.Drawing.Color.Black
        Me.lv_available.LargeImageList = Me.il_available
        Me.lv_available.Location = New System.Drawing.Point(354, 214)
        Me.lv_available.MultiSelect = False
        Me.lv_available.Name = "lv_available"
        Me.lv_available.Size = New System.Drawing.Size(252, 344)
        Me.lv_available.TabIndex = 5
        Me.lv_available.UseCompatibleStateImageBehavior = False
        '
        'tp2
        '
        Me.tp2.BackColor = System.Drawing.SystemColors.Control
        Me.tp2.Controls.Add(Me.lv_import)
        Me.tp2.Location = New System.Drawing.Point(4, 22)
        Me.tp2.Name = "tp2"
        Me.tp2.Padding = New System.Windows.Forms.Padding(3)
        Me.tp2.Size = New System.Drawing.Size(616, 567)
        Me.tp2.TabIndex = 1
        Me.tp2.Text = "Importer"
        Me.tp2.ToolTipText = "From this tab you can import DVD artwork for new movies"
        '
        'lv_import
        '
        Me.lv_import.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_import.AllowColumnReorder = True
        Me.lv_import.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.i_Movie, Me.i_IMDb_id})
        Me.lv_import.ContextMenuStrip = Me.cms_import
        Me.lv_import.GridLines = True
        Me.lv_import.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_import.Location = New System.Drawing.Point(8, 8)
        Me.lv_import.Name = "lv_import"
        Me.lv_import.Size = New System.Drawing.Size(595, 550)
        Me.lv_import.StateImageList = Me.il_state
        Me.lv_import.TabIndex = 9
        Me.lv_import.UseCompatibleStateImageBehavior = False
        Me.lv_import.View = System.Windows.Forms.View.Details
        '
        'i_Movie
        '
        Me.i_Movie.Text = "Movie"
        Me.i_Movie.Width = 500
        '
        'i_IMDb_id
        '
        Me.i_IMDb_id.Text = "IMDb ID"
        Me.i_IMDb_id.Width = 74
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
        'tp3
        '
        Me.tp3.BackColor = System.Drawing.SystemColors.Control
        Me.tp3.Controls.Add(Me.lv_missing)
        Me.tp3.Location = New System.Drawing.Point(4, 22)
        Me.tp3.Name = "tp3"
        Me.tp3.Padding = New System.Windows.Forms.Padding(3)
        Me.tp3.Size = New System.Drawing.Size(616, 567)
        Me.tp3.TabIndex = 2
        Me.tp3.Text = "Movies missing DVDArt"
        '
        'lv_missing
        '
        Me.lv_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_missing.AllowColumnReorder = True
        Me.lv_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.m_Movie, Me.m_IMDb_id})
        Me.lv_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_missing.GridLines = True
        Me.lv_missing.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_missing.Location = New System.Drawing.Point(8, 8)
        Me.lv_missing.Name = "lv_missing"
        Me.lv_missing.Size = New System.Drawing.Size(595, 550)
        Me.lv_missing.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_missing.TabIndex = 10
        Me.lv_missing.UseCompatibleStateImageBehavior = False
        Me.lv_missing.View = System.Windows.Forms.View.Details
        '
        'm_Movie
        '
        Me.m_Movie.Text = "Movie"
        Me.m_Movie.Width = 500
        '
        'm_IMDb_id
        '
        Me.m_IMDb_id.Text = "IMDb ID"
        Me.m_IMDb_id.Width = 74
        '
        'cms_missing
        '
        Me.cms_missing.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendToolStripMenuItem1, Me.RescanToolStripMenuItem1})
        Me.cms_missing.Name = "cms_missing"
        Me.cms_missing.Size = New System.Drawing.Size(179, 48)
        '
        'SendToolStripMenuItem1
        '
        Me.SendToolStripMenuItem1.Image = Global.DVDArt_Plugin.My.Resources.Resources.arrow
        Me.SendToolStripMenuItem1.Name = "SendToolStripMenuItem1"
        Me.SendToolStripMenuItem1.Size = New System.Drawing.Size(178, 22)
        Me.SendToolStripMenuItem1.Text = "Send to importer"
        Me.SendToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'RescanToolStripMenuItem1
        '
        Me.RescanToolStripMenuItem1.Image = Global.DVDArt_Plugin.My.Resources.Resources.rescan_movies
        Me.RescanToolStripMenuItem1.Name = "RescanToolStripMenuItem1"
        Me.RescanToolStripMenuItem1.Size = New System.Drawing.Size(178, 22)
        Me.RescanToolStripMenuItem1.Text = "Rescan ALL missing"
        Me.RescanToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tp4
        '
        Me.tp4.BackColor = System.Drawing.SystemColors.Control
        Me.tp4.Controls.Add(Me.gb1)
        Me.tp4.Location = New System.Drawing.Point(4, 22)
        Me.tp4.Name = "tp4"
        Me.tp4.Padding = New System.Windows.Forms.Padding(3)
        Me.tp4.Size = New System.Drawing.Size(616, 567)
        Me.tp4.TabIndex = 3
        Me.tp4.Text = "Settings"
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
        Me.gb1.Location = New System.Drawing.Point(13, 22)
        Me.gb1.Name = "gb1"
        Me.gb1.Size = New System.Drawing.Size(584, 190)
        Me.gb1.TabIndex = 12
        Me.gb1.TabStop = False
        Me.gb1.Text = "Settings"
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
        'ChangeIMDBTMDBNumberToolStripMenuItem1
        '
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1.Name = "ChangeIMDBTMDBNumberToolStripMenuItem1"
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1.Size = New System.Drawing.Size(232, 22)
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1.Text = "Change IMDB/TMDB Number"
        '
        'bw_import
        '
        '
        'l_movingpictures
        '
        Me.l_movingpictures.AutoSize = True
        Me.l_movingpictures.BackColor = System.Drawing.Color.Transparent
        Me.l_movingpictures.Font = New System.Drawing.Font("Bell MT", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.l_movingpictures.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.l_movingpictures.Location = New System.Drawing.Point(176, 612)
        Me.l_movingpictures.Name = "l_movingpictures"
        Me.l_movingpictures.Size = New System.Drawing.Size(200, 30)
        Me.l_movingpictures.TabIndex = 27
        Me.l_movingpictures.Text = "Moving Pictures"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.DVDArt_Plugin.My.Resources.Resources.splash_small_transparent
        Me.PictureBox1.Location = New System.Drawing.Point(368, 597)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(80, 79)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 28
        Me.PictureBox1.TabStop = False
        '
        'pbFTV_Logo
        '
        Me.pbFTV_Logo.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.pbFTV_Logo.Location = New System.Drawing.Point(457, 612)
        Me.pbFTV_Logo.Name = "pbFTV_Logo"
        Me.pbFTV_Logo.Size = New System.Drawing.Size(258, 48)
        Me.pbFTV_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFTV_Logo.TabIndex = 25
        Me.pbFTV_Logo.TabStop = False
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
        'DVDArt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(648, 659)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.l_movingpictures)
        Me.Controls.Add(Me.pbFTV_Logo)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "DVDArt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DVDArt v1.0.0.1"
        Me.cms_movies.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tp1.ResumeLayout(False)
        Me.tp1.PerformLayout()
        CType(Me.pb_current, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp2.ResumeLayout(False)
        Me.cms_import.ResumeLayout(False)
        Me.tp3.ResumeLayout(False)
        Me.cms_missing.ResumeLayout(False)
        Me.tp4.ResumeLayout(False)
        Me.gb1.ResumeLayout(False)
        Me.gb1.PerformLayout()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents il_available As System.Windows.Forms.ImageList
    Friend WithEvents cms_movies As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bw_download_thumb As System.ComponentModel.BackgroundWorker
    Friend WithEvents bw_download_fullsize As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pbFTV_Logo As System.Windows.Forms.PictureBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tp1 As System.Windows.Forms.TabPage
    Friend WithEvents tp2 As System.Windows.Forms.TabPage
    Friend WithEvents tp3 As System.Windows.Forms.TabPage
    Friend WithEvents cms_missing As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents SendToImporterToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescanAllToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeIMDBTMDBNumberToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescanToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents l_imdb_id As System.Windows.Forms.Label
    Friend WithEvents pb_current As System.Windows.Forms.PictureBox
    Friend WithEvents lv_available As System.Windows.Forms.ListView
    Friend WithEvents bw_import As System.ComponentModel.BackgroundWorker
    Friend WithEvents lv_import As System.Windows.Forms.ListView
    Friend WithEvents il_state As System.Windows.Forms.ImageList
    Friend WithEvents i_Movie As System.Windows.Forms.ColumnHeader
    Friend WithEvents l_movingpictures As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cms_import As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RestartImporterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents i_IMDb_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents lv_missing As System.Windows.Forms.ListView
    Friend WithEvents m_Movie As System.Windows.Forms.ColumnHeader
    Friend WithEvents m_IMDb_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents lv_movies As System.Windows.Forms.ListView
    Friend WithEvents Movie As System.Windows.Forms.ColumnHeader
    Friend WithEvents IMDb_id As System.Windows.Forms.ColumnHeader
    Friend WithEvents tp4 As System.Windows.Forms.TabPage
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

End Class
