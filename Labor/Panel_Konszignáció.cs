using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct Konszignáció_Szállítólevél
    {
        public byte szlevél_szám;
        public string szlevél;
        public string fnév;
        public string elszállítás_ideje;
        public string nyelv;
        public string vevő;
        public string gépkocsi1;
        public string gépkocsi2;
        public byte foglalt_hordó;
        public string gyártási_idő;
        public string szín;
        public string íz;
        public string illat;

        public Konszignáció_Szállítólevél(byte _szlevél_szám, string _szlevél, string _fnév, string _elszállítás_ideje, string _nyelv, string _vevő, string _gépkocsi1, string _gépkocsi2, byte _foglalt_hordó, string _gyártási_idő, string _szín, string _íz, string _illat)
        {
            szlevél_szám = _szlevél_szám;
            szlevél = _szlevél;
            fnév = _fnév;
            elszállítás_ideje = _elszállítás_ideje;
            nyelv = _nyelv;
            vevő = _vevő;
            gépkocsi1 = _gépkocsi1;
            gépkocsi2 = _gépkocsi2;
            foglalt_hordó = _foglalt_hordó;
            gyártási_idő = _gyártási_idő;
            szín = _szín;
            íz = _íz;
            illat = _illat;
        }
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

    public sealed class Panel_Konszignáció : Tokenized_Control<Foglalás>
    {
        #region Constructor
        public Panel_Konszignáció()
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
            table.Width = 750;
            table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            table.DataSource = CreateSource();
            table.DataBindingComplete += table_DataBindingComplete;
            table.UserDeletingRow += table_UserDeletingRow;
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
            nyomtatás.Enabled = Program.felhasználó.Value.jogosultságok.Value.konszignáció_nyomtatás ? true : false;
            nyomtatás.Click += nyomtatás_Click;

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
            data.Columns.Add(new DataColumn("Kijelölés", System.Type.GetType("System.Boolean")));

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, Foglalás _foglalás) { Foglalás.SetRow(_row, _foglalás); }

        protected override bool SameKeys(Foglalás _1, Foglalás _2) { return Foglalás.SameKeys(_1, _2); }

        protected override bool SameKeys(Foglalás _1, DataRow _row) { return Foglalás.SameKeys(_1, _row); }

        protected override List<Foglalás> CurrentData() { return Program.database.Konszingnáció_Foglalások(); }
        #endregion

        #region EventHandlers
        private void nyomtatás_Click(object _sender, EventArgs _event)
        {
            List<Foglalás> foglalások = new List<Foglalás>();

            foreach (DataGridViewRow item in table.Rows)
            {
                if( item.Cells[6].Value.ToString() != "" && Convert.ToBoolean(item.Cells[6].Value)== true)
                {
                    foglalások.Add(new Foglalás((int)item.Cells[Foglalás.TableIndexes.id].Value, (string)item.Cells[Foglalás.TableIndexes.név].Value,
                (int)item.Cells[Foglalás.TableIndexes.hordók_száma].Value, (string)item.Cells[Foglalás.TableIndexes.típus].Value,
                (string)item.Cells[Foglalás.TableIndexes.készítő].Value, (string)item.Cells[Foglalás.TableIndexes.idő].Value));
                }
            }

            if(foglalások.Count!=0)
            {
                Konszignáció_Nyomtatás nyomtató = new Konszignáció_Nyomtatás(foglalások);
                nyomtató.ShowDialog();
            }

            Program.RefreshData();
        }

        private void table_CellMouseUp(object _sender, DataGridViewCellMouseEventArgs _event)
        {
            // End of edition on each click on column of checkbox
            if (_event.ColumnIndex == 6 && _event.RowIndex != -1)
            {
                table.EndEdit();
            }
        }
     
        private void hordók_Click(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;

            Foglalás foglalás = new Foglalás((int)table.SelectedRows[0].Cells[Foglalás.TableIndexes.id].Value, (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.név].Value,
                (int)table.SelectedRows[0].Cells[Foglalás.TableIndexes.hordók_száma].Value, (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.típus].Value,
                (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.készítő].Value, (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.idő].Value);

            Konszignáció_Hordók hordó_megjelenítő = new Konszignáció_Hordók(foglalás);
            hordó_megjelenítő.ShowDialog(this);

            Program.RefreshData();
        }

        private void table_DataBindingComplete(object _sender, DataGridViewBindingCompleteEventArgs _event)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[0].Width = 100 - 3;
            table.Columns[0].ReadOnly = true;
            table.Columns[1].Width = 120;
            table.Columns[1].ReadOnly = true;
            table.Columns[2].Width = 120;
            table.Columns[2].ReadOnly = true;
            table.Columns[3].Width = 120;
            table.Columns[3].ReadOnly = true;
            table.Columns[4].Width = 120;
            table.Columns[4].ReadOnly = true;
            table.Columns[5].Width = 120;
            table.Columns[5].ReadOnly = true;
            table.Columns[6].Width = 50;
            table.Columns[6].ReadOnly = false;
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            _event.Cancel = true;
        }
        #endregion

        public sealed class Konszignáció_Nyomtatás : Form
        {
            ComboBox combo_megrendelők;
            ComboBox combo_nyelv;
            TextBox box_rendszám1;
            TextBox box_rendszám2;
            TextBox box_levél;
            TextBox box_gyártási_idő;
            TextBox box_szín;
            TextBox box_íz;
            TextBox box_illat;

            List<Foglalás> foglalások;

            #region Constructor
            public Konszignáció_Nyomtatás(List<Foglalás> _foglalások)
            {
                foglalások = _foglalások;

                InitializeForm();
                InitializeContent();
            }

            private void InitializeForm()
            {
                Text = "Nyomtatás";
                ClientSize = new Size(320, 368);
                StartPosition = FormStartPosition.CenterParent;
                MinimumSize = ClientSize;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                Label label_nyelv;
                Label label_vevő;
                Label label_gépkocsi;
                Label label_szállítólevél;
                Label label_gyártási_idő;
                Label label_szín;
                Label label_íz;
                Label label_illat;
                Label vonal = new Label();
                vonal.Height = 3;
                vonal.Width = 1000;
                vonal.BackColor = Color.Gray;

                Label vonal2 = new Label();
                vonal2.Height = 3;
                vonal2.Width = 1000;
                vonal2.BackColor = Color.Gray;

                Button rendben;

                label_nyelv = MainForm.createlabel("Nyelv:", 16, 16 + 0 * 32, this);
                vonal.Location = new Point(label_nyelv.Location.X - label_nyelv.Width, label_nyelv.Location.Y + 32);
                Controls.Add(vonal);

                label_vevő = MainForm.createlabel("Vevő:", 16, 32 + 1 * 32, this);
                label_gépkocsi = MainForm.createlabel("Gépkocsi:", 16, 32 + 2 * 32, this);
                label_szállítólevél = MainForm.createlabel("Szállítólevél:", 16, 32 + 3 * 32, this);

                vonal2.Location = new Point(label_szállítólevél.Location.X - label_szállítólevél.Width, label_szállítólevél.Location.Y + 32);
                Controls.Add(vonal2);


                label_gyártási_idő = MainForm.createlabel("Gyártási idő:", 16, 32 + 5 * 32, this);
                label_szín = MainForm.createlabel("Szín:", 16, 32 + 6 * 32, this);
                label_íz = MainForm.createlabel("Íz:", 16, 32 + 7 * 32, this);
                label_illat = MainForm.createlabel("Illat:", 16, 32 + 8 * 32, this);

                combo_nyelv = MainForm.createcombobox(label_nyelv.Location.X + 48 + label_nyelv.Width, label_nyelv.Location.Y, 200, this);
                combo_megrendelők = MainForm.createcombobox(combo_nyelv.Location.X, label_vevő.Location.Y, 200, this);
                
                box_rendszám1 = MainForm.createtextbox(combo_nyelv.Location.X, label_gépkocsi.Location.Y, 20, 70, this);
                box_rendszám2 = MainForm.createtextbox(box_rendszám1.Location.X + box_rendszám1.Width + 8, label_gépkocsi.Location.Y, 20, 70, this);
                box_levél = MainForm.createtextbox(combo_nyelv.Location.X, label_szállítólevél.Location.Y, 20, 70, this);
                box_gyártási_idő = MainForm.createtextbox(combo_nyelv.Location.X, label_gyártási_idő.Location.Y, 20, 70, this);
                box_szín = MainForm.createtextbox(combo_nyelv.Location.X, label_szín.Location.Y, 20, 70, this);
                box_íz = MainForm.createtextbox(combo_nyelv.Location.X, label_íz.Location.Y, 20, 70, this);
                box_illat = MainForm.createtextbox(combo_nyelv.Location.X, label_illat.Location.Y, 20, 70, this);

                rendben = new Button();
                rendben.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(150, 300);

                combo_nyelv.Items.Add("Magyar"); combo_nyelv.Items.Add("Angol"); combo_nyelv.Items.Add("3. label_nyelv"); combo_nyelv.SelectedIndex = 0;

                List<string> megrendelok = Program.database.Megrendelők();
                foreach (string item in megrendelok) { combo_megrendelők.Items.Add(item); } combo_megrendelők.SelectedIndex = 0;

                /*csak tesztelés miatt!
                */
                box_rendszám1.Text = "KTM791";
                box_rendszám2.Text = "HCS850";
                box_levél.Text = "levél";
                box_gyártási_idő.Text = "2014.04.20";
                box_szín.Text = "Szín";
                box_íz.Text = "Íz";
                box_illat.Text = "Illat";

                rendben.Click += rendben_Click;
                Controls.Add(rendben);
            }
            #endregion

            #region EventHandlers
            private void rendben_Click(object _sender, EventArgs _event)
            {
                //TODO check jó-é, gyártási idő??
                string date = DateTime.Now.Year.ToString() + '.'+ DateTime.Now.Month + '.' + DateTime.Now.Day;

                Konszignáció_Szállítólevél szállítólevél = new Konszignáció_Szállítólevél(0, box_levél.Text, foglalások[0].készítő, date, combo_nyelv.Text[0].ToString(), combo_megrendelők.Text, box_rendszám1.Text, box_rendszám2.Text, (byte)foglalások[0].hordók_száma, "??", box_szín.Text, box_íz.Text, box_illat.Text);
                Nyomtat.Nyomtat_Konszignáció(szállítólevél, foglalások);
                //Program.database.Konszignáció_FoglalásokKiszállítása(szállítólevél.szlevél_szám, foglalások);
                 szállítólevél.szlevél_szám =  Convert.ToByte( Program.database.Konszignáció_ÚJSzállítólevél(szállítólevél));

                 //Nyomtat.Nyomtat_MinőségBizonylatok(szállítólevél, foglalások);
                 
                Close();
            }
            #endregion
        }

        public sealed class Konszignáció_Hordók : Tokenized_Form<Hordó>
        {
            private Foglalás foglalás;

            #region Constructor
            public Konszignáció_Hordók(Foglalás _foglalás)
            {
                foglalás = _foglalás;

                InitializeForm();
                InitializeContent();
                InitializeTokens();
            }

            private void InitializeForm()
            {
                Text = "Hordók megtekintése";
                ClientSize = new Size(430, 568);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

                Load += Konszignáció_Hordók_Load;
            }

            private void InitializeContent()
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

            private DataTable CreateSource()
            {
                data = new DataTable();

                data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Sarzs", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Hordó száma", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Foglalás száma", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Gyártási év", System.Type.GetType("System.String")));

                return data;
            }
            #endregion

            #region Tokenizer
            protected override void SetRow(DataRow _row, Hordó _hordó) { Hordó.SetRow(_row, _hordó); }

            protected override bool SameKeys(Hordó _1, Hordó _2) { return Hordó.SameKeys(_1, _2); }

            protected override bool SameKeys(Hordó _1, DataRow _row) { return Hordó.SameKeys(_1, _row); }

            protected override List<Hordó> CurrentData() { return Program.database.Foglalás_Hordók(foglalás); }
            #endregion

            #region EventHandlers
            private void Konszignáció_Hordók_Load(object _sender, EventArgs _event)
            {
                table.Columns[Hordó.TableIndexes.termékkód].Width = 430 / 4;
                table.Columns[Hordó.TableIndexes.sarzs].Width = 430 / 4;
                table.Columns[Hordó.TableIndexes.id].Width = 430 / 4;
                table.Columns[Hordó.TableIndexes.foglalás_száma].Visible = false;
                table.Columns[Hordó.TableIndexes.gyártási_év].Width = 430 / 4 - 1;
            }

            private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
            {
                _event.Cancel = true;
            }
            #endregion
        }
    }
}
