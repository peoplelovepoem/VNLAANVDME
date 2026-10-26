using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Tetris;
using Tetris.GameLogic;
using Tetris.GameSetting;
using Tetris.Models;

internal static class GameplayFeaturesTests
{
    const BindingFlags Flags = BindingFlags.Instance | BindingFlags.NonPublic;
    static int checks;
    static object Field(object o, string name) { return o.GetType().GetField(name, Flags).GetValue(o); }
    static object Call(object o, string name, params object[] args) { return o.GetType().GetMethod(name, Flags).Invoke(o, args); }
    static void Check(bool condition, string message) { if (!condition) throw new Exception(message); checks++; }
    [STAThread]
    static void Main()
    {
        var session = new PlayerSession(); session.SetName("Feature Test");
        using (var game = new Form1(session))
        {
            game.Show(); ((Timer)Field(game,"gameTimer")).Stop();
            var current = (Block)Field(game,"currentBlock");
            var queue = (Queue<int>)Field(game,"upcoming");
            Check(queue.Count == 1, "One next piece");
            int next = queue.Peek();
            foreach (Keys key in new[] { Keys.C, Keys.ShiftKey, Keys.Shift | Keys.ShiftKey })
            {
                Call(game, "ProcessCmdKey", new Message(), key);
                Check(ReferenceEquals(current, Field(game, "currentBlock")) && queue.Peek() == next,
                    "Former Hold key does not swap pieces");
            }
            Check(game.Controls.Find("picHold", true).Length == 0 &&
                game.Controls.Find("lblHold", true).Length == 0, "Hold controls removed");
            var board = (Board)Field(game, "board");
            var boardView = (PictureBox)Field(game, "picBoard");
            var bounds = (Rectangle)Call(game, "GetBoardRectangle");
            int cell = bounds.Width / 10;
            using (var bitmap = new Bitmap(boardView.Width, boardView.Height))
            {
                boardView.DrawToBitmap(bitmap, new Rectangle(Point.Empty, boardView.Size));
                for (int row = 4; row < 20; row++)
                    for (int col = 0; col < 10; col++)
                        Check(bitmap.GetPixel(bounds.Left + col * cell + cell / 2,
                            bounds.Top + row * cell + cell / 2).ToArgb() == boardView.BackColor.ToArgb(),
                            "Empty board has no landing shadow");
            }
            int previousY = current.y;
            Call(game, "GameTick", null, EventArgs.Empty);
            Check(current.y == previousY + 1, "Piece falls normally");
            do { current.y++; } while (!board.CheckCollision(current));
            current.y--;
            Call(game, "GameTick", null, EventArgs.Empty);
            Check(((Block)Field(game, "currentBlock")).ID == next && queue.Count == 1,
                "Lock spawns the preview and refills one next piece");
            int occupied = 0;
            foreach (int value in board.Grid) if (value != 0) occupied++;
            Check(occupied == 4, "Locked piece stays on board");
            foreach(var size in new[]{new Size(919,611),new Size(1600,900),new Size(700,500)})
            {
                game.ClientSize = size; Application.DoEvents();
                var pic=(Control)Field(game,"picBoard");
                var next2=(Control)Field(game,"picNextBlock");
                Check(next2.Left > pic.Right, "Next stays beside board");
                Check(pic.Bottom <= game.ClientSize.Height-20 && next2.Bottom < game.ClientSize.Height,"Board and next previews fit");
            }
            // The modal settings form must freeze the engine and elapsed play time.
            using(var closeSettings = new Timer { Interval = 50 })
            {
                closeSettings.Tick += (s,e) => {
                    foreach(Form f in Application.OpenForms) if(f is SettingsForm)
                    {
                        Check((bool)Field(game,"paused") && !((Timer)Field(game,"gameTimer")).Enabled,"Settings pauses timer");
                        Check(!((Stopwatch)Field(game,"playTime")).IsRunning,"Pause excludes elapsed time");
                        int y = ((Block)Field(game,"currentBlock")).y;
                        Call(game,"GameTick",null,EventArgs.Empty);
                        Check(((Block)Field(game,"currentBlock")).y == y,"No falling while paused");
                        f.Close(); break;
                    }
                };
                closeSettings.Start(); Call(game,"OpenGameSettings");
            }
            Check(!(bool)Field(game,"paused") && ((Timer)Field(game,"gameTimer")).Enabled,"Close/Esc resumes");
            ((Timer)Field(game,"gameTimer")).Stop();
            game.ClientSize = new Size(919,611);
            using(var image = new Bitmap(game.Width,game.Height))
            {
                game.DrawToBitmap(image,new Rectangle(Point.Empty,game.Size));
                image.Save(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"game-features.png"));
            }
            using(var closeSettings = new Timer { Interval = 50 })
            {
                closeSettings.Tick += (s,e) => {
                    foreach(Form f in Application.OpenForms) if(f is SettingsForm) { Call(f,"btnBackMenu_Click",null,EventArgs.Empty); break; }
                };
                closeSettings.Start(); Call(game,"OpenGameSettings");
            }
            Check(game.IsDisposed,"Menu leaves round without resuming");
        }
        using(var settings = new SettingsForm(true))
        {
            ((TrackBar)Field(settings,"volumeBar")).Value=23;
            Check(AudioSettings.Volume==23,"Volume bar is wired");
            Call(settings,"btnExitGame_Click",null,EventArgs.Empty);
            Check(settings.Action==GameSettingsAction.Exit,"Exit action");
        }
        Console.WriteLine("PASS: "+checks+" gameplay checks");
    }
}
