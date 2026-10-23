using System;

namespace Tetris.GameSetting
{
    public static class AudioSettings
    {
        private static bool musicEnabled = true;
        private static bool effectsEnabled = true;
        private static int volume = 70;
        public static int Volume
        {
            get
            {
                return volume;
            }
            set
            {
                int next = Math.Max(0, Math.Min(100, value));
                if (volume == next)
                    return;
                volume = next;
                Changed?.Invoke(null, EventArgs.Empty);
            }
        }

        public static event EventHandler Changed;
        public static bool MusicEnabled
        {
            get
            {
                return musicEnabled;
            }
            set
            {
                if (musicEnabled == value)
                    return;
                musicEnabled = value;
                Changed?.Invoke(null, EventArgs.Empty);
            }
        }

        public static bool EffectsEnabled
        {
            get
            {
                return effectsEnabled;
            }
            set
            {
                if (effectsEnabled == value)
                    return;
                effectsEnabled = value;
                Changed?.Invoke(null, EventArgs.Empty);
            }
        }
    }
}
