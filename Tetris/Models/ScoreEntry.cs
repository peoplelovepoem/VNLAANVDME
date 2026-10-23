using System;

namespace Tetris.Models
{
    public class ScoreEntry
    {
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public DateTime DateAchieved { get; set; }

        public ScoreEntry(string playerName, int score)
        {
            PlayerName = playerName;
            Score = score;
            DateAchieved = DateTime.Now;
        }
    }
}
