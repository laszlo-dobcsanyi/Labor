using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{

    public struct Konszignáció_Hordók_TableIndexes
    {
        public const int termékkód = 0;
        public const int gyártási_év = 1;
        public const int sarzs = 2;
        public const int hordószám = 3;

    }

    public struct Konszignáció_TableIndexes
    {
        public const int foglalás_száma = 0;
        public const int foglalás_neve = 1;
        public const int foglalt_hordók_száma = 2;
        public const int foglalás_típusa = 3;
        public const int készítette = 4;
        public const int foglalás_ideje = 5;
        public const int kijelölés = 6;
    }

    public sealed class Panel_Konszignáció : Control
    {
        private DataTable data;
        private DataGridView table;

        public Panel_Konszignáció()
        {
            InitializeContent();
        }
        private void InitializeContent()
        {
            table = new DataGridView();
            table.Dock = DockStyle.Left;
            table.RowHeadersVisible = false;
            table.AllowUserToResizeRows = false;
            table.AllowUserToResizeColumns = false;
            table.AllowUserToAddRows = false;
            table.Width = 700;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //table.MultiSelect = false;
            table.ReadOnly = false;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += módosítás_Click;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();
            table.CellMouseUp += table_CellMouseUp;


            Button hordók = new Button();
            hordók.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hordók.Text = "Hordók";
            hordók.Size = new System.Drawing.Size(96, 32);
            hordók.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            hordók.Click += hordók_Click;

            Button nyomtatás = new Button();
            nyomtatás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            nyomtatás.Text = "Nyomtatás";
            nyomtatás.Size = new System.Drawing.Size(96, 32);
            nyomtatás.Location = new Point(hordók.Location.X + hordók.Width + 16, hordók.Location.Y);
            nyomtatás.Click += nyomtatás_Click;

            Controls.Add(table);
            Controls.Add(hordók);
            Controls.Add(nyomtatás);
        }




        private DataTable CreateSource()
        {
            data = new DataTable();

            DataColumn column = new DataColumn("Foglalás száma", System.Type.GetType("System.Int32"));
            data.Columns.Add(column);
            column.ReadOnly = true;

            column = new DataColumn("Foglalás neve", System.Type.GetType("System.String"));
            data.Columns.Add(column);
            column.ReadOnly = true;

            column = new DataColumn("Foglalt hordók száma", System.Type.GetType("System.Int32"));
            data.Columns.Add(column);
            column.ReadOnly = true;

            column = new DataColumn("Foglalás típusa", System.Type.GetType("System.String"));
            data.Columns.Add(column);
            column.ReadOnly = true;

            column = new DataColumn("Készítette", System.Type.GetType("System.String"));
            data.Columns.Add(column);
            column.ReadOnly = true;

            column = new DataColumn("Foglalás ideje", System.Type.GetType("System.String"));
            data.Columns.Add(column);
            column.ReadOnly = true;

            column = new DataColumn("Kijelölés", System.Type.GetType("System.Boolean"));
            data.Columns.Add(column);
            column.ReadOnly = false;

            List<Foglalás> foglalások = Program.database.Konszingnáció_Foglalások();

            foreach (Foglalás item in foglalások)
            {
                DataRow row = data.NewRow();

                row[Konszignáció_TableIndexes.foglalás_száma] = item.id;
                row[Konszignáció_TableIndexes.foglalás_neve] = item.név;
                row[Konszignáció_TableIndexes.foglalt_hordók_száma] = item.hordók_száma;
                row[Konszignáció_TableIndexes.foglalás_típusa] = item.típus;
                row[Konszignáció_TableIndexes.készítette] = item.készítő;
                row[Konszignáció_TableIndexes.foglalás_ideje] = item.idő;

                data.Rows.Add(row);
            }

            return data;
        }

        #region EventHandlers
        ComboBox combo_megrendelők;

        void nyomtatás_Click(object sender, EventArgs e)
        {
            Form form_nyomtatás = new Form();
            form_nyomtatás.Text = "Nyomtatás";
            form_nyomtatás.ClientSize = new Size(320, 368);
            form_nyomtatás.StartPosition = FormStartPosition.CenterScreen;


            Label label_nyelv;
            Label label_vevő;
            Label label_gépkocsi;
            Label label_szállítólevél;
            Label label_gyártási_idő;
            Label label_szín;
            Label label_íz;
            Label label_illat;

            ComboBox combo_nyelv;
            ComboBox combo_megrendelők;
            TextBox box_rendszám1;
            TextBox box_rendszám2;
            TextBox box_levél;
            TextBox box_gyártási_idő;
            TextBox box_szín;
            TextBox box_íz;
            TextBox box_illat;
            Button rendben;


            label_nyelv = MainForm.createlabel("Nyelv:", 16, 16 + 0 * 32, form_nyomtatás);
            label_vevő = MainForm.createlabel("Vevő:", 16, 16 + 1 * 32, form_nyomtatás);
            label_gépkocsi = MainForm.createlabel("Gépkocsi:", 16, 16 + 2 * 32, form_nyomtatás);
            label_szállítólevél = MainForm.createlabel("Szállítólevél:", 16, 16 + 3 * 32, form_nyomtatás);
            label_gyártási_idő = MainForm.createlabel("Gyártási idő:", 16, 16 + 4 * 32, form_nyomtatás);
            label_szín = MainForm.createlabel("Szín:", 16, 16 + 5 * 32, form_nyomtatás);
            label_íz = MainForm.createlabel("Íz:", 16, 16 + 6 * 32, form_nyomtatás);
            label_illat = MainForm.createlabel("Illat:", 16, 16 + 7 * 32, form_nyomtatás);

            combo_nyelv = MainForm.createcombobox(label_nyelv.Location.X + 48 + label_nyelv.Width, label_nyelv.Location.Y, 200, form_nyomtatás);
            combo_nyelv.Items.Add("Magyar"); combo_nyelv.Items.Add("Angol"); combo_nyelv.Items.Add("3. label_nyelv"); combo_nyelv.SelectedIndex = 0;

            combo_megrendelők = MainForm.createcombobox(combo_nyelv.Location.X, label_vevő.Location.Y, 200, form_nyomtatás);
            List<string> megrendelok = Program.database.Megrendelők();
            foreach (string item in megrendelok) { combo_megrendelők.Items.Add(item); } combo_megrendelők.SelectedIndex = 0;

            box_rendszám1 = MainForm.createtextbox(combo_nyelv.Location.X, label_gépkocsi.Location.Y, 20, 70, form_nyomtatás);
            box_rendszám2 = MainForm.createtextbox(box_rendszám1.Location.X + box_rendszám1.Width + 8, label_gépkocsi.Location.Y, 20, 70, form_nyomtatás);
            box_levél = MainForm.createtextbox(combo_nyelv.Location.X, label_szállítólevél.Location.Y, 20, 70, form_nyomtatás);

            box_gyártási_idő = MainForm.createtextbox(combo_nyelv.Location.X, label_gyártási_idő.Location.Y, 20, 70, form_nyomtatás);
            box_szín = MainForm.createtextbox(combo_nyelv.Location.X, label_szín.Location.Y, 20, 70, form_nyomtatás);
            box_íz = MainForm.createtextbox(combo_nyelv.Location.X, label_íz.Location.Y, 20, 70, form_nyomtatás);
            box_illat = MainForm.createtextbox(combo_nyelv.Location.X, label_illat.Location.Y, 20, 70, form_nyomtatás);

            rendben = new Button();
            rendben.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            rendben.Text = "Rendben";
            rendben.Size = new System.Drawing.Size(96, 32);
            rendben.Location = new Point(150, 300);
            rendben.Click += nyomtatás_rendben_Click;

            form_nyomtatás.Controls.Add(rendben);
            form_nyomtatás.ShowDialog();
        }

        void nyomtatás_rendben_Click(object sender, EventArgs e)
        {
            Nyomtat.Nyomtat_Konszignáció(combo_megrendelők.Text);
        }



        private void table_CellMouseUp(object _sender, DataGridViewCellMouseEventArgs _event)
        {
            // End of edition on each click on column of checkbox
            if (_event.ColumnIndex == 6 && _event.RowIndex != -1)
            {
                table.EndEdit();
            }
        }
     
        void hordók_Click(object sender, EventArgs e)
        {
            Konszignáció_Hordók konszignáció_hordók = new Konszignáció_Hordók( (int)table.SelectedRows[0].Cells[Konszignáció_TableIndexes.foglalás_száma].Value );
            int q = (int)table.SelectedRows[0].Cells[Konszignáció_TableIndexes.foglalás_száma].Value;
            konszignáció_hordók.Show();
        }

        private void módosítás_Click(object sender, DataGridViewCellEventArgs e)
        {
        }

        void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 100;
            table.Columns[1].Width = 100;
            table.Columns[2].Width = 100;
            table.Columns[3].Width = 100;
            table.Columns[4].Width = 100;
            table.Columns[5].Width = 100;
            table.Columns[6].Width = 100;
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            //Vizsgálat_Törlés(_sender, _event);
        }
        #endregion

        public sealed class Konszignáció_Hordók : Form
        {
            public int id;
            private DataTable data;
            private DataGridView table;
  
            public Konszignáció_Hordók(int _id)
            {
                id = _id;
                InitializeForm();
                InitializeContent();
                InitializeData();

            }

            private void InitializeForm()
            {
                Text = "Hordók megtekintése";
                ClientSize = new Size(280, 568);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                table = new DataGridView();
                table.Dock = DockStyle.Left;
                table.RowHeadersVisible = false;
                table.AllowUserToResizeRows = false;
                table.AllowUserToResizeColumns = false;
                table.AllowUserToAddRows = false;
                table.Width = 700;
                table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //table.MultiSelect = false;
                table.ReadOnly = true;
                table.DataBindingComplete += table_DataBindingComplete;
                table.UserDeletingRow += table_UserDeletingRow;
                table.DataSource = CreateSource();

                Controls.Add(table);
            }

            private void InitializeData()
            {
            }

            private DataTable CreateSource()
            {
                data = new DataTable();

                data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Gyártási év", System.Type.GetType("System.Int32")));
                data.Columns.Add(new DataColumn("Sarzs", System.Type.GetType("System.Int32")));
                data.Columns.Add(new DataColumn("Hordószám", System.Type.GetType("System.String")));

                List<Hordó> hordók = Program.database.Konszignáció_Hordók(id);

                foreach (Hordó item in hordók)
                {
                    DataRow row = data.NewRow();

                    row[Konszignáció_Hordók_TableIndexes.termékkód] = item.termékkód;
                    row[Konszignáció_Hordók_TableIndexes.gyártási_év] = item.gyártási_év;
                    row[Konszignáció_Hordók_TableIndexes.sarzs] = item.sarzs;
                    row[Konszignáció_Hordók_TableIndexes.hordószám] = item.id;
                    data.Rows.Add(row);
                }
                return data;
            }

            #region EventHandlers
 
            void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
            {
                table.DataBindingComplete -= table_DataBindingComplete;
                table.Columns[0].Width = 70;
                table.Columns[1].Width = 70;
                table.Columns[2].Width = 70;
                table.Columns[3].Width = 70;
            }
                 private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
            {
                // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                _event.Cancel = true;
                // A saját törlést azért elindítjuk Delete gomb lenyomása után.
               // Vizsgálat_Törlés(_sender, _event);
            }
            #endregion
        }
    }
}
