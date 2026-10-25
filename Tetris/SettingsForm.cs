using System;
using System.Windows.Forms;
using Tetris.GameSetting;
namespace Tetris
{
    public enum GameSettingsAction
    {
        Resume, Menu, Exit
    }
    public partial class SettingsForm : Form
    {
        public GameSettingsAction Action
        {
            get; private set;
        }

        public SettingsForm() : this(false) { }
        public SettingsForm(bool inGame)
        {
            InitializeComponent();
            btnBackMenu.Visible = inGame;
            btnExitGame.Visible = inGame;
            btnClose.Text = inGame ? "TIẾP TỤC" : "ĐÓNG";
            volumeBar.Value = AudioSettings.Volume;
            lblVolume.Text = "Âm lượng: " + volumeBar.Value + "%";
            volumeBar.ValueChanged += (s, e) =>
            {
                AudioSettings.Volume = volumeBar.Value;
                lblVolume.Text = "Âm lượng: " + volumeBar.Value + "%";
            };
            chkMusic.Checked = AudioSettings.MusicEnabled;
            chkEffects.Checked = AudioSettings.EffectsEnabled;
            chkMusic.CheckedChanged += (s, e) => AudioSettings.MusicEnabled = chkMusic.Checked;
            chkEffects.CheckedChanged += (s, e) => AudioSettings.EffectsEnabled = chkEffects.Checked;
        }

        private void btnBackMenu_Click(object sender, EventArgs e)
        {
            Action = GameSettingsAction.Menu;
            Close();
        }

        private void btnExitGame_Click(object sender, EventArgs e)
        {
            Action = GameSettingsAction.Exit;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
