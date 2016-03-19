/******************************************************************************
*    File Watcher Utilities / File Watcher Controller
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

#if (!_NET_20)
using FileWatcherUtilities.FileWatcherServiceContract;
#endif

using System;
using System.IO;
using System.Text;
using System.Security;
using System.Threading;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Security.Permissions;
using FileWatcherUtilities.Controller.Properties;
using System.Security.Cryptography;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Provides control for a group of configured file watchers.
    /// </summary>
    public sealed class FileWatcherController : IDisposable
    {
        #region Public events

        /// <summary>
        /// Occurs when file watcher is stopped.
        /// </summary>
        public event EventHandler<FileWatcherStoppedEventArgs> FileWatcherStopped;

        /// <summary>
        /// Occurs when file watcher is started.
        /// </summary>
        public event EventHandler<FileWatcherStartedEventArgs> FileWatcherStarted;

        /// <summary>
        /// Occurs when file watcher is starting.
        /// </summary>
        public event EventHandler<FileWatcherStartingEventArgs> FileWatcherStarting;

        /// <summary>
        /// Occurs when process encountered error.
        /// </summary>
        public event EventHandler<ProcessErrorEventArgs> ProcessError;

        /// <summary>
        /// Occurs when process has exited.
        /// </summary>
        public event EventHandler<ProcessExitEventArgs> ProcessExited;

        /// <summary>
        /// Occurs when process has started.
        /// </summary>
        public event EventHandler<ControllerProcessStartedEventArgs> ProcessStarted;

        /// <summary>
        /// Occurs when process has output data.
        /// </summary>
        public event EventHandler<ProcessDataEventArgs> ProcessOutputData;

        /// <summary>
        /// Occurs when process has error data.
        /// </summary>
        public event EventHandler<ProcessDataEventArgs> ProcessErrorData;

        /// <summary>
        /// Occurs when file system has changed.
        /// </summary>
        public event EventHandler<FileWatcherEventArgs> SystemChanged;

        /// <summary>
        /// Occurs when buffer has overflown.
        /// </summary>
        public event EventHandler<FileWatcherBufferErrorEventArgs> BufferError;

        /// <summary>
        /// Occurs when controller has stopped.
        /// </summary>
        public event EventHandler<EventArgs> ControllerStopped;

        /// <summary>
        /// Occurs when file watcher file search encountered an error.
        /// </summary>
        public event EventHandler<FileWatcherSearchErrorEventArgs> FileWatcherSearchError;

        /// <summary>
        /// Occurs when file watcher file search advances to new directory.
        /// </summary>
        public event EventHandler<FileWatcherSearchProgressEventArgs> FileWatcherSearchProgress;

        /// <summary>
        /// Occures when file watcher configuration has changed (added, removed or updated).
        /// </summary>
        public event EventHandler<EventArgs> ConfigurationChanged;

        /// <summary>
        /// Occures when file watcher is stopped due to an invalid path.
        /// </summary>
        public event EventHandler<FileWatcherPathErrorEventArgs> FileWatcherPathError;

#if (_NET_20)
#pragma warning disable 0067
#endif

        /// <summary>
        /// Occures when service is called.
        /// </summary>
        public event EventHandler<ServiceCalledEventArgs> ServiceCalled;

        /// <summary>
        /// Occures when service call fails.
        /// </summary>
        public event EventHandler<ServiceErrorEventArgs> ServiceError;

        /// <summary>
        /// Occures when service proxy creation fails.
        /// </summary>
        public event EventHandler<ServiceProxyCreationErrorEventArgs> ServiceProxyCreationError;

        /// <summary>
        /// Occures when service is about to get called.
        /// </summary>
        public event EventHandler<ServiceBeginCallEventArgs> ServiceBeginCall;

#if (_NET_20)
#pragma warning restore 0067
#endif

        /// <summary>
        /// Occures when process is canceled.
        /// </summary>
        public event EventHandler<ProcessCanceledEventArgs> ProcessCanceled;

        /// <summary>
        /// Occurs when file watcher recycles file system watcher.
        /// </summary>
        public event EventHandler<FileWatcherRecycledEventArgs> FileWatcherRecycle;

        #endregion Public events

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FileWatcherController class. 
        /// </summary>
        /// <param name="configurationDictionary">File watcher configuration.</param>
        /// <exception cref="ArgumentNullException">FileWatcherConfigurationSet is null.</exception>
        public FileWatcherController(Dictionary<string, FileWatcherConfigurationSet> configurationDictionary)
        {
            if (configurationDictionary == null)
            {
                throw new ArgumentNullException("configurationDictionary",
                                                Resources.ArgumentNullException);
            }

            // Check for null values.
            foreach (KeyValuePair<string, FileWatcherConfigurationSet> valueKeyPair in configurationDictionary)
            {
                if (valueKeyPair.Value == null)
                {
                    throw new ArgumentNullException("configurationDictionary",
                                                    Resources.ArgumentNullException);
                }
            }

            _configurationDictionary = configurationDictionary;

            // Set synchronization context for running events in main thread.
            SetSynchronizationContext();

            // Initialize file watchers.
            SetFileWatchers();
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// True if to run and to wait for queued processes when file 
        /// watcher controller is stopping.
        /// </summary>
        public bool RunQueuedProcesses
        {
            get
            {
                CheckDisposed();

                lock (_lockRunQueuedProcesses)
                {
                    return _runQueuedProcesses;
                }
            }
            set
            {
                CheckDisposed();

                lock (_lockRunQueuedProcesses)
                {
                    _runQueuedProcesses = value;
                }
            }
        }

        /// <summary>
        /// Returns number of file watchers loaded on the file watcher controller that are running.
        /// </summary>
        /// <returns>Returns number of file watchers loaded on the file watcher controller that are running.</returns>
        public int RunningFileWatchers
        {
            get
            {
                CheckDisposed();

                int count = 0;

                foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
                {
                    if (fileWatcher.Value.IsRunning)
                    {
                        count++;
                    }
                }
                return count;
            }
        }

        /// <summary>
        /// Gets or sets file watcher controller process batch size. Zero to disable process batch size.
        /// </summary>
        public int ProcessBatchSize
        {
            get
            {
                CheckDisposed();

                lock (_lockProcessBatchSize)
                {
                    return _internalProcessBatchSize;
                }
            }
            set
            {
                CheckDisposed();

                lock (_lockProcessBatchSize)
                {
                    if (value < 0)
                    {
                        _internalProcessBatchSize = 0;
                    }
                    else
                    {
                        _internalProcessBatchSize = value;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets synchronous execution.
        /// </summary>
        /// <exception cref="InvalidOperationException">Synchronous execution cannot be set if file watceher controller is active.</exception>
        public bool SynchronousExecution
        {
            get
            {
                CheckDisposed();

                lock (_lockSynchronousExecution)
                {
                    return _synchronousExecution;
                }
            }
            set
            {
                CheckDisposed();

                if (IsActive())
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionSynchronousExecutionCannotBeSet);
                }
                _synchronousExecution = value;
            }
        }

        #endregion Public properties

        #region Public status

        /// <summary>
        /// Returns number of running processes.
        /// </summary>
        /// <returns>Number of running processes.</returns>
        public int RunningProcesses()
        {
            CheckDisposed();

            lock (_lockExternalRunningProcesses)
            {
                return _externalRunningProcesses;
            }
        }

        /// <summary>
        /// Returns true if file watcher configuration can be added.
        /// </summary>
        /// <returns>True if file watcher configuration can be added.</returns>
        public bool CanAddFileWatcherConfiguration()
        {
            CheckDisposed();

            // If some file watchers are running.
            if (IsRunning())
            {
                return false;
            }
            // If some file watcher is starting.
            if (HasStartingFileWatcher)
            {
                return false;
            }
            // If controller is stopping.
            if (IsControllerStopping)
            {
                return false;
            }
            // If there is file watchers and no file watcher is running/starting yet.
            return true;
        }

        /// <summary>
        /// Returns true if file watcher configuration can be edited.
        /// </summary>
        /// <returns>True if file watcher configuratin can be edited.</returns>
        public bool CanEditFileWatcherConfiguration()
        {
            CheckDisposed();

            // If some file watchers are running.
            if (IsRunning())
            {
                return false;
            }
            // If some file watcher is starting.
            if (HasStartingFileWatcher)
            {
                return false;
            }
            // If no file watchers.
            if (_configurationDictionary.Count == 0)
            {
                return false;
            }
            // If controller is stopping.
            if (IsControllerStopping)
            {
                return false;
            }
            // If there is file watchers and no file watcher is running/starting yet.
            return true;
        }

        /// <summary>
        /// Returns true if the specified file watcher can be started.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <returns>True if the specified file watcher can be started.</returns>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentException">Daemon was not found.</exception>
        public bool CanStartFileWatcher(string daemonName)
        {
            CheckDisposed();

            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }

            FileWatcher tempFileWatcher;

            if (_fileWatchers.TryGetValue(daemonName, out tempFileWatcher))
            {
                if (IsControllerStopping)
                {
                    return false;
                }
                if (tempFileWatcher.IsRunning)
                {
                    return false;
                }
                if (tempFileWatcher.IsStarting)
                {
                    return false;
                }
                return true;
            }
            // If file watcher is disabled.
            if (_configurationDictionary.ContainsKey(daemonName))
            {
                return false;
            }

            // If no daemon was found throw exception.
            throw new ArgumentException(
                Resources.ArgumentExceptionDaemonNotFound);
        }

        /// <summary>
        /// Returns true if specified file watcher can be stopped.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <returns>True if the specified file watcher can be stopped.</returns>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentException">Daemon was not found.</exception>
        public bool CanStopFileWatcher(string daemonName)
        {
            CheckDisposed();

            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }

            FileWatcher tempFileWatcher;

            if (_fileWatchers.TryGetValue(daemonName, out tempFileWatcher))
            {
                if (IsControllerStopping)
                {
                    return false;
                }
                return tempFileWatcher.IsRunning;
            }
            // If file watcher is disabled.
            if (_configurationDictionary.ContainsKey(daemonName))
            {
                return false;
            }

            // If no daemon was found throw exception.
            throw new ArgumentException(
                Resources.ArgumentExceptionDaemonNotFound);
        }

        /// <summary>
        /// Returns true if start all file watchers can be called.
        /// </summary>
        /// <returns>True if start all file watcher can be called.</returns>
        public bool CanStartAllFileWatchers()
        {
            CheckDisposed();

            // If all file watchers are running or starting.
            if (IsAllRunning())
            {
                return false;
            }
            // If all file watchers are starting.
            if (IsAllStartingFileWatchers)
            {
                return false;
            }
            // If no enabled file watchers or no file watcher at all.
            if (_fileWatchers.Count == 0)
            {
                return false;
            }
            // If controller is stopping.
            if (IsControllerStopping)
            {
                return false;
            }
            // If there is enabled file watcher and some or none of the file watcher is running/starting yet.
            return true;
        }

        /// <summary>
        /// Returns true if stop all file watchers can be called.
        /// </summary>
        /// <returns>True if stop all file watcher can be called.</returns>
        public bool CanStopAllFileWatchers()
        {
            CheckDisposed();

            // If some file watchers are starting.
            if (HasStartingFileWatcher)
            {
                return false;
            }
            // If no file watcher is running.
            if (!IsRunning())
            {
                return false;
            }
            // If controller is stopping.
            if (IsControllerStopping)
            {
                return false;
            }
            // If there is enabled file watcher that is running.
            return true;
        }

        /// <summary>
        /// Returns true if controller is performing any actions.
        /// </summary>
        /// <returns>True if controller is performing any actions.</returns>
        public bool IsActive()
        {
            CheckDisposed();

            if (IsRunning())
            {
                return true;
            }
            if (HasStartingFileWatcher)
            {
                return true;
            }
            if (IsControllerStopping)
            {
                return true;
            }
            return false;
        }

        #endregion Public status

        /// <summary>
        /// Adds file watcher to the controller.
        /// </summary>
        /// <param name="configurationKeyValuePair">File watcher configuration of the added file watcher.</param>
        /// <exception cref="InvalidOperationException">Configuration cannot be added.</exception>       
        /// <exception cref="ArgumentNullException">configurationKeyValuePair is null.</exception>
        /// <exception cref="ArgumentException">Invalid file watcher configuration.</exception>       
        public void AddFileWatcher(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair)
        {
            CheckDisposed();

            if (configurationKeyValuePair.Value == null)
            {
                throw new ArgumentNullException("configurationKeyValuePair",
                                                Resources.ArgumentNullException);
            }
            if (!CanAddFileWatcherConfiguration())
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionConfigurationCannotBeAdded);
            }

            // Check configuration.
            CheckFileWatcherConfiguration(configurationKeyValuePair, false);

            // Add configuration.
            _configurationDictionary.Add(configurationKeyValuePair.Key, configurationKeyValuePair.Value);

            // Initialize file watchers.
            SetFileWatchers();

            // Raise configuration changed event.
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Updates file watcher configuration.
        /// </summary>
        /// <param name="originalDaemonName">Original daemon name.</param>
        /// <param name="configurationKeyValuePair">New configuration.</param>
        /// <exception cref="InvalidOperationException">Configuration cannot be updated.</exception>       
        /// <exception cref="ArgumentNullException">originalDaemoName is null.</exception>
        /// <exception cref="ArgumentNullException">configurationKeyValuePair is null.</exception>
        /// <exception cref="ArgumentException">Invalid file watcher configuration.</exception>      
        public void UpdateFileWatcher(string originalDaemonName,
                                      KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair)
        {
            CheckDisposed();

            if (originalDaemonName == null)
            {
                throw new ArgumentNullException("originalDaemonName",
                                                Resources.ArgumentNullException);
            }
            if (configurationKeyValuePair.Value == null)
            {
                throw new ArgumentNullException("configurationKeyValuePair",
                                                Resources.ArgumentNullException);
            }
            if (!CanAddFileWatcherConfiguration() || !CanEditFileWatcherConfiguration())
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionConfigurationCannotBeUpdated);
            }

            // Check configuration.
            CheckFileWatcherConfiguration(configurationKeyValuePair, true);
            // Remove old configuration.
            RemoveFileWatcher(originalDaemonName);
            // Add new configuration.
            AddFileWatcher(configurationKeyValuePair);

            // Raise configuration changed event.
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Checks file watcher configuration.
        /// </summary>
        /// <param name="configurationKeyValuePair">File watcher configuration to check.</param>
        /// <param name="update">True if to check updated file watcher configuration.</param>
        /// <exception cref="ArgumentException">Invalid file watcher configuration.</exception>
        private void CheckFileWatcherConfiguration(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair,
                                                   bool update)
        {
            // If update, the daemon name can be the same.
            if (!update)
            {
                // Check if file watcher name is unique.
                if (_configurationDictionary.ContainsKey(configurationKeyValuePair.Key))
                {
                    throw new ArgumentException(
                        Resources.ArgumentExceptionDaemonNameIsNotUnique);
                }
            }
            // Check file watcher directory.
            if (!Directory.Exists(configurationKeyValuePair.Value.Path))
            {
                throw new ArgumentException(
                    Resources.ArgumentExceptionPathDoesNotExist);
            }
            // Check process configuration if start process is selected.
            if (configurationKeyValuePair.Value.StartProcess)
            {
                // If working directory does not exsist.
                if (!Directory.Exists(configurationKeyValuePair.Value.ProcessWorkingDirectory))
                {
                    throw new ArgumentException(
                        Resources.ArgumentExceptionWorkingDirectoryDoesNotExist);
                }
                // Process file name may not be empty.
                if (String.IsNullOrEmpty(configurationKeyValuePair.Value.ProcessFileName))
                {
                    throw new ArgumentException(
                        Resources.ArgumentExceptionFileNameIsEmpty);
                }
                // If use shell execute is selected.
                if (configurationKeyValuePair.Value.ProcessUseShellExecute)
                {
                    // Redirect standard output must be disabled.
                    if (configurationKeyValuePair.Value.ProcessRedirectStandardOutput)
                    {
                        throw new ArgumentException(
                            Resources.ArgumentExceptionRedirectOutputAndShellExecuteSeleted);
                    }
                    // Redirect standard error must be disabled.
                    if (configurationKeyValuePair.Value.ProcessRedirectStandardError)
                    {
                        throw new ArgumentException(
                            Resources.ArgumentExceptionRedirectErrorAndShellExecuteSeleted);
                    }
                    // User name must be empty.
                    if (!String.IsNullOrEmpty(configurationKeyValuePair.Value.ProcessUserName))
                    {
                        throw new ArgumentException(
                            Resources.ArgumentExceptionUserNameAndShellExecute);
                    }
                    // User password must be empty.
                    if (!String.IsNullOrEmpty(configurationKeyValuePair.Value.ProcessUserName))
                    {
                        throw new ArgumentException(
                            Resources.ArgumentExceptionPasswordAndShellExecute);
                    }
                }
            }
        }

        /// <summary>
        /// Removes file watcher configuration from the controller.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher configuration to remove.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="InvalidOperationException">Configuration cannot be removed.</exception>    
        /// <exception cref="ArgumentException">Daemon was not found.</exception>
        public void RemoveFileWatcher(string daemonName)
        {
            CheckDisposed();

            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (!CanEditFileWatcherConfiguration())
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionConfigurationCannotBeRemoved);
            }
            if (_configurationDictionary.ContainsKey(daemonName))
            {
                _configurationDictionary.Remove(daemonName);
                // Initialize file watchers.
                SetFileWatchers();
            }
            else
            {
                // If no daemon was found throw exception.
                throw new ArgumentException(
                    Resources.ArgumentExceptionDaemonNotFound);
            }
            // Raise configuration changed event.
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets copy of file watcher configuration.
        /// </summary>
        public Dictionary<string, FileWatcherConfigurationSet> FileWatcherConfiguration
        {
            get
            {
                CheckDisposed();

                Dictionary<string, FileWatcherConfigurationSet> tempDictionary =
                    new Dictionary<string, FileWatcherConfigurationSet>();

                // Copy key and value.
                foreach (KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair in _configurationDictionary)
                {
                    tempDictionary.Add(configurationKeyValuePair.Key, configurationKeyValuePair.Value);
                }
                return tempDictionary;
            }
        }

        /// <summary>
        /// Sets queue trim interval in minutes.
        /// </summary>
        /// <param name="intervalInMinutes">Queue trim interval in minutes.</param>
        /// <exception cref="ArgumentException">Queue trim interval cannot be negative.</exception>
        public void SetQueueTrimInterval(int intervalInMinutes)
        {
            CheckDisposed();

            if (intervalInMinutes < 0)
            {
                throw new ArgumentException(
                    Resources.ArgumentExceptionQueueTrimIntervalCannotBeNegative);
            }
            lock (_lockQueueTrimInterval)
            {
                _queueTrimInterval = intervalInMinutes;
            }
        }

        #region Start/Stop file watchers

        /// <summary>
        /// Starts the specified file watcher.
        /// </summary>
        /// <param name="daemonName">Name of the daemon to start.</param>
        /// <exception cref="InvalidOperationException">File watcher controller is stopping.</exception>
        /// <exception cref="InvalidOperationException">File watcher is already started.</exception>
        /// <exception cref="InvalidOperationException">File watcher is already starting.</exception>
        /// <exception cref="ArgumentException">Daemon not found.</exception>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="OutOfMemoryException">No memory.</exception>
        public void StartFileWatcher(string daemonName)
        {
            CheckDisposed();

            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (IsControllerStopping)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatcherControllerIsStopping);
            }

            FileWatcher tempFileWatcher;

            if (_fileWatchers.TryGetValue(daemonName, out tempFileWatcher))
            {
                if (!IsRunning())
                {
                    // Turn running state on.
                    SetRunningOn();

                    // Start process thread.
                    StartProcessThread();
                }
                // Raise file watcher path error event if starting of the file watcher is canceled.
                if (!tempFileWatcher.StartFileWatcher())
                {
                    EventHandler<FileWatcherPathErrorEventArgs> handler = FileWatcherPathError;
                    if (handler != null)
                    {
                        handler(this,
                                new FileWatcherPathErrorEventArgs(tempFileWatcher.DaemonName,
                                                                  _configurationDictionary[daemonName].Path));
                    }
                }
            }
            else
            {
                // If no daemon was found throw exception.
                throw new ArgumentException(
                    Resources.ArgumentExceptionDaemonNotFound);
            }
            // Starting file watcher may canceled due to invalid path at runtime.
            // So if no running or starting file watchers then clean up.
            if (!HasRunningFileWatcher())
            {
                // Performs clean stop of controller.
                CleanStopController();
            }
        }

        /// <summary>
        /// Stops the specified file watcher.
        /// </summary>
        /// <param name="daemonName">Name of the daemon to stop.</param>
        /// <exception cref="InvalidOperationException">File watcher controller is stopping.</exception>
        /// <exception cref="InvalidOperationException">File watcher is already stopped.</exception>
        /// <exception cref="InvalidOperationException">File watcher is starting.</exception>
        /// <exception cref="ArgumentException">Daemon not found.</exception>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="OutOfMemoryException">No memory.</exception>
        public void StopFileWatcher(string daemonName)
        {
            CheckDisposed();

            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (IsControllerStopping)
            {
                throw new InvalidOperationException(
                   Resources.InvalidOperationExceptionFileWatcherControllerIsStopping);
            }

            FileWatcher tempFileWatcher;

            if (_fileWatchers.TryGetValue(daemonName, out tempFileWatcher))
            {
                tempFileWatcher.StopFileWatcher();
            }
            else
            {
                // If no daemon was found throw exception.
                throw new ArgumentException(
                    Resources.ArgumentExceptionDaemonNotFound);
            }
            // if no running or starting file watchers then clean up.
            if (!HasRunningFileWatcher())
            {
                // Performs clean stop of controller.
                CleanStopController();
            }
        }

        /// <summary>
        /// Starts all file watchers.
        /// </summary>
        /// <exception cref="InvalidOperationException">File watcher controller is stopping.</exception>
        /// <exception cref="InvalidOperationException">No enabled file watchers.</exception>
        /// <exception cref="OutOfMemoryException">No memory.</exception>
        public void StartAllFileWatchers()
        {
            CheckDisposed();

            if (IsControllerStopping)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatcherControllerIsStopping);
            }
            if (_fileWatchers.Count == 0)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionNoEnabledFileWatchers);
            }

            foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
            {
                if (!fileWatcher.Value.IsRunning)
                {
                    if (!IsRunning())
                    {
                        // Turn running state on.
                        SetRunningOn();

                        // Start process thread.
                        StartProcessThread();
                    }
                    if (!fileWatcher.Value.IsStarting)
                    {
                        // Raise file watcher path error event if starting of the file watcher is canceled.
                        if (!fileWatcher.Value.StartFileWatcher())
                        {
                            EventHandler<FileWatcherPathErrorEventArgs> handler = FileWatcherPathError;
                            if (handler != null)
                            {
                                handler(this,
                                    new FileWatcherPathErrorEventArgs(fileWatcher.Key,
                                                                      _configurationDictionary[fileWatcher.Key].Path));
                            }
                        }
                    }
                }
            }
            // Starting file watcher may canceled due to invalid path at runtime.
            // So if no running or starting file watchers then clean up.
            if (!HasRunningFileWatcher())
            {
                // Performs clean stop of controller.
                CleanStopController();
            }
        }

        /// <summary>
        /// Stops all file watchers.
        /// </summary>
        /// <exception cref="InvalidOperationException">File watchers are already stopped.</exception>
        /// <exception cref="InvalidOperationException">File watcher controller is stopping.</exception>
        /// <exception cref="InvalidOperationException">File watcher is starting.</exception>
        /// <exception cref="OutOfMemoryException">No memory.</exception>
        public void StopAllFileWatchers()
        {
            CheckDisposed();

            if (IsControllerStopping)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatcherControllerIsStopping);
            }
            // If file watchers are already stopped.
            if (!IsRunning())
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatchersAlreadyStopped);
            }

            foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
            {
                // Check if file watcher is starting.
                if (fileWatcher.Value.IsStarting)
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionFileWatchersIsStarting);
                }
                if (fileWatcher.Value.IsRunning)
                {
                    fileWatcher.Value.StopFileWatcher();
                }
            }

            // Performs clean stop of controller.
            CleanStopController();
        }

        #endregion Start/Stop file watchers

        #region IDisposable

        /// <summary>
        /// Implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">True if disposing.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    // Clean up.
                    if (IsActive())
                    {
                        // If controller is stopping wait for it.
                        if (IsControllerStopping)
                        {
                            // Wait if this is still running?
                            while (IsControllerStopping)
                            {
                                Thread.Sleep(50);
                            }
                        }
                        else
                        {
                            // Wait until can stop all.
                            while (!CanStopAllFileWatchers())
                            {
                                Thread.Sleep(50);
                            }
                            // Stop running file watchers.
                            StopAllFileWatchers();
                            // Wait until stopped.
                            while (IsControllerStopping)
                            {
                                Thread.Sleep(50);
                            }
                        }
                    }

                    // Dispose wait handles.
                    _processThreadAutoResetEvent.Close();
                    _syncProcessAutoResetEvent.Close();
                    _processQueueDequeuedAutoResetEvent.Close();
                    _waitForProcessesAutoResetEvent.Close();

                    foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
                    {
                        if (fileWatcher.Value != null)
                        {
                            fileWatcher.Value.Dispose();
                        }
                    }

                    // Set disposed to true.
                    _disposed = true;
                }
            }
        }

        #endregion IDisposable

        #region Framework specific code

        /// <summary>
        /// Start the process or calls service synchronized with other processes.
        /// </summary>
        /// <param name="e">File watcher event args for running a process.</param>
        private void StartProcessSynchronizedHelper(FileWatcherEventArgs e)
        {
#if (!_NET_20)
            // If to call service.
            if (e.ConfigurationKeyValuePair.Value.CallService)
            {
                CallServiceSynchronized(e);
            }
            else
            {
#endif
                StartProcess(e);
#if (!_NET_20)
            }
#endif
        }

        /// <summary>
        /// Runs synchronous or asynchronous process.
        /// </summary>
        /// <param name="process">Process to run.</param>
        private void RunProcessHelper(FileWatcherEventArgs process)
        {
#if (!_NET_20)
            // If to call service.
            if (process.ConfigurationKeyValuePair.Value.CallService)
            {
                BeginCallService(process);
            }
            else
            {
#endif
                StartProcess(process);
#if (!_NET_20)
            }
#endif
        }

#if (!_NET_20)

        /// <summary>
        /// Builds service request.
        /// </summary>
        /// <param name="e">File watcher event args for calling a service.</param>
        /// <param name="checksum">File checksum.</param>
        /// <returns>Service request.</returns>
        private static SystemChangedReqDC CreateRequest(FileWatcherEventArgs e,
                                                        string checksum)
        {
            SystemChangedReqDC request = new SystemChangedReqDC();
            request.ChangeType = e.ChangeType;
            request.DaemonName = e.ConfigurationKeyValuePair.Key;
            request.DateTime = e.DateTime;
            request.FileName = e.FileName;
            request.FullPath = e.FullPath;
            request.Id = e.Id;
            request.OldFullPath = e.OldFullPath;
            request.MachineName = Environment.MachineName;
            request.Checksum = checksum;
            return request;
        }

        /// <summary>
        /// Begins calling of service.
        /// </summary>
        /// <param name="e">File watcher event args for calling a service.</param>
        private void BeginCallService(FileWatcherEventArgs e)
        {
            // Turn on process running for synchronization of processes.
            SetProcessRunningOn();
            // Increase external process counter.
            SetExternalProcessRunningOn();

            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ServiceBeginCallEventArgs> handler = ServiceBeginCall;
                if (handler != null)
                {
                    handler(this,
                            new ServiceBeginCallEventArgs(e.ConfigurationKeyValuePair.Key,
                                                          GetProcessQueueCount(),
                                                          e.Id));
                }
            }), null);

            CallServiceDelegate callServiceDelegate = new CallServiceDelegate(CallService);
            callServiceDelegate.BeginInvoke(e, null, null);
        }

        /// <summary>
        /// Creates file watcher service proxy or return null if creation fails.
        /// </summary>
        /// <param name="endPointConfigurationName">Proxy endpoint configuration name.</param>
        /// <returns>FileWatcherServiceProxy or null, if creation of the proxy fails.</returns>
        private static FileWatcherServiceProxy CreateProxy(string endPointConfigurationName)
        {
            try
            {
                return new FileWatcherServiceProxy(endPointConfigurationName);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Creates file watcher streaming service proxy or return null if creation fails.
        /// </summary>
        /// <param name="endPointConfigurationName">Proxy endpoint configuration name.</param>
        /// <returns>FileWatcherStreamingServiceProxy or null, if creation of the proxy fails.</returns>
        private static FileWatcherStreamingServiceProxy CreateStreamingProxy(string endPointConfigurationName)
        {
            try
            {
                return new FileWatcherStreamingServiceProxy(endPointConfigurationName);
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Call service specified in file watcher event args.
        /// </summary>
        /// <param name="e">File watcher event args for calling a service.</param>
        private void CallService(FileWatcherEventArgs e)
        {
            FileWatcherStreamingServiceProxy fileWatcherStreamingServiceProxy = null;
            FileWatcherServiceProxy fileWatcherServiceProxy = null;

            if (e.ConfigurationKeyValuePair.Value.StreamFile)
            {
                fileWatcherStreamingServiceProxy = CreateStreamingProxy(e.ConfigurationKeyValuePair.Key);
            }
            else
            {
                fileWatcherServiceProxy = CreateProxy(e.ConfigurationKeyValuePair.Key);
            }
            // If failed to a create proxy.
            if (fileWatcherStreamingServiceProxy == null &&
                fileWatcherServiceProxy == null)
            {
                // Decrease external process counter.
                SetExternalProcessRunningOff();

                // Run threaded event in main thread.
                _synchronizationContext.Send(new SendOrPostCallback(delegate
                {
                    EventHandler<ServiceProxyCreationErrorEventArgs> handler = ServiceProxyCreationError;
                    if (handler != null)
                    {
                        handler(this,
                                new ServiceProxyCreationErrorEventArgs(e.ConfigurationKeyValuePair.Key,
                                                                       e.Id,
                                                                       GetProcessQueueCount()));
                    }
                }), null);

                // Remove available process from the file watcher.
                RemoveAvailableProcess(e.ConfigurationKeyValuePair.Key);

                // Decrease file watcher process count.
                RemoveProcessFromProcessBatchSize(e.ConfigurationKeyValuePair.Key);

                SetProcessRunningOff();

                return;
            }

            try
            {
                IResponse response;
                string checksum = CalculateMD5Sum(e.FullPath);

                if (e.ConfigurationKeyValuePair.Value.StreamFile)
                {
                    using (Stream fileStream = File.OpenRead(e.FullPath))
                    {
                        response = new SystemChangedRespMC();

                        response.Message =
                            fileWatcherStreamingServiceProxy.SystemChangedStreaming(e.ChangeType,
                                                                                    checksum,
                                                                                    e.ConfigurationKeyValuePair.Key,
                                                                                    e.DateTime,
                                                                                    e.FileName,
                                                                                    fileStream.Length,
                                                                                    e.FullPath,
                                                                                    e.Id,
                                                                                    Environment.MachineName,
                                                                                    e.OldFullPath,
                                                                                    fileStream);
                    }

                    // Close proxy.
                    fileWatcherStreamingServiceProxy.Close();
                }
                else
                {
                    response = fileWatcherServiceProxy.SystemChanged(CreateRequest(e, checksum));

                    // Close proxy.
                    fileWatcherServiceProxy.Close();
                }

                // Decrease external process counter.
                SetExternalProcessRunningOff();

                // Check for null.
                if (response == null)
                {
                    // Run threaded event in main thread.
                    _synchronizationContext.Send(new SendOrPostCallback(delegate
                    {
                        EventHandler<ServiceCalledEventArgs> handler = ServiceCalled;
                        if (handler != null)
                        {
                            handler(this,
                                    new ServiceCalledEventArgs(e.ConfigurationKeyValuePair.Key,
                                                               String.Empty,
                                                               e.Id));
                        }
                    }), null);
                }
                else
                {
                    // Run threaded event in main thread.
                    _synchronizationContext.Send(new SendOrPostCallback(delegate
                    {
                        EventHandler<ServiceCalledEventArgs> handler = ServiceCalled;
                        if (handler != null)
                        {
                            handler(this,
                                    new ServiceCalledEventArgs(e.ConfigurationKeyValuePair.Key,
                                                               response.Message,
                                                               e.Id));
                        }
                    }), null);
                }
            }
            catch (Exception ex)
            {
                // Abort proxy.
                if (e.ConfigurationKeyValuePair.Value.StreamFile)
                {
                    fileWatcherStreamingServiceProxy.Abort();
                }
                else
                {
                    fileWatcherServiceProxy.Abort();
                }

                // Decrease external process counter.
                SetExternalProcessRunningOff();

                // Run threaded event in main thread.
                _synchronizationContext.Send(new SendOrPostCallback(delegate
                {
                    EventHandler<ServiceErrorEventArgs> handler = ServiceError;
                    if (handler != null)
                    {
                        handler(this,
                                new ServiceErrorEventArgs(e.ConfigurationKeyValuePair.Key,
                                                          ex,
                                                          e.Id,
                                                          GetProcessQueueCount()));
                    }
                }), null);
            }
            finally
            {
                // Remove available process from the file watcher.
                RemoveAvailableProcess(e.ConfigurationKeyValuePair.Key);

                // Decrease file watcher process count.
                RemoveProcessFromProcessBatchSize(e.ConfigurationKeyValuePair.Key);

                SetProcessRunningOff();
            }
        }

        /// <summary>
        /// Call service specified in file watcher event args.
        /// </summary>
        /// <param name="e">File watcher event args for calling a service.</param>
        private void CallServiceSynchronized(FileWatcherEventArgs e)
        {
            // Turn on process running for synchronization of processes.
            SetProcessRunningOn();
            // Increase external process counter.
            SetExternalProcessRunningOn();

            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ServiceBeginCallEventArgs> handler = ServiceBeginCall;
                if (handler != null)
                {
                    handler(this,
                            new ServiceBeginCallEventArgs(e.ConfigurationKeyValuePair.Key,
                                                          GetProcessQueueCount(),
                                                          e.Id));
                }
            }), null);

            FileWatcherStreamingServiceProxy fileWatcherStreamingServiceProxy = null;
            FileWatcherServiceProxy fileWatcherServiceProxy = null;

            if (e.ConfigurationKeyValuePair.Value.StreamFile)
            {
                fileWatcherStreamingServiceProxy = CreateStreamingProxy(e.ConfigurationKeyValuePair.Key);
            }
            else
            {
                fileWatcherServiceProxy = CreateProxy(e.ConfigurationKeyValuePair.Key);
            }

            // If failed to create a proxy.
            if (fileWatcherStreamingServiceProxy == null &&
                fileWatcherServiceProxy == null)
            {
                // Decrease external process counter.
                SetExternalProcessRunningOff();

                // Run threaded event in main thread.
                _synchronizationContext.Send(new SendOrPostCallback(delegate
                {
                    EventHandler<ServiceProxyCreationErrorEventArgs> handler = ServiceProxyCreationError;
                    if (handler != null)
                    {
                        handler(this,
                                new ServiceProxyCreationErrorEventArgs(e.ConfigurationKeyValuePair.Key,
                                                                       e.Id,
                                                                       GetProcessQueueCount()));
                    }
                }), null);

                // Remove available process from the file watcher.
                RemoveAvailableProcess(e.ConfigurationKeyValuePair.Key);

                // Decrease file watcher process count.
                RemoveProcessFromProcessBatchSize(e.ConfigurationKeyValuePair.Key);

                SetProcessRunningOff();

                return;
            }

            try
            {
                IResponse response;
                string checksum = CalculateMD5Sum(e.FullPath);

                if (e.ConfigurationKeyValuePair.Value.StreamFile)
                {
                    using (Stream fileStream = File.OpenRead(e.FullPath))
                    {
                        response = new SystemChangedRespMC();

                        response.Message =
                            fileWatcherStreamingServiceProxy.SystemChangedStreaming(e.ChangeType,
                                                                                    checksum,
                                                                                    e.ConfigurationKeyValuePair.Key,
                                                                                    e.DateTime,
                                                                                    e.FileName,
                                                                                    fileStream.Length,
                                                                                    e.FullPath,
                                                                                    e.Id,
                                                                                    Environment.MachineName,
                                                                                    e.OldFullPath,
                                                                                    fileStream);
                    }

                    // Close proxy.
                    fileWatcherStreamingServiceProxy.Close();
                }
                else
                {
                    response = fileWatcherServiceProxy.SystemChanged(CreateRequest(e, checksum));

                    // Close proxy.
                    fileWatcherServiceProxy.Close();
                }

                // Decrease external process counter.
                SetExternalProcessRunningOff();

                if (response == null)
                {
                    // Run threaded event in main thread.
                    _synchronizationContext.Send(new SendOrPostCallback(delegate
                    {
                        EventHandler<ServiceCalledEventArgs> handler = ServiceCalled;
                        if (handler != null)
                        {
                            handler(this,
                                    new ServiceCalledEventArgs(e.ConfigurationKeyValuePair.Key,
                                                               String.Empty,
                                                               e.Id));
                        }
                    }), null);
                }
                else
                {
                    // Run threaded event in main thread.
                    _synchronizationContext.Send(new SendOrPostCallback(delegate
                    {
                        EventHandler<ServiceCalledEventArgs> handler = ServiceCalled;
                        if (handler != null)
                        {
                            handler(this,
                                    new ServiceCalledEventArgs(e.ConfigurationKeyValuePair.Key,
                                                               response.Message,
                                                               e.Id));
                        }
                    }), null);
                }
            }
            catch (Exception ex)
            {
                // Abort proxy.
                if (e.ConfigurationKeyValuePair.Value.StreamFile)
                {
                    fileWatcherStreamingServiceProxy.Abort();
                }
                else
                {
                    fileWatcherServiceProxy.Abort();
                }

                // Decrease external process counter.
                SetExternalProcessRunningOff();

                // Run threaded event in main thread.
                _synchronizationContext.Send(new SendOrPostCallback(delegate
                {
                    EventHandler<ServiceErrorEventArgs> handler = ServiceError;
                    if (handler != null)
                    {
                        handler(this,
                                new ServiceErrorEventArgs(e.ConfigurationKeyValuePair.Key,
                                                          ex,
                                                          e.Id,
                                                          GetProcessQueueCount()));
                    }
                }), null);
            }
            finally
            {
                // Remove available process from the file watcher.
                RemoveAvailableProcess(e.ConfigurationKeyValuePair.Key);

                // Decrease file watcher process count.
                RemoveProcessFromProcessBatchSize(e.ConfigurationKeyValuePair.Key);

                SetProcessRunningOff();
            }
        }

#endif

        #endregion

        /// <summary>
        /// Check if this instance is disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(Resources.ObjectNameFileWatcherContoller);
            }
        }

        /// <summary>
        /// Returns true if controller has unstarted starting file watchers.
        /// </summary>
        /// <returns>True if controller has unstarted starting file watchers.</returns>
        private bool HasStartingFileWatcher
        {
            get
            {
                foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
                {
                    if (fileWatcher.Value.IsStarting)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Returns true if all file watchers are starting.
        /// </summary>
        private bool IsAllStartingFileWatchers
        {
            get
            {
                foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
                {
                    if (!fileWatcher.Value.IsStarting)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Returns true if one of the file watchers has running threads.
        /// </summary>
        private bool HasPendingFileWatcher
        {
            get
            {
                foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
                {
                    if (fileWatcher.Value.HasRunningThreads())
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Returns true if file watchers loaded on file watcher controller are running.
        /// </summary>
        /// <returns>True if file watchers loaded on file watcher controller are running.</returns>
        private bool IsRunning()
        {
            lock (_lockRunning)
            {
                return _isRunning;
            }
        }

        /// <summary>
        /// Returns true if all file watchers loaded on the file watcher controller are running or starting.
        /// </summary>
        /// <returns>True if all file watchers loaded on the file watcher controller are running or starting.</returns>
        private bool IsAllRunning()
        {
            foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
            {
                if (!fileWatcher.Value.IsRunning && !fileWatcher.Value.IsStarting)
                {
                    return false;
                }
            }
            return true;
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
        /// Returns true if file watcher is running or starting.
        /// </summary>
        /// <returns>True if file watcher is running or starting</returns>
        private bool HasRunningFileWatcher()
        {
            foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
            {
                if (fileWatcher.Value.IsRunning || fileWatcher.Value.IsStarting)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Performs cleans stop of controller.
        /// </summary>
        /// <exception cref="OutOfMemoryException">No Memory.</exception>
        private void CleanStopController()
        {
            // Turn running state off.
            SetRunningOff();

            // Set stopping to true
            IsControllerStopping = true;

            // Wait for queued and running processes.
            StartWaitThread();
        }

        /// <summary>
        /// Starts wait thread.
        /// </summary>
        /// <exception cref="OutOfMemoryException">No memory.</exception>
        private void StartWaitThread()
        {
            try
            {
                Thread waitThread =
                    new Thread(new ThreadStart(WaitForQueuedProcesses));
                waitThread.Name = "WaitThread";
                waitThread.Start();
            }
            catch (OutOfMemoryException)
            {
                if (_processThread != null)
                {
                    ExitProcessThread = true;
                    _processThreadAutoResetEvent.Set(); // Signal processThreadAutoResetEvent.
                    _processThread.Abort(); // No deadlock
                    _processThread.Join();
                    ExitProcessThread = false;
                }
                ClearProcessQueue();
                TrimProcessQueueExcess();
                throw; // die!
            }
        }

        /// <summary>
        /// Starts the wait for queued processes.
        /// </summary>
        private void WaitForQueuedProcesses()
        {
            // If file watcher has running threads. No lock needed since file watchers
            // cannot be modified when controller is stopping.
            while (HasPendingFileWatcher)
            {
                Thread.Sleep(50);
            }

            // If to run queued processes
            if (RunQueuedProcesses)
            {
                WaitForProcessQueueToEmpty();
            }

            // Stop process thread.
            StopProcessThread();

            // Wait for pending processes.
            WaitForProcesses();

            // Clean up dictionaries (single thread so no lock needed).
            _availableProcessesDictionary.Clear();
            _processBatchSizeDictionary.Clear();
            _lockedProcesses.Clear();
            _delayedProcesses.Clear();
            _tryRenameFile.Clear();

            // Set controller stopping to false.
            IsControllerStopping = false;

            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<EventArgs> handler = ControllerStopped;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }), null);
        }

        /// <summary>
        /// Creates and configures file watchers.
        /// </summary>
        /// <exception cref="ArgumentNullException">File watcher config set is null.</exception>
        private void SetFileWatchers()
        {
            // Dispose file watchers if there is some.
            if (_fileWatchers != null)
            {
                foreach (KeyValuePair<string, FileWatcher> fileWatcher in _fileWatchers)
                {
                    if (fileWatcher.Value != null)
                    {
                        fileWatcher.Value.Dispose();
                    }
                }
            }

            _fileWatchers = new Dictionary<string, FileWatcher>();

            foreach (KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair in _configurationDictionary)
            {
                if (configurationKeyValuePair.Value.StartDaemon)
                {
                    AddFileWatcherToController(new FileWatcher(configurationKeyValuePair));
                }
            }
        }

        /// <summary>
        /// Adds file watcher and subscribes to events.
        /// </summary>
        /// <param name="fileWatcher">File watcher to add.</param>
        private void AddFileWatcherToController(FileWatcher fileWatcher)
        {
            fileWatcher.SystemChanged +=
                new EventHandler<FileWatcherEventArgs>(OnSystemChanged);

            fileWatcher.BufferError +=
                new EventHandler<FileWatcherBufferErrorEventArgs>(OnBufferError);

            fileWatcher.FileWatcherStarted +=
                new EventHandler<FileWatcherStartedEventArgs>(OnFileWatcherStarted);

            fileWatcher.FileWatcherStarting +=
                new EventHandler<FileWatcherStartingEventArgs>(OnFileWatcherStarting);

            fileWatcher.FileWatcherStopped +=
                new EventHandler<FileWatcherStoppedEventArgs>(OnFileWatcherStopped);

            fileWatcher.FileWatcherSearchError +=
                new EventHandler<FileWatcherSearchErrorEventArgs>(OnFileWatcherSearchError);

            fileWatcher.FileWatcherSearchProgress +=
                new EventHandler<FileWatcherSearchProgressEventArgs>(OnFileWatcherSearchProgress);

            fileWatcher.FileWatcherRecycle +=
                new EventHandler<FileWatcherRecycledEventArgs>(OnFileWatcherRecycle);

            _fileWatchers.Add(fileWatcher.DaemonName, fileWatcher);
        }

        /// <summary>
        /// Start the process or calls service synchronized with other processes.
        /// </summary>
        /// <param name="e">File watcher event args for running a process.</param>
        private void StartProcessSynchronized(FileWatcherEventArgs e)
        {
            // If no process is running yet. Then start the process.
            if (!IsProcessRunning())
            {
                StartProcessSynchronizedHelper(e);
            }
            else
            {
                bool exitLoop = false;

                // Wait until the current running process has exited.
                while (!exitLoop)
                {
                    if (!IsProcessRunning())
                    {
                        exitLoop = true;

                        StartProcessSynchronizedHelper(e);
                    }
                    else
                    {
                        _syncProcessAutoResetEvent.WaitOne();
                    }
                }
            }
        }

        /// <summary>
        /// Start process specified in file watcher event args.
        /// </summary>
        /// <param name="e">File watcher event args for running a process.</param>
        /// <exception cref="Win32Exception">Unexpected error.</exception>
        private void StartProcess(FileWatcherEventArgs e)
        {
            try
            {
                // Turn on process running for synchronization of processes.
                SetProcessRunningOn();
                // Increase external process counter.
                SetExternalProcessRunningOn();

                ProcessRunner processRunner = new ProcessRunner(e);

                // Subscribe to process events.

                processRunner.ProcessStarted +=
                    new EventHandler<ProcessStartedEventArgs>(OnProcessStarted);

                processRunner.ProcessExited +=
                    new EventHandler<ProcessExitEventArgs>(OnProcessExited);

                if (e.ConfigurationKeyValuePair.Value.ProcessRedirectStandardOutput)
                {
                    processRunner.ProcessErrorData +=
                        new EventHandler<ProcessDataEventArgs>(OnProcessErrorData);
                }
                if (e.ConfigurationKeyValuePair.Value.ProcessRedirectStandardError)
                {
                    processRunner.ProcessOutputData +=
                        new EventHandler<ProcessDataEventArgs>(OnProcessOutputData);
                }

                processRunner.StartProcess();
            }
            catch (InvalidOperationException exception)
            {
                RaiseProcessError(exception,
                                  e);
            }
            catch (ArgumentException exception)
            {
                RaiseProcessError(exception,
                                  e);
            }
            catch (Win32Exception exception)
            {
                if (exception.NativeErrorCode == ErrorFileNotFound)
                {
                    RaiseProcessError(exception,
                                      e);
                }
                else if (exception.NativeErrorCode == ErrorAccessDenied)
                {
                    RaiseProcessError(exception,
                                      e);
                }
                else if (exception.NativeErrorCode == // User account.
                         ErrorUnknownUserNameOrBadPassword)
                {
                    RaiseProcessError(exception,
                                      e);
                }
                else if (exception.NativeErrorCode == // User account.
                         ErrorUserAccountRestriction)
                {
                    RaiseProcessError(exception,
                                      e);
                }
                else // Unexpected error occured.
                {
                    // Raise event since we are in a thread.
                    RaiseProcessError(exception,
                                      e);
                }
            }
        }

        /// <summary>
        /// Waits until process queue is empty.
        /// </summary>
        private void WaitForProcessQueueToEmpty()
        {
            // Exits loop when true.
            bool exitLoop = false;

            while (!exitLoop)
            {
                // If queue is empty.
                if (GetProcessQueueCount() == 0)
                {
                    // Exit loop.
                    exitLoop = true;
                }
                else
                {
                    _processQueueDequeuedAutoResetEvent.WaitOne();
                }
            }
        }

        /// <summary>
        /// Waits until there are no processes running.
        /// </summary>
        private void WaitForProcesses()
        {
            // Exit loop when true.
            bool exitLoop = false;

            // Wait for processes.
            while (!exitLoop)
            {
                // If there is no running processes.
                if (InternalRunningProcesses == 0)
                {
                    exitLoop = true;
                }
                else
                {
                    _waitForProcessesAutoResetEvent.WaitOne();
                }
            }
        }

        /// <summary>
        /// Starts process thread.
        /// </summary>
        /// <exception cref="OutOfMemoryException">No memory.</exception>
        private void StartProcessThread()
        {
            _processThread = new Thread(new ThreadStart(StartProcessThreading));
            _processThread.Name = "ProcessThread";
            _processThread.Start();
        }

        /// <summary>
        /// Stops process thread.
        /// </summary>
        private void StopProcessThread()
        {
            // Set exit thread.
            ExitProcessThread = true;

            // Signal processThreadAutoResetEvent.
            _processThreadAutoResetEvent.Set();

            // Wait for thread to terminate.
            if (_processThread != null)
            {
                _processThread.Join();
            }

            ExitProcessThread = false;

            // Clear process queue.
            ClearProcessQueue();

            // Trim process queue.
            TrimProcessQueueExcess();
        }

        /// <summary>
        /// Starts process from process queue while ExitProcessThread is false.
        /// </summary>
        private void StartProcessThreading()
        {
            while (!ExitProcessThread)
            {
                // If processes to run.
                if (GetProcessQueueCount() > 0)
                {
                    // Check file watcher controller process batch size.
                    if (ProcessBatchSize != 0)
                    {
                        // If file watcher controller process batch size is not met.
                        if (ProcessBatchSize > InternalRunningProcesses)
                        {
                            // Starts process or re-enqueues the process if
                            // batch size is full or if the process is delayed.
                            CheckStartProcess();
                        }
                        else
                        {
                            // Set when process has completed.
                            _waitForProcessesAutoResetEvent.WaitOne();
                        }
                    }
                    else
                    {
                        // Starts process or re-enqueues the process if 
                        // batch size is full or if the process is delayed.
                        CheckStartProcess();
                    }
                }
                else // If no processes in queue.
                {
                    if (!ExitProcessThread) // If not to exit thread.
                    {
                        _processThreadAutoResetEvent.WaitOne();
                    }
                }

                // Trim process queue.
                TrimProcessQueueExcessTimed();
            }
        }

        /// <summary>
        /// Starts process or re-enqueues the process if batch size is full or if the process is delayed.
        /// </summary>
        private void CheckStartProcess()
        {
            FileWatcherEventArgs process = DequeueProcess();

            if (IsProcessBatchSizeFull(process.ConfigurationKeyValuePair.Key,
                process.ConfigurationKeyValuePair.Value.ProcessBatchSize))
            {
                // Re-enqueues process for a run.
                EnqueueProcess(process);

                // If not everything is requeued.
                if (AreAllProcessBatchSizesFull())
                {
                    // Set when process has completed.
                    _waitForProcessesAutoResetEvent.WaitOne();
                }
            }
            else
            {
                if (!CheckProcessDelay(process))
                {
                    // Re-enqueues process for a run.
                    EnqueueProcess(process);
                    return;
                }
                if (!CheckFileMustExist(process))
                {
                    // Run threaded event in main thread.
                    _synchronizationContext.Send(new SendOrPostCallback(delegate
                    {
                        EventHandler<ProcessCanceledEventArgs> handler = ProcessCanceled;
                        if (handler != null)
                        {
                            handler(this,
                                    new ProcessCanceledEventArgs(process.ConfigurationKeyValuePair.Key,
                                                                 ProcessCanceledReason.FileWasMissing,
                                                                 process.FullPath,
                                                                 GetProcessQueueCount()));
                        }
                    }), null);

                    // Remove available process from the file watcher.
                    RemoveAvailableProcess(process.ConfigurationKeyValuePair.Key);

                    // Remove process from the queue.
                    return;
                }
                if (!CheckFileLock(process))
                {
                    // Check process re-enqueue count.
                    if (CheckEnqueueCount(process))
                    {
                        // Re-enqueues process for a run.
                        EnqueueProcess(process);
                    }
                    else
                    {
                        // Run threaded event in main thread.
                        _synchronizationContext.Send(new SendOrPostCallback(delegate
                        {
                            EventHandler<ProcessCanceledEventArgs> handler = ProcessCanceled;
                            if (handler != null)
                            {
                                handler(this,
                                        new ProcessCanceledEventArgs(process.ConfigurationKeyValuePair.Key,
                                                                     ProcessCanceledReason.FileLockTestFailed,
                                                                     process.FullPath,
                                                                     GetProcessQueueCount()));
                            }
                        }), null);
                    }
                    return;
                }
                // Check if to try rename file.
                if (process.ConfigurationKeyValuePair.Value.TryRenameFile)
                {
                    RenameResult renameResult;
                    FileWatcherEventArgs renamedProcess = CallRenameFile(process, out renameResult);

                    // If rename failed and retries count is not met and we want to retry it.
                    if (renameResult == RenameResult.Retry)
                    {
                        // Re-enqueues process for a run.
                        EnqueueProcess(process);
                    }
                    // Retries count is met or file cannot be renamed.
                    else if (renameResult == RenameResult.Cancel)
                    {
                        // Run threaded event in main thread.
                        _synchronizationContext.Send(new SendOrPostCallback(delegate
                        {
                            EventHandler<ProcessCanceledEventArgs> handler = ProcessCanceled;
                            if (handler != null)
                            {
                                handler(this,
                                        new ProcessCanceledEventArgs(process.ConfigurationKeyValuePair.Key,
                                                                     ProcessCanceledReason.RenameFailed,
                                                                     process.FullPath,
                                                                     GetProcessQueueCount()));
                            }
                        }), null);

                        // Remove available process from the file watcher.
                        RemoveAvailableProcess(process.ConfigurationKeyValuePair.Key);
                    }
                    else // Success.
                    {
                        // Run process with renamed file information.
                        RunProcess(renamedProcess);
                    }
                }
                else
                {
                    // Run process (not renamed).
                    RunProcess(process);
                }
            }
        }

        /// <summary>
        /// Returns true if process should be re-enqueued.
        /// </summary>
        /// <param name="process">Process to check.</param>
        /// <returns>True if process should be re-enqueued.</returns>
        private bool CheckEnqueueCount(FileWatcherEventArgs process)
        {
            // If no limit set.
            if (process.ConfigurationKeyValuePair.Value.ProcessLockFileRetries == 0)
            {
                return true;
            }
            // If not controller is stopping and if the process queue limit is not reached.
            if (!IsControllerStopping &&
                process.ConfigurationKeyValuePair.Value.ProcessLockFileRetriesQueueLimit >= (GetProcessQueueCount() + 1))
            {
                return true;
            }
            // Check the process retries count.
            return _lockedProcesses[process.Id] <= process.ConfigurationKeyValuePair.Value.ProcessLockFileRetries;
        }

        /// <summary>
        /// Returns true if process is ready to run. Removes locked process if process is not re-enqueued.
        /// </summary>
        /// <param name="process">Process to check.</param>
        /// <returns>True if process is ready to run.</returns>
        private bool CheckFileMustExist(FileWatcherEventArgs process)
        {
            if (process.ConfigurationKeyValuePair.Value.ProcessFileMustExist)
            {
                if (!File.Exists(process.FullPath))
                {
                    // Remove locked process (file).
                    if (_lockedProcesses.ContainsKey(process.Id))
                    {
                        _lockedProcesses.Remove(process.Id);
                    }
                    // Remove process.
                    return false;
                }
            }
            // Run process.
            return true;
        }

        /// <summary>
        /// Returns true if process is ready to run. Waits if all processes are delayed or locked.
        /// </summary>
        /// <param name="process">Process to check.</param>
        /// <returns>True if process is ready to run.</returns>
        private bool CheckProcessDelay(FileWatcherEventArgs process)
        {
            if (process.ConfigurationKeyValuePair.Value.ProcessDelay == 0)
            {
                // Run process.
                return true;
            }

            DateTime delayedTime = process.DateTime.AddMilliseconds(
                process.ConfigurationKeyValuePair.Value.ProcessDelay);

            if (delayedTime > DateTime.Now)
            {
                if (!_delayedProcesses.ContainsKey(process.Id))
                {
                    _delayedProcesses.Add(process.Id, true);
                }
                // If all processes are delayed.
                if ((GetProcessQueueCount() + 1) == _delayedProcesses.Count)
                {
                    Thread.Sleep(ProcessInterval);
                }
                // If all processes are locked or delayed.
                else if ((GetProcessQueueCount() + 1) == (_lockedProcesses.Count + _delayedProcesses.Count))
                {
                    Thread.Sleep(ProcessInterval);
                }

                // Re-enqueues process for a run.
                return false;
            }
            if (_delayedProcesses.ContainsKey(process.Id))
            {
                _delayedProcesses.Remove(process.Id);
            }

            // Run process.
            return true;
        }

        /// <summary>
        /// Returns true if process is ready to run. Waits if all processes are delayed or locked.
        /// </summary>
        /// <param name="process">Process to check.</param>
        /// <returns>True if process is ready to run.</returns>
        private bool CheckFileLock(FileWatcherEventArgs process)
        {
            if (!process.ConfigurationKeyValuePair.Value.ProcessLockFile)
            {
                // Run process.
                return true;
            }
            // Check if file is locked.
            if (IsFileLocked(process))
            {
                if (_lockedProcesses.ContainsKey(process.Id))
                {
                    // Nice overflow.
                    if (_lockedProcesses[process.Id] != Int32.MaxValue)
                    {
                        // Increase enqueue count.
                        _lockedProcesses[process.Id]++;
                    }
                }
                else
                {
                    // Add locked process (file) if it does not exists.
                    _lockedProcesses.Add(process.Id, 1);
                }
                // If all processes are locked.
                if ((GetProcessQueueCount() + 1) == _lockedProcesses.Count)
                {
                    Thread.Sleep(ProcessInterval);
                }
                // If all processes are locked or delayed.
                else if ((GetProcessQueueCount() + 1) == (_lockedProcesses.Count + _delayedProcesses.Count))
                {
                    Thread.Sleep(ProcessInterval);
                }

                // Re-enqueues process for a run.
                return false;
            }
            // Remove locked process (file).
            if (_lockedProcesses.ContainsKey(process.Id))
            {
                _lockedProcesses.Remove(process.Id);
            }

            // Run process.
            return true;
        }

        /// <summary>
        /// Calls rename file and counts rename retries.
        /// </summary>
        /// <param name="process">Running process.</param>
        /// <param name="renameResult">Rename result.</param>
        /// <returns>Returns new FileWatcherEventArgs with new renamed file path or null
        /// if the process is re-queued or rename fails or is canceled.</returns>
        private FileWatcherEventArgs CallRenameFile(FileWatcherEventArgs process,
                                                    out RenameResult renameResult)
        {
            // If file is a directory.
            if (Directory.Exists(process.FullPath))
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            // Check if file exists.
            if (!File.Exists(process.FullPath))
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            // Check retries count if to continue.
            if (_tryRenameFile.ContainsKey(process.Id))
            {
                if (process.ConfigurationKeyValuePair.Value.TryRenameFileRetries == _tryRenameFile[process.Id])
                {
                    renameResult = RenameResult.Cancel;
                    return null;
                }
            }

            // Try to rename the file.
            FileWatcherEventArgs fileWatcherEventArgs = TryRenameFile(process,
                                                                      out renameResult);

            // If to re-enqueue.
            if (renameResult == RenameResult.Retry)
            {
                // Add or increment counter.
                if (!_tryRenameFile.ContainsKey(process.Id))
                {
                    _tryRenameFile.Add(process.Id, 0);
                }
                else
                {
                    _tryRenameFile[process.Id]++;
                }
            }
            else // Remove counter.
            {
                if (_tryRenameFile.ContainsKey(process.Id))
                {
                    _tryRenameFile.Remove(process.Id);
                }
            }

            // Return original process or a new process with renamed file path.
            return fileWatcherEventArgs;
        }

        /// <summary>
        /// Tries to rename file.
        /// </summary>
        /// <param name="process">Running process.</param>
        /// <param name="renameResult">Rename result.</param>
        /// <returns>Returns new FileWatcherEventArgs with new renamed file path or null
        /// if the process is re-queued or rename fails or is canceled.</returns>
        private static FileWatcherEventArgs TryRenameFile(FileWatcherEventArgs process,
                                                          out RenameResult renameResult)
        {
            string fileName = Path.GetFileName(process.FullPath);

            if (fileName == null)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }

            const string FileNameFormat = @"{0}.{{{1}}}.process";

            string newFileName = String.Format(CultureInfo.CurrentCulture,
                                               FileNameFormat,
                                               fileName,
                                               process.Id);

            string newFullPath = process.FullPath.Remove(process.FullPath.Length - fileName.Length,
                                                         fileName.Length) + newFileName;

            try
            {
                // Rename file.
                File.Move(process.FullPath, newFullPath);
                renameResult = RenameResult.Success;
            }
            catch (PathTooLongException)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            catch (DirectoryNotFoundException)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            catch (FileNotFoundException)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            catch (UnauthorizedAccessException)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            catch (NotSupportedException)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            catch (ArgumentException)
            {
                renameResult = RenameResult.Cancel;
                return null;
            }
            catch (IOException) // File can be locked by another process or something else goes wrong.
            {
                // Retry renaming.
                renameResult = RenameResult.Retry;
                return null;
            }

            // Return new process with renamed file information.
            return new FileWatcherEventArgs(process.ConfigurationKeyValuePair,
                                            process.ChangeType,
                                            newFileName,
                                            newFullPath,
                                            process.FullPath,
                                            process.Id);
        }

        /// <summary>
        /// Runs synchronous or asynchronous process.
        /// </summary>
        /// <param name="process">Process to run.</param>
        private void RunProcess(FileWatcherEventArgs process)
        {
            // Increase file watcher process count.
            AddProcessToProcessBatchSize(process.ConfigurationKeyValuePair.Key);

            // Check if to use synchronized execution.
            if (SynchronousExecution)
            {
                StartProcessSynchronized(process);
            }
            else
            {
                RunProcessHelper(process);
            }
        }

        /// <summary>
        /// Returns true if file is locked.
        /// </summary>
        /// <param name="process">Process to run.</param>
        /// <returns>True if file is locked.</returns>
        /// <remarks>Returns false if full path is a directory or file does not exist.</remarks>
        private static bool IsFileLocked(FileWatcherEventArgs process)
        {
            // If file is a directory.
            if (Directory.Exists(process.FullPath))
            {
                return false;
            }
            // If file does not exist.
            if (!File.Exists(process.FullPath))
            {
                return false;
            }

            try
            {
                FileInfo fileInfo = new FileInfo(process.FullPath);
                DateTime lastWrite = fileInfo.LastWriteTime;

                // Filter too fresh files.
                lastWrite = lastWrite.AddMilliseconds(
                    process.ConfigurationKeyValuePair.Value.ProcessLockFileLastWriteDelay);

                // Check last write and creation time.
                if (lastWrite < DateTime.Now)
                {
                    // If file can be opened or there is a critical error when opening the file.
                    if (TryOpenFile(process.FullPath))
                    {
                        return false;
                    }
                }
                // File is locked.
                return true;
            }
            catch (SecurityException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (PathTooLongException)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if file can be opened or there is a critical error opening a the file.
        /// </summary>
        /// <param name="fullPath">Full path of the file.</param>
        /// <returns>True if file can be opened or there is a critical error opening a the file.</returns>
        private static bool TryOpenFile(string fullPath)
        {
            try
            {
                FileStream fileStream = File.Open(fullPath,
                                                  FileMode.Open,
                                                  FileAccess.Read,
                                                  FileShare.None);
                fileStream.Close();
                return true;
            }
            catch (SecurityException)
            {
                return true;
            }
            catch (FileNotFoundException)
            {
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return true;
            }
            catch (DirectoryNotFoundException)
            {
                return true;
            }
            catch (IOException)
            {
                // The file is already open.
                return false;
            }
        }

        /// <summary>
        /// Calculates MD5sum.
        /// </summary>
        /// <param name="fullPath">Path of the file.</param>
        /// <returns>MD5sum.</returns>
        private static string CalculateMD5Sum(string fullPath)
        {
            using (FileStream fileStream = File.OpenRead(fullPath))
            {
                byte[] output;

                using (MD5 md5 = MD5.Create())
                {
                    output = md5.ComputeHash(fileStream);
                }

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < output.Length; i++)
                {
                    stringBuilder.Append(output[i].ToString("x2", CultureInfo.InvariantCulture));
                }

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Returns true if file watcher process batch size is met.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <param name="processBatchSize">Process batch size of the file watcher.</param>
        /// <returns>True if process batch size is full.</returns>
        private bool IsProcessBatchSizeFull(string daemonName, int processBatchSize)
        {
            lock (_lockProcessBatchSizeDictionary)
            {
                // Zero in process batch size.
                if (processBatchSize == 0)
                {
                    return false;
                }

                int tempProcesses;

                // If process batch size is found.
                if (_processBatchSizeDictionary.TryGetValue(daemonName, out tempProcesses))
                {
                    // If there is no available process.
                    if (tempProcesses == processBatchSize)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Returns true if all file watchers have met the process batch size.
        /// </summary>
        /// <returns>True if all file watchers have met the process batch size.</returns>
        private bool AreAllProcessBatchSizesFull()
        {
            lock (_lockProcessBatchSizeDictionary)
            {
                // Not all file watchers have started a process yet.
                if (AvailableProcessDictionaryCount() != _processBatchSizeDictionary.Count)
                {
                    return false;
                }
                // Check all file watchers that run a process.
                foreach (KeyValuePair<string, int> keyValuePair in _processBatchSizeDictionary)
                {
                    // Check if there is available processes for the file watcher.
                    // Another lock in this one.
                    if (HasAvailableProcess(keyValuePair.Key))
                    {
                        // Check current process value against the max value.
                        if (_configurationDictionary[keyValuePair.Key].ProcessBatchSize > keyValuePair.Value)
                        {
                            return false;
                        }
                        // If file watcher has no batch limit.
                        if (_configurationDictionary[keyValuePair.Key].ProcessBatchSize == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Returns count of process dictionary.
        /// </summary>
        /// <returns>Count of process dictionary.</returns>
        private int AvailableProcessDictionaryCount()
        {
            lock (_lockAvailableProcessDictionary)
            {
                return _availableProcessesDictionary.Count;
            }
        }

        /// <summary>
        /// Returns true if file watcher has available process.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <returns>True if file watcher has available process.</returns>
        private bool HasAvailableProcess(string daemonName)
        {
            lock (_lockAvailableProcessDictionary)
            {
                int tempAvailableProcessies;

                if (_availableProcessesDictionary.TryGetValue(daemonName, out tempAvailableProcessies))
                {
                    // If some processes.
                    if (tempAvailableProcessies > 0)
                    {
                        return true;
                    }
                }

                // If no available procesesses for the file watcher.
                return false;
            }
        }

        /// <summary>
        /// Adds available process for file watcher.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        private void AddAvailableProcess(string daemonName)
        {
            lock (_lockAvailableProcessDictionary)
            {
                if (!_availableProcessesDictionary.ContainsKey(daemonName))
                {
                    _availableProcessesDictionary.Add(daemonName, 1);
                }
                else
                {
                    _availableProcessesDictionary[daemonName]++;
                }
            }
        }

        /// <summary>
        /// Removes available process from the file watcher.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        private void RemoveAvailableProcess(string daemonName)
        {
            lock (_lockAvailableProcessDictionary)
            {
                _availableProcessesDictionary[daemonName]--;
            }
        }

        /// <summary>
        /// Adds process to file watcher batch size.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        private void AddProcessToProcessBatchSize(string daemonName)
        {
            lock (_lockProcessBatchSizeDictionary)
            {
                if (!_processBatchSizeDictionary.ContainsKey(daemonName))
                {
                    _processBatchSizeDictionary.Add(daemonName, 1);
                }
                else
                {
                    // Increase running processes.
                    _processBatchSizeDictionary[daemonName]++;
                }
            }
        }

        /// <summary>
        /// Removes process form the file watcher batch size.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        private void RemoveProcessFromProcessBatchSize(string daemonName)
        {
            lock (_lockProcessBatchSizeDictionary)
            {
                // Reduce running processes.
                _processBatchSizeDictionary[daemonName]--;
            }
        }

        /// <summary>
        /// Sets external process running on.
        /// </summary>
        private void SetExternalProcessRunningOn()
        {
            lock (_lockExternalRunningProcesses)
            {
                _externalRunningProcesses++;
            }
        }

        /// <summary>
        /// Sets external process running off.
        /// </summary>
        private void SetExternalProcessRunningOff()
        {
            lock (_lockExternalRunningProcesses)
            {
                _externalRunningProcesses--;
            }
        }

        /// <summary>
        /// Sets process runnning on. Increases running process counter.
        /// </summary>
        private void SetProcessRunningOn()
        {
            lock (_lockProcessRunning)
            {
                _runningProcesses++;
                _processRunning = true;
            }
        }

        /// <summary>
        /// Sets process running off. Decreases running process counter.
        /// Signals syncProcessAutoResetEvent.
        /// Signals waitForProcessesAutoResetEvent.
        /// </summary>
        private void SetProcessRunningOff()
        {
            lock (_lockProcessRunning)
            {
                _runningProcesses--;
                _processRunning = false;
                // Signal syncProcessAutoResetEvent the process has exited.
                _syncProcessAutoResetEvent.Set();
                // Signal waitForProcessesAutoResetEvent that process count has reduced.
                _waitForProcessesAutoResetEvent.Set();
            }
        }

        /// <summary>
        /// Returns internal count of running processes.
        /// </summary>
        private int InternalRunningProcesses
        {
            get
            {
                lock (_lockProcessRunning)
                {
                    return _runningProcesses;
                }
            }
        }

        /// <summary>
        /// Returns true if process is running.
        /// </summary>
        /// <returns>True if process is running.</returns>
        private bool IsProcessRunning()
        {
            lock (_lockProcessRunning)
            {
                return _processRunning;
            }
        }

        /// <summary>
        /// Sets running of the file watchers on.
        /// </summary>
        private void SetRunningOn()
        {
            lock (_lockRunning)
            {
                _isRunning = true;
            }
        }

        /// <summary>
        /// Sets running of the file watchers off.
        /// </summary>
        private void SetRunningOff()
        {
            lock (_lockRunning)
            {
                _isRunning = false;
            }
        }

        /// <summary>
        /// Gets the count of enqueued processes.
        /// </summary>
        /// <returns>Count of enqueued processes.</returns>
        private int GetProcessQueueCount()
        {
            lock (_lockProcessQueue)
            {
                return _processQueue.Count;
            }
        }

        /// <summary>
        /// Enqueues process to process queue. Signals processThreadAutoResetEvent.
        /// </summary>
        /// <param name="e">File watcher event args for running a process.</param>
        private void EnqueueProcess(FileWatcherEventArgs e)
        {
            lock (_lockProcessQueue)
            {
                _processQueue.Enqueue(e);
                // Signal syncProcessAutoResetEvent there is an item to process.
                _processThreadAutoResetEvent.Set();
            }
        }

        /// <summary>
        /// Dequeues process from process queue. Signals processQueueDequeuedAutoResetEvent.
        /// </summary>
        /// <returns>Returns file watcher event args for running dequeued process.</returns>
        /// <exception cref="InvalidOperationException">Queue is empty.</exception>
        private FileWatcherEventArgs DequeueProcess()
        {
            lock (_lockProcessQueue)
            {
                // Signal processQueueDequeuedAutoResetEvent for exit WaitForProcessQueueToEmpty.
                _processQueueDequeuedAutoResetEvent.Set();
                return _processQueue.Dequeue();
            }
        }

        /// <summary>
        /// Clears process queue.
        /// </summary>
        private void ClearProcessQueue()
        {
            lock (_lockProcessQueue)
            {
                _processQueue.Clear();
            }
        }

        /// <summary>
        /// Trims process queue if the last excess trim was performed more than specified minutes ago.
        /// </summary>
        private void TrimProcessQueueExcessTimed()
        {
            int tempQueueTrimInterval = GetQueueTrimInterval();

            // If queue trimming is not disabled.
            if (tempQueueTrimInterval > 0)
            {
                // If time interval has elapsed.
                if (DateTime.Now > _queueLastTrimTime)
                {
                    // Trim queue.
                    lock (_lockProcessQueue)
                    {
                        _processQueue.TrimExcess();
                    }

                    // Set next time to do some trimming.
                    _queueLastTrimTime = DateTime.Now.AddMinutes(tempQueueTrimInterval);
                }
            }
        }

        /// <summary>
        /// Gets queue trim interval.
        /// </summary>
        /// <returns>Queue trim interval.</returns>
        private int GetQueueTrimInterval()
        {
            lock (_lockQueueTrimInterval)
            {
                return _queueTrimInterval;
            }
        }

        /// <summary>
        /// Trims process queue.
        /// </summary>
        private void TrimProcessQueueExcess()
        {
            lock (_lockProcessQueue)
            {
                _processQueue.TrimExcess();
            }
        }

        /// <summary>
        /// Gets or sets exit process thread.
        /// </summary>
        private bool ExitProcessThread
        {
            get
            {
                lock (_lockExitProcessThread)
                {
                    return _exitProcessThread;
                }
            }
            set
            {
                lock (_lockExitProcessThread)
                {
                    _exitProcessThread = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets stopping value of file watcher controller.
        /// </summary>
        private bool IsControllerStopping
        {
            get
            {
                lock (_lockIsControllerStopping)
                {
                    return _isControllerStopping;
                }
            }
            set
            {
                lock (_lockIsControllerStopping)
                {
                    _isControllerStopping = value;
                }
            }
        }

        /// <summary>
        /// Raises process error event. Reduces process counts.
        /// </summary>
        /// <param name="exception">Exception that occured.</param>
        /// <param name="e">Event data.</param>
        private void RaiseProcessError(Exception exception,
                                       FileWatcherEventArgs e)
        {
            // Decrease external process counter.
            SetExternalProcessRunningOff();

            ProcessErrorEventArgs processErrorEventArgs =
                new ProcessErrorEventArgs(e.ConfigurationKeyValuePair.Key,
                                          exception,
                                          GetProcessQueueCount());
            OnProcessError(this,
                           processErrorEventArgs);

            // Remove available process from the file watcher.
            RemoveAvailableProcess(e.ConfigurationKeyValuePair.Key);

            // Decrease file watcher process count.
            RemoveProcessFromProcessBatchSize(e.ConfigurationKeyValuePair.Key);

            SetProcessRunningOff();
        }

        #region Private event handlers

        /// <summary>
        /// Handles system changed event. Runs in the main thread.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnSystemChanged(object sender,
                                     FileWatcherEventArgs e)
        {
            EventHandler<FileWatcherEventArgs> handler = SystemChanged;
            if (handler != null)
            {
                handler(this, e);
            }

            // If file watcher is configured for running a process or calling a service.
            if (e.ConfigurationKeyValuePair.Value.StartProcess || e.ConfigurationKeyValuePair.Value.CallService)
            {
                // Enqueue process for a run.
                EnqueueProcess(e);

                // Add available process for the file watcher.
                AddAvailableProcess(e.ConfigurationKeyValuePair.Key);
            }
        }

        /// <summary>
        /// Handles process error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnProcessError(object sender,
                                    ProcessErrorEventArgs e)
        {
            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ProcessErrorEventArgs> handler = ProcessError;
                if (handler != null)
                {
                    handler(this, e);
                }
            }), null);
        }

        /// <summary>
        /// Handles buffer error event. Runs in the main thread.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnBufferError(object sender,
                                   FileWatcherBufferErrorEventArgs e)
        {
            EventHandler<FileWatcherBufferErrorEventArgs> handler = BufferError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles process started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnProcessStarted(object sender,
                                      ProcessStartedEventArgs e)
        {
            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ControllerProcessStartedEventArgs> handler = ProcessStarted;
                if (handler != null)
                {
                    ControllerProcessStartedEventArgs controllerProcessStartedEventArgs =
                        new ControllerProcessStartedEventArgs(e.DaemonName,
                                                              e.ProcessId,
                                                              e.FileName,
                                                              e.Arguments,
                                                              e.Verb,
                                                              e.ProcessStartTime,
                                                              e.LogEvent,
                                                              GetProcessQueueCount());
                    handler(this, controllerProcessStartedEventArgs);
                }
            }), null);
        }

        /// <summary>
        /// Handles process exited event. Decreases process counts.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnProcessExited(object sender,
                                     ProcessExitEventArgs e)
        {
            // Decrease external process counter.
            SetExternalProcessRunningOff();

            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ProcessExitEventArgs> handler = ProcessExited;
                if (handler != null)
                {
                    handler(this, e);
                }
            }), null);

            // Remove available process from the file watcher.
            RemoveAvailableProcess(e.DaemonName);

            // Decrease file watcher process count.
            RemoveProcessFromProcessBatchSize(e.DaemonName);

            SetProcessRunningOff();
        }

        /// <summary>
        /// Handles process error data event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnProcessErrorData(object sender,
                                        ProcessDataEventArgs e)
        {
            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ProcessDataEventArgs> handler = ProcessErrorData;
                if (handler != null)
                {
                    handler(this, e);
                }
            }), null);
        }

        /// <summary>
        /// Handles process output data event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnProcessOutputData(object sender,
                                         ProcessDataEventArgs e)
        {
            // Run threaded event in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                EventHandler<ProcessDataEventArgs> handler = ProcessOutputData;
                if (handler != null)
                {
                    handler(this, e);
                }
            }), null);
        }

        /// <summary>
        /// Handles file watcher file search error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileWatcherSearchError(object sender,
                                              FileWatcherSearchErrorEventArgs e)
        {
            EventHandler<FileWatcherSearchErrorEventArgs> handler = FileWatcherSearchError;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles file watcher file search progress event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileWatcherSearchProgress(object sender,
                                                 FileWatcherSearchProgressEventArgs e)
        {
            EventHandler<FileWatcherSearchProgressEventArgs> handler = FileWatcherSearchProgress;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles file watcher started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileWatcherStarted(object sender,
                                          FileWatcherStartedEventArgs e)
        {
            EventHandler<FileWatcherStartedEventArgs> handler = FileWatcherStarted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles file watcher starting event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileWatcherStarting(object sender,
                                           FileWatcherStartingEventArgs e)
        {
            EventHandler<FileWatcherStartingEventArgs> handler = FileWatcherStarting;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles file watcher stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileWatcherStopped(object sender,
                                          FileWatcherStoppedEventArgs e)
        {
            EventHandler<FileWatcherStoppedEventArgs> handler = FileWatcherStopped;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Handles file watcher recycle event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileWatcherRecycle(object sender,
                                          FileWatcherRecycledEventArgs e)
        {
            EventHandler<FileWatcherRecycledEventArgs> handler = FileWatcherRecycle;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion Private event handlers

        /// <summary>
        /// Call service delegate.
        /// </summary>
        /// <param name="e">File watcher event args for calling a service.</param>
        private delegate void CallServiceDelegate(FileWatcherEventArgs e);

        #region Private enums

        /// <summary>
        /// Rename result values.
        /// </summary>
        private enum RenameResult
        {
            /// <summary>
            /// Cannot rename the file.
            /// </summary>
            Cancel,
            /// <summary>
            /// Rename was successfull.
            /// </summary>
            Success,
            /// <summary>
            /// Retry renaming.
            /// </summary>
            Retry
        }

        #endregion

        #region Private members

        /// <summary>
        /// Queue excess trim interval on minutes. Zero to disable.
        /// </summary>
        private int _queueTrimInterval = 5;

        /// <summary>
        /// Last queue excess trim time.
        /// </summary>
        private DateTime _queueLastTrimTime;

        /// <summary>
        /// Holds information about the file rename retries.
        /// </summary>
        private readonly Dictionary<Guid, int> _tryRenameFile = new Dictionary<Guid, int>();

        /// <summary>
        /// Process interval in ms.
        /// </summary>
        private const int ProcessInterval = 20;

        /// <summary>
        /// Auto reset event for processThread. Set when item is added to queue.
        /// </summary>
        private readonly AutoResetEvent _processThreadAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Auto reset event for starting synchronous processes.
        /// </summary>
        private readonly AutoResetEvent _syncProcessAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Auto reset event for polling the queue to be empty.
        /// </summary>
        private readonly AutoResetEvent _processQueueDequeuedAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Auto reset event for polling running processes.
        /// </summary>
        private readonly AutoResetEvent _waitForProcessesAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Process batch size for file watcher controller.
        /// </summary>
        private int _internalProcessBatchSize;

        /// <summary>
        /// Delayed processes.
        /// </summary>
        private readonly Dictionary<Guid, bool> _delayedProcesses = new Dictionary<Guid, bool>();

        /// <summary>
        /// Locked processes.
        /// </summary>
        private readonly Dictionary<Guid, int> _lockedProcesses = new Dictionary<Guid, int>();

        /// <summary>
        /// Holds file watcher configuration information.
        /// </summary>
        private readonly Dictionary<string, FileWatcherConfigurationSet> _configurationDictionary;

        /// <summary>
        /// Holds process batch size information.
        /// </summary>
        private readonly Dictionary<string, int> _processBatchSizeDictionary = new Dictionary<string, int>();

        /// <summary>
        /// Holds information about processes that are available to file watchers. 
        /// </summary>
        private readonly Dictionary<string, int> _availableProcessesDictionary = new Dictionary<string, int>();

        /// <summary>
        /// Contains synchronization context for running the events in the main thread.
        /// </summary>
        private SynchronizationContext _synchronizationContext;

        /// <summary>
        /// True if file watcher controller is stopping file watchers or
        /// waiting pending processes.
        /// </summary>
        private bool _isControllerStopping;

        /// <summary>
        /// True if to run and to wait for queued processes when file watcher controller is stopping.
        /// </summary>
        private bool _runQueuedProcesses;

        /// <summary>
        /// Lock object for queue trim interval.
        /// </summary>
        private readonly object _lockQueueTrimInterval = new object();

        /// <summary>
        /// Lock object for file watcher controller process batch size.
        /// </summary>
        private readonly object _lockProcessBatchSize = new object();

        /// <summary>
        /// Lock object for available process dictionary.
        /// </summary>
        private readonly object _lockAvailableProcessDictionary = new object();

        /// <summary>
        /// Lock object for process batch size dictionary.
        /// </summary>
        private readonly object _lockProcessBatchSizeDictionary = new object();

        /// <summary>
        /// Lock object for is controller stopping.
        /// </summary>
        private readonly object _lockIsControllerStopping = new object();

        /// <summary>
        /// Lock object for synchronous execution.
        /// </summary>
        private readonly object _lockSynchronousExecution = new object();

        /// <summary>
        /// Lock object for run queued processes.
        /// </summary>
        private readonly object _lockRunQueuedProcesses = new object();

        /// <summary>
        /// Lock object for exit process thread.
        /// </summary>
        private readonly object _lockExitProcessThread = new object();

        /// <summary>
        /// Lock object for running.
        /// </summary>
        private readonly object _lockRunning = new object();

        /// <summary>
        /// Lock object for process running.
        /// </summary>
        private readonly object _lockProcessRunning = new object();

        /// <summary>
        /// Lock object for process queued.
        /// </summary>
        private readonly object _lockProcessQueue = new object();

        /// <summary>
        /// Lock object for exterternal running processes.
        /// </summary>
        private readonly object _lockExternalRunningProcesses = new object();

        /// <summary>
        /// Contains process thread.
        /// </summary>
        private Thread _processThread;

        /// <summary>
        /// True if to exit process thread.
        /// </summary>
        private bool _exitProcessThread;

        /// <summary>
        /// Contains process queue.
        /// </summary>
        private readonly Queue<FileWatcherEventArgs> _processQueue = new Queue<FileWatcherEventArgs>();

        /// <summary>
        /// Contains state of file watchers loaded on file watcher controller.
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// Contains number of running processes.
        /// </summary>
        private int _runningProcesses;

        /// <summary>
        /// Contains number of running processes (external).
        /// </summary>
        private int _externalRunningProcesses;

        /// <summary>
        /// True if process is running.
        /// </summary>
        private bool _processRunning;

        /// <summary>
        /// True if all processes are run synchronously.
        /// </summary>
        private bool _synchronousExecution;

        /// <summary>
        /// Constains file watchers.
        /// </summary>
        private Dictionary<string, FileWatcher> _fileWatchers = new Dictionary<string, FileWatcher>();

        /// <summary>
        /// True if this is disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Windows native error constant.
        /// </summary>
        private const int ErrorFileNotFound = 2;

        /// <summary>
        /// Windows native error constant.
        /// </summary>
        private const int ErrorAccessDenied = 5;

        /// <summary>
        /// Windows native error constant.
        /// </summary>
        private const int ErrorUnknownUserNameOrBadPassword = 1326;

        /// <summary>
        /// Windows native error constant.
        /// </summary>
        private const int ErrorUserAccountRestriction = 1327;

        #endregion Private members
    }
}