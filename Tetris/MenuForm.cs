using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenuForm : Form
    {
        // Keep standard controls in Designer; apply the title outline only at runtime.
        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode && System.ComponentModel.LicenseManager.UsageMode !=
                System.ComponentModel.LicenseUsageMode.Designtime)
                label1 = GameUI.OutlinedLabel.Replace(label1);
            base.OnLoad(e);
        }

        private readonly Models.PlayerSession session;
        public event EventHandler ChangeNameRequested;
        public MenuForm() : this(CreateDesignerSession()) { }
        private static Models.PlayerSession CreateDesignerSession()
        {
            var session = new Models.PlayerSession();
            session.SetName("Player");
            return session;
        }

        public MenuForm(Models.PlayerSession session)
        {
            this.session = session;
            InitializeComponent();
            AttachHoverSounds(this);
            lblPlayer.Text = "Người chơi: " + session.PlayerName;
        }

        private void AttachHoverSounds(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Button)
                    control.MouseEnter += MenuOption_MouseEnter;
                if (control.HasChildren)
                    AttachHoverSounds(control);
            }
        }

        private void MenuOption_MouseEnter(object sender, EventArgs e)
        {
            GameSetting.AudioPlayback.PlayEffect("ui_click.wav");
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            ChangeNameRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            GameSetting.AudioPlayback.PlayEffect("ui_click.wav");

            try
            {
                using (Form1 gameForm = new Form1(session))
                {

                    Hide();

                    try
                    {
                        gameForm.ShowDialog();
                    }
                    finally
                    {
                        if (!IsDisposed && !Disposing)
                        {
                            Show();
                            Activate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể mở trò chơi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            GameSetting.AudioPlayback.PlayEffect("ui_click.wav");

            using (var settings = new SettingsForm())
                settings.ShowDialog(this);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            GameSetting.AudioPlayback.PlayEffect("ui_click.wav");
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var leaderboard = new LeaderboardForm())
                leaderboard.ShowDialog(this);
        }
    }
}
