namespace RunescapeHelper.Modules.AutoClicker
{
    partial class AutoClickerMainForm
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
            this.startButton = new System.Windows.Forms.Button();
            this.endButton = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.timerLabel = new System.Windows.Forms.Label();
            this.timerText = new System.Windows.Forms.TextBox();
            this.eventLog = new System.Windows.Forms.ListView();
            this.action = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.detail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeOccurred = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.resetButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.loopCounterText = new System.Windows.Forms.TextBox();
            this.runInfinitelyCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playbackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suspendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToSetButton = new System.Windows.Forms.Button();
            this.playbackFromSet = new System.Windows.Forms.CheckBox();
            this.removeRecordingFromSet = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(12, 35);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(128, 31);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start New Recording";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // endButton
            // 
            this.endButton.Location = new System.Drawing.Point(148, 35);
            this.endButton.Name = "endButton";
            this.endButton.Size = new System.Drawing.Size(128, 31);
            this.endButton.TabIndex = 1;
            this.endButton.Text = "Stop Recording";
            this.endButton.UseVisualStyleBackColor = true;
            this.endButton.Click += new System.EventHandler(this.endButton_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 206);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(265, 14);
            this.progressBar.TabIndex = 5;
            // 
            // timerLabel
            // 
            this.timerLabel.Location = new System.Drawing.Point(162, 76);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(34, 16);
            this.timerLabel.TabIndex = 21;
            this.timerLabel.Text = "Time:";
            // 
            // timerText
            // 
            this.timerText.Location = new System.Drawing.Point(202, 73);
            this.timerText.Name = "timerText";
            this.timerText.Size = new System.Drawing.Size(74, 20);
            this.timerText.TabIndex = 7;
            // 
            // eventLog
            // 
            this.eventLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.action,
            this.detail,
            this.timeOccurred});
            this.eventLog.Location = new System.Drawing.Point(12, 102);
            this.eventLog.Name = "eventLog";
            this.eventLog.Size = new System.Drawing.Size(265, 104);
            this.eventLog.TabIndex = 8;
            this.eventLog.UseCompatibleStateImageBehavior = false;
            this.eventLog.View = System.Windows.Forms.View.Details;
            // 
            // action
            // 
            this.action.Text = "Action";
            this.action.Width = 50;
            // 
            // detail
            // 
            this.detail.Text = "Detail";
            this.detail.Width = 75;
            // 
            // timeOccurred
            // 
            this.timeOccurred.Text = "Time";
            this.timeOccurred.Width = 135;
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(12, 72);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(93, 24);
            this.resetButton.TabIndex = 10;
            this.resetButton.Text = "Clear Grid";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Loops:";
            // 
            // loopCounterText
            // 
            this.loopCounterText.Location = new System.Drawing.Point(53, 263);
            this.loopCounterText.Name = "loopCounterText";
            this.loopCounterText.Size = new System.Drawing.Size(25, 20);
            this.loopCounterText.TabIndex = 15;
            // 
            // runInfinitelyCheckBox
            // 
            this.runInfinitelyCheckBox.AutoSize = true;
            this.runInfinitelyCheckBox.Location = new System.Drawing.Point(93, 265);
            this.runInfinitelyCheckBox.Name = "runInfinitelyCheckBox";
            this.runInfinitelyCheckBox.Size = new System.Drawing.Size(57, 17);
            this.runInfinitelyCheckBox.TabIndex = 16;
            this.runInfinitelyCheckBox.Text = "Infinite";
            this.runInfinitelyCheckBox.UseVisualStyleBackColor = true;
            this.runInfinitelyCheckBox.CheckedChanged += new System.EventHandler(this.runInfinitelyCheckBox_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.playbackToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(288, 24);
            this.menuStrip1.TabIndex = 19;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backToolStripMenuItem
            // 
            this.backToolStripMenuItem.Name = "backToolStripMenuItem";
            this.backToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.backToolStripMenuItem.Text = "Back";
            this.backToolStripMenuItem.Click += new System.EventHandler(this.backToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.importToolStripMenuItem.Text = "Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exportToolStripMenuItem.Text = "Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // playbackToolStripMenuItem
            // 
            this.playbackToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.suspendToolStripMenuItem});
            this.playbackToolStripMenuItem.Name = "playbackToolStripMenuItem";
            this.playbackToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.playbackToolStripMenuItem.Text = "Playback";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.startToolStripMenuItem.Text = "Start (F1)";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.stopToolStripMenuItem.Text = "Stop (F2)";
            // 
            // suspendToolStripMenuItem
            // 
            this.suspendToolStripMenuItem.Name = "suspendToolStripMenuItem";
            this.suspendToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.suspendToolStripMenuItem.Text = "Suspend (F3)";
            this.suspendToolStripMenuItem.Click += new System.EventHandler(this.suspendToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.viewToolStripMenuItem.Text = "View Recordings";
            // 
            // addToSetButton
            // 
            this.addToSetButton.Location = new System.Drawing.Point(12, 226);
            this.addToSetButton.Name = "addToSetButton";
            this.addToSetButton.Size = new System.Drawing.Size(128, 31);
            this.addToSetButton.TabIndex = 20;
            this.addToSetButton.Text = "Add Recording To Set";
            this.addToSetButton.UseVisualStyleBackColor = true;
            this.addToSetButton.Click += new System.EventHandler(this.addToSetButton_Click);
            // 
            // playbackFromSet
            // 
            this.playbackFromSet.AutoSize = true;
            this.playbackFromSet.Location = new System.Drawing.Point(161, 265);
            this.playbackFromSet.Name = "playbackFromSet";
            this.playbackFromSet.Size = new System.Drawing.Size(115, 17);
            this.playbackFromSet.TabIndex = 22;
            this.playbackFromSet.Text = "Playback From Set";
            this.playbackFromSet.UseVisualStyleBackColor = true;
            this.playbackFromSet.CheckedChanged += new System.EventHandler(this.playbackFromSet_CheckedChanged);
            // 
            // removeRecordingFromSet
            // 
            this.removeRecordingFromSet.Location = new System.Drawing.Point(161, 227);
            this.removeRecordingFromSet.Name = "removeRecordingFromSet";
            this.removeRecordingFromSet.Size = new System.Drawing.Size(115, 31);
            this.removeRecordingFromSet.TabIndex = 23;
            this.removeRecordingFromSet.Text = "Remove Recording";
            this.removeRecordingFromSet.UseVisualStyleBackColor = true;
            this.removeRecordingFromSet.Click += new System.EventHandler(this.removeRecordingFromSet_Click);
            // 
            // AutoClickerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 290);
            this.Controls.Add(this.removeRecordingFromSet);
            this.Controls.Add(this.playbackFromSet);
            this.Controls.Add(this.addToSetButton);
            this.Controls.Add(this.runInfinitelyCheckBox);
            this.Controls.Add(this.loopCounterText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.eventLog);
            this.Controls.Add(this.timerText);
            this.Controls.Add(this.timerLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.endButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AutoClickerMainForm";
            this.Text = "Auto Clicker";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AutoClickerMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button endButton;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label timerLabel;
        private System.Windows.Forms.TextBox timerText;
        private System.Windows.Forms.ListView eventLog;
        private System.Windows.Forms.ColumnHeader timeOccurred;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox loopCounterText;
        private System.Windows.Forms.CheckBox runInfinitelyCheckBox;
        private System.Windows.Forms.ColumnHeader action;
        private System.Windows.Forms.ColumnHeader detail;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem backToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.Button addToSetButton;
        private System.Windows.Forms.ToolStripMenuItem playbackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.CheckBox playbackFromSet;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suspendToolStripMenuItem;
        private System.Windows.Forms.Button removeRecordingFromSet;
    }
}

