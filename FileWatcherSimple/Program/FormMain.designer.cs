namespace FileWatcherUtilities.FileWatcherSimple
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabFileWatchers = new System.Windows.Forms.TabControl();
            this.tabPageFileWatchers = new System.Windows.Forms.TabPage();
            this.listViewFileWatchers = new System.Windows.Forms.ListView();
            this.columnHeaderDaemon = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderStatus = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEvents = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLastEvent = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderLastEventTime = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderErrors = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderEnabled = new System.Windows.Forms.ColumnHeader();
            this.tabPageLogMessages = new System.Windows.Forms.TabPage();
            this.textBoxLogMessages = new System.Windows.Forms.TextBox();
            this.panelTabs = new System.Windows.Forms.Panel();
            this.toolStripFileWatchers = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStartAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStopAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDelete = new System.Windows.Forms.ToolStripButton();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.propertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lisenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelWatchers = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelWatcherCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProcesses = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProcessCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelAutoStartupValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProcessesToRun = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelProcessesToRunCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabFileWatchers.SuspendLayout();
            this.tabPageFileWatchers.SuspendLayout();
            this.tabPageLogMessages.SuspendLayout();
            this.panelTabs.SuspendLayout();
            this.toolStripFileWatchers.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFileWatchers
            // 
            this.tabFileWatchers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabFileWatchers.Controls.Add(this.tabPageFileWatchers);
            this.tabFileWatchers.Controls.Add(this.tabPageLogMessages);
            this.tabFileWatchers.Location = new System.Drawing.Point(0, 0);
            this.tabFileWatchers.Name = "tabFileWatchers";
            this.tabFileWatchers.SelectedIndex = 0;
            this.tabFileWatchers.Size = new System.Drawing.Size(632, 369);
            this.tabFileWatchers.TabIndex = 0;
            this.tabFileWatchers.SelectedIndexChanged += new System.EventHandler(this.TabFileWatchersSelectedIndexChanged);
            // 
            // tabPageFileWatchers
            // 
            this.tabPageFileWatchers.Controls.Add(this.listViewFileWatchers);
            this.tabPageFileWatchers.Location = new System.Drawing.Point(4, 22);
            this.tabPageFileWatchers.Name = "tabPageFileWatchers";
            this.tabPageFileWatchers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFileWatchers.Size = new System.Drawing.Size(624, 343);
            this.tabPageFileWatchers.TabIndex = 0;
            this.tabPageFileWatchers.Text = "File watchers";
            this.tabPageFileWatchers.UseVisualStyleBackColor = true;
            // 
            // listViewFileWatchers
            // 
            this.listViewFileWatchers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDaemon,
            this.columnHeaderStatus,
            this.columnHeaderEvents,
            this.columnHeaderLastEvent,
            this.columnHeaderLastEventTime,
            this.columnHeaderErrors,
            this.columnHeaderEnabled});
            this.listViewFileWatchers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFileWatchers.FullRowSelect = true;
            this.listViewFileWatchers.Location = new System.Drawing.Point(3, 3);
            this.listViewFileWatchers.MultiSelect = false;
            this.listViewFileWatchers.Name = "listViewFileWatchers";
            this.listViewFileWatchers.Size = new System.Drawing.Size(618, 337);
            this.listViewFileWatchers.TabIndex = 0;
            this.listViewFileWatchers.UseCompatibleStateImageBehavior = false;
            this.listViewFileWatchers.View = System.Windows.Forms.View.Details;
            this.listViewFileWatchers.SelectedIndexChanged += new System.EventHandler(this.ListViewFileWatchersSelectedIndexChanged);
            // 
            // columnHeaderDaemon
            // 
            this.columnHeaderDaemon.Text = "Daemon";
            this.columnHeaderDaemon.Width = 136;
            // 
            // columnHeaderStatus
            // 
            this.columnHeaderStatus.Text = "Status";
            this.columnHeaderStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderStatus.Width = 65;
            // 
            // columnHeaderEvents
            // 
            this.columnHeaderEvents.Text = "Events";
            this.columnHeaderEvents.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderEvents.Width = 48;
            // 
            // columnHeaderLastEvent
            // 
            this.columnHeaderLastEvent.Text = "Last event type";
            this.columnHeaderLastEvent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderLastEvent.Width = 130;
            // 
            // columnHeaderLastEventTime
            // 
            this.columnHeaderLastEventTime.Text = "Last event time";
            this.columnHeaderLastEventTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderLastEventTime.Width = 130;
            // 
            // columnHeaderErrors
            // 
            this.columnHeaderErrors.Text = "Errors";
            this.columnHeaderErrors.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeaderErrors.Width = 45;
            // 
            // columnHeaderEnabled
            // 
            this.columnHeaderEnabled.Text = "Enabled";
            this.columnHeaderEnabled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tabPageLogMessages
            // 
            this.tabPageLogMessages.Controls.Add(this.textBoxLogMessages);
            this.tabPageLogMessages.Location = new System.Drawing.Point(4, 22);
            this.tabPageLogMessages.Name = "tabPageLogMessages";
            this.tabPageLogMessages.Size = new System.Drawing.Size(624, 343);
            this.tabPageLogMessages.TabIndex = 1;
            this.tabPageLogMessages.Text = "Log messages";
            this.tabPageLogMessages.UseVisualStyleBackColor = true;
            // 
            // textBoxLogMessages
            // 
            this.textBoxLogMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLogMessages.Location = new System.Drawing.Point(0, 0);
            this.textBoxLogMessages.Multiline = true;
            this.textBoxLogMessages.Name = "textBoxLogMessages";
            this.textBoxLogMessages.ReadOnly = true;
            this.textBoxLogMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLogMessages.Size = new System.Drawing.Size(624, 343);
            this.textBoxLogMessages.TabIndex = 0;
            // 
            // panelTabs
            // 
            this.panelTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTabs.Controls.Add(this.tabFileWatchers);
            this.panelTabs.Location = new System.Drawing.Point(0, 52);
            this.panelTabs.Name = "panelTabs";
            this.panelTabs.Size = new System.Drawing.Size(632, 369);
            this.panelTabs.TabIndex = 1;
            // 
            // toolStripFileWatchers
            // 
            this.toolStripFileWatchers.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripFileWatchers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonStart,
            this.toolStripButtonStartAll,
            this.toolStripButtonStop,
            this.toolStripButtonStopAll,
            this.toolStripButtonProperties,
            this.toolStripButtonNew,
            this.toolStripButtonDelete});
            this.toolStripFileWatchers.Location = new System.Drawing.Point(0, 24);
            this.toolStripFileWatchers.Name = "toolStripFileWatchers";
            this.toolStripFileWatchers.Size = new System.Drawing.Size(632, 25);
            this.toolStripFileWatchers.TabIndex = 2;
            this.toolStripFileWatchers.Text = "toolStrip1";
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStart.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStart.Image")));
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonStart.Text = "Start";
            this.toolStripButtonStart.Click += new System.EventHandler(this.ToolStripButtonStartClick);
            // 
            // toolStripButtonStartAll
            // 
            this.toolStripButtonStartAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStartAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStartAll.Image")));
            this.toolStripButtonStartAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStartAll.Name = "toolStripButtonStartAll";
            this.toolStripButtonStartAll.Size = new System.Drawing.Size(50, 22);
            this.toolStripButtonStartAll.Text = "Start all";
            this.toolStripButtonStartAll.Click += new System.EventHandler(this.ToolStripButtonStartAllClick);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.ToolStripButtonStopClick);
            // 
            // toolStripButtonStopAll
            // 
            this.toolStripButtonStopAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStopAll.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStopAll.Image")));
            this.toolStripButtonStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStopAll.Name = "toolStripButtonStopAll";
            this.toolStripButtonStopAll.Size = new System.Drawing.Size(50, 22);
            this.toolStripButtonStopAll.Text = "Stop all";
            this.toolStripButtonStopAll.Click += new System.EventHandler(this.ToolStripButtonStopAllClick);
            // 
            // toolStripButtonProperties
            // 
            this.toolStripButtonProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonProperties.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonProperties.Image")));
            this.toolStripButtonProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonProperties.Name = "toolStripButtonProperties";
            this.toolStripButtonProperties.Size = new System.Drawing.Size(64, 22);
            this.toolStripButtonProperties.Text = "Properties";
            this.toolStripButtonProperties.Click += new System.EventHandler(this.ToolStripButtonPropertiesClick);
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNew.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNew.Image")));
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonNew.Text = "New";
            this.toolStripButtonNew.Click += new System.EventHandler(this.ToolStripButtonNewClick);
            // 
            // toolStripButtonDelete
            // 
            this.toolStripButtonDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDelete.Image")));
            this.toolStripButtonDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDelete.Name = "toolStripButtonDelete";
            this.toolStripButtonDelete.Size = new System.Drawing.Size(44, 22);
            this.toolStripButtonDelete.Text = "Delete";
            this.toolStripButtonDelete.Click += new System.EventHandler(this.ToolStripButtonDeleteClick);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.runToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem2,
            this.propertiesToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.newToolStripMenuItem.Text = "&New...";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItemClick);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.deleteToolStripMenuItem.Text = "&Delete...";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(147, 6);
            // 
            // propertiesToolStripMenuItem
            // 
            this.propertiesToolStripMenuItem.Name = "propertiesToolStripMenuItem";
            this.propertiesToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.propertiesToolStripMenuItem.Text = "&Properties...";
            this.propertiesToolStripMenuItem.Click += new System.EventHandler(this.PropertiesToolStripMenuItemClick);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.startAllToolStripMenuItem,
            this.toolStripMenuItem1,
            this.stopToolStripMenuItem,
            this.stopAllToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "&Run";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.startToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItemClick);
            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            this.startAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.startAllToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.startAllToolStripMenuItem.Text = "Start all";
            this.startAllToolStripMenuItem.Click += new System.EventHandler(this.StartAllToolStripMenuItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 6);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItemClick);
            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            this.stopAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            this.stopAllToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.stopAllToolStripMenuItem.Text = "Stop all";
            this.stopAllToolStripMenuItem.Click += new System.EventHandler(this.StopAllToolStripMenuItemClick);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.optionsToolStripMenuItem.Text = "&Options...";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItemClick);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem1,
            this.lisenceToolStripMenuItem,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.helpToolStripMenuItem1.Text = "&Help...";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.HelpToolStripMenuItem1Click);
            // 
            // lisenceToolStripMenuItem
            // 
            this.lisenceToolStripMenuItem.Name = "lisenceToolStripMenuItem";
            this.lisenceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.lisenceToolStripMenuItem.Text = "&License...";
            this.lisenceToolStripMenuItem.Click += new System.EventHandler(this.LisenceToolStripMenuItemClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(149, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelWatchers,
            this.toolStripStatusLabelWatcherCount,
            this.toolStripStatusLabelProcesses,
            this.toolStripStatusLabelProcessCount,
            this.toolStripStatusLabelAutoStartupValue,
            this.toolStripStatusLabelProcessesToRun,
            this.toolStripStatusLabelProcessesToRunCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 422);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 24);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabelWatchers
            // 
            this.toolStripStatusLabelWatchers.Name = "toolStripStatusLabelWatchers";
            this.toolStripStatusLabelWatchers.Size = new System.Drawing.Size(105, 19);
            this.toolStripStatusLabelWatchers.Text = "Running watchers:";
            // 
            // toolStripStatusLabelWatcherCount
            // 
            this.toolStripStatusLabelWatcherCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelWatcherCount.Name = "toolStripStatusLabelWatcherCount";
            this.toolStripStatusLabelWatcherCount.Size = new System.Drawing.Size(17, 19);
            this.toolStripStatusLabelWatcherCount.Text = "0";
            this.toolStripStatusLabelWatcherCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelProcesses
            // 
            this.toolStripStatusLabelProcesses.Name = "toolStripStatusLabelProcesses";
            this.toolStripStatusLabelProcesses.Size = new System.Drawing.Size(109, 19);
            this.toolStripStatusLabelProcesses.Text = "Running processes:";
            // 
            // toolStripStatusLabelProcessCount
            // 
            this.toolStripStatusLabelProcessCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelProcessCount.Name = "toolStripStatusLabelProcessCount";
            this.toolStripStatusLabelProcessCount.Size = new System.Drawing.Size(17, 19);
            this.toolStripStatusLabelProcessCount.Text = "0";
            this.toolStripStatusLabelProcessCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelAutoStartupValue
            // 
            this.toolStripStatusLabelAutoStartupValue.Name = "toolStripStatusLabelAutoStartupValue";
            this.toolStripStatusLabelAutoStartupValue.Size = new System.Drawing.Size(0, 19);
            // 
            // toolStripStatusLabelProcessesToRun
            // 
            this.toolStripStatusLabelProcessesToRun.Name = "toolStripStatusLabelProcessesToRun";
            this.toolStripStatusLabelProcessesToRun.Size = new System.Drawing.Size(96, 19);
            this.toolStripStatusLabelProcessesToRun.Text = "Processes to run:";
            // 
            // toolStripStatusLabelProcessesToRunCount
            // 
            this.toolStripStatusLabelProcessesToRunCount.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelProcessesToRunCount.Name = "toolStripStatusLabelProcessesToRunCount";
            this.toolStripStatusLabelProcessesToRunCount.Size = new System.Drawing.Size(17, 19);
            this.toolStripStatusLabelProcessesToRunCount.Text = "0";
            this.toolStripStatusLabelProcessesToRunCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStripFileWatchers);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.panelTabs);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Watcher Simple";
            this.Load += new System.EventHandler(this.FormMainLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMainFormClosing);
            this.tabFileWatchers.ResumeLayout(false);
            this.tabPageFileWatchers.ResumeLayout(false);
            this.tabPageLogMessages.ResumeLayout(false);
            this.tabPageLogMessages.PerformLayout();
            this.panelTabs.ResumeLayout(false);
            this.toolStripFileWatchers.ResumeLayout(false);
            this.toolStripFileWatchers.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabFileWatchers;
        private System.Windows.Forms.TabPage tabPageFileWatchers;
        private System.Windows.Forms.ListView listViewFileWatchers;
        private System.Windows.Forms.ColumnHeader columnHeaderDaemon;
        private System.Windows.Forms.ColumnHeader columnHeaderStatus;
        private System.Windows.Forms.ColumnHeader columnHeaderEvents;
        private System.Windows.Forms.ColumnHeader columnHeaderLastEvent;
        private System.Windows.Forms.ColumnHeader columnHeaderLastEventTime;
        private System.Windows.Forms.Panel panelTabs;
        private System.Windows.Forms.ToolStrip toolStripFileWatchers;
        private System.Windows.Forms.ToolStripButton toolStripButtonStopAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonStartAll;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonProperties;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ColumnHeader columnHeaderEnabled;
        private System.Windows.Forms.ToolStripButton toolStripButtonDelete;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem stopAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem propertiesToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWatchers;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProcesses;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelWatcherCount;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProcessCount;
        private System.Windows.Forms.ColumnHeader columnHeaderErrors;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAutoStartupValue;
        private System.Windows.Forms.TabPage tabPageLogMessages;
        private System.Windows.Forms.TextBox textBoxLogMessages;
        private System.Windows.Forms.ToolStripMenuItem lisenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProcessesToRun;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProcessesToRunCount;
    }
}

