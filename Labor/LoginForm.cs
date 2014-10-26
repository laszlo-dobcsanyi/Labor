using System;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public sealed class LoginForm : Form
    {
        #region Declaration
        public Felhasználó? felhasználó = null;

        private TextBox box_felhasználónév;
        private TextBox box_jelszó;
        private TextBox box_szerver;
        private TextBox box_marillen;
        private TextBox box_labor;
        #endregion

        #region Constructor
        public LoginForm()
        {
            InitializeForm();
            InitializeContent();
            InitializeData();
        }

        private void InitializeForm()
        {
            ClientSize = new Size(400, 300 + 16);
            MaximumSize = ClientSize;
            Text = "Labor Belépés";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
        }

        private void InitializeContent()
        {
            const int offset = 16;
            const int spacer = 32;
            
            string[] labels = new string[] { "Felhasználónév", "Jelszó", "Szerver elérése", "Marillen adatbázis", "Labor adatbázis" };
            for(int current = 0; current < labels.Length; ++current)
            { Label label = MainForm.createlabel(labels[current] + ":", offset, (current < 2 ? current : current + 1) * spacer + offset, this); }
            box_felhasználónév      = MainForm.createtextbox(128 + 32, 0 * spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
            box_jelszó              = MainForm.createtextbox(128 + 32, 1 * spacer + offset, 15, 15 * 8, this, CharacterCasing.Normal);
            box_jelszó.PasswordChar = '*';
            box_szerver             = MainForm.createtextbox(128 + 32, 3 * spacer + offset, 20, 20 * 8, this, CharacterCasing.Normal);
            box_marillen            = MainForm.createtextbox(128 + 32, 4 * spacer + offset, 20, 20 * 8, this, CharacterCasing.Normal);
            box_labor               = MainForm.createtextbox(128 + 32, 5 * spacer + offset, 20, 20 * 8, this, CharacterCasing.Normal);

            Button rendben = new Button();
            rendben.Size = new Size(96, 32);
            rendben.Location = new Point(ClientSize.Width - rendben.Width - spacer, ClientSize.Height - rendben.Height - spacer);
            rendben.Click += rendben_Click;
            rendben.Text = "Rendben";

            Controls.Add(rendben);

        }

        private void InitializeData()
        {
            box_felhasználónév.Text = Settings.ui_login_name;
            box_jelszó.Text = "admin";

            box_szerver.Text = Settings.server;
            box_marillen.Text = Settings.marillen_database;
            box_labor.Text = Settings.labor_database;
        }
        #endregion

        #region EventHandlers
        private void rendben_Click(object _sender, EventArgs _event)
        {
            Settings.server = box_szerver.Text;
            Settings.marillen_database = box_marillen.Text;
            Settings.labor_database = box_labor.Text;

            if (!Database.IsCorrectSQLText(box_felhasználónév.Text)) { MessageBox.Show("Nem megfelelő karakter a felhasználónévben!\n", "Belépési hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            felhasználó = Program.database.Felhasználó(box_felhasználónév.Text);

            if (felhasználó != null)
            {
                if (felhasználó.Value.jelszó == box_jelszó.Text)
                {
                    Close();
                }
                else
                {
                    felhasználó = null;
                }
            }

            if (felhasználó == null)
            {
                MessageBox.Show("A megadott felhasználónév, jelszó párosítás nem megfelelő!\nKérem ellenőrizze őket!", "Belépési hiba!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}
