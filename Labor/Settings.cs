using System;
using System.IO;

namespace Labor
{
    public static class Settings
    {
        public static string server = ".\\SQLEXPRESS";
        public static string marillen_database = "marillen2013";
        public static string labor_database = "Labor";
        public static bool Logging = false;
        public static int RefreshTime = 10;
        public static string LoginName = "";
        public static bool ManualLocations = false;

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
                                string[] arguments = line.ToLower().Split('=');

                                switch (arguments[0])
                                {
                                    case "server": server = arguments[1]; break;
                                    case "marillen_database": marillen_database = arguments[1]; break;
                                    case "labor_database": labor_database = arguments[1]; break;
                                    case "logging": Logging = true; break;
                                    case "refresh": RefreshTime = Convert.ToInt32(arguments[1]); break;
                                    case "login_name": LoginName = arguments[1]; break;
                                    case "manual_locations": ManualLocations = true; break;
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
