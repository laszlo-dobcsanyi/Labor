using System;
using System.Windows.Forms;

namespace Labor
{
    public static class Program
    {
        private static Timer    refresher;
        public static Database  database;
        public static MainForm  mainform;

        [STAThread]
        public static void Main()
        {
            Settings.Configurate();

            database = new Database();
            mainform = new MainForm();
            
            refresher = new Timer();
            refresher.Interval = Settings.RefreshTime * 1000;
            refresher.Tick += Refresher_Elapsed;
            refresher.Start();

            Application.Run(mainform);
        }

        #region Refresh
        private static void Refresher_Elapsed(object _sender, EventArgs _event)
        {
            RefreshData();
        }

        public static void RefreshData()
        {
            Form.ActiveForm.Refresh();
            refresher.Start();
        }
        #endregion
    }
}
