using Gma.UserActivityMonitor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RunescapeHelper.Modules.SeersVillageAgility
{
    public partial class SeersVillageAgilityMainForm : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private bool playbackActive = false;
        private BackgroundWorker playbackThread = new BackgroundWorker();

        public static string agilityObstacleHex;
        public static int agilityObstacleArgb;
        public static int teleportX;
        public static int teleportY;

        public static bool obstacleColorActive = false;
        public static bool teleportCoordsActive = false;

        public int obstacleSearchDelay = 4000;

        public SeersVillageAgilityMainForm()
        {
            InitializeComponent();
            
            playbackThread.DoWork += MainLoop;
            playbackThread.WorkerSupportsCancellation = true;

            HookManager.KeyDown += HandleKeyDown;
        }

        private void RetrieveTeleportCoords()
        {
            var teleportConfigurationDialog = new TeleportConfigurationDialog();
            teleportConfigurationDialog.ShowDialog();
        }

        private void RetrieveObstacleColor()
        {
            var obstacleColorConfigurationDialog = new ObstacleColorConfigurationDialog();
            obstacleColorConfigurationDialog.ShowDialog();
        }

        private void MainLoop(object sender, DoWorkEventArgs e)
        {
            playbackActive = true;
            var lastValidPointFound = new Stopwatch();
            lastValidPointFound.Start();

            while (playbackActive)
            {
                var validPoint = RetrieveValidPoint();

                if (lastValidPointFound.ElapsedMilliseconds > 3500)
                {
                    ExecuteTeleport();
                    lastValidPointFound.Restart();
                    continue;
                }

                if (validPoint != null)
                {
                    ExecuteClick(validPoint);
                    lastValidPointFound.Restart();
                }
            }
        }

        private Point? RetrieveValidPoint()
        {
            List<Point> result = new List<Point>();
            using (Bitmap bmp = GetScreenShot())
            {
                for (int y = bmp.Height - 1; y >= 0; y -= 10)
                {
                    for (int x = 0; x < bmp.Width; x += 10)
                    {
                        var pixel = bmp.GetPixel(x, y);
                        var pixelColor = pixel.ToArgb();

                        if (agilityObstacleArgb.Equals(pixelColor))
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

        private void ExecuteTeleport()
        {
            Cursor.Position = new Point(teleportX, teleportY);

            Thread.Sleep(new Random().Next(150, 451));

            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);

            Thread.Sleep(new Random().Next(4000, 5000));
        }

        private void ExecuteClick(Point? point)
        {
            Cursor.Position = new Point(point.Value.X, point.Value.Y);

            Thread.Sleep(new Random().Next(100, 125));

            var color = GetColorAtCursor(new Point(Cursor.Position.X, Cursor.Position.Y));
            if (color.ToArgb() != agilityObstacleArgb)
            {
                return;
            }

            Invoke((MethodInvoker)delegate {
                lastClickCoordinatesValue.Text = $"X: {point.Value.X} Y: {point.Value.Y}";
            });

            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);

            Thread.Sleep(obstacleSearchDelay);
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

        private void pauseButton_Click(object sender, EventArgs e)
        {
            StopPlayback();
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

            if (!obstacleColorActive || !teleportCoordsActive)
            {
                var confirmationMessage = "Obstacle color or Teleport coordinates are not configured.";
                var confirmationCaption = "Error";
                var confirmationButtons = MessageBoxButtons.OK;

                MessageBox.Show(confirmationMessage, confirmationCaption, confirmationButtons);

                return;
            }

            teleportCoordinatesValue.Text = $"X: {teleportX} Y: {teleportY}";
            obstacleColorValue.Text = $"{agilityObstacleHex}";

            playbackActive = true;
            playbackThread.RunWorkerAsync();
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

        private void configureObstacleColor_Click(object sender, EventArgs e)
        {
            RetrieveObstacleColor();
        }

        private void configureTeleportButton_Click(object sender, EventArgs e)
        {
            RetrieveTeleportCoords();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartPlayback();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SeersVillageAgilityMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopPlayback();
            MainForm.mainForm.Show();
        }

        private void targetSearchDelayValue_TextChanged(object sender, EventArgs e)
        {
            var text = targetSearchDelayValue.Text;
            if (string.IsNullOrEmpty(text))
            {
                targetSearchDelayValue.Text = "4000";
                return;
            }

            obstacleSearchDelay = int.Parse(targetSearchDelayValue.Text);
        }
    }
}
