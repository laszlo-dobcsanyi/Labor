using System;
using System.Windows.Forms;

namespace Labor
{
    public static class Program
    {
        public static Database database;
        public static MainForm  mainform;

        [STAThread]
        public static void Main()
        {
            Settings.Configurate();

            database = new Database();
            mainform = new MainForm();

            Application.Run(mainform);
        }
    }
}
