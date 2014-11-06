using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct Kiszállítás
    {
        public int szállítólevélszám;
        public string szállítólevél;
        public string felhasználó;
        public string dátum;
        public string vevő;
        public Int16 fogalthordó;

        public Kiszállítás(int _szállítólevélszám, string _szállítólevél, string _felhasználó, string _dátum, string _vevő, Int16 _fogalthordó)
        {
            szállítólevélszám = _szállítólevélszám;
            szállítólevél = _szállítólevél;
            felhasználó = _felhasználó;
            dátum = _dátum;
            vevő = _vevő;
            fogalthordó = _fogalthordó;
        }

        public struct TableIndexes
        {
            public const int szállítólevélszám = 0;
            public const int szállítólevél = 1;
            public const int vevő = 2;
            public const int fogalthordó = 3;
            public const int felhasználó = 4;
            public const int dátum = 5;
        }

        public static void SetRow(DataRow _row, Kiszállítás _azonosító)
        {
            _row[TableIndexes.szállítólevélszám] = _azonosító.szállítólevélszám;
            _row[TableIndexes.szállítólevél] = _azonosító.szállítólevél;
            _row[TableIndexes.vevő] = _azonosító.vevő;
            _row[TableIndexes.fogalthordó] = _azonosító.fogalthordó;
            _row[TableIndexes.felhasználó] = _azonosító.felhasználó;
            _row[TableIndexes.dátum] = _azonosító.dátum;

        }

        public static bool SameKeys(Kiszállítás _1, Kiszállítás _2)
        {
            if (_1.szállítólevélszám == _2.szállítólevélszám ) return true;
            return false;
        }

        public static bool SameKeys(Kiszállítás _1, DataRow _row)
        {
            if (_1.szállítólevélszám == (int)_row[TableIndexes.szállítólevélszám]) return true;
            return false;
        }
    }

    public sealed class Panel_Kiszállítások : Tokenized_Control<Kiszállítás>
    {
        #region Counstructor
        public Panel_Kiszállítások()
        {
            InitializeContent();
            InitializeTokens();
        }

        private void InitializeContent()
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

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Enabled = Program.felhasználó.Value.jogosultságok.Value.kiszállítások_törlés ? true : false;
            törlés.Click += törlés_Click;
        
            Controls.Add(table);
            Controls.Add(törlés);
        }

        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Szállítólevél száma", System.Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Szállítólevél", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Vevő", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Összes hordó", System.Type.GetType("System.Int16")));
            data.Columns.Add(new DataColumn("Készítette", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítetés ideje", System.Type.GetType("System.String")));

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, Kiszállítás _azonosító) { Kiszállítás.SetRow(_row, _azonosító); }

        protected override bool SameKeys(Kiszállítás _1, Kiszállítás _2) { return Kiszállítás.SameKeys(_1, _2); }

        protected override bool SameKeys(Kiszállítás _1, DataRow _row) { return Kiszállítás.SameKeys(_1, _row); }

        protected override List<Kiszállítás> CurrentData() { return Program.database.Kiszállítások(); }
        #endregion

        #region EventHandlers
        void törlés_Click(object sender, EventArgs e)
        {
            if (table.SelectedRows.Count == 1) { if (MessageBox.Show("Biztosan törli a kiválasztott kiszállítást?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            if (!Program.database.Kiszállítás_Törlés((int)table.SelectedRows[0].Cells[0].Value))
            {
                MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő foglalás?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
   
        void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 120;
            table.Columns[1].Width = 120;
            table.Columns[2].Width = 120;
            table.Columns[3].Width = 120;
            table.Columns[4].Width = 120;
            table.Columns[5].Width = 120;
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            //Foglalás_Törlés(_sender, _event);
        }
        #endregion
    }
}
