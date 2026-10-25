using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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
        private readonly Queue<int> upcoming = new Queue<int>();
        private int? heldId;
        private bool holdUsed;
        private Timer gameTimer;
        private readonly Random random = new Random();
        private bool isGameOver;
        private bool paused;
        private bool resultSaved;
        private const int NextPieceCount = 2;
        private static readonly int[] LineScores = { 0, 100, 300, 500, 800 };
        private int score;
        private readonly string playerName;
        private readonly Stopwatch playTime = new Stopwatch();
        private Rectangle designedBoardBounds;
        private Size designedClientSize;

        public Form1() : this(CreateDesignerSession()) { }
        private static PlayerSession CreateDesignerSession()
        {
            var session = new PlayerSession();
            session.SetName("Player");
            return session;
        }

        public Form1(PlayerSession session)
        {
            playerName = session.PlayerName;
            InitializeComponent();
            designedBoardBounds = picBoard.Bounds;
            designedClientSize = ClientSize;
            MinimumSize = new Size(700, 500);
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.ShiftKey)
                {
                    HoldCurrentBlock();
                    e.Handled = true;
                }
            };
            StartPosition = FormStartPosition.CenterScreen;
            Resize += (s, e) => LayoutGameBoard();
            picBoard.Resize += (s, e) => picBoard.Invalidate();
            Paint += (s, e) => DrawFrames(e.Graphics);
            Shown += (s, e) => AudioPlayback.UseGameMusic(true);
            FormClosed += (s, e) =>
            {
                gameTimer.Stop();
                gameTimer.Dispose();
                playTime.Stop();
                AudioPlayback.UseGameMusic(false);
            };
            LayoutGameBoard();
            InitGame();
        }

        public void InitGame()
        {
            if (gameTimer != null)
            {
                gameTimer.Stop();
                gameTimer.Dispose();
            }
            board = new Board();
            upcoming.Clear();
            heldId = null;
            holdUsed = false;
            isGameOver = false;
            paused = false;
            resultSaved = false;
            score = 0;
            gameTimer = new Timer { Interval = 500 };
            gameTimer.Tick += GameTick;
            FillQueue();
            SpawnNewBlock();
            UpdateScoreDisplay();
            playTime.Restart();
            gameTimer.Start();
            picHold.Invalidate();
        }

        private void FillQueue()
        {
            while (upcoming.Count < NextPieceCount)
                upcoming.Enqueue(random.Next(1, 8));
        }

        private void SpawnNewBlock()
        {
            FillQueue();
            int id = upcoming.Dequeue();
            FillQueue();
            ActivateBlock(id);
        }

        private void ActivateBlock(int id)
        {
            currentBlock = new Block(0, 0, id);
            currentBlock.x = (Setting.col - currentBlock.shape.GetLength(0)) / 2;
            picBoard.Invalidate();
            picNextBlock.Invalidate();
            picHold.Invalidate();
            if (board.CheckCollision(currentBlock))
            {
                isGameOver = true;
                gameTimer.Stop();
                playTime.Stop();
                BeginInvoke(new Action(ShowGameOverAndSaveScore));
            }
        }

        private void HoldCurrentBlock()
        {
            if (paused || isGameOver || holdUsed)
                return;
            int outgoing = currentBlock.ID;
            int? incoming = heldId;
            heldId = outgoing;
            holdUsed = true;
            if (incoming.HasValue)
                ActivateBlock(incoming.Value);
            else
                SpawnNewBlock();
        }

        private Block GetGhostBlock()
        {
            if (currentBlock == null || isGameOver)
                return null;
            var ghost = currentBlock.Copy();
            do
            {
                ghost.y++;
            } while (!board.CheckCollision(ghost));
            ghost.y--;
            return ghost;
        }

        private void GameTick(object sender, EventArgs e)
        {
            if (paused || isGameOver)
                return;
            currentBlock.y++;
            if (board.CheckCollision(currentBlock))
            {
                currentBlock.y--;
                board.LockBlock(currentBlock);
                int lines = board.ClearLine();
                if (lines > 0)
                {
                    score += LineScores[Math.Min(LineScores.Length - 1, lines)];
                    AudioPlayback.PlayEffect("ClearLine.wav");
                    UpdateScoreDisplay();
                }
                holdUsed = false;
                SpawnNewBlock();
            }
            picBoard.Invalidate();
        }

        private void UpdateScoreDisplay()
        {
            Text = "Tetris - " + playerName;
            lblScore.Text = "SCORE: " + score;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                OpenGameSettings();
                return true;
            }
            if (paused || isGameOver)
                return true;
            if (keyData == Keys.C || keyData == Keys.ShiftKey)
            {
                HoldCurrentBlock();
                return true;
            }
            var before = currentBlock.Copy();
            switch (keyData)
            {
                case Keys.Left:
                    currentBlock.x--;
                    break;
                case Keys.Right:
                    currentBlock.x++;
                    break;
                case Keys.Down:
                    currentBlock.y++;
                    break;
                case Keys.Up:
                    currentBlock.RotateCW();
                    break;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
            if (board.CheckCollision(currentBlock))
                currentBlock = before;
            picBoard.Invalidate();
            return true;
        }

        private void btnGameSettings_Click(object sender, EventArgs e)
        {
            OpenGameSettings();
        }

        private void OpenGameSettings()
        {
            if (paused || isGameOver)
                return;
            paused = true;
            gameTimer.Stop();
            playTime.Stop();
            try
            {
                using (var settings = new SettingsForm(true))
                {
                    settings.ShowDialog(this);
                    if (settings.Action == GameSettingsAction.Menu)
                    {
                        Close();
                        return;
                    }
                    if (settings.Action == GameSettingsAction.Exit)
                    {
                        Application.Exit();
                        return;
                    }
                }
            }
            finally
            {
                if (!IsDisposed && !Disposing && !isGameOver)
                {
                    paused = false;
                    playTime.Start();
                    gameTimer.Start();
                }
            }
        }

        private void ShowGameOverAndSaveScore()
        {
            if (resultSaved || IsDisposed || Disposing)
                return;
            resultSaved = true;
            gameTimer.Stop();
            playTime.Stop();
            AudioPlayback.PlayEffect("Lose.wav");
            LeaderboardManager.SaveScore(playerName, score, (int)playTime.Elapsed.TotalSeconds);
            using (var results = new LeaderboardForm())
                results.ShowDialog(this);
            Close();
        }
    }
}
