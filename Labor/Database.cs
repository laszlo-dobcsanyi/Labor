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


                            // TODO kellene egy vizsgalap id, ami 1-től nő
                            "CREATE TABLE L_VIZSLAP (VITEKO varchar(4) NOT NULL,VIZSSZ varchar(4) NOT NULL, VIGYEV varchar(1) NOT NULL, VISARZ varchar(3) NOT NULL, VIMSSZ int IDENTITY(1,1), VITENE varchar(50), VISZAT tinyint," +
                                "VIHOKE tinyint, VINETO DECIMAL(6, 3),VIMUJE varchar(1),VITOGE varchar(1), VIHOTI varchar(15),VIMEGR varchar(50),VISZOR varchar(15),VIFAJT varchar(50),VIBRIX DECIMAL(3,1),VICSAV DECIMAL(4,2)," +
                                "VIBOSA DECIMAL(4,2),VIPEHA DECIMAL(4,2),VIBOST DECIMAL(3,1),VIASAV tinyint,VICIAD tinyint, VIMATO tinyint,VIFEFE tinyint,VIFEBA tinyint,VILEOL varchar(11),VIERDA varchar(11)," +
                                "VIOCS1 tinyint,VIOCS2 tinyint,VIELE1 tinyint,VIELE2 tinyint,VIPEN1 tinyint,VIPEN2 tinyint,VIEMEJE varchar(300), VIMTS1 varchar(15),VIMTD1 varchar(8),VIMKS1 varchar(15),VIMKD1 varchar(8)," +
                                "VIMKS2 varchar(15),VIMKD2 varchar(8),VIMKS3 varchar(15),VIMKD3 varchar(8),VIMKS4 varchar(15),VIMKD4 varchar(8),VIMKS5 varchar(15),VIMKD5 varchar(8), VIMKS6 varchar(15),VIMKD6 varchar(8),VILABO varchar(15)," +
                                "VISZIN varchar(60),VIIZEK varchar(60),VIILLA varchar(60));" +

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
            if (type == typeof(string)) { if (_value != null) return _column_name + " = '" + _value + "'"; }

            if (type == typeof(int)) { return _column_name + " = " + _value; }
            if (type == typeof(int?)) { if (_value != null) return _column_name + " = " + _value; }

            if (type == typeof(double)) { return _column_name + " = " + _value; }
            if (type == typeof(double?)) { if (_value != null) return _column_name + " = " + _value; }
            return "";
        }

        #region Marillen Adatbázisából
        public List<string> Gyümölcsfajták( string _termékkód )
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

        public List<string> Megrendelők()
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

        /// <summary>
        /// Visszaadja a termékkód darabkával kezdődő kódokat
        /// </summary>
        public List<string> Termékkódok(string _termékkód_darab)
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
        #endregion

        #region Törzsadatok
        public List<Törzsadat> Törzsadatok(string _TOTIPU)
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

        public bool Törzsadat_Hozzáadás(Törzsadat _törzsadat)
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

        public List<string> Törzsadat_Típusok()
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

        public bool Törzsadat_Módosítás(string _azonosító, Törzsadat törzsadat)
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

        public bool Törzsadat_Törlés(string _azonosító)
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
        #endregion

        #region Vizsgálatok
        public Vizsgálat? Vizsgálat(Vizsgálat.Azonosító _azonosító)
        {
            Vizsgálat? value = null;
            return value;
        }

        /*row[0] = token.data.termékkód;
          row[1] = token.data.sarzs;
          row[2] = token.data.hordószám;
          row[3] = token.data.hordótípus;
          row[4] = token.data.nettó_töltet;
          row[5] = token.data.szita_átmérő;
          row[6] = token.data.megrendelő;
          row[7] = token.data.sorszám;*/
        public List<Vizsgálat.Azonosító> Vizsgálatok()
        {
            List<Vizsgálat.Azonosító> data = new List<Vizsgálat.Azonosító>();
            laborconnection.Open();

            SqlCommand command = laborconnection.CreateCommand();
            command.CommandText = "SELECT VITEKO, VIZSSZ, VISARZ, VIMSSZ, VIHOTI, VINETO, VISZAT, VIMEGR FROM L_VIZSLAP";

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // TODO asdasd
                data.Add(new Vizsgálat.Azonosító(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), (double)reader.GetDecimal(5), reader.GetByte(6), reader.GetString(7)));
            }
            command.Dispose();
            laborconnection.Close();

            return data;
        }

        public bool Vizsgálat_Törlés(Vizsgálat.Azonosító _azonosító)
        {
            laborconnection.Open();
            SqlCommand command = laborconnection.CreateCommand();
            command.CommandText = "DELETE FROM L_VIZSLAP WHERE VITEKO = '" + _azonosító.termékkód + "' AND VIZSSZ= '" + _azonosító.hordószám  + "' AND VISARZ= '" + _azonosító.sarzs + "' AND VIMSSZ= '" + _azonosító.sorszám + "';";
            command.ExecuteNonQuery();
            command.Dispose();
            laborconnection.Close();
            return true;
        }

        /// <summary>
        /// Ha a Vizsgálat táblában ennek a vizsgálatnak már van eltérő hordótípus, akkor az előző típust kell visszaadni, különben null!
        /// </summary>
        public string Vizsgálat_SarzsEllenőrzés(Vizsgálat.Azonosító _azonosító)
        {
            return null;
        }

        /// <summary>
        /// Termékkód, hordószám megadása után a fejléc adatait kérjük le!
        /// box_szita_átmérő, nettó_töltet, műszak_jele, töltőgép_száma, sarzs
        /// </summary>
        public List<string> Vizsgálat_Prod_Id(string _prod_id)
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

        /// <summary>
        /// Termékkódhoz tartozó terméknél lekérdezése, ha nem találja, akkor null-t ad vissza!
        /// </summary>
        public string Vizsgálat_Terméknév(string _termékkód)
        {
            string value=null;
            marillenconnection.Open();
            SqlCommand command = new SqlCommand("SELECT cikkek.name FROM cikkek WHERE (item_nr ='" + _termékkód + "01" +  "') ORDER BY item_nr");
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

        public bool Vizsgálat_Hozzáadás(Vizsgálat _vizsgálat)
        {
            string data;
            SqlCommand command;

            laborconnection.Open();

            command = laborconnection.CreateCommand();
            command.CommandText = "INSERT INTO L_VIZSLAP (VITEKO, VIZSSZ, VISARZ, VIHOTI, VINETO, VISZAT, VIMEGR, VIGYEV)" +
                                " VALUES('" + _vizsgálat.azonosító.termékkód + "', '" + _vizsgálat.azonosító.hordószám + "', '" + _vizsgálat.azonosító.sarzs + "', '" + _vizsgálat.azonosító.hordótípus + "', "
                                + _vizsgálat.azonosító.nettó_töltet.ToString().Replace(',', '.') + ", " + _vizsgálat.azonosító.szita_átmérő + ", '" + _vizsgálat.azonosító.megrendelő + "', '"
                                + _vizsgálat.adatok1.gyártási_év[_vizsgálat.adatok1.gyártási_év.Length - 1] + "')";

            try { command.ExecuteNonQuery(); }
            catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> INSERT hiba:\n" + q.Message); }
            finally { command.Dispose(); laborconnection.Close(); }
            if (laborconnection.State != System.Data.ConnectionState.Open) return false;

            data = V(new string[] {Update<string>("VITENE", _vizsgálat.adatok1.terméknév), Update<int>("VIHOKE", _vizsgálat.adatok1.hőkezelés),
                Update<string>("VIMUJE", _vizsgálat.adatok1.műszak_jele), Update<string>("VITOGE", _vizsgálat.adatok1.töltőgép), Update<string>("VISZOR", _vizsgálat.adatok1.szárm_ország),
                Update<string>("VIFAJT", _vizsgálat.adatok1.gyümölcsfajta)});

            if (data != null)
            {
                string where = A(new string[] {Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIZSSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs)});

                command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                try { command.ExecuteNonQuery(); }
                catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #1 hiba:\n" + q.Message); }
                finally {
                    command.Dispose(); laborconnection.Close(); }
            }
            if (laborconnection.State != System.Data.ConnectionState.Open) return false;

            data = V(new string[] {Update<double?>("VIBRIX", _vizsgálat.adatok2.brix), Update<double?>("VICSAV", _vizsgálat.adatok2.citromsav),
                Update<double?>("VIBOSA", _vizsgálat.adatok2.borkősav), Update<double?>("VIPEHA", _vizsgálat.adatok2.ph), Update<double?>("VIBOST", _vizsgálat.adatok2.bostwick),
                Update<int?>("VIASAV", _vizsgálat.adatok2.aszkorbinsav), Update<int?>("VICIAD", _vizsgálat.adatok2.citromsav_adagolás),  Update<int?>("VIMATO", _vizsgálat.adatok2.magtöret),
                Update<int?>("VIFEFE", _vizsgálat.adatok2.feketepont),  Update<int?>("VIFEBA", _vizsgálat.adatok2.barnapont),  Update<string>("VISZIN", _vizsgálat.adatok2.szín),
                Update<string>("VIIZEK", _vizsgálat.adatok2.íz), Update<string>("VIILLA", _vizsgálat.adatok2.illat)});

            if (data != null)
            {
                string where = A(new string[] { Update<string>("VITEKO", _vizsgálat.azonosító.termékkód), Update<string>("VIZSSZ", _vizsgálat.azonosító.hordószám), Update<string>("VISARZ", _vizsgálat.azonosító.sarzs) });

                command = laborconnection.CreateCommand();
                command.CommandText = "UPDATE L_VIZSLAP SET " + data + " WHERE " + where;

                try { command.ExecuteNonQuery(); }
                catch (SqlException q) { MessageBox.Show("Vizsgálat_Hozzáadás -> UPDATE #2 hiba:\n" + q.Message); }
                finally { command.Dispose(); laborconnection.Close(); }
                if (laborconnection.State != System.Data.ConnectionState.Open) return false;
            
            }
            /*
            command = laborconnection.CreateCommand();
            command.CommandText = "UPDATE L_VIZSLAP SET VILEOL= '" + _vizsgálat.adatok3.leoltás + "',VIERDA='" + _vizsgálat.adatok3.értékelés + "',VIOCS1='" + _vizsgálat.adatok3.összcsíra_1 + "',VIOCS2='" + _vizsgálat.adatok3.összcsíra_2 + "',VIELE1='"
                                    + _vizsgálat.adatok3.élesztő_1 + "',VIELE2='" + _vizsgálat.adatok3.élesztő_2 + "',VIPEN1='" + _vizsgálat.adatok3.penész_1 + "',VIPEN2='" + _vizsgálat.adatok3.penész_2 + "',VIEMEJE='" + _vizsgálat.adatok3.megjegyzés + "'" +
                                    " WHERE VITEKO = '" + _vizsgálat.azonosító.termékkód + "' AND VIZSSZ= '" + _vizsgálat.azonosító.hordószám + "' AND VIGYEV= '" + _vizsgálat.azonosító.gyártási_év + "' AND VISARZ= '" + _vizsgálat.azonosító.sarzs + "' AND VIMSSZ= '" + _vizsgálat.azonosító.sorszám + "';";


            try { command.ExecuteNonQuery(); }
            catch (SqlException q) { MessageBox.Show("adat3:" + q.Message); return false; }
            finally { command.Dispose(); }

            command = laborconnection.CreateCommand();
            command.CommandText = "UPDATE L_VIZSLAP SET VIMTS1= '" + _vizsgálat.adatok4.címzett_t + "',VIMTD1='" + _vizsgálat.adatok4.dátum_t + "',VIMKS1='" + _vizsgálat.adatok4.címzett_k1 + "',VIMKD1='" + _vizsgálat.adatok4.dátum_k1 + "',VIMKS2='"
                                    + _vizsgálat.adatok4.címzett_k2 + "',VIMKD2='" + _vizsgálat.adatok4.dátum_k2 + "',VIMKS3='" + _vizsgálat.adatok4.címzett_k3 + "',VIMKD3='" + _vizsgálat.adatok4.dátum_k3 + "',VIMKS4='"
                                    + _vizsgálat.adatok4.címzett_k4 + "',VIMKD4='" + _vizsgálat.adatok4.dátum_k4 + "',VIMKS5='" + _vizsgálat.adatok4.címzett_k5 + "',VIMKD5='" + _vizsgálat.adatok4.dátum_k5 + "',VIMKS6='"
                                    + _vizsgálat.adatok4.címzett_k6 + "',VIMKD6='" + _vizsgálat.adatok4.dátum_k6 + "',VILABO='" + _vizsgálat.adatok4.laboros + "'" +
                                    " WHERE VITEKO = '" + _vizsgálat.azonosító.termékkód + "' AND VIZSSZ= '" + _vizsgálat.azonosító.hordószám + "' AND VIGYEV= '" + _vizsgálat.azonosító.gyártási_év + "' AND VISARZ= '" + _vizsgálat.azonosító.sarzs + "' AND VIMSSZ= '" + _vizsgálat.azonosító.sorszám + "';";

            try { command.ExecuteNonQuery(); }
            catch (SqlException q) { MessageBox.Show("adat4:" + q.Message); return false; }
            finally { command.Dispose(); laborconnection.Close(); }*/

            return true;
        }

        public bool Vizsgálat_Módosítás(Vizsgálat _eredeti, Vizsgálat _új)
        {
            return true;
        }
        #endregion

        #region Foglalások
        public List<Foglalás> Foglalások()
        {
            List<Foglalás> data = new List<Foglalás>();
            return data;
        }

        public bool Foglalás_Hozzáadás(Foglalás _foglalás )
        {
            return true;
        }

        public bool Foglalás_Módosítás(Foglalás _eredeti , Foglalás _új)
        {
            return true;
        }

        public bool Foglalás_Törlés(Foglalás _azonosító)
        {
            return true;
        }

        public List<Hordó> Foglalás_Hordók(Foglalás _foglalás)
        {
            List<Hordó> value = new List<Hordó>();
            return value;
        }

        public List<Sarzs> Sarzsok(Vizsgalap_Szűrő _szűrő)
        {
            List<Sarzs> value = new List<Sarzs>();
            return value;
        }

        public List<Hordó> Hordók(Vizsgalap_Szűrő _szűrő, Sarzs _sarzs)
        {
            List<Hordó> value = new List<Hordó>();
            return value;
        }

        #endregion
        
        #region Keresés


        #endregion
    }
}
