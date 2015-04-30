using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct TORZSADAT
    {
        public string Tipus;
        /// <summary>Magyar név</summary>
        public string Azonosito;
        /// <summary>Angol név</summary>
        public string Megnevezes;
        ///<summary>Német név</summary>
        public string Megnevezes2;

        public TORZSADAT( string _Tipus,
                         string _Azonosito,
                         string _Megnevezes,
                         string _Megnevezes2 )
        {
            Tipus = _Tipus;
            Azonosito = _Azonosito;
            Megnevezes = _Megnevezes;
            Megnevezes2 = _Megnevezes2;
        }

        #region Tokenizer
        public static void
        SetRow( DataRow _row, TORZSADAT _törzsadat )
        {
            _row[ 0 ] = _törzsadat.Azonosito;
            _row[ 1 ] = _törzsadat.Megnevezes;
            _row[ 2 ] = _törzsadat.Megnevezes2;
        }

        public static bool
        SameKeys( TORZSADAT _1, TORZSADAT _2 )
        {
            if ( _1.Azonosito == _2.Azonosito ) return true;
            return false;
        }

        public static bool
        SameKeys( TORZSADAT _1, DataRow _row )
        {
            if ( _1.Azonosito == ( string )_row[ 0 ] ) return true;
            return false;
        }
        #endregion
    }

    public sealed class Panel_Törzsadatok : Tokenized_Control<TORZSADAT>
    {
        private ComboBox cboTorzsadat;

        #region Constructor
        public Panel_Törzsadatok( )
        {
            InitializeContent( );
            InitializeTokens( );

            KeyDown += Panel_Törzsadatok_KeyDown;
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
                Width = 600 + 3,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true
            };
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += TorzsadatModositas;

            Label lblTorzsadat = new Label
            {
                Text = "Törzsadat típusa:",
                Location = new Point(table.Width + 50, 15),
                AutoSize = true
            };

            cboTorzsadat = new ComboBox
            {
                Location = new Point(lblTorzsadat.Location.X + 100, lblTorzsadat.Location.Y),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            List<string> típusok = Program.database.Törzsadat_Típusok( );
            foreach ( string item in típusok )
            {
                cboTorzsadat.Items.Add( item );
            }
            cboTorzsadat.SelectedIndex = 0;
            cboTorzsadat.SelectedIndexChanged += cboTorzsadat_SelectedIndexChanged;

            table.DataSource = CreateSource( );

            Button btnTorles = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "Törlés",
                Size = new Size(96, 32),
                Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16),
                Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Torzsadatok.Torles
            };
            btnTorles.Click += btnTorles_Click;

            Button btnHozzaadas = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "Hozzáadás",
                Size = new Size(96, 32),
                Location = new Point(btnTorles.Location.X + btnTorles.Width + 16, btnTorles.Location.Y),
                Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Torzsadatok.Hozzaadas
            };
            btnHozzaadas.Click += btnHozzaadas_Click;

            Controls.Add( table );
            Controls.Add( lblTorzsadat );
            Controls.Add( cboTorzsadat );
            Controls.Add( btnTorles );
            Controls.Add( btnHozzaadas );
        }

        private DataTable
        CreateSource( )
        {
            data = new DataTable( );
            data.Columns.Add( new DataColumn( "Magyar", typeof (string)) );
            data.Columns.Add( new DataColumn( "Angol", typeof (string)) );
            data.Columns.Add( new DataColumn( "Német", typeof (string)) );

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void
        SetRow( DataRow _row, TORZSADAT _törzsadat ) { TORZSADAT.SetRow( _row, _törzsadat ); }

        protected override bool
        SameKeys( TORZSADAT _1, TORZSADAT _2 ) { return TORZSADAT.SameKeys( _1, _2 ); }

        protected override bool
        SameKeys( TORZSADAT _1, DataRow _row ) { return TORZSADAT.SameKeys( _1, _row ); }

        protected override List<TORZSADAT>
        CurrentData( ) { return Program.database.Törzsadatok( cboTorzsadat.Text ); }
        #endregion

        #region EventHandlers
        private void
        TorzsadatModositas( object _sender, EventArgs _event )
        {
            if ( table.SelectedRows.Count != 1 ) return;

            if ( !Program.felhasználó.Value.Jogosultsagok.Value.Torzsadatok.Modositas ) return;

            Form_Törzsadatok form = new Form_Törzsadatok( new TORZSADAT( cboTorzsadat.SelectedItem.ToString( ),
                                                                       ( string )table.SelectedRows[ 0 ].Cells[ 0 ].Value,
                                                                       ( string )table.SelectedRows[ 0 ].Cells[ 1 ].Value,
                                                                       ( string )table.SelectedRows[ 0 ].Cells[ 2 ].Value ) );
            form.ShowDialog( );

            Program.RefreshData( );
        }

        private void
        btnTorles_Click( object _sender, EventArgs _event )
        {
            if ( !Program.felhasználó.Value.Jogosultsagok.Value.Torzsadatok.Torles ) return;

            if ( table.SelectedRows.Count == 1 ) { if ( MessageBox.Show( "Biztosan törli a kiválasztott törzsadatot?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) != DialogResult.Yes ) return; }
            else if ( table.SelectedRows.Count != 0 ) { if ( MessageBox.Show( "Biztosan törli a kiválasztott törzsadatokat?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question ) != DialogResult.Yes ) return; }
            foreach ( DataGridViewRow selected in table.SelectedRows )
            {
                string azonosító = ( string )selected.Cells[ 0 ].Value;
                if ( !Program.database.Törzsadat_Törlés( azonosító ) )
                { MessageBox.Show( "Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő törzsadat (" + azonosító + ")?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                Program.RefreshData( );
            }
        }

        private void
            table_DataBindingComplete(object _sender, DataGridViewBindingCompleteEventArgs _event)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 200;
            table.Columns[1].Width = 200;
            table.Columns[2].Width = 200;
        }

        private void
        table_UserDeletingRow( object _sender, DataGridViewRowCancelEventArgs _event )
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            btnTorles_Click( _sender, _event );
        }

        private void
        btnHozzaadas_Click( object _sender, EventArgs _event )
        {
            Form_Törzsadatok form = new Form_Törzsadatok( cboTorzsadat.SelectedItem.ToString( ) );
            form.ShowDialog( );
        }

        private void
        Panel_Törzsadatok_KeyDown( object _sender, KeyEventArgs _event )
        {
            if ( _event.KeyCode == Keys.Enter ) TorzsadatModositas( _sender, _event );
        }

        private void
        cboTorzsadat_SelectedIndexChanged( object _sender, EventArgs _event )
        {
            data.Rows.Clear( );
            tokens.Clear( );
            Refresh( );
        }
        #endregion

        private sealed class Form_Törzsadatok : Form
        {
            private TORZSADAT? Torzsadat;

            private Label lblTipus2;
            private TextBox txtAzonosito;
            private TextBox txtMegnevezes;
            private TextBox txtMegnevezes2;

            #region Constructor
            public Form_Törzsadatok( string _típus )
            {
                InitializeForm( _típus, false );
                InitializeContent( );
                InitializeData( _típus );
            }

            public Form_Törzsadatok( TORZSADAT _eredeti )
            {
                Torzsadat = _eredeti;

                InitializeForm( _eredeti.Tipus, true );
                InitializeContent( );
                InitializeData( _eredeti );
            }

            private void
            InitializeForm( string _típus, bool _módosítás )
            {
                Text = _módosítás ? _típus + " módosítás" : "Új " + _típus + " hozzáadás";
                ClientSize = new Size( 400 - 64, 128 + 64 );
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void
            InitializeContent( )
            {
                Label lblTipus = new Label
                {
                    Text = "Típus:",
                    Location = new Point(16, 16 + 0*32)
                };

                Label lblMagyar = new Label
                {
                    Text = "Magyar:",
                    Location = new Point(lblTipus.Location.X, 16 + 1*32),
                    Height = 32
                };

                Label lblAngol = new Label
                {
                    Text = "Angol:",
                    Location = new Point(lblTipus.Location.X, 16 + 2*32),
                    Height = lblMagyar.Height
                };

                Label lblNemet = new Label
                {
                    Text = "Német:",
                    Location = new Point(lblTipus.Location.X, 16 + 3*32),
                    Height = lblMagyar.Height
                };

                lblTipus2 = new Label
                {
                    Location = new Point(lblTipus.Location.X + lblTipus.Width + 16, lblTipus.Location.Y),
                    Size = new Size(128 + 128, 24)
                };

                txtAzonosito = new TextBox
                {
                    Location = new Point(lblMagyar.Location.X + lblMagyar.Width + 16, lblMagyar.Location.Y),
                    Size = new Size(128 + 64, 34),
                    MaxLength = 25
                };

                txtMegnevezes = new TextBox
                {
                    Location = new Point(lblAngol.Location.X + lblAngol.Width + 16, lblAngol.Location.Y),
                    Size = txtAzonosito.Size,
                    MaxLength = 25
                };

                txtMegnevezes2 = new TextBox
                {
                    Location = new Point(lblNemet.Location.X + lblNemet.Width + 16, lblNemet.Location.Y),
                    Size = txtAzonosito.Size,
                    MaxLength = 25
                };

                Button btnRendben = new Button
                {
                    Text = "Rendben",
                    Size = new Size(96, 32)
                };
                btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16, ClientRectangle.Height - btnRendben.Size.Height - 16 );
                btnRendben.Click += btnRendben_Click;

                Controls.Add( lblTipus2 );
                Controls.Add( lblMagyar );
                Controls.Add( lblAngol );
                Controls.Add( lblNemet );

                Controls.Add( lblTipus );
                Controls.Add( txtAzonosito );
                Controls.Add( txtMegnevezes );
                Controls.Add( txtMegnevezes2 );

                Controls.Add( btnRendben );
            }

            private void
            InitializeData( string _típus )
            {
                lblTipus2.Text = _típus;
            }

            private void
            InitializeData( TORZSADAT _eredeti )
            {
                InitializeData( _eredeti.Tipus );

                txtAzonosito.Text = _eredeti.Azonosito;
                txtMegnevezes.Text = _eredeti.Megnevezes;
                txtMegnevezes2.Text = _eredeti.Megnevezes2;
            }
            #endregion

            #region EventHandlers
            private void 
            btnRendben_Click( object _sender, EventArgs _event )
            {
                if ( txtAzonosito.Text.Length == 0 ) { MessageBox.Show( "Nem megfelelő a magyar megnevezés hossza!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }
                if ( txtMegnevezes.Text.Length == 0 ) { MessageBox.Show( "Nem megfelelő az angol megnevezés hossza!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }
                if ( txtMegnevezes2.Text.Length == 0 ) { MessageBox.Show( "Nem megfelelő a német megnevezés hossza!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                if ( !Database.IsCorrectSQLText( txtAzonosito.Text ) ) { MessageBox.Show( "Nem megfelelő karakter a magyar megnevezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }
                if ( !Database.IsCorrectSQLText( txtMegnevezes.Text ) ) { MessageBox.Show( "Nem megfelelő karakter az angol megnevezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }
                if ( !Database.IsCorrectSQLText( txtMegnevezes2.Text ) ) { MessageBox.Show( "Nem megfelelő karakter a német megnevezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }

                if ( Torzsadat != null )
                {
                    if ( !Program.database.Törzsadat_Módosítás( Torzsadat.Value.Azonosito, new TORZSADAT( lblTipus2.Text,
                                                                                                        txtAzonosito.Text,
                                                                                                        txtMegnevezes.Text,
                                                                                                        txtMegnevezes2.Text ) ) )
                    { MessageBox.Show( "Adatbázis hiba!\nLehetséges, hogy a módosítandó törzsadat már nem létezik, vagy van már ilyen magyar megnevezés?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }
                }
                else if ( !Program.database.Törzsadat_Hozzáadás( new TORZSADAT( lblTipus2.Text,
                                                                            txtAzonosito.Text,
                                                                            txtMegnevezes.Text,
                                                                            txtMegnevezes2.Text ) ) )
                { MessageBox.Show( "Adatbázis hiba!\nLehetséges, hogy van már ilyen magyar megnevezés?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error ); return; }


                Close( );
            }
            #endregion
        }
    }
}
