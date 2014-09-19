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
                public Feladó( string _feladó_név, string _feladó_cím )
                {
                    feladó_név = _feladó_név;
                    feladó_cím = _feladó_cím;
                }
            }

            public Feladó feladó;
            public Vevő vevő;
            public List<string> rendszámok;
            public string dátum;
            public string szállítólevél;
        }

        public Fejléc fejléc;
    }




    public sealed class Nyomtat
    {
        public static void Nyomtat_Konszignáció( string _vevő_név )
        {
            Node_Konszignáció data = new Node_Konszignáció();
            data.fejléc = new Node_Konszignáció.Fejléc();
            data.fejléc.vevő = Program.database.Konszignáció_Vevő(_vevő_név);
        }

    }
}
