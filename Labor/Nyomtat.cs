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

            public struct Szlevél
            {
                public string[] rendszámok;
                public string dátum;
                public string szállítólevél;

                public Szlevél(Szállítólevél _szlevél)
                {
                    rendszámok = new string[] { _szlevél.gépkocsi1, _szlevél.gépkocsi2 };
                    dátum = _szlevél.elszállítás_ideje;
                    szállítólevél = _szlevél.szlevél;
                }
            }

            public Feladó feladó;
            public Vevő vevő;
            public Szlevél szállítólevél;
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

                public Adat(int _sorszám, string _megnevezés, int _hordó, string _sarzs, double? _nettó_súly, string _hordó_típus, string _gyártás_dátum)
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
                vtsz = _vtsz;
                megnevezés = _megnevezés;
                sum_nettó_súly = 0;

            }
            public List<Adat> adat;
            public int sum_nettó_súly;
            public string vtsz;
            public string megnevezés;

        }

        public Fejléc fejléc;
        public List<Gyümölcstípus> gyümölcstípus;
    }

    public sealed class Nyomtat
    {
        public static void Nyomtat_Konszignáció( Szállítólevél _szlevél, int _foglalás_id)
        {
            
        }

    }
}
