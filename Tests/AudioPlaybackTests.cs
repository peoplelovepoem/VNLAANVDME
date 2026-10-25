using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Tetris;
using Tetris.GameSetting;

internal static class AudioPlaybackTests
{
    [DllImport("winmm.dll", CharSet = CharSet.Unicode)]
    private static extern int mciSendString(string command, StringBuilder value, int length, IntPtr callback);
    private static int checks;
    private static void CheckMode(string expected)
    {
        var value = new StringBuilder(64);
        int error = mciSendString("status tetrisMusic mode", value, 64, IntPtr.Zero);
        if (error != 0 || value.ToString() != expected)
            throw new Exception("Expected " + expected + ", got " + value + ", error " + error);
        checks++;
    }
    [STAThread]
    private static void Main()
    {
        Type audio = typeof(SettingsForm).Assembly.GetType("Tetris.GameSetting.AudioPlayback");
        Action<string> call = name => audio.GetMethod(name).Invoke(null, null);
        try
        {
            call("Start");
            CheckMode("playing");
            audio.GetMethod("UseGameMusic").Invoke(null, new object[] { true });
            CheckMode("playing");
            AudioSettings.MusicEnabled = false;
            audio.GetMethod("UseGameMusic").Invoke(null, new object[] { false });
            AudioSettings.MusicEnabled = true;
            CheckMode("playing");
            using (var settings = new SettingsForm())
            {
                var flags = BindingFlags.NonPublic | BindingFlags.Instance;
                var music = (CheckBox)typeof(SettingsForm).GetField("chkMusic", flags).GetValue(settings);
                music.Checked = false;
                CheckMode("stopped");
                music.Checked = true;
                CheckMode("playing");
                foreach (string file in new[] { "ui_click.wav", "ClearLine.wav", "Lose.wav" })
                {
                    audio.GetMethod("PlayEffect").Invoke(null, new object[] { file });
                    CheckMode("playing");
                }
                AudioSettings.Volume = 23;
                foreach (string alias in new[] { "tetrisMusic", "tetrisEffect" })
                {
                    var level = new StringBuilder(64);
                    int err = mciSendString("status " + alias + " volume", level, 64, IntPtr.Zero);
                    int actual;
                    if (err != 0 || !int.TryParse(level.ToString(), out actual) || Math.Abs(actual - 230) > 2) throw new Exception("Volume not applied: " + alias + " " + level + " error " + err);
                    checks++;
                }
                AudioSettings.EffectsEnabled = false;
                CheckMode("playing");
                AudioSettings.MusicEnabled = false;
                CheckMode("stopped");
            }
        }
        finally { call("Stop"); }
        var result = new StringBuilder(64);
        if (mciSendString("status tetrisMusic mode", result, 64, IntPtr.Zero) == 0)
            throw new Exception("Music device was not closed");
        Console.WriteLine("PASS: " + (checks + 1) + " audio checks");
    }
}
