namespace FileWatcherUtilities.FileWatcherSimple
{
    partial class FormOptions
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
            this.panelTabs = new System.Windows.Forms.Panel();
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.labelFileWatchers = new System.Windows.Forms.Label();
            this.checkBoxApplicationAutoStartup = new System.Windows.Forms.CheckBox();
            this.numericUpDownProcessBatchSize = new System.Windows.Forms.NumericUpDown();
            this.labelProcessBatchSize = new System.Windows.Forms.Label();
            this.checkBoxApplicationRunQueuedProcesses = new System.Windows.Forms.CheckBox();
            this.checkBoxApplicationSynchronousExecution = new System.Windows.Forms.CheckBox();
            this.labelProcesses = new System.Windows.Forms.Label();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.numericUpDownLogMessages = new System.Windows.Forms.NumericUpDown();
            this.labelLogMessagesToDisplay = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelTabs.SuspendLayout();
            this.tabControlOptions.SuspendLayout();
            this.tabPageProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessBatchSize)).BeginInit();
            this.tabPageAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLogMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTabs
            // 
            this.panelTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTabs.Controls.Add(this.tabControlOptions);
            this.panelTabs.Location = new System.Drawing.Point(4, 4);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(382, 372);
            this.panelTabs.TabIndex = 1;
            // 
            // tabControlOptions
            // 
            this.tabControlOptions.Controls.Add(this.tabPageProcess);
            this.tabControlOptions.Controls.Add(this.tabPageAdvanced);
            this.tabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlOptions.Location = new System.Drawing.Point(0, 0);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(382, 372);
            this.tabControlOptions.TabIndex = 0;
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.labelFileWatchers);
            this.tabPageProcess.Controls.Add(this.checkBoxApplicationAutoStartup);
            this.tabPageProcess.Controls.Add(this.numericUpDownProcessBatchSize);
            this.tabPageProcess.Controls.Add(this.labelProcessBatchSize);
            this.tabPageProcess.Controls.Add(this.checkBoxApplicationRunQueuedProcesses);
            this.tabPageProcess.Controls.Add(this.checkBoxApplicationSynchronousExecution);
            this.tabPageProcess.Controls.Add(this.labelProcesses);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProcess.Size = new System.Drawing.Size(374, 346);
            this.tabPageProcess.TabIndex = 0;
            this.tabPageProcess.Text = "Application";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // labelFileWatchers
            // 
            this.labelFileWatchers.AutoSize = true;
            this.labelFileWatchers.Location = new System.Drawing.Point(6, 139);
            this.labelFileWatchers.Name = "labelFileWatchers";
            this.labelFileWatchers.Size = new System.Drawing.Size(72, 13);
            this.labelFileWatchers.TabIndex = 28;
            this.labelFileWatchers.Text = "File watchers:";
            // 
            // checkBoxApplicationAutoStartup
            // 
            this.checkBoxApplicationAutoStartup.AutoSize = true;
            this.checkBoxApplicationAutoStartup.Location = new System.Drawing.Point(9, 166);
            this.checkBoxApplicationAutoStartup.Name = "checkBoxApplicationAutoStartup";
            this.checkBoxApplicationAutoStartup.Size = new System.Drawing.Size(211, 17);
            this.checkBoxApplicationAutoStartup.TabIndex = 27;
            this.checkBoxApplicationAutoStartup.Text = "Start all enabled file watchers at startup";
            this.checkBoxApplicationAutoStartup.UseVisualStyleBackColor = true;
            // 
            // numericUpDownProcessBatchSize
            // 
            this.numericUpDownProcessBatchSize.Location = new System.Drawing.Point(9, 107);
            this.numericUpDownProcessBatchSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownProcessBatchSize.Name = "numericUpDownProcessBatchSize";
            this.numericUpDownProcessBatchSize.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownProcessBatchSize.TabIndex = 25;
            // 
            // labelProcessBatchSize
            // 
            this.labelProcessBatchSize.AutoSize = true;
            this.labelProcessBatchSize.Location = new System.Drawing.Point(6, 91);
            this.labelProcessBatchSize.Name = "labelProcessBatchSize";
            this.labelProcessBatchSize.Size = new System.Drawing.Size(99, 13);
            this.labelProcessBatchSize.TabIndex = 26;
            this.labelProcessBatchSize.Text = "Process batch size:";
            // 
            // checkBoxApplicationRunQueuedProcesses
            // 
            this.checkBoxApplicationRunQueuedProcesses.AutoSize = true;
            this.checkBoxApplicationRunQueuedProcesses.Location = new System.Drawing.Point(9, 62);
            this.checkBoxApplicationRunQueuedProcesses.Name = "checkBoxApplicationRunQueuedProcesses";
            this.checkBoxApplicationRunQueuedProcesses.Size = new System.Drawing.Size(299, 17);
            this.checkBoxApplicationRunQueuedProcesses.TabIndex = 1;
            this.checkBoxApplicationRunQueuedProcesses.Text = "Run queued processes when all file watchers are stopped";
            this.checkBoxApplicationRunQueuedProcesses.UseVisualStyleBackColor = true;
            // 
            // checkBoxApplicationSynchronousExecution
            // 
            this.checkBoxApplicationSynchronousExecution.AutoSize = true;
            this.checkBoxApplicationSynchronousExecution.Location = new System.Drawing.Point(9, 39);
            this.checkBoxApplicationSynchronousExecution.Name = "checkBoxApplicationSynchronousExecution";
            this.checkBoxApplicationSynchronousExecution.Size = new System.Drawing.Size(248, 17);
            this.checkBoxApplicationSynchronousExecution.TabIndex = 0;
            this.checkBoxApplicationSynchronousExecution.Text = "Synchronous execution of all started processes";
            this.checkBoxApplicationSynchronousExecution.UseVisualStyleBackColor = true;
            // 
            // labelProcesses
            // 
            this.labelProcesses.AutoSize = true;
            this.labelProcesses.Location = new System.Drawing.Point(6, 12);
            this.labelProcesses.Name = "labelProcesses";
            this.labelProcesses.Size = new System.Drawing.Size(59, 13);
            this.labelProcesses.TabIndex = 0;
            this.labelProcesses.Text = "Processes:";
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.numericUpDownLogMessages);
            this.tabPageAdvanced.Controls.Add(this.labelLogMessagesToDisplay);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Size = new System.Drawing.Size(374, 346);
            this.tabPageAdvanced.TabIndex = 1;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // numericUpDownLogMessages
            // 
            this.numericUpDownLogMessages.Location = new System.Drawing.Point(9, 26);
            this.numericUpDownLogMessages.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLogMessages.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownLogMessages.Name = "numericUpDownLogMessages";
            this.numericUpDownLogMessages.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownLogMessages.TabIndex = 13;
            this.numericUpDownLogMessages.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelLogMessagesToDisplay
            // 
            this.labelLogMessagesToDisplay.AutoSize = true;
            this.labelLogMessagesToDisplay.Location = new System.Drawing.Point(6, 10);
            this.labelLogMessagesToDisplay.Name = "labelLogMessagesToDisplay";
            this.labelLogMessagesToDisplay.Size = new System.Drawing.Size(172, 13);
            this.labelLogMessagesToDisplay.TabIndex = 15;
            this.labelLogMessagesToDisplay.Text = "Amount of log messages to display:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(232, 382);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(74, 23);
            this.buttonOK.TabIndex = 100;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(312, 382);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(74, 23);
            this.buttonCancel.TabIndex = 101;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormOptions
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(390, 411);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.panelTabs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(398, 445);
            this.Name = "FormOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.panelTabs.ResumeLayout(false);
            this.tabControlOptions.ResumeLayout(false);
            this.tabPageProcess.ResumeLayout(false);
            this.tabPageProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessBatchSize)).EndInit();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLogMessages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTabs;
        private System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabPageProcess;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.CheckBox checkBoxApplicationSynchronousExecution;
        private System.Windows.Forms.CheckBox checkBoxApplicationRunQueuedProcesses;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessBatchSize;
        private System.Windows.Forms.Label labelProcessBatchSize;
        private System.Windows.Forms.Label labelFileWatchers;
        private System.Windows.Forms.CheckBox checkBoxApplicationAutoStartup;
        private System.Windows.Forms.Label labelProcesses;
        private System.Windows.Forms.NumericUpDown numericUpDownLogMessages;
        private System.Windows.Forms.Label labelLogMessagesToDisplay;
    }
}