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

        public Felhasználó(string _név1, string _név2, string _beosztás1, string _beosztás2, string _felhasználó_név, string _jelszó, Jogosultságok? _jogosultságok)
        {
            név1 = _név1;
            név2 = _név2;
            beosztás1 = _beosztás1;
            beosztás2 = _beosztás2;
            felhasználó_név = _felhasználó_név;
            jelszó = _jelszó;
            jogosultságok = _jogosultságok;
        }

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

                public Műveletek(bool _hozzáadás, bool _módosítás, bool _törlés)
                {
                    hozzáadás = _hozzáadás;
                    módosítás = _módosítás;
                    törlés = _törlés;
                }
            }

            public Műveletek törzsadatok;
            public Műveletek vizsgálatok;
            public Műveletek foglalások;
            public bool konszignáció_nyomtatás;
            public bool kiszállítások_törlés;
            public Műveletek felhasználók;

            public Jogosultságok(Műveletek _törzsadatok, Műveletek _vizsgálatok, Műveletek _foglalások, bool _konszignáció_nyomtatás, bool _kiszállítások_törlés, Műveletek _felhasználók)
            {
                törzsadatok = _törzsadatok;
                vizsgálatok = _vizsgálatok;
                foglalások = _foglalások;
                konszignáció_nyomtatás = _konszignáció_nyomtatás;
                kiszállítások_törlés = _kiszállítások_törlés;
                felhasználók = _felhasználók;
            }
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
            törlés.Enabled = Program.felhasználó.Value.jogosultságok.Value.felhasználók.törlés ? true : false;
            törlés.Click += Felhasználó_Törlés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new System.Drawing.Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            hozzáadás.Enabled = Program.felhasználó.Value.jogosultságok.Value.felhasználók.hozzáadás ? true : false;
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
            if (!Program.felhasználó.Value.jogosultságok.Value.felhasználók.módosítás) return;

            Felhasználó? felhasználó = Program.database.Felhasználó((string)table.SelectedRows[0].Cells[Felhasználó.TableIndexes.felhasználó_név].Value);
            if (felhasználó == null) { MessageBox.Show("Hiba a felhasználó lekérdezésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Felhasználó_Megjelenítő módosító = new Felhasználó_Megjelenítő(felhasználó.Value);
            módosító.ShowDialog();

            Program.RefreshData();
        }

        private void Felhasználó_Törlés(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;
            if (!Program.felhasználó.Value.jogosultságok.Value.felhasználók.törlés) return;

            if (!Program.database.Felhasználó_Törlés((string)table.SelectedRows[0].Cells[Felhasználó.TableIndexes.felhasználó_név].Value))
                { MessageBox.Show("Hiba a felhasználó törlésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            else Program.RefreshData();
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
            #region Declaration
            private Felhasználó? felhasználó = null;

            private TextBox box_név1;
            private TextBox box_név2;
            private TextBox box_beosztás1;
            private TextBox box_beosztás2;
            private TextBox box_felhasználó_név;
            private TextBox box_jelszó;

            private CheckBox check_törzs_új;
            private CheckBox check_törzs_módosít;
            private CheckBox check_törzs_töröl;

            private CheckBox check_vizsgálat_új;
            private CheckBox check_vizsgálat_módosít;
            private CheckBox check_vizsgálat_töröl;

            private CheckBox check_foglalás_új;
            private CheckBox check_foglalás_módosít;
            private CheckBox check_foglalás_töröl;

            private CheckBox check_konszignáció_nyomtat;

            private CheckBox check_kiszállítások_törlés;

            private CheckBox check_felhasználók_új;
            private CheckBox check_felhasználók_módosít;
            private CheckBox check_felhasználók_töröl;
            #endregion

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
                ClientSize = new Size(400, 600 + 64);
                MaximumSize = ClientSize;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                Text = felhasználó == null ? "Új felhasználó" : felhasználó.Value.név1 + " " + felhasználó.Value.név2;
                StartPosition = FormStartPosition.CenterScreen;
            }

            public void InitializeContent()
            {
                const int offset = 16;
                const int spacer = 24;
                const int group_spacer = 8;
                Tuple<string, int, int>[] labels = new Tuple<string, int, int>[]{
                    new Tuple<string, int, int>("Név", 2, 1),
                    new Tuple<string, int, int>("Beosztás", 2, 1),
                    new Tuple<string, int, int>("Belépési kód", 1, 0),
                    new Tuple<string, int, int>("Jelszó", 2, 1),
                    new Tuple<string, int, int>("Törzsadatok", 2, 1),
                    new Tuple<string, int, int>("Vizsgálatok", 2, 1),
                    new Tuple<string, int, int>("Foglalások", 2, 1),
                    new Tuple<string, int, int>("Konszignáció", 2, 1),
                    new Tuple<string, int, int>("Kiszállítások", 2, 1),
                    new Tuple<string, int, int>("Felhasználók", 2, 1)};

                int count = 0;
                int group = 0;
                for (int current = 0; current < labels.Length; ++current)
                {
                    Label label = MainForm.createlabel(labels[current].Item1 + ":", offset, count * spacer + group * group_spacer + offset, this);
                    count += labels[current].Item2;
                    group += labels[current].Item3;
                }

                //

                const int column = 100;
                box_név1 = MainForm.createtextbox(column, 0 * spacer + 0 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_név2 = MainForm.createtextbox(column, 1 * spacer + 0 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);

                box_beosztás1 = MainForm.createtextbox(column, 2 * spacer + 1 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_beosztás2 = MainForm.createtextbox(column, 3 * spacer + 1 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);

                box_felhasználó_név = MainForm.createtextbox(column, 4 * spacer + 2 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_jelszó = MainForm.createtextbox(column, 5 * spacer + 2 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_jelszó.PasswordChar = '*';

                //

                int[] columns = new int[] { 100, 195, 275 };

                group = 0;
                MainForm.createlabel("Hozzáadás:",   0 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_törzs_új = MainForm.Create_CheckBox(columns[0], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Módosítás:",   1 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_törzs_módosít = MainForm.Create_CheckBox(columns[1], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Törlés:", 2 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_törzs_töröl = MainForm.Create_CheckBox(columns[2], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);

                ++group;
                MainForm.createlabel("Hozzáadás:", 0 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_vizsgálat_új = MainForm.Create_CheckBox(columns[0], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Módosítás:", 1 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_vizsgálat_módosít = MainForm.Create_CheckBox(columns[1], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Törlés:", 2 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_vizsgálat_töröl = MainForm.Create_CheckBox(columns[2], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);

                ++group;
                MainForm.createlabel("Hozzáadás:", 0 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_foglalás_új = MainForm.Create_CheckBox(columns[0], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Módosítás:", 1 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_foglalás_módosít = MainForm.Create_CheckBox(columns[1], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Törlés:", 2 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_foglalás_töröl = MainForm.Create_CheckBox(columns[2], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);

                ++group;
                MainForm.createlabel("Nyomtatás:", 0 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_konszignáció_nyomtat = MainForm.Create_CheckBox(columns[0], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);

                ++group;
                MainForm.createlabel("Törlés:", 2 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_kiszállítások_törlés = MainForm.Create_CheckBox(columns[0], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);

                ++group;
                MainForm.createlabel("Hozzáadás:", 0 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_felhasználók_új = MainForm.Create_CheckBox(columns[0], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Módosítás:", 1 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_felhasználók_módosít = MainForm.Create_CheckBox(columns[1], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                MainForm.createlabel("Törlés:", 2 * 100 + 2 * offset, (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);
                check_felhasználók_töröl = MainForm.Create_CheckBox(columns[2], (8 + 2 * group) * spacer + (3 + group) * group_spacer + offset, this);

                //

                Button rendben = new Button();
                rendben.Size = new Size(96, 32);
                rendben.Location = new Point(ClientSize.Width - rendben.Width - spacer, ClientSize.Height - rendben.Height - spacer);
                rendben.Click += rendben_Click;
                rendben.Text = "Rendben";

                Controls.Add(rendben);
            }

            public void InitializeData()
            {
                if (felhasználó != null)
                {
                    box_név1.Text = felhasználó.Value.név1;
                    box_név2.Text = felhasználó.Value.név2;

                    box_beosztás1.Text = felhasználó.Value.beosztás1;
                    box_beosztás2.Text = felhasználó.Value.beosztás2;

                    box_felhasználó_név.Text = felhasználó.Value.felhasználó_név;
                    box_felhasználó_név.Enabled = false;
                    box_jelszó.Text = felhasználó.Value.jelszó;

                    check_törzs_új.CheckState = felhasználó.Value.jogosultságok.Value.törzsadatok.hozzáadás ? CheckState.Checked : CheckState.Unchecked;
                    check_törzs_módosít.CheckState = felhasználó.Value.jogosultságok.Value.törzsadatok.módosítás ? CheckState.Checked : CheckState.Unchecked;
                    check_törzs_töröl.CheckState = felhasználó.Value.jogosultságok.Value.törzsadatok.törlés ? CheckState.Checked : CheckState.Unchecked;

                    check_vizsgálat_új.CheckState = felhasználó.Value.jogosultságok.Value.vizsgálatok.hozzáadás ? CheckState.Checked : CheckState.Unchecked;
                    check_vizsgálat_módosít.CheckState = felhasználó.Value.jogosultságok.Value.vizsgálatok.módosítás ? CheckState.Checked : CheckState.Unchecked;
                    check_vizsgálat_töröl.CheckState = felhasználó.Value.jogosultságok.Value.vizsgálatok.törlés ? CheckState.Checked : CheckState.Unchecked;

                    check_foglalás_új.CheckState = felhasználó.Value.jogosultságok.Value.foglalások.hozzáadás ? CheckState.Checked : CheckState.Unchecked;
                    check_foglalás_módosít.CheckState = felhasználó.Value.jogosultságok.Value.foglalások.módosítás ? CheckState.Checked : CheckState.Unchecked;
                    check_foglalás_töröl.CheckState = felhasználó.Value.jogosultságok.Value.foglalások.törlés ? CheckState.Checked : CheckState.Unchecked;

                    check_konszignáció_nyomtat.CheckState = felhasználó.Value.jogosultságok.Value.konszignáció_nyomtatás ? CheckState.Checked : CheckState.Unchecked;

                    check_kiszállítások_törlés.CheckState = felhasználó.Value.jogosultságok.Value.kiszállítások_törlés ? CheckState.Checked : CheckState.Unchecked;

                    check_felhasználók_új.CheckState = felhasználó.Value.jogosultságok.Value.felhasználók.hozzáadás ? CheckState.Checked : CheckState.Unchecked;
                    check_felhasználók_módosít.CheckState = felhasználó.Value.jogosultságok.Value.felhasználók.módosítás ? CheckState.Checked : CheckState.Unchecked;
                    check_felhasználók_töröl.CheckState = felhasználó.Value.jogosultságok.Value.felhasználók.törlés ? CheckState.Checked : CheckState.Unchecked;
                }
            }
            #endregion

            #region EventHandlers
            private bool Checked(CheckBox _checkbox)
            {
                return _checkbox.CheckState == CheckState.Checked ? true : false;
            }

            private void rendben_Click(object _sender, EventArgs _event)
            {
                if (!Database.IsCorrectSQLText(box_név1.Text)) { MessageBox.Show("Nem megengedett karakter a név1 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_név2.Text)) { MessageBox.Show("Nem megengedett karakter a név2 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_beosztás1.Text)) { MessageBox.Show("Nem megengedett karakter a beosztás1 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_beosztás2.Text)) { MessageBox.Show("Nem megengedett karakter a beosztás2 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_felhasználó_név.Text)) { MessageBox.Show("Nem megengedett karakter a felhasználó név mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_jelszó.Text)) { MessageBox.Show("Nem megengedett karakter a jelszó mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                Felhasználó.Jogosultságok.Műveletek törzsadat = new Felhasználó.Jogosultságok.Műveletek(Checked(check_törzs_új), Checked(check_törzs_módosít), Checked(check_törzs_töröl));
                Felhasználó.Jogosultságok.Műveletek vizsgálat = new Felhasználó.Jogosultságok.Műveletek(Checked(check_vizsgálat_új), Checked(check_vizsgálat_módosít), Checked(check_vizsgálat_töröl));
                Felhasználó.Jogosultságok.Műveletek foglalás = new Felhasználó.Jogosultságok.Műveletek(Checked(check_foglalás_új), Checked(check_foglalás_módosít), Checked(check_foglalás_töröl));
                Felhasználó.Jogosultságok.Műveletek felhasználók = new Felhasználó.Jogosultságok.Műveletek(Checked(check_felhasználók_új), Checked(check_felhasználók_módosít), Checked(check_felhasználók_töröl));
                Felhasználó.Jogosultságok jogosultságok = new Felhasználó.Jogosultságok(törzsadat, vizsgálat, foglalás, Checked(check_konszignáció_nyomtat), Checked(check_kiszállítások_törlés), felhasználók);
                Felhasználó felhasználó_adatok = new Felhasználó(box_név1.Text, box_név2.Text, box_beosztás1.Text, box_beosztás2.Text, box_felhasználó_név.Text, box_jelszó.Text, jogosultságok);

                if (felhasználó == null)
                {
                    if (!Program.database.Felhasználó_Hozzáadás(felhasználó_adatok)) { MessageBox.Show("Hiba a felhasználó hozzáadása során!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    else Program.RefreshData();
                }
                else
                {
                    if (!Program.database.Felhasználó_Módosítás(felhasználó.Value.felhasználó_név, felhasználó_adatok)) { MessageBox.Show("Hiba a felhasználó módosítása során!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    else Program.RefreshData();
                }

                Close();
            }
            #endregion
        }
    }
}
