using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                public int hordó;
                public string sarzs;
                public double? nettó_súly;
                public string hordó_típus;
                public string gyártás_dátum;

                public Adat(int _sorszám, int _hordó, string _sarzs, double? _nettó_súly, string _hordó_típus, string _gyártás_dátum)
                {
                    sorszám = _sorszám;
                    hordó = _hordó;
                    sarzs = _sarzs;
                    nettó_súly = _nettó_súly;
                    hordó_típus = _hordó_típus;
                    gyártás_dátum = _gyártás_dátum;
                }
            }

            public Gyümölcstípus( string _vtsz, string _megnevezés)
            {
                adat = new List<Adat>();
                megnevezés = _megnevezés;
                vtsz = _vtsz;
            }
            public List<Adat> adat;

            public string vtsz;
            public string megnevezés;

        }

        public Fejléc fejléc;
        public List<Gyümölcstípus> gyümölcstípusok;
    }

    public sealed class Nyomtat
    {
        public static void Nyomtat_Konszignáció(Konszignáció_Szállítólevél _szállítólevél, int _foglalás_id)
        {
            Node_Konszignáció konszignáció = new Node_Konszignáció();
            int sorszám = 0;

            #region Termékkódok lekérdezése
            List<Hordó> hordók = Program.database.Konszignáció_Hordók(_foglalás_id);

            List<string> hordó_termékkódok = new List<string>();
            hordó_termékkódok.Add(hordók[0].termékkód);
            for (int i = 0; i < hordók.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < hordó_termékkódok.Count; j++)
                {
                    if (hordók[i].termékkód == hordó_termékkódok[j]) { found = true; break; }
                }
                if (!found) hordó_termékkódok.Add(hordók[i].termékkód);
            }
            #endregion

            konszignáció.fejléc = new Node_Konszignáció.Fejléc();
            konszignáció.fejléc.feladó = new Node_Konszignáció.Fejléc.Feladó("Marillen Gyümölcsfeldolgozó Kft", "Kiskunfélegyháza, VIII. ker. 99/A");
            konszignáció.fejléc.vevő = Program.database.Konszignáció_Vevő(_szállítólevél.vevő);
            konszignáció.fejléc.szállítólevél = new Node_Konszignáció.Fejléc.Szállítólevél(_szállítólevél);

            konszignáció.gyümölcstípusok = new List<Node_Konszignáció.Gyümölcstípus>();
            foreach (string item in hordó_termékkódok) { konszignáció.gyümölcstípusok.Add(Program.database.Konszignáció_Gyümölcstípus(item)); }

            List<Node_Konszignáció.Gyümölcstípus.Adat> gyümölcstípus_adat = new List<Node_Konszignáció.Gyümölcstípus.Adat>();
            foreach (Node_Konszignáció.Gyümölcstípus innner in konszignáció.gyümölcstípusok)
            {
                foreach (Hordó inner in hordók)
                {
                    if (innner.megnevezés == Program.database.Name(inner.termékkód)) { innner.adat.Add(Program.database.Konszignáció_Gyümölcstípus_Adatok(inner, sorszám++)); }
                }
            }

            /*kiiratas
            foreach (Node_Konszignáció.Gyümölcstípus outer in konszignáció.gyümölcstípusok)
            {
                Console.WriteLine(outer.megnevezés + "  " + outer.vtsz);

                foreach (Node_Konszignáció.Gyümölcstípus.Adat inner in outer.adat)
                {
                    Console.WriteLine("    " + inner.sorszám + " " + outer.megnevezés + " " + inner.hordó + " " + inner.sarzs + " " + inner.nettó_súly + " " + inner.hordó_típus + " " + inner.gyártás_dátum);
                }
            }
             */
        }
    }
}
