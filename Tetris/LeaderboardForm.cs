using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                dgvLeaderboard.ColumnCount = 3;
                dgvLeaderboard.Columns[0].Name = "Hạng";
                dgvLeaderboard.Columns[1].Name = "Tên Người Chơi";
                dgvLeaderboard.Columns[2].Name = "Điểm Số";

                dgvLeaderboard.Rows.Clear();

                int rank = 1;
                foreach (var entry in scores)
                {
                    dgvLeaderboard.Rows.Add(rank++, entry.PlayerName, entry.Score);
                }
            }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
