using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Tetris.GameSetting
{
    // Separate MCI devices let music and sound effects play at the same time.
    internal static class AudioPlayback
    {
        private static bool initialized;
        private static bool opened;
        private static string musicFile = "msbg.wav";
        private static bool effectOpened;
        private static bool warned;
        [DllImport("winmm.dll", CharSet = CharSet.Unicode)]
        private static extern int mciSendString(string command, StringBuilder result, int length, IntPtr callback);
        [DllImport("winmm.dll", CharSet = CharSet.Unicode)]
        private static extern bool mciGetErrorString(int code, StringBuilder text, int length);

        private static void Send(string command)
        {
            int code = mciSendString(command, null, 0, IntPtr.Zero);
            if (code == 0)
                return;
            var message = new StringBuilder(256);
            mciGetErrorString(code, message, message.Capacity);
            throw new InvalidOperationException(message.ToString());
        }

        public static void Start()
        {
            if (initialized)
                return;
            initialized = true;
            AudioSettings.Changed += ApplySettings;
            ApplySettings(null, EventArgs.Empty);
        }

        public static void UseGameMusic(bool playing)
        {
            string next = playing ? "music_game.wav" : "msbg.wav";
            if (next == musicFile)
                return;
            if (opened)
            {
                mciSendString("close tetrisMusic", null, 0, IntPtr.Zero);
                opened = false;
            }
            musicFile = next;
            if (initialized)
                ApplySettings(null, EventArgs.Empty);
        }

        private static void ApplySettings(object sender, EventArgs e)
        {
            try
            {
                if (effectOpened)
                {
                    Send("setaudio tetrisEffect volume to " + AudioSettings.Volume * 10);
                    if (!AudioSettings.EffectsEnabled)
                        Send("stop tetrisEffect");
                }
                if (!AudioSettings.MusicEnabled)
                {
                    if (opened)
                        Send("stop tetrisMusic");
                    return;
                }
                if (!opened)
                {
                    string path = Path.Combine(Application.StartupPath, "Sounds", musicFile);
                    if (!File.Exists(path))
                        throw new FileNotFoundException("Thiếu file nhạc nền", path);
                    Send("open \"" + path + "\" type mpegvideo alias tetrisMusic");
                    opened = true;
                }
                Send("setaudio tetrisMusic volume to " + AudioSettings.Volume * 10);
                Send("play tetrisMusic repeat");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                if (!warned)
                {
                    warned = true;
                    MessageBox.Show("Không thể phát nhạc nền: " + ex.Message,
                        "Âm thanh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public static void PlayEffect(string file)
        {
            if (!AudioSettings.EffectsEnabled)
                return;
            try
            {
                string path = Path.Combine(Application.StartupPath, "Sounds", file);
                if (!File.Exists(path))
                    return;
                if (effectOpened)
                {
                    mciSendString("close tetrisEffect", null, 0, IntPtr.Zero);
                    effectOpened = false;
                }
                Send("open \"" + path + "\" type mpegvideo alias tetrisEffect");
                effectOpened = true;
                Send("setaudio tetrisEffect volume to " + AudioSettings.Volume * 10);
                Send("play tetrisEffect from 0");
            }
            catch (Exception ex) { Debug.WriteLine(ex); }
        }

        public static void Stop()
        {
            AudioSettings.Changed -= ApplySettings;
            if (effectOpened)
                mciSendString("close tetrisEffect", null, 0, IntPtr.Zero);
            effectOpened = false;
            if (opened)
                mciSendString("close tetrisMusic", null, 0, IntPtr.Zero);
            opened = false;
            initialized = false;
        }
    }
}
