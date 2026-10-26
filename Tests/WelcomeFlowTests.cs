using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Tetris;
using Tetris.Models;
using Tetris.GameSetting;

// Run from the isolated Tests/bin directory; never touches the player's score file.
internal static class WelcomeFlowTests
{
    private static int checks;
    private static void Check(bool value, string message)
    {
        if (!value) throw new Exception(message);
        checks++;
    }
    private static object Field(object target, string name)
    {
        return target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(target);
    }
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        string name, error;
        foreach (string invalid in new[] { "", "   ", new string('a', 21), "Nam|An", "Nam\nAn", "\tNam", "Nam An", "An_01", "An-01", "An.01", "@An", "An!", "An😊", "An\u200B", "An\u00A0B" })
            Check(!PlayerSession.TryValidateName(invalid, out name, out error), "Invalid name accepted");
        Check(PlayerSession.TryValidateName("  NguyễnAn  ", out name, out error) && name == "NguyễnAn", "Vietnamese/trim");
        Check(PlayerSession.TryValidateName(new string('a', 20), out name, out error), "20-character boundary");
        foreach (string valid in new[] { "An123", "123456", "ĐặngThịÁnh", "ABCxyz" })
            Check(PlayerSession.TryValidateName(valid, out name, out error), "Letters and digits accepted");
        string temp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test-data");
        Directory.CreateDirectory(temp);
        string nameFile = Path.Combine(temp, "name.txt");
        File.Delete(nameFile);
        var store = new PlayerNameStore(nameFile);
        Check(store.Load() == "", "Missing file");
        Check(store.TrySave("NguyễnAn") && store.Load() == "NguyễnAn", "Remember name");
        File.WriteAllText(nameFile, "Bad|Name");
        Check(store.Load() == "", "Corrupt name");
        File.WriteAllBytes(nameFile, new byte[] { 255, 255 });
        Check(store.Load() == "", "Invalid UTF8");
        Check(!new PlayerNameStore(temp).TrySave("An"), "Write failure must be reported");

        string scoresFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "leaderboard.txt");
        File.WriteAllLines(scoresFile, new[] { "Old|100", "New|200|65", "Broken", "Bad|x", "Negative|12|-1" });
        var scores = LeaderboardManager.LoadScores();
        Check(scores.Count == 2 && scores[0].PlayerName == "New", "Legacy/new records and invalid lines");
        Check(scores[0].DurationText == "01:05" && scores[1].DurationText == "—", "Duration display");
        LeaderboardManager.SaveScore("NguyễnAn", 300, 125);
        scores = LeaderboardManager.LoadScores();
        Check(scores.Count == 3 && scores[0].DurationSeconds == 125 && scores[2].PlayerName == "Old", "Save preserves legacy");
        using (var board = new LeaderboardForm())
        {
            var grid = (DataGridView)Field(board, "dgvLeaderboard");
            Check(grid.ReadOnly && !grid.AllowUserToAddRows && grid.ColumnCount == 4, "Read-only leaderboard");
        }
        var session = new PlayerSession();
        using (var welcome = new WelcomeForm(session, store))
        {
            var box = (TextBox)Field(welcome, "textBox1");
            var accept = welcome.GetType().GetMethod("btnJoin_Click", BindingFlags.Instance | BindingFlags.NonPublic);
            bool accepted = false;
            welcome.PlayerAccepted += (s, e) => accepted = true;
            box.Text = " ";
            accept.Invoke(welcome, new object[] { null, EventArgs.Empty });
            Check(!accepted && ((Label)Field(welcome, "lblError")).Text.Length > 0, "Inline validation");
            foreach (string invalid in new[] { "An@123", "Nguyễn An", "An_01", "An😊" })
            {
                box.Text = invalid;
                accept.Invoke(welcome, new object[] { null, EventArgs.Empty });
                Check(!accepted && session.PlayerName == null, "Invalid text cannot enter menu");
            }
            box.Text = "NguyễnAn";
            accept.Invoke(welcome, new object[] { null, EventArgs.Empty });
            Check(accepted && session.PlayerName == "NguyễnAn", "Accept name into session");
            foreach (float scale in new[] { 1f, 1.25f, 1.5f })
            {
                using (var view = new WelcomeForm(session, store))
                {
                    view.Show();
                    Application.DoEvents();
                    view.Scale(new System.Drawing.SizeF(scale, scale));
                    Application.DoEvents();
                    var panel = (Panel)Field(view, "panel1");
                    foreach (Control control in panel.Controls)
                        Check(panel.ClientRectangle.Contains(control.Bounds), "Scaled control clipped: " + scale);
                    using (var image = new System.Drawing.Bitmap(view.Width, view.Height))
                    {
                        view.DrawToBitmap(image, new System.Drawing.Rectangle(0, 0, view.Width, view.Height));
                        image.Save(Path.Combine(temp, "welcome-" + scale.ToString(System.Globalization.CultureInfo.InvariantCulture) + ".png"));
                    }
                }
            }
        }
        session.SetName("RoundOwner");
        using (var game = new Form1(session))
        {
            session.SetName("ChangedLater");
            game.Show();
            var timer = (Timer)Field(game, "gameTimer");
            timer.Stop();
            var model = (Tetris.Models.Board)Field(game, "board");
            for (int r = 0; r < 4; r++)
                for (int c = 0; c < 10; c++) model.Grid[r, c] = 1;
            using (var closeResults = new Timer { Interval = 50 })
            {
                closeResults.Tick += (s, e) => {
                    foreach (Form form in Application.OpenForms)
                        if (form is LeaderboardForm) { form.Close(); break; }
                };
                closeResults.Start();
                game.GetType().GetMethod("SpawnNewBlock", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(game, null);
                Application.DoEvents();
            }
            scores = LeaderboardManager.LoadScores();
            Check(scores.FindAll(x => x.PlayerName == "RoundOwner").Count == 1, "Game over saves once with round name");
            game.GetType().GetMethod("ShowGameOverAndSaveScore", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(game, null);
            Check(LeaderboardManager.LoadScores().Count == scores.Count, "No duplicate result");
            Check(game.IsDisposed && !timer.Enabled, "Game over closes form and stops timer");
        }
        // Exercise the real ApplicationContext transition handlers without launching gameplay/audio.
        Type contextType = typeof(WelcomeForm).Assembly.GetType("Tetris.TetrisApplicationContext");
        using (var context = (ApplicationContext)Activator.CreateInstance(contextType, true))
        {
            Form welcome = context.MainForm;
            var current = (PlayerSession)Field(welcome, "session");
            current.SetName("SessionTest");
            ((EventHandler)Field(welcome, "PlayerAccepted"))(welcome, EventArgs.Empty);
            Check(welcome.IsDisposed && context.MainForm is MenuForm, "Welcome to menu");
            Form menu = context.MainForm;
            ((EventHandler)Field(menu, "ChangeNameRequested"))(menu, EventArgs.Empty);
            Check(menu.IsDisposed && context.MainForm is WelcomeForm, "Menu to rename");
            bool exited = false;
            context.ThreadExit += (s, e) => exited = true;
            context.MainForm.Close();
            Check(exited, "Closing welcome exits context");
        }
        Console.WriteLine("PASS: " + checks + " checks");
    }
}
