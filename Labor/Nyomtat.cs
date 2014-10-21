using Novacode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
                public int sorszám;
                public string hordó;
                public string sarzs;
                public double nettó_súly;
                public string hordó_típus;
                public string gyártás_dátum;

                public Adat(int _sorszám, string _hordó, string _sarzs, double _nettó_súly, string _hordó_típus, string _gyártás_dátum)
                {
                    sorszám = _sorszám;
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

            public FixString()
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
        public FixString fixstring;
        public Felhasználó felhasználó;

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
            int sorszám = 0;
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

                foreach (string item in hordó_termékkódok) { konszignáció.gyümölcstípusok.Add(Program.database.Konszignáció_Gyümölcstípus(item)); }

                for (int i = 0; i < konszignáció.gyümölcstípusok.Count; i++)
                {
                    foreach (Hordó inner in hordók)
                    {
                        if (konszignáció.gyümölcstípusok[i].megnevezés == Program.database.Name(inner.termékkód))
                        {
                            Node_Konszignáció.Gyümölcstípus.Adat temp = new Node_Konszignáció.Gyümölcstípus.Adat(++sorszám,inner.gyártási_év[3] + inner.id, inner.sarzs, Convert.ToDouble(inner.mennyiség), "", inner.time.Substring(0,11));
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
                pic1.SetPictureShape(BasicShapes.cube); // Set picture shape (if needed)

                title.InsertPicture(pic1, 20); // Insert picture into paragraph.
                title.Alignment = Alignment.right;
            }

            title.Bold();
            titleFormat.Position = 12;

            #region Fejléc
            Table table_fejléc = document.AddTable(3, 4);
            table_fejléc.Alignment = Alignment.left;
            table_fejléc.Rows[0].Cells[0].Paragraphs[0].Append("Vevő:").Bold();
            table_fejléc.Rows[1].Cells[0].Paragraphs[0].Append("Gépkocsi:").Bold();
            table_fejléc.Rows[0].Cells[2].Paragraphs[0].Append("Feladó:").Bold();
            table_fejléc.Rows[1].Cells[2].Paragraphs[0].Append("Dátum:").Bold();
            table_fejléc.Rows[2].Cells[2].Paragraphs[0].Append("Szállítólevél:").Bold();

            table_fejléc.Rows[0].Cells[1].Paragraphs[0].Append(konszignáció.fejléc.vevő.vevő_név);
            table_fejléc.Rows[0].Cells[1].Paragraphs[0].AppendLine(konszignáció.fejléc.vevő.vevő_név);
            table_fejléc.Rows[0].Cells[1].Paragraphs[0].AppendLine(konszignáció.fejléc.vevő.vevő_cím);

            table_fejléc.Rows[1].Cells[1].Paragraphs[0].Append(konszignáció.fejléc.szállítólevél.rendszámok[0]);
            table_fejléc.Rows[1].Cells[1].Paragraphs[0].AppendLine(konszignáció.fejléc.szállítólevél.rendszámok[1]);

            table_fejléc.Rows[0].Cells[3].Paragraphs[0].Append(konszignáció.fejléc.feladó.feladó_név);
            table_fejléc.Rows[0].Cells[3].Paragraphs[0].AppendLine(konszignáció.fejléc.feladó.feladó_cím);

            table_fejléc.Rows[1].Cells[3].Paragraphs[0].Append(konszignáció.fejléc.szállítólevél.dátum);
            table_fejléc.Rows[2].Cells[3].Paragraphs[0].Append(konszignáció.fejléc.szállítólevél.szállítólevél);

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



            int c = 1;
            foreach (Node_Konszignáció.Gyümölcstípus outer in konszignáció.gyümölcstípusok)
            {
                foreach (Node_Konszignáció.Gyümölcstípus.Adat inner in outer.adat)
                {
                    data_table.Rows[c].Cells[0].Paragraphs[0].Append(inner.sorszám.ToString() + '.');
                    data_table.Rows[c].Cells[1].Paragraphs[0].Append(outer.megnevezés);
                    data_table.Rows[c].Cells[2].Paragraphs[0].Append(inner.hordó.ToString());
                    data_table.Rows[c].Cells[3].Paragraphs[0].Append(inner.sarzs);
                    data_table.Rows[c].Cells[4].Paragraphs[0].Append(inner.nettó_súly + " kg");
                    data_table.Rows[c].Cells[5].Paragraphs[0].Append(inner.hordó_típus);
                    data_table.Rows[c].Cells[6].Paragraphs[0].Append(inner.gyártás_dátum);
                    c++;
                }
                data_table.Rows[c].Cells[1].Paragraphs[0].Append(outer.megnevezés + " összesen:").Bold();
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

        public static void Nyomtat_MinőségBizonylat( Konszignáció_Szállítólevél _szállítólevél, int foglalás_id )
        {
            Node_MinőségBizonylat data = new Node_MinőségBizonylat();

            #region Data
            
            data.szállítólevél = new Node_MinőségBizonylat.Szállítólevél(_szállítólevél);
            data.fixstring = new Node_MinőségBizonylat.FixString();
            data.felhasználó = new Node_MinőségBizonylat.Felhasználó("név", "beosztás");
            data.vizsgálatilap = Program.database.MinőségBizonylat(foglalás_id);
            data.tápérték = Program.database.MinBiz_Tápérték(data.vizsgálatilap.hoteko);
            #endregion

            if (!Directory.Exists("Listák"))
            {
                Directory.CreateDirectory("Listák");
            }


            string filename = "Listák//" + "MinBiz.docx";
            var document = DocX.Create(filename);
            document.AddHeaders();
            document.AddFooters();

            var titleFormat = new Formatting();
            titleFormat.Size = 18D;
            titleFormat.Position = 1;
            titleFormat.Spacing = 5;
            titleFormat.Bold = true;

            Paragraph title = document.InsertParagraph("\nMINŐSÉGI BIZONYÍTVÁNY\n", false, titleFormat);
            title.Alignment = Alignment.center;

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

            #region DataTable
            Table data_table;

            if (data.szállítólevél.vevő == "GABONAL  Kft.                                     ") 
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
                #region Sufni
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
            }
            else if (_szállítólevél.nyelv == "A")
            {
                #region Sufni
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
            }

            c = -1;
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.szállítólevél.szlevél_szám.ToString()).Bold();
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.szállítólevél.vevő).Bold();
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.vizsgálatilap.megnevezés).Bold();
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.szállítólevél.gyártási_idő).Bold();
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.vizsgálatilap.sarzs).Bold();
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.szállítólevél.szín);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.szállítólevél.íz);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.szállítólevél.illat);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data.vizsgálatilap.brix) + " %");
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data.vizsgálatilap.citromsav) + " %");
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data.vizsgálatilap.ph));
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data.vizsgálatilap.bostwick) + " cm/30 sec");
            if (MinMax(data.vizsgálatilap.citromsavad) == null) { data_table.Rows[++c].Cells[1].Paragraphs[0].Append("nincs"); } else { data_table.Rows[++c].Cells[1].Paragraphs[0].Append(MinMax(data.vizsgálatilap.citromsavad)); }
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.hozzáadott_cukor);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append("maximum " + data.vizsgálatilap.aszkorbinsav + " mg/kg");
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.hozzáadott_színezék);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.hozzáadott_aroma);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.hozzáadott_aroma);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append("");
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.tápérték.energia_tartalom1.ToString() + " kj / " + data.tápérték.energia_tartalom2.ToString() + " kcal" );
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.tápérték.fehérje.ToString());
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.tápérték.szénhidrát.ToString());
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.tápérték.zsír.ToString());
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.tápérték.élelmi_rost.ToString());
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.mikrobiológia);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.minőségét_megőrzi);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.vizsgálatilap.passzírozottság + " mm-es szitán");
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.nettó_tömeg);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.fixstring.tárolás);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append("aszeptikus zsákban és " + data.vizsgálatilap.csomagolás);
            data_table.Rows[++c].Cells[1].Paragraphs[0].Append(data.vizsgálatilap.származási_hely);
            MinBizDataTáblázatFormázása(data_table);
            document.InsertTable(data_table);
            #endregion

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
            for (int i = 0; i < _table.Rows.Count; i++)
                _table.Rows[i].Cells[6].Width = 160;

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


        #endregion
    }
}
