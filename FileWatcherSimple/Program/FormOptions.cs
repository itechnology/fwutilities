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
using FileWatcherUtilities.Options;
using FileWatcherUtilities.Presenter;
using FileWatcherUtilities.FileWatcherSimple.Properties;

namespace FileWatcherUtilities.FileWatcherSimple
{
    /// <summary>
    /// Provides application options dialog.
    /// </summary>
    public partial class FormOptions : Form, IOptionsView
    {
        /// <summary>
        /// Initializes a new instance of the FormOptions class.
        /// </summary>
        public FormOptions()
        {
             InitializeComponent();
        }

        /// <summary>
        /// Handles OK button click event.
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

        #region IOptionsView Members

        /// <summary>
        /// Occures when application options are ready to be saved.
        /// </summary>
        public event EventHandler<EventArgs> Save;

        /// <summary>
        /// Display application options on the view.
        /// </summary>
        /// <param name="applicationOptions">Application options to display.</param>
        public void ViewApplicationOptions(ApplicationOptions applicationOptions)
        {
            if (applicationOptions == null)
            {
                throw new ArgumentNullException("applicationOptions",
                                                Resources.ArgumentNullException);
            }

            checkBoxApplicationAutoStartup.Checked =
                applicationOptions.AutoStartup;

            checkBoxApplicationRunQueuedProcesses.Checked =
                applicationOptions.RunQueuedProcesses;

            checkBoxApplicationSynchronousExecution.Checked =
                applicationOptions.SynchronousExecution;

            numericUpDownLogMessages.Value =
                applicationOptions.LogMessages;

            numericUpDownProcessBatchSize.Value =
                applicationOptions.ProcessBatchSize;
        }

        /// <summary>
        /// Returns modified application options.
        /// </summary>
        /// <returns>Modified application options.</returns>
        public ApplicationOptions NewApplicationOptions()
        {
            ApplicationOptions applicationOptions = new ApplicationOptions();

            applicationOptions.AutoStartup = 
                checkBoxApplicationAutoStartup.Checked;

            applicationOptions.RunQueuedProcesses = 
                checkBoxApplicationRunQueuedProcesses.Checked;

            applicationOptions.SynchronousExecution = 
                checkBoxApplicationSynchronousExecution.Checked;

            applicationOptions.LogMessages =
                Convert.ToInt32(numericUpDownLogMessages.Value);

            applicationOptions.ProcessBatchSize =
                Convert.ToInt32(numericUpDownProcessBatchSize.Value);

            return applicationOptions;
        }

        /// <summary>
        /// Sets or gets synchronous execution enabled.
        /// </summary>
        public bool SynchronousExecutionEnabled 
        {
            get
            {
                return checkBoxApplicationSynchronousExecution.Enabled;
            }
            set
            {
                checkBoxApplicationSynchronousExecution.Enabled = value;
            }
        }

        /// <summary>
        /// Shows view.
        /// </summary>
        public void ShowView()
        {
            ShowDialog();
        }

        /// <summary>
        /// Hides view.
        /// </summary>
        public void HideView()
        {
            Hide();
        }

        #endregion
    }
}