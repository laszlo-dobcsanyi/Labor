using System;
using System.Windows.Forms;

namespace Labor
{
    public static class Program
    {
        public static Database database;
        public static Felhasználó? felhasználó;

        private static Timer refresher;

        [STAThread]
        public static void Main()
        {
            Settings.Configurate();

            database = new Database();
            felhasználó = null;

            LoginForm loginform = new LoginForm();
            Application.Run(loginform);

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
