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
            Check(queue.Count == 2,"Two next pieces");
            int first = current.ID, next = queue.Peek();
            var ghost = (Block)Call(game,"GetGhostBlock");
            var board = (Board)Field(game,"board");
            Check(current.y == 0 && !board.CheckCollision(ghost),"Ghost is valid without moving active piece");
            ghost.y++; Check(board.CheckCollision(ghost),"Ghost stops at landing");
            Call(game,"HoldCurrentBlock");
            Check((int)Field(game,"heldId") == first && ((Block)Field(game,"currentBlock")).ID == next,"First hold consumes next");
            var afterHold = Field(game,"currentBlock");
            Call(game,"HoldCurrentBlock");
            Check(ReferenceEquals(afterHold,Field(game,"currentBlock")),"Hold only once per lock");
            ghost = (Block)Call(game,"GetGhostBlock");
            ((Block)Field(game,"currentBlock")).y = ghost.y;
            Call(game,"GameTick",null,EventArgs.Empty);
            Check(!(bool)Field(game,"holdUsed"),"Lock resets hold permission");
            int beforeQueue = queue.Peek();
            Call(game,"HoldCurrentBlock");
            Check(((Block)Field(game,"currentBlock")).ID == first && queue.Peek() == beforeQueue,"Swap does not consume next");
            Check(((Block)Field(game,"currentBlock")).y == 0,"Held piece respawns at top");
            board.Grid[15,4] = 7;
            current = (Block)Field(game,"currentBlock");
            int[,] snapshot = (int[,])board.Grid.Clone();
            ghost = (Block)Call(game,"GetGhostBlock");
            Check(!board.CheckCollision(ghost),"Ghost valid on stack");
            for(int r=0;r<20;r++) for(int c=0;c<10;c++)
                if(snapshot[r,c]!=board.Grid[r,c]) throw new Exception("Ghost changes board");
            checks++;
            foreach(var size in new[]{new Size(919,611),new Size(1600,900),new Size(700,500)})
            {
                game.ClientSize = size; Application.DoEvents();
                var pic=(Control)Field(game,"picBoard");
                var hold=(Control)Field(game,"picHold");
                var next2=(Control)Field(game,"picNextBlock");
                Check(hold.Right < pic.Left && next2.Left > pic.Right,"Hold/Next flank board");
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
