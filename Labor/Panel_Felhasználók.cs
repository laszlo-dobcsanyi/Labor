using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct FELHASZNALO
    {
        public string Nev1;
        public string Nev2;
        public string Beosztas1;
        public string Beosztas2;
        public string FelhasznaloNev;
        public string Jelszo;

        public JOGOSULTSAGOK? Jogosultsagok;

        public FELHASZNALO( string _Nev1,
                           string _Nev2,
                           string _Beosztas1,
                           string _Beosztas2,
                           string _FelhasznaloNev,
                           string _Jelszo,
                           JOGOSULTSAGOK? _Jogosultsagok )
        {
            Nev1 = _Nev1;
            Nev2 = _Nev2;
            Beosztas1 = _Beosztas1;
            Beosztas2 = _Beosztas2;
            FelhasznaloNev = _FelhasznaloNev;
            Jelszo = _Jelszo;
            Jogosultsagok = _Jogosultsagok;
        }

        public struct TABLEINDEXES
        {
            public const int Nev1 = 0;
            public const int Beosztas1 = 1;
            public const int FelhasznaloNev = 2;
        }

        public struct JOGOSULTSAGOK
        {
            public struct Muveletek
            {
                public bool Hozzaadas;
                public bool Modositas;
                public bool Torles;

                public Muveletek( bool _Hozzaadas,
                                 bool _Modositas,
                                 bool _Torles )
                {
                    Hozzaadas = _Hozzaadas;
                    Modositas = _Modositas;
                    Torles = _Torles;
                }
            }

            public Muveletek Torzsadatok;
            public Muveletek Vizsgalatok;
            public Muveletek Foglalasok;
            public bool KonszignacioNyomtatas;
            public bool KiszallitasokTorlese;
            public Muveletek Felhasznalok;

            public JOGOSULTSAGOK( Muveletek _Torzsadatok,
                                 Muveletek _Vizsgalatok,
                                 Muveletek _Foglalasok,
                                 bool _KonszignacioNyomtatas,
                                 bool _KiszallitasokTorles,
                                 Muveletek _Felhasznalok )
            {
                Torzsadatok = _Torzsadatok;
                Vizsgalatok = _Vizsgalatok;
                Foglalasok = _Foglalasok;
                KonszignacioNyomtatas = _KonszignacioNyomtatas;
                KiszallitasokTorlese = _KiszallitasokTorles;
                Felhasznalok = _Felhasznalok;
            }
        }

        #region Tokenizer

        public static void
        SetRow( DataRow _row, FELHASZNALO _felhasználó )
        {
            _row[ TABLEINDEXES.Nev1 ] = _felhasználó.Nev1;
            _row[ TABLEINDEXES.Beosztas1 ] = _felhasználó.Beosztas1;
            _row[ TABLEINDEXES.FelhasznaloNev ] = _felhasználó.FelhasznaloNev;
        }

        public static bool
        SameKeys( FELHASZNALO _1, FELHASZNALO _2 )
        {
            return _1.FelhasznaloNev == _2.FelhasznaloNev;
        }

        public static bool
        SameKeys( FELHASZNALO _1, DataRow _row )
        {
            return _1.FelhasznaloNev == ( string )_row[ TABLEINDEXES.FelhasznaloNev ];
        }

        #endregion
    }

    public sealed class Panel_Felhasználók : Tokenized_Control<FELHASZNALO>
    {
        #region Constructor

        public Panel_Felhasználók( )
        {
            InitializeContent( );
            InitializeTokens( );
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
                Width = 75 * 8 + 3,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += Felhasználó_Módosítás;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource( );

            //

            Button btnTorles = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "Törlés",
                Size = new Size( 96, 32 ),
                Location = new Point( ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16 ),
                Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Felhasznalok.Torles ? true : false
            };
            btnTorles.Click += Felhasználó_Törlés;

            Button btnHozzaadas = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "Hozzáadás",
                Size = new Size( 96, 32 ),
                Location = new Point( btnTorles.Location.X + btnTorles.Width + 16,
                btnTorles.Location.Y ),
                Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Felhasznalok.Hozzaadas
            };
            btnHozzaadas.Click += Felhasználó_Hozzáadás;

            //

            Controls.Add( table );
            Controls.Add( btnTorles );
            Controls.Add( btnHozzaadas );
        }

        private DataTable
        CreateSource( )
        {
            data = new DataTable( );

            data.Columns.Add( new DataColumn( "Név", typeof( string ) ) );
            data.Columns.Add( new DataColumn( "Beosztás", typeof( string ) ) );
            data.Columns.Add( new DataColumn( "Felhasználónév", typeof( string ) ) );

            return data;
        }

        #endregion

        #region Tokenizer

        protected override void
        SetRow( DataRow _row, FELHASZNALO _felhasználó ) { FELHASZNALO.SetRow( _row, _felhasználó ); }

        protected override bool
        SameKeys( FELHASZNALO _1, FELHASZNALO _2 ) { return FELHASZNALO.SameKeys( _1, _2 ); }

        protected override bool
        SameKeys( FELHASZNALO _1, DataRow _row ) { return FELHASZNALO.SameKeys( _1, _row ); }

        protected override List<FELHASZNALO>
        CurrentData( ) { return Program.database.Felhasználók( ); }

        #endregion

        #region EventHandlers

        private static void
        Felhasználó_Hozzáadás( object _sender, EventArgs _event )
        {
            FelhasználóMegjelenítő hozzáadó = new FelhasználóMegjelenítő( );
            hozzáadó.ShowDialog( );
            Program.RefreshData( );
        }

        private void
        Felhasználó_Módosítás( object _sender, EventArgs _event )
        {
            if ( table.SelectedRows.Count != 1 ) return;
            if ( !Program.felhasználó.Value.Jogosultsagok.Value.Felhasznalok.Modositas ) return;

            FELHASZNALO? felhasználó = Program.database.Felhasználó( ( string )table.SelectedRows[ 0 ].Cells[ FELHASZNALO.TABLEINDEXES.FelhasznaloNev ].Value );
            if ( felhasználó == null )
            {
                MessageBox.Show( "Hiba a felhasználó lekérdezésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );
                return;
            }

            FelhasználóMegjelenítő módosító = new FelhasználóMegjelenítő( felhasználó.Value );
            módosító.ShowDialog( );

            Program.RefreshData( );
        }

        private void 
        Felhasználó_Törlés(object sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;
            if (!Program.felhasználó.Value.Jogosultsagok.Value.Felhasznalok.Torles) return;

            if (!Program.database.Felhasználó_Törlés((string) table.SelectedRows[0].Cells[FELHASZNALO.TABLEINDEXES.FelhasznaloNev].Value))
            {
                MessageBox.Show("Hiba a felhasználó törlésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Program.RefreshData();
        }

        private void
        table_DataBindingComplete( object sender, DataGridViewBindingCompleteEventArgs e )
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[ FELHASZNALO.TABLEINDEXES.Nev1 ].Width = 30 * 8;
            table.Columns[ FELHASZNALO.TABLEINDEXES.Beosztas1 ].Width = 30 * 8;
            table.Columns[ FELHASZNALO.TABLEINDEXES.FelhasznaloNev ].Width = 15 * 8;
        }

        private void
        table_UserDeletingRow( object _sender, DataGridViewRowCancelEventArgs _event )
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            Felhasználó_Törlés( _sender,
            _event );
        }

        #endregion

        public class FelhasználóMegjelenítő : Form
        {
            #region Declaration

            private FELHASZNALO? felhasznalo;

            private TextBox txtNev;
            private TextBox txtNev2;
            private TextBox txtBeosztas1;
            private TextBox txtBeosztas2;
            private TextBox txtFelhasznaloNev;
            private TextBox txtJelszo;

            private CheckBox chkTorzsadatUj;
            private CheckBox chkTorzsadatModositas;
            private CheckBox chkTorzsadatTorles;

            private CheckBox chkVizsgalatUj;
            private CheckBox chkVizsgalatModositas;
            private CheckBox chkVizsgalatTorles;

            private CheckBox chkFoglalasUj;
            private CheckBox chkFoglalasModositas;
            private CheckBox chkFoglalasTorles;

            private CheckBox chkKonszignacioNyomtat;

            private CheckBox chkKiszallitasokTorles;

            private CheckBox chkFelhasznalokUj;
            private CheckBox chkFelhasznalokModositas;
            private CheckBox chkFelhasznalokTorles;

            #endregion

            #region Constructor

            public FelhasználóMegjelenítő( )
            {
                InitializeForm( );
                InitializeContent( );
                InitializeData( );
            }

            public
            FelhasználóMegjelenítő( FELHASZNALO _Felhasználó )
            {
                felhasznalo = _Felhasználó;

                InitializeForm( );
                InitializeContent( );
                InitializeData( );
            }

            public void
            InitializeForm( )
            {
                ClientSize = new Size( 450, 600 + 64 );
                MaximumSize = ClientSize;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
                Text = felhasznalo == null ? "Új felhasználó" : felhasznalo.Value.Nev1;
                StartPosition = FormStartPosition.CenterScreen;
            }

            public void
            InitializeContent( )
            {
                const int offset = 16;
                const int spacer = 24;
                const int groupSpacer = 8;
                Tuple<string, int, int>[] labels = {
                    new Tuple<string, int, int>("Név", 2, 1),
                    new Tuple<string, int, int>("Beosztás", 2, 1),
                    new Tuple<string, int, int>("Belépési kód", 1, 0),
                    new Tuple<string, int, int>("Jelszó", 2, 1),
                    new Tuple<string, int, int>("Törzsadatok", 2, 1),
                    new Tuple<string, int, int>("Vizsgálatok", 2, 1),
                    new Tuple<string, int, int>("Foglalások", 2, 1),
                    new Tuple<string, int, int>("Konszignáció", 2, 1),
                    new Tuple<string, int, int>("Kiszállítások", 2, 1),
                    new Tuple<string, int, int>("Felhasználók", 2, 1)
                };

                int count = 0;
                int group = 0;

                for (int current = 0; current < labels.Length; ++current)
                {
                    Label label = MainForm.createlabel(labels[current].Item1 + ":",
                        offset,
                        count*spacer + group*groupSpacer + offset,
                        this);
                    count += labels[current].Item2;
                    group += labels[current].Item3;
                }


                const int column = 100;
                txtNev = MainForm.createtextbox( column,
                                                0 * spacer + 0 * groupSpacer + offset,
                                                30,
                                                30 * 8,
                                                this,
                                                CharacterCasing.Normal );

                txtNev2 = MainForm.createtextbox( column,
                                                1 * spacer + 0 * groupSpacer + offset,
                                                30,
                                                30 * 8,
                                                this,
                                                CharacterCasing.Normal );

                txtBeosztas1 = MainForm.createtextbox( column,
                                                        2 * spacer + 1 * groupSpacer + offset,
                                                        30,
                                                        30 * 8,
                                                        this,
                                                        CharacterCasing.Normal );
                txtBeosztas2 = MainForm.createtextbox( column,
                                                        3 * spacer + 1 * groupSpacer + offset,
                                                        30,
                                                        30 * 8,
                                                        this,
                                                        CharacterCasing.Normal );

                txtFelhasznaloNev = MainForm.createtextbox( column,
                                                            4 * spacer + 2 * groupSpacer + offset,
                                                            15,
                                                            15 * 8,
                                                            this,
                                                            CharacterCasing.Normal );
                txtJelszo = MainForm.createtextbox( column,
                                                    5 * spacer + 2 * groupSpacer + offset,
                                                    15,
                                                    15 * 8,
                                                    this,
                                                    CharacterCasing.Normal );
                txtJelszo.PasswordChar = '*';


                int[] columns = { 100, 245, 375 };

                group = 0;

                MainForm.createlabel( "Hozzáadás:",
                                        0 * 150 + 2 * offset,
                                        ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                        this );

                chkTorzsadatUj = MainForm.Create_CheckBox( columns[ 0 ],
                                                            ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                            this );
                MainForm.createlabel( "Módosítás:",
                                        1 * 150 + 2 * offset,
                                        ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                        this );
                chkTorzsadatModositas = MainForm.Create_CheckBox( columns[ 1 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                MainForm.createlabel( "Törlés:",
                                    2 * 150 + 2 * offset,
                                    ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                    this );
                chkTorzsadatTorles = MainForm.Create_CheckBox( columns[ 2 ],
                                                            ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                            this );

                ++group;
                MainForm.createlabel( "Hozzáadás:",
                                    0 * 150 + 2 * offset,
                                    ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                    this );
                chkVizsgalatUj = MainForm.Create_CheckBox( columns[ 0 ],
                                                            ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                            this );
                MainForm.createlabel( "Módosítás:",
                                                1 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkVizsgalatModositas = MainForm.Create_CheckBox( columns[ 1 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                MainForm.createlabel( "Törlés:",
                                                2 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkVizsgalatTorles = MainForm.Create_CheckBox( columns[ 2 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );

                ++group;
                MainForm.createlabel( "Hozzáadás:",
                                    0 * 150 + 2 * offset,
                                    ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                    this );
                chkFoglalasUj = MainForm.Create_CheckBox( columns[ 0 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );

                MainForm.createlabel( "Módosítás:",
                1 * 150 + 2 * offset,
                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                this );
                chkFoglalasModositas = MainForm.Create_CheckBox( columns[ 1 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                MainForm.createlabel( "Törlés:",
                                                2 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkFoglalasTorles = MainForm.Create_CheckBox( columns[ 2 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );

                ++group;
                MainForm.createlabel( "Nyomtatás:",
                                                0 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkKonszignacioNyomtat = MainForm.Create_CheckBox( columns[ 0 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );

                ++group;
                MainForm.createlabel( "Törlés:",
                                                0 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkKiszallitasokTorles = MainForm.Create_CheckBox( columns[ 0 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );

                ++group;
                MainForm.createlabel( "Hozzáadás:",
                                                0 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkFelhasznalokUj = MainForm.Create_CheckBox( columns[ 0 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                MainForm.createlabel( "Módosítás:",
                                                1 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkFelhasznalokModositas = MainForm.Create_CheckBox( columns[ 1 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                MainForm.createlabel( "Törlés:",
                                                2 * 150 + 2 * offset,
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );
                chkFelhasznalokTorles = MainForm.Create_CheckBox( columns[ 2 ],
                                                ( 8 + 2 * group ) * spacer + ( 3 + group ) * groupSpacer + offset,
                                                this );



                Button btnRendben = new Button();
                btnRendben.Size = new Size(96, 32);

                btnRendben.Location = new Point( ClientSize.Width - btnRendben.Width - spacer,
                                                ClientSize.Height - btnRendben.Height - spacer );
                btnRendben.Click += btnRendben_Click;
                btnRendben.Text = "Rendben";

                Controls.Add( btnRendben );
            }

            public void InitializeData( )
            {
                if ( felhasznalo != null )
                {
                    txtNev.Text = felhasznalo.Value.Nev1;
                    txtNev2.Text = felhasznalo.Value.Nev2;

                    txtBeosztas1.Text = felhasznalo.Value.Beosztas1;
                    txtBeosztas2.Text = felhasznalo.Value.Beosztas2;

                    txtFelhasznaloNev.Text = felhasznalo.Value.FelhasznaloNev;
                    txtFelhasznaloNev.Enabled = false;
                    txtJelszo.Text = felhasznalo.Value.Jelszo;

                    chkTorzsadatUj.CheckState = felhasznalo.Value.Jogosultsagok.Value.Torzsadatok.Hozzaadas ? CheckState.Checked : CheckState.Unchecked;
                    chkTorzsadatModositas.CheckState = felhasznalo.Value.Jogosultsagok.Value.Torzsadatok.Modositas ? CheckState.Checked : CheckState.Unchecked;
                    chkTorzsadatTorles.CheckState = felhasznalo.Value.Jogosultsagok.Value.Torzsadatok.Torles ? CheckState.Checked : CheckState.Unchecked;

                    chkVizsgalatUj.CheckState = felhasznalo.Value.Jogosultsagok.Value.Vizsgalatok.Hozzaadas ? CheckState.Checked : CheckState.Unchecked;
                    chkVizsgalatModositas.CheckState = felhasznalo.Value.Jogosultsagok.Value.Vizsgalatok.Modositas ? CheckState.Checked : CheckState.Unchecked;
                    chkVizsgalatTorles.CheckState = felhasznalo.Value.Jogosultsagok.Value.Vizsgalatok.Torles ? CheckState.Checked : CheckState.Unchecked;

                    chkFoglalasUj.CheckState = felhasznalo.Value.Jogosultsagok.Value.Foglalasok.Hozzaadas ? CheckState.Checked : CheckState.Unchecked;
                    chkFoglalasModositas.CheckState = felhasznalo.Value.Jogosultsagok.Value.Foglalasok.Modositas ? CheckState.Checked : CheckState.Unchecked;
                    chkFoglalasTorles.CheckState = felhasznalo.Value.Jogosultsagok.Value.Foglalasok.Torles ? CheckState.Checked : CheckState.Unchecked;

                    chkKonszignacioNyomtat.CheckState = felhasznalo.Value.Jogosultsagok.Value.KonszignacioNyomtatas ? CheckState.Checked : CheckState.Unchecked;

                    chkKiszallitasokTorles.CheckState = felhasznalo.Value.Jogosultsagok.Value.KiszallitasokTorlese ? CheckState.Checked : CheckState.Unchecked;

                    chkFelhasznalokUj.CheckState = felhasznalo.Value.Jogosultsagok.Value.Felhasznalok.Hozzaadas ? CheckState.Checked : CheckState.Unchecked;
                    chkFelhasznalokModositas.CheckState = felhasznalo.Value.Jogosultsagok.Value.Felhasznalok.Modositas ? CheckState.Checked : CheckState.Unchecked;
                    chkFelhasznalokTorles.CheckState = felhasznalo.Value.Jogosultsagok.Value.Felhasznalok.Torles ? CheckState.Checked : CheckState.Unchecked;
                }
            }

            #endregion

            #region EventHandlers

            private static bool
            Checked( CheckBox _checkbox )
            {
                return _checkbox.CheckState == CheckState.Checked;
            }

            private void
            btnRendben_Click( object _sender, EventArgs _event )
            {
                if ( !Database.IsCorrectSQLText( txtNev.Text ) )
                {
                    MessageBox.Show( "Nem megengedett karakter a név1 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                if ( !Database.IsCorrectSQLText( txtNev2.Text ) )
                {
                    MessageBox.Show( "Nem megengedett karakter a név2 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                if ( !Database.IsCorrectSQLText( txtBeosztas1.Text ) )
                {
                    MessageBox.Show( "Nem megengedett karakter a beosztás1 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                if ( !Database.IsCorrectSQLText( txtBeosztas2.Text ) )
                {
                    MessageBox.Show( "Nem megengedett karakter a beosztás2 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                if ( !Database.IsCorrectSQLText( txtFelhasznaloNev.Text ) )
                {
                    MessageBox.Show( "Nem megengedett karakter a felhasználó név mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                if ( !Database.IsCorrectSQLText( txtJelszo.Text ) )
                {
                    MessageBox.Show( "Nem megengedett karakter a jelszó mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }

                FELHASZNALO.JOGOSULTSAGOK.Muveletek törzsadat = new FELHASZNALO.JOGOSULTSAGOK.Muveletek( Checked( chkTorzsadatUj ),
                                                                                                        Checked( chkTorzsadatModositas ),
                                                                                                        Checked( chkTorzsadatTorles ) );
                FELHASZNALO.JOGOSULTSAGOK.Muveletek vizsgálat = new FELHASZNALO.JOGOSULTSAGOK.Muveletek( Checked( chkVizsgalatUj ),
                                                                                                        Checked( chkVizsgalatModositas ),
                                                                                                        Checked( chkVizsgalatTorles ) );
                FELHASZNALO.JOGOSULTSAGOK.Muveletek foglalás = new FELHASZNALO.JOGOSULTSAGOK.Muveletek( Checked( chkFoglalasUj ),
                                                                                                        Checked( chkFoglalasModositas ),
                                                                                                        Checked( chkFoglalasTorles ) );
                FELHASZNALO.JOGOSULTSAGOK.Muveletek felhasználók = new FELHASZNALO.JOGOSULTSAGOK.Muveletek( Checked( chkFelhasznalokUj ),
                                                                                                            Checked( chkFelhasznalokModositas ),
                                                                                                            Checked( chkFelhasznalokTorles ) );
                FELHASZNALO.JOGOSULTSAGOK jogosultságok = new FELHASZNALO.JOGOSULTSAGOK( törzsadat,
                                                                                        vizsgálat,
                                                                                        foglalás,
                                                                                        Checked( chkKonszignacioNyomtat ),
                                                                                        Checked( chkKiszallitasokTorles ),
                                                                                        felhasználók );
                FELHASZNALO felhasználó_adatok = new FELHASZNALO( txtNev.Text,
                                                                txtNev2.Text,
                                                                txtBeosztas1.Text,
                                                                txtBeosztas2.Text,
                                                                txtFelhasznaloNev.Text,
                                                                txtJelszo.Text,
                                                                jogosultságok );

                if ( felhasznalo == null )
                {
                    if ( !Program.database.Felhasználó_Hozzáadás( felhasználó_adatok ) )
                    {
                        MessageBox.Show( "Hiba a felhasználó hozzáadása során!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );
                        return;
                    }
                    Program.RefreshData( );
                }
                else
                {
                    if ( !Program.database.Felhasználó_Módosítás( felhasznalo.Value.FelhasznaloNev, felhasználó_adatok ) )
                    {
                        MessageBox.Show( "Hiba a felhasználó módosítása során!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error );
                        return;
                    }
                    Program.RefreshData( );
                }

                Close( );
            }
            #endregion
        }
    }
}
