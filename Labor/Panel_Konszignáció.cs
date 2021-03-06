﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct KONSZIGNACIOSZALLITOLEVEL
    {
        public Int16 SzallitolevelSzam;
        public string Szallitolevel;
        public string FelhasznaloNev;
        public string ElszallitasIdeje;
        public string Nyelv;
        public string Vevo;
        public string Rendszam1;
        public string Rendszam2;
        public Int16 FoglaltHordo;
        public string GyartasiIdo;
        public string Szin;
        public string Iz;
        public string Illat;

        public KONSZIGNACIOSZALLITOLEVEL(Int16 _SzallitolevelSzam,
                                         string _Szallitolevel, 
                                         string _FelhasznaloNev,
                                         string _ElszallitasIdeje, 
                                         string _Nyelv, 
                                         string _Vevo,
                                         string _Rendszam1, 
                                         string _Rendszam2, 
                                         Int16 _FoglaltHordo, 
                                         string _GyartasiIdo,
                                         string _Szin, 
                                         string _Iz, 
                                         string _Illat)
        {
            SzallitolevelSzam = _SzallitolevelSzam;
            Szallitolevel = _Szallitolevel;
            FelhasznaloNev = _FelhasznaloNev;
            ElszallitasIdeje = _ElszallitasIdeje;
            Nyelv = _Nyelv;
            Vevo = _Vevo;
            Rendszam1 = _Rendszam1;
            Rendszam2 = _Rendszam2;
            FoglaltHordo = _FoglaltHordo;
            GyartasiIdo = _GyartasiIdo;
            Szin = _Szin;
            Iz = _Iz;
            Illat = _Illat;
        }
    }

    public struct KONSZIGNACIO_TABLEINDEXES
    {
        public const int Szam = 0;
        public const int Nev = 1;
        public const int HordokSzama = 2;
        public const int Tipus = 3;
        public const int Keszitette = 4;
        public const int Ido = 5;
        public const int Kijeloles = 6;
    }

    public sealed class Panel_Konszignáció : Tokenized_Control<FOGLALAS>
    {
        #region Constructor

        public 
        Panel_Konszignáció()
        {
            InitializeContent();
            InitializeTokens();
        }

        private void 
        InitializeContent()
        {
            table = new DataGridView
            {
                Dock = DockStyle.Left,
                RowHeadersVisible = false,
                AllowUserToResizeRows = false,
                AllowUserToResizeColumns = false,
                AllowUserToAddRows = false,
                Width = (6 + 30 + 6 + 10 + 30 + 15 + 6)*8 + 3,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                DataSource = CreateSource()
            };
            table.DataBindingComplete += table_DataBindingComplete;
            table.UserDeletingRow += table_UserDeletingRow;
            table.CellMouseUp += table_CellMouseUp;

            Button btnNyomtatás = new Button
            {
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom,
                Text = "Nyomtatás",
                Size = new Size(128, 32)
            };
            btnNyomtatás.Location = new Point(ClientRectangle.Width - btnNyomtatás.Size.Width - 16,
                                            ClientRectangle.Height - btnNyomtatás.Height - 16);
            btnNyomtatás.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.KonszignacioNyomtatas ? true : false;
            btnNyomtatás.Click += Nyomtatas_Click;

            Controls.Add(table);
            Controls.Add(btnNyomtatás);
        }

        private DataTable 
        CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Száma", Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Foglalás neve", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Hordók", Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Típusa", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítette", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Foglalás ideje", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Kijelölés", Type.GetType("System.Boolean")));

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, FOGLALAS _foglalás) { FOGLALAS.SetRow(_row, _foglalás); }

        protected override bool SameKeys(FOGLALAS _1, FOGLALAS _2) { return FOGLALAS.SameKeys(_1, _2); }

        protected override bool SameKeys(FOGLALAS _1, DataRow _row) { return FOGLALAS.SameKeys(_1, _row); }

        protected override List<FOGLALAS> CurrentData() { return Program.database.Konszingnáció_Foglalások(); }
        #endregion

        #region EventHandlers

        private void
        Nyomtatas_Click( object _sender, EventArgs _event )
        {
            List<FOGLALAS> Foglalások = new List<FOGLALAS>( );

            foreach ( DataGridViewRow item in table.Rows )
            {
                if ( item.Cells[ 6 ].Value.ToString( ) != "" &&
                    Convert.ToBoolean( item.Cells[ 6 ].Value ) )
                {
                    Foglalások.Add( new FOGLALAS( ( int )item.Cells[ FOGLALAS.TABLEINDEXES.ID ].Value,
                                                ( string )item.Cells[ FOGLALAS.TABLEINDEXES.Nev ].Value,
                                                ( int )item.Cells[ FOGLALAS.TABLEINDEXES.HordokSzama ].Value,
                                                ( string )item.Cells[ FOGLALAS.TABLEINDEXES.Tipus ].Value,
                                                ( string )item.Cells[ FOGLALAS.TABLEINDEXES.Keszito ].Value,
                                                ( string )item.Cells[ FOGLALAS.TABLEINDEXES.Ido ].Value ) );
                }
            }

            if ( Foglalások.Count != 0 )
            {
                KonszignacioNyomtatas nyomtató = new KonszignacioNyomtatas( Foglalások );
                nyomtató.ShowDialog( );
            }
            Program.RefreshData( );
        }

        private void
        table_CellMouseUp( object _sender, DataGridViewCellMouseEventArgs _event )
        {
            // End of edition on each click on column of checkbox
            if ( _event.RowIndex == -1 ) return;

            if ( _event.ColumnIndex == 6 ) { table.EndEdit( ); }
            else
            {
                if ( _event.Clicks == 2 )
                {
                    if ( table.SelectedRows.Count != 1 ) return;

                    FOGLALAS foglalás = new FOGLALAS( ( int )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.ID ].Value,
                                                     ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Nev ].Value,
                                                     ( int )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.HordokSzama ].Value,
                                                     ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Tipus ].Value,
                                                     ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Keszito ].Value,
                                                     ( string )table.SelectedRows[ 0 ].Cells[ FOGLALAS.TABLEINDEXES.Ido ].Value );

                    KonszignacioHordok hordó_megjelenítő = new KonszignacioHordok( foglalás );
                    hordó_megjelenítő.ShowDialog( this );

                    Program.RefreshData( );
                }
            }
        }

        private void 
        table_DataBindingComplete(object _sender, DataGridViewBindingCompleteEventArgs _event)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 6 * 8;
            table.Columns[0].ReadOnly = true;
            table.Columns[1].Width = 30 * 8;
            table.Columns[1].ReadOnly = true;
            table.Columns[2].Width = 6 * 8;
            table.Columns[2].ReadOnly = true;
            table.Columns[3].Width = 10 * 8;
            table.Columns[3].ReadOnly = true;
            table.Columns[4].Width = 30 * 8;
            table.Columns[4].ReadOnly = true;
            table.Columns[5].Width = 15 * 8;
            table.Columns[5].ReadOnly = true;
            table.Columns[6].Width = 6 * 8;
            table.Columns[6].ReadOnly = false;

            table.Sort(table.Columns[0], ListSortDirection.Descending);

        }

        private void
        table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            _event.Cancel = true;
        }
        #endregion

        public sealed class MyClass : Form
        {

            #region Declaration

            #endregion

            #region Constructor
            public
            MyClass( )
            {
                InitializeForm( );
                InitializeContent( );
                InitializeData( );
            }

            private void
            InitializeForm( )
            {
                Text = "";
                ClientSize = new Size( 640, 480 );
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void
            InitializeContent( )
            {

            }

            private void
            InitializeData( )
            {

            }
            #endregion

            #region EventHandlers

            #endregion
        }

        public sealed class KonszignacioNyomtatas: Form
        {
            ComboBox cboMegrendelok;
            ComboBox cboNyelv;
            TextBox txtRendszam1;
            TextBox txtRendszam2;
            TextBox txtLevel;
            TextBox txtGyartasiIdo;
            TextBox txtSzin;
            TextBox txtIz;
            TextBox txtIllat;

            List<FOGLALAS> Foglalasok;

            #region Constructor

            public 
            KonszignacioNyomtatas(List<FOGLALAS> _foglalások)
            {
                Foglalasok = _foglalások;

                InitializeForm();
                InitializeContent();
            }

            private void 
            InitializeForm()
            {
                Text = "Nyomtatás";
                FormBorderStyle = FormBorderStyle.FixedSingle;
                MaximizeBox = false;
                ClientSize = new Size(320, 465);
                StartPosition = FormStartPosition.CenterParent;
                MinimumSize = ClientSize;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void
            InitializeContent( )
            {
                Label lblNyelv;
                Label lblVevo;
                Label lblGepkocsi;
                Label lblSzallitolevel;
                Label lblGyartasiIdo;
                Label lblSzin;
                Label lblIz;
                Label lblIllat;

                Label lblVonal = new Label( );
                lblVonal.Height = 3;
                lblVonal.Width = 1000;
                lblVonal.BackColor = Color.Gray;

                Label lblVonal2 = new Label( );
                lblVonal2.Height = 3;
                lblVonal2.Width = 1000;
                lblVonal2.BackColor = Color.Gray;

                Button btnRendben;

                lblNyelv = MainForm.createlabel( "Nyelv:", 16, 16 + 0 * 32, this );
                lblVonal.Location = new Point( lblNyelv.Location.X - lblNyelv.Width,
                                               lblNyelv.Location.Y + 32 );
                Controls.Add( lblVonal );

                lblVevo = MainForm.createlabel( "Vevő:", 16, 32 + 1 * 32, this );
                lblGepkocsi = MainForm.createlabel( "Gépkocsi:", 16, 32 + 2 * 32, this );
                lblSzallitolevel = MainForm.createlabel( "Szállítólevél:", 16, 32 + 3 * 32, this );

                lblVonal2.Location = new Point( lblSzallitolevel.Location.X - lblSzallitolevel.Width,
                                                lblSzallitolevel.Location.Y + 32 );
                Controls.Add( lblVonal2 );

                lblGyartasiIdo = MainForm.createlabel( "Gyártási idő:", 16, 32 + 5 * 32, this );

                lblSzin = MainForm.createlabel( "Szín:", 16, 32 + 6 * 32, this );
                lblIz = MainForm.createlabel( "Íz:", 16, 32 + 8 * 32, this );
                lblIllat = MainForm.createlabel( "Illat:", 16, 32 + 10 * 32, this );

                cboNyelv = MainForm.createcombobox( lblNyelv.Location.X + 48 + lblNyelv.Width,
                    lblNyelv.Location.Y, 200, this );
                cboMegrendelok = MainForm.createcombobox( cboNyelv.Location.X, lblVevo.Location.Y, 200, this );

                txtRendszam1 = MainForm.createtextbox( cboNyelv.Location.X, lblGepkocsi.Location.Y, 20, 70, this );
                txtRendszam2 = MainForm.createtextbox( txtRendszam1.Location.X + txtRendszam1.Width + 8,
                                                      lblGepkocsi.Location.Y, 20, 70, this );
                txtLevel = MainForm.createtextbox( cboNyelv.Location.X, lblSzallitolevel.Location.Y, 20, 70, this );
                txtGyartasiIdo = MainForm.createtextbox( cboNyelv.Location.X, lblGyartasiIdo.Location.Y, 20, 70, this );

                txtSzin = MainForm.createtextbox( cboNyelv.Location.X, lblSzin.Location.Y, 60, 200, this );
                txtSzin.Multiline = true;
                txtSzin.Height = 50;

                txtIz = MainForm.createtextbox( cboNyelv.Location.X, lblIz.Location.Y, 60, 200, this );
                txtIz.Multiline = true;
                txtIz.Height = 50;

                txtIllat = MainForm.createtextbox( cboNyelv.Location.X, lblIllat.Location.Y, 60, 200, this );
                txtIllat.Multiline = true;
                txtIllat.Height = 50;

                btnRendben = new Button( );
                btnRendben.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                btnRendben.Text = "Rendben";
                btnRendben.Size = new Size( 96, 32 );
                btnRendben.Location = new Point( ClientRectangle.Width - btnRendben.Size.Width - 16,
                                                ClientRectangle.Height - btnRendben.Size.Height - 16 );

                cboNyelv.Items.Add( "Magyar" );
                cboNyelv.Items.Add( "Angol" );
                cboNyelv.Items.Add( "3. label_nyelv" );
                cboNyelv.SelectedIndex = 0;

                List<string> Megrendelok = Program.database.Megrendelők( );
                foreach ( string item in Megrendelok )
                {
                    cboMegrendelok.Items.Add( item );
                }
                cboMegrendelok.SelectedIndex = 0;


                /*csak tesztelés miatt!
                txtRendszam1.Text = "KTM791";
                txtRendszam2.Text = "HCS850";
                txtLevel.Text = "levél";
                txtGyartasiIdo.Text = "2014.04.20";
                txtSzin.Text = "Szín";
                txtIz.Text = "Íz";
                txtIllat.Text = "Illat";
                */

                btnRendben.Click += btnRendben_Click;
                Controls.Add( btnRendben );
            }
            #endregion

            #region EventHandlers

            private void 
            btnRendben_Click(object _sender, EventArgs _event)
            {
                string date = DateTime.Now.Year.ToString() + '.'+ DateTime.Now.Month.ToString("d2") + '.' + DateTime.Now.Day.ToString("d2");

                if (cboNyelv.Text == "3. label_nyelv")
                { MessageBox.Show("Nyelv mező nem lehet" + '"'+ "3. label_nyelv" + '"' , "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtRendszam1.Text == "" )
                { MessageBox.Show("Rendszám1 mező nem lehet üres!", "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtLevel.Text == "")
                { MessageBox.Show("Szállítólevél mező nem lehet üres!", "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if(txtLevel.Text.Contains("/"))
                { MessageBox.Show( "Nem megengedett karakter a szállítólevél mezőben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning ); return; }
                if (txtGyartasiIdo.Text == "" || txtGyartasiIdo.Text.Length < 4)
                { MessageBox.Show("Gyártási idő mező nem lehet üres és minimum 4 karakter!", "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtSzin.Text == "")
                { MessageBox.Show("Szín mező nem lehet üres!", "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtIz.Text == "")
                { MessageBox.Show("Íz mező nem lehet üres!", "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (txtIllat.Text == "")
                { MessageBox.Show("Illat mező nem lehet üres!", "Egyenlőre!", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                KONSZIGNACIOSZALLITOLEVEL szállítólevél = new KONSZIGNACIOSZALLITOLEVEL(0,
                                                                                        txtLevel.Text, 
                                                                                        Foglalasok[0].Keszito, 
                                                                                        date, 
                                                                                        cboNyelv.Text[0].ToString(), 
                                                                                        cboMegrendelok.Text, 
                                                                                        txtRendszam1.Text, 
                                                                                        txtRendszam2.Text, 
                                                                                        (Int16)Foglalasok[0].HordokSzama, 
                                                                                        txtGyartasiIdo.Text, 
                                                                                        txtSzin.Text, txtIz.Text, 
                                                                                        txtIllat.Text);
                Nyomtat.Konszignacio(szállítólevél, Foglalasok);
                szállítólevél.SzallitolevelSzam =  Convert.ToInt16( Program.database.Konszignáció_ÚJSzállítólevél(szállítólevél));
                Program.database.Konszignáció_FoglalásokKiszállítása(szállítólevél.SzallitolevelSzam, Foglalasok);

                 Nyomtat.MinosegBizonylatok(szállítólevél, Foglalasok);
                 
                Close();
            }
            #endregion
        }

        public sealed class KonszignacioHordok : Tokenized_Form<HORDO>
        {
            private FOGLALAS Foglalás;

            #region Constructor

            public 
            KonszignacioHordok(FOGLALAS _foglalás)
            {
                Foglalás = _foglalás;

                InitializeForm();
                InitializeContent();
                InitializeTokens();
            }

            private void
            InitializeForm()
            {
                Text = "Hordók megtekintése";
                ClientSize = new Size(430, 568);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;

                Load += Konszignáció_Hordók_Load;
            }

            private void 
            InitializeContent()
            {
                table = new DataGridView();
                table.Dock = DockStyle.Left;
                table.RowHeadersVisible = false;
                table.AllowUserToResizeRows = false;
                table.AllowUserToResizeColumns = false;
                table.AllowUserToAddRows = false;
                table.Width = 430;
                table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                table.ReadOnly = true;
                table.DataSource = CreateSource();
                table.UserDeletingRow += table_UserDeletingRow;

                Controls.Add(table);
            }

            private DataTable 
            CreateSource()
            {
                data = new DataTable();

                data.Columns.Add(new DataColumn("Termékkód", Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("5-6 kód", Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Sarzs", Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Hordó száma", Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Foglalás száma", Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Gyártási év", Type.GetType("System.String")));

                return data;
            }
            #endregion

            #region Tokenizer
            protected override void SetRow(DataRow _row, HORDO _hordó) { HORDO.SetRow(_row, _hordó); }

            protected override bool SameKeys(HORDO _1, HORDO _2) { return HORDO.SameKeys(_1, _2); }

            protected override bool SameKeys(HORDO _1, DataRow _row) { return HORDO.SameKeys(_1, _row); }

            protected override List<HORDO> CurrentData() { return Program.database.Hordók(Foglalás); }
            #endregion

            #region EventHandlers
            private void Konszignáció_Hordók_Load(object _sender, EventArgs _event)
            {
                table.Columns[HORDO.TABLEINDEXES.Termekkod].Width = 430 / 4;
                table.Columns[HORDO.TABLEINDEXES.Sarzs].Width = 430 / 4;
                table.Columns[HORDO.TABLEINDEXES.ID].Width = 430 / 4;
                table.Columns[HORDO.TABLEINDEXES.FoglalasSzama].Visible = false;
                table.Columns[HORDO.TABLEINDEXES.GyartasiEv].Width = 430 / 4 - 1;
            }

            private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
            {
                _event.Cancel = true;
            }
            #endregion
        }
    }
}
