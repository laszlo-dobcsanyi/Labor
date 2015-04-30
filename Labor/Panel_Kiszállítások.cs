using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct KISZALLITAS
    {
        public int SzallitolevelSzam;
        public string Szallitolevel;
        public string Felhasznalo;
        public string Datum;
        public string Vevo;
        public Int16 FoglaltHordo;

        public KISZALLITAS( int _SzallitolevelSzam,
                            string _Szallitolevel,
                            string _Felhasznalo,
                            string _Datum,
                            string _Vevo,
                            Int16 _FoglaltHordo )
        {
            SzallitolevelSzam = _SzallitolevelSzam;
            Szallitolevel = _Szallitolevel;
            Felhasznalo = _Felhasznalo;
            Datum = _Datum;
            Vevo = _Vevo;
            FoglaltHordo = _FoglaltHordo;
        }

        public struct TABLEINDEXES
        {
            public const int Szallitolevelszam = 0;
            public const int Szallitolevel = 1;
            public const int Vevo = 2;
            public const int FoglaltHordo = 3;
            public const int Felhasznalo = 4;
            public const int Datum = 5;
        }

        public static void 
        SetRow(DataRow _row, KISZALLITAS _azonosító)
        {
            _row[TABLEINDEXES.Szallitolevelszam] = _azonosító.SzallitolevelSzam;
            _row[TABLEINDEXES.Szallitolevel] = _azonosító.Szallitolevel;
            _row[TABLEINDEXES.Vevo] = _azonosító.Vevo;
            _row[TABLEINDEXES.FoglaltHordo] = _azonosító.FoglaltHordo;
            _row[TABLEINDEXES.Felhasznalo] = _azonosító.Felhasznalo;
            _row[TABLEINDEXES.Datum] = _azonosító.Datum;

        }

        public static bool 
        SameKeys(KISZALLITAS _1, KISZALLITAS _2)
        {
            if (_1.SzallitolevelSzam == _2.SzallitolevelSzam ) return true;
            return false;
        }

        public static bool 
        SameKeys(KISZALLITAS _1, DataRow _row)
        {
            if (_1.SzallitolevelSzam == (int)_row[TABLEINDEXES.Szallitolevelszam]) return true;
            return false;
        }
    }

    public sealed class Panel_Kiszállítások : Tokenized_Control<KISZALLITAS>
    {
        #region Counstructor
        public 
        Panel_Kiszállítások()
        {
            InitializeContent();
            InitializeTokens();
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
            table.Width = 720 + 3;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            table.MultiSelect = false;
            table.ReadOnly = true;
            table.DataSource = CreateSource();
            table.DataBindingComplete += table_DataBindingComplete;
            table.UserDeletingRow += table_UserDeletingRow;

            Button btnTorles = new Button();
            btnTorles.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnTorles.Text = "Törlés";
            btnTorles.Size = new Size(96, 32);
            btnTorles.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            btnTorles.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.KiszallitasokTorlese ? true : false;
            btnTorles.Click += btnTorles_Click;
        
            Controls.Add(table);
            Controls.Add(btnTorles);
        }

        private DataTable 
        CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Szállítólevél száma", Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Szállítólevél", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Vevő", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Összes hordó", Type.GetType("System.Int16")));
            data.Columns.Add(new DataColumn("Készítette", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítés ideje", Type.GetType("System.String")));

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void 
        SetRow(DataRow _row, KISZALLITAS _azonosító) { KISZALLITAS.SetRow(_row, _azonosító); }

        protected override bool 
        SameKeys(KISZALLITAS _1, KISZALLITAS _2) { return KISZALLITAS.SameKeys(_1, _2); }

        protected override bool 
        SameKeys(KISZALLITAS _1, DataRow _row) { return KISZALLITAS.SameKeys(_1, _row); }

        protected override List<KISZALLITAS> 
        CurrentData() { return Program.database.Kiszállítások(); }
        #endregion

        #region EventHandlers
        void 
        btnTorles_Click(object sender, EventArgs e)
        {
            if (table.SelectedRows.Count == 1) { if (MessageBox.Show("Biztosan törli a kiválasztott kiszállítást?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            if (!Program.database.Kiszállítás_Törlés((int)table.SelectedRows[0].Cells[0].Value))
            {
                MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő foglalás?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
   
        void 
        table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 120;
            table.Columns[1].Width = 120;
            table.Columns[2].Width = 120;
            table.Columns[3].Width = 120;
            table.Columns[4].Width = 120;
            table.Columns[5].Width = 120;
                
            table.Sort(table.Columns[0], ListSortDirection.Descending);

        }

        private void 
        table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            //Foglalás_Törlés(_sender, _event);
        }
        #endregion
    }
}
