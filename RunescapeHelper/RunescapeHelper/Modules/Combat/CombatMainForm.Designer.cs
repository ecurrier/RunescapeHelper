namespace RunescapeHelper.Modules.Combat
{
    partial class CombatMainForm
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
            this.configureNPCColorButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.targetSearchDelayValue = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // configureNPCColorButton
            // 
            this.configureNPCColorButton.Location = new System.Drawing.Point(12, 38);
            this.configureNPCColorButton.Name = "configureNPCColorButton";
            this.configureNPCColorButton.Size = new System.Drawing.Size(129, 31);
            this.configureNPCColorButton.TabIndex = 0;
            this.configureNPCColorButton.Text = "Configure NPC Color";
            this.configureNPCColorButton.UseVisualStyleBackColor = true;
            this.configureNPCColorButton.Click += new System.EventHandler(this.configureNPCColorButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(263, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.backToolStripMenuItem.Text = "Back";
            this.backToolStripMenuItem.Click += new System.EventHandler(this.backToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Delay (ms): ";
            // 
            // targetSearchDelayValue
            // 
            this.targetSearchDelayValue.Location = new System.Drawing.Point(77, 126);
            this.targetSearchDelayValue.Name = "targetSearchDelayValue";
            this.targetSearchDelayValue.Size = new System.Drawing.Size(100, 20);
            this.targetSearchDelayValue.TabIndex = 3;
            this.targetSearchDelayValue.Text = "15000";
            this.targetSearchDelayValue.TextChanged += new System.EventHandler(this.targetSearchDelayValue_TextChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 80);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(85, 35);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Start (F1)";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(114, 80);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(85, 35);
            this.pauseButton.TabIndex = 6;
            this.pauseButton.Text = "Pause (F2)";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // CombatMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 158);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.targetSearchDelayValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.configureNPCColorButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CombatMainForm";
            this.Text = "CombatMainForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CombatMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button configureNPCColorButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox targetSearchDelayValue;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button pauseButton;
    }
}