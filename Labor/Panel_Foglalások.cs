using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public struct Hordó
    {

    }

    public struct Sarzs
    {

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
            public const int név = 0;
            public const int id = 1;
            public const int hordók_száma = 2;
            public const int típus = 3;
            public const int készítő = 4;
            public const int idő = 5;
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
            public string foglalás_ideje;
            public string foglalás_típusa;
            public string termékkód;

            public Adatok1(string _gyümölcsfajta, string _hordótípus, string _megrendelő, string _származási_ország, string _műszak_jele, string _töltőgép_száma, string _foglalás_ideje, string _foglalás_típusa, string _termékkód)
            {
                gyümölcsfajta = _gyümölcsfajta;
                hordótípus = _hordótípus;
                megrendelő = _megrendelő;
                származási_ország = _származási_ország;
                műszak_jele = _műszak_jele;
                töltőgép_száma = _töltőgép_száma;
                foglalás_ideje = _foglalás_ideje;
                foglalás_típusa = _foglalás_típusa;
                termékkód = _termékkód;
            }
        }
        public struct Adatok2
        {
            public int? min_sarzs;
            public int? max_sarzs;
            public int? min_hordószám;
            public int? max_hordószám;

            public double? min_brix;
            public double? max_brix;
            public double? min_citromsav;
            public double? max_citromsav;
            public double? min_borkősav;
            public double? max_borkősav;
            public double? min_ph;
            public double? max_ph;
            public double? min_bostwick;
            public double? max_bostwick;

            public int? min_aszkorbinsav;
            public int? max_aszkorbinsav;
            public int? min_nettó_töltet;
            public int? max_nettó_töltet;
            public int? min_hőkezelés;
            public int? max_hőkezelés;
            public int? min_citromsav_ad;
            public int? max_citromsav_ad;
            public int? min_szita_átmérő;
            public int? max_szita_átmérő;

            public Adatok2(int? _min_sarzs, int? _max_sarzs, int? _min_hordószám, int? _max_hordószám, double? _min_brix, double? _max_brix, double? _min_citromsav, double? _max_citromsav, double? _min_borkősav, double? _max_borkősav, double? _min_ph, double? _max_ph, double? _min_bostwick,
                double? _max_bostwick, int? _min_aszkorbinsav, int? _max_aszkorbinsav, int? _min_nettó_töltet, int? _max_nettó_töltet, int? _min_hőkezelés, int? _max_hőkezelés, int? _min_szita_átmérő, int? _max_szita_átmérő, int? _min_citromsav_ad, int? _max_citromsav_ad)
            {
                min_sarzs = _min_sarzs;
                max_sarzs = _max_sarzs;
                min_hordószám = _min_hordószám;
                max_hordószám = _max_hordószám;
                min_brix = _min_brix;
                max_brix = _max_brix;
                min_citromsav = _min_citromsav;
                max_citromsav = _max_citromsav;
                min_borkősav = _min_borkősav;
                max_borkősav = _max_borkősav;
                min_ph = _min_ph;
                max_ph = _max_ph;
                min_bostwick = _min_bostwick;
                max_bostwick = _max_bostwick;
                min_aszkorbinsav = _min_aszkorbinsav;
                max_aszkorbinsav = _max_aszkorbinsav;
                min_nettó_töltet = _min_nettó_töltet;
                max_nettó_töltet = _max_nettó_töltet;
                min_hőkezelés = _min_hőkezelés;
                max_hőkezelés = _max_hőkezelés;
                min_szita_átmérő = _min_szita_átmérő;
                max_szita_átmérő = _max_szita_átmérő;
                min_citromsav_ad = _min_citromsav_ad;
                max_citromsav_ad = _max_citromsav_ad;
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


    public sealed class Panel_Foglalások : Control
    {
        private DataTable data;
        private DataGridView table;

        private List<DataToken<Foglalás>> foglalás_tokenek = new List<DataToken<Foglalás>>();

        public Panel_Foglalások()
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
            table.CellDoubleClick += módosítás_Click;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Click += Foglalás_Törlés;

            Button keresés = new Button();
            keresés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            keresés.Text = "Keresés";
            keresés.Size = new System.Drawing.Size(96, 32);
            keresés.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            keresés.Click += Foglalás_Keresése;

            Button feltöltés = new Button();
            feltöltés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            feltöltés.Text = "Feltöltés";
            feltöltés.Size = new System.Drawing.Size(96, 32);
            feltöltés.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y - törlés.Height - 16);
            feltöltés.Click += Foglalás_Feltöltés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new System.Drawing.Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X, feltöltés.Location.Y);
            hozzáadás.Click += hozzáadás_Click;

            Controls.Add(table);
            Controls.Add(törlés);
            Controls.Add(keresés);
            Controls.Add(feltöltés);
            Controls.Add(hozzáadás);
        }

        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Foglalás száma", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Foglalás neve", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Foglalt hordók száma", System.Type.GetType("System.Int32")));
            data.Columns.Add(new DataColumn("Foglalás típusa", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Készítette", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Foglalás ideje", System.Type.GetType("System.String")));

            return data;
        }

        public override void Refresh()
        {
            // Összes adat lekérdezése
            List<Foglalás> foglalások = Program.database.Foglalás_Azonosítók();
            // Minden token beállítása a kereséshez
            foreach (DataToken<Foglalás> token in foglalás_tokenek) { token.type = DataToken<Foglalás>.TokenType.NOT_FOUND; }

            // A már táblán fennlévő tokenek összevetése a lekért adatokkal
            foreach (Foglalás item in foglalások)
            {
                bool found = false;
                foreach (DataToken<Foglalás> token in foglalás_tokenek)
                {
                    if (item.Equals(token.data))
                    {
                        // A megtalált token kivétele a keresésből
                        token.type = DataToken<Foglalás>.TokenType.FOUND;
                        found = true;
                        break;
                    }
                }

                // Még tokenek között nem szereplő adat hozzáadása
                if (!found) foglalás_tokenek.Add(new DataToken<Foglalás>(item));
            }

            // A tábla kiegésszítése a tokenekből származó adatokkal
            List<DataToken<Foglalás>> kitörlendők = new List<DataToken<Foglalás>>();
            foreach (DataToken<Foglalás> token in foglalás_tokenek)
            {
                switch (token.type)
                {
                    case DataToken<Foglalás>.TokenType.NEW:
                        DataRow row = data.NewRow();
                        row[0] = token.data.id;
                        row[1] = token.data.név;
                        row[2] = token.data.hordók_száma;
                        row[3] = token.data.típus;
                        row[4] = token.data.készítő;
                        row[5] = token.data.idő;
                        data.Rows.Add(row);
                        break;

                    case DataToken<Foglalás>.TokenType.NOT_FOUND:
                        foreach (DataRow current in data.Rows)
                        {
                            if (token.data.id == (int)current[0])
                            {
                                data.Rows.Remove(current);
                                kitörlendők.Add(token);
                                break;
                            }
                        }
                        break;
                }
            }

            // Nem talált tokenek kivétele
            foreach (DataToken<Foglalás> token in kitörlendők) { foglalás_tokenek.Remove(token); }
            base.Refresh();
        }


        #region EventHandlers
        private void Foglalás_Törlés(object _sender, EventArgs _event)
        {

        }

        void Foglalás_Feltöltés(object sender, EventArgs e)
        {
        }

        void Foglalás_Keresése(object sender, EventArgs e)
        {

            Foglalás_Keresés foglalás_keresés = new Foglalás_Keresés();
            foglalás_keresés.ShowDialog();
        }

        private void hozzáadás_Click(object _sender, System.EventArgs _event)
        {
            Foglalás_Hozzáadás foglalás_hozzáadás = new Foglalás_Hozzáadás();
            foglalás_hozzáadás.ShowDialog();
        }

        private void módosítás_Click(object sender, DataGridViewCellEventArgs e)
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
            Foglalás_Törlés(_sender, _event);
        }
        #endregion

        public sealed class Foglalás_Hozzáadás : Form
        {
            public Foglalás_Hozzáadás()
            {
                InitializeForm();
                InitializeContent();
            }

            private void InitializeForm()
            {
                Text = "Új Foglalás";
                ClientSize = new Size(400, 250);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                Label foglalás_neve = Program.mainform.createlabel("Foglalás neve:", 8, 1 * 32, this);
                Label foglalás_típusa = Program.mainform.createlabel("Foglalás típusa:", 8, 2 * 32, this);
                Label foglalt_hordók = Program.mainform.createlabel("Foglalt hordók száma:", 8, 3 * 32, this);
                Label készítette = Program.mainform.createlabel("Készítette:", 8, 4 * 32, this);
                Label foglalás_ideje = Program.mainform.createlabel("Foglalás ideje:", 8, 5 * 32, this);

                TextBox box_foglalás_neve = Program.mainform.createtextbox(foglalás_neve.Location.X + 128, foglalás_neve.Location.Y, 10, 160, this);
                Label foglalt_hordók_száma = Program.mainform.createlabel("0", box_foglalás_neve.Location.X, foglalt_hordók.Location.Y, this);
                TextBox box_foglalás_típusa = Program.mainform.createtextbox(box_foglalás_neve.Location.X, foglalás_típusa.Location.Y, 10, 160, this);
                Label label_készítette = Program.mainform.createlabel("Felhasználó", box_foglalás_neve.Location.X, készítette.Location.Y, this);
                Label label_foglalás_ideje = Program.mainform.createlabel(DateTime.Now.ToString(), box_foglalás_neve.Location.X, foglalás_ideje.Location.Y, this);

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += rendben_Click;

                Controls.Add(rendben);
            }

            #region EventHandlers
            private void rendben_Click(object _sender, EventArgs _event)
            {
            }
            #endregion
        }

        public sealed class Foglalás_Szerkesztő : Form
        {
            private DataTable data;
            private DataGridView table;

            private Foglalás foglalás;

            public Foglalás_Szerkesztő(Foglalás _foglalás)
            {
                foglalás = _foglalás;

                InitializeForm();
                InitializeContent();
            }

            private void InitializeForm()
            {
                Text = "Foglalás adatai";
                ClientSize = new Size(400, 250);
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
                table.Width = 720;
                table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                //table.MultiSelect = false;
                table.ReadOnly = true;
                table.DataBindingComplete += table_DataBindingComplete;
                table.UserDeletingRow += table_UserDeletingRow;
                table.DataSource = CreateSource();

                //

                Button törlés = new Button();
                törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                törlés.Text = "Törlés";
                törlés.Size = new System.Drawing.Size(96, 32);
                törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
                törlés.Click += Hordó_Törlés;

                Button keresés = new Button();
                keresés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
                keresés.Text = "Keresés";
                keresés.Size = new System.Drawing.Size(96, 32);
                keresés.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
                keresés.Click += keresés_Click;

                //

                Controls.Add(table);

                Controls.Add(törlés);
                Controls.Add(keresés);
            }

            private DataTable CreateSource()
            {
                data = new DataTable();

                // TODO
                data.Columns.Add(new DataColumn("Foglalás száma", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Foglalás neve", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Foglalt hordók száma", System.Type.GetType("System.Int32")));
                data.Columns.Add(new DataColumn("Foglalás típusa", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Készítette", System.Type.GetType("System.String")));
                data.Columns.Add(new DataColumn("Foglalás ideje", System.Type.GetType("System.String")));

                return data;
            }

            #region EventHandlers
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

            private void Hordó_Törlés(object _sender, EventArgs _event)
            {

            }

            private void keresés_Click(object _sender, EventArgs _event)
            {
                Foglalás_Keresés kereső = new Foglalás_Keresés(foglalás);
                kereső.ShowDialog();
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

        public sealed class Foglalás_Keresés : Form
        {
            #region TextBox
            TextBox box_termékkód;
            TextBox box_min_sarzs;
            TextBox box_min_hordószám;
            TextBox box_min_brix;
            TextBox box_min_citromsav;
            TextBox box_min_borkősav;
            TextBox box_min_ph;
            TextBox box_min_bostwick;
            TextBox box_min_aszkorbinsav;
            TextBox box_min_nettó_töltet;
            TextBox box_min_hőkezelés;
            TextBox box_min_szita_átmérő;
            TextBox box_min_citromsav_ad;
            TextBox box_max_sarzs;
            TextBox box_max_hordószám;
            TextBox box_max_brix;
            TextBox box_max_citromsav;
            TextBox box_max_borkősav;
            TextBox box_max_ph;
            TextBox box_max_bostwick;
            TextBox box_max_aszkorbinsav;
            TextBox box_max_nettó_töltet;
            TextBox box_max_hőkezelés;
            TextBox box_max_szita_átmérő;
            TextBox box_max_citromsav_ad;
            ComboBox combo_gyümölcsfajta;
            ComboBox combo_hordótípus;
            ComboBox combo_megrendelő;
            ComboBox combo_származási_ország;
            TextBox box_műszak_jele;
            TextBox box_töltőgép_száma;
            #endregion

            Foglalás? eredeti = null;

            public Foglalás_Keresés()
            {
                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            public Foglalás_Keresés(Foglalás _eredeti)
            {
                eredeti = _eredeti;
                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            private void InitializeForm()
            {
                Text = "Vizsgálat keresés";
                ClientSize = new Size(500, 588);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                int oszlop = 130;
                int sor = 30;

                #region Controls

                Label termékkód = Program.mainform.createlabel("Termékkód:", 10, 10, this);
                Label sarzs = Program.mainform.createlabel("Sarzs:", termékkód.Location.X, termékkód.Location.Y + sor, this);
                Label hordószám = Program.mainform.createlabel("Hordószám:", termékkód.Location.X, sarzs.Location.Y + sor, this);
                Label brix = Program.mainform.createlabel("Brix %:", termékkód.Location.X, hordószám.Location.Y + sor, this);
                Label citromsav = Program.mainform.createlabel("Citromsav %:", termékkód.Location.X, brix.Location.Y + sor, this);
                Label borkősav = Program.mainform.createlabel("Borkősav:", termékkód.Location.X, citromsav.Location.Y + sor, this);
                Label ph = Program.mainform.createlabel("PH:", termékkód.Location.X, borkősav.Location.Y + sor, this);
                Label bostwick = Program.mainform.createlabel("Bostwick cm/30sec, 20°C:", termékkód.Location.X, ph.Location.Y + sor, this);
                Label aszkorbinsav = Program.mainform.createlabel("Aszkorbinsav mg/kg:", termékkód.Location.X, bostwick.Location.Y + sor, this);
                Label nettó_töltet = Program.mainform.createlabel("Nettó töltet kg:", termékkód.Location.X, aszkorbinsav.Location.Y + sor, this);
                Label hőkezelés = Program.mainform.createlabel("Hőkezelés °C:", termékkód.Location.X, nettó_töltet.Location.Y + sor, this);
                Label szita_átmérő = Program.mainform.createlabel("Szita átmérő:", termékkód.Location.X, hőkezelés.Location.Y + sor, this);
                Label citromsav_ad = Program.mainform.createlabel("Citromsav adagolás mg/kg:", termékkód.Location.X, szita_átmérő.Location.Y + sor, this);
                Label gyümölcsfajta = Program.mainform.createlabel("Gyümölcsfajta", termékkód.Location.X, citromsav_ad.Location.Y + sor, this);
                Label hordótípus = Program.mainform.createlabel("Hordótípus:", termékkód.Location.X, gyümölcsfajta.Location.Y + sor, this);
                Label megrendelő = Program.mainform.createlabel("Megrendelők:", termékkód.Location.X, hordótípus.Location.Y + sor, this);
                Label származási_ország = Program.mainform.createlabel("Származási ország:", termékkód.Location.X, megrendelő.Location.Y + sor, this);
                Label műszak_jele = Program.mainform.createlabel("Műszak jele:", termékkód.Location.X, származási_ország.Location.Y + sor, this);
                Label töltőgép_száma = Program.mainform.createlabel("Töltőgép száma:", termékkód.Location.X, műszak_jele.Location.Y + sor, this);
                #endregion

                #region Boxes
                box_termékkód = Program.mainform.createtextbox(termékkód.Location.X + oszlop + 20, termékkód.Location.Y, 5, 70, this);
                box_termékkód.Name = "box_termékkód";
                box_min_sarzs = Program.mainform.createtextbox(box_termékkód.Location.X, sarzs.Location.Y, 5, 70, this);
                box_max_sarzs = Program.mainform.createtextbox(box_termékkód.Location.X + oszlop, sarzs.Location.Y, 5, 70, this);
                box_min_hordószám = Program.mainform.createtextbox(box_termékkód.Location.X, hordószám.Location.Y, 5, 70, this);
                box_max_hordószám = Program.mainform.createtextbox(box_max_sarzs.Location.X, hordószám.Location.Y, 5, 70, this);
                box_min_brix = Program.mainform.createtextbox(box_termékkód.Location.X, brix.Location.Y, 5, 70, this);
                box_max_brix = Program.mainform.createtextbox(box_max_sarzs.Location.X, brix.Location.Y, 5, 70, this);
                box_min_citromsav = Program.mainform.createtextbox(box_termékkód.Location.X, citromsav.Location.Y, 7, 70, this);
                box_max_citromsav = Program.mainform.createtextbox(box_max_sarzs.Location.X, citromsav.Location.Y, 7, 70, this);
                box_min_borkősav = Program.mainform.createtextbox(box_termékkód.Location.X, borkősav.Location.Y, 7, 70, this);
                box_max_borkősav = Program.mainform.createtextbox(box_max_sarzs.Location.X, borkősav.Location.Y, 7, 70, this);
                box_min_ph = Program.mainform.createtextbox(box_termékkód.Location.X, ph.Location.Y, 7, 70, this);
                box_max_ph = Program.mainform.createtextbox(box_max_sarzs.Location.X, ph.Location.Y, 7, 70, this);
                box_min_bostwick = Program.mainform.createtextbox(box_termékkód.Location.X, bostwick.Location.Y, 5, 70, this);
                box_max_bostwick = Program.mainform.createtextbox(box_max_sarzs.Location.X, bostwick.Location.Y, 5, 70, this);
                box_min_aszkorbinsav = Program.mainform.createtextbox(box_termékkód.Location.X, aszkorbinsav.Location.Y, 5, 70, this);
                box_max_aszkorbinsav = Program.mainform.createtextbox(box_max_sarzs.Location.X, aszkorbinsav.Location.Y, 5, 70, this);
                box_min_nettó_töltet = Program.mainform.createtextbox(box_termékkód.Location.X, nettó_töltet.Location.Y, 5, 70, this);
                box_max_nettó_töltet = Program.mainform.createtextbox(box_max_sarzs.Location.X, nettó_töltet.Location.Y, 5, 70, this);
                box_min_hőkezelés = Program.mainform.createtextbox(box_termékkód.Location.X, hőkezelés.Location.Y, 5, 70, this);
                box_max_hőkezelés = Program.mainform.createtextbox(box_max_sarzs.Location.X, hőkezelés.Location.Y, 5, 70, this);
                box_min_szita_átmérő = Program.mainform.createtextbox(box_termékkód.Location.X, szita_átmérő.Location.Y, 5, 70, this);
                box_max_szita_átmérő = Program.mainform.createtextbox(box_max_sarzs.Location.X, szita_átmérő.Location.Y, 5, 70, this);
                box_min_citromsav_ad = Program.mainform.createtextbox(box_termékkód.Location.X, citromsav_ad.Location.Y, 5, 70, this);
                box_max_citromsav_ad = Program.mainform.createtextbox(box_max_sarzs.Location.X, citromsav_ad.Location.Y, 5, 70, this);

                combo_gyümölcsfajta = Program.mainform.createcombobox(box_termékkód.Location.X, gyümölcsfajta.Location.Y, 200, this);
                combo_hordótípus = Program.mainform.createcombobox(box_termékkód.Location.X, hordótípus.Location.Y, 200, this);
                combo_megrendelő = Program.mainform.createcombobox(box_termékkód.Location.X, megrendelő.Location.Y, 200, this);
                combo_származási_ország = Program.mainform.createcombobox(box_termékkód.Location.X, származási_ország.Location.Y, 200, this);
                box_műszak_jele = Program.mainform.createtextbox(box_termékkód.Location.X, műszak_jele.Location.Y, 1, 20, this);
                box_műszak_jele.Name = "műszak";
                box_töltőgép_száma = Program.mainform.createtextbox(box_termékkód.Location.X, töltőgép_száma.Location.Y, 1, 20, this);
                box_töltőgép_száma.Name = "töltő";
                #endregion

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += Keresés_Rendben;

                Controls.Add(rendben);
            }

            private void Keresés_Rendben(object sender, EventArgs e)
            {
                if (box_min_sarzs.Text.Length != 0 && box_max_sarzs.Text.Length != 0) if (Convert.ToInt32(box_min_sarzs.Text) > Convert.ToInt32(box_max_sarzs.Text)) { MessageBox.Show("Sarzs!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_brix.Text.Length != 0 && box_max_brix.Text.Length != 0) if (Convert.ToInt32(box_min_brix.Text) > Convert.ToInt32(box_max_brix.Text)) { MessageBox.Show("brix!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_citromsav.Text.Length != 0 && box_max_citromsav.Text.Length != 0) if (Convert.ToInt32(box_min_citromsav.Text) > Convert.ToInt32(box_max_citromsav.Text)) { MessageBox.Show("citromsav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_borkősav.Text.Length != 0 && box_max_borkősav.Text.Length != 0) if (Convert.ToInt32(box_min_borkősav.Text) > Convert.ToInt32(box_max_borkősav.Text)) { MessageBox.Show("borkősav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_ph.Text.Length != 0 && box_max_ph.Text.Length != 0) if (Convert.ToInt32(box_min_ph.Text) > Convert.ToInt32(box_max_ph.Text)) { MessageBox.Show("ph!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_bostwick.Text.Length != 0 && box_max_bostwick.Text.Length != 0) if (Convert.ToInt32(box_min_bostwick.Text) > Convert.ToInt32(box_max_bostwick.Text)) { MessageBox.Show("bostwick!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_aszkorbinsav.Text.Length != 0 && box_max_aszkorbinsav.Text.Length != 0) if (Convert.ToInt32(box_min_aszkorbinsav.Text) > Convert.ToInt32(box_max_aszkorbinsav.Text)) { MessageBox.Show("aszkorbinsav!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_nettó_töltet.Text.Length != 0 && box_max_nettó_töltet.Text.Length != 0) if (Convert.ToInt32(box_min_nettó_töltet.Text) > Convert.ToInt32(box_max_nettó_töltet.Text)) { MessageBox.Show("nettó_töltet!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_hőkezelés.Text.Length != 0 && box_max_hőkezelés.Text.Length != 0) if (Convert.ToInt32(box_min_hőkezelés.Text) > Convert.ToInt32(box_max_hőkezelés.Text)) { MessageBox.Show("hőkezelés!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_citromsav_ad.Text.Length != 0 && box_max_citromsav_ad.Text.Length != 0) if (Convert.ToInt32(box_min_citromsav_ad.Text) > Convert.ToInt32(box_max_citromsav_ad.Text)) { MessageBox.Show("citromsav_ad!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_min_szita_átmérő.Text.Length != 0 && box_max_szita_átmérő.Text.Length != 0) if (Convert.ToInt32(box_min_szita_átmérő.Text) > Convert.ToInt32(box_max_szita_átmérő.Text)) { MessageBox.Show("szita_átmérő!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                Vizsgalap_Szűrő.Adatok1 adatok1 = new Vizsgalap_Szűrő.Adatok1
                    (
                    Program.mainform.ConvertOrDieString(combo_gyümölcsfajta.SelectedText),
                    Program.mainform.ConvertOrDieString(combo_hordótípus.SelectedText),
                    Program.mainform.ConvertOrDieString(combo_megrendelő.SelectedText),
                    Program.mainform.ConvertOrDieString(combo_származási_ország.SelectedText),
                    Program.mainform.ConvertOrDieString(box_műszak_jele.Text),
                    Program.mainform.ConvertOrDieString(box_töltőgép_száma.Text),
                    "TODO",
                    "TODO",
                    Program.mainform.ConvertOrDieString(box_termékkód.Text));

                Vizsgalap_Szűrő.Adatok2 adatok2 = new Vizsgalap_Szűrő.Adatok2
                    (
                    Program.mainform.ConvertOrDie<int>(box_min_sarzs.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_sarzs.Text),
                    Program.mainform.ConvertOrDie<int>(box_min_hordószám.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_hordószám.Text),
                    Program.mainform.ConvertOrDie<double>(box_min_brix.Text),
                    Program.mainform.ConvertOrDie<double>(box_max_brix.Text),
                    Program.mainform.ConvertOrDie<double>(box_min_citromsav.Text),
                    Program.mainform.ConvertOrDie<double>(box_max_citromsav.Text),
                    Program.mainform.ConvertOrDie<double>(box_min_borkősav.Text),
                    Program.mainform.ConvertOrDie<double>(box_max_borkősav.Text),
                    Program.mainform.ConvertOrDie<double>(box_min_ph.Text),
                    Program.mainform.ConvertOrDie<double>(box_max_ph.Text),
                    Program.mainform.ConvertOrDie<double>(box_min_bostwick.Text),
                    Program.mainform.ConvertOrDie<double>(box_max_bostwick.Text),
                    Program.mainform.ConvertOrDie<int>(box_min_aszkorbinsav.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_aszkorbinsav.Text),
                    Program.mainform.ConvertOrDie<int>(box_min_nettó_töltet.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_nettó_töltet.Text),
                    Program.mainform.ConvertOrDie<int>(box_min_hőkezelés.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_hőkezelés.Text),
                    Program.mainform.ConvertOrDie<int>(box_min_citromsav_ad.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_citromsav_ad.Text),
                    Program.mainform.ConvertOrDie<int>(box_min_szita_átmérő.Text),
                    Program.mainform.ConvertOrDie<int>(box_max_szita_átmérő.Text));

                if (eredeti == null)
                {
                    Keresés_Eredmény keresés_eredmény = new Keresés_Eredmény(new Vizsgalap_Szűrő(adatok1, adatok2));
                    keresés_eredmény.ShowDialog();
                }
                else
                {
                    Keresés_Eredmény keresés_eredmény = new Keresés_Eredmény(new Vizsgalap_Szűrő(adatok1, adatok2), eredeti.Value);
                    keresés_eredmény.ShowDialog();
                }
            }

            private void InitializeData()
            {
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

                // TODO ne feljts el!
                /* List<string> megrendelok = Program.database.Megrendelők();
                 foreach (string item in megrendelok)
                 {
                     combo_megrendelő.Items.Add(item);
                 }*/
            }

            private void InitializeData(Foglalás _foglalás)
            {

            }

            public sealed class Keresés_Eredmény : Form
            {
                private DataTable data;
                private DataGridView table;

                private Vizsgalap_Szűrő szűrő;
                private Foglalás? foglalás = null;

                public Keresés_Eredmény(Vizsgalap_Szűrő _szűrő)
                {
                    szűrő = _szűrő;

                    InitializeForm();
                    InitializeContent();
                    InitializeData();
                }

                public Keresés_Eredmény(Vizsgalap_Szűrő _szűrő, Foglalás _foglalás)
                {
                    szűrő = _szűrő;
                    foglalás = _foglalás;

                    InitializeForm();
                    InitializeContent();
                    InitializeData();
                }

                private void InitializeForm()
                {
                    Text = "Keresés eredménye";
                    ClientSize = new Size(500, 600);
                    MinimumSize = ClientSize;
                    StartPosition = FormStartPosition.CenterScreen;
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
                    table.Width = 500;
                    table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    //table.MultiSelect = false;
                    table.ReadOnly = true;
                    table.DataBindingComplete += table_DataBindingComplete;
                    table.CellDoubleClick += módosítás_Click;
                    table.UserDeletingRow += table_UserDeletingRow;
                    table.DataSource = CreateSource();

                    // TODO frissítés gomb!

                    Button rögzítés = new Button();
                    rögzítés.Text = "Rögzítés";
                    rögzítés.Size = new System.Drawing.Size(96, 32);
                    rögzítés.Location = new Point(ClientRectangle.Width - rögzítés.Size.Width - 16, ClientRectangle.Height - rögzítés.Size.Height - 16);

                    Button hordók = new Button();
                    hordók.Text = "Hordók";
                    hordók.Size = new System.Drawing.Size(96, 32);
                    hordók.Location = new Point(ClientRectangle.Width - hordók.Size.Width - rögzítés.Width - 32, ClientRectangle.Height - hordók.Size.Height - 16);
                    hordók.Click += hordók_Click;


                    Controls.Add(hordók);
                    Controls.Add(rögzítés);
                    Controls.Add(table);
                }

                void hordók_Click(object sender, EventArgs e)
                {
                    // TODO tábláról kibányászni!
                    Sarzs sarzs;

                    Eredmény_Hordók eredmény_hordók;
                    if (foglalás == null) eredmény_hordók = new Eredmény_Hordók(szűrő, sarzs);
                    else eredmény_hordók = new Eredmény_Hordók(szűrő, sarzs, foglalás.Value);

                    eredmény_hordók.ShowDialog();
                }

                private DataTable CreateSource()
                {
                    data = new DataTable();

                    data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
                    data.Columns.Add(new DataColumn("Sarzs", System.Type.GetType("System.String")));
                    data.Columns.Add(new DataColumn("Foglalt hordó", System.Type.GetType("System.Int32")));
                    data.Columns.Add(new DataColumn("Szabad hordó", System.Type.GetType("System.Int32")));

                    List<Sarzs> sarzsok = Program.database.Sarzsok(szűrő);
                    // TODO WTF

                    return data;
                }

                void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
                {
                    table.DataBindingComplete -= table_DataBindingComplete;
                    table.Columns[0].Width = 125;
                    table.Columns[1].Width = 125;
                    table.Columns[2].Width = 125;
                    table.Columns[3].Width = 125;
                }
                private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
                {
                    // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                    _event.Cancel = true;
                    // A saját törlést azért elindítjuk Delete gomb lenyomása után.

                    //Vizsgálat_Törlés(_sender, _event);
                }

                private void InitializeData()
                {

                }

                private void InitializeData(Foglalás _foglalás)
                {

                }

                public DataGridViewCellEventHandler módosítás_Click { get; set; }

                public sealed class Eredmény_Hordók : Form
                {
                    private DataTable data;
                    private DataGridView table;

                    private Sarzs sarzs;
                    private Vizsgalap_Szűrő szűrő;
                    private Foglalás? foglalás = null;

                    public Eredmény_Hordók(Vizsgalap_Szűrő _szűrő, Sarzs _sarsz)
                    {
                        szűrő = _szűrő;
                        sarzs = _sarsz;

                        InitializeForm();
                        InitializeContent();
                        InitializeData();

                    }
                    public Eredmény_Hordók(Vizsgalap_Szűrő _szűrő, Sarzs _sarsz, Foglalás _foglalás)
                    {
                        szűrő = _szűrő;
                        sarzs = _sarsz;
                        foglalás = _foglalás;

                        InitializeForm();
                        InitializeContent();
                        InitializeData();
                    }

                    private void InitializeForm()
                    {
                        Text = "Hordók";
                        ClientSize = new Size(300, 500);
                        MinimumSize = ClientSize;
                        StartPosition = FormStartPosition.CenterScreen;
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
                        table.Width = 300;
                        table.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        //table.MultiSelect = false;
                        table.ReadOnly = true;
                        table.DataBindingComplete += table_DataBindingComplete;
                        table.UserDeletingRow += table_UserDeletingRow;
                        table.DataSource = CreateSource();


                        Button rendben = new Button();
                        rendben.Text = "Rendben";
                        rendben.Size = new System.Drawing.Size(96, 32);
                        rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                        rendben.Click += rendben_Click;

                        Button kijelölés_megfordítása = new Button();
                        kijelölés_megfordítása.Text = "Kijelölés megfordítása";
                        kijelölés_megfordítása.Size = new System.Drawing.Size(128, 32);
                        kijelölés_megfordítása.Location = new Point(ClientRectangle.Width - kijelölés_megfordítása.Size.Width - rendben.Width - 32, ClientRectangle.Height - kijelölés_megfordítása.Size.Height - 16);
                        kijelölés_megfordítása.Click += kijelölés_megfordítása_Click;

                        Label label_foglalt_hordó = new Label();
                        label_foglalt_hordó.Text = "Foglalt hordó:      ";
                        label_foglalt_hordó.Location = new Point(8, kijelölés_megfordítása.Location.Y - 32 - 8);

                        Label vonal = new Label();
                        vonal.Location = new Point(0, label_foglalt_hordó.Location.Y + 26);
                        vonal.Height = 3;
                        vonal.Width = 1000;
                        vonal.BackColor = Color.Black;


                        Controls.Add(kijelölés_megfordítása);
                        Controls.Add(label_foglalt_hordó);
                        Controls.Add(vonal);
                        Controls.Add(rendben);
                        Controls.Add(table);
                    }

                    private void InitializeData()
                    {

                    }

                    private DataTable CreateSource()
                    {
                        data = new DataTable();

                        data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
                        data.Columns.Add(new DataColumn("Hordószám", System.Type.GetType("System.Int32")));
                        data.Columns.Add(new DataColumn("Foglalt hordó", System.Type.GetType("System.String")));

                        // TODO adatokat felpattintani
                        List<Hordó> hordók = Program.database.Hordók(szűrő, sarzs);

                        if (foglalás != null)
                        {
                            List<Hordó> foglalás_hordói = Program.database.Foglalás_Hordók(foglalás.Value);
                            // TODO lehessen bejelölni utolsó sort, meg kell jeleníteni, és a foglalás_hordóiból kivenni, hogy le van-e már foglalva
                        }

                        return data;
                    }

                    #region EventHandlers
                    private void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
                    {
                        table.DataBindingComplete -= table_DataBindingComplete;
                        table.Columns[0].Width = 100;
                        table.Columns[1].Width = 100;
                        table.Columns[2].Width = 100;
                        if (foglalás != null)
                        {
                            // TODO CheckBoxok megjelenítése
                        }
                    }

                    private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
                    {
                        // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
                        _event.Cancel = true;
                        // A saját törlést azért elindítjuk Delete gomb lenyomása után.

                        //Vizsgálat_Törlés(_sender, _event);
                    }

                    private void rendben_Click(object _sender, EventArgs _event)
                    {
                        if (foglalás != null)
                        {
                            // TODO foglalások elmentése!
                        }
                    }

                    private void kijelölés_megfordítása_Click(object _sender, EventArgs _event)
                    {

                    }
                    #endregion
                }
            }
        }
    }
}
