using Gma.UserActivityMonitor;
using System;
using System.Windows.Forms;

namespace RunescapeHelper.Modules.SeersVillageAgility
{
    public partial class TeleportConfigurationDialog : Form
    {
        private bool recordingActive = false;

        public TeleportConfigurationDialog()
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

            SeersVillageAgilityMainForm.teleportX = Cursor.Position.X;
            SeersVillageAgilityMainForm.teleportY = Cursor.Position.Y;
            SeersVillageAgilityMainForm.teleportCoordsActive = true;

            var confirmationMessage = "Coordinates have been successfully saved.";
            var confirmationCaption = "Success";
            var confirmationButtons = MessageBoxButtons.OK;

            MessageBox.Show(confirmationMessage, confirmationCaption, confirmationButtons);

            Close();
        }
    }
}
