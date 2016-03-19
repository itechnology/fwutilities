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

using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Runtime.Remoting.Messaging;
using FileWatcherUtilities.Controller.Properties;
using System.ComponentModel;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Monitors file system for changes.
    /// </summary>
    internal sealed class FileWatcher : IDisposable
    {
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
        /// Occurs when file system is changed.
        /// </summary>
        public event EventHandler<FileWatcherEventArgs> SystemChanged;

        /// <summary>
        /// Occurs when buffer overflows.
        /// </summary>
        public event EventHandler<FileWatcherBufferErrorEventArgs> BufferError;

        /// <summary>
        /// Occurs when file watcher file search encountered an error.
        /// </summary>
        public event EventHandler<FileWatcherSearchErrorEventArgs> FileWatcherSearchError;

        /// <summary>
        /// Occurs when file watcher file search advances to new directory.
        /// </summary>
        public event EventHandler<FileWatcherSearchProgressEventArgs> FileWatcherSearchProgress;

        /// <summary>
        /// Occurs when file watcher recycles file system watcher.
        /// </summary>
        public event EventHandler<FileWatcherRecycledEventArgs> FileWatcherRecycle;

        /// <summary>
        /// Initializes a new instance of the FileWatcher class.
        /// </summary>
        /// <param name="configurationKeyValuePair">Configuration KeyValuePair.</param>
        /// <exception cref="ArgumentNullException">fileWatcherConfigSet is null.</exception>
        public FileWatcher(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair)
        {
            if (configurationKeyValuePair.Value == null)
            {
                throw new ArgumentNullException("configurationKeyValuePair",
                                                Resources.ArgumentNullException);
            }

            _configurationKeyValuePair = configurationKeyValuePair;

            // Set regular expression filters.
            SetRegularExpressionFilters();

            // Set synchronization context for running events in main thread.
            SetSynchronizationContext();
        }

        /// <summary>
        /// Returns true if file watcher is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
        }

        /// <summary>
        /// Returns true if file watcher is starting.
        /// </summary>
        public bool IsStarting
        {
            get
            {
                return _isStarting;
            }
        }

        /// <summary>
        /// Gets the daemon name of the file watcher.
        /// </summary>
        public string DaemonName
        {
            get
            {
                return _configurationKeyValuePair.Key;
            }
        }

        /// <summary>
        /// Returns true if file watcher has running threads.
        /// </summary>
        /// <returns>Returns true if file watcher has running threads.</returns>
        public bool HasRunningThreads()
        {
            return ExitRemoveOldFiltersThread ||
                   ExitPollingThread ||
                   ExitEventInvocationThread;
        }

        /// <summary>
        /// Starts the file watcher.
        /// </summary>
        /// <returns>Returns true if file watcher was started successfully.</returns>
        /// <exception cref="InvalidOperationException">File watcher is already started.</exception>
        /// <exception cref="InvalidOperationException">File watcher is already starting.</exception>
        public bool StartFileWatcher()
        {
            // Check if file watcher is already running.
            if (IsRunning)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatcherAlreadyStarted);
            }
            // Check if file watcher is already starting.
            if (IsStarting)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatchersIsStarting);
            }
            // Prepare file watcher for starting.
            if (PrepareStart())
            {
                // If generate file system events at startup is selected.
                if (_configurationKeyValuePair.Value.GenerateFileSystemEventsAtStartup)
                {
                    // File watcher is started after the file search and
                    // event generation is completed.
                    StartSearchFilesAndGenerateEventsAsynchronous();
                }
                else
                {
                    // Start file watcher and set starting off.
                    StartFileSystemWatcher();
                }

                return true;
            }

            // Invalid path.
            return false;
        }

        /// <summary>
        /// Stops the file watcher.
        /// </summary>
        /// <exception cref="InvalidOperationException">File watcher is already stopped.</exception>
        /// <exception cref="InvalidOperationException">File watcher is starting.</exception>
        public void StopFileWatcher()
        {
            // Check if file watcher is already stopped.
            if (!IsRunning)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatcherAlreadyStopped);
            }
            // Check if file watcher is starting.
            if (IsStarting)
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileWatchersIsStarting);
            }
            // Stop polling thread.
            if (_configurationKeyValuePair.Value.PollDirectory ||
                _configurationKeyValuePair.Value.RecycleFileWatcher)
            {
                StopPollingThread();
            }

            // Disable file system watcher events.
            StopInvokeEventsThread();
            _fileSystemWatcher.EnableRaisingEvents = false;
            UnsubscribeFileSystemWatcherEvents(_fileSystemWatcher);
            _fileSystemWatcher.Dispose();

            // Stop remove old filters thread.
            if (_configurationKeyValuePair.Value.FilteredMode)
            {
                StopRemoveOldFiltersThread();
            }

            _isRunning = false;
        }

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
                if (_fileSystemWatcher != null)
                {
                    // Dispose wait handles.
                    _removeOldFiltersThreadAutoResetEvent.Close();
                    _pollingThreadAutoResetEvent.Close();
                    _eventInvocationThreadAutoResetEvent.Close();

                    _fileSystemWatcher.Dispose();
                    _fileSystemWatcher = null;
                }
            }
        }

        /// <summary>
        /// Prepares file watcher for starting.
        /// </summary>
        /// <returns>True if starting of the file watcher may continue.</returns>
        private bool PrepareStart()
        {
            // Set is starting to true.
            _isStarting = true;

            // Raise file watcher starting event.
            EventHandler<FileWatcherStartingEventArgs> startingHandler = FileWatcherStarting;
            if (startingHandler != null)
            {
                startingHandler(this,
                                new FileWatcherStartingEventArgs(DaemonName));
            }

            try
            {
                _fileSystemWatcher =
                    new FileSystemWatcher(_configurationKeyValuePair.Value.Path,
                                          _configurationKeyValuePair.Value.Filter);
            }
            catch (ArgumentException) // Invalid path.
            {
                // Set starting to false.
                _isStarting = false;

                // Raise file watcher stopped event.
                EventHandler<FileWatcherStoppedEventArgs> stoppedHandler = FileWatcherStopped;
                if (stoppedHandler != null)
                {
                    stoppedHandler(this,
                                   new FileWatcherStoppedEventArgs(DaemonName));
                }

                // Cancel start of the file watcher.
                return false;
            }

            ConfigureFileSystemWatcher();
            SubscribeToFileSystemWatcherEvents();

            // Continue starting of file watcher.
            return true;
        }

        /// <summary>
        /// Starts file watcher and sets starting off.
        /// </summary>
        private void StartFileSystemWatcher()
        {
            // Set starting off.
            _isStarting = false;
            _isRunning = true;

            // Raise file watcher started event.
            EventHandler<FileWatcherStartedEventArgs> startedHandler = FileWatcherStarted;
            if (startedHandler != null)
            {
                startedHandler(this,
                               new FileWatcherStartedEventArgs(DaemonName));
            }

            _lastErrors = new List<FileWatcherBufferErrorEventArgs>();

            // Enable file system watcher events.
            StartInvokeEventsThread();

            Exception exception = null;

            // Start file watcher.
            try
            {
                _fileSystemWatcher.EnableRaisingEvents = true;
            }
            catch (ArgumentException ex) // Invalid path.
            {
                exception = ex;
            }
            catch (FileNotFoundException ex) // Directory not found.
            {
                exception = ex;
            }

            // Start remove old filters thread.
            if (_configurationKeyValuePair.Value.FilteredMode)
            {
                StartRemoveOldFiltersThread();
            }

            // Raise buffer error if EnableRaisingEvents failed.
            EventHandler<FileWatcherBufferErrorEventArgs> bufferHandler = BufferError;
            if (bufferHandler != null &&
                exception != null)
            {
                bufferHandler(this,
                              new FileWatcherBufferErrorEventArgs(DaemonName,
                                                                  new ErrorEventArgs(exception)));
            }
            if (_configurationKeyValuePair.Value.RecycleFileWatcher)
            {
                _nextRecycle = CalculateTimeout(_configurationKeyValuePair.Value.RecycleInterval);
                StartPollingThread();
            }
            else if (_configurationKeyValuePair.Value.PollDirectory)
            {
                StartPollingThread();
            }
        }

        /// <summary>
        /// Starts file searching and event generation asynchronously.
        /// </summary>
        private void StartSearchFilesAndGenerateEventsAsynchronous()
        {
            MethodDelegate methodDelegate =
                new MethodDelegate(SearchFilesAndGenerateEvents);
            methodDelegate.BeginInvoke(new AsyncCallback(StartSearchFilesAndGenerateEventsCallback),
                                       null);
        }

        /// <summary>
        /// Callback for asynchronously started file search and event generation.
        /// </summary>
        /// <param name="iAsyncResult">Status of asynchronous operation.</param>
        private void StartSearchFilesAndGenerateEventsCallback(IAsyncResult iAsyncResult)
        {
            // Get result delegate.
            MethodDelegate resultDelegate = ((MethodDelegate)
                ((AsyncResult)iAsyncResult).AsyncDelegate);

            // Run in main thread.
            _synchronizationContext.Send(new SendOrPostCallback(delegate
            {
                // End invoking.
                resultDelegate.EndInvoke(iAsyncResult);
                // Start file watcher and set starting off.
                StartFileSystemWatcher();
            }), null);
        }

        /// <summary>
        /// Searches files and generates events for each found file.
        /// </summary>
        private void SearchFilesAndGenerateEvents()
        {
            // Store DateTime to enable polling of files that are created or changed during the directory scan.
            _lastKnownGoodPollTime = DateTime.Now;

            FileSystemSearcher fileSystemSearcher = new FileSystemSearcher();

            // Subscribe to events.
            if (_configurationKeyValuePair.Value.LogFileSystemSearchError ||
                _configurationKeyValuePair.Value.DisplayFileSystemSearchError)
            {
                fileSystemSearcher.OnFileSystemSearchError +=
                    new EventHandler<FileSystemSearcherErrorEventArgs>(OnFileSystemSearcherError);
            }
            if (_configurationKeyValuePair.Value.LogFileSystemSearchProgress ||
                _configurationKeyValuePair.Value.DisplayFileSystemSearchProgress)
            {
                fileSystemSearcher.OnFileSystemSearchProgress +=
                    new EventHandler<FileSystemSearcherProgressEventArgs>(OnFileSystemSearcherProgress);
            }

            // Configuration holds a valid regular expression.
            fileSystemSearcher.Search(_configurationKeyValuePair.Value.Path,
                                      _configurationKeyValuePair.Value.IncludeSubdirectories,
                                      _configurationKeyValuePair.Value.GeneratedEventFileNameRegularExpressionFilter);

            // Get search results.
            Collection<FileInfo> foundFiles = fileSystemSearcher.FoundFiles;

            // Raise system generated event for each found file.
            foreach (FileInfo fileInfo in foundFiles)
            {
                RaiseSystemGeneratedEvent(fileInfo.Name,
                                          fileInfo.FullName);
            }
        }

        /// <summary>
        /// Configures the file watcher.
        /// </summary>
        private void ConfigureFileSystemWatcher()
        {
            _fileSystemWatcher.NotifyFilter =
                _configurationKeyValuePair.Value.NotifyFilters;

            _fileSystemWatcher.InternalBufferSize =
                _configurationKeyValuePair.Value.InternalBufferSize;

            _fileSystemWatcher.IncludeSubdirectories =
                _configurationKeyValuePair.Value.IncludeSubdirectories;
        }

        /// <summary>
        /// Subscribes the file watcher to the file system watcher events.
        /// </summary>
        private void SubscribeToFileSystemWatcherEvents()
        {
            if (_configurationKeyValuePair.Value.SubscribeToChangedEvent)
            {
                _fileSystemWatcher.Changed +=
                    new FileSystemEventHandler(OnChanged);
            }
            if (_configurationKeyValuePair.Value.SubscribeToCreatedEvent)
            {
                _fileSystemWatcher.Created +=
                    new FileSystemEventHandler(OnCreated);
            }
            if (_configurationKeyValuePair.Value.SubscribeToDeletedEvent)
            {
                _fileSystemWatcher.Deleted +=
                    new FileSystemEventHandler(OnDeleted);
            }
            if (_configurationKeyValuePair.Value.SubscribeToRenamedEvent)
            {
                _fileSystemWatcher.Renamed +=
                    new RenamedEventHandler(OnRenamed);
            }
            if (_configurationKeyValuePair.Value.SubscribeToBufferErrorEvent)
            {
                _fileSystemWatcher.Error +=
                    new System.IO.ErrorEventHandler(OnBufferError);
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
        /// Sets regular expression filters.
        /// </summary>
        private void SetRegularExpressionFilters()
        {
            if (!String.IsNullOrEmpty(_configurationKeyValuePair.Value.ChangedRegularExpressionFilter))
            {
                _regexFilterChanged =
                    new Regex(_configurationKeyValuePair.Value.ChangedRegularExpressionFilter);
            }
            if (!String.IsNullOrEmpty(_configurationKeyValuePair.Value.CreatedRegularExpressionFilter))
            {
                _regexFilterCreated =
                    new Regex(_configurationKeyValuePair.Value.CreatedRegularExpressionFilter);
            }
            if (!String.IsNullOrEmpty(_configurationKeyValuePair.Value.DeletedRegularExpressionFilter))
            {
                _regexFilterDeleted =
                    new Regex(_configurationKeyValuePair.Value.DeletedRegularExpressionFilter);
            }
            if (!String.IsNullOrEmpty(_configurationKeyValuePair.Value.RenamedRegularExpressionFilter))
            {
                _regexFilterRenamed =
                    new Regex(_configurationKeyValuePair.Value.RenamedRegularExpressionFilter);
            }
        }

        /// <summary>
        /// Handles file system searcher error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileSystemSearcherError(object sender,
                                               FileSystemSearcherErrorEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherSearchErrorEventArgs> handler = FileWatcherSearchError;
                if (handler != null)
                {
                    handler(this,
                            new FileWatcherSearchErrorEventArgs(DaemonName,
                                                                e.Exception,
                                                                _configurationKeyValuePair.Value.DisplayFileSystemSearchError,
                                                                _configurationKeyValuePair.Value.LogFileSystemSearchError));
                }
            }));
        }

        /// <summary>
        /// Handles file system searcher progress event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnFileSystemSearcherProgress(object sender,
                                                  FileSystemSearcherProgressEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherSearchProgressEventArgs> handler = FileWatcherSearchProgress;
                if (handler != null)
                {
                    handler(this,
                            new FileWatcherSearchProgressEventArgs(DaemonName,
                                                                   e.DirectoryFullPath,
                                                                   _configurationKeyValuePair.Value.DisplayFileSystemSearchProgress,
                                                                   _configurationKeyValuePair.Value.LogFileSystemSearchProgress));
                }
            }));
        }

        /// <summary>
        /// Handles changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnChanged(object sender,
                               FileSystemEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherEventArgs> handler = SystemChanged;
                if (handler != null &&
                    IsMatchFilterChangedEvent(e) &&
                    !FilterEvent(e.FullPath, WatcherChangeTypes.Changed))
                {
                    handler(this,
                            new FileWatcherEventArgs(_configurationKeyValuePair,
                                                     e.ChangeType.ToString(),
                                                     e.Name,
                                                     e.FullPath,
                                                     String.Empty));
                }
            }));
        }

        /// <summary>
        /// Matches regular expression filter to full path.
        /// </summary>
        /// <param name="e">Event data.</param>
        /// <returns>True if regular expression finds a match in the full path string.</returns>
        /// <remarks>Null regular expression returns always true.</remarks>
        private bool IsMatchFilterChangedEvent(FileSystemEventArgs e)
        {
            // No filtering.
            if (_regexFilterChanged == null)
            {
                return true;
            }
            return (_regexFilterChanged.IsMatch(e.FullPath));
        }

        /// <summary>
        /// Handles created event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnCreated(object sender,
                               FileSystemEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherEventArgs> handler = SystemChanged;
                if (handler != null &&
                    IsMatchFilterCreadtedEvent(e) &&
                    !FilterEvent(e.FullPath, WatcherChangeTypes.Created))
                {
                    handler(this,
                             new FileWatcherEventArgs(_configurationKeyValuePair,
                                                      e.ChangeType.ToString(),
                                                      e.Name,
                                                      e.FullPath,
                                                      String.Empty));
                }
            }));
        }

        /// <summary>
        /// Matches regular expression filter to full path.
        /// </summary>
        /// <param name="e">Event data.</param>
        /// <returns>True if regular expression finds a match in the full path string.</returns>
        /// <remarks>Null regular expression returns always true.</remarks>
        private bool IsMatchFilterCreadtedEvent(FileSystemEventArgs e)
        {
            // No filtering.
            if (_regexFilterCreated == null)
            {
                return true;
            }
            return (_regexFilterCreated.IsMatch(e.FullPath));
        }

        /// <summary>
        /// Handles deleted event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnDeleted(object sender,
                               FileSystemEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherEventArgs> handler = SystemChanged;
                if (handler != null &&
                    IsMatchFilterDeletedEvent(e) &&
                    !FilterEvent(e.FullPath, WatcherChangeTypes.Deleted))
                {
                    handler(this,
                            new FileWatcherEventArgs(_configurationKeyValuePair,
                                                     e.ChangeType.ToString(),
                                                     e.Name,
                                                     e.FullPath,
                                                     String.Empty));

                }
            }));
        }

        /// <summary>
        /// Matches regular expression filter to full path.
        /// </summary>
        /// <param name="e">Event data.</param>
        /// <returns>True if regular expression finds a match in the full path string.</returns>
        /// <remarks>Null regular expression returns always true.</remarks>
        private bool IsMatchFilterDeletedEvent(FileSystemEventArgs e)
        {
            // No filtering.
            if (_regexFilterDeleted == null)
            {
                return true;
            }
            return (_regexFilterDeleted.IsMatch(e.FullPath));
        }

        /// <summary>
        /// Handles renamed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnRenamed(object sender,
                               RenamedEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherEventArgs> handler = SystemChanged;
                if (handler != null &&
                    IsMatchFilterRenamedEvent(e))
                {
                    handler(this,
                            new FileWatcherEventArgs(_configurationKeyValuePair,
                                                     e.ChangeType.ToString(),
                                                     e.Name,
                                                     e.FullPath,
                                                     e.OldFullPath));
                }
            }));
        }

        /// <summary>
        /// Matches regular expression filter to full path.
        /// </summary>
        /// <param name="e">Event data.</param>
        /// <returns>True if regular expression finds a match in the full path string.</returns>
        /// <remarks>Null regular expression returns always true.</remarks>
        private bool IsMatchFilterRenamedEvent(FileSystemEventArgs e)
        {
            // No filtering.
            if (_regexFilterRenamed == null)
            {
                return true;
            }
            return (_regexFilterRenamed.IsMatch(e.FullPath));
        }

        /// <summary>
        /// Handles buffer error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnBufferError(object sender,
                                   ErrorEventArgs e)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherBufferErrorEventArgs> handler = BufferError;
                if (handler != null)
                {
                    FileWatcherBufferErrorEventArgs fileWatcherBufferErrorEventArgs =
                        new FileWatcherBufferErrorEventArgs(_configurationKeyValuePair.Key,
                                                            e);
                    AddError(fileWatcherBufferErrorEventArgs);
                    handler(this,
                            fileWatcherBufferErrorEventArgs);
                }
            }));
        }

        /// <summary>
        /// Raises system generated event.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="fullPath">File full path.</param>
        private void RaiseSystemGeneratedEvent(string fileName,
                                               string fullPath)
        {
            AddEventInvocation(new SendOrPostCallback(delegate
            {
                EventHandler<FileWatcherEventArgs> handler = SystemChanged;
                if (handler != null)
                {
                    FileWatcherEventArgs fileWatcherEventArgs =
                        new FileWatcherEventArgs(_configurationKeyValuePair,
                                                 Resources.FileSystemChangeTypeSystemGenerated,
                                                 fileName,
                                                 fullPath,
                                                 String.Empty);
                    handler(this,
                            fileWatcherEventArgs);
                }
            }));
        }

        /// <summary>
        /// Stops remove old filters thread.
        /// </summary>
        private void StopRemoveOldFiltersThread()
        {
            ExitRemoveOldFiltersThread = true;
            _removeOldFiltersThreadAutoResetEvent.Set();
            _removeOldFiltersThread = null;
        }

        /// <summary>
        /// Starts remove old filters thread.
        /// </summary>
        private void StartRemoveOldFiltersThread()
        {
            _removeOldFiltersThread = new Thread(new ThreadStart(RemoveOldEventFiltersThreading));
            _removeOldFiltersThread.Name = "RemoveOldEventsThread";
            _removeOldFiltersThread.Start();
        }

        /// <summary>
        /// Gets or sets exit remove old filters thread.
        /// </summary>
        private bool ExitRemoveOldFiltersThread
        {
            get
            {
                lock (_lockExitRemoveOldFiltersThread)
                {
                    return _exitRemoveOldFiltersThread;
                }
            }
            set
            {
                lock (_lockExitRemoveOldFiltersThread)
                {
                    _exitRemoveOldFiltersThread = value;
                }
            }
        }

        /// <summary>
        /// Removes old items in the file watcher event filter collections.
        /// </summary>
        private void RemoveOldEventFiltersThreading()
        {
            const int FastPolling = 5;
            const int SlowPolling = 60000;

            bool removed = false;

            while (!ExitRemoveOldFiltersThread)
            {
                if (DoRemoveOldFilters(_changedCollection, _lockChangedCollection))
                {
                    removed = true;
                }
                if (DoRemoveOldFilters(_createdCollection, _lockCreatedCollection))
                {
                    removed = true;
                }
                if (DoRemoveOldFilters(_deletedCollection, _lockDeletedCollection))
                {
                    removed = true;
                }
                // If something was removed then poll again fast.
                if (removed)
                {
                    removed = false;
                    _removeOldFiltersThreadAutoResetEvent.WaitOne(FastPolling);
                }
                else // Slow polling.
                {
                    _removeOldFiltersThreadAutoResetEvent.WaitOne(SlowPolling);
                }
            }

            ExitRemoveOldFiltersThread = false;
        }

        /// <summary>
        /// Removes old items in the file watcher event filter collection.
        /// Retunrs true if filter was removed.
        /// </summary>
        /// <param name="collection">Collection to remove old filters.</param>
        /// <param name="lockObject">Collection lock object.</param>
        /// <returns>True if filter was removed.</returns>
        private static bool DoRemoveOldFilters(EventFilterCollection collection,
                                               object lockObject)
        {
            bool removed = false;

            lock (lockObject)
            {
                // If something to do.
                if (collection.Count != 0)
                {
                    // If timeout has elapsed then remove filter.
                    if (collection[0].Timeout < DateTime.Now)
                    {
                        collection.RemoveAt(0);
                        removed = true;
                    }
                }
            }

            return removed;
        }

        /// <summary>
        /// Checks if the changed, created or deleted event should be filtered.
        /// Renamed events are not filtered.
        /// </summary>
        /// <param name="fullPath">Full path.</param>
        /// <param name="changeType">Type of the change.</param>
        /// <returns>Returns true if the event should be filtered.</returns>
        private bool FilterEvent(string fullPath,
                                 WatcherChangeTypes changeType)
        {
            // If filtered mode is not enabled.
            if (!_configurationKeyValuePair.Value.FilteredMode)
            {
                return false;
            }
            if (changeType == WatcherChangeTypes.Changed)
            {
                return DoFilterEvent(fullPath, _changedCollection, _lockChangedCollection);
            }
            if (changeType == WatcherChangeTypes.Created)
            {
                return DoFilterEvent(fullPath, _createdCollection, _lockCreatedCollection);
            }
            if (changeType == WatcherChangeTypes.Deleted)
            {
                return DoFilterEvent(fullPath, _deletedCollection, _lockDeletedCollection);
            }

            return false;
        }

        /// <summary>
        /// Return true if event should be filtered.
        /// </summary>
        /// <param name="fullPath">Full path.</param>
        /// <param name="collection">Filter collection.</param>
        /// <param name="lockObject">Collection lock object.</param>
        /// <returns>True if created event should be filtered.</returns>
        private bool DoFilterEvent(string fullPath,
                                   EventFilterCollection collection,
                                   object lockObject)
        {
            bool filterEvent = false;

            lock (lockObject)
            {
                if (collection.Contains(fullPath))
                {
                    // If timeout has elapsed, add a new timeout and show event.
                    if (collection[fullPath].Timeout < DateTime.Now)
                    {
                        collection.Remove(fullPath);
                        collection.Add(
                            new EventFilter(fullPath,
                                            CalculateTimeout(_configurationKeyValuePair.Value.FilteredModeFilterTimeout)));
                    }
                    else // Filter event.
                    {
                        filterEvent = true;
                    }
                }
                else // Add new and show event.
                {
                    collection.Add(
                        new EventFilter(fullPath,
                                        CalculateTimeout(_configurationKeyValuePair.Value.FilteredModeFilterTimeout)));
                }
            }

            return filterEvent;
        }

        /// <summary>
        /// Calculates timeout.
        /// </summary>
        /// <param name="timeoutInMinutes">Timeout in minutes.</param>
        /// <returns>Calculated timeout.</returns>
        private DateTime CalculateTimeout(int timeoutInMinutes)
        {
            DateTime now = DateTime.Now;

            // If timeout is too large return maximum value.
            if (now > DateTime.MaxValue.AddMinutes(timeoutInMinutes * -1))
            {
                return DateTime.MaxValue;
            }

            return now.AddMinutes(timeoutInMinutes);
        }

        #region Nested classes

        /// <summary>
        /// KeyedCollection for event filters.
        /// </summary>
        public class EventFilterCollection : KeyedCollection<string, EventFilter>
        {
            /// <summary>
            /// Extracts key for dictionary.
            /// </summary>
            /// <param name="item">Item to extract key from.</param>
            /// <returns>Key for the item.</returns>
            protected override string GetKeyForItem(EventFilter item)
            {
                return item.FullPath;
            }
        }

        /// <summary>
        /// File system event filter.
        /// </summary>
        public class EventFilter
        {
            /// <summary>
            /// Filter full path.
            /// </summary>
            private readonly string _internalFullPath;

            /// <summary>
            /// Filter timeout.
            /// </summary>
            private readonly DateTime _internalTimeout;

            /// <summary>
            /// Initializes a new instance of the EventFilter class.
            /// </summary>
            /// <param name="fullPath">Full path.</param>
            /// <param name="timeout">Filter timeout.</param>
            /// <exception cref="ArgumentNullException">fullPath is null.</exception>
            public EventFilter(string fullPath,
                               DateTime timeout)
            {
                if (fullPath == null)
                {
                    throw new ArgumentNullException("fullPath",
                                                    Resources.ArgumentNullException);
                }

                _internalFullPath = fullPath;
                _internalTimeout = timeout;
            }

            /// <summary>
            /// Gets filter timeout.
            /// </summary>
            public DateTime Timeout
            {
                get
                {
                    return _internalTimeout;
                }
            }

            /// <summary>
            /// Gets filter full path.
            /// </summary>
            public string FullPath
            {
                get
                {
                    return _internalFullPath;
                }
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets exit polling thread.
        /// </summary>
        private bool ExitPollingThread
        {
            get
            {
                lock (_lockExitPollingThread)
                {
                    return _exitPollingThread;
                }
            }
            set
            {
                lock (_lockExitPollingThread)
                {
                    _exitPollingThread = value;
                }
            }
        }

        /// <summary>
        /// Starts polling thread.
        /// </summary>
        private void StartPollingThread()
        {
            _pollingThread = new Thread(new ThreadStart(PollDirectoryThreading));
            _pollingThread.Name = "PollingThread";
            _pollingThread.Start();
        }

        /// <summary>
        /// Polls watched directory.
        /// </summary>
        private void PollDirectoryThreading()
        {
            const int Interval = 60000;
            RecycleReason recycleReason = RecycleReason.None;
            int directoryPollMinutes = 0;

            if (_lastKnownGoodPollTime == DateTime.MinValue)
            {
                _lastKnownGoodPollTime = DateTime.Now;
            }

            while (!ExitPollingThread)
            {
                if (_configurationKeyValuePair.Value.RecycleFileWatcher)
                {
                    // Polling may produce duplicate events if directory is renamed.
                    bool directoryExists = Directory.Exists(_configurationKeyValuePair.Value.Path);

                    // Check if directory is still there.
                    if (!directoryExists)
                    {
                        _recycleFileSystemWatcher = true;
                        recycleReason = RecycleReason.DirectoryNotFound;
                    }
                    else
                    {
                        // Check if there are any file system watcher errors.
                        if (GetErrorCount() > 0 &&
                            HandleErrors())
                        {
                            _recycleFileSystemWatcher = true;
                            if (recycleReason == RecycleReason.None)
                            {
                                recycleReason = RecycleReason.Error;
                            }
                        }
                        // If time to recycle the file system watcher.
                        if (_nextRecycle < DateTime.Now)
                        {
                            _recycleFileSystemWatcher = true;
                            _nextRecycle = CalculateTimeout(_configurationKeyValuePair.Value.RecycleInterval);
                            if (recycleReason == RecycleReason.None)
                            {
                                recycleReason = RecycleReason.Recycle;
                            }
                        }
                        if (_recycleFileSystemWatcher)
                        {
                            _recycleFileSystemWatcher = !RecycleFileSystemWatcher(recycleReason);
                            if (!_recycleFileSystemWatcher)
                            {
                                recycleReason = RecycleReason.None;
                            }
                        }
                    }
                }
                if (_configurationKeyValuePair.Value.PollDirectory &&
                    directoryPollMinutes == _configurationKeyValuePair.Value.DirectoryPollInterval)
                {
                    // Poll for files that may have been missed by the file watcher.
                    SearchFilesAndGenerateEventsFiltered();
                    directoryPollMinutes = 0;
                }
                if (!ExitEventInvocationThread)
                {
                    _pollingThreadAutoResetEvent.WaitOne(Interval);
                }
                directoryPollMinutes++;
            }

            _lastKnownGoodPollTime = DateTime.MinValue;
            ExitPollingThread = false;            
        }

        /// <summary>
        /// Recycles file system watcher.
        /// </summary>
        /// <param name="recycleReason">Recycle reason.</param>
        /// <returns>True if file system watcher was created successfully.</returns>
        private bool RecycleFileSystemWatcher(RecycleReason recycleReason)
        {
            FileSystemWatcher tempFileSystemWatcher;

            try
            {
                tempFileSystemWatcher =
                    new FileSystemWatcher(_configurationKeyValuePair.Value.Path,
                                          _configurationKeyValuePair.Value.Filter);
            }
            catch (ArgumentException) // Invalid path.
            {
                return false;
            }

            if (tempFileSystemWatcher != null)
            {
                FileSystemWatcher tempOldFileSystemWatcher = _fileSystemWatcher;
                tempOldFileSystemWatcher.EnableRaisingEvents = false;
                UnsubscribeFileSystemWatcherEvents(tempOldFileSystemWatcher);
                tempOldFileSystemWatcher.Dispose();

                _fileSystemWatcher = tempFileSystemWatcher;
                ConfigureFileSystemWatcher();
                SubscribeToFileSystemWatcherEvents();

                try
                {
                    _fileSystemWatcher.EnableRaisingEvents = true;
                }
                catch (ArgumentException) // Invalid path.
                {
                    return false;
                }
                catch (FileNotFoundException) // Directory not found.
                {
                    return false;
                }
            }

            AddEventInvocation(new SendOrPostCallback(delegate 
            {
                EventHandler<FileWatcherRecycledEventArgs> handler = FileWatcherRecycle;
                if (handler != null)
                {
                    handler(this,
                            new FileWatcherRecycledEventArgs(_configurationKeyValuePair.Key,
                                                             recycleReason));
                }
            }));

            return true;
        }

        /// <summary>
        /// Searches files and generates events for each found file that has last write time or created time
        /// larger than the last known good poll time. Uses changed and created filters to filter events.
        /// </summary>
        private void SearchFilesAndGenerateEventsFiltered()
        {
            // If not interested in changed and created files do nothing.
            if (!_configurationKeyValuePair.Value.SubscribeToChangedEvent &&
                !_configurationKeyValuePair.Value.SubscribeToCreatedEvent)
            {
                return;
            }

            FileSystemSearcher fileSystemSearcher = new FileSystemSearcher();

            // Subscribe to events.
            if (_configurationKeyValuePair.Value.LogFileSystemSearchError ||
                _configurationKeyValuePair.Value.DisplayFileSystemSearchError)
            {
                fileSystemSearcher.OnFileSystemSearchError +=
                    new EventHandler<FileSystemSearcherErrorEventArgs>(OnFileSystemSearcherError);
            }
            if (_configurationKeyValuePair.Value.LogFileSystemSearchProgress ||
                _configurationKeyValuePair.Value.DisplayFileSystemSearchProgress)
            {
                fileSystemSearcher.OnFileSystemSearchProgress +=
                    new EventHandler<FileSystemSearcherProgressEventArgs>(OnFileSystemSearcherProgress);
            }

            // Configuration holds a valid regular expression.
            fileSystemSearcher.Search(_configurationKeyValuePair.Value.Path,
                                      _configurationKeyValuePair.Value.IncludeSubdirectories,
                                      _configurationKeyValuePair.Value.GeneratedEventFileNameRegularExpressionFilter);

            // Get search results.
            Collection<FileInfo> foundFiles = fileSystemSearcher.FoundFiles;
            DateTime now = DateTime.Now;

            foreach (FileInfo fileInfo in foundFiles)
            {
                if (ExitPollingThread)
                {
                    break;
                }

                bool filterEvent = true;

                // Filter poll results for duplicate events.
                if (fileInfo.LastWriteTime > _lastKnownGoodPollTime &&
                    _configurationKeyValuePair.Value.SubscribeToChangedEvent)
                {
                    filterEvent = FilterEvent(fileInfo.FullName, WatcherChangeTypes.Changed);
                }
                else if (fileInfo.CreationTime > _lastKnownGoodPollTime &&
                         _configurationKeyValuePair.Value.SubscribeToCreatedEvent)
                {
                    filterEvent = FilterEvent(fileInfo.FullName, WatcherChangeTypes.Created);
                }
                // If event is not filtered or there was no filter at all raise event.
                if (!filterEvent)
                {
                    RaiseSystemGeneratedEvent(fileInfo.Name,
                                              fileInfo.FullName);
                }
            }

            _lastKnownGoodPollTime = now;
        }

        /// <summary>
        /// Unsubscribes file system watcher events.
        /// </summary>
        /// <param name="fileSystemWatcher">File system watcher to unsubscribe.</param>
        private void UnsubscribeFileSystemWatcherEvents(FileSystemWatcher fileSystemWatcher)
        {
            if (_configurationKeyValuePair.Value.SubscribeToChangedEvent)
            {
                fileSystemWatcher.Changed -=
                    new FileSystemEventHandler(OnChanged);
            }
            if (_configurationKeyValuePair.Value.SubscribeToCreatedEvent)
            {
                fileSystemWatcher.Created -=
                    new FileSystemEventHandler(OnCreated);
            }
            if (_configurationKeyValuePair.Value.SubscribeToDeletedEvent)
            {
                fileSystemWatcher.Deleted -=
                    new FileSystemEventHandler(OnDeleted);
            }
            if (_configurationKeyValuePair.Value.SubscribeToRenamedEvent)
            {
                fileSystemWatcher.Renamed -=
                    new RenamedEventHandler(OnRenamed);
            }
            if (_configurationKeyValuePair.Value.SubscribeToBufferErrorEvent)
            {
                fileSystemWatcher.Error -=
                    new System.IO.ErrorEventHandler(OnBufferError);
            }
        }

        /// <summary>
        /// Stops polling thread.
        /// </summary>
        private void StopPollingThread()
        {
            ExitPollingThread = true;
            _pollingThreadAutoResetEvent.Set();
            _pollingThread = null;
        }

        /// <summary>
        /// Adds last error.
        /// </summary>
        /// <param name="fileWatcherBufferErrorEventArgs">File watcher buffer error event args.</param>
        private void AddError(FileWatcherBufferErrorEventArgs fileWatcherBufferErrorEventArgs)
        {
            if (!_configurationKeyValuePair.Value.RecycleFileWatcher)
            {
                return;
            }

            lock (_lockLastErrors)
            {
                _lastErrors.Add(fileWatcherBufferErrorEventArgs);
            }
        }

        /// <summary>
        /// Gets error count.
        /// </summary>
        /// <returns>Error count.</returns>
        private int GetErrorCount()
        {
            lock (_lockLastErrors)
            {
                return _lastErrors.Count;
            }
        }

        /// <summary>
        /// Handles file system watcher errors.
        /// </summary>
        /// <returns>True if the file system watcher should be recycled.</returns>
        private bool HandleErrors()
        {
            List<FileWatcherBufferErrorEventArgs> errors = new List<FileWatcherBufferErrorEventArgs>();

            lock (_lockLastErrors)
            {
                while (_lastErrors.Count > 0)
                {
                    errors.Add(_lastErrors[0]);
                    _lastErrors.RemoveAt(0);
                }
            }

            foreach (FileWatcherBufferErrorEventArgs error in errors)
            {
                Exception exception = error.ErrorEventArgs.GetException();
                Win32Exception win32Exception = exception as Win32Exception;

                if (win32Exception != null)
                {
                    switch (win32Exception.NativeErrorCode)
                    {
                        case ErrorInvalidFunction:
                            return false;
                        default:
                            return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Gets or sets exit event invocation thread.
        /// </summary>
        private bool ExitEventInvocationThread
        {
            get
            {
                lock (_lockExitEventInvocationThread)
                {
                    return _exitEventInvocationThread;
                }
            }
            set
            {
                lock (_lockExitEventInvocationThread)
                {
                    _exitEventInvocationThread = value;
                }
            }
        }

        /// <summary>
        /// Starts invoke events thread.
        /// </summary>
        private void StartInvokeEventsThread()
        {
            _eventInvocationThread = new Thread(new ThreadStart(InvokeEventsThreading));
            _eventInvocationThread.Name = "EventInvocationThread";
            _eventInvocationThread.Start();
        }

        /// <summary>
        /// Stops invoke events thread.
        /// </summary>
        private void StopInvokeEventsThread()
        {
            ExitEventInvocationThread = true;
            _eventInvocationThreadAutoResetEvent.Set();
            _eventInvocationThread = null;
        }

        /// <summary>
        /// Invokes events.
        /// </summary>
        private void InvokeEventsThreading()
        {
            while (!ExitEventInvocationThread)
            {
                bool waitOne = false;

                lock (_lockEventInvocationQueue)
                {
                    if (_eventInvocationQueue.Count > 0)
                    {
                        // Run threaded event in main thread.
                        _synchronizationContext.Send(_eventInvocationQueue.Dequeue(), null);
                    }
                    else
                    {
                        waitOne = true;
                    }
                }

                if (!ExitEventInvocationThread &&
                    waitOne)
                {
                    _eventInvocationThreadAutoResetEvent.WaitOne();
                }
            }
            
            // Raise file watcher stopped event.
            EventHandler<FileWatcherStoppedEventArgs> handler = FileWatcherStopped;
            if (handler != null)
            {
                // Run threaded event in main thread.
                _synchronizationContext.Send(new SendOrPostCallback(delegate
                {
                    handler(this,
                            new FileWatcherStoppedEventArgs(DaemonName));
                }), null);
            }            

            // Wait other threads to terminate.
            while (ExitPollingThread || ExitRemoveOldFiltersThread)
            {
                Thread.Sleep(50);
            }

            // Clear invocation list.
            _eventInvocationQueue.Clear();
            _eventInvocationQueue.TrimExcess();
            // Clear collections (shared across threads).
            _createdCollection = new EventFilterCollection();
            _changedCollection = new EventFilterCollection();
            _deletedCollection = new EventFilterCollection();
            ExitEventInvocationThread = false;
        }

        /// <summary>
        /// Adds event to invocation queue.
        /// </summary>
        /// <param name="sendOrPostCallback">Delegate to invoke event.</param>
        private void AddEventInvocation(SendOrPostCallback sendOrPostCallback)
        {
            lock (_lockEventInvocationQueue)
            {
                _eventInvocationQueue.Enqueue(sendOrPostCallback);
                _eventInvocationThreadAutoResetEvent.Set();
            }
        }

        /// <summary>
        /// Event invocation queue.
        /// </summary>
        private readonly Queue<SendOrPostCallback> _eventInvocationQueue = new Queue<SendOrPostCallback>();

        /// <summary>
        /// Lock object for event invokation queue.
        /// </summary>
        private readonly object _lockEventInvocationQueue = new object();

        /// <summary>
        /// Contains event invocation thread.
        /// </summary>
        private Thread _eventInvocationThread;

        /// <summary>
        /// Auto reset event for event invocation thread.
        /// </summary>
        private readonly AutoResetEvent _eventInvocationThreadAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Lock object for exit event invocation thread.
        /// </summary>
        private readonly object _lockExitEventInvocationThread = new object();

        /// <summary>
        /// True if to exit event invocation thread.
        /// </summary>
        private bool _exitEventInvocationThread;

        /// <summary>
        /// File system watcher cannot do any work.
        /// </summary>
        private const int ErrorInvalidFunction = 1;

        /// <summary>
        /// Time of the next recycle of the file system watcher.
        /// </summary>
        private DateTime _nextRecycle;

        /// <summary>
        /// Lock object for last errors.
        /// </summary>
        private readonly object _lockLastErrors = new object();

        /// <summary>
        /// List of last errors.
        /// </summary>
        private List<FileWatcherBufferErrorEventArgs> _lastErrors;

        /// <summary>
        /// Last known good poll time.
        /// </summary>
        private DateTime _lastKnownGoodPollTime;

        /// <summary>
        /// True if file watcher is running.
        /// </summary>
        private bool _isRunning;

        /// <summary>
        /// True if directory was not found when polling.
        /// </summary>
        private bool _recycleFileSystemWatcher;

        /// <summary>
        /// Contains polling thread.
        /// </summary>
        private Thread _pollingThread;

        /// <summary>
        /// Lock object for exit polling thread.
        /// </summary>
        private readonly object _lockExitPollingThread = new object();

        /// <summary>
        /// Auto reset event for polling thread.
        /// </summary>
        private readonly AutoResetEvent _pollingThreadAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// True if to exit polling thread.
        /// </summary>
        private bool _exitPollingThread;

        /// <summary>
        /// Contains remove old filters thread.
        /// </summary>
        private Thread _removeOldFiltersThread;

        /// <summary>
        /// Auto reset event for remove old filters thread.
        /// </summary>
        private readonly AutoResetEvent _removeOldFiltersThreadAutoResetEvent = new AutoResetEvent(false);

        /// <summary>
        /// Lock object for exit remove old filters thread.
        /// </summary>
        private readonly object _lockExitRemoveOldFiltersThread = new object();

        /// <summary>
        /// True if to exit remove old filters thread.
        /// </summary>
        private bool _exitRemoveOldFiltersThread;

        /// <summary>
        /// Holds list of created files and folders and the timeout value.
        /// </summary>
        private EventFilterCollection _createdCollection = new EventFilterCollection();

        /// <summary>
        /// Lock object for created collection.
        /// </summary>
        private readonly object _lockCreatedCollection = new object();

        /// <summary>
        /// Holds list of changed files and folders and the timeout value.
        /// </summary>
        private EventFilterCollection _changedCollection = new EventFilterCollection();

        /// <summary>
        /// Lock object for changed collection.
        /// </summary>
        private readonly object _lockChangedCollection = new object();

        /// <summary>
        /// Holds list of deleted files and folders and the timeout value.
        /// </summary>
        private EventFilterCollection _deletedCollection = new EventFilterCollection();

        /// <summary>
        /// Lock object for deleted collection.
        /// </summary>
        private readonly object _lockDeletedCollection = new object();

        /// <summary>
        /// True if file watcher is starting.
        /// </summary>
        private bool _isStarting;

        /// <summary>
        /// Delegate for starting file search and event generation asynchronously.
        /// </summary>
        private delegate void MethodDelegate();

        /// <summary>
        /// Contains synchronization context for running the events in the main thread.
        /// </summary>
        private SynchronizationContext _synchronizationContext;

        /// <summary>
        /// Contains file watcher configuration set.
        /// </summary>
        private KeyValuePair<string, FileWatcherConfigurationSet> _configurationKeyValuePair;

        /// <summary>
        /// File system watcher.
        /// </summary>
        private FileSystemWatcher _fileSystemWatcher;

        /// <summary>
        /// Changed regular expression filter.
        /// </summary>
        private Regex _regexFilterChanged;

        /// <summary>
        /// Created regular expression filter.
        /// </summary>
        private Regex _regexFilterCreated;

        /// <summary>
        /// Deleted regular expression filter.
        /// </summary>
        private Regex _regexFilterDeleted;

        /// <summary>
        /// Renamed regular expression filter.
        /// </summary>
        private Regex _regexFilterRenamed;
    }
}