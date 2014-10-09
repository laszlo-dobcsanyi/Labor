using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Labor
{
    public struct Törzsadat
    {
        public string típus;
        /// <summary>
        /// Magyar név.
        /// </summary>
        public string azonosító;
        /// <summary>
        /// Angol név.
        /// </summary>
        public string megnevezés_2;
        /// <summary>
        /// Német név.
        /// </summary>
        public string megnevezés_3;

        public Törzsadat(string _típus, string _azonosító, string _megnevezés_2, string _megnevezés_3)
        {
            típus = _típus;
            azonosító = _azonosító;
            megnevezés_2 = _megnevezés_2;
            megnevezés_3 = _megnevezés_3;
        }

        //

        public static void SetRow(DataRow _row, Törzsadat _törzsadat)
        {
            _row[0] = _törzsadat.azonosító;
            _row[1] = _törzsadat.megnevezés_2;
            _row[2] = _törzsadat.megnevezés_3;
        }

        public static bool SameKeys(Törzsadat _1, Törzsadat _2)
        {
            if (_1.azonosító == _2.azonosító) return true;
            return false;
        }

        public static bool SameKeys(Törzsadat _1, DataRow _row)
        {
            if (_1.azonosító == (string)_row[0]) return true;
            return false;
        }

    }

    public sealed class Panel_Törzsadatok : Tokenized_Control<Törzsadat>
    {
        private ComboBox combo_törzsadat;

        #region Constructor
        public Panel_Törzsadatok()
        {
            InitializeContent();
            InitializeTokens();

            KeyDown += Panel_Törzsadatok_KeyDown;
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
            table.UserDeletingRow += table_UserDeletingRow;
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += TörzsadatMódosítás;

            Label label_törzsadat = new Label();
            label_törzsadat.Text = "Törzsadat típusa:";
            label_törzsadat.Location = new Point(table.Width + 50, 15);
            label_törzsadat.AutoSize = true;

            combo_törzsadat = new ComboBox();
            combo_törzsadat.Location = new Point(label_törzsadat.Location.X + 100, label_törzsadat.Location.Y);
            combo_törzsadat.DropDownStyle = ComboBoxStyle.DropDownList;
            List<string> típusok = Program.database.Törzsadat_Típusok();
            foreach (string item in típusok)
            {
                combo_törzsadat.Items.Add(item);
            }
            combo_törzsadat.SelectedIndex = 0;
            combo_törzsadat.SelectedIndexChanged += combo_törzsadat_SelectedIndexChanged;

            table.DataSource = CreateSource();

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Click += TörzsadatTörlés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new System.Drawing.Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            hozzáadás.Click += hozzáadás_Click;

            Controls.Add(table);
            Controls.Add(label_törzsadat);
            Controls.Add(combo_törzsadat);
            Controls.Add(törlés);
            Controls.Add(hozzáadás);
        }

        private DataTable CreateSource()
        {
            data = new DataTable();
            data.Columns.Add(new DataColumn("Magyar", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Angol", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Német", System.Type.GetType("System.String")));

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, Törzsadat _törzsadat) { Törzsadat.SetRow(_row, _törzsadat); }

        protected override bool SameKeys(Törzsadat _1, Törzsadat _2) { return Törzsadat.SameKeys(_1, _2); }

        protected override bool SameKeys(Törzsadat _1, DataRow _row) { return Törzsadat.SameKeys(_1, _row); }

        protected override List<Törzsadat> CurrentData() { return Program.database.Törzsadatok(combo_törzsadat.Text); }
        #endregion

        #region EventHandlers
        private void TörzsadatMódosítás(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;

            Form_Törzsadatok form = new Form_Törzsadatok(new Törzsadat(combo_törzsadat.SelectedItem.ToString(),
                (string)table.SelectedRows[0].Cells[0].Value, (string)table.SelectedRows[0].Cells[1].Value, (string)table.SelectedRows[0].Cells[2].Value));
            form.ShowDialog();

            Program.RefreshData();
        }

        private void TörzsadatTörlés(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count == 1) { if (MessageBox.Show("Biztosan törli a kiválasztott törzsadatot?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            else if (table.SelectedRows.Count != 0) { if (MessageBox.Show("Biztosan törli a kiválasztott törzsadatokat?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            foreach (DataGridViewRow selected in table.SelectedRows)
            {
                string azonosító = (string)selected.Cells[0].Value;
                if (!Program.database.Törzsadat_Törlés(azonosító))
                { MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő törzsadat (" + azonosító + ")?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                Program.RefreshData();
            }
        }

        //

        private void table_DataBindingComplete(object _sender, DataGridViewBindingCompleteEventArgs _event)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 150;
            table.Columns[1].Width = 150;
            table.Columns[2].Width = 150;
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            TörzsadatTörlés(_sender, _event);
        }

        private void hozzáadás_Click(object _sender, EventArgs _event)
        {
            Form_Törzsadatok form = new Form_Törzsadatok(combo_törzsadat.SelectedItem.ToString());
            form.ShowDialog();
        }

        private void Panel_Törzsadatok_KeyDown(object _sender, KeyEventArgs _event)
        {
            if (_event.KeyCode == Keys.Enter) TörzsadatMódosítás(_sender, _event);
        }

        private void combo_törzsadat_SelectedIndexChanged(object _sender, EventArgs _event)
        {
            data.Rows.Clear();
            tokens.Clear();
            Refresh();
        }
        #endregion

        private sealed class Form_Törzsadatok : Form
        {
            private Törzsadat? törzsadat = null;

            private Label label_típus;
            private TextBox box_azonosító;
            private TextBox box_megnevezés2;
            private TextBox box_megnevezés3;

            #region Constructor
            public Form_Törzsadatok(string _típus)
            {
                InitializeForm(_típus, false);
                InitializeContent();
                InitializeData(_típus);
            }

            public Form_Törzsadatok(Törzsadat _eredeti)
            {
                törzsadat = _eredeti;

                InitializeForm(_eredeti.típus, true);
                InitializeContent();
                InitializeData(_eredeti);
            }

            private void InitializeForm(string _típus, bool _módosítás)
            {
                Text = _módosítás ? _típus + " módosítás" : "Új " + _típus + " hozzáadás";
                ClientSize = new Size(400 - 64, 128 + 64);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                Label típus = new Label();
                típus.Text = "Típus:";
                típus.Location = new Point(16, 16 + 0 * 32);

                Label megn1 = new Label();
                megn1.Text = "Magyar:";
                megn1.Location = new Point(típus.Location.X, 16 + 1 * 32);
                megn1.Height = 32;

                Label megn2 = new Label();
                megn2.Text = "Angol:";
                megn2.Location = new Point(típus.Location.X, 16 + 2 * 32);
                megn2.Height = megn1.Height;

                Label megn3 = new Label();
                megn3.Text = "Német:";
                megn3.Location = new Point(típus.Location.X, 16 + 3 * 32);
                megn3.Height = megn1.Height;

                label_típus = new Label();
                label_típus.Location = new Point(típus.Location.X + típus.Width + 16, típus.Location.Y);
                label_típus.Size = new Size(128 + 128, 24);

                box_azonosító = new TextBox();
                box_azonosító.Location = new Point(megn1.Location.X + megn1.Width + 16, megn1.Location.Y);
                box_azonosító.Size = new Size(128 + 64, 34);
                box_azonosító.MaxLength = 25;

                box_megnevezés2 = new TextBox();
                box_megnevezés2.Location = new Point(megn2.Location.X + megn2.Width + 16, megn2.Location.Y);
                box_megnevezés2.Size = box_azonosító.Size;
                box_megnevezés2.MaxLength = 25;

                box_megnevezés3 = new TextBox();
                box_megnevezés3.Location = new Point(megn3.Location.X + megn3.Width + 16, megn3.Location.Y);
                box_megnevezés3.Size = box_azonosító.Size;
                box_megnevezés3.MaxLength = 25;

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += rendben_Click;

                Controls.Add(típus);
                Controls.Add(megn1);
                Controls.Add(megn2);
                Controls.Add(megn3);

                Controls.Add(label_típus);
                Controls.Add(box_azonosító);
                Controls.Add(box_megnevezés2);
                Controls.Add(box_megnevezés3);

                Controls.Add(rendben);
            }

            private void InitializeData(string _típus)
            {
                label_típus.Text = _típus;
            }

            private void InitializeData(Törzsadat _eredeti)
            {
                InitializeData(_eredeti.típus);

                box_azonosító.Text = _eredeti.azonosító;
                box_megnevezés2.Text = _eredeti.megnevezés_2;
                box_megnevezés3.Text = _eredeti.megnevezés_3;
            }
            #endregion

            #region EventHandlers
            private void rendben_Click(object _sender, System.EventArgs _event)
            {
                // Hossz ellenőrzések
                if (box_azonosító.Text.Length == 0 || 25 < box_azonosító.Text.Length) { MessageBox.Show("Nem megfelelő a magyar megnevezés hossza!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (box_megnevezés2.Text.Length == 0 || 25 < box_megnevezés2.Text.Length) { MessageBox.Show("Nem megfelelő az angol megnevezés hossza!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (box_megnevezés3.Text.Length == 0 || 25 < box_megnevezés3.Text.Length) { MessageBox.Show("Nem megfelelő a német megnevezés hossza!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                // SQL text ellenőrzések
                if (!Database.IsCorrectSQLText(box_azonosító.Text)) { MessageBox.Show("Nem megfelelő karakter a magyar megnevezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_megnevezés2.Text)) { MessageBox.Show("Nem megfelelő karakter az angol megnevezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_megnevezés3.Text)) { MessageBox.Show("Nem megfelelő karakter a német megnevezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (törzsadat != null)
                {
                    if (!Program.database.Törzsadat_Módosítás(törzsadat.Value.azonosító, new Törzsadat(label_típus.Text, box_azonosító.Text, box_megnevezés2.Text, box_megnevezés3.Text)))
                    { MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy a módosítandó törzsadat már nem létezik, vagy van már ilyen magyar megnevezés?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }
                else
                {
                    if (!Program.database.Törzsadat_Hozzáadás(new Törzsadat(label_típus.Text, box_azonosító.Text, box_megnevezés2.Text, box_megnevezés3.Text)))
                    { MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy van már ilyen magyar megnevezés?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }

                Close();
            }
            #endregion
        }
    }
}
