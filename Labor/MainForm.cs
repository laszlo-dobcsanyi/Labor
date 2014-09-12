using System;
using System.Drawing;
using System.Windows.Forms;

namespace Labor
{
    public class DataToken<T>
    {
        public enum TokenType
        {
            FOUND,
            NOT_FOUND,
            NEW
        }
        
        public T data;
        public TokenType type;

        public DataToken(T _data) { data = _data; type = TokenType.NEW; }
        public DataToken(T _data, TokenType _type) { data = _data; type = _type; }
    }

    public partial class MainForm : Form
    {
        public Timer refresher;

        public Panel_Törzsadatok    törzsadatok_panel;
        public Panel_Vizsgálatok    vizsgálatok_panel;
        public Panel_Foglalások     foglalások_panel;
        public Panel_Konszignáció   konszignáció_panel;
        public Panel_Kiszállítások  kiszállítások_panel;
        public Panel_Felhasználók   felhasználók_panel;

        private TabControl          menu;
        private StatusStrip         status;

        #region Constructor
        public MainForm()
        {
            InitializeForm();
            InitializeContent();

            refresher = new Timer();
            refresher.Interval = Settings.RefreshTime * 1000;
            refresher.Tick += Refresher_Elapsed;
            refresher.Start();
        }

        private void InitializeForm()
        {
            ClientSize = new Size(1024, 768);
            MinimumSize = ClientSize;
            Text = "Labor";
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeContent()
        {
            #region Status
            ToolStripStatusLabel Készítők = new ToolStripStatusLabel("Belinyák Nándor és Társai.");
            Készítők.BorderSides = ToolStripStatusLabelBorderSides.Left;

            status = new StatusStrip();
            status.Items.Add(new ToolStripStatusLabel("Marillen"));
            status.Items.Add(Készítők);
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
            menu.Padding = new Point(18, 5);
            menu.Dock = DockStyle.Fill;
            #endregion

            Controls.Add(status);
            Controls.Add(menu);
            menu.BringToFront();
        }
        #endregion

        #region Events
        /// <summary>
        /// Aktuális panel nevének sötétítése a menüsorban.
        /// </summary>
        void menu_DrawItem(object _sender, DrawItemEventArgs _event)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            if (Convert.ToBoolean(_event.State & DrawItemState.Selected))
                _event.Graphics.DrawString(menu.TabPages[_event.Index].Text, new Font(menu.Font.Name, menu.Font.Size + 1, FontStyle.Bold), new SolidBrush(Color.Black), menu.GetTabRect(_event.Index), sf);
            else
                _event.Graphics.DrawString(menu.TabPages[_event.Index].Text, _event.Font, new SolidBrush(Color.Black), menu.GetTabRect(_event.Index), sf);
        }
        #endregion

        #region Refresh
        private void Refresher_Elapsed(object _sender, EventArgs _event)
        {
            RefreshData();
        }

        public void RefreshData()
        {
            menu.SelectedTab.Controls[0].Refresh();
            refresher.Start();
        }
        #endregion

        #region Segédfüggvények
        public T? ConvertOrDie<T>(string _text) where T : struct, IComparable
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

        public string ConvertOrDieString(string _text)
        {
            if (_text == "") return null;
            return _text;
        }

        public Label createlabel(string _szöveg, int _x, int _y, Form _form)
        {
            Label label = new Label();
            label.Text = _szöveg;
            label.Location = new Point(_x, _y);
            label.AutoSize = true;
            _form.Controls.Add(label);
            return label;
        }

        public TextBox createtextbox(int _x, int _y, int _maxlength, int _width, Form _form)
        {
            TextBox box = new TextBox();
            box.Location = new Point(_x, _y - 3);
            box.MaxLength = _maxlength;
            box.Width = _width;
            box.CharacterCasing = CharacterCasing.Upper;
            _form.Controls.Add(box);
            return box;
        }
        public TextBox createtextbox(string _text, int _x, int _y, int _maxlength, int _width, Form _form)
        {
            TextBox box = new TextBox();
            box.Text = _text;
            box.Location = new Point(_x, _y - 3);
            box.MaxLength = _maxlength;
            box.Width = _width;
            box.CharacterCasing = CharacterCasing.Upper;
            _form.Controls.Add(box);
            return box;
        }

        public ComboBox createcombobox(int _x, int _y, int _width,Form _form)
        {
            ComboBox combo = new ComboBox();
            combo.Location = new Point(_x, _y);
            combo.Width = _width;
            combo.DropDownStyle = ComboBoxStyle.DropDownList;
            _form.Controls.Add(combo);
            return combo;
        }
        #endregion
    }
}
