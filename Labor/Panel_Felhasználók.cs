using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Labor
{
    public struct Felhasználó
    {
        public string név1;
        public string név2;
        public string beosztás1;
        public string beosztás2;
        public string felhasználó_név;
        public string jelszó;

        public Jogosultságok? jogosultságok;

        public struct TableIndexes
        {
            public const int név1 = 0;
            public const int beosztás1 = 1;
            public const int felhasználó_név = 2;
        }

        public struct Jogosultságok
        {
            public struct Műveletek
            {
                public bool hozzáadás;
                public bool módosítás;
                public bool törlés;
            }

            public Műveletek törzsadatok;
            public Műveletek vizsgálatok;
            public Műveletek foglalások;
            public Műveletek felhasználók;
            public bool konszignáció_nyomtatás;
            public bool kiszállítás_törlés;
        }

        public static void SetRow(DataRow _row, Felhasználó _felhasználó)
        {
            _row[TableIndexes.név1] = _felhasználó.név1;
            _row[TableIndexes.beosztás1] = _felhasználó.beosztás1;
            _row[TableIndexes.felhasználó_név] = _felhasználó.felhasználó_név;
        }

        public static bool SameKeys(Felhasználó _1, Felhasználó _2)
        {
            if (_1.felhasználó_név == _2.felhasználó_név) return true;
            return false;
        }

        public static bool SameKeys(Felhasználó _1, DataRow _row)
        {
            if (_1.felhasználó_név == (string)_row[TableIndexes.felhasználó_név]) return true;
            return false;
        }
    }

    public sealed class Panel_Felhasználók : Tokenized_Control<Felhasználó>
    {
        #region Constructor
        public Panel_Felhasználók()
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
            table.Width = 450 + 3;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += Felhasználó_Módosítás;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();

            //

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Click += Felhasználó_Törlés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new System.Drawing.Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            hozzáadás.Click += Felhasználó_Hozzáadás;

            //

            Controls.Add(table);
            Controls.Add(törlés);
            Controls.Add(hozzáadás);
        }

        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Név1", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Beosztás1", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Felhasználónév", System.Type.GetType("System.String")));

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, Felhasználó _felhasználó) { Felhasználó.SetRow(_row, _felhasználó); }

        protected override bool SameKeys(Felhasználó _1, Felhasználó _2) { return Felhasználó.SameKeys(_1, _2); }

        protected override bool SameKeys(Felhasználó _1, DataRow _row) { return Felhasználó.SameKeys(_1, _row); }

        protected override List<Felhasználó> CurrentData() { return Program.database.Felhasználók(); }

        #endregion

        #region EventHandlers
        private void Felhasználó_Hozzáadás(object _sender, EventArgs _event)
        {
            Felhasználó_Megjelenítő hozzáadó = new Felhasználó_Megjelenítő();
            hozzáadó.ShowDialog();

            Program.RefreshData();
        }

        private void Felhasználó_Módosítás(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;

            Felhasználó? felhasználó = Program.database.Felhasználó((string)table.SelectedRows[0].Cells[Felhasználó.TableIndexes.felhasználó_név].Value);
            if (felhasználó == null) { MessageBox.Show("Hiba a felhasználó lekérdezésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Felhasználó_Megjelenítő módosító = new Felhasználó_Megjelenítő(felhasználó.Value);
            módosító.ShowDialog();

            Program.RefreshData();
        }

        private void Felhasználó_Törlés(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;

            if (!Program.database.Felhasználó_Törlés((string)table.SelectedRows[0].Cells[Felhasználó.TableIndexes.felhasználó_név].Value))
                { MessageBox.Show("Hiba a felhasználó törlésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Program.RefreshData();
        }

        //

        private void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[Felhasználó.TableIndexes.név1].Width = 150;
            table.Columns[Felhasználó.TableIndexes.beosztás1].Width = 150;
            table.Columns[Felhasználó.TableIndexes.felhasználó_név].Width = 150;
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            Felhasználó_Törlés(_sender, _event);
        }
        #endregion

        public class Felhasználó_Megjelenítő : Form
        {
            Felhasználó? felhasználó = null;

            #region Constructor
            public Felhasználó_Megjelenítő()
            {
                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            public Felhasználó_Megjelenítő(Felhasználó _felhasználó)
            {
                felhasználó = _felhasználó;

                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            public void InitializeForm()
            {

            }
            public void InitializeContent()
            {

            }
            public void InitializeData()
            {

            }
            #endregion

            #region EventHandlers
            #endregion
        }
    }
}
