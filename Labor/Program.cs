using System;
using System.IO;
using System.Windows.Forms;

namespace Labor
{
    public static class Program
    {
        public static Database database;
        public static FELHASZNALO? felhasználó;

        private static Timer refresher;

        [STAThread]
        public static void Main()
        {
            try
            {
                Settings.Configurate();

                //database = new Database();
                felhasználó = null;
                database = new Database();

                LoginForm loginform = new LoginForm();
                Application.Run(loginform);
                database = new Database();
                if (loginform.felhasználó != null)
                {
                    felhasználó = loginform.felhasználó;
                    loginform.Dispose();

                    refresher = new Timer();
                    refresher.Interval = Settings.ui_refresh * 1000;
                    refresher.Tick += Refresher_Elapsed;
                    refresher.Start();

                    MainForm mainform = new MainForm();
                    Application.Run(mainform);

                    refresher.Dispose();
                    mainform.Dispose();
                }
            }
            catch (Exception _e)
            {
                MessageBox.Show("Kezeletlen globális hiba a program futása során!\nKérem jelezze a hibát a rendszergazdának!\nHiba adatai:\n" + _e.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);

                string file_name = string.Format("crash-{0:yyyy-MM-dd_hh-mm-ss}.data", DateTime.Now);

                try
                {
                    StreamWriter file = new StreamWriter(file_name);
                    file.WriteLine("Message:\t" + _e.Message);
                    file.WriteLine("Source:\t" + _e.Source);
                    file.WriteLine("Data:\t" + _e.Data);
                    file.WriteLine("Stack:\t" + _e.StackTrace);
                    file.Close();
                }
                catch (Exception _ex)
                {
                    MessageBox.Show("További hiba a kivétel mentésekor!\n" + _ex.Message, "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(1);
                }

                MessageBox.Show("A hiba adatait a " + file_name + " nevű file tartalmazza!", "Hiba adatainak elérése", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
        }

        #region Refresh
        private static void Refresher_Elapsed(object _sender, EventArgs _event)
        {
            RefreshData();
        }

        public static void RefreshData()
        {
            if (Form.ActiveForm != null) Form.ActiveForm.Refresh();
            refresher.Start();
        }
        #endregion
    }
}
