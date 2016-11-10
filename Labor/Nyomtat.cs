using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Novacode;
using BorderStyle = Novacode.BorderStyle;
using Image = System.Drawing.Image;

namespace Labor
{
    public struct KONSZIGNACIO
    {
        public struct FEJLEC
        {
            public struct VEVO
            {
                public string Nev;
                public string Varos;
                public string Cim;
                public string HazSzam;

                public VEVO( string _Nev,
                             string _Varos,
                             string _Cim,
                             string _HazSzam )
                {
                    Nev = _Nev;
                    Varos = _Varos;
                    Cim = _Cim;
                    HazSzam = _HazSzam;
                }
            }

            public struct FELADO
            {
                public string Nev;
                public string Cim;

                public FELADO( string _Nev, string _Cim )
                {
                    Nev = _Nev;
                    Cim = _Cim;
                }
            }

            public struct SZALLITOLEVEL
            {
                public string[ ] Rendszamok;
                public string Datum;
                public string Szoveg;

                public SZALLITOLEVEL( KONSZIGNACIOSZALLITOLEVEL _szallitolevel )
                {
                    Rendszamok = new[ ] { _szallitolevel.Rendszam1, _szallitolevel.Rendszam2 };
                    Datum = _szallitolevel.ElszallitasIdeje;
                    Szoveg = _szallitolevel.Szallitolevel;
                }
            }

            public FELADO felado;
            public VEVO vevo;
            public SZALLITOLEVEL szallitolevel;
        }

        public struct GYUMOLCSTIPUS
        {
            public struct ADAT
            {
                public string Hordo;
                public string Sarzs;
                public double NettoSuly;
                public string HordoTipus;
                public string GyartasDatum;

                public ADAT( string _Hordo, string _Sarzs, double _NettoSuly, string _HordoTipus, string _GyartasDatum )
                {
                    Hordo = _Hordo;
                    Sarzs = _Sarzs;
                    NettoSuly = _NettoSuly;
                    HordoTipus = _HordoTipus;
                    GyartasDatum = _GyartasDatum;
                }
            }

            public GYUMOLCSTIPUS( string _vtsz, string _megnevezes )
            {
                adat = new List<ADAT>( );
                Megnevezes = _megnevezes;
                VTSZ = _vtsz;
                OsszSuly = 0;
            }
            public List<ADAT> adat;

            public string VTSZ;
            public string Megnevezes;
            public double OsszSuly;

        }

        public FEJLEC fejlec;
        public List<GYUMOLCSTIPUS> gyumolcstipusok;
    }

    public struct MINOSEGBIZONYLAT
    {
        public struct SZALLITOLEVEL
        {
            public Int16 SzallitolevelSzam;
            public string Vevo;
            public string GyartasiIdo;
            public string Szin;
            public string Iz;
            public string Illat;

            public SZALLITOLEVEL( KONSZIGNACIOSZALLITOLEVEL _szallitolevel )
            {
                SzallitolevelSzam = _szallitolevel.SzallitolevelSzam;
                Vevo = _szallitolevel.Vevo;
                GyartasiIdo = _szallitolevel.GyartasiIdo;
                Szin = _szallitolevel.Szin;
                Iz = _szallitolevel.Iz;
                Illat = _szallitolevel.Illat;
            }
        }

        public struct VIZSGALATILAP
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


            public VIZSGALATILAP( MinMaxPair<double?> _Brix,
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
                                 string _SzarmazasiHely )
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
        }

        public struct TAPERTEK
        {
            public Int16? EnergiaTartalom1;
            public Int16? EnergiaTartalom2;
            public double? Feherje;
            public double? Szenhidrat;
            public double? Zsir;
            public double? Elelmirost;

            public TAPERTEK( Int16? _EnergiaTartalom1,
                            Int16? _EnergiaTartalom2,
                            double? _Feherje,
                            double? _Szenhidrat,
                            double? _Zsir,
                            double? _Elelmirost )
            {
                EnergiaTartalom1 = _EnergiaTartalom1;
                EnergiaTartalom2 = _EnergiaTartalom2;
                Feherje = _Feherje;
                Szenhidrat = _Szenhidrat;
                Zsir = _Zsir;
                Elelmirost = _Elelmirost;
            }
        }

        public class FIXSTRING
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

            public FIXSTRING( string _nyelv )
            {

                if ( _nyelv == "M" )
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
                else if ( _nyelv == "A" )
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

        public struct FELHASZNALO
        {
            public string Nev;
            public string Beosztas;

            public FELHASZNALO( string _nyelv )
            {
                if ( _nyelv == "M" )
                {
                    Nev = Program.felhasználó.Value.Nev1;
                    Beosztas = Program.felhasználó.Value.Beosztas1;
                }
                else
                {
                    Nev = Program.felhasználó.Value.Nev2;
                    Beosztas = Program.felhasználó.Value.Beosztas2;
                }
            }
        }

        public SZALLITOLEVEL szallitolevel;
        public VIZSGALATILAP vizsgalatilap;
        public TAPERTEK tapertek;
        public FIXSTRING fixstring;
        public FELHASZNALO felhasznalo;
    }

    public struct MINOSEGBIZONYLAT_SZOVEG
    {
        public string MagyarSzoveg1;
        public string AngolSzoveg1;
        public string MagyarSzoveg2;
        public string AngolSzoveg2;

        public MINOSEGBIZONYLAT_SZOVEG( string _MagyarSzoveg1,
                                  string _AngolSzoveg1,
                                  string _MagyarSzoveg2,
                                  string _AngolSzoveg2 )
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
        Konszignacio( KONSZIGNACIOSZALLITOLEVEL _szállítólevél, List<FOGLALAS> _foglalások )
        {
            regex = new Regex( @"[ ]{2,}", RegexOptions.None );

            KONSZIGNACIO Konszignacio = new KONSZIGNACIO( );

            Konszignacio.fejlec = new KONSZIGNACIO.FEJLEC( );
            Konszignacio.fejlec.felado = new KONSZIGNACIO.FEJLEC.FELADO( "Marillen Gyümölcsfeldolgozó Kft",
                                                                         "Kiskunfélegyháza, VIII. kerület 99/A" );
            Konszignacio.fejlec.vevo = Program.database.Konszignáció_Vevő( _szállítólevél.Vevo );
            Konszignacio.fejlec.szallitolevel = new KONSZIGNACIO.FEJLEC.SZALLITOLEVEL( _szállítólevél );
            Konszignacio.gyumolcstipusok = new List<KONSZIGNACIO.GYUMOLCSTIPUS>( );

            double összes_súly = 0;

            foreach ( FOGLALAS foglalás_iterator in _foglalások )
            {
                összes_súly = 0;

                List<HORDO> hordók = Program.database.Konszignáció_Hordók( foglalás_iterator.ID );
                List<string> hordó_termékkódok = new List<string>( );

                for ( int i = 0 ; i < hordók.Count ; i++ )
                {
                    bool found = false;
                    for ( int j = 0 ; j < hordó_termékkódok.Count ; j++ )
                    {
                        if ( hordók[ i ].Termekkod == hordó_termékkódok[ j ] ) { found = true; break; }

                    }
                    if ( !found ) hordó_termékkódok.Add( hordók[ i ].Termekkod );
                }

                foreach ( string item in hordó_termékkódok )
                {
                    bool found = false;
                    KONSZIGNACIO.GYUMOLCSTIPUS temp = Program.database.Konszignáció_Gyümölcstípus( item );
                    foreach ( KONSZIGNACIO.GYUMOLCSTIPUS gyitem in Konszignacio.gyumolcstipusok )
                    {
                        if ( temp.Megnevezes == gyitem.Megnevezes && temp.VTSZ == gyitem.VTSZ )
                        {
                            found = true;
                        }
                    }
                    if ( !found )
                    {
                        Konszignacio.gyumolcstipusok.Add( Program.database.Konszignáció_Gyümölcstípus( item ) );
                    }
                }

                for ( int i = 0 ; i < Konszignacio.gyumolcstipusok.Count ; i++ )
                {
                    foreach ( HORDO inner in hordók )
                    {
                        if ( Konszignacio.gyumolcstipusok[ i ].Megnevezes == Program.database.Name( inner.Termekkod ) )
                        {
                            KONSZIGNACIO.GYUMOLCSTIPUS.ADAT temp;
                            if (inner.GyartasiEv.Length == 1)
                            {
                                temp = new KONSZIGNACIO.GYUMOLCSTIPUS.ADAT(inner.GyartasiEv + inner.ID, inner.Sarzs, Convert.ToDouble(inner.Mennyiseg), "", inner.Time.Substring(0, 11));
                            }
                            else
                            {
                                temp = new KONSZIGNACIO.GYUMOLCSTIPUS.ADAT(inner.GyartasiEv[3] + inner.ID, inner.Sarzs, Convert.ToDouble(inner.Mennyiseg), "", inner.Time.Substring(0, 11));
                            }

                            List<Vizsgálat.Azonosító> vizsgálatok = Program.database.Vizsgálatok( );
                            foreach ( Vizsgálat.Azonosító item in vizsgálatok )
                            {
                                if ( item.termékkód == inner.Termekkod && item.sarzs == inner.Sarzs )
                                {
                                    temp.HordoTipus = item.hordótípus;
                                }
                            }
                            Konszignacio.gyumolcstipusok[ i ].adat.Add( temp );
                            KONSZIGNACIO.GYUMOLCSTIPUS tempgy = Konszignacio.gyumolcstipusok[ i ];
                            tempgy.OsszSuly += temp.NettoSuly;
                            Konszignacio.gyumolcstipusok[ i ] = tempgy;
                        }
                    }
                }
            }

            string filename = ( Settings.save_directory == null ) ? "Listák//" + _szállítólevél.Szallitolevel + ".docx" : Settings.save_directory + "//" + _szállítólevél.Szallitolevel + ".docx";

            if ( _szállítólevél.Szallitolevel.Contains( '/' ) )
            {
                MessageBox.Show( "Nem megengedett karakter a szállítólevél mezőben", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                return;
            }

            var document = DocX.Create( filename );
            document.DifferentFirstPage = true;
            document.AddHeaders( );

            document.MarginLeft = 40;       //BN
            document.MarginRight = 40;      //BN
            document.MarginTop = 40;        //BN

            var titleFormat = new Formatting
            {
                Size = 14D,
                Position = 1,
                Spacing = 5,
                Bold = true
            };

            Header FirstPageHeader = document.Headers.first;
            Paragraph HeaderParagraph = FirstPageHeader.InsertParagraph( "Konszignáció\n", false, titleFormat ); //BN

            HeaderParagraph.Alignment = Alignment.center;
            HeaderParagraph.Bold( );
            titleFormat.Position = 12;

            #region Fejléc
            Table table_fejléc = document.AddTable( 2, 4 );
            table_fejléc.Alignment = Alignment.left;
            table_fejléc.Rows[ 0 ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Vevő:" ).Bold( );
            table_fejléc.Rows[ 1 ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Gépkocsi:" ).Bold( );
            table_fejléc.Rows[ 0 ].Cells[ 2 ].Paragraphs[ 0 ].Append( "Feladó:" ).Bold( );
            table_fejléc.Rows[ 1 ].Cells[ 2 ].Paragraphs[ 0 ].Append( "Dátum:" ).Bold( );

            table_fejléc.Rows[ 1 ].Cells[ 3 ].Paragraphs[ 0 ].Append( Konszignacio.fejlec.szallitolevel.Datum + "    " );
            table_fejléc.Rows[ 1 ].Cells[ 3 ].Paragraphs[ 0 ].Append( "Szállítólevél:" ).Bold( );
            table_fejléc.Rows[ 1 ].Cells[ 3 ].Paragraphs[ 0 ].Append( " " + Konszignacio.fejlec.szallitolevel.Szoveg );

            table_fejléc.Rows[ 0 ].Cells[ 1 ].Paragraphs[ 0 ].Append( Konszignacio.fejlec.vevo.Nev );
            table_fejléc.Rows[ 0 ].Cells[ 1 ].Paragraphs[ 0 ].AppendLine( Konszignacio.fejlec.vevo.Varos );
            table_fejléc.Rows[ 0 ].Cells[ 1 ].Paragraphs[ 0 ].AppendLine( Konszignacio.fejlec.vevo.Cim + " " + Konszignacio.fejlec.vevo.HazSzam );

            table_fejléc.Rows[ 1 ].Cells[ 1 ].Paragraphs[ 0 ].Append( Konszignacio.fejlec.szallitolevel.Rendszamok[ 0 ] );
            table_fejléc.Rows[ 1 ].Cells[ 1 ].Paragraphs[ 0 ].Append( " " + Konszignacio.fejlec.szallitolevel.Rendszamok[ 1 ] );

            table_fejléc.Rows[ 0 ].Cells[ 3 ].Paragraphs[ 0 ].Append( Konszignacio.fejlec.felado.Nev );
            table_fejléc.Rows[ 0 ].Cells[ 3 ].Paragraphs[ 0 ].AppendLine( Konszignacio.fejlec.felado.Cim );

            KonszignacioFejlecTablazatFormazas( table_fejléc );
            FirstPageHeader.InsertTable( table_fejléc );
            #endregion

            #region HeaderTable

            Header TablazatFejlec = document.Headers.odd;

            Table HeaderTable = document.AddTable( 1, 7 );
            HeaderTable.AutoFit = AutoFit.ColumnWidth;

            HeaderTable.Rows[ 0 ].Cells[ 0 ].Paragraphs[ 0 ].Append( "S.Sz." ).Bold( );
            HeaderTable.Rows[ 0 ].Cells[ 1 ].Paragraphs[ 0 ].Append( "Megnevezés" ).Bold( );
            HeaderTable.Rows[ 0 ].Cells[ 2 ].Paragraphs[ 0 ].Append( "Hordó" ).Bold( );
            HeaderTable.Rows[ 0 ].Cells[ 3 ].Paragraphs[ 0 ].Append( "Sarzs" ).Bold( );
            HeaderTable.Rows[ 0 ].Cells[ 4 ].Paragraphs[ 0 ].Append( "Nettó súly" ).Bold( );
            HeaderTable.Rows[ 0 ].Cells[ 5 ].Paragraphs[ 0 ].Append( "Hordó típus" ).Bold( );
            HeaderTable.Rows[ 0 ].Cells[ 6 ].Paragraphs[ 0 ].Append( "Gyártás dátum" ).Bold( );
            KonszignacioDataTableFormazas( HeaderTable );
            TablazatFejlec.InsertTable( HeaderTable );
            #endregion

            #region Data_Table

            Paragraph paragraph_data_table = document.InsertParagraph( );

            Table data_table = document.AddTable( 1, 7 );
            data_table.AutoFit = AutoFit.ColumnWidth;

            data_table.Rows[ 0 ].Cells[ 0 ].Paragraphs[ 0 ].Append( "S.Sz." ).Bold( );
            data_table.Rows[ 0 ].Cells[ 1 ].Paragraphs[ 0 ].Append( "Megnevezés" ).Bold( );
            data_table.Rows[ 0 ].Cells[ 2 ].Paragraphs[ 0 ].Append( "Hordó" ).Bold( );
            data_table.Rows[ 0 ].Cells[ 3 ].Paragraphs[ 0 ].Append( "Sarzs" ).Bold( );
            data_table.Rows[ 0 ].Cells[ 4 ].Paragraphs[ 0 ].Append( "Nettó súly" ).Bold( );
            data_table.Rows[ 0 ].Cells[ 5 ].Paragraphs[ 0 ].Append( "Hordó típus" ).Bold( );
            data_table.Rows[ 0 ].Cells[ 6 ].Paragraphs[ 0 ].Append( "Gyártás dátum" ).Bold( );

            int c = 1;
            int sorszám = 1;
            KonszignacioRendezes( Konszignacio );
            foreach ( KONSZIGNACIO.GYUMOLCSTIPUS outer in Konszignacio.gyumolcstipusok )
            {
                string temp = regex.Replace( outer.Megnevezes, @" " );
                foreach ( KONSZIGNACIO.GYUMOLCSTIPUS.ADAT inner in outer.adat )
                {
                    data_table.InsertRow( );

                    data_table.Rows[ c ].Cells[ 0 ].Paragraphs[ 0 ].Append( sorszám.ToString( ) + '.' );
                    sorszám++;
                    data_table.Rows[ c ].Cells[ 1 ].Paragraphs[ 0 ].Append( temp );
                    data_table.Rows[ c ].Cells[ 2 ].Paragraphs[ 0 ].Append( inner.Hordo );
                    data_table.Rows[ c ].Cells[ 3 ].Paragraphs[ 0 ].Append( inner.Sarzs );
                    data_table.Rows[ c ].Cells[ 4 ].Paragraphs[ 0 ].Append( inner.NettoSuly + " kg" );
                    data_table.Rows[ c ].Cells[ 5 ].Paragraphs[ 0 ].Append( inner.HordoTipus );
                    data_table.Rows[ c ].Cells[ 6 ].Paragraphs[ 0 ].Append( inner.GyartasDatum );

                    c++;
                }

                data_table.InsertRow( );
                data_table.InsertRow( );
                data_table.Rows[ c ].Cells[ 1 ].Paragraphs[ 0 ].Append( temp + "összesen:" ).Bold( );
                data_table.Rows[ c ].Cells[ 4 ].Paragraphs[ 0 ].Append( outer.OsszSuly + " kg" ).Bold( );
                összes_súly += outer.OsszSuly;
                data_table.Rows[ c ].Cells[ 5 ].Paragraphs[ 0 ].Append( "                                 VTSZ:" ).Bold( );        //BN
                data_table.Rows[ c ].Cells[ 6 ].Paragraphs[ 0 ].Append( outer.VTSZ ).Bold( );
                c += 2;
            }
            data_table.InsertRow( );
            data_table.Rows[ c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "Összes elszállítás:" ).Bold( );
            data_table.Rows[ c ].Cells[ 4 ].Paragraphs[ 0 ].Append( összes_súly + " kg" ).Bold( );

            for ( int i = 0 ; i < data_table.Rows.Count ; ++i )
            {
                data_table.Rows[ i ].Cells[ 3 ].Paragraphs[ 0 ].Alignment = Alignment.center;      //sarzs -BN
                data_table.Rows[ i ].Cells[ 4 ].Paragraphs[ 0 ].Alignment = Alignment.center;      //súly -BN
                data_table.Rows[ i ].Cells[ 6 ].Paragraphs[ 0 ].Alignment = Alignment.center;      //dátum
            }

            KonszignacioDataTableFormazas( data_table );
            document.InsertTable( data_table );
            #endregion

            try { document.Save( ); }
            catch ( Exception ) { MessageBox.Show( "A dokumentum meg van nyitva!" ); }
        }

        public static void
        MinosegBizonylatok( KONSZIGNACIOSZALLITOLEVEL _szállítólevél, List<FOGLALAS> _foglalások )
        {
            regex = new Regex( @"[ ]{2,}", RegexOptions.None );

            List<MINOSEGBIZONYLAT> data = new List<MINOSEGBIZONYLAT>( );

            #region Data
            MINOSEGBIZONYLAT temp = new MINOSEGBIZONYLAT( );
            temp.fixstring = new MINOSEGBIZONYLAT.FIXSTRING( _szállítólevél.Nyelv );
            temp.felhasznalo = new MINOSEGBIZONYLAT.FELHASZNALO( _szállítólevél.Nyelv );
            temp.szallitolevel = new MINOSEGBIZONYLAT.SZALLITOLEVEL( _szállítólevél );

            List<string> hotekok = Program.database.MinőségBizonylatHotekok( _foglalások );
            foreach ( string item in hotekok )
            {
                temp.vizsgalatilap = Program.database.MinőségBizonylat( _foglalások, item );
                temp.tapertek = Program.database.MinBiz_Tápérték( temp.vizsgalatilap.Hoteko );
                data.Add( temp );
            }

            #endregion

            if ( _szállítólevél.Szallitolevel.Contains( '/' ) ) { return; }

            string filename = ( Settings.save_directory == null ) ? "Listák//" + _szállítólevél.Szallitolevel + "-MinBiz.docx" : Settings.save_directory + "//" + _szállítólevél.Szallitolevel + "-MinBiz.docx";

            var document = DocX.Create( filename );
            document.AddHeaders( );
            document.AddFooters( );

            #region Header

            Header header = document.Headers.odd;
            Paragraph paragraph_header = header.InsertParagraph( );
            paragraph_header.Direction = Direction.LeftToRight;

            using ( MemoryStream ms = new MemoryStream( ) )
            {
                Image myImg = Image.FromFile( @"Marillen_fejlec.jpg" );     //BN

                myImg.Save( ms, myImg.RawFormat );  // Save your picture in a memory stream.
                ms.Seek( 0, SeekOrigin.Begin );

                Novacode.Image img = document.AddImage( ms ); // Create image.
                Picture pic1 = img.CreatePicture( );     // Create picture.

                paragraph_header.AppendPicture( pic1 );
                paragraph_header.Alignment = Alignment.center;

                var titleFormat = new Formatting( );
                titleFormat.Size = 18D;
                titleFormat.Position = 1;
                titleFormat.Spacing = 5;
                titleFormat.Bold = true;
                Paragraph title = null;
                if ( _szállítólevél.Nyelv == "M" )
                {
                    title = header.InsertParagraph( "MINŐSÉGI BIZONYÍTVÁNY\n", false, titleFormat );
                }
                else
                {
                    title = header.InsertParagraph( "QUALITY CERTIFICATE\n", false, titleFormat );

                }
                title.Alignment = Alignment.center;
            }
            #endregion

            #region Footer

            Footer footer = document.Footers.odd;
            Paragraph paragraph_footer = footer.InsertParagraph( );
            paragraph_footer.Direction = Direction.LeftToRight;

            using ( MemoryStream ms = new MemoryStream( ) )
            {
                Image myImg = Image.FromFile( @"Marillen_lablec.jpg" );     //BN

                myImg.Save( ms, myImg.RawFormat );  // Save your picture in a memory stream.
                ms.Seek( 0, SeekOrigin.Begin );

                Novacode.Image img = document.AddImage( ms ); // Create image.
                Picture pic1 = img.CreatePicture( );     // Create picture.

                paragraph_footer.AppendPicture( pic1 );
                paragraph_footer.Alignment = Alignment.center;
            }
            #endregion

            for ( int i = 0 ; i < data.Count ; i++ )
            {
                #region DataTable
                Table data_table;

                if ( data[ i ].szallitolevel.Vevo == "GABONAL  Kft.                                     " )
                {
                    data_table = document.AddTable( 34, 2 );
                    data_table.Rows[ 32 ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Ethanol:" ).Bold( );
                    data_table.Rows[ 33 ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Hydroxymethylfurfural:" ).Bold( );
                    data_table.Rows[ 32 ].Cells[ 1 ].Paragraphs[ 0 ].Append( "max. 0,2 %" );
                    data_table.Rows[ 33 ].Cells[ 1 ].Paragraphs[ 0 ].Append( "max. 5 mg/l" );

                }
                else { data_table = document.AddTable( 31, 2 ); }
                MinBizDataTable( data[ i ], _szállítólevél, i, data_table );

                MinBizDataTablazatFormazasa( data_table );
                document.InsertTable( data_table );

                Paragraph p = document.InsertParagraph( );

                MinBizSzoveg( _szállítólevél.Nyelv, p, data[ i ], document );

                if ( i != data.Count - 1 )
                    document.InsertSectionPageBreak( false );
                #endregion
            }

            try { document.Save( ); }
            catch ( Exception ) { MessageBox.Show( "A dokumentum meg van nyitva!" ); }
        }

        #region SegédFüggvények

        public static void
        MinBizDataTable( MINOSEGBIZONYLAT _data, KONSZIGNACIOSZALLITOLEVEL _szállítólevél, int i, Table table )
        {
            int c;
            if ( _szállítólevél.Nyelv == "M" )
            {
                #region fixstring
                c = -1;
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Szállítólevél szám:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Vevő megnevezése:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Megnevezés:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Gyártási idő:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Sarzs:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Szín:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Íz:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Illat:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Brix:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Savtartalom (citromsavban):" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "pH:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Konzisztencia (Bostwick fok):" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Hozzáadott citromsav:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Hozzáadott cukor:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Aszkorbinsav:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Hozzáadott színezék:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Hozzáadott aroma:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Hozzáadott tartósítószer:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Átlagos tápérték tartalom 100 g termékben" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Energia tartalom:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Fehérje:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Szénhidrát:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Zsír:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Élelmi rost:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Mikrobiológia:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Minőségét megőrzi:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Passzírozottság:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Nettó tömeg:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Tárolás:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Csomagolás:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Származási hely:" ).Bold( );
                #endregion

                #region data
                c = -1;
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _szállítólevél.Szallitolevel ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Vevo ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.Megnevezes ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.GyartasiIdo.Substring( 0, 4 ) + ". évben" ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.Sarzs ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Szin );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Iz );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Illat );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Brix ) + " %" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Citromsav ) + " %" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Ph ) );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Bostwick ) + " cm/30 sec" );
                if ( _data.vizsgalatilap.CitromsavAdagolas.min == 0 && _data.vizsgalatilap.CitromsavAdagolas.max == 0 ) { table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "nincs" ); }
                else { table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.CitromsavAdagolas ) ); }
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Cukor );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "maximum " + _data.vizsgalatilap.Aszkorbinsav + " mg/kg" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Szinezek );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Aroma );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Aroma );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.EnergiaTartalom1 + " kj / " + _data.tapertek.EnergiaTartalom2 + " kcal" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Feherje + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Szenhidrat + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Zsir + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Elelmirost + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.MikroBiologia );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.MinosegetMegorzi );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.Paszirozottsag + "-es szitán" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.NettoTomeg );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Tarolas );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "aszeptikus zsákban és " + _data.vizsgalatilap.Csomagolas );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.SzarmazasiHely );
                #endregion
            }
            else
            {
                #region fixstring
                c = -1;
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Customer name:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Product name:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Date of production:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Batch number:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Colour:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Taste:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Odour:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Brix:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Acid content (in citric acid):" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "pH:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Consistence (Bostwick, 20°C):" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Added citric acid:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Added sugar:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Ascorbic acid:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Added colours:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Added flavours:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Added preservatives:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Average nutritional values in 100 g product" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Energy:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Protein:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Carbohydrate:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Fat:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "     Dietary fiber:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Microbiological status:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Best before:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Sieve size:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Net weight:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Storage:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Packaging:" ).Bold( );
                table.Rows[ ++c ].Cells[ 0 ].Paragraphs[ 0 ].Append( "Country of origin:" ).Bold( );
                #endregion

                #region data
                c = -1;
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Vevo ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.Megnevezes ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.GyartasiIdo.Substring( 0, 4 ) ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.Sarzs ).Bold( );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Szin );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Iz );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.szallitolevel.Illat );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Brix ) + " %" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Citromsav ) + " %" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Ph ) );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.Bostwick ) + " cm/30 sec" );
                if ( MinMax( _data.vizsgalatilap.CitromsavAdagolas ) == null ) { table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "no" ); } else { table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( MinMax( _data.vizsgalatilap.CitromsavAdagolas ) ); }
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Cukor );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "maximum " + _data.vizsgalatilap.Aszkorbinsav + " mg/kg" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Szinezek );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Aroma );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Aroma );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.EnergiaTartalom1 + " kj / " + _data.tapertek.EnergiaTartalom2 + " kcal" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Feherje + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Szenhidrat + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Zsir + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.tapertek.Elelmirost + " g" );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.MikroBiologia );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.MinosegetMegorzi );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.vizsgalatilap.Paszirozottsag );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.NettoTomeg );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( _data.fixstring.Tarolas );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( "aseptic bags and " + Program.database.Törzsadat_Angol( _data.vizsgalatilap.Csomagolas ) );
                table.Rows[ ++c ].Cells[ 1 ].Paragraphs[ 0 ].Append( Program.database.Törzsadat_Angol( _data.vizsgalatilap.SzarmazasiHely ) );
                #endregion
            }
        }

        public static void
        MinBizSzoveg( string _nyelv, Paragraph p, MINOSEGBIZONYLAT _data, DocX document )
        {
            MINOSEGBIZONYLAT_SZOVEG minbizszöveg = Program.database.MinőségBizonylat_Szöveg( );

            if ( _nyelv == "M" )
            {
                p.AppendLine( minbizszöveg.MagyarSzoveg1 );
                p.AppendLine( minbizszöveg.MagyarSzoveg2 );
                p.AppendLine( );
                p.AppendLine( "Kiskunfélegyháza, " + DateTime.Now.Year + ". " + DateTime.Now.ToString( "MMMM" ) + " " + DateTime.Now.Day + "." );
                Paragraph q = document.InsertParagraph( );
                q.Alignment = Alignment.right;
                q.Append( _data.felhasznalo.Nev + "\n" + _data.felhasznalo.Beosztas );
            }
            else
            {
                CultureInfo ci = new CultureInfo( "en-US" );
                var month = DateTime.Now.ToString( "MMMM", ci );

                p.AppendLine( minbizszöveg.AngolSzoveg1 );
                p.AppendLine( minbizszöveg.AngolSzoveg2 );
                p.AppendLine( );
                p.AppendLine( "Kiskunfélegyháza, " + DateTime.Now.Day + "nd " + month + " " + DateTime.Now.Year );
                Paragraph q = document.InsertParagraph( );
                q.Alignment = Alignment.right;
                q.Append( _data.felhasznalo.Nev + "\n" + _data.felhasznalo.Beosztas );
            }
        }

        public static string
        MinMax( MinMaxPair<double?> _minmax )
        {
            string value = null;

            if ( _minmax.min == null && _minmax.max == null ) { return value; }
            if ( _minmax.min != null && _minmax.max != null ) { value = _minmax.min + " - " + _minmax.max; }
            else if ( _minmax.min != null ) { value = _minmax.min.ToString( ); }
            else { value = _minmax.max.ToString( ); }
            return value;
        }

        public static void
        MinBizDataTablazatFormazasa( Table _table )
        {
            _table.AutoFit = AutoFit.Contents;

            Border c = new Border( BorderStyle.Tcbs_none, BorderSize.seven, 0, Color.Black );
            _table.SetBorder( TableBorderType.InsideH, c );
            _table.SetBorder( TableBorderType.InsideV, c );
            _table.SetBorder( TableBorderType.Bottom, c );
            _table.SetBorder( TableBorderType.Top, c );
            _table.SetBorder( TableBorderType.Left, c );
            _table.SetBorder( TableBorderType.Right, c );
        }

        public static void
        KonszignacioFejlecTablazatFormazas( Table _table )
        {
            Border c = new Border( BorderStyle.Tcbs_none, BorderSize.seven, 0, Color.Black );
            _table.SetBorder( TableBorderType.InsideH, c );
            _table.SetBorder( TableBorderType.InsideV, c );
            _table.SetBorder( TableBorderType.Bottom, c );
            _table.SetBorder( TableBorderType.Top, c );
            _table.SetBorder( TableBorderType.Left, c );
            _table.SetBorder( TableBorderType.Right, c );

            //konszig táblázat fejléc-BN
            _table.AutoFit = AutoFit.ColumnWidth;

            for ( int i = 0 ; i < _table.Rows.Count ; i++ )
            {
                _table.Rows[ i ].Cells[ 0 ].Width = 80;         //vevő fix
                _table.Rows[ i ].Cells[ 1 ].Width = 300;        //vevő
                _table.Rows[ i ].Cells[ 2 ].Width = 60;         //feladó fix
                _table.Rows[ i ].Cells[ 3 ].Width = 300;        //feladó
            }
            _table.AutoFit = AutoFit.ColumnWidth;
        }

        public static void
        KonszignacioDataTableFormazas( Table _table )
        {
            _table.AutoFit = AutoFit.Contents;
            Border c = new Border( BorderStyle.Tcbs_none, BorderSize.two, 0, Color.Black );
            _table.SetBorder( TableBorderType.InsideH, c );
            _table.SetBorder( TableBorderType.InsideV, c );
            _table.SetBorder( TableBorderType.Bottom, c );
            _table.SetBorder( TableBorderType.Top, c );
            _table.SetBorder( TableBorderType.Left, c );
            _table.SetBorder( TableBorderType.Right, c );

            for ( int i = 0 ; i < _table.Rows[ 0 ].Cells.Count ; i++ )
            {
                _table.Rows[ 0 ].Cells[ i ].SetBorder( TableCellBorderType.Top, new Border( BorderStyle.Tcbs_single, BorderSize.six, 0, Color.Black ) );
                _table.Rows[ 0 ].Cells[ i ].SetBorder( TableCellBorderType.Bottom, new Border( BorderStyle.Tcbs_single, BorderSize.six, 0, Color.Black ) );
            }
            for ( int i = 0 ; i < _table.Rows.Count ; i++ )
            {
                if ( _table.Rows[ i ].Cells[ 1 ].Paragraphs[ 0 ].Text.Contains( "összesen" ) )
                {
                    for ( int j = 0 ;
                         j < _table.Rows[ i ].Cells.Count ;
                         j++ )
                    {
                        _table.Rows[ i ].Cells[ j ].SetBorder( TableCellBorderType.Top, new Border( BorderStyle.Tcbs_single,
                                                                                       BorderSize.six, 0, Color.Black ) );
                    }
                }
                else if ( _table.Rows[ i ].Cells[ 1 ].Paragraphs[ 0 ].Text.Contains( "elszállítás" ) )
                {
                    for ( int j = 0 ;
                         j < _table.Rows[ i ].Cells.Count ;
                         j++ )
                    {
                        _table.Rows[ i ].Cells[ j ].SetBorder( TableCellBorderType.Top, new Border( BorderStyle.Tcbs_single,
                                                                                               BorderSize.six, 0, Color.Black ) );
                        _table.Rows[ i ].Cells[ j ].SetBorder( TableCellBorderType.Bottom, new Border( BorderStyle.Tcbs_single,
                                                                                                BorderSize.six, 0, Color.Black ) );
                    }
                    _table.Rows[ i ].Cells[ 0 ].SetBorder( TableCellBorderType.Left, new Border( BorderStyle.Tcbs_single,
                                                                                            BorderSize.six, 0, Color.Black ) );
                    _table.Rows[ i ].Cells[ 6 ].SetBorder( TableCellBorderType.Right, new Border( BorderStyle.Tcbs_single,
                                                                                            BorderSize.six, 0, Color.Black ) );
                }
            }

            //konszig táblázat-BN
            _table.AutoFit = AutoFit.ColumnWidth;

            for ( int i = 0 ; i < _table.Rows.Count ; i++ )
            {
                _table.Rows[ i ].Cells[ 0 ].Width = 45;     //sorszám
                _table.Rows[ i ].Cells[ 1 ].Width = 210;    //megnevezés
                _table.Rows[ i ].Cells[ 2 ].Width = 60;     //hordó
                _table.Rows[ i ].Cells[ 3 ].Width = 50;     //sarzs
                _table.Rows[ i ].Cells[ 4 ].Width = 80;     //súly
                _table.Rows[ i ].Cells[ 5 ].Width = 160;    //h.típus
                _table.Rows[ i ].Cells[ 6 ].Width = 110;    //dátum
            }
            _table.AutoFit = AutoFit.ColumnWidth;
        }

        /// <summary>
        /// lista sorrend
        /// </summary>
        public static void
        KonszignacioRendezes( KONSZIGNACIO _eredeti )
        {
            foreach ( KONSZIGNACIO.GYUMOLCSTIPUS outer in _eredeti.gyumolcstipusok )
            {
                for ( int i = 0 ; i < outer.adat.Count ; i++ )
                {
                    for ( int j = 0 ; j < outer.adat.Count ; j++ )
                    {
                        if ( Convert.ToInt32( outer.adat[ i ].Hordo ) < Convert.ToInt32( outer.adat[ j ].Hordo ) )
                        {
                            KONSZIGNACIO.GYUMOLCSTIPUS.ADAT temp = new KONSZIGNACIO.GYUMOLCSTIPUS.ADAT( );
                            temp = outer.adat[ i ];
                            outer.adat[ i ] = outer.adat[ j ];
                            outer.adat[ j ] = temp;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
