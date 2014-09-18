using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Labor
{
    public struct Vizsgálat
    {
        public struct Azonosító
        {
            public string termékkód;
            public string sarzs;
            public string hordószám;
            public string hordótípus;
            public double nettó_töltet;
            public byte szita_átmérő;
            public string megrendelő;
            public int? sorszám;
            public int? foglalás;

            public Azonosító(string _termékkód, string _sarzs, string _hordószám, string _hordótípus, double _nettó_töltet, byte _szita_átmérő, string _megrendelő, int? _sorszám, int? _foglalás)
            {
                termékkód = _termékkód;
                sarzs = _sarzs;
                hordószám = _hordószám;
                hordótípus = _hordótípus;
                nettó_töltet = _nettó_töltet;
                szita_átmérő = _szita_átmérő;
                megrendelő = _megrendelő;
                sorszám = _sorszám;
                foglalás = _foglalás;
            }

            public struct TableIndexes
            {
                public const int termékkód = 0;
                public const int sarzs = 1;
                public const int hordószám = 2;
                public const int hordótípus = 3;
                public const int nettó_töltet = 4;
                public const int szita_átmérő = 5;
                public const int megrendelő = 6;
                public const int sorszám = 7;
                public const int foglalás = 8;
            }
        }

        public struct Adatok1
        {
            public string terméknév;
            public byte hőkezelés;
            public string gyártási_év;
            public string műszak_jele;
            public string töltőgép;
            public string szárm_ország;
            public string gyümölcsfajta;

            public Adatok1(string _terméknév, byte _hőkezelés, string _gyártási_év, string _műszak_jele, string _töltőgép, string _szárm_ország, string _gyümölcsfajta)
            {
                terméknév = _terméknév;
                hőkezelés = _hőkezelés;
                műszak_jele = _műszak_jele;
                töltőgép = _töltőgép;
                szárm_ország = _szárm_ország;
                gyártási_év = _gyártási_év;
                gyümölcsfajta = _gyümölcsfajta;
            }
        }

        public struct Adatok2
        {
            public double? brix;
            public double? citromsav;
            public double? borkősav;
            public double? ph;
            public double? bostwick;
            public byte? aszkorbinsav;
            public byte? citromsav_adagolás;
            public byte? magtöret;
            public byte? feketepont;
            public byte? barnapont;
            public string szín;
            public string íz;
            public string illat;
            public Adatok2(double? _brix, double? _citromsav, double? _borkősav, double? _ph, double? _bostwick, byte? _aszkorbinsav, byte? _citromsav_adagolás, byte? _magtöret, byte? _feketepont, byte? _barnapont, string _szín, string _íz, string _illat)
            {
                brix = _brix;
                citromsav = _citromsav;
                borkősav = _borkősav;
                ph = _ph;
                bostwick = _bostwick;
                aszkorbinsav = _aszkorbinsav;
                citromsav_adagolás = _citromsav_adagolás;
                magtöret = _magtöret;
                feketepont = _feketepont;
                barnapont = _barnapont;
                szín = _szín;
                íz = _íz;
                illat = _illat;
            }
        }

        public struct Adatok3
        {
            public string leoltás;
            public string értékelés;
            public byte? összcsíra_1;
            public byte? összcsíra_2;
            public byte? penész_1;
            public byte? penész_2;
            public byte? élesztő_1;
            public byte? élesztő_2;
            public string megjegyzés;
            public Adatok3(string _leoltás, string _értékelés, byte? _összcsíra_1, byte? _összcsíra_2, byte? _penész_1, byte? _penész_2, byte? _élesztő_1, byte? _élesztő_2, string _megjegyzés)
            {
                leoltás = _leoltás;
                értékelés = _értékelés;
                összcsíra_1 = _összcsíra_1;
                összcsíra_2 = _összcsíra_2;
                penész_1 = _penész_1;
                penész_2 = _penész_2;
                élesztő_1 = _élesztő_1;
                élesztő_2 = _élesztő_2;
                megjegyzés = _megjegyzés;
            }
        }

        public struct Adatok4
        {
            public string címzett_t;
            public string dátum_t;
            public string címzett_k1;
            public string dátum_k1;
            public string címzett_k2;
            public string dátum_k2;
            public string címzett_k3;
            public string dátum_k3;
            public string címzett_k4;
            public string dátum_k4;
            public string címzett_k5;
            public string dátum_k5;
            public string címzett_k6;
            public string dátum_k6;
            public string laboros;

            public Adatok4(string _címzett_t, string _dátum_t, string _címzett_k1, string _dátum_k1, string _címzett_k2, string _dátum_k2, string _címzett_k3, string _dátum_k3, string _címzett_k4, string _dátum_k4, string _címzett_k5, string _dátum_k5, string _címzett_k6, string _dátum_k6, string _laboros)
            {
                címzett_t = _címzett_t;
                dátum_t = _dátum_t;
                címzett_k1 = _címzett_k1;
                dátum_k1 = _dátum_k1;
                címzett_k2 = _címzett_k2;
                dátum_k2 = _dátum_k2;
                címzett_k3 = _címzett_k3;
                dátum_k3 = _dátum_k3;
                címzett_k4 = _címzett_k4;
                dátum_k4 = _dátum_k4;
                címzett_k5 = _címzett_k5;
                dátum_k5 = _dátum_k5;
                címzett_k6 = _címzett_k6;
                dátum_k6 = _dátum_k6;
                laboros = _laboros;
            }
        }

        public Azonosító azonosító;
        public Adatok1 adatok1;
        public Adatok2 adatok2;
        public Adatok3 adatok3;
        public Adatok4 adatok4;

        public Vizsgálat(Azonosító _azonosító, Adatok1 _adatok1, Adatok2 _adatok2, Adatok3 _adatok3, Adatok4 _adatok4)
        {
            azonosító = _azonosító;
            adatok1 = _adatok1;
            adatok2 = _adatok2;
            adatok3 = _adatok3;
            adatok4 = _adatok4;
        }
    }

    public sealed class Panel_Vizsgálatok : Control
    {
        private DataTable data;
        private DataGridView table;
        private TextBox box_termékkód;

        private List<DataToken<Vizsgálat.Azonosító>> vizsgálat_tokenek = new List<DataToken<Vizsgálat.Azonosító>>();

        public Panel_Vizsgálatok()
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
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += Vizsgálat_Módosítás;
            table.UserDeletingRow += table_UserDeletingRow;
            table.DataSource = CreateSource();

            //

            Label label_zsák = new Label();
            label_zsák.Text = "Termékkód:";
            label_zsák.Location = new Point(table.Width + 50, 15);
            label_zsák.AutoSize = true;

            box_termékkód = new TextBox();
            box_termékkód.Location = new Point(label_zsák.Location.X + 100, label_zsák.Location.Y);

            //

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new System.Drawing.Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Click += Vizsgálat_Törlés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new System.Drawing.Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            hozzáadás.Click += Vizsgálat_Hozzáadás;

            //

            Controls.Add(table);
            Controls.Add(törlés);
            Controls.Add(hozzáadás);
            Controls.Add(label_zsák);
            Controls.Add(box_termékkód);
        }

        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Termékkód", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Sarzs", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Hordószám", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Hordótípus", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Nettó töltet", System.Type.GetType("System.Double")));
            data.Columns.Add(new DataColumn("Szitaátmérő", System.Type.GetType("System.Byte")));
            data.Columns.Add(new DataColumn("Megrendelő", System.Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Sorszám", System.Type.GetType("System.Int32")));
            DataColumn column = new DataColumn("Foglalás", System.Type.GetType("System.Int32"));
            column.AllowDBNull = true;
            data.Columns.Add(column);

            List<Vizsgálat.Azonosító> vizsgálatok = Program.database.Vizsgálatok();

            foreach (Vizsgálat.Azonosító item in vizsgálatok)
            {
                DataRow row = data.NewRow();
                row[Vizsgálat.Azonosító.TableIndexes.termékkód] = item.termékkód;
                row[Vizsgálat.Azonosító.TableIndexes.sarzs] = item.sarzs;
                row[Vizsgálat.Azonosító.TableIndexes.hordószám] = item.hordószám;
                row[Vizsgálat.Azonosító.TableIndexes.hordótípus] = item.hordótípus;
                row[Vizsgálat.Azonosító.TableIndexes.nettó_töltet] = item.nettó_töltet;
                row[Vizsgálat.Azonosító.TableIndexes.szita_átmérő] = item.szita_átmérő;
                row[Vizsgálat.Azonosító.TableIndexes.megrendelő] = item.megrendelő;
                row[Vizsgálat.Azonosító.TableIndexes.sorszám] = item.sorszám;
                if (item.foglalás == null) row[Vizsgálat.Azonosító.TableIndexes.foglalás] =  DBNull.Value;
                else row[Vizsgálat.Azonosító.TableIndexes.foglalás] = item.foglalás.Value;
                data.Rows.Add(row);

                vizsgálat_tokenek.Add(new DataToken<Vizsgálat.Azonosító>(item));
            }
            return data;
        }

        public override void Refresh()
        {
            // Összes adat lekérdezése
            List<Vizsgálat.Azonosító> vizsgálatok = Program.database.Vizsgálatok();
            // Minden token beállítása a kereséshez
            foreach (DataToken<Vizsgálat.Azonosító> token in vizsgálat_tokenek) { token.type = DataToken<Vizsgálat.Azonosító>.TokenType.NOT_FOUND; }

            // A már táblán fennlévő tokenek összevetése a lekért adatokkal
            foreach (Vizsgálat.Azonosító item in vizsgálatok)
            {
                bool found = false;
                foreach (DataToken<Vizsgálat.Azonosító> token in vizsgálat_tokenek)
                {
                    if (item.Equals(token.data))
                    {
                        // A megtalált token kivétele a keresésből
                        token.type = DataToken<Vizsgálat.Azonosító>.TokenType.FOUND;
                        found = true;
                        break;
                    }
                }

                // Még tokenek között nem szereplő adat hozzáadása
                if (!found) vizsgálat_tokenek.Add(new DataToken<Vizsgálat.Azonosító>(item));
            }

            // A tábla kiegésszítése a tokenekből származó adatokkal
            List<DataToken<Vizsgálat.Azonosító>> kitörlendők = new List<DataToken<Vizsgálat.Azonosító>>();
            foreach (DataToken<Vizsgálat.Azonosító> token in vizsgálat_tokenek)
            {
                switch (token.type)
                {
                    case DataToken<Vizsgálat.Azonosító>.TokenType.NEW:
                        DataRow row = data.NewRow();
                        row[Vizsgálat.Azonosító.TableIndexes.termékkód] = token.data.termékkód;
                        row[Vizsgálat.Azonosító.TableIndexes.sarzs] = token.data.sarzs;
                        row[Vizsgálat.Azonosító.TableIndexes.hordószám] = token.data.hordószám;
                        row[Vizsgálat.Azonosító.TableIndexes.hordótípus] = token.data.hordótípus;
                        row[Vizsgálat.Azonosító.TableIndexes.nettó_töltet] = token.data.nettó_töltet;
                        row[Vizsgálat.Azonosító.TableIndexes.szita_átmérő] = token.data.szita_átmérő;
                        row[Vizsgálat.Azonosító.TableIndexes.megrendelő] = token.data.megrendelő;
                        row[Vizsgálat.Azonosító.TableIndexes.sorszám] = token.data.sorszám;
                        row[Vizsgálat.Azonosító.TableIndexes.foglalás] = token.data.foglalás;
                        data.Rows.Add(row);
                        break;

                    case DataToken<Vizsgálat.Azonosító>.TokenType.NOT_FOUND:
                        foreach (DataRow current in data.Rows)
                        {
                            if (token.data.termékkód == current[0].ToString() && token.data.sarzs == current[1].ToString()
                                && token.data.hordószám == current[2].ToString() && token.data.hordótípus == current[3].ToString())
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
            foreach (DataToken<Vizsgálat.Azonosító> token in kitörlendők) { vizsgálat_tokenek.Remove(token); }
            base.Refresh();
        }

        #region EventHandlers
        private void Vizsgálat_Hozzáadás(object _sender, System.EventArgs _event)
        {
            Vizsgálati_Lap vizsgálati_lap = new Vizsgálati_Lap();
            vizsgálati_lap.ShowDialog();

            Refresh();
        }

        private void Vizsgálat_Módosítás(object sender, DataGridViewCellEventArgs e)
        {
            if (table.SelectedRows.Count != 1) return;

            Vizsgálat.Azonosító azonosító = new Vizsgálat.Azonosító((string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.termékkód].Value,
                    (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.sarzs].Value, (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.hordószám].Value,
                    (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.hordótípus].Value, (double)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.nettó_töltet].Value,
                    (byte)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.szita_átmérő].Value, (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.megrendelő].Value,
                    (int)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.sorszám].Value, table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.foglalás].Value == DBNull.Value ? null : (int?)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.foglalás].Value);

            Vizsgálat? _vizsgálat = Program.database.Vizsgálat(azonosító);
            if (_vizsgálat == null) { MessageBox.Show("A kiválasztott vizsgálati lap nem található!", "Adatbázis hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Vizsgálati_Lap vizsgálati_lap = new Vizsgálati_Lap(_vizsgálat.Value);
            vizsgálati_lap.ShowDialog();

            Refresh();
        }

        private void Vizsgálat_Törlés(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count == 1) { if (MessageBox.Show("Biztosan törli a kiválasztott vizsgálatot?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            else if (table.SelectedRows.Count != 0) { if (MessageBox.Show("Biztosan törli a kiválasztott vizsgálatokat?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            foreach (DataGridViewRow selected in table.SelectedRows)
            {

                Vizsgálat.Azonosító azonosító = new Vizsgálat.Azonosító((string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.termékkód].Value,
                    (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.sarzs].Value, (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.hordószám].Value,
                    (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.hordótípus].Value, (double)selected.Cells[Vizsgálat.Azonosító.TableIndexes.nettó_töltet].Value,
                    (byte)selected.Cells[Vizsgálat.Azonosító.TableIndexes.szita_átmérő].Value, (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.megrendelő].Value,
                    (int)selected.Cells[Vizsgálat.Azonosító.TableIndexes.sorszám].Value, (int)selected.Cells[Vizsgálat.Azonosító.TableIndexes.foglalás].Value);

                if (!Program.database.Vizsgálat_Törlés(azonosító))
                {
                    MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő vizsgálat?\nTermékkód: " + azonosító.termékkód + "\nSarzs: " + azonosító.sarzs + "\nHordószám: " + azonosító.hordószám +
                      "\nSorszám: " + azonosító.sorszám, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else Refresh();

            }

        }

        //

        private void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.termékkód].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.sarzs].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.hordószám].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.hordótípus].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.nettó_töltet].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.szita_átmérő].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.megrendelő].Width = 280 - 3 - 100;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.sorszám].Visible = false;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.foglalás].Width = 100;
        }

        private void table_UserDeletingRow(object _sender, DataGridViewRowCancelEventArgs _event)
        {
            // Delete lenyomása esetén kitörli az adott sorokat, ezt iktatjuk ki ezzel!
            _event.Cancel = true;
            // A saját törlést azért elindítjuk Delete gomb lenyomása után.
            Vizsgálat_Törlés(_sender, _event);
        }
        #endregion

        public sealed class Vizsgálati_Lap : Form
        {
            #region vizsgálati lap
            TextBox box_termékkód;

            TextBox box_szita_átmérő;
            TextBox box_hordószám;
            TextBox box_hőkezelés;
            TextBox box_brix;
            TextBox box_citromsav;
            TextBox box_borkősav;
            TextBox box_ph;
            TextBox box_bostwick;
            TextBox box_aszkorbinsav;
            TextBox box_citromsav_ad;
            TextBox box_magtöret;
            TextBox box_feketepont;
            TextBox box_barnapont;
            TextBox box_szin;
            TextBox box_iz;
            TextBox box_illat;
            TextBox box_sarzs;
            TextBox box_nettó_töltet;
            TextBox box_műszak_jele;
            TextBox box_terméknév;
            TextBox box_töltőgép_száma;
            TextBox box_leoltas;
            TextBox box_ertekeles;
            TextBox box_megjegyzes;
            TextBox box_összcsíra_higit_1;
            TextBox box_összcsíra_higit_2;
            TextBox box_penész_higit_1;
            TextBox box_penész_higit_2;
            TextBox box_élesztő_higit_1;
            TextBox box_élesztő_higit_2;
            TextBox box_t_cimzett;
            TextBox box_t_datum;
            TextBox box_k1_cimzett;
            TextBox box_k1_datum;
            TextBox box_k2_cimzett;
            TextBox box_k2_datum;
            TextBox box_k3_cimzett;
            TextBox box_k3_datum;
            TextBox box_k4_cimzett;
            TextBox box_k4_datum;
            TextBox box_k5_cimzett;
            TextBox box_k5_datum;
            TextBox box_k6_cimzett;
            TextBox box_k6_datum;

            ComboBox combo_laboros;
            ComboBox combo_gyümölcsfajta;
            ComboBox combo_származási_ország;
            ComboBox combo_hordótípus;
            ComboBox combo_megrendelő;
            #endregion

            private enum States
            {
                NINCS = 0,
                TERMÉKKÓD = 1,
                TERMÉKKÓD_BEÁLLÍTÁS = 2,
                HORDÓSZÁM = 3,
                KÉSZ = 4

            }

            private string gyártási_év = "2013";

            private States state = States.NINCS;
            private Vizsgálat? eredeti = null;

            public Vizsgálati_Lap()
            {
                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            public Vizsgálati_Lap(Vizsgálat _eredeti)
            {
                eredeti = _eredeti;

                InitializeForm();
                InitializeContent();
                InitializeData(_eredeti);
            }

            private void InitializeForm()
            {
                Text = "Vizsgálati lap";
                ClientSize = new Size(1024, 568);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                #region Formázás
                int sor = 30;
                int oszlop = 160;
                int köz = 16;
                int[] méret = new int[] { 45, 30, 55, 20, 120, 90, 110, 273, 435, 280, 230 };

                #region Labels
                Label termékkód = MainForm.createlabel("Termékkód:", 10, 10, this);
                termékkód.Font = new System.Drawing.Font(termékkód.Font, FontStyle.Bold);
                Label szita_átmérő = MainForm.createlabel("Szita átmérő:", termékkód.Location.X, termékkód.Location.Y + sor, this);
                Label megrendelő = MainForm.createlabel("Megrendelők:", termékkód.Location.X, szita_átmérő.Location.Y + sor, this);
                Label hordószám = MainForm.createlabel("Hordószám:", termékkód.Location.X + oszlop, termékkód.Location.Y, this);
                hordószám.Font = new System.Drawing.Font(hordószám.Font, FontStyle.Bold);
                Label hőkezelés = MainForm.createlabel("Hőkezelés °C:", hordószám.Location.X, szita_átmérő.Location.Y, this);
                Label vonal = new Label();
                vonal.Location = new Point(termékkód.Location.X, megrendelő.Location.Y + sor);
                vonal.Height = 3;
                vonal.Width = 1000;
                vonal.BackColor = Color.Black;
                Label brix = MainForm.createlabel("Brix % nn.n:", termékkód.Location.X, vonal.Location.Y + sor, this);
                Label citromsav = MainForm.createlabel("Citromsav % nn.nn:", termékkód.Location.X, brix.Location.Y + sor, this);
                Label borkősav = MainForm.createlabel("Borkősav: nn.nn", termékkód.Location.X, citromsav.Location.Y + sor, this);
                Label ph = MainForm.createlabel("PH: nn.nn", termékkód.Location.X, borkősav.Location.Y + sor, this);
                Label bostwick = MainForm.createlabel("Bostwick cm/30sec, 20°C nn.n:", termékkód.Location.X, ph.Location.Y + sor, this);
                Label aszkorbinsav = MainForm.createlabel("Aszkorbinsav mg/kg:", termékkód.Location.X, bostwick.Location.Y + sor, this);
                Label citromsav_ad = MainForm.createlabel("Citromsav adagolás mg/kg:", termékkód.Location.X, aszkorbinsav.Location.Y + sor, this);
                Label magtöret = MainForm.createlabel("Magtöret db:", termékkód.Location.X, citromsav_ad.Location.Y + sor, this);
                Label feketepont = MainForm.createlabel("Feketepont db:", termékkód.Location.X, magtöret.Location.Y + sor, this);
                Label barnapont = MainForm.createlabel("Barnapont db:", termékkód.Location.X, feketepont.Location.Y + sor, this);
                Label szin = MainForm.createlabel("Szín:", termékkód.Location.X, barnapont.Location.Y + sor, this);
                Label iz = MainForm.createlabel("Íz:", termékkód.Location.X, szin.Location.Y + sor, this);
                Label illat = MainForm.createlabel("Illat:", termékkód.Location.X, iz.Location.Y + sor, this);
                Label sarzs = MainForm.createlabel("Sarzs:", hordószám.Location.X + oszlop, termékkód.Location.Y, this);
                Label nettó_töltet = MainForm.createlabel("Nettó töltet kg:", sarzs.Location.X, szita_átmérő.Location.Y, this);
                Label származási_ország = MainForm.createlabel("Származási ország:", sarzs.Location.X, megrendelő.Location.Y, this);
                Label műszak_jele = MainForm.createlabel("Műszak jele:", sarzs.Location.X + oszlop, szita_átmérő.Location.Y, this);
                Label töltőgép_száma = MainForm.createlabel("Töltőgép száma:", műszak_jele.Location.X + oszlop, szita_átmérő.Location.Y, this);
                Label terméknév = MainForm.createlabel("Terméknév:", töltőgép_száma.Location.X, termékkód.Location.Y, this);
                Label hordótípus = MainForm.createlabel("Hordótípus:", töltőgép_száma.Location.X + oszlop - 10, szita_átmérő.Location.Y, this);
                Label gyümölcsfajta = MainForm.createlabel("Gyümölcsfajta", töltőgép_száma.Location.X, megrendelő.Location.Y, this);
                Label leoltas = MainForm.createlabel("Leoltás ideje (hh.nn oo:pp,this):", sarzs.Location.X, brix.Location.Y, this);
                Label ertekeles = MainForm.createlabel("Értékelés dátuma (hh.nn oo:pp,this):", sarzs.Location.X, citromsav.Location.Y, this);
                Label higit_1 = MainForm.createlabel("1. hígítás:", sarzs.Location.X, ph.Location.Y, this);
                Label higit_2 = MainForm.createlabel("2. hígítás:", sarzs.Location.X, bostwick.Location.Y, this);
                Label megjegyzes = MainForm.createlabel("Megjegyzés:", sarzs.Location.X, aszkorbinsav.Location.Y, this);
                Label összcsira = MainForm.createlabel("Összcsíra", higit_1.Location.X + oszlop / 2, borkősav.Location.Y, this);
                Label penesz = MainForm.createlabel("Penész", higit_1.Location.X + oszlop, borkősav.Location.Y, this);
                Label eleszto = MainForm.createlabel("Élesztő", higit_1.Location.X + oszlop + oszlop / 2, borkősav.Location.Y, this);
                Label minta_jele = MainForm.createlabel("Minta jele", leoltas.Location.X + 2 * oszlop - 10, brix.Location.Y, this);
                Label cimzett = MainForm.createlabel("Címzett", minta_jele.Location.X + oszlop / 2, brix.Location.Y, this);
                Label datum = MainForm.createlabel("Dátum (éé.hh.nn,this)", cimzett.Location.X + oszlop - 50, brix.Location.Y, this);
                Label t = MainForm.createlabel("T", minta_jele.Location.X + 30, citromsav.Location.Y, this);
                Label k1 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + sor, this);
                Label k2 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 2 * sor, this);
                Label k3 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 3 * sor, this);
                Label k4 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 4 * sor, this);
                Label k5 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 5 * sor, this);
                Label k6 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 6 * sor, this);
                Label laboros = MainForm.createlabel("Laboros:", minta_jele.Location.X, feketepont.Location.Y, this);

                #endregion

                box_termékkód = MainForm.createtextbox(termékkód.Location.X + termékkód.Width + köz, termékkód.Location.Y, 4, méret[0], this);
                box_hordószám = MainForm.createtextbox(hordószám.Location.X + hordószám.Width + köz, termékkód.Location.Y, 4, méret[0], this);
                box_sarzs = MainForm.createtextbox(sarzs.Location.X + sarzs.Width + köz + 28, termékkód.Location.Y, 4, méret[0], this);
                box_terméknév = MainForm.createtextbox(terméknév.Location.X + terméknév.Width + köz, termékkód.Location.Y, 35, méret[9], this);
                box_szita_átmérő = MainForm.createtextbox(box_termékkód.Location.X, szita_átmérő.Location.Y, 6, méret[0], this);
                box_szita_átmérő.Width = box_hordószám.Width;
                box_hőkezelés = MainForm.createtextbox("110", box_hordószám.Location.X, box_szita_átmérő.Location.Y, 3, méret[0], this);
                box_nettó_töltet = MainForm.createtextbox(box_sarzs.Location.X, nettó_töltet.Location.Y, 6, méret[0], this);
                box_műszak_jele = MainForm.createtextbox(műszak_jele.Location.X + műszak_jele.Width + köz, szita_átmérő.Location.Y, 1, méret[3], this);
                box_töltőgép_száma = MainForm.createtextbox(töltőgép_száma.Location.X + töltőgép_száma.Width + köz, töltőgép_száma.Location.Y, 1, méret[3], this);
                combo_hordótípus = MainForm.createcombobox(hordótípus.Location.X + hordótípus.Width + köz, hordótípus.Location.Y, méret[4], this);
                combo_megrendelő = MainForm.createcombobox(box_termékkód.Location.X, megrendelő.Location.Y, méret[10], this);
                combo_származási_ország = MainForm.createcombobox(származási_ország.Location.X + származási_ország.Width + köz, származási_ország.Location.Y, méret[4], this);
                combo_gyümölcsfajta = MainForm.createcombobox(gyümölcsfajta.Location.X + gyümölcsfajta.Width + köz, gyümölcsfajta.Location.Y, méret[4], this);
                box_brix = MainForm.createtextbox(citromsav_ad.Location.X + citromsav_ad.Width + köz, brix.Location.Y, 4, méret[2], this);

                box_citromsav = MainForm.createtextbox(box_brix.Location.X, citromsav.Location.Y, 5, méret[2], this);
                box_borkősav = MainForm.createtextbox(box_brix.Location.X, borkősav.Location.Y, 5, méret[2], this);
                box_ph = MainForm.createtextbox(box_brix.Location.X, ph.Location.Y, 5, méret[2], this);
                box_bostwick = MainForm.createtextbox(box_brix.Location.X, bostwick.Location.Y, 4, méret[2], this);
                box_aszkorbinsav = MainForm.createtextbox(box_brix.Location.X, aszkorbinsav.Location.Y, 4, méret[2], this);
                box_citromsav_ad = MainForm.createtextbox("0", box_brix.Location.X, citromsav_ad.Location.Y, 3, méret[2], this);
                box_magtöret = MainForm.createtextbox(box_brix.Location.X, magtöret.Location.Y, 2, méret[1], this);
                box_feketepont = MainForm.createtextbox(box_brix.Location.X, feketepont.Location.Y, 2, méret[1], this);
                box_barnapont = MainForm.createtextbox(box_brix.Location.X, barnapont.Location.Y, 2, méret[1], this);
                box_szin = MainForm.createtextbox(box_magtöret.Location.X, szin.Location.Y, 60, méret[8], this);
                box_iz = MainForm.createtextbox(box_magtöret.Location.X, iz.Location.Y, 60, méret[8], this);
                box_illat = MainForm.createtextbox(box_magtöret.Location.X, illat.Location.Y, 60, méret[8], this);
                box_leoltas = MainForm.createtextbox(DateTime.Now.ToString("MM.dd hh:mm"), ertekeles.Location.X + ertekeles.Width + köz, leoltas.Location.Y, 11, méret[6], this);
                box_ertekeles = MainForm.createtextbox(ertekeles.Location.X + ertekeles.Width + köz, ertekeles.Location.Y, 11, méret[6], this);
                box_összcsíra_higit_1 = MainForm.createtextbox("0", összcsira.Location.X, higit_1.Location.Y, 2, méret[1], this);
                box_penész_higit_1 = MainForm.createtextbox("0", penesz.Location.X, higit_1.Location.Y, 2, méret[1], this);
                box_élesztő_higit_1 = MainForm.createtextbox("0", eleszto.Location.X, higit_1.Location.Y, 2, méret[1], this);
                box_összcsíra_higit_2 = MainForm.createtextbox("0", összcsira.Location.X, higit_2.Location.Y, 2, méret[1], this);
                box_penész_higit_2 = MainForm.createtextbox("0", penesz.Location.X, higit_2.Location.Y, 2, méret[1], this);
                box_élesztő_higit_2 = MainForm.createtextbox("0", eleszto.Location.X, higit_2.Location.Y, 2, méret[1], this);
                box_megjegyzes = MainForm.createtextbox(megjegyzes.Location.X, megjegyzes.Location.Y + sor, 300, méret[7], this);
                box_megjegyzes.Multiline = true;
                box_megjegyzes.Height = 100;
                box_t_cimzett = MainForm.createtextbox(t.Location.X + t.Width + köz, t.Location.Y, 15, méret[4], this);
                box_t_datum = MainForm.createtextbox(box_t_cimzett.Location.X + box_t_cimzett.Width + köz, t.Location.Y, 8, méret[5], this);
                box_k1_cimzett = MainForm.createtextbox(t.Location.X + t.Width + köz, k1.Location.Y, 15, méret[4], this);
                box_k1_datum = MainForm.createtextbox(box_t_datum.Location.X, k1.Location.Y, 8, méret[5], this);
                box_k2_cimzett = MainForm.createtextbox(box_k1_cimzett.Location.X, k2.Location.Y, 15, méret[4], this);
                box_k2_datum = MainForm.createtextbox(box_t_datum.Location.X, k2.Location.Y, 8, méret[5], this);
                box_k3_cimzett = MainForm.createtextbox(box_k1_cimzett.Location.X, k3.Location.Y, 15, méret[4], this);
                box_k3_datum = MainForm.createtextbox(box_t_datum.Location.X, k3.Location.Y, 8, méret[5], this);
                box_k4_cimzett = MainForm.createtextbox(box_k1_cimzett.Location.X, k4.Location.Y, 15, méret[4], this);
                box_k4_datum = MainForm.createtextbox(box_t_datum.Location.X, k4.Location.Y, 8, méret[5], this);
                box_k5_cimzett = MainForm.createtextbox(box_k1_cimzett.Location.X, k5.Location.Y, 15, méret[4], this);
                box_k5_datum = MainForm.createtextbox(box_t_datum.Location.X, k5.Location.Y, 8, méret[5], this);
                box_k6_cimzett = MainForm.createtextbox(box_k1_cimzett.Location.X, k6.Location.Y, 15, méret[4], this);
                box_k6_datum = MainForm.createtextbox(box_t_datum.Location.X, k6.Location.Y, 8, méret[5], this);
                combo_laboros = MainForm.createcombobox(box_t_cimzett.Location.X, laboros.Location.Y, méret[4], this);
                box_sarzs.Enabled = false;
                box_terméknév.Enabled = false;
                box_szita_átmérő.Enabled = false;
                box_nettó_töltet.Enabled = false;
                box_műszak_jele.Enabled = false;
                box_töltőgép_száma.Enabled = false;
                box_borkősav.Enabled = false;
                #endregion

                #region Events
                box_termékkód.TextChanged += box_termékkód_TextChanged;
                box_hordószám.TextChanged += box_hordószám_TextChanged;
                box_citromsav.Leave += box_citromsav_Leave;
                box_brix.KeyPress += MainForm.OnlyNumber;
                box_citromsav.KeyPress += MainForm.OnlyNumber;
                box_ph.KeyPress += MainForm.OnlyNumber;
                box_bostwick.KeyPress += MainForm.OnlyNumber;
                box_aszkorbinsav.KeyPress += MainForm.OnlyNumber;
                box_citromsav_ad.KeyPress += MainForm.OnlyNumber;
                box_magtöret.KeyPress += MainForm.OnlyNumber;
                box_feketepont.KeyPress += MainForm.OnlyNumber;
                box_barnapont.KeyPress += MainForm.OnlyNumber;
                box_t_datum.Leave += MainForm.OnlyDate;
                box_k1_datum.Leave += MainForm.OnlyDate;
                box_k2_datum.Leave += MainForm.OnlyDate;
                box_k3_datum.Leave += MainForm.OnlyDate;
                box_k4_datum.Leave += MainForm.OnlyDate;
                box_k5_datum.Leave += MainForm.OnlyDate;
                box_k6_datum.Leave += MainForm.OnlyDate;
                box_leoltas.Leave += MainForm.OnlyTime;
                box_ertekeles.Leave += MainForm.OnlyTime;
                combo_hordótípus.Leave += combo_hordótípus_Leave;
                #endregion

                #region Data
                // TODO REFRESHBE!

                List<string> megrendelok = Program.database.Megrendelők();
                foreach (string item in megrendelok)
                {
                    combo_megrendelő.Items.Add(item);
                }
                combo_megrendelő.SelectedIndex = 0;

                List<Törzsadat> seged = Program.database.Törzsadatok("Hordótípus");
                foreach (Törzsadat item in seged)
                {
                    combo_hordótípus.Items.Add(item.azonosító);
                }
                combo_hordótípus.SelectedIndex = 0;

                seged.Clear();
                seged = Program.database.Törzsadatok("Származási ország");
                foreach (Törzsadat item in seged)
                {
                    combo_származási_ország.Items.Add(item.azonosító);
                }
                combo_származási_ország.SelectedIndex = 0;

                seged.Clear();
                seged = Program.database.Törzsadatok("Laboros");
                foreach (Törzsadat item in seged)
                {
                    combo_laboros.Items.Add(item.azonosító);
                }
                #endregion

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += rendben_Click;

                Controls.Add(rendben);
                Controls.Add(vonal);
            }

            private void InitializeData()
            {
                SetState(States.TERMÉKKÓD);
            }

            private void InitializeData(Vizsgálat _vizsgálat)
            {
                box_termékkód.Text = _vizsgálat.azonosító.termékkód;
                box_szita_átmérő.Text = _vizsgálat.azonosító.szita_átmérő.ToString();
                box_hordószám.Text = _vizsgálat.azonosító.hordószám.ToString();
                box_hőkezelés.Text = _vizsgálat.adatok1.hőkezelés.ToString();
                box_brix.Text = _vizsgálat.adatok2.brix.ToString();
                box_citromsav.Text = _vizsgálat.adatok2.citromsav.ToString();
                box_borkősav.Text = _vizsgálat.adatok2.borkősav.ToString();
                box_ph.Text = _vizsgálat.adatok2.ph.ToString();
                box_bostwick.Text = _vizsgálat.adatok2.bostwick.ToString();
                box_aszkorbinsav.Text = _vizsgálat.adatok2.aszkorbinsav.ToString();
                box_citromsav_ad.Text = _vizsgálat.adatok2.citromsav_adagolás.ToString();
                box_magtöret.Text = _vizsgálat.adatok2.magtöret.ToString();
                box_feketepont.Text = _vizsgálat.adatok2.feketepont.ToString();
                box_barnapont.Text = _vizsgálat.adatok2.barnapont.ToString();
                box_szin.Text = _vizsgálat.adatok2.szín;
                box_iz.Text = _vizsgálat.adatok2.íz;
                box_illat.Text = _vizsgálat.adatok2.illat;
                box_nettó_töltet.Text = _vizsgálat.azonosító.nettó_töltet.ToString();
                box_sarzs.Text = _vizsgálat.azonosító.sarzs;
                box_műszak_jele.Text = _vizsgálat.adatok1.műszak_jele;
                box_terméknév.Text = _vizsgálat.adatok1.terméknév;
                box_töltőgép_száma.Text = _vizsgálat.adatok1.töltőgép;
                box_leoltas.Text = _vizsgálat.adatok3.leoltás;
                box_ertekeles.Text = _vizsgálat.adatok3.értékelés;
                box_megjegyzes.Text = _vizsgálat.adatok3.megjegyzés;
                box_összcsíra_higit_1.Text = _vizsgálat.adatok3.összcsíra_1.ToString();
                box_összcsíra_higit_2.Text = _vizsgálat.adatok3.összcsíra_2.ToString();
                box_penész_higit_1.Text = _vizsgálat.adatok3.penész_1.ToString();
                box_penész_higit_2.Text = _vizsgálat.adatok3.penész_2.ToString();
                box_élesztő_higit_1.Text = _vizsgálat.adatok3.élesztő_1.ToString();
                box_élesztő_higit_2.Text = _vizsgálat.adatok3.élesztő_2.ToString();
                box_t_cimzett.Text = _vizsgálat.adatok4.címzett_t;
                box_t_datum.Text = _vizsgálat.adatok4.dátum_t;
                box_k1_cimzett.Text = _vizsgálat.adatok4.címzett_k1;
                box_k1_datum.Text = _vizsgálat.adatok4.dátum_k1;
                box_k2_cimzett.Text = _vizsgálat.adatok4.címzett_k2;
                box_k2_datum.Text = _vizsgálat.adatok4.dátum_k2;
                box_k3_cimzett.Text = _vizsgálat.adatok4.címzett_k3;
                box_k3_datum.Text = _vizsgálat.adatok4.dátum_k3;
                box_k4_cimzett.Text = _vizsgálat.adatok4.címzett_k4;
                box_k4_datum.Text = _vizsgálat.adatok4.dátum_k4;
                box_k5_cimzett.Text = _vizsgálat.adatok4.címzett_k5;
                box_k5_datum.Text = _vizsgálat.adatok4.dátum_k5;
                box_k6_cimzett.Text = _vizsgálat.adatok4.címzett_k6;
                box_k6_datum.Text = _vizsgálat.adatok4.dátum_k6;
                combo_laboros.SelectedItem = _vizsgálat.adatok4.laboros;
                combo_gyümölcsfajta.SelectedItem = _vizsgálat.adatok1.gyümölcsfajta;
                combo_származási_ország.SelectedItem = _vizsgálat.adatok1.szárm_ország;
                combo_hordótípus.SelectedItem = _vizsgálat.azonosító.hordótípus;
                combo_megrendelő.SelectedItem = _vizsgálat.azonosító.megrendelő;

                List<string> gyümölcsfajták = Program.database.Gyümölcsfajták(_vizsgálat.azonosító.termékkód);
                foreach (string item in gyümölcsfajták)
                {
                    combo_gyümölcsfajta.Items.Add(item);
                }
                combo_gyümölcsfajta.SelectedIndex = 0;

                SetState(States.KÉSZ);
                box_termékkód.Enabled = false;
                box_hordószám.Enabled = false;
            }

            private void SetState(States _state)
            {
                state = _state;
                switch (_state)
                {
                    case States.TERMÉKKÓD:
                        box_hordószám.Text = "";
                        foreach (Control control in Controls)
                        {
                            control.Enabled = false;
                        }

                        box_termékkód.Enabled = true;
                        box_termékkód.Focus();
                        break;

                    case States.HORDÓSZÁM:
                        foreach (Control control in Controls)
                        {
                            control.Enabled = false;
                        }

                        box_termékkód.Enabled = true;
                        box_hordószám.Enabled = true;
                        box_hordószám.Focus();
                        break;

                    case States.KÉSZ:
                        foreach (Control control in Controls)
                        {
                            control.Enabled = true;
                        }

                        box_sarzs.Enabled = false;
                        box_terméknév.Enabled = false;
                        box_szita_átmérő.Enabled = false;
                        box_nettó_töltet.Enabled = false;
                        box_műszak_jele.Enabled = false;
                        box_töltőgép_száma.Enabled = false;
                        box_borkősav.Enabled = false;
                        break;
                }
            }

            #region EventHandlers
            private void combo_hordótípus_Leave(object _sender, EventArgs _event)
            {
                Vizsgálat temp = new Vizsgálat();
                temp.azonosító = new Vizsgálat.Azonosító(
                                        box_termékkód.Text,
                                        box_sarzs.Text,
                                        box_hordószám.Text,
                                        combo_hordótípus.Text,
                                        Convert.ToDouble(box_nettó_töltet.Text),
                                        Convert.ToByte(box_szita_átmérő.Text),
                                        combo_megrendelő.Text,
                                        eredeti == null ? null : eredeti.Value.azonosító.sorszám,
                                        eredeti == null ? null : eredeti.Value.azonosító.foglalás);
                temp.adatok1.gyártási_év = DateTime.Now.Year.ToString();

                string hordótípus = Program.database.Vizsgálat_SarzsEllenőrzés(temp);
                if (hordótípus != null)
                {
                    MessageBox.Show("Nem egyezik meg a hordótípus! (" + hordótípus + ")", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    combo_hordótípus.Focus();
                    return;
                }

            }

            private void box_termékkód_TextChanged(object _sender, EventArgs _event)
            {
                if (box_termékkód.Text.Length == 3)
                {
                    if (box_termékkód.Text.Length != 3) return;
                    List<string> temp = Program.database.Termékkódok(box_termékkód.Text);
                    if (temp.Count == 0) { MessageBox.Show("Nem található ilyen termékkódú cikk!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    ListBox termékkódválasztó = new ListBox();
                    termékkódválasztó.Location = new Point(box_termékkód.Location.X, box_termékkód.Location.Y);
                    termékkódválasztó.Width = 300;
                    termékkódválasztó.KeyDown += termékkódválasztó_KeyDown;

                    foreach (string item in temp)
                    {
                        termékkódválasztó.Items.Add(item);
                    }

                    Controls.Add(termékkódválasztó);
                    termékkódválasztó.SelectedIndex = 0;
                    termékkódválasztó.BringToFront();
                    termékkódválasztó.Focus();

                    gyártási_év = "201" + box_termékkód.Text[2];
                }
                else
                {
                    if (state == States.TERMÉKKÓD_BEÁLLÍTÁS) SetState(States.HORDÓSZÁM);
                    else if (state != States.TERMÉKKÓD) SetState(States.TERMÉKKÓD);
                }
            }

            private void termékkódválasztó_KeyDown(object _sender, KeyEventArgs _event)
            {
                if (_event.KeyCode == Keys.Enter)
                {
                    ListBox termékkódválasztó = (ListBox)_sender;
                    Controls.Remove(termékkódválasztó);

                    SetState(States.TERMÉKKÓD_BEÁLLÍTÁS);
                    box_termékkód.Text = termékkódválasztó.SelectedItem.ToString().Substring(0, 4);

                    box_terméknév.Text = Program.database.Vizsgálat_Terméknév(box_termékkód.Text);

                    List<string> gyümölcsfajták = Program.database.Gyümölcsfajták(box_termékkód.Text);
                    foreach (string item in gyümölcsfajták)
                    {
                        combo_gyümölcsfajta.Items.Add(item);
                    }
                    if(combo_gyümölcsfajta.Items.Count!=0)
                    combo_gyümölcsfajta.SelectedIndex = 0;
                }
            }

            private void box_hordószám_TextChanged(object _sender, EventArgs _event)
            {
                if (box_hordószám.Text.Length == 4)
                {
                    string prodid = "12" + box_termékkód.Text.Substring(0, 2) + "01" + gyártási_év[gyártási_év.Length - 1] + "_0" + gyártási_év[gyártási_év.Length - 1] + box_hordószám.Text;
                    List<string> temp = Program.database.Vizsgálat_Prod_Id(prodid);
                    if (temp.Count != 0)
                    {
                        box_szita_átmérő.Text = temp[0];
                        box_nettó_töltet.Text = temp[1];
                        box_műszak_jele.Text = temp[2];
                        box_töltőgép_száma.Text = temp[3];
                        box_sarzs.Text = temp[4];

                        SetState(States.KÉSZ);
                    }
                    else
                    {
                        box_szita_átmérő.Text = "";
                        box_nettó_töltet.Text = "";
                        box_műszak_jele.Text = "";
                        box_töltőgép_száma.Text = "";
                        box_sarzs.Text = "";

                        MessageBox.Show("Nem található ilyen!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (state != States.HORDÓSZÁM) SetState(States.HORDÓSZÁM);
                }
            }

            private void box_citromsav_Leave(object sender, EventArgs e)
            {
                if (box_citromsav.Text != "")
                {
                    try
                    {
                        box_borkősav.Text = Convert.ToDouble(((Convert.ToDouble(box_citromsav.Text) * 1.171875))).ToString();
                    }
                    catch (Exception)
                    {
                        box_citromsav.Focus();
                    }
                }
            }

            private void rendben_Click(object _sender, System.EventArgs _event)
            {
                if (box_termékkód.Text == "") { MessageBox.Show("Termékkód kitöltése kötelező!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_hordószám.Text == "") { MessageBox.Show("Hordószám kitöltése kötelező!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (box_sarzs.Text == "") { MessageBox.Show("Sarzs kitöltése kötelező!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                //if (box_hőkezelés.Text == "") { MessageBox.Show("Hőkezelés kitöltése kötelező!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; } // TODO kell vagy nem!?

                Vizsgálat.Azonosító azonosító = new Vizsgálat.Azonosító(
                        box_termékkód.Text,
                        box_sarzs.Text,
                        box_hordószám.Text,
                        combo_hordótípus.Text,
                        Convert.ToDouble(box_nettó_töltet.Text),
                        Convert.ToByte(box_szita_átmérő.Text),
                        combo_megrendelő.Text,
                        eredeti == null ? null : eredeti.Value.azonosító.sorszám,
                        eredeti == null ? null : eredeti.Value.azonosító.foglalás);

                Vizsgálat.Adatok1 adatok1 = new Vizsgálat.Adatok1(
                       box_terméknév.Text,
                       Convert.ToByte(box_hőkezelés.Text),
                       gyártási_év,
                       box_műszak_jele.Text,
                       box_töltőgép_száma.Text,
                       combo_származási_ország.Text,
                       combo_gyümölcsfajta.Text);

                Vizsgálat.Adatok2 adatok2 = new Vizsgálat.Adatok2(
                    MainForm.ConvertOrDie<double>(box_brix.Text),
                    MainForm.ConvertOrDie<double>(box_citromsav.Text),
                    MainForm.ConvertOrDie<double>(box_borkősav.Text),
                    MainForm.ConvertOrDie<double>(box_ph.Text),
                    MainForm.ConvertOrDie<double>(box_bostwick.Text),
                    MainForm.ConvertOrDie<byte>(box_aszkorbinsav.Text),
                    MainForm.ConvertOrDie<byte>(box_citromsav_ad.Text),
                    MainForm.ConvertOrDie<byte>(box_magtöret.Text),
                    MainForm.ConvertOrDie<byte>(box_feketepont.Text),
                    MainForm.ConvertOrDie<byte>(box_barnapont.Text),
                    MainForm.ConvertOrDieString(box_szin.Text),
                    MainForm.ConvertOrDieString(box_iz.Text),
                    MainForm.ConvertOrDieString(box_illat.Text));

                Vizsgálat.Adatok3 adatok3 = new Vizsgálat.Adatok3(
                    MainForm.ConvertOrDieString(box_leoltas.Text),
                    MainForm.ConvertOrDieString(box_ertekeles.Text),
                    MainForm.ConvertOrDie<byte>(box_összcsíra_higit_1.Text),
                    MainForm.ConvertOrDie<byte>(box_összcsíra_higit_2.Text),
                    MainForm.ConvertOrDie<byte>(box_penész_higit_1.Text),
                    MainForm.ConvertOrDie<byte>(box_penész_higit_2.Text),
                    MainForm.ConvertOrDie<byte>(box_élesztő_higit_1.Text),
                    MainForm.ConvertOrDie<byte>(box_élesztő_higit_2.Text),
                    MainForm.ConvertOrDieString(box_megjegyzes.Text));

                Vizsgálat.Adatok4 adatok4 = new Vizsgálat.Adatok4(
                    MainForm.ConvertOrDieString(box_t_cimzett.Text),
                    MainForm.ConvertOrDieString(box_t_datum.Text),
                    MainForm.ConvertOrDieString(box_k1_cimzett.Text),
                    MainForm.ConvertOrDieString(box_k1_datum.Text),
                    MainForm.ConvertOrDieString(box_k2_cimzett.Text),
                    MainForm.ConvertOrDieString(box_k2_datum.Text),
                    MainForm.ConvertOrDieString(box_k3_cimzett.Text),
                    MainForm.ConvertOrDieString(box_k3_datum.Text),
                    MainForm.ConvertOrDieString(box_k4_cimzett.Text),
                    MainForm.ConvertOrDieString(box_k4_datum.Text),
                    MainForm.ConvertOrDieString(box_k5_cimzett.Text),
                    MainForm.ConvertOrDieString(box_k5_datum.Text),
                    MainForm.ConvertOrDieString(box_k6_cimzett.Text),
                    MainForm.ConvertOrDieString(box_k6_datum.Text),
                    MainForm.ConvertOrDieString(combo_laboros.Text));

                Vizsgálat _vizsgálat = new Vizsgálat(azonosító, adatok1, adatok2, adatok3, adatok4);

                if (eredeti != null)
                {
                    if (!Program.database.Vizsgálat_Módosítás(eredeti.Value, _vizsgálat))
                    {
                        MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a módosítandó vizsgálat?\nTermékkód: " + azonosító.termékkód + "\nSarzs: " + azonosító.sarzs + "\nHordószám: " + azonosító.hordószám +
                          "\nSorszám: " + azonosító.sorszám, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (!Program.database.Vizsgálat_Hozzáadás(_vizsgálat))
                    {
                        MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy már létezik ilyen vizsgálat?\nTermékkód: " + azonosító.termékkód + "\nSarzs: " + azonosító.sarzs + "\nHordószám: " + azonosító.hordószám +
                          "\nSorszám: " + azonosító.sorszám, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                Close();
            }
            #endregion
        }
    }
}
