namespace Tetris
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.CheckBox chkMusic;
        private System.Windows.Forms.CheckBox chkEffects;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Button btnBackMenu;
        private System.Windows.Forms.Button btnExitGame;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.chkMusic = new System.Windows.Forms.CheckBox();
            this.chkEffects = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnBackMenu = new System.Windows.Forms.Button();
            this.btnExitGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            this.lblHeading.Location = new System.Drawing.Point(24, 20);
            this.lblHeading.Size = new System.Drawing.Size(480, 40);
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.Maroon;
            this.lblHeading.Text = "CÀI ĐẶT";
            this.lblHelp.Location = new System.Drawing.Point(24, 78);
            this.lblHelp.Size = new System.Drawing.Size(480, 240);
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblHelp.Text = "HƯỚNG DẪN CHƠI\r\n\r\nTrái / Phải: Di chuyển khối.\r\nMũi tên Lên: Xoay khối.\r\nMũi tên Xuống: Rơi nhanh hơn.\r\nEsc: Tạm dừng / Cài đặt.\r\n\r\nXếp đầy hàng ngang để xóa hàng.\r\nXóa 1 / 2 / 3 / 4 hàng: 100 / 300 / 500 / 800 điểm.\r\nKhối mới không còn chỗ xuất hiện thì kết thúc ván.\r\nĐóng bảng xếp hạng, chọn CHƠI NGAY để chơi lại.";
            this.chkMusic.Location = new System.Drawing.Point(24, 314);
            this.chkMusic.Size = new System.Drawing.Size(480, 32);
            this.chkMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.chkMusic.Text = "Bật nhạc nền";
            this.chkMusic.TabIndex = 0;
            this.chkEffects.Location = new System.Drawing.Point(24, 354);
            this.chkEffects.Size = new System.Drawing.Size(480, 32);
            this.chkEffects.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.chkEffects.Text = "Bật âm thanh click, xóa hàng, thua";
            this.chkEffects.TabIndex = 1;
            this.btnClose.Location = new System.Drawing.Point(354, 520);
            this.btnClose.Size = new System.Drawing.Size(150, 42);
            this.btnClose.BackColor = System.Drawing.Color.Orange;
            this.btnClose.Text = "ĐÓNG";
            this.btnClose.TabIndex = 2;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 580);
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt";
            this.CancelButton = this.btnClose;
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Location = new System.Drawing.Point(24, 400);
            this.lblVolume.Size = new System.Drawing.Size(480, 24);
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Location = new System.Drawing.Point(24, 430);
            this.volumeBar.Size = new System.Drawing.Size(480, 50);
            this.volumeBar.Minimum = 0;
            this.volumeBar.Maximum = 100;
            this.volumeBar.TickFrequency = 10;
            this.volumeBar.TabIndex = 2;
            this.btnBackMenu.Name = "btnBackMenu";
            this.btnBackMenu.Text = "VỀ MENU";
            this.btnBackMenu.Location = new System.Drawing.Point(24, 520);
            this.btnBackMenu.Size = new System.Drawing.Size(150, 42);
            this.btnBackMenu.TabIndex = 3;
            this.btnBackMenu.Click += new System.EventHandler(this.btnBackMenu_Click);
            this.btnExitGame.Name = "btnExitGame";
            this.btnExitGame.Text = "THOÁT GAME";
            this.btnExitGame.Location = new System.Drawing.Point(189, 520);
            this.btnExitGame.Size = new System.Drawing.Size(150, 42);
            this.btnExitGame.TabIndex = 4;
            this.btnExitGame.Click += new System.EventHandler(this.btnExitGame_Click);
            this.btnClose.TabIndex = 5;
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.btnBackMenu);
            this.Controls.Add(this.btnExitGame);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.chkMusic);
            this.Controls.Add(this.chkEffects);
            this.Controls.Add(this.btnClose);
            this.ResumeLayout(false);
        }
    }
}
