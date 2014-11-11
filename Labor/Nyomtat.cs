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
            public Int16 szlevél_szám;
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

        public class FixString
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

            public FixString(string _nyelv)
            {

                if (_nyelv == "M")
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
                else if (_nyelv == "A")
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
            }
        } 

        public struct Felhasználó
        {
            public string felhasználó_neve;
            public string felhasználó_beosztása;
            public Felhasználó(string _nyelv)
            {
                if(_nyelv == "M")
                {
                    felhasználó_neve =  Program.felhasználó.Value.név1;
                    felhasználó_beosztása= Program.felhasználó.Value.beosztás1;
                }
                else
                {
                   felhasználó_neve= Program.felhasználó.Value.név2;
                   felhasználó_beosztása = Program.felhasználó.Value.beosztás2;
                }
            }
        }

        public Szállítólevél szállítólevél;
        public VizsgálatiLap vizsgálatilap;
        public Tápérték tápérték;
        public FixString fixstring;
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
        static Regex regex;

        public static void Nyomtat_Konszignáció(Konszignáció_Szállítólevél _szállítólevél, List<Foglalás> _foglalások)
        {
            regex = new Regex(@"[ ]{2,}", RegexOptions.None);

            Node_Konszignáció konszignáció = new Node_Konszignáció();

            konszignáció.fejléc = new Node_Konszignáció.Fejléc();
            konszignáció.fejléc.feladó = new Node_Konszignáció.Fejléc.Feladó("Marillen Gyümölcsfeldolgozó Kft", "Kiskunfélegyháza, VIII. ker. 99/A");
            konszignáció.fejléc.vevő = Program.database.Konszignáció_Vevő(_szállítólevél.vevő);
            konszignáció.fejléc.szállítólevél = new Node_Konszignáció.Fejléc.Szállítólevél(_szállítólevél);
            konszignáció.gyümölcstípusok = new List<Node_Konszignáció.Gyümölcstípus>();

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
            KonszignációRendezés(konszignáció);
            foreach (Node_Konszignáció.Gyümölcstípus outer in konszignáció.gyümölcstípusok)
            {
                string temp = regex.Replace(outer.megnevezés, @" ");
                foreach (Node_Konszignáció.Gyümölcstípus.Adat inner in outer.adat)
                {
                    data_table.InsertRow();

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

                data_table.InsertRow();
                data_table.InsertRow();
                data_table.Rows[c].Cells[1].Paragraphs[0].Append( temp + "összesen:").Bold();
                data_table.Rows[c].Cells[4].Paragraphs[0].Append(outer.összsúly + " kg").Bold();
                összes_súly += outer.összsúly;
                data_table.Rows[c].Cells[5].Paragraphs[0].Append("VTSZ:").Bold();
                data_table.Rows[c].Cells[6].Paragraphs[0].Append(outer.vtsz).Bold();
                c += 2;
            }
            data_table.InsertRow();
            data_table.Rows[c].Cells[1].Paragraphs[0].Append("Összes elszállítás:").Bold();
            data_table.Rows[c].Cells[4].Paragraphs[0].Append(összes_súly + " kg").Bold();

            KonszignációsDataTáblázatFormázás(data_table);
            document.InsertTable(data_table);
            #endregion

            try { document.Save(); }
            catch (System.Exception) { MessageBox.Show("A dokumentum meg van nyitva!"); }
        }

        public static void Nyomtat_MinőségBizonylatok(Konszignáció_Szállítólevél _szállítólevél, List<Foglalás> _foglalások)
        {
            regex = new Regex(@"[ ]{2,}", RegexOptions.None);

            List<Node_MinőségBizonylat> data = new List<Node_MinőségBizonylat>();

            #region Data
            Node_MinőségBizonylat temp = new Node_MinőségBizonylat();
            temp.fixstring = new Node_MinőségBizonylat.FixString(_szállítólevél.nyelv);
            temp.felhasználó = new Node_MinőségBizonylat.Felhasználó(_szállítólevél.nyelv);
            temp.szállítólevél = new Node_MinőségBizonylat.Szállítólevél(_szállítólevél);

            List<string> hotekok = Program.database.MinőségBizonylatHotekok(_foglalások);
            foreach (string item in hotekok)
            {
                temp.vizsgálatilap = Program.database.MinőségBizonylat(_foglalások, item);
                temp.tápérték = Program.database.MinBiz_Tápérték(temp.vizsgálatilap.hoteko);
                data.Add(temp);
            }

            #endregion

            if (!Directory.Exists("Listák"))
            {
                Directory.CreateDirectory("Listák");
            }

            string filename = "Listák//" + _szállítólevél.szlevél + "-MinBiz.docx";
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
                if (_szállítólevél.nyelv == "M")
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

                if (data[i].szállítólevél.vevő == "GABONAL  Kft.                                     ")
                {
                    data_table = document.AddTable(34, 2);
                    data_table.Rows[32].Cells[0].Paragraphs[0].Append("Ethanol:").Bold();
                    data_table.Rows[33].Cells[0].Paragraphs[0].Append("Hydroxymethylfurfural:").Bold();
                    data_table.Rows[32].Cells[1].Paragraphs[0].Append("max. 0,2 %");
                    data_table.Rows[33].Cells[1].Paragraphs[0].Append("max. 5 mg/l");

                }
                else { data_table = document.AddTable(31, 2); }
                MinBizDataTable(data[i], _szállítólevél.nyelv, i, data_table);

                MinBizDataTáblázatFormázása(data_table);
                document.InsertTable(data_table);

                Paragraph p = document.InsertParagraph();

                MinBizSzöveg(_szállítólevél.nyelv, p, data[i], document);

                if (i != data.Count - 1)
                    document.InsertSectionPageBreak(false);
                #endregion
            }

            try { document.Save(); }
            catch (System.Exception) { MessageBox.Show("A dokumentum meg van nyitva!"); }

        }

        #region SegédFüggvények
        public static void MinBizDataTable( Node_MinőségBizonylat _data, string _nyelv , int i, Table table)
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
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.szlevél_szám.ToString()).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.vevő).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.megnevezés).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.gyártási_idő).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.sarzs).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.szín);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.íz);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.illat);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.brix) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.citromsav) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.ph));
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.bostwick) + " cm/30 sec");
                if (_data.vizsgálatilap.citromsavad.min == 0 && _data.vizsgálatilap.citromsavad.max == 0){ table.Rows[++c].Cells[1].Paragraphs[0].Append("nincs"); } 
                else { table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.citromsavad)); }
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_cukor);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + _data.vizsgálatilap.aszkorbinsav + " mg/kg");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_színezék);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.energia_tartalom1.ToString() + " kj / " + _data.tápérték.energia_tartalom2.ToString() + " kcal");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.fehérje.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.szénhidrát.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.zsír.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.élelmi_rost.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.mikrobiológia);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.minőségét_megőrzi);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.passzírozottság + " -es szitán");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.nettó_tömeg);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.tárolás);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("aszeptikus zsákban és " + _data.vizsgálatilap.csomagolás);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.származási_hely);
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
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.vevő).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.megnevezés).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.gyártási_idő).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.sarzs).Bold();
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.szín);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.íz);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.szállítólevél.illat);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.brix) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.citromsav) + " %");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.ph));
                table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.bostwick) + " cm/30 sec");
                if (MinMax(_data.vizsgálatilap.citromsavad) == null) { table.Rows[++c].Cells[1].Paragraphs[0].Append("no"); } else { table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(_data.vizsgálatilap.citromsavad)); }
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_cukor);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + _data.vizsgálatilap.aszkorbinsav + " mg/kg");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_színezék);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.hozzáadott_aroma);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.energia_tartalom1.ToString() + " kj / " + _data.tápérték.energia_tartalom2.ToString() + " kcal");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.fehérje.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.szénhidrát.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.zsír.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.tápérték.élelmi_rost.ToString() + " g");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.mikrobiológia);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.minőségét_megőrzi);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.vizsgálatilap.passzírozottság + " mm");
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.nettó_tömeg);
                table.Rows[++c].Cells[1].Paragraphs[0].Append(_data.fixstring.tárolás);
                table.Rows[++c].Cells[1].Paragraphs[0].Append("aseptic bags and " + Program.database.Törzsadat_Angol( _data.vizsgálatilap.csomagolás));
                table.Rows[++c].Cells[1].Paragraphs[0].Append(Program.database.Törzsadat_Angol(_data.vizsgálatilap.származási_hely));
                #endregion
            }
        }

        public static void MinBizSzöveg(string _nyelv, Paragraph p, Node_MinőségBizonylat _data, DocX document )
        {
            Node_MinBiz_Szöveg minbizszöveg = Program.database.MinőségBizonylat_Szöveg();

            if (_nyelv == "M")
            {
                p.AppendLine(minbizszöveg.sz1_m);
                p.AppendLine();
                p.AppendLine(minbizszöveg.sz2_m);
                p.AppendLine();
                p.AppendLine("Kiskunfélegyháza, " + DateTime.Now.Year + ". " +  DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day + "."  );
                Paragraph q = document.InsertParagraph();
                q.Alignment = Alignment.right;
                q.Append(_data.felhasználó.felhasználó_neve + "\n" + _data.felhasználó.felhasználó_beosztása);
            }
            else
            {
                CultureInfo ci = new CultureInfo("en-US");
                var month = DateTime.Now.ToString("MMMM", ci);

                p.AppendLine(minbizszöveg.sz1_a);
                p.AppendLine();
                p.AppendLine(minbizszöveg.sz2_a);
                p.AppendLine();
                p.AppendLine("Kiskunfélegyháza, " + DateTime.Now.Day + "nd " + month + " " + DateTime.Now.Year);
                Paragraph q = document.InsertParagraph();
                q.Alignment = Alignment.right;
                q.Append(_data.felhasználó.felhasználó_neve + "\n" + _data.felhasználó.felhasználó_beosztása);

            }
        }

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
