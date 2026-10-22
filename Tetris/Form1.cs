using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.GameLogic;
using Tetris.GameSetting;
using Tetris.Models;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private Board board = new Board();
        private Block currentBlock;
        private Block nextBlock;
        private Timer gameTimer;
        private Random random = new Random();

        private bool isGameOver = false;
        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Tạo nút Quay về Menu bằng code tự động
            CreateMenuButton();

            InitGame();
        }

        // --- TẠO NÚT QUAY VỀ MENU ---
        private void CreateMenuButton()
        {
            Button btnMenu = new Button();
            btnMenu.Text = "HOME";
            btnMenu.Font = new Font("Arial", 10, FontStyle.Bold);
            btnMenu.Size = new Size(90, 32);
            btnMenu.Location = new Point(10, 10); // Vị trí góc trên bên trái
            btnMenu.BackColor = Color.FromArgb(255, 235, 200);
            btnMenu.ForeColor = Color.FromArgb(30, 30, 30);
            btnMenu.FlatStyle = FlatStyle.Popup;
            btnMenu.Cursor = Cursors.Hand;

            // Xử lý sự kiện khi bấm nút
            btnMenu.Click += (sender, e) =>
            {
                gameTimer?.Stop();
                this.Close(); // Đóng Form1, tự động quay về MenuForm
            };

            this.Controls.Add(btnMenu);
            btnMenu.BringToFront(); // Đưa nút lên lớp trên cùng để không bị che
        }

        public void InitGame()
        {
            board = new Board();
            nextBlock = null;
            isGameOver = false;
            score = 0;
            UpdateScoreDisplay();

            SpawnNewBlock();

            gameTimer = new Timer();
            gameTimer.Interval = 500;
            gameTimer.Tick += GameTick;
            gameTimer.Start();

            picBoard.Invalidate();
            picNextBlock.Invalidate();
        }

        private void UpdateScoreDisplay()
        {
            this.Text = $"Tetris Game - Điểm của bạn: {score}";
            lblScore.Text = "SCORE: " + score;
        }

        private Block CreateRandomBlock()
        {
            int randomID = random.Next(1, 8);
            return new Block(0, 0, randomID);
        }

        private void SpawnNewBlock()
        {
            if (nextBlock == null)
            {
                nextBlock = CreateRandomBlock();
            }

            currentBlock = nextBlock;
            currentBlock.x = 5;
            currentBlock.y = 0;

            nextBlock = CreateRandomBlock();

            picNextBlock.Invalidate();
            picBoard.Invalidate();

            if (board.CheckCollision(currentBlock))
            {
                gameTimer.Stop();
                isGameOver = true;
                picBoard.Invalidate();
            }
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (isGameOver) return;

            currentBlock.y++;
            if (board.CheckCollision(currentBlock))
            {
                currentBlock.y--;
                board.LockBlock(currentBlock);

                int lines = board.ClearLine();
                if (lines > 0)
                {
                    if (lines == 1) score += 100;
                    else if (lines == 2) score += 300;
                    else if (lines == 3) score += 500;
                    else if (lines >= 4) score += 800;

                    UpdateScoreDisplay();
                }

                SpawnNewBlock();
            }
            picBoard.Invalidate();
        }

        private void picBoard_Paint(object sender, PaintEventArgs e)
        {
            if (board == null || currentBlock == null) return;

            Graphics g = e.Graphics;

            for (int r = 0; r < Setting.row; r++)
            {
                for (int c = 0; c < Setting.col; c++)
                {
                    if (board.Grid[r, c] != 0)
                    {
                        DrawCell(g, c, r, board.Grid[r, c]);
                    }
                }
            }

            int size = currentBlock.shape.GetLength(0);
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (currentBlock.shape[r, c] != 0)
                    {
                        DrawCell(g, currentBlock.x + c, currentBlock.y + r, currentBlock.ID);
                    }
                }
            }

            Pen gridPen = new Pen(Color.FromArgb(100, 100, 100));
            for (int r = 0; r <= Setting.row; r++)
                g.DrawLine(gridPen, 0, r * Setting.BlockSize, picBoard.Width, r * Setting.BlockSize);

            for (int c = 0; c <= Setting.col; c++)
                g.DrawLine(gridPen, c * Setting.BlockSize, 0, c * Setting.BlockSize, picBoard.Height);

            if (isGameOver)
            {
                using (SolidBrush overlayBrush = new SolidBrush(Color.FromArgb(180, 0, 0, 0)))
                {
                    g.FillRectangle(overlayBrush, 0, 0, picBoard.Width, picBoard.Height);
                }

                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                Rectangle mainRect = new Rectangle(0, picBoard.Height / 2 - 40, picBoard.Width, 50);
                Rectangle subRect = new Rectangle(0, picBoard.Height / 2 + 10, picBoard.Width, 30);
                Rectangle scoreRect = new Rectangle(0, picBoard.Height / 2 + 50, picBoard.Width, 30);

                using (Font fontMain = new Font("Arial", 22, FontStyle.Bold))
                using (Font fontSub = new Font("Arial", 12, FontStyle.Regular))
                using (SolidBrush redBrush = new SolidBrush(Color.Red))
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                using (SolidBrush yellowBrush = new SolidBrush(Color.Yellow))
                {
                    g.DrawString("GAME OVER", fontMain, redBrush, mainRect, sf);
                    g.DrawString("Nhấn Enter để chơi lại (hoặc Esc về Menu)", fontSub, whiteBrush, subRect, sf);
                    g.DrawString($"Tổng điểm: {score}", fontSub, yellowBrush, scoreRect, sf);
                }
            }
        }

        private void DrawCell(Graphics g, int x, int y, int id)
        {
            Color color = GetColorByID(id);
            int drawX = x * Setting.BlockSize;
            int drawY = y * Setting.BlockSize;

            g.FillRectangle(new SolidBrush(color), drawX, drawY, Setting.BlockSize, Setting.BlockSize);
            g.DrawRectangle(new Pen(ControlPaint.Light(color)), drawX, drawY, Setting.BlockSize, Setting.BlockSize);
        }

        private Color GetColorByID(int id)
        {
            switch (id)
            {
                case 1: return Color.Cyan;
                case 2: return Color.Blue;
                case 3: return Color.Orange;
                case 4: return Color.Yellow;
                case 5: return Color.Lime;
                case 6: return Color.Purple;
                case 7: return Color.Red;
                default: return Color.White;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Phím ESC: Thoát về Menu chính bất cứ lúc nào
            if (keyData == Keys.Escape)
            {
                gameTimer?.Stop();
                this.Close();
                return true;
            }

            if (isGameOver)
            {
                if (keyData == Keys.Enter)
                {
                    InitGame();
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if (currentBlock == null) return base.ProcessCmdKey(ref msg, keyData);

            int oldX = currentBlock.x;
            int oldY = currentBlock.y;

            switch (keyData)
            {
                case Keys.Left:
                    currentBlock.x--;
                    if (board.CheckCollision(currentBlock)) currentBlock.x = oldX;
                    break;

                case Keys.Right:
                    currentBlock.x++;
                    if (board.CheckCollision(currentBlock)) currentBlock.x = oldX;
                    break;

                case Keys.Down:
                    currentBlock.y++;
                    if (board.CheckCollision(currentBlock)) currentBlock.y = oldY;
                    break;

                case Keys.Up:
                    currentBlock.RotateCW();
                    if (board.CheckCollision(currentBlock))
                    {
                        currentBlock.RotateCW();
                        currentBlock.RotateCW();
                        currentBlock.RotateCW();
                    }
                    break;
            }

            this.Invalidate();
            return true;
        }

        private void picBoard_Click(object sender, EventArgs e) { }

        private void picNextBlock_Paint(object sender, PaintEventArgs e)
        {
            if (nextBlock == null) return;

            Graphics g = e.Graphics;

            int size = nextBlock.shape.GetLength(0);
            int cellSize = 20;

            int minR = size, maxR = -1;
            int minC = size, maxC = -1;

            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    if (nextBlock.shape[r, c] != 0)
                    {
                        minR = Math.Min(minR, r);
                        maxR = Math.Max(maxR, r);
                        minC = Math.Min(minC, c);
                        maxC = Math.Max(maxC, c);
                    }
                }
            }

            if (maxR < 0) return;

            int blockWidth = (maxC - minC + 1) * cellSize;
            int blockHeight = (maxR - minR + 1) * cellSize;

            int startX = (picNextBlock.ClientSize.Width - blockWidth) / 2;
            int startY = (picNextBlock.ClientSize.Height - blockHeight) / 2;

            Color color = GetColorByID(nextBlock.ID);

            using (SolidBrush brush = new SolidBrush(color))
            using (Pen pen = new Pen(ControlPaint.Dark(color)))
            {
                for (int r = minR; r <= maxR; r++)
                {
                    for (int c = minC; c <= maxC; c++)
                    {
                        if (nextBlock.shape[r, c] == 0) continue;

                        int x = startX + (c - minC) * cellSize;
                        int y = startY + (r - minR) * cellSize;

                        Rectangle rect = new Rectangle(x, y, cellSize, cellSize);

                        g.FillRectangle(brush, rect);
                        g.DrawRectangle(pen, rect);
                    }
                }
            }
        }

        private void picNextBlock_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}