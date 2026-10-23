using System;
using System.Windows.Forms;

namespace Tetris
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameSetting.AudioPlayback.Start();
            try
            {
                Application.Run(new TetrisApplicationContext());
            }
            finally { GameSetting.AudioPlayback.Stop(); }

        }
    }
}
