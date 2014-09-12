using System;
using System.IO;

namespace Labor
{
    public static class Settings
    {
        public static bool Logging = false;
        public static int RefreshTime = 10;
        public static string LoginName = "";

        public static void Configurate()
        {
            try
            {
                StreamReader config = new StreamReader("config");

                string line = null;
                while ((line = config.ReadLine()) != null)
                {
                    string[] arguments = line.ToLower().Split('=');

                    switch (arguments[0])
                    {
                        case "logging": Logging = true; break;
                        case "refresh": RefreshTime = Convert.ToInt32(arguments[1]); break;
                        case "login_name": LoginName = arguments[1]; break;
                    }
                }
                config.Close();
            }
            catch
            {

            }
        }
    }
}
