namespace Tetris
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHeading;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblAbout;
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
            this.lblAbout = new System.Windows.Forms.Label();
            this.chkMusic = new System.Windows.Forms.CheckBox();
            this.chkEffects = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.lblVolume = new System.Windows.Forms.Label();
            this.btnBackMenu = new System.Windows.Forms.Button();
            this.btnExitGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeading
            // 
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblHeading.ForeColor = System.Drawing.Color.Maroon;
            this.lblHeading.Location = new System.Drawing.Point(24, 20);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(480, 40);
            this.lblHeading.TabIndex = 5;
            this.lblHeading.Text = "CÀI ĐẶT";
            // 
            // lblHelp
            // 
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.lblHelp.Location = new System.Drawing.Point(24, 78);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(480, 208);
            this.lblHelp.TabIndex = 6;
            this.lblHelp.Text = "HƯỚNG DẪN CHƠI\r\n\r\nTrái / Phải: Di chuyển khối.\r\nMũi tên Lên: Xoay khối.\r\nMũi tên Xuống: Rơi nhanh hơn.\r\nEsc: Tạm dừng / Cài đặt.\r\n\r\nXếp đầy hàng ngang để xóa hàng.\r\nXóa 1 / 2 / 3 / 4 hàng: 100 / 300 / 500 / 800 điểm.\r\nKhối mới không còn chỗ xuất hiện thì kết thúc ván.\r\nĐóng bảng xếp hạng, chọn CHƠI NGAY để chơi lại.";
            // 
            // lblAbout
            // 
            this.lblAbout.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.lblAbout.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lblAbout.Location = new System.Drawing.Point(24, 294);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(480, 136);
            this.lblAbout.TabIndex = 7;
            this.lblAbout.Text = "THÔNG TIN NHÓM 404\r\nLớp: 2611COMP101901\r\nVũ Thành Công  51.01.104.010\r\nVũ Hoàng N" +
    "guyễn Luân  51.01.104.057\r\nNguyễn Trí Phát  51.01.104.071\r\nNguyễn Trường Sơn  51" +
    ".01.104.086";
            this.lblAbout.Click += new System.EventHandler(this.lblAbout_Click);
            // 
            // chkMusic
            // 
            this.chkMusic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.chkMusic.Location = new System.Drawing.Point(24, 438);
            this.chkMusic.Name = "chkMusic";
            this.chkMusic.Size = new System.Drawing.Size(480, 32);
            this.chkMusic.TabIndex = 0;
            this.chkMusic.Text = "Bật nhạc nền";
            // 
            // chkEffects
            // 
            this.chkEffects.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.chkEffects.Location = new System.Drawing.Point(24, 478);
            this.chkEffects.Name = "chkEffects";
            this.chkEffects.Size = new System.Drawing.Size(480, 32);
            this.chkEffects.TabIndex = 1;
            this.chkEffects.Text = "Bật âm thanh click, xóa hàng, thua";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Orange;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(354, 610);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 42);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "ĐÓNG";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // volumeBar
            // 
            this.volumeBar.Location = new System.Drawing.Point(24, 550);
            this.volumeBar.Maximum = 100;
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(480, 45);
            this.volumeBar.TabIndex = 2;
            this.volumeBar.TickFrequency = 10;
            // 
            // lblVolume
            // 
            this.lblVolume.Location = new System.Drawing.Point(24, 522);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(480, 24);
            this.lblVolume.TabIndex = 0;
            // 
            // btnBackMenu
            // 
            this.btnBackMenu.Location = new System.Drawing.Point(24, 610);
            this.btnBackMenu.Name = "btnBackMenu";
            this.btnBackMenu.Size = new System.Drawing.Size(150, 42);
            this.btnBackMenu.TabIndex = 3;
            this.btnBackMenu.Text = "VỀ MENU";
            this.btnBackMenu.Click += new System.EventHandler(this.btnBackMenu_Click);
            // 
            // btnExitGame
            // 
            this.btnExitGame.Location = new System.Drawing.Point(189, 610);
            this.btnExitGame.Name = "btnExitGame";
            this.btnExitGame.Size = new System.Drawing.Size(150, 42);
            this.btnExitGame.TabIndex = 4;
            this.btnExitGame.Text = "THOÁT GAME";
            this.btnExitGame.Click += new System.EventHandler(this.btnExitGame_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(530, 670);
            this.Controls.Add(this.lblVolume);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.btnBackMenu);
            this.Controls.Add(this.btnExitGame);
            this.Controls.Add(this.lblHeading);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.chkMusic);
            this.Controls.Add(this.chkEffects);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cài đặt";
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
