namespace FileWatcherUtilities.FileWatcherSimple
{
    partial class FormProperties
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
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.labelMinutes = new System.Windows.Forms.Label();
            this.numericUpDownRecycleInterval = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPollDirectoryInterval = new System.Windows.Forms.NumericUpDown();
            this.labelPollDirectoryInterval = new System.Windows.Forms.Label();
            this.checkBoxRecycleFileWatcher = new System.Windows.Forms.CheckBox();
            this.checkBoxPollDirectory = new System.Windows.Forms.CheckBox();
            this.checkBoxGenerateFileSystemEventsAtStartup = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeSubdirectories = new System.Windows.Forms.CheckBox();
            this.buttonBrowseDirectory = new System.Windows.Forms.Button();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPathToWatch = new System.Windows.Forms.Label();
            this.checkBoxStartDaemon = new System.Windows.Forms.CheckBox();
            this.textBoxDaemonName = new System.Windows.Forms.TextBox();
            this.labelDaemonName = new System.Windows.Forms.Label();
            this.tabPageFileSystemEvents = new System.Windows.Forms.TabPage();
            this.labelFileSystemEventFilters = new System.Windows.Forms.Label();
            this.labelFileSystemEventsToWatch = new System.Windows.Forms.Label();
            this.checkBoxNotifyDirectoryName = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyLastWrite = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyAttribute = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifySecurity = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifySize = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyLastAccess = new System.Windows.Forms.CheckBox();
            this.checkBoxSubscribeToChangedEvent = new System.Windows.Forms.CheckBox();
            this.checkBoxSubscribeToCreatedEvent = new System.Windows.Forms.CheckBox();
            this.checkBoxSubscribeToRenamedEvent = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyCreationTime = new System.Windows.Forms.CheckBox();
            this.checkBoxNotifyFileName = new System.Windows.Forms.CheckBox();
            this.checkBoxSubscribeToDeletedEvent = new System.Windows.Forms.CheckBox();
            this.tabPageFilters = new System.Windows.Forms.TabPage();
            this.textBoxGenereatedEventFileNameRegularExpressionFilter = new System.Windows.Forms.TextBox();
            this.labelGnereatedEventFileNameRegularExpressionFilter = new System.Windows.Forms.Label();
            this.labelAdditionalFilters = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.labelFileNameFilterString = new System.Windows.Forms.Label();
            this.textBoxRenamedRegularExpressionFilter = new System.Windows.Forms.TextBox();
            this.labelRenamedRegularExpressionFilter = new System.Windows.Forms.Label();
            this.textBoxDeletedRegularExpressionFilter = new System.Windows.Forms.TextBox();
            this.labelDeletedRegularExpressionFilter = new System.Windows.Forms.Label();
            this.textBoxCreatedRegularExpressionFilter = new System.Windows.Forms.TextBox();
            this.labelCreatedRegularExpressionFilter = new System.Windows.Forms.Label();
            this.textBoxChangedRegularExpressionFilter = new System.Windows.Forms.TextBox();
            this.labelChangedRegularExpressionFilter = new System.Windows.Forms.Label();
            this.tabPageFilteredMode = new System.Windows.Forms.TabPage();
            this.numericUpDownFilteredModeFilterTimeout = new System.Windows.Forms.NumericUpDown();
            this.labelFilteredModeFilterTimeout = new System.Windows.Forms.Label();
            this.labelFilteredMode = new System.Windows.Forms.Label();
            this.checkBoxFilteredMode = new System.Windows.Forms.CheckBox();
            this.tabPageCallService = new System.Windows.Forms.TabPage();
            this.checkBoxStreamFile = new System.Windows.Forms.CheckBox();
            this.textBoxCallServiceNote = new System.Windows.Forms.TextBox();
            this.labelCallServiceSettings = new System.Windows.Forms.Label();
            this.checkBoxCallService = new System.Windows.Forms.CheckBox();
            this.tabPageProcess = new System.Windows.Forms.TabPage();
            this.numericUpDownProcessBatchSize = new System.Windows.Forms.NumericUpDown();
            this.labelProcessBatchSize = new System.Windows.Forms.Label();
            this.labelProcessWindowStyle = new System.Windows.Forms.Label();
            this.comboBoxWindowStyle = new System.Windows.Forms.ComboBox();
            this.buttonBrowseWorkingDirectory = new System.Windows.Forms.Button();
            this.textBoxProcessWorkingDirectory = new System.Windows.Forms.TextBox();
            this.labelProcessWorkingDirectory = new System.Windows.Forms.Label();
            this.textBoxProcessVerb = new System.Windows.Forms.TextBox();
            this.labelProcessVerb = new System.Windows.Forms.Label();
            this.buttonBrowseExecutable = new System.Windows.Forms.Button();
            this.textBoxProcessFileName = new System.Windows.Forms.TextBox();
            this.labelProcessFileName = new System.Windows.Forms.Label();
            this.checkBoxStartProcess = new System.Windows.Forms.CheckBox();
            this.tabPageProcessArguments = new System.Windows.Forms.TabPage();
            this.labelProcessArgumentsChangeTypeEscapeString = new System.Windows.Forms.Label();
            this.textBoxProcessArgumentsChangeTypeEscapeString = new System.Windows.Forms.TextBox();
            this.checkBoxProcessUseChangeTypeAsArgument = new System.Windows.Forms.CheckBox();
            this.labelProcessArgumentsOldFileNameEscapeString = new System.Windows.Forms.Label();
            this.textBoxProcessArgumentsOldFileNameEscapeString = new System.Windows.Forms.TextBox();
            this.checkBoxProcessUseOldFileNameAsArgument = new System.Windows.Forms.CheckBox();
            this.labelProcessArgumentsFileNameEscapeString = new System.Windows.Forms.Label();
            this.textBoxProcessArgumentsFileNameEscapeString = new System.Windows.Forms.TextBox();
            this.checkBoxProcessUseFileNameAsArgument = new System.Windows.Forms.CheckBox();
            this.textBoxProcessArguments = new System.Windows.Forms.TextBox();
            this.labelProcessArguments = new System.Windows.Forms.Label();
            this.tabPageProcessAdvanced = new System.Windows.Forms.TabPage();
            this.groupBoxSynchronizedExecution = new System.Windows.Forms.GroupBox();
            this.checkBoxProcessSynchronizedExecution = new System.Windows.Forms.CheckBox();
            this.numericUpDownProcessMaxWaitTime = new System.Windows.Forms.NumericUpDown();
            this.labelProcessMaxWaitTime = new System.Windows.Forms.Label();
            this.groupBoxAdvanced = new System.Windows.Forms.GroupBox();
            this.checkBoxProcessUseShellExecute = new System.Windows.Forms.CheckBox();
            this.checkBoxProcessCreateNoWindow = new System.Windows.Forms.CheckBox();
            this.checkBoxProcessRedirectStandardOutput = new System.Windows.Forms.CheckBox();
            this.checkBoxProcessRedirectStandardError = new System.Windows.Forms.CheckBox();
            this.groupBoxUserProfile = new System.Windows.Forms.GroupBox();
            this.checkBoxProcessLoadUserProfile = new System.Windows.Forms.CheckBox();
            this.labelProcessPassword = new System.Windows.Forms.Label();
            this.textBoxProcessDomain = new System.Windows.Forms.TextBox();
            this.labelProcessUserName = new System.Windows.Forms.Label();
            this.textBoxProcessUserName = new System.Windows.Forms.TextBox();
            this.labelProcessDomain = new System.Windows.Forms.Label();
            this.maskedTextBoxProcessPassword = new System.Windows.Forms.MaskedTextBox();
            this.tabPageFileProcessing = new System.Windows.Forms.TabPage();
            this.groupBoxFileProcessingSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxProcessFileMustExist = new System.Windows.Forms.CheckBox();
            this.labelProcessDelay = new System.Windows.Forms.Label();
            this.numericUpDownProcessDelay = new System.Windows.Forms.NumericUpDown();
            this.groupBoxFileLockTest = new System.Windows.Forms.GroupBox();
            this.checkBoxProcessLockFile = new System.Windows.Forms.CheckBox();
            this.numericUpDownProcessLockFileLastWriteDelay = new System.Windows.Forms.NumericUpDown();
            this.labelProcessLockFileRetries = new System.Windows.Forms.Label();
            this.labelProcessLockFileLastWriteDelay = new System.Windows.Forms.Label();
            this.labelProcessLockFileRetriesQueueLimit = new System.Windows.Forms.Label();
            this.numericUpDownProcessLockFileRetriesQueueLimit = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownProcessLockFileRetries = new System.Windows.Forms.NumericUpDown();
            this.tabPageFileRenaming = new System.Windows.Forms.TabPage();
            this.labelTryRenameFileRetries = new System.Windows.Forms.Label();
            this.numericUpDownTryRenameRetries = new System.Windows.Forms.NumericUpDown();
            this.labelRenameFileSettings = new System.Windows.Forms.Label();
            this.checkBoxTryRenameFile = new System.Windows.Forms.CheckBox();
            this.tabPageLoggingAndMessages = new System.Windows.Forms.TabPage();
            this.checkBoxLogFileSystemSearchProgress = new System.Windows.Forms.CheckBox();
            this.checkBoxLogFileSystemSearchError = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayFileSystemSearchProgress = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayFileSystemSearchError = new System.Windows.Forms.CheckBox();
            this.labelMessages = new System.Windows.Forms.Label();
            this.labelLogging = new System.Windows.Forms.Label();
            this.checkBoxLogProcessEnd = new System.Windows.Forms.CheckBox();
            this.checkBoxLogProcessStart = new System.Windows.Forms.CheckBox();
            this.checkBoxLogFileSystemChange = new System.Windows.Forms.CheckBox();
            this.checkBoxDisplayFileSystemChange = new System.Windows.Forms.CheckBox();
            this.tabPageAdvanced = new System.Windows.Forms.TabPage();
            this.checkBoxSubscribeToBufferErrorEvent = new System.Windows.Forms.CheckBox();
            this.numericUpDownInternalBufferSize = new System.Windows.Forms.NumericUpDown();
            this.labelInternalBufferSize = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.panelTabs.SuspendLayout();
            this.tabControlProperties.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecycleInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPollDirectoryInterval)).BeginInit();
            this.tabPageFileSystemEvents.SuspendLayout();
            this.tabPageFilters.SuspendLayout();
            this.tabPageFilteredMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilteredModeFilterTimeout)).BeginInit();
            this.tabPageCallService.SuspendLayout();
            this.tabPageProcess.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessBatchSize)).BeginInit();
            this.tabPageProcessArguments.SuspendLayout();
            this.tabPageProcessAdvanced.SuspendLayout();
            this.groupBoxSynchronizedExecution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessMaxWaitTime)).BeginInit();
            this.groupBoxAdvanced.SuspendLayout();
            this.groupBoxUserProfile.SuspendLayout();
            this.tabPageFileProcessing.SuspendLayout();
            this.groupBoxFileProcessingSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessDelay)).BeginInit();
            this.groupBoxFileLockTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessLockFileLastWriteDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessLockFileRetriesQueueLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessLockFileRetries)).BeginInit();
            this.tabPageFileRenaming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTryRenameRetries)).BeginInit();
            this.tabPageLoggingAndMessages.SuspendLayout();
            this.tabPageAdvanced.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInternalBufferSize)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTabs
            // 
            this.panelTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTabs.Controls.Add(this.tabControlProperties);
            this.panelTabs.Location = new System.Drawing.Point(4, 4);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(382, 372);
            this.panelTabs.TabIndex = 0;
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Controls.Add(this.tabPageGeneral);
            this.tabControlProperties.Controls.Add(this.tabPageFileSystemEvents);
            this.tabControlProperties.Controls.Add(this.tabPageFilters);
            this.tabControlProperties.Controls.Add(this.tabPageFilteredMode);
            this.tabControlProperties.Controls.Add(this.tabPageCallService);
            this.tabControlProperties.Controls.Add(this.tabPageProcess);
            this.tabControlProperties.Controls.Add(this.tabPageProcessArguments);
            this.tabControlProperties.Controls.Add(this.tabPageProcessAdvanced);
            this.tabControlProperties.Controls.Add(this.tabPageFileProcessing);
            this.tabControlProperties.Controls.Add(this.tabPageFileRenaming);
            this.tabControlProperties.Controls.Add(this.tabPageLoggingAndMessages);
            this.tabControlProperties.Controls.Add(this.tabPageAdvanced);
            this.tabControlProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlProperties.Location = new System.Drawing.Point(0, 0);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(382, 372);
            this.tabControlProperties.TabIndex = 0;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.labelMinutes);
            this.tabPageGeneral.Controls.Add(this.numericUpDownRecycleInterval);
            this.tabPageGeneral.Controls.Add(this.label1);
            this.tabPageGeneral.Controls.Add(this.numericUpDownPollDirectoryInterval);
            this.tabPageGeneral.Controls.Add(this.labelPollDirectoryInterval);
            this.tabPageGeneral.Controls.Add(this.checkBoxRecycleFileWatcher);
            this.tabPageGeneral.Controls.Add(this.checkBoxPollDirectory);
            this.tabPageGeneral.Controls.Add(this.checkBoxGenerateFileSystemEventsAtStartup);
            this.tabPageGeneral.Controls.Add(this.checkBoxIncludeSubdirectories);
            this.tabPageGeneral.Controls.Add(this.buttonBrowseDirectory);
            this.tabPageGeneral.Controls.Add(this.textBoxPath);
            this.tabPageGeneral.Controls.Add(this.labelPathToWatch);
            this.tabPageGeneral.Controls.Add(this.checkBoxStartDaemon);
            this.tabPageGeneral.Controls.Add(this.textBoxDaemonName);
            this.tabPageGeneral.Controls.Add(this.labelDaemonName);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(374, 346);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // labelMinutes
            // 
            this.labelMinutes.AutoSize = true;
            this.labelMinutes.Location = new System.Drawing.Point(245, 236);
            this.labelMinutes.Name = "labelMinutes";
            this.labelMinutes.Size = new System.Drawing.Size(46, 13);
            this.labelMinutes.TabIndex = 31;
            this.labelMinutes.Text = "minutes.";
            // 
            // numericUpDownRecycleInterval
            // 
            this.numericUpDownRecycleInterval.Location = new System.Drawing.Point(170, 234);
            this.numericUpDownRecycleInterval.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownRecycleInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRecycleInterval.Name = "numericUpDownRecycleInterval";
            this.numericUpDownRecycleInterval.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownRecycleInterval.TabIndex = 10;
            this.numericUpDownRecycleInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Always recycle file watcher after";
            // 
            // numericUpDownPollDirectoryInterval
            // 
            this.numericUpDownPollDirectoryInterval.Location = new System.Drawing.Point(184, 192);
            this.numericUpDownPollDirectoryInterval.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownPollDirectoryInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPollDirectoryInterval.Name = "numericUpDownPollDirectoryInterval";
            this.numericUpDownPollDirectoryInterval.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownPollDirectoryInterval.TabIndex = 8;
            this.numericUpDownPollDirectoryInterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelPollDirectoryInterval
            // 
            this.labelPollDirectoryInterval.AutoSize = true;
            this.labelPollDirectoryInterval.Location = new System.Drawing.Point(6, 194);
            this.labelPollDirectoryInterval.Name = "labelPollDirectoryInterval";
            this.labelPollDirectoryInterval.Size = new System.Drawing.Size(172, 13);
            this.labelPollDirectoryInterval.TabIndex = 28;
            this.labelPollDirectoryInterval.Text = "Directory polling interval in minutes:";
            // 
            // checkBoxRecycleFileWatcher
            // 
            this.checkBoxRecycleFileWatcher.AutoSize = true;
            this.checkBoxRecycleFileWatcher.Location = new System.Drawing.Point(9, 216);
            this.checkBoxRecycleFileWatcher.Name = "checkBoxRecycleFileWatcher";
            this.checkBoxRecycleFileWatcher.Size = new System.Drawing.Size(153, 17);
            this.checkBoxRecycleFileWatcher.TabIndex = 9;
            this.checkBoxRecycleFileWatcher.Text = "Allow file watcher recycling";
            this.checkBoxRecycleFileWatcher.UseVisualStyleBackColor = true;
            // 
            // checkBoxPollDirectory
            // 
            this.checkBoxPollDirectory.AutoSize = true;
            this.checkBoxPollDirectory.Location = new System.Drawing.Point(9, 174);
            this.checkBoxPollDirectory.Name = "checkBoxPollDirectory";
            this.checkBoxPollDirectory.Size = new System.Drawing.Size(256, 17);
            this.checkBoxPollDirectory.TabIndex = 7;
            this.checkBoxPollDirectory.Text = "Poll directory for new (created) and changed files";
            this.checkBoxPollDirectory.UseVisualStyleBackColor = true;
            // 
            // checkBoxGenerateFileSystemEventsAtStartup
            // 
            this.checkBoxGenerateFileSystemEventsAtStartup.AutoSize = true;
            this.checkBoxGenerateFileSystemEventsAtStartup.Location = new System.Drawing.Point(9, 151);
            this.checkBoxGenerateFileSystemEventsAtStartup.Name = "checkBoxGenerateFileSystemEventsAtStartup";
            this.checkBoxGenerateFileSystemEventsAtStartup.Size = new System.Drawing.Size(291, 17);
            this.checkBoxGenerateFileSystemEventsAtStartup.TabIndex = 6;
            this.checkBoxGenerateFileSystemEventsAtStartup.Text = "Generate file system events at startup for each found file";
            this.checkBoxGenerateFileSystemEventsAtStartup.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeSubdirectories
            // 
            this.checkBoxIncludeSubdirectories.AutoSize = true;
            this.checkBoxIncludeSubdirectories.Location = new System.Drawing.Point(9, 128);
            this.checkBoxIncludeSubdirectories.Name = "checkBoxIncludeSubdirectories";
            this.checkBoxIncludeSubdirectories.Size = new System.Drawing.Size(129, 17);
            this.checkBoxIncludeSubdirectories.TabIndex = 5;
            this.checkBoxIncludeSubdirectories.Text = "Monitor subdirectories";
            this.checkBoxIncludeSubdirectories.UseVisualStyleBackColor = true;
            // 
            // buttonBrowseDirectory
            // 
            this.buttonBrowseDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseDirectory.Location = new System.Drawing.Point(292, 100);
            this.buttonBrowseDirectory.Name = "buttonBrowseDirectory";
            this.buttonBrowseDirectory.Size = new System.Drawing.Size(74, 23);
            this.buttonBrowseDirectory.TabIndex = 4;
            this.buttonBrowseDirectory.Text = "Browse...";
            this.buttonBrowseDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseDirectory.Click += new System.EventHandler(this.ButtonBrowseDirectoryClick);
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(9, 102);
            this.textBoxPath.MaxLength = 247;
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(277, 20);
            this.textBoxPath.TabIndex = 3;
            // 
            // labelPathToWatch
            // 
            this.labelPathToWatch.AutoSize = true;
            this.labelPathToWatch.Location = new System.Drawing.Point(6, 86);
            this.labelPathToWatch.Name = "labelPathToWatch";
            this.labelPathToWatch.Size = new System.Drawing.Size(149, 13);
            this.labelPathToWatch.TabIndex = 3;
            this.labelPathToWatch.Text = "Path of the directory to watch:";
            // 
            // checkBoxStartDaemon
            // 
            this.checkBoxStartDaemon.AutoSize = true;
            this.checkBoxStartDaemon.Location = new System.Drawing.Point(9, 54);
            this.checkBoxStartDaemon.Name = "checkBoxStartDaemon";
            this.checkBoxStartDaemon.Size = new System.Drawing.Size(100, 17);
            this.checkBoxStartDaemon.TabIndex = 2;
            this.checkBoxStartDaemon.Text = "Enable daemon";
            this.checkBoxStartDaemon.UseVisualStyleBackColor = true;
            // 
            // textBoxDaemonName
            // 
            this.textBoxDaemonName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDaemonName.Location = new System.Drawing.Point(9, 28);
            this.textBoxDaemonName.Name = "textBoxDaemonName";
            this.textBoxDaemonName.Size = new System.Drawing.Size(357, 20);
            this.textBoxDaemonName.TabIndex = 1;
            // 
            // labelDaemonName
            // 
            this.labelDaemonName.AutoSize = true;
            this.labelDaemonName.Location = new System.Drawing.Point(6, 12);
            this.labelDaemonName.Name = "labelDaemonName";
            this.labelDaemonName.Size = new System.Drawing.Size(137, 13);
            this.labelDaemonName.TabIndex = 0;
            this.labelDaemonName.Text = "File watcher daemon name:";
            // 
            // tabPageFileSystemEvents
            // 
            this.tabPageFileSystemEvents.Controls.Add(this.labelFileSystemEventFilters);
            this.tabPageFileSystemEvents.Controls.Add(this.labelFileSystemEventsToWatch);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifyDirectoryName);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifyLastWrite);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifyAttribute);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifySecurity);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifySize);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifyLastAccess);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxSubscribeToChangedEvent);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxSubscribeToCreatedEvent);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxSubscribeToRenamedEvent);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifyCreationTime);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxNotifyFileName);
            this.tabPageFileSystemEvents.Controls.Add(this.checkBoxSubscribeToDeletedEvent);
            this.tabPageFileSystemEvents.Location = new System.Drawing.Point(4, 22);
            this.tabPageFileSystemEvents.Name = "tabPageFileSystemEvents";
            this.tabPageFileSystemEvents.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFileSystemEvents.Size = new System.Drawing.Size(374, 346);
            this.tabPageFileSystemEvents.TabIndex = 1;
            this.tabPageFileSystemEvents.Text = "File system events";
            this.tabPageFileSystemEvents.UseVisualStyleBackColor = true;
            // 
            // labelFileSystemEventFilters
            // 
            this.labelFileSystemEventFilters.AutoSize = true;
            this.labelFileSystemEventFilters.Location = new System.Drawing.Point(6, 90);
            this.labelFileSystemEventFilters.Name = "labelFileSystemEventFilters";
            this.labelFileSystemEventFilters.Size = new System.Drawing.Size(174, 13);
            this.labelFileSystemEventFilters.TabIndex = 9;
            this.labelFileSystemEventFilters.Text = "Select event filters to enable event:";
            // 
            // labelFileSystemEventsToWatch
            // 
            this.labelFileSystemEventsToWatch.AutoSize = true;
            this.labelFileSystemEventsToWatch.Location = new System.Drawing.Point(6, 12);
            this.labelFileSystemEventsToWatch.Name = "labelFileSystemEventsToWatch";
            this.labelFileSystemEventsToWatch.Size = new System.Drawing.Size(170, 13);
            this.labelFileSystemEventsToWatch.TabIndex = 8;
            this.labelFileSystemEventsToWatch.Text = "Select file system events to watch:";
            // 
            // checkBoxNotifyDirectoryName
            // 
            this.checkBoxNotifyDirectoryName.AutoSize = true;
            this.checkBoxNotifyDirectoryName.Location = new System.Drawing.Point(9, 162);
            this.checkBoxNotifyDirectoryName.Name = "checkBoxNotifyDirectoryName";
            this.checkBoxNotifyDirectoryName.Size = new System.Drawing.Size(278, 17);
            this.checkBoxNotifyDirectoryName.TabIndex = 6;
            this.checkBoxNotifyDirectoryName.Text = "Directory name (created, deleted and renamed event)";
            this.checkBoxNotifyDirectoryName.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyLastWrite
            // 
            this.checkBoxNotifyLastWrite.AutoSize = true;
            this.checkBoxNotifyLastWrite.Location = new System.Drawing.Point(9, 231);
            this.checkBoxNotifyLastWrite.Name = "checkBoxNotifyLastWrite";
            this.checkBoxNotifyLastWrite.Size = new System.Drawing.Size(152, 17);
            this.checkBoxNotifyLastWrite.TabIndex = 9;
            this.checkBoxNotifyLastWrite.Text = "Last write (changed event)";
            this.checkBoxNotifyLastWrite.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyAttribute
            // 
            this.checkBoxNotifyAttribute.AutoSize = true;
            this.checkBoxNotifyAttribute.Location = new System.Drawing.Point(9, 116);
            this.checkBoxNotifyAttribute.Name = "checkBoxNotifyAttribute";
            this.checkBoxNotifyAttribute.Size = new System.Drawing.Size(146, 17);
            this.checkBoxNotifyAttribute.TabIndex = 4;
            this.checkBoxNotifyAttribute.Text = "Attribute (changed event)";
            this.checkBoxNotifyAttribute.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifySecurity
            // 
            this.checkBoxNotifySecurity.AutoSize = true;
            this.checkBoxNotifySecurity.Location = new System.Drawing.Point(9, 254);
            this.checkBoxNotifySecurity.Name = "checkBoxNotifySecurity";
            this.checkBoxNotifySecurity.Size = new System.Drawing.Size(145, 17);
            this.checkBoxNotifySecurity.TabIndex = 10;
            this.checkBoxNotifySecurity.Text = "Security (changed event)";
            this.checkBoxNotifySecurity.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifySize
            // 
            this.checkBoxNotifySize.AutoSize = true;
            this.checkBoxNotifySize.Location = new System.Drawing.Point(9, 277);
            this.checkBoxNotifySize.Name = "checkBoxNotifySize";
            this.checkBoxNotifySize.Size = new System.Drawing.Size(127, 17);
            this.checkBoxNotifySize.TabIndex = 11;
            this.checkBoxNotifySize.Text = "Size (changed event)";
            this.checkBoxNotifySize.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyLastAccess
            // 
            this.checkBoxNotifyLastAccess.AutoSize = true;
            this.checkBoxNotifyLastAccess.Location = new System.Drawing.Point(9, 208);
            this.checkBoxNotifyLastAccess.Name = "checkBoxNotifyLastAccess";
            this.checkBoxNotifyLastAccess.Size = new System.Drawing.Size(164, 17);
            this.checkBoxNotifyLastAccess.TabIndex = 8;
            this.checkBoxNotifyLastAccess.Text = "Last access (changed event)";
            this.checkBoxNotifyLastAccess.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubscribeToChangedEvent
            // 
            this.checkBoxSubscribeToChangedEvent.AutoSize = true;
            this.checkBoxSubscribeToChangedEvent.Location = new System.Drawing.Point(9, 38);
            this.checkBoxSubscribeToChangedEvent.Name = "checkBoxSubscribeToChangedEvent";
            this.checkBoxSubscribeToChangedEvent.Size = new System.Drawing.Size(128, 17);
            this.checkBoxSubscribeToChangedEvent.TabIndex = 0;
            this.checkBoxSubscribeToChangedEvent.Text = "File or folder changed";
            this.checkBoxSubscribeToChangedEvent.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubscribeToCreatedEvent
            // 
            this.checkBoxSubscribeToCreatedEvent.AutoSize = true;
            this.checkBoxSubscribeToCreatedEvent.Location = new System.Drawing.Point(9, 61);
            this.checkBoxSubscribeToCreatedEvent.Name = "checkBoxSubscribeToCreatedEvent";
            this.checkBoxSubscribeToCreatedEvent.Size = new System.Drawing.Size(122, 17);
            this.checkBoxSubscribeToCreatedEvent.TabIndex = 2;
            this.checkBoxSubscribeToCreatedEvent.Text = "File or folder created";
            this.checkBoxSubscribeToCreatedEvent.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubscribeToRenamedEvent
            // 
            this.checkBoxSubscribeToRenamedEvent.AutoSize = true;
            this.checkBoxSubscribeToRenamedEvent.Location = new System.Drawing.Point(178, 61);
            this.checkBoxSubscribeToRenamedEvent.Name = "checkBoxSubscribeToRenamedEvent";
            this.checkBoxSubscribeToRenamedEvent.Size = new System.Drawing.Size(127, 17);
            this.checkBoxSubscribeToRenamedEvent.TabIndex = 3;
            this.checkBoxSubscribeToRenamedEvent.Text = "File or folder renamed";
            this.checkBoxSubscribeToRenamedEvent.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyCreationTime
            // 
            this.checkBoxNotifyCreationTime.AutoSize = true;
            this.checkBoxNotifyCreationTime.Location = new System.Drawing.Point(9, 139);
            this.checkBoxNotifyCreationTime.Name = "checkBoxNotifyCreationTime";
            this.checkBoxNotifyCreationTime.Size = new System.Drawing.Size(168, 17);
            this.checkBoxNotifyCreationTime.TabIndex = 5;
            this.checkBoxNotifyCreationTime.Text = "Creation time (changed event)";
            this.checkBoxNotifyCreationTime.UseVisualStyleBackColor = true;
            // 
            // checkBoxNotifyFileName
            // 
            this.checkBoxNotifyFileName.AutoSize = true;
            this.checkBoxNotifyFileName.Location = new System.Drawing.Point(9, 185);
            this.checkBoxNotifyFileName.Name = "checkBoxNotifyFileName";
            this.checkBoxNotifyFileName.Size = new System.Drawing.Size(252, 17);
            this.checkBoxNotifyFileName.TabIndex = 7;
            this.checkBoxNotifyFileName.Text = "File name (created, deleted and renamed event)";
            this.checkBoxNotifyFileName.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubscribeToDeletedEvent
            // 
            this.checkBoxSubscribeToDeletedEvent.AutoSize = true;
            this.checkBoxSubscribeToDeletedEvent.Location = new System.Drawing.Point(178, 38);
            this.checkBoxSubscribeToDeletedEvent.Name = "checkBoxSubscribeToDeletedEvent";
            this.checkBoxSubscribeToDeletedEvent.Size = new System.Drawing.Size(121, 17);
            this.checkBoxSubscribeToDeletedEvent.TabIndex = 1;
            this.checkBoxSubscribeToDeletedEvent.Text = "File or folder deleted";
            this.checkBoxSubscribeToDeletedEvent.UseVisualStyleBackColor = true;
            // 
            // tabPageFilters
            // 
            this.tabPageFilters.Controls.Add(this.textBoxGenereatedEventFileNameRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.labelGnereatedEventFileNameRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.labelAdditionalFilters);
            this.tabPageFilters.Controls.Add(this.textBoxFilter);
            this.tabPageFilters.Controls.Add(this.labelFileNameFilterString);
            this.tabPageFilters.Controls.Add(this.textBoxRenamedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.labelRenamedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.textBoxDeletedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.labelDeletedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.textBoxCreatedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.labelCreatedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.textBoxChangedRegularExpressionFilter);
            this.tabPageFilters.Controls.Add(this.labelChangedRegularExpressionFilter);
            this.tabPageFilters.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilters.Name = "tabPageFilters";
            this.tabPageFilters.Size = new System.Drawing.Size(374, 346);
            this.tabPageFilters.TabIndex = 7;
            this.tabPageFilters.Text = "Filters";
            this.tabPageFilters.UseVisualStyleBackColor = true;
            // 
            // textBoxGenereatedEventFileNameRegularExpressionFilter
            // 
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.Location = new System.Drawing.Point(9, 302);
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.MaxLength = 0;
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.Name = "textBoxGenereatedEventFileNameRegularExpressionFilter";
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.Size = new System.Drawing.Size(357, 20);
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.TabIndex = 6;
            this.textBoxGenereatedEventFileNameRegularExpressionFilter.Tag = "";
            // 
            // labelGnereatedEventFileNameRegularExpressionFilter
            // 
            this.labelGnereatedEventFileNameRegularExpressionFilter.AutoSize = true;
            this.labelGnereatedEventFileNameRegularExpressionFilter.Location = new System.Drawing.Point(6, 286);
            this.labelGnereatedEventFileNameRegularExpressionFilter.Name = "labelGnereatedEventFileNameRegularExpressionFilter";
            this.labelGnereatedEventFileNameRegularExpressionFilter.Size = new System.Drawing.Size(147, 13);
            this.labelGnereatedEventFileNameRegularExpressionFilter.TabIndex = 37;
            this.labelGnereatedEventFileNameRegularExpressionFilter.Text = "System generated event filter:";
            // 
            // labelAdditionalFilters
            // 
            this.labelAdditionalFilters.AutoSize = true;
            this.labelAdditionalFilters.Location = new System.Drawing.Point(6, 62);
            this.labelAdditionalFilters.Name = "labelAdditionalFilters";
            this.labelAdditionalFilters.Size = new System.Drawing.Size(168, 13);
            this.labelAdditionalFilters.TabIndex = 35;
            this.labelAdditionalFilters.Text = "Additional regular expression filters";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(9, 28);
            this.textBoxFilter.MaxLength = 255;
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(100, 20);
            this.textBoxFilter.TabIndex = 1;
            // 
            // labelFileNameFilterString
            // 
            this.labelFileNameFilterString.AutoSize = true;
            this.labelFileNameFilterString.Location = new System.Drawing.Point(6, 12);
            this.labelFileNameFilterString.Name = "labelFileNameFilterString";
            this.labelFileNameFilterString.Size = new System.Drawing.Size(128, 13);
            this.labelFileNameFilterString.TabIndex = 33;
            this.labelFileNameFilterString.Text = "Main file name filter string:";
            // 
            // textBoxRenamedRegularExpressionFilter
            // 
            this.textBoxRenamedRegularExpressionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRenamedRegularExpressionFilter.Location = new System.Drawing.Point(9, 253);
            this.textBoxRenamedRegularExpressionFilter.MaxLength = 0;
            this.textBoxRenamedRegularExpressionFilter.Name = "textBoxRenamedRegularExpressionFilter";
            this.textBoxRenamedRegularExpressionFilter.Size = new System.Drawing.Size(357, 20);
            this.textBoxRenamedRegularExpressionFilter.TabIndex = 5;
            this.textBoxRenamedRegularExpressionFilter.Tag = "";
            // 
            // labelRenamedRegularExpressionFilter
            // 
            this.labelRenamedRegularExpressionFilter.AutoSize = true;
            this.labelRenamedRegularExpressionFilter.Location = new System.Drawing.Point(6, 237);
            this.labelRenamedRegularExpressionFilter.Name = "labelRenamedRegularExpressionFilter";
            this.labelRenamedRegularExpressionFilter.Size = new System.Drawing.Size(133, 13);
            this.labelRenamedRegularExpressionFilter.TabIndex = 32;
            this.labelRenamedRegularExpressionFilter.Text = "File or folder renamed filter:";
            // 
            // textBoxDeletedRegularExpressionFilter
            // 
            this.textBoxDeletedRegularExpressionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDeletedRegularExpressionFilter.Location = new System.Drawing.Point(9, 203);
            this.textBoxDeletedRegularExpressionFilter.MaxLength = 0;
            this.textBoxDeletedRegularExpressionFilter.Name = "textBoxDeletedRegularExpressionFilter";
            this.textBoxDeletedRegularExpressionFilter.Size = new System.Drawing.Size(357, 20);
            this.textBoxDeletedRegularExpressionFilter.TabIndex = 4;
            // 
            // labelDeletedRegularExpressionFilter
            // 
            this.labelDeletedRegularExpressionFilter.AutoSize = true;
            this.labelDeletedRegularExpressionFilter.Location = new System.Drawing.Point(6, 187);
            this.labelDeletedRegularExpressionFilter.Name = "labelDeletedRegularExpressionFilter";
            this.labelDeletedRegularExpressionFilter.Size = new System.Drawing.Size(127, 13);
            this.labelDeletedRegularExpressionFilter.TabIndex = 30;
            this.labelDeletedRegularExpressionFilter.Text = "File or folder deleted filter:";
            // 
            // textBoxCreatedRegularExpressionFilter
            // 
            this.textBoxCreatedRegularExpressionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCreatedRegularExpressionFilter.Location = new System.Drawing.Point(9, 154);
            this.textBoxCreatedRegularExpressionFilter.MaxLength = 0;
            this.textBoxCreatedRegularExpressionFilter.Name = "textBoxCreatedRegularExpressionFilter";
            this.textBoxCreatedRegularExpressionFilter.Size = new System.Drawing.Size(357, 20);
            this.textBoxCreatedRegularExpressionFilter.TabIndex = 3;
            // 
            // labelCreatedRegularExpressionFilter
            // 
            this.labelCreatedRegularExpressionFilter.AutoSize = true;
            this.labelCreatedRegularExpressionFilter.Location = new System.Drawing.Point(6, 138);
            this.labelCreatedRegularExpressionFilter.Name = "labelCreatedRegularExpressionFilter";
            this.labelCreatedRegularExpressionFilter.Size = new System.Drawing.Size(128, 13);
            this.labelCreatedRegularExpressionFilter.TabIndex = 28;
            this.labelCreatedRegularExpressionFilter.Text = "File or folder created filter:";
            // 
            // textBoxChangedRegularExpressionFilter
            // 
            this.textBoxChangedRegularExpressionFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxChangedRegularExpressionFilter.Location = new System.Drawing.Point(9, 105);
            this.textBoxChangedRegularExpressionFilter.MaxLength = 0;
            this.textBoxChangedRegularExpressionFilter.Name = "textBoxChangedRegularExpressionFilter";
            this.textBoxChangedRegularExpressionFilter.Size = new System.Drawing.Size(357, 20);
            this.textBoxChangedRegularExpressionFilter.TabIndex = 2;
            // 
            // labelChangedRegularExpressionFilter
            // 
            this.labelChangedRegularExpressionFilter.AutoSize = true;
            this.labelChangedRegularExpressionFilter.Location = new System.Drawing.Point(6, 89);
            this.labelChangedRegularExpressionFilter.Name = "labelChangedRegularExpressionFilter";
            this.labelChangedRegularExpressionFilter.Size = new System.Drawing.Size(134, 13);
            this.labelChangedRegularExpressionFilter.TabIndex = 26;
            this.labelChangedRegularExpressionFilter.Text = "File or folder changed filter:";
            // 
            // tabPageFilteredMode
            // 
            this.tabPageFilteredMode.Controls.Add(this.numericUpDownFilteredModeFilterTimeout);
            this.tabPageFilteredMode.Controls.Add(this.labelFilteredModeFilterTimeout);
            this.tabPageFilteredMode.Controls.Add(this.labelFilteredMode);
            this.tabPageFilteredMode.Controls.Add(this.checkBoxFilteredMode);
            this.tabPageFilteredMode.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilteredMode.Name = "tabPageFilteredMode";
            this.tabPageFilteredMode.Size = new System.Drawing.Size(374, 346);
            this.tabPageFilteredMode.TabIndex = 11;
            this.tabPageFilteredMode.Text = "Filtered mode";
            this.tabPageFilteredMode.UseVisualStyleBackColor = true;
            // 
            // numericUpDownFilteredModeFilterTimeout
            // 
            this.numericUpDownFilteredModeFilterTimeout.Location = new System.Drawing.Point(9, 89);
            this.numericUpDownFilteredModeFilterTimeout.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownFilteredModeFilterTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFilteredModeFilterTimeout.Name = "numericUpDownFilteredModeFilterTimeout";
            this.numericUpDownFilteredModeFilterTimeout.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownFilteredModeFilterTimeout.TabIndex = 2;
            this.numericUpDownFilteredModeFilterTimeout.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // labelFilteredModeFilterTimeout
            // 
            this.labelFilteredModeFilterTimeout.AutoSize = true;
            this.labelFilteredModeFilterTimeout.Location = new System.Drawing.Point(6, 73);
            this.labelFilteredModeFilterTimeout.Name = "labelFilteredModeFilterTimeout";
            this.labelFilteredModeFilterTimeout.Size = new System.Drawing.Size(282, 13);
            this.labelFilteredModeFilterTimeout.TabIndex = 26;
            this.labelFilteredModeFilterTimeout.Text = "Duplicate file system event suppression timeout in minutes:";
            // 
            // labelFilteredMode
            // 
            this.labelFilteredMode.AutoSize = true;
            this.labelFilteredMode.Location = new System.Drawing.Point(6, 12);
            this.labelFilteredMode.Name = "labelFilteredMode";
            this.labelFilteredMode.Size = new System.Drawing.Size(112, 13);
            this.labelFilteredMode.TabIndex = 3;
            this.labelFilteredMode.Text = "Filtered mode settings:";
            // 
            // checkBoxFilteredMode
            // 
            this.checkBoxFilteredMode.AutoSize = true;
            this.checkBoxFilteredMode.Location = new System.Drawing.Point(9, 38);
            this.checkBoxFilteredMode.Name = "checkBoxFilteredMode";
            this.checkBoxFilteredMode.Size = new System.Drawing.Size(348, 17);
            this.checkBoxFilteredMode.TabIndex = 1;
            this.checkBoxFilteredMode.Text = "Suppress duplicate created, changed and deleted file system events";
            this.checkBoxFilteredMode.UseVisualStyleBackColor = true;
            // 
            // tabPageCallService
            // 
            this.tabPageCallService.Controls.Add(this.checkBoxStreamFile);
            this.tabPageCallService.Controls.Add(this.textBoxCallServiceNote);
            this.tabPageCallService.Controls.Add(this.labelCallServiceSettings);
            this.tabPageCallService.Controls.Add(this.checkBoxCallService);
            this.tabPageCallService.Location = new System.Drawing.Point(4, 22);
            this.tabPageCallService.Name = "tabPageCallService";
            this.tabPageCallService.Size = new System.Drawing.Size(374, 346);
            this.tabPageCallService.TabIndex = 9;
            this.tabPageCallService.Text = "Call service";
            this.tabPageCallService.UseVisualStyleBackColor = true;
            // 
            // checkBoxStreamFile
            // 
            this.checkBoxStreamFile.AutoSize = true;
            this.checkBoxStreamFile.Location = new System.Drawing.Point(9, 61);
            this.checkBoxStreamFile.Name = "checkBoxStreamFile";
            this.checkBoxStreamFile.Size = new System.Drawing.Size(124, 17);
            this.checkBoxStreamFile.TabIndex = 2;
            this.checkBoxStreamFile.Text = "Stream file to service";
            this.checkBoxStreamFile.UseVisualStyleBackColor = true;
            // 
            // textBoxCallServiceNote
            // 
            this.textBoxCallServiceNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCallServiceNote.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCallServiceNote.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCallServiceNote.Location = new System.Drawing.Point(9, 84);
            this.textBoxCallServiceNote.Multiline = true;
            this.textBoxCallServiceNote.Name = "textBoxCallServiceNote";
            this.textBoxCallServiceNote.ReadOnly = true;
            this.textBoxCallServiceNote.Size = new System.Drawing.Size(361, 43);
            this.textBoxCallServiceNote.TabIndex = 3;
            this.textBoxCallServiceNote.TabStop = false;
            this.textBoxCallServiceNote.Text = "The file watcher daemon name will be used to uniquely identify the service endpoi" +
    "nt to be called.";
            // 
            // labelCallServiceSettings
            // 
            this.labelCallServiceSettings.AutoSize = true;
            this.labelCallServiceSettings.Location = new System.Drawing.Point(6, 12);
            this.labelCallServiceSettings.Name = "labelCallServiceSettings";
            this.labelCallServiceSettings.Size = new System.Drawing.Size(103, 13);
            this.labelCallServiceSettings.TabIndex = 1;
            this.labelCallServiceSettings.Text = "Call service settings:";
            // 
            // checkBoxCallService
            // 
            this.checkBoxCallService.AutoSize = true;
            this.checkBoxCallService.Location = new System.Drawing.Point(9, 38);
            this.checkBoxCallService.Name = "checkBoxCallService";
            this.checkBoxCallService.Size = new System.Drawing.Size(254, 17);
            this.checkBoxCallService.TabIndex = 1;
            this.checkBoxCallService.Text = "Call service when file system change is detected";
            this.checkBoxCallService.UseVisualStyleBackColor = true;
            // 
            // tabPageProcess
            // 
            this.tabPageProcess.Controls.Add(this.numericUpDownProcessBatchSize);
            this.tabPageProcess.Controls.Add(this.labelProcessBatchSize);
            this.tabPageProcess.Controls.Add(this.labelProcessWindowStyle);
            this.tabPageProcess.Controls.Add(this.comboBoxWindowStyle);
            this.tabPageProcess.Controls.Add(this.buttonBrowseWorkingDirectory);
            this.tabPageProcess.Controls.Add(this.textBoxProcessWorkingDirectory);
            this.tabPageProcess.Controls.Add(this.labelProcessWorkingDirectory);
            this.tabPageProcess.Controls.Add(this.textBoxProcessVerb);
            this.tabPageProcess.Controls.Add(this.labelProcessVerb);
            this.tabPageProcess.Controls.Add(this.buttonBrowseExecutable);
            this.tabPageProcess.Controls.Add(this.textBoxProcessFileName);
            this.tabPageProcess.Controls.Add(this.labelProcessFileName);
            this.tabPageProcess.Controls.Add(this.checkBoxStartProcess);
            this.tabPageProcess.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcess.Name = "tabPageProcess";
            this.tabPageProcess.Size = new System.Drawing.Size(374, 346);
            this.tabPageProcess.TabIndex = 2;
            this.tabPageProcess.Text = "Process";
            this.tabPageProcess.UseVisualStyleBackColor = true;
            // 
            // numericUpDownProcessBatchSize
            // 
            this.numericUpDownProcessBatchSize.Location = new System.Drawing.Point(9, 247);
            this.numericUpDownProcessBatchSize.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownProcessBatchSize.Name = "numericUpDownProcessBatchSize";
            this.numericUpDownProcessBatchSize.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownProcessBatchSize.TabIndex = 8;
            // 
            // labelProcessBatchSize
            // 
            this.labelProcessBatchSize.AutoSize = true;
            this.labelProcessBatchSize.Location = new System.Drawing.Point(6, 231);
            this.labelProcessBatchSize.Name = "labelProcessBatchSize";
            this.labelProcessBatchSize.Size = new System.Drawing.Size(99, 13);
            this.labelProcessBatchSize.TabIndex = 24;
            this.labelProcessBatchSize.Text = "Process batch size:";
            // 
            // labelProcessWindowStyle
            // 
            this.labelProcessWindowStyle.AutoSize = true;
            this.labelProcessWindowStyle.Location = new System.Drawing.Point(6, 180);
            this.labelProcessWindowStyle.Name = "labelProcessWindowStyle";
            this.labelProcessWindowStyle.Size = new System.Drawing.Size(111, 13);
            this.labelProcessWindowStyle.TabIndex = 22;
            this.labelProcessWindowStyle.Text = "Process window style:";
            // 
            // comboBoxWindowStyle
            // 
            this.comboBoxWindowStyle.FormattingEnabled = true;
            this.comboBoxWindowStyle.Items.AddRange(new object[] {
            "Hidden",
            "Maximized",
            "Minimized",
            "Normal"});
            this.comboBoxWindowStyle.Location = new System.Drawing.Point(9, 196);
            this.comboBoxWindowStyle.Name = "comboBoxWindowStyle";
            this.comboBoxWindowStyle.Size = new System.Drawing.Size(108, 21);
            this.comboBoxWindowStyle.TabIndex = 7;
            // 
            // buttonBrowseWorkingDirectory
            // 
            this.buttonBrowseWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseWorkingDirectory.Location = new System.Drawing.Point(292, 143);
            this.buttonBrowseWorkingDirectory.Name = "buttonBrowseWorkingDirectory";
            this.buttonBrowseWorkingDirectory.Size = new System.Drawing.Size(74, 23);
            this.buttonBrowseWorkingDirectory.TabIndex = 6;
            this.buttonBrowseWorkingDirectory.Text = "Browse...";
            this.buttonBrowseWorkingDirectory.UseVisualStyleBackColor = true;
            this.buttonBrowseWorkingDirectory.Click += new System.EventHandler(this.ButtonBrowseWorkingDirectoryClick);
            // 
            // textBoxProcessWorkingDirectory
            // 
            this.textBoxProcessWorkingDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessWorkingDirectory.Location = new System.Drawing.Point(9, 146);
            this.textBoxProcessWorkingDirectory.MaxLength = 247;
            this.textBoxProcessWorkingDirectory.Name = "textBoxProcessWorkingDirectory";
            this.textBoxProcessWorkingDirectory.Size = new System.Drawing.Size(280, 20);
            this.textBoxProcessWorkingDirectory.TabIndex = 5;
            // 
            // labelProcessWorkingDirectory
            // 
            this.labelProcessWorkingDirectory.AutoSize = true;
            this.labelProcessWorkingDirectory.Location = new System.Drawing.Point(6, 130);
            this.labelProcessWorkingDirectory.Name = "labelProcessWorkingDirectory";
            this.labelProcessWorkingDirectory.Size = new System.Drawing.Size(131, 13);
            this.labelProcessWorkingDirectory.TabIndex = 15;
            this.labelProcessWorkingDirectory.Text = "Process working directory:";
            // 
            // textBoxProcessVerb
            // 
            this.textBoxProcessVerb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessVerb.Location = new System.Drawing.Point(9, 99);
            this.textBoxProcessVerb.MaxLength = 255;
            this.textBoxProcessVerb.Name = "textBoxProcessVerb";
            this.textBoxProcessVerb.Size = new System.Drawing.Size(357, 20);
            this.textBoxProcessVerb.TabIndex = 4;
            // 
            // labelProcessVerb
            // 
            this.labelProcessVerb.AutoSize = true;
            this.labelProcessVerb.Location = new System.Drawing.Point(6, 83);
            this.labelProcessVerb.Name = "labelProcessVerb";
            this.labelProcessVerb.Size = new System.Drawing.Size(72, 13);
            this.labelProcessVerb.TabIndex = 11;
            this.labelProcessVerb.Text = "Process verb:";
            // 
            // buttonBrowseExecutable
            // 
            this.buttonBrowseExecutable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseExecutable.Location = new System.Drawing.Point(292, 25);
            this.buttonBrowseExecutable.Name = "buttonBrowseExecutable";
            this.buttonBrowseExecutable.Size = new System.Drawing.Size(74, 23);
            this.buttonBrowseExecutable.TabIndex = 2;
            this.buttonBrowseExecutable.Text = "Browse...";
            this.buttonBrowseExecutable.UseVisualStyleBackColor = true;
            this.buttonBrowseExecutable.Click += new System.EventHandler(this.ButtonBrowseExecutableClick);
            // 
            // textBoxProcessFileName
            // 
            this.textBoxProcessFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessFileName.Location = new System.Drawing.Point(9, 28);
            this.textBoxProcessFileName.MaxLength = 247;
            this.textBoxProcessFileName.Name = "textBoxProcessFileName";
            this.textBoxProcessFileName.Size = new System.Drawing.Size(280, 20);
            this.textBoxProcessFileName.TabIndex = 1;
            // 
            // labelProcessFileName
            // 
            this.labelProcessFileName.AutoSize = true;
            this.labelProcessFileName.Location = new System.Drawing.Point(6, 12);
            this.labelProcessFileName.Name = "labelProcessFileName";
            this.labelProcessFileName.Size = new System.Drawing.Size(103, 13);
            this.labelProcessFileName.TabIndex = 6;
            this.labelProcessFileName.Text = "Process executable:";
            // 
            // checkBoxStartProcess
            // 
            this.checkBoxStartProcess.AutoSize = true;
            this.checkBoxStartProcess.Location = new System.Drawing.Point(9, 54);
            this.checkBoxStartProcess.Name = "checkBoxStartProcess";
            this.checkBoxStartProcess.Size = new System.Drawing.Size(262, 17);
            this.checkBoxStartProcess.TabIndex = 3;
            this.checkBoxStartProcess.Text = "Start process when file system change is detected";
            this.checkBoxStartProcess.UseVisualStyleBackColor = true;
            // 
            // tabPageProcessArguments
            // 
            this.tabPageProcessArguments.Controls.Add(this.labelProcessArgumentsChangeTypeEscapeString);
            this.tabPageProcessArguments.Controls.Add(this.textBoxProcessArgumentsChangeTypeEscapeString);
            this.tabPageProcessArguments.Controls.Add(this.checkBoxProcessUseChangeTypeAsArgument);
            this.tabPageProcessArguments.Controls.Add(this.labelProcessArgumentsOldFileNameEscapeString);
            this.tabPageProcessArguments.Controls.Add(this.textBoxProcessArgumentsOldFileNameEscapeString);
            this.tabPageProcessArguments.Controls.Add(this.checkBoxProcessUseOldFileNameAsArgument);
            this.tabPageProcessArguments.Controls.Add(this.labelProcessArgumentsFileNameEscapeString);
            this.tabPageProcessArguments.Controls.Add(this.textBoxProcessArgumentsFileNameEscapeString);
            this.tabPageProcessArguments.Controls.Add(this.checkBoxProcessUseFileNameAsArgument);
            this.tabPageProcessArguments.Controls.Add(this.textBoxProcessArguments);
            this.tabPageProcessArguments.Controls.Add(this.labelProcessArguments);
            this.tabPageProcessArguments.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcessArguments.Name = "tabPageProcessArguments";
            this.tabPageProcessArguments.Size = new System.Drawing.Size(374, 346);
            this.tabPageProcessArguments.TabIndex = 6;
            this.tabPageProcessArguments.Text = "Process arguments";
            this.tabPageProcessArguments.UseVisualStyleBackColor = true;
            // 
            // labelProcessArgumentsChangeTypeEscapeString
            // 
            this.labelProcessArgumentsChangeTypeEscapeString.AutoSize = true;
            this.labelProcessArgumentsChangeTypeEscapeString.Location = new System.Drawing.Point(6, 202);
            this.labelProcessArgumentsChangeTypeEscapeString.Name = "labelProcessArgumentsChangeTypeEscapeString";
            this.labelProcessArgumentsChangeTypeEscapeString.Size = new System.Drawing.Size(228, 13);
            this.labelProcessArgumentsChangeTypeEscapeString.TabIndex = 31;
            this.labelProcessArgumentsChangeTypeEscapeString.Text = "Process arguments change type escape string:";
            // 
            // textBoxProcessArgumentsChangeTypeEscapeString
            // 
            this.textBoxProcessArgumentsChangeTypeEscapeString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessArgumentsChangeTypeEscapeString.Location = new System.Drawing.Point(9, 218);
            this.textBoxProcessArgumentsChangeTypeEscapeString.MaxLength = 255;
            this.textBoxProcessArgumentsChangeTypeEscapeString.Name = "textBoxProcessArgumentsChangeTypeEscapeString";
            this.textBoxProcessArgumentsChangeTypeEscapeString.Size = new System.Drawing.Size(357, 20);
            this.textBoxProcessArgumentsChangeTypeEscapeString.TabIndex = 6;
            // 
            // checkBoxProcessUseChangeTypeAsArgument
            // 
            this.checkBoxProcessUseChangeTypeAsArgument.AutoSize = true;
            this.checkBoxProcessUseChangeTypeAsArgument.Location = new System.Drawing.Point(9, 244);
            this.checkBoxProcessUseChangeTypeAsArgument.Name = "checkBoxProcessUseChangeTypeAsArgument";
            this.checkBoxProcessUseChangeTypeAsArgument.Size = new System.Drawing.Size(168, 17);
            this.checkBoxProcessUseChangeTypeAsArgument.TabIndex = 7;
            this.checkBoxProcessUseChangeTypeAsArgument.Text = "Use change type as argument";
            this.checkBoxProcessUseChangeTypeAsArgument.UseVisualStyleBackColor = true;
            // 
            // labelProcessArgumentsOldFileNameEscapeString
            // 
            this.labelProcessArgumentsOldFileNameEscapeString.AutoSize = true;
            this.labelProcessArgumentsOldFileNameEscapeString.Location = new System.Drawing.Point(6, 131);
            this.labelProcessArgumentsOldFileNameEscapeString.Name = "labelProcessArgumentsOldFileNameEscapeString";
            this.labelProcessArgumentsOldFileNameEscapeString.Size = new System.Drawing.Size(228, 13);
            this.labelProcessArgumentsOldFileNameEscapeString.TabIndex = 28;
            this.labelProcessArgumentsOldFileNameEscapeString.Text = "Process arguments old file name escape string:";
            // 
            // textBoxProcessArgumentsOldFileNameEscapeString
            // 
            this.textBoxProcessArgumentsOldFileNameEscapeString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessArgumentsOldFileNameEscapeString.Location = new System.Drawing.Point(9, 147);
            this.textBoxProcessArgumentsOldFileNameEscapeString.MaxLength = 255;
            this.textBoxProcessArgumentsOldFileNameEscapeString.Name = "textBoxProcessArgumentsOldFileNameEscapeString";
            this.textBoxProcessArgumentsOldFileNameEscapeString.Size = new System.Drawing.Size(357, 20);
            this.textBoxProcessArgumentsOldFileNameEscapeString.TabIndex = 4;
            // 
            // checkBoxProcessUseOldFileNameAsArgument
            // 
            this.checkBoxProcessUseOldFileNameAsArgument.AutoSize = true;
            this.checkBoxProcessUseOldFileNameAsArgument.Location = new System.Drawing.Point(9, 173);
            this.checkBoxProcessUseOldFileNameAsArgument.Name = "checkBoxProcessUseOldFileNameAsArgument";
            this.checkBoxProcessUseOldFileNameAsArgument.Size = new System.Drawing.Size(168, 17);
            this.checkBoxProcessUseOldFileNameAsArgument.TabIndex = 5;
            this.checkBoxProcessUseOldFileNameAsArgument.Text = "Use old file name as argument";
            this.checkBoxProcessUseOldFileNameAsArgument.UseVisualStyleBackColor = true;
            // 
            // labelProcessArgumentsFileNameEscapeString
            // 
            this.labelProcessArgumentsFileNameEscapeString.AutoSize = true;
            this.labelProcessArgumentsFileNameEscapeString.Location = new System.Drawing.Point(6, 60);
            this.labelProcessArgumentsFileNameEscapeString.Name = "labelProcessArgumentsFileNameEscapeString";
            this.labelProcessArgumentsFileNameEscapeString.Size = new System.Drawing.Size(211, 13);
            this.labelProcessArgumentsFileNameEscapeString.TabIndex = 25;
            this.labelProcessArgumentsFileNameEscapeString.Text = "Process arguments file name escape string:";
            // 
            // textBoxProcessArgumentsFileNameEscapeString
            // 
            this.textBoxProcessArgumentsFileNameEscapeString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessArgumentsFileNameEscapeString.Location = new System.Drawing.Point(9, 76);
            this.textBoxProcessArgumentsFileNameEscapeString.MaxLength = 255;
            this.textBoxProcessArgumentsFileNameEscapeString.Name = "textBoxProcessArgumentsFileNameEscapeString";
            this.textBoxProcessArgumentsFileNameEscapeString.Size = new System.Drawing.Size(357, 20);
            this.textBoxProcessArgumentsFileNameEscapeString.TabIndex = 2;
            // 
            // checkBoxProcessUseFileNameAsArgument
            // 
            this.checkBoxProcessUseFileNameAsArgument.AutoSize = true;
            this.checkBoxProcessUseFileNameAsArgument.Location = new System.Drawing.Point(9, 102);
            this.checkBoxProcessUseFileNameAsArgument.Name = "checkBoxProcessUseFileNameAsArgument";
            this.checkBoxProcessUseFileNameAsArgument.Size = new System.Drawing.Size(151, 17);
            this.checkBoxProcessUseFileNameAsArgument.TabIndex = 3;
            this.checkBoxProcessUseFileNameAsArgument.Text = "Use file name as argument";
            this.checkBoxProcessUseFileNameAsArgument.UseVisualStyleBackColor = true;
            // 
            // textBoxProcessArguments
            // 
            this.textBoxProcessArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxProcessArguments.Location = new System.Drawing.Point(9, 28);
            this.textBoxProcessArguments.MaxLength = 2003;
            this.textBoxProcessArguments.Name = "textBoxProcessArguments";
            this.textBoxProcessArguments.Size = new System.Drawing.Size(357, 20);
            this.textBoxProcessArguments.TabIndex = 1;
            // 
            // labelProcessArguments
            // 
            this.labelProcessArguments.AutoSize = true;
            this.labelProcessArguments.Location = new System.Drawing.Point(6, 12);
            this.labelProcessArguments.Name = "labelProcessArguments";
            this.labelProcessArguments.Size = new System.Drawing.Size(100, 13);
            this.labelProcessArguments.TabIndex = 24;
            this.labelProcessArguments.Text = "Process arguments:";
            // 
            // tabPageProcessAdvanced
            // 
            this.tabPageProcessAdvanced.Controls.Add(this.groupBoxSynchronizedExecution);
            this.tabPageProcessAdvanced.Controls.Add(this.groupBoxAdvanced);
            this.tabPageProcessAdvanced.Controls.Add(this.groupBoxUserProfile);
            this.tabPageProcessAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageProcessAdvanced.Name = "tabPageProcessAdvanced";
            this.tabPageProcessAdvanced.Size = new System.Drawing.Size(374, 346);
            this.tabPageProcessAdvanced.TabIndex = 5;
            this.tabPageProcessAdvanced.Text = "Process advanced";
            this.tabPageProcessAdvanced.UseVisualStyleBackColor = true;
            // 
            // groupBoxSynchronizedExecution
            // 
            this.groupBoxSynchronizedExecution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSynchronizedExecution.Controls.Add(this.checkBoxProcessSynchronizedExecution);
            this.groupBoxSynchronizedExecution.Controls.Add(this.numericUpDownProcessMaxWaitTime);
            this.groupBoxSynchronizedExecution.Controls.Add(this.labelProcessMaxWaitTime);
            this.groupBoxSynchronizedExecution.Location = new System.Drawing.Point(6, 91);
            this.groupBoxSynchronizedExecution.Name = "groupBoxSynchronizedExecution";
            this.groupBoxSynchronizedExecution.Size = new System.Drawing.Size(363, 69);
            this.groupBoxSynchronizedExecution.TabIndex = 1;
            this.groupBoxSynchronizedExecution.TabStop = false;
            this.groupBoxSynchronizedExecution.Text = "Synchronization";
            // 
            // checkBoxProcessSynchronizedExecution
            // 
            this.checkBoxProcessSynchronizedExecution.AutoSize = true;
            this.checkBoxProcessSynchronizedExecution.Location = new System.Drawing.Point(12, 20);
            this.checkBoxProcessSynchronizedExecution.Name = "checkBoxProcessSynchronizedExecution";
            this.checkBoxProcessSynchronizedExecution.Size = new System.Drawing.Size(191, 17);
            this.checkBoxProcessSynchronizedExecution.TabIndex = 5;
            this.checkBoxProcessSynchronizedExecution.Text = "Synchronized execution of process";
            this.checkBoxProcessSynchronizedExecution.UseVisualStyleBackColor = true;
            // 
            // numericUpDownProcessMaxWaitTime
            // 
            this.numericUpDownProcessMaxWaitTime.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownProcessMaxWaitTime.Location = new System.Drawing.Point(289, 38);
            this.numericUpDownProcessMaxWaitTime.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownProcessMaxWaitTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownProcessMaxWaitTime.Name = "numericUpDownProcessMaxWaitTime";
            this.numericUpDownProcessMaxWaitTime.Size = new System.Drawing.Size(68, 20);
            this.numericUpDownProcessMaxWaitTime.TabIndex = 6;
            // 
            // labelProcessMaxWaitTime
            // 
            this.labelProcessMaxWaitTime.AutoSize = true;
            this.labelProcessMaxWaitTime.Location = new System.Drawing.Point(9, 40);
            this.labelProcessMaxWaitTime.Name = "labelProcessMaxWaitTime";
            this.labelProcessMaxWaitTime.Size = new System.Drawing.Size(274, 13);
            this.labelProcessMaxWaitTime.TabIndex = 6;
            this.labelProcessMaxWaitTime.Text = "Synchronized process maximum wait time in milliseconds:";
            // 
            // groupBoxAdvanced
            // 
            this.groupBoxAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAdvanced.Controls.Add(this.checkBoxProcessUseShellExecute);
            this.groupBoxAdvanced.Controls.Add(this.checkBoxProcessCreateNoWindow);
            this.groupBoxAdvanced.Controls.Add(this.checkBoxProcessRedirectStandardOutput);
            this.groupBoxAdvanced.Controls.Add(this.checkBoxProcessRedirectStandardError);
            this.groupBoxAdvanced.Location = new System.Drawing.Point(6, 12);
            this.groupBoxAdvanced.Name = "groupBoxAdvanced";
            this.groupBoxAdvanced.Size = new System.Drawing.Size(363, 73);
            this.groupBoxAdvanced.TabIndex = 0;
            this.groupBoxAdvanced.TabStop = false;
            this.groupBoxAdvanced.Text = "Advanced";
            // 
            // checkBoxProcessUseShellExecute
            // 
            this.checkBoxProcessUseShellExecute.AutoSize = true;
            this.checkBoxProcessUseShellExecute.Location = new System.Drawing.Point(12, 20);
            this.checkBoxProcessUseShellExecute.Name = "checkBoxProcessUseShellExecute";
            this.checkBoxProcessUseShellExecute.Size = new System.Drawing.Size(148, 17);
            this.checkBoxProcessUseShellExecute.TabIndex = 1;
            this.checkBoxProcessUseShellExecute.Text = "Use shell execute method";
            this.checkBoxProcessUseShellExecute.UseVisualStyleBackColor = true;
            // 
            // checkBoxProcessCreateNoWindow
            // 
            this.checkBoxProcessCreateNoWindow.AutoSize = true;
            this.checkBoxProcessCreateNoWindow.Location = new System.Drawing.Point(12, 42);
            this.checkBoxProcessCreateNoWindow.Name = "checkBoxProcessCreateNoWindow";
            this.checkBoxProcessCreateNoWindow.Size = new System.Drawing.Size(111, 17);
            this.checkBoxProcessCreateNoWindow.TabIndex = 3;
            this.checkBoxProcessCreateNoWindow.Text = "Create no window";
            this.checkBoxProcessCreateNoWindow.UseVisualStyleBackColor = true;
            // 
            // checkBoxProcessRedirectStandardOutput
            // 
            this.checkBoxProcessRedirectStandardOutput.AutoSize = true;
            this.checkBoxProcessRedirectStandardOutput.Location = new System.Drawing.Point(178, 20);
            this.checkBoxProcessRedirectStandardOutput.Name = "checkBoxProcessRedirectStandardOutput";
            this.checkBoxProcessRedirectStandardOutput.Size = new System.Drawing.Size(179, 17);
            this.checkBoxProcessRedirectStandardOutput.TabIndex = 2;
            this.checkBoxProcessRedirectStandardOutput.Text = "Process redirect standard output";
            this.checkBoxProcessRedirectStandardOutput.UseVisualStyleBackColor = true;
            // 
            // checkBoxProcessRedirectStandardError
            // 
            this.checkBoxProcessRedirectStandardError.AutoSize = true;
            this.checkBoxProcessRedirectStandardError.Location = new System.Drawing.Point(178, 42);
            this.checkBoxProcessRedirectStandardError.Name = "checkBoxProcessRedirectStandardError";
            this.checkBoxProcessRedirectStandardError.Size = new System.Drawing.Size(170, 17);
            this.checkBoxProcessRedirectStandardError.TabIndex = 4;
            this.checkBoxProcessRedirectStandardError.Text = "Process redirect standard error";
            this.checkBoxProcessRedirectStandardError.UseVisualStyleBackColor = true;
            // 
            // groupBoxUserProfile
            // 
            this.groupBoxUserProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxUserProfile.Controls.Add(this.checkBoxProcessLoadUserProfile);
            this.groupBoxUserProfile.Controls.Add(this.labelProcessPassword);
            this.groupBoxUserProfile.Controls.Add(this.textBoxProcessDomain);
            this.groupBoxUserProfile.Controls.Add(this.labelProcessUserName);
            this.groupBoxUserProfile.Controls.Add(this.textBoxProcessUserName);
            this.groupBoxUserProfile.Controls.Add(this.labelProcessDomain);
            this.groupBoxUserProfile.Controls.Add(this.maskedTextBoxProcessPassword);
            this.groupBoxUserProfile.Location = new System.Drawing.Point(6, 166);
            this.groupBoxUserProfile.Name = "groupBoxUserProfile";
            this.groupBoxUserProfile.Size = new System.Drawing.Size(363, 165);
            this.groupBoxUserProfile.TabIndex = 2;
            this.groupBoxUserProfile.TabStop = false;
            this.groupBoxUserProfile.Text = "User profile";
            // 
            // checkBoxProcessLoadUserProfile
            // 
            this.checkBoxProcessLoadUserProfile.AutoSize = true;
            this.checkBoxProcessLoadUserProfile.Location = new System.Drawing.Point(12, 20);
            this.checkBoxProcessLoadUserProfile.Name = "checkBoxProcessLoadUserProfile";
            this.checkBoxProcessLoadUserProfile.Size = new System.Drawing.Size(104, 17);
            this.checkBoxProcessLoadUserProfile.TabIndex = 7;
            this.checkBoxProcessLoadUserProfile.Text = "Load user profile";
            this.checkBoxProcessLoadUserProfile.UseVisualStyleBackColor = true;
            // 
            // labelProcessPassword
            // 
            this.labelProcessPassword.AutoSize = true;
            this.labelProcessPassword.Location = new System.Drawing.Point(9, 118);
            this.labelProcessPassword.Name = "labelProcessPassword";
            this.labelProcessPassword.Size = new System.Drawing.Size(56, 13);
            this.labelProcessPassword.TabIndex = 15;
            this.labelProcessPassword.Text = "Password:";
            // 
            // textBoxProcessDomain
            // 
            this.textBoxProcessDomain.Location = new System.Drawing.Point(12, 56);
            this.textBoxProcessDomain.MaxLength = 255;
            this.textBoxProcessDomain.Name = "textBoxProcessDomain";
            this.textBoxProcessDomain.Size = new System.Drawing.Size(191, 20);
            this.textBoxProcessDomain.TabIndex = 8;
            // 
            // labelProcessUserName
            // 
            this.labelProcessUserName.AutoSize = true;
            this.labelProcessUserName.Location = new System.Drawing.Point(9, 79);
            this.labelProcessUserName.Name = "labelProcessUserName";
            this.labelProcessUserName.Size = new System.Drawing.Size(61, 13);
            this.labelProcessUserName.TabIndex = 14;
            this.labelProcessUserName.Text = "User name:";
            // 
            // textBoxProcessUserName
            // 
            this.textBoxProcessUserName.Location = new System.Drawing.Point(12, 95);
            this.textBoxProcessUserName.MaxLength = 255;
            this.textBoxProcessUserName.Name = "textBoxProcessUserName";
            this.textBoxProcessUserName.Size = new System.Drawing.Size(191, 20);
            this.textBoxProcessUserName.TabIndex = 9;
            // 
            // labelProcessDomain
            // 
            this.labelProcessDomain.AutoSize = true;
            this.labelProcessDomain.Location = new System.Drawing.Point(9, 40);
            this.labelProcessDomain.Name = "labelProcessDomain";
            this.labelProcessDomain.Size = new System.Drawing.Size(46, 13);
            this.labelProcessDomain.TabIndex = 13;
            this.labelProcessDomain.Text = "Domain:";
            // 
            // maskedTextBoxProcessPassword
            // 
            this.maskedTextBoxProcessPassword.Location = new System.Drawing.Point(12, 134);
            this.maskedTextBoxProcessPassword.Name = "maskedTextBoxProcessPassword";
            this.maskedTextBoxProcessPassword.PasswordChar = '#';
            this.maskedTextBoxProcessPassword.Size = new System.Drawing.Size(191, 20);
            this.maskedTextBoxProcessPassword.TabIndex = 10;
            // 
            // tabPageFileProcessing
            // 
            this.tabPageFileProcessing.Controls.Add(this.groupBoxFileProcessingSettings);
            this.tabPageFileProcessing.Controls.Add(this.groupBoxFileLockTest);
            this.tabPageFileProcessing.Location = new System.Drawing.Point(4, 22);
            this.tabPageFileProcessing.Name = "tabPageFileProcessing";
            this.tabPageFileProcessing.Size = new System.Drawing.Size(374, 346);
            this.tabPageFileProcessing.TabIndex = 8;
            this.tabPageFileProcessing.Text = "File processing";
            this.tabPageFileProcessing.UseVisualStyleBackColor = true;
            // 
            // groupBoxFileProcessingSettings
            // 
            this.groupBoxFileProcessingSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFileProcessingSettings.Controls.Add(this.checkBoxProcessFileMustExist);
            this.groupBoxFileProcessingSettings.Controls.Add(this.labelProcessDelay);
            this.groupBoxFileProcessingSettings.Controls.Add(this.numericUpDownProcessDelay);
            this.groupBoxFileProcessingSettings.Location = new System.Drawing.Point(6, 12);
            this.groupBoxFileProcessingSettings.Name = "groupBoxFileProcessingSettings";
            this.groupBoxFileProcessingSettings.Size = new System.Drawing.Size(361, 99);
            this.groupBoxFileProcessingSettings.TabIndex = 1;
            this.groupBoxFileProcessingSettings.TabStop = false;
            this.groupBoxFileProcessingSettings.Text = "File processing settings";
            // 
            // checkBoxProcessFileMustExist
            // 
            this.checkBoxProcessFileMustExist.AutoSize = true;
            this.checkBoxProcessFileMustExist.Location = new System.Drawing.Point(12, 20);
            this.checkBoxProcessFileMustExist.Name = "checkBoxProcessFileMustExist";
            this.checkBoxProcessFileMustExist.Size = new System.Drawing.Size(333, 17);
            this.checkBoxProcessFileMustExist.TabIndex = 1;
            this.checkBoxProcessFileMustExist.Text = "Do not run the process or call the service if the file does not exist.";
            this.checkBoxProcessFileMustExist.UseVisualStyleBackColor = true;
            // 
            // labelProcessDelay
            // 
            this.labelProcessDelay.AutoSize = true;
            this.labelProcessDelay.Location = new System.Drawing.Point(9, 47);
            this.labelProcessDelay.Name = "labelProcessDelay";
            this.labelProcessDelay.Size = new System.Drawing.Size(214, 13);
            this.labelProcessDelay.TabIndex = 30;
            this.labelProcessDelay.Text = "Process or service call delay in milliseconds:";
            // 
            // numericUpDownProcessDelay
            // 
            this.numericUpDownProcessDelay.Location = new System.Drawing.Point(12, 63);
            this.numericUpDownProcessDelay.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.numericUpDownProcessDelay.Name = "numericUpDownProcessDelay";
            this.numericUpDownProcessDelay.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownProcessDelay.TabIndex = 2;
            // 
            // groupBoxFileLockTest
            // 
            this.groupBoxFileLockTest.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFileLockTest.Controls.Add(this.checkBoxProcessLockFile);
            this.groupBoxFileLockTest.Controls.Add(this.numericUpDownProcessLockFileLastWriteDelay);
            this.groupBoxFileLockTest.Controls.Add(this.labelProcessLockFileRetries);
            this.groupBoxFileLockTest.Controls.Add(this.labelProcessLockFileLastWriteDelay);
            this.groupBoxFileLockTest.Controls.Add(this.labelProcessLockFileRetriesQueueLimit);
            this.groupBoxFileLockTest.Controls.Add(this.numericUpDownProcessLockFileRetriesQueueLimit);
            this.groupBoxFileLockTest.Controls.Add(this.numericUpDownProcessLockFileRetries);
            this.groupBoxFileLockTest.Location = new System.Drawing.Point(6, 117);
            this.groupBoxFileLockTest.Name = "groupBoxFileLockTest";
            this.groupBoxFileLockTest.Size = new System.Drawing.Size(361, 196);
            this.groupBoxFileLockTest.TabIndex = 2;
            this.groupBoxFileLockTest.TabStop = false;
            this.groupBoxFileLockTest.Text = "File lock test";
            // 
            // checkBoxProcessLockFile
            // 
            this.checkBoxProcessLockFile.AutoSize = true;
            this.checkBoxProcessLockFile.Location = new System.Drawing.Point(12, 21);
            this.checkBoxProcessLockFile.Name = "checkBoxProcessLockFile";
            this.checkBoxProcessLockFile.Size = new System.Drawing.Size(101, 17);
            this.checkBoxProcessLockFile.TabIndex = 3;
            this.checkBoxProcessLockFile.Text = "Test for file lock";
            this.checkBoxProcessLockFile.UseVisualStyleBackColor = true;
            // 
            // numericUpDownProcessLockFileLastWriteDelay
            // 
            this.numericUpDownProcessLockFileLastWriteDelay.Location = new System.Drawing.Point(12, 167);
            this.numericUpDownProcessLockFileLastWriteDelay.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownProcessLockFileLastWriteDelay.Name = "numericUpDownProcessLockFileLastWriteDelay";
            this.numericUpDownProcessLockFileLastWriteDelay.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownProcessLockFileLastWriteDelay.TabIndex = 6;
            // 
            // labelProcessLockFileRetries
            // 
            this.labelProcessLockFileRetries.AutoSize = true;
            this.labelProcessLockFileRetries.Location = new System.Drawing.Point(9, 50);
            this.labelProcessLockFileRetries.Name = "labelProcessLockFileRetries";
            this.labelProcessLockFileRetries.Size = new System.Drawing.Size(149, 13);
            this.labelProcessLockFileRetries.TabIndex = 28;
            this.labelProcessLockFileRetries.Text = "Retries (zero value for infinite):";
            // 
            // labelProcessLockFileLastWriteDelay
            // 
            this.labelProcessLockFileLastWriteDelay.AutoSize = true;
            this.labelProcessLockFileLastWriteDelay.Location = new System.Drawing.Point(9, 151);
            this.labelProcessLockFileLastWriteDelay.Name = "labelProcessLockFileLastWriteDelay";
            this.labelProcessLockFileLastWriteDelay.Size = new System.Drawing.Size(258, 13);
            this.labelProcessLockFileLastWriteDelay.TabIndex = 34;
            this.labelProcessLockFileLastWriteDelay.Text = "The file last write time must occur milliseconds before:";
            // 
            // labelProcessLockFileRetriesQueueLimit
            // 
            this.labelProcessLockFileRetriesQueueLimit.AutoSize = true;
            this.labelProcessLockFileRetriesQueueLimit.Location = new System.Drawing.Point(9, 98);
            this.labelProcessLockFileRetriesQueueLimit.Name = "labelProcessLockFileRetriesQueueLimit";
            this.labelProcessLockFileRetriesQueueLimit.Size = new System.Drawing.Size(260, 13);
            this.labelProcessLockFileRetriesQueueLimit.TabIndex = 26;
            this.labelProcessLockFileRetriesQueueLimit.Text = "Preserve processes (event if retries count is reached):";
            // 
            // numericUpDownProcessLockFileRetriesQueueLimit
            // 
            this.numericUpDownProcessLockFileRetriesQueueLimit.Location = new System.Drawing.Point(12, 114);
            this.numericUpDownProcessLockFileRetriesQueueLimit.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownProcessLockFileRetriesQueueLimit.Name = "numericUpDownProcessLockFileRetriesQueueLimit";
            this.numericUpDownProcessLockFileRetriesQueueLimit.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownProcessLockFileRetriesQueueLimit.TabIndex = 5;
            // 
            // numericUpDownProcessLockFileRetries
            // 
            this.numericUpDownProcessLockFileRetries.Location = new System.Drawing.Point(12, 66);
            this.numericUpDownProcessLockFileRetries.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownProcessLockFileRetries.Name = "numericUpDownProcessLockFileRetries";
            this.numericUpDownProcessLockFileRetries.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownProcessLockFileRetries.TabIndex = 4;
            // 
            // tabPageFileRenaming
            // 
            this.tabPageFileRenaming.Controls.Add(this.labelTryRenameFileRetries);
            this.tabPageFileRenaming.Controls.Add(this.numericUpDownTryRenameRetries);
            this.tabPageFileRenaming.Controls.Add(this.labelRenameFileSettings);
            this.tabPageFileRenaming.Controls.Add(this.checkBoxTryRenameFile);
            this.tabPageFileRenaming.Location = new System.Drawing.Point(4, 22);
            this.tabPageFileRenaming.Name = "tabPageFileRenaming";
            this.tabPageFileRenaming.Size = new System.Drawing.Size(374, 346);
            this.tabPageFileRenaming.TabIndex = 10;
            this.tabPageFileRenaming.Text = "File renaming";
            this.tabPageFileRenaming.UseVisualStyleBackColor = true;
            // 
            // labelTryRenameFileRetries
            // 
            this.labelTryRenameFileRetries.AutoSize = true;
            this.labelTryRenameFileRetries.Location = new System.Drawing.Point(6, 73);
            this.labelTryRenameFileRetries.Name = "labelTryRenameFileRetries";
            this.labelTryRenameFileRetries.Size = new System.Drawing.Size(81, 13);
            this.labelTryRenameFileRetries.TabIndex = 32;
            this.labelTryRenameFileRetries.Text = "Rename retries:";
            // 
            // numericUpDownTryRenameRetries
            // 
            this.numericUpDownTryRenameRetries.Location = new System.Drawing.Point(9, 89);
            this.numericUpDownTryRenameRetries.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownTryRenameRetries.Name = "numericUpDownTryRenameRetries";
            this.numericUpDownTryRenameRetries.Size = new System.Drawing.Size(69, 20);
            this.numericUpDownTryRenameRetries.TabIndex = 2;
            // 
            // labelRenameFileSettings
            // 
            this.labelRenameFileSettings.AutoSize = true;
            this.labelRenameFileSettings.Location = new System.Drawing.Point(6, 12);
            this.labelRenameFileSettings.Name = "labelRenameFileSettings";
            this.labelRenameFileSettings.Size = new System.Drawing.Size(105, 13);
            this.labelRenameFileSettings.TabIndex = 3;
            this.labelRenameFileSettings.Text = "Rename file settings:";
            // 
            // checkBoxTryRenameFile
            // 
            this.checkBoxTryRenameFile.AutoSize = true;
            this.checkBoxTryRenameFile.Location = new System.Drawing.Point(9, 38);
            this.checkBoxTryRenameFile.Name = "checkBoxTryRenameFile";
            this.checkBoxTryRenameFile.Size = new System.Drawing.Size(177, 17);
            this.checkBoxTryRenameFile.TabIndex = 1;
            this.checkBoxTryRenameFile.Text = "Rename file before processing it";
            this.checkBoxTryRenameFile.UseVisualStyleBackColor = true;
            // 
            // tabPageLoggingAndMessages
            // 
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxLogFileSystemSearchProgress);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxLogFileSystemSearchError);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxDisplayFileSystemSearchProgress);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxDisplayFileSystemSearchError);
            this.tabPageLoggingAndMessages.Controls.Add(this.labelMessages);
            this.tabPageLoggingAndMessages.Controls.Add(this.labelLogging);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxLogProcessEnd);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxLogProcessStart);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxLogFileSystemChange);
            this.tabPageLoggingAndMessages.Controls.Add(this.checkBoxDisplayFileSystemChange);
            this.tabPageLoggingAndMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageLoggingAndMessages.Name = "tabPageLoggingAndMessages";
            this.tabPageLoggingAndMessages.Size = new System.Drawing.Size(374, 346);
            this.tabPageLoggingAndMessages.TabIndex = 4;
            this.tabPageLoggingAndMessages.Text = "Logging and messages";
            this.tabPageLoggingAndMessages.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogFileSystemSearchProgress
            // 
            this.checkBoxLogFileSystemSearchProgress.AutoSize = true;
            this.checkBoxLogFileSystemSearchProgress.Location = new System.Drawing.Point(9, 130);
            this.checkBoxLogFileSystemSearchProgress.Name = "checkBoxLogFileSystemSearchProgress";
            this.checkBoxLogFileSystemSearchProgress.Size = new System.Drawing.Size(173, 17);
            this.checkBoxLogFileSystemSearchProgress.TabIndex = 5;
            this.checkBoxLogFileSystemSearchProgress.Text = "Log file system search progress";
            this.checkBoxLogFileSystemSearchProgress.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogFileSystemSearchError
            // 
            this.checkBoxLogFileSystemSearchError.AutoSize = true;
            this.checkBoxLogFileSystemSearchError.Location = new System.Drawing.Point(9, 107);
            this.checkBoxLogFileSystemSearchError.Name = "checkBoxLogFileSystemSearchError";
            this.checkBoxLogFileSystemSearchError.Size = new System.Drawing.Size(154, 17);
            this.checkBoxLogFileSystemSearchError.TabIndex = 4;
            this.checkBoxLogFileSystemSearchError.Text = "Log file system search error";
            this.checkBoxLogFileSystemSearchError.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayFileSystemSearchProgress
            // 
            this.checkBoxDisplayFileSystemSearchProgress.AutoSize = true;
            this.checkBoxDisplayFileSystemSearchProgress.Location = new System.Drawing.Point(9, 232);
            this.checkBoxDisplayFileSystemSearchProgress.Name = "checkBoxDisplayFileSystemSearchProgress";
            this.checkBoxDisplayFileSystemSearchProgress.Size = new System.Drawing.Size(234, 17);
            this.checkBoxDisplayFileSystemSearchProgress.TabIndex = 8;
            this.checkBoxDisplayFileSystemSearchProgress.Text = "Display file system search progress message";
            this.checkBoxDisplayFileSystemSearchProgress.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayFileSystemSearchError
            // 
            this.checkBoxDisplayFileSystemSearchError.AutoSize = true;
            this.checkBoxDisplayFileSystemSearchError.Location = new System.Drawing.Point(9, 209);
            this.checkBoxDisplayFileSystemSearchError.Name = "checkBoxDisplayFileSystemSearchError";
            this.checkBoxDisplayFileSystemSearchError.Size = new System.Drawing.Size(215, 17);
            this.checkBoxDisplayFileSystemSearchError.TabIndex = 7;
            this.checkBoxDisplayFileSystemSearchError.Text = "Display file system search error message";
            this.checkBoxDisplayFileSystemSearchError.UseVisualStyleBackColor = true;
            // 
            // labelMessages
            // 
            this.labelMessages.AutoSize = true;
            this.labelMessages.Location = new System.Drawing.Point(6, 160);
            this.labelMessages.Name = "labelMessages";
            this.labelMessages.Size = new System.Drawing.Size(58, 13);
            this.labelMessages.TabIndex = 5;
            this.labelMessages.Text = "Messages:";
            // 
            // labelLogging
            // 
            this.labelLogging.AutoSize = true;
            this.labelLogging.Location = new System.Drawing.Point(6, 12);
            this.labelLogging.Name = "labelLogging";
            this.labelLogging.Size = new System.Drawing.Size(48, 13);
            this.labelLogging.TabIndex = 4;
            this.labelLogging.Text = "Logging:";
            // 
            // checkBoxLogProcessEnd
            // 
            this.checkBoxLogProcessEnd.AutoSize = true;
            this.checkBoxLogProcessEnd.Location = new System.Drawing.Point(9, 84);
            this.checkBoxLogProcessEnd.Name = "checkBoxLogProcessEnd";
            this.checkBoxLogProcessEnd.Size = new System.Drawing.Size(105, 17);
            this.checkBoxLogProcessEnd.TabIndex = 3;
            this.checkBoxLogProcessEnd.Text = "Log process end";
            this.checkBoxLogProcessEnd.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogProcessStart
            // 
            this.checkBoxLogProcessStart.AutoSize = true;
            this.checkBoxLogProcessStart.Location = new System.Drawing.Point(9, 61);
            this.checkBoxLogProcessStart.Name = "checkBoxLogProcessStart";
            this.checkBoxLogProcessStart.Size = new System.Drawing.Size(107, 17);
            this.checkBoxLogProcessStart.TabIndex = 2;
            this.checkBoxLogProcessStart.Text = "Log process start";
            this.checkBoxLogProcessStart.UseVisualStyleBackColor = true;
            // 
            // checkBoxLogFileSystemChange
            // 
            this.checkBoxLogFileSystemChange.AutoSize = true;
            this.checkBoxLogFileSystemChange.Location = new System.Drawing.Point(9, 38);
            this.checkBoxLogFileSystemChange.Name = "checkBoxLogFileSystemChange";
            this.checkBoxLogFileSystemChange.Size = new System.Drawing.Size(134, 17);
            this.checkBoxLogFileSystemChange.TabIndex = 1;
            this.checkBoxLogFileSystemChange.Text = "Log file system change";
            this.checkBoxLogFileSystemChange.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisplayFileSystemChange
            // 
            this.checkBoxDisplayFileSystemChange.AutoSize = true;
            this.checkBoxDisplayFileSystemChange.Location = new System.Drawing.Point(9, 186);
            this.checkBoxDisplayFileSystemChange.Name = "checkBoxDisplayFileSystemChange";
            this.checkBoxDisplayFileSystemChange.Size = new System.Drawing.Size(195, 17);
            this.checkBoxDisplayFileSystemChange.TabIndex = 6;
            this.checkBoxDisplayFileSystemChange.Text = "Display file system change message";
            this.checkBoxDisplayFileSystemChange.UseVisualStyleBackColor = true;
            // 
            // tabPageAdvanced
            // 
            this.tabPageAdvanced.Controls.Add(this.checkBoxSubscribeToBufferErrorEvent);
            this.tabPageAdvanced.Controls.Add(this.numericUpDownInternalBufferSize);
            this.tabPageAdvanced.Controls.Add(this.labelInternalBufferSize);
            this.tabPageAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdvanced.Name = "tabPageAdvanced";
            this.tabPageAdvanced.Size = new System.Drawing.Size(374, 346);
            this.tabPageAdvanced.TabIndex = 3;
            this.tabPageAdvanced.Text = "Advanced";
            this.tabPageAdvanced.UseVisualStyleBackColor = true;
            // 
            // checkBoxSubscribeToBufferErrorEvent
            // 
            this.checkBoxSubscribeToBufferErrorEvent.AutoSize = true;
            this.checkBoxSubscribeToBufferErrorEvent.Location = new System.Drawing.Point(9, 54);
            this.checkBoxSubscribeToBufferErrorEvent.Name = "checkBoxSubscribeToBufferErrorEvent";
            this.checkBoxSubscribeToBufferErrorEvent.Size = new System.Drawing.Size(255, 17);
            this.checkBoxSubscribeToBufferErrorEvent.TabIndex = 2;
            this.checkBoxSubscribeToBufferErrorEvent.Text = "Display and log error message on buffer overflow";
            this.checkBoxSubscribeToBufferErrorEvent.UseVisualStyleBackColor = true;
            // 
            // numericUpDownInternalBufferSize
            // 
            this.numericUpDownInternalBufferSize.Increment = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.numericUpDownInternalBufferSize.Location = new System.Drawing.Point(9, 28);
            this.numericUpDownInternalBufferSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownInternalBufferSize.Name = "numericUpDownInternalBufferSize";
            this.numericUpDownInternalBufferSize.Size = new System.Drawing.Size(90, 20);
            this.numericUpDownInternalBufferSize.TabIndex = 1;
            // 
            // labelInternalBufferSize
            // 
            this.labelInternalBufferSize.AutoSize = true;
            this.labelInternalBufferSize.Location = new System.Drawing.Point(6, 12);
            this.labelInternalBufferSize.Name = "labelInternalBufferSize";
            this.labelInternalBufferSize.Size = new System.Drawing.Size(155, 13);
            this.labelInternalBufferSize.TabIndex = 0;
            this.labelInternalBufferSize.Text = "File watcher internal buffer size:";
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
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(233, 382);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(74, 23);
            this.buttonOK.TabIndex = 100;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.ButtonOKClick);
            // 
            // FormProperties
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
            this.Name = "FormProperties";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Properties";
            this.panelTabs.ResumeLayout(false);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRecycleInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPollDirectoryInterval)).EndInit();
            this.tabPageFileSystemEvents.ResumeLayout(false);
            this.tabPageFileSystemEvents.PerformLayout();
            this.tabPageFilters.ResumeLayout(false);
            this.tabPageFilters.PerformLayout();
            this.tabPageFilteredMode.ResumeLayout(false);
            this.tabPageFilteredMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFilteredModeFilterTimeout)).EndInit();
            this.tabPageCallService.ResumeLayout(false);
            this.tabPageCallService.PerformLayout();
            this.tabPageProcess.ResumeLayout(false);
            this.tabPageProcess.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessBatchSize)).EndInit();
            this.tabPageProcessArguments.ResumeLayout(false);
            this.tabPageProcessArguments.PerformLayout();
            this.tabPageProcessAdvanced.ResumeLayout(false);
            this.groupBoxSynchronizedExecution.ResumeLayout(false);
            this.groupBoxSynchronizedExecution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessMaxWaitTime)).EndInit();
            this.groupBoxAdvanced.ResumeLayout(false);
            this.groupBoxAdvanced.PerformLayout();
            this.groupBoxUserProfile.ResumeLayout(false);
            this.groupBoxUserProfile.PerformLayout();
            this.tabPageFileProcessing.ResumeLayout(false);
            this.groupBoxFileProcessingSettings.ResumeLayout(false);
            this.groupBoxFileProcessingSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessDelay)).EndInit();
            this.groupBoxFileLockTest.ResumeLayout(false);
            this.groupBoxFileLockTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessLockFileLastWriteDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessLockFileRetriesQueueLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownProcessLockFileRetries)).EndInit();
            this.tabPageFileRenaming.ResumeLayout(false);
            this.tabPageFileRenaming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTryRenameRetries)).EndInit();
            this.tabPageLoggingAndMessages.ResumeLayout(false);
            this.tabPageLoggingAndMessages.PerformLayout();
            this.tabPageAdvanced.ResumeLayout(false);
            this.tabPageAdvanced.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownInternalBufferSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTabs;
        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageGeneral;
        private System.Windows.Forms.TabPage tabPageFileSystemEvents;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TabPage tabPageProcess;
        private System.Windows.Forms.TabPage tabPageLoggingAndMessages;
        private System.Windows.Forms.TabPage tabPageAdvanced;
        private System.Windows.Forms.Label labelDaemonName;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPathToWatch;
        private System.Windows.Forms.CheckBox checkBoxStartDaemon;
        private System.Windows.Forms.TextBox textBoxDaemonName;
        private System.Windows.Forms.Button buttonBrowseDirectory;
        private System.Windows.Forms.CheckBox checkBoxIncludeSubdirectories;
        private System.Windows.Forms.CheckBox checkBoxSubscribeToDeletedEvent;
        private System.Windows.Forms.CheckBox checkBoxSubscribeToCreatedEvent;
        private System.Windows.Forms.CheckBox checkBoxSubscribeToChangedEvent;
        private System.Windows.Forms.CheckBox checkBoxSubscribeToRenamedEvent;
        private System.Windows.Forms.CheckBox checkBoxNotifySize;
        private System.Windows.Forms.CheckBox checkBoxNotifySecurity;
        private System.Windows.Forms.CheckBox checkBoxNotifyLastWrite;
        private System.Windows.Forms.CheckBox checkBoxNotifyLastAccess;
        private System.Windows.Forms.CheckBox checkBoxNotifyFileName;
        private System.Windows.Forms.CheckBox checkBoxNotifyDirectoryName;
        private System.Windows.Forms.CheckBox checkBoxNotifyCreationTime;
        private System.Windows.Forms.CheckBox checkBoxNotifyAttribute;
        private System.Windows.Forms.CheckBox checkBoxStartProcess;
        private System.Windows.Forms.Button buttonBrowseExecutable;
        private System.Windows.Forms.TextBox textBoxProcessFileName;
        private System.Windows.Forms.Label labelProcessFileName;
        private System.Windows.Forms.TextBox textBoxProcessVerb;
        private System.Windows.Forms.Label labelProcessVerb;
        private System.Windows.Forms.Button buttonBrowseWorkingDirectory;
        private System.Windows.Forms.TextBox textBoxProcessWorkingDirectory;
        private System.Windows.Forms.Label labelProcessWorkingDirectory;
        private System.Windows.Forms.TabPage tabPageProcessAdvanced;
        private System.Windows.Forms.CheckBox checkBoxProcessCreateNoWindow;
        private System.Windows.Forms.CheckBox checkBoxProcessUseShellExecute;
        private System.Windows.Forms.CheckBox checkBoxProcessSynchronizedExecution;
        private System.Windows.Forms.CheckBox checkBoxProcessRedirectStandardOutput;
        private System.Windows.Forms.CheckBox checkBoxProcessRedirectStandardError;
        private System.Windows.Forms.Label labelProcessMaxWaitTime;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessMaxWaitTime;
        private System.Windows.Forms.CheckBox checkBoxProcessLoadUserProfile;
        private System.Windows.Forms.Label labelProcessWindowStyle;
        private System.Windows.Forms.ComboBox comboBoxWindowStyle;
        private System.Windows.Forms.TextBox textBoxProcessUserName;
        private System.Windows.Forms.TextBox textBoxProcessDomain;
        private System.Windows.Forms.Label labelProcessPassword;
        private System.Windows.Forms.Label labelProcessUserName;
        private System.Windows.Forms.Label labelProcessDomain;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxProcessPassword;
        private System.Windows.Forms.GroupBox groupBoxUserProfile;
        private System.Windows.Forms.GroupBox groupBoxAdvanced;
        private System.Windows.Forms.GroupBox groupBoxSynchronizedExecution;
        private System.Windows.Forms.NumericUpDown numericUpDownInternalBufferSize;
        private System.Windows.Forms.Label labelInternalBufferSize;
        private System.Windows.Forms.Label labelFileSystemEventsToWatch;
        private System.Windows.Forms.Label labelFileSystemEventFilters;
        private System.Windows.Forms.CheckBox checkBoxSubscribeToBufferErrorEvent;
        private System.Windows.Forms.CheckBox checkBoxDisplayFileSystemChange;
        private System.Windows.Forms.CheckBox checkBoxLogFileSystemChange;
        private System.Windows.Forms.Label labelMessages;
        private System.Windows.Forms.Label labelLogging;
        private System.Windows.Forms.CheckBox checkBoxLogProcessEnd;
        private System.Windows.Forms.CheckBox checkBoxLogProcessStart;
        private System.Windows.Forms.TabPage tabPageFilters;
        private System.Windows.Forms.TabPage tabPageProcessArguments;
        private System.Windows.Forms.Label labelProcessArgumentsFileNameEscapeString;
        private System.Windows.Forms.TextBox textBoxProcessArgumentsFileNameEscapeString;
        private System.Windows.Forms.CheckBox checkBoxProcessUseFileNameAsArgument;
        private System.Windows.Forms.TextBox textBoxProcessArguments;
        private System.Windows.Forms.Label labelProcessArguments;
        private System.Windows.Forms.Label labelProcessArgumentsOldFileNameEscapeString;
        private System.Windows.Forms.TextBox textBoxProcessArgumentsOldFileNameEscapeString;
        private System.Windows.Forms.CheckBox checkBoxProcessUseOldFileNameAsArgument;
        private System.Windows.Forms.Label labelProcessArgumentsChangeTypeEscapeString;
        private System.Windows.Forms.TextBox textBoxProcessArgumentsChangeTypeEscapeString;
        private System.Windows.Forms.CheckBox checkBoxProcessUseChangeTypeAsArgument;
        private System.Windows.Forms.TextBox textBoxChangedRegularExpressionFilter;
        private System.Windows.Forms.Label labelChangedRegularExpressionFilter;
        private System.Windows.Forms.TextBox textBoxRenamedRegularExpressionFilter;
        private System.Windows.Forms.Label labelRenamedRegularExpressionFilter;
        private System.Windows.Forms.TextBox textBoxDeletedRegularExpressionFilter;
        private System.Windows.Forms.Label labelDeletedRegularExpressionFilter;
        private System.Windows.Forms.TextBox textBoxCreatedRegularExpressionFilter;
        private System.Windows.Forms.Label labelCreatedRegularExpressionFilter;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label labelFileNameFilterString;
        private System.Windows.Forms.Label labelAdditionalFilters;
        private System.Windows.Forms.CheckBox checkBoxGenerateFileSystemEventsAtStartup;
        private System.Windows.Forms.TextBox textBoxGenereatedEventFileNameRegularExpressionFilter;
        private System.Windows.Forms.Label labelGnereatedEventFileNameRegularExpressionFilter;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessBatchSize;
        private System.Windows.Forms.Label labelProcessBatchSize;
        private System.Windows.Forms.CheckBox checkBoxLogFileSystemSearchProgress;
        private System.Windows.Forms.CheckBox checkBoxLogFileSystemSearchError;
        private System.Windows.Forms.CheckBox checkBoxDisplayFileSystemSearchProgress;
        private System.Windows.Forms.CheckBox checkBoxDisplayFileSystemSearchError;
        private System.Windows.Forms.TabPage tabPageFileProcessing;
        private System.Windows.Forms.CheckBox checkBoxProcessFileMustExist;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessDelay;
        private System.Windows.Forms.Label labelProcessDelay;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessLockFileRetries;
        private System.Windows.Forms.Label labelProcessLockFileRetries;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessLockFileRetriesQueueLimit;
        private System.Windows.Forms.Label labelProcessLockFileRetriesQueueLimit;
        private System.Windows.Forms.NumericUpDown numericUpDownProcessLockFileLastWriteDelay;
        private System.Windows.Forms.Label labelProcessLockFileLastWriteDelay;
        private System.Windows.Forms.CheckBox checkBoxProcessLockFile;
        private System.Windows.Forms.GroupBox groupBoxFileLockTest;
        private System.Windows.Forms.GroupBox groupBoxFileProcessingSettings;
        private System.Windows.Forms.TabPage tabPageCallService;
        private System.Windows.Forms.CheckBox checkBoxCallService;
        private System.Windows.Forms.Label labelCallServiceSettings;
        private System.Windows.Forms.TextBox textBoxCallServiceNote;
        private System.Windows.Forms.CheckBox checkBoxStreamFile;
        private System.Windows.Forms.TabPage tabPageFileRenaming;
        private System.Windows.Forms.Label labelRenameFileSettings;
        private System.Windows.Forms.CheckBox checkBoxTryRenameFile;
        private System.Windows.Forms.Label labelTryRenameFileRetries;
        private System.Windows.Forms.NumericUpDown numericUpDownTryRenameRetries;
        private System.Windows.Forms.TabPage tabPageFilteredMode;
        private System.Windows.Forms.Label labelFilteredMode;
        private System.Windows.Forms.CheckBox checkBoxFilteredMode;
        private System.Windows.Forms.NumericUpDown numericUpDownFilteredModeFilterTimeout;
        private System.Windows.Forms.Label labelFilteredModeFilterTimeout;
        private System.Windows.Forms.NumericUpDown numericUpDownPollDirectoryInterval;
        private System.Windows.Forms.Label labelPollDirectoryInterval;
        private System.Windows.Forms.CheckBox checkBoxRecycleFileWatcher;
        private System.Windows.Forms.CheckBox checkBoxPollDirectory;
        private System.Windows.Forms.NumericUpDown numericUpDownRecycleInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelMinutes;
    }
}