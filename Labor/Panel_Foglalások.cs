using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Labor
{
    public struct HORDO
    {
        public string Termekkod;
        public string Sarzs;
        public string ID;
        public int? FoglalasSzama;
        public string GyartasiEv;
        public decimal Mennyiseg;
        public string Time;

        public HORDO( string _Termekkod,
                      string _Sarzs,
                      string _ID,
                      int? _FoglalasSzama,
                      string _GyartasiEv,
                      decimal _Mennyiseg,
                      string _Time )
        {
            Termekkod = _Termekkod;
            Sarzs = _Sarzs;
            ID = _ID;
            FoglalasSzama = _FoglalasSzama;
            GyartasiEv = _GyartasiEv;
            Mennyiseg = _Mennyiseg;
            Time = _Time;
        }

        public struct TABLEINDEXES
        {
            public const int Termekkod = 0;
            public const int Sarzs = 1;
            public const int ID = 2;
            public const int FoglalasSzama = 3;
            public const int GyartasiEv = 4;
        }

        public static void SetRow( DataRow _row, HORDO _hordó )
        {
            _row[ TABLEINDEXES.Termekkod ] = _hordó.Termekkod;
            _row[ TABLEINDEXES.Sarzs ] = _hordó.Sarzs;
            _row[ TABLEINDEXES.ID ] = _hordó.ID;
            _row[ TABLEINDEXES.FoglalasSzama ] = _hordó.FoglalasSzama == null ? -1 : _hordó.FoglalasSzama.Value;
            _row[ TABLEINDEXES.GyartasiEv ] = _hordó.GyartasiEv;
        }

        public static bool SameKeys( HORDO _1, HORDO _2 )
        {
            if ( _1.Termekkod == _2.Termekkod && _1.ID == _2.ID && _1.Sarzs == _2.Sarzs && _1.GyartasiEv == _2.GyartasiEv ) return true;
            return false;
        }

        public static bool SameKeys( HORDO _1, DataRow _row )
        {
            if ( _1.Termekkod == ( string )_row[ TABLEINDEXES.Termekkod ] && _1.ID == ( string )_row[ TABLEINDEXES.ID ] &&
                _1.Sarzs == ( string )_row[ TABLEINDEXES.Sarzs ] && _1.GyartasiEv == ( string )_row[ TABLEINDEXES.GyartasiEv ] ) return true;
            return false;
        }
    }

    public struct SARZS
    {
        public string Termekkod;
        public string Sarzs;
        public int Foglalt;
        public int Szabad;

        public SARZS( string _Termekkod,
                      string _Sarzs,
                      int _Foglalt,
                      int _Szabad )
        {
            Termekkod = _Termekkod;
            Sarzs = _Sarzs;
            Foglalt = _Foglalt;
            Szabad = _Szabad;
        }

        public struct TABLEINDEXES
        {
            public const int Termekkod = 0;
            public const int Sarzs = 1;
            public const int Foglalt = 2;
            public const int Szabad = 3;
        }

        public static void
        SetRow( DataRow _row, SARZS _sarzs )
        {
            _row[ TABLEINDEXES.Termekkod ] = _sarzs.Termekkod;
            _row[ TABLEINDEXES.Sarzs ] = _sarzs.Sarzs;
            _row[ TABLEINDEXES.Foglalt ] = _sarzs.Foglalt;
            _row[ TABLEINDEXES.Szabad ] = _sarzs.Szabad;
        }

        public static bool
        SameKeys( SARZS _1, SARZS _2 )
        {
            if ( _1.Termekkod == _2.Termekkod && _1.Sarzs == _2.Sarzs ) return true;
            return false;
        }

        public static bool
        SameKeys( SARZS _1, DataRow _row )
        {
            if ( _1.Termekkod == ( string )_row[ TABLEINDEXES.Termekkod ] && _1.Sarzs == ( string )_row[ TABLEINDEXES.Sarzs ] ) return true;
            return false;
        }
    }

    public struct FOGLALAS
    {
        public string Nev;
        public int ID;
        public int HordokSzama;
        public string Tipus;
        public string Keszito;
        public string Ido;

        public VIZSGALAP_SZURO? szűrő;

        public FOGLALAS( int _ID,
                         string _Nev,
                         int _HordokSzama,
                         string _Tipus,
                         string _Keszito,
                         string _Ido )
        {
            Nev = _Nev;
            ID = _ID;
            HordokSzama = _HordokSzama;
            Tipus = _Tipus;
            Keszito = _Keszito;
            Ido = _Ido;

            szűrő = null;
        }

        public FOGLALAS( int _ID,
                         string _Nev,
                         int _HordokSzama,
                         string _Tipus,
                         string _Keszito,
                         string _Ido,
                         VIZSGALAP_SZURO _szűrő )
        {
            Nev = _Nev;
            ID = _ID;
            HordokSzama = _HordokSzama;
            Tipus = _Tipus;
            Keszito = _Keszito;
            Ido = _Ido;

            szűrő = _szűrő;
        }

        public struct TABLEINDEXES
        {
            public const int ID = 0;
            public const int Nev = 1;
            public const int HordokSzama = 2;
            public const int Tipus = 3;
            public const int Keszito = 4;
            public const int Ido = 5;
        }

        public static void
        SetRow( DataRow _row, FOGLALAS _foglalás )
        {
            _row[ TABLEINDEXES.ID ] = _foglalás.ID;
            _row[ TABLEINDEXES.Nev ] = _foglalás.Nev;
            _row[ TABLEINDEXES.HordokSzama ] = _foglalás.HordokSzama;
            _row[ TABLEINDEXES.Tipus ] = _foglalás.Tipus;
            _row[ TABLEINDEXES.Keszito ] = _foglalás.Keszito;
            _row[ TABLEINDEXES.Ido ] = _foglalás.Ido;
        }

        public static bool
        SameKeys( FOGLALAS _1, FOGLALAS _2 )
        {
            if ( _1.ID == _2.ID ) return true;
            return false;
        }

        public static bool
        SameKeys( FOGLALAS _1, DataRow _row )
        {
            if ( _1.ID == ( int )_row[ TABLEINDEXES.ID ] ) return true;
            return false;
        }
    }

    public struct VIZSGALAP_SZURO
    {
        public struct ADATOK1
        {
            public string GyumolcsFajta;
            public string HordoTipus;
            public string Megrendelo;
            public string SzarmazasiOrszag;
            public string MuszakJele;
            public string ToltogepSzama;
            public string Termekkod;

            public ADATOK1( string _GyumolcsFajta,
                            string _HordoTipus,
                            string _Megrendelo,
                            string _SzarmazasiOrszag,
                            string _MuszakJele,
                            string _ToltogepSzama,
                            string _Termekkod )
            {
                GyumolcsFajta = _GyumolcsFajta;
                HordoTipus = _HordoTipus;
                Megrendelo = _Megrendelo;
                SzarmazasiOrszag = _SzarmazasiOrszag;
                MuszakJele = _MuszakJele;
                ToltogepSzama = _ToltogepSzama;
                Termekkod = _Termekkod;
            }
        }


        public struct ADATOK2
        {
            public MinMaxPair<string> Sarzs;
            public MinMaxPair<string> HordoID;

            public MinMaxPair<double?> Brix;
            public MinMaxPair<double?> Citromsav;
            public MinMaxPair<double?> Borkosav;
            public MinMaxPair<double?> Ph;
            public MinMaxPair<double?> Bostwick;

            public MinMaxPair<Int16?> Aszkorbinsav;
            public MinMaxPair<Int16?> NettoToltet;
            public MinMaxPair<Int16?> Hokezeles;
            public MinMaxPair<Int16?> CitromsavAd;
            public MinMaxPair<Int16?> SzitaAtmero;

            public ADATOK2( string _MinSarzs, string _MaxSarzs,
                            string _MinHordoID, string _MaxHordoID,
                            double? _MinBrix, double? _MaxBrix,
                            double? _MinCitromsav, double? _MaxCitromsav,
                            double? _MinBorkosav, double? _MaxBorkosav,
                            double? _MinPh, double? _MaxPh,
                            double? _MinBostwick, double? _MaxBostwick,
                            Int16? _MinAszkorbinsav, Int16? _MaxAszkorbinsav,
                            Int16? _MinNettoToltet, Int16? _MaxNettoToltet,
                            Int16? _MinHokezeles, Int16? _MaxHokezeles,
                            Int16? _MinSzitaAtmero, Int16? _MaxSzitaAtmero,
                            Int16? _MinCitromsavAd, Int16? _MaxCitromsavAd )
            {
                Sarzs = new MinMaxPair<string>( _MinSarzs, _MaxSarzs );
                HordoID = new MinMaxPair<string>( _MinHordoID, _MaxHordoID );

                Brix = new MinMaxPair<double?>( _MinBrix, _MaxBrix );
                Citromsav = new MinMaxPair<double?>( _MinCitromsav, _MaxCitromsav );
                Borkosav = new MinMaxPair<double?>( _MinBorkosav, _MaxBorkosav );
                Ph = new MinMaxPair<double?>( _MinPh, _MaxPh );
                Bostwick = new MinMaxPair<double?>( _MinBostwick, _MaxBostwick );

                Aszkorbinsav = new MinMaxPair<short?>( _MinAszkorbinsav, _MaxAszkorbinsav );
                NettoToltet = new MinMaxPair<short?>( _MinNettoToltet, _MaxNettoToltet );
                Hokezeles = new MinMaxPair<Int16?>( _MinHokezeles, _MaxHokezeles );
                SzitaAtmero = new MinMaxPair<Int16?>( _MinSzitaAtmero, _MaxSzitaAtmero );
                CitromsavAd = new MinMaxPair<Int16?>( _MinCitromsavAd, _MaxCitromsavAd );
            }

            public ADATOK2( MinMaxPair<string> _Sarzs,
                            MinMaxPair<string> _HordoID,
                            MinMaxPair<double?> _Brix,
                            MinMaxPair<double?> _Citromsav,
                            MinMaxPair<double?> _Borkosav,
                            MinMaxPair<double?> _Ph,
                            MinMaxPair<double?> _Bostwick,
                            MinMaxPair<Int16?> _Aszkorbinsav,
                            MinMaxPair<Int16?> _NettoToltet,
                            MinMaxPair<Int16?> _Hokezeles,
                            MinMaxPair<Int16?> _CitromsavAd,
                            MinMaxPair<Int16?> _SzitaAtmero )
            {
                Sarzs = _Sarzs;
                HordoID = _HordoID;

                Brix = _Brix;
                Citromsav = _Citromsav;
                Borkosav = _Borkosav;
                Ph = _Ph;
                Bostwick = _Bostwick;

                Aszkorbinsav = _Aszkorbinsav;
                NettoToltet = _NettoToltet;
                Hokezeles = _Hokezeles;
                CitromsavAd = _CitromsavAd;
                SzitaAtmero = _SzitaAtmero;
            }
        }

        public ADATOK1 adatok1;
        public ADATOK2 adatok2;

        public VIZSGALAP_SZURO( ADATOK1 _adatok1, ADATOK2 _adatok2 )
        {
            adatok1 = _adatok1;
            adatok2 = _adatok2;
        }
    }

    public struct IMPORT
    {
        public struct IMPORTHORDO
        {
            public string Sorszam;
            public string Termekkod;
            public string GyartasiEv;
            public string Hordoszam;

            public IMPORTHORDO( string _Sorszam,
                                string _Termekkod,
                                string _GyartasiEv,
                                string _Hordoszam )
            {
                Sorszam = _Sorszam;
                Termekkod = _Termekkod;
                GyartasiEv = _GyartasiEv;
                Hordoszam = _Hordoszam;
            }
        }
        public List<IMPORTHORDO> ImportHordok;
    }

    public sealed class Panel_Foglalások : Tokenized_Control<FOGLALAS>
    {
        #region Constructor
        public Panel_Foglalások( )
        {
            InitializeContent( );
            InitializeTokens( );

            KeyDown += Panel_Foglalások_KeyDown;
        }

        private void
        InitializeContent( )
        {
            table = new DataGridView
            {
                Dock = DockStyle.Left,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToAddRows = false,
                Width = (6 + 30 + 6 + 10 + 30 + 15)*8 + 3,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += FoglalasModositas;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource( );

            const int spacer = 16;

            Button btnHozzaadas = new Button( );
            btnHozzaadas.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnHozzaadas.Text = "Hozzáadás";
            btnHozzaadas.Size = new Size( 85, 32 );
            btnHozzaadas.Location = new Point( ClientRectangle.Width - btnHozzaadas.Size.Width - spacer, ClientRectangle.Height - btnHozzaadas.Size.Height - spacer );
            btnHozzaadas.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Hozzaadas ? true : false;
            btnHozzaadas.Click += FoglalasHozzaadas;

            Button btnFeltoltes = new Button( );
            btnFeltoltes.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnFeltoltes.Text = "Feltöltés";
            btnFeltoltes.Size = new Size( 85, 32 );
            btnFeltoltes.Location = new Point( ClientRectangle.Width - btnHozzaadas.Size.Width - spacer, btnHozzaadas.Location.Y - btnFeltoltes.Size.Height - spacer );
            btnFeltoltes.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Hozzaadas ? true : false;
            btnFeltoltes.Click += btnFeltoltes_Click;

            Button btnTorles = new Button( );
            btnTorles.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnTorles.Text = "Törlés";
            btnTorles.Size = new Size( 85, 32 );
            btnTorles.Location = new Point( btnHozzaadas.Location.X - btnTorles.Size.Width - spacer, btnHozzaadas.Location.Y );
            btnTorles.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Torles ? true : false;
            btnTorles.Click += FoglalasTorles;

            Button btnKereses = new Button( );
            btnKereses.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnKereses.Text = "Keresés";
            btnKereses.Size = new Size( 85, 32 );
            btnKereses.Location = new Point( btnTorles.Location.X, btnFeltoltes.Location.Y );
            btnKereses.Click += VizsgalatKereses;

            Controls.Add( table );
            Controls.Add( btnTorles );
            Controls.Add( btnHozzaadas );
            Controls.Add( btnFeltoltes );
            Controls.Add( btnKereses );
        }

        private DataTable
        CreateSource( )
        {
            data = new DataTable( );

            data.Columns.Add( new DataColumn( "Száma", Type.GetType( "System.Int32" ) ) );
            data.Columns.Add( new DataColumn( "Foglalás neve", Type.GetType( "System.String" ) ) );
            data.Columns.Add( new DataColumn( "Hordók", Type.GetType( "System.Int32" ) ) );
            data.Columns.Add( new DataColumn( "Típusa", Type.GetType( "System.String" ) ) );
            data.Columns.Add( new DataColumn( "Készítette", Type.GetType( "System.String" ) ) );
            data.Columns.Add( new DataColumn( "Foglalás ideje", Type.GetType( "System.String" ) ) );

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void
        SetRow( DataRow _row, FOGLALAS _foglalás ) { FOGLALAS.SetRow( _row, _foglalás ); }

        protected override bool
        SameKeys( FOGLALAS _1, FOGLALAS _2 ) { return FOGLALAS.SameKeys( _1, _2 ); }

        protected override bool
        SameKeys( FOGLALAS _1, DataRow _row ) { return FOGLALAS.SameKeys( _1, _row ); }

        protected override List<FOGLALAS> CurrentData( ) { return Program.database.Foglalások( ); }
        #endregion

        #region EventHandlers
        private void
        FoglalasHozzaadas( object _sender, EventArgs _event )
        {
            Foglalás_Hozzáadó foglalás_hozzáadó = new Foglalás_Hozzáadó( );
            foglalás_hozzáadó.ShowDialog( );

            Program.RefreshData( );
        }

        private void
        btnFeltoltes_Click( object _sender, EventArgs _event )
        {
            string data = null;
            OpenFileDialog file = new OpenFileDialog( );

            try
            {
                if ( !( Directory.Exists( Path.GetFullPath( "IMPORT" ) ) ) )
                {
                    Directory.CreateDirectory( "IMPORT" );
                }
                file.InitialDirectory = Settings.import_directory;
                if ( file.ShowDialog( ) == DialogResult.OK )
                {
                    StreamReader sr = new StreamReader( file.FileName );
                    data = sr.ReadToEnd( );
                }
                else return;
            }
            catch ( Exception _e )
            {
                MessageBox.Show( "Hiba a foglalás adatok beolvasása során, kérem ellenőrizze a file tartalmát!\n" + _e.Message,
                                "Hiba",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error );
                return;
            }

            IMPORT import = new IMPORT( );
            import.ImportHordok = new List<IMPORT.IMPORTHORDO>( );
            string[] splitted = data.Split( new[ ] { "\r\n" }, StringSplitOptions.None );

            try
            {
                for ( int i = 0 ; i < splitted.Length - 1 ; i++ )
                {
                    IMPORT.IMPORTHORDO hordo = new IMPORT.IMPORTHORDO( splitted[ i ].Substring( 0, 3 ),
                                                                        splitted[ i ].Substring( 8, 3 ),
                                                                        splitted[ i ].Substring( 14, 1 ),
                                                                        splitted[ i ].Substring( 15, 4 ) );
                    import.ImportHordok.Add( hordo );
                }
            }


            catch ( Exception _e )
            {
                MessageBox.Show( "Hiba a foglalás adatok szeparálása során, kérem ellenőrizze a file tartalmát!\n" + _e.Message,
                                "Hiba",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error );
                return;
            }

            List<string> hibák = Program.database.Foglalás_Feltöltés_Ellenőrzés( import );

            if ( hibák.Count != 0 )
            {
                if ( file.FileName.Length == 0 ) return;

                try
                {
                    StreamWriter sw = File.CreateText( file.FileName.Substring( 0, file.FileName.Length - 3 ) +
                                                                              "-hibalista.txt" );
                    foreach ( string item in hibák )
                        sw.WriteLine( item );

                    sw.Close( );
                }
                catch ( Exception _e )
                {
                    MessageBox.Show( "Hiba a hibalista kiírása során!\n" + _e.Message,
                                    "Hiba",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error );
                    return;
                }

                MessageBox.Show( "Hibalista készült. A hibalista file neve: " +
                                ( file.FileName.Substring( 0, file.FileName.Length - 3 ) +
                                "-hibalista.txt" ), "Hibalista",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information );
            }
            Foglalás_Feltöltés foglalás_feltöltés = new Foglalás_Feltöltés( file.FileName, import );
            foglalás_feltöltés.ShowDialog( );

            Program.RefreshData( );
        }

        private void
        FoglalasModositas( object _sender, EventArgs _event )
        {
            if ( table.SelectedRows.Count != 1 ) return;

            FOGLALAS foglalás = new FOGLALAS( ( int )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.ID ].Value,
                                             ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Nev ].Value,
                                             ( int )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.HordokSzama ].Value,
                                             ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Tipus ].Value,
                                             ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Keszito ].Value,
                                             ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Ido ].Value );

            Foglalás_Szerkesztő foglalás_szerkesztő = new Foglalás_Szerkesztő( foglalás );
            foglalás_szerkesztő.ShowDialog( this );

            Program.RefreshData( );
        }

        private void
        FoglalasTorles( object _sender, EventArgs _event )
        {
            if ( table.SelectedRows.Count == 1 ) { if ( MessageBox.Show( "Biztosan törli a kiválasztott foglalást?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) != DialogResult.Yes ) return; }
            else if ( table.SelectedRows.Count != 0 ) { if ( MessageBox.Show( "Biztosan törli a kiválasztott foglalást?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) != DialogResult.Yes ) return; }
            if ( !Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Torles ) return;

            foreach ( DataGridViewRow selected in table.SelectedRows )
            {
                FOGLALAS azonosító = new FOGLALAS( ( int )selected.Cells[ FOGLALAS.TABLEINDEXES.ID ].Value,
                                                    ( string )selected.Cells[ FOGLALAS.TABLEINDEXES.Nev ].Value,
                                                    ( int )selected.Cells[ FOGLALAS.TABLEINDEXES.HordokSzama ].Value,
                                                    ( string )selected.Cells[ FOGLALAS.TABLEINDEXES.Tipus ].Value,
                                                    ( string )selected.Cells[ FOGLALAS.TABLEINDEXES.Keszito ].Value,
                                                    ( string )selected.Cells[ FOGLALAS.TABLEINDEXES.Ido ].Value );

                if ( !Program.database.Foglalás_Törlés( azonosító ) )
                {
                    MessageBox.Show( "Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő foglalás?\nID: " + azonosító.ID + "\nNév: " + azonosító.Nev, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                Program.RefreshData( );
            }
        }

        private void
        VizsgalatKereses( object _sender, EventArgs _event )
        {
            Vizsgálat_Kereső vizsgálat_kereső = new Vizsgálat_Kereső( );
            vizsgálat_kereső.ShowDialog( this );

            Program.RefreshData( );
        }

        private void
        table_DataBindingComplete( object _sender, DataGridViewBindingCompleteEventArgs _event )
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[ FOGLALAS.TABLEINDEXES.ID ].Width = 6 * 8;
            table.Columns[ FOGLALAS.TABLEINDEXES.Nev ].Width = 30 * 8;
            table.Columns[ FOGLALAS.TABLEINDEXES.HordokSzama ].Width = 6 * 8;
            table.Columns[ FOGLALAS.TABLEINDEXES.Tipus ].Width = 10 * 8;
            table.Columns[ FOGLALAS.TABLEINDEXES.Keszito ].Width = 30 * 8;
            table.Columns[ FOGLALAS.TABLEINDEXES.Ido ].Width = 15 * 8;

            table.Sort(table.Columns[0], ListSortDirection.Descending);
        }

        private void
        Panel_Foglalások_KeyDown( object _sender, KeyEventArgs _event )
        {
            if ( _event.KeyCode == Keys.Enter ) FoglalasModositas( _sender, _event );
        }

        private void
        table_UserDeletingRow( object _sender, DataGridViewRowCancelEventArgs _event )
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            FoglalasTorles( _sender, _event );
        }
        #endregion

        public sealed class Foglalás_Hozzáadó : Form
        {
            TextBox txtFoglalasNev;
            Label lblFoglalasTipus;
            Label lblKeszitette;
            Label lblFoglalasIdeje;

            #region Constructor
            public Foglalás_Hozzáadó( )
            {
                InitializeForm( );
                InitializeContent( );
            }

            private void
            InitializeForm( )
            {
                Text = "Új Foglalás";
                ClientSize = new Size( 400, 250 - 64 );
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void
            InitializeContent( )
            {
                Label foglalás_neve = MainForm.createlabel( "Foglalás neve:", 8, 1 * 32, this );
                Label foglalás_típusa = MainForm.createlabel( "Foglalás típusa:", 8, 2 * 32, this );
                Label készítette = MainForm.createlabel( "Készítette:", 8, 3 * 32, this );
                Label foglalás_ideje = MainForm.createlabel( "Foglalás ideje:", 8, 4 * 32, this );

                txtFoglalasNev = MainForm.createtextbox( foglalás_neve.Location.X + 128, foglalás_neve.Location.Y, 30, 240, this );
                lblFoglalasTipus = MainForm.createlabel( "Keresés", txtFoglalasNev.Location.X, foglalás_típusa.Location.Y, this );
                lblKeszitette = MainForm.createlabel( Program.felhasználó.Value.Nev1, txtFoglalasNev.Location.X, készítette.Location.Y, this );
                lblFoglalasIdeje = MainForm.createlabel( DateTime.Now.ToString( ), txtFoglalasNev.Location.X, foglalás_ideje.Location.Y, this );

                Button btnRendben = new Button( );
                btnRendben.Text = "Rendben";
                btnRendben.Size = new Size( 96, 32 );
                btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16, ClientRectangle.Height - btnRendben.Size.Height - 16 );
                btnRendben.Click += btnRendben_Click;

                Controls.Add( btnRendben );
            }
            #endregion

            #region EventHandlers
            private void
            btnRendben_Click( object _sender, EventArgs _event )
            {
                if ( !Database.IsCorrectSQLText( txtFoglalasNev.Text ) ) { MessageBox.Show( "Nem megfelelő karakter a névben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }
                if ( !Database.IsCorrectSQLText( lblFoglalasTipus.Text ) ) { MessageBox.Show( "Nem megfelelő karakter a típusban!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                if ( !Program.database.Foglalás_Hozzáadás( new FOGLALAS( 0, txtFoglalasNev.Text, 0, lblFoglalasTipus.Text, lblKeszitette.Text, lblFoglalasIdeje.Text ) ) )
                { MessageBox.Show( "Adatbázis hiba!\nLehetséges, hogy létezik már ilyen foglalás?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); }

                Close( );
            }
            #endregion
        }

        public sealed class Foglalás_Szerkesztő : Tokenized_Form<HORDO>
        {
            private FOGLALAS foglalas;
            public TextBox txtFoglalasNev;

            #region Constructor
            public Foglalás_Szerkesztő( FOGLALAS _foglalás )
            {
                foglalas = _foglalás;

                InitializeForm( );
                InitializeContent( );
                InitializeData( );
                InitializeTokens( );
            }

            private void
            InitializeForm( )
            {
                Text = "Foglalás adatai";
                ClientSize = new Size( 430, 600 );
                MinimumSize = ClientSize;
                Location = new Point( 0 * ( 430 + 16 ), 0 );
                StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;

                Load += Foglalás_Szerkesztő_Load;
                FormClosing += Foglalás_Szerkesztő_FormClosing;
            }

            private void
            InitializeContent( )
            {
                table = new DataGridView( );
                table.Dock = DockStyle.None;
                table.RowHeadersVisible = false;
                table.AllowUserToResizeRows = false;
                table.AllowUserToResizeColumns = false;
                table.AllowUserToAddRows = false;
                table.Width = 430;
                table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                table.ReadOnly = true;
                table.UserDeletingRow += table_UserDeletingRow;
                table.DataSource = CreateSource( );
                table.Location = new Point( 0, 100 );
                table.Height = 400;

                //

                Button btnTorles = new Button( );
                btnTorles.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                btnTorles.Text = "Törlés";
                btnTorles.Size = new Size( 96, 32 );
                btnTorles.Location = new Point( ClientRectangle.Width - btnTorles.Size.Width - 16, ClientRectangle.Height - btnTorles.Size.Height - 2 * 16 );
                btnTorles.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Modositas ? true : false;
                btnTorles.Click += Hordó_Törlés;

                Button btnKereses = new Button( );
                btnKereses.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                btnKereses.Text = "Keresés";
                btnKereses.Size = new Size( 96, 32 );
                btnKereses.Location = new Point( btnTorles.Location.X - btnTorles.Width - 16, btnTorles.Location.Y );
                btnKereses.Click += Vizsgálat_Keresés;

                Label lblFoglalasNev = MainForm.createlabel( "Foglalás neve:", 10, 30, this );

                txtFoglalasNev = MainForm.createtextbox( lblFoglalasNev.Location.X + lblFoglalasNev.Width + 16, lblFoglalasNev.Location.Y, 30, 30 * 8, this );

                //

                Controls.Add( table );

                Controls.Add( btnTorles );
                Controls.Add( btnKereses );
                Controls.Add( lblFoglalasNev );
            }

            private void
            InitializeData( )
            {
                txtFoglalasNev.Text = foglalas.Nev;
            }

            private DataTable
            CreateSource( )
            {
                data = new DataTable( );

                data.Columns.Add( new DataColumn( "Termékkód", Type.GetType( "System.String" ) ) );
                data.Columns.Add( new DataColumn( "Sarzs", Type.GetType( "System.String" ) ) );
                data.Columns.Add( new DataColumn( "Hordó száma", Type.GetType( "System.String" ) ) );
                data.Columns.Add( new DataColumn( "Foglalás száma", Type.GetType( "System.String" ) ) );
                data.Columns.Add( new DataColumn( "Gyártási év", Type.GetType( "System.String" ) ) );

                return data;
            }
            #endregion

            #region Tokenizer
            protected override void SetRow( DataRow _row, HORDO _hordó ) { HORDO.SetRow( _row, _hordó ); }

            protected override bool SameKeys( HORDO _1, HORDO _2 ) { return HORDO.SameKeys( _1, _2 ); }

            protected override bool SameKeys( HORDO _1, DataRow _row ) { return HORDO.SameKeys( _1, _row ); }

            protected override List<HORDO> CurrentData( ) { return Program.database.Hordók( foglalas ); }
            #endregion

            #region EventHandlers
            private void
            Foglalás_Szerkesztő_Load( object _sender, EventArgs _event )
            {
                table.Columns[ HORDO.TABLEINDEXES.Termekkod ].Width = 430 / 4;
                table.Columns[ HORDO.TABLEINDEXES.Sarzs ].Width = 430 / 4;
                table.Columns[ HORDO.TABLEINDEXES.ID ].Width = 430 / 4;
                table.Columns[ HORDO.TABLEINDEXES.FoglalasSzama ].Visible = false;
                table.Columns[ HORDO.TABLEINDEXES.GyartasiEv ].Width = 430 / 4 - 1;
            }

            private void
            Foglalás_Szerkesztő_FormClosing( object _sender, FormClosingEventArgs _event )
            {
                if ( !Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Modositas ) return;

                if ( foglalas.Nev != txtFoglalasNev.Text )
                {
                    // SQL ellenőrzések
                    if ( !Database.IsCorrectSQLText( txtFoglalasNev.Text ) ) { MessageBox.Show( "Nem megfelelő karakter a névben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                    Program.database.Foglalás_Módosítás( foglalas, new FOGLALAS( foglalas.ID,
                                                                               txtFoglalasNev.Text,
                                                                               foglalas.HordokSzama,
                                                                               foglalas.Tipus,
                                                                               foglalas.Keszito,
                                                                               foglalas.Ido ) );
                }
            }

            private void
            Hordó_Törlés( object _sender, EventArgs _event )
            {
                if ( table.SelectedRows.Count != 1 ) return;
                if ( !Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Torles ) return;

                if ( !Program.database.Hordók_Foglalás( true,
                                                      foglalas.ID,
                                                      ( string )table.SelectedRows[ 0 ].Cells[ HORDO.TABLEINDEXES.Termekkod ].Value,
                                                      ( string )table.SelectedRows[ 0 ].Cells[ HORDO.TABLEINDEXES.Sarzs ].Value,
                                                      ( string )table.SelectedRows[ 0 ].Cells[ HORDO.TABLEINDEXES.ID ].Value ) )
                { MessageBox.Show( "Hiba a hordó lefoglalásakor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                Program.RefreshData( );
            }

            private void
            Vizsgálat_Keresés( object _sender, EventArgs _event )
            {
                foglalas.szűrő = Program.database.Foglalás_Vizsgálat_Szűrő( foglalas );
                if ( foglalas.szűrő == null ) { MessageBox.Show( "Hiba a foglalás szűrőjének lekérdezésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                Vizsgálat_Kereső vizsgálat_kereső = new Vizsgálat_Kereső( foglalas );
                vizsgálat_kereső.ShowDialog( this );

                Program.RefreshData( );
            }

            private void
            table_UserDeletingRow( object _sender, DataGridViewRowCancelEventArgs _event )
            {
                // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                _event.Cancel = true;
                // A saját törlést azért elindítjuk Delete gomb lenyomása után.
                Hordó_Törlés( _sender, _event );
            }
            #endregion
        }

        public sealed class Vizsgálat_Kereső : Form
        {
            private FOGLALAS? Eredeti;

            #region Declaration
            TextBox txtTermekkod;
            TextBox txtSarzsMin;
            TextBox txtHordoIDMin;
            TextBox txtBrixMin;
            TextBox txtCitromsavMin;
            TextBox txtBorkosavMin;
            TextBox txtPhMin;
            TextBox txtBostwickMin;
            TextBox txtAszkorbinsavMin;
            TextBox txtNettoToltetMin;
            TextBox txtHokezelesMin;
            TextBox txtSzitaAtmeroMin;
            TextBox txtCitromsavAdMin;
            TextBox txtSarzsMax;
            TextBox txtHordoIDMax;
            TextBox txtBrixMax;
            TextBox txtCitromsavMax;
            TextBox txtBorkosavMax;
            TextBox txtPhMax;
            TextBox txtBostwickMax;
            TextBox txtAszkorbinsavMax;
            TextBox txtNettoTolterMax;
            TextBox txtHokezelesMax;
            TextBox txtSzitaAtmeroMax;
            TextBox txtCitromsavAdMax;
            ComboBox cboGyumolcsfajta;
            ComboBox cboHordotipus;
            ComboBox cboMegrendelo;
            ComboBox cboSzarmazasiOrszag;
            TextBox txtMuszakJele;
            TextBox txtToltogepSzama;
            #endregion

            #region Constructor
            public Vizsgálat_Kereső( )
            {
                Text = "Vizsgálat keresés";
                InitializeForm( );
                InitializeContent( );
                InitializeData( );
            }

            public Vizsgálat_Kereső( FOGLALAS _eredeti )
            {
                Eredeti = _eredeti;

                Text = "Vizsgálat keresés " + _eredeti.Nev + " foglalás számára";
                InitializeForm( );
                InitializeContent( );
                InitializeData( );

                KeyDown += Vizsgálat_Kereső_KeyDown;
            }

            private void
            InitializeForm( )
            {
                ClientSize = new Size( 430, 600 );
                MinimumSize = ClientSize;
                Location = new Point( 1 * ( 430 + 16 ), 0 );
                StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void
            InitializeContent( )
            {
                int oszlop = 130;
                int sor = 30;

                #region Controls
                Label termékkód = MainForm.createlabel( "Termékkód:", 10, 10, this );
                Label sarzs = MainForm.createlabel( "Sarzs:", termékkód.Location.X, termékkód.Location.Y + sor, this );
                Label hordószám = MainForm.createlabel( "Hordószám:", termékkód.Location.X, sarzs.Location.Y + sor, this );
                Label brix = MainForm.createlabel( "Brix %:", termékkód.Location.X, hordószám.Location.Y + sor, this );
                Label citromsav = MainForm.createlabel( "Citromsav %:", termékkód.Location.X, brix.Location.Y + sor, this );
                Label borkősav = MainForm.createlabel( "Borkősav:", termékkód.Location.X, citromsav.Location.Y + sor, this );
                Label ph = MainForm.createlabel( "pH:", termékkód.Location.X, borkősav.Location.Y + sor, this );
                Label bostwick = MainForm.createlabel( "Bostwick cm/30sec, 20°C:", termékkód.Location.X, ph.Location.Y + sor, this );
                Label aszkorbinsav = MainForm.createlabel( "Aszkorbinsav mg/kg:", termékkód.Location.X, bostwick.Location.Y + sor, this );
                Label nettó_töltet = MainForm.createlabel( "Nettó töltet kg:", termékkód.Location.X, aszkorbinsav.Location.Y + sor, this );
                Label hőkezelés = MainForm.createlabel( "Hőkezelés °C:", termékkód.Location.X, nettó_töltet.Location.Y + sor, this );
                Label szita_átmérő = MainForm.createlabel( "Szita átmérő:", termékkód.Location.X, hőkezelés.Location.Y + sor, this );
                Label citromsav_ad = MainForm.createlabel( "Citromsav adagolás mg/kg:", termékkód.Location.X, szita_átmérő.Location.Y + sor, this );
                Label gyümölcsfajta = MainForm.createlabel( "Gyümölcsfajta", termékkód.Location.X, citromsav_ad.Location.Y + sor, this );
                Label hordótípus = MainForm.createlabel( "Hordótípus:", termékkód.Location.X, gyümölcsfajta.Location.Y + sor, this );
                Label megrendelő = MainForm.createlabel( "Megrendelők:", termékkód.Location.X, hordótípus.Location.Y + sor, this );
                Label származási_ország = MainForm.createlabel( "Származási ország:", termékkód.Location.X, megrendelő.Location.Y + sor, this );
                Label műszak_jele = MainForm.createlabel( "Műszak jele:", termékkód.Location.X, származási_ország.Location.Y + sor, this );
                Label töltőgép_száma = MainForm.createlabel( "Töltőgép száma:", termékkód.Location.X, műszak_jele.Location.Y + sor, this );
                #endregion

                #region Boxes
                txtTermekkod = MainForm.createtextbox( termékkód.Location.X + oszlop + 20, termékkód.Location.Y, 5, 70, this );
                txtTermekkod.Name = "box_termékkód";
                txtSarzsMin = MainForm.createtextbox( txtTermekkod.Location.X, sarzs.Location.Y, 5, 70, this );
                txtSarzsMax = MainForm.createtextbox( txtTermekkod.Location.X + oszlop, sarzs.Location.Y, 5, 70, this );
                txtHordoIDMin = MainForm.createtextbox( txtTermekkod.Location.X, hordószám.Location.Y, 5, 70, this );
                txtHordoIDMax = MainForm.createtextbox( txtSarzsMax.Location.X, hordószám.Location.Y, 5, 70, this );
                txtBrixMin = MainForm.createtextbox( txtTermekkod.Location.X, brix.Location.Y, 5, 70, this );
                txtBrixMax = MainForm.createtextbox( txtSarzsMax.Location.X, brix.Location.Y, 5, 70, this );
                txtCitromsavMin = MainForm.createtextbox( txtTermekkod.Location.X, citromsav.Location.Y, 7, 70, this );
                txtCitromsavMax = MainForm.createtextbox( txtSarzsMax.Location.X, citromsav.Location.Y, 7, 70, this );
                txtBorkosavMin = MainForm.createtextbox( txtTermekkod.Location.X, borkősav.Location.Y, 7, 70, this );
                txtBorkosavMax = MainForm.createtextbox( txtSarzsMax.Location.X, borkősav.Location.Y, 7, 70, this );
                txtPhMin = MainForm.createtextbox( txtTermekkod.Location.X, ph.Location.Y, 7, 70, this );
                txtPhMax = MainForm.createtextbox( txtSarzsMax.Location.X, ph.Location.Y, 7, 70, this );
                txtBostwickMin = MainForm.createtextbox( txtTermekkod.Location.X, bostwick.Location.Y, 5, 70, this );
                txtBostwickMax = MainForm.createtextbox( txtSarzsMax.Location.X, bostwick.Location.Y, 5, 70, this );
                txtAszkorbinsavMin = MainForm.createtextbox( txtTermekkod.Location.X, aszkorbinsav.Location.Y, 5, 70, this );
                txtAszkorbinsavMax = MainForm.createtextbox( txtSarzsMax.Location.X, aszkorbinsav.Location.Y, 5, 70, this );
                txtNettoToltetMin = MainForm.createtextbox( txtTermekkod.Location.X, nettó_töltet.Location.Y, 5, 70, this );
                txtNettoTolterMax = MainForm.createtextbox( txtSarzsMax.Location.X, nettó_töltet.Location.Y, 5, 70, this );
                txtHokezelesMin = MainForm.createtextbox( txtTermekkod.Location.X, hőkezelés.Location.Y, 5, 70, this );
                txtHokezelesMax = MainForm.createtextbox( txtSarzsMax.Location.X, hőkezelés.Location.Y, 5, 70, this );
                txtSzitaAtmeroMin = MainForm.createtextbox( txtTermekkod.Location.X, szita_átmérő.Location.Y, 5, 70, this );
                txtSzitaAtmeroMax = MainForm.createtextbox( txtSarzsMax.Location.X, szita_átmérő.Location.Y, 5, 70, this );
                txtCitromsavAdMin = MainForm.createtextbox( txtTermekkod.Location.X, citromsav_ad.Location.Y, 5, 70, this );
                txtCitromsavAdMax = MainForm.createtextbox( txtSarzsMax.Location.X, citromsav_ad.Location.Y, 5, 70, this );

                cboGyumolcsfajta = MainForm.createcombobox( txtTermekkod.Location.X, gyümölcsfajta.Location.Y, 200, this );
                cboHordotipus = MainForm.createcombobox( txtTermekkod.Location.X, hordótípus.Location.Y, 200, this );
                cboMegrendelo = MainForm.createcombobox( txtTermekkod.Location.X, megrendelő.Location.Y, 200, this );
                cboSzarmazasiOrszag = MainForm.createcombobox( txtTermekkod.Location.X, származási_ország.Location.Y, 200, this );
                txtMuszakJele = MainForm.createtextbox( txtTermekkod.Location.X, műszak_jele.Location.Y, 1, 20, this );
                txtMuszakJele.Name = "műszak";
                txtToltogepSzama = MainForm.createtextbox( txtTermekkod.Location.X, töltőgép_száma.Location.Y, 1, 20, this );
                txtToltogepSzama.Name = "töltő";
                #endregion

                #region Events
                txtBrixMin.KeyPress += MainForm.OnlyNumber;
                txtBrixMax.KeyPress += MainForm.OnlyNumber;
                txtCitromsavMin.KeyPress += MainForm.OnlyNumber;
                txtCitromsavMax.KeyPress += MainForm.OnlyNumber;
                txtPhMin.KeyPress += MainForm.OnlyNumber;
                txtPhMax.KeyPress += MainForm.OnlyNumber;
                txtBostwickMin.KeyPress += MainForm.OnlyNumber;
                txtBostwickMax.KeyPress += MainForm.OnlyNumber;
                txtAszkorbinsavMin.KeyPress += MainForm.OnlyNumber;
                txtAszkorbinsavMax.KeyPress += MainForm.OnlyNumber;
                txtCitromsavMin.KeyPress += MainForm.OnlyNumber;
                txtCitromsavMax.KeyPress += MainForm.OnlyNumber;
                txtTermekkod.Leave += txtTermekkod_Leave;
                #endregion

                List<TORZSADAT> seged = Program.database.Törzsadatok( "Hordótípus" );
                foreach ( TORZSADAT item in seged )
                {
                    cboHordotipus.Items.Add( item.Azonosito );
                }

                seged.Clear( );
                seged = Program.database.Törzsadatok( "Származási ország" );
                foreach ( TORZSADAT item in seged )
                {
                    cboSzarmazasiOrszag.Items.Add( item.Azonosito );
                }

                List<string> megrendelok = Program.database.Megrendelők( );
                foreach ( string item in megrendelok )
                {
                    cboMegrendelo.Items.Add( item );
                }


                Button btnRendben = new Button( );
                btnRendben.Text = "Szűrés";
                btnRendben.Size = new Size( 96, 32 );
                btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16, ClientRectangle.Height - btnRendben.Size.Height - 16 );
                btnRendben.Click += Kereses;

                Controls.Add( btnRendben );
            }

            private void
            InitializeData( )
            {
                if ( Eredeti != null )
                {
                    txtTermekkod.Text = Eredeti.Value.szűrő.Value.adatok1.Termekkod;
                    txtSarzsMin.Text = Eredeti.Value.szűrő.Value.adatok2.Sarzs.min;
                    txtSarzsMax.Text = Eredeti.Value.szűrő.Value.adatok2.Sarzs.max;
                    txtHordoIDMin.Text = Eredeti.Value.szűrő.Value.adatok2.HordoID.min;
                    txtHordoIDMax.Text = Eredeti.Value.szűrő.Value.adatok2.HordoID.max;
                    txtBrixMin.Text = Eredeti.Value.szűrő.Value.adatok2.Brix.min.ToString( );
                    txtBrixMax.Text = Eredeti.Value.szűrő.Value.adatok2.Brix.max.ToString( );
                    txtCitromsavMin.Text = Eredeti.Value.szűrő.Value.adatok2.Citromsav.min.ToString( );
                    txtCitromsavMax.Text = Eredeti.Value.szűrő.Value.adatok2.Citromsav.max.ToString( );
                    txtBorkosavMin.Text = Eredeti.Value.szűrő.Value.adatok2.Borkosav.min.ToString( );
                    txtBorkosavMax.Text = Eredeti.Value.szűrő.Value.adatok2.Borkosav.max.ToString( );
                    txtPhMin.Text = Eredeti.Value.szűrő.Value.adatok2.Ph.min.ToString( );
                    txtPhMax.Text = Eredeti.Value.szűrő.Value.adatok2.Ph.max.ToString( );
                    txtBostwickMin.Text = Eredeti.Value.szűrő.Value.adatok2.Bostwick.min.ToString( );
                    txtBostwickMax.Text = Eredeti.Value.szűrő.Value.adatok2.Bostwick.max.ToString( );
                    txtAszkorbinsavMin.Text = Eredeti.Value.szűrő.Value.adatok2.Aszkorbinsav.min.ToString( );
                    txtAszkorbinsavMax.Text = Eredeti.Value.szűrő.Value.adatok2.Aszkorbinsav.max.ToString( );
                    txtNettoToltetMin.Text = Eredeti.Value.szűrő.Value.adatok2.NettoToltet.min.ToString( );
                    txtNettoTolterMax.Text = Eredeti.Value.szűrő.Value.adatok2.NettoToltet.max.ToString( );
                    txtHokezelesMin.Text = Eredeti.Value.szűrő.Value.adatok2.Hokezeles.min.ToString( );
                    txtHokezelesMax.Text = Eredeti.Value.szűrő.Value.adatok2.Hokezeles.max.ToString( );
                    txtSzitaAtmeroMin.Text = Eredeti.Value.szűrő.Value.adatok2.SzitaAtmero.min.ToString( );
                    txtSzitaAtmeroMax.Text = Eredeti.Value.szűrő.Value.adatok2.SzitaAtmero.max.ToString( );
                    txtCitromsavAdMin.Text = Eredeti.Value.szűrő.Value.adatok2.CitromsavAd.min.ToString( );
                    txtCitromsavAdMax.Text = Eredeti.Value.szűrő.Value.adatok2.CitromsavAd.max.ToString( );

                    cboGyumolcsfajta.Text = Eredeti.Value.szűrő.Value.adatok1.GyumolcsFajta;
                    cboHordotipus.Text = Eredeti.Value.szűrő.Value.adatok1.HordoTipus;
                    cboSzarmazasiOrszag.Text = Eredeti.Value.szűrő.Value.adatok1.SzarmazasiOrszag;
                    txtToltogepSzama.Text = Eredeti.Value.szűrő.Value.adatok1.ToltogepSzama;
                }

            }
            #endregion

            #region EventHandlers
            public override void
            Refresh( )
            {
                base.Refresh( );

                if ( Owner != null ) Owner.Refresh( );
            }

            private void
            txtTermekkod_Leave( object _sender, EventArgs _event )
            {
                if ( cboGyumolcsfajta.Items.Count != 0 ) { cboGyumolcsfajta.Items.Clear( ); }

                if ( txtTermekkod.Text.Length != 3 ) { return; }
                List<string> gyümölcsfajták = Program.database.Gyümölcsfajták( txtTermekkod.Text );
                foreach ( string item in gyümölcsfajták ) { cboGyumolcsfajta.Items.Add( item ); }
            }

            private void
            Vizsgálat_Kereső_KeyDown( object _sender, KeyEventArgs _event )
            {
                if ( _event.KeyCode == Keys.Enter ) Kereses( _sender, _event );
            }

            private void
            Kereses( object _sender, EventArgs _event )
            {
                if ( txtSarzsMin.Text.Length != 0 && txtSarzsMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<int>( txtSarzsMin.Text ) > MainForm.ConvertOrDie<int>( txtSarzsMax.Text ) ) { MessageBox.Show( "Sarzs!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtBrixMin.Text.Length != 0 && txtBrixMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<double>( txtBrixMin.Text ) > MainForm.ConvertOrDie<double>( txtBrixMax.Text ) ) { MessageBox.Show( "brix!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtCitromsavMin.Text.Length != 0 && txtCitromsavMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<double>( txtCitromsavMin.Text ) > MainForm.ConvertOrDie<double>( txtCitromsavMax.Text ) ) { MessageBox.Show( "citromsav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtBorkosavMin.Text.Length != 0 && txtBorkosavMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<double>( txtBorkosavMin.Text ) > MainForm.ConvertOrDie<double>( txtBorkosavMax.Text ) ) { MessageBox.Show( "borkősav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtPhMin.Text.Length != 0 && txtPhMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<double>( txtPhMin.Text ) > MainForm.ConvertOrDie<double>( txtPhMax.Text ) ) { MessageBox.Show( "ph!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtBostwickMin.Text.Length != 0 && txtBostwickMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<double>( txtBostwickMin.Text ) > MainForm.ConvertOrDie<double>( txtBostwickMax.Text ) ) { MessageBox.Show( "bostwick!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtAszkorbinsavMin.Text.Length != 0 && txtAszkorbinsavMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<int>( txtAszkorbinsavMin.Text ) > MainForm.ConvertOrDie<int>( txtAszkorbinsavMax.Text ) ) { MessageBox.Show( "aszkorbinsav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtNettoToltetMin.Text.Length != 0 && txtNettoTolterMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<int>( txtNettoToltetMin.Text ) > MainForm.ConvertOrDie<int>( txtNettoTolterMax.Text ) ) { MessageBox.Show( "nettó_töltet!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtHokezelesMin.Text.Length != 0 && txtHokezelesMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<int>( txtHokezelesMin.Text ) > MainForm.ConvertOrDie<int>( txtHokezelesMax.Text ) ) { MessageBox.Show( "hőkezelés!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtCitromsavAdMin.Text.Length != 0 && txtCitromsavAdMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<int>( txtCitromsavAdMin.Text ) > MainForm.ConvertOrDie<int>( txtCitromsavAdMax.Text ) ) { MessageBox.Show( "citromsav_ad!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if ( txtSzitaAtmeroMin.Text.Length != 0 && txtSzitaAtmeroMax.Text.Length != 0 ) if ( MainForm.ConvertOrDie<int>( txtSzitaAtmeroMin.Text ) > MainForm.ConvertOrDie<int>( txtSzitaAtmeroMax.Text ) ) { MessageBox.Show( "szita_átmérő!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }

                VIZSGALAP_SZURO.ADATOK1 adatok1 = new VIZSGALAP_SZURO.ADATOK1(
                    MainForm.ConvertOrDieSQLString( cboGyumolcsfajta.Text ),
                    MainForm.ConvertOrDieSQLString( cboHordotipus.Text ),
                    MainForm.ConvertOrDieSQLString( cboMegrendelo.Text ),
                    MainForm.ConvertOrDieSQLString( cboSzarmazasiOrszag.Text ),
                    MainForm.ConvertOrDieSQLString( txtMuszakJele.Text ),
                    MainForm.ConvertOrDieSQLString( txtToltogepSzama.Text ),
                    MainForm.ConvertOrDieSQLString( txtTermekkod.Text ) );

                VIZSGALAP_SZURO.ADATOK2 adatok2 = new VIZSGALAP_SZURO.ADATOK2(
                    MainForm.ConvertOrDieSQLString( txtSarzsMin.Text ),
                    MainForm.ConvertOrDieSQLString( txtSarzsMax.Text ),
                    MainForm.ConvertOrDieSQLString( txtHordoIDMin.Text ),
                    MainForm.ConvertOrDieSQLString( txtHordoIDMax.Text ),
                    MainForm.ConvertOrDie<double>( txtBrixMin.Text ),
                    MainForm.ConvertOrDie<double>( txtBrixMax.Text ),
                    MainForm.ConvertOrDie<double>( txtCitromsavMin.Text ),
                    MainForm.ConvertOrDie<double>( txtCitromsavMax.Text ),
                    MainForm.ConvertOrDie<double>( txtBorkosavMin.Text ),
                    MainForm.ConvertOrDie<double>( txtBorkosavMax.Text ),
                    MainForm.ConvertOrDie<double>( txtPhMin.Text ),
                    MainForm.ConvertOrDie<double>( txtPhMax.Text ),
                    MainForm.ConvertOrDie<double>( txtBostwickMin.Text ),
                    MainForm.ConvertOrDie<double>( txtBostwickMax.Text ),
                    MainForm.ConvertOrDie<Int16>( txtAszkorbinsavMin.Text ),
                    MainForm.ConvertOrDie<Int16>( txtAszkorbinsavMax.Text ),
                    MainForm.ConvertOrDie<Int16>( txtNettoToltetMin.Text ),
                    MainForm.ConvertOrDie<Int16>( txtNettoTolterMax.Text ),
                    MainForm.ConvertOrDie<Int16>( txtHokezelesMin.Text ),
                    MainForm.ConvertOrDie<Int16>( txtHokezelesMax.Text ),
                    MainForm.ConvertOrDie<Int16>( txtCitromsavAdMin.Text ),
                    MainForm.ConvertOrDie<Int16>( txtCitromsavAdMax.Text ),
                    MainForm.ConvertOrDie<Int16>( txtSzitaAtmeroMin.Text ),
                    MainForm.ConvertOrDie<Int16>( txtSzitaAtmeroMax.Text ) );

                if ( Eredeti == null )
                {
                    Keresés_Eredmény keresés_eredmény = new Keresés_Eredmény( new VIZSGALAP_SZURO( adatok1, adatok2 ) );
                    keresés_eredmény.ShowDialog( this );
                }
                else
                {
                    if ( !Program.database.Foglalás_Vizsgálat_Szűrő_Hozzáadás( Eredeti.Value, new VIZSGALAP_SZURO( adatok1, adatok2 ) ) )
                    { MessageBox.Show( "Nem sikerült hozzáadni a foglaláshoz az aktuális vizsgálati lap szűrőt!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                    Keresés_Eredmény keresés_eredmény = new Keresés_Eredmény( new VIZSGALAP_SZURO( adatok1, adatok2 ), Eredeti.Value );
                    keresés_eredmény.ShowDialog( this );
                }
            }
            #endregion

            public sealed class Keresés_Eredmény : Tokenized_Form<SARZS>
            {
                private VIZSGALAP_SZURO Szuro;
                private FOGLALAS? Foglalas;

                #region Constructor
                public Keresés_Eredmény( VIZSGALAP_SZURO _szűrő )
                {
                    Szuro = _szűrő;

                    InitializeForm( );
                    InitializeContent( );
                    InitializeTokens( );
                }

                public Keresés_Eredmény( VIZSGALAP_SZURO _szűrő, FOGLALAS _foglalás )
                {
                    Szuro = _szűrő;
                    Foglalas = _foglalás;

                    InitializeForm( );
                    InitializeContent( );
                    InitializeTokens( );
                }

                private void
                InitializeForm( )
                {
                    Text = "Keresés eredménye";
                    ClientSize = new Size( 4 * 75 + 3, 600 );
                    MinimumSize = ClientSize;
                    Location = new Point( 2 * ( 430 + 16 ), 0 );
                    StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                    FormBorderStyle = FormBorderStyle.FixedToolWindow;
                }

                private void
                InitializeContent( )
                {
                    table = new DataGridView( );
                    table.RowHeadersVisible = false;
                    table.AllowUserToResizeRows = false;
                    table.AllowUserToResizeColumns = false;
                    table.AllowUserToAddRows = false;
                    table.Width = 4 * 75 + 3;
                    table.Height = 500;
                    table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    table.ReadOnly = true;
                    table.DataBindingComplete += table_DataBindingComplete;
                    table.CellDoubleClick += Sarzs_Módosítás;
                    table.UserDeletingRow += table_UserDeletingRow;
                    table.DataSource = CreateSource( );

                    Button btnRendben = new Button( );
                    btnRendben.Text = "Rendben";
                    btnRendben.Size = new Size( 96, 32 );
                    btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16, ClientRectangle.Height - btnRendben.Size.Height - 16 );
                    btnRendben.Click += btnRendben_Click;

                    Controls.Add( btnRendben );
                    Controls.Add( table );
                }

                private DataTable
                CreateSource( )
                {
                    data = new DataTable( );

                    data.Columns.Add( new DataColumn( "Termékkód", Type.GetType( "System.String" ) ) );
                    data.Columns.Add( new DataColumn( "Sarzs", Type.GetType( "System.String" ) ) );
                    data.Columns.Add( new DataColumn( "Foglalt", Type.GetType( "System.Int32" ) ) );
                    data.Columns.Add( new DataColumn( "Szabad", Type.GetType( "System.Int32" ) ) );

                    return data;
                }
                #endregion

                #region Tokenizer
                protected override void
                SetRow( DataRow _row, SARZS _sarzs ) { SARZS.SetRow( _row, _sarzs ); }

                protected override bool
                SameKeys( SARZS _1, SARZS _2 ) { return SARZS.SameKeys( _1, _2 ); }

                protected override bool
                SameKeys( SARZS _1, DataRow _row ) { return SARZS.SameKeys( _1, _row ); }

                protected override List<SARZS>
                CurrentData( ) { return Program.database.Sarzsok( Szuro ); }
                #endregion

                #region EventHandlers
                private void
                table_DataBindingComplete( object _sender, DataGridViewBindingCompleteEventArgs _event )
                {
                    table.DataBindingComplete -= table_DataBindingComplete;
                    table.Columns[ SARZS.TABLEINDEXES.Termekkod ].Width = 75;
                    table.Columns[ SARZS.TABLEINDEXES.Sarzs ].Width = 75;
                    table.Columns[ SARZS.TABLEINDEXES.Foglalt ].Width = 75;
                    table.Columns[ SARZS.TABLEINDEXES.Szabad ].Width = 75;
                }

                private void
                table_UserDeletingRow( object _sender, DataGridViewRowCancelEventArgs _event )
                {
                    _event.Cancel = true;
                }

                private void
                Sarzs_Módosítás( object _sender, DataGridViewCellEventArgs _event )
                {
                    if ( table.SelectedRows.Count != 1 ) return;

                    SARZS sarzs = new SARZS( ( string )table.SelectedRows[ 0 ].Cells[ SARZS.TABLEINDEXES.Termekkod ].Value, ( string )table.SelectedRows[ 0 ].Cells[ SARZS.TABLEINDEXES.Sarzs ].Value,
                        ( int )table.SelectedRows[ 0 ].Cells[ SARZS.TABLEINDEXES.Foglalt ].Value, ( int )table.SelectedRows[ 0 ].Cells[ SARZS.TABLEINDEXES.Szabad ].Value );

                    Eredmény_Hordók eredmény_hordók;
                    if ( Foglalas == null ) eredmény_hordók = new Eredmény_Hordók( Szuro, sarzs );
                    else eredmény_hordók = new Eredmény_Hordók( Szuro, sarzs, Foglalas.Value );
                    eredmény_hordók.ShowDialog( this );

                    Program.RefreshData( );
                }

                private void
                btnRendben_Click( object _sender, EventArgs _event )
                {
                    Close( );
                }
                #endregion

                public sealed class Eredmény_Hordók : Tokenized_Form<HORDO>
                {
                    private SARZS Sarzs;
                    private VIZSGALAP_SZURO Szuro;
                    private FOGLALAS? Foglalas;

                    private bool OsszesKijeloles;
                    private Label FoglaltHordok;

                    #region Constructor
                    public Eredmény_Hordók( VIZSGALAP_SZURO _Szuro, SARZS _Sarzs )
                    {
                        Szuro = _Szuro;
                        Sarzs = _Sarzs;

                        InitializeForm( );
                        InitializeContent( );
                        InitializeTokens( );
                    }

                    public Eredmény_Hordók( VIZSGALAP_SZURO _Szuro, SARZS _Sarzs, FOGLALAS _Foglalas )
                    {
                        Szuro = _Szuro;
                        Sarzs = _Sarzs;
                        Foglalas = _Foglalas;

                        InitializeForm( );
                        InitializeContent( );
                        InitializeTokens( );

                        Hordók_Számolás( );
                    }

                    private void
                    InitializeForm( )
                    {
                        Text = "Sarzs(" + Sarzs.Sarzs + ") hordói";
                        ClientSize = new Size( 4 * 75 + 3, 600 );
                        MinimumSize = ClientSize;
                        Location = new Point( 2 * ( 430 + 16 ), 0 );
                        StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                        FormBorderStyle = FormBorderStyle.FixedToolWindow;
                    }

                    private void
                    InitializeContent( )
                    {
                        table = new DataGridView( );
                        table.Dock = DockStyle.Top;
                        table.RowHeadersVisible = false;
                        table.AllowUserToResizeRows = false;
                        table.AllowUserToResizeColumns = false;
                        table.AllowUserToAddRows = false;
                        table.Width = 300 + 3;
                        table.Height = 500;
                        table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        table.DataBindingComplete += table_DataBindingComplete;
                        table.UserDeletingRow += table_UserDeletingRow;
                        table.CellValueChanged += table_CellValueChanged;
                        table.CellMouseUp += table_CellMouseUp;
                        table.DataSource = CreateSource( );

                        //

                        Button btnRendben = new Button( );
                        btnRendben.Text = "Rendben";
                        btnRendben.Size = new Size( 96, 32 );
                        btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16, ClientRectangle.Height - btnRendben.Size.Height - 16 );
                        btnRendben.Click += btnRendben_Click;

                        Controls.Add( btnRendben );
                        Controls.Add( table );


                        if ( Foglalas != null )
                        {
                            Button btnKijelolesValtas = new Button( );
                            btnKijelolesValtas.Text = "Összes Be/Ki";
                            btnKijelolesValtas.Size = new Size( 128, 32 );
                            btnKijelolesValtas.Location = new Point( ClientRectangle.Width - btnKijelolesValtas.Size.Width - btnRendben.Width - 32, ClientRectangle.Height - btnKijelolesValtas.Size.Height - 16 );
                            btnKijelolesValtas.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Modositas ? true : false;
                            btnKijelolesValtas.Click += btnKijelolesValtas_Click;

                            Label lblFoglaltHordo = new Label( );
                            lblFoglaltHordo.Text = "Foglalt hordó:";
                            lblFoglaltHordo.Location = new Point( 8, btnKijelolesValtas.Location.Y - 32 - 8 );

                            FoglaltHordok = new Label( );
                            FoglaltHordok.Text = "0 db";
                            FoglaltHordok.Location = new Point( lblFoglaltHordo.Location.X + lblFoglaltHordo.Size.Width, lblFoglaltHordo.Location.Y );

                            Label vonal = new Label( );
                            vonal.Location = new Point( 0, lblFoglaltHordo.Location.Y + 26 );
                            vonal.Height = 3;
                            vonal.Width = 1000;
                            vonal.BackColor = Color.Black;

                            Controls.Add( btnKijelolesValtas );
                            Controls.Add( lblFoglaltHordo );
                            Controls.Add( FoglaltHordok );
                            Controls.Add( vonal );
                        }
                    }

                    private DataTable
                    CreateSource( )
                    {
                        data = new DataTable( );

                        data.Columns.Add( new DataColumn( "Termékkód", Type.GetType( "System.String" ) ) );
                        data.Columns.Add( new DataColumn( "Hordószám", Type.GetType( "System.String" ) ) );
                        if ( Foglalas != null )
                        {
                            DataColumn column = new DataColumn( "Foglalva", Type.GetType( "System.Boolean" ) );
                            column.ReadOnly = Program.felhasználó.Value.Jogosultsagok.Value.Foglalasok.Modositas ? false : true;
                            data.Columns.Add( column );
                        }
                        else
                        {
                            DataColumn column = new DataColumn( "Foglalás száma", Type.GetType( "System.Int32" ) );
                            column.AllowDBNull = true;
                            data.Columns.Add( column );
                        }

                        return data;
                    }
                    #endregion

                    #region Tokenizer
                    protected override void SetRow( DataRow _row, HORDO _hordó )
                    {
                        if ( Foglalas != null )
                        {
                            _row[ 0 ] = _hordó.Termekkod;
                            _row[ 1 ] = _hordó.ID;
                            _row[ 2 ] = _hordó.FoglalasSzama == null ? false : true;
                        }
                        else
                        {
                            _row[ 0 ] = _hordó.Termekkod;
                            _row[ 1 ] = _hordó.ID;
                            if ( _hordó.FoglalasSzama == null ) _row[ 2 ] = DBNull.Value;
                            else _row[ 2 ] = _hordó.FoglalasSzama.Value;
                        }
                    }

                    protected override bool SameKeys( HORDO _1, HORDO _2 )
                    {
                        if ( _1.Termekkod == _2.Termekkod && _1.ID == _2.ID ) return true;
                        return false;
                    }

                    protected override bool SameKeys( HORDO _1, DataRow _row )
                    {
                        if ( _1.Termekkod == ( string )_row[ HORDO.TABLEINDEXES.Termekkod ] && _1.ID == ( string )_row[ 1 ] ) return true;
                        return false;
                    }

                    protected override List<HORDO> CurrentData( )
                    {
                        if ( Foglalas != null )
                            return Program.database.Hordók( Foglalas.Value, Sarzs );
                        return Program.database.Hordók( Sarzs );
                    }

                    public override void Refresh( )
                    {
                        base.Refresh( );

                        if ( Foglalas != null ) Hordók_Számolás( );
                    }
                    #endregion

                    #region EventHandlers
                    private void table_DataBindingComplete( object sender, DataGridViewBindingCompleteEventArgs e )
                    {
                        table.DataBindingComplete -= table_DataBindingComplete;

                        table.Columns[ 0 ].Width = 100;
                        table.Columns[ 0 ].ReadOnly = true;
                        table.Columns[ 1 ].Width = 100;
                        table.Columns[ 1 ].ReadOnly = true;
                        table.Columns[ 2 ].Width = 100;
                        //table.Columns[2].ReadOnly = (foglalás == null) ? true : false;
                    }

                    private void Hordók_Számolás( )
                    {
                        int count = 0;
                        OsszesKijeloles = true;

                        foreach ( DataRow row in data.Rows )
                        {
                            if ( ( bool )row[ 2 ] )
                            {
                                ++count;
                                OsszesKijeloles = false;
                            }
                        }

                        FoglaltHordok.Text = count + " db";
                    }

                    private void table_UserDeletingRow( object _sender, DataGridViewRowCancelEventArgs _event )
                    {
                        // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                        _event.Cancel = true;
                        // A saját törlést azért elindítjuk Delete gomb lenyomása után.
                    }

                    private void table_CellValueChanged( object _sender, DataGridViewCellEventArgs _event )
                    {
                        if ( Foglalas != null )
                        {
                            if ( _event.ColumnIndex == 2 && _event.RowIndex != -1 )
                            {
                                if ( !Program.database.Hordók_Foglalás( !( bool )table.Rows[ _event.RowIndex ].Cells[ _event.ColumnIndex ].Value,
                                    Foglalas.Value.ID, Sarzs.Termekkod, Sarzs.Sarzs, ( string )table.Rows[ _event.RowIndex ].Cells[ 1 ].Value ) )
                                { MessageBox.Show( "Hiba a hordó lefoglalásakor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                                Program.RefreshData( );
                            }
                        }
                    }

                    private void table_CellMouseUp( object _sender, DataGridViewCellMouseEventArgs _event )
                    {
                        // End of edition on each click on column of checkbox
                        if ( _event.ColumnIndex == 2 && _event.RowIndex != -1 )
                        {
                            table.EndEdit( );
                        }
                    }

                    private void btnKijelolesValtas_Click( object _sender, EventArgs _event )
                    {
                        List< Tuple <bool, string, string, string > > data = new List<Tuple<bool, string, string, string>>( );

                        foreach ( DataGridViewRow row in table.Rows )
                        {
                            if ( ( bool )row.Cells[ 2 ].Value )
                            {
                                if ( !OsszesKijeloles ) data.Add( new Tuple<bool, string, string, string>( true, Sarzs.Termekkod, Sarzs.Sarzs, ( string )row.Cells[ 1 ].Value ) );
                            }
                            else
                            {
                                if ( OsszesKijeloles ) data.Add( new Tuple<bool, string, string, string>( false, Sarzs.Termekkod, Sarzs.Sarzs, ( string )row.Cells[ 1 ].Value ) );
                            }
                        }

                        int modified = Program.database.Hordók_ListaFoglalás( Foglalas.Value.ID, data );
                        if ( modified != data.Count ) MessageBox.Show( "Hiba a kijelölés váltása során (Összes: " + data.Count + ", Módosított: " + modified, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );

                        Program.RefreshData( );
                    }

                    private void btnRendben_Click( object _sender, EventArgs _event )
                    {
                        Close( );
                    }
                    #endregion
                }
            }
        }

        public sealed class Foglalás_Feltöltés : Form
        {
            #region Declaration
            private IMPORT Import;
            private TextBox txtFoglalasNeve;
            private Label lblFoglalasTipus;
            private Label lblKeszitette;
            private Label lblFoglalasIdeje;
            #endregion

            #region Constructor
            public Foglalás_Feltöltés( string _filename, IMPORT _import )
            {
                Import = _import;

                InitializeForm( );
                InitializeContent( _filename );
            }

            private void
            InitializeForm( )
            {
                Text = "Foglalás Feltöltés";
                ClientSize = new Size( 400, 250 - 64 );
                MinimumSize = ClientSize;
                Location = new Point( 1 * ( 430 + 16 ), 0 );
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void
            InitializeContent( string _filename )
            {
                Label foglalás_neve = MainForm.createlabel( "Foglalás neve:", 8, 1 * 32, this );
                Label foglalás_típusa = MainForm.createlabel( "Foglalás típusa:", 8, 2 * 32, this );
                Label készítette = MainForm.createlabel( "Készítette:", 8, 3 * 32, this );
                Label foglalás_ideje = MainForm.createlabel( "Foglalás ideje:", 8, 4 * 32, this );

                txtFoglalasNeve = MainForm.createtextbox( foglalás_neve.Location.X + 128, foglalás_neve.Location.Y, 30, 240, this );
                lblFoglalasTipus = MainForm.createlabel( "Feltöltés", txtFoglalasNeve.Location.X, foglalás_típusa.Location.Y, this );
                lblKeszitette = MainForm.createlabel( Program.felhasználó.Value.Nev1, txtFoglalasNeve.Location.X, készítette.Location.Y, this );
                lblFoglalasIdeje = MainForm.createlabel( DateTime.Now.ToString( ), txtFoglalasNeve.Location.X, foglalás_ideje.Location.Y, this );

                Button btnRendben = new Button( );
                btnRendben.Text = "Rendben";
                btnRendben.Size = new Size( 96, 32 );
                btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16, ClientRectangle.Height - btnRendben.Size.Height - 16 );
                btnRendben.Click += btnRendben_Click;

                Controls.Add( btnRendben );
            }

            #region EventHandlers
            private void
            btnRendben_Click( object _sender, EventArgs _event )
            {
                // SQL ellenőrzések
                if ( !Database.IsCorrectSQLText( txtFoglalasNeve.Text ) ) { MessageBox.Show( "Nem megfelelő karakter a névben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                if ( !Program.database.Foglalás_Hozzáadás( new FOGLALAS( 0,
                                                                      txtFoglalasNeve.Text,
                                                                      0,
                                                                      lblFoglalasTipus.Text,
                                                                      lblKeszitette.Text,
                                                                      lblFoglalasIdeje.Text ) ) )
                { MessageBox.Show( "Adatbázis hiba!\nLehetséges, hogy létezik már ilyen foglalás?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); }

                List<FOGLALAS> foglalások = Program.database.Foglalások( );
                List<string> sarzsok = Program.database.Foglalás_Sarzsok( Import );

                bool found = false;
                foreach ( FOGLALAS outer in foglalások )
                {
                    if ( outer.Nev == txtFoglalasNeve.Text )
                    {
                        List< Tuple< bool, string, string, string > > data = new List<Tuple<bool, string, string, string>>( );
                        for ( int i = 0 ; i < Import.ImportHordok.Count ; i++ )
                        {
                            data.Add( new Tuple<bool, string, string, string>( false, Import.ImportHordok[ i ].Termekkod, sarzsok[ i ], Import.ImportHordok[ i ].Hordoszam ) );
                        }

                        int modified = Program.database.Hordók_ListaFoglalás( outer.ID, data );
                        if ( modified != data.Count ) MessageBox.Show( "Hiba a hordók lefoglalása során (Összes: " + data.Count + ", Módosított: " + modified, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );

                        found = true;
                        break;
                    }
                }

                if ( !found ) MessageBox.Show( "Nem található az adatbázisban ilyen foglalás!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );

                Program.RefreshData( );
                Close( );
            }
            #endregion

            #endregion
        }
    }
}
