using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
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
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += módosítás_Click;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();


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

            Controls.Add(table);
            Controls.Add(hordók);
            Controls.Add(nyomtatás);
        }



        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Foglalás száma", System.Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Foglalás neve", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Foglalt hordók száma", System.Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Foglalás típusa", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítette", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Foglalás ideje", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Kijelölés", System.Type.GetType("System.String")));

            return data;
        }

        #region EventHandlers
        void hordók_Click(object sender, EventArgs e)
        {
            Konszignáció_Hordók konszignáció_hordók = new Konszignáció_Hordók();
            konszignáció_hordók.ShowDialog();
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
            private DataTable data;
            private DataGridView table;
  
            public Konszignáció_Hordók()
            {
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
