namespace Tetris
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.picBoard = new System.Windows.Forms.PictureBox();
            this.picNextBlock = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.picHold = new System.Windows.Forms.PictureBox();
            this.lblHold = new System.Windows.Forms.Label();
            this.btnGameSettings = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHold)).BeginInit();
            this.SuspendLayout();
            //
            // picBoard
            //
            this.picBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(200)))));
            this.picBoard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.picBoard.Location = new System.Drawing.Point(309, 24);
            this.picBoard.Name = "picBoard";
            this.picBoard.Size = new System.Drawing.Size(282, 562);
            this.picBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoard.TabIndex = 0;
            this.picBoard.TabStop = false;
            this.picBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.picBoard_Paint);
            //
            // picNextBlock
            //
            this.picNextBlock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(200)))));
            this.picNextBlock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picNextBlock.Location = new System.Drawing.Point(609, 54);
            this.picNextBlock.Name = "picNextBlock";
            this.picNextBlock.Size = new System.Drawing.Size(160, 218);
            this.picNextBlock.TabIndex = 1;
            this.picNextBlock.TabStop = false;
            this.picNextBlock.Paint += new System.Windows.Forms.PaintEventHandler(this.picNextBlock_Paint);
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(609, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "KHỐI TIẾP THEO";
            //
            // lblScore
            //
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(200)))));
            this.lblScore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(161, 182);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(98, 26);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "SCORE: 0";
            //
            // picHold
            //
            this.picHold.BackColor = System.Drawing.Color.PapayaWhip;
            this.picHold.Location = new System.Drawing.Point(161, 54);
            this.picHold.Name = "picHold";
            this.picHold.Size = new System.Drawing.Size(130, 104);
            this.picHold.TabIndex = 0;
            this.picHold.TabStop = false;
            this.picHold.Paint += new System.Windows.Forms.PaintEventHandler(this.picHold_Paint);
            //
            //
            //
            // lblHold
            //
            this.lblHold.AutoSize = true;
            this.lblHold.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.lblHold.Location = new System.Drawing.Point(161, 24);
            this.lblHold.Name = "lblHold";
            this.lblHold.Size = new System.Drawing.Size(91, 24);
            this.lblHold.TabIndex = 2;
            this.lblHold.Text = "GIỮ KHỐI";
            //
            // btnGameSettings
            //
            this.btnGameSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGameSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnGameSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGameSettings.FlatAppearance.BorderSize = 0;
            this.btnGameSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGameSettings.BackgroundImage")));
            this.btnGameSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGameSettings.AccessibleName = "Cài đặt";
            this.btnGameSettings.Location = new System.Drawing.Point(855, 551);
            this.btnGameSettings.Name = "btnGameSettings";
            this.btnGameSettings.Size = new System.Drawing.Size(48, 48);
            this.btnGameSettings.TabIndex = 3;
            this.btnGameSettings.Text = "";
            this.btnGameSettings.UseVisualStyleBackColor = false;
            this.btnGameSettings.Click += new System.EventHandler(this.btnGameSettings_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(919, 611);
            this.Controls.Add(this.picHold);
            this.Controls.Add(this.lblHold);
            this.Controls.Add(this.btnGameSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picNextBlock);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.picBoard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picBoard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picNextBlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHold;
        private System.Windows.Forms.Label lblHold;
        private System.Windows.Forms.Button btnGameSettings;
        private System.Windows.Forms.PictureBox picBoard;
        private System.Windows.Forms.PictureBox picNextBlock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblScore;
    }
}

