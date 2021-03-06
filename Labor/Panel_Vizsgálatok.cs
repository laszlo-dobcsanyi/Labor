﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Labor
{
    public struct Vizsgálat
    {
        public struct Azonosító
        {
            public string termékkód;
            public string othatkod;
            public string sarzs;
            public string hordószám;
            public string hordótípus;
            public double nettó_töltet;
            public string szita_átmérő;
            public string megrendelő;
            public int? sorszám;

            public Azonosító(string _termékkód, string _othatkod, string _sarzs, string _hordószám, string _hordótípus, double _nettó_töltet, string _szita_átmérő, string _megrendelő, int? _sorszám)
            {
                termékkód = _termékkód;
                othatkod = _othatkod;
                sarzs = _sarzs;
                hordószám = _hordószám;
                hordótípus = _hordótípus;
                nettó_töltet = _nettó_töltet;
                szita_átmérő = _szita_átmérő;
                megrendelő = _megrendelő;
                sorszám = _sorszám;
            }

            public struct TableIndexes
            {
                public const int termékkód = 0;
                public const int othatkod = 1;
                public const int sarzs = 2;
                public const int hordószám = 3;
                public const int hordótípus = 4;
                public const int nettó_töltet = 5;
                public const int szita_átmérő = 6;
                public const int megrendelő = 7;
                public const int sorszám = 8;
            }

            public static void SetRow(DataRow _row, Azonosító _azonosító)
            {
                _row[TableIndexes.termékkód] = _azonosító.termékkód;
                _row[TableIndexes.othatkod] = _azonosító.othatkod;
                _row[TableIndexes.sarzs] = _azonosító.sarzs;
                _row[TableIndexes.hordószám] = _azonosító.hordószám;
                _row[TableIndexes.hordótípus] = _azonosító.hordótípus;
                _row[TableIndexes.nettó_töltet] = _azonosító.nettó_töltet;
                _row[TableIndexes.szita_átmérő] = _azonosító.szita_átmérő;
                _row[TableIndexes.megrendelő] = _azonosító.megrendelő;
                _row[TableIndexes.sorszám] = _azonosító.sorszám;
            }

            public static bool SameKeys(Azonosító _1, Azonosító _2)
            {
                if (_1.termékkód == _2.termékkód && _1.sarzs == _2.sarzs && _1.hordószám == _2.hordószám && _1.hordótípus == _2.hordótípus) return true;
                return false;
            }

            public static bool SameKeys(Azonosító _1, DataRow _row)
            {
                if (_1.termékkód == (string)_row[TableIndexes.termékkód] && _1.sarzs == (string)_row[TableIndexes.sarzs] &&
                        _1.hordószám == (string)_row[TableIndexes.hordószám] && _1.hordótípus == (string)_row[TableIndexes.hordótípus]) return true;
                return false;
            }
        }

        public struct Adatok1
        {
            public string terméknév;
            public int hőkezelés;
            public string gyártási_év;
            public string műszak_jele;
            public string töltőgép;
            public string szárm_ország;
            public string gyümölcsfajta;

            public Adatok1(string _terméknév, int _hőkezelés, string _gyártási_év, string _műszak_jele, string _töltőgép, string _szárm_ország, string _gyümölcsfajta)
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
            public int? aszkorbinsav;
            public int? citromsav_adagolás;
            public int? magtöret;
            public int? feketepont;
            public int? barnapont;
            public string szín;
            public string íz;
            public string illat;
            public Adatok2(double? _brix, double? _citromsav, double? _borkősav, double? _ph, double? _bostwick, int? _aszkorbinsav, int? _citromsav_adagolás, int? _magtöret, int? _feketepont, int? _barnapont, string _szín, string _íz, string _illat)
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
            public int? összcsíra_1;
            public int? összcsíra_2;
            public int? penész_1;
            public int? penész_2;
            public int? élesztő_1;
            public int? élesztő_2;
            public string megjegyzés;
            public Adatok3(string _leoltás, string _értékelés, int? _összcsíra_1, int? _összcsíra_2, int? _penész_1, int? _penész_2, int? _élesztő_1, int? _élesztő_2, string _megjegyzés)
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

    public sealed class Panel_Vizsgálatok : Tokenized_Control<Vizsgálat.Azonosító>
    {
        private TextBox box_kereső;

        #region Constructor
        public Panel_Vizsgálatok()
        {
            InitializeContent();
            InitializeTokens();

            KeyDown += Panel_Vizsgálatok_KeyDown;
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
            table.ReadOnly = true;
            table.DataBindingComplete += table_DataBindingComplete;
            table.CellDoubleClick += Vizsgálat_Módosítás;
            table.UserDeletingRow += table_UserDeletingRow;

            CreateSource();
            CreateView();
            table.DataSource = view;
            
            //

            Label label_kereső = new Label();
            label_kereső.Text = "Termékkód:";
            label_kereső.Location = new Point(table.Width + 50, 15);
            label_kereső.AutoSize = true;

            box_kereső = new TextBox();
            box_kereső.Location = new Point(label_kereső.Location.X + 100, label_kereső.Location.Y);
            box_kereső.TextChanged += box_kereső_TextChanged;

            //

            Button törlés = new Button();
            törlés.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            törlés.Text = "Törlés";
            törlés.Size = new Size(96, 32);
            törlés.Location = new Point(ClientRectangle.Width - 224 - 16, ClientRectangle.Height - 32 - 16);
            törlés.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Vizsgalatok.Torles ? true : false;
            törlés.Click += Vizsgálat_Törlés;

            Button hozzáadás = new Button();
            hozzáadás.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            hozzáadás.Text = "Hozzáadás";
            hozzáadás.Size = new Size(96, 32);
            hozzáadás.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y);
            hozzáadás.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Vizsgalatok.Hozzaadas ? true : false;
            hozzáadás.Click += Vizsgálat_Hozzáadás;

            Button javitas = new Button();
            javitas.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            javitas.Text = "Javítás";
            javitas.Size = new Size(96, 32);
            javitas.Location = new Point(törlés.Location.X + törlés.Width + 16, törlés.Location.Y - 64);
            javitas.Enabled = Program.felhasználó.Value.Jogosultsagok.Value.Vizsgalatok.Hozzaadas ? true : false;
            javitas.Click += Javitas_Click;

            //

            Controls.Add(table);
            Controls.Add(javitas);
            Controls.Add(törlés);
            Controls.Add(hozzáadás);
            Controls.Add(label_kereső);
            Controls.Add(box_kereső);
        }

        private void Javitas_Click(object sender, EventArgs e)
        {
            var vizsgálatok = Program.database.Vizsgálatok();
            String message = "Hordók másolása" + Environment.NewLine;
            var vizsgalatok = new List<Vizsgálat>();
            
            for (int i = 0; i < vizsgálatok.Count; i++)
            {
                var vizsgálat = Program.database.Vizsgálat(vizsgálatok[i]);
                if (vizsgálat != null)
                {
                    try
                    {
                        bool shouldCopy = Program.database.Hordók_Javítás(vizsgálat.Value);

                        if (shouldCopy == true)
                        {
                            bool found = false;
                            for (int j = 0; j < vizsgalatok.Count; j++)
                            {
                                if (vizsgalatok[j].azonosító.termékkód == vizsgálat.Value.azonosító.termékkód &&
                                    vizsgalatok[j].azonosító.sarzs == vizsgálat.Value.azonosító.sarzs &&
                                    vizsgalatok[j].azonosító.othatkod == vizsgálat.Value.azonosító.othatkod &&
                                    vizsgalatok[j].adatok1.gyártási_év == vizsgálat.Value.adatok1.gyártási_év)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                vizsgalatok.Add(vizsgálat.Value);
                            }

                            message += "termékkód: " +  vizsgálat.Value.azonosító.termékkód + " sarzs: " + vizsgálat.Value.azonosító.sarzs + " gyártási év:" + vizsgálat.Value.adatok1.gyártási_év + Environment.NewLine;
                        }
                    }
                    catch (Exception ex )
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            foreach (Vizsgálat vizsgalat in vizsgalatok)
            {
                Program.database.Hordók_Másolás(vizsgalat, true);
            }

            MessageBox.Show("Hordók Javítása kész");
        }

        void box_kereső_TextChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in table.Rows)
            {
                int talált = 0;
                for (int i = 0; i < box_kereső.Text.Length; i++)
                {
                    if (row.Cells[0].Value.ToString().Length < box_kereső.Text.Length)
                    {
                        break;
                    }
                    if (row.Cells[0].Value.ToString()[i] == box_kereső.Text[i] || row.Cells[0].Value.ToString()[i] == Char.ToUpper(box_kereső.Text[i]))
                    {
                        talált++;
                    }
                }
                if (talált == box_kereső.Text.Length)
                {
                    foreach (DataGridViewRow row2 in table.SelectedRows)
                        row2.Selected = false;

                    table.Rows[row.Index].Selected = true;
                    table.FirstDisplayedScrollingRowIndex = row.Index;
                    return;
                }
            }
        }

        private DataTable CreateSource()
        {
            data = new DataTable();

            data.Columns.Add(new DataColumn("Termékkód", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("5-6 kód", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Sarzs", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Hordószám", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Hordótípus", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Nettó töltet", Type.GetType("System.Double")));
            data.Columns.Add(new DataColumn("Szitaátmérő", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Megrendelő", Type.GetType("System.String")));
            data.Columns.Add(new DataColumn("Sorszám", Type.GetType("System.Int32")));

            return data;
        }

        private DataView CreateView( )
        {
            view = new DataView( data );
            view.Sort = "Termékkód ASC, Hordószám ASC";

            return view;

        }

        #endregion

        #region Tokenizer
        protected override void SetRow(DataRow _row, Vizsgálat.Azonosító _azonosító) { Vizsgálat.Azonosító.SetRow(_row, _azonosító); }

        protected override bool SameKeys(Vizsgálat.Azonosító _1, Vizsgálat.Azonosító _2) { return Vizsgálat.Azonosító.SameKeys(_1, _2); }

        protected override bool SameKeys(Vizsgálat.Azonosító _1, DataRow _row) { return Vizsgálat.Azonosító.SameKeys(_1, _row); }

        protected override List<Vizsgálat.Azonosító> CurrentData() { return Program.database.Vizsgálatok(); }
        #endregion

        #region EventHandlers
        private void Vizsgálat_Hozzáadás(object _sender, EventArgs _event)
        {
            Vizsgálati_Lap vizsgálati_lap = new Vizsgálati_Lap();
            vizsgálati_lap.ShowDialog();

            Program.RefreshData();
        }

        private void Vizsgálat_Módosítás(object _sender, EventArgs _event)
        {

            if (table.SelectedRows.Count != 1) return;
            if (!Program.felhasználó.Value.Jogosultsagok.Value.Vizsgalatok.Modositas) return;

            Vizsgálat.Azonosító azonosító = new Vizsgálat.Azonosító((string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.termékkód].Value,
                                                                    table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.othatkod].Value.ToString(),
                                                                    table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.sarzs].Value.ToString(), 
                                                                    (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.hordószám].Value,
                                                                    (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.hordótípus].Value, 
                                                                    (double)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.nettó_töltet].Value,
                                                                    (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.szita_átmérő].Value,
                                                                    (string)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.megrendelő].Value,
                                                                    (int)table.SelectedRows[0].Cells[Vizsgálat.Azonosító.TableIndexes.sorszám].Value);
            string q = azonosító.ToString();
            Vizsgálat? _vizsgálat = Program.database.Vizsgálat(azonosító);
            if (_vizsgálat == null) { MessageBox.Show("A kiválasztott vizsgálati lap nem található!", "Adatbázis hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            Vizsgálati_Lap vizsgálati_lap = new Vizsgálati_Lap(_vizsgálat.Value);
            vizsgálati_lap.ShowDialog();

            Program.RefreshData();
        }

        private void Vizsgálat_Törlés(object _sender, EventArgs _event)
        {
            if (table.SelectedRows.Count == 1) { if (MessageBox.Show("Biztosan törli a kiválasztott vizsgálatot?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            else if (table.SelectedRows.Count != 0) { if (MessageBox.Show("Biztosan törli a kiválasztott vizsgálatokat?", "Megerősítés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return; }
            if (!Program.felhasználó.Value.Jogosultsagok.Value.Vizsgalatok.Torles) return;
            
            foreach (DataGridViewRow selected in table.SelectedRows)
            {
                Vizsgálat.Azonosító azonosító = new Vizsgálat.Azonosító((string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.termékkód].Value,
                                                                        (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.othatkod].Value,
                                                                        (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.sarzs].Value,
                                                                        (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.hordószám].Value,
                                                                        (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.hordótípus].Value,
                                                                        (double)selected.Cells[Vizsgálat.Azonosító.TableIndexes.nettó_töltet].Value,
                                                                        (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.szita_átmérő].Value,
                                                                        (string)selected.Cells[Vizsgálat.Azonosító.TableIndexes.megrendelő].Value,
                                                                        (int)selected.Cells[Vizsgálat.Azonosító.TableIndexes.sorszám].Value);

                Vizsgálat? vizsgálat = Program.database.Vizsgálat(azonosító);
                if (vizsgálat == null) { MessageBox.Show("A kiválasztott vizsgálati lap nem található!", "Adatbázis hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (!Program.database.Vizsgálat_Törlés(vizsgálat.Value))
                {
                    MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a törlendő vizsgálat?\nTermékkód: " + azonosító.termékkód + "\nSarzs: " + azonosító.sarzs + "\nHordószám: " + azonosító.hordószám +
                      "\nSorszám: " + azonosító.sorszám, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        
            Program.RefreshData();
        }

        //

        private void table_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            table.DataBindingComplete -= table_DataBindingComplete;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.termékkód].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.othatkod].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.sarzs].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.hordószám].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.hordótípus].Width = 120;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.nettó_töltet].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.szita_átmérő].Width = 70;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.megrendelő].Width = 280 - 3;
            table.Columns[Vizsgálat.Azonosító.TableIndexes.sorszám].Visible = false;
        }

        private void Panel_Vizsgálatok_KeyDown(object _sender, KeyEventArgs _event)
        {
            if (_event.KeyCode == Keys.Enter) Vizsgálat_Módosítás(_sender, _event);
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
            private string gyártási_év = "2013";
            private Vizsgálat? eredeti;

            #region Declaration
            TextBox box_termékkód;
            TextBox box_othatkód;

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

            #region Constructor
            public Vizsgálati_Lap()
            {
                InitializeForm();
                InitializeContent();
                SetState(States.TERMÉKKÓD);
                InitializeData();
            }

            public Vizsgálati_Lap(Vizsgálat _eredeti)
            {
                eredeti = _eredeti;

                InitializeForm();
                InitializeContent();
                SetState(States.KÉSZ);
                InitializeData();
            }

            private void InitializeForm()
            {
                Text = "Vizsgálati lap";
                ClientSize = new Size(1024, 568);
                MinimumSize = ClientSize;
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                #region Formázás
                int sor = 30;
                int oszlop = 160;
                int köz = 16;
                int[] méret = { 45, 30, 55, 20, 120, 90, 110, 273, 435, 280, 230 };

                #region Labels
                Label termékkód = MainForm.createlabel("Termékkód:", 10, 10, this);
                termékkód.Font = new Font(termékkód.Font, FontStyle.Bold);

                Label szita_átmérő = MainForm.createlabel("Szita átmérő:", termékkód.Location.X, termékkód.Location.Y + sor, this);
                Label megrendelő = MainForm.createlabel("Megrendelő:", termékkód.Location.X, szita_átmérő.Location.Y + sor, this);

                Label hordószám = MainForm.createlabel("Hordószám:", termékkód.Location.X + 1 * oszlop + 20, termékkód.Location.Y, this);
                hordószám.Font = new Font(hordószám.Font, FontStyle.Bold);
                Label hőkezelés = MainForm.createlabel("Hőkezelés °C:", hordószám.Location.X, szita_átmérő.Location.Y, this);
                Label vonal = new Label();
                vonal.Location = new Point(termékkód.Location.X, megrendelő.Location.Y + sor);
                vonal.Height = 3;
                vonal.Width = 1000;
                vonal.BackColor = Color.Black;
                Label brix = MainForm.createlabel("Brix %:", termékkód.Location.X, vonal.Location.Y + sor, this);
                Label citromsav = MainForm.createlabel("Citromsav %:", termékkód.Location.X, brix.Location.Y + sor, this);
                Label borkősav = MainForm.createlabel("Borkősav:", termékkód.Location.X, citromsav.Location.Y + sor, this);
                Label ph = MainForm.createlabel("pH:  ", termékkód.Location.X, borkősav.Location.Y + sor, this);
                Label bostwick = MainForm.createlabel("Bostwick cm/30sec, 20°C:", termékkód.Location.X, ph.Location.Y + sor, this);
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
                Label töltőgép_száma = MainForm.createlabel("Töltőgép száma:", műszak_jele.Location.X + 110, szita_átmérő.Location.Y, this);
                Label terméknév = MainForm.createlabel("Terméknév:", töltőgép_száma.Location.X, termékkód.Location.Y, this);
                Label hordótípus = MainForm.createlabel("Hordótípus:", töltőgép_száma.Location.X, megrendelő.Location.Y, this);
                Label gyümölcsfajta = MainForm.createlabel("Gyümölcsfajta:",töltőgép_száma.Location.X + oszlop - 10, szita_átmérő.Location.Y, this);
                Label leoltas = MainForm.createlabel("Leoltás ideje (hhnn oopp):", sarzs.Location.X, brix.Location.Y, this);
                Label ertekeles = MainForm.createlabel("Értékelés dátuma (hhnn oopp):", sarzs.Location.X, citromsav.Location.Y, this);
                Label higit_1 = MainForm.createlabel("1. hígítás:", sarzs.Location.X, ph.Location.Y, this);
                Label higit_2 = MainForm.createlabel("2. hígítás:", sarzs.Location.X, bostwick.Location.Y, this);
                Label megjegyzes = MainForm.createlabel("Megjegyzés:", sarzs.Location.X, aszkorbinsav.Location.Y, this);
                Label összcsira = MainForm.createlabel("Összcsíra", higit_1.Location.X + oszlop / 2, borkősav.Location.Y, this);
                Label penesz = MainForm.createlabel("Penész", higit_1.Location.X + oszlop, borkősav.Location.Y, this);
                Label eleszto = MainForm.createlabel("Élesztő", higit_1.Location.X + oszlop + oszlop / 2, borkősav.Location.Y, this);
                Label minta_jele = MainForm.createlabel("Minta jele", leoltas.Location.X + 2 * oszlop - 10, brix.Location.Y, this);
                Label cimzett = MainForm.createlabel("Címzett", minta_jele.Location.X + oszlop / 2, brix.Location.Y, this);
                Label datum = MainForm.createlabel("Dátum (éé.hh.nn)", cimzett.Location.X + oszlop - 50, brix.Location.Y, this);
                Label t = MainForm.createlabel("T", minta_jele.Location.X + 30, citromsav.Location.Y, this);
                Label k1 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + sor, this);
                Label k2 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 2 * sor, this);
                Label k3 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 3 * sor, this);
                Label k4 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 4 * sor, this);
                Label k5 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 5 * sor, this);
                Label k6 = MainForm.createlabel("K", minta_jele.Location.X + 30, citromsav.Location.Y + 6 * sor, this);
                Label laboros = MainForm.createlabel("Laboros:", minta_jele.Location.X, feketepont.Location.Y, this);

                #endregion

                box_termékkód = MainForm.createtextbox(termékkód.Location.X + termékkód.Width + köz, termékkód.Location.Y, 3, méret[0], this);
                box_othatkód = MainForm.createtextbox(termékkód.Location.X + termékkód.Width + 4 * köz, termékkód.Location.Y, 2, méret[0], this);
                box_hordószám = MainForm.createtextbox(hordószám.Location.X + hordószám.Width + köz, termékkód.Location.Y, 4, méret[0], this);
                box_sarzs = MainForm.createtextbox(sarzs.Location.X + sarzs.Width + köz + 28, termékkód.Location.Y, 3, méret[0], this);
                box_terméknév = MainForm.createtextbox(terméknév.Location.X + terméknév.Width + köz, termékkód.Location.Y, 35, méret[9], this);
                box_szita_átmérő = MainForm.createtextbox(box_termékkód.Location.X, szita_átmérő.Location.Y, 10, 70, this);
                box_hőkezelés = MainForm.createtextbox("110", box_hordószám.Location.X, box_szita_átmérő.Location.Y, 3, méret[0], this);
                box_nettó_töltet = MainForm.createtextbox(box_sarzs.Location.X, nettó_töltet.Location.Y, 6, méret[0], this);
                box_műszak_jele = MainForm.createtextbox(műszak_jele.Location.X + műszak_jele.Width + köz, szita_átmérő.Location.Y, 1, méret[3], this);
                box_töltőgép_száma = MainForm.createtextbox(töltőgép_száma.Location.X + töltőgép_száma.Width + köz, töltőgép_száma.Location.Y, 1, méret[3], this);
                combo_hordótípus = MainForm.createcombobox(hordótípus.Location.X + hordótípus.Width + köz, hordótípus.Location.Y, méret[10], this);
                combo_megrendelő = MainForm.createcombobox(box_termékkód.Location.X, megrendelő.Location.Y, méret[10], this);
                combo_származási_ország = MainForm.createcombobox(származási_ország.Location.X + származási_ország.Width + köz, származási_ország.Location.Y, méret[4], this);
                combo_gyümölcsfajta = MainForm.createcombobox(gyümölcsfajta.Location.X + gyümölcsfajta.Width + köz, gyümölcsfajta.Location.Y, méret[4], this);
                box_brix = MainForm.createtextbox(citromsav_ad.Location.X + citromsav_ad.Width + köz + 8, brix.Location.Y, 4, méret[2], this);

                box_citromsav = MainForm.createtextbox(box_brix.Location.X, citromsav.Location.Y, 5, méret[2], this);
                box_borkősav = MainForm.createtextbox(box_brix.Location.X, borkősav.Location.Y, 5, méret[2], this);
                box_ph = MainForm.createtextbox(box_brix.Location.X, ph.Location.Y, 5, méret[2], this);
                box_bostwick = MainForm.createtextbox(box_brix.Location.X, bostwick.Location.Y, 4, méret[2], this);
                box_aszkorbinsav = MainForm.createtextbox(box_brix.Location.X, aszkorbinsav.Location.Y, 4, méret[2], this);
                box_citromsav_ad = MainForm.createtextbox("0", box_brix.Location.X, citromsav_ad.Location.Y, 3, méret[2], this);
                box_magtöret = MainForm.createtextbox(box_brix.Location.X, magtöret.Location.Y, 2, méret[1], this);
                box_feketepont = MainForm.createtextbox(box_brix.Location.X, feketepont.Location.Y, 4, méret[2], this);
                box_barnapont = MainForm.createtextbox(box_brix.Location.X, barnapont.Location.Y, 4, méret[2], this);
                box_szin = MainForm.createtextbox(box_magtöret.Location.X, szin.Location.Y, 60, méret[8], this);
                box_iz = MainForm.createtextbox(box_magtöret.Location.X, iz.Location.Y, 60, méret[8], this);
                box_illat = MainForm.createtextbox(box_magtöret.Location.X, illat.Location.Y, 60, méret[8], this);
                box_leoltas = MainForm.createtextbox(DateTime.Now.ToString("MMdd HHmm"), ertekeles.Location.X + ertekeles.Width + köz, leoltas.Location.Y, 11, méret[6], this);
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
                combo_laboros = MainForm.createcombobox(box_t_cimzett.Location.X, laboros.Location.Y, méret[10], this);

                box_othatkód.Text = "01";

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
                box_othatkód.TextChanged += box_termékkód_TextChanged;

                box_termékkód.TextChanged += box_hordószám_TextChanged; ;
                box_othatkód.TextChanged += box_hordószám_TextChanged; ;
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

               // TODO Címzett Leavehez kellenek, lásd lejjebb.
                box_t_cimzett.Leave += CímzettLeave;
                box_k1_cimzett.Leave += CímzettLeave;
                box_k2_cimzett.Leave += CímzettLeave;
                box_k3_cimzett.Leave += CímzettLeave;
                box_k4_cimzett.Leave += CímzettLeave;
                box_k5_cimzett.Leave += CímzettLeave;
                box_k6_cimzett.Leave += CímzettLeave;

                box_t_datum.Leave += MainForm.OnlyDate;
                box_k1_datum.Leave += MainForm.OnlyDate;
                box_k2_datum.Leave += MainForm.OnlyDate;
                box_k3_datum.Leave += MainForm.OnlyDate;
                box_k4_datum.Leave += MainForm.OnlyDate;
                box_k5_datum.Leave += MainForm.OnlyDate;
                box_k6_datum.Leave += MainForm.OnlyDate;
                box_leoltas.Leave += MainForm.OnlyTime;
                box_ertekeles.Leave += MainForm.OnlyTime;
                #endregion

                #region Data
                // TODO REFRESHBE!

                List<string> megrendelok = Program.database.Megrendelők();
                foreach (string item in megrendelok)
                {
                    combo_megrendelő.Items.Add(item);
                }
                combo_megrendelő.SelectedIndex = 0;

                List<TORZSADAT> seged = Program.database.Törzsadatok("Hordótípus");
                foreach (TORZSADAT item in seged)
                {
                    combo_hordótípus.Items.Add(item.Azonosito);
                }
                combo_hordótípus.SelectedIndex = 0;

                seged.Clear();
                seged = Program.database.Törzsadatok("Származási ország");
                foreach (TORZSADAT item in seged)
                {
                    combo_származási_ország.Items.Add(item.Azonosito);
                }
                combo_származási_ország.SelectedIndex = 0;

                seged.Clear();
                seged = Program.database.Törzsadatok("Laboros");
                foreach (TORZSADAT item in seged)
                {
                    combo_laboros.Items.Add(item.Azonosito);
                }
                #endregion

                Button rendben = new Button();
                rendben.Text = "Rendben";
                rendben.Size = new Size(96, 32);
                rendben.Location = new Point(ClientRectangle.Width - rendben.Size.Width - 16, ClientRectangle.Height - rendben.Size.Height - 16);
                rendben.Click += rendben_Click;

                Controls.Add(rendben);
                Controls.Add(vonal);
            }

            private void InitializeData()
            {
                if (eredeti != null)
                {
                    box_termékkód.Text = eredeti.Value.azonosító.termékkód;
                    box_othatkód.Text = eredeti.Value.azonosító.othatkod;
                    box_szita_átmérő.Text = eredeti.Value.azonosító.szita_átmérő;
                    box_hordószám.Text = eredeti.Value.azonosító.hordószám;
                    box_hőkezelés.Text = eredeti.Value.adatok1.hőkezelés.ToString();
                    box_brix.Text = eredeti.Value.adatok2.brix.ToString();
                    box_citromsav.Text = eredeti.Value.adatok2.citromsav.ToString();
                    box_borkősav.Text = eredeti.Value.adatok2.borkősav.ToString();
                    box_ph.Text = eredeti.Value.adatok2.ph.ToString();
                    box_bostwick.Text = eredeti.Value.adatok2.bostwick.ToString();
                    box_aszkorbinsav.Text = eredeti.Value.adatok2.aszkorbinsav.ToString();
                    box_citromsav_ad.Text = eredeti.Value.adatok2.citromsav_adagolás.ToString();
                    box_magtöret.Text = eredeti.Value.adatok2.magtöret.ToString();
                    box_feketepont.Text = eredeti.Value.adatok2.feketepont.ToString();
                    box_barnapont.Text = eredeti.Value.adatok2.barnapont.ToString();
                    box_szin.Text = eredeti.Value.adatok2.szín;
                    box_iz.Text = eredeti.Value.adatok2.íz;
                    box_illat.Text = eredeti.Value.adatok2.illat;
                    box_nettó_töltet.Text = eredeti.Value.azonosító.nettó_töltet.ToString();
                    box_sarzs.Text = eredeti.Value.azonosító.sarzs;
                    box_műszak_jele.Text = eredeti.Value.adatok1.műszak_jele;
                    box_terméknév.Text = eredeti.Value.adatok1.terméknév;
                    box_töltőgép_száma.Text = eredeti.Value.adatok1.töltőgép;
                    box_leoltas.Text = eredeti.Value.adatok3.leoltás;
                    box_ertekeles.Text = eredeti.Value.adatok3.értékelés;
                    box_megjegyzes.Text = eredeti.Value.adatok3.megjegyzés;
                    box_összcsíra_higit_1.Text = eredeti.Value.adatok3.összcsíra_1.ToString();
                    box_összcsíra_higit_2.Text = eredeti.Value.adatok3.összcsíra_2.ToString();
                    box_penész_higit_1.Text = eredeti.Value.adatok3.penész_1.ToString();
                    box_penész_higit_2.Text = eredeti.Value.adatok3.penész_2.ToString();
                    box_élesztő_higit_1.Text = eredeti.Value.adatok3.élesztő_1.ToString();
                    box_élesztő_higit_2.Text = eredeti.Value.adatok3.élesztő_2.ToString();
                    box_t_cimzett.Text = eredeti.Value.adatok4.címzett_t;
                    box_t_datum.Text = eredeti.Value.adatok4.dátum_t;
                    box_k1_cimzett.Text = eredeti.Value.adatok4.címzett_k1;
                    box_k1_datum.Text = eredeti.Value.adatok4.dátum_k1;
                    box_k2_cimzett.Text = eredeti.Value.adatok4.címzett_k2;
                    box_k2_datum.Text = eredeti.Value.adatok4.dátum_k2;
                    box_k3_cimzett.Text = eredeti.Value.adatok4.címzett_k3;
                    box_k3_datum.Text = eredeti.Value.adatok4.dátum_k3;
                    box_k4_cimzett.Text = eredeti.Value.adatok4.címzett_k4;
                    box_k4_datum.Text = eredeti.Value.adatok4.dátum_k4;
                    box_k5_cimzett.Text = eredeti.Value.adatok4.címzett_k5;
                    box_k5_datum.Text = eredeti.Value.adatok4.dátum_k5;
                    box_k6_cimzett.Text = eredeti.Value.adatok4.címzett_k6;
                    box_k6_datum.Text = eredeti.Value.adatok4.dátum_k6;
                    combo_laboros.SelectedItem = eredeti.Value.adatok4.laboros;
                    combo_származási_ország.SelectedItem = eredeti.Value.adatok1.szárm_ország;
                    combo_hordótípus.SelectedItem = eredeti.Value.azonosító.hordótípus;
                    combo_megrendelő.SelectedItem = eredeti.Value.azonosító.megrendelő;

                    List<string> gyümölcsfajták = Program.database.Gyümölcsfajták(eredeti.Value.azonosító.termékkód);
                    foreach (string item in gyümölcsfajták)
                    {
                        combo_gyümölcsfajta.Items.Add(item);
                    }
                    combo_gyümölcsfajta.Text = eredeti.Value.adatok1.gyümölcsfajta;

                    box_termékkód.Enabled = false;
                    
                    box_hordószám.Enabled = false;
                }
            }
            #endregion

            #region States
            private enum States
            {
                NINCS = 0,
                TERMÉKKÓD = 1,
                OTHATKOD = 2,
                TERMÉKKÓD_BEÁLLÍTÁS = 3,
                HORDÓSZÁM = 4,
                KÉSZ = 5
            }
            private States state = States.NINCS;

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
                        combo_gyümölcsfajta.Items.Clear();

                        box_termékkód.Enabled = true;
                        box_termékkód.Focus();
                        break;

                    case States.OTHATKOD:
                        foreach (Control control in Controls)
                        {
                            control.Enabled = false;
                        }
                        box_termékkód.Enabled = true;
                        box_hordószám.Enabled = true;
                        box_othatkód.Enabled = true;
                        box_othatkód.Focus();
                        break;

                    case States.HORDÓSZÁM:
                        foreach (Control control in Controls)
                        {
                            control.Enabled = false;
                        }

                        box_termékkód.Enabled = true;
                        box_hordószám.Enabled = true;
                        box_othatkód.Enabled = true;
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
            #endregion

            #region EventHandlers
            private void CímzettLeave(object _sender, EventArgs _event)
            {
                string DateTimeNow = DateTime.Now.ToString("yy.MM.dd");

                if (_sender as TextBox == box_t_cimzett && box_t_cimzett.Text != "") { if (box_t_datum.Text == "") { box_t_datum.Text = DateTimeNow; } }
                if (_sender as TextBox == box_k1_cimzett && box_k1_cimzett.Text != "") { if (box_k1_datum.Text == "") { box_k1_datum.Text = DateTimeNow; } }
                if (_sender as TextBox == box_k2_cimzett && box_k2_cimzett.Text != "") { if (box_k2_datum.Text == "") { box_k2_datum.Text = DateTimeNow; } }
                if (_sender as TextBox == box_k3_cimzett && box_k3_cimzett.Text != "") { if (box_k2_datum.Text == "") { box_k2_datum.Text = DateTimeNow; } }
                if (_sender as TextBox == box_k4_cimzett && box_k4_cimzett.Text != "") { if (box_k3_datum.Text == "") { box_k3_datum.Text = DateTimeNow; } }
                if (_sender as TextBox == box_k5_cimzett && box_k4_cimzett.Text != "") { if (box_k4_datum.Text == "") { box_k4_datum.Text = DateTimeNow; } }
                if (_sender as TextBox == box_k6_cimzett && box_k6_cimzett.Text != "") { if (box_k5_datum.Text == "") { box_k5_datum.Text = DateTimeNow; } }
            }

            private void box_termékkód_TextChanged(object _sender, EventArgs _event)
            {
                if (box_termékkód.Text.Length == 3 && state != States.KÉSZ && box_othatkód.Text.Length == 2)
                {
                    if (!Database.IsCorrectSQLText(box_termékkód.Text)) { MessageBox.Show("Nem megfelelő karakter a termékkódban!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    List<string> temp = Program.database.Termékkódok(box_termékkód.Text, box_othatkód.Text);
                    if (temp.Count == 0) { MessageBox.Show("Nem található ilyen termékkódú cikk!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                    ListBox termékkódválasztó = new ListBox();
                    termékkódválasztó.Location = new Point(box_termékkód.Location.X, box_termékkód.Location.Y);
                    termékkódválasztó.Width = 300;
                    termékkódválasztó.KeyDown += termékkódválasztó_KeyDown;
                    termékkódválasztó.Leave += termékkódválasztó_Leave;

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
                    //SetState(States.TERMÉKKÓD);
                }
            }


            private void termékkódválasztó_KeyDown(object _sender, KeyEventArgs _event)
            {
                if (_event.KeyCode == Keys.Enter)
                {
                    ListBox termékkódválasztó = (ListBox) _sender;
                    Controls.Remove(termékkódválasztó);

                    if (state == States.TERMÉKKÓD)
                    {
                        SetState(States.OTHATKOD);
                    }
                    else if (state == States.OTHATKOD)
                    {
                        SetState(States.HORDÓSZÁM);
                    }

                    box_terméknév.Text = Program.database.Vizsgálat_Terméknév(termékkódválasztó.SelectedItem.ToString().Substring(0, 4));

                    List<string> gyümölcsfajták = Program.database.Gyümölcsfajták(box_termékkód.Text);
                    foreach (string item in gyümölcsfajták)
                    {
                        combo_gyümölcsfajta.Items.Add(item);
                    }
                    if(combo_gyümölcsfajta.Items.Count!=0)
                    combo_gyümölcsfajta.SelectedIndex = 0;
                }
            }
       
            private void termékkódválasztó_Leave(object _sender, EventArgs _event)
            {
                ListBox termékkódválasztó = (ListBox)_sender;
                termékkódválasztó.Focus();
            }

            private void box_hordószám_TextChanged(object _sender, EventArgs _event)
            {
                if (box_hordószám.Text.Length == 4 && box_termékkód.Text.Length == 3 && box_othatkód.Text.Length == 2 )
                {
                    string prodid = "12" + box_termékkód.Text.Substring(0, 2) + box_othatkód.Text + box_termékkód.Text.Substring(2, 1) + "_0" + box_termékkód.Text.Substring(2, 1) + box_hordószám.Text;

                    if (!Database.IsCorrectSQLText(prodid))
                        { MessageBox.Show("Nem megfelelő karakter a lekérdezésben!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    if (eredeti == null && !Program.database.Vizsgálat_Hordószám_Ellenőrzés(box_termékkód.Text, box_hordószám.Text, gyártási_év))
                        { MessageBox.Show("Már létezik ehhez a termékkódhoz ilyen hordószám erre az évre!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    List<string> fejléc_adatok = Program.database.Vizsgálat_Fejlécadatok(prodid);
                    if (fejléc_adatok.Count != 0)
                    {
                        box_nettó_töltet.Text = fejléc_adatok[0];
                        box_műszak_jele.Text = fejléc_adatok[1];
                        box_töltőgép_száma.Text = fejléc_adatok[2];
                        box_sarzs.Text = fejléc_adatok[3];
                        box_szita_átmérő.Text = fejléc_adatok[4];

                        string hordótípus = Program.database.Vizsgálat_Hordótípus_Ellenőrzés(box_termékkód.Text, box_hordószám.Text, gyártási_év, box_sarzs.Text);
                        if (hordótípus != null)
                        {
                            combo_hordótípus.Text = hordótípus;
                        }

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
                    //if (state != States.HORDÓSZÁM) SetState(States.HORDÓSZÁM);
                }
            }

            private void box_citromsav_Leave(object sender, EventArgs e)
            {
                if (box_citromsav.Text != "")
                {
                    try
                    {
                        box_borkősav.Text = Convert.ToDouble(((Convert.ToDouble(box_citromsav.Text) * 1.171875))).ToString("F2");
                    }
                    catch (Exception)
                    {
                        box_citromsav.Focus();
                    }
                }
                else box_borkősav.Text = "";
            }

               
            private void rendben_Click(object _sender, EventArgs _event)
            {
                gyártási_év = "201" +  box_termékkód.Text.Substring(box_termékkód.Text.Length - 1, 1);
                Vizsgálat.Azonosító azonosító = new Vizsgálat.Azonosító(
                        box_termékkód.Text,
                        box_othatkód.Text,
                        box_sarzs.Text,
                        box_hordószám.Text,
                        combo_hordótípus.Text,
                        Convert.ToDouble(box_nettó_töltet.Text),
                        box_szita_átmérő.Text,
                        combo_megrendelő.Text,
                        eredeti == null ? null : eredeti.Value.azonosító.sorszám);

                Vizsgálat.Adatok1 adatok1 = new Vizsgálat.Adatok1(
                       box_terméknév.Text,
                       Convert.ToInt32(box_hőkezelés.Text),
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
                    MainForm.ConvertOrDie<int>(box_aszkorbinsav.Text),
                    MainForm.ConvertOrDie<int>(box_citromsav_ad.Text),
                    MainForm.ConvertOrDie<int>(box_magtöret.Text),
                    MainForm.ConvertOrDie<int>(box_feketepont.Text),
                    MainForm.ConvertOrDie<int>(box_barnapont.Text),
                    MainForm.ConvertOrDieSQLString(box_szin.Text),
                    MainForm.ConvertOrDieSQLString(box_iz.Text),
                    MainForm.ConvertOrDieSQLString(box_illat.Text));

                Vizsgálat.Adatok3 adatok3 = new Vizsgálat.Adatok3(
                    MainForm.ConvertOrDieSQLString(box_leoltas.Text),
                    MainForm.ConvertOrDieSQLString(box_ertekeles.Text),
                    MainForm.ConvertOrDie<int>(box_összcsíra_higit_1.Text),
                    MainForm.ConvertOrDie<int>(box_összcsíra_higit_2.Text),
                    MainForm.ConvertOrDie<int>(box_penész_higit_1.Text),
                    MainForm.ConvertOrDie<int>(box_penész_higit_2.Text),
                    MainForm.ConvertOrDie<int>(box_élesztő_higit_1.Text),
                    MainForm.ConvertOrDie<int>(box_élesztő_higit_2.Text),
                    MainForm.ConvertOrDieSQLString(box_megjegyzes.Text));

                Vizsgálat.Adatok4 adatok4 = new Vizsgálat.Adatok4(
                    MainForm.ConvertOrDieSQLString(box_t_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_t_datum.Text),
                    MainForm.ConvertOrDieSQLString(box_k1_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_k1_datum.Text),
                    MainForm.ConvertOrDieSQLString(box_k2_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_k2_datum.Text),
                    MainForm.ConvertOrDieSQLString(box_k3_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_k3_datum.Text),
                    MainForm.ConvertOrDieSQLString(box_k4_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_k4_datum.Text),
                    MainForm.ConvertOrDieSQLString(box_k5_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_k5_datum.Text),
                    MainForm.ConvertOrDieSQLString(box_k6_cimzett.Text),
                    MainForm.ConvertOrDieSQLString(box_k6_datum.Text),
                    MainForm.ConvertOrDieSQLString(combo_laboros.Text));

                Vizsgálat _vizsgálat = new Vizsgálat(azonosító, adatok1, adatok2, adatok3, adatok4);

                bool success = false;
                if (eredeti != null)
                {
                    success = Program.database.Vizsgálat_Módosítás(eredeti.Value, _vizsgálat);
                }
                else
                {
                    success = Program.database.Vizsgálat_Hozzáadás(_vizsgálat);
                }

                if (!success)
                {
                    MessageBox.Show("Adatbázis hiba!\nLehetséges, hogy nem létezik már a módosítandó vizsgálat?\nTermékkód: " + azonosító.termékkód + "\nSarzs: " + azonosító.sarzs + "\nHordószám: " + azonosító.hordószám +
                        "\nSorszám: " + azonosító.sorszám, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!Program.database.Vizsgálat_Módosítás_Hordótípus(box_termékkód.Text, box_sarzs.Text, gyártási_év, combo_hordótípus.Text))
                    {
                        MessageBox.Show("Adatbázis hiba a hordótípusok módosításakor!\nTermékkód: " + azonosító.termékkód + "\nSarzs: " + azonosító.sarzs + "\nGyártási év: " + gyártási_év, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                Close();
            }
            #endregion
        }
    }
}
