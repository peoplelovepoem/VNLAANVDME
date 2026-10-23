using System;
using System.Windows.Forms;
using Tetris.GameSetting;
using Tetris.Models;

namespace Tetris
{
    internal sealed class TetrisApplicationContext : ApplicationContext
    {
        private readonly PlayerSession session = new PlayerSession();
        private readonly PlayerNameStore store = new PlayerNameStore();
        public TetrisApplicationContext()
        {
            ShowWelcome();
        }

        private void SwitchTo(Form next)
        {
            Form previous = MainForm;
            // Setting MainForm detaches ApplicationContext's old FormClosed handler.
            MainForm = next;
            if (previous != null)
            {
                previous.Close();
                previous.Dispose();
            }
            next.Show();
        }

        private void ShowWelcome()
        {
            var welcome = new WelcomeForm(session, store);
            welcome.PlayerAccepted += (s, e) => ShowMenu();
            SwitchTo(welcome);
        }

        private void ShowMenu()
        {
            var menu = new MenuForm(session);
            menu.ChangeNameRequested += (s, e) => ShowWelcome();
            SwitchTo(menu);
        }
    }
}
