using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tetris.Models;

namespace Tetris
{
    public static class LeaderboardManager
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leaderboard.txt");
        private const int MaxEntries = 10;

        // ĐỌC dữ liệu từ file .txt
        public static List<ScoreEntry> LoadScores()
        {
            var scores = new List<ScoreEntry>();

            if (!File.Exists(FilePath))
            {
                return scores;
            }

            try
            {
                // Đọc từng dòng trong file .txt
                var lines = File.ReadAllLines(FilePath);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 2)
                    {
                        string name = parts[0];
                        if (int.TryParse(parts[1], out int score))
                        {
                            scores.Add(new ScoreEntry(name, score));
                        }
                    }
                }
            }
            catch
            {
            }

            return scores;
        }

        public static void SaveScore(string playerName, int score)
        {
            var scores = LoadScores();
            scores.Add(new ScoreEntry(playerName, score));

            scores = scores.OrderByDescending(s => s.Score)
                           .Take(MaxEntries)
                           .ToList();

            try
            {
                var lines = new List<string>();
                foreach (var entry in scores)
                {
                    lines.Add($"{entry.PlayerName}|{entry.Score}");
                }

                // Ghi đè toàn bộ danh sách mới vào file .txt
                File.WriteAllLines(FilePath, lines);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Không thể lưu điểm vào file .txt: " + ex.Message);
            }
        }
    }
}
