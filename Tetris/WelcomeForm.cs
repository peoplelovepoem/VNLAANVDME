using System;
using System.Windows.Forms;
using Tetris.GameSetting;
using Tetris.Models;

namespace Tetris
{
    public partial class WelcomeForm : Form
    {
        // Keep standard controls in Designer; apply the title outline only at runtime.
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode !=
                System.ComponentModel.LicenseUsageMode.Designtime)
                lblTitle = GameUI.OutlinedLabel.Replace(lblTitle);
            base.OnLoad(e);
        }

        private readonly PlayerSession session;
        private readonly PlayerNameStore store;
        public event EventHandler PlayerAccepted;

        public WelcomeForm() : this(new PlayerSession(), new PlayerNameStore()) { }
        public WelcomeForm(PlayerSession session, PlayerNameStore store)
        {
            this.session = session;
            this.store = store;
            InitializeComponent();
            // Allow the form to run while its controls are being designed manually.
            var txtName = (FindControl<TextBox>("txtName") ?? FindControl<TextBox>("textBox1"));
            if (txtName != null)
            {
                txtName.Text = session.PlayerName ?? store.Load();
                Shown += (s, e) => { txtName.Focus(); txtName.SelectAll(); };
            }
            var btnJoin = (FindControl<Button>("btnJoin") ?? FindControl<Button>("button1"));
            if (btnJoin != null)
            {
                btnJoin.Click -= btnJoin_Click;
                btnJoin.Click += btnJoin_Click;
                AcceptButton = btnJoin;
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var settings = new SettingsForm())
                settings.ShowDialog(this);
        }

        private T FindControl<T>(string name) where T : Control
        {
            var matches = Controls.Find(name, true);
            return matches.Length > 0 ? matches[0] as T : null;
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var txtName = (FindControl<TextBox>("txtName") ?? FindControl<TextBox>("textBox1"));
            var lblError = FindControl<Label>("lblError");
            if (txtName == null)
                return;
            string name, error;
            if (!PlayerSession.TryValidateName(txtName.Text, out name, out error))
            {
                if (lblError != null)
                    lblError.Text = error;
                else
                    MessageBox.Show(this, error, "Tên người chơi");
                txtName.Focus();
                return;
            }
            session.SetName(name);
            if (!store.TrySave(name))
                MessageBox.Show(this, "Bạn vẫn có thể chơi, nhưng tên chưa được nhớ cho lần sau.",
                    "Không thể nhớ tên", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            PlayerAccepted?.Invoke(this, EventArgs.Empty);
        }
    }
}
