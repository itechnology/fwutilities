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
using System.Timers;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using FileWatcherUtilities.Logger;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides main view presenter.
    /// </summary>
    public class MainViewPresenter : MainViewPresenterBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewPresenter class.
        /// </summary>
        /// <param name="mainView">Main view.</param>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <param name="viewUpdateInterval">View update interval.</param>
        /// <exception cref="ArgumentNullException">mainView is null.</exception>
        /// <exception cref="ArgumentNullException">fileWatcherController is null.</exception>
        /// <exception cref="ArgumentNullException">logger is null.</exception>
        /// <exception cref="ArgumentNullException">formatter is null.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        public MainViewPresenter(IMainView mainView,
                                 FileWatcherController fileWatcherController,
                                 ILogger logger,
                                 IFormatter formatter,
                                 string xmlConfigFilePath,
                                 string xmlSchemaConfigFilePath,
                                 double viewUpdateInterval)
            : base(fileWatcherController, logger, formatter)
        {
            if (mainView == null)
            {
                throw new ArgumentNullException("mainView",
                                                Resources.ArgumentNullException);
            }
            if (fileWatcherController == null)
            {
                throw new ArgumentNullException("fileWatcherController",
                                                Resources.ArgumentNullException);
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger",
                                                Resources.ArgumentNullException);
            }
            if (formatter == null)
            {
                throw new ArgumentNullException("formatter",
                                                Resources.ArgumentNullException);
            }
            if (xmlConfigFilePath == null)
            {
                throw new ArgumentNullException("xmlConfigFilePath",
                                                Resources.ArgumentNullException);
            }
            if (xmlSchemaConfigFilePath == null)
            {
                throw new ArgumentNullException("xmlSchemaConfigFilePath",
                                                Resources.ArgumentNullException);
            }
            _mainView = mainView;
            _xmlConfigFilePath = xmlConfigFilePath;
            _xmlSchemaConfigFilePath = xmlSchemaConfigFilePath;

            // Check if view interval is less than default value.
            if (viewUpdateInterval < 100)
            {
                _updateTimer.Interval = 100;
            }
            else
            {
                _updateTimer.Interval = viewUpdateInterval;
            }

            // Subscribe to timer event. (updates view).
            _updateTimer.Elapsed += new ElapsedEventHandler(OnElapsed);

            // Set synchronization context for running events in main thread.
            SetSynchronizationContext();

            SubscribeToMainViewEvents();
            SetFileWatcherSortedDictionary();

            // Write message to log.
            base.WriteApplicationStartedMessage();
        }

        /// <summary>
        /// Gets or sets selected daemon.
        /// </summary>
        private string SelectedDaemon
        {
            get
            {
                return _internalSelectedDaemon;
            }
            set
            {
                _internalSelectedDaemon = value;
            }
        }

        /// <summary>
        /// Sets synchronization context to current thread.
        /// </summary>
        private void SetSynchronizationContext()
        {
            _synchronizationContext = SynchronizationContext.Current;

            if (_synchronizationContext == null)
            {
                _synchronizationContext = new SynchronizationContext();
            }
        }

        /// <summary>
        /// Subscribes to main view events.
        /// </summary>
        private void SubscribeToMainViewEvents()
        {
            _mainView.DaemonSelected +=
                new EventHandler<DaemonSelectedEventArgs>(OnDaemonSelected);

            _mainView.Delete +=
                new EventHandler<EventArgs>(OnDelete);

            _mainView.Exit +=
                new EventHandler<EventArgs>(OnExit);

            _mainView.Start +=
                new EventHandler<EventArgs>(OnStart);

            _mainView.StartAll +=
                new EventHandler<EventArgs>(OnStartAll);

            _mainView.Stop +=
                new EventHandler<EventArgs>(OnStop);

            _mainView.StopAll +=
                new EventHandler<EventArgs>(OnStopAll);

            _mainView.ListInitialize +=
                new EventHandler<EventArgs>(OnListInitialize);
        }

        /// <summary>
        /// Handles list initialize event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnListInitialize(object sender,
                                      EventArgs e)
        {
            // Force update.
            UpdateList(true);
        }

        /// <summary>
        /// Handles stop all event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnStopAll(object sender,
                               EventArgs e)
        {
            StopAll();
        }

        /// <summary>
        /// Handles stop event event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnStop(object sender,
                            EventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// Handles start all event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnStartAll(object sender,
                                EventArgs e)
        {
            StartAll();
        }

        /// <summary>
        /// Handles start event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnStart(object sender,
                             EventArgs e)
        {
            Start();
        }

        /// <summary>
        /// Handles exit event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnExit(object sender,
                            EventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// Handles delete event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnDelete(object sender,
                              EventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// Handles daemon selected event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnDaemonSelected(object sender,
                                      DaemonSelectedEventArgs e)
        {
            SelectedDaemon = e.DaemonName;
            EnableDisableControls();
        }

        /// <summary>
        /// Performs start all.
        /// </summary>
        private void StartAll()
        {
            FileWatcherController.StartAllFileWatchers();
            EnableDisableControls();
        }

        /// <summary>
        /// Performs stop all.
        /// </summary>
        private void StopAll()
        {
            FileWatcherController.StopAllFileWatchers();
            EnableDisableControls();
        }

        /// <summary>
        /// Performs start.
        /// </summary>
        private void Start()
        {
            if (SelectedDaemon != null)
            {
                FileWatcherController.StartFileWatcher(SelectedDaemon);
            }
            EnableDisableControls();
        }

        /// <summary>
        /// Performs stop.
        /// </summary>
        private void Stop()
        {
            if (SelectedDaemon != null)
            {
                FileWatcherController.StopFileWatcher(SelectedDaemon);
            }
            EnableDisableControls();
        }

        /// <summary>
        /// Performs delete.
        /// </summary>
        private void Delete()
        {
            if (SelectedDaemon != null)
            {
                // Remove before refreshing the selected daemon.
                _fileWatcherSortedDictionary.Remove(SelectedDaemon);
                // This will refresh the selected daemon.
                FileWatcherController.RemoveFileWatcher(SelectedDaemon);

                _mainView.UpdateList(_fileWatcherSortedDictionary);

                // Save configuration changes.
                XmlConfigurationSaver.SaveConfigurationSets(FileWatcherController.FileWatcherConfiguration,
                                                            _xmlConfigFilePath, _xmlSchemaConfigFilePath);

                EnableDisableControls();
            }
        }

        /// <summary>
        /// Performs exit.
        /// </summary>
        private void Exit()
        {
            // Write message to log.
            base.WriteApplicationStoppedMessage();
            Dispose();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">True if disposing.</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_updateTimer != null)
                {
                    _updateTimer.Dispose();
                    _updateTimer = null;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        #region Controller event handlers

        /// <summary>
        /// Handles system changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnSystemChanged(object sender,
                                                FileWatcherEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnSystemChanged(sender, e);
            _fileWatcherSortedDictionary[e.ConfigurationKeyValuePair.Key].LastEventType =
                e.ChangeType;
            _fileWatcherSortedDictionary[e.ConfigurationKeyValuePair.Key].LastEventTime =
                e.DateTime.ToLocalTime().ToString(CultureInfo.CurrentCulture);
            IncreaseEventCount(e.ConfigurationKeyValuePair.Key);
            UpdateList(false);
        }

        /// <summary>
        /// Handles buffer error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnBufferError(object sender,
                                              FileWatcherBufferErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }
            
            base.OnBufferError(sender, e);
            IncreaseErrorCount(e.DaemonName);
            UpdateList(false);
        }

        /// <summary>
        /// Handles file watcher configuration changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnConfigurationChanged(object sender,
                                                       EventArgs e)
        {
            // Update changes.
            SetFileWatcherSortedDictionary();
            UpdateList(false);
            EnableDisableControls();
        }

        /// <summary>
        /// Handles process started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnProcessStarted(object sender,
                                                 ControllerProcessStartedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnProcessStarted(sender, e);
            UpdateRunningProcesses();
            UpdateProcessesToRun(e.QueuedProcesses);
        }

        /// <summary>
        /// Handles process exited event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnProcessExited(object sender,
                                                ProcessExitEventArgs e)
        {
            base.OnProcessExited(sender, e);
            UpdateRunningProcesses();
        }

        /// <summary>
        /// Handles process error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnProcessError(object sender,
                                               ProcessErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnProcessError(sender, e);
            IncreaseErrorCount(e.DaemonName);
            UpdateList(false);
            UpdateProcessesToRun(e.QueuedProcesses);
            UpdateRunningProcesses();
        }

        /// <summary>
        /// Handles file watcher started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherStarted(object sender,
                                                     FileWatcherStartedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherStarted(sender, e);
            SetStartedState(e.DaemonName);
            UpdateList(false);
            UpdateRunningFileWatchers();
        }

        /// <summary>
        /// Handles file watcher starting event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherStarting(object sender,
                                                      FileWatcherStartingEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherStarting(sender, e);
            SetStartingState(e.DaemonName);
            UpdateList(false);
        }

        /// <summary>
        /// Handles file watcher stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherStopped(object sender,
                                                     FileWatcherStoppedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherStopped(sender, e);
            ResetInfo(e.DaemonName);
            UpdateList(false);
            UpdateRunningFileWatchers();
        }

        /// <summary>
        /// Handles controller stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnControllerStopped(object sender,
                                                    EventArgs e)
        {
            base.OnControllerStopped(sender, e);
            ResetFileWatcherSorterDictionary();
            // Set process to run to zero.
            UpdateProcessesToRun(0);
            UpdateList(false);
            EnableDisableControls();
        }

        /// <summary>
        /// Handles file watcher searcher error.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherSearchError(object sender,
                                                         FileWatcherSearchErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherSearchError(sender, e);
            IncreaseErrorCount(e.DaemonName);
            UpdateList(false);
        }

        /// <summary>
        /// Handles file watcher path error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherPathError(object sender,
                                                       FileWatcherPathErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherPathError(sender, e);
            _mainView.ShowFileWatcherPathErrorMessage(String.Format(CultureInfo.CurrentCulture,
                                                                   @Resources.MessageDaemonPathErrorWinForms,
                                                                   @e.DaemonName,
                                                                   @e.Path));
        }

        /// <summary>
        /// Handles service error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnServiceError(object sender,
                                               ServiceErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnServiceError(sender, e);
            IncreaseErrorCount(e.DaemonName);
            UpdateList(false);
            UpdateProcessesToRun(e.QueuedProcesses);
            UpdateRunningProcesses();
        }

        /// <summary>
        /// Handles service called event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnServiceCalled(object sender,
                                                ServiceCalledEventArgs e)
        {
            base.OnServiceCalled(sender, e);
            UpdateRunningProcesses();
        }

        /// <summary>
        /// Handles service begin called event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnServiceBeginCall(object sender,
                                                   ServiceBeginCallEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnServiceBeginCall(sender, e);
            UpdateRunningProcesses();
            UpdateProcessesToRun(e.QueuedProcesses);
        }

        /// <summary>
        /// Handles service proxy creation error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnServiceProxyCreationError(object sender,
                                                            ServiceProxyCreationErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnServiceProxyCreationError(sender, e);
            IncreaseErrorCount(e.DaemonName);
            UpdateList(false);
            UpdateProcessesToRun(e.QueuedProcesses);
            UpdateRunningProcesses();
        }

        /// <summary>
        /// Handles process canceled event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnProcessCanceled(object sender,
                                                  ProcessCanceledEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnProcessCanceled(sender, e);
            UpdateProcessesToRun(e.QueuedProcesses);
        }

        #endregion Controller event handlers

        /// <summary>
        /// Sets file watcher sorted dictionary from file watcher controller.
        /// </summary>
        private void SetFileWatcherSortedDictionary()
        {
            Dictionary<string, FileWatcherConfigurationSet> tempDictionary =
                FileWatcherController.FileWatcherConfiguration;

            _fileWatcherSortedDictionary = new SortedDictionary<string, FileWatcherInfo>();

            foreach (KeyValuePair<string, FileWatcherConfigurationSet> keyValuePair in tempDictionary)
            {
                FileWatcherInfo fileWatcherInfo = new FileWatcherInfo();
                fileWatcherInfo.Enabled = keyValuePair.Value.StartDaemon;
                _fileWatcherSortedDictionary.Add(keyValuePair.Key, fileWatcherInfo);
            }
        }

        /// <summary>
        /// Resets file watcher info in file watcher sorted dictionary.
        /// </summary>
        private void ResetFileWatcherSorterDictionary()
        {
            foreach (KeyValuePair<string, FileWatcherInfo> keyValuePair in _fileWatcherSortedDictionary)
            {
                keyValuePair.Value.Reset();
            }
        }

        /// <summary>
        /// Updates main view list.
        /// </summary>
        /// <param name="forceUpdate">Force update even if file watcher controller is running.</param>
        private void UpdateList(bool forceUpdate)
        {
            // True if to update.
            _updateInfo = true;

            // If file watcher is active, use delayed update.
            if (FileWatcherController.IsActive())
            {
                if (!_updateTimer.Enabled)
                {
                    _updateTimer.Enabled = true;
                }
                if (forceUpdate)
                {
                    _mainView.UpdateList(_fileWatcherSortedDictionary);
                }
            }
            else
            {
                if (_updateTimer.Enabled)
                {
                    _updateTimer.Enabled = false;
                }
                _mainView.UpdateList(_fileWatcherSortedDictionary);
            }
        }

        /// <summary>
        /// Handles elapsed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnElapsed(object sender,
                               ElapsedEventArgs e)
        {
            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                // True if something to update.
                if (_updateInfo)
                {
                    // Has exit been called?
                    if (_updateTimer != null)
                    {
                        if (!_updateTimer.Enabled)
                        {
                            return;
                        }
                        _mainView.UpdateList(_fileWatcherSortedDictionary);
                    }
                }
                // Reset update info.
                _updateInfo = false;
            }), null);
        }

        /// <summary>
        /// Updates main view running processes.
        /// </summary>
        private void UpdateRunningProcesses()
        {
            _mainView.UpdateRunningProcesses(FileWatcherController.RunningProcesses());
        }

        /// <summary>
        /// Updates main view processes to run.
        /// </summary>
        private void UpdateProcessesToRun(int processesToRun)
        {
            _mainView.UpdateProcessesToRun(processesToRun);
        }

        /// <summary>
        /// Updates main view running file watchers.
        /// </summary>
        private void UpdateRunningFileWatchers()
        {
            _mainView.UpdateRunningFileWatchers(FileWatcherController.RunningFileWatchers);
        }

        /// <summary>
        /// Increases error count of specified daemon.
        /// </summary>
        /// <param name="daemonName">Daemon name.</param>
        private void IncreaseErrorCount(string daemonName)
        {
            // Nice overflow.
            if (_fileWatcherSortedDictionary[daemonName].Errors == Int32.MaxValue)
            {
                _fileWatcherSortedDictionary[daemonName].Errors = Int32.MinValue;
            }
            _fileWatcherSortedDictionary[daemonName].Errors++;
        }

        /// <summary>
        /// Increases event count of specified daemon.
        /// </summary>
        /// <param name="daemonName">Daemon name.</param>
        private void IncreaseEventCount(string daemonName)
        {
            // Nice overflow.
            if (_fileWatcherSortedDictionary[daemonName].Events == Int32.MaxValue)
            {
                _fileWatcherSortedDictionary[daemonName].Events = Int32.MinValue;
            }
            _fileWatcherSortedDictionary[daemonName].Events++;
        }

        /// <summary>
        /// Sets started state for specified daemon.
        /// </summary>
        /// <param name="daemonName">Daemon name.</param>
        private void SetStartedState(string daemonName)
        {
            _fileWatcherSortedDictionary[daemonName].Status = Resources.StateStarted;
        }

        /// <summary>
        /// Sets starting state for specified daemon.
        /// </summary>
        /// <param name="daemonName">Daemon name.</param>
        private void SetStartingState(string daemonName)
        {
            _fileWatcherSortedDictionary[daemonName].Status = Resources.StateStarting;
        }

        /// <summary>
        /// Resets file watcher info.
        /// </summary>
        /// <param name="daemonName">Daemon name.</param>
        private void ResetInfo(string daemonName)
        {
            _fileWatcherSortedDictionary[daemonName].Reset();
        }

        /// <summary>
        /// Enables or disables controls.
        /// </summary>
        private void EnableDisableControls()
        {
            if (SelectedDaemon != null)
            {
                UpdateControls(SelectedDaemon);
            }
            else
            {
                UpdateControls();
            }
        }

        /// <summary>
        /// Enables or disables controls.
        /// </summary>
        /// <param name="selectedDaemon">Selected daemon name.</param>
        private void UpdateControls(string selectedDaemon)
        {
            _mainView.StartEnabled = FileWatcherController.CanStartFileWatcher(selectedDaemon);
            _mainView.StopEnabled = FileWatcherController.CanStopFileWatcher(selectedDaemon);
            _mainView.StartAllEnabled = FileWatcherController.CanStartAllFileWatchers();
            _mainView.StopAllEnabled = FileWatcherController.CanStopAllFileWatchers();
            _mainView.NewEnabled = FileWatcherController.CanAddFileWatcherConfiguration();

            bool canEdit = FileWatcherController.CanEditFileWatcherConfiguration();
            _mainView.DeleteEnabled = canEdit;
            _mainView.PropertiesEnabled = canEdit;
            _mainView.ExitEnabled = !FileWatcherController.IsActive();
        }

        /// <summary>
        /// Enables or disables controls.
        /// </summary>
        private void UpdateControls()
        {
            _mainView.StartEnabled = false;
            _mainView.StopEnabled = false;
            _mainView.DeleteEnabled = false;
            _mainView.PropertiesEnabled = false;

            _mainView.StartAllEnabled = FileWatcherController.CanStartAllFileWatchers();
            _mainView.StopAllEnabled = FileWatcherController.CanStopAllFileWatchers();
            _mainView.NewEnabled = FileWatcherController.CanAddFileWatcherConfiguration();
            _mainView.ExitEnabled = !FileWatcherController.IsActive();
        }

        /// <summary>
        /// Selected daemon name
        /// </summary>
        private string _internalSelectedDaemon;

        /// <summary>
        /// Main view.
        /// </summary>
        private readonly IMainView _mainView;

        /// <summary>
        /// Sorted dictionary of file watcher information.
        /// </summary>
        private SortedDictionary<string, FileWatcherInfo> _fileWatcherSortedDictionary;

        /// <summary>
        /// Contains path of the XML configuration file.
        /// </summary>
        private readonly string _xmlConfigFilePath;

        /// <summary>
        /// Contains path of the XML Schema configuration file.
        /// </summary>
        private readonly string _xmlSchemaConfigFilePath;

        /// <summary>
        /// Update timer.
        /// </summary>
        private System.Timers.Timer _updateTimer = new System.Timers.Timer();

        /// <summary>
        /// True if to update.
        /// </summary>
        private bool _updateInfo;

        /// <summary>
        /// Contains synchronization context for running the events in the main thread.
        /// </summary>
        private SynchronizationContext _synchronizationContext;
    }
}