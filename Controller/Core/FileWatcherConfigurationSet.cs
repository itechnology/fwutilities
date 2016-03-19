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
using System.Text.RegularExpressions;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Provides file watcher configuration.
    /// </summary>
    public class FileWatcherConfigurationSet
    {
        /// <summary>
        /// Gets or sets filtered mode filter timeout in minutes.
        /// </summary>
        public int FilteredModeFilterTimeout
        {
            get
            {
                return _filterdModeFilterTimeout;
            }
            set
            {
                if (value > 0)
                {
                    _filterdModeFilterTimeout = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets filtered mode.
        /// </summary>
        public bool FilteredMode
        {
            get
            {
                return _filteredMode;
            }
            set
            {
                _filteredMode = value;
            }
        }

        /// <summary>
        /// Gets or sets if to call a service.
        /// </summary>
        /// <exception cref="ArgumentException">Call service and start process are both selected.</exception>
        /// <exception cref="ArgumentException">This version does not support calling services.</exception>
        public bool CallService
        {
            get
            {
                return _callService;
            }
            set
            {
#if (_NET_20)
                if (value == true)
                {
                    throw new ArgumentException(
                        Resources.ArgumentExceptionThisVersionDoesNotSupportCallingServices);
                }
#endif
                if (value && StartProcess)
                {
                    throw new ArgumentException(
                        Resources.ArgumentExceptionCallServiceAndStartProcessAreBothSelected);
                }
                _callService = value;
            }
        }

        /// <summary>
        /// Gets or sets if to stream file to the service.
        /// </summary>
        public bool StreamFile
        {
            get
            {
                return _streamFile;
            }
            set
            {
                _streamFile = value;
            }
        }

        /// <summary>
        /// Gets or sets if to try to rename the changed file before processing the file.
        /// </summary>
        public bool TryRenameFile
        {
            get
            {
                return _tryRenameFile;
            }
            set
            {
                _tryRenameFile = value;
            }
        }

        /// <summary>
        /// Gets or sets try rename retries count.
        /// </summary>
        public int TryRenameFileRetries
        {
            get
            {
                return _tryRenameFileRetries;
            }
            set
            {
                if (value < 0)
                {
                    _tryRenameFileRetries = 0;
                }
                else if (value > 99)
                {
                    _tryRenameFileRetries = 99;
                }
                else
                {
                    _tryRenameFileRetries = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets file watcher path.
        /// </summary>
        /// <exception cref="PathTooLongException">Path string is too long.</exception>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (value.Length > PathMaxLength)
                {
                    throw new PathTooLongException(value);
                }
                _path = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher filter.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                _filter = value;
            }
        }

        /// <summary>
        /// Gets or sets if to generate file system events at startup.
        /// </summary>
        public bool GenerateFileSystemEventsAtStartup
        {
            get
            {
                return _generateFileSystemEventsAtStartup;
            }
            set
            {
                _generateFileSystemEventsAtStartup = value;
            }
        }

        /// <summary>
        /// Gets or sets changed event file name regular expression filter. Empty string to disable filtering.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Invalid regular expression.</exception>
        public string ChangedRegularExpressionFilter
        {
            get
            {
                return _regexFilterChanged;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                try // Test regex filter.
                {
                    new Regex(value);
                }
                catch (ArgumentException exception)
                {
                    throw new ArgumentException(Resources.ArgumentExceptionChangedFilter,
                                                _regexFilterChanged,
                                                exception);
                }
                _regexFilterChanged = value;
            }
        }

        /// <summary>
        /// Gets or sets created event file name regular expression filter. Empty string to disable filtering.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Invalid regular expression.</exception>
        public string CreatedRegularExpressionFilter
        {
            get
            {
                return _regexFilterCreated;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                try // Test regex filter.
                {
                    new Regex(value);
                }
                catch (ArgumentException exception)
                {
                    throw new ArgumentException(Resources.ArgumentExceptionCreatedFilter,
                                                _regexFilterCreated,
                                                exception);
                }
                _regexFilterCreated = value;
            }
        }

        /// <summary>
        /// Gets or sets deleted event file name regular expression filter. Empty string to disable filtering.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Invalid regular expression.</exception>
        public string DeletedRegularExpressionFilter
        {
            get
            {
                return _regexFilterDeleted;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                try // Test regex filter.
                {
                    new Regex(value);
                }
                catch (ArgumentException exception)
                {
                    throw new ArgumentException(Resources.ArgumentExceptionDeletedFilter,
                                                _regexFilterDeleted,
                                                exception);
                }
                _regexFilterDeleted = value;
            }
        }

        /// <summary>
        /// Gets or sets renamed event old file name regular expression filter. Empty string to disable filtering.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Invalid regular expression.</exception>
        public string RenamedRegularExpressionFilter
        {
            get
            {
                return _regexFilterRenamed;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                try // Test regex filter.
                {
                    new Regex(value);
                }
                catch (ArgumentException exception)
                {
                    throw new ArgumentException(Resources.ArgumentExceptionRenamedFilter,
                                                _regexFilterRenamed,
                                                exception);
                }
                _regexFilterRenamed = value;
            }
        }

        /// <summary>
        /// Gets or sets generated event file name regular expression filter. Empty string to disable filtering.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Invalid regular expression.</exception>
        public string GeneratedEventFileNameRegularExpressionFilter
        {
            get
            {
                return _regexFilterFileName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                try // Test regex filter.
                {
                    new Regex(value);
                }
                catch (ArgumentException exception)
                {
                    throw new ArgumentException(Resources.ArgumentExceptionGeneratedEventFileNameFilter,
                                                _regexFilterFileName,
                                                exception);
                }
                _regexFilterFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher internal buffer size.
        /// </summary>
        public int InternalBufferSize
        {
            get
            {
                return _internalBufferSize;
            }
            set
            {
                if (value < 0)
                {
                    _internalBufferSize = 0;
                }
                else
                {
                    _internalBufferSize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets process file name.
        /// </summary>
        /// <exception cref="PathTooLongException">ProcessFileName string is too long.</exception>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessFileName
        {
            get
            {
                return _processFileName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (value.Length > PathMaxLength)
                {
                    throw new PathTooLongException(value);
                }
                _processFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets start process.
        /// </summary>
        public bool StartProcess
        {
            get
            {
                return _startProcess;
            }
            set
            {
                _startProcess = value;
            }
        }

        /// <summary>
        /// Gets or sets process arguments.
        /// </summary>
        /// <exception cref="ArgumentException">Arguments string is too long.</exception>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessArguments
        {
            get
            {
                return _processArguments;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (value.Length > ArgumentsMaxLength)
                {
                    throw new ArgumentException(Resources.ArgumentsStringIsTooLongException);
                }
                _processArguments = value;
            }
        }

        /// <summary>
        /// Gets or sets process use file name as arguments.
        /// </summary>
        public bool ProcessUseFileNameAsArgument
        {
            get
            {
                return _processUseFileNameAsArgument;
            }
            set
            {
                _processUseFileNameAsArgument = value;
            }
        }

        /// <summary>
        /// Gets or sets process arguments file name escape string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Duplicate argument escape string.</exception>
        public string ProcessArgumentsFileNameEscapeString
        {
            get
            {
                return _processArgumentsFileNameEscapeString;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (!String.IsNullOrEmpty(_processArgumentsOldFileNameEscapeString))
                {
                    if (String.Compare(value, _processArgumentsOldFileNameEscapeString, StringComparison.Ordinal) == 0)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateArgumentEscapeString);
                    }
                }
                if (!String.IsNullOrEmpty(_processArgumentsChangeTypeEscapeString))
                {
                    if (String.Compare(value, _processArgumentsChangeTypeEscapeString, StringComparison.Ordinal) == 0)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateArgumentEscapeString);
                    }
                }
                _processArgumentsFileNameEscapeString = value;
            }
        }

        /// <summary>
        /// Gets or sets process use old file name as arguments.
        /// </summary>
        public bool ProcessUseOldFileNameAsArgument
        {
            get
            {
                return _processUseOldFileNameAsArgument;
            }
            set
            {
                _processUseOldFileNameAsArgument = value;
            }
        }

        /// <summary>
        /// Gets or sets process arguments old file name escape string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Duplicate argument escape string.</exception>
        public string ProcessArgumentsOldFileNameEscapeString
        {
            get
            {
                return _processArgumentsOldFileNameEscapeString;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (!String.IsNullOrEmpty(_processArgumentsFileNameEscapeString))
                {
                    if (String.Compare(value, _processArgumentsFileNameEscapeString, StringComparison.Ordinal) == 0)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateArgumentEscapeString);
                    }
                }
                if (!String.IsNullOrEmpty(_processArgumentsChangeTypeEscapeString))
                {
                    if (String.Compare(value, _processArgumentsChangeTypeEscapeString, StringComparison.Ordinal) == 0)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateArgumentEscapeString);
                    }
                }
                _processArgumentsOldFileNameEscapeString = value;
            }
        }

        /// <summary>
        /// Gets or sets process use change type as arguments.
        /// </summary>
        public bool ProcessUseChangeTypeAsArgument
        {
            get
            {
                return _processUseChangeTypeAsArgument;
            }
            set
            {
                _processUseChangeTypeAsArgument = value;
            }
        }

        /// <summary>
        /// Gets or sets process arguments change type escape string.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        /// <exception cref="ArgumentException">Duplicate argument escape string.</exception>
        public string ProcessArgumentsChangeTypeEscapeString
        {
            get
            {
                return _processArgumentsChangeTypeEscapeString;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (!String.IsNullOrEmpty(_processArgumentsFileNameEscapeString))
                {
                    if (String.Compare(value, _processArgumentsFileNameEscapeString, StringComparison.Ordinal) == 0)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateArgumentEscapeString);
                    }
                }
                if (!String.IsNullOrEmpty(_processArgumentsOldFileNameEscapeString))
                {
                    if (String.Compare(value, _processArgumentsOldFileNameEscapeString, StringComparison.Ordinal) == 0)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateArgumentEscapeString);
                    }
                }
                _processArgumentsChangeTypeEscapeString = value;
            }
        }

        /// <summary>
        /// Gets or sets process create no window.
        /// </summary>
        public bool ProcessCreateNoWindow
        {
            get
            {
                return _processCreateNoWindow;
            }
            set
            {
                _processCreateNoWindow = value;
            }
        }

        /// <summary>
        /// Gets or sets process domain.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessDomain
        {
            get
            {
                return _processDomain;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                _processDomain = value;
            }
        }

        /// <summary>
        /// Gets or sets process user name.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessUserName
        {
            get
            {
                return _processUserName;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                _processUserName = value;
            }
        }

        /// <summary>
        /// Gets or sets process password.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessPassword
        {
            get
            {
                return _processPassword;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                _processPassword = value;
            }
        }

        /// <summary>
        /// Gets or sets load user profile.
        /// </summary>
        public bool ProcessLoadUserProfile
        {
            get
            {
                return _processLoadUserProfile;
            }
            set
            {
                _processLoadUserProfile = value;
            }
        }

        /// <summary>
        /// Gets or sets redirect standard error.
        /// </summary>
        public bool ProcessRedirectStandardError
        {
            get
            {
                return _processRedirectStandardError;
            }
            set
            {
                _processRedirectStandardError = value;
            }
        }

        /// <summary>
        /// Gets or sets redirect standard output.
        /// </summary>
        public bool ProcessRedirectStandardOutput
        {
            get
            {
                return _processRedirectStandardOutput;
            }
            set
            {
                _processRedirectStandardOutput = value;
            }
        }

        /// <summary>
        /// Gets or sets process use shell execute.
        /// </summary>
        public bool ProcessUseShellExecute
        {
            get
            {
                return _processUseShellExecute;
            }
            set
            {
                _processUseShellExecute = value;
            }
        }

        /// <summary>
        /// Gets or sets process verb.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessVerb
        {
            get
            {
                return _processVerb;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                _processVerb = value;
            }
        }

        /// <summary>
        /// Gets or sets process windows style.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessWindowStyle
        {
            get
            {
                return _processWindowStyle;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                _processWindowStyle = value;
            }
        }

        /// <summary>
        /// Gets or sets process working directory.
        /// </summary>
        /// <exception cref="PathTooLongException">ProcessWorkingDirectory string is too long.</exception>
        /// <exception cref="ArgumentNullException">Value cannot be null.</exception>
        public string ProcessWorkingDirectory
        {
            get
            {
                return _processWorkingDirectory;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value",
                                                    Resources.ArgumentNullException);
                }
                if (value.Length > PathMaxLength)
                {
                    throw new PathTooLongException(value);
                }
                _processWorkingDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets process synchronized execution.
        /// </summary>
        public bool ProcessSynchronizedExecution
        {
            get
            {
                return _processSynchronizedExecution;
            }
            set
            {
                _processSynchronizedExecution = value;
            }
        }

        /// <summary>
        /// Gets or sets process max wait time.
        /// </summary>
        public int ProcessMaxWaitTime
        {
            get
            {
                return _processMaxWaitTime;
            }
            set
            {
                if (value < -1)
                {
                    _processMaxWaitTime = -1;
                }
                else
                {
                    _processMaxWaitTime = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets process delay. Zero to disable.
        /// </summary>
        public int ProcessDelay
        {
            get
            {
                return _processDelay;
            }
            set
            {
                if (value < 0)
                {
                    _processDelay = 0;
                }
                else if (value > 9999999)
                {
                    _processDelay = 9999999;
                }
                else
                {
                    _processDelay = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets process file must exist.
        /// </summary>
        public bool ProcessFileMustExist
        {
            get
            {
                return _processFileMustExist;
            }
            set
            {
                _processFileMustExist = value;
            }
        }

        /// <summary>
        /// Gets or sets process lock file.
        /// </summary>
        public bool ProcessLockFile
        {
            get
            {
                return _processLockFile;
            }
            set
            {
                _processLockFile = value;
            }
        }

        /// <summary>
        /// Gets or sets process lock file last write delay.
        /// </summary>
        public int ProcessLockFileLastWriteDelay
        {
            get
            {
                return _processLockFileLastWriteDelay;
            }
            set
            {
                if (value < 0)
                {
                    _processLockFileLastWriteDelay = 0;
                }
                else if (value > 999999)
                {
                    _processLockFileLastWriteDelay = 999999;
                }
                else
                {
                    _processLockFileLastWriteDelay = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets process lock file retries. Zero to disable.
        /// </summary>
        public int ProcessLockFileRetries
        {
            get
            {
                return _processLockFileRetries;
            }
            set
            {
                if (value < 0)
                {
                    _processLockFileRetries = 0;
                }
                else if (value > 9999)
                {
                    _processLockFileRetries = 9999;
                }
                else
                {
                    _processLockFileRetries = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets process lock file retries queue limit. Zero to disable.
        /// </summary>
        public int ProcessLockFileRetriesQueueLimit
        {
            get
            {
                return _processLockFileRetriesQueueLimit;
            }
            set
            {
                if (value < 0)
                {
                    _processLockFileRetriesQueueLimit = 0;
                }
                else if (value > 9999)
                {
                    _processLockFileRetriesQueueLimit = 9999;
                }
                else
                {
                    _processLockFileRetriesQueueLimit = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets start daemon.
        /// </summary>
        public bool StartDaemon
        {
            get
            {
                return _startDaemon;
            }
            set
            {
                _startDaemon = value;
            }
        }

        /// <summary>
        /// Gets or sets display file system change.
        /// </summary>
        public bool DisplayFileSystemChange
        {
            get
            {
                return _displayFileSystemChange;
            }
            set
            {
                _displayFileSystemChange = value;
            }
        }

        /// <summary>
        /// Gets or sets log file system change.
        /// </summary>
        public bool LogFileSystemChange
        {
            get
            {
                return _logFileSystemChange;
            }
            set
            {
                _logFileSystemChange = value;
            }
        }

        /// <summary>
        /// Gets or sets log file system search error.
        /// </summary>
        public bool LogFileSystemSearchError
        {
            get
            {
                return _logFileSystemSearchError;
            }
            set
            {
                _logFileSystemSearchError = value;
            }
        }

        /// <summary>
        /// Gets or sets display file system search error.
        /// </summary>
        public bool DisplayFileSystemSearchError
        {
            get
            {
                return _displayFileSystemSearchError;
            }
            set
            {
                _displayFileSystemSearchError = value;
            }
        }

        /// <summary>
        /// Gets or sets log file system search progress.
        /// </summary>
        public bool LogFileSystemSearchProgress
        {
            get
            {
                return _logFileSystemSearchProgress;
            }
            set
            {
                _logFileSystemSearchProgress = value;
            }
        }

        /// <summary>
        /// Gets or sets display file system search progess.
        /// </summary>
        public bool DisplayFileSystemSearchProgress
        {
            get
            {
                return _displayFileSystemSearchProgress;
            }
            set
            {
                _displayFileSystemSearchProgress = value;
            }
        }

        /// <summary>
        /// Gets or sets batch size. Zero to disable process batch size.
        /// </summary>
        public int ProcessBatchSize
        {
            get
            {
                return _processBatchSize;
            }
            set
            {
                if (value < 0)
                {
                    ProcessBatchSize = 0;
                }
                else if (value > 99)
                {
                    ProcessBatchSize = 99;
                }
                else
                {
                    _processBatchSize = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets log process start.
        /// </summary>
        public bool LogProcessStart
        {
            get
            {
                return _logProcessStart;
            }
            set
            {
                _logProcessStart = value;
            }
        }

        /// <summary>
        /// Gets or sets log process end.
        /// </summary>
        public bool LogProcessEnd
        {
            get
            {
                return _logProcessEnd;
            }
            set
            {
                _logProcessEnd = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher include subdirectories.
        /// </summary>
        public bool IncludeSubdirectories
        {
            get
            {
                return _includeSubdirectories;
            }
            set
            {
                _includeSubdirectories = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher subscribe to changed event.
        /// </summary>
        public bool SubscribeToChangedEvent
        {
            get
            {
                return _subscribeToChangedEvent;
            }
            set
            {
                _subscribeToChangedEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher subscribe to created event.
        /// </summary>
        public bool SubscribeToCreatedEvent
        {
            get
            {
                return _subscribeToCreatedEvent;
            }
            set
            {
                _subscribeToCreatedEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher subscribe to deleted event.
        /// </summary>
        public bool SubscribeToDeletedEvent
        {
            get
            {
                return _subscribeToDeletedEvent;
            }
            set
            {
                _subscribeToDeletedEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher subscribe to renamed event.
        /// </summary>
        public bool SubscribeToRenamedEvent
        {
            get
            {
                return _subscribeToRenamedEvent;
            }
            set
            {
                _subscribeToRenamedEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher subscribe to error event.
        /// </summary>
        public bool SubscribeToBufferErrorEvent
        {
            get
            {
                return _subscribeToBufferErrorEvent;
            }
            set
            {
                _subscribeToBufferErrorEvent = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter attributes.
        /// </summary>
        public bool NotifyFilterAttributes
        {
            get
            {
                return _notifyAttribute;
            }
            set
            {
                _notifyAttribute = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter creation time.
        /// </summary>
        public bool NotifyFilterCreationTime
        {
            get
            {
                return _notifyCreationTime;
            }
            set
            {
                _notifyCreationTime = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter file name.
        /// </summary>
        public bool NotifyFilterDirectoryName
        {
            get
            {
                return _notifyDirectoryName;
            }
            set
            {
                _notifyDirectoryName = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter file name.
        /// </summary>
        public bool NotifyFilterFileName
        {
            get
            {
                return _notifyFileName;
            }
            set
            {
                _notifyFileName = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter last access.
        /// </summary>
        public bool NotifyFilterLastAccess
        {
            get
            {
                return _notifyLastAccess;
            }
            set
            {
                _notifyLastAccess = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter last write.
        /// </summary>
        public bool NotifyFilterLastWrite
        {
            get
            {
                return _notifyLastWrite;
            }
            set
            {
                _notifyLastWrite = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter security.
        /// </summary>
        public bool NotifyFilterSecurity
        {
            get
            {
                return _notifySecurity;
            }
            set
            {
                _notifySecurity = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher notify filter size.
        /// </summary>
        public bool NotifyFilterSize
        {
            get
            {
                return _notifySize;
            }
            set
            {
                _notifySize = value;
            }
        }

        /// <summary>
        /// Returns notify filters enumeration.
        /// </summary>
        /// <returns>Returns notify filters enumeration.</returns>
        public NotifyFilters NotifyFilters
        {
            get
            {
                NotifyFilters notifyFilters = 0;

                if (_notifyAttribute)
                {
                    notifyFilters = notifyFilters | NotifyFilters.Attributes;
                }
                if (_notifyCreationTime)
                {
                    notifyFilters = notifyFilters | NotifyFilters.CreationTime;
                }
                if (_notifyDirectoryName)
                {
                    notifyFilters = notifyFilters | NotifyFilters.DirectoryName;
                }
                if (_notifyFileName)
                {
                    notifyFilters = notifyFilters | NotifyFilters.FileName;
                }
                if (_notifyLastAccess)
                {
                    notifyFilters = notifyFilters | NotifyFilters.LastAccess;
                }
                if (_notifyLastWrite)
                {
                    notifyFilters = notifyFilters | NotifyFilters.LastWrite;
                }
                if (_notifySecurity)
                {
                    notifyFilters = notifyFilters | NotifyFilters.Security;
                }
                if (_notifySize)
                {
                    notifyFilters = notifyFilters | NotifyFilters.Size;
                }
                return notifyFilters;
            }
        }

        /// <summary>
        /// Gets or sets file watcher recycling.
        /// </summary>
        public bool RecycleFileWatcher
        {
            get
            {
                return _recycleFileWatcher;
            }
            set
            {
                _recycleFileWatcher = value;
            }
        }

        /// <summary>
        /// Gets or sets directory polling.
        /// </summary>
        public bool PollDirectory
        {
            get
            {
                return _pollDirectory;
            }
            set
            {
                _pollDirectory = value;
            }
        }

        /// <summary>
        /// Gets or sets directory poll interval.
        /// </summary>
        public int DirectoryPollInterval
        {
            get
            {
                return _directoryPollInterval;
            }
            set
            {
                if (value < 0)
                {
                    _directoryPollInterval = 1;
                }
                else
                {
                    _directoryPollInterval = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets file watcher recycle interval in minutes.
        /// </summary>
        public int RecycleInterval
        {
            get 
            {
                return _recycleInterval;
            }
            set
            {
                if (value < 0)
                {
                    _recycleInterval = 1;
                }
                else
                {
                    _recycleInterval = value;
                }
            }
        }

        /// <summary>
        /// True if to poll watched directory for changed and created files.
        /// </summary>
        private bool _pollDirectory;

        /// <summary>
        /// Directory poll interval in minutes. Default is 10.
        /// </summary>
        private int _directoryPollInterval = 10;

        /// <summary>
        /// True if to recycle file watcher.
        /// </summary>
        private bool _recycleFileWatcher;

        /// <summary>
        /// File wacher recycle interval in minutes. Default is 60.
        /// </summary>
        private int _recycleInterval = 60;

        /// <summary>
        /// File watcher filtered mode filter timeout in minutes. Default is 60.
        /// </summary>
        private int _filterdModeFilterTimeout = 60;

        /// <summary>
        /// True if file watcher is in filtered mode.
        /// </summary>
        private bool _filteredMode;

        /// <summary>
        /// True if to try to rename the changed file before processing the file.
        /// </summary>
        private bool _tryRenameFile;

        /// <summary>
        /// Try rename retries.
        /// </summary>
        private int _tryRenameFileRetries;

        /// <summary>
        /// True if to stream file to the service.
        /// </summary>
        private bool _streamFile;

        /// <summary>
        /// True if to call a service.
        /// </summary>
        private bool _callService;

        /// <summary>
        /// Max length of path (windows system).
        /// </summary>
        private const int PathMaxLength = 247;

        /// <summary>
        /// Process arguments string max length.
        /// </summary>
        private const int ArgumentsMaxLength = 2003;

        /// <summary>
        /// Normal window style.
        /// </summary>
        private const string WindowStyleNormal = "Normal";

        /// <summary>
        /// Contains file watcher path.
        /// </summary>
        private string _path = String.Empty;

        /// <summary>
        /// Contains file watcher filter. Default is "*".
        /// </summary>
        private string _filter = "*";

        /// <summary>
        /// True if to generate file system events at startup.
        /// </summary>
        private bool _generateFileSystemEventsAtStartup;

        /// <summary>
        /// Changed event regex filter.
        /// </summary>
        private string _regexFilterChanged = String.Empty;

        /// <summary>
        /// Deleted event regex filter.
        /// </summary>
        private string _regexFilterDeleted = String.Empty;

        /// <summary>
        /// Renamed event regex filter. Applies to old file name.
        /// </summary>
        private string _regexFilterRenamed = String.Empty;

        /// <summary>
        /// Created event regex filter.
        /// </summary>
        private string _regexFilterCreated = String.Empty;

        /// <summary>
        /// File system event generation file name filter.
        /// </summary>
        private string _regexFilterFileName = String.Empty;

        /// <summary>
        /// Contains file watcher internal buffer size. Default is "8192".
        /// </summary>
        private int _internalBufferSize = 8192;

        /// <summary>
        /// True if file watcher watches for subdirectories.
        /// </summary>
        private bool _includeSubdirectories;

        /// <summary>
        /// True if daemon will be started. Default is "True".
        /// </summary>
        private bool _startDaemon = true;

        /// <summary>
        /// True if file system change will be displayed. Default is "True".
        /// </summary>
        private bool _displayFileSystemChange = true;

        /// <summary>
        /// True if file system change should be logged. Default is "True".
        /// </summary>
        private bool _logFileSystemChange = true;

        /// <summary>
        /// True if process start should be logged. Default is "True".
        /// </summary>
        private bool _logProcessStart = true;

        /// <summary>
        /// True if process end should be logged. Default is "True".
        /// </summary>
        private bool _logProcessEnd = true;

        /// <summary>
        /// Process delay.
        /// </summary>
        private int _processDelay;

        /// <summary>
        /// True if process file must exist.
        /// </summary>
        private bool _processFileMustExist;

        /// <summary>
        /// True if process must lock file.
        /// </summary>
        private bool _processLockFile;

        /// <summary>
        /// Process lock file last write delay.
        /// </summary>
        private int _processLockFileLastWriteDelay;

        /// <summary>
        /// Process lock file retries.
        /// </summary>
        private int _processLockFileRetries;

        /// <summary>
        /// Process lock file retries queue limit.
        /// </summary>
        private int _processLockFileRetriesQueueLimit;

        /// <summary>
        /// True if process will be started.
        /// </summary>
        private bool _startProcess;

        /// <summary>
        /// Contains process executable file name.
        /// </summary>
        private string _processFileName = String.Empty;

        /// <summary>
        /// Contains process arguments.
        /// </summary>
        private string _processArguments = String.Empty;

        /// <summary>
        /// True if file name will be used as argument.
        /// </summary>
        private bool _processUseFileNameAsArgument;

        /// <summary>
        /// True if old file name will be used as argument.
        /// </summary>
        private bool _processUseOldFileNameAsArgument;

        /// <summary>
        /// True if change type will be used as argument.
        /// </summary>
        private bool _processUseChangeTypeAsArgument;

        /// <summary>
        /// Contains escape string for file name.
        /// </summary>
        private string _processArgumentsFileNameEscapeString = String.Empty;

        /// <summary>
        /// Contains escape string for old file name.
        /// </summary>
        private string _processArgumentsOldFileNameEscapeString = String.Empty;

        /// <summary>
        /// Contains escape string for change type.
        /// </summary>
        private string _processArgumentsChangeTypeEscapeString = String.Empty;

        /// <summary>
        /// True if process will not create window.
        /// </summary>
        private bool _processCreateNoWindow;

        /// <summary>
        /// Contains process domain.
        /// </summary>
        private string _processDomain = String.Empty;

        /// <summary>
        /// Contains process user name.
        /// </summary>
        private string _processUserName = String.Empty;

        /// <summary>
        /// Contains process password.
        /// </summary>
        private string _processPassword = String.Empty;

        /// <summary>
        /// True if process will load user profile.
        /// </summary>
        private bool _processLoadUserProfile;

        /// <summary>
        /// True if process will redirect standard error.
        /// </summary>
        private bool _processRedirectStandardError;

        /// <summary>
        /// True if process will redirect standard output.
        /// </summary>
        private bool _processRedirectStandardOutput;

        /// <summary>
        /// True if process uses shell execute.
        /// </summary>
        private bool _processUseShellExecute;

        /// <summary>
        /// Contains process verb.
        /// </summary>
        private string _processVerb = String.Empty;

        /// <summary>
        /// Contains process window style. Default is "Normal".
        /// </summary>
        private string _processWindowStyle = WindowStyleNormal;

        /// <summary>
        /// Contains process working directory.
        /// </summary>
        private string _processWorkingDirectory = String.Empty;

        /// <summary>
        /// True if process will be executed synchronous.
        /// </summary>
        private bool _processSynchronizedExecution;

        /// <summary>
        /// Maximum wait time to wait for synchtonous process. Default is "-1".
        /// </summary>
        private int _processMaxWaitTime = -1;

        /// <summary>
        /// True if file watcher subscribes to changed event. Default is "True".
        /// </summary>
        private bool _subscribeToChangedEvent = true;

        /// <summary>
        /// True if file watcher subscribes to created event. Default is "True".
        /// </summary>
        private bool _subscribeToCreatedEvent = true;

        /// <summary>
        /// True if file watcher subscribes to deleted event. Default is "True".
        /// </summary>
        private bool _subscribeToDeletedEvent = true;

        /// <summary>
        /// True if file watcher subscribes to renamed event. Default is "True".
        /// </summary>
        private bool _subscribeToRenamedEvent = true;

        /// <summary>
        /// True if file watcher subscribes to buffer error event. Default is "True".
        /// </summary>
        private bool _subscribeToBufferErrorEvent = true;

        /// <summary>
        /// True if file watcher notifies attribute change.
        /// </summary>
        private bool _notifyAttribute;

        /// <summary>
        /// True if file watcher notifies creation time change.
        /// </summary>
        private bool _notifyCreationTime;

        /// <summary>
        /// True if file watcher notifies size change.
        /// </summary>
        private bool _notifySize;

        /// <summary>
        /// True if file watcher notifies last access change.
        /// </summary>
        private bool _notifyLastAccess;

        /// <summary>
        /// True if file watcher notifies directory name change. Default is "True".
        /// </summary>
        private bool _notifyDirectoryName = true;

        /// <summary>
        /// True if file watcher notifies last write change. Default is "True".
        /// </summary>
        private bool _notifyLastWrite = true;

        /// <summary>
        /// True if file watcher notifies file name change. Default is "True".
        /// </summary>
        private bool _notifyFileName = true;

        /// <summary>
        /// True if file watcher notifies security change.
        /// </summary>
        private bool _notifySecurity;

        /// <summary>
        /// True if to log file system search error.
        /// </summary>
        private bool _logFileSystemSearchError;

        /// <summary>
        /// True if to display file system search error.
        /// </summary>
        private bool _displayFileSystemSearchError;

        /// <summary>
        /// True if to log file system search progress.
        /// </summary>
        private bool _logFileSystemSearchProgress;

        /// <summary>
        /// True if to display file system search progress.
        /// </summary>
        private bool _displayFileSystemSearchProgress;

        /// <summary>
        /// Process batch size. Zero to disable process batch size.
        /// </summary>
        private int _processBatchSize;
    }
}