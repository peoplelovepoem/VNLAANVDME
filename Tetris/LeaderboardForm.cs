using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class LeaderboardForm : Form
    {
        public LeaderboardForm()
        {
            InitializeComponent();
            LoadLeaderboardData();
        }

        private void LoadLeaderboardData()
        {
            var scores = LeaderboardManager.LoadScores();

            // Định cấu hình DataGridView hiển thị đẹp mắt hơn
            dgvLeaderboard.ReadOnly = true;
            dgvLeaderboard.AllowUserToAddRows = false;
            dgvLeaderboard.AllowUserToDeleteRows = false;
            dgvLeaderboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLeaderboard.RowHeadersVisible = false;
            dgvLeaderboard.ColumnCount = 4;
            dgvLeaderboard.Columns[0].Name = "Hạng";
            dgvLeaderboard.Columns[1].Name = "Tên Người Chơi";
            dgvLeaderboard.Columns[2].Name = "Điểm Số";

            dgvLeaderboard.Columns[3].Name = "Thời gian";
            foreach (DataGridViewColumn column in dgvLeaderboard.Columns)
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvLeaderboard.Rows.Clear();

            int rank = 1;
            foreach (var entry in scores)
            {
                dgvLeaderboard.Rows.Add(rank++, entry.PlayerName, entry.Score, entry.DurationText);
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
