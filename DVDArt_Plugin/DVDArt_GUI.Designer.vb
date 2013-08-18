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
        Me.ChangeMBIDToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
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
        Me.ChangeMBIDToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.bw_import = New System.ComponentModel.BackgroundWorker()
        Me.tbc_main = New System.Windows.Forms.TabControl()
        Me.tp_MovingPictures = New System.Windows.Forms.TabPage()
        Me.pb_movingpictures = New System.Windows.Forms.PictureBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tp_movies = New System.Windows.Forms.TabPage()
        Me.lv_movies = New System.Windows.Forms.ListView()
        Me.Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_movies = New System.Windows.Forms.TabControl()
        Me.tp_Movie_DVDArt = New System.Windows.Forms.TabPage()
        Me.b_movie_delete = New System.Windows.Forms.Button()
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
        Me.tp_movies_missing = New System.Windows.Forms.TabPage()
        Me.lv_movies_missing = New System.Windows.Forms.ListView()
        Me.m_Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_DVDArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_ClearArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pbFTV_Logo = New System.Windows.Forms.PictureBox()
        Me.tp_TVSeries = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tp_series = New System.Windows.Forms.TabPage()
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
        Me.tp_series_missing = New System.Windows.Forms.TabPage()
        Me.lv_series_missing = New System.Windows.Forms.ListView()
        Me.c_Serie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_ClearArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.tp_Music = New System.Windows.Forms.TabPage()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.tbc_music = New System.Windows.Forms.TabControl()
        Me.tp_artists = New System.Windows.Forms.TabPage()
        Me.lv_artist = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_artist = New System.Windows.Forms.TabControl()
        Me.tp_artist_banner = New System.Windows.Forms.TabPage()
        Me.b_artist_deletebanner = New System.Windows.Forms.Button()
        Me.pb_artist_banner = New System.Windows.Forms.PictureBox()
        Me.lv_artist_banner = New System.Windows.Forms.ListView()
        Me.il_banner = New System.Windows.Forms.ImageList(Me.components)
        Me.tp_artist_clearlogo = New System.Windows.Forms.TabPage()
        Me.b_artist_deletelogo = New System.Windows.Forms.Button()
        Me.pb_artist_clearlogo = New System.Windows.Forms.PictureBox()
        Me.lv_artist_clearlogo = New System.Windows.Forms.ListView()
        Me.tp_albums = New System.Windows.Forms.TabPage()
        Me.lv_album = New System.Windows.Forms.ListView()
        Me.Album = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MBID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_album = New System.Windows.Forms.TabControl()
        Me.tp_Music_CDArt = New System.Windows.Forms.TabPage()
        Me.b_album_delete = New System.Windows.Forms.Button()
        Me.b_album_preview = New System.Windows.Forms.Button()
        Me.b_album_compress = New System.Windows.Forms.Button()
        Me.l_music_size = New System.Windows.Forms.Label()
        Me.lv_album_cdart = New System.Windows.Forms.ListView()
        Me.pb_album_cdart = New System.Windows.Forms.PictureBox()
        Me.tp_artist_album_missing = New System.Windows.Forms.TabPage()
        Me.lv_music_missing = New System.Windows.Forms.ListView()
        Me.u_Artist_Music = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_CDArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_Banner = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_MBID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tp_Importer = New System.Windows.Forms.TabPage()
        Me.lv_import = New System.Windows.Forms.ListView()
        Me.i_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tp_Settings = New System.Windows.Forms.TabPage()
        Me.gb2 = New System.Windows.Forms.GroupBox()
        Me.tbc_scraper = New System.Windows.Forms.TabControl()
        Me.tp_sMovies = New System.Windows.Forms.TabPage()
        Me.TabControl3 = New System.Windows.Forms.TabControl()
        Me.tp_movies_settings = New System.Windows.Forms.TabPage()
        Me.pb2 = New System.Windows.Forms.PictureBox()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.pb3 = New System.Windows.Forms.PictureBox()
        Me.cb_DVDArt_movies = New System.Windows.Forms.CheckBox()
        Me.cb_ClearArt_movies = New System.Windows.Forms.CheckBox()
        Me.cb_ClearLogo_movies = New System.Windows.Forms.CheckBox()
        Me.tp_manual_dvdart = New System.Windows.Forms.TabPage()
        Me.rb_t2 = New System.Windows.Forms.RadioButton()
        Me.rb_t1 = New System.Windows.Forms.RadioButton()
        Me.tp_sSeries = New System.Windows.Forms.TabPage()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.cb_ClearLogo_series = New System.Windows.Forms.CheckBox()
        Me.cb_ClearArt_series = New System.Windows.Forms.CheckBox()
        Me.tp_sMusic = New System.Windows.Forms.TabPage()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.cb_ClearLogo_artist = New System.Windows.Forms.CheckBox()
        Me.cb_Banner_artist = New System.Windows.Forms.CheckBox()
        Me.cb_CDArt_music = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_language = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.gb1 = New System.Windows.Forms.GroupBox()
        Me.cb_backgroundscraper = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cb_delay = New System.Windows.Forms.ComboBox()
        Me.nud_delay = New System.Windows.Forms.NumericUpDown()
        Me.cb_missing = New System.Windows.Forms.ComboBox()
        Me.nud_missing = New System.Windows.Forms.NumericUpDown()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cb_scraping = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.nud_scraping = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.mtb_cpu = New System.Windows.Forms.MaskedTextBox()
        Me.il_column = New System.Windows.Forms.ImageList(Me.components)
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MUploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendToImporterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.u_Artist = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cms_found.SuspendLayout()
        Me.cms_import.SuspendLayout()
        Me.cms_missing.SuspendLayout()
        Me.tbc_main.SuspendLayout()
        Me.tp_MovingPictures.SuspendLayout()
        CType(Me.pb_movingpictures, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.tp_movies.SuspendLayout()
        Me.tbc_movies.SuspendLayout()
        Me.tp_Movie_DVDArt.SuspendLayout()
        CType(Me.pb_movie_dvdart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Movie_ClearArt.SuspendLayout()
        CType(Me.pb_movie_clearart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Movie_ClearLogo.SuspendLayout()
        CType(Me.pb_movie_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_movies_missing.SuspendLayout()
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_TVSeries.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tp_series.SuspendLayout()
        Me.tbc_series.SuspendLayout()
        Me.tp_Serie_ClearArt.SuspendLayout()
        CType(Me.pb_serie_clearart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Serie_ClearLogo.SuspendLayout()
        CType(Me.pb_serie_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_series_missing.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_Music.SuspendLayout()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbc_music.SuspendLayout()
        Me.tp_artists.SuspendLayout()
        Me.tbc_artist.SuspendLayout()
        Me.tp_artist_banner.SuspendLayout()
        CType(Me.pb_artist_banner, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_artist_clearlogo.SuspendLayout()
        CType(Me.pb_artist_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_albums.SuspendLayout()
        Me.tbc_album.SuspendLayout()
        Me.tp_Music_CDArt.SuspendLayout()
        CType(Me.pb_album_cdart, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_artist_album_missing.SuspendLayout()
        Me.tp_Importer.SuspendLayout()
        Me.tp_Settings.SuspendLayout()
        Me.gb2.SuspendLayout()
        Me.tbc_scraper.SuspendLayout()
        Me.tp_sMovies.SuspendLayout()
        Me.TabControl3.SuspendLayout()
        Me.tp_movies_settings.SuspendLayout()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_manual_dvdart.SuspendLayout()
        Me.tp_sSeries.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_sMusic.SuspendLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cms_found.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshArtworkFromOnlineToolStripMenuItem, Me.ManuallyUploadArtworkToolStripMenuItem, Me.SelectCoverArtForDVDArtToolStripMenuItem2, Me.ChangeMBIDToolStripMenuItem})
        Me.cms_found.Name = "cms_movies"
        Me.cms_found.Size = New System.Drawing.Size(247, 92)
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
        'ChangeMBIDToolStripMenuItem
        '
        Me.ChangeMBIDToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.musicbrainz_picard
        Me.ChangeMBIDToolStripMenuItem.Name = "ChangeMBIDToolStripMenuItem"
        Me.ChangeMBIDToolStripMenuItem.Size = New System.Drawing.Size(246, 22)
        Me.ChangeMBIDToolStripMenuItem.Text = "Change MBID"
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
        Me.cms_missing.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendToolStripMenuItem, Me.RescanToolStripMenuItem, Me.UploadToolStripMenuItem, Me.SelectCoverArtForDVDArtToolStripMenuItem, Me.UseCoverArtForDVDArtToolStripMenuItem, Me.ChangeMBIDToolStripMenuItem1})
        Me.cms_missing.Name = "cms_missing"
        Me.cms_missing.Size = New System.Drawing.Size(247, 158)
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
        'ChangeMBIDToolStripMenuItem1
        '
        Me.ChangeMBIDToolStripMenuItem1.Image = Global.DVDArt_Plugin.My.Resources.Resources.musicbrainz_picard
        Me.ChangeMBIDToolStripMenuItem1.Name = "ChangeMBIDToolStripMenuItem1"
        Me.ChangeMBIDToolStripMenuItem1.Size = New System.Drawing.Size(246, 22)
        Me.ChangeMBIDToolStripMenuItem1.Text = "Change MBID"
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
        Me.tbc_main.Controls.Add(Me.tp_Music)
        Me.tbc_main.Controls.Add(Me.tp_Importer)
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
        Me.TabControl1.Controls.Add(Me.tp_movies)
        Me.TabControl1.Controls.Add(Me.tp_movies_missing)
        Me.TabControl1.Location = New System.Drawing.Point(4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(650, 593)
        Me.TabControl1.TabIndex = 30
        '
        'tp_movies
        '
        Me.tp_movies.BackColor = System.Drawing.SystemColors.Control
        Me.tp_movies.Controls.Add(Me.lv_movies)
        Me.tp_movies.Controls.Add(Me.tbc_movies)
        Me.tp_movies.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies.Name = "tp_movies"
        Me.tp_movies.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies.Size = New System.Drawing.Size(642, 567)
        Me.tp_movies.TabIndex = 0
        Me.tp_movies.Text = "Movies with Artwork"
        Me.tp_movies.ToolTipText = "This tab contails all movies that already have a DVD artwork."
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
        Me.lv_movies.Size = New System.Drawing.Size(340, 553)
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
        Me.tp_Movie_DVDArt.Controls.Add(Me.b_movie_delete)
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
        'b_movie_delete
        '
        Me.b_movie_delete.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_movie_delete.Location = New System.Drawing.Point(234, 53)
        Me.b_movie_delete.Name = "b_movie_delete"
        Me.b_movie_delete.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_delete.TabIndex = 18
        Me.b_movie_delete.UseVisualStyleBackColor = True
        Me.b_movie_delete.Visible = False
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
        'tp_movies_missing
        '
        Me.tp_movies_missing.BackColor = System.Drawing.SystemColors.Control
        Me.tp_movies_missing.Controls.Add(Me.lv_movies_missing)
        Me.tp_movies_missing.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies_missing.Name = "tp_movies_missing"
        Me.tp_movies_missing.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies_missing.Size = New System.Drawing.Size(642, 567)
        Me.tp_movies_missing.TabIndex = 2
        Me.tp_movies_missing.Text = "Movies missing Artwork"
        '
        'lv_movies_missing
        '
        Me.lv_movies_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movies_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.m_Movie, Me.m_DVDArt, Me.m_ClearArt, Me.m_ClearLogo, Me.m_IMDb_id})
        Me.lv_movies_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_movies_missing.FullRowSelect = True
        Me.lv_movies_missing.GridLines = True
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
        Me.TabControl2.Controls.Add(Me.tp_series)
        Me.TabControl2.Controls.Add(Me.tp_series_missing)
        Me.TabControl2.Location = New System.Drawing.Point(4, 3)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.Size = New System.Drawing.Size(650, 593)
        Me.TabControl2.TabIndex = 32
        '
        'tp_series
        '
        Me.tp_series.BackColor = System.Drawing.SystemColors.Control
        Me.tp_series.Controls.Add(Me.lv_series)
        Me.tp_series.Controls.Add(Me.tbc_series)
        Me.tp_series.Location = New System.Drawing.Point(4, 22)
        Me.tp_series.Name = "tp_series"
        Me.tp_series.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_series.Size = New System.Drawing.Size(642, 567)
        Me.tp_series.TabIndex = 0
        Me.tp_series.Text = "Series with Artwork"
        Me.tp_series.ToolTipText = "This tab contails all movies that already have a DVD artwork."
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
        Me.lv_series.Size = New System.Drawing.Size(340, 553)
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
        'tp_series_missing
        '
        Me.tp_series_missing.BackColor = System.Drawing.SystemColors.Control
        Me.tp_series_missing.Controls.Add(Me.lv_series_missing)
        Me.tp_series_missing.Location = New System.Drawing.Point(4, 22)
        Me.tp_series_missing.Name = "tp_series_missing"
        Me.tp_series_missing.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_series_missing.Size = New System.Drawing.Size(642, 567)
        Me.tp_series_missing.TabIndex = 2
        Me.tp_series_missing.Text = "Series missing Artwork"
        '
        'lv_series_missing
        '
        Me.lv_series_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_series_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c_Serie, Me.c_ClearArt, Me.c_ClearLogo, Me.c_id})
        Me.lv_series_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_series_missing.FullRowSelect = True
        Me.lv_series_missing.GridLines = True
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
        'tp_Music
        '
        Me.tp_Music.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Music.Controls.Add(Me.PictureBox10)
        Me.tp_Music.Controls.Add(Me.PictureBox9)
        Me.tp_Music.Controls.Add(Me.PictureBox5)
        Me.tp_Music.Controls.Add(Me.tbc_music)
        Me.tp_Music.Location = New System.Drawing.Point(4, 22)
        Me.tp_Music.Name = "tp_Music"
        Me.tp_Music.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Music.Size = New System.Drawing.Size(656, 650)
        Me.tp_Music.TabIndex = 4
        Me.tp_Music.Text = "Music"
        '
        'PictureBox10
        '
        Me.PictureBox10.Image = Global.DVDArt_Plugin.My.Resources.Resources.lastfm
        Me.PictureBox10.Location = New System.Drawing.Point(598, 635)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(55, 14)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox10.TabIndex = 34
        Me.PictureBox10.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox9.Image = Global.DVDArt_Plugin.My.Resources.Resources.music
        Me.PictureBox9.Location = New System.Drawing.Point(259, 596)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(208, 59)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox9.TabIndex = 33
        Me.PictureBox9.TabStop = False
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.PictureBox5.Location = New System.Drawing.Point(469, 601)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(258, 48)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 32
        Me.PictureBox5.TabStop = False
        '
        'tbc_music
        '
        Me.tbc_music.Controls.Add(Me.tp_artists)
        Me.tbc_music.Controls.Add(Me.tp_albums)
        Me.tbc_music.Controls.Add(Me.tp_artist_album_missing)
        Me.tbc_music.Location = New System.Drawing.Point(4, 3)
        Me.tbc_music.Name = "tbc_music"
        Me.tbc_music.SelectedIndex = 0
        Me.tbc_music.Size = New System.Drawing.Size(650, 593)
        Me.tbc_music.TabIndex = 31
        '
        'tp_artists
        '
        Me.tp_artists.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_artists.Controls.Add(Me.lv_artist)
        Me.tp_artists.Controls.Add(Me.tbc_artist)
        Me.tp_artists.Location = New System.Drawing.Point(4, 22)
        Me.tp_artists.Name = "tp_artists"
        Me.tp_artists.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_artists.Size = New System.Drawing.Size(642, 567)
        Me.tp_artists.TabIndex = 3
        Me.tp_artists.Text = "Artists with Artwork"
        '
        'lv_artist
        '
        Me.lv_artist.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_artist.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4, Me.ColumnHeader5})
        Me.lv_artist.ContextMenuStrip = Me.cms_found
        Me.lv_artist.FullRowSelect = True
        Me.lv_artist.GridLines = True
        Me.lv_artist.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_artist.Location = New System.Drawing.Point(8, 8)
        Me.lv_artist.Name = "lv_artist"
        Me.lv_artist.Size = New System.Drawing.Size(340, 553)
        Me.lv_artist.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_artist.TabIndex = 13
        Me.lv_artist.UseCompatibleStateImageBehavior = False
        Me.lv_artist.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Artist"
        Me.ColumnHeader4.Width = 180
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "MBID"
        Me.ColumnHeader5.Width = 220
        '
        'tbc_artist
        '
        Me.tbc_artist.Controls.Add(Me.tp_artist_banner)
        Me.tbc_artist.Controls.Add(Me.tp_artist_clearlogo)
        Me.tbc_artist.Location = New System.Drawing.Point(352, 8)
        Me.tbc_artist.Name = "tbc_artist"
        Me.tbc_artist.SelectedIndex = 0
        Me.tbc_artist.Size = New System.Drawing.Size(284, 553)
        Me.tbc_artist.TabIndex = 14
        '
        'tp_artist_banner
        '
        Me.tp_artist_banner.BackColor = System.Drawing.SystemColors.Control
        Me.tp_artist_banner.Controls.Add(Me.b_artist_deletebanner)
        Me.tp_artist_banner.Controls.Add(Me.pb_artist_banner)
        Me.tp_artist_banner.Controls.Add(Me.lv_artist_banner)
        Me.tp_artist_banner.Location = New System.Drawing.Point(4, 22)
        Me.tp_artist_banner.Name = "tp_artist_banner"
        Me.tp_artist_banner.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_artist_banner.Size = New System.Drawing.Size(276, 527)
        Me.tp_artist_banner.TabIndex = 4
        Me.tp_artist_banner.Text = "Banner"
        '
        'b_artist_deletebanner
        '
        Me.b_artist_deletebanner.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_artist_deletebanner.Location = New System.Drawing.Point(234, 7)
        Me.b_artist_deletebanner.Name = "b_artist_deletebanner"
        Me.b_artist_deletebanner.Size = New System.Drawing.Size(40, 40)
        Me.b_artist_deletebanner.TabIndex = 17
        Me.b_artist_deletebanner.UseVisualStyleBackColor = True
        Me.b_artist_deletebanner.Visible = False
        '
        'pb_artist_banner
        '
        Me.pb_artist_banner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_artist_banner.Location = New System.Drawing.Point(12, 7)
        Me.pb_artist_banner.Name = "pb_artist_banner"
        Me.pb_artist_banner.Size = New System.Drawing.Size(222, 41)
        Me.pb_artist_banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_artist_banner.TabIndex = 14
        Me.pb_artist_banner.TabStop = False
        '
        'lv_artist_banner
        '
        Me.lv_artist_banner.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_artist_banner.BackColor = System.Drawing.SystemColors.Control
        Me.lv_artist_banner.ForeColor = System.Drawing.Color.Black
        Me.lv_artist_banner.LargeImageList = Me.il_banner
        Me.lv_artist_banner.Location = New System.Drawing.Point(12, 54)
        Me.lv_artist_banner.MultiSelect = False
        Me.lv_artist_banner.Name = "lv_artist_banner"
        Me.lv_artist_banner.Size = New System.Drawing.Size(252, 466)
        Me.lv_artist_banner.TabIndex = 16
        Me.lv_artist_banner.UseCompatibleStateImageBehavior = False
        '
        'il_banner
        '
        Me.il_banner.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_banner.ImageSize = New System.Drawing.Size(200, 37)
        Me.il_banner.TransparentColor = System.Drawing.Color.Transparent
        '
        'tp_artist_clearlogo
        '
        Me.tp_artist_clearlogo.BackColor = System.Drawing.SystemColors.Control
        Me.tp_artist_clearlogo.Controls.Add(Me.b_artist_deletelogo)
        Me.tp_artist_clearlogo.Controls.Add(Me.pb_artist_clearlogo)
        Me.tp_artist_clearlogo.Controls.Add(Me.lv_artist_clearlogo)
        Me.tp_artist_clearlogo.Location = New System.Drawing.Point(4, 22)
        Me.tp_artist_clearlogo.Name = "tp_artist_clearlogo"
        Me.tp_artist_clearlogo.Size = New System.Drawing.Size(276, 527)
        Me.tp_artist_clearlogo.TabIndex = 2
        Me.tp_artist_clearlogo.Text = "ClearLogo"
        '
        'b_artist_deletelogo
        '
        Me.b_artist_deletelogo.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_artist_deletelogo.Location = New System.Drawing.Point(234, 7)
        Me.b_artist_deletelogo.Name = "b_artist_deletelogo"
        Me.b_artist_deletelogo.Size = New System.Drawing.Size(39, 40)
        Me.b_artist_deletelogo.TabIndex = 20
        Me.b_artist_deletelogo.UseVisualStyleBackColor = True
        Me.b_artist_deletelogo.Visible = False
        '
        'pb_artist_clearlogo
        '
        Me.pb_artist_clearlogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_artist_clearlogo.Location = New System.Drawing.Point(34, 7)
        Me.pb_artist_clearlogo.Name = "pb_artist_clearlogo"
        Me.pb_artist_clearlogo.Size = New System.Drawing.Size(200, 77)
        Me.pb_artist_clearlogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_artist_clearlogo.TabIndex = 17
        Me.pb_artist_clearlogo.TabStop = False
        '
        'lv_artist_clearlogo
        '
        Me.lv_artist_clearlogo.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_artist_clearlogo.BackColor = System.Drawing.SystemColors.Control
        Me.lv_artist_clearlogo.ForeColor = System.Drawing.Color.Black
        Me.lv_artist_clearlogo.LargeImageList = Me.il_clearlogo
        Me.lv_artist_clearlogo.Location = New System.Drawing.Point(12, 90)
        Me.lv_artist_clearlogo.MultiSelect = False
        Me.lv_artist_clearlogo.Name = "lv_artist_clearlogo"
        Me.lv_artist_clearlogo.Size = New System.Drawing.Size(252, 430)
        Me.lv_artist_clearlogo.TabIndex = 19
        Me.lv_artist_clearlogo.UseCompatibleStateImageBehavior = False
        '
        'tp_albums
        '
        Me.tp_albums.BackColor = System.Drawing.SystemColors.Control
        Me.tp_albums.Controls.Add(Me.lv_album)
        Me.tp_albums.Controls.Add(Me.tbc_album)
        Me.tp_albums.Location = New System.Drawing.Point(4, 22)
        Me.tp_albums.Name = "tp_albums"
        Me.tp_albums.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_albums.Size = New System.Drawing.Size(642, 567)
        Me.tp_albums.TabIndex = 0
        Me.tp_albums.Text = "Albums with Artwork"
        Me.tp_albums.ToolTipText = "This tab contails all movies that already have a DVD artwork."
        '
        'lv_album
        '
        Me.lv_album.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_album.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.Album, Me.MBID})
        Me.lv_album.ContextMenuStrip = Me.cms_found
        Me.lv_album.FullRowSelect = True
        Me.lv_album.GridLines = True
        Me.lv_album.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_album.Location = New System.Drawing.Point(8, 8)
        Me.lv_album.Name = "lv_album"
        Me.lv_album.Size = New System.Drawing.Size(340, 553)
        Me.lv_album.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_album.TabIndex = 11
        Me.lv_album.UseCompatibleStateImageBehavior = False
        Me.lv_album.View = System.Windows.Forms.View.Details
        '
        'Album
        '
        Me.Album.Text = "Music Album"
        Me.Album.Width = 180
        '
        'MBID
        '
        Me.MBID.Text = "MBID"
        Me.MBID.Width = 220
        '
        'tbc_album
        '
        Me.tbc_album.Controls.Add(Me.tp_Music_CDArt)
        Me.tbc_album.Location = New System.Drawing.Point(352, 8)
        Me.tbc_album.Name = "tbc_album"
        Me.tbc_album.SelectedIndex = 0
        Me.tbc_album.Size = New System.Drawing.Size(284, 553)
        Me.tbc_album.TabIndex = 12
        '
        'tp_Music_CDArt
        '
        Me.tp_Music_CDArt.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Music_CDArt.Controls.Add(Me.b_album_delete)
        Me.tp_Music_CDArt.Controls.Add(Me.b_album_preview)
        Me.tp_Music_CDArt.Controls.Add(Me.b_album_compress)
        Me.tp_Music_CDArt.Controls.Add(Me.l_music_size)
        Me.tp_Music_CDArt.Controls.Add(Me.lv_album_cdart)
        Me.tp_Music_CDArt.Controls.Add(Me.pb_album_cdart)
        Me.tp_Music_CDArt.Location = New System.Drawing.Point(4, 22)
        Me.tp_Music_CDArt.Name = "tp_Music_CDArt"
        Me.tp_Music_CDArt.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Music_CDArt.Size = New System.Drawing.Size(276, 527)
        Me.tp_Music_CDArt.TabIndex = 0
        Me.tp_Music_CDArt.Text = "CDArt"
        '
        'b_album_delete
        '
        Me.b_album_delete.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_album_delete.Location = New System.Drawing.Point(234, 53)
        Me.b_album_delete.Name = "b_album_delete"
        Me.b_album_delete.Size = New System.Drawing.Size(40, 40)
        Me.b_album_delete.TabIndex = 18
        Me.b_album_delete.UseVisualStyleBackColor = True
        Me.b_album_delete.Visible = False
        '
        'b_album_preview
        '
        Me.b_album_preview.Image = Global.DVDArt_Plugin.My.Resources.Resources.preview
        Me.b_album_preview.Location = New System.Drawing.Point(234, 7)
        Me.b_album_preview.Name = "b_album_preview"
        Me.b_album_preview.Size = New System.Drawing.Size(40, 40)
        Me.b_album_preview.TabIndex = 17
        Me.b_album_preview.UseVisualStyleBackColor = True
        Me.b_album_preview.Visible = False
        '
        'b_album_compress
        '
        Me.b_album_compress.Image = Global.DVDArt_Plugin.My.Resources.Resources.compress
        Me.b_album_compress.Location = New System.Drawing.Point(234, 7)
        Me.b_album_compress.Name = "b_album_compress"
        Me.b_album_compress.Size = New System.Drawing.Size(40, 40)
        Me.b_album_compress.TabIndex = 16
        Me.b_album_compress.UseVisualStyleBackColor = True
        Me.b_album_compress.Visible = False
        '
        'l_music_size
        '
        Me.l_music_size.AutoSize = True
        Me.l_music_size.Location = New System.Drawing.Point(9, 195)
        Me.l_music_size.Name = "l_music_size"
        Me.l_music_size.Size = New System.Drawing.Size(24, 13)
        Me.l_music_size.TabIndex = 14
        Me.l_music_size.Text = "0x0"
        Me.l_music_size.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lv_album_cdart
        '
        Me.lv_album_cdart.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_album_cdart.BackColor = System.Drawing.SystemColors.Control
        Me.lv_album_cdart.ForeColor = System.Drawing.Color.Black
        Me.lv_album_cdart.LargeImageList = Me.il_dvdart
        Me.lv_album_cdart.Location = New System.Drawing.Point(12, 215)
        Me.lv_album_cdart.MultiSelect = False
        Me.lv_album_cdart.Name = "lv_album_cdart"
        Me.lv_album_cdart.Size = New System.Drawing.Size(252, 306)
        Me.lv_album_cdart.TabIndex = 13
        Me.lv_album_cdart.UseCompatibleStateImageBehavior = False
        '
        'pb_album_cdart
        '
        Me.pb_album_cdart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_album_cdart.Location = New System.Drawing.Point(34, 8)
        Me.pb_album_cdart.Name = "pb_album_cdart"
        Me.pb_album_cdart.Size = New System.Drawing.Size(200, 200)
        Me.pb_album_cdart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_album_cdart.TabIndex = 11
        Me.pb_album_cdart.TabStop = False
        '
        'tp_artist_album_missing
        '
        Me.tp_artist_album_missing.BackColor = System.Drawing.SystemColors.Control
        Me.tp_artist_album_missing.Controls.Add(Me.lv_music_missing)
        Me.tp_artist_album_missing.Location = New System.Drawing.Point(4, 22)
        Me.tp_artist_album_missing.Name = "tp_artist_album_missing"
        Me.tp_artist_album_missing.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_artist_album_missing.Size = New System.Drawing.Size(642, 567)
        Me.tp_artist_album_missing.TabIndex = 2
        Me.tp_artist_album_missing.Text = "Artists/Albums missing Artwork"
        '
        'lv_music_missing
        '
        Me.lv_music_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_music_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.u_Artist_Music, Me.u_CDArt, Me.u_Banner, Me.u_ClearLogo, Me.u_MBID, Me.u_Artist})
        Me.lv_music_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_music_missing.FullRowSelect = True
        Me.lv_music_missing.GridLines = True
        Me.lv_music_missing.Location = New System.Drawing.Point(8, 8)
        Me.lv_music_missing.Name = "lv_music_missing"
        Me.lv_music_missing.Size = New System.Drawing.Size(628, 550)
        Me.lv_music_missing.SmallImageList = Me.il_state
        Me.lv_music_missing.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_music_missing.TabIndex = 10
        Me.lv_music_missing.UseCompatibleStateImageBehavior = False
        Me.lv_music_missing.View = System.Windows.Forms.View.Details
        '
        'u_Artist_Music
        '
        Me.u_Artist_Music.Text = "Artist/Music Album"
        Me.u_Artist_Music.Width = 238
        '
        'u_CDArt
        '
        Me.u_CDArt.Text = "CDArt"
        Me.u_CDArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.u_CDArt.Width = 45
        '
        'u_Banner
        '
        Me.u_Banner.Text = "Banner"
        Me.u_Banner.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.u_Banner.Width = 48
        '
        'u_ClearLogo
        '
        Me.u_ClearLogo.Text = "ClearLogo"
        Me.u_ClearLogo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'u_MBID
        '
        Me.u_MBID.Text = "MBID"
        Me.u_MBID.Width = 216
        '
        'tp_Importer
        '
        Me.tp_Importer.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Importer.Controls.Add(Me.lv_import)
        Me.tp_Importer.Location = New System.Drawing.Point(4, 22)
        Me.tp_Importer.Name = "tp_Importer"
        Me.tp_Importer.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Importer.Size = New System.Drawing.Size(656, 650)
        Me.tp_Importer.TabIndex = 3
        Me.tp_Importer.Text = "Importer"
        '
        'lv_import
        '
        Me.lv_import.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_import.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.i_Name, Me.i_ID, Me.i_Type})
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
        'i_Name
        '
        Me.i_Name.Text = "Movie/TVSeries/Music"
        Me.i_Name.Width = 333
        '
        'i_ID
        '
        Me.i_ID.Text = "ID"
        Me.i_ID.Width = 220
        '
        'i_Type
        '
        Me.i_Type.Text = "Type"
        Me.i_Type.Width = 54
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
        Me.gb2.Controls.Add(Me.tbc_scraper)
        Me.gb2.Controls.Add(Me.Label7)
        Me.gb2.Controls.Add(Me.cb_language)
        Me.gb2.Controls.Add(Me.Label6)
        Me.gb2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb2.Location = New System.Drawing.Point(36, 227)
        Me.gb2.Name = "gb2"
        Me.gb2.Size = New System.Drawing.Size(584, 366)
        Me.gb2.TabIndex = 21
        Me.gb2.TabStop = False
        Me.gb2.Text = " Scrape for "
        '
        'tbc_scraper
        '
        Me.tbc_scraper.Controls.Add(Me.tp_sMovies)
        Me.tbc_scraper.Controls.Add(Me.tp_sSeries)
        Me.tbc_scraper.Controls.Add(Me.tp_sMusic)
        Me.tbc_scraper.Location = New System.Drawing.Point(14, 22)
        Me.tbc_scraper.Name = "tbc_scraper"
        Me.tbc_scraper.SelectedIndex = 0
        Me.tbc_scraper.Size = New System.Drawing.Size(554, 280)
        Me.tbc_scraper.TabIndex = 9
        '
        'tp_sMovies
        '
        Me.tp_sMovies.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_sMovies.Controls.Add(Me.TabControl3)
        Me.tp_sMovies.Location = New System.Drawing.Point(4, 25)
        Me.tp_sMovies.Name = "tp_sMovies"
        Me.tp_sMovies.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_sMovies.Size = New System.Drawing.Size(546, 251)
        Me.tp_sMovies.TabIndex = 0
        Me.tp_sMovies.Text = "Movies"
        '
        'TabControl3
        '
        Me.TabControl3.Controls.Add(Me.tp_movies_settings)
        Me.TabControl3.Controls.Add(Me.tp_manual_dvdart)
        Me.TabControl3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl3.Location = New System.Drawing.Point(6, 6)
        Me.TabControl3.Name = "TabControl3"
        Me.TabControl3.SelectedIndex = 0
        Me.TabControl3.Size = New System.Drawing.Size(533, 239)
        Me.TabControl3.TabIndex = 18
        '
        'tp_movies_settings
        '
        Me.tp_movies_settings.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_movies_settings.Controls.Add(Me.pb2)
        Me.tp_movies_settings.Controls.Add(Me.pb1)
        Me.tp_movies_settings.Controls.Add(Me.pb3)
        Me.tp_movies_settings.Controls.Add(Me.cb_DVDArt_movies)
        Me.tp_movies_settings.Controls.Add(Me.cb_ClearArt_movies)
        Me.tp_movies_settings.Controls.Add(Me.cb_ClearLogo_movies)
        Me.tp_movies_settings.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies_settings.Name = "tp_movies_settings"
        Me.tp_movies_settings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies_settings.Size = New System.Drawing.Size(525, 213)
        Me.tp_movies_settings.TabIndex = 0
        Me.tp_movies_settings.Text = "Scraper options"
        '
        'pb2
        '
        Me.pb2.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_clearart
        Me.pb2.Location = New System.Drawing.Point(336, 109)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(100, 56)
        Me.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb2.TabIndex = 17
        Me.pb2.TabStop = False
        '
        'pb1
        '
        Me.pb1.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_dvdart
        Me.pb1.Location = New System.Drawing.Point(336, 6)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(100, 100)
        Me.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb1.TabIndex = 15
        Me.pb1.TabStop = False
        '
        'pb3
        '
        Me.pb3.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_clearlogo
        Me.pb3.Location = New System.Drawing.Point(336, 168)
        Me.pb3.Name = "pb3"
        Me.pb3.Size = New System.Drawing.Size(100, 39)
        Me.pb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb3.TabIndex = 16
        Me.pb3.TabStop = False
        '
        'cb_DVDArt_movies
        '
        Me.cb_DVDArt_movies.AutoSize = True
        Me.cb_DVDArt_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_DVDArt_movies.Location = New System.Drawing.Point(89, 45)
        Me.cb_DVDArt_movies.Name = "cb_DVDArt_movies"
        Me.cb_DVDArt_movies.Size = New System.Drawing.Size(75, 20)
        Me.cb_DVDArt_movies.TabIndex = 12
        Me.cb_DVDArt_movies.Text = "DVD Art"
        Me.cb_DVDArt_movies.UseVisualStyleBackColor = True
        '
        'cb_ClearArt_movies
        '
        Me.cb_ClearArt_movies.AutoSize = True
        Me.cb_ClearArt_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_ClearArt_movies.Location = New System.Drawing.Point(89, 125)
        Me.cb_ClearArt_movies.Name = "cb_ClearArt_movies"
        Me.cb_ClearArt_movies.Size = New System.Drawing.Size(178, 20)
        Me.cb_ClearArt_movies.TabIndex = 13
        Me.cb_ClearArt_movies.Text = "ClearArt and HD Clear Art"
        Me.cb_ClearArt_movies.UseVisualStyleBackColor = True
        '
        'cb_ClearLogo_movies
        '
        Me.cb_ClearLogo_movies.AutoSize = True
        Me.cb_ClearLogo_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_ClearLogo_movies.Location = New System.Drawing.Point(89, 178)
        Me.cb_ClearLogo_movies.Name = "cb_ClearLogo_movies"
        Me.cb_ClearLogo_movies.Size = New System.Drawing.Size(90, 20)
        Me.cb_ClearLogo_movies.TabIndex = 14
        Me.cb_ClearLogo_movies.Text = "ClearLogo"
        Me.cb_ClearLogo_movies.UseVisualStyleBackColor = True
        '
        'tp_manual_dvdart
        '
        Me.tp_manual_dvdart.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_manual_dvdart.Controls.Add(Me.rb_t2)
        Me.tp_manual_dvdart.Controls.Add(Me.rb_t1)
        Me.tp_manual_dvdart.Location = New System.Drawing.Point(4, 22)
        Me.tp_manual_dvdart.Name = "tp_manual_dvdart"
        Me.tp_manual_dvdart.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_manual_dvdart.Size = New System.Drawing.Size(525, 213)
        Me.tp_manual_dvdart.TabIndex = 1
        Me.tp_manual_dvdart.Text = "Manual DVDArt layout options"
        '
        'rb_t2
        '
        Me.rb_t2.Image = Global.DVDArt_Plugin.My.Resources.Resources.template_2
        Me.rb_t2.Location = New System.Drawing.Point(282, 2)
        Me.rb_t2.Name = "rb_t2"
        Me.rb_t2.Size = New System.Drawing.Size(222, 208)
        Me.rb_t2.TabIndex = 3
        Me.rb_t2.TabStop = True
        Me.rb_t2.UseVisualStyleBackColor = True
        '
        'rb_t1
        '
        Me.rb_t1.Image = Global.DVDArt_Plugin.My.Resources.Resources.template_1
        Me.rb_t1.Location = New System.Drawing.Point(27, 2)
        Me.rb_t1.Name = "rb_t1"
        Me.rb_t1.Size = New System.Drawing.Size(222, 208)
        Me.rb_t1.TabIndex = 2
        Me.rb_t1.TabStop = True
        Me.rb_t1.UseVisualStyleBackColor = True
        '
        'tp_sSeries
        '
        Me.tp_sSeries.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_sSeries.Controls.Add(Me.PictureBox3)
        Me.tp_sSeries.Controls.Add(Me.PictureBox4)
        Me.tp_sSeries.Controls.Add(Me.cb_ClearLogo_series)
        Me.tp_sSeries.Controls.Add(Me.cb_ClearArt_series)
        Me.tp_sSeries.Location = New System.Drawing.Point(4, 25)
        Me.tp_sSeries.Name = "tp_sSeries"
        Me.tp_sSeries.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_sSeries.Size = New System.Drawing.Size(546, 251)
        Me.tp_sSeries.TabIndex = 1
        Me.tp_sSeries.Text = "Series"
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.DVDArt_Plugin.My.Resources.Resources.grimm_clearart
        Me.PictureBox3.Location = New System.Drawing.Point(346, 69)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(100, 56)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 17
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.DVDArt_Plugin.My.Resources.Resources.grimm_clearlogo
        Me.PictureBox4.Location = New System.Drawing.Point(346, 143)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(100, 39)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox4.TabIndex = 16
        Me.PictureBox4.TabStop = False
        '
        'cb_ClearLogo_series
        '
        Me.cb_ClearLogo_series.AutoSize = True
        Me.cb_ClearLogo_series.Location = New System.Drawing.Point(101, 149)
        Me.cb_ClearLogo_series.Name = "cb_ClearLogo_series"
        Me.cb_ClearLogo_series.Size = New System.Drawing.Size(90, 20)
        Me.cb_ClearLogo_series.TabIndex = 14
        Me.cb_ClearLogo_series.Text = "ClearLogo"
        Me.cb_ClearLogo_series.UseVisualStyleBackColor = True
        '
        'cb_ClearArt_series
        '
        Me.cb_ClearArt_series.AutoSize = True
        Me.cb_ClearArt_series.Location = New System.Drawing.Point(101, 85)
        Me.cb_ClearArt_series.Name = "cb_ClearArt_series"
        Me.cb_ClearArt_series.Size = New System.Drawing.Size(178, 20)
        Me.cb_ClearArt_series.TabIndex = 13
        Me.cb_ClearArt_series.Text = "ClearArt and HD Clear Art"
        Me.cb_ClearArt_series.UseVisualStyleBackColor = True
        '
        'tp_sMusic
        '
        Me.tp_sMusic.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_sMusic.Controls.Add(Me.PictureBox6)
        Me.tp_sMusic.Controls.Add(Me.PictureBox7)
        Me.tp_sMusic.Controls.Add(Me.PictureBox8)
        Me.tp_sMusic.Controls.Add(Me.cb_ClearLogo_artist)
        Me.tp_sMusic.Controls.Add(Me.cb_Banner_artist)
        Me.tp_sMusic.Controls.Add(Me.cb_CDArt_music)
        Me.tp_sMusic.Location = New System.Drawing.Point(4, 25)
        Me.tp_sMusic.Name = "tp_sMusic"
        Me.tp_sMusic.Size = New System.Drawing.Size(546, 251)
        Me.tp_sMusic.TabIndex = 2
        Me.tp_sMusic.Text = "Music"
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.DVDArt_Plugin.My.Resources.Resources.queen_banner
        Me.PictureBox6.Location = New System.Drawing.Point(297, 132)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(200, 37)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox6.TabIndex = 17
        Me.PictureBox6.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.DVDArt_Plugin.My.Resources.Resources.queen_clearlogo
        Me.PictureBox7.Location = New System.Drawing.Point(297, 185)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(100, 39)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 16
        Me.PictureBox7.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.DVDArt_Plugin.My.Resources.Resources.queen_cdart
        Me.PictureBox8.Location = New System.Drawing.Point(297, 26)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox8.TabIndex = 15
        Me.PictureBox8.TabStop = False
        '
        'cb_ClearLogo_artist
        '
        Me.cb_ClearLogo_artist.AutoSize = True
        Me.cb_ClearLogo_artist.Location = New System.Drawing.Point(50, 191)
        Me.cb_ClearLogo_artist.Name = "cb_ClearLogo_artist"
        Me.cb_ClearLogo_artist.Size = New System.Drawing.Size(173, 20)
        Me.cb_ClearLogo_artist.TabIndex = 14
        Me.cb_ClearLogo_artist.Text = "ClearLogo and HD Logo"
        Me.cb_ClearLogo_artist.UseVisualStyleBackColor = True
        '
        'cb_Banner_artist
        '
        Me.cb_Banner_artist.AutoSize = True
        Me.cb_Banner_artist.Location = New System.Drawing.Point(50, 136)
        Me.cb_Banner_artist.Name = "cb_Banner_artist"
        Me.cb_Banner_artist.Size = New System.Drawing.Size(70, 20)
        Me.cb_Banner_artist.TabIndex = 13
        Me.cb_Banner_artist.Text = "Banner"
        Me.cb_Banner_artist.UseVisualStyleBackColor = True
        '
        'cb_CDArt_music
        '
        Me.cb_CDArt_music.AutoSize = True
        Me.cb_CDArt_music.Location = New System.Drawing.Point(50, 61)
        Me.cb_CDArt_music.Name = "cb_CDArt_music"
        Me.cb_CDArt_music.Size = New System.Drawing.Size(65, 20)
        Me.cb_CDArt_music.TabIndex = 12
        Me.cb_CDArt_music.Text = "CD Art"
        Me.cb_CDArt_music.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(421, 338)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(147, 9)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "(English is by default secondary language)"
        '
        'cb_language
        '
        Me.cb_language.FormattingEnabled = True
        Me.cb_language.Location = New System.Drawing.Point(273, 329)
        Me.cb_language.Name = "cb_language"
        Me.cb_language.Size = New System.Drawing.Size(142, 24)
        Me.cb_language.TabIndex = 7
        Me.cb_language.Text = "Any"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 332)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(194, 16)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Preferred language for Importer"
        '
        'gb1
        '
        Me.gb1.Controls.Add(Me.cb_backgroundscraper)
        Me.gb1.Controls.Add(Me.Panel1)
        Me.gb1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb1.Location = New System.Drawing.Point(36, 19)
        Me.gb1.Name = "gb1"
        Me.gb1.Size = New System.Drawing.Size(584, 190)
        Me.gb1.TabIndex = 20
        Me.gb1.TabStop = False
        Me.gb1.Text = " Background scraper settings "
        '
        'cb_backgroundscraper
        '
        Me.cb_backgroundscraper.AutoSize = True
        Me.cb_backgroundscraper.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cb_backgroundscraper.Location = New System.Drawing.Point(22, 22)
        Me.cb_backgroundscraper.Name = "cb_backgroundscraper"
        Me.cb_backgroundscraper.Size = New System.Drawing.Size(205, 20)
        Me.cb_backgroundscraper.TabIndex = 19
        Me.cb_backgroundscraper.Text = "Background Scraper Enabled"
        Me.cb_backgroundscraper.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cb_delay)
        Me.Panel1.Controls.Add(Me.nud_delay)
        Me.Panel1.Controls.Add(Me.cb_missing)
        Me.Panel1.Controls.Add(Me.nud_missing)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cb_scraping)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.nud_scraping)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.mtb_cpu)
        Me.Panel1.Location = New System.Drawing.Point(17, 32)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(550, 145)
        Me.Panel1.TabIndex = 20
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(20, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(202, 16)
        Me.Label3.TabIndex = 29
        Me.Label3.Text = "on MediaPortal Startup, delay by"
        '
        'cb_delay
        '
        Me.cb_delay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_delay.FormattingEnabled = True
        Me.cb_delay.Items.AddRange(New Object() {"seconds", "minutes", "hours"})
        Me.cb_delay.Location = New System.Drawing.Point(268, 19)
        Me.cb_delay.Name = "cb_delay"
        Me.cb_delay.Size = New System.Drawing.Size(80, 24)
        Me.cb_delay.TabIndex = 28
        Me.cb_delay.Text = "minutes"
        '
        'nud_delay
        '
        Me.nud_delay.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_delay.Location = New System.Drawing.Point(228, 20)
        Me.nud_delay.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nud_delay.Name = "nud_delay"
        Me.nud_delay.Size = New System.Drawing.Size(36, 22)
        Me.nud_delay.TabIndex = 27
        Me.nud_delay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_delay.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'cb_missing
        '
        Me.cb_missing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_missing.FormattingEnabled = True
        Me.cb_missing.Location = New System.Drawing.Point(344, 109)
        Me.cb_missing.Name = "cb_missing"
        Me.cb_missing.Size = New System.Drawing.Size(80, 24)
        Me.cb_missing.TabIndex = 26
        Me.cb_missing.Text = "disabled"
        '
        'nud_missing
        '
        Me.nud_missing.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_missing.Location = New System.Drawing.Point(304, 110)
        Me.nud_missing.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.nud_missing.Name = "nud_missing"
        Me.nud_missing.Size = New System.Drawing.Size(36, 22)
        Me.nud_missing.TabIndex = 25
        Me.nud_missing.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(20, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(281, 16)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "automatically re-scrape missing movies every"
        '
        'cb_scraping
        '
        Me.cb_scraping.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_scraping.FormattingEnabled = True
        Me.cb_scraping.Location = New System.Drawing.Point(153, 81)
        Me.cb_scraping.Name = "cb_scraping"
        Me.cb_scraping.Size = New System.Drawing.Size(80, 24)
        Me.cb_scraping.TabIndex = 23
        Me.cb_scraping.Text = "minutes"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(20, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(195, 16)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "scrape when CPU usage below"
        '
        'nud_scraping
        '
        Me.nud_scraping.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nud_scraping.Location = New System.Drawing.Point(113, 82)
        Me.nud_scraping.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nud_scraping.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_scraping.Name = "nud_scraping"
        Me.nud_scraping.Size = New System.Drawing.Size(36, 22)
        Me.nud_scraping.TabIndex = 22
        Me.nud_scraping.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_scraping.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(20, 84)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 16)
        Me.Label2.TabIndex = 20
        Me.Label2.Text = "scrape every"
        '
        'mtb_cpu
        '
        Me.mtb_cpu.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.mtb_cpu.Location = New System.Drawing.Point(217, 52)
        Me.mtb_cpu.Mask = "00%"
        Me.mtb_cpu.Name = "mtb_cpu"
        Me.mtb_cpu.Size = New System.Drawing.Size(34, 22)
        Me.mtb_cpu.TabIndex = 19
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
        'u_Artist
        '
        Me.u_Artist.Text = ""
        Me.u_Artist.Width = 0
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
        Me.tp_movies.ResumeLayout(False)
        Me.tbc_movies.ResumeLayout(False)
        Me.tp_Movie_DVDArt.ResumeLayout(False)
        Me.tp_Movie_DVDArt.PerformLayout()
        CType(Me.pb_movie_dvdart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Movie_ClearArt.ResumeLayout(False)
        CType(Me.pb_movie_clearart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Movie_ClearLogo.ResumeLayout(False)
        CType(Me.pb_movie_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_movies_missing.ResumeLayout(False)
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_TVSeries.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.tp_series.ResumeLayout(False)
        Me.tbc_series.ResumeLayout(False)
        Me.tp_Serie_ClearArt.ResumeLayout(False)
        Me.tp_Serie_ClearArt.PerformLayout()
        CType(Me.pb_serie_clearart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Serie_ClearLogo.ResumeLayout(False)
        CType(Me.pb_serie_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_series_missing.ResumeLayout(False)
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_Music.ResumeLayout(False)
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbc_music.ResumeLayout(False)
        Me.tp_artists.ResumeLayout(False)
        Me.tbc_artist.ResumeLayout(False)
        Me.tp_artist_banner.ResumeLayout(False)
        CType(Me.pb_artist_banner, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_artist_clearlogo.ResumeLayout(False)
        CType(Me.pb_artist_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_albums.ResumeLayout(False)
        Me.tbc_album.ResumeLayout(False)
        Me.tp_Music_CDArt.ResumeLayout(False)
        Me.tp_Music_CDArt.PerformLayout()
        CType(Me.pb_album_cdart, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_artist_album_missing.ResumeLayout(False)
        Me.tp_Importer.ResumeLayout(False)
        Me.tp_Settings.ResumeLayout(False)
        Me.gb2.ResumeLayout(False)
        Me.gb2.PerformLayout()
        Me.tbc_scraper.ResumeLayout(False)
        Me.tp_sMovies.ResumeLayout(False)
        Me.TabControl3.ResumeLayout(False)
        Me.tp_movies_settings.ResumeLayout(False)
        Me.tp_movies_settings.PerformLayout()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_manual_dvdart.ResumeLayout(False)
        Me.tp_sSeries.ResumeLayout(False)
        Me.tp_sSeries.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_sMusic.ResumeLayout(False)
        Me.tp_sMusic.PerformLayout()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb1.ResumeLayout(False)
        Me.gb1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents tp_movies As System.Windows.Forms.TabPage
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
    Friend WithEvents tp_movies_missing As System.Windows.Forms.TabPage
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
    Friend WithEvents gb1 As System.Windows.Forms.GroupBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents tp_Importer As System.Windows.Forms.TabPage
    Friend WithEvents lv_import As System.Windows.Forms.ListView
    Friend WithEvents i_Name As System.Windows.Forms.ColumnHeader
    Friend WithEvents i_ID As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents tp_series As System.Windows.Forms.TabPage
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
    Friend WithEvents tp_series_missing As System.Windows.Forms.TabPage
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
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents tp_Music As System.Windows.Forms.TabPage
    Friend WithEvents tbc_scraper As System.Windows.Forms.TabControl
    Friend WithEvents tp_sMovies As System.Windows.Forms.TabPage
    Friend WithEvents pb2 As System.Windows.Forms.PictureBox
    Friend WithEvents pb3 As System.Windows.Forms.PictureBox
    Friend WithEvents pb1 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_ClearLogo_movies As System.Windows.Forms.CheckBox
    Friend WithEvents cb_ClearArt_movies As System.Windows.Forms.CheckBox
    Friend WithEvents cb_DVDArt_movies As System.Windows.Forms.CheckBox
    Friend WithEvents tp_sSeries As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox4 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_ClearLogo_series As System.Windows.Forms.CheckBox
    Friend WithEvents cb_ClearArt_series As System.Windows.Forms.CheckBox
    Friend WithEvents tp_sMusic As System.Windows.Forms.TabPage
    Friend WithEvents PictureBox6 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox7 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox8 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_ClearLogo_artist As System.Windows.Forms.CheckBox
    Friend WithEvents cb_Banner_artist As System.Windows.Forms.CheckBox
    Friend WithEvents cb_CDArt_music As System.Windows.Forms.CheckBox
    Friend WithEvents tbc_music As System.Windows.Forms.TabControl
    Friend WithEvents tp_albums As System.Windows.Forms.TabPage
    Friend WithEvents lv_album As System.Windows.Forms.ListView
    Friend WithEvents Album As System.Windows.Forms.ColumnHeader
    Friend WithEvents tp_artist_album_missing As System.Windows.Forms.TabPage
    Friend WithEvents lv_music_missing As System.Windows.Forms.ListView
    Friend WithEvents u_Artist_Music As System.Windows.Forms.ColumnHeader
    Friend WithEvents u_CDArt As System.Windows.Forms.ColumnHeader
    Friend WithEvents u_Banner As System.Windows.Forms.ColumnHeader
    Friend WithEvents u_ClearLogo As System.Windows.Forms.ColumnHeader
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents MBID As System.Windows.Forms.ColumnHeader
    Friend WithEvents u_MBID As System.Windows.Forms.ColumnHeader
    Friend WithEvents cb_backgroundscraper As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cb_missing As System.Windows.Forms.ComboBox
    Friend WithEvents nud_missing As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_scraping As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nud_scraping As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mtb_cpu As System.Windows.Forms.MaskedTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_delay As System.Windows.Forms.ComboBox
    Friend WithEvents nud_delay As System.Windows.Forms.NumericUpDown
    Friend WithEvents b_movie_delete As System.Windows.Forms.Button
    Public WithEvents lv_movies_missing As System.Windows.Forms.ListView
    Friend WithEvents tp_artists As System.Windows.Forms.TabPage
    Friend WithEvents lv_artist As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents tbc_album As System.Windows.Forms.TabControl
    Friend WithEvents tp_Music_CDArt As System.Windows.Forms.TabPage
    Friend WithEvents b_album_preview As System.Windows.Forms.Button
    Friend WithEvents b_album_compress As System.Windows.Forms.Button
    Friend WithEvents l_music_size As System.Windows.Forms.Label
    Friend WithEvents lv_album_cdart As System.Windows.Forms.ListView
    Friend WithEvents pb_album_cdart As System.Windows.Forms.PictureBox
    Friend WithEvents tbc_artist As System.Windows.Forms.TabControl
    Friend WithEvents tp_artist_clearlogo As System.Windows.Forms.TabPage
    Friend WithEvents b_artist_deletelogo As System.Windows.Forms.Button
    Friend WithEvents pb_artist_clearlogo As System.Windows.Forms.PictureBox
    Friend WithEvents lv_artist_clearlogo As System.Windows.Forms.ListView
    Friend WithEvents tp_artist_banner As System.Windows.Forms.TabPage
    Friend WithEvents b_artist_deletebanner As System.Windows.Forms.Button
    Friend WithEvents pb_artist_banner As System.Windows.Forms.PictureBox
    Friend WithEvents lv_artist_banner As System.Windows.Forms.ListView
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents b_album_delete As System.Windows.Forms.Button
    Friend WithEvents TabControl3 As System.Windows.Forms.TabControl
    Friend WithEvents tp_movies_settings As System.Windows.Forms.TabPage
    Friend WithEvents tp_manual_dvdart As System.Windows.Forms.TabPage
    Friend WithEvents rb_t2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_t1 As System.Windows.Forms.RadioButton
    Friend WithEvents il_banner As System.Windows.Forms.ImageList
    Friend WithEvents ChangeMBIDToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeMBIDToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents u_Artist As System.Windows.Forms.ColumnHeader

End Class
