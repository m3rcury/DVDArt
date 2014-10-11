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
        Me.SelectCoverArtForDVDArtToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.il_clearart = New System.Windows.Forms.ImageList(Me.components)
        Me.il_clearlogo = New System.Windows.Forms.ImageList(Me.components)
        Me.cms_import = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.il_state = New System.Windows.Forms.ImageList(Me.components)
        Me.cms_missing = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ChangeIMDBTMDBNumberToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.bw_import = New System.ComponentModel.BackgroundWorker()
        Me.tbc_main = New System.Windows.Forms.TabControl()
        Me.tp_MovingPictures = New System.Windows.Forms.TabPage()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tp_movies = New System.Windows.Forms.TabPage()
        Me.lv_movies = New System.Windows.Forms.ListView()
        Me.Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_movies = New System.Windows.Forms.TabControl()
        Me.tp_Movie_DVDArt = New System.Windows.Forms.TabPage()
        Me.l_movie_size = New System.Windows.Forms.Label()
        Me.lv_movie_dvdart = New System.Windows.Forms.ListView()
        Me.l_imdb_id = New System.Windows.Forms.Label()
        Me.tp_Movie_ClearArt = New System.Windows.Forms.TabPage()
        Me.lv_movie_clearart = New System.Windows.Forms.ListView()
        Me.tp_Movie_ClearLogo = New System.Windows.Forms.TabPage()
        Me.lv_movie_clearlogo = New System.Windows.Forms.ListView()
        Me.tp_Movie_Banner = New System.Windows.Forms.TabPage()
        Me.lv_movie_banner = New System.Windows.Forms.ListView()
        Me.il_banner = New System.Windows.Forms.ImageList(Me.components)
        Me.tp_Movie_Backdrop = New System.Windows.Forms.TabPage()
        Me.lv_movie_backdrop = New System.Windows.Forms.ListView()
        Me.il_backdrop = New System.Windows.Forms.ImageList(Me.components)
        Me.tp_Movie_Cover = New System.Windows.Forms.TabPage()
        Me.lv_movie_cover = New System.Windows.Forms.ListView()
        Me.il_cover = New System.Windows.Forms.ImageList(Me.components)
        Me.tp_movies_missing = New System.Windows.Forms.TabPage()
        Me.lv_movies_missing = New System.Windows.Forms.ListView()
        Me.m_Movie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_DVDArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_ClearArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_Banner = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_Backdrop = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_Cover = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.m_IMDb_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.lv_person = New System.Windows.Forms.ListView()
        Me.c_person = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cms_person_found = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.lv_persons_missing = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cms_person_missing = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tp_TVSeries = New System.Windows.Forms.TabPage()
        Me.TabControl2 = New System.Windows.Forms.TabControl()
        Me.tp_series = New System.Windows.Forms.TabPage()
        Me.lv_series = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_series = New System.Windows.Forms.TabControl()
        Me.tp_Serie_ClearArt = New System.Windows.Forms.TabPage()
        Me.l_thetvdb_id = New System.Windows.Forms.Label()
        Me.lv_serie_clearart = New System.Windows.Forms.ListView()
        Me.tp_Serie_ClearLogo = New System.Windows.Forms.TabPage()
        Me.lv_serie_clearlogo = New System.Windows.Forms.ListView()
        Me.tp_series_missing = New System.Windows.Forms.TabPage()
        Me.lv_series_missing = New System.Windows.Forms.ListView()
        Me.c_Serie = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_ClearArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.c_id = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tp_Music = New System.Windows.Forms.TabPage()
        Me.tbc_music = New System.Windows.Forms.TabControl()
        Me.tp_artists = New System.Windows.Forms.TabPage()
        Me.lv_artist = New System.Windows.Forms.ListView()
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_artist = New System.Windows.Forms.TabControl()
        Me.tp_artist_banner = New System.Windows.Forms.TabPage()
        Me.lv_artist_banner = New System.Windows.Forms.ListView()
        Me.tp_artist_clearlogo = New System.Windows.Forms.TabPage()
        Me.lv_artist_clearlogo = New System.Windows.Forms.ListView()
        Me.tp_albums = New System.Windows.Forms.TabPage()
        Me.lv_album = New System.Windows.Forms.ListView()
        Me.Album = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.MBID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tbc_album = New System.Windows.Forms.TabControl()
        Me.tp_Music_CDArt = New System.Windows.Forms.TabPage()
        Me.l_music_size = New System.Windows.Forms.Label()
        Me.lv_album_cdart = New System.Windows.Forms.ListView()
        Me.tp_artist_album_missing = New System.Windows.Forms.TabPage()
        Me.lv_music_missing = New System.Windows.Forms.ListView()
        Me.u_Artist_Music = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_CDArt = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_Banner = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_ClearLogo = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_MBID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.u_Artist = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.tp_Importer = New System.Windows.Forms.TabPage()
        Me.lv_import = New System.Windows.Forms.ListView()
        Me.i_Name = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_ID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.i_Type = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.b_import = New System.Windows.Forms.Button()
        Me.tp_Settings = New System.Windows.Forms.TabPage()
        Me.tbc_settings = New System.Windows.Forms.TabControl()
        Me.tp_pluginsettings = New System.Windows.Forms.TabPage()
        Me.b_save1 = New System.Windows.Forms.Button()
        Me.gb4 = New System.Windows.Forms.GroupBox()
        Me.tb_personalapikey = New System.Windows.Forms.TextBox()
        Me.gb1 = New System.Windows.Forms.GroupBox()
        Me.cb_backgroundscraper = New System.Windows.Forms.CheckBox()
        Me.pnl_background = New System.Windows.Forms.Panel()
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
        Me.gb3 = New System.Windows.Forms.GroupBox()
        Me.pnl_online = New System.Windows.Forms.Panel()
        Me.nud_downloads = New System.Windows.Forms.NumericUpDown()
        Me.l_downloads = New System.Windows.Forms.Label()
        Me.cb_downloads = New System.Windows.Forms.CheckBox()
        Me.cb_autoimport = New System.Windows.Forms.CheckBox()
        Me.cb_debug = New System.Windows.Forms.CheckBox()
        Me.tp_scrapersettings = New System.Windows.Forms.TabPage()
        Me.b_save2 = New System.Windows.Forms.Button()
        Me.gb2 = New System.Windows.Forms.GroupBox()
        Me.tbc_scraper = New System.Windows.Forms.TabControl()
        Me.tp_sMovies = New System.Windows.Forms.TabPage()
        Me.tbc_movie_settings = New System.Windows.Forms.TabControl()
        Me.tp_movies_scraper = New System.Windows.Forms.TabPage()
        Me.cb_Banner_movies = New System.Windows.Forms.CheckBox()
        Me.cb_Cover_movies = New System.Windows.Forms.CheckBox()
        Me.cb_Backdrop_movies = New System.Windows.Forms.CheckBox()
        Me.cb_DVDArt_movies = New System.Windows.Forms.CheckBox()
        Me.cb_ClearArt_movies = New System.Windows.Forms.CheckBox()
        Me.cb_ClearLogo_movies = New System.Windows.Forms.CheckBox()
        Me.tp_manual_dvdart = New System.Windows.Forms.TabPage()
        Me.tp_movies_path = New System.Windows.Forms.TabPage()
        Me.tb_movie_path = New System.Windows.Forms.TextBox()
        Me.b_movie_path = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.tp_movies_persons = New System.Windows.Forms.TabPage()
        Me.tb_person_path = New System.Windows.Forms.TextBox()
        Me.b_person_path = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cb_persons = New System.Windows.Forms.CheckBox()
        Me.tp_sSeries = New System.Windows.Forms.TabPage()
        Me.tbc_series_settings = New System.Windows.Forms.TabControl()
        Me.tp_series_scraper = New System.Windows.Forms.TabPage()
        Me.cb_ClearArt_series = New System.Windows.Forms.CheckBox()
        Me.cb_ClearLogo_series = New System.Windows.Forms.CheckBox()
        Me.tp_series_path = New System.Windows.Forms.TabPage()
        Me.tb_series_path = New System.Windows.Forms.TextBox()
        Me.b_series_path = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.tp_sMusic = New System.Windows.Forms.TabPage()
        Me.tbc_music_settings = New System.Windows.Forms.TabControl()
        Me.tp_music_scraper = New System.Windows.Forms.TabPage()
        Me.cb_ClearLogo_artist = New System.Windows.Forms.CheckBox()
        Me.cb_Banner_artist = New System.Windows.Forms.CheckBox()
        Me.cb_CDArt_music = New System.Windows.Forms.CheckBox()
        Me.tp_music_path = New System.Windows.Forms.TabPage()
        Me.tb_music_path = New System.Windows.Forms.TextBox()
        Me.b_music_path = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cb_language = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.tp_about = New System.Windows.Forms.TabPage()
        Me.l_copyright = New System.Windows.Forms.Label()
        Me.l_version = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel6 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel7 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel8 = New System.Windows.Forms.LinkLabel()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ll_wiki = New System.Windows.Forms.LinkLabel()
        Me.ll_forum = New System.Windows.Forms.LinkLabel()
        Me.ll_project = New System.Windows.Forms.LinkLabel()
        Me.ll_developer = New System.Windows.Forms.LinkLabel()
        Me.il_column = New System.Windows.Forms.ImageList(Me.components)
        Me.pbthemoviedb = New System.Windows.Forms.PictureBox()
        Me.pb_movingpictures = New System.Windows.Forms.PictureBox()
        Me.RefreshArtworkFromOnline_found = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManuallyUploadArtwork_found = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectCoverArtForDVDArt_found = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeMBID_found = New System.Windows.Forms.ToolStripMenuItem()
        Me.b_movie_delete = New System.Windows.Forms.Button()
        Me.b_movie_preview = New System.Windows.Forms.Button()
        Me.b_movie_compress = New System.Windows.Forms.Button()
        Me.pb_movie_dvdart = New System.Windows.Forms.PictureBox()
        Me.b_movie_deleteart = New System.Windows.Forms.Button()
        Me.pb_movie_clearart = New System.Windows.Forms.PictureBox()
        Me.b_movie_deletelogo = New System.Windows.Forms.Button()
        Me.pb_movie_clearlogo = New System.Windows.Forms.PictureBox()
        Me.b_movie_deletebanner = New System.Windows.Forms.Button()
        Me.pb_movie_banner = New System.Windows.Forms.PictureBox()
        Me.b_movie_deletebackdrop = New System.Windows.Forms.Button()
        Me.pb_movie_backdrop = New System.Windows.Forms.PictureBox()
        Me.b_movie_deletecover = New System.Windows.Forms.Button()
        Me.pb_movie_cover = New System.Windows.Forms.PictureBox()
        Me.SendtoImporter_missing = New System.Windows.Forms.ToolStripMenuItem()
        Me.ManuallyUpload_missing = New System.Windows.Forms.ToolStripMenuItem()
        Me.SelectCoverArtForDVDArt_missing = New System.Windows.Forms.ToolStripMenuItem()
        Me.UseCoverArtForDVDArt_missing = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanAll_missing = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeMBID_missing = New System.Windows.Forms.ToolStripMenuItem()
        Me.pb_person = New System.Windows.Forms.PictureBox()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem6 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripMenuItem()
        Me.pbFTV_Logo = New System.Windows.Forms.PictureBox()
        Me.PictureBox14 = New System.Windows.Forms.PictureBox()
        Me.b_serie_deleteart = New System.Windows.Forms.Button()
        Me.pb_serie_clearart = New System.Windows.Forms.PictureBox()
        Me.b_serie_deletelogo = New System.Windows.Forms.Button()
        Me.pb_serie_clearlogo = New System.Windows.Forms.PictureBox()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.pbFTV_Logo2 = New System.Windows.Forms.PictureBox()
        Me.pbAudioDB = New System.Windows.Forms.PictureBox()
        Me.pbLastFM = New System.Windows.Forms.PictureBox()
        Me.PictureBox9 = New System.Windows.Forms.PictureBox()
        Me.pbFTV_Logo3 = New System.Windows.Forms.PictureBox()
        Me.b_artist_deletebanner = New System.Windows.Forms.Button()
        Me.pb_artist_banner = New System.Windows.Forms.PictureBox()
        Me.b_artist_deletelogo = New System.Windows.Forms.Button()
        Me.pb_artist_clearlogo = New System.Windows.Forms.PictureBox()
        Me.b_album_delete = New System.Windows.Forms.Button()
        Me.b_album_preview = New System.Windows.Forms.Button()
        Me.b_album_compress = New System.Windows.Forms.Button()
        Me.pb_album_cdart = New System.Windows.Forms.PictureBox()
        Me.RestartImporterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PictureBox5 = New System.Windows.Forms.PictureBox()
        Me.PictureBox15 = New System.Windows.Forms.PictureBox()
        Me.PictureBox12 = New System.Windows.Forms.PictureBox()
        Me.pb2 = New System.Windows.Forms.PictureBox()
        Me.pb1 = New System.Windows.Forms.PictureBox()
        Me.pb3 = New System.Windows.Forms.PictureBox()
        Me.rb_t2 = New System.Windows.Forms.RadioButton()
        Me.rb_t1 = New System.Windows.Forms.RadioButton()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.PictureBox4 = New System.Windows.Forms.PictureBox()
        Me.PictureBox6 = New System.Windows.Forms.PictureBox()
        Me.PictureBox8 = New System.Windows.Forms.PictureBox()
        Me.PictureBox7 = New System.Windows.Forms.PictureBox()
        Me.PictureBox10 = New System.Windows.Forms.PictureBox()
        Me.pb_donate = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MUploadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendToImporterToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RescanAllToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.cms_found.SuspendLayout()
        Me.cms_import.SuspendLayout()
        Me.cms_missing.SuspendLayout()
        Me.tbc_main.SuspendLayout()
        Me.tp_MovingPictures.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tp_movies.SuspendLayout()
        Me.tbc_movies.SuspendLayout()
        Me.tp_Movie_DVDArt.SuspendLayout()
        Me.tp_Movie_ClearArt.SuspendLayout()
        Me.tp_Movie_ClearLogo.SuspendLayout()
        Me.tp_Movie_Banner.SuspendLayout()
        Me.tp_Movie_Backdrop.SuspendLayout()
        Me.tp_Movie_Cover.SuspendLayout()
        Me.tp_movies_missing.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.cms_person_found.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.cms_person_missing.SuspendLayout()
        Me.tp_TVSeries.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.tp_series.SuspendLayout()
        Me.tbc_series.SuspendLayout()
        Me.tp_Serie_ClearArt.SuspendLayout()
        Me.tp_Serie_ClearLogo.SuspendLayout()
        Me.tp_series_missing.SuspendLayout()
        Me.tp_Music.SuspendLayout()
        Me.tbc_music.SuspendLayout()
        Me.tp_artists.SuspendLayout()
        Me.tbc_artist.SuspendLayout()
        Me.tp_artist_banner.SuspendLayout()
        Me.tp_artist_clearlogo.SuspendLayout()
        Me.tp_albums.SuspendLayout()
        Me.tbc_album.SuspendLayout()
        Me.tp_Music_CDArt.SuspendLayout()
        Me.tp_artist_album_missing.SuspendLayout()
        Me.tp_Importer.SuspendLayout()
        Me.tp_Settings.SuspendLayout()
        Me.tbc_settings.SuspendLayout()
        Me.tp_pluginsettings.SuspendLayout()
        Me.gb4.SuspendLayout()
        Me.gb1.SuspendLayout()
        Me.pnl_background.SuspendLayout()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb3.SuspendLayout()
        Me.pnl_online.SuspendLayout()
        CType(Me.nud_downloads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tp_scrapersettings.SuspendLayout()
        Me.gb2.SuspendLayout()
        Me.tbc_scraper.SuspendLayout()
        Me.tp_sMovies.SuspendLayout()
        Me.tbc_movie_settings.SuspendLayout()
        Me.tp_movies_scraper.SuspendLayout()
        Me.tp_manual_dvdart.SuspendLayout()
        Me.tp_movies_path.SuspendLayout()
        Me.tp_movies_persons.SuspendLayout()
        Me.tp_sSeries.SuspendLayout()
        Me.tbc_series_settings.SuspendLayout()
        Me.tp_series_scraper.SuspendLayout()
        Me.tp_series_path.SuspendLayout()
        Me.tp_sMusic.SuspendLayout()
        Me.tbc_music_settings.SuspendLayout()
        Me.tp_music_scraper.SuspendLayout()
        Me.tp_music_path.SuspendLayout()
        Me.tp_about.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pbthemoviedb, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movingpictures, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movie_dvdart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movie_clearart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movie_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movie_banner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movie_backdrop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_movie_cover, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_person, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_serie_clearart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_serie_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFTV_Logo2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbAudioDB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbLastFM, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFTV_Logo3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_artist_banner, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_artist_clearlogo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_album_cdart, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pb_donate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.cms_found.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshArtworkFromOnline_found, Me.ManuallyUploadArtwork_found, Me.SelectCoverArtForDVDArt_found, Me.ChangeMBID_found})
        Me.cms_found.Name = "cms_movies"
        Me.cms_found.Size = New System.Drawing.Size(247, 92)
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
        Me.Label1.Location = New System.Drawing.Point(1, 747)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(160, 13)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Copyright © 2012-2014, m3rcury"
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
        'il_state
        '
        Me.il_state.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_state.ImageSize = New System.Drawing.Size(16, 16)
        Me.il_state.TransparentColor = System.Drawing.Color.Transparent
        '
        'cms_missing
        '
        Me.cms_missing.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SendtoImporter_missing, Me.ManuallyUpload_missing, Me.SelectCoverArtForDVDArt_missing, Me.UseCoverArtForDVDArt_missing, Me.RescanAll_missing, Me.ChangeMBID_missing})
        Me.cms_missing.Name = "cms_missing"
        Me.cms_missing.Size = New System.Drawing.Size(247, 136)
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
        Me.tbc_main.Controls.Add(Me.tp_about)
        Me.tbc_main.Location = New System.Drawing.Point(12, 4)
        Me.tbc_main.Name = "tbc_main"
        Me.tbc_main.SelectedIndex = 0
        Me.tbc_main.Size = New System.Drawing.Size(664, 729)
        Me.tbc_main.TabIndex = 29
        '
        'tp_MovingPictures
        '
        Me.tp_MovingPictures.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_MovingPictures.Controls.Add(Me.pbthemoviedb)
        Me.tp_MovingPictures.Controls.Add(Me.pb_movingpictures)
        Me.tp_MovingPictures.Controls.Add(Me.TabControl1)
        Me.tp_MovingPictures.Controls.Add(Me.pbFTV_Logo)
        Me.tp_MovingPictures.Controls.Add(Me.PictureBox14)
        Me.tp_MovingPictures.Location = New System.Drawing.Point(4, 22)
        Me.tp_MovingPictures.Name = "tp_MovingPictures"
        Me.tp_MovingPictures.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_MovingPictures.Size = New System.Drawing.Size(656, 703)
        Me.tp_MovingPictures.TabIndex = 0
        Me.tp_MovingPictures.Text = "Movies"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tp_movies)
        Me.TabControl1.Controls.Add(Me.tp_movies_missing)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(650, 639)
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
        Me.tp_movies.Size = New System.Drawing.Size(642, 613)
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
        Me.lv_movies.MultiSelect = False
        Me.lv_movies.Name = "lv_movies"
        Me.lv_movies.Size = New System.Drawing.Size(340, 599)
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
        Me.tbc_movies.Controls.Add(Me.tp_Movie_Banner)
        Me.tbc_movies.Controls.Add(Me.tp_Movie_Backdrop)
        Me.tbc_movies.Controls.Add(Me.tp_Movie_Cover)
        Me.tbc_movies.Location = New System.Drawing.Point(352, 8)
        Me.tbc_movies.Name = "tbc_movies"
        Me.tbc_movies.SelectedIndex = 0
        Me.tbc_movies.Size = New System.Drawing.Size(284, 599)
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
        Me.tp_Movie_DVDArt.Size = New System.Drawing.Size(276, 573)
        Me.tp_Movie_DVDArt.TabIndex = 0
        Me.tp_Movie_DVDArt.Text = "DVDArt"
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
        Me.lv_movie_dvdart.Size = New System.Drawing.Size(252, 352)
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
        'tp_Movie_ClearArt
        '
        Me.tp_Movie_ClearArt.BackColor = System.Drawing.SystemColors.Control
        Me.tp_Movie_ClearArt.Controls.Add(Me.b_movie_deleteart)
        Me.tp_Movie_ClearArt.Controls.Add(Me.pb_movie_clearart)
        Me.tp_Movie_ClearArt.Controls.Add(Me.lv_movie_clearart)
        Me.tp_Movie_ClearArt.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_ClearArt.Name = "tp_Movie_ClearArt"
        Me.tp_Movie_ClearArt.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Movie_ClearArt.Size = New System.Drawing.Size(276, 573)
        Me.tp_Movie_ClearArt.TabIndex = 1
        Me.tp_Movie_ClearArt.Text = "ClearArt"
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
        Me.lv_movie_clearart.Size = New System.Drawing.Size(252, 442)
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
        Me.tp_Movie_ClearLogo.Size = New System.Drawing.Size(276, 573)
        Me.tp_Movie_ClearLogo.TabIndex = 2
        Me.tp_Movie_ClearLogo.Text = "ClearLogo"
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
        Me.lv_movie_clearlogo.Size = New System.Drawing.Size(252, 476)
        Me.lv_movie_clearlogo.TabIndex = 19
        Me.lv_movie_clearlogo.UseCompatibleStateImageBehavior = False
        '
        'tp_Movie_Banner
        '
        Me.tp_Movie_Banner.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Movie_Banner.Controls.Add(Me.b_movie_deletebanner)
        Me.tp_Movie_Banner.Controls.Add(Me.pb_movie_banner)
        Me.tp_Movie_Banner.Controls.Add(Me.lv_movie_banner)
        Me.tp_Movie_Banner.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_Banner.Name = "tp_Movie_Banner"
        Me.tp_Movie_Banner.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Movie_Banner.Size = New System.Drawing.Size(276, 573)
        Me.tp_Movie_Banner.TabIndex = 5
        Me.tp_Movie_Banner.Text = "Banners"
        '
        'lv_movie_banner
        '
        Me.lv_movie_banner.Alignment = System.Windows.Forms.ListViewAlignment.Left
        Me.lv_movie_banner.BackColor = System.Drawing.SystemColors.Control
        Me.lv_movie_banner.ForeColor = System.Drawing.Color.Black
        Me.lv_movie_banner.LargeImageList = Me.il_banner
        Me.lv_movie_banner.Location = New System.Drawing.Point(10, 54)
        Me.lv_movie_banner.MultiSelect = False
        Me.lv_movie_banner.Name = "lv_movie_banner"
        Me.lv_movie_banner.Size = New System.Drawing.Size(256, 513)
        Me.lv_movie_banner.TabIndex = 19
        Me.lv_movie_banner.UseCompatibleStateImageBehavior = False
        '
        'il_banner
        '
        Me.il_banner.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_banner.ImageSize = New System.Drawing.Size(200, 37)
        Me.il_banner.TransparentColor = System.Drawing.Color.Transparent
        '
        'tp_Movie_Backdrop
        '
        Me.tp_Movie_Backdrop.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Movie_Backdrop.Controls.Add(Me.b_movie_deletebackdrop)
        Me.tp_Movie_Backdrop.Controls.Add(Me.pb_movie_backdrop)
        Me.tp_Movie_Backdrop.Controls.Add(Me.lv_movie_backdrop)
        Me.tp_Movie_Backdrop.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_Backdrop.Name = "tp_Movie_Backdrop"
        Me.tp_Movie_Backdrop.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Movie_Backdrop.Size = New System.Drawing.Size(276, 573)
        Me.tp_Movie_Backdrop.TabIndex = 3
        Me.tp_Movie_Backdrop.Text = "Backdrop"
        '
        'lv_movie_backdrop
        '
        Me.lv_movie_backdrop.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movie_backdrop.BackColor = System.Drawing.SystemColors.Control
        Me.lv_movie_backdrop.ForeColor = System.Drawing.Color.Black
        Me.lv_movie_backdrop.LargeImageList = Me.il_backdrop
        Me.lv_movie_backdrop.Location = New System.Drawing.Point(12, 125)
        Me.lv_movie_backdrop.MultiSelect = False
        Me.lv_movie_backdrop.Name = "lv_movie_backdrop"
        Me.lv_movie_backdrop.Size = New System.Drawing.Size(252, 442)
        Me.lv_movie_backdrop.TabIndex = 19
        Me.lv_movie_backdrop.UseCompatibleStateImageBehavior = False
        '
        'il_backdrop
        '
        Me.il_backdrop.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_backdrop.ImageSize = New System.Drawing.Size(200, 112)
        Me.il_backdrop.TransparentColor = System.Drawing.Color.Transparent
        '
        'tp_Movie_Cover
        '
        Me.tp_Movie_Cover.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Movie_Cover.Controls.Add(Me.b_movie_deletecover)
        Me.tp_Movie_Cover.Controls.Add(Me.lv_movie_cover)
        Me.tp_Movie_Cover.Controls.Add(Me.pb_movie_cover)
        Me.tp_Movie_Cover.Location = New System.Drawing.Point(4, 22)
        Me.tp_Movie_Cover.Name = "tp_Movie_Cover"
        Me.tp_Movie_Cover.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Movie_Cover.Size = New System.Drawing.Size(276, 573)
        Me.tp_Movie_Cover.TabIndex = 4
        Me.tp_Movie_Cover.Text = "Covers"
        '
        'lv_movie_cover
        '
        Me.lv_movie_cover.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movie_cover.BackColor = System.Drawing.SystemColors.Control
        Me.lv_movie_cover.ForeColor = System.Drawing.Color.Black
        Me.lv_movie_cover.LargeImageList = Me.il_cover
        Me.lv_movie_cover.Location = New System.Drawing.Point(43, 215)
        Me.lv_movie_cover.MultiSelect = False
        Me.lv_movie_cover.Name = "lv_movie_cover"
        Me.lv_movie_cover.Size = New System.Drawing.Size(190, 352)
        Me.lv_movie_cover.TabIndex = 15
        Me.lv_movie_cover.UseCompatibleStateImageBehavior = False
        '
        'il_cover
        '
        Me.il_cover.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_cover.ImageSize = New System.Drawing.Size(146, 200)
        Me.il_cover.TransparentColor = System.Drawing.Color.Transparent
        '
        'tp_movies_missing
        '
        Me.tp_movies_missing.BackColor = System.Drawing.SystemColors.Control
        Me.tp_movies_missing.Controls.Add(Me.lv_movies_missing)
        Me.tp_movies_missing.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies_missing.Name = "tp_movies_missing"
        Me.tp_movies_missing.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies_missing.Size = New System.Drawing.Size(642, 613)
        Me.tp_movies_missing.TabIndex = 2
        Me.tp_movies_missing.Text = "Movies missing Artwork"
        '
        'lv_movies_missing
        '
        Me.lv_movies_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_movies_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.m_Movie, Me.m_DVDArt, Me.m_ClearArt, Me.m_ClearLogo, Me.m_Banner, Me.m_Backdrop, Me.m_Cover, Me.m_IMDb_id})
        Me.lv_movies_missing.ContextMenuStrip = Me.cms_missing
        Me.lv_movies_missing.FullRowSelect = True
        Me.lv_movies_missing.GridLines = True
        Me.lv_movies_missing.Location = New System.Drawing.Point(8, 8)
        Me.lv_movies_missing.Name = "lv_movies_missing"
        Me.lv_movies_missing.Size = New System.Drawing.Size(628, 599)
        Me.lv_movies_missing.SmallImageList = Me.il_state
        Me.lv_movies_missing.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_movies_missing.TabIndex = 10
        Me.lv_movies_missing.UseCompatibleStateImageBehavior = False
        Me.lv_movies_missing.View = System.Windows.Forms.View.Details
        '
        'm_Movie
        '
        Me.m_Movie.Text = "Movie"
        Me.m_Movie.Width = 270
        '
        'm_DVDArt
        '
        Me.m_DVDArt.Text = "DVDArt"
        Me.m_DVDArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_ClearArt
        '
        Me.m_ClearArt.Text = "ClearArt"
        Me.m_ClearArt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_ClearLogo
        '
        Me.m_ClearLogo.Text = "ClearLogo"
        Me.m_ClearLogo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_Banner
        '
        Me.m_Banner.Text = "Banner"
        Me.m_Banner.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_Backdrop
        '
        Me.m_Backdrop.Text = "Backdrop"
        Me.m_Backdrop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_Cover
        '
        Me.m_Cover.Text = "Cover"
        Me.m_Cover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_IMDb_id
        '
        Me.m_IMDb_id.Text = "IMDb ID"
        Me.m_IMDb_id.Width = 70
        '
        'TabPage1
        '
        Me.TabPage1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TabPage1.Controls.Add(Me.pb_person)
        Me.TabPage1.Controls.Add(Me.lv_person)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(642, 613)
        Me.TabPage1.TabIndex = 3
        Me.TabPage1.Text = "Actors/Writers/Directors with Picture"
        '
        'lv_person
        '
        Me.lv_person.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_person.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c_person})
        Me.lv_person.ContextMenuStrip = Me.cms_person_found
        Me.lv_person.FullRowSelect = True
        Me.lv_person.GridLines = True
        Me.lv_person.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
        Me.lv_person.Location = New System.Drawing.Point(8, 8)
        Me.lv_person.MultiSelect = False
        Me.lv_person.Name = "lv_person"
        Me.lv_person.Size = New System.Drawing.Size(240, 599)
        Me.lv_person.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_person.TabIndex = 12
        Me.lv_person.UseCompatibleStateImageBehavior = False
        Me.lv_person.View = System.Windows.Forms.View.Details
        '
        'c_person
        '
        Me.c_person.Text = "Actors/Writers/Directors"
        Me.c_person.Width = 219
        '
        'cms_person_found
        '
        Me.cms_person_found.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1, Me.ToolStripMenuItem2, Me.ToolStripMenuItem3})
        Me.cms_person_found.Name = "cms_movies"
        Me.cms_person_found.Size = New System.Drawing.Size(254, 70)
        '
        'TabPage2
        '
        Me.TabPage2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TabPage2.Controls.Add(Me.lv_persons_missing)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(642, 613)
        Me.TabPage2.TabIndex = 4
        Me.TabPage2.Text = "Actors/Writers/Directors missing Picture"
        '
        'lv_persons_missing
        '
        Me.lv_persons_missing.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_persons_missing.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3})
        Me.lv_persons_missing.ContextMenuStrip = Me.cms_person_missing
        Me.lv_persons_missing.FullRowSelect = True
        Me.lv_persons_missing.GridLines = True
        Me.lv_persons_missing.Location = New System.Drawing.Point(8, 8)
        Me.lv_persons_missing.Name = "lv_persons_missing"
        Me.lv_persons_missing.Size = New System.Drawing.Size(628, 599)
        Me.lv_persons_missing.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lv_persons_missing.TabIndex = 11
        Me.lv_persons_missing.UseCompatibleStateImageBehavior = False
        Me.lv_persons_missing.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Actors/Writers/Directors"
        Me.ColumnHeader3.Width = 607
        '
        'cms_person_missing
        '
        Me.cms_person_missing.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem4, Me.ToolStripMenuItem6, Me.ToolStripMenuItem5})
        Me.cms_person_missing.Name = "cms_missing"
        Me.cms_person_missing.Size = New System.Drawing.Size(240, 70)
        '
        'tp_TVSeries
        '
        Me.tp_TVSeries.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_TVSeries.Controls.Add(Me.TabControl2)
        Me.tp_TVSeries.Controls.Add(Me.PictureBox2)
        Me.tp_TVSeries.Controls.Add(Me.pbFTV_Logo2)
        Me.tp_TVSeries.Location = New System.Drawing.Point(4, 22)
        Me.tp_TVSeries.Name = "tp_TVSeries"
        Me.tp_TVSeries.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_TVSeries.Size = New System.Drawing.Size(656, 703)
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
        Me.TabControl2.Size = New System.Drawing.Size(650, 639)
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
        Me.tp_series.Size = New System.Drawing.Size(642, 613)
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
        Me.lv_series.MultiSelect = False
        Me.lv_series.Name = "lv_series"
        Me.lv_series.Size = New System.Drawing.Size(340, 599)
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
        Me.tbc_series.Size = New System.Drawing.Size(284, 599)
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
        Me.tp_Serie_ClearArt.Size = New System.Drawing.Size(276, 573)
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
        'lv_serie_clearart
        '
        Me.lv_serie_clearart.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid
        Me.lv_serie_clearart.BackColor = System.Drawing.SystemColors.Control
        Me.lv_serie_clearart.ForeColor = System.Drawing.Color.Black
        Me.lv_serie_clearart.LargeImageList = Me.il_clearart
        Me.lv_serie_clearart.Location = New System.Drawing.Point(12, 125)
        Me.lv_serie_clearart.MultiSelect = False
        Me.lv_serie_clearart.Name = "lv_serie_clearart"
        Me.lv_serie_clearart.Size = New System.Drawing.Size(252, 442)
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
        Me.tp_Serie_ClearLogo.Size = New System.Drawing.Size(276, 573)
        Me.tp_Serie_ClearLogo.TabIndex = 2
        Me.tp_Serie_ClearLogo.Text = "ClearLogo"
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
        Me.lv_serie_clearlogo.Size = New System.Drawing.Size(252, 476)
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
        Me.tp_series_missing.Size = New System.Drawing.Size(642, 613)
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
        Me.lv_series_missing.Size = New System.Drawing.Size(628, 599)
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
        'tp_Music
        '
        Me.tp_Music.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Music.Controls.Add(Me.pbAudioDB)
        Me.tp_Music.Controls.Add(Me.pbLastFM)
        Me.tp_Music.Controls.Add(Me.PictureBox9)
        Me.tp_Music.Controls.Add(Me.pbFTV_Logo3)
        Me.tp_Music.Controls.Add(Me.tbc_music)
        Me.tp_Music.Location = New System.Drawing.Point(4, 22)
        Me.tp_Music.Name = "tp_Music"
        Me.tp_Music.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Music.Size = New System.Drawing.Size(656, 703)
        Me.tp_Music.TabIndex = 4
        Me.tp_Music.Text = "Music"
        '
        'tbc_music
        '
        Me.tbc_music.Controls.Add(Me.tp_artists)
        Me.tbc_music.Controls.Add(Me.tp_albums)
        Me.tbc_music.Controls.Add(Me.tp_artist_album_missing)
        Me.tbc_music.Location = New System.Drawing.Point(4, 3)
        Me.tbc_music.Name = "tbc_music"
        Me.tbc_music.SelectedIndex = 0
        Me.tbc_music.Size = New System.Drawing.Size(650, 639)
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
        Me.tp_artists.Size = New System.Drawing.Size(642, 613)
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
        Me.lv_artist.MultiSelect = False
        Me.lv_artist.Name = "lv_artist"
        Me.lv_artist.Size = New System.Drawing.Size(340, 599)
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
        Me.tbc_artist.Size = New System.Drawing.Size(284, 599)
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
        Me.tp_artist_banner.Size = New System.Drawing.Size(276, 573)
        Me.tp_artist_banner.TabIndex = 4
        Me.tp_artist_banner.Text = "Banner"
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
        Me.lv_artist_banner.Size = New System.Drawing.Size(252, 513)
        Me.lv_artist_banner.TabIndex = 16
        Me.lv_artist_banner.UseCompatibleStateImageBehavior = False
        '
        'tp_artist_clearlogo
        '
        Me.tp_artist_clearlogo.BackColor = System.Drawing.SystemColors.Control
        Me.tp_artist_clearlogo.Controls.Add(Me.b_artist_deletelogo)
        Me.tp_artist_clearlogo.Controls.Add(Me.pb_artist_clearlogo)
        Me.tp_artist_clearlogo.Controls.Add(Me.lv_artist_clearlogo)
        Me.tp_artist_clearlogo.Location = New System.Drawing.Point(4, 22)
        Me.tp_artist_clearlogo.Name = "tp_artist_clearlogo"
        Me.tp_artist_clearlogo.Size = New System.Drawing.Size(276, 573)
        Me.tp_artist_clearlogo.TabIndex = 2
        Me.tp_artist_clearlogo.Text = "ClearLogo"
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
        Me.lv_artist_clearlogo.Size = New System.Drawing.Size(252, 476)
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
        Me.tp_albums.Size = New System.Drawing.Size(642, 613)
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
        Me.lv_album.MultiSelect = False
        Me.lv_album.Name = "lv_album"
        Me.lv_album.Size = New System.Drawing.Size(340, 599)
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
        Me.tbc_album.Size = New System.Drawing.Size(284, 599)
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
        Me.tp_Music_CDArt.Size = New System.Drawing.Size(276, 573)
        Me.tp_Music_CDArt.TabIndex = 0
        Me.tp_Music_CDArt.Text = "CDArt"
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
        Me.lv_album_cdart.Size = New System.Drawing.Size(252, 352)
        Me.lv_album_cdart.TabIndex = 13
        Me.lv_album_cdart.UseCompatibleStateImageBehavior = False
        '
        'tp_artist_album_missing
        '
        Me.tp_artist_album_missing.BackColor = System.Drawing.SystemColors.Control
        Me.tp_artist_album_missing.Controls.Add(Me.lv_music_missing)
        Me.tp_artist_album_missing.Location = New System.Drawing.Point(4, 22)
        Me.tp_artist_album_missing.Name = "tp_artist_album_missing"
        Me.tp_artist_album_missing.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_artist_album_missing.Size = New System.Drawing.Size(642, 613)
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
        Me.lv_music_missing.Size = New System.Drawing.Size(628, 599)
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
        'u_Artist
        '
        Me.u_Artist.Text = ""
        Me.u_Artist.Width = 0
        '
        'tp_Importer
        '
        Me.tp_Importer.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Importer.Controls.Add(Me.lv_import)
        Me.tp_Importer.Controls.Add(Me.b_import)
        Me.tp_Importer.Location = New System.Drawing.Point(4, 22)
        Me.tp_Importer.Name = "tp_Importer"
        Me.tp_Importer.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Importer.Size = New System.Drawing.Size(656, 703)
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
        Me.lv_import.Size = New System.Drawing.Size(628, 656)
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
        'b_import
        '
        Me.b_import.Location = New System.Drawing.Point(521, 677)
        Me.b_import.Name = "b_import"
        Me.b_import.Size = New System.Drawing.Size(121, 23)
        Me.b_import.TabIndex = 30
        Me.b_import.Text = "Start Importer Now"
        Me.b_import.UseVisualStyleBackColor = True
        Me.b_import.Visible = False
        '
        'tp_Settings
        '
        Me.tp_Settings.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_Settings.Controls.Add(Me.tbc_settings)
        Me.tp_Settings.Location = New System.Drawing.Point(4, 22)
        Me.tp_Settings.Name = "tp_Settings"
        Me.tp_Settings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_Settings.Size = New System.Drawing.Size(656, 703)
        Me.tp_Settings.TabIndex = 2
        Me.tp_Settings.Text = "Settings"
        '
        'tbc_settings
        '
        Me.tbc_settings.Controls.Add(Me.tp_pluginsettings)
        Me.tbc_settings.Controls.Add(Me.tp_scrapersettings)
        Me.tbc_settings.Location = New System.Drawing.Point(7, 7)
        Me.tbc_settings.Name = "tbc_settings"
        Me.tbc_settings.SelectedIndex = 0
        Me.tbc_settings.Size = New System.Drawing.Size(643, 690)
        Me.tbc_settings.TabIndex = 24
        '
        'tp_pluginsettings
        '
        Me.tp_pluginsettings.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_pluginsettings.Controls.Add(Me.b_save1)
        Me.tp_pluginsettings.Controls.Add(Me.gb4)
        Me.tp_pluginsettings.Controls.Add(Me.gb1)
        Me.tp_pluginsettings.Controls.Add(Me.gb3)
        Me.tp_pluginsettings.Controls.Add(Me.cb_debug)
        Me.tp_pluginsettings.Location = New System.Drawing.Point(4, 22)
        Me.tp_pluginsettings.Name = "tp_pluginsettings"
        Me.tp_pluginsettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_pluginsettings.Size = New System.Drawing.Size(635, 664)
        Me.tp_pluginsettings.TabIndex = 0
        Me.tp_pluginsettings.Text = "Plugin Settings"
        '
        'b_save1
        '
        Me.b_save1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_save1.Location = New System.Drawing.Point(280, 631)
        Me.b_save1.Name = "b_save1"
        Me.b_save1.Size = New System.Drawing.Size(75, 23)
        Me.b_save1.TabIndex = 24
        Me.b_save1.Text = "Save"
        Me.b_save1.UseVisualStyleBackColor = True
        '
        'gb4
        '
        Me.gb4.Controls.Add(Me.tb_personalapikey)
        Me.gb4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb4.Location = New System.Drawing.Point(25, 353)
        Me.gb4.Name = "gb4"
        Me.gb4.Size = New System.Drawing.Size(584, 73)
        Me.gb4.TabIndex = 23
        Me.gb4.TabStop = False
        Me.gb4.Text = " Personal Fanart.tv API key "
        '
        'tb_personalapikey
        '
        Me.tb_personalapikey.Location = New System.Drawing.Point(136, 30)
        Me.tb_personalapikey.Name = "tb_personalapikey"
        Me.tb_personalapikey.Size = New System.Drawing.Size(313, 22)
        Me.tb_personalapikey.TabIndex = 4
        Me.tb_personalapikey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'gb1
        '
        Me.gb1.Controls.Add(Me.cb_backgroundscraper)
        Me.gb1.Controls.Add(Me.pnl_background)
        Me.gb1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb1.Location = New System.Drawing.Point(25, 148)
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
        'pnl_background
        '
        Me.pnl_background.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_background.Controls.Add(Me.Label3)
        Me.pnl_background.Controls.Add(Me.cb_delay)
        Me.pnl_background.Controls.Add(Me.nud_delay)
        Me.pnl_background.Controls.Add(Me.cb_missing)
        Me.pnl_background.Controls.Add(Me.nud_missing)
        Me.pnl_background.Controls.Add(Me.Label5)
        Me.pnl_background.Controls.Add(Me.cb_scraping)
        Me.pnl_background.Controls.Add(Me.Label4)
        Me.pnl_background.Controls.Add(Me.nud_scraping)
        Me.pnl_background.Controls.Add(Me.Label2)
        Me.pnl_background.Controls.Add(Me.mtb_cpu)
        Me.pnl_background.Location = New System.Drawing.Point(17, 32)
        Me.pnl_background.Name = "pnl_background"
        Me.pnl_background.Size = New System.Drawing.Size(550, 145)
        Me.pnl_background.TabIndex = 21
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
        Me.cb_missing.Items.AddRange(New Object() {"days", "weeks", "months", "disabled"})
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
        Me.cb_scraping.Items.AddRange(New Object() {"minutes", "hours"})
        Me.cb_scraping.Location = New System.Drawing.Point(163, 81)
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
        Me.nud_scraping.Size = New System.Drawing.Size(46, 22)
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
        'gb3
        '
        Me.gb3.Controls.Add(Me.pnl_online)
        Me.gb3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb3.Location = New System.Drawing.Point(25, 36)
        Me.gb3.Name = "gb3"
        Me.gb3.Size = New System.Drawing.Size(584, 97)
        Me.gb3.TabIndex = 22
        Me.gb3.TabStop = False
        Me.gb3.Text = " On-Line Importer settings "
        '
        'pnl_online
        '
        Me.pnl_online.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnl_online.Controls.Add(Me.nud_downloads)
        Me.pnl_online.Controls.Add(Me.l_downloads)
        Me.pnl_online.Controls.Add(Me.cb_downloads)
        Me.pnl_online.Controls.Add(Me.cb_autoimport)
        Me.pnl_online.Location = New System.Drawing.Point(17, 26)
        Me.pnl_online.Name = "pnl_online"
        Me.pnl_online.Size = New System.Drawing.Size(550, 59)
        Me.pnl_online.TabIndex = 20
        '
        'nud_downloads
        '
        Me.nud_downloads.Location = New System.Drawing.Point(481, 28)
        Me.nud_downloads.Maximum = New Decimal(New Integer() {99, 0, 0, 0})
        Me.nud_downloads.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.nud_downloads.Name = "nud_downloads"
        Me.nud_downloads.Size = New System.Drawing.Size(44, 22)
        Me.nud_downloads.TabIndex = 33
        Me.nud_downloads.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.nud_downloads.Value = New Decimal(New Integer() {3, 0, 0, 0})
        Me.nud_downloads.Visible = False
        '
        'l_downloads
        '
        Me.l_downloads.AutoSize = True
        Me.l_downloads.Location = New System.Drawing.Point(357, 30)
        Me.l_downloads.Name = "l_downloads"
        Me.l_downloads.Size = New System.Drawing.Size(118, 16)
        Me.l_downloads.TabIndex = 32
        Me.l_downloads.Text = "Limit downloads to"
        Me.l_downloads.Visible = False
        '
        'cb_downloads
        '
        Me.cb_downloads.AutoSize = True
        Me.cb_downloads.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.cb_downloads.Location = New System.Drawing.Point(23, 29)
        Me.cb_downloads.Name = "cb_downloads"
        Me.cb_downloads.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cb_downloads.Size = New System.Drawing.Size(169, 20)
        Me.cb_downloads.TabIndex = 31
        Me.cb_downloads.Text = "Limit artwork downloads"
        Me.cb_downloads.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cb_downloads.UseVisualStyleBackColor = True
        '
        'cb_autoimport
        '
        Me.cb_autoimport.AutoSize = True
        Me.cb_autoimport.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.cb_autoimport.Location = New System.Drawing.Point(23, 9)
        Me.cb_autoimport.Name = "cb_autoimport"
        Me.cb_autoimport.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cb_autoimport.Size = New System.Drawing.Size(294, 20)
        Me.cb_autoimport.TabIndex = 30
        Me.cb_autoimport.Text = "Automatically start Importing on Plugin startup"
        Me.cb_autoimport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cb_autoimport.UseVisualStyleBackColor = True
        '
        'cb_debug
        '
        Me.cb_debug.AutoSize = True
        Me.cb_debug.Location = New System.Drawing.Point(517, 13)
        Me.cb_debug.Name = "cb_debug"
        Me.cb_debug.Size = New System.Drawing.Size(92, 17)
        Me.cb_debug.TabIndex = 23
        Me.cb_debug.Text = "Enable debug"
        Me.cb_debug.UseVisualStyleBackColor = True
        '
        'tp_scrapersettings
        '
        Me.tp_scrapersettings.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_scrapersettings.Controls.Add(Me.b_save2)
        Me.tp_scrapersettings.Controls.Add(Me.gb2)
        Me.tp_scrapersettings.Location = New System.Drawing.Point(4, 22)
        Me.tp_scrapersettings.Name = "tp_scrapersettings"
        Me.tp_scrapersettings.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_scrapersettings.Size = New System.Drawing.Size(635, 664)
        Me.tp_scrapersettings.TabIndex = 2
        Me.tp_scrapersettings.Text = "Scraper Settings"
        '
        'b_save2
        '
        Me.b_save2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.b_save2.Location = New System.Drawing.Point(280, 631)
        Me.b_save2.Name = "b_save2"
        Me.b_save2.Size = New System.Drawing.Size(75, 23)
        Me.b_save2.TabIndex = 25
        Me.b_save2.Text = "Save"
        Me.b_save2.UseVisualStyleBackColor = True
        '
        'gb2
        '
        Me.gb2.Controls.Add(Me.tbc_scraper)
        Me.gb2.Controls.Add(Me.Label7)
        Me.gb2.Controls.Add(Me.cb_language)
        Me.gb2.Controls.Add(Me.Label6)
        Me.gb2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gb2.Location = New System.Drawing.Point(25, 36)
        Me.gb2.Name = "gb2"
        Me.gb2.Size = New System.Drawing.Size(584, 570)
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
        Me.tbc_scraper.Size = New System.Drawing.Size(554, 507)
        Me.tbc_scraper.TabIndex = 9
        '
        'tp_sMovies
        '
        Me.tp_sMovies.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_sMovies.Controls.Add(Me.tbc_movie_settings)
        Me.tp_sMovies.Location = New System.Drawing.Point(4, 25)
        Me.tp_sMovies.Name = "tp_sMovies"
        Me.tp_sMovies.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_sMovies.Size = New System.Drawing.Size(546, 478)
        Me.tp_sMovies.TabIndex = 0
        Me.tp_sMovies.Text = "Movies"
        '
        'tbc_movie_settings
        '
        Me.tbc_movie_settings.Controls.Add(Me.tp_movies_scraper)
        Me.tbc_movie_settings.Controls.Add(Me.tp_manual_dvdart)
        Me.tbc_movie_settings.Controls.Add(Me.tp_movies_path)
        Me.tbc_movie_settings.Controls.Add(Me.tp_movies_persons)
        Me.tbc_movie_settings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbc_movie_settings.Location = New System.Drawing.Point(6, 6)
        Me.tbc_movie_settings.Name = "tbc_movie_settings"
        Me.tbc_movie_settings.SelectedIndex = 0
        Me.tbc_movie_settings.Size = New System.Drawing.Size(533, 466)
        Me.tbc_movie_settings.TabIndex = 18
        '
        'tp_movies_scraper
        '
        Me.tp_movies_scraper.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_movies_scraper.Controls.Add(Me.PictureBox5)
        Me.tp_movies_scraper.Controls.Add(Me.cb_Banner_movies)
        Me.tp_movies_scraper.Controls.Add(Me.cb_Cover_movies)
        Me.tp_movies_scraper.Controls.Add(Me.PictureBox15)
        Me.tp_movies_scraper.Controls.Add(Me.PictureBox12)
        Me.tp_movies_scraper.Controls.Add(Me.cb_Backdrop_movies)
        Me.tp_movies_scraper.Controls.Add(Me.pb2)
        Me.tp_movies_scraper.Controls.Add(Me.pb1)
        Me.tp_movies_scraper.Controls.Add(Me.pb3)
        Me.tp_movies_scraper.Controls.Add(Me.cb_DVDArt_movies)
        Me.tp_movies_scraper.Controls.Add(Me.cb_ClearArt_movies)
        Me.tp_movies_scraper.Controls.Add(Me.cb_ClearLogo_movies)
        Me.tp_movies_scraper.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies_scraper.Name = "tp_movies_scraper"
        Me.tp_movies_scraper.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies_scraper.Size = New System.Drawing.Size(525, 440)
        Me.tp_movies_scraper.TabIndex = 0
        Me.tp_movies_scraper.Text = "Scraper options"
        '
        'cb_Banner_movies
        '
        Me.cb_Banner_movies.AutoSize = True
        Me.cb_Banner_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_Banner_movies.Location = New System.Drawing.Point(89, 232)
        Me.cb_Banner_movies.Name = "cb_Banner_movies"
        Me.cb_Banner_movies.Size = New System.Drawing.Size(70, 20)
        Me.cb_Banner_movies.TabIndex = 22
        Me.cb_Banner_movies.Text = "Banner"
        Me.cb_Banner_movies.UseVisualStyleBackColor = True
        '
        'cb_Cover_movies
        '
        Me.cb_Cover_movies.AutoSize = True
        Me.cb_Cover_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_Cover_movies.Location = New System.Drawing.Point(89, 367)
        Me.cb_Cover_movies.Name = "cb_Cover_movies"
        Me.cb_Cover_movies.Size = New System.Drawing.Size(63, 20)
        Me.cb_Cover_movies.TabIndex = 21
        Me.cb_Cover_movies.Text = "Cover"
        Me.cb_Cover_movies.UseVisualStyleBackColor = True
        '
        'cb_Backdrop_movies
        '
        Me.cb_Backdrop_movies.AutoSize = True
        Me.cb_Backdrop_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_Backdrop_movies.Location = New System.Drawing.Point(89, 284)
        Me.cb_Backdrop_movies.Name = "cb_Backdrop_movies"
        Me.cb_Backdrop_movies.Size = New System.Drawing.Size(86, 20)
        Me.cb_Backdrop_movies.TabIndex = 18
        Me.cb_Backdrop_movies.Text = "Backdrop"
        Me.cb_Backdrop_movies.UseVisualStyleBackColor = True
        '
        'cb_DVDArt_movies
        '
        Me.cb_DVDArt_movies.AutoSize = True
        Me.cb_DVDArt_movies.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_DVDArt_movies.Location = New System.Drawing.Point(89, 54)
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
        Me.cb_ClearArt_movies.Location = New System.Drawing.Point(89, 137)
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
        Me.cb_ClearLogo_movies.Location = New System.Drawing.Point(89, 189)
        Me.cb_ClearLogo_movies.Name = "cb_ClearLogo_movies"
        Me.cb_ClearLogo_movies.Size = New System.Drawing.Size(205, 20)
        Me.cb_ClearLogo_movies.TabIndex = 14
        Me.cb_ClearLogo_movies.Text = "ClearLogo and HD ClearLogo"
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
        Me.tp_manual_dvdart.Size = New System.Drawing.Size(525, 440)
        Me.tp_manual_dvdart.TabIndex = 1
        Me.tp_manual_dvdart.Text = "Manual DVDArt layout options"
        '
        'tp_movies_path
        '
        Me.tp_movies_path.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_movies_path.Controls.Add(Me.tb_movie_path)
        Me.tp_movies_path.Controls.Add(Me.b_movie_path)
        Me.tp_movies_path.Controls.Add(Me.Label8)
        Me.tp_movies_path.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies_path.Name = "tp_movies_path"
        Me.tp_movies_path.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies_path.Size = New System.Drawing.Size(525, 440)
        Me.tp_movies_path.TabIndex = 2
        Me.tp_movies_path.Text = "Movies Path"
        '
        'tb_movie_path
        '
        Me.tb_movie_path.Location = New System.Drawing.Point(22, 55)
        Me.tb_movie_path.Name = "tb_movie_path"
        Me.tb_movie_path.Size = New System.Drawing.Size(393, 20)
        Me.tb_movie_path.TabIndex = 3
        '
        'b_movie_path
        '
        Me.b_movie_path.Location = New System.Drawing.Point(431, 53)
        Me.b_movie_path.Name = "b_movie_path"
        Me.b_movie_path.Size = New System.Drawing.Size(75, 23)
        Me.b_movie_path.TabIndex = 2
        Me.b_movie_path.Text = "Browse"
        Me.b_movie_path.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 32)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Artwork Path"
        '
        'tp_movies_persons
        '
        Me.tp_movies_persons.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_movies_persons.Controls.Add(Me.tb_person_path)
        Me.tp_movies_persons.Controls.Add(Me.b_person_path)
        Me.tp_movies_persons.Controls.Add(Me.Label11)
        Me.tp_movies_persons.Controls.Add(Me.cb_persons)
        Me.tp_movies_persons.Location = New System.Drawing.Point(4, 22)
        Me.tp_movies_persons.Name = "tp_movies_persons"
        Me.tp_movies_persons.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_movies_persons.Size = New System.Drawing.Size(525, 440)
        Me.tp_movies_persons.TabIndex = 3
        Me.tp_movies_persons.Text = "Actors/Writers/Directors"
        '
        'tb_person_path
        '
        Me.tb_person_path.Location = New System.Drawing.Point(22, 156)
        Me.tb_person_path.Name = "tb_person_path"
        Me.tb_person_path.Size = New System.Drawing.Size(393, 20)
        Me.tb_person_path.TabIndex = 16
        '
        'b_person_path
        '
        Me.b_person_path.Location = New System.Drawing.Point(431, 154)
        Me.b_person_path.Name = "b_person_path"
        Me.b_person_path.Size = New System.Drawing.Size(75, 23)
        Me.b_person_path.TabIndex = 15
        Me.b_person_path.Text = "Browse"
        Me.b_person_path.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(19, 133)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(178, 13)
        Me.Label11.TabIndex = 14
        Me.Label11.Text = "Actors/Writers/Directors image Path"
        '
        'cb_persons
        '
        Me.cb_persons.AutoSize = True
        Me.cb_persons.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cb_persons.Location = New System.Drawing.Point(22, 80)
        Me.cb_persons.Name = "cb_persons"
        Me.cb_persons.Size = New System.Drawing.Size(169, 20)
        Me.cb_persons.TabIndex = 13
        Me.cb_persons.Text = "Actors/Writers/Directors"
        Me.cb_persons.UseVisualStyleBackColor = True
        '
        'tp_sSeries
        '
        Me.tp_sSeries.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_sSeries.Controls.Add(Me.tbc_series_settings)
        Me.tp_sSeries.Location = New System.Drawing.Point(4, 25)
        Me.tp_sSeries.Name = "tp_sSeries"
        Me.tp_sSeries.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_sSeries.Size = New System.Drawing.Size(546, 478)
        Me.tp_sSeries.TabIndex = 1
        Me.tp_sSeries.Text = "Series"
        '
        'tbc_series_settings
        '
        Me.tbc_series_settings.Controls.Add(Me.tp_series_scraper)
        Me.tbc_series_settings.Controls.Add(Me.tp_series_path)
        Me.tbc_series_settings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbc_series_settings.Location = New System.Drawing.Point(6, 6)
        Me.tbc_series_settings.Name = "tbc_series_settings"
        Me.tbc_series_settings.SelectedIndex = 0
        Me.tbc_series_settings.Size = New System.Drawing.Size(533, 466)
        Me.tbc_series_settings.TabIndex = 19
        '
        'tp_series_scraper
        '
        Me.tp_series_scraper.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_series_scraper.Controls.Add(Me.PictureBox3)
        Me.tp_series_scraper.Controls.Add(Me.PictureBox4)
        Me.tp_series_scraper.Controls.Add(Me.cb_ClearArt_series)
        Me.tp_series_scraper.Controls.Add(Me.cb_ClearLogo_series)
        Me.tp_series_scraper.Location = New System.Drawing.Point(4, 22)
        Me.tp_series_scraper.Name = "tp_series_scraper"
        Me.tp_series_scraper.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_series_scraper.Size = New System.Drawing.Size(525, 440)
        Me.tp_series_scraper.TabIndex = 0
        Me.tp_series_scraper.Text = "Scraper options"
        '
        'cb_ClearArt_series
        '
        Me.cb_ClearArt_series.AutoSize = True
        Me.cb_ClearArt_series.Location = New System.Drawing.Point(89, 184)
        Me.cb_ClearArt_series.Name = "cb_ClearArt_series"
        Me.cb_ClearArt_series.Size = New System.Drawing.Size(146, 17)
        Me.cb_ClearArt_series.TabIndex = 13
        Me.cb_ClearArt_series.Text = "ClearArt and HD Clear Art"
        Me.cb_ClearArt_series.UseVisualStyleBackColor = True
        '
        'cb_ClearLogo_series
        '
        Me.cb_ClearLogo_series.AutoSize = True
        Me.cb_ClearLogo_series.Location = New System.Drawing.Point(89, 249)
        Me.cb_ClearLogo_series.Name = "cb_ClearLogo_series"
        Me.cb_ClearLogo_series.Size = New System.Drawing.Size(74, 17)
        Me.cb_ClearLogo_series.TabIndex = 14
        Me.cb_ClearLogo_series.Text = "ClearLogo"
        Me.cb_ClearLogo_series.UseVisualStyleBackColor = True
        '
        'tp_series_path
        '
        Me.tp_series_path.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_series_path.Controls.Add(Me.tb_series_path)
        Me.tp_series_path.Controls.Add(Me.b_series_path)
        Me.tp_series_path.Controls.Add(Me.Label9)
        Me.tp_series_path.Location = New System.Drawing.Point(4, 22)
        Me.tp_series_path.Name = "tp_series_path"
        Me.tp_series_path.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_series_path.Size = New System.Drawing.Size(525, 440)
        Me.tp_series_path.TabIndex = 2
        Me.tp_series_path.Text = "Path"
        '
        'tb_series_path
        '
        Me.tb_series_path.Location = New System.Drawing.Point(22, 55)
        Me.tb_series_path.Name = "tb_series_path"
        Me.tb_series_path.Size = New System.Drawing.Size(393, 20)
        Me.tb_series_path.TabIndex = 3
        '
        'b_series_path
        '
        Me.b_series_path.Location = New System.Drawing.Point(431, 55)
        Me.b_series_path.Name = "b_series_path"
        Me.b_series_path.Size = New System.Drawing.Size(75, 23)
        Me.b_series_path.TabIndex = 2
        Me.b_series_path.Text = "Browse"
        Me.b_series_path.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 32)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(68, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Artwork Path"
        '
        'tp_sMusic
        '
        Me.tp_sMusic.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_sMusic.Controls.Add(Me.tbc_music_settings)
        Me.tp_sMusic.Location = New System.Drawing.Point(4, 25)
        Me.tp_sMusic.Name = "tp_sMusic"
        Me.tp_sMusic.Size = New System.Drawing.Size(546, 478)
        Me.tp_sMusic.TabIndex = 2
        Me.tp_sMusic.Text = "Music"
        '
        'tbc_music_settings
        '
        Me.tbc_music_settings.Controls.Add(Me.tp_music_scraper)
        Me.tbc_music_settings.Controls.Add(Me.tp_music_path)
        Me.tbc_music_settings.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tbc_music_settings.Location = New System.Drawing.Point(6, 6)
        Me.tbc_music_settings.Name = "tbc_music_settings"
        Me.tbc_music_settings.SelectedIndex = 0
        Me.tbc_music_settings.Size = New System.Drawing.Size(533, 466)
        Me.tbc_music_settings.TabIndex = 20
        '
        'tp_music_scraper
        '
        Me.tp_music_scraper.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_music_scraper.Controls.Add(Me.PictureBox6)
        Me.tp_music_scraper.Controls.Add(Me.PictureBox8)
        Me.tp_music_scraper.Controls.Add(Me.cb_ClearLogo_artist)
        Me.tp_music_scraper.Controls.Add(Me.PictureBox7)
        Me.tp_music_scraper.Controls.Add(Me.cb_Banner_artist)
        Me.tp_music_scraper.Controls.Add(Me.cb_CDArt_music)
        Me.tp_music_scraper.Location = New System.Drawing.Point(4, 22)
        Me.tp_music_scraper.Name = "tp_music_scraper"
        Me.tp_music_scraper.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_music_scraper.Size = New System.Drawing.Size(525, 440)
        Me.tp_music_scraper.TabIndex = 0
        Me.tp_music_scraper.Text = "Scraper options"
        '
        'cb_ClearLogo_artist
        '
        Me.cb_ClearLogo_artist.AutoSize = True
        Me.cb_ClearLogo_artist.Location = New System.Drawing.Point(89, 291)
        Me.cb_ClearLogo_artist.Name = "cb_ClearLogo_artist"
        Me.cb_ClearLogo_artist.Size = New System.Drawing.Size(141, 17)
        Me.cb_ClearLogo_artist.TabIndex = 14
        Me.cb_ClearLogo_artist.Text = "ClearLogo and HD Logo"
        Me.cb_ClearLogo_artist.UseVisualStyleBackColor = True
        '
        'cb_Banner_artist
        '
        Me.cb_Banner_artist.AutoSize = True
        Me.cb_Banner_artist.Location = New System.Drawing.Point(89, 242)
        Me.cb_Banner_artist.Name = "cb_Banner_artist"
        Me.cb_Banner_artist.Size = New System.Drawing.Size(60, 17)
        Me.cb_Banner_artist.TabIndex = 13
        Me.cb_Banner_artist.Text = "Banner"
        Me.cb_Banner_artist.UseVisualStyleBackColor = True
        '
        'cb_CDArt_music
        '
        Me.cb_CDArt_music.AutoSize = True
        Me.cb_CDArt_music.Location = New System.Drawing.Point(89, 163)
        Me.cb_CDArt_music.Name = "cb_CDArt_music"
        Me.cb_CDArt_music.Size = New System.Drawing.Size(57, 17)
        Me.cb_CDArt_music.TabIndex = 12
        Me.cb_CDArt_music.Text = "CD Art"
        Me.cb_CDArt_music.UseVisualStyleBackColor = True
        '
        'tp_music_path
        '
        Me.tp_music_path.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_music_path.Controls.Add(Me.tb_music_path)
        Me.tp_music_path.Controls.Add(Me.b_music_path)
        Me.tp_music_path.Controls.Add(Me.Label10)
        Me.tp_music_path.Location = New System.Drawing.Point(4, 22)
        Me.tp_music_path.Name = "tp_music_path"
        Me.tp_music_path.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_music_path.Size = New System.Drawing.Size(525, 440)
        Me.tp_music_path.TabIndex = 2
        Me.tp_music_path.Text = "Path"
        '
        'tb_music_path
        '
        Me.tb_music_path.Location = New System.Drawing.Point(22, 55)
        Me.tb_music_path.Name = "tb_music_path"
        Me.tb_music_path.Size = New System.Drawing.Size(393, 20)
        Me.tb_music_path.TabIndex = 3
        '
        'b_music_path
        '
        Me.b_music_path.Location = New System.Drawing.Point(431, 55)
        Me.b_music_path.Name = "b_music_path"
        Me.b_music_path.Size = New System.Drawing.Size(75, 23)
        Me.b_music_path.TabIndex = 2
        Me.b_music_path.Text = "Browse"
        Me.b_music_path.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(19, 32)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(68, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Artwork Path"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(421, 544)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(147, 9)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "(English is by default secondary language)"
        '
        'cb_language
        '
        Me.cb_language.FormattingEnabled = True
        Me.cb_language.Location = New System.Drawing.Point(273, 535)
        Me.cb_language.Name = "cb_language"
        Me.cb_language.Size = New System.Drawing.Size(142, 24)
        Me.cb_language.TabIndex = 7
        Me.cb_language.Text = "Any"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(13, 538)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(194, 16)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Preferred language for Importer"
        '
        'tp_about
        '
        Me.tp_about.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.tp_about.Controls.Add(Me.l_copyright)
        Me.tp_about.Controls.Add(Me.l_version)
        Me.tp_about.Controls.Add(Me.Label14)
        Me.tp_about.Controls.Add(Me.GroupBox6)
        Me.tp_about.Controls.Add(Me.GroupBox3)
        Me.tp_about.Controls.Add(Me.GroupBox2)
        Me.tp_about.Controls.Add(Me.GroupBox1)
        Me.tp_about.Controls.Add(Me.PictureBox1)
        Me.tp_about.Location = New System.Drawing.Point(4, 22)
        Me.tp_about.Name = "tp_about"
        Me.tp_about.Padding = New System.Windows.Forms.Padding(3)
        Me.tp_about.Size = New System.Drawing.Size(656, 703)
        Me.tp_about.TabIndex = 5
        Me.tp_about.Text = "About"
        '
        'l_copyright
        '
        Me.l_copyright.AutoSize = True
        Me.l_copyright.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.l_copyright.Location = New System.Drawing.Point(222, 132)
        Me.l_copyright.Name = "l_copyright"
        Me.l_copyright.Size = New System.Drawing.Size(166, 13)
        Me.l_copyright.TabIndex = 32
        Me.l_copyright.Text = "Copyright ©  2012-2014 : m3rcury"
        Me.l_copyright.TextAlign = System.Drawing.ContentAlignment.BottomRight
        Me.l_copyright.UseWaitCursor = True
        '
        'l_version
        '
        Me.l_version.AutoSize = True
        Me.l_version.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.l_version.Location = New System.Drawing.Point(222, 118)
        Me.l_version.Name = "l_version"
        Me.l_version.Size = New System.Drawing.Size(41, 13)
        Me.l_version.TabIndex = 31
        Me.l_version.Text = "version"
        Me.l_version.UseWaitCursor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(210, 66)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(410, 59)
        Me.Label14.TabIndex = 30
        Me.Label14.Text = "DVDArt Plugin"
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label16)
        Me.GroupBox6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox6.Location = New System.Drawing.Point(36, 180)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(584, 83)
        Me.GroupBox6.TabIndex = 26
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = " About "
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(46, 20)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(492, 48)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "This plugin scans your MovingPictures, My Films, My Videos, TVSeries and Music" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "d" & _
    "atabases to download available Artwork from the fanart.tv and themoviedb.org" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "we" & _
    "bsites."
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.GroupBox9)
        Me.GroupBox3.Controls.Add(Me.GroupBox7)
        Me.GroupBox3.Controls.Add(Me.GroupBox8)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(36, 180)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(584, 83)
        Me.GroupBox3.TabIndex = 26
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = " About "
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(46, 20)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(492, 48)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "This plugin scans your MovingPictures, My Films, My Videos, TVSeries and Music" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "d" & _
    "atabases to download available Artwork from the fanart.tv and themoviedb.org" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "we" & _
    "bsites."
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label18)
        Me.GroupBox9.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox9.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(584, 83)
        Me.GroupBox9.TabIndex = 26
        Me.GroupBox9.TabStop = False
        Me.GroupBox9.Text = " About "
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(46, 20)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(492, 48)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "This plugin scans your MovingPictures, My Films, My Videos, TVSeries and Music" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "d" & _
    "atabases to download available Artwork from the fanart.tv and themoviedb.org" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "we" & _
    "bsites."
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.LinkLabel5)
        Me.GroupBox7.Controls.Add(Me.LinkLabel6)
        Me.GroupBox7.Controls.Add(Me.LinkLabel7)
        Me.GroupBox7.Controls.Add(Me.LinkLabel8)
        Me.GroupBox7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox7.Location = New System.Drawing.Point(0, 92)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(282, 118)
        Me.GroupBox7.TabIndex = 27
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = " On-line resouces "
        '
        'LinkLabel5
        '
        Me.LinkLabel5.AutoSize = True
        Me.LinkLabel5.Location = New System.Drawing.Point(38, 82)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.Size = New System.Drawing.Size(69, 16)
        Me.LinkLabel5.TabIndex = 6
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "Wiki page"
        '
        'LinkLabel6
        '
        Me.LinkLabel6.AutoSize = True
        Me.LinkLabel6.Location = New System.Drawing.Point(38, 64)
        Me.LinkLabel6.Name = "LinkLabel6"
        Me.LinkLabel6.Size = New System.Drawing.Size(110, 16)
        Me.LinkLabel6.TabIndex = 5
        Me.LinkLabel6.TabStop = True
        Me.LinkLabel6.Text = "Discussion forum"
        '
        'LinkLabel7
        '
        Me.LinkLabel7.AutoSize = True
        Me.LinkLabel7.Location = New System.Drawing.Point(38, 46)
        Me.LinkLabel7.Name = "LinkLabel7"
        Me.LinkLabel7.Size = New System.Drawing.Size(74, 16)
        Me.LinkLabel7.TabIndex = 4
        Me.LinkLabel7.TabStop = True
        Me.LinkLabel7.Text = "Project site"
        '
        'LinkLabel8
        '
        Me.LinkLabel8.AutoSize = True
        Me.LinkLabel8.Location = New System.Drawing.Point(38, 28)
        Me.LinkLabel8.Name = "LinkLabel8"
        Me.LinkLabel8.Size = New System.Drawing.Size(110, 16)
        Me.LinkLabel8.TabIndex = 0
        Me.LinkLabel8.TabStop = True
        Me.LinkLabel8.Text = "Plugin developer"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.Label17)
        Me.GroupBox8.Controls.Add(Me.PictureBox10)
        Me.GroupBox8.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox8.Location = New System.Drawing.Point(302, 92)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(282, 118)
        Me.GroupBox8.TabIndex = 28
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = " Donate "
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(23, 22)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(236, 26)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "If you enjoy DVDArt, please consider donating to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "help support continued developm" & _
    "ent.  Thanks."
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.pb_donate)
        Me.GroupBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(338, 272)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(282, 118)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = " Donate "
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(23, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(236, 26)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "If you enjoy DVDArt, please consider donating to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "help support continued developm" & _
    "ent.  Thanks."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ll_wiki)
        Me.GroupBox1.Controls.Add(Me.ll_forum)
        Me.GroupBox1.Controls.Add(Me.ll_project)
        Me.GroupBox1.Controls.Add(Me.ll_developer)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(36, 272)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(282, 118)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = " On-line resouces "
        '
        'll_wiki
        '
        Me.ll_wiki.AutoSize = True
        Me.ll_wiki.Location = New System.Drawing.Point(38, 82)
        Me.ll_wiki.Name = "ll_wiki"
        Me.ll_wiki.Size = New System.Drawing.Size(69, 16)
        Me.ll_wiki.TabIndex = 6
        Me.ll_wiki.TabStop = True
        Me.ll_wiki.Text = "Wiki page"
        '
        'll_forum
        '
        Me.ll_forum.AutoSize = True
        Me.ll_forum.Location = New System.Drawing.Point(38, 64)
        Me.ll_forum.Name = "ll_forum"
        Me.ll_forum.Size = New System.Drawing.Size(110, 16)
        Me.ll_forum.TabIndex = 5
        Me.ll_forum.TabStop = True
        Me.ll_forum.Text = "Discussion forum"
        '
        'll_project
        '
        Me.ll_project.AutoSize = True
        Me.ll_project.Location = New System.Drawing.Point(38, 46)
        Me.ll_project.Name = "ll_project"
        Me.ll_project.Size = New System.Drawing.Size(74, 16)
        Me.ll_project.TabIndex = 4
        Me.ll_project.TabStop = True
        Me.ll_project.Text = "Project site"
        '
        'll_developer
        '
        Me.ll_developer.AutoSize = True
        Me.ll_developer.Location = New System.Drawing.Point(38, 28)
        Me.ll_developer.Name = "ll_developer"
        Me.ll_developer.Size = New System.Drawing.Size(110, 16)
        Me.ll_developer.TabIndex = 0
        Me.ll_developer.TabStop = True
        Me.ll_developer.Text = "Plugin developer"
        '
        'il_column
        '
        Me.il_column.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit
        Me.il_column.ImageSize = New System.Drawing.Size(8, 8)
        Me.il_column.TransparentColor = System.Drawing.Color.Transparent
        '
        'pbthemoviedb
        '
        Me.pbthemoviedb.Image = Global.DVDArt_Plugin.My.Resources.Resources.tmdb_logo
        Me.pbthemoviedb.Location = New System.Drawing.Point(522, 680)
        Me.pbthemoviedb.Name = "pbthemoviedb"
        Me.pbthemoviedb.Size = New System.Drawing.Size(96, 20)
        Me.pbthemoviedb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbthemoviedb.TabIndex = 33
        Me.pbthemoviedb.TabStop = False
        '
        'pb_movingpictures
        '
        Me.pb_movingpictures.BackColor = System.Drawing.Color.Transparent
        Me.pb_movingpictures.Image = Global.DVDArt_Plugin.My.Resources.Resources.movingpictures
        Me.pb_movingpictures.Location = New System.Drawing.Point(259, 644)
        Me.pb_movingpictures.Name = "pb_movingpictures"
        Me.pb_movingpictures.Size = New System.Drawing.Size(208, 59)
        Me.pb_movingpictures.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_movingpictures.TabIndex = 32
        Me.pb_movingpictures.TabStop = False
        '
        'RefreshArtworkFromOnline_found
        '
        Me.RefreshArtworkFromOnline_found.Image = Global.DVDArt_Plugin.My.Resources.Resources.movie_search
        Me.RefreshArtworkFromOnline_found.Name = "RefreshArtworkFromOnline_found"
        Me.RefreshArtworkFromOnline_found.Size = New System.Drawing.Size(246, 22)
        Me.RefreshArtworkFromOnline_found.Text = "Refresh artwork from online"
        Me.RefreshArtworkFromOnline_found.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ManuallyUploadArtwork_found
        '
        Me.ManuallyUploadArtwork_found.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.ManuallyUploadArtwork_found.Name = "ManuallyUploadArtwork_found"
        Me.ManuallyUploadArtwork_found.Size = New System.Drawing.Size(246, 22)
        Me.ManuallyUploadArtwork_found.Text = "Manually Upload Artwork"
        Me.ManuallyUploadArtwork_found.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SelectCoverArtForDVDArt_found
        '
        Me.SelectCoverArtForDVDArt_found.Image = Global.DVDArt_Plugin.My.Resources.Resources.selectcoverart
        Me.SelectCoverArtForDVDArt_found.Name = "SelectCoverArtForDVDArt_found"
        Me.SelectCoverArtForDVDArt_found.Size = New System.Drawing.Size(246, 22)
        Me.SelectCoverArtForDVDArt_found.Text = "Select/Edit Cover Art for DVD Art"
        Me.SelectCoverArtForDVDArt_found.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ChangeMBID_found
        '
        Me.ChangeMBID_found.Image = Global.DVDArt_Plugin.My.Resources.Resources.musicbrainz_picard
        Me.ChangeMBID_found.Name = "ChangeMBID_found"
        Me.ChangeMBID_found.Size = New System.Drawing.Size(246, 22)
        Me.ChangeMBID_found.Text = "Change MBID"
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
        'b_movie_deletebanner
        '
        Me.b_movie_deletebanner.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_movie_deletebanner.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_deletebanner.Name = "b_movie_deletebanner"
        Me.b_movie_deletebanner.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_deletebanner.TabIndex = 20
        Me.b_movie_deletebanner.UseVisualStyleBackColor = True
        Me.b_movie_deletebanner.Visible = False
        '
        'pb_movie_banner
        '
        Me.pb_movie_banner.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_movie_banner.Location = New System.Drawing.Point(33, 9)
        Me.pb_movie_banner.Name = "pb_movie_banner"
        Me.pb_movie_banner.Size = New System.Drawing.Size(200, 37)
        Me.pb_movie_banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pb_movie_banner.TabIndex = 18
        Me.pb_movie_banner.TabStop = False
        '
        'b_movie_deletebackdrop
        '
        Me.b_movie_deletebackdrop.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_movie_deletebackdrop.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_deletebackdrop.Name = "b_movie_deletebackdrop"
        Me.b_movie_deletebackdrop.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_deletebackdrop.TabIndex = 20
        Me.b_movie_deletebackdrop.UseVisualStyleBackColor = True
        Me.b_movie_deletebackdrop.Visible = False
        '
        'pb_movie_backdrop
        '
        Me.pb_movie_backdrop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_movie_backdrop.Location = New System.Drawing.Point(33, 7)
        Me.pb_movie_backdrop.Name = "pb_movie_backdrop"
        Me.pb_movie_backdrop.Size = New System.Drawing.Size(200, 112)
        Me.pb_movie_backdrop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_movie_backdrop.TabIndex = 18
        Me.pb_movie_backdrop.TabStop = False
        '
        'b_movie_deletecover
        '
        Me.b_movie_deletecover.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_movie_deletecover.Location = New System.Drawing.Point(234, 7)
        Me.b_movie_deletecover.Name = "b_movie_deletecover"
        Me.b_movie_deletecover.Size = New System.Drawing.Size(40, 40)
        Me.b_movie_deletecover.TabIndex = 18
        Me.b_movie_deletecover.UseVisualStyleBackColor = True
        Me.b_movie_deletecover.Visible = False
        '
        'pb_movie_cover
        '
        Me.pb_movie_cover.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_movie_cover.Location = New System.Drawing.Point(65, 8)
        Me.pb_movie_cover.Name = "pb_movie_cover"
        Me.pb_movie_cover.Size = New System.Drawing.Size(146, 200)
        Me.pb_movie_cover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb_movie_cover.TabIndex = 14
        Me.pb_movie_cover.TabStop = False
        '
        'SendtoImporter_missing
        '
        Me.SendtoImporter_missing.Image = Global.DVDArt_Plugin.My.Resources.Resources.arrow
        Me.SendtoImporter_missing.Name = "SendtoImporter_missing"
        Me.SendtoImporter_missing.Size = New System.Drawing.Size(246, 22)
        Me.SendtoImporter_missing.Text = "Send to importer"
        Me.SendtoImporter_missing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ManuallyUpload_missing
        '
        Me.ManuallyUpload_missing.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.ManuallyUpload_missing.Name = "ManuallyUpload_missing"
        Me.ManuallyUpload_missing.Size = New System.Drawing.Size(246, 22)
        Me.ManuallyUpload_missing.Text = "Manually Upload Artwork"
        Me.ManuallyUpload_missing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'SelectCoverArtForDVDArt_missing
        '
        Me.SelectCoverArtForDVDArt_missing.Image = Global.DVDArt_Plugin.My.Resources.Resources.selectcoverart
        Me.SelectCoverArtForDVDArt_missing.Name = "SelectCoverArtForDVDArt_missing"
        Me.SelectCoverArtForDVDArt_missing.Size = New System.Drawing.Size(246, 22)
        Me.SelectCoverArtForDVDArt_missing.Text = "Select/Edit Cover Art for DVD Art"
        '
        'UseCoverArtForDVDArt_missing
        '
        Me.UseCoverArtForDVDArt_missing.Image = Global.DVDArt_Plugin.My.Resources.Resources.convert1
        Me.UseCoverArtForDVDArt_missing.Name = "UseCoverArtForDVDArt_missing"
        Me.UseCoverArtForDVDArt_missing.Size = New System.Drawing.Size(246, 22)
        Me.UseCoverArtForDVDArt_missing.Text = "Use Cover Art for DVD Art"
        '
        'RescanAll_missing
        '
        Me.RescanAll_missing.Image = Global.DVDArt_Plugin.My.Resources.Resources.rescan_movies
        Me.RescanAll_missing.Name = "RescanAll_missing"
        Me.RescanAll_missing.Size = New System.Drawing.Size(246, 22)
        Me.RescanAll_missing.Text = "Rescan ALL missing"
        Me.RescanAll_missing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ChangeMBID_missing
        '
        Me.ChangeMBID_missing.Image = Global.DVDArt_Plugin.My.Resources.Resources.musicbrainz_picard
        Me.ChangeMBID_missing.Name = "ChangeMBID_missing"
        Me.ChangeMBID_missing.Size = New System.Drawing.Size(246, 22)
        Me.ChangeMBID_missing.Text = "Change MBID"
        '
        'pb_person
        '
        Me.pb_person.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.pb_person.Location = New System.Drawing.Point(254, 8)
        Me.pb_person.Name = "pb_person"
        Me.pb_person.Size = New System.Drawing.Size(382, 599)
        Me.pb_person.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pb_person.TabIndex = 13
        Me.pb_person.TabStop = False
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Image = Global.DVDArt_Plugin.My.Resources.Resources.person_download
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(253, 22)
        Me.ToolStripMenuItem1.Text = "Refresh person image from online"
        Me.ToolStripMenuItem1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(253, 22)
        Me.ToolStripMenuItem2.Text = "Manually Upload person image"
        Me.ToolStripMenuItem2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(253, 22)
        Me.ToolStripMenuItem3.Text = "Delete person image"
        Me.ToolStripMenuItem3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Image = Global.DVDArt_Plugin.My.Resources.Resources.person_download
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(239, 22)
        Me.ToolStripMenuItem4.Text = "Search person image on-line"
        Me.ToolStripMenuItem4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripMenuItem6
        '
        Me.ToolStripMenuItem6.Image = Global.DVDArt_Plugin.My.Resources.Resources.upload
        Me.ToolStripMenuItem6.Name = "ToolStripMenuItem6"
        Me.ToolStripMenuItem6.Size = New System.Drawing.Size(239, 22)
        Me.ToolStripMenuItem6.Text = "Manually Upload person image"
        Me.ToolStripMenuItem6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Image = Global.DVDArt_Plugin.My.Resources.Resources.rescan_movies
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(239, 22)
        Me.ToolStripMenuItem5.Text = "Rescan ALL missing"
        Me.ToolStripMenuItem5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbFTV_Logo
        '
        Me.pbFTV_Logo.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.pbFTV_Logo.Location = New System.Drawing.Point(469, 649)
        Me.pbFTV_Logo.Name = "pbFTV_Logo"
        Me.pbFTV_Logo.Size = New System.Drawing.Size(258, 48)
        Me.pbFTV_Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFTV_Logo.TabIndex = 29
        Me.pbFTV_Logo.TabStop = False
        '
        'PictureBox14
        '
        Me.PictureBox14.Image = Global.DVDArt_Plugin.My.Resources.Resources.myfilms
        Me.PictureBox14.Location = New System.Drawing.Point(77, 644)
        Me.PictureBox14.Name = "PictureBox14"
        Me.PictureBox14.Size = New System.Drawing.Size(180, 60)
        Me.PictureBox14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox14.TabIndex = 34
        Me.PictureBox14.TabStop = False
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
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.DVDArt_Plugin.My.Resources.Resources.tvseries
        Me.PictureBox2.Location = New System.Drawing.Point(259, 644)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(208, 59)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 31
        Me.PictureBox2.TabStop = False
        '
        'pbFTV_Logo2
        '
        Me.pbFTV_Logo2.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.pbFTV_Logo2.Location = New System.Drawing.Point(469, 649)
        Me.pbFTV_Logo2.Name = "pbFTV_Logo2"
        Me.pbFTV_Logo2.Size = New System.Drawing.Size(258, 48)
        Me.pbFTV_Logo2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFTV_Logo2.TabIndex = 30
        Me.pbFTV_Logo2.TabStop = False
        '
        'pbAudioDB
        '
        Me.pbAudioDB.Image = Global.DVDArt_Plugin.My.Resources.Resources.theAudioDB
        Me.pbAudioDB.Location = New System.Drawing.Point(519, 681)
        Me.pbAudioDB.Name = "pbAudioDB"
        Me.pbAudioDB.Size = New System.Drawing.Size(80, 17)
        Me.pbAudioDB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pbAudioDB.TabIndex = 35
        Me.pbAudioDB.TabStop = False
        '
        'pbLastFM
        '
        Me.pbLastFM.Image = Global.DVDArt_Plugin.My.Resources.Resources.lastfm
        Me.pbLastFM.Location = New System.Drawing.Point(600, 683)
        Me.pbLastFM.Name = "pbLastFM"
        Me.pbLastFM.Size = New System.Drawing.Size(55, 14)
        Me.pbLastFM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbLastFM.TabIndex = 34
        Me.pbLastFM.TabStop = False
        '
        'PictureBox9
        '
        Me.PictureBox9.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox9.Image = Global.DVDArt_Plugin.My.Resources.Resources.music
        Me.PictureBox9.Location = New System.Drawing.Point(259, 644)
        Me.PictureBox9.Name = "PictureBox9"
        Me.PictureBox9.Size = New System.Drawing.Size(208, 59)
        Me.PictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox9.TabIndex = 33
        Me.PictureBox9.TabStop = False
        '
        'pbFTV_Logo3
        '
        Me.pbFTV_Logo3.Image = Global.DVDArt_Plugin.My.Resources.Resources.logo
        Me.pbFTV_Logo3.Location = New System.Drawing.Point(469, 649)
        Me.pbFTV_Logo3.Name = "pbFTV_Logo3"
        Me.pbFTV_Logo3.Size = New System.Drawing.Size(258, 48)
        Me.pbFTV_Logo3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbFTV_Logo3.TabIndex = 32
        Me.pbFTV_Logo3.TabStop = False
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
        'b_artist_deletelogo
        '
        Me.b_artist_deletelogo.Image = Global.DVDArt_Plugin.My.Resources.Resources.delete
        Me.b_artist_deletelogo.Location = New System.Drawing.Point(235, 7)
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
        'RestartImporterToolStripMenuItem
        '
        Me.RestartImporterToolStripMenuItem.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RestartImporterToolStripMenuItem.Image = Global.DVDArt_Plugin.My.Resources.Resources.restart
        Me.RestartImporterToolStripMenuItem.Name = "RestartImporterToolStripMenuItem"
        Me.RestartImporterToolStripMenuItem.Size = New System.Drawing.Size(171, 22)
        Me.RestartImporterToolStripMenuItem.Text = "Restart Importer"
        Me.RestartImporterToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'PictureBox5
        '
        Me.PictureBox5.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_banner
        Me.PictureBox5.Location = New System.Drawing.Point(286, 224)
        Me.PictureBox5.Name = "PictureBox5"
        Me.PictureBox5.Size = New System.Drawing.Size(200, 37)
        Me.PictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox5.TabIndex = 23
        Me.PictureBox5.TabStop = False
        '
        'PictureBox15
        '
        Me.PictureBox15.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_cover
        Me.PictureBox15.Location = New System.Drawing.Point(350, 327)
        Me.PictureBox15.Name = "PictureBox15"
        Me.PictureBox15.Size = New System.Drawing.Size(73, 100)
        Me.PictureBox15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox15.TabIndex = 20
        Me.PictureBox15.TabStop = False
        '
        'PictureBox12
        '
        Me.PictureBox12.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_backdrop
        Me.PictureBox12.Location = New System.Drawing.Point(336, 266)
        Me.PictureBox12.Name = "PictureBox12"
        Me.PictureBox12.Size = New System.Drawing.Size(100, 56)
        Me.PictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox12.TabIndex = 19
        Me.PictureBox12.TabStop = False
        '
        'pb2
        '
        Me.pb2.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_clearart
        Me.pb2.Location = New System.Drawing.Point(336, 119)
        Me.pb2.Name = "pb2"
        Me.pb2.Size = New System.Drawing.Size(100, 56)
        Me.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pb2.TabIndex = 17
        Me.pb2.TabStop = False
        '
        'pb1
        '
        Me.pb1.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_dvdart
        Me.pb1.Location = New System.Drawing.Point(336, 14)
        Me.pb1.Name = "pb1"
        Me.pb1.Size = New System.Drawing.Size(100, 100)
        Me.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb1.TabIndex = 15
        Me.pb1.TabStop = False
        '
        'pb3
        '
        Me.pb3.Image = Global.DVDArt_Plugin.My.Resources.Resources.armageddon_clearlogo
        Me.pb3.Location = New System.Drawing.Point(336, 180)
        Me.pb3.Name = "pb3"
        Me.pb3.Size = New System.Drawing.Size(100, 39)
        Me.pb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb3.TabIndex = 16
        Me.pb3.TabStop = False
        '
        'rb_t2
        '
        Me.rb_t2.Image = Global.DVDArt_Plugin.My.Resources.Resources.template_2
        Me.rb_t2.Location = New System.Drawing.Point(279, 116)
        Me.rb_t2.Name = "rb_t2"
        Me.rb_t2.Size = New System.Drawing.Size(222, 208)
        Me.rb_t2.TabIndex = 3
        Me.rb_t2.TabStop = True
        Me.rb_t2.UseVisualStyleBackColor = True
        '
        'rb_t1
        '
        Me.rb_t1.Image = Global.DVDArt_Plugin.My.Resources.Resources.template_1
        Me.rb_t1.Location = New System.Drawing.Point(24, 116)
        Me.rb_t1.Name = "rb_t1"
        Me.rb_t1.Size = New System.Drawing.Size(222, 208)
        Me.rb_t1.TabIndex = 2
        Me.rb_t1.TabStop = True
        Me.rb_t1.UseVisualStyleBackColor = True
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = Global.DVDArt_Plugin.My.Resources.Resources.grimm_clearart
        Me.PictureBox3.Location = New System.Drawing.Point(335, 164)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(100, 56)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox3.TabIndex = 17
        Me.PictureBox3.TabStop = False
        '
        'PictureBox4
        '
        Me.PictureBox4.Image = Global.DVDArt_Plugin.My.Resources.Resources.grimm_clearlogo
        Me.PictureBox4.Location = New System.Drawing.Point(335, 238)
        Me.PictureBox4.Name = "PictureBox4"
        Me.PictureBox4.Size = New System.Drawing.Size(100, 39)
        Me.PictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox4.TabIndex = 16
        Me.PictureBox4.TabStop = False
        '
        'PictureBox6
        '
        Me.PictureBox6.Image = Global.DVDArt_Plugin.My.Resources.Resources.queen_banner
        Me.PictureBox6.Location = New System.Drawing.Point(286, 232)
        Me.PictureBox6.Name = "PictureBox6"
        Me.PictureBox6.Size = New System.Drawing.Size(200, 37)
        Me.PictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox6.TabIndex = 17
        Me.PictureBox6.TabStop = False
        '
        'PictureBox8
        '
        Me.PictureBox8.Image = Global.DVDArt_Plugin.My.Resources.Resources.queen_cdart
        Me.PictureBox8.Location = New System.Drawing.Point(336, 121)
        Me.PictureBox8.Name = "PictureBox8"
        Me.PictureBox8.Size = New System.Drawing.Size(100, 100)
        Me.PictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox8.TabIndex = 15
        Me.PictureBox8.TabStop = False
        '
        'PictureBox7
        '
        Me.PictureBox7.Image = Global.DVDArt_Plugin.My.Resources.Resources.queen_clearlogo
        Me.PictureBox7.Location = New System.Drawing.Point(336, 280)
        Me.PictureBox7.Name = "PictureBox7"
        Me.PictureBox7.Size = New System.Drawing.Size(100, 39)
        Me.PictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox7.TabIndex = 16
        Me.PictureBox7.TabStop = False
        '
        'PictureBox10
        '
        Me.PictureBox10.Image = Global.DVDArt_Plugin.My.Resources.Resources.btn_donateCC_LG
        Me.PictureBox10.Location = New System.Drawing.Point(68, 55)
        Me.PictureBox10.Name = "PictureBox10"
        Me.PictureBox10.Size = New System.Drawing.Size(147, 47)
        Me.PictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PictureBox10.TabIndex = 7
        Me.PictureBox10.TabStop = False
        '
        'pb_donate
        '
        Me.pb_donate.Image = Global.DVDArt_Plugin.My.Resources.Resources.btn_donateCC_LG
        Me.pb_donate.Location = New System.Drawing.Point(68, 55)
        Me.pb_donate.Name = "pb_donate"
        Me.pb_donate.Size = New System.Drawing.Size(147, 47)
        Me.pb_donate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pb_donate.TabIndex = 7
        Me.pb_donate.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.DVDArt_Plugin.My.Resources.Resources.movies
        Me.PictureBox1.Location = New System.Drawing.Point(36, 31)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(128, 128)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 29
        Me.PictureBox1.TabStop = False
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
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(688, 762)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbc_main)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DVDArt_GUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DVDArt "
        Me.cms_found.ResumeLayout(False)
        Me.cms_import.ResumeLayout(False)
        Me.cms_missing.ResumeLayout(False)
        Me.tbc_main.ResumeLayout(False)
        Me.tp_MovingPictures.ResumeLayout(False)
        Me.tp_MovingPictures.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tp_movies.ResumeLayout(False)
        Me.tbc_movies.ResumeLayout(False)
        Me.tp_Movie_DVDArt.ResumeLayout(False)
        Me.tp_Movie_DVDArt.PerformLayout()
        Me.tp_Movie_ClearArt.ResumeLayout(False)
        Me.tp_Movie_ClearLogo.ResumeLayout(False)
        Me.tp_Movie_Banner.ResumeLayout(False)
        Me.tp_Movie_Backdrop.ResumeLayout(False)
        Me.tp_Movie_Cover.ResumeLayout(False)
        Me.tp_movies_missing.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.cms_person_found.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.cms_person_missing.ResumeLayout(False)
        Me.tp_TVSeries.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.tp_series.ResumeLayout(False)
        Me.tbc_series.ResumeLayout(False)
        Me.tp_Serie_ClearArt.ResumeLayout(False)
        Me.tp_Serie_ClearArt.PerformLayout()
        Me.tp_Serie_ClearLogo.ResumeLayout(False)
        Me.tp_series_missing.ResumeLayout(False)
        Me.tp_Music.ResumeLayout(False)
        Me.tp_Music.PerformLayout()
        Me.tbc_music.ResumeLayout(False)
        Me.tp_artists.ResumeLayout(False)
        Me.tbc_artist.ResumeLayout(False)
        Me.tp_artist_banner.ResumeLayout(False)
        Me.tp_artist_clearlogo.ResumeLayout(False)
        Me.tp_albums.ResumeLayout(False)
        Me.tbc_album.ResumeLayout(False)
        Me.tp_Music_CDArt.ResumeLayout(False)
        Me.tp_Music_CDArt.PerformLayout()
        Me.tp_artist_album_missing.ResumeLayout(False)
        Me.tp_Importer.ResumeLayout(False)
        Me.tp_Settings.ResumeLayout(False)
        Me.tbc_settings.ResumeLayout(False)
        Me.tp_pluginsettings.ResumeLayout(False)
        Me.tp_pluginsettings.PerformLayout()
        Me.gb4.ResumeLayout(False)
        Me.gb4.PerformLayout()
        Me.gb1.ResumeLayout(False)
        Me.gb1.PerformLayout()
        Me.pnl_background.ResumeLayout(False)
        Me.pnl_background.PerformLayout()
        CType(Me.nud_delay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_missing, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nud_scraping, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb3.ResumeLayout(False)
        Me.pnl_online.ResumeLayout(False)
        Me.pnl_online.PerformLayout()
        CType(Me.nud_downloads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tp_scrapersettings.ResumeLayout(False)
        Me.gb2.ResumeLayout(False)
        Me.gb2.PerformLayout()
        Me.tbc_scraper.ResumeLayout(False)
        Me.tp_sMovies.ResumeLayout(False)
        Me.tbc_movie_settings.ResumeLayout(False)
        Me.tp_movies_scraper.ResumeLayout(False)
        Me.tp_movies_scraper.PerformLayout()
        Me.tp_manual_dvdart.ResumeLayout(False)
        Me.tp_movies_path.ResumeLayout(False)
        Me.tp_movies_path.PerformLayout()
        Me.tp_movies_persons.ResumeLayout(False)
        Me.tp_movies_persons.PerformLayout()
        Me.tp_sSeries.ResumeLayout(False)
        Me.tbc_series_settings.ResumeLayout(False)
        Me.tp_series_scraper.ResumeLayout(False)
        Me.tp_series_scraper.PerformLayout()
        Me.tp_series_path.ResumeLayout(False)
        Me.tp_series_path.PerformLayout()
        Me.tp_sMusic.ResumeLayout(False)
        Me.tbc_music_settings.ResumeLayout(False)
        Me.tp_music_scraper.ResumeLayout(False)
        Me.tp_music_scraper.PerformLayout()
        Me.tp_music_path.ResumeLayout(False)
        Me.tp_music_path.PerformLayout()
        Me.tp_about.ResumeLayout(False)
        Me.tp_about.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox8.ResumeLayout(False)
        Me.GroupBox8.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pbthemoviedb, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movingpictures, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movie_dvdart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movie_clearart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movie_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movie_banner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movie_backdrop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_movie_cover, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_person, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFTV_Logo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox14, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_serie_clearart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_serie_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFTV_Logo2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbAudioDB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbLastFM, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox9, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFTV_Logo3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_artist_banner, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_artist_clearlogo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_album_cdart, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox5, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox15, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox12, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox8, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox7, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox10, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pb_donate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents SendtoImporter_missing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RescanAll_missing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents bw_import As System.ComponentModel.BackgroundWorker
    Friend WithEvents il_state As System.Windows.Forms.ImageList
    Friend WithEvents cms_import As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RestartImporterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents il_clearart As System.Windows.Forms.ImageList
    Friend WithEvents il_clearlogo As System.Windows.Forms.ImageList
    Friend WithEvents ManuallyUpload_missing As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents pbFTV_Logo2 As System.Windows.Forms.PictureBox
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
    Friend WithEvents UseCoverArtForDVDArt_missing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectCoverArtForDVDArtToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectCoverArtForDVDArt_missing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshArtworkFromOnline_found As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ManuallyUploadArtwork_found As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SelectCoverArtForDVDArt_found As System.Windows.Forms.ToolStripMenuItem
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
    Friend WithEvents pbFTV_Logo3 As System.Windows.Forms.PictureBox
    Friend WithEvents PictureBox9 As System.Windows.Forms.PictureBox
    Friend WithEvents MBID As System.Windows.Forms.ColumnHeader
    Friend WithEvents u_MBID As System.Windows.Forms.ColumnHeader
    Friend WithEvents cb_backgroundscraper As System.Windows.Forms.CheckBox
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
    Friend WithEvents pbLastFM As System.Windows.Forms.PictureBox
    Friend WithEvents b_album_delete As System.Windows.Forms.Button
    Friend WithEvents tbc_movie_settings As System.Windows.Forms.TabControl
    Friend WithEvents tp_movies_scraper As System.Windows.Forms.TabPage
    Friend WithEvents tp_manual_dvdart As System.Windows.Forms.TabPage
    Friend WithEvents rb_t2 As System.Windows.Forms.RadioButton
    Friend WithEvents rb_t1 As System.Windows.Forms.RadioButton
    Friend WithEvents il_banner As System.Windows.Forms.ImageList
    Friend WithEvents ChangeMBID_found As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeMBID_missing As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents u_Artist As System.Windows.Forms.ColumnHeader
    Friend WithEvents pbAudioDB As System.Windows.Forms.PictureBox
    Friend WithEvents tp_Movie_Backdrop As System.Windows.Forms.TabPage
    Friend WithEvents b_movie_deletebackdrop As System.Windows.Forms.Button
    Friend WithEvents pb_movie_backdrop As System.Windows.Forms.PictureBox
    Friend WithEvents lv_movie_backdrop As System.Windows.Forms.ListView
    Friend WithEvents PictureBox12 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_Backdrop_movies As System.Windows.Forms.CheckBox
    Friend WithEvents il_backdrop As System.Windows.Forms.ImageList
    Friend WithEvents pbthemoviedb As System.Windows.Forms.PictureBox
    Friend WithEvents m_Backdrop As System.Windows.Forms.ColumnHeader
    Friend WithEvents gb3 As System.Windows.Forms.GroupBox
    Friend WithEvents pnl_online As System.Windows.Forms.Panel
    Friend WithEvents b_import As System.Windows.Forms.Button
    Friend WithEvents cb_autoimport As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox14 As System.Windows.Forms.PictureBox
    Friend WithEvents tp_movies_path As System.Windows.Forms.TabPage
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents tb_movie_path As System.Windows.Forms.TextBox
    Friend WithEvents b_movie_path As System.Windows.Forms.Button
    Friend WithEvents tbc_series_settings As System.Windows.Forms.TabControl
    Friend WithEvents tp_series_scraper As System.Windows.Forms.TabPage
    Friend WithEvents tp_series_path As System.Windows.Forms.TabPage
    Friend WithEvents tb_series_path As System.Windows.Forms.TextBox
    Friend WithEvents b_series_path As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents tbc_music_settings As System.Windows.Forms.TabControl
    Friend WithEvents tp_music_scraper As System.Windows.Forms.TabPage
    Friend WithEvents tp_music_path As System.Windows.Forms.TabPage
    Friend WithEvents tb_music_path As System.Windows.Forms.TextBox
    Friend WithEvents b_music_path As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents pnl_background As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cb_delay As System.Windows.Forms.ComboBox
    Friend WithEvents nud_delay As System.Windows.Forms.NumericUpDown
    Friend WithEvents cb_missing As System.Windows.Forms.ComboBox
    Friend WithEvents nud_missing As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cb_scraping As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents nud_scraping As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents mtb_cpu As System.Windows.Forms.MaskedTextBox
    Friend WithEvents PictureBox15 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_Cover_movies As System.Windows.Forms.CheckBox
    Friend WithEvents il_cover As System.Windows.Forms.ImageList
    Friend WithEvents tp_Movie_Cover As System.Windows.Forms.TabPage
    Friend WithEvents b_movie_deletecover As System.Windows.Forms.Button
    Friend WithEvents lv_movie_cover As System.Windows.Forms.ListView
    Friend WithEvents pb_movie_cover As System.Windows.Forms.PictureBox
    Friend WithEvents m_Cover As System.Windows.Forms.ColumnHeader
    Friend WithEvents nud_downloads As System.Windows.Forms.NumericUpDown
    Friend WithEvents l_downloads As System.Windows.Forms.Label
    Friend WithEvents cb_downloads As System.Windows.Forms.CheckBox
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents pb_person As System.Windows.Forms.PictureBox
    Friend WithEvents lv_person As System.Windows.Forms.ListView
    Friend WithEvents c_person As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents cms_person_found As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents lv_persons_missing As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cms_person_missing As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem6 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tp_movies_persons As System.Windows.Forms.TabPage
    Friend WithEvents tb_person_path As System.Windows.Forms.TextBox
    Friend WithEvents b_person_path As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cb_persons As System.Windows.Forms.CheckBox
    Friend WithEvents cb_debug As System.Windows.Forms.CheckBox
    Friend WithEvents tbc_settings As System.Windows.Forms.TabControl
    Friend WithEvents tp_pluginsettings As System.Windows.Forms.TabPage
    Friend WithEvents tp_scrapersettings As System.Windows.Forms.TabPage
    Friend WithEvents gb4 As System.Windows.Forms.GroupBox
    Friend WithEvents tb_personalapikey As System.Windows.Forms.TextBox
    Friend WithEvents b_save1 As System.Windows.Forms.Button
    Friend WithEvents b_save2 As System.Windows.Forms.Button
    Friend WithEvents tp_about As System.Windows.Forms.TabPage
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel6 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel7 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel8 As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents PictureBox10 As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents pb_donate As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ll_wiki As System.Windows.Forms.LinkLabel
    Friend WithEvents ll_forum As System.Windows.Forms.LinkLabel
    Friend WithEvents ll_project As System.Windows.Forms.LinkLabel
    Friend WithEvents ll_developer As System.Windows.Forms.LinkLabel
    Friend WithEvents l_version As System.Windows.Forms.Label
    Friend WithEvents l_copyright As System.Windows.Forms.Label
    Friend WithEvents PictureBox5 As System.Windows.Forms.PictureBox
    Friend WithEvents cb_Banner_movies As System.Windows.Forms.CheckBox
    Friend WithEvents tp_Movie_Banner As System.Windows.Forms.TabPage
    Friend WithEvents b_movie_deletebanner As System.Windows.Forms.Button
    Friend WithEvents pb_movie_banner As System.Windows.Forms.PictureBox
    Friend WithEvents lv_movie_banner As System.Windows.Forms.ListView
    Friend WithEvents m_Banner As System.Windows.Forms.ColumnHeader

End Class
