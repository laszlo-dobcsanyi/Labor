using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            string MarillenConnectionString = @"Data Source=" + Settings.server + ";Initial Catalog=" + Settings.marillen_database + ";User ID=" +
                Settings.sql_username + ";Password=" + Settings.sql_password + ";Integrated Security=true;";
            
            marillenconnection = new SqlConnection(MarillenConnectionString);

            string LaborConnectionString = @"Data Source=" + Settings.server + ";Initial Catalog=" +
                Settings.labor_database + ";User ID=" + Settings.sql_username + ";Password=" + Settings.sql_password + ";Integrated Security=true;";

            laborconnection = new SqlConnection(LaborConnectionString);

            try
            {
                laborconnection.Open();
                laborconnection.Close();
            }
            catch
            {
                SqlConnection create_connection = new SqlConnection("Server=" + Settings.server + ";Integrated Security=true;");

                try
                {
                    create_connection.Open();
                    SqlCommand command = new SqlCommand("CREATE DATABASE " + Settings.labor_database, create_connection);
                    command.ExecuteNonQuery();
                    command.Dispose();

                    create_connection.Close();
                }
                catch
                {
                    MessageBox.Show("Hiba a csatlakozás során! Ellenőrizze az adatbázis elérést, felhasználó jogosultságát!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }

                //try
                {
                    laborconnection = new SqlConnection("Server=" + Settings.server + ";Database=" + Settings.labor_database + ";Integrated Security=true");
                    AdminForm adminform = new AdminForm();
                    adminform.ShowDialog();

                    laborconnection.Open();
                    SqlCommand command = new SqlCommand(
                            "CREATE TABLE L_TORZSA (TOTIPU varchar(20) NOT NULL,TOAZON varchar(25) PRIMARY KEY,TOSZO2 varchar(25),TOSZO3 varchar(25));" +

                            // Vizsgálat
                            "CREATE TABLE L_VIZSLAP (VITEKO varchar(3) NOT NULL, VISARZ varchar(3) NOT NULL, VIHOSZ varchar(4) NOT NULL, VIHOTI varchar(25), VINETO DECIMAL(14, 2), VISZAT varchar(100), VIMEGR varchar(50), " +
                                "VIMSSZ int, " +

                                // Adatok1
                                "VITENE varchar(50), VIHOKE smallint, VIGYEV varchar(1), VIMUJE varchar(1), VITOGE varchar(1),  VISZOR varchar(15), VIFAJT varchar(50), " +

                                // Adatok2
                                "VIBRIX DECIMAL(3,1), VICSAV DECIMAL(4,2), VIBOSA DECIMAL(4,2), VIPEHA DECIMAL(4,2), VIBOST DECIMAL(3,1) ,VIASAV smallint, VICIAD smallint, VIMATO smallint, VIFEFE smallint, VIFEBA smallint, " +
                                "VISZIN varchar(60), VIIZEK varchar(60), VIILLA varchar(60), " +

                                // Adatok3
                                "VILEOL varchar(11), VIERDA varchar(11), VIOCS1 smallint, VIOCS2 smallint, VIELE1 smallint, VIELE2 smallint, VIPEN1 smallint, VIPEN2 smallint, VIEMEJE varchar(300), " +

                                // Adatok4
                                "VIMTS1 varchar(15),VIMTD1 varchar(8),VIMKS1 varchar(15),VIMKD1 varchar(8), VIMKS2 varchar(15), VIMKD2 varchar(8), VIMKS3 varchar(15), VIMKD3 varchar(8), VIMKS4 varchar(15), VIMKD4 varchar(8), " +
                                "VIMKS5 varchar(15), VIMKD5 varchar(8), VIMKS6 varchar(15), VIMKD6 varchar(8),VILABO varchar(15));" +

                            "CREATE TABLE L_HORDO(HOTEKO varchar(10), HOSARZ varchar(10), HOSZAM varchar(10), FOSZAM int, VIGYEV varchar(10), HOQTY decimal(14, 2), HOTIME char(30));" +

                            "CREATE TABLE L_FOGLAL (FONEVE varchar(30), FOSZAM int IDENTITY(1,1), FODATE varchar(20), FOTIPU varchar(10), FOFENE varchar(30), FOTEKO varchar(3), FOSARZT varchar(3), FOSARZI varchar(3), FOHOSZT varchar(4)," +
                                "FOHOSZI varchar(4), FOBRIXT DECIMAL(4,2), FOBRIXI DECIMAL(4,2), FOCSAVT DECIMAL(4,2), FOCSAVI DECIMAL(4,2), FOPEHAT DECIMAL(4,2), FOPEHAI DECIMAL(4,2), FOBOSTT DECIMAL(4,2)," +
                                "FOBOSTI DECIMAL(4,2), FOASAVT smallint, FOASAVI smallint, FONETOT smallint, FONETOI smallint, FOHOFOT smallint, FOHOFOI smallint, FOCIADT smallint, FOCIADI smallint," +
                                "FOFAJT varchar(15), FOHOTI varchar(15), FOMEGR varchar(15), FOSZOR varchar(15), FOMUJE varchar(1), FOTOGE varchar(1), FOFOHO smallint, FOSZSZ smallint ," +
                                "FOSZATI varchar(6) , FOSZATT varchar(6), FOBOSAI smallint, FOBOSAT smallint, SZSZAM smallint);" +

                            "CREATE TABLE L_FELHASZ (FEFEN1 varchar(30), FEFEN2 varchar(30), FEBEO1 varchar(30), FEBEO2 varchar(30), FEBEKO varchar(15) PRIMARY KEY, FEJELS varchar(15), " +
                                "FETOHO varchar(1), FETORO varchar(1), FETOTO varchar(1),   FEVIHO varchar(1), FEVIRO varchar(1), FEVITO varchar(1), " +
                                "FEFOKE varchar(1), FEFOFE varchar(1), FEFOTO varchar(1),   FEKONY varchar(1),   FEKITO varchar(1), " +
                                "FEFEHO varchar(1), FEFERO varchar(1), FEFETO varchar(1));" +

                            "CREATE TABLE L_SZLEV (SZSZAM int IDENTITY(1,1), SZSZSZ varchar(50), SZFENE varchar(50), SZDATE varchar(10), SZNYEL varchar(1), SZVEVO varchar(100), SZGKR1 varchar(7), SZGKR2 varchar(7)," +
                                "FOFOHO smallint, SZGYEV varchar(4), SZSZIN varchar(60), SZIZEK varchar(60), SZILLA varchar(60));" +

                            "CREATE TABLE L_GYFAJTA(GFTEKO varchar(2), GFAZON varchar(30), GFSZO2 varchar(30), GFSZO3 varchar(30));" +

                            "CREATE TABLE L_TAPERTEK(TATEKO varchar(2), TAKIO smallint, TAKCAL smallint, TAFEHE DECIMAL(2, 1), TASZHI DECIMAL(3, 1), TAZSIR DECIMAL(2, 1), TAELRO DECIMAL(2, 1));" +

                            "CREATE TABLE L_MINBIZ(MISZ1M varchar(600), MISZ1A varchar(600), MISZ2M varchar(600), MISZ2A varchar(600));" +
                            
                            HardcodedData(adminform.Data) , laborconnection);


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

        #region Segédfüggvények
        public static bool IsCorrectSQLText(string _text)
        {
            if (_text.Contains("'") || _text.Contains("\"") || _text.Contains("(") || _text.Contains(")")) return false;
            return true;
        }

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

        public static string V_Apostrophe(string[] _texts)
        {
            string value = null;
            int length = 0;
            for (int current = 0; current < _texts.Length; ++current)
                if (_texts[current] != null)
                {
                    value += "'" + _texts[current] + "', ";
                    length++;
                }
            if (length == 0) return null;
            return value.Substring(0, value.Length - 2);
        }

        public static string Update<T>(string _column_name, T _value)
        {
            Type type = typeof(T);
            if (type == typeof(char)) { return _column_name + " = '" + _value + "'"; }
            if (type == typeof(string)) { if (_value != null) return _column_name + " = '" + _value + "'"; else return _column_name + " = NULL"; }

            if (type == typeof(Int16)) { return _column_name + " = " + _value; }
            if (type == typeof(Int16?)) { if (_value != null) return _column_name + " = " + _value; else return _column_name + " = NULL"; }

            if (type == typeof(short)) { return _column_name + " = " + _value; }
            if (type == typeof(short?)) { if (_value != null) return _column_name + " = " + _value; else return _column_name + " = NULL"; }

            if (type == typeof(int)) { return _column_name + " = " + _value; }
            if (type == typeof(int?)) { if (_value != null) return _column_name + " = " + _value; else return _column_name + " = NULL"; }

            if (type == typeof(double)) { return _column_name + " = " + _value.ToString().Replace(',', '.'); }
            if (type == typeof(double?)) { if (_value != null) return _column_name + " = " + _value.ToString().Replace(',', '.'); else return _column_name + " = NULL"; }

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

        public static bool VB(string _value)
        {
            return (_value == "I" ? true : false);
        }

        public static string BV(bool _value)
        {
            return (_value ? "I" : "H");
        }

        public static string Is(string _filter, string _value_field)
        {
            if (_filter != null) return _value_field + " = " + "'" + _filter + "'";
            return null;
        }

        public static string Between<T>(MinMaxPair<T?> _filter, string _value_field) where T : struct
        {
            if (typeof(T) != typeof(double))
            {
                string value = _value_field + " IS NOT NULL AND ";
                if ((_filter.min != null) && (_filter.max == null)) return value + _filter.min.Value + " <= " + _value_field;
                if ((_filter.min == null) && (_filter.max != null)) return value + _value_field + " <= " + _filter.max.Value;
                if ((_filter.min != null) && (_filter.max != null)) return value + _value_field + " BETWEEN " + _filter.min.Value + " AND " + _filter.max.Value;
                return null;
            }
            else
            {
                string value = _value_field + " IS NOT NULL AND ";
                if ((_filter.min != null) && (_filter.max == null)) return value + _filter.min.Value.ToString().Replace(',', '.') + " <= " + _value_field;
                if ((_filter.min == null) && (_filter.max != null)) return value + _value_field + " <= " + _filter.max.Value.ToString().Replace(',', '.');
                if ((_filter.min != null) && (_filter.max != null)) return value + _value_field + " BETWEEN " + _filter.min.ToString().Replace(',', '.') + " AND " + _filter.max.Value.ToString().Replace(',', '.');
                return null;
            }
        }

        public static string BetweenString(MinMaxPair<string> _filter, string _value_field)
        {
            string value = _value_field + " IS NOT NULL AND ";
            if ((_filter.min != null) && (_filter.max == null)) return value + "'" + _filter.min + "' <= " + _value_field;
            if ((_filter.min == null) && (_filter.max != null)) return value + _value_field + " <= '" + _filter.max + "'";
            if ((_filter.min != null) && (_filter.max != null)) return value + _value_field + " BETWEEN '" + _filter.min + "' AND '" + _filter.max + "'";
            return null;
        }
        #endregion

        #region Marillen Adatbázisából
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
        /// nettó_töltet, műszak_jele, töltőgép_száma, sarzs, szita_átmérő
        /// </summary>
        public List<string> Vizsgálat_Fejlécadatok(string _prod_id)
        {
            List<string> values = new List<string>();

            lock (MarillenLock)
            {
                int iteration;
                int szita_átmérő_száma = -1;
                string serial = null;
                string prodid = null;

                marillenconnection.Open();

                // nettó_töltet
                SqlCommand command = new SqlCommand("SELECT serial_nr, prod_id, qty FROM tetelek WHERE (type=300) AND (prod_id LIKE '" + _prod_id + "') AND (qty > 0) ORDER BY serial_nr");
                command.Connection = marillenconnection;
                SqlDataReader reader = command.ExecuteReader();
                iteration = 0;
                while (reader.Read())
                {
                    serial = reader.GetString(0);
                    prodid = reader.GetString(1);
                    string netto = (reader.GetValue(2)).ToString();

                    if (iteration == 0)
                    {
                        //szita_átmérő_száma
                        szita_átmérő_száma = Convert.ToInt32(prodid[7].ToString());
                        values.Add(netto);
                    }

                    ++iteration;
                }
                reader.Close();

                // műszak_jele
                command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr = '" + serial + "') AND (code=1)");
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                iteration = 0;
                while (reader.Read())
                {
                    string jel = reader.GetString(0).Substring(0, 1);
                    if (iteration == 0)
                    {
                        values.Add(jel);
                    }

                    ++iteration;
                }
                reader.Close();

                // töltőgép_száma
                command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr = '" + serial + "') AND (code=2)");
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                iteration = 0;
                while (reader.Read())
                {
                    string szám = reader.GetString(0);
                    if (iteration == 0)
                    {
                        values.Add(szám);
                    }

                    ++iteration;
                }
                reader.Close();

                // sarzs
                command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr = '" + serial + "') AND (code=3)");
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                iteration = 0;
                while (reader.Read())
                {
                    string sarzs = reader.GetString(0);
                    if (iteration == 0)
                    {
                        values.Add(sarzs);
                    }
                    ++iteration;
                }
                reader.Close();

                //szita_átmérő
                command = new SqlCommand("SELECT name FROM darabossag WHERE code = " + szita_átmérő_száma);
                command.Connection = marillenconnection;
                reader = command.ExecuteReader();
                iteration = 0;
                while (reader.Read())
                {
                    string szita_átmérő = reader.GetString(0);
                    if (iteration == 0)
                    {
                        values.Add(szita_átmérő);
                    }

                    ++iteration;
                }
                reader.Close();

                marillenconnection.Close();
            }

            return values;
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
                SqlCommand command = new SqlCommand("SELECT cikkek.name FROM cikkek WHERE (item_nr ='12" + _termékkód.Substring(2, 2) + "01') ORDER BY item_nr");
                command.Connection = marillenconnection;
                SqlDataReader reader = command.ExecuteReader();
                int iteration = 0;
                while (reader.Read())
                {
                    string terméknév = reader.GetString(0);
                    if (iteration == 0) value = terméknév;
                    ++iteration;
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

                string where = A(new string[] { Update<string>("VITEKO", _azonosító.termékkód), Update<string>("VIHOSZ", _azonosító.hordószám), Update<string>("VISARZ", _azonosító.sarzs) });

                // Azonosító
                Vizsgálat.Azonosító? azonosító = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VITEKO, VISARZ, VIHOSZ, VIHOTI, VINETO, VISZAT, VIMEGR, VIMSSZ FROM L_VIZSLAP WHERE " + where;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        azonosító = new Vizsgálat.Azonosító(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), (double)reader.GetDecimal(4), reader.GetString(5),
                            reader.GetString(6), reader.GetInt32(7));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Azonosító lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok1
                Vizsgálat.Adatok1? adatok1 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VITENE, VIHOKE, VIGYEV, VIMUJE, VITOGE, VISZOR, VIFAJT FROM L_VIZSLAP WHERE " + where;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok1 = new Vizsgálat.Adatok1(reader.GetString(0), reader.GetInt16(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok1 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok2
                Vizsgálat.Adatok2? adatok2 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VIBRIX, VICSAV, VIBOSA, VIPEHA, VIBOST, VIASAV, VICIAD, VIMATO, VIFEFE, VIFEBA, VISZIN, VIIZEK, VIILLA FROM L_VIZSLAP WHERE " + where;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok2 = new Vizsgálat.Adatok2((double?)GetNullable<decimal>(reader, 0), (double?)GetNullable<decimal>(reader, 1), (double?)GetNullable<decimal>(reader, 2),
                            (double?)GetNullable<decimal>(reader, 3), (double?)GetNullable<decimal>(reader, 4), GetNullable<Int16>(reader, 5), GetNullable<Int16>(reader, 6), GetNullable<Int16>(reader, 7),
                            GetNullable<Int16>(reader, 8), GetNullable<Int16>(reader, 9), GetNullableString(reader, 10), GetNullableString(reader, 11), GetNullableString(reader, 12));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok2 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok3
                Vizsgálat.Adatok3? adatok3 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VILEOL, VIERDA, VIOCS1, VIOCS2, VIELE1, VIELE2, VIPEN1, VIPEN2, VIEMEJE FROM L_VIZSLAP WHERE " + where;
                try
                {
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        adatok3 = new Vizsgálat.Adatok3(GetNullableString(reader, 0), GetNullableString(reader, 1), GetNullable<Int16>(reader, 2), GetNullable<Int16>(reader, 3), GetNullable<Int16>(reader, 4),
                                    GetNullable<Int16>(reader, 5), GetNullable<Int16>(reader, 6), GetNullable<Int16>(reader, 7), GetNullableString(reader, 8));
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Vizsgálat -> Adatok1 lekérdezés hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open || azonosító == null) return null;

                // Adatok4
                Vizsgálat.Adatok4? adatok4 = null;
                command = laborconnection.CreateCommand();
                command.CommandText = "SELECT VIMTS1, VIMTD1, VIMKS1, VIMKD1, VIMKS2, VIMKD2, VIMKS3, VIMKD3, VIMKS4, VIMKD4, VIMKS5, VIMKD5, VIMKS6, VIMKD6, VILABO FROM L_VIZSLAP WHERE " + where;
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
                    data.Add(new Vizsgálat.Azonosító(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), (double)reader.GetDecimal(4), reader.GetString(5),
                        reader.GetString(6), reader.GetInt32(7)));
                }
                command.Dispose();
                laborconnection.Close();

                return data;
            }
        }

        /// <summary>
        /// Egy Termékkód-Hordószám-Gyártási év hármashoz csak egy Hordótípus tartozhat.
        /// Ez a függvény ellenőrzi, hogy az adott Hordószámon kívül van-e ilyen hordó, és lekérdezi annak típusát.
        /// </summary>
        public string Vizsgálat_Hordótípus_Ellenőrzés(string _termékkód, string _hordószám, string _gyártási_év, string _sarzs)
        {
            string hordó_típus = null;

            lock (LaborLock)
            {
                laborconnection.Open();

                string where = A(new string[] { Update<string>("VITEKO", _termékkód), Update<string>("VIGYEV", _gyártási_év), Update<string>("VISARZ", _sarzs) });

                SqlCommand command = new SqlCommand("SELECT VIHOTI FROM L_VIZSLAP WHERE " + where + " AND VIHOSZ <> '" + _hordószám + "';");
                command.Connection = laborconnection;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    hordó_típus = reader.GetString(0);
                }

                command.Dispose();
                laborconnection.Close();
            }

            return hordó_típus;
        }

        public bool Vizsgálat_Hozzáadás(Vizsgálat _vizsgálat)
        {
            lock (LaborLock)
            {
                string data;
                SqlCommand command;
                string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIHOSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) });

                laborconnection.Open();

                // Azonosító
                command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_VIZSLAP (VITEKO, VISARZ, VIHOSZ, VIHOTI, VINETO, VISZAT, VIMEGR, VIMSSZ)" +
                                    " VALUES('" + _vizsgálat.azonosító.termékkód + "', '" + _vizsgálat.azonosító.sarzs + "', '" + _vizsgálat.azonosító.hordószám + "', '" + _vizsgálat.azonosító.hordótípus + "', " +
                                    _vizsgálat.azonosító.nettó_töltet.ToString().Replace(',', '.') + ", '" + _vizsgálat.azonosító.szita_átmérő + "', '" + _vizsgálat.azonosító.megrendelő + "', " +
                                    "(SELECT COUNT(*) FROM L_VIZSLAP WHERE " + A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) }) + ") + 1)";

                try { command.ExecuteNonQuery(); command.Dispose(); }
                catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> INSERT hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                // Adatok1
                data = V(new string[] {Update<string>("VITENE", _vizsgálat.adatok1.terméknév), Update<int>("VIHOKE", _vizsgálat.adatok1.hőkezelés), Update<char>("VIGYEV", _vizsgálat.adatok1.gyártási_év[_vizsgálat.adatok1.gyártási_év.Length - 1]),
                Update<string>("VIMUJE", _vizsgálat.adatok1.műszak_jele), Update<string>("VITOGE", _vizsgálat.adatok1.töltőgép), Update<string>("VISZOR", _vizsgálat.adatok1.szárm_ország),
                Update<string>("VIFAJT", _vizsgálat.adatok1.gyümölcsfajta)});

                if (data != null)
                {
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
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #3 hiba:\n" + q.Message); }
                }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                laborconnection.Close();
            }

            return Hordók_Másolás(_vizsgálat);
        }

        public bool Vizsgálat_Módosítás(Vizsgálat _eredeti, Vizsgálat _új)
        {
            lock (LaborLock)
            {
                string data;
                string where = A(new string[] { Update<string>("VITEKO", _eredeti.azonosító.termékkód), Update<string>("VIHOSZ", _eredeti.azonosító.hordószám), Update<string>("VISARZ", _eredeti.azonosító.sarzs) });
                SqlCommand command;

                laborconnection.Open();

                // Azonosító

                command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_VIZSLAP SET VIHOTI = '" + _új.azonosító.hordótípus + "', VIMEGR = '" + _új.azonosító.megrendelő+ "' WHERE " + where;

                try { command.ExecuteNonQuery(); command.Dispose(); }
                catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #1 hiba:\n" + q.Message); }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;
                
                // Adatok1

                data = V(new string[] {Update<string>("VITENE", _új.adatok1.terméknév), Update<int>("VIHOKE", _új.adatok1.hőkezelés),
                Update<string>("VIGYEV", _új.adatok1.gyártási_év[_új.adatok1.gyártási_év.Length - 1].ToString()), Update<string>("VIMUJE", _új.adatok1.műszak_jele),
                Update<string>("VITOGE", _új.adatok1.töltőgép), Update<string>("VISZOR", _új.adatok1.szárm_ország), Update<string>("VIFAJT", _új.adatok1.gyümölcsfajta)});

                if (data != null)
                {
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

        public bool Vizsgálat_Törlés(Vizsgálat _vizsgálat)
        {
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "DELETE FROM L_VIZSLAP WHERE VITEKO = '" + _vizsgálat.azonosító.termékkód + "' AND VIHOSZ= '" + _vizsgálat.azonosító.hordószám + "'" +
                    " AND VISARZ= '" + _vizsgálat.azonosító.sarzs + "' AND VIMSSZ= '" + _vizsgálat.azonosító.sorszám + "';";
                command.ExecuteNonQuery();
                command.Dispose();
                laborconnection.Close();
            }

            return Hordók_Törlés(_vizsgálat);
        }
        #endregion

        #region Hordók
        public List<Hordó> Hordók(Sarzs _sarzs)
        {
            lock (LaborLock)
            {
                List<Hordó> value = new List<Hordó>();

                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT HOTEKO, HOSARZ, HOSZAM, VIGYEV, FOSZAM, HOQTY, HOTIME FROM L_HORDO WHERE HOTEKO = '" + _sarzs.termékkód + "' AND HOSARZ = '" + _sarzs.sarzs + "';";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(new Hordó(reader.GetString(0), reader.GetString(1), reader.GetString(2), GetNullable<int>(reader, 4), reader.GetString(3), reader.GetDecimal(5), reader.GetString(6)));
                }

                command.Dispose();
                laborconnection.Close();

                return value;
            }
        }
        
        public List<Hordó> Hordók(Foglalás _foglalás)
        {
            List<Hordó> value = new List<Hordó>();
            
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT HOTEKO, HOSARZ, HOSZAM, VIGYEV, HOQTY, HOTIME FROM L_HORDO WHERE FOSZAM = " + _foglalás.id;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(new Hordó(reader.GetString(0), reader.GetString(1), reader.GetString(2), _foglalás.id, reader.GetString(3),reader.GetDecimal(4),reader.GetString(5)));
                }

                command.Dispose();
                laborconnection.Close();
            }

            return value;
        }

        public List<Hordó> Hordók(Foglalás _foglalás, Sarzs _sarzs)
        {
            List<Hordó> value = new List<Hordó>();

            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT HOSZAM, FOSZAM, VIGYEV, HOQTY, HOTIME FROM L_HORDO WHERE (FOSZAM IS NULL OR FOSZAM = " + _foglalás.id + ") AND HOTEKO = '" + _sarzs.termékkód + "' AND HOSARZ = '" + _sarzs.sarzs + "';";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(new Hordó(_sarzs.termékkód, _sarzs.sarzs, reader.GetString(0), GetNullable<int>(reader, 1), reader.GetString(2),reader.GetDecimal(3),reader.GetString(4)));
                }

                command.Dispose();
                laborconnection.Close();
            }

            return value;
        }

        private struct Hordó_Adat
        {
            public string szám;
            public decimal tömeg;
            public string év;

            public Hordó_Adat(string _szám, decimal _tömeg, string _év)
            {
                szám = _szám;
                tömeg = _tömeg;
                év = _év;
            }
        };

        public bool Hordók_Másolás(Vizsgálat _vizsgálat)
        {
            string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs), Update<string>("VIGYEV", _vizsgálat.adatok1.gyártási_év.Substring(3, 1)) });

            lock (LaborLock)
            {
                int count = 0;

                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM L_VIZSLAP WHERE " + where;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                reader.Close();
                laborconnection.Close();

                if (count != 1) return true;
            }

            List<Hordó_Adat> hordó_adatok = new List<Hordó_Adat>();
            string iPROD_ID = "12" + _vizsgálat.azonosító.termékkód.ToString().Substring(0, 2) + "01" + _vizsgálat.adatok1.gyártási_év[_vizsgálat.adatok1.gyártási_év.Length - 1];

            lock (MarillenLock)
            {
                string előző = null;

                marillenconnection.Open();
                SqlCommand command = new SqlCommand("SELECT tetelek.prod_id, tetelek.qty, tetelek.time_ FROM tetelek" +
                                      " INNER JOIN folyoprops ON tetelek.serial_nr=folyoprops.serial_nr" +
                                      " WHERE left(tetelek.prod_id,7) = '" + iPROD_ID + "' AND folyoprops.code= '3' AND folyoprops.propstr = '" + _vizsgálat.azonosító.sarzs +"'  ORDER BY tetelek.prod_id;");
                command.Connection = marillenconnection;
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    előző = reader.GetString(0).Substring(reader.GetString(0).Length - 4);
                    hordó_adatok.Add(new Hordó_Adat(előző, reader.GetDecimal(1), reader.GetDateTime(2).ToString()));

                    while (reader.Read())
                    {
                        string szám = reader.GetString(0).Substring(reader.GetString(0).Length - 4);
                        if (szám != előző)
                        {
                            hordó_adatok.Add(new Hordó_Adat(szám, reader.GetDecimal(1), reader.GetDateTime(2).ToString()));
                        }

                        előző = szám;
                    }
                }

                reader.Close();
                marillenconnection.Close();
            }

            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command3 = laborconnection.CreateCommand();
                foreach (Hordó_Adat adat in hordó_adatok)
                {
                    command3.CommandText += "INSERT INTO L_HORDO (HOTEKO, HOSARZ, HOSZAM, VIGYEV, HOQTY, HOTIME) VALUES('" + _vizsgálat.azonosító.termékkód + "','" + _vizsgálat.azonosító.sarzs + "','" +
                        adat.szám + "','" + _vizsgálat.adatok1.gyártási_év + "', " + adat.tömeg.ToString("F2").Replace(',', '.') + ", '" + adat.év + "');";
                }
                command3.ExecuteNonQuery();
                command3.Dispose();
                laborconnection.Close();
            }

            return true;
        }
        
        public bool Hordók_Foglalás(bool _törlés, int _foglalás_id, string _termékkód, string _sarzs, string _hordó_szám)
        {
            lock (LaborLock)
            {
                SqlCommand command;

                laborconnection.Open();

                command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_FOGLAL SET FOFOHO = FOFOHO " + (_törlés ? "-1" : "+1") + " WHERE FOSZAM = " + _foglalás_id + ";" +

                    "UPDATE L_HORDO " + (_törlés ? "SET FOSZAM = NULL" : "SET FOSZAM = " + _foglalás_id) +
                    " WHERE " + (_törlés ? "FOSZAM = " + _foglalás_id : "FOSZAM IS NULL") + " AND HOTEKO = '" + _termékkód + "' AND HOSARZ = '" + _sarzs + "' AND HOSZAM = '" + _hordó_szám + "';";

                int modified = command.ExecuteNonQuery();

                command.Dispose();
                laborconnection.Close();

                if (modified != 2) return false;
                return true;
            }
        }

        /// <summary>
        /// Egy hordó lista összes elemét az adott foglaláshoz rendeli.
        /// </summary>
        /// <param name="_foglalás_id"></param>
        /// <param name="_adatok">Törlés? - Termékkód - Sarzs - Hordó_szám sorrendben kell a Tuple-ben lennie.</param>
        /// <returns>Lefoglalt hordók száma.</returns>
        public int Hordók_ListaFoglalás(int _foglalás_id, List< Tuple< bool, string, string, string > > _adatok)
        {
            int offset = 0;
            int modified = 0;

            lock (LaborLock)
            {
                SqlCommand command;

                laborconnection.Open();

                foreach (Tuple<bool, string, string, string> current in _adatok)
                {
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_HORDO " + (current.Item1 ? "SET FOSZAM = NULL" : "SET FOSZAM = " + _foglalás_id) +
                        " WHERE " + (current.Item1 ? "FOSZAM = " + _foglalás_id : "FOSZAM IS NULL") + " AND HOTEKO = '" + current.Item2 + "' AND HOSARZ = '" + current.Item3 + "' AND HOSZAM = '" + current.Item4 + "';";

                    if (command.ExecuteNonQuery() == 1) modified++;
                    offset += current.Item1 ? -1 : +1;
                    command.Dispose();
                }

                if (offset != 0)
                { 
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_FOGLAL SET FOFOHO = FOFOHO " + (0 < offset ? "+" + offset : offset.ToString()) + " WHERE FOSZAM = " + _foglalás_id + ";";
                    command.ExecuteNonQuery();
                    command.Dispose();
                }

                laborconnection.Close();
            }

            return modified;
        }

        public bool Hordók_Törlés(Vizsgálat _vizsgálat)
        {
            string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs), Update<string>("VIGYEV", _vizsgálat.adatok1.gyártási_év) });

            lock (LaborLock)
            {
                int count = 0;

                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT COUNT(*) FROM L_VIZSLAP WHERE " + where;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = reader.GetInt32(0);
                }
                reader.Close();

                if (count != 0)
                {
                    command.Dispose();
                    laborconnection.Close();
                    return true;
                }

                command = laborconnection.CreateCommand();
                command.CommandText = "DELETE FROM L_HORDO WHERE HOTEKO = '" + _vizsgálat.azonosító.termékkód  + "' AND HOSARZ= '" + _vizsgálat.azonosító.sarzs + "';";
                command.ExecuteNonQuery();
                command.Dispose();
                laborconnection.Close();
            }

            return true;
        }

        #endregion

        #region Foglalások
        public List<Foglalás> Foglalások()
        {
            lock (LaborLock)
            {
                List<Foglalás> data = new List<Foglalás>();
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT FOSZAM, FONEVE, FOFOHO, FOTIPU, FOFENE, FODATE FROM L_FOGLAL";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int c = -1;
                    Foglalás temp_foglalás = new Foglalás(reader.GetInt32(++c), reader.GetString(++c), reader.GetInt16(++c), reader.GetString(++c), reader.GetString(++c), reader.GetString(++c));
                    data.Add(temp_foglalás);
                }
                command.Dispose();
                laborconnection.Close();
                return data;
            }
        }

        public Vizsgalap_Szűrő? Foglalás_Vizsgálat_Szűrő(Foglalás _foglalás)
        {
            lock (LaborLock)
            {
                Vizsgalap_Szűrő data = new Vizsgalap_Szűrő();

                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT FOFAJT, FOHOTI, FOMEGR, FOSZOR, FOMUJE, FOTOGE, FOTEKO, " +
                    "FOSARZT, FOSARZI, FOHOSZT, FOHOSZI, FOBRIXT, FOBRIXI, FOCSAVT, FOCSAVI, FOBOSAT, FOBOSAI, FOPEHAT, FOPEHAI, FOBOSTT, FOBOSTI, FOASAVT, FOASAVI, FONETOT, FONETOI, FOHOFOT, FOHOFOI, " +
                    "FOCIADT, FOCIADI, FOSZATI, FOSZATT  FROM L_FOGLAL WHERE FOSZAM = " + _foglalás.id;

                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int c = -1;
                        data.adatok1 = new Vizsgalap_Szűrő.Adatok1(
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c));

                        data.adatok2 = new Vizsgalap_Szűrő.Adatok2(
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),
                            GetNullableString(reader, ++c),

                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),
                            (double?)GetNullable<decimal>(reader, ++c),

                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),

                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c),
                            GetNullable<Int16>(reader, ++c));
                    };
                }
                catch (SqlException q)
                {
                    MessageBox.Show("Foglalás_Vizsgalap_Szűrő hiba:\n" + q.Message);
                    command.Dispose();
                    laborconnection.Close();
                    return null;
                }

                command.Dispose();
                laborconnection.Close();

                return data;
            }
        }

        public bool Foglalás_Vizsgálat_Szűrő_Hozzáadás(Foglalás _azonosító, Vizsgalap_Szűrő _szűrő)
        {
            lock (LaborLock)
            {
                string data;
                SqlCommand command;
                laborconnection.Open();
                command = laborconnection.CreateCommand();

                // Adatok1
                data = V(new string[] {Update<string>("FOFAJT", _szűrő.adatok1.gyümölcsfajta), Update<string>("FOHOTI", _szűrő.adatok1.hordótípus), Update<string>("FOMEGR", _szűrő.adatok1.megrendelő),
                Update<string>("FOSZOR", _szűrő.adatok1.származási_ország), Update<string>("FOMUJE", _szűrő.adatok1.műszak_jele), Update<string>("FOTOGE", _szűrő.adatok1.töltőgép_száma),
                Update<string>("FOTEKO", _szűrő.adatok1.termékkód)});

                if (data != null)
                {
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_FOGLAL SET " + data + " WHERE FOSZAM = " + _azonosító.id;

                    try { command.ExecuteNonQuery(); command.Dispose(); }
                    catch (SqlException) { MessageBox.Show("Foglalás_ÚjVizsgalap -> adat1 hiba"); }
                }

                // Adatok2
                data = V(new string[] {
                Update<string>("FOSARZT", _szűrő.adatok2.sarzs.min), Update<string>("FOSARZI", _szűrő.adatok2.sarzs.max),
                Update<string>("FOHOSZT", _szűrő.adatok2.hordó_id.min), Update<string>("FOHOSZI", _szűrő.adatok2.hordó_id.max),
                Update<double?>("FOBRIXT", _szűrő.adatok2.brix.min), Update<double?>("FOBRIXI", _szűrő.adatok2.brix.max),
                Update<double?>("FOCSAVT", _szűrő.adatok2.citromsav.min), Update<double?>("FOCSAVI", _szűrő.adatok2.citromsav.max),
                Update<double?>("FOBOSAT", _szűrő.adatok2.borkősav.min), Update<double?>("FOBOSAI", _szűrő.adatok2.borkősav.max),
                Update<double?>("FOPEHAT", _szűrő.adatok2.ph.min), Update<double?>("FOPEHAI", _szűrő.adatok2.ph.max),
                Update<double?>("FOBOSTT", _szűrő.adatok2.bostwick.min), Update<double?>("FOBOSTI", _szűrő.adatok2.bostwick.max),
                Update<short?>("FOASAVT", _szűrő.adatok2.aszkorbinsav.min), Update<short?>("FOASAVI", _szűrő.adatok2.aszkorbinsav.max),
                Update<short?>("FONETOT", _szűrő.adatok2.nettó_töltet.min), Update<short?>("FONETOI", _szűrő.adatok2.nettó_töltet.max),
                Update<Int16?>("FOHOFOT", _szűrő.adatok2.hőkezelés.min), Update<Int16?>("FOHOFOI", _szűrő.adatok2.hőkezelés.max),
                Update<Int16?>("FOCIADT", _szűrő.adatok2.citromsav_ad.min), Update<Int16?>("FOCIADI", _szűrő.adatok2.citromsav_ad.max),
                Update<Int16?>("FOSZATT", _szűrő.adatok2.szita_átmérő.min), Update<Int16?>("FOSZATI", _szűrő.adatok2.szita_átmérő.max)});

                if (data != null)
                {
                    command = laborconnection.CreateCommand();
                    command.CommandText = "UPDATE L_FOGLAL SET " + data + " WHERE FOSZAM = " + _azonosító.id;

                    try { command.ExecuteNonQuery(); }
                    catch (SqlException q) { MessageBox.Show("Foglalás_ÚjVizsgalap -> adat2 hiba:\n" + q.Message); }
                }

                if (laborconnection.State != System.Data.ConnectionState.Open) return false;

                laborconnection.Close();
                return true;
            }
        }

        public bool Foglalás_Hozzáadás(Foglalás _foglalás)
        {
            lock (LaborLock)
            {
                SqlCommand command;

                laborconnection.Open();

                command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_FOGLAL (FONEVE, FOFOHO, FOTIPU, FOFENE, FODATE, FOFAJT)" +
                                    " VALUES('" + _foglalás.név + "', " + _foglalás.hordók_száma + ", '" + _foglalás.típus + "', '"
                                    + _foglalás.készítő + "', '" + _foglalás.idő + "', '" + _foglalás.típus + "')";

                try { command.ExecuteNonQuery(); }
                catch (Exception e) { MessageBox.Show(e.Message); return false; }
                finally { command.Dispose(); laborconnection.Close(); }

                laborconnection.Close();
                return true;
            }
        }

        public bool Foglalás_Módosítás(Foglalás _eredeti, Foglalás _új)
        {
            lock (LaborLock)
            {
                SqlCommand command;
                string where = A(new string[] { Update<int>("FOSZAM", _eredeti.id)});

                laborconnection.Open();

                command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_FOGLAL SET FONEVE='" + _új.név + "', FOTIPU= '" + _új.típus + "' WHERE " + where + ";";
                try { command.ExecuteNonQuery(); command.Dispose(); }
                catch (SqlException q) { MessageBox.Show("Foglalás Módosítás hiba:\n" + q.Message); }
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
                command.CommandText = "DELETE FROM L_FOGLAL WHERE FOSZAM = " + _azonosító.id + "; UPDATE L_HORDO SET FOSZAM = NULL WHERE FOSZAM = " + _azonosító.id;
                if (command.ExecuteNonQuery() == 0) found = false;

                command.Dispose();
                laborconnection.Close();

                return found;
            }
        }

        public List<Sarzs> Sarzsok(Vizsgalap_Szűrő _szűrő)
        {
            lock (LaborLock)
            {
                List<Sarzs> value = new List<Sarzs>();

                string Filter = A(new string[]
                {
                    Is(_szűrő.adatok1.gyümölcsfajta, "VIFAJT"),
                    Is(_szűrő.adatok1.hordótípus, "VIHOTI"),
                    Is(_szűrő.adatok1.megrendelő, "VIMEGR"),
                    Is(_szűrő.adatok1.származási_ország, "VISZOR"), 
                    Is(_szűrő.adatok1.műszak_jele, "VIMUJE"),
                    Is(_szűrő.adatok1.töltőgép_száma, "VITOGE"),
                    Is(_szűrő.adatok1.termékkód, "VITEKO"),
                    BetweenString(_szűrő.adatok2.sarzs, "CAST( VISARZ AS smallint)"),
                    BetweenString(_szűrő.adatok2.hordó_id, "VIHOSZ"),

                    Between<double>(_szűrő.adatok2.brix, "VIBRIX") ,
                    Between<double>(_szűrő.adatok2.citromsav, "VICSAV") ,
                    Between<double>(_szűrő.adatok2.borkősav, "VIBOSA") ,
                    Between<double>(_szűrő.adatok2.ph, "VIPEHA") ,
                    Between<double>(_szűrő.adatok2.bostwick, "VIBOST") ,

                    Between<Int16>(_szűrő.adatok2.aszkorbinsav, "VIASAV") ,
                    Between<Int16>(_szűrő.adatok2.nettó_töltet, "VINETO") ,
                    Between<Int16>(_szűrő.adatok2.hőkezelés, "VIHOKE") ,
                    Between<Int16>(_szűrő.adatok2.citromsav_ad, "VICIAD") ,
                    Between<Int16>(_szűrő.adatok2.szita_átmérő, "VISZAT") ,

                });

                laborconnection.Open();

                List<MinMaxPair<string>> filter = new List<MinMaxPair<string>>();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "(SELECT DISTINCT VITEKO, VISARZ, VIHOSZ FROM L_VIZSLAP) EXCEPT (SELECT DISTINCT VITEKO, VISARZ, VIHOSZ FROM L_VIZSLAP" + (Filter == null ? "" : " WHERE " + Filter) + ")";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    filter.Add(new MinMaxPair<string>(reader.GetString(0), reader.GetString(1)));
                }
                reader.Close();

                List<MinMaxPair<string>> data = new List<MinMaxPair<string>>();
                command.CommandText = "SELECT DISTINCT VITEKO, VISARZ FROM L_VIZSLAP" + (Filter == null ? "" : " WHERE " + Filter);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new MinMaxPair<string>(reader.GetString(0), reader.GetString(1)));
                }

                foreach (MinMaxPair<string> item in data)
                {
                    bool found = false;
                    foreach(MinMaxPair<string> filtered in filter)
                    {
                        if (filtered.min == item.min && filtered.max == item.max)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found) continue;

                    command.Dispose();
                    string where = A(new string[] { Update<string>("HOTEKO", item.min), Update<string>("HOSARZ", item.max)});

                    command = laborconnection.CreateCommand();
                    command.CommandText = "SELECT HOTEKO, HOSARZ, SUM(case when FOSZAM IS NULL then 0 else 1 end), SUM(case when FOSZAM IS NULL then 1 else 0 end) " +
                                        "FROM L_HORDO WHERE " + where + " GROUP BY HOTEKO, HOSARZ;";
                    reader.Close();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                       value.Add( new Sarzs(reader.GetString(0),reader.GetString(1), reader.GetInt32(2), reader.GetInt32(3)));
                    }
                }

                laborconnection.Close();

                return value;
            }
        }

        public List<string> Foglalás_Feltöltés_Ellenőrzés(Import _import)
        {
            List<string> hibák = new List<string>();

            lock (MarillenLock)
            {
                marillenconnection.Open();

                lock (laborconnection)
                {
                    laborconnection.Open();

            foreach (Import.Import_Hordó item in _import.import_hordók)
            {
                string iProdId = "12" + item.termékkód.Substring(0, 2) + "01" + item.gyártási_év + "_0" + item.gyártási_év + item.hordószám;
                string serial_nr = null;
                        string visarz = null;
                string vigyev = null;

                    SqlCommand command = new SqlCommand("SELECT serial_nr, prod_id, qty FROM tetelek WHERE (type=300) AND (prod_id like '" + iProdId + "') AND (qty > 0) ORDER BY serial_nr");
                    command.Connection = marillenconnection;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        serial_nr = reader.GetString(0);
                    }
                        reader.Close();

                    if (serial_nr == null)
                    {
                        hibák.Add(item.termékkód + " " + item.hordószám + " -nincs ilyen hordó");
                            continue;
                    }
                    else
                    {
                        command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr= " + serial_nr + " ) AND (code=3)");
                        command.Connection = marillenconnection;
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                                visarz = reader.GetString(0);
                            }
                    reader.Close();
                }

                        if (visarz != null)
                {
                            command = new SqlCommand("SELECT vigyev FROM l_vizslap WHERE (viteko=" + item.termékkód + ") AND (visarz= " + visarz + ");");
                    command.Connection = laborconnection;
                            reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        vigyev = reader.GetString(0);
                    }
                            reader.Close();

                    if (vigyev == null)
                    {
                        hibák.Add(item.termékkód + " " + item.hordószám + " -nincs vizsgálati lap");
                    }
                        }
                    }

                    laborconnection.Close();
                }

                marillenconnection.Close();
            }
            return hibák;
        }

        public List<string> Foglalás_Sarzsok(Import _import)
        {
            List<string> sarzsok = new List<string>();

            foreach (Import.Import_Hordó item in _import.import_hordók)
            {
                string iProdId = "12" + item.termékkód.Substring(0, 2) + "01" + item.gyártási_év + "_0" + item.gyártási_év + item.hordószám;
                string serial_nr = null;

                lock (MarillenLock)
                {
                    marillenconnection.Open();

                    SqlCommand command = new SqlCommand("SELECT serial_nr, prod_id, qty FROM tetelek WHERE (type=300) AND (prod_id like '" + iProdId + "') AND (qty > 0) ORDER BY serial_nr");
                    command.Connection = marillenconnection;
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        serial_nr = reader.GetString(0);
                    }

                        command = new SqlCommand("SELECT propstr FROM folyoprops WHERE (serial_nr= " + serial_nr + " ) AND (code=3)");
                        command.Connection = marillenconnection;
                        reader.Close();
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            sarzsok.Add( reader.GetString(0));
                        }
                    
                    reader.Close();
                    marillenconnection.Close();
                }
            }
            return sarzsok;
        }


        #endregion

        #region Gyümölcsfajták
        public List<string> Gyümölcsfajták(string _termékkód)
        {
            lock (LaborLock)
            {
                List<string> value = new List<string>();
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT GFAZON FROM L_GYFAJTA WHERE (GFTEKO = '" + _termékkód.Substring(0, 2) + "') ORDER BY GFAZON";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add(reader.GetString(0));
                }
                command.Dispose();
                laborconnection.Close();
                return value;
            }
        }
        #endregion

        #region Konszignáció
        /// <summary>
        /// Meg kell jeleníteni a rendszerben elmentett foglalásokat, amelyek még nem kerültek kiszállításra, nincs a szállítólevél mezőjük kitöltve (L_FOGLAL.SZSZAM=NULL).
        /// TODO: Ide majd kell vmi ellenőrzés de még nincs kitalálva
        /// </summary>
        public List<Foglalás> Konszingnáció_Foglalások()
        {
            List<Foglalás> data = new List<Foglalás>();

            lock (LaborLock)
            {
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT FOSZAM, FONEVE, FOFOHO, FOTIPU, FOFENE, FODATE FROM L_FOGLAL WHERE SZSZAM IS NULL";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int c = -1;
                    Foglalás temp_foglalás = new Foglalás(reader.GetInt32(++c), reader.GetString(++c), reader.GetInt16(++c), reader.GetString(++c), reader.GetString(++c), reader.GetString(++c));
                    data.Add(temp_foglalás);
                }
                command.Dispose();
                laborconnection.Close();
            }
                return data;
        }

        /// <summary>
        /// Itt lehet megnézni a foglaláshoz tartozó hordókat.
        /// </summary>
        public List<Hordó> Konszignáció_Hordók(int _foglalás_száma)
        {
            List<Hordó> value = new List<Hordó>();
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT HOTEKO, HOSARZ, HOSZAM, VIGYEV, HOQTY,HOTIME FROM L_HORDO WHERE FOSZAM = " + _foglalás_száma;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int c = -1;
                    value.Add(new Hordó(reader.GetString(++c), reader.GetString(++c), reader.GetString(++c), _foglalás_száma, reader.GetString(++c),reader.GetDecimal(++c),reader.GetString(++c)));
                }

                command.Dispose();
                laborconnection.Close();
            }
            return value;
        }

        public int Konszignáció_ÚJSzállítólevél(Konszignáció_Szállítólevél _szállítólevél)
        {
            lock (LaborLock)
            {
                int modified = 0;
                SqlCommand command;
                laborconnection.Open();
                command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_SZLEV (SZSZSZ,SZFENE,SZDATE,SZNYEL,SZVEVO,SZGKR1,SZGKR2,FOFOHO,SZGYEV,SZSZIN,SZIZEK,SZILLA) output INSERTED.SZSZAM" +  
                    " VALUES(" + "'" + _szállítólevél.szlevél +  "','" + _szállítólevél.fnév + "','" + _szállítólevél.elszállítás_ideje + "','"+ _szállítólevél.nyelv + "','" +_szállítólevél.vevő + "','"+ _szállítólevél.gépkocsi1 + "','" +_szállítólevél.gépkocsi2  + "'," + _szállítólevél.foglalt_hordó + ",'"+ _szállítólevél.gyártási_idő.Substring(0,4) + "','"+ _szállítólevél.szín + "','"+ _szállítólevél.íz + "','"+ _szállítólevél.illat + "');";
               ;
               try { modified = (int)command.ExecuteScalar(); }
                catch{ return modified; }
                finally { command.Dispose(); laborconnection.Close(); }
                laborconnection.Close();
                return modified;
            }
        }

        /// <summary>
        /// Marillenből, vevő adatai
        /// </summary>
        public Node_Konszignáció.Fejléc.Vevő Konszignáció_Vevő(string _partner)
        {
            Node_Konszignáció.Fejléc.Vevő data = new Node_Konszignáció.Fejléc.Vevő();

            lock (MarillenLock)
            {
                marillenconnection.Open();
                SqlCommand command = marillenconnection.CreateCommand();
                command.CommandText = "SELECT name, city, addr FROM partner WHERE partner.name=" + "'" + _partner + "'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int c = -1;
                    data = new Node_Konszignáció.Fejléc.Vevő(reader.GetString(++c), reader.GetString(++c), reader.GetString(++c));
                }
                command.Dispose();
                marillenconnection.Close();
            }

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            data.vevő_cím = regex.Replace(data.vevő_cím, @" ");
            data.vevő_név = regex.Replace(data.vevő_név, @" ");
            data.vevő_város = regex.Replace(data.vevő_város, @" ");

            return data;
        }

        /// <summary>
        /// Marillenből, gyümölcsnév, vtsz
        /// </summary>
        public Node_Konszignáció.Gyümölcstípus Konszignáció_Gyümölcstípus(string _termékkód)
        {
            Node_Konszignáció.Gyümölcstípus data = new Node_Konszignáció.Gyümölcstípus();
            lock (MarillenLock)
            {
                marillenconnection.Open();
                SqlCommand command = marillenconnection.CreateCommand();
                command.CommandText = "SELECT vtsz,name FROM cikkek WHERE cikkek.item_nr=" + "'12" + _termékkód.Substring(0, 2) + "01'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int c = -1;
                    data = new Node_Konszignáció.Gyümölcstípus(reader.GetString(++c), reader.GetString(++c));
                }
                command.Dispose();
                marillenconnection.Close();
            }
            return data;
        }


        public string Name(string _termékkód)
        {
            string name = "" ;
            lock (MarillenLock)
            {
                marillenconnection.Open();
                SqlCommand command = marillenconnection.CreateCommand();
                command.CommandText = "SELECT name FROM cikkek WHERE cikkek.item_nr=" + "'12" + _termékkód.Substring(0, 2) + "01'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    name =  reader.GetString(0);
                }
                command.Dispose();
                marillenconnection.Close();
            }
            return name;
        }


        /// <summary>
        /// a kinyomtatott foglalások szállítólevél mezőjét (L_FOGLAL.SZSZAM) kitölti a megadott értékkel hogy a következő foglalás alkalmával ne vegye őket figyelembe 
        /// </summary>
        public bool Konszignáció_FoglalásokKiszállítása(int _szállítólevélszám, List<Foglalás> _foglalások)
        {
            string where = null;

            for (int i = 0; i < _foglalások.Count; i++)
            {
                if(i!=_foglalások.Count-1)
                {
                    where += Update<int>("FOSZAM", _foglalások[i].id) + " or ";
                }
                else
                {
                    where += Update<int>("FOSZAM", _foglalások[i].id);
                }
            }

            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_FOGLAL SET SZSZAM='" + _szállítólevélszám + "' WHERE " + where;
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
                return true;
            }
        }
        #endregion

        #region MinőségBizonylat
        public Node_MinőségBizonylat.VizsgálatiLap MinőségBizonylat(int _id)
        {
            Node_MinőségBizonylat.VizsgálatiLap data = new Node_MinőségBizonylat.VizsgálatiLap();
            lock (LaborLock)
            {
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT  MIN(vibrix), MAX(vibrix), MIN(vicsav), MAX(vicsav), MIN(vipeha), MAX(vipeha), MIN(vibost), MAX(vibost),  MIN(viciad), MAX(viciad), MAX(viasav), hoteko, hosarz , vitene, viszat, vihoti, viszor " +
                    "FROM l_hordo " +
                    "INNER JOIN l_vizslap ON l_hordo.hoteko= l_vizslap.viteko AND l_hordo.hosarz= l_vizslap.visarz " +
                    "WHERE l_hordo.foszam= " + _id + " group by l_hordo.hosarz, l_hordo.hoteko, vitene, viszat, vihoti, viszor ";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data = new Node_MinőségBizonylat.VizsgálatiLap(
                    (double?)GetNullable<decimal>(reader, 0), (double?)GetNullable<decimal>(reader, 1),
                    (double?)GetNullable<decimal>(reader, 2), (double?)GetNullable<decimal>(reader, 3),
                    (double?)GetNullable<decimal>(reader, 4), (double?)GetNullable<decimal>(reader, 5),
                    (double?)GetNullable<decimal>(reader, 6), (double?)GetNullable<decimal>(reader, 7),
                    (double?)GetNullable<Int16>(reader, 8), (double?)GetNullable<Int16>(reader, 9),
                    (double?)GetNullable<Int16>(reader, 10),
                    reader.GetString(11),
                    reader.GetString(12),reader.GetString(13),reader.GetString(14),reader.GetString(15),reader.GetString(16));
                }

                command.Dispose();
                laborconnection.Close();
            }
            return data;
        }

        public Node_MinőségBizonylat.Tápérték MinBiz_Tápérték( string _hoteko)
        {
            Node_MinőségBizonylat.Tápérték data = new Node_MinőségBizonylat.Tápérték();
            lock (LaborLock)
            {
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT takio, takcal, tafehe, taszhi, tazsir, taelro FROM l_tapertek WHERE " + _hoteko.Substring(0, 2) + " = l_tapertek.tateko";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data = new Node_MinőségBizonylat.Tápérték(
                    GetNullable<short>(reader, 0), GetNullable<short>(reader, 1),
                    (double)GetNullable<decimal>(reader, 2), (double)GetNullable<decimal>(reader, 3),
                    (double)GetNullable<decimal>(reader, 4), (double)GetNullable<decimal>(reader, 5));
                }
                command.Dispose();
                laborconnection.Close();
            }
            return data;
        }

        public Node_MinBiz_Szöveg MinőségBizonylat_Szöveg()
        {
            Node_MinBiz_Szöveg data = new Node_MinBiz_Szöveg();

            lock (LaborLock)
            {
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT MISZ1M,MISZ1A, MISZ2M,MISZ2A FROM L_MINBIZ";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.sz1_m = reader.GetString(0);
                    data.sz1_a = reader.GetString(1);
                    data.sz2_m = reader.GetString(2);
                    data.sz2_a = reader.GetString(3);
                }
                command.Dispose();
                laborconnection.Close();
            }
            return data;
        }

        #endregion

        #region Kiszállítások
        public List<Kiszállítás> Kiszállítások()
        {
            List<Kiszállítás> value = new List<Kiszállítás>();

            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT szszam, szszsz,szfene,szdate, szvevo,fofoho FROM l_szlev ORDER BY szszam";

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    value.Add( new Kiszállítás(reader.GetInt32(0),reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetString(4),reader.GetInt16(5)));
                }
                command.Dispose();
                laborconnection.Close();
            }
            return value;
        }

        public bool Kiszállítás_Törlés(int _szállítólevél_szám)
        {
            lock (LaborLock)
            {
                bool found = true;
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE l_foglal SET szszam=NULL WHERE szszam= '" + _szállítólevél_szám + "'; DELETE FROM l_szlev WHERE szszam= '" + _szállítólevél_szám + "'";

                if (command.ExecuteNonQuery() == 0) found = false;

                command.Dispose();
                laborconnection.Close();

                return found;
            }
        }
        #endregion

        #region Felhasználók
        public List<Felhasználó> Felhasználók()
        {
            List<Felhasználó> values = new List<Felhasználó>();

            lock(LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT FEFEN1, FEFEN2, FEBEO1, FEBEO2, FEBEKO, FEJELS FROM L_FELHASZ;";
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        values.Add(new Felhasználó(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), null));
                    }
                }
                catch (SqlException q) { MessageBox.Show("Összes Felhasználó lekérdezés hiba:\n" + q.Message); }

                command.Dispose();
                laborconnection.Close();
            }

            return values;
        }

        public Felhasználó? Felhasználó(string _felhasználó_név)
        {
            Felhasználó? felhasználó = null;

            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "SELECT * FROM L_FELHASZ WHERE FEBEKO = '" + _felhasználó_név + "';";
                try
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Felhasználó.Jogosultságok.Műveletek törzsadatok = new Labor.Felhasználó.Jogosultságok.Műveletek(VB(reader.GetString(6)), VB(reader.GetString(7)), VB(reader.GetString(8)));
                        Felhasználó.Jogosultságok.Műveletek vizsgálatok = new Labor.Felhasználó.Jogosultságok.Műveletek(VB(reader.GetString(9)), VB(reader.GetString(10)), VB(reader.GetString(11)));
                        Felhasználó.Jogosultságok.Műveletek foglalások = new Labor.Felhasználó.Jogosultságok.Műveletek(VB(reader.GetString(12)), VB(reader.GetString(13)), VB(reader.GetString(14)));
                        Felhasználó.Jogosultságok.Műveletek felhasználók = new Labor.Felhasználó.Jogosultságok.Műveletek(VB(reader.GetString(17)), VB(reader.GetString(18)), VB(reader.GetString(19)));

                        Felhasználó.Jogosultságok jogosultságok = new Labor.Felhasználó.Jogosultságok(törzsadatok, vizsgálatok, foglalások, VB(reader.GetString(15)), VB(reader.GetString(16)), felhasználók);

                        felhasználó = new Felhasználó(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), _felhasználó_név, reader.GetString(5), jogosultságok);
                    }
                    reader.Close();
                }
                catch (SqlException q) { MessageBox.Show("Felhasználó adatainak lekérdezésekor hiba:\n" + q.Message); }

                command.Dispose();
                laborconnection.Close();
            }

            return felhasználó;
        }

        public bool Felhasználó_Hozzáadás(Felhasználó _f)
        {            
            string data = V_Apostrophe(new string[] { _f.név1, _f.név2, _f.beosztás1, _f.beosztás2, _f.felhasználó_név, _f.jelszó,
                BV(_f.jogosultságok.Value.törzsadatok.hozzáadás), BV(_f.jogosultságok.Value.törzsadatok.módosítás), BV(_f.jogosultságok.Value.törzsadatok.törlés),
                BV(_f.jogosultságok.Value.vizsgálatok.hozzáadás), BV(_f.jogosultságok.Value.vizsgálatok.módosítás), BV(_f.jogosultságok.Value.vizsgálatok.törlés),
                BV(_f.jogosultságok.Value.foglalások.hozzáadás), BV(_f.jogosultságok.Value.foglalások.módosítás), BV(_f.jogosultságok.Value.foglalások.törlés),
                BV(_f.jogosultságok.Value.konszignáció_nyomtatás), BV(_f.jogosultságok.Value.kiszállítások_törlés),
                BV(_f.jogosultságok.Value.felhasználók.hozzáadás), BV(_f.jogosultságok.Value.felhasználók.módosítás), BV(_f.jogosultságok.Value.felhasználók.törlés) });

            lock (LaborLock)
            {
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "INSERT INTO L_FELHASZ VALUES(" + data + ");";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException q) { MessageBox.Show("Felhasználó hozzáadás hiba:\n" + q.Message); return false; }

                command.Dispose();
                laborconnection.Close();
            }

            return true;
        }

        public bool Felhasználó_Módosítás(string _felhasználó_név, Felhasználó _f)
        {
            string data = V(new string[] { Update<string>("FEFEN1", _f.név1),  Update<string>("FEFEN2", _f.név2), Update<string>("FEBEO1", _f.beosztás1), Update<string>("FEBEO2", _f.beosztás2), Update<string>("FEJELS", _f.jelszó),
                Update<string>("FETOHO", BV(_f.jogosultságok.Value.törzsadatok.hozzáadás)), Update<string>("FETORO", BV(_f.jogosultságok.Value.törzsadatok.módosítás)), Update<string>("FETOTO", BV(_f.jogosultságok.Value.törzsadatok.törlés)),
                Update<string>("FEVIHO", BV(_f.jogosultságok.Value.vizsgálatok.hozzáadás)), Update<string>("FEVIRO", BV(_f.jogosultságok.Value.vizsgálatok.módosítás)), Update<string>("FEVITO", BV(_f.jogosultságok.Value.vizsgálatok.törlés)),
                Update<string>("FEFOKE", BV(_f.jogosultságok.Value.foglalások.hozzáadás)), Update<string>("FEFOFE", BV(_f.jogosultságok.Value.foglalások.módosítás)), Update<string>("FEFOTO", BV(_f.jogosultságok.Value.foglalások.törlés)),
                Update<string>("FEKONY", BV(_f.jogosultságok.Value.konszignáció_nyomtatás)), Update<string>("FEKITO", BV(_f.jogosultságok.Value.kiszállítások_törlés)),
                Update<string>("FEFEHO", BV(_f.jogosultságok.Value.felhasználók.hozzáadás)), Update<string>("FEFERO", BV(_f.jogosultságok.Value.felhasználók.módosítás)), Update<string>("FEFETO", BV(_f.jogosultságok.Value.felhasználók.törlés)) });

            lock (LaborLock)
            {
                laborconnection.Open();

                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_FELHASZ SET " + data + " WHERE FEBEKO = '" + _felhasználó_név + "';";
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException q) { MessageBox.Show("Felhasználó hozzáadás hiba:\n" + q.Message); }

                command.Dispose();
                laborconnection.Close();
            }

            return true;
        }

        public bool Felhasználó_Törlés(string _felhasználó_név)
        {
            int value = 0;
            lock (LaborLock)
            {
                laborconnection.Open();
                SqlCommand command = laborconnection.CreateCommand();
                command.CommandText = "DELETE FROM L_FELHASZ WHERE FEBEKO = '" + _felhasználó_név + "';";
                value = command.ExecuteNonQuery();
                command.Dispose();
                laborconnection.Close();
            }

            return value == 1 ? true : false;
        }
        #endregion

        #region Sufni
        private class AdminForm : Form
        {
            public string Data = null;

            #region Declaration
            private TextBox box_név1;
            private TextBox box_név2;
            private TextBox box_beosztás1;
            private TextBox box_beosztás2;
            private TextBox box_felhasználó_név;
            private TextBox box_jelszó;
            private TextBox box_jelszó_mégegyszer;
            #endregion

            #region Constructor
            public AdminForm()
            {
                InitializeForm();
                InitializeContent();
                InitializeData();
            }

            private void InitializeForm()
            {
                ClientSize = new System.Drawing.Size(400, 200 + 64);
                MaximumSize = ClientSize;
                Text = "Admin adatai";
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            }

            private void InitializeContent()
            {
                const int offset = 16;
                const int spacer = 24;
                const int group_spacer = 8;
                Tuple<string, int, int>[] labels = new Tuple<string, int, int>[]{
                    new Tuple<string, int, int>("Név", 2, 1),
                    new Tuple<string, int, int>("Beosztás", 2, 1),
                    new Tuple<string, int, int>("Belépési kód", 1, 0),
                    new Tuple<string, int, int>("Jelszó", 2, 1) };

                int count = 0;
                int group = 0;
                for (int current = 0; current < labels.Length; ++current)
                {
                    Label label = MainForm.createlabel(labels[current].Item1 + ":", offset, count * spacer + group * group_spacer + offset, this);
                    count += labels[current].Item2;
                    group += labels[current].Item3;
                }

                //

                const int column = 100;
                box_név1 = MainForm.createtextbox(column, 0 * spacer + 0 * group_spacer + offset, 30, 30 * 8, this, CharacterCasing.Normal);
                box_név2 = MainForm.createtextbox(column, 1 * spacer + 0 * group_spacer + offset, 30, 30 * 8, this, CharacterCasing.Normal);

                box_beosztás1 = MainForm.createtextbox(column, 2 * spacer + 1 * group_spacer + offset, 30, 30 * 8, this, CharacterCasing.Normal);
                box_beosztás2 = MainForm.createtextbox(column, 3 * spacer + 1 * group_spacer + offset, 30, 30 * 8, this, CharacterCasing.Normal);

                box_felhasználó_név = MainForm.createtextbox(column, 4 * spacer + 2 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_jelszó = MainForm.createtextbox(column, 5 * spacer + 2 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_jelszó.PasswordChar = '*';
                box_jelszó_mégegyszer = MainForm.createtextbox(column, 6 * spacer + 2 * group_spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
                box_jelszó_mégegyszer.PasswordChar = '*';

                //

                Button rendben = new Button();
                rendben.Size = new System.Drawing.Size(96, 32);
                rendben.Location = new System.Drawing.Point(ClientSize.Width - rendben.Width - spacer, ClientSize.Height - rendben.Height - spacer);
                rendben.Click += rendben_Click;
                rendben.Text = "Rendben";

                Controls.Add(rendben);
            }

            private void InitializeData()
            {
                box_név1.Text = "Marillen";

                box_beosztás1.Text = "Adminisztrátor";
                box_beosztás2.Text = "System Administrator";

                box_felhasználó_név.Text = "admin";
                box_jelszó.Text = "admin";
                box_jelszó_mégegyszer.Text = "admin";
            }
            #endregion

            #region EventHandlers
            private void rendben_Click(object _sender, EventArgs _event)
            {
                if (box_jelszó.Text != box_jelszó_mégegyszer.Text) { MessageBox.Show("Nem egyezik a két jelszó!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                if (!Database.IsCorrectSQLText(box_név1.Text)) { MessageBox.Show("Nem megengedett karakter a név1 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_név2.Text)) { MessageBox.Show("Nem megengedett karakter a név2 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_beosztás1.Text)) { MessageBox.Show("Nem megengedett karakter a beosztás1 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_beosztás2.Text)) { MessageBox.Show("Nem megengedett karakter a beosztás2 mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_felhasználó_név.Text)) { MessageBox.Show("Nem megengedett karakter a felhasználó név mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                if (!Database.IsCorrectSQLText(box_jelszó.Text)) { MessageBox.Show("Nem megengedett karakter a jelszó mezőben!", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                Data = V_Apostrophe(new string[] { box_név1.Text, box_név2.Text, box_beosztás1.Text, box_beosztás2.Text, box_felhasználó_név.Text, box_jelszó.Text }) + 
                    ", 'I', 'I', 'I',   'I', 'I', 'I',   'I', 'I', 'I',   'I',   'I',   'I', 'I', 'I'";

                Close();
            }
            #endregion
        }
        private string HardcodedData(string _admin_data)
        {
            return
                "INSERT INTO L_GYFAJTA (GFTEKO, GFAZON, GFSZO2,GFSZO3) VALUES    ('21','Érdi jubileum','','')," +
                    "('21','Érdi bőtermő','','')," +
                    "('21','Cigány','Gipsy','')," +
                    "('21','Pándy','','')," +
                    "('21','Újfehértói fürtös','','')," +
                    "('21','Debreceni Bőtermő','','')," +
                    "('21','Kántorjánosi','','')," +
                    "('22','Vega','','')," +
                    "('14','Anita','','')," +
                    "('14','Elsanta','','')," +
                    "('14','Ázsia','Asia','')," +
                    "('14','Clary','','')," +
                    "('11','Piros','red','')," +
                    "('10','Fekete','black','')," +
                    "('31','Piroska','','')," +
                    "('31','Pannónia','','')," +
                    "('31','Gönci kajszi','','')," +
                    "('31','Ceglédi óriás','','')," +
                    "('31','Bergeron','','')," +
                    "('31','Sweetred','','')," +
                    "('31','SPB-1','','')," +
                    "('31','Pincot','','')," +
                    "('31','Sylred','','')," +
                    "('31','MK','','')," +
                    "('31','Bergarouge','','')," +
                    "('31','Mandula','','')," +
                    "('31','Zebra','','')," +
                    "('31','Berge cot','','')," +
                    "('31','Flavor cot','','')," +
                    "('31','Gold cot','','')," +
                    "('31','Vertige','','')," +
                    "('31','Tardiff de provance','','')," +
                    "('41','Early redhaven','','')," +
                    "('41','Suncrest','','')," +
                    "('41','Babygold','','')," +
                    "('41','Fayette','','')," +
                    "('41','Cresthaven','','')," +
                    "('41','Padana','','')," +
                    "('41','Yo-yo','','')," +
                    "('41','President','','')," +
                    "('41','sárgahúsú','yellow sort','')," +
                    "('42','fehérhúsú','white sort','')," +
                    "('42','Incrocio Pierry','','')," +
                    "('42','Champion','','')," +
                    "('42','Michellini','','')," +
                    "('61','Lepotica','','')," +
                    "('61','Stanley','','')," +
                    "('61','Bluefree','','')," +
                    "('61','Empress','','')," +
                    "('61','Besztercei','','')," +
                    "('61','Elena','','')," +
                    "('61','Presenta','','')," +
                    "('61','Rodna','','')," +
                    "('51','Clapp','','')," +
                    "('51','Vilmos','Williams','')," +
                    "('51','Bosc kobak','','')," +
                    "('51','Kálmán','','')," +
                    "('51','Papp','','')," +
                    "('51','Decana','','')," +
                    "('51','Conferance','','')," +
                    "('51','Packhams','','')," +
                    "('51','Serres Olivér','','')," +
                    "('71','Jonathan','','')," +
                    "('72','Golden','','')," +
                    "('71','piros','red','')," +
                    "('75','Idared','','')," +
                    "('78','Jonagored','','')," +
                    "('78','Ozar gold','','')," +
                    "('92','Orange','','')," +
                    "('92','Nagydobosi','','')," +
                    "('92','Butternut','','')," +
                    "('92','Muscat de provance','','')," +
                    "('18','gyűjtött','collected','')," +
                    "('12','Hasberg','','')," +
                    "('12','gyűjtött','collected','');" +

                "INSERT INTO L_TAPERTEK (TATEKO, TAKIO , TAKCAL, TAFEHE , TASZHI , TAZSIR , TAELRO) VALUES" +
                    "(10, 143, 34, 0.6, 7.0, 1.2, 0)," +
                    "(11, 202, 48, 0.9, 9.5, 2.0, 7.80)," +
                    "(12,0, 0, 0, 0, 0, 0)," +
                    "(13, 139, 33, 0.8, 6.0, 1.8, 0)," +
                    "(14, 147, 35, 0.9, 7.2, 0.6, 1.70)," +
                    "(15, 122, 29, 1.2, 5.4, 0.8, 9.10)," +
                    "(16,0, 0, 0, 0, 0, 0)," +
                    "(17,0, 0, 0, 0, 0, 0)," +
                    "(18, 214, 51, 3.6, 8.0, 0, 0)," +
                    "(19,0, 0, 0, 0, 0, 0)," +
                    "(21, 218, 52, 0.8, 11.0, 1.4, 4.20)," +
                    "(22, 265, 63, 0.8, 14.0, 0.7, 0)," +
                    "(23, 164, 39, 0.6, 8.0, 1.4, 3.50)," +
                    "(24,0, 0, 0, 0, 0, 0)," +
                    "(25,0, 0, 0, 0, 0, 0)," +
                    "(27, 265, 63, 0.8, 14.0, 0.7, 0)," +
                    "(28, 218, 52, 0.8, 11.0, 1.4, 4.20)," +
                    "(29, 218, 52, 0.8, 11.0, 1.4, 4.20)," +
                    "(31, 202, 48, 0.9, 10.2, 0.6, 3.60)," +
                    "(39, 202, 48, 0.9, 10.2, 0.6, 3.60)," +
                    "(41, 172, 41, 0.7, 9.0, 0.3, 3.20)," +
                    "(42, 172, 41, 0.7, 9.0, 0.3, 3.20)," +
                    "(43, 206, 49, 0.4, 11.4, 0.5, 0)," +
                    "(49, 172, 41, 0.7, 9.0, 0.3, 3.20)," +
                    "(51, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(52, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(53, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(54, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(57, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(58, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(59, 218, 52, 0.4, 12.0, 0.3, 6.20)," +
                    "(61, 197, 47, 0.8, 10.2, 0.8, 0)," +
                    "(69, 197, 47, 0.8, 10.2, 0.8, 0)," +
                    "(71, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(72, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(73, 176, 42, 0.6, 9.1, 0.9, 0)," +
                    "(74, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(75, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(76, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(77, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(78, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(79, 130, 31, 0.4, 7.0, 0.4, 3.70)," +
                    "(81, 168, 40, 0.3, 9.5, 0, 0)," +
                    "(89, 130, 31, 1.3, 5.9, 0.1, 4.24)," +
                    "(91, 97, 23, 1.0, 4.0, 0.2, 1.73)," +
                    "(92, 336, 80, 1.5, 16.5, 0.6, 0)," +
                    "(93, 168, 40, 1.2, 8.1, 0.2, 3.27)," +
                    "(94, 130, 31, 1.3, 5.9, 0.1, 4.24)," +
                    "(97, 97, 23, 1.0, 4.0, 0.2, 1.73)," +
                    "(98, 168, 40, 1.2, 8.1, 0.2, 3.27)," +
                    "(99, 336, 80, 1.5, 16.5, 0.6, 0);" +

                "INSERT INTO L_TORZSA (TOTIPU, TOAZON, TOSZO2, TOSZO3) VALUES('Származási ország','Magyarország','Hungary','Ungarn')," +
                    "('Származási ország','Szlovákia','Slovakia','Slowakei')," +
                    "('Származási ország','Románia','Romania','Rumänien')," +
                    "('Hordótípus','Kicsi','Small','Klein')," +
                    "('Hordótípus','Nagy','Big','Groß')," +
                    "('Laboros','Belinyák Máté','Máté Belinyák','Máté Belinyák')," +
                    "('Laboros','Belinyák Nándor','Nándor Belinyák','Nándor Belinyák');" +

                "INSERT INTO L_MINBIZ (MISZ1M , MISZ1A , MISZ2M , MISZ2A) "  +
                    "VALUES ('Alulírott Marillen Kft. kijelenti, hogy a fenti termék mindenben megfelel az érvényes magyar előírásoknak.'," +
                    " 'Marillen Kft. certifies that the above mentioned product is in accordance with current Hungarian legislation.',"+
                    " 'Alulírott Marillen Kft. nevében kijelentem, hogy az általunk gyártott aszeptikus velő nem génmanipulált termék. Génmanipulált alap- és segédanyagokat, ill. allergén anyagokat nem tartalmaz.',"+
                    " 'Aseptic purees produced by Marillen Ltd. are not genetically modified and don’t contain any genetically modified raw materials and additives.');" +

                "INSERT INTO L_FELHASZ (FEFEN1, FEFEN2, FEBEO1, FEBEO2, FEBEKO, FEJELS,   FETOHO, FETORO, FETOTO,   FEVIHO, FEVIRO, FEVITO,   FEFOKE, FEFOFE, FEFOTO,   FEKONY,   FEKITO,   FEFEHO, FEFERO, FEFETO) " +
                    "VALUES (" + (_admin_data == null ? "'Marillen', 'Adminisztrátor', 'Admin', '', 'admin', 'admin',   'I', 'I', 'I',   'I', 'I', 'I',   'I', 'I', 'I',   'I',   'I',   'I', 'I', 'I'" : _admin_data) +");";
        }
        #endregion
    }
}
