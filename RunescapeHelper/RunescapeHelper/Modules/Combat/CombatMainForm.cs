using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RunescapeHelper.Modules.Combat
{
    public partial class CombatMainForm : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private bool playbackActive = false;
        private BackgroundWorker playbackThread = new BackgroundWorker();

        public static bool npcColorActive;
        public static int npcColorArgb;
        public int targetSearchDelay = 15000;

        public CombatMainForm()
        {
            InitializeComponent();

            playbackThread.DoWork += MainLoop;
            playbackThread.WorkerSupportsCancellation = true;

            HookManager.KeyDown += HandleKeyDown;
        }

        private void MainLoop(object sender, DoWorkEventArgs e)
        {
            playbackActive = true;
            while (playbackActive)
            {
                var validPoint = RetrieveValidPoint();
                if (validPoint != null)
                {
                    ExecuteClick(validPoint);
                }
            }
        }

        private Point? RetrieveValidPoint()
        {
            List<Point> result = new List<Point>();
            using (Bitmap bmp = GetScreenShot())
            {
                for (int x = 0; x < bmp.Width; x += 10)
                {
                    for (int y = 0; y < bmp.Height; y += 10)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        var pixelColor = pixel.ToArgb();

                        if (npcColorArgb.Equals(pixelColor))
                        {
                            return new Point(x, y);
                        }
                    }
                }
            }

            return null;
        }

        private Bitmap GetScreenShot()
        {
            Bitmap result = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            {
                using (Graphics gfx = Graphics.FromImage(result))
                {
                    gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
                }
            }

            return result;
        }

        private void RetrieveNpcColor()
        {
            var npcColorConfigurationDialog = new NpcColorConfigurationDialog();
            npcColorConfigurationDialog.ShowDialog();
        }

        private void ExecuteClick(Point? point)
        {
            Cursor.Position = new Point(point.Value.X, point.Value.Y);

            Thread.Sleep(new Random().Next(150, 450));

            var color = GetColorAtCursor(new Point(Cursor.Position.X, Cursor.Position.Y));
            if (color.ToArgb() != npcColorArgb)
            {
                return;
            }

            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);

            Thread.Sleep(targetSearchDelay);
        }

        private Color GetColorAtCursor(Point? point)
        {
            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, point.Value.X, point.Value.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }

        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    StartPlayback();
                    return;
                case Keys.F2:
                    StopPlayback();
                    return;
            }
        }

        private void StopPlayback()
        {
            if (!playbackActive)
            {
                return;
            }

            playbackActive = false;
            playbackThread.CancelAsync();
        }

        private void StartPlayback()
        {
            if (playbackActive)
            {
                return;
            }

            if (!npcColorActive)
            {
                var confirmationMessage = "Obstacle color is not configured.";
                var confirmationCaption = "Error";
                var confirmationButtons = MessageBoxButtons.OK;

                MessageBox.Show(confirmationMessage, confirmationCaption, confirmationButtons);

                return;
            }

            playbackActive = true;
            playbackThread.RunWorkerAsync();
        }

        private void targetSearchDelayValue_TextChanged(object sender, EventArgs e)
        {
            var text = targetSearchDelayValue.Text;
            if (string.IsNullOrEmpty(text))
            {
                targetSearchDelayValue.Text = "35000";
                return;
            }

            targetSearchDelay = int.Parse(targetSearchDelayValue.Text);
        }

        private void configureNPCColorButton_Click(object sender, EventArgs e)
        {
            RetrieveNpcColor();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartPlayback();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CombatMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPlayback();
            MainForm.mainForm.Show();
        }
    }
}
