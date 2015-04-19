using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Labor
{
    public partial class MainForm : Form
    {
        #region Declaration
        public Panel_Törzsadatok    törzsadatok_panel;
        public Panel_Vizsgálatok    vizsgálatok_panel;
        public Panel_Foglalások     foglalások_panel;
        public Panel_Konszignáció   konszignáció_panel;
        public Panel_Kiszállítások  kiszállítások_panel;
        public Panel_Felhasználók   felhasználók_panel;

        private TabControl          menu;
        private StatusStrip         status;
        #endregion

        #region Constructor
        public MainForm()
        {
            InitializeForm();
            InitializeContent();
        }

        private void InitializeForm()
        {
            ClientSize = new Size(1024, 768);
            MinimumSize = ClientSize;
            Text = "Labor";
            StartPosition = FormStartPosition.CenterScreen;
            try
            {
                Icon = new Icon("Labor.ico");
            }
            catch
            {

            }

            KeyPreview = true;
            KeyDown += MainForm_KeyDown;
        }

        private void InitializeContent()
        {
            #region Status
            ToolStripStatusLabel Készítők = new ToolStripStatusLabel("Belinyák Nándor és Társai. \u00A9 2014");
            Készítők.BorderSides = ToolStripStatusLabelBorderSides.Left;

            ToolStripStatusLabel Verzió = new ToolStripStatusLabel("Verzió: 0.5.6-2015.04.19");
            Verzió.BorderSides = ToolStripStatusLabelBorderSides.Left;

            ToolStripStatusLabel Bejelentkezve = new ToolStripStatusLabel("Bejelentkezve " + Program.felhasználó.Value.név1 + " néven." );
            Bejelentkezve.BorderSides = ToolStripStatusLabelBorderSides.Left;

            status = new StatusStrip();
            status.Items.Add(new ToolStripStatusLabel("Marillen Gyümölcsfeldolgozó Kft."));
            status.Items.Add(Készítők);
            status.Items.Add(Verzió);
            status.Items.Add(Bejelentkezve);
            #endregion

            #region Menü
            TabPage Törzsadatok = new TabPage("Törzsadatok");
            törzsadatok_panel = new Panel_Törzsadatok();
            törzsadatok_panel.Dock = DockStyle.Fill;
            Törzsadatok.Controls.Add(törzsadatok_panel);

            TabPage Vizsgálatok = new TabPage("Vizsgálatok");
            vizsgálatok_panel = new Panel_Vizsgálatok();
            vizsgálatok_panel.Dock = DockStyle.Fill;
            Vizsgálatok.Controls.Add(vizsgálatok_panel);

            TabPage Foglalások = new TabPage("Foglalások");
            foglalások_panel = new Panel_Foglalások();
            foglalások_panel.Dock = DockStyle.Fill;
            Foglalások.Controls.Add(foglalások_panel);

            TabPage Konszignáció = new TabPage("Konszignáció");
            konszignáció_panel = new Panel_Konszignáció();
            konszignáció_panel.Dock = DockStyle.Fill;
            Konszignáció.Controls.Add(konszignáció_panel);

            TabPage Kiszállítások = new TabPage("Kiszállítások");
            kiszállítások_panel = new Panel_Kiszállítások();
            kiszállítások_panel.Dock = DockStyle.Fill;
            Kiszállítások.Controls.Add(kiszállítások_panel);

            TabPage Felhasználók = new TabPage("Felhasználók");
            felhasználók_panel = new Panel_Felhasználók();
            felhasználók_panel.Dock = DockStyle.Fill;
            Felhasználók.Controls.Add(felhasználók_panel);
            //

            menu = new TabControl();

            menu.TabPages.Add(Törzsadatok);
            menu.TabPages.Add(Vizsgálatok);
            menu.TabPages.Add(Foglalások);
            menu.TabPages.Add(Konszignáció);
            menu.TabPages.Add(Kiszállítások);
            menu.TabPages.Add(Felhasználók);

            menu.DrawItem += menu_DrawItem;
            menu.DrawMode = TabDrawMode.OwnerDrawFixed;
            menu.Selected += menu_Selected;
            menu.Padding = new Point(18, 5);
            menu.Dock = DockStyle.Fill;
            #endregion

            Controls.Add(status);
            Controls.Add(menu);
            menu.BringToFront();
        }
        #endregion

        #region Events
        public override void Refresh()
        {
            menu.SelectedTab.Controls[0].Refresh();

            base.Refresh();
        }
        /// <summary>
        /// Aktuális panel nevének sötétítése a menüsorban.
        /// </summary>
        private void menu_DrawItem(object _sender, DrawItemEventArgs _event)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            if (Convert.ToBoolean(_event.State & DrawItemState.Selected))
                _event.Graphics.DrawString(menu.TabPages[_event.Index].Text, new Font(menu.Font.Name, menu.Font.Size + 1, FontStyle.Bold), new SolidBrush(Color.Black), menu.GetTabRect(_event.Index), sf);
            else
                _event.Graphics.DrawString(menu.TabPages[_event.Index].Text, _event.Font, new SolidBrush(Color.Black), menu.GetTabRect(_event.Index), sf);
        }

        private void menu_Selected(object _sender, EventArgs _event)
        {
            Program.RefreshData();
        }

        private void MainForm_KeyDown(object _sender, KeyEventArgs _event)
        {
            if (_event.KeyCode == Keys.F1) { menu.SelectedIndex = 0; return; }
            if (_event.KeyCode == Keys.F2) { menu.SelectedIndex = 1; return; }
            if (_event.KeyCode == Keys.F3) { menu.SelectedIndex = 2; return; }
            if (_event.KeyCode == Keys.F4) { menu.SelectedIndex = 3; return; }
            if (_event.KeyCode == Keys.F5) { menu.SelectedIndex = 4; return; }
            if (_event.KeyCode == Keys.F6) { menu.SelectedIndex = 5; return; }
        }
        #endregion

        #region Segédfüggvények
        public static T? ConvertOrDie<T>(string _text) where T : struct, IComparable
        {
            if (_text == "") return null;
            try
            {
                return (T)Convert.ChangeType(_text, typeof(T));
            }
            catch
            {
                return null;
            }
        }

        public static void OnlyNumber(object sender, KeyPressEventArgs e)
        {
            // TODO nem az igazi!!
            if (Char.IsLetter(e.KeyChar) || e.KeyChar == '.')
                e.Handled = true;
        }

        public static void OnlyDate(object _sender, EventArgs _event)
        {
            TextBox box = (TextBox)_sender;

            if (box.Text.Length == 0) return;

            DateTime dt;
            if (!DateTime.TryParseExact(box.Text, "yy.MM.dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) { box.Focus(); return; }
        }

        public static void OnlyTime(object _sender, EventArgs _event)
        {
            TextBox box = (TextBox)_sender;

            if (box.Text.Length == 0) return;

            DateTime dt;
            if (!DateTime.TryParseExact(box.Text, "MMdd HHmm", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) { box.Focus(); return; }
        }

        public static string ConvertOrDieSQLString(string _text)
        {
            if (_text == "" || !Database.IsCorrectSQLText(_text)) return null;
            return _text;
        }

        public static Label createlabel(string _szöveg, int _x, int _y, Form _form)
        {
            Label label = new Label();
            label.AutoSize = true;
            label.Text = _szöveg;
            label.Location = new Point(_x, _y);
            _form.Controls.Add(label);
            return label;
        }

        public static TextBox createtextbox(int _x, int _y, int _maxlength, int _width, Form _form, CharacterCasing _casing = CharacterCasing.Upper)
        {
            TextBox box = new TextBox();
            box.Location = new Point(_x, _y - 3);
            box.MaxLength = _maxlength;
            box.Width = _width;
            box.CharacterCasing = _casing;
            _form.Controls.Add(box);
            return box;
        }
        public static TextBox createtextbox(string _text, int _x, int _y, int _maxlength, int _width, Form _form, CharacterCasing _casing = CharacterCasing.Upper)
        {
            TextBox box = new TextBox();
            box.Text = _text;
            box.Location = new Point(_x, _y - 3);
            box.MaxLength = _maxlength;
            box.Width = _width;
            box.CharacterCasing = _casing;
            _form.Controls.Add(box);
            return box;
        }

        public static ComboBox createcombobox(int _x, int _y, int _width, Form _form)
        {
            ComboBox combo = new ComboBox();
            combo.Location = new Point(_x, _y);
            combo.Width = _width;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            _form.Controls.Add(combo);
            return combo;
        }

        public static CheckBox Create_CheckBox(int _x, int _y, Form _form, bool _checked = false)
        {
            CheckBox checkbox = new CheckBox();
            checkbox.AutoSize = true;
            checkbox.Location = new Point(_x, _y);
            checkbox.CheckState = _checked ? CheckState.Checked : CheckState.Unchecked;
            _form.Controls.Add(checkbox);
            return checkbox;
        }
        #endregion
    }

}
