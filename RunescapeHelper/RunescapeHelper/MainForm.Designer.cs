namespace RunescapeHelper
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.modulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoClickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seersVillageAgilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.combatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureGeneralOptionsButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modulesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(263, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // modulesToolStripMenuItem
            // 
            this.modulesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.autoClickerToolStripMenuItem,
            this.seersVillageAgilityToolStripMenuItem,
            this.combatToolStripMenuItem});
            this.modulesToolStripMenuItem.Name = "modulesToolStripMenuItem";
            this.modulesToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.modulesToolStripMenuItem.Text = "Modules";
            // 
            // autoClickerToolStripMenuItem
            // 
            this.autoClickerToolStripMenuItem.Name = "autoClickerToolStripMenuItem";
            this.autoClickerToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.autoClickerToolStripMenuItem.Text = "Auto Clicker";
            this.autoClickerToolStripMenuItem.Click += new System.EventHandler(this.autoClickerToolStripMenuItem_Click);
            // 
            // seersVillageAgilityToolStripMenuItem
            // 
            this.seersVillageAgilityToolStripMenuItem.Name = "seersVillageAgilityToolStripMenuItem";
            this.seersVillageAgilityToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.seersVillageAgilityToolStripMenuItem.Text = "Seer\'s Village Agility";
            this.seersVillageAgilityToolStripMenuItem.Click += new System.EventHandler(this.seersVillageAgilityToolStripMenuItem_Click);
            // 
            // combatToolStripMenuItem
            // 
            this.combatToolStripMenuItem.Name = "combatToolStripMenuItem";
            this.combatToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.combatToolStripMenuItem.Text = "Combat";
            this.combatToolStripMenuItem.Click += new System.EventHandler(this.combatToolStripMenuItem_Click);
            // 
            // configureGeneralOptionsButton
            // 
            this.configureGeneralOptionsButton.Location = new System.Drawing.Point(49, 46);
            this.configureGeneralOptionsButton.Name = "configureGeneralOptionsButton";
            this.configureGeneralOptionsButton.Size = new System.Drawing.Size(158, 34);
            this.configureGeneralOptionsButton.TabIndex = 1;
            this.configureGeneralOptionsButton.Text = "Configure General Options";
            this.configureGeneralOptionsButton.UseVisualStyleBackColor = true;
            this.configureGeneralOptionsButton.Click += new System.EventHandler(this.configureGeneralOptionsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 26);
            this.Controls.Add(this.configureGeneralOptionsButton);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MainForm";
            this.Text = "Runescape Helper";
            this.TopMost = true;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem modulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seersVillageAgilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem combatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoClickerToolStripMenuItem;
        private System.Windows.Forms.Button configureGeneralOptionsButton;
    }
}

