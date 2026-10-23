using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();

        }

        // Tự chạy khi menu được hiển thị lần đầu.
        

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Sounds", "ui_click.wav");
                            SoundPlayer soundPlayer = new SoundPlayer(path);
                            soundPlayer.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                            "Lỗi phát tiếng click: " + ex);
            }
            
            try
            {
                using (Form1 gameForm = new Form1())
                {
                    

                    this.Hide();

                    try
                    {
                        gameForm.ShowDialog();
                    }
                    finally
                    {
                        if (!IsDisposed && !Disposing)
                        {
                            this.Show();
                            this.Activate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không thể mở trò chơi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnHelp_Click(object sender, EventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Sounds", "ui_click.wav");
                SoundPlayer soundPlayer = new SoundPlayer(path);
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                            "Lỗi phát tiếng click: " + ex);
            }
            
            MessageBox.Show(
                "Mũi tên Trái/Phải: Di chuyển gạch sang 2 bên\n\n" +
                "Mũi tên Lên: Xoay khối gạch\n\n" +
                "Mũi tên Xuống: Cho gạch rơi nhanh hơn\n\n" +
                "Enter: Chơi lại từ đầu khi thua",
                "Hướng dẫn chơi",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private async void BtnExit_Click(object sender, EventArgs e)
        {
            try{
                string path = Path.Combine(Application.StartupPath, "Sounds", "ui_click.wav");
                SoundPlayer soundPlayer = new SoundPlayer(path);
                soundPlayer.Play();
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                            "Lỗi phát tiếng click: " + ex);
            }
            Application.Exit();
        }

        
    }
}