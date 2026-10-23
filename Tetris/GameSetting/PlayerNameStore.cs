using System;
using System.IO;
using System.Text;
using Tetris.Models;

namespace Tetris.GameSetting
{
    public sealed class PlayerNameStore
    {
        private readonly string filePath;
        public PlayerNameStore() : this(Path.Combine(Environment.GetFolderPath(
            Environment.SpecialFolder.LocalApplicationData), "Tetris", "player-name.txt"))
        {
        }

        public PlayerNameStore(string path)
        {
            filePath = path;
        }

        public string Load()
        {
            try
            {
                if (!File.Exists(filePath) || new FileInfo(filePath).Length > 1024)
                    return "";
                string name, error;
                return PlayerSession.TryValidateName(File.ReadAllText(filePath,
                    new UTF8Encoding(false, true)), out name, out error) ? name : "";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return "";
            }
        }

        public bool TrySave(string name)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(filePath)));
                File.WriteAllText(filePath, name, new UTF8Encoding(false));
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }
    }
}
