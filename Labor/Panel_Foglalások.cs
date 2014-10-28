using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Labor
{
    public struct Hordó
    {
        public string termékkód;
        public string sarzs;
        public string id;
        public int? foglalás_száma;
        public string gyártási_év;
        public decimal mennyiség;
        public string time;

        public Hordó(string _termékkód, string _sarzs, string _id, int? _foglalás_száma, string _gyártási_év, decimal _mennyiség, string _time)
        {
            termékkód = _termékkód;
            sarzs = _sarzs;
            id = _id;
            foglalás_száma = _foglalás_száma;
            gyártási_év = _gyártási_év;
            mennyiség = _mennyiség;
            time = _time;
        }

        public struct TableIndexes
        {
            public const int termékkód = 0;
            public const int sarzs = 1;
            public const int id = 2;
            public const int foglalás_száma = 3;
            public const int gyártási_év = 4;
        }

        public static void SetRow(DataRow _row, Hordó _hordó)
        {
            _row[TableIndexes.termékkód] = _hordó.termékkód;
            _row[TableIndexes.sarzs] = _hordó.sarzs;
            _row[TableIndexes.id] = _hordó.id;
            _row[TableIndexes.foglalás_száma] = _hordó.foglalás_száma == null ? -1 : _hordó.foglalás_száma.Value;
            _row[TableIndexes.gyártási_év] = _hordó.gyártási_év;
        }

        public static bool SameKeys(Hordó _1, Hordó _2)
        {
            if (_1.termékkód == _2.termékkód && _1.id == _2.id && _1.sarzs == _2.sarzs && _1.gyártási_év == _2.gyártási_év) return true;
            return false;
        }

        public static bool SameKeys(Hordó _1, DataRow _row)
        {
            if (_1.termékkód == (string)_row[TableIndexes.termékkód] && _1.id == (string)_row[TableIndexes.id] &&
                _1.sarzs == (string)_row[TableIndexes.sarzs] && _1.gyártási_év == (string)_row[TableIndexes.gyártási_év]) return true;
            return false;
        }
    }

    public struct Sarzs
    {
        public string termékkód;
        public string sarzs;
        public int foglalt;
        public int szabad;

        public Sarzs(string _termékkód, string _sarzs, int _foglalt, int _szabad)
        {
            termékkód = _termékkód;
            sarzs = _sarzs;
            foglalt = _foglalt;
            szabad = _szabad;
        }

        public struct TableIndexes
        {
            public const int termékkód = 0;
            public const int sarzs = 1;
            public const int foglalt = 2;
            public const int szabad = 3;
        }

        public static void SetRow(DataRow _row, Sarzs _sarzs)
        {
            _row[Sarzs.TableIndexes.termékkód] = _sarzs.termékkód;
            _row[Sarzs.TableIndexes.sarzs] = _sarzs.sarzs;
            _row[Sarzs.TableIndexes.foglalt] = _sarzs.foglalt;
            _row[Sarzs.TableIndexes.szabad] = _sarzs.szabad;
        }


        public static bool SameKeys(Sarzs _1, Sarzs _2)
        {
            if (_1.termékkód == _2.termékkód && _1.sarzs == _2.sarzs) return true;
            return false;
        }

        public static bool SameKeys(Sarzs _1, DataRow _row)
        {
            if (_1.termékkód == (string)_row[TableIndexes.termékkód] && _1.sarzs == (string)_row[TableIndexes.sarzs]) return true;
            return false;
        }
    }

    public struct Foglalás
    {
        public string név;
        public int id;
        public int hordók_száma;
        public string típus;
        public string készítő;
        public string idő;

        public Vizsgalap_Szűrő? szűrő;
        public Foglalás(int _id, string _név, int _hordók_száma, string _típus, string _készítő, string _idő)
        {
            név = _név;
            id = _id;
            hordók_száma = _hordók_száma;
            típus = _típus;
            készítő = _készítő;
            idő = _idő;

            szűrő = null;
        }

        public Foglalás(int _id, string _név, int _hordók_száma, string _típus, string _készítő, string _idő, Vizsgalap_Szűrő _szűrő)
        {
            név = _név;
            id = _id;
            hordók_száma = _hordók_száma;
            típus = _típus;
            készítő = _készítő;
            idő = _idő;

            szűrő = _szűrő;
        }

        public struct TableIndexes
        {
            public const int id = 0;
            public const int név = 1;
            public const int hordók_száma = 2;
            public const int típus = 3;
            public const int készítő = 4;
            public const int idő = 5;
        }

        public static void SetRow(DataRow _row, Foglalás _foglalás)
        {
            _row[TableIndexes.id] = _foglalás.id;
            _row[TableIndexes.név] = _foglalás.név;
            _row[TableIndexes.hordók_száma] = _foglalás.hordók_száma;
            _row[TableIndexes.típus] = _foglalás.típus;
            _row[TableIndexes.készítő] = _foglalás.készítő;
            _row[TableIndexes.idő] = _foglalás.idő;
        }

        public static bool SameKeys(Foglalás _1, Foglalás _2)
        {
            if (_1.id == _2.id) return true;
            return false;
        }

        public static bool SameKeys(Foglalás _1, DataRow _row)
        {
            if (_1.id == (int)_row[Foglalás.TableIndexes.id]) return true;
            return false;
        }
    }

    public struct Vizsgalap_Szűrő
    {
        public struct Adatok1
        {
            public string gyümölcsfajta;
            public string hordótípus;
            public string megrendelő;
            public string származási_ország;
            public string műszak_jele;
            public string töltőgép_száma;
            public string termékkód;

            public Adatok1(string _gyümölcsfajta, string _hordótípus, string _megrendelő, string _származási_ország, string _műszak_jele, string _töltőgép_száma, string _termékkód)
            {
                gyümölcsfajta = _gyümölcsfajta;
                hordótípus = _hordótípus;
                megrendelő = _megrendelő;
                származási_ország = _származási_ország;
                műszak_jele = _műszak_jele;
                töltőgép_száma = _töltőgép_száma;
                termékkód = _termékkód;
            }
        }


        public struct Adatok2
        {
            public MinMaxPair<string> sarzs;
            public MinMaxPair<string> hordó_id;

            public MinMaxPair<double?> brix;
            public MinMaxPair<double?> citromsav;
            public MinMaxPair<double?> borkősav;
            public MinMaxPair<double?> ph;
            public MinMaxPair<double?> bostwick;

            public MinMaxPair<Int16?> aszkorbinsav;
            public MinMaxPair<Int16?> nettó_töltet;
            public MinMaxPair<byte?> hőkezelés;
            public MinMaxPair<byte?> citromsav_ad;
            public MinMaxPair<byte?> szita_átmérő;

            public Adatok2(string _min_sarzs, string _max_sarzs, string _min_hordó_id, string _max_hordó_id, double? _min_brix, double? _max_brix, double? _min_citromsav, double? _max_citromsav, double? _min_borkősav, double? _max_borkősav, double? _min_ph, double? _max_ph, double? _min_bostwick,
                double? _max_bostwick, Int16? _min_aszkorbinsav, Int16? _max_aszkorbinsav, Int16? _min_nettó_töltet, Int16? _max_nettó_töltet, byte? _min_hőkezelés, byte? _max_hőkezelés, byte? _min_szita_átmérő, byte? _max_szita_átmérő, byte? _min_citromsav_ad, byte? _max_citromsav_ad)
            {
                sarzs = new MinMaxPair<string>(_min_sarzs, _max_sarzs);
                hordó_id = new MinMaxPair<string>(_min_hordó_id, _max_hordó_id);

                brix = new MinMaxPair<double?>(_min_brix, _max_brix);
                citromsav = new MinMaxPair<double?>(_min_citromsav, _max_citromsav);
                borkősav = new MinMaxPair<double?>(_min_borkősav, _max_borkősav);
                ph = new MinMaxPair<double?>(_min_ph, _max_ph);
                bostwick = new MinMaxPair<double?>(_min_bostwick, _max_bostwick);

                aszkorbinsav = new MinMaxPair<short?>(_min_aszkorbinsav, _max_aszkorbinsav);
                nettó_töltet = new MinMaxPair<short?>(_min_nettó_töltet, _max_nettó_töltet);
                hőkezelés = new MinMaxPair<byte?>(_min_hőkezelés, _max_hőkezelés);
                szita_átmérő = new MinMaxPair<byte?>(_min_szita_átmérő, _max_szita_átmérő);
                citromsav_ad = new MinMaxPair<byte?>(_min_citromsav_ad, _max_citromsav_ad);
            }

            public Adatok2(MinMaxPair<string> _sarzs, MinMaxPair<string> _hordó_id, MinMaxPair<double?> _brix, MinMaxPair<double?> _citromsav, MinMaxPair<double?> _borkősav, MinMaxPair<double?> _ph,
                MinMaxPair<double?> _bostwick, MinMaxPair<Int16?> _aszkorbinsav, MinMaxPair<Int16?> _nettó_töltet, MinMaxPair<byte?> _hőkezelés, MinMaxPair<byte?> _citromsav_ad, MinMaxPair<byte?> _szita_átmérő)
            {
                sarzs = _sarzs;
                hordó_id = _hordó_id;

                brix = _brix;
                citromsav = _citromsav;
                borkősav = _borkősav;
                ph = _ph;
                bostwick = _bostwick;

                aszkorbinsav = _aszkorbinsav;
                nettó_töltet = _nettó_töltet;
                hőkezelés = _hőkezelés;
                citromsav_ad = _citromsav_ad;
                szita_átmérő = _szita_átmérő;
            }
        }

        public Adatok1 adatok1;
        public Adatok2 adatok2;

        public Vizsgalap_Szűrő(Adatok1 _adatok1, Adatok2 _adatok2)
        {
            adatok1 = _adatok1;
            adatok2 = _adatok2;
        }
    }

    public struct Import
    {
        public struct Import_Hordó
        {
            public string sorszám;
            public string termékkód;
            public string gyártási_év;
            public string hordószám;
            public Import_Hordó(string _sorszám,string _termékkód, string _gyártási_év, string _hordószám )
            {
                termékkód = _termékkód;
                gyártási_év = _gyártási_év;
                hordószám = _hordószám;
                sorszám = _sorszám;
            }
        }
        public List<Import_Hordó> import_hordók;
    }

    public sealed class Panel_Foglalások : Tokenized_Control<Foglalás>
    {
        #region Constructor
        public Panel_Foglalások()
        {
            InitializeContent();
            InitializeTokens();

            KeyDown += Panel_Foglalások_KeyDown;
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
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += Foglalás_Módosítás;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Enabled = Program.felhasználó.Value.jogosultságok.Value.foglalások.törlés ? true : false;
            törlés.Click += Foglalás_Törlés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new System.Drawing.Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            hozzáadás.Enabled = Program.felhasználó.Value.jogosultságok.Value.foglalások.hozzáadás ? true : false;
            hozzáadás.Click += Foglalás_Hozzáadás;

            Button feltöltés = new Button();
            feltöltés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            feltöltés.Text = "Feltöltés";
            feltöltés.Size = new System.Drawing.Size(96, 32);
            feltöltés.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y - törlés.Height - 16);
            feltöltés.Enabled = Program.felhasználó.Value.jogosultságok.Value.foglalások.hozzáadás ? true : false;
            feltöltés.Click += Foglalás_Feltöltése;

            Button keresés = new Button();
            keresés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            keresés.Text = "Keresés";
            keresés.Size = new System.Drawing.Size(96, 32);
            keresés.Location = new Point(törlés.Location.X, feltöltés.Location.Y);
            keresés.Click += Vizsgálat_Keresése;

            Controls.Add(table);
            Controls.Add(törlés);
            Controls.Add(hozzáadás);
            Controls.Add(feltöltés);
            Controls.Add(keresés);
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

            return data;
        }
        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, Foglalás _foglalás) { Foglalás.SetRow(_row, _foglalás); }

        protected override bool SameKeys(Foglalás _1, Foglalás _2) { return Foglalás.SameKeys(_1, _2); }

        protected override bool SameKeys(Foglalás _1, DataRow _row) { return Foglalás.SameKeys(_1, _row); }

        protected override List<Foglalás> CurrentData() { return Program.database.Foglalások(); }
        #endregion

        #region EventHandlers
        private void Foglalás_Hozzáadás(object _sender, System.EventArgs _event)
        {
            Foglalás_Hozzáadó foglalás_hozzáadó = new Foglalás_Hozzáadó();
            foglalás_hozzáadó.ShowDialog();

            Program.RefreshData();
        }

        private void Foglalás_Feltöltése(object _sender, EventArgs _event)
        {
            string data = null;
            OpenFileDialog file = new OpenFileDialog();
            if (!(System.IO.Directory.Exists(Path.GetFullPath("IMPORT")))){ Directory.CreateDirectory("IMPORT");}

            file.InitialDirectory = Path.GetFullPath("IMPORT");
            if (file.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                System.IO.StreamReader(file.FileName);
                data = sr.ReadToEnd();
            }
            else return;
            
            Import import = new Import();
            import.import_hordók = new List<Import.Import_Hordó>();
            string[] splitted = data.Split('\r');

            for (int i = 0; i < splitted.Length - 1; i++)
            {
                Import.Import_Hordó hordó = new Import.Import_Hordó(splitted[i].Substring(0, 3), splitted[i].Substring(8, 3), splitted[i].Substring(14, 1), splitted[i].Substring(15, 4));
                import.import_hordók.Add(hordó);
            }

            List<string> hibák = Program.database.Foglalás_Feltöltés_Ellenőrzés(import);

            if (hibák.Count != 0)
            {
                StreamWriter sw;
                if (file.FileName.Length == 0) { return; }
                sw = File.CreateText(file.FileName.Substring(0, file.FileName.Length - 3) + "-hibalista.txt");
                foreach (string item in hibák)
                    sw.WriteLine(item);

                sw.Close();
                MessageBox.Show("Hibalista készült. A hibalista file neve: " + (file.FileName.Substring(0, file.FileName.Length - 3) + "-hibalista.txt"), "Hibalista", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                Foglalás_Feltöltés foglalás_feltöltés = new Foglalás_Feltöltés(file.FileName,import);
                foglalás_feltöltés.ShowDialog();

            }
            Program.RefreshData();
        }

        private void Foglalás_Módosítás(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count != 1) return;

            Foglalás foglalás = new Foglalás((int)table.SelectedRows[0].Cells[Foglalás.TableIndexes.id].Value, (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.név].Value,
                (int)table.SelectedRows[0].Cells[Foglalás.TableIndexes.hordók_száma].Value, (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.típus].Value,
                (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.készítő].Value, (string)table.SelectedRows[0].Cells[Foglalás.TableIndexes.idő].Value);

            Foglalás_Szerkesztő foglalás_szerkesztő = new Foglalás_Szerkesztő(foglalás);
            foglalás_szerkesztő.ShowDialog(this);

            Program.RefreshData();
        }

        private void Foglalás_Törlés(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count == 1) { if (MessageBox.Show("Biztosan törli a kiválasztott foglalást?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            else if (table.SelectedRows.Count != 0) { if (MessageBox.Show("Biztosan törli a kiválasztott foglalást?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            if (!Program.felhasználó.Value.jogosultságok.Value.foglalások.törlés) return;

            foreach (DataGridViewRow selected in table.SelectedRows)
            {
                Foglalás azonosító = new Foglalás((int)selected.Cells[Foglalás.TableIndexes.id].Value, (string)selected.Cells[Foglalás.TableIndexes.név].Value, (int)selected.Cells[Foglalás.TableIndexes.hordók_száma].Value,
                    (string)selected.Cells[Foglalás.TableIndexes.típus].Value, (string)selected.Cells[Foglalás.TableIndexes.készítő].Value, (string)selected.Cells[Foglalás.TableIndexes.idő].Value);


                if (!Program.database.Foglalás_Törlés(azonosító))
                {
                    MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő foglalás?\nID: " + azonosító.id + "\nNév: " + azonosító.név, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                else Program.RefreshData();
            }
        }

        private void Vizsgálat_Keresése(object _sender, EventArgs _event)
        {
            Vizsgálat_Kereső vizsgálat_kereső = new Vizsgálat_Kereső();
            vizsgálat_kereső.ShowDialog(this);

            Program.RefreshData();
        }

        private void table_DataBindingComplete(object _sender, DataGridViewBindingCompleteEventArgs _event)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[Foglalás.TableIndexes.id].Width = 100 - 3;
            table.Columns[Foglalás.TableIndexes.név].Width = 120;
            table.Columns[Foglalás.TableIndexes.hordók_száma].Width = 120;
            table.Columns[Foglalás.TableIndexes.típus].Width = 120;
            table.Columns[Foglalás.TableIndexes.készítő].Width = 120;
            table.Columns[Foglalás.TableIndexes.idő].Width = 120;
        }

        private void Panel_Foglalások_KeyDown(object _sender, KeyEventArgs _event)
        {
            if (_event.KeyCode == Keys.Enter) Foglalás_Módosítás(_sender, _event);
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            Foglalás_Törlés(_sender, _event);
        }
        #endregion

        public sealed class Foglalás_Hozzáadó : Form
        {
            TextBox box_foglalás_neve;
            Label label_foglalás_típusa;
            Label label_készítette;
            Label label_foglalás_ideje;

            #region Constructor
            public Foglalás_Hozzáadó()
            {
                InitializeForm();
                InitializeContent();
            }

            private void InitializeForm()
            {
                Text = "Új Foglalás";
                ClientSize = new Size(400, 250 - 64);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                Label foglalás_neve = MainForm.createlabel("Foglalás neve:", 8, 1 * 32, this);
                Label foglalás_típusa = MainForm.createlabel("Foglalás típusa:", 8, 2 * 32, this);
                Label készítette = MainForm.createlabel("Készítette:", 8, 3 * 32, this);
                Label foglalás_ideje = MainForm.createlabel("Foglalás ideje:", 8, 4 * 32, this);

                box_foglalás_neve = MainForm.createtextbox(foglalás_neve.Location.X + 128, foglalás_neve.Location.Y, 30, 240, this);
                label_foglalás_típusa = MainForm.createlabel("Keresés", box_foglalás_neve.Location.X, foglalás_típusa.Location.Y, this);
                label_készítette = MainForm.createlabel(Program.felhasználó.Value.név1 + " " + Program.felhasználó.Value.név2, box_foglalás_neve.Location.X, készítette.Location.Y, this);
                label_foglalás_ideje = MainForm.createlabel(DateTime.Now.ToString(), box_foglalás_neve.Location.X, foglalás_ideje.Location.Y, this);

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += rendben_Click;

                Controls.Add(rendben);
            }
            #endregion

            #region EventHandlers
            private void rendben_Click(object _sender, EventArgs _event)
            {
                // SQL ellenőrzések
                if (!Database.IsCorrectSQLText(box_foglalás_neve.Text)) { MessageBox.Show("Nem megfelelő karakter a névben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(label_foglalás_típusa.Text)) { MessageBox.Show("Nem megfelelő karakter a típusban!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (!Program.database.Foglalás_Hozzáadás(new Foglalás(0, box_foglalás_neve.Text, 0, label_foglalás_típusa.Text, label_készítette.Text, label_foglalás_ideje.Text)))
                { MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy létezik már ilyen foglalás?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                Close();
            }
            #endregion
        }

        public sealed class Foglalás_Szerkesztő : Tokenized_Form<Hordó>
        {
            private Foglalás foglalás;
            public TextBox box_foglalás_neve;

            #region Constructor
            public Foglalás_Szerkesztő(Foglalás _foglalás)
            {
                foglalás = _foglalás;

                InitializeForm();
                InitializeContent();
                InitializeData();
                InitializeTokens();
            }

            private void InitializeForm()
            {
                Text = "Foglalás adatai";
                ClientSize = new Size(430, 600);
                MinimumSize = ClientSize;
                Location = new Point(0 * (430 + 16), 0);
                StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;

                Load += Foglalás_Szerkesztő_Load;
                FormClosing += Foglalás_Szerkesztő_FormClosing;
            }

            private void InitializeContent()
            {
                table = new DataGridView();
                table.Dock = DockStyle.None;
                table.RowHeadersVisible = false;
                table.AllowUserToResizeRows = false;
                table.AllowUserToResizeColumns = false;
                table.AllowUserToAddRows = false;
                table.Width = 430;
                table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                table.ReadOnly = true;
                table.UserDeletingRow += table_UserDeletingRow;
                table.DataSource = CreateSource();
                table.Location = new Point(0, 100);
                table.Height = 400;
                
                //

                Button törlés = new Button();
                törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                törlés.Text = "Törlés";
                törlés.Size = new System.Drawing.Size(96, 32);
                törlés.Location = new Point(ClientRectangle.Width - törlés.Size.Width - 16, ClientRectangle.Height - törlés.Size.Height - 2 * 16);
                törlés.Enabled = Program.felhasználó.Value.jogosultságok.Value.foglalások.módosítás ? true : false;
                törlés.Click += Hordó_Törlés;

                Button keresés = new Button();
                keresés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                keresés.Text = "Keresés";
                keresés.Size = new System.Drawing.Size(96, 32);
                keresés.Location = new Point(törlés.Location.X - törlés.Width - 16, törlés.Location.Y );
                keresés.Click += Vizsgálat_Keresés;

                Label foglalás_neve = MainForm.createlabel("Foglalás neve:", 10, 30, this);

                box_foglalás_neve = MainForm.createtextbox(foglalás_neve.Location.X + foglalás_neve.Width + 16, foglalás_neve.Location.Y, 30, 200, this);

                //

                Controls.Add(table);

                Controls.Add(törlés);
                Controls.Add(keresés);
                Controls.Add(foglalás_neve);
            }

            private void InitializeData()
            {
                box_foglalás_neve.Text = foglalás.név;
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

            protected override List<Hordó> CurrentData() { return Program.database.Hordók(foglalás); }
            #endregion

            #region EventHandlers
            private void Foglalás_Szerkesztő_Load(object _sender, EventArgs _event)
            {
                table.Columns[Hordó.TableIndexes.termékkód].Width = 430 / 4;
                table.Columns[Hordó.TableIndexes.sarzs].Width = 430 / 4;
                table.Columns[Hordó.TableIndexes.id].Width = 430 / 4;
                table.Columns[Hordó.TableIndexes.foglalás_száma].Visible = false;
                table.Columns[Hordó.TableIndexes.gyártási_év].Width = 430 / 4 - 1;
            }

            private void Foglalás_Szerkesztő_FormClosing(object _sender, FormClosingEventArgs _event)
            {
                if (!Program.felhasználó.Value.jogosultságok.Value.foglalások.módosítás) return;

                if (foglalás.név != box_foglalás_neve.Text )
                {
                    // SQL ellenőrzések
                    if (!Database.IsCorrectSQLText(box_foglalás_neve.Text)) { MessageBox.Show("Nem megfelelő karakter a névben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    Program.database.Foglalás_Módosítás(foglalás, new Foglalás(foglalás.id, box_foglalás_neve.Text, foglalás.hordók_száma,foglalás.típus, foglalás.készítő, foglalás.idő));
                }
            }

            private void Hordó_Törlés(object _sender, EventArgs _event)
            {
                if (table.SelectedRows.Count != 1) return;
                if (!Program.felhasználó.Value.jogosultságok.Value.foglalások.törlés) return;

                if (!Program.database.Hordó_Foglalás(true, foglalás.id, (string)table.SelectedRows[0].Cells[Hordó.TableIndexes.termékkód].Value,
                    (string)table.SelectedRows[0].Cells[Hordó.TableIndexes.sarzs].Value, (string)table.SelectedRows[0].Cells[Hordó.TableIndexes.id].Value))
                { MessageBox.Show("Hiba a hordó lefoglalásakor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                Program.RefreshData();
            }

            private void Vizsgálat_Keresés(object _sender, EventArgs _event)
            {
                foglalás.szűrő = Program.database.Foglalás_Vizsgálat_Szűrő(foglalás);
                if (foglalás.szűrő == null) { MessageBox.Show("Hiba a foglalás szűrőjének lekérdezésekor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                Vizsgálat_Kereső vizsgálat_kereső = new Vizsgálat_Kereső(foglalás);
                vizsgálat_kereső.ShowDialog(this);

                Program.RefreshData();
            }

            private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
            {
                // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                _event.Cancel = true;
                // A saját törlést azért elindítjuk Delete gomb lenyomása után.
                Hordó_Törlés(_sender, _event);
            }
            #endregion
        }

        public sealed class Vizsgálat_Kereső : Form
        {
            private Foglalás? eredeti = null;

            #region Declaration
            TextBox box_termékkód;
            TextBox box_sarzs_min;
            TextBox box_hordó_id_min;
            TextBox box_brix_min;
            TextBox box_citromsav_min;
            TextBox box_borkősav_min;
            TextBox box_ph_min;
            TextBox box_bostwick_min;
            TextBox box_aszkorbinsav_min;
            TextBox box_nettó_töltet_min;
            TextBox box_hőkezelés_min;
            TextBox box_szita_átmérő_min;
            TextBox box_citromsav_ad_min;
            TextBox box_sarzs_max;
            TextBox box_hordó_id_max;
            TextBox box_brix_max;
            TextBox box_citromsav_max;
            TextBox box_borkősav_max;
            TextBox box_ph_max;
            TextBox box_bostwick_max;
            TextBox box_aszkorbinsav_max;
            TextBox box_nettó_töltet_max;
            TextBox box_hőkezelés_max;
            TextBox box_szita_átmérő_max;
            TextBox box_citromsav_ad_max;
            ComboBox combo_gyümölcsfajta;
            ComboBox combo_hordótípus;
            ComboBox combo_megrendelő;
            ComboBox combo_származási_ország;
            TextBox box_műszak_jele;
            TextBox box_töltőgép_száma;
            #endregion

            #region Constructor
            public Vizsgálat_Kereső()
            {
                Text = "Vizsgálat keresés";
                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            public Vizsgálat_Kereső(Foglalás _eredeti)
            {
                eredeti = _eredeti;

                Text = "Vizsgálat keresés " + _eredeti.név + " foglalás számára";
                InitializeForm();
                InitializeContent();
                InitializeData(_eredeti);

                KeyDown += Vizsgálat_Kereső_KeyDown;
            }

            private void InitializeForm()
            {
                ClientSize = new Size(430, 600);
                MinimumSize = ClientSize;
                Location = new Point(1 * (430 + 16), 0);
                StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                int oszlop = 130;
                int sor = 30;

                #region Controls
                Label termékkód = MainForm.createlabel("Termékkód:", 10, 10, this);
                Label sarzs = MainForm.createlabel("Sarzs:", termékkód.Location.X, termékkód.Location.Y + sor, this);
                Label hordószám = MainForm.createlabel("Hordószám:", termékkód.Location.X, sarzs.Location.Y + sor, this);
                Label brix = MainForm.createlabel("Brix %:", termékkód.Location.X, hordószám.Location.Y + sor, this);
                Label citromsav = MainForm.createlabel("Citromsav %:", termékkód.Location.X, brix.Location.Y + sor, this);
                Label borkősav = MainForm.createlabel("Borkősav:", termékkód.Location.X, citromsav.Location.Y + sor, this);
                Label ph = MainForm.createlabel("pH:", termékkód.Location.X, borkősav.Location.Y + sor, this);
                Label bostwick = MainForm.createlabel("Bostwick cm/30sec, 20°C:", termékkód.Location.X, ph.Location.Y + sor, this);
                Label aszkorbinsav = MainForm.createlabel("Aszkorbinsav mg/kg:", termékkód.Location.X, bostwick.Location.Y + sor, this);
                Label nettó_töltet = MainForm.createlabel("Nettó töltet kg:", termékkód.Location.X, aszkorbinsav.Location.Y + sor, this);
                Label hőkezelés = MainForm.createlabel("Hőkezelés °C:", termékkód.Location.X, nettó_töltet.Location.Y + sor, this);
                Label szita_átmérő = MainForm.createlabel("Szita átmérő:", termékkód.Location.X, hőkezelés.Location.Y + sor, this);
                Label citromsav_ad = MainForm.createlabel("Citromsav adagolás mg/kg:", termékkód.Location.X, szita_átmérő.Location.Y + sor, this);
                Label gyümölcsfajta = MainForm.createlabel("Gyümölcsfajta", termékkód.Location.X, citromsav_ad.Location.Y + sor, this);
                Label hordótípus = MainForm.createlabel("Hordótípus:", termékkód.Location.X, gyümölcsfajta.Location.Y + sor, this);
                Label megrendelő = MainForm.createlabel("Megrendelők:", termékkód.Location.X, hordótípus.Location.Y + sor, this);
                Label származási_ország = MainForm.createlabel("Származási ország:", termékkód.Location.X, megrendelő.Location.Y + sor, this);
                Label műszak_jele = MainForm.createlabel("Műszak jele:", termékkód.Location.X, származási_ország.Location.Y + sor, this);
                Label töltőgép_száma = MainForm.createlabel("Töltőgép száma:", termékkód.Location.X, műszak_jele.Location.Y + sor, this);
                #endregion

                #region Boxes
                box_termékkód = MainForm.createtextbox(termékkód.Location.X + oszlop + 20, termékkód.Location.Y, 5, 70, this);
                box_termékkód.Name = "box_termékkód";
                box_sarzs_min = MainForm.createtextbox(box_termékkód.Location.X, sarzs.Location.Y, 5, 70, this);
                box_sarzs_max = MainForm.createtextbox(box_termékkód.Location.X + oszlop, sarzs.Location.Y, 5, 70, this);
                box_hordó_id_min = MainForm.createtextbox(box_termékkód.Location.X, hordószám.Location.Y, 5, 70, this);
                box_hordó_id_max = MainForm.createtextbox(box_sarzs_max.Location.X, hordószám.Location.Y, 5, 70, this);
                box_brix_min = MainForm.createtextbox(box_termékkód.Location.X, brix.Location.Y, 5, 70, this);
                box_brix_max = MainForm.createtextbox(box_sarzs_max.Location.X, brix.Location.Y, 5, 70, this);
                box_citromsav_min = MainForm.createtextbox(box_termékkód.Location.X, citromsav.Location.Y, 7, 70, this);
                box_citromsav_max = MainForm.createtextbox(box_sarzs_max.Location.X, citromsav.Location.Y, 7, 70, this);
                box_borkősav_min = MainForm.createtextbox(box_termékkód.Location.X, borkősav.Location.Y, 7, 70, this);
                box_borkősav_max = MainForm.createtextbox(box_sarzs_max.Location.X, borkősav.Location.Y, 7, 70, this);
                box_ph_min = MainForm.createtextbox(box_termékkód.Location.X, ph.Location.Y, 7, 70, this);
                box_ph_max = MainForm.createtextbox(box_sarzs_max.Location.X, ph.Location.Y, 7, 70, this);
                box_bostwick_min = MainForm.createtextbox(box_termékkód.Location.X, bostwick.Location.Y, 5, 70, this);
                box_bostwick_max = MainForm.createtextbox(box_sarzs_max.Location.X, bostwick.Location.Y, 5, 70, this);
                box_aszkorbinsav_min = MainForm.createtextbox(box_termékkód.Location.X, aszkorbinsav.Location.Y, 5, 70, this);
                box_aszkorbinsav_max = MainForm.createtextbox(box_sarzs_max.Location.X, aszkorbinsav.Location.Y, 5, 70, this);
                box_nettó_töltet_min = MainForm.createtextbox(box_termékkód.Location.X, nettó_töltet.Location.Y, 5, 70, this);
                box_nettó_töltet_max = MainForm.createtextbox(box_sarzs_max.Location.X, nettó_töltet.Location.Y, 5, 70, this);
                box_hőkezelés_min = MainForm.createtextbox(box_termékkód.Location.X, hőkezelés.Location.Y, 5, 70, this);
                box_hőkezelés_max = MainForm.createtextbox(box_sarzs_max.Location.X, hőkezelés.Location.Y, 5, 70, this);
                box_szita_átmérő_min = MainForm.createtextbox(box_termékkód.Location.X, szita_átmérő.Location.Y, 5, 70, this);
                box_szita_átmérő_max = MainForm.createtextbox(box_sarzs_max.Location.X, szita_átmérő.Location.Y, 5, 70, this);
                box_citromsav_ad_min = MainForm.createtextbox(box_termékkód.Location.X, citromsav_ad.Location.Y, 5, 70, this);
                box_citromsav_ad_max = MainForm.createtextbox(box_sarzs_max.Location.X, citromsav_ad.Location.Y, 5, 70, this);

                combo_gyümölcsfajta = MainForm.createcombobox(box_termékkód.Location.X, gyümölcsfajta.Location.Y, 200, this);
                combo_hordótípus = MainForm.createcombobox(box_termékkód.Location.X, hordótípus.Location.Y, 200, this);
                combo_megrendelő = MainForm.createcombobox(box_termékkód.Location.X, megrendelő.Location.Y, 200, this);
                combo_származási_ország = MainForm.createcombobox(box_termékkód.Location.X, származási_ország.Location.Y, 200, this);
                box_műszak_jele = MainForm.createtextbox(box_termékkód.Location.X, műszak_jele.Location.Y, 1, 20, this);
                box_műszak_jele.Name = "műszak";
                box_töltőgép_száma = MainForm.createtextbox(box_termékkód.Location.X, töltőgép_száma.Location.Y, 1, 20, this);
                box_töltőgép_száma.Name = "töltő";
                #endregion

                #region Events
                box_brix_min.KeyPress += MainForm.OnlyNumber;
                box_brix_max.KeyPress += MainForm.OnlyNumber;
                box_citromsav_min.KeyPress += MainForm.OnlyNumber;
                box_citromsav_max.KeyPress += MainForm.OnlyNumber;
                box_ph_min.KeyPress += MainForm.OnlyNumber;
                box_ph_max.KeyPress += MainForm.OnlyNumber;
                box_bostwick_min.KeyPress += MainForm.OnlyNumber;
                box_bostwick_max.KeyPress += MainForm.OnlyNumber;
                box_aszkorbinsav_min.KeyPress += MainForm.OnlyNumber;
                box_aszkorbinsav_max.KeyPress += MainForm.OnlyNumber;
                box_citromsav_min.KeyPress += MainForm.OnlyNumber;
                box_citromsav_max.KeyPress += MainForm.OnlyNumber;
                box_termékkód.Leave += box_termékkód_Leave;
                #endregion

                List<Törzsadat> seged = Program.database.Törzsadatok("Hordótípus");
                foreach (Törzsadat item in seged)
                {
                    combo_hordótípus.Items.Add(item.azonosító);
                }

                seged.Clear();
                seged = Program.database.Törzsadatok("Származási ország");
                foreach (Törzsadat item in seged)
                {
                    combo_származási_ország.Items.Add(item.azonosító);
                }

                 List<string> megrendelok = Program.database.Megrendelők();
                 foreach (string item in megrendelok)
                 {
                     combo_megrendelő.Items.Add(item);
                 }


                Button rendben = new Button();
                rendben.Text = "Szűrés";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += Keresés;

                Controls.Add(rendben);
            }
           
            private void InitializeData()
            {

            }

            private void InitializeData(Foglalás _foglalás)
            {
                box_termékkód.Text = _foglalás.szűrő.Value.adatok1.termékkód;
                box_sarzs_min.Text = _foglalás.szűrő.Value.adatok2.sarzs.min;
                box_sarzs_max.Text = _foglalás.szűrő.Value.adatok2.sarzs.max;
                box_hordó_id_min.Text = _foglalás.szűrő.Value.adatok2.hordó_id.min;
                box_hordó_id_max.Text = _foglalás.szűrő.Value.adatok2.hordó_id.max;
                box_brix_min.Text = _foglalás.szűrő.Value.adatok2.brix.min.ToString();
                box_brix_max.Text = _foglalás.szűrő.Value.adatok2.brix.max.ToString();
                box_citromsav_min.Text = _foglalás.szűrő.Value.adatok2.citromsav.min.ToString();
                box_citromsav_max.Text = _foglalás.szűrő.Value.adatok2.citromsav.max.ToString();
                box_borkősav_min.Text = _foglalás.szűrő.Value.adatok2.borkősav.min.ToString();
                box_borkősav_max.Text = _foglalás.szűrő.Value.adatok2.borkősav.max.ToString();
                box_ph_min.Text = _foglalás.szűrő.Value.adatok2.ph.min.ToString();
                box_ph_max.Text = _foglalás.szűrő.Value.adatok2.ph.max.ToString();
                box_bostwick_min.Text = _foglalás.szűrő.Value.adatok2.bostwick.min.ToString();
                box_bostwick_max.Text = _foglalás.szűrő.Value.adatok2.bostwick.max.ToString();
                box_aszkorbinsav_min.Text = _foglalás.szűrő.Value.adatok2.aszkorbinsav.min.ToString();
                box_aszkorbinsav_max.Text = _foglalás.szűrő.Value.adatok2.aszkorbinsav.max.ToString();
                box_nettó_töltet_min.Text = _foglalás.szűrő.Value.adatok2.nettó_töltet.min.ToString();
                box_nettó_töltet_max.Text = _foglalás.szűrő.Value.adatok2.nettó_töltet.max.ToString();
                box_hőkezelés_min.Text = _foglalás.szűrő.Value.adatok2.hőkezelés.min.ToString();
                box_hőkezelés_max.Text = _foglalás.szűrő.Value.adatok2.hőkezelés.max.ToString();
                box_szita_átmérő_min.Text = _foglalás.szűrő.Value.adatok2.szita_átmérő.min.ToString();
                box_szita_átmérő_max.Text = _foglalás.szűrő.Value.adatok2.szita_átmérő.max.ToString();
                box_citromsav_ad_min.Text = _foglalás.szűrő.Value.adatok2.citromsav_ad.min.ToString();
                box_citromsav_ad_max.Text = _foglalás.szűrő.Value.adatok2.citromsav_ad.max.ToString();

                combo_gyümölcsfajta.Text = _foglalás.szűrő.Value.adatok1.gyümölcsfajta;
                combo_hordótípus.Text = _foglalás.szűrő.Value.adatok1.hordótípus;
                combo_származási_ország.Text = _foglalás.szűrő.Value.adatok1.származási_ország;
                box_töltőgép_száma.Text = _foglalás.szűrő.Value.adatok1.töltőgép_száma;
            }
            #endregion

            #region EventHandlers
            public override void Refresh()
            {
                base.Refresh();

                if (Owner != null) Owner.Refresh();
            }

            private void box_termékkód_Leave(object _sender, EventArgs _event)
            {
                if(combo_gyümölcsfajta.Items.Count != 0) { combo_gyümölcsfajta.Items.Clear(); }

                if(box_termékkód.Text.Length != 3) { return; }
                List<string> gyümölcsfajták = Program.database.Gyümölcsfajták(box_termékkód.Text);
                foreach(string item in gyümölcsfajták) { combo_gyümölcsfajta.Items.Add(item); }
            }

            private void Vizsgálat_Kereső_KeyDown(object _sender, KeyEventArgs _event)
            {
                if (_event.KeyCode == Keys.Enter) Keresés(_sender, _event);
            }

            private void Keresés(object _sender, EventArgs _event)
            {
                if (box_sarzs_min.Text.Length != 0 && box_sarzs_max.Text.Length != 0) if (MainForm.ConvertOrDie<int>(box_sarzs_min.Text) > MainForm.ConvertOrDie<int>(box_sarzs_max.Text)) { MessageBox.Show("Sarzs!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_brix_min.Text.Length != 0 && box_brix_max.Text.Length != 0) if (MainForm.ConvertOrDie<double>(box_brix_min.Text) > MainForm.ConvertOrDie<double>(box_brix_max.Text)) { MessageBox.Show("brix!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_citromsav_min.Text.Length != 0 && box_citromsav_max.Text.Length != 0) if (MainForm.ConvertOrDie<double>(box_citromsav_min.Text) > MainForm.ConvertOrDie<double>(box_citromsav_max.Text)) { MessageBox.Show("citromsav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_borkősav_min.Text.Length != 0 && box_borkősav_max.Text.Length != 0) if (MainForm.ConvertOrDie<double>(box_borkősav_min.Text) > MainForm.ConvertOrDie<double>(box_borkősav_max.Text)) { MessageBox.Show("borkősav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_ph_min.Text.Length != 0 && box_ph_max.Text.Length != 0) if (MainForm.ConvertOrDie<double>(box_ph_min.Text) > MainForm.ConvertOrDie<double>(box_ph_max.Text)) { MessageBox.Show("ph!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_bostwick_min.Text.Length != 0 && box_bostwick_max.Text.Length != 0) if (MainForm.ConvertOrDie<double>(box_bostwick_min.Text) > MainForm.ConvertOrDie<double>(box_bostwick_max.Text)) { MessageBox.Show("bostwick!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_aszkorbinsav_min.Text.Length != 0 && box_aszkorbinsav_max.Text.Length != 0) if (MainForm.ConvertOrDie<int>(box_aszkorbinsav_min.Text) > MainForm.ConvertOrDie<int>(box_aszkorbinsav_max.Text)) { MessageBox.Show("aszkorbinsav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_nettó_töltet_min.Text.Length != 0 && box_nettó_töltet_max.Text.Length != 0) if (MainForm.ConvertOrDie<int>(box_nettó_töltet_min.Text) > MainForm.ConvertOrDie<int>(box_nettó_töltet_max.Text)) { MessageBox.Show("nettó_töltet!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_hőkezelés_min.Text.Length != 0 && box_hőkezelés_max.Text.Length != 0) if (MainForm.ConvertOrDie<int>(box_hőkezelés_min.Text) > MainForm.ConvertOrDie<int>(box_hőkezelés_max.Text)) { MessageBox.Show("hőkezelés!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_citromsav_ad_min.Text.Length != 0 && box_citromsav_ad_max.Text.Length != 0) if (MainForm.ConvertOrDie<int>(box_citromsav_ad_min.Text) > MainForm.ConvertOrDie<int>(box_citromsav_ad_max.Text)) { MessageBox.Show("citromsav_ad!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_szita_átmérő_min.Text.Length != 0 && box_szita_átmérő_max.Text.Length != 0) if (MainForm.ConvertOrDie<int>(box_szita_átmérő_min.Text) > MainForm.ConvertOrDie<int>(box_szita_átmérő_max.Text)) { MessageBox.Show("szita_átmérő!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                Vizsgalap_Szűrő.Adatok1 adatok1 = new Vizsgalap_Szűrő.Adatok1(
                    MainForm.ConvertOrDieSQLString(combo_gyümölcsfajta.Text),
                    MainForm.ConvertOrDieSQLString(combo_hordótípus.Text),
                    MainForm.ConvertOrDieSQLString(combo_megrendelő.Text),
                    MainForm.ConvertOrDieSQLString(combo_származási_ország.Text),
                    MainForm.ConvertOrDieSQLString(box_műszak_jele.Text),
                    MainForm.ConvertOrDieSQLString(box_töltőgép_száma.Text),
                    MainForm.ConvertOrDieSQLString(box_termékkód.Text));

                Vizsgalap_Szűrő.Adatok2 adatok2 = new Vizsgalap_Szűrő.Adatok2(
                    MainForm.ConvertOrDieSQLString(box_sarzs_min.Text),
                    MainForm.ConvertOrDieSQLString(box_sarzs_max.Text),
                    MainForm.ConvertOrDieSQLString(box_hordó_id_min.Text),
                    MainForm.ConvertOrDieSQLString(box_hordó_id_max.Text),
                    MainForm.ConvertOrDie<double>(box_brix_min.Text),
                    MainForm.ConvertOrDie<double>(box_brix_max.Text),
                    MainForm.ConvertOrDie<double>(box_citromsav_min.Text),
                    MainForm.ConvertOrDie<double>(box_citromsav_max.Text),
                    MainForm.ConvertOrDie<double>(box_borkősav_min.Text),
                    MainForm.ConvertOrDie<double>(box_borkősav_max.Text),
                    MainForm.ConvertOrDie<double>(box_ph_min.Text),
                    MainForm.ConvertOrDie<double>(box_ph_max.Text),
                    MainForm.ConvertOrDie<double>(box_bostwick_min.Text),
                    MainForm.ConvertOrDie<double>(box_bostwick_max.Text),
                    MainForm.ConvertOrDie<Int16>(box_aszkorbinsav_min.Text),
                    MainForm.ConvertOrDie<Int16>(box_aszkorbinsav_max.Text),
                    MainForm.ConvertOrDie<Int16>(box_nettó_töltet_min.Text),
                    MainForm.ConvertOrDie<Int16>(box_nettó_töltet_max.Text),
                    MainForm.ConvertOrDie<byte>(box_hőkezelés_min.Text),
                    MainForm.ConvertOrDie<byte>(box_hőkezelés_max.Text),
                    MainForm.ConvertOrDie<byte>(box_citromsav_ad_min.Text),
                    MainForm.ConvertOrDie<byte>(box_citromsav_ad_max.Text),
                    MainForm.ConvertOrDie<byte>(box_szita_átmérő_min.Text),
                    MainForm.ConvertOrDie<byte>(box_szita_átmérő_max.Text));

                if (eredeti == null)
                {
                    Keresés_Eredmény keresés_eredmény = new Keresés_Eredmény(new Vizsgalap_Szűrő(adatok1, adatok2));
                    keresés_eredmény.ShowDialog(this);
                }
                else
                {
                    if (!Program.database.Foglalás_Vizsgálat_Szűrő_Hozzáadás(eredeti.Value, new Vizsgalap_Szűrő(adatok1, adatok2)))
                        { MessageBox.Show("Nem sikerült hozzáadni a foglaláshoz az aktuális vizsgálati lap szűrőt!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; } 

                    Keresés_Eredmény keresés_eredmény = new Keresés_Eredmény(new Vizsgalap_Szűrő(adatok1, adatok2), eredeti.Value);
                    keresés_eredmény.ShowDialog(this);
                }
            }
            #endregion

            public sealed class Keresés_Eredmény : Tokenized_Form<Sarzs>
            {
                private Vizsgalap_Szűrő szűrő;
                private Foglalás? foglalás = null;

                #region Constructor
                public Keresés_Eredmény(Vizsgalap_Szűrő _szűrő)
                {
                    szűrő = _szűrő;

                    InitializeForm();
                    InitializeContent();
                    InitializeTokens();
                }

                public Keresés_Eredmény(Vizsgalap_Szűrő _szűrő, Foglalás _foglalás)
                {
                    szűrő = _szűrő;
                    foglalás = _foglalás;

                    InitializeForm();
                    InitializeContent();
                    InitializeTokens();
                }

                private void InitializeForm()
                {
                    Text = "Keresés eredménye";
                    ClientSize = new Size(4 * 75 + 3, 600);
                    MinimumSize = ClientSize;
                    Location = new Point(2 * (430 + 16), 0);
                    StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                    FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                }

                private void InitializeContent()
                {
                    table = new DataGridView();
                    //table.Dock = DockStyle.Left;
                    table.RowHeadersVisible = false;
                    table.AllowUserToResizeRows = false;
                    table.AllowUserToResizeColumns = false;
                    table.AllowUserToAddRows = false;
                    table.Width = 4 * 75 + 3;
                    table.Height = 500;
                    table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    table.ReadOnly = true;
                    table.DataBindingComplete += table_DataBindingComplete;
                    table.CellDoubleClick += Sarzs_Módosítás;
                    table.UserDeletingRow += table_UserDeletingRow;
                    table.DataSource = CreateSource();

                    //

                    Button rendben = new Button();
                    rendben.Text = "Rendben";
                    rendben.Size = new System.Drawing.Size(96, 32);
                    rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                    rendben.Click += rendben_Click;

                    //

                    Controls.Add(rendben);
                    Controls.Add(table);
                }

                private DataTable CreateSource()
                {
                    data = new DataTable();

                    data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
                    data.Columns.Add(new DataColumn("Sarzs", System.Type.GetType("System.String")));
                    data.Columns.Add(new DataColumn("Foglalt", System.Type.GetType("System.Int32")));
                    data.Columns.Add(new DataColumn("Szabad", System.Type.GetType("System.Int32")));

                    return data;
                }
                #endregion

                #region Tokenizer
                protected override void SetRow(DataRow _row, Sarzs _sarzs) { Sarzs.SetRow(_row, _sarzs); }

                protected override bool SameKeys(Sarzs _1, Sarzs _2) { return Sarzs.SameKeys(_1, _2); }

                protected override bool SameKeys(Sarzs _1, DataRow _row) { return Sarzs.SameKeys(_1, _row); }

                protected override List<Sarzs> CurrentData() { return Program.database.Sarzsok(szűrő); }
                #endregion

                #region EventHandlers
                private void table_DataBindingComplete(object _sender, DataGridViewBindingCompleteEventArgs _event)
                {
                    table.DataBindingComplete -= table_DataBindingComplete;
                    table.Columns[Sarzs.TableIndexes.termékkód].Width = 75;
                    table.Columns[Sarzs.TableIndexes.sarzs].Width = 75;
                    table.Columns[Sarzs.TableIndexes.foglalt].Width = 75;
                    table.Columns[Sarzs.TableIndexes.szabad].Width = 75;
                }

                private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
                {
                    // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                    _event.Cancel = true;
                    // A saját törlést azért elindítjuk Delete gomb lenyomása után.

                    //Vizsgálat_Törlés(_sender, _event);
                }

                private void Sarzs_Módosítás(object _sender, DataGridViewCellEventArgs _event)
                {
                    if (table.SelectedRows.Count != 1) return;

                    Sarzs sarzs = new Sarzs((string)table.SelectedRows[0].Cells[Sarzs.TableIndexes.termékkód].Value, (string)table.SelectedRows[0].Cells[Sarzs.TableIndexes.sarzs].Value,
                        (int)table.SelectedRows[0].Cells[Sarzs.TableIndexes.foglalt].Value, (int)table.SelectedRows[0].Cells[Sarzs.TableIndexes.szabad].Value);

                    Eredmény_Hordók eredmény_hordók;
                    if (foglalás == null) eredmény_hordók = new Eredmény_Hordók(szűrő, sarzs);
                    else eredmény_hordók = new Eredmény_Hordók(szűrő, sarzs, foglalás.Value);
                    eredmény_hordók.ShowDialog(this);

                    Program.RefreshData();
                }

                private void rendben_Click(object _sender, EventArgs _event)
                {
                    Close();
                }
                #endregion

                public sealed class Eredmény_Hordók : Tokenized_Form<Hordó>
                {
                    private Sarzs sarzs;
                    private Vizsgalap_Szűrő szűrő;
                    private Foglalás? foglalás = null;

                    private bool kijelölés_összes;
                    private Label foglalt_hordók;

                    #region Constructor
                    public Eredmény_Hordók(Vizsgalap_Szűrő _szűrő, Sarzs _sarsz)
                    {
                        szűrő = _szűrő;
                        sarzs = _sarsz;

                        InitializeForm();
                        InitializeContent();
                        InitializeTokens();
                    }

                    public Eredmény_Hordók(Vizsgalap_Szűrő _szűrő, Sarzs _sarsz, Foglalás _foglalás)
                    {
                        szűrő = _szűrő;
                        sarzs = _sarsz;
                        foglalás = _foglalás;

                        InitializeForm();
                        InitializeContent();
                        InitializeTokens();

                        Hordók_Számolás();
                    }

                    private void InitializeForm()
                    {
                        Text = "Sarzs(" + sarzs.sarzs + ") hordói";
                        ClientSize = new Size(4 * 75 + 3, 600);
                        MinimumSize = ClientSize;
                        Location = new Point(2 * (430 + 16), 0);
                        StartPosition = Settings.ui_manual_locations ? FormStartPosition.Manual : FormStartPosition.CenterParent;
                        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
                    }

                    private void InitializeContent()
                    {
                        table = new DataGridView();
                        table.Dock = DockStyle.Top;
                        table.RowHeadersVisible = false;
                        table.AllowUserToResizeRows = false;
                        table.AllowUserToResizeColumns = false;
                        table.AllowUserToAddRows = false;
                        table.Width = 300 + 3;
                        table.Height = 500;
                        table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        table.DataBindingComplete += table_DataBindingComplete;
                        table.UserDeletingRow += table_UserDeletingRow;
                        table.CellValueChanged += table_CellValueChanged;
                        table.CellMouseUp += table_CellMouseUp;
                        table.DataSource = CreateSource();

                        //

                        Button rendben = new Button();
                        rendben.Text = "Rendben";
                        rendben.Size = new System.Drawing.Size(96, 32);
                        rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                        rendben.Click += rendben_Click;

                        Controls.Add(rendben);
                        Controls.Add(table);

                        //

                        if (foglalás != null)
                        {
                            Button kijelölés_váltás = new Button();
                            kijelölés_váltás.Text = "Kijelölés váltás";
                            kijelölés_váltás.Size = new System.Drawing.Size(128, 32);
                            kijelölés_váltás.Location = new Point(ClientRectangle.Width - kijelölés_váltás.Size.Width - rendben.Width - 32, ClientRectangle.Height - kijelölés_váltás.Size.Height - 16);
                            kijelölés_váltás.Enabled = Program.felhasználó.Value.jogosultságok.Value.foglalások.módosítás ? true : false;
                            kijelölés_váltás.Click += kijelölés_váltás_Click;

                            Label label_foglalt_hordó = new Label();
                            label_foglalt_hordó.Text = "Foglalt hordó:";
                            label_foglalt_hordó.Location = new Point(8, kijelölés_váltás.Location.Y - 32 - 8);

                            foglalt_hordók = new Label();
                            foglalt_hordók.Text = "0 db";
                            foglalt_hordók.Location = new Point(label_foglalt_hordó.Location.X + label_foglalt_hordó.Size.Width, label_foglalt_hordó.Location.Y);

                            Label vonal = new Label();
                            vonal.Location = new Point(0, label_foglalt_hordó.Location.Y + 26);
                            vonal.Height = 3;
                            vonal.Width = 1000;
                            vonal.BackColor = Color.Black;

                            Controls.Add(kijelölés_váltás);
                            Controls.Add(label_foglalt_hordó);
                            Controls.Add(foglalt_hordók);
                            Controls.Add(vonal);
                        }
                    }

                    private DataTable CreateSource()
                    {
                        data = new DataTable();

                        data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
                        data.Columns.Add(new DataColumn("Hordószám", System.Type.GetType("System.String")));
                        if (foglalás != null)
                        {
                            DataColumn column = new DataColumn("Foglalva", System.Type.GetType("System.Boolean"));
                            column.ReadOnly = Program.felhasználó.Value.jogosultságok.Value.foglalások.módosítás ? false : true;
                            data.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn("Foglalás száma", System.Type.GetType("System.Int32"));
                            column.AllowDBNull = true;
                            data.Columns.Add(column);
                        }

                        return data;
                    }
                    #endregion

                    #region Tokenizer
                    protected override void SetRow(DataRow _row, Hordó _hordó)
                    {
                        if (foglalás != null)
                        {
                            _row[0] = _hordó.termékkód;
                            _row[1] = _hordó.id;
                            _row[2] = _hordó.foglalás_száma == null ? false : true;
                        }
                        else
                        {
                            _row[0] = _hordó.termékkód;
                            _row[1] = _hordó.id;
                            if (_hordó.foglalás_száma == null) _row[2] = DBNull.Value;
                            else _row[2] = _hordó.foglalás_száma.Value;
                        }
                    }

                    protected override bool SameKeys(Hordó _1, Hordó _2)
                    {
                        if (_1.termékkód == _2.termékkód && _1.id == _2.id) return true;
                        return false;
                    }

                    protected override bool SameKeys(Hordó _1, DataRow _row)
                    {
                        if (_1.termékkód == (string)_row[Hordó.TableIndexes.termékkód] && _1.id == (string)_row[1]) return true;
                        return false;
                    }

                    protected override List<Hordó> CurrentData()
                    {
                        if (foglalás != null)
                            return Program.database.Hordók(foglalás.Value, sarzs);
                        else
                            return Program.database.Hordók(sarzs);
                    }

                    public override void Refresh()
                    {
                        base.Refresh();

                        if (foglalás != null) Hordók_Számolás();
                    }
                    #endregion

                    #region EventHandlers
                    private void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
                    {
                        table.DataBindingComplete -= table_DataBindingComplete;

                        table.Columns[0].Width = 100;
                        table.Columns[0].ReadOnly = true;
                        table.Columns[1].Width = 100;
                        table.Columns[1].ReadOnly = true;
                        table.Columns[2].Width = 100;
                        //table.Columns[2].ReadOnly = (foglalás == null) ? true : false;
                    }

                    private void Hordók_Számolás()
                    {
                        int count = 0;
                        kijelölés_összes = true;

                        foreach (DataRow row in data.Rows)
                        {
                            if ((bool)row[2] == true)
                            {
                                ++count;
                                kijelölés_összes = false;
                            }
                        }

                        foglalt_hordók.Text = count + " db";
                    }

                    private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
                    {
                        // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                        _event.Cancel = true;
                        // A saját törlést azért elindítjuk Delete gomb lenyomása után.
                    }

                    private void table_CellValueChanged(object _sender, DataGridViewCellEventArgs _event)
                    {
                        if (foglalás != null)
                        {
                            if (_event.ColumnIndex == 2 && _event.RowIndex != -1)
                            {
                                if (!Program.database.Hordó_Foglalás(!(bool)table.Rows[_event.RowIndex].Cells[_event.ColumnIndex].Value,
                                    foglalás.Value.id, sarzs.termékkód, sarzs.sarzs, (string)table.Rows[_event.RowIndex].Cells[1].Value))
                                { MessageBox.Show("Hiba a hordó lefoglalásakor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                                Program.RefreshData();
                            }
                        }
                    }

                    private void table_CellMouseUp(object _sender, DataGridViewCellMouseEventArgs _event)
                    {
                        // End of edition on each click on column of checkbox
                        if (_event.ColumnIndex == 2 && _event.RowIndex != -1)
                        {
                            table.EndEdit();
                        }
                    }

                    private void kijelölés_váltás_Click(object _sender, EventArgs _event)
                    {
                        foreach(DataGridViewRow row in table.Rows)
                        {
                            if ((bool)row.Cells[2].Value == true)
                            {
                                if (!kijelölés_összes)
                                    if (!Program.database.Hordó_Foglalás(true, foglalás.Value.id, sarzs.termékkód, sarzs.sarzs, (string)row.Cells[1].Value))
                                    { MessageBox.Show("Hiba a hordó lefoglalásakor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            }
                            else
                            {
                                if (kijelölés_összes)
                                    if (!Program.database.Hordó_Foglalás(false, foglalás.Value.id, sarzs.termékkód, sarzs.sarzs, (string)row.Cells[1].Value))
                                    { MessageBox.Show("Hiba a hordó lefoglalásakor!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                            }
                        }

                        Program.RefreshData();
                    }

                    private void rendben_Click(object _sender, EventArgs _event)
                    {
                        Close();
                    }
                    #endregion
                }
            }
        }

        public sealed class Foglalás_Feltöltés : Form
        {
            Import import;
            TextBox box_foglalás_neve;
            Label label_foglalás_típusa;
            Label label_készítette ;
            Label label_foglalás_ideje;
            #region Declaration
            
            #endregion

            #region Constructor
            public Foglalás_Feltöltés(string _filename,Import _import)
            {
                import = _import;
                Text = "Foglalás Feltöltés";
                InitializeForm();
                InitializeContent(_filename);
                InitializeData();
            }


            private void InitializeForm()
            {
                ClientSize = new Size(400, 250 - 64);
                MinimumSize = ClientSize;
                Location = new Point(1 * (430 + 16), 0);
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent(string _filename)
            {
                Label foglalás_neve = MainForm.createlabel("Foglalás neve:", 8, 1 * 32, this);
                Label foglalás_típusa = MainForm.createlabel("Foglalás típusa:", 8, 2 * 32, this);
                Label készítette = MainForm.createlabel("Készítette:", 8, 3 * 32, this);
                Label foglalás_ideje = MainForm.createlabel("Foglalás ideje:", 8, 4 * 32, this);

                box_foglalás_neve = MainForm.createtextbox(foglalás_neve.Location.X + 128, foglalás_neve.Location.Y, 30, 240, this);
                label_foglalás_típusa = MainForm.createlabel("Feltöltés", box_foglalás_neve.Location.X, foglalás_típusa.Location.Y, this);
                label_készítette = MainForm.createlabel(Program.felhasználó.Value.név1 + " " + Program.felhasználó.Value.név2, box_foglalás_neve.Location.X, készítette.Location.Y, this);
                label_foglalás_ideje = MainForm.createlabel(DateTime.Now.ToString(), box_foglalás_neve.Location.X, foglalás_ideje.Location.Y, this);

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += rendben_Click;

                Controls.Add(rendben);
            }

            private void rendben_Click(object sender, EventArgs e)
            {
                // SQL ellenőrzések
                if (!Database.IsCorrectSQLText(box_foglalás_neve.Text)) { MessageBox.Show("Nem megfelelő karakter a névben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (!Program.database.Foglalás_Hozzáadás(new Foglalás(0, box_foglalás_neve.Text, 0, label_foglalás_típusa.Text, label_készítette.Text, label_foglalás_ideje.Text)))
                { MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy létezik már ilyen foglalás?", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                List<Foglalás> foglalások = Program.database.Foglalások();
                List<string> sarzsok = Program.database.Foglalás_Sarzsok(import);


                foreach (Foglalás outer in foglalások)
                {
                    if( outer.név == box_foglalás_neve.Text)
                    {
                        for (int i = 0; i < import.import_hordók.Count; i++)
                        {
                            Program.database.Hordó_Foglalás(false, outer.id, import.import_hordók[i].termékkód, sarzsok[i], import.import_hordók[i].hordószám);
                        }
                    }
                }
                Program.RefreshData();
                Close();
            }


            private void InitializeData()
            {

            }

            private void InitializeData(Foglalás _foglalás)
            {
               
            }
            #endregion
        }
    }
}
