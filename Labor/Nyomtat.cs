using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labor
{
    public struct Node_Konszignáció
    {
        public struct Fejléc
        {
            public struct Vevő
            {
                public string vevő_név;
                public string vevő_város;
                public string vevő_cím;

                public Vevő(string _vevő_név, string _vevő_város, string _vevő_cím)
                {
                    vevő_név = _vevő_név;
                    vevő_város = _vevő_város;
                    vevő_cím = _vevő_cím;
                }
            }

            public struct Feladó
            {
                public string feladó_név;
                public string feladó_cím;

                public Feladó(string _feladó_név, string _feladó_cím)
                {
                    feladó_név = _feladó_név;
                    feladó_cím = _feladó_cím;
                }
            }

            public struct Szállítólevél
            {
                public string[] rendszámok;
                public string dátum;
                public string szállítólevél;

                public Szállítólevél(Konszignáció_Szállítólevél _szállítólevél)
                {
                    rendszámok = new string[] { _szállítólevél.gépkocsi1, _szállítólevél.gépkocsi2 };
                    dátum = _szállítólevél.elszállítás_ideje;
                    szállítólevél = _szállítólevél.szlevél;
                }
            }

            public Feladó feladó;
            public Vevő vevő;
            public Szállítólevél szállítólevél;
        }

        public struct Gyümölcstípus
        {
            public struct Adat
            {
                public string hordó;
                public string sarzs;
                public double nettó_súly;
                public string hordó_típus;
                public string gyártás_dátum;

                public Adat( string _hordó, string _sarzs, double _nettó_súly, string _hordó_típus, string _gyártás_dátum)
                {
                    hordó = _hordó;
                    sarzs = _sarzs;
                    nettó_súly = _nettó_súly;
                    hordó_típus = _hordó_típus;
                    gyártás_dátum = _gyártás_dátum;
                }
            }

            public Gyümölcstípus(string _vtsz, string _megnevezés)
            {
                adat = new List<Adat>();
                megnevezés = _megnevezés;
                vtsz = _vtsz;
                összsúly = 0;
            }
            public List<Adat> adat;

            public string vtsz;
            public string megnevezés;
            public double összsúly;

        }

        public Fejléc fejléc;
        public List<Gyümölcstípus> gyümölcstípusok;
    }

    public struct Node_MinőségBizonylat
    {
        public struct Szállítólevél
        {
            public byte szlevél_szám;
            public string vevő;
            public string gyártási_idő;
            public string szín;
            public string íz;
            public string illat;

            public Szállítólevél(Konszignáció_Szállítólevél _szállítólevél)
            {
                szlevél_szám = _szállítólevél.szlevél_szám;
                vevő = _szállítólevél.vevő;
                gyártási_idő = _szállítólevél.gyártási_idő;
                szín = _szállítólevél.szín;
                íz = _szállítólevél.íz;
                illat = _szállítólevél.illat;
            }

        }

        public struct VizsgálatiLap
        {
            public MinMaxPair<double?> brix;
            public MinMaxPair<double?> citromsav;
            public MinMaxPair<double?> ph;
            public MinMaxPair<double?> bostwick;
            public MinMaxPair<double?> citromsavad;
            public double? aszkorbinsav;
            public string hoteko;
            public string sarzs;
            public string megnevezés;
            public string passzírozottság;
            public string csomagolás;
            public string származási_hely;

            public VizsgálatiLap(double? _minbrix, double? _maxbrix, double? _mincitromasav, double? _maxcitromsav, double? _minph, double? _maxph, double? _minbostwick, double? _maxbostwick, double? _mincitromsavad, double? _maxcitromsavad, double? _aszkorbinsav, string _hoteko, string _sarzs, string _megnevezés, string _passzírozottság, string _csomagolás, string _származási_hely)
            {
                brix = new MinMaxPair<double?>(_minbrix, _maxbrix);
                citromsav = new MinMaxPair<double?>(_mincitromasav, _maxcitromsav);
                ph = new MinMaxPair<double?>(_minph, _maxph);
                bostwick = new MinMaxPair<double?>(_minbostwick, _maxbostwick);
                citromsavad = new MinMaxPair<double?>(_mincitromasav, _maxcitromsavad);
                aszkorbinsav = _aszkorbinsav;
                hoteko = _hoteko;
                sarzs = _sarzs;
                megnevezés = _megnevezés;
                passzírozottság = _passzírozottság;
                csomagolás = _csomagolás;
                származási_hely = _származási_hely;
            }
        } //kész

        public struct Tápérték
        {
            public Int16? energia_tartalom1;
            public Int16? energia_tartalom2;
            public double? fehérje;
            public double? szénhidrát;
            public double? zsír;
            public double? élelmi_rost;

            public Tápérték(Int16? _energia_tartalom1,Int16? _energia_tartalom2, double? _fehérje, double? _szénhidrát, double? _zsír, double? _élelmi_rost)
            {
                energia_tartalom1 = _energia_tartalom1;
                energia_tartalom2 = _energia_tartalom2;
                fehérje = _fehérje;
                szénhidrát = _szénhidrát;
                zsír = _zsír;
                élelmi_rost = _élelmi_rost;
            }
        }//kész

        public class FixStringMagyar
            {
                public string hozzáadott_cukor;
                public string hozzáadott_színezék;
                public string hozzáadott_aroma;
                public string hozzáadott_tartósítószer;
                public string mikrobiológia;
                public string minőségét_megőrzi;
                public string nettó_tömeg;
                public string tárolás;
                public string Ethanol;
                public string HMF;

                public FixStringMagyar()
                {
                    hozzáadott_cukor = "nincs";
                    hozzáadott_színezék = "nincs";
                    hozzáadott_aroma = "nincs";
                    hozzáadott_tartósítószer = "nincs";
                    mikrobiológia = "technológiailag steril";
                    minőségét_megőrzi = "a hordó címkéjén feltüntetett időpontig";
                    nettó_tömeg = "a hordó címkéjén szereplő töltőtömeg";
                    tárolás = "0 - 20°C közötti hőmérsékleten, felbontás nélkül";
                    Ethanol = "0 - 20°C közötti hőmérsékleten, felbontás nélkül";
                    HMF = "max. 5 mg/l";
                }
            } 

        public class FixStringAngol
        {
            public string hozzáadott_cukor;
            public string hozzáadott_színezék;
            public string hozzáadott_aroma;
            public string hozzáadott_tartósítószer;
            public string mikrobiológia;
            public string minőségét_megőrzi;
            public string nettó_tömeg;
            public string tárolás;
            public string Ethanol;
            public string HMF;

            public FixStringAngol()
            {
                hozzáadott_cukor = "no";
                hozzáadott_színezék = "no";
                hozzáadott_aroma = "no";
                hozzáadott_tartósítószer = "no";
                mikrobiológia = "aseptic technology";
                minőségét_megőrzi = "see on label of drum";
                nettó_tömeg = "as labelled";
                tárolás = "between 0 and 20°C, without opening";
                Ethanol = "between 0 and 20°C, without opening";
                HMF = "max. 5 mg/l";
            }
        } // kész

        public struct Felhasználó
        {
            public string felhasználó_neve;
            public string felhasználó_beosztása;

            public Felhasználó(string _felhasználó_neve, string _felhasználó_beosztása)
            {
                felhasználó_neve = _felhasználó_neve;
                felhasználó_beosztása = _felhasználó_beosztása;
            }
        }

        public Szállítólevél szállítólevél;
        public VizsgálatiLap vizsgálatilap;
        public Tápérték tápérték;
        public FixStringMagyar fixstringmagyar;
        public FixStringAngol fixstringangol;
        public Felhasználó felhasználó;
    }

    public struct Node_MinBiz_Szöveg
    {
        public string sz1_m;
        public string sz1_a;
        public string sz2_m;
        public string sz2_a;

        public Node_MinBiz_Szöveg(string _sz1_m,string _sz1_a,string _sz2_m,string _sz2_a)
        {
                sz1_m = _sz1_m;
                sz1_a= _sz1_a;
                sz2_m = _sz2_m;
                sz2_a = _sz2_a;
        }
    }

    public sealed class Nyomtat
    {
        public static void Nyomtat_Konszignáció(Konszignáció_Szállítólevél _szállítólevél, List<Foglalás> _foglalások)
        {
            Node_Konszignáció konszignáció = new Node_Konszignáció();

            konszignáció.fejléc = new Node_Konszignáció.Fejléc();
            konszignáció.fejléc.feladó = new Node_Konszignáció.Fejléc.Feladó("Marillen Gyümölcsfeldolgozó Kft", "Kiskunfélegyháza, VIII. ker. 99/A");
            konszignáció.fejléc.vevő = Program.database.Konszignáció_Vevő(_szállítólevél.vevő);
            konszignáció.fejléc.szállítólevél = new Node_Konszignáció.Fejléc.Szállítólevél(_szállítólevél);
            konszignáció.gyümölcstípusok = new List<Node_Konszignáció.Gyümölcstípus>();

            int sorok_száma = 3;
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
                    Node_Konszignáció.Gyümölcstípus temp = Program.database.Konszignáció_Gyümölcstípus(item);
                    foreach (Node_Konszignáció.Gyümölcstípus gyitem in konszignáció.gyümölcstípusok)
                    {
                        if (temp.megnevezés == gyitem.megnevezés && temp.vtsz == gyitem.vtsz)
                        {
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        konszignáció.gyümölcstípusok.Add(Program.database.Konszignáció_Gyümölcstípus(item));
                    }
                }

                for (int i = 0; i < konszignáció.gyümölcstípusok.Count; i++)
                {
                    foreach (Hordó inner in hordók)
                    {
                        if (konszignáció.gyümölcstípusok[i].megnevezés == Program.database.Name(inner.termékkód))
                        {
                            Node_Konszignáció.Gyümölcstípus.Adat temp = new Node_Konszignáció.Gyümölcstípus.Adat(inner.gyártási_év[3] + inner.id, inner.sarzs, Convert.ToDouble(inner.mennyiség), "", inner.time.Substring(0, 11));
                            sorok_száma++;
                            List<Vizsgálat.Azonosító> vizsgálatok = Program.database.Vizsgálatok();
                            foreach (Vizsgálat.Azonosító item in vizsgálatok)
                            {
                                if (item.termékkód == inner.termékkód && item.sarzs == inner.sarzs)
                                {
                                    temp.hordó_típus = item.hordótípus;
                                }
                            }
                            konszignáció.gyümölcstípusok[i].adat.Add(temp);
                            Node_Konszignáció.Gyümölcstípus tempgy = konszignáció.gyümölcstípusok[i];
                            tempgy.összsúly += temp.nettó_súly;
                            konszignáció.gyümölcstípusok[i] = tempgy;
                        }
                    }
                    sorok_száma += 2;
                }
            }

            if (!Directory.Exists("Listák"))
            {
                Directory.CreateDirectory("Listák");
            }

            string filename = "Listák//" + _szállítólevél.szlevél + ".docx";
            var document = DocX.Create(filename);

            var titleFormat = new Formatting();
            titleFormat.Size = 14D;
            titleFormat.Position = 1;
            titleFormat.Spacing = 5;
            titleFormat.Bold = true;

            Paragraph title = document.InsertParagraph("Konszignáció                  \n", false, titleFormat);

            using (MemoryStream ms = new MemoryStream())
            {
                System.Drawing.Image myImg = System.Drawing.Image.FromFile(@"Marillen_logo.png");

                myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                ms.Seek(0, SeekOrigin.Begin);

                Novacode.Image img = document.AddImage(ms); // Create image.

                Picture pic1 = img.CreatePicture(100, 150);     // Create picture.

                title.InsertPicture(pic1, 20); // Insert picture into paragraph.
                title.Alignment = Alignment.right;
            }

            title.Bold();
            titleFormat.Position = 12;

            #region Fejléc
            Table table_fejléc = document.AddTable(2, 4);
            table_fejléc.Alignment = Alignment.left;
            table_fejléc.Rows[0].Cells[0].Paragraphs[0].Append("Vevő:").Bold();
            table_fejléc.Rows[1].Cells[0].Paragraphs[0].Append("Gépkocsi:").Bold();
            table_fejléc.Rows[0].Cells[2].Paragraphs[0].Append("Feladó:").Bold();
            table_fejléc.Rows[1].Cells[2].Paragraphs[0].Append("Dátum:").Bold();

            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append(konszignáció.fejléc.szállítólevél.dátum + "    ");
            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append("Szállítólevél:").Bold();
            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append(" " + konszignáció.fejléc.szállítólevél.szállítólevél);

            table_fejléc.Rows[0].Cells[1].Paragraphs[0].Append(konszignáció.fejléc.vevő.vevő_név);
            table_fejléc.Rows[0].Cells[1].Paragraphs[0].AppendLine(konszignáció.fejléc.vevő.vevő_város);
            table_fejléc.Rows[0].Cells[1].Paragraphs[0].AppendLine(konszignáció.fejléc.vevő.vevő_cím);

            table_fejléc.Rows[1].Cells[1].Paragraphs[0].Append(konszignáció.fejléc.szállítólevél.rendszámok[0]);
            table_fejléc.Rows[1].Cells[1].Paragraphs[0].Append( " " + konszignáció.fejléc.szállítólevél.rendszámok[1]);

            table_fejléc.Rows[0].Cells[3].Paragraphs[0].Append(konszignáció.fejléc.feladó.feladó_név);
            table_fejléc.Rows[0].Cells[3].Paragraphs[0].AppendLine(konszignáció.fejléc.feladó.feladó_cím);


            KonszignációsFejlécTáblázatFormázása(table_fejléc);
            document.InsertTable(table_fejléc);
            #endregion

            #region Data_Table
            Paragraph paragraph_data_table = document.InsertParagraph();

            
            Table data_table = document.AddTable(sorok_száma, 7);
            data_table.Rows[0].Cells[0].Paragraphs[0].Append("S.Sz.").Bold();
            data_table.Rows[0].Cells[1].Paragraphs[0].Append("Megnevezés").Bold();
            data_table.Rows[0].Cells[2].Paragraphs[0].Append("Hordó").Bold();
            data_table.Rows[0].Cells[3].Paragraphs[0].Append("Sarzs").Bold();
            data_table.Rows[0].Cells[4].Paragraphs[0].Append("Nettó súly").Bold();
            data_table.Rows[0].Cells[5].Paragraphs[0].Append("Hordó típus").Bold();
            data_table.Rows[0].Cells[6].Paragraphs[0].Append("Gyártás dátuma").Bold();
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);

            int c = 1;
            int sorszám = 1;
            KonszignációRendezés(konszignáció);
            foreach (Node_Konszignáció.Gyümölcstípus outer in konszignáció.gyümölcstípusok)
            {
                string temp = regex.Replace(outer.megnevezés, @" ");
                foreach (Node_Konszignáció.Gyümölcstípus.Adat inner in outer.adat)
                {

                    data_table.Rows[c].Cells[0].Paragraphs[0].Append(sorszám.ToString() + '.');
                    sorszám++;
                    data_table.Rows[c].Cells[1].Paragraphs[0].Append(temp);
                    data_table.Rows[c].Cells[2].Paragraphs[0].Append(inner.hordó.ToString());
                    data_table.Rows[c].Cells[3].Paragraphs[0].Append(inner.sarzs);
                    data_table.Rows[c].Cells[4].Paragraphs[0].Append(inner.nettó_súly + " kg");
                    data_table.Rows[c].Cells[5].Paragraphs[0].Append(inner.hordó_típus);
                    data_table.Rows[c].Cells[6].Paragraphs[0].Append(inner.gyártás_dátum);
                    c++;
                }
                data_table.Rows[c].Cells[1].Paragraphs[0].Append( temp + "összesen:").Bold();
                data_table.Rows[c].Cells[4].Paragraphs[0].Append(outer.összsúly + " kg").Bold();
                összes_súly += outer.összsúly;
                data_table.Rows[c].Cells[5].Paragraphs[0].Append("VTSZ:").Bold();
                data_table.Rows[c].Cells[6].Paragraphs[0].Append(outer.vtsz).Bold();
                c += 2;
            }
            data_table.Rows[c].Cells[1].Paragraphs[0].Append("Összes elszállítás:").Bold();
            data_table.Rows[c].Cells[4].Paragraphs[0].Append(összes_súly + " kg").Bold();

            KonszignációsDataTáblázatFormázás(data_table);
            document.InsertTable(data_table);
            #endregion

            try { document.Save(); }
            catch (System.Exception) { MessageBox.Show("A dokumentum meg van nyitva!"); }
        }

        public static void Nyomtat_MinőségBizonylatok( Konszignáció_Szállítólevél _szállítólevél, List<Foglalás> _foglalások )
        {
            List<Node_MinőségBizonylat> data = new List<Node_MinőségBizonylat>();

            #region Data
            foreach (Foglalás item in _foglalások)
            {
                Node_MinőségBizonylat temp = new Node_MinőségBizonylat();
                temp.szállítólevél =  new Node_MinőségBizonylat.Szállítólevél(_szállítólevél);
                if(_szállítólevél.nyelv == "M")
                {
                    temp.fixstringmagyar = new Node_MinőségBizonylat.FixStringMagyar(); 
                }
                else
                {
                    temp.fixstringangol = new Node_MinőségBizonylat.FixStringAngol(); 

                }
                if(_szállítólevél.nyelv == "M")
                {
                    temp.felhasználó = new Node_MinőségBizonylat.Felhasználó(Program.felhasználó.Value.felhasználó_név,Program.felhasználó.Value.beosztás1);
                }
                else
                {
                    temp.felhasználó = new Node_MinőségBizonylat.Felhasználó(Program.felhasználó.Value.felhasználó_név, Program.felhasználó.Value.beosztás2);

                }
                temp.vizsgálatilap = Program.database.MinőségBizonylat(item.id);
                temp.tápérték = Program.database.MinBiz_Tápérték(temp.vizsgálatilap.hoteko);
                data.Add(temp);
            }
            #endregion

            if (!Directory.Exists("Listák"))
            {
                Directory.CreateDirectory("Listák");
            }


            string filename = "Listák//" + "MinBiz.docx";
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
                if(_szállítólevél.nyelv == "M")
                {
                    title = header.InsertParagraph("\nMINŐSÉGI BIZONYÍTVÁNY\n", false, titleFormat);
                }
                else
                {
                    title = header.InsertParagraph("\nQUALITY CERTIFICATE\n", false, titleFormat);

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

                for (int i = 0; i < data.Count;i++ )
                {
                    #region DataTable
                    Table data_table;

                    if (data[i].szállítólevél.vevő == "GABONAL  Kft.                                     ")
                    {
                        data_table = document.AddTable(34, 2);
                        data_table.Rows[32].Cells[0].Paragraphs[0].Append("Ethanol:").Bold();
                        data_table.Rows[33].Cells[0].Paragraphs[0].Append("Hydroxymethylfurfural:").Bold();
                        data_table.Rows[32].Cells[1].Paragraphs[0].Append("max. 0,2 %");
                        data_table.Rows[33].Cells[1].Paragraphs[0].Append("max. 5 mg/l");

                    }
                    else { data_table = document.AddTable(31, 2); }

                    int c = -1;

                    if (_szállítólevél.nyelv == "M")
                    {
                        #region Magyar Sufni
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Szállítólevél szám:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Vevő megnevezése:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Megnevezés:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Gyártási idő:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Sarzs:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Szín:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Íz:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Illat:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Brix:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Savtartalom (citromsavban):").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("pH:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Konzisztencia (Bostwick fok):").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott citromsav:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott cukor:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Aszkorbinsav:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott színezék:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott aroma:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Hozzáadott tartósítószer:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Átlagos tápérték tartalom 100 g termékben").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Energia tartalom:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Fehérje:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Szénhidrát:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Zsír:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Élelmi rost:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Mikrobiológia:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Minőségét megőrzi:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Passzírozottság:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Nettó tömeg:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Tárolás:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Csomagolás:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Származási hely:").Bold();
                        #endregion

                        #region Magyar Adat
                        c = -1;
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.szlevél_szám.ToString()).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.vevő).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.megnevezés).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.gyártási_idő).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.sarzs).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.szín);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.íz);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.illat);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.brix) + " %");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.citromsav) + " %");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.ph));
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.bostwick) + " cm/30 sec");
                        if (MinMax(data[i].vizsgálatilap.citromsavad) == null) { data_table.Rows[++c].Cells[1].Paragraphs[0].Append("nincs"); } else { data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.citromsavad)); }
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.hozzáadott_cukor);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + data[i].vizsgálatilap.aszkorbinsav + " mg/kg");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.hozzáadott_színezék);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.hozzáadott_aroma);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.hozzáadott_aroma);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append("");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.energia_tartalom1.ToString() + " kj / " + data[i].tápérték.energia_tartalom2.ToString() + " kcal");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.fehérje.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.szénhidrát.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.zsír.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.élelmi_rost.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.mikrobiológia);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.minőségét_megőrzi);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.passzírozottság + " mm-es szitán");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.nettó_tömeg);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringmagyar.tárolás);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append("aszeptikus zsákban és " + data[i].vizsgálatilap.csomagolás);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.származási_hely);
                        #endregion
                    }
                    else if (_szállítólevél.nyelv == "A")
                    {
                        #region Angol Sufni
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Customer name:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Product name:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Date of production:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Batch number:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Colour:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Taste:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Odour:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Brix:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Acid content (in citric acid):").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("pH:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Consistence (Bostwick, 20°C):").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Added citric acid:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Added sugar:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Ascorbic acid:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Added colours:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Added flavours:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Added preservatives:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Average nutritional values in 100 g product").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Energy:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Protein:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Carbohydrate:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Fat:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("     Dietary fiber:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Microbiological status:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Best before:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Sieve size:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Net weight:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Storage:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Packaging:").Bold();
                        data_table.Rows[++c].Cells[0].Paragraphs[0].Append("Country of origin:").Bold();
                        #endregion

                        #region Angol Adat
                        c = -1;
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.vevő).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.megnevezés).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.gyártási_idő).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.sarzs).Bold();
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.szín);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.íz);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].szállítólevél.illat);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.brix) + " %");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.citromsav) + " %");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.ph));
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.bostwick) + " cm/30 sec");
                        if (MinMax(data[i].vizsgálatilap.citromsavad) == null) { data_table.Rows[++c].Cells[1].Paragraphs[0].Append("no"); } else { data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data[i].vizsgálatilap.citromsavad)); }
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.hozzáadott_cukor);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + data[i].vizsgálatilap.aszkorbinsav + " mg/kg");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.hozzáadott_színezék);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.hozzáadott_aroma);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.hozzáadott_aroma);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append("");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.energia_tartalom1.ToString() + " kj / " + data[i].tápérték.energia_tartalom2.ToString() + " kcal");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.fehérje.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.szénhidrát.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.zsír.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].tápérték.élelmi_rost.ToString());
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.mikrobiológia);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.minőségét_megőrzi);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.passzírozottság + " mm");
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.nettó_tömeg);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].fixstringangol.tárolás);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append("aseptic bags and " + data[i].vizsgálatilap.csomagolás);
                        data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data[i].vizsgálatilap.származási_hely);
                        #endregion
                    }

                    MinBizDataTáblázatFormázása(data_table);
                    document.InsertTable(data_table);

                    Paragraph p = document.InsertParagraph();
                    Node_MinBiz_Szöveg minbizszöveg = Program.database.MinőségBizonylat_Szöveg();

                    if (_szállítólevél.nyelv == "M")
                    {
                        p.AppendLine(minbizszöveg.sz1_m);
                        p.AppendLine();
                        p.AppendLine(minbizszöveg.sz2_m);
                        p.AppendLine();
                        p.AppendLine("Kiskunfélegyháza " + DateTime.Now.Day + ". " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year);
                        Paragraph q = document.InsertParagraph();
                        q.Alignment = Alignment.right;
                        q.AppendLine( data[0].felhasználó.felhasználó_neve + "\n" + data[0].felhasználó.felhasználó_beosztása);
                    }
                    else
                    {
                        p.AppendLine(minbizszöveg.sz1_a);
                        p.AppendLine();
                        p.AppendLine(minbizszöveg.sz2_a);
                        p.AppendLine();
                        p.AppendLine("Kiskunfélegyháza " + DateTime.Now.Day + "nd " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Year);
                        Paragraph q = document.InsertParagraph();
                        q.Alignment = Alignment.right;
                        q.AppendLine( data[0].felhasználó.felhasználó_neve + "\n" + data[0].felhasználó.felhasználó_beosztása);

                    }

                    if(i!=data.Count-1)
                    document.InsertSectionPageBreak(false);
                    #endregion
                }

            try { document.Save(); }
            catch (System.Exception) { MessageBox.Show("A dokumentum meg van nyitva!"); }

        }

        #region SegédFüggvények

        public static string MinMax(MinMaxPair<double?> minmax)
        {
            string value = null;

            if (minmax.min == null && minmax.max == null) { return value; }
            else if (minmax.min != null && minmax.max != null) { value = minmax.min.ToString() + " - " + minmax.max.ToString() ; }
            else if (minmax.min != null) { value = minmax.min.ToString() ; }
            else { value = minmax.max.ToString() ; }
            return value;
        }

        public static void MinBizDataTáblázatFormázása(Table _table)
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

        public static void KonszignációsFejlécTáblázatFormázása(Table _table)
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

        public static void KonszignációsDataTáblázatFormázás(Table _table)
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
        }

        /// <summary>
        /// lista sorrend
        /// </summary>
        public static void KonszignációRendezés(Node_Konszignáció _eredeti)
        {
            foreach (Node_Konszignáció.Gyümölcstípus outer in _eredeti.gyümölcstípusok)
            {
                for (int i = 0; i < outer.adat.Count; i++)
                {
                    for (int j = 0; j < outer.adat.Count; j++)
                    {
                        if (Convert.ToInt32(outer.adat[i].hordó) < Convert.ToInt32(outer.adat[j].hordó))
                        {
                            Node_Konszignáció.Gyümölcstípus.Adat temp = new Node_Konszignáció.Gyümölcstípus.Adat();
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
