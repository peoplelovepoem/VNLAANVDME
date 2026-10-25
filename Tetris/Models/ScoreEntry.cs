using System;
namespace Tetris.Models
{
    public class ScoreEntry
    {
        public string PlayerName
        {
            get; set;
        }

        public int Score
        {
            get; set;
        }

        public DateTime DateAchieved
        {
            get; set;
        }

        public int? DurationSeconds
        {
            get; set;
        }

        public string DurationText
        {
            get
            {
                return DurationSeconds.HasValue ? string.Format("{0:00}:{1:00}", DurationSeconds.Value / 60, DurationSeconds.Value % 60) : "—";
            }
        }

        public ScoreEntry(string playerName, int score, int? durationSeconds = null)
        {
            PlayerName = playerName;
            Score = score;
            DurationSeconds = durationSeconds;
            DateAchieved = DateTime.Now;
        }
    }
}
