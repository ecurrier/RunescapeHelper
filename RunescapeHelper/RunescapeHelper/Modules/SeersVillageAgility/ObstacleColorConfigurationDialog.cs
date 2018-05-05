using Gma.UserActivityMonitor;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RunescapeHelper.Modules.SeersVillageAgility
{
    public partial class ObstacleColorConfigurationDialog : Form
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        private bool recordingActive = false;

        public ObstacleColorConfigurationDialog()
        {
            InitializeComponent();

            HookManager.KeyUp += HandleKeyUp;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            recordingActive = true;
        }

        private void HandleKeyUp(object sender, KeyEventArgs e)
        {
            if (!recordingActive)
            {
                return;
            }

            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            recordingActive = false;

            var color = GetColorAtCursor(new Point(Cursor.Position.X, Cursor.Position.Y));

            SeersVillageAgilityMainForm.agilityObstacleHex = $"#{color.R.ToString("X2")}{color.G.ToString("X2")}{color.B.ToString("X2")}";
            SeersVillageAgilityMainForm.agilityObstacleArgb = color.ToArgb();
            SeersVillageAgilityMainForm.obstacleColorActive = true;

            var confirmationMessage = "Obstacle color has been successfully saved.";
            var confirmationCaption = "Success";
            var confirmationButtons = MessageBoxButtons.OK;

            MessageBox.Show(confirmationMessage, confirmationCaption, confirmationButtons);

            Close();
        }

        private Color GetColorAtCursor(Point point)
        {
            Bitmap screenPixel = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, point.X, point.Y, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }

            return screenPixel.GetPixel(0, 0);
        }
    }
}
