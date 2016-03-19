/******************************************************************************
*    File Watcher Utilities / File Watcher Simple
*    Copyright (c) 2006-2013 Jussi Hiltunen
*
*    This program is free software; you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation; either version 2 of the License, or
*    (at your option) any later version.
*
*    This program is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*
*    You should have received a copy of the GNU General Public License
*    along with this program; if not, write to the Free Software
*    Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
******************************************************************************/

using System;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using FileWatcherUtilities.Presenter;
using FileWatcherUtilities.FileWatcherSimple.Properties;

namespace FileWatcherUtilities.FileWatcherSimple
{
    /// <summary>
    /// Provides application main window.
    /// </summary>
    public partial class FormMain : Form, IMainView, ILogView
    {
        /// <summary>
        /// Initializes a new instance of the FormMain class.
        /// </summary>
        public FormMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles form load event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event data.</param>
        private void FormMainLoad(object sender, 
                                  EventArgs e)
        {
            // Notify that list must be filled.
            if (ListInitialize != null)
            {
                ListInitialize(this,
                               EventArgs.Empty);
            }
            
            // Set double buffering for the form.
            SetDoubleBuffering();
        }

        /// <summary>
        /// Set double buffering for the form. 
        /// </summary>
        private void SetDoubleBuffering()
        {
            // Use double buffering.
            SetStyle(ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.OptimizedDoubleBuffer | 
                     ControlStyles.UserPaint, true);
        }

        /// <summary>
        /// Handles form closing event.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event data.</param>
        private void FormMainFormClosing(object sender,
                                         FormClosingEventArgs e)
        {
            if (!ExitEnabled)
            {
                MessageBox.Show(this,
                                Resources.StopControllerMessage,
                                Resources.MessageBoxCaptionApplication,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions);
                e.Cancel = true;
            }
            else
            {
                if (Exit != null)
                {
                    Exit(this, 
                         EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Returns first selected daemon or null.
        /// </summary>
        /// <returns>First selected daemon or null.</returns>
        private string GetFirstSelectedItem()
        {
            ListView.SelectedListViewItemCollection selectedItems =
                listViewFileWatchers.SelectedItems;

            foreach (ListViewItem item in selectedItems)
            {
                return item.Text;
            }
            return null;
        }

        /// <summary>
        /// Disables or enables menus when selected tab index changes.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void TabFileWatchersSelectedIndexChanged(object sender,
                                                         EventArgs e)
        {
            if (tabFileWatchers.SelectedIndex == 1)
            {
                if (ViewLog != null)
                {
                    ViewLog(this,
                            EventArgs.Empty);
                }
            }
            else if (tabFileWatchers.SelectedIndex != 1)
            {
                if (ViewClosed != null)
                {
                    ViewClosed(this,
                               EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Enables or disables menus when selected item changes.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">e</param>
        private void ListViewFileWatchersSelectedIndexChanged(object sender,
                                                              EventArgs e)
        {
            if (DaemonSelected != null)
            {
                DaemonSelected(this, 
                               new DaemonSelectedEventArgs(GetFirstSelectedItem()));
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void OptionsToolStripMenuItemClick(object sender,
                                                   EventArgs e)
        {
            if (ViewOptions != null)
            {
                ViewOptions(this, 
                            EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets MessageBoxOptions.
        /// </summary>
        private MessageBoxOptions MessageBoxOptions
        {
            get
            {
                if (RightToLeft == RightToLeft.Yes)
                {
                    return MessageBoxOptions.RtlReading |
                           MessageBoxOptions.RightAlign;
                }
                return 0;
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void NewToolStripMenuItemClick(object sender,
                                               EventArgs e)
        {
            if (ViewNew != null)
            {
                ViewNew(this,
                        EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void DeleteToolStripMenuItemClick(object sender,
                                                  EventArgs e)
        {
            string daemonName = GetFirstSelectedItem();

            if (daemonName != null)
            {
                DialogResult dialogResult = MessageBox.Show(this,
                                                            String.Format(CultureInfo.CurrentCulture,
                                                                          Resources.MessageAreYouSureYouWantToDeleteFileWatcher,
                                                                          daemonName),
                                                            Resources.MessageBoxCaptionApplication,
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button2,
                                                            MessageBoxOptions);

                if (dialogResult == DialogResult.Yes)
                {
                    if (Delete != null)
                    {
                        Delete(this,
                               EventArgs.Empty);
                    }
                    if (DaemonSelected != null)
                    {
                        DaemonSelected(this,
                                       new DaemonSelectedEventArgs(GetFirstSelectedItem()));
                    }
                }
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void PropertiesToolStripMenuItemClick(object sender,
                                                      EventArgs e)
        {
            if (ViewProperties != null)
            {
                ViewProperties(this, 
                               EventArgs.Empty);
            }
            if (DaemonSelected != null)
            {
                DaemonSelected(this, 
                               new DaemonSelectedEventArgs(GetFirstSelectedItem()));
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void HelpToolStripMenuItem1Click(object sender, 
                                                 EventArgs e)
        {
            if (ViewHelp != null)
            {
                ViewHelp(this,
                         EventArgs.Empty);
            }

            try
            {
                // Display help in another application.
                Process.Start(Path.Combine(Environment.CurrentDirectory, @"Help\Index.html"));
            }
            catch
            {
                MessageBox.Show(this,
                                Resources.MessageHelpCannotBeViewed,
                                Resources.MessageBoxCaptionApplication,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions);
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void StartToolStripMenuItemClick(object sender,
                                                 EventArgs e)
        {
            if (Start != null)
            {
                Start(this,
                      EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void StartAllToolStripMenuItemClick(object sender,
                                                    EventArgs e)
        {
            if (StartAll != null)
            {
                StartAll(this, 
                         EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void StopToolStripMenuItemClick(object sender,
                                                EventArgs e)
        {
            if (Stop != null)
            {
                Stop(this, 
                     EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void StopAllToolStripMenuItemClick(object sender,
                                                   EventArgs e)
        {
            if (StopAll != null)
            {
                StopAll(this, 
                        EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ExitToolStripMenuItemClick(object sender,
                                                EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonStopAllClick(object sender,
                                                 EventArgs e)
        {
            if (StopAll != null)
            {
                StopAll(this,
                        EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonStartAllClick(object sender,
                                                  EventArgs e)
        {
            if (StartAll != null)
            {
                StartAll(this,
                         EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonStartClick(object sender,
                                               EventArgs e)
        {
            if (Start != null)
            {
                Start(this,
                      EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonStopClick(object sender,
                                              EventArgs e)
        {
            if (Stop != null)
            {
                Stop(this,
                     EventArgs.Empty);
            }
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonPropertiesClick(object sender,
                                                    EventArgs e)
        {
            if (ViewProperties != null)
            {
                ViewProperties(this, 
                               EventArgs.Empty);
            }
            if (DaemonSelected != null)
            {
                DaemonSelected(this, 
                               new DaemonSelectedEventArgs(GetFirstSelectedItem()));
            }
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonNewClick(object sender,
                                             EventArgs e)
        {
            if (ViewNew != null)
            {
                ViewNew(this,
                        EventArgs.Empty);
            }
            if (DaemonSelected != null)
            {
                DaemonSelected(this,
                               new DaemonSelectedEventArgs(GetFirstSelectedItem()));
            }
        }

        /// <summary>
        /// Handles menu button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ToolStripButtonDeleteClick(object sender,
                                                EventArgs e)
        {
            string daemonName = GetFirstSelectedItem();

            if (daemonName != null)
            {
                DialogResult dialogResult = MessageBox.Show(this,
                                                            String.Format(CultureInfo.CurrentCulture,
                                                                          Resources.MessageAreYouSureYouWantToDeleteFileWatcher,
                                                                          daemonName),
                                                            Resources.MessageBoxCaptionApplication,
                                                            MessageBoxButtons.YesNo,
                                                            MessageBoxIcon.Question,
                                                            MessageBoxDefaultButton.Button2,
                                                            MessageBoxOptions);

                if (dialogResult == DialogResult.Yes)
                {
                    if (Delete != null)
                    {
                        Delete(this,
                               EventArgs.Empty);
                    }
                    if (DaemonSelected != null)
                    {
                        DaemonSelected(this,
                                       new DaemonSelectedEventArgs(GetFirstSelectedItem()));
                    }
                }
            }
        }

        /// <summary>
        /// Handles menu item click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void AboutToolStripMenuItemClick(object sender,
                                                 EventArgs e)
        {
            if (ViewAbout != null)
            {
                ViewAbout(this, 
                          EventArgs.Empty);
            }

            // Show about dialog.
            using (FormAbout formAbout = new FormAbout())
            {
                formAbout.ShowDialog();
            }
        }

        /// <summary>
        /// Handles lisence menu click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void LisenceToolStripMenuItemClick(object sender,
                                                   EventArgs e)
        {
            if (ViewLicense != null)
            {
                ViewLicense(this, 
                            EventArgs.Empty);
            }

            try
            {
                // Display license in another application.
                Process.Start("COPYING.TXT");
            }
            catch (Win32Exception)
            {
                MessageBox.Show(this,
                                Resources.MessageLicenseCannotBeViewed,
                                Resources.MessageBoxCaptionApplication,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Stop,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show(this,
                Resources.MessageLicenseCannotBeViewed,
                Resources.MessageBoxCaptionApplication,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions);
            }
        }

        #region IMainView Members

        /// <summary>
        /// Occures when start is selected.
        /// </summary>
        public event EventHandler<EventArgs> Start;

        /// <summary>
        /// Occures when stop is selected.
        /// </summary>
        public event EventHandler<EventArgs> Stop;

        /// <summary>
        /// Occures when stop all is selected.
        /// </summary>
        public event EventHandler<EventArgs> StopAll;

        /// <summary>
        /// Occures when start all is selected.
        /// </summary>
        public event EventHandler<EventArgs> StartAll;

        /// <summary>
        /// Occures when delete is selected.
        /// </summary>
        public event EventHandler<EventArgs> Delete;

        /// <summary>
        /// Occures when exit is selected.
        /// </summary>
        public event EventHandler<EventArgs> Exit;

        /// <summary>
        /// Occures when list initializes.
        /// </summary>
        public event EventHandler<EventArgs> ListInitialize;

        /// <summary>
        /// Occures when daemon is selected.
        /// </summary>
        public event EventHandler<DaemonSelectedEventArgs> DaemonSelected;

        /// <summary>
        /// Occures when viewing new.
        /// </summary>
        public event EventHandler<EventArgs> ViewNew;

        /// <summary>
        /// Occures when viewing properties.
        /// </summary>
        public event EventHandler<EventArgs> ViewProperties;

        /// <summary>
        /// Occures when viewing options.
        /// </summary>
        public event EventHandler<EventArgs> ViewOptions;

        /// <summary>
        /// Occures when viewing about.
        /// </summary>
        public event EventHandler<EventArgs> ViewAbout;

        /// <summary>
        /// Occures when viewing help.
        /// </summary>
        public event EventHandler<EventArgs> ViewHelp;

        /// <summary>
        /// Occures when viewing license.
        /// </summary>
        public event EventHandler<EventArgs> ViewLicense;

        /// <summary>
        /// Occures when viewing log.
        /// </summary>
        public event EventHandler<EventArgs> ViewLog;

        /// <summary>
        /// Gets or sets new enabled.
        /// </summary>
        public bool NewEnabled
        {
            get
            {
                return toolStripButtonNew.Enabled && newToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonNew.Enabled = value;
                newToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets start enabled.
        /// </summary>
        public bool StartEnabled
        {
            get
            {
                return toolStripButtonStart.Enabled && startToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonStart.Enabled = value;
                startToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets stop enabled.
        /// </summary>
        public bool StopEnabled
        {
            get
            {
                return toolStripButtonStop.Enabled && stopToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonStop.Enabled = value;
                stopToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets start all enabled.
        /// </summary>
        public bool StartAllEnabled
        {
            get
            {
                return toolStripButtonStartAll.Enabled && startAllToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonStartAll.Enabled = value;
                startAllToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets stop all enabled.
        /// </summary>
        public bool StopAllEnabled
        {
            get
            {
                return toolStripButtonStopAll.Enabled && stopAllToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonStopAll.Enabled = value;
                stopAllToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets delete enabled.
        /// </summary>
        public bool DeleteEnabled
        {
            get
            {
                return toolStripButtonDelete.Enabled && deleteToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonDelete.Enabled = value;
                deleteToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets properties enabled.
        /// </summary>
        public bool PropertiesEnabled
        {
            get
            {
                return toolStripButtonProperties.Enabled && propertiesToolStripMenuItem.Enabled;
            }
            set
            {
                toolStripButtonProperties.Enabled = value;
                propertiesToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets exit enabled.
        /// </summary>
        public bool ExitEnabled
        {
            get
            {
                return exitToolStripMenuItem.Enabled;
            }
            set
            {
                exitToolStripMenuItem.Enabled = value;
            }
        }

        /// <summary>
        /// Updates file watcher list.
        /// </summary>
        /// <param name="fileWatcherSortedDictionary">List of file watcher info.</param>
        public void UpdateList(SortedDictionary<string, FileWatcherInfo> fileWatcherSortedDictionary)
        {
            if (fileWatcherSortedDictionary == null)
            {
                throw new ArgumentNullException("fileWatcherSortedDictionary",
                                                Resources.ArgumentNullException);
            }

            // Begin update of control.
            listViewFileWatchers.BeginUpdate();

            // If new or removed items to update.
            if (listViewFileWatchers.Items.Count != fileWatcherSortedDictionary.Count)
            {
                // Remove file watchers
                listViewFileWatchers.Items.Clear();

                // Add file watchers to list view
                foreach (KeyValuePair<string, FileWatcherInfo> configurationKeyValuePair in fileWatcherSortedDictionary)
                {
                    listViewFileWatchers.Items.Add(configurationKeyValuePair.Key);
                    listViewFileWatchers.Items[listViewFileWatchers.Items.Count - 1].SubItems.Add(configurationKeyValuePair.Value.Status);
                    listViewFileWatchers.Items[listViewFileWatchers.Items.Count - 1].SubItems.Add(configurationKeyValuePair.Value.Events.ToString(NumberFormatInfo.InvariantInfo));
                    listViewFileWatchers.Items[listViewFileWatchers.Items.Count - 1].SubItems.Add(configurationKeyValuePair.Value.LastEventTime);
                    listViewFileWatchers.Items[listViewFileWatchers.Items.Count - 1].SubItems.Add(configurationKeyValuePair.Value.LastEventType);
                    listViewFileWatchers.Items[listViewFileWatchers.Items.Count - 1].SubItems.Add(configurationKeyValuePair.Value.Errors.ToString(NumberFormatInfo.InvariantInfo));
                    listViewFileWatchers.Items[listViewFileWatchers.Items.Count - 1].SubItems.Add(configurationKeyValuePair.Value.Enabled.ToString(CultureInfo.InvariantCulture));
                }
            }
            else
            {
                int index = 0;

                // Update file watchers to list view.
                foreach (KeyValuePair<string, FileWatcherInfo> configurationKeyValuePair in fileWatcherSortedDictionary)
                {
                    listViewFileWatchers.Items[index].SubItems[0].Text = (configurationKeyValuePair.Key);
                    listViewFileWatchers.Items[index].SubItems[1].Text = (configurationKeyValuePair.Value.Status);
                    listViewFileWatchers.Items[index].SubItems[2].Text = (configurationKeyValuePair.Value.Events.ToString(NumberFormatInfo.InvariantInfo));
                    listViewFileWatchers.Items[index].SubItems[3].Text = (configurationKeyValuePair.Value.LastEventType);
                    listViewFileWatchers.Items[index].SubItems[4].Text = (configurationKeyValuePair.Value.LastEventTime);
                    listViewFileWatchers.Items[index].SubItems[5].Text = (configurationKeyValuePair.Value.Errors.ToString(NumberFormatInfo.InvariantInfo));
                    listViewFileWatchers.Items[index].SubItems[6].Text = (configurationKeyValuePair.Value.Enabled.ToString(CultureInfo.InvariantCulture));

                    // Increase index.
                    index++;
                }
            }

            // Refresh selected daemon.
            if (DaemonSelected != null)
            {
                DaemonSelected(this, new DaemonSelectedEventArgs(GetFirstSelectedItem()));
            }

            // End update of control.
            listViewFileWatchers.EndUpdate();
        }

        /// <summary>
        /// Updates number of running processes.
        /// </summary>
        /// <param name="runningProcesses">Number of running processes.</param>
        public void UpdateRunningProcesses(int runningProcesses)
        {
            toolStripStatusLabelProcessCount.Text = runningProcesses.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Updates number of running file watchers.
        /// </summary>
        /// <param name="runningFileWatchers">Number of running file watchers.</param>
        public void UpdateRunningFileWatchers(int runningFileWatchers)
        { 
            toolStripStatusLabelWatcherCount.Text = runningFileWatchers.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Updates number of processes to run.
        /// </summary>
        /// <param name="processesToRun">Number of processes to run.</param>
        public void UpdateProcessesToRun(int processesToRun)
        {
            toolStripStatusLabelProcessesToRunCount.Text = processesToRun.ToString(NumberFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// Shows file watcher path error message.
        /// </summary>
        /// <param name="message">message.</param>
        public void ShowFileWatcherPathErrorMessage(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message",
                                                Resources.ArgumentNullException);
            }
 
            MessageBox.Show(message,
                            Resources.MessageBoxCaptionApplication,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions);
        }

        #endregion

        #region ILogView Members

        /// <summary>
        /// Occures when view is closed.
        /// </summary>
        public event EventHandler<EventArgs> ViewClosed;

        /// <summary>
        /// Updates view.
        /// </summary>
        /// <param name="logMessages">Log messages.</param>
        public void Update(string logMessages)
        {
            if (logMessages == null)
            {
                throw new ArgumentNullException("logMessages",
                                                Resources.ArgumentNullException);
            }
            textBoxLogMessages.Text = logMessages;
        }

        #endregion
    }
}