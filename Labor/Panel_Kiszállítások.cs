using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public sealed class Panel_Kiszállítások : Control
    {
        private DataTable data;
        private DataGridView table;
        
        public Panel_Kiszállítások()
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
            table.Width = 720;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //table.MultiSelect = false;
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Click += törlés_Click;
        
            Controls.Add(table);
            Controls.Add(törlés);
        }

        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Szállítólevél száma", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Szállítólevél", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Vevő", System.Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Összes hordó", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítette", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítetés ideje", System.Type.GetType("System.String")));

            return data;
        }

        #region EventHandlers
        void törlés_Click(object sender, EventArgs e)
        {
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
