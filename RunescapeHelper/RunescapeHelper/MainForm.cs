using RunescapeHelper.Modules.AutoClicker;
using RunescapeHelper.Modules.Combat;
using RunescapeHelper.Modules.SeersVillageAgility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunescapeHelper
{
    public partial class MainForm : Form
    {
        public static MainForm mainForm;

        public MainForm()
        {
            InitializeComponent();
            mainForm = this;
        }

        private void autoClickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var autoClickerForm = new AutoClickerMainForm();
            autoClickerForm.Show();
            Hide();
        }

        private void seersVillageAgilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var seersVillageAgilityForm = new SeersVillageAgilityMainForm();
            seersVillageAgilityForm.Show();
            Hide();
        }

        private void combatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var combatForm = new CombatMainForm();
            combatForm.Show();
            Hide();
        }

        private void configureGeneralOptionsButton_Click(object sender, EventArgs e)
        {

        }
    }
}
