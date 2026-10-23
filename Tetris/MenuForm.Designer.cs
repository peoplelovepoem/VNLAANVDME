namespace Tetris
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.BtnHelp = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("UTM Hanzel", 35F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label1.Location = new System.Drawing.Point(298, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 69);
            this.label1.TabIndex = 0;
            this.label1.Text = "TETRIS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnPlay
            // 
            this.BtnPlay.BackColor = System.Drawing.Color.Lime;
            this.BtnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnPlay.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnPlay.Font = new System.Drawing.Font("SVN-Cookies", 19F, System.Drawing.FontStyle.Bold);
            this.BtnPlay.ForeColor = System.Drawing.Color.White;
            this.BtnPlay.Location = new System.Drawing.Point(310, 170);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(182, 55);
            this.BtnPlay.TabIndex = 1;
            this.BtnPlay.Text = "CHƠI NGAY";
            this.BtnPlay.UseVisualStyleBackColor = false;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // BtnHelp
            // 
            this.BtnHelp.BackColor = System.Drawing.Color.Orange;
            this.BtnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnHelp.Font = new System.Drawing.Font("SVN-Cookies", 19F, System.Drawing.FontStyle.Bold);
            this.BtnHelp.ForeColor = System.Drawing.Color.White;
            this.BtnHelp.Location = new System.Drawing.Point(310, 242);
            this.BtnHelp.Name = "BtnHelp";
            this.BtnHelp.Size = new System.Drawing.Size(182, 55);
            this.BtnHelp.TabIndex = 2;
            this.BtnHelp.Text = "HƯỚNG DẪN";
            this.BtnHelp.UseVisualStyleBackColor = false;
            this.BtnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.BackColor = System.Drawing.Color.Red;
            this.BtnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.BtnExit.Font = new System.Drawing.Font("SVN-Cookies", 19F, System.Drawing.FontStyle.Bold);
            this.BtnExit.ForeColor = System.Drawing.Color.White;
            this.BtnExit.Location = new System.Drawing.Point(310, 314);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(182, 55);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "THOÁT";
            this.BtnExit.UseVisualStyleBackColor = false;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnHelp);
            this.Controls.Add(this.BtnPlay);
            this.Controls.Add(this.label1);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnHelp;
        private System.Windows.Forms.Button BtnExit;
        public System.Windows.Forms.Button BtnPlay;
    }
}