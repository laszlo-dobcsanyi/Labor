using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labor
{
    public struct Node_Konszignacio
    {
        public struct Fejlec
        {
            public struct Vevo
            {
                public string Nev;
                public string Varos;
                public string Cim;
                public string HazSzam;

                public Vevo(string _Nev, string _Varos, string _Cim, string _HazSzam)
                {
                    Nev = _Nev;
                    Varos = _Varos;
                    Cim = _Cim;
                    HazSzam = _HazSzam;
                }
            }

            public struct Felado
            {
                public string Nev;
                public string Cim;

                public Felado(string _Nev, string _Cim)
                {
                    Nev = _Nev;
                    Cim = _Cim;
                }
            }

            public struct Szallitolevel
            {
                public string[] Rendszamok;
                public string Datum;
                public string Szoveg;

                public Szallitolevel(KonszignacioSzallitolevel _szallitolevel)
                {
                    Rendszamok = new string[] { _szallitolevel.Rendszam1, _szallitolevel.Rendszam2 };
                    Datum = _szallitolevel.ElszallitasIdeje;
                    Szoveg = _szallitolevel.Szallitolevel;
                }
            }

            public Felado felado;
            public Vevo vevo;
            public Szallitolevel szallitolevel;
        }

        public struct GyumolcsTipus
        {
            public struct Adat
            {
                public string Hordo;
                public string Sarzs;
                public double NettoSuly;
                public string HordoTipus;
                public string GyartasDatum;

                public Adat( string _Hordo, string _Sarzs, double _NettoSuly, string _HordoTipus, string _GyartasDatum)
                {
                    Hordo = _Hordo;
                    Sarzs = _Sarzs;
                    NettoSuly = _NettoSuly;
                    HordoTipus = _HordoTipus;
                    GyartasDatum = _GyartasDatum;
                }
            }

            public GyumolcsTipus(string _vtsz, string _megnevezes)
            {
                adat = new List<Adat>();
                Megnevezes = _megnevezes;
                VTSz = _vtsz;
                OsszSuly = 0;
            }
            public List<Adat> adat;

            public string VTSz;
            public string Megnevezes;
            public double OsszSuly;

        }

        public Fejlec fejlec;
        public List<GyumolcsTipus> gyumolcstipusok;
    }

    public struct Node_MinosegBizonylat
    {
        public struct SzallitoLevel
        {
            public Int16 SzallitolevelSzam;
            public string Vevo;
            public string GyartasiIdo;
            public string Szin;
            public string Iz;
            public string Illat;

            public SzallitoLevel(KonszignacioSzallitolevel _szallitolevel)
            {
                SzallitolevelSzam =_szallitolevel.SzallitolevelSzam;
                Vevo = _szallitolevel.Vevo;
                GyartasiIdo = _szallitolevel.GyartasiIdo;
                Szin = _szallitolevel.Szin;
                Iz = _szallitolevel.Iz;
                Illat = _szallitolevel.Illat;
            }
        }

        public struct VizsgalatiLap
        {
            public MinMaxPair<double?> Brix;
            public MinMaxPair<double?> Citromsav;
            public MinMaxPair<double?> Ph;
            public MinMaxPair<double?> Bostwick;
            public MinMaxPair<double?> CitromsavAdagolas;
            public double? Aszkorbinsav;
            public string Hoteko;
            public string Sarzs;
            public string Megnevezes;
            public string Paszirozottsag;
            public string Csomagolas;
            public string SzarmazasiHely;


            public VizsgalatiLap(MinMaxPair<double?> _Brix,
                                 MinMaxPair<double?> _Citromsav,
                                 MinMaxPair<double?> _Ph,
                                 MinMaxPair<double?> _Bostwick,
                                 MinMaxPair<double?> _CitromsavAdagolas,
                                 double? _Aszkorbinsav,
                                 string _Hoteko,
                                 string _Sarzs,
                                 string _Megnevezes,
                                 string _Paszirozottsag,
                                 string _Csomagolas,
                                 string _SzarmazasiHely)
            {
                Brix = _Brix;
                Citromsav = _Citromsav;
                Ph = _Ph;
                Bostwick = _Bostwick;
                CitromsavAdagolas = _CitromsavAdagolas;
                Aszkorbinsav = _Aszkorbinsav;
                Hoteko = _Hoteko;
                Sarzs = _Sarzs;
                Megnevezes = _Megnevezes;
                Paszirozottsag = _Paszirozottsag;
                Csomagolas = _Csomagolas;
                SzarmazasiHely = _SzarmazasiHely;
            }
        } //kész

        public struct Tapertek
        {
            public Int16? EnergiaTartalom1;
            public Int16? EnergiaTartalom2;
            public double? Feherje;
            public double? Szenhidrat;
            public double? Zsir;
            public double? Elelmirost;

            public Tapertek(Int16? _EnergiaTartalom1,
                            Int16? _EnergiaTartalom2,
                            double? _Feherje,
                            double? _Szenhidrat,
                            double? _Zsir,
                            double? _Elelmirost)
            {
                EnergiaTartalom1 = _EnergiaTartalom1;
                EnergiaTartalom2 = _EnergiaTartalom2;
                Feherje = _Feherje;
                Szenhidrat = _Szenhidrat;
                Zsir = _Zsir;
                Elelmirost = _Elelmirost;
            }
        }//kész

        public class FixString
        {
            public string Cukor;
            public string Szinezek;
            public string Aroma;
            public string Tartositoszer;
            public string MikroBiologia;
            public string MinosegetMegorzi;
            public string NettoTomeg;
            public string Tarolas;
            public string Ethanol;
            public string HMF;

            public FixString(string _nyelv)
            {

                if (_nyelv == "M")
                {
                    Cukor = "nincs";
                    Szinezek = "nincs";
                    Aroma = "nincs";
                    Tartositoszer = "nincs";
                    MikroBiologia = "technológiailag steril";
                    MinosegetMegorzi = "a hordó címkéjén feltüntetett időpontig";
                    NettoTomeg = "a hordó címkéjén szereplő töltőtömeg";
                    Tarolas = "0 - 20°C közötti hőmérsékleten, felbontás nélkül";
                    Ethanol = "0 - 20°C közötti hőmérsékleten, felbontás nélkül";
                    HMF = "max. 5 mg/l";
                }
                else if (_nyelv == "A")
                {
                    Cukor = "no";
                    Szinezek = "no";
                    Aroma = "no";
                    Tartositoszer = "no";
                    MikroBiologia = "aseptic technology";
                    MinosegetMegorzi = "see on label of drum";
                    NettoTomeg = "as labelled";
                    Tarolas = "between 0 and 20°C, without opening";
                    Ethanol = "between 0 and 20°C, without opening";
                    HMF = "max. 5 mg/l";
                }
            }
        } 

        public struct Felhasznalo
        {
            public string Nev;
            public string Beosztas;

            public Felhasznalo(string _nyelv)
            {
                if(_nyelv == "M")
                {
                    Nev =  Program.felhasználó.Value.név1;
                    Beosztas= Program.felhasználó.Value.beosztás1;
                }
                else
                {
                   Nev= Program.felhasználó.Value.név2;
                   Beosztas = Program.felhasználó.Value.beosztás2;
                }
            }
        }

        public SzallitoLevel szallitolevel;
        public VizsgalatiLap vizsgalatilap;
        public Tapertek tapertek;
        public FixString fixstring;
        public Felhasznalo felhasznalo;
    }

    public struct Node_MinBiz_Szöveg
    {
        public string MagyarSzoveg1;
        public string AngolSzoveg1;
        public string MagyarSzoveg2;
        public string AngolSzoveg2;

        public Node_MinBiz_Szöveg(string _MagyarSzoveg1,
                                  string _AngolSzoveg1,
                                  string _MagyarSzoveg2,
                                  string _AngolSzoveg2)
        {
            MagyarSzoveg1 = _MagyarSzoveg1;
            AngolSzoveg1 = _AngolSzoveg1;
            MagyarSzoveg2 = _MagyarSzoveg2;
            AngolSzoveg2 = _AngolSzoveg2;
        }
    }

    public sealed class Nyomtat
    {
        static Regex regex;

        public static void
        Nyomtat_Konszignacio(KonszignacioSzallitolevel _szállítólevél, List<Foglalás> _foglalások)
        {
            regex = new Regex(@"[ ]{2,}", RegexOptions.None);

            Node_Konszignacio Konszignacio = new Node_Konszignacio();

            Konszignacio.fejlec = new Node_Konszignacio.Fejlec();
            Konszignacio.fejlec.felado = new Node_Konszignacio.Fejlec.Felado("Marillen Gyümölcsfeldolgozó Kft",
                                                                             "Kiskunfélegyháza, VIII. ker. 99/A");
            Konszignacio.fejlec.vevo = Program.database.Konszignáció_Vevő(_szállítólevél.Vevo);
            Konszignacio.fejlec.szallitolevel = new Node_Konszignacio.Fejlec.Szallitolevel(_szállítólevél);
            Konszignacio.gyumolcstipusok = new List<Node_Konszignacio.GyumolcsTipus>();

            double összes_súly = 0;

            foreach (Foglalás foglalás_iterator in _foglalások)
            {
                összes_súly = 0;

                List<Hordó> hordók = Program.database.Konszignáció_Hordók(foglalás_iterator.id);
                List<string> hordó_termékkódok = new List<string>();

                for (int i = 0; i < hordók.Count; i++)
                {
                    bool found = false;
                    for (int j = 0; j < hordó_termékkódok.Count; j++)
                    {
                        if (hordók[i].termékkód == hordó_termékkódok[j]) { found = true; break; }
                    }
                    if (!found) hordó_termékkódok.Add(hordók[i].termékkód);
                }

                foreach (string item in hordó_termékkódok)
                {
                    bool found = false;
                    Node_Konszignacio.GyumolcsTipus temp = Program.database.Konszignáció_Gyümölcstípus(item);
                    foreach (Node_Konszignacio.GyumolcsTipus gyitem in Konszignacio.gyumolcstipusok)
                    {
                        if (temp.Megnevezes == gyitem.Megnevezes && temp.VTSz == gyitem.VTSz)
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        Konszignacio.gyumolcstipusok.Add(Program.database.Konszignáció_Gyümölcstípus(item));
                    }
                }

                for (int i = 0; i < Konszignacio.gyumolcstipusok.Count; i++)
                {
                    foreach (Hordó inner in hordók)
                    {
                        if (Konszignacio.gyumolcstipusok[i].Megnevezes == Program.database.Name(inner.termékkód))
                        {
                            Node_Konszignacio.GyumolcsTipus.Adat temp = new Node_Konszignacio.GyumolcsTipus.Adat(inner.gyártási_év[3] + inner.id, inner.sarzs, Convert.ToDouble(inner.mennyiség), "", inner.time.Substring(0, 11));
                            List<Vizsgálat.Azonosító> vizsgálatok = Program.database.Vizsgálatok();
                            foreach (Vizsgálat.Azonosító item in vizsgálatok)
                            {
                                if (item.termékkód == inner.termékkód && item.sarzs == inner.sarzs)
                                {
                                    temp.HordoTipus = item.hordótípus;
                                }
                            }
                            Konszignacio.gyumolcstipusok[i].adat.Add(temp);
                            Node_Konszignacio.GyumolcsTipus tempgy = Konszignacio.gyumolcstipusok[i];
                            tempgy.OsszSuly += temp.NettoSuly;
                            Konszignacio.gyumolcstipusok[i] = tempgy;
                        }
                    }
                }
            }

            if (!Directory.Exists("Listák"))
            {
                Directory.CreateDirectory("Listák");
            }

            string filename = "Listák//" + _szállítólevél.Szallitolevel + ".docx";
            var document = DocX.Create(filename);
            document.DifferentFirstPage = true;
            document.AddHeaders();

            var titleFormat = new Formatting();
            titleFormat.Size = 14D;
            titleFormat.Position = 1;
            titleFormat.Spacing = 5;
            titleFormat.Bold = true;

            Header FirstPageHeader = document.Headers.first;


            Paragraph HeaderParagraph = FirstPageHeader.InsertParagraph("Konszignáció                  \n", false, titleFormat);

            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"Marillen_logo.png");

                myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                ms.Seek(0, SeekOrigin.Begin);

                Novacode.Image img = document.AddImage(ms); // Create image.

                Picture pic1 = img.CreatePicture(100, 150);     // Create picture.

                HeaderParagraph.InsertPicture(pic1, 20); // Insert picture into paragraph.
                HeaderParagraph.Alignment = Alignment.right;
            }

            HeaderParagraph.Bold();
            titleFormat.Position = 12;

            #region Fejléc
            Table table_fejléc = document.AddTable(2, 4);
            table_fejléc.Alignment = Alignment.left;
            table_fejléc.Rows[0].Cells[0].Paragraphs[0].Append("Vevő:").Bold();
            table_fejléc.Rows[1].Cells[0].Paragraphs[0].Append("Gépkocsi:").Bold();
            table_fejléc.Rows[0].Cells[2].Paragraphs[0].Append("Feladó:").Bold();
            table_fejléc.Rows[1].Cells[2].Paragraphs[0].Append("Dátum:").Bold();

            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append(Konszignacio.fejlec.szallitolevel.Datum + "    ");
            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append("Szállítólevél:").Bold();
            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append(" " + Konszignacio.fejlec.szallitolevel.Szoveg);

            table_fejléc.Rows[0].Cells[1].Paragraphs[0].Append(Konszignacio.fejlec.vevo.Nev);
            table_fejléc.Rows[0].Cells[1].Paragraphs[0].AppendLine(Konszignacio.fejlec.vevo.Varos);
            table_fejléc.Rows[0].Cells[1].Paragraphs[0].AppendLine(Konszignacio.fejlec.vevo.Cim + " " + Konszignacio.fejlec.vevo.HazSzam);

            table_fejléc.Rows[1].Cells[1].Paragraphs[0].Append(Konszignacio.fejlec.szallitolevel.Rendszamok[0]);
            table_fejléc.Rows[1].Cells[1].Paragraphs[0].Append(" " + Konszignacio.fejlec.szallitolevel.Rendszamok[1]);

            table_fejléc.Rows[0].Cells[3].Paragraphs[0].Append(Konszignacio.fejlec.felado.Nev);
            table_fejléc.Rows[0].Cells[3].Paragraphs[0].AppendLine(Konszignacio.fejlec.felado.Cim);


            KonszignacioFejlecTablazatFormazas(table_fejléc);
            FirstPageHeader.InsertTable(table_fejléc);



            #endregion

            #region Data_Table
            Paragraph paragraph_data_table = document.InsertParagraph();

            Table data_table = document.AddTable(1, 7);

            data_table.Rows[0].Cells[0].Paragraphs[0].Append("S.Sz.").Bold();
            data_table.Rows[0].Cells[1].Paragraphs[0].Append("Megnevezés").Bold();
            data_table.Rows[0].Cells[2].Paragraphs[0].Append("Hordó").Bold();
            data_table.Rows[0].Cells[3].Paragraphs[0].Append("Sarzs").Bold();
            data_table.Rows[0].Cells[4].Paragraphs[0].Append("Nettó súly").Bold();
            data_table.Rows[0].Cells[5].Paragraphs[0].Append("Hordó típus").Bold();
            data_table.Rows[0].Cells[6].Paragraphs[0].Append("Gyártás dátuma").Bold();




            int c = 1;
            int sorszám = 1;
            KonszignacioRendezes(Konszignacio);
            foreach (Node_Konszignacio.GyumolcsTipus outer in Konszignacio.gyumolcstipusok)
            {
                string temp = regex.Replace(outer.Megnevezes, @" ");
                foreach (Node_Konszignacio.GyumolcsTipus.Adat inner in outer.adat)
                {
                    data_table.InsertRow();

                    data_table.Rows[c].Cells[0].Paragraphs[0].Append(sorszám.ToString() + '.');
                    sorszám++;
                    data_table.Rows[c].Cells[1].Paragraphs[0].Append(temp);
                    data_table.Rows[c].Cells[2].Paragraphs[0].Append(inner.Hordo.ToString());
                    data_table.Rows[c].Cells[3].Paragraphs[0].Append(inner.Sarzs);
                    data_table.Rows[c].Cells[4].Paragraphs[0].Append(inner.NettoSuly + " kg");
                    data_table.Rows[c].Cells[5].Paragraphs[0].Append(inner.HordoTipus);
                    data_table.Rows[c].Cells[6].Paragraphs[0].Append(inner.GyartasDatum);

                    c++;
                }

                data_table.InsertRow();
                data_table.InsertRow();
                data_table.Rows[c].Cells[1].Paragraphs[0].Append(temp + "összesen:").Bold();
                data_table.Rows[c].Cells[4].Paragraphs[0].Append(outer.OsszSuly + " kg").Bold();
                összes_súly += outer.OsszSuly;
                data_table.Rows[c].Cells[5].Paragraphs[0].Append("VTSZ:").Bold();
                data_table.Rows[c].Cells[6].Paragraphs[0].Append(outer.VTSz).Bold();
                c += 2;
            }
            data_table.InsertRow();
            data_table.Rows[c].Cells[1].Paragraphs[0].Append("Összes elszállítás:").Bold();
            data_table.Rows[c].Cells[4].Paragraphs[0].Append(összes_súly + " kg").Bold();


            for (int i = 0;
                i < data_table.Rows.Count;
                ++i)
            {
                data_table.Rows[i].Cells[3].Paragraphs[0].Alignment = Alignment.right;
                data_table.Rows[i].Cells[4].Paragraphs[0].Alignment = Alignment.right;
                data_table.Rows[i].Cells[6].Paragraphs[0].Alignment = Alignment.right;

            }
            KonszignacioDataTableFormazas(data_table);
            document.InsertTable(data_table);

            #endregion

            try { document.Save(); }
            catch (System.Exception) { MessageBox.Show("A dokumentum meg van nyitva!"); }

        }

        public static void
        Nyomtat_MinosegBizonylatok(KonszignacioSzallitolevel _szállítólevél, List<Foglalás> _foglalások)
        {
            regex = new Regex(@"[ ]{2,}", RegexOptions.None);

            List<Node_MinosegBizonylat> data = new List<Node_MinosegBizonylat>();

            #region Data
            Node_MinosegBizonylat temp = new Node_MinosegBizonylat();
            temp.fixstring = new Node_MinosegBizonylat.FixString(_szállítólevél.Nyelv);
            temp.felhasznalo = new Node_MinosegBizonylat.Felhasznalo(_szállítólevél.Nyelv);
            temp.szallitolevel = new Node_MinosegBizonylat.SzallitoLevel(_szállítólevél);

            List<string> hotekok = Program.database.MinőségBizonylatHotekok(_foglalások);
            foreach (string item in hotekok)
            {
                temp.vizsgalatilap = Program.database.MinőségBizonylat(_foglalások, item);
                temp.tapertek = Program.database.MinBiz_Tápérték(temp.vizsgalatilap.Hoteko);
                data.Add(temp);
            }

            #endregion

            if (!Directory.Exists("Listák"))
            {
                Directory.CreateDirectory("Listák");
            }

            string filename = "Listák//" + _szállítólevél.Szallitolevel + "-MinBiz.docx";
            var document = DocX.Create(filename);
            document.AddHeaders();
            document.AddFooters();

            #region Header

            Header header = document.Headers.odd;
            Paragraph paragraph_header = header.InsertParagraph();
            paragraph_header.Direction = Direction.LeftToRight;

            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"Marillen_fejlec.jpg");

                myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                ms.Seek(0, SeekOrigin.Begin);

                Novacode.Image img = document.AddImage(ms); // Create image.
                Picture pic1 = img.CreatePicture();     // Create picture.

                paragraph_header.AppendPicture(pic1);
                paragraph_header.Alignment = Alignment.center;

                var titleFormat = new Formatting();
                titleFormat.Size = 18D;
                titleFormat.Position = 1;
                titleFormat.Spacing = 5;
                titleFormat.Bold = true;
                Paragraph title = null;
                if (_szállítólevél.Nyelv == "M")
                {
                    title = header.InsertParagraph("MINŐSÉGI BIZONYÍTVÁNY\n", false, titleFormat);
                }
                else
                {
                    title = header.InsertParagraph("QUALITY CERTIFICATE\n", false, titleFormat);

                }
                title.Alignment = Alignment.center;
            }
            #endregion

            #region Footer

            Footer footer = document.Footers.odd;
            Paragraph paragraph_footer = footer.InsertParagraph();
            paragraph_footer.Direction = Direction.LeftToRight;

            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"Marillen_lablec.jpg");

                myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                ms.Seek(0, SeekOrigin.Begin);

                Novacode.Image img = document.AddImage(ms); // Create image.
                Picture pic1 = img.CreatePicture();     // Create picture.

                paragraph_footer.AppendPicture(pic1);
                paragraph_footer.Alignment = Alignment.center;
            }
            #endregion

            for (int i = 0; i < data.Count; i++)
            {
                #region DataTable
                Table data_table;

                if (data[i].szallitolevel.Vevo == "GABONAL  Kft.                                     ")
                {
                    data_table = document.AddTable(34, 2);
                    data_table.Rows[32].Cells[0].Paragraphs[0].Append("Ethanol:").Bold();
                    data_table.Rows[33].Cells[0].Paragraphs[0].Append("Hydroxymethylfurfural:").Bold();
                    data_table.Rows[32].Cells[1].Paragraphs[0].Append("max. 0,2 %");
                    data_table.Rows[33].Cells[1].Paragraphs[0].Append("max. 5 mg/l");

                }
                else { data_table = document.AddTable(31, 2); }
                MinBizDataTable(data[i], _szállítólevél.Nyelv, i, data_table);

                MinBizDataTablazatFormazasa(data_table);
                document.InsertTable(data_table);

                Paragraph p = document.InsertParagraph();

                MinBizSzoveg(_szállítólevél.Nyelv, p, data[i], document);

                if (i != data.Count - 1)
                    document.InsertSectionPageBreak(false);
                #endregion
            }

            try { document.Save(); }
            catch (System.Exception) { MessageBox.Show("A dokumentum meg van nyitva!"); }

        }

        #region SegédFüggvények

        public static void 
        MinBizDataTable( Node_MinosegBizonylat _data, string _nyelv , int i, Table table)
        {
            int c;
            if (_nyelv == "M")
            {
                #region fixstring
                c=-1;
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Szállítólevél szám:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Vevő megnevezése:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Megnevezés:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Gyártási idő:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Sarzs:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Szín:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Íz:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Illat:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Brix:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Savtartalom (citromsavban):").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("pH:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Konzisztencia (Bostwick fok):").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott citromsav:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott cukor:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Aszkorbinsav:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott színezék:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott aroma:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott tartósítószer:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Átlagos tápérték tartalom 100 g termékben").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Energia tartalom:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Fehérje:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Szénhidrát:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Zsír:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Élelmi rost:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Mikrobiológia:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Minőségét megőrzi:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Passzírozottság:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Nettó tömeg:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Tárolás:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Csomagolás:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Származási hely:").Bold();
                #endregion

                #region data
                c=-1;
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.SzallitolevelSzam.ToString()).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Vevo).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.Megnevezes).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.GyartasiIdo.Substring(0,4) + ". évben").Bold() ;
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.Sarzs).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Szin);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Iz);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Illat);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Brix) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Citromsav) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Ph));
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Bostwick) + " cm/30 sec");
                if (_data.vizsgalatilap.CitromsavAdagolas.min == 0 && _data.vizsgalatilap.CitromsavAdagolas.max == 0){ table.Rows[++c].Cells[1].Paragraphs[0].Append("nincs"); } 
                else { table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.CitromsavAdagolas)); }
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Cukor);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + _data.vizsgalatilap.Aszkorbinsav + " mg/kg");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Szinezek);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.EnergiaTartalom1.ToString() + " kj / " + _data.tapertek.EnergiaTartalom2.ToString() + " kcal");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Feherje.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Szenhidrat.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Zsir.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Elelmirost.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.MikroBiologia);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.MinosegetMegorzi);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.Paszirozottsag + "-es szitán");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.NettoTomeg);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Tarolas);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("aszeptikus zsákban és " + _data.vizsgalatilap.Csomagolas);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.SzarmazasiHely);
                #endregion
            }
            else
            {
                #region fixstring
                c=-1;
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Customer name:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Product name:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Date of production:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Batch number:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Colour:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Taste:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Odour:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Brix:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Acid content (in citric acid):").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("pH:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Consistence (Bostwick, 20°C):").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Added citric acid:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Added sugar:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Ascorbic acid:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Added colours:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Added flavours:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Added preservatives:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Average nutritional values in 100 g product").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Energy:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Protein:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Carbohydrate:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Fat:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("     Dietary fiber:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Microbiological status:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Best before:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Sieve size:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Net weight:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Storage:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Packaging:").Bold();
                table.Rows[++c].Cells[0].Paragraphs[0].Append("Country of origin:").Bold();
                #endregion

                #region data
                c=-1;
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Vevo).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.Megnevezes).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.GyartasiIdo.Substring(0,4)).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.Sarzs).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Szin);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Iz);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szallitolevel.Illat);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Brix) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Citromsav) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Ph));
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.Bostwick) + " cm/30 sec");
                if (MinMax(_data.vizsgalatilap.CitromsavAdagolas) == null) { table.Rows[++c].Cells[1].Paragraphs[0].Append("no"); } else { table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgalatilap.CitromsavAdagolas)); }
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Cukor);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + _data.vizsgalatilap.Aszkorbinsav + " mg/kg");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Szinezek);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.EnergiaTartalom1.ToString() + " kj / " + _data.tapertek.EnergiaTartalom2.ToString() + " kcal");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Feherje.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Szenhidrat.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Zsir.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tapertek.Elelmirost.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.MikroBiologia);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.MinosegetMegorzi);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgalatilap.Paszirozottsag);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.NettoTomeg);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.Tarolas);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("aseptic bags and " + Program.database.Törzsadat_Angol( _data.vizsgalatilap.Csomagolas));
                table.Rows[++c].Cells[1].Paragraphs[0].Append(Program.database.Törzsadat_Angol(_data.vizsgalatilap.SzarmazasiHely));
                #endregion
            }
        }

        public static void 
        MinBizSzoveg(string _nyelv, Paragraph p, Node_MinosegBizonylat _data, DocX document)
        {
            Node_MinBiz_Szöveg minbizszöveg = Program.database.MinőségBizonylat_Szöveg();

            if (_nyelv == "M")
            {
                p.AppendLine(minbizszöveg.MagyarSzoveg1);
                p.AppendLine();
                p.AppendLine(minbizszöveg.MagyarSzoveg2);
                p.AppendLine();
                p.AppendLine("Kiskunfélegyháza, " + DateTime.Now.Year + ". " +  DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day + "."  );
                Paragraph q = document.InsertParagraph();
                q.Alignment = Alignment.right;
                q.Append(_data.felhasznalo.Nev + "\n" + _data.felhasznalo.Beosztas);
            }
            else
            {
                CultureInfo ci = new CultureInfo("en-US");
                var month = DateTime.Now.ToString("MMMM", ci);

                p.AppendLine(minbizszöveg.AngolSzoveg1);
                p.AppendLine();
                p.AppendLine(minbizszöveg.AngolSzoveg2);
                p.AppendLine();
                p.AppendLine("Kiskunfélegyháza, " + DateTime.Now.Day + "nd " + month + " " + DateTime.Now.Year);
                Paragraph q = document.InsertParagraph();
                q.Alignment = Alignment.right;
                q.Append(_data.felhasznalo.Nev + "\n" + _data.felhasznalo.Beosztas);

            }
        }

        public static string 
        MinMax(MinMaxPair<double?> _minmax)
        {
            string value = null;

            if (_minmax.min == null && _minmax.max == null) { return value; }
            else if (_minmax.min != null && _minmax.max != null) { value = _minmax.min.ToString() + " - " + _minmax.max.ToString() ; }
            else if (_minmax.min != null) { value = _minmax.min.ToString() ; }
            else { value = _minmax.max.ToString() ; }
            return value;
        }

        public static void 
        MinBizDataTablazatFormazasa(Table _table)
        {
            _table.AutoFit = AutoFit.Contents;

            Border c = new Border(Novacode.BorderStyle.Tcbs_none, BorderSize.seven, 0, Color.Black);
            _table.SetBorder(TableBorderType.InsideH, c);
            _table.SetBorder(TableBorderType.InsideV, c);
            _table.SetBorder(TableBorderType.Bottom, c);
            _table.SetBorder(TableBorderType.Top, c);
            _table.SetBorder(TableBorderType.Left, c);
            _table.SetBorder(TableBorderType.Right, c);
        }

        public static void 
        KonszignacioFejlecTablazatFormazas(Table _table)
        {
            _table.AutoFit = AutoFit.Contents;
            
            Border c = new Border(Novacode.BorderStyle.Tcbs_none, BorderSize.seven, 0, Color.Black);
            _table.SetBorder(TableBorderType.InsideH, c);
            _table.SetBorder(TableBorderType.InsideV, c);
            _table.SetBorder(TableBorderType.Bottom, c);
            _table.SetBorder(TableBorderType.Top, c);
            _table.SetBorder(TableBorderType.Left, c);
            _table.SetBorder(TableBorderType.Right, c);
             
        }

        public static void 
        KonszignacioDataTableFormazas(Table _table)
        {
            _table.AutoFit = AutoFit.Contents;
            Border c = new Border(Novacode.BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black);
            _table.SetBorder(TableBorderType.InsideH, c);
            _table.SetBorder(TableBorderType.InsideV, c);
            _table.SetBorder(TableBorderType.Bottom, c);
            _table.SetBorder(TableBorderType.Top, c);
            _table.SetBorder(TableBorderType.Left, c);
            _table.SetBorder(TableBorderType.Right, c);
             
            for (int i = 0; i < _table.Rows[0].Cells.Count; i++)
            {
                _table.Rows[0].Cells[i].SetBorder(TableCellBorderType.Top, new Border(Novacode.BorderStyle.Tcbs_single, BorderSize.six, 0, Color.Black));
                _table.Rows[0].Cells[i].SetBorder(TableCellBorderType.Bottom, new Border(Novacode.BorderStyle.Tcbs_single, BorderSize.six, 0, Color.Black));
            }
            for (int i = 0;
                i < _table.Rows.Count;
                i++)
            {
                if (_table.Rows[i].Cells[1].Paragraphs[0].Text.Contains("összesen"))
                {
                    for (int j = 0;
                         j < _table.Rows[i].Cells.Count;
                         j++)
                    {
                         _table.Rows[i].Cells[j].SetBorder(TableCellBorderType.Top, new Border(Novacode.BorderStyle.Tcbs_single,
                                                                                        BorderSize.six, 0, Color.Black));
                    }
                }
                else if (_table.Rows[i].Cells[1].Paragraphs[0].Text.Contains("elszállítás"))
                {
                    for (int j = 0;
                         j < _table.Rows[i].Cells.Count;
                         j++)
                    {
                        _table.Rows[i].Cells[j].SetBorder(TableCellBorderType.Top, new Border(Novacode.BorderStyle.Tcbs_single,
                                                                                               BorderSize.six, 0, Color.Black));
                        _table.Rows[i].Cells[j].SetBorder(TableCellBorderType.Bottom, new Border(Novacode.BorderStyle.Tcbs_single,
                                                                                                BorderSize.six, 0, Color.Black));
                    }
                        _table.Rows[i].Cells[0].SetBorder(TableCellBorderType.Left, new Border(Novacode.BorderStyle.Tcbs_single,
                                                                                                BorderSize.six, 0, Color.Black));
                        _table.Rows[i].Cells[6].SetBorder(TableCellBorderType.Right, new Border(Novacode.BorderStyle.Tcbs_single,
                                                                                                BorderSize.six, 0, Color.Black));
                }
            }
        }

        /// <summary>
        /// lista sorrend
        /// </summary>
        public static void 
        KonszignacioRendezes(Node_Konszignacio _eredeti)
        {
            foreach (Node_Konszignacio.GyumolcsTipus outer in _eredeti.gyumolcstipusok)
            {
                for (int i = 0; i < outer.adat.Count; i++)
                {
                    for (int j = 0; j < outer.adat.Count; j++)
                    {
                        if (Convert.ToInt32(outer.adat[i].Hordo) < Convert.ToInt32(outer.adat[j].Hordo))
                        {
                            Node_Konszignacio.GyumolcsTipus.Adat temp = new Node_Konszignacio.GyumolcsTipus.Adat();
                            temp = outer.adat[i];
                            outer.adat[i] = outer.adat[j];
                            outer.adat[j] = temp;
                        }
                    }
                }

            }
        }
        #endregion
    }
}
