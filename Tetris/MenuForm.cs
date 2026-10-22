using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            // Thiết lập cơ bản cho cửa sổ Menu
            this.Text = "Tetris - Màn Hình Chính";
            this.Size = new Size(935, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 224, 192);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // 1. Vẽ Tên Game
            Label lblTitle = new Label();
            lblTitle.Text = "T E T R I S";
            lblTitle.Font = new Font("Consolas", 40, FontStyle.Bold);
            lblTitle.ForeColor = Color.DarkBlue;
            lblTitle.AutoSize = false;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 200;

            // 2. Tạo Nút Chơi Game
            Button btnPlay = new Button();
            btnPlay.Text = "CHƠI NGAY";
            btnPlay.Font = new Font("Arial", 16, FontStyle.Bold);
            btnPlay.Size = new Size(220, 60);
            btnPlay.Location = new Point((this.ClientSize.Width - 220) / 2, 200);
            btnPlay.BackColor = Color.LimeGreen;
            btnPlay.ForeColor = Color.White;
            btnPlay.FlatStyle = FlatStyle.Popup;
            btnPlay.Cursor = Cursors.Hand;
            btnPlay.Click += BtnPlay_Click;

            // 3. Tạo Nút Hướng Dẫn
            Button btnHelp = new Button();
            btnHelp.Text = "HƯỚNG DẪN";
            btnHelp.Font = new Font("Arial", 16, FontStyle.Bold);
            btnHelp.Size = new Size(220, 60);
            btnHelp.Location = new Point((this.ClientSize.Width - 220) / 2, 280);
            btnHelp.BackColor = Color.Orange;
            btnHelp.ForeColor = Color.White;
            btnHelp.FlatStyle = FlatStyle.Popup;
            btnHelp.Cursor = Cursors.Hand;
            btnHelp.Click += BtnHelp_Click;

            // 4. Tạo Nút Thoát
            Button btnExit = new Button();
            btnExit.Text = "THOÁT";
            btnExit.Font = new Font("Arial", 16, FontStyle.Bold);
            btnExit.Size = new Size(220, 60);
            btnExit.Location = new Point((this.ClientSize.Width - 220) / 2, 360);
            btnExit.BackColor = Color.Crimson;
            btnExit.ForeColor = Color.White;
            btnExit.FlatStyle = FlatStyle.Popup;
            btnExit.Cursor = Cursors.Hand;
            btnExit.Click += BtnExit_Click;

            // Đưa tất cả các thành phần vừa tạo lên màn hình
            this.Controls.Add(btnPlay);
            this.Controls.Add(btnHelp);
            this.Controls.Add(btnExit);
            this.Controls.Add(lblTitle);
        }

        // --- CÁC SỰ KIỆN KHI BẤM NÚT ---

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(); // Tạo trang trò chơi
            this.Hide();                  // Tạm thời giấu trang Menu đi
            gameForm.ShowDialog();        // Hiển thị trang trò chơi lên
            this.Show();                  // Khi người chơi tắt trang trò chơi (nhấn dấu X), hiện lại trang Menu
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Mũi tên Trái/Phải: Di chuyển gạch sang 2 bên\n\n" +
                "Mũi tên Lên: Xoay khối gạch\n\n" +
                "Mũi tên Xuống: Cho gạch rơi nhanh hơn\n\n" +
                "Enter: Chơi lại từ đầu khi thua",
                "Hướng dẫn chơi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Thoát hẳn chương trình
        }
    }
}