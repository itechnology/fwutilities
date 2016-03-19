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
using System.Windows.Forms;
using System.Collections.Generic;
using FileWatcherUtilities.Presenter;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.FileWatcherSimple.Properties;

namespace FileWatcherUtilities.FileWatcherSimple
{
    /// <summary>
    /// Provides file watcher properties dialog.
    /// </summary>
    public partial class FormProperties : Form, IPropertiesView
    {
        /// <summary>
        /// Initializes a new instance of the FormProperties class.
        /// </summary>
        public FormProperties()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles browser directory button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ButtonBrowseDirectoryClick(object sender,
                                                EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = Resources.DialogDescriptionPathToWatch;

                // Show folder selection dialog.
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Update path.
                    textBoxPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Handles browser executable button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ButtonBrowseExecutableClick(object sender,
                                                 EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = Resources.ExecutableFilter;
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                // Show file selection dialog.
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Update path.
                    textBoxProcessFileName.Text = openFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// Handles browse working directory button click event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ButtonBrowseWorkingDirectoryClick(object sender,
                                                       EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = Resources.DialogDescriptionWorkingDirectory;

                // Show folder selection dialog.
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    // Update path.
                    textBoxProcessWorkingDirectory.Text =
                        folderBrowserDialog.SelectedPath;
                }
            }
        }

        /// <summary>
        /// Handles OK button click.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private void ButtonOKClick(object sender,
                                   EventArgs e)
        {
            if (Save != null)
            {
                Save(this, EventArgs.Empty);
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

        #region IPropertiesView Members

        /// <summary>
        /// Occures when configuration is ready to be saved.
        /// </summary>
        public event EventHandler<EventArgs> Save;

        /// <summary>
        /// Occures when configuration has been saved.
        /// </summary>
        public event EventHandler<EventArgs> Saved;

        #region New/Update

        /// <summary>
        /// Returns new or modified file watcher config set.
        /// </summary>
        /// <returns>New or modified file watcher config set.</returns>
        public KeyValuePair<string, FileWatcherConfigurationSet> NewConfigurationKeyValuePair()
        {
            KeyValuePair<string, FileWatcherConfigurationSet> tempConfigurationValueKeyPair =
                new KeyValuePair<string, FileWatcherConfigurationSet>(textBoxDaemonName.Text,
                                                                      new FileWatcherConfigurationSet());

            tempConfigurationValueKeyPair.Value.Filter =
                textBoxFilter.Text;

            tempConfigurationValueKeyPair.Value.GenerateFileSystemEventsAtStartup =
                checkBoxGenerateFileSystemEventsAtStartup.Checked;

            tempConfigurationValueKeyPair.Value.ChangedRegularExpressionFilter =
                textBoxChangedRegularExpressionFilter.Text;

            tempConfigurationValueKeyPair.Value.CreatedRegularExpressionFilter =
                textBoxCreatedRegularExpressionFilter.Text;

            tempConfigurationValueKeyPair.Value.DeletedRegularExpressionFilter =
                textBoxDeletedRegularExpressionFilter.Text;

            tempConfigurationValueKeyPair.Value.RenamedRegularExpressionFilter =
                textBoxRenamedRegularExpressionFilter.Text;

            tempConfigurationValueKeyPair.Value.GeneratedEventFileNameRegularExpressionFilter =
                textBoxGenereatedEventFileNameRegularExpressionFilter.Text;

            tempConfigurationValueKeyPair.Value.Path =
                textBoxPath.Text;

            tempConfigurationValueKeyPair.Value.ProcessArguments =
                textBoxProcessArguments.Text;

            tempConfigurationValueKeyPair.Value.ProcessArgumentsFileNameEscapeString =
                textBoxProcessArgumentsFileNameEscapeString.Text;

            tempConfigurationValueKeyPair.Value.ProcessArgumentsChangeTypeEscapeString =
                textBoxProcessArgumentsChangeTypeEscapeString.Text;

            tempConfigurationValueKeyPair.Value.ProcessArgumentsOldFileNameEscapeString =
                textBoxProcessArgumentsOldFileNameEscapeString.Text;

            tempConfigurationValueKeyPair.Value.ProcessDomain =
                textBoxProcessDomain.Text;

            tempConfigurationValueKeyPair.Value.ProcessFileName =
                textBoxProcessFileName.Text;

            tempConfigurationValueKeyPair.Value.ProcessUserName =
                textBoxProcessUserName.Text;

            tempConfigurationValueKeyPair.Value.ProcessPassword =
                maskedTextBoxProcessPassword.Text;

            tempConfigurationValueKeyPair.Value.ProcessVerb =
                textBoxProcessVerb.Text;

            tempConfigurationValueKeyPair.Value.ProcessWorkingDirectory =
                textBoxProcessWorkingDirectory.Text;

            tempConfigurationValueKeyPair.Value.DisplayFileSystemChange =
                checkBoxDisplayFileSystemChange.Checked;

            tempConfigurationValueKeyPair.Value.IncludeSubdirectories =
                checkBoxIncludeSubdirectories.Checked;

            tempConfigurationValueKeyPair.Value.LogFileSystemChange =
                checkBoxLogFileSystemChange.Checked;

            tempConfigurationValueKeyPair.Value.LogProcessEnd =
                checkBoxLogProcessEnd.Checked;

            tempConfigurationValueKeyPair.Value.LogProcessStart =
                checkBoxLogProcessStart.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterAttributes =
                checkBoxNotifyAttribute.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterCreationTime =
                checkBoxNotifyCreationTime.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterDirectoryName =
                checkBoxNotifyDirectoryName.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterFileName =
                checkBoxNotifyFileName.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterLastAccess =
                checkBoxNotifyLastAccess.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterLastWrite =
                checkBoxNotifyLastWrite.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterSecurity =
                checkBoxNotifySecurity.Checked;

            tempConfigurationValueKeyPair.Value.NotifyFilterSize =
                checkBoxNotifySize.Checked;

            tempConfigurationValueKeyPair.Value.ProcessCreateNoWindow =
                checkBoxProcessCreateNoWindow.Checked;

            tempConfigurationValueKeyPair.Value.ProcessLoadUserProfile =
                checkBoxProcessLoadUserProfile.Checked;

            tempConfigurationValueKeyPair.Value.ProcessRedirectStandardError =
                checkBoxProcessRedirectStandardError.Checked;

            tempConfigurationValueKeyPair.Value.ProcessRedirectStandardOutput =
                checkBoxProcessRedirectStandardOutput.Checked;

            tempConfigurationValueKeyPair.Value.ProcessSynchronizedExecution =
                checkBoxProcessSynchronizedExecution.Checked;

            tempConfigurationValueKeyPair.Value.ProcessUseFileNameAsArgument =
                checkBoxProcessUseFileNameAsArgument.Checked;

            tempConfigurationValueKeyPair.Value.ProcessUseChangeTypeAsArgument =
                checkBoxProcessUseChangeTypeAsArgument.Checked;

            tempConfigurationValueKeyPair.Value.ProcessUseOldFileNameAsArgument =
                checkBoxProcessUseOldFileNameAsArgument.Checked;

            tempConfigurationValueKeyPair.Value.ProcessUseShellExecute =
                checkBoxProcessUseShellExecute.Checked;

            tempConfigurationValueKeyPair.Value.StartDaemon =
                checkBoxStartDaemon.Checked;

            tempConfigurationValueKeyPair.Value.StartProcess =
                checkBoxStartProcess.Checked;

            tempConfigurationValueKeyPair.Value.SubscribeToBufferErrorEvent =
                checkBoxSubscribeToBufferErrorEvent.Checked;

            tempConfigurationValueKeyPair.Value.SubscribeToChangedEvent =
                checkBoxSubscribeToChangedEvent.Checked;

            tempConfigurationValueKeyPair.Value.SubscribeToCreatedEvent =
                checkBoxSubscribeToCreatedEvent.Checked;

            tempConfigurationValueKeyPair.Value.SubscribeToDeletedEvent =
                checkBoxSubscribeToDeletedEvent.Checked;

            tempConfigurationValueKeyPair.Value.SubscribeToRenamedEvent =
                checkBoxSubscribeToRenamedEvent.Checked;

            tempConfigurationValueKeyPair.Value.InternalBufferSize =
                Convert.ToInt32(numericUpDownInternalBufferSize.Value);

            tempConfigurationValueKeyPair.Value.ProcessMaxWaitTime =
                Convert.ToInt32(numericUpDownProcessMaxWaitTime.Value);

            tempConfigurationValueKeyPair.Value.ProcessBatchSize =
               Convert.ToInt32(numericUpDownProcessBatchSize.Value);

            tempConfigurationValueKeyPair.Value.LogFileSystemSearchError =
                checkBoxLogFileSystemSearchError.Checked;

            tempConfigurationValueKeyPair.Value.LogFileSystemSearchProgress =
                checkBoxLogFileSystemSearchProgress.Checked;

            tempConfigurationValueKeyPair.Value.DisplayFileSystemSearchError =
                checkBoxDisplayFileSystemSearchError.Checked;

            tempConfigurationValueKeyPair.Value.DisplayFileSystemSearchProgress =
               checkBoxDisplayFileSystemSearchProgress.Checked;

            tempConfigurationValueKeyPair.Value.ProcessLockFile =
                checkBoxProcessLockFile.Checked;

            tempConfigurationValueKeyPair.Value.ProcessFileMustExist =
                checkBoxProcessFileMustExist.Checked;

            tempConfigurationValueKeyPair.Value.ProcessDelay =
                Convert.ToInt32(numericUpDownProcessDelay.Value);

            tempConfigurationValueKeyPair.Value.ProcessLockFileLastWriteDelay =
                Convert.ToInt32(numericUpDownProcessLockFileLastWriteDelay.Value);

            tempConfigurationValueKeyPair.Value.ProcessLockFileRetries =
                Convert.ToInt32(numericUpDownProcessLockFileRetries.Value);

            tempConfigurationValueKeyPair.Value.ProcessLockFileRetriesQueueLimit =
                Convert.ToInt32(numericUpDownProcessLockFileRetriesQueueLimit.Value);

            tempConfigurationValueKeyPair.Value.CallService =
                checkBoxCallService.Checked;

            tempConfigurationValueKeyPair.Value.StreamFile =
                checkBoxStreamFile.Checked;

            tempConfigurationValueKeyPair.Value.TryRenameFile =
                checkBoxTryRenameFile.Checked;

            tempConfigurationValueKeyPair.Value.TryRenameFileRetries =
                Convert.ToInt32(numericUpDownTryRenameRetries.Value);
                    
            // Selected index text.
            tempConfigurationValueKeyPair.Value.ProcessWindowStyle =
                comboBoxWindowStyle.SelectedItem.ToString();

            tempConfigurationValueKeyPair.Value.FilteredMode =
                checkBoxFilteredMode.Checked;

            tempConfigurationValueKeyPair.Value.FilteredModeFilterTimeout =
                Convert.ToInt32(numericUpDownFilteredModeFilterTimeout.Value);

            tempConfigurationValueKeyPair.Value.PollDirectory =
                checkBoxPollDirectory.Checked;

            tempConfigurationValueKeyPair.Value.DirectoryPollInterval =
                Convert.ToInt32(numericUpDownPollDirectoryInterval.Value);

            tempConfigurationValueKeyPair.Value.RecycleFileWatcher =
                checkBoxRecycleFileWatcher.Checked;

            tempConfigurationValueKeyPair.Value.RecycleInterval =
                Convert.ToInt32(numericUpDownRecycleInterval.Value);

            return tempConfigurationValueKeyPair;
        }

        /// <summary>
        /// Updates file watcher configuration to view.
        /// </summary>
        /// <param name="configurationKeyValuePair">Configuration to view.</param>
        public void ViewFileWatcherConfiguration(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair)
        {
            textBoxDaemonName.Text =
                configurationKeyValuePair.Key;

            textBoxFilter.Text =
                configurationKeyValuePair.Value.Filter;

            checkBoxGenerateFileSystemEventsAtStartup.Checked =
                configurationKeyValuePair.Value.GenerateFileSystemEventsAtStartup;

            textBoxChangedRegularExpressionFilter.Text =
                configurationKeyValuePair.Value.ChangedRegularExpressionFilter;

            textBoxCreatedRegularExpressionFilter.Text =
                configurationKeyValuePair.Value.CreatedRegularExpressionFilter;

            textBoxDeletedRegularExpressionFilter.Text =
                configurationKeyValuePair.Value.DeletedRegularExpressionFilter;

            textBoxRenamedRegularExpressionFilter.Text =
                configurationKeyValuePair.Value.RenamedRegularExpressionFilter;

            textBoxGenereatedEventFileNameRegularExpressionFilter.Text =
                configurationKeyValuePair.Value.GeneratedEventFileNameRegularExpressionFilter;

            textBoxPath.Text =
                configurationKeyValuePair.Value.Path;

            textBoxProcessArguments.Text =
                configurationKeyValuePair.Value.ProcessArguments;

            textBoxProcessArgumentsFileNameEscapeString.Text =
                configurationKeyValuePair.Value.ProcessArgumentsFileNameEscapeString;

            textBoxProcessArgumentsChangeTypeEscapeString.Text =
                configurationKeyValuePair.Value.ProcessArgumentsChangeTypeEscapeString;

            textBoxProcessArgumentsOldFileNameEscapeString.Text =
                configurationKeyValuePair.Value.ProcessArgumentsOldFileNameEscapeString;

            textBoxProcessDomain.Text =
                configurationKeyValuePair.Value.ProcessDomain;

            textBoxProcessFileName.Text =
                configurationKeyValuePair.Value.ProcessFileName;

            textBoxProcessUserName.Text =
                configurationKeyValuePair.Value.ProcessUserName;

            maskedTextBoxProcessPassword.Text =
                configurationKeyValuePair.Value.ProcessPassword;

            textBoxProcessVerb.Text =
                configurationKeyValuePair.Value.ProcessVerb;

            textBoxProcessWorkingDirectory.Text =
                configurationKeyValuePair.Value.ProcessWorkingDirectory;

            checkBoxDisplayFileSystemChange.Checked =
                configurationKeyValuePair.Value.DisplayFileSystemChange;

            checkBoxIncludeSubdirectories.Checked =
                configurationKeyValuePair.Value.IncludeSubdirectories;

            checkBoxLogFileSystemChange.Checked =
                configurationKeyValuePair.Value.LogFileSystemChange;

            checkBoxLogProcessEnd.Checked =
                configurationKeyValuePair.Value.LogProcessEnd;

            checkBoxLogProcessStart.Checked =
                configurationKeyValuePair.Value.LogProcessStart;

            checkBoxNotifyAttribute.Checked =
                configurationKeyValuePair.Value.NotifyFilterAttributes;

            checkBoxNotifyCreationTime.Checked =
                configurationKeyValuePair.Value.NotifyFilterCreationTime;

            checkBoxNotifyDirectoryName.Checked =
                configurationKeyValuePair.Value.NotifyFilterDirectoryName;

            checkBoxNotifyFileName.Checked =
                configurationKeyValuePair.Value.NotifyFilterFileName;

            checkBoxNotifyLastAccess.Checked =
                configurationKeyValuePair.Value.NotifyFilterLastAccess;

            checkBoxNotifyLastWrite.Checked =
                configurationKeyValuePair.Value.NotifyFilterLastWrite;

            checkBoxNotifySecurity.Checked =
                configurationKeyValuePair.Value.NotifyFilterSecurity;

            checkBoxNotifySize.Checked =
                configurationKeyValuePair.Value.NotifyFilterSize;

            checkBoxProcessCreateNoWindow.Checked =
                configurationKeyValuePair.Value.ProcessCreateNoWindow;

            checkBoxProcessLoadUserProfile.Checked =
                configurationKeyValuePair.Value.ProcessLoadUserProfile;

            checkBoxProcessRedirectStandardError.Checked =
                configurationKeyValuePair.Value.ProcessRedirectStandardError;

            checkBoxProcessRedirectStandardOutput.Checked =
                configurationKeyValuePair.Value.ProcessRedirectStandardOutput;

            checkBoxProcessSynchronizedExecution.Checked =
                configurationKeyValuePair.Value.ProcessSynchronizedExecution;

            checkBoxProcessUseFileNameAsArgument.Checked =
                configurationKeyValuePair.Value.ProcessUseFileNameAsArgument;

            checkBoxProcessUseChangeTypeAsArgument.Checked =
                configurationKeyValuePair.Value.ProcessUseChangeTypeAsArgument;

            checkBoxProcessUseOldFileNameAsArgument.Checked =
                configurationKeyValuePair.Value.ProcessUseOldFileNameAsArgument;

            checkBoxProcessUseShellExecute.Checked =
                configurationKeyValuePair.Value.ProcessUseShellExecute;

            checkBoxStartDaemon.Checked =
                configurationKeyValuePair.Value.StartDaemon;

            checkBoxStartProcess.Checked =
                configurationKeyValuePair.Value.StartProcess;

            checkBoxSubscribeToBufferErrorEvent.Checked =
                configurationKeyValuePair.Value.SubscribeToBufferErrorEvent;

            checkBoxSubscribeToChangedEvent.Checked =
                configurationKeyValuePair.Value.SubscribeToChangedEvent;

            checkBoxSubscribeToCreatedEvent.Checked =
                configurationKeyValuePair.Value.SubscribeToCreatedEvent;

            checkBoxSubscribeToDeletedEvent.Checked =
                configurationKeyValuePair.Value.SubscribeToDeletedEvent;

            checkBoxSubscribeToRenamedEvent.Checked =
                configurationKeyValuePair.Value.SubscribeToRenamedEvent;

            numericUpDownInternalBufferSize.Value =
                configurationKeyValuePair.Value.InternalBufferSize;

            numericUpDownProcessMaxWaitTime.Value =
                configurationKeyValuePair.Value.ProcessMaxWaitTime;

            numericUpDownProcessBatchSize.Value =
                configurationKeyValuePair.Value.ProcessBatchSize;

            checkBoxLogFileSystemSearchError.Checked =
                configurationKeyValuePair.Value.LogFileSystemSearchError;

            checkBoxLogFileSystemSearchProgress.Checked =
                configurationKeyValuePair.Value.LogFileSystemSearchProgress;

            checkBoxDisplayFileSystemSearchError.Checked =
                configurationKeyValuePair.Value.DisplayFileSystemSearchError;

            checkBoxDisplayFileSystemSearchProgress.Checked =
                configurationKeyValuePair.Value.DisplayFileSystemSearchProgress;

            checkBoxProcessLockFile.Checked =
                configurationKeyValuePair.Value.ProcessLockFile;

            checkBoxProcessFileMustExist.Checked =
                configurationKeyValuePair.Value.ProcessFileMustExist;

            numericUpDownProcessDelay.Value =
                configurationKeyValuePair.Value.ProcessDelay;

            numericUpDownProcessLockFileLastWriteDelay.Value =
                configurationKeyValuePair.Value.ProcessLockFileLastWriteDelay;

            numericUpDownProcessLockFileRetries.Value =
                configurationKeyValuePair.Value.ProcessLockFileRetries;

            numericUpDownProcessLockFileRetriesQueueLimit.Value =
                configurationKeyValuePair.Value.ProcessLockFileRetriesQueueLimit;

            checkBoxCallService.Checked =
                configurationKeyValuePair.Value.CallService;

            checkBoxStreamFile.Checked =
                configurationKeyValuePair.Value.StreamFile;

            checkBoxTryRenameFile.Checked =
                configurationKeyValuePair.Value.TryRenameFile;

            numericUpDownTryRenameRetries.Value =
                configurationKeyValuePair.Value.TryRenameFileRetries;

            // Seek selected index.
            comboBoxWindowStyle.SelectedIndex =
                comboBoxWindowStyle.FindStringExact(configurationKeyValuePair.Value.ProcessWindowStyle);

            checkBoxFilteredMode.Checked =
                configurationKeyValuePair.Value.FilteredMode;

            numericUpDownFilteredModeFilterTimeout.Value =
                configurationKeyValuePair.Value.FilteredModeFilterTimeout;

            checkBoxPollDirectory.Checked =
                configurationKeyValuePair.Value.PollDirectory;

            numericUpDownPollDirectoryInterval.Value =
                configurationKeyValuePair.Value.DirectoryPollInterval;

            checkBoxRecycleFileWatcher.Checked =
                configurationKeyValuePair.Value.RecycleFileWatcher;

            numericUpDownRecycleInterval.Value =
                configurationKeyValuePair.Value.RecycleInterval;
        }

        #endregion

        /// <summary>
        /// Shows this view.
        /// </summary>
        public void ShowView()
        {
            ShowDialog();
        }

        /// <summary>
        /// Hides this view.
        /// </summary>
        public void HideView()
        {
            if (Saved != null)
            {
                Saved(this,
                      EventArgs.Empty);
            }
            Hide();
        }

        /// <summary>
        /// Shows error message.
        /// </summary>
        /// <param name="message">Error message to display.</param>
        public void ShowError(string message)
        {
            MessageBox.Show(this,
                            message,
                            Resources.MessageBoxCaptionError,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions);
        }

        #endregion        
    }
}