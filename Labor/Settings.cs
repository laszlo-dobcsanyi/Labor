using System;
using System.IO;

namespace Labor
{
    public static class Settings
    {
        
        public static string server = @".\SQLEXPRESS";
        public static bool integrated_security;

        public static string marillen_database = "marillen2013";
        public static string labor_database = "Labor";

        public static string sql_username = "labor";
        public static string sql_password = "labor";

        public static int    ui_refresh = 5;
        public static string ui_login_name = "";
        public static bool   ui_manual_locations;

        public static string import_directory;
        public static string save_directory;

        public static void Configurate()
        {  
            try
            {
                StreamReader config = new StreamReader("config");

                string line = null;
                while ((line = config.ReadLine()) != null)
                {
                    try
                    {
                        if (0 < line.Length)
                            if (line[0] != '#')
                            {
                                string[] arguments = line.Split('=');

                                switch (arguments[0])
                                {
                                    case "server": server = arguments[1]; break;
                                    case "integrated_security": integrated_security = true; break;
                                    
                                    case "marillen_database": marillen_database = arguments[1]; break;
                                    case "labor_database": labor_database = arguments[1]; break;
                                    
                                    case "sql_username": sql_username = arguments[1]; break;
                                    case "sql_password": sql_password = arguments[1]; break;

                                    case "ui_refresh": ui_refresh = Convert.ToInt32(arguments[1]); break;
                                    case "ui_login_name": ui_login_name = arguments[1]; break;
                                    case "ui_manual_locations": ui_manual_locations = true; break;
                                    case "import": import_directory = arguments[1]; break;
                                    case "save": save_directory = arguments[1]; break;
                                }
                            }
                    }
                    catch { }
                }
                config.Close();
            }
            catch { }
        }

    }
}
