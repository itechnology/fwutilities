/******************************************************************************
*    File Watcher Utilities / Presenter
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
using System.Text;
using System.Threading;
using System.Globalization;
using FileWatcherUtilities.Logger;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides windows service view presenter.
    /// </summary>
    public class WindowsServiceViewPresenter : MainViewPresenterBase
    {
        /// <summary>
        /// Initializes a new instance of the WindowsServiceViewPresenter class.
        /// </summary>
        /// <param name="windowsServiceView">Windows service view.</param>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="formatter">Log formatter.</param>
        public WindowsServiceViewPresenter(IWindowsServiceView windowsServiceView,
                                           FileWatcherController fileWatcherController,
                                           ILogger logger,
                                           IFormatter formatter)
            : base(fileWatcherController, 
                   logger,
                   formatter)
        {
            if (windowsServiceView == null)
            {
                throw new ArgumentNullException("windowsServiceView",
                                                Resources.ArgumentNullException);
            }
            _windowsServiceView = windowsServiceView;
            SubscribeToWindowsServiceViewEvents();
        }

        #region Windows service view events

        /// <summary>
        /// Handles service started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected void OnServiceStarted(object sender, 
                                        ServiceStartedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            if (ProcessCommandLineArguments(e.Args))
            {
                base.WriteApplicationStartedMessage();
                StartAllFileWatchers();
            }
            else // If invalid service arguments.
            {
                throw new ArgumentException(Resources.MessageInvalidArguments,
                                            "e");
            }
        }

        /// <summary>
        /// Handles service stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected void OnServiceStopped(object sender,
                                        ServiceStoppedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            StopAllFileWatchers();
            WaitForControllerStopped();
            base.WriteApplicationStoppedMessage();
            Dispose();
        }

        #endregion

        #region Command line arguments

        /// <summary>
        /// Processes command line arguments.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Returns true if to start main application.</returns>
        private bool ProcessCommandLineArguments(Collection<string> args)
        {
            const int MaxArguments = 2;

            if (args.Count == 0)
            {
                FileWatcherController.SynchronousExecution = true;
                return true;
            }
            if (args.Count > MaxArguments)
            {
                return false;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string item in args)
            {
                stringBuilder.Append(item);
            }

            string arguments = stringBuilder.ToString();

            if (arguments.Length > 9)
            {
                return false;
            }
            if (String.Compare(arguments, @"-a", StringComparison.Ordinal) == 0)
            {
                return true;
            }
            if (Regex.IsMatch(arguments, @"((-a-b:){1,1}([1-9]){1,2}){1,1}") && 
                args.Count == 2)
            {
                int batchSize = Convert.ToInt32(args[1].Substring(3), CultureInfo.InvariantCulture);
                FileWatcherController.ProcessBatchSize = batchSize;
                return true;
            }

            return false;
        }

        #endregion Command line arguments

        #region File watcher controller events

        /// <summary>
        /// Handles controller stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnControllerStopped(object sender, EventArgs e)
        {
            base.OnControllerStopped(sender, e);
            // Set exit.
            _canExit.Set();
        }

        /// <summary>
        /// Handles file watcher started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnFileWatcherStarted(object sender,
                                                     FileWatcherStartedEventArgs e)
        {
            base.OnFileWatcherStarted(sender, e);
            // Set can stop all.
            _canStopAll.Set();
        }

        /// <summary>
        /// Handles file watcher path error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnFileWatcherPathError(object sender,
                                                       FileWatcherPathErrorEventArgs e)
        {
            base.OnFileWatcherPathError(sender, e);
            // Set can stop all. Avoid deadlock if all file watchers have invalid path.
            _canStopAll.Set();
        }

        #endregion

        /// <summary>
        /// Subscribes to all windows service view events.
        /// </summary>
        private void SubscribeToWindowsServiceViewEvents()
        {
            _windowsServiceView.ServiceStarted +=
                new EventHandler<ServiceStartedEventArgs>(OnServiceStarted);

            _windowsServiceView.ServiceStopped +=
                new EventHandler<ServiceStoppedEventArgs>(OnServiceStopped);
        }

        /// <summary>
        /// Starts file watchers.
        /// </summary>
        private void StartAllFileWatchers()
        {
            FileWatcherController.StartAllFileWatchers();
        }

        /// <summary>
        /// Stops file watchers.
        /// </summary>
        private void StopAllFileWatchers()
        {
            // Avoid deadlock if all file watchers have invalid path by checking IsActive.
            while (!FileWatcherController.CanStopAllFileWatchers() &&
                   FileWatcherController.IsActive())
            {
                // Wait for starting file watchers.
                _canStopAll.WaitOne();
            }
            if (FileWatcherController.CanStopAllFileWatchers())
            {
                FileWatcherController.StopAllFileWatchers();
            }
        }

        /// <summary>
        /// Waits for controller to stop.
        /// </summary>
        private void WaitForControllerStopped()
        {
            // Wait for file watcher controller stopped event.
            _canExit.WaitOne();
        }

        /// <summary>
        /// Auto reset event for can stop all.
        /// </summary>
        private readonly AutoResetEvent _canStopAll = new AutoResetEvent(false);

        /// <summary>
        /// Auto reset event for can exit.
        /// </summary>
        private readonly AutoResetEvent _canExit = new AutoResetEvent(false);

        /// <summary>
        /// Windows service view.
        /// </summary>
        private readonly IWindowsServiceView _windowsServiceView;
    }
}