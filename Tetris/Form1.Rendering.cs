using System;
using System.Drawing;
using System.Windows.Forms;
using Tetris.GameLogic;
using Tetris.GameSetting;

namespace Tetris
{
    public partial class Form1
    {
        private void LayoutGameBoard()
        {
            if (WindowState == FormWindowState.Minimized)
                return;
            const int margin = 24, gap = 18;
            int side = Math.Max(130, picNextBlock.Width);
            int maxWidth = Math.Max(10, ClientSize.Width - 2 * (margin + side + gap));
            int maxHeight = Math.Max(20, ClientSize.Height - 2 * margin);
            int cell = Math.Max(1, Math.Min((Math.Min(designedBoardBounds.Width, maxWidth) - 2) / 10,
                (Math.Min(designedBoardBounds.Height, maxHeight) - 2) / 20));
            int width = cell * 10 + 2, height = cell * 20 + 2;
            int x = designedBoardBounds.Left + (ClientSize.Width - designedClientSize.Width) / 2
                + (designedBoardBounds.Width - width) / 2;
            int y = designedBoardBounds.Top + (ClientSize.Height - designedClientSize.Height) / 2
                + (designedBoardBounds.Height - height) / 2;
            x = Math.Max(margin + side + gap, Math.Min(x, ClientSize.Width - margin - side - gap - width));
            y = Math.Max(margin, Math.Min(y, ClientSize.Height - margin - height));
            picBoard.SetBounds(x, y, width, height);
            lblScore.Location = new Point(x - side - gap, y + 158);
            label1.Location = new Point(picBoard.Right + gap, y);
            picNextBlock.SetBounds(label1.Left, label1.Bottom + 6, side, 120);
            btnGameSettings.Location = new Point(ClientSize.Width - btnGameSettings.Width - 20,
                ClientSize.Height - btnGameSettings.Height - 20);
            Invalidate(true);
        }

        private void DrawFrames(Graphics g)
        {
            using (var pen = new Pen(Color.SaddleBrown, 3))
            {
                var bounds = picNextBlock.Bounds;
                bounds.Inflate(2, 2);
                g.DrawRectangle(pen, bounds);
            }
        }

        private Rectangle GetBoardRectangle()
        {
            int cell = Math.Min((picBoard.ClientSize.Width - 1) / 10, (picBoard.ClientSize.Height - 1) / 20);
            if (cell < 1)
                return Rectangle.Empty;
            return new Rectangle((picBoard.ClientSize.Width - cell * 10 - 1) / 2,
                (picBoard.ClientSize.Height - cell * 20 - 1) / 2, cell * 10, cell * 20);
        }

        private void picBoard_Paint(object sender, PaintEventArgs e)
        {
            var bounds = GetBoardRectangle();
            if (bounds.IsEmpty || currentBlock == null)
                return;
            Graphics g = e.Graphics;
            for (int r = 0; r < Setting.row; r++)
                for (int c = 0; c < Setting.col; c++)
                    if (board.Grid[r, c] != 0)
                        DrawCell(g, c, r, board.Grid[r, c]);
            DrawBlock(g, currentBlock);
            int cell = bounds.Width / 10;
            using (var pen = new Pen(Color.FromArgb(145, 130, 110)))
            {
                for (int r = 1; r < 20; r++)
                    g.DrawLine(pen, bounds.Left, bounds.Top + r * cell, bounds.Right, bounds.Top + r * cell);
                for (int c = 1; c < 10; c++)
                    g.DrawLine(pen, bounds.Left + c * cell, bounds.Top, bounds.Left + c * cell, bounds.Bottom);
            }
            using (var border = new Pen(Color.SaddleBrown, 2))
            {
                border.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                g.DrawRectangle(border, bounds);
            }
        }

        private void DrawBlock(Graphics g, Block block)
        {
            for (int r = 0; r < block.shape.GetLength(0); r++)
                for (int c = 0; c < block.shape.GetLength(1); c++)
                    if (block.shape[r, c] != 0)
                        DrawCell(g, block.x + c, block.y + r, block.ID);
        }

        private void DrawCell(Graphics g, int x, int y, int id)
        {
            var bounds = GetBoardRectangle();
            if (bounds.IsEmpty)
                return;
            int cell = bounds.Width / 10;
            var rect = new Rectangle(bounds.Left + x * cell + 1, bounds.Top + y * cell + 1, cell - 2, cell - 2);
            Color color = GetColorByID(id);
            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(ControlPaint.Light(color), 1))
            {
                g.FillRectangle(brush, rect);
                g.DrawRectangle(pen, rect);
            }
        }

        private Color GetColorByID(int id)
        {
            switch (id)
            {
                case 1:
                    return Color.Gold;
                case 2:
                    return Color.MediumPurple;
                case 3:
                    return Color.LimeGreen;
                case 4:
                    return Color.Orange;
                case 5:
                    return Color.Cyan;
                case 6:
                    return Color.RoyalBlue;
                case 7:
                    return Color.Crimson;
                default:
                    return Color.Gray;
            }
        }

        private void DrawPreview(Graphics g, Rectangle area, int? id)
        {
            if (!id.HasValue)
                return;
            var b = new Block(0, 0, id.Value);
            int minR = 4, minC = 4, maxR = 0, maxC = 0;
            for (int r = 0; r < b.shape.GetLength(0); r++)
            for (int c = 0; c < b.shape.GetLength(1); c++)
                if (b.shape[r, c] != 0)
                {
                    minR = Math.Min(minR, r);
                    minC = Math.Min(minC, c);
                    maxR = Math.Max(maxR, r);
                    maxC = Math.Max(maxC, c);
                }
            int cell = Math.Min(24, Math.Min((area.Width - 16) / (maxC - minC + 1), (area.Height - 16) / (maxR - minR + 1)));
            if (cell < 2)
                return;
            int left = area.Left + (area.Width - (maxC - minC + 1) * cell) / 2, top = area.Top + (area.Height - (maxR - minR + 1) * cell) / 2;
            using (var brush = new SolidBrush(GetColorByID(id.Value)))
            using (var pen = new Pen(ControlPaint.Light(GetColorByID(id.Value))))
                for (int r = minR; r <= maxR; r++)
                for (int c = minC; c <= maxC; c++)
                if (b.shape[r, c] != 0)
                {
                    var rect = new Rectangle(left + (c - minC) * cell, top + (r - minR) * cell, cell - 1, cell - 1);
                    g.FillRectangle(brush, rect);
                    g.DrawRectangle(pen, rect);
                }
        }

        private void picNextBlock_Paint(object sender, PaintEventArgs e)
        {
            if (upcoming.Count > 0)
                DrawPreview(e.Graphics, picNextBlock.ClientRectangle, upcoming.Peek());
        }
    }
}
