using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Labor
{
    public sealed class Database
    {
        private SqlConnection marillenconnection;
        private SqlConnection laborconnection;
        private Object MarillenLock = new Object();
        private Object LaborLock = new Object();

        public Database()
        {
            marillenconnection = new SqlConnection("Server=.\\SQLEXPRESS;Database=marillen2013;Integrated Security=true");
            laborconnection = new SqlConnection("Server=.\\SQLEXPRESS;Database=Labor;Integrated Security=true");

            try
            {
                laborconnection.Open();

                /*try
                {
                }
                catch (Exception )
                {
                    laborconnection.Close();
                    return;
                }*/

                laborconnection.Close();
            }
            catch
            {
                SqlConnection create_connection = new SqlConnection("Server=.\\SQLEXPRESS;Integrated Security=true");

                try
                {
                    create_connection.Open();
                    SqlCommand command = new SqlCommand("CREATE DATABASE Labor", create_connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    create_connection.Close();
                }
                catch
                {
                    return;
                }

                //try
                {
                    //MessageBox.Show("Várakozás a Labor adatbázis létrehozására..", "Információ");
                    System.Threading.Thread.Sleep(5000);
                    laborconnection = new SqlConnection("Server=.\\SQLEXPRESS;Database=Labor;Integrated Security=true");
                    laborconnection.Open();
                    SqlCommand command = new SqlCommand(
                            "CREATE TABLE L_TORZSA (TOTIPU varchar(20) NOT NULL,TOAZON varchar(15) PRIMARY KEY,TOSZO2 varchar(15),TOSZO3 varchar(15));" +

                            // Vizsgálat
                            "CREATE TABLE L_VIZSLAP (VITEKO varchar(4) NOT NULL, VISARZ varchar(3) NOT NULL, VIHOSZ varchar(4) NOT NULL, VIHOTI varchar(15), VINETO DECIMAL(6, 3), VISZAT tinyint, VIMEGR varchar(50), VIMSSZ int IDENTITY(1,1)," +

                                // Adatok1
                                "VITENE varchar(50), VIHOKE tinyint, VIGYEV varchar(1), VIMUJE varchar(1), VITOGE varchar(1),  VISZOR varchar(15), VIFAJT varchar(50), " +
                                
                                // Adatok2
                                "VIBRIX DECIMAL(3,1), VICSAV DECIMAL(4,2), VIBOSA DECIMAL(4,2), VIPEHA DECIMAL(4,2), VIBOST DECIMAL(3,1) ,VIASAV tinyint, VICIAD tinyint, VIMATO tinyint, VIFEFE tinyint, VIFEBA tinyint, " +
                                "VISZIN varchar(60), VIIZEK varchar(60), VIILLA varchar(60), " +
                                
                                // Adatok3
                                "VILEOL varchar(11), VIERDA varchar(11), VIOCS1 tinyint, VIOCS2 tinyint, VIELE1 tinyint, VIELE2 tinyint, VIPEN1 tinyint, VIPEN2 tinyint, VIEMEJE varchar(300), " +
                                
                                // Adatok4
                                "VIMTS1 varchar(15),VIMTD1 varchar(8),VIMKS1 varchar(15),VIMKD1 varchar(8), VIMKS2 varchar(15), VIMKD2 varchar(8), VIMKS3 varchar(15), VIMKD3 varchar(8), VIMKS4 varchar(15), VIMKD4 varchar(8), " +
                                "VIMKS5 varchar(15), VIMKD5 varchar(8), VIMKS6 varchar(15), VIMKD6 varchar(8),VILABO varchar(15));" +

                            "CREATE TABLE L_HORDO(HOTEKO varchar(3),HOSARZ varchar(3) ,HOZSSZ varchar(4),FOSZAM tinyint, VIGYEV varchar(1));" +

                            "CREATE TABLE L_FOGLAL (FONEVE varchar(30), FOSZAM tinyint, FODATE varchar(15), FOTIPU varchar(9), FOFENE varchar(15), FOTEKO varchar(3), FOSARZT varchar(3), FOSARZI varchar(3), FOZSSZT varchar(4)," +
                                "FOZSSZI varchar(4), FOBRIXT DECIMAL(4,4), FOBRIXI DECIMAL(4,4), FOCSAVT DECIMAL(4,4), FOCSAVI DECIMAL(4,4), FOPEHAT DECIMAL(4,4), FOPEHAI DECIMAL(4,4), FOBOSTT DECIMAL(4,4)," +
                                "FOBOSTI DECIMAL(4,4), FOASAVT smallint, FOASAVI smallint, FONETOT smallint, FONETOI smallint, FOHOFOT tinyint, FOHOFOI tinyint, FOCIADT smallint, FOCIADI smallint," +
                                "FOFAJT varchar(15), FOHOTI varchar(15), FOMEGR varchar(15), FOSZOR varchar(15), FOMUJE varchar(1), FOTOGE varchar(1), FOFOHO tinyint, FOSZSZ tinyint ," +
                                "FOSZATI varchar(6) , FOSZATT varchar(6), FOBOSAI smallint, FOBOSAT smallint, SZSZAM tinyint);" +

                            "CREATE TABLE L_FELHASZ (FEFEN1 varchar(15), FEFEN2 varchar(15), FEBEO1 varchar(15), FEBEO2 varchar(15), FEBEKO varchar(15), FEJELS varchar(15), FETOHO varchar(1), FETORO varchar(1), FETOTO varchar(1)," +
                                "FEVIHO varchar(1), FEVIRO varchar(1), FEVITO varchar(1), FEFOKE varchar(1), FEFOFE varchar(1), FEFOTO varchar(1), FEFEHO varchar(1), FEFERO varchar(1), FEFETO varchar(1), FEKONY varchar(1), FEKITO varchar(1));" +

                            "CREATE TABLE L_SZLEV (SZSZAM tinyint PRIMARY KEY, SZSZSZ varchar(15), SZFENE varchar(15), SZDATE varchar(10), SZNYEL varchar(1), SZVEVO varchar(15), SZGKR1 varchar(7), SZGKR2 varchar(7)," +
                                "FOFOHO tinyint, SZGYEV varchar(4), SZSZIN varchar(60), SZIZEK varchar(60), SZILLA varchar(60));" +

                            "CREATE TABLE L_EREDMENY(RTEKO varchar(3), RSARZ varchar(3), RSZHO tinyint, FOFOHO tinyint);" +

                            "CREATE TABLE L_GYFAJTA(GFTEKO varchar(2), GFAZON varchar(15), GFSZO2 varchar(15), GFSZO3 varchar(15));" +

                            "CREATE TABLE L_TAPERTEK(TATEKO varchar(2), TAKIO tinyint, TAKCAL tinyint, TAFEHE DECIMAL(2, 1), TASZHI DECIMAL(3, 1), TAZSIR DECIMAL(2, 1), TAELRO DECIMAL(2, 1));" +

                            "CREATE TABLE L_MINBIZ(MISZ1M varchar(600), MISZ1A varchar(600), MISZ2M varchar(600), MISZ2A varchar(600));" +

                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Származási ország" + "','" + "Magyarország" + "','" + "Hungary" + "','" + "Ungarn" + "');" +
                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Származási ország" + "','" + "Szlovákia" + "','" + "Slovakia" + "','" + "Slowakei" + "');" +
                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Származási ország" + "','" + "Románia" + "','" + "Romania" + "','" + "Rumänien" + "');" +
                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Hordótípus" + "','" + "Kicsi" + "','" + "Small" + "','" + "Klein" + "');" +
                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Hordótípus" + "','" + "Nagy" + "','" + "Big" + "','" + "Groß" + "');" +
                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Laboros" + "','" + "Belinyák Máté" + "','" + "Máté Belinyák" + "','" + "Máté Belinyák" + "');" +
                            "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + "Laboros" + "','" + "Belinyák Nándor" + "','" + "Nándor Belinyák" + "','" + "Nándor Belinyák" + "');", laborconnection);
                    command.ExecuteNonQuery();
                    command.Dispose();
                    laborconnection.Close();
                }
                /*catch (Exception _e)
                {
                    return;
                }*/
            }

        }

        public static bool IsCorrectSQLText(string _text)
        {
            if (_text.Contains("'") || _text.Contains("\"") || _text.Contains("(") || _text.Contains(")")) return false;
            return true;
        }

        #region Segédfüggvények
        public static string A(string[] _texts)
        {
            string value = null;
            int length = 0;
            for (int current = 0; current < _texts.Length; ++current)
                if (_texts[current] != null)
                {
                    value += _texts[current] + " AND ";
                    length++;
                }
            if (length == 0) return null;
            return value.Substring(0, value.Length - 5);
        }

        public static string V(string[] _texts)
        {
            string value = null;
            int length = 0;
            for (int current = 0; current < _texts.Length; ++current)
                if (_texts[current] != null)
                {
                    value += _texts[current] + ", ";
                    length++;
                }
            if (length == 0) return null;
            return value.Substring(0, value.Length - 2);
        }

        public static string Update<T>(string _column_name, T _value)
        {
            Type type = typeof(T);
            if (type == typeof(char)) { return _column_name + " = '" + _value + "'"; }
            if (type == typeof(string)) { if (_value != null) return _column_name + " = '" + _value + "'"; else return null; }

            if (type == typeof(byte)) { return _column_name + " = " + _value; }

            if (type == typeof(int)) { return _column_name + " = " + _value; }
            if (type == typeof(int?)) { if (_value != null) return _column_name + " = " + _value; else return null; }

            if (type == typeof(double)) { return _column_name + " = " + _value.ToString().Replace(',', '.'); }
            if (type == typeof(double?)) { if (_value != null) return _column_name + " = " + _value.ToString().Replace(',', '.'); else return null; }

            throw new IndexOutOfRangeException();
        }

        public static T? GetNullable<T>(SqlDataReader _reader, int _column) where T : struct 
        {
            if (!_reader.IsDBNull(_column))
                return _reader.GetFieldValue<T>(_column);
            return null;
        }

        public static string GetNullableString(SqlDataReader _reader, int _column)
        {
            if (!_reader.IsDBNull(_column)) return _reader.GetString(_column);
            return null;
        }
        #endregion

        #region Marillen Adatbázisából
        public List<string> Gyümölcsfajták( string _termékkód )
        {
            lock (MarillenLock)
            {
                List<string> value = new List<string>();
                /*
                marillenconnection.Open();
                SqlCommand command = marillenconnection.CreateCommand();
                command.CommandText = "SELECT gfazon FROM l_gyfajta WHERE (l_gyfajta.gfazon = " + _termékkód.Substring(0, 2) + ") ORDER BY gfazon";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(reader.GetString(0));
                }
                command.Dispose();
                marillenconnection.Close();
                 */
                value.Add("FIXME - gyümölcsfajta");
                return value;
            }
        }

        public List<string> Megrendelők()
        {
            lock (MarillenLock)
            {
                List<string> value = new List<string>();
                marillenconnection.Open();
                SqlCommand command = marillenconnection.CreateCommand();
                command.CommandText = "SELECT name FROM partner ORDER BY name";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(reader.GetString(0));
                }
                command.Dispose();
                marillenconnection.Close();
                return value;
            }
        }

        /// <summary>
        /// Visszaadja a termékkód darabkával kezdődő kódokat
        /// </summary>
        public List<string> Termékkódok(string _termékkód_darab)
        {
            lock (MarillenLock)
            {
                List<string> value = new List<string>();
                string temp = "12" + _termékkód_darab.Substring(0, 2) + "01";
                marillenconnection.Open();
                SqlCommand command = marillenconnection.CreateCommand();
                command.CommandText = "SELECT item_nr, name FROM cikkek WHERE item_nr LIKE '" + temp + "' ORDER BY item_nr";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(reader.GetString(0) + "-" + reader.GetString(1));
                }
                command.Dispose();
                marillenconnection.Close();

                return value;
            }
        }

        /// <summary>
        /// Termékkód, hordószám megadása után a fejléc adatait kérjük le!
        /// box_szita_átmérő, nettó_töltet, műszak_jele, töltőgép_száma, sarzs
        /// </summary>
        public List<string> Vizsgálat_Prod_Id(string _prod_id)
        {
            lock (MarillenLock)
            {
                List<string> value = new List<string>();

                string serial = null;
                string prodid = null;

                marillenconnection.Open();

                SqlCommand command = new SqlCommand("SELECT serial_nr, prod_id, qty FROM tetelek WHERE (type=300) AND (prod_id LIKE '" + _prod_id + "')");
                command.Connection = marillenconnection;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    serial = reader.GetString(0);
                    prodid = reader.GetString(1);
                    //szitaméret
                    value.Add(prodid[7].ToString());
                    //nettó töltet
                    value.Add((reader.GetValue(2)).ToString());
                }
                reader.Close();

                command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr = '" + serial + "') AND (code=1)");
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //múszak jele
                    value.Add(reader.GetString(0).Substring(0, 1));
                }
                reader.Close();

                command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr = '" + serial + "') AND (code=2)");
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //töltőgép száma
                    value.Add(reader.GetString(0));
                }
                reader.Close();

                command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr = '" + serial + "') AND (code=3)");
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //sarzs
                    value.Add(reader.GetString(0));
                }
                reader.Close();
                marillenconnection.Close();

                return value;
            }
        }

        /// <summary>
        /// Termékkódhoz tartozó terméknév lekérdezése, ha nem találja, akkor null-t ad vissza!
        /// </summary>
        public string Vizsgálat_Terméknév(string _termékkód)
        {
            lock (MarillenLock)
            {
                string value = null;
                marillenconnection.Open();
                SqlCommand command = new SqlCommand("SELECT cikkek.name FROM cikkek WHERE (item_nr ='" + _termékkód + "01" + "') ORDER BY item_nr");
                command.Connection = marillenconnection;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value = reader.GetString(0);
                }
                reader.Close();
                marillenconnection.Close();
                return value;
            }
        }
        #endregion

        #region Törzsadatok
        public List<Törzsadat> Törzsadatok(string _TOTIPU)
        {
            lock (LaborLock)
            {
                List<Törzsadat> data = new List<Törzsadat>();
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT TOTIPU, TOAZON, TOSZO2, TOSZO3 FROM L_TORZSA WHERE TOTIPU = '" + _TOTIPU + "'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new Törzsadat(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                }
                command.Dispose();
                laborconnection.Close();

                return data;
            }
        }

        public bool Törzsadat_Hozzáadás(Törzsadat _törzsadat)
        {
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2,TOSZO3) VALUES('" + _törzsadat.típus + "','" + _törzsadat.azonosító + "','" + _törzsadat.megnevezés_2 + "','" + _törzsadat.megnevezés_3 + "');";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    laborconnection.Close();
                }

                Program.mainform.RefreshData();
                return true;
            }
        }

        public List<string> Törzsadat_Típusok()
        {
            lock (LaborLock)
            {
                List<string> típusok = new List<string>();

                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT TOTIPU FROM L_TORZSA GROUP BY TOTIPU;";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    típusok.Add(reader.GetString(0));
                }
                command.Dispose();
                laborconnection.Close();
                return típusok;
            }
        }

        public bool Törzsadat_Módosítás(string _azonosító, Törzsadat törzsadat)
        {
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_TORZSA SET TOAZON='" + törzsadat.azonosító + "', TOSZO2= '" + törzsadat.megnevezés_2 + "', TOSZO3= '" + törzsadat.megnevezés_3 + "' WHERE TOAZON = '" + _azonosító + "';";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (System.Exception)
                {
                    return false;
                }
                finally
                {
                    command.Dispose();
                    laborconnection.Close();
                }

                Program.mainform.RefreshData();
                return true;
            }
        }

        public bool Törzsadat_Törlés(string _azonosító)
        {
            lock (LaborLock)
            {
                bool found = true;
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "DELETE FROM L_TORZSA WHERE TOAZON= '" + _azonosító + "';";
                if (command.ExecuteNonQuery() == 0) found = false;
                command.Dispose();
                laborconnection.Close();

                Program.mainform.RefreshData();
                return found;
            }
        }
        #endregion

        #region Vizsgálatok
        public Vizsgálat? Vizsgálat(Vizsgálat.Azonosító _azonosító)
        {
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command;
                SqlDataReader reader;

                // Azonosító
                Vizsgálat.Azonosító? azonosító = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VITEKO, VISARZ, VIHOSZ, VIHOTI, VINETO, VISZAT, VIMEGR, VIMSSZ FROM L_VIZSLAP";
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        azonosító = new Vizsgálat.Azonosító(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), (double)reader.GetDecimal(4), reader.GetByte(5), reader.GetString(6), reader.GetInt32(7));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Azonosító lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok1
                Vizsgálat.Adatok1? adatok1 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VITENE, VIHOKE, VIGYEV, VIMUJE, VITOGE, VISZOR, VIFAJT FROM L_VIZSLAP";
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok1 = new Vizsgálat.Adatok1(reader.GetString(0), reader.GetByte(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok1 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok2
                Vizsgálat.Adatok2? adatok2 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VIBRIX, VICSAV, VIBOSA, VIPEHA, VIBOST, VIASAV, VICIAD, VIMATO, VIFEFE, VIFEBA, VISZIN, VIIZEK, VIILLA FROM L_VIZSLAP";
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok2 = new Vizsgálat.Adatok2((double?)GetNullable<decimal>(reader, 0), (double?)GetNullable<decimal>(reader, 1), (double?)GetNullable<decimal>(reader, 2),
                            (double?)GetNullable<decimal>(reader, 3), (double?)GetNullable<decimal>(reader, 4), GetNullable<byte>(reader, 5), GetNullable<byte>(reader, 6), GetNullable<byte>(reader, 7),
                            GetNullable<byte>(reader, 8), GetNullable<byte>(reader, 9), GetNullableString(reader, 10), GetNullableString(reader, 11), GetNullableString(reader, 12));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok2 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok3
                Vizsgálat.Adatok3? adatok3 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VILEOL, VIERDA, VIOCS1, VIOCS2, VIELE1, VIELE2, VIPEN1, VIPEN2, VIEMEJE FROM L_VIZSLAP";
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok3 = new Vizsgálat.Adatok3(GetNullableString(reader, 0), GetNullableString(reader, 1), GetNullable<byte>(reader, 2), GetNullable<byte>(reader, 3), GetNullable<byte>(reader, 4),
                            GetNullable<byte>(reader, 5), GetNullable<byte>(reader, 6), GetNullable<byte>(reader, 7), GetNullableString(reader, 8));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok1 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok4
                Vizsgálat.Adatok4? adatok4 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VIMTS1, VIMTD1, VIMKS1, VIMKD1, VIMKS2, VIMKD2, VIMKS3, VIMKD3, VIMKS4, VIMKD4, VIMKS5, VIMKD5, VIMKS6, VIMKD6, VILABO FROM L_VIZSLAP";
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok4 = new Vizsgálat.Adatok4(GetNullableString(reader, 0), GetNullableString(reader, 1), GetNullableString(reader, 2), GetNullableString(reader, 3), GetNullableString(reader, 4), GetNullableString(reader, 5),
                            GetNullableString(reader, 6), GetNullableString(reader, 7), GetNullableString(reader, 8), GetNullableString(reader, 9), GetNullableString(reader, 10), GetNullableString(reader, 11), GetNullableString(reader, 12),
                            GetNullableString(reader, 13), GetNullableString(reader, 14));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok1 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                command.Dispose();
                laborconnection.Close();

                return new Vizsgálat(azonosító.Value, adatok1.Value, adatok2.Value, adatok3.Value, adatok4.Value);
            }
        }

        public List<Vizsgálat.Azonosító> Vizsgálatok()
        {
            lock (LaborLock)
            {
                List<Vizsgálat.Azonosító> data = new List<Vizsgálat.Azonosító>();
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VITEKO, VISARZ, VIHOSZ, VIHOTI, VINETO, VISZAT, VIMEGR, VIMSSZ FROM L_VIZSLAP";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new Vizsgálat.Azonosító(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), (double)reader.GetDecimal(4), reader.GetByte(5), reader.GetString(6), reader.GetInt32(7)));
                }
                command.Dispose();
                laborconnection.Close();

                return data;
            }
        }

        /// <summary>
        /// Ha a Vizsgálat táblában ennek a vizsgálatnak már van eltérő hordótípusa, akkor az előző típust kell visszaadni, különben null!
        /// </summary>
        public string Vizsgálat_SarzsEllenőrzés(Vizsgálat.Azonosító _azonosító)
        {
            lock (LaborLock)
            {
                return null;
            }
        }

        public bool Vizsgálat_Hozzáadás(Vizsgálat _vizsgálat)
        {
            lock (LaborLock)
            {
                string data;
                SqlCommand command;

                laborconnection.Open();

                // Azonosító
                command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_VIZSLAP (VITEKO, VISARZ, VIHOSZ, VIHOTI, VINETO, VISZAT, VIMEGR)" +
                                    " VALUES('" + _vizsgálat.azonosító.termékkód + "', '" + _vizsgálat.azonosító.sarzs + "', '" + _vizsgálat.azonosító.hordószám + "', '" + _vizsgálat.azonosító.hordótípus + "', "
                                    + _vizsgálat.azonosító.nettó_töltet.ToString().Replace(',', '.') + ", " + _vizsgálat.azonosító.szita_átmérő + ", '" + _vizsgálat.azonosító.megrendelő + "')";

                try { command.ExecuteNonQuery(); command.Dispose(); }
                catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> INSERT hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok1

                data = V(new string[] {Update<string>("VITENE", _vizsgálat.adatok1.terméknév), Update<byte>("VIHOKE", _vizsgálat.adatok1.hőkezelés), Update<char>("VIGYEV", _vizsgálat.adatok1.gyártási_év[_vizsgálat.adatok1.gyártási_év.Length - 1]),
                Update<string>("VIMUJE", _vizsgálat.adatok1.műszak_jele), Update<string>("VITOGE", _vizsgálat.adatok1.töltőgép), Update<string>("VISZOR", _vizsgálat.adatok1.szárm_ország),
                Update<string>("VIFAJT", _vizsgálat.adatok1.gyümölcsfajta)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIHOSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #1 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok2

                data = V(new string[] {Update<double?>("VIBRIX", _vizsgálat.adatok2.brix), Update<double?>("VICSAV", _vizsgálat.adatok2.citromsav),
                Update<double?>("VIBOSA", _vizsgálat.adatok2.borkősav), Update<double?>("VIPEHA", _vizsgálat.adatok2.ph), Update<double?>("VIBOST", _vizsgálat.adatok2.bostwick),
                Update<int?>("VIASAV", _vizsgálat.adatok2.aszkorbinsav), Update<int?>("VICIAD", _vizsgálat.adatok2.citromsav_adagolás),  Update<int?>("VIMATO", _vizsgálat.adatok2.magtöret),
                Update<int?>("VIFEFE", _vizsgálat.adatok2.feketepont),  Update<int?>("VIFEBA", _vizsgálat.adatok2.barnapont),  Update<string>("VISZIN", _vizsgálat.adatok2.szín),
                Update<string>("VIIZEK", _vizsgálat.adatok2.íz), Update<string>("VIILLA", _vizsgálat.adatok2.illat)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIHOSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #2 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok3

                data = V(new string[] {Update<string>("VILEOL", _vizsgálat.adatok3.leoltás), Update<string>("VIERDA", _vizsgálat.adatok3.értékelés),
                Update<int?>("VIOCS1", _vizsgálat.adatok3.összcsíra_1), Update<int?>("VIOCS2", _vizsgálat.adatok3.összcsíra_2), Update<int?>("VIPEN1", _vizsgálat.adatok3.penész_1),
                Update<int?>("VIPEN2", _vizsgálat.adatok3.penész_2), Update<int?>("VIELE1", _vizsgálat.adatok3.élesztő_1),  Update<int?>("VIELE2", _vizsgálat.adatok3.élesztő_2),
                Update<string>("VIEMEJE", _vizsgálat.adatok3.megjegyzés)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIHOSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #3 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok4

                data = V(new string[] { Update<string>("VIMTS1", _vizsgálat.adatok4.címzett_t), Update<string>("VIMTD1", _vizsgálat.adatok4.dátum_t),
                Update<string>("VIMKS1", _vizsgálat.adatok4.címzett_k1), Update<string>("VIMKD1", _vizsgálat.adatok4.dátum_k1), Update<string>("VIMKS2", _vizsgálat.adatok4.címzett_k2), Update<string>("VIMKD2", _vizsgálat.adatok4.dátum_k2),
                Update<string>("VIMKS3", _vizsgálat.adatok4.címzett_k3), Update<string>("VIMKD3", _vizsgálat.adatok4.dátum_k3), Update<string>("VIMKS4", _vizsgálat.adatok4.címzett_k4), Update<string>("VIMKD4", _vizsgálat.adatok4.dátum_k4),
                Update<string>("VIMKS5", _vizsgálat.adatok4.címzett_k5), Update<string>("VIMKD5", _vizsgálat.adatok4.dátum_k5), Update<string>("VIMKS6", _vizsgálat.adatok4.címzett_k6), Update<string>("VIMKD6", _vizsgálat.adatok4.dátum_k6),
                Update<string>("VILABO", _vizsgálat.adatok4.laboros)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIHOSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #3 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                laborconnection.Close();

                return true;
            }
        }

        public bool Vizsgálat_Módosítás(Vizsgálat _eredeti, Vizsgálat _új)
        {
            lock (LaborLock)
            {
                string data;
                SqlCommand command;

                laborconnection.Open();

                // Adatok1

                data = V(new string[] {Update<string>("VITENE", _új.adatok1.terméknév), Update<byte>("VIHOKE", _új.adatok1.hőkezelés),
                Update<string>("VIGYEV", _új.adatok1.gyártási_év[_új.adatok1.gyártási_év.Length - 1].ToString()), Update<string>("VIMUJE", _új.adatok1.műszak_jele),
                Update<string>("VITOGE", _új.adatok1.töltőgép), Update<string>("VISZOR", _új.adatok1.szárm_ország), Update<string>("VIFAJT", _új.adatok1.gyümölcsfajta)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _eredeti.azonosító.termékkód), Update<string>("VIHOSZ", _eredeti.azonosító.hordószám), Update<string>("VISARZ", _eredeti.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #1 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok2

                data = V(new string[] {Update<double?>("VIBRIX", _új.adatok2.brix), Update<double?>("VICSAV", _új.adatok2.citromsav),
                Update<double?>("VIBOSA", _új.adatok2.borkősav), Update<double?>("VIPEHA", _új.adatok2.ph), Update<double?>("VIBOST", _új.adatok2.bostwick),
                Update<int?>("VIASAV", _új.adatok2.aszkorbinsav), Update<int?>("VICIAD", _új.adatok2.citromsav_adagolás),  Update<int?>("VIMATO", _új.adatok2.magtöret),
                Update<int?>("VIFEFE", _új.adatok2.feketepont),  Update<int?>("VIFEBA", _új.adatok2.barnapont),  Update<string>("VISZIN", _új.adatok2.szín),
                Update<string>("VIIZEK", _új.adatok2.íz), Update<string>("VIILLA", _új.adatok2.illat)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _eredeti.azonosító.termékkód), Update<string>("VIHOSZ", _eredeti.azonosító.hordószám), Update<string>("VISARZ", _eredeti.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #2 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok3

                data = V(new string[] {Update<string>("VILEOL", _új.adatok3.leoltás), Update<string>("VIERDA", _új.adatok3.értékelés),
                Update<int?>("VIOCS1", _új.adatok3.összcsíra_1), Update<int?>("VIOCS2", _új.adatok3.összcsíra_2), Update<int?>("VIPEN1", _új.adatok3.penész_1),
                Update<int?>("VIPEN2", _új.adatok3.penész_2), Update<int?>("VIELE1", _új.adatok3.élesztő_1),  Update<int?>("VIELE2", _új.adatok3.élesztő_2),
                Update<string>("VIEMEJE", _új.adatok3.megjegyzés)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _eredeti.azonosító.termékkód), Update<string>("VIHOSZ", _eredeti.azonosító.hordószám), Update<string>("VISARZ", _eredeti.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #3 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok4

                data = V(new string[] { Update<string>("VIMTS1", _új.adatok4.címzett_t), Update<string>("VIMTD1", _új.adatok4.dátum_t),
                Update<string>("VIMKS1", _új.adatok4.címzett_k1), Update<string>("VIMKD1", _új.adatok4.dátum_k1), Update<string>("VIMKS2", _új.adatok4.címzett_k2), Update<string>("VIMKD2", _új.adatok4.dátum_k2),
                Update<string>("VIMKS3", _új.adatok4.címzett_k3), Update<string>("VIMKD3", _új.adatok4.dátum_k3), Update<string>("VIMKS4", _új.adatok4.címzett_k4), Update<string>("VIMKD4", _új.adatok4.dátum_k4),
                Update<string>("VIMKS5", _új.adatok4.címzett_k5), Update<string>("VIMKD5", _új.adatok4.dátum_k5), Update<string>("VIMKS6", _új.adatok4.címzett_k6), Update<string>("VIMKD6", _új.adatok4.dátum_k6),
                Update<string>("VILABO", _új.adatok4.laboros)});

                if (data != null)
                {
                    string where = A(new string[] { Update<string>("VITEKO", _eredeti.azonosító.termékkód), Update<string>("VIHOSZ", _eredeti.azonosító.hordószám), Update<string>("VISARZ", _eredeti.azonosító.sarzs) });

                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #3 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                laborconnection.Close();

                return true;
            }
        }

        public bool Vizsgálat_Törlés(Vizsgálat.Azonosító _azonosító)
        {
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "DELETE FROM L_VIZSLAP WHERE VITEKO = '" + _azonosító.termékkód + "' AND VIHOSZ= '" + _azonosító.hordószám + "' AND VISARZ= '" + _azonosító.sarzs + "' AND VIMSSZ= '" + _azonosító.sorszám + "';";
                command.ExecuteNonQuery();
                command.Dispose();
                laborconnection.Close();
                return true;
            }
        }

        #endregion

        #region Foglalások
        public List<Foglalás> Foglalás_Azonosítók()
        {
            lock (LaborLock)
            {
                List<Foglalás> data = new List<Foglalás>();
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT FOSZAM,FONEVE,FOFOHO,FOTIPU,FOFENE,FODATE FROM L_FOGLAL";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int c = 0;
                    Foglalás temp_foglalás = new Foglalás(reader.GetInt32(c), reader.GetString(++c), reader.GetByte(++c), reader.GetString(++c), reader.GetString(++c), reader.GetString(++c));
                    data.Add(temp_foglalás);
                }
                command.Dispose();
                laborconnection.Close();
                return data;
            }
        }

        public Vizsgalap_Szűrő Foglalás_Vizsgalap_Szűrő(Foglalás _foglalás)
        {
            lock(LaborLock)
            {
            Vizsgalap_Szűrő data = new Vizsgalap_Szűrő();

            laborconnection.Open();

            SqlCommand command = laborconnection.CreateCommand();
            command.CommandText = "SELECT FOFAJT,FOHOTI,FOMEGR,FOSZOR, FOMUJE,FOTOGE,FODATE,FOTIPU,FOTEKO,FOCSAVT,FOCSAVI,FOSARZT,FOSARZI,FOZSSZT,FOZSSZI,FOBRIXT,FOBRIXI,FOBOSAI,FOBOSAT,FOPEHAT,FOPEHAI,FOBOSTT,FOBOSTI,FOASAVT,FOASAVI,FONETOT,FONETOI,FOHOFOT,FOHOFOI,FOSZATI,FOSZATT,FOCIADT,FOCIADI FROM L_FOGLAL";


            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int c = 0;
                data.adatok1 = new Vizsgalap_Szűrő.Adatok1(
                    
                    GetNullableString(reader, c),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++),
                    GetNullableString(reader, c++));

                data.adatok2 = new Vizsgalap_Szűrő.Adatok2(
                    Program.mainform.ConvertOrDie<int>(reader.GetString(c++)),
                    Program.mainform.ConvertOrDie<int>(reader.GetString(c++)),
                    Program.mainform.ConvertOrDie<int>(reader.GetString(c++)),
                    Program.mainform.ConvertOrDie<int>(reader.GetString(c++)),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                    (double)reader.GetDecimal(c++),
                /*
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                    Program.mainform.ConvertOrDie<double>(reader.GetDecimal(c++).ToString()),
                  */
                 Program.mainform.ConvertOrDie<short>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<short>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<short>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<short>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<byte>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<byte>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<byte>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<byte>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<byte>(reader.GetInt16(c++).ToString()),
                    Program.mainform.ConvertOrDie<byte>(reader.GetInt16(c++).ToString()));

                /*
                    //(GetNullableString(reader, c++)),
                       0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                    0);
                    (int?)GetNullable<Int32>(reader, c++),
                    (int?)GetNullable<Int32>(reader, c++),
                    (int?)GetNullable<Int32>(reader, c++),
                    
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),
                    (double?)GetNullable<decimal>(reader,c++),

                    (short?)GetNullable<Int16>(reader, c++),
                    (short?)GetNullable<Int16>(reader, c++),
                    (short?)GetNullable<Int32>(reader, c++),
                    (short?)GetNullable<Int32>(reader, c++),
                    (byte?)GetNullable<Int16>(reader, c++),
                    (byte?)GetNullable<Int16>(reader, c++),
                    (byte?)GetNullable<Int16>(reader, c++),
                    (byte?)GetNullable<Int16>(reader, c++),
                    (byte?)GetNullable<Int16>(reader, c++),
                    (byte?)GetNullable<Int16>(reader, c++));
                 */
            };

            command.Dispose();
            laborconnection.Close();
            return data;
            }
        }

        public bool Foglalás_Hozzáadás(Foglalás _foglalás)
        {
            lock (LaborLock)
            {
                SqlCommand command;

                laborconnection.Open();

                command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_FOGLAL (FOSZAM,FONEVE,FOFOHO,FOTIPU,FOFENE,FODATE,FOFAJT)" +
                                    " VALUES('" + _foglalás.id + "', '" + _foglalás.név + "', " + _foglalás.hordók_száma + ", '" + _foglalás.típus + "', '"
                                    + _foglalás.készítő + "', '" + _foglalás.idő + "', '" + _foglalás.típus + "')";

                try { command.ExecuteNonQuery(); }
                catch (Exception e) { MessageBox.Show(e.Message); return false; }
                finally { command.Dispose(); laborconnection.Close(); }

                laborconnection.Close();
                Program.mainform.RefreshData();
                return true;
            }
        }

        public bool Foglalás_Módosítás(Foglalás _eredeti, Foglalás _új)
        {
            lock (LaborLock)
            {
                return true;
            }
        }

        public bool Foglalás_ÚjVizsgalap(Foglalás _azonosító, Vizsgalap_Szűrő _szűrő)
        {
            lock (LaborLock)
            {
                string data;
                SqlCommand command;
                laborconnection.Open();
                command = laborconnection.CreateCommand();

                string where = A(new string[] { Update<int>("FOSZAM", _azonosító.id), Update<string>("FONEVE", _azonosító.név), Update<int>("FOFOHO", _azonosító.hordók_száma),
                     Update<string>("FOTIPU", _azonosító.típus) ,Update<string>("FOFENE", _azonosító.készítő) ,Update<string>("FODATE", _azonosító.idő),Update<string>("FOFAJT", _azonosító.típus)  });

                // public Adatok1(string _gyümölcsfajta, string _hordótípus, string _megrendelő, string _származási_ország, string _műszak_jele, string _töltőgép_száma, string _foglalás_ideje, string _foglalás_típusa, string _termékkód)
                data = V(new string[] {Update<string>("FOFAJT", _szűrő.adatok1.foglalás_típusa), Update<string>("FOHOTI", _szűrő.adatok1.hordótípus), Update<string>("FOMEGR", _szűrő.adatok1.megrendelő),
                Update<string>("FOSZOR", _szűrő.adatok1.származási_ország), Update<string>("FOMUJE", _szűrő.adatok1.műszak_jele), Update<string>("FOTOGE", _szűrő.adatok1.töltőgép_száma),
                Update<string>("FODATE", _szűrő.adatok1.foglalás_ideje),Update<string>("FOTIPU", _szűrő.adatok1.foglalás_típusa),Update<string>("FOTEKO", _szűrő.adatok1.termékkód)});

                if (data != null)
                {
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_FOGLAL SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException) { MessageBox.Show("Vizsgalap_Hozzáad -> adat1 hiba"); }
                }

                // Adatok2

                data = V(new string[] {
                Update<int?>("FOSARZT", _szűrő.adatok2.min_sarzs), Update<int?>("FOSARZI", _szűrő.adatok2.max_sarzs),
                Update<int?>("FOZSSZT", _szűrő.adatok2.min_hordószám), Update<int?>("FOZSSZI", _szűrő.adatok2.max_hordószám),
                Update<double?>("FOBRIXT", _szűrő.adatok2.min_brix), Update<double?>("FOBRIXI", _szűrő.adatok2.max_brix),
                Update<double?>("FOCSAVT", _szűrő.adatok2.min_citromsav), Update<double?>("FOCSAVI", _szűrő.adatok2.max_citromsav),
                Update<double?>("FOBOSAT", _szűrő.adatok2.min_borkősav), Update<double?>("FOBOSAI", _szűrő.adatok2.max_borkősav),
                Update<double?>("FOPEHAT", _szűrő.adatok2.min_ph), Update<double?>("FOPEHAI", _szűrő.adatok2.max_ph),
                Update<double?>("FOBOSTT", _szűrő.adatok2.min_bostwick), Update<double?>("FOBOSTI", _szűrő.adatok2.max_bostwick),
                Update<int?>("FOASAVT", _szűrő.adatok2.min_aszkorbinsav), Update<int?>("FOASAVI", _szűrő.adatok2.max_aszkorbinsav),
                Update<int?>("FONETOT", _szűrő.adatok2.min_nettó_töltet), Update<int?>("FONETOI", _szűrő.adatok2.max_nettó_töltet),
                Update<int?>("FOHOFOT", _szűrő.adatok2.min_hőkezelés), Update<int?>("FOHOFOI", _szűrő.adatok2.max_hőkezelés),
                Update<int?>("FOSZATT", _szűrő.adatok2.min_szita_átmérő), Update<int?>("FOSZATI", _szűrő.adatok2.max_szita_átmérő),
                Update<int?>("FOCIADT", _szűrő.adatok2.min_citromsav_ad), Update<int?>("FOCIADI", _szűrő.adatok2.max_citromsav_ad)});

                if (data != null)
                {
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_FOGLAL SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgalap_Hozzáad -> adat1 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                laborconnection.Close();
                return true;
            }
        }


        public bool Foglalás_Törlés(Foglalás _azonosító)
        {
            lock (LaborLock)
            {
                bool found = true;
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                string where = A(new string[] { Update<int>("FOSZAM", _azonosító.id), Update<string>("FONEVE", _azonosító.név), Update<int>("FOFOHO", _azonosító.hordók_száma),
                     Update<string>("FOTIPU", _azonosító.típus) ,Update<string>("FOFENE", _azonosító.készítő) ,Update<string>("FODATE", _azonosító.idő),Update<string>("FOFAJT", _azonosító.típus)  });

                command.CommandText = "DELETE FROM L_FOGLAL WHERE " + where;
                if (command.ExecuteNonQuery() == 0) found = false;
                command.Dispose();
                laborconnection.Close();

                Program.mainform.RefreshData();
                return found;
            }
        }

        public List<Hordó> Foglalás_Hordók(Foglalás _foglalás)
        {
            lock (LaborLock)
            {
                List<Hordó> value = new List<Hordó>();
                return value;
            }
        }

        public List<Sarzs> Sarzsok(Vizsgalap_Szűrő _szűrő)
        {
            lock (LaborLock)
            {
                List<Sarzs> value = new List<Sarzs>();
                return value;
            }
        }

        public List<Hordó> Hordók(Vizsgalap_Szűrő _szűrő, Sarzs _sarzs)
        {
            lock (LaborLock)
            {
                List<Hordó> value = new List<Hordó>();
                return value;
            }
        }

        #endregion
        
        #region Keresés


        #endregion
    }
}
