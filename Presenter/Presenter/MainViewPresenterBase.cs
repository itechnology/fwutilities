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
using System.Security.Permissions;
using FileWatcherUtilities.Logger;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides main view presenter base.
    /// </summary>
    public class MainViewPresenterBase : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the MainViewPresenterBase class. 
        /// </summary>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="formatter">Log formatter.</param>
        /// <exception cref="ArgumentNullException">fileWatcherController is null.</exception>
        /// <exception cref="ArgumentNullException">logger is null.</exception>
        /// <exception cref="ArgumentNullException">formatter is null.</exception>
        public MainViewPresenterBase(FileWatcherController fileWatcherController,
                                     ILogger logger, 
                                     IFormatter formatter)
        {
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
            _fileWatcherController = fileWatcherController;
            _logger = logger;
            _formatter = formatter;
            SubscribeToFileWatcherControllerEvents();
        }

        #region IDisposable

        /// <summary>
        /// Implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">True if disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_fileWatcherController != null)
                {
                    _fileWatcherController.Dispose();
                    _fileWatcherController = null;
                }
            }
        }

        #endregion IDisposable

        #region Controller event handlers

        /// <summary>
        /// Handles system changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected virtual void OnSystemChanged(object sender,
                                               FileWatcherEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }
            if (e.ConfigurationKeyValuePair.Value.LogFileSystemChange)
            {
                _logger.Log(_formatter.Format(e));
            }
        }

        /// <summary>
        /// Handles buffer error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnBufferError(object sender,
                                             FileWatcherBufferErrorEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles file watcher configuration changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnConfigurationChanged(object sender,
                                                      EventArgs e)
        {
        }

        /// <summary>
        /// Handles process started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected virtual void OnProcessStarted(object sender,
                                                ControllerProcessStartedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }
            if (e.LogEvent)
            {
                _logger.Log(_formatter.Format(e));
            }
        }

        /// <summary>
        /// Handles process exited event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected virtual void OnProcessExited(object sender,
                                               ProcessExitEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }
            if (e.LogEvent)
            {
                _logger.Log(_formatter.Format(e));
            }
        }

        /// <summary>
        /// Handles process error data event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnProcessErrorData(object sender,
                                                  ProcessDataEventArgs e)
        {
            _logger.Log(_formatter.FormatProcessErrorData(e));
        }

        /// <summary>
        /// Handles process output data event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnProcessOutputData(object sender,
                                                   ProcessDataEventArgs e)
        {
            _logger.Log(_formatter.FormatProcessOutputData(e));
        }

        /// <summary>
        /// Handles process error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnProcessError(object sender,
                                              ProcessErrorEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles file watcher started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnFileWatcherStarted(object sender,
                                                    FileWatcherStartedEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles file watcher starting event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnFileWatcherStarting(object sender,
                                                     FileWatcherStartingEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles file watcher stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnFileWatcherStopped(object sender,
                                                    FileWatcherStoppedEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles controller stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnControllerStopped(object sender,
                                                   EventArgs e)
        {
            WriteAllProcessesHaveExitedMessage();
        }

        /// <summary>
        /// Handles file watcher search process event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected virtual void OnFileWatcherSearchProgress(object sender,
                                                           FileWatcherSearchProgressEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }
            if (e.LogEvent)
            {
                _logger.Log(_formatter.Format(e));
            }
        }

        /// <summary>
        /// Handles file watcher searcher error.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected virtual void OnFileWatcherSearchError(object sender,
                                                        FileWatcherSearchErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }
            if (e.LogEvent)
            {
                _logger.Log(_formatter.Format(e));
            }
        }

        /// <summary>
        /// Handles file watcher path error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnFileWatcherPathError(object sender, 
                                                      FileWatcherPathErrorEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles service error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnServiceError(object sender,
                                              ServiceErrorEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }
        
        /// <summary>
        /// Handles service called event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnServiceCalled(object sender, 
                                               ServiceCalledEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles service begin called event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnServiceBeginCall(object sender,
                                                  ServiceBeginCallEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles service proxy creation error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnServiceProxyCreationError(object sender,
                                                           ServiceProxyCreationErrorEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles process canceled event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnProcessCanceled(object sender,
                                                 ProcessCanceledEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        /// <summary>
        /// Handles file watcher recycle event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>   
        protected virtual void OnFileWatcherRecycle(object sender,
                                                    FileWatcherRecycledEventArgs e)
        {
            _logger.Log(_formatter.Format(e));
        }

        #endregion Controller event handlers       

        /// <summary>
        /// Writes all processes have exited message to log.
        /// </summary>
        protected virtual void WriteAllProcessesHaveExitedMessage()
        {
            _logger.Log(_formatter.Format(Resources.MessageAllHaveProcessExited, MessageType.Info));
        }

        /// <summary>
        /// Writes application started message to log.
        /// </summary>
        protected virtual void WriteApplicationStartedMessage()
        {
            _logger.Log(_formatter.Format(Resources.MessageApplicationStarted, MessageType.Info));
        }

        /// <summary>
        /// Writes application stopped message to log.
        /// </summary>
        protected virtual void WriteApplicationStoppedMessage()
        {
            _logger.Log(_formatter.Format(Resources.MessageApplicatonStopped, MessageType.Info));
        }

        /// <summary>
        /// Gets or sets logger.
        /// </summary>
        protected ILogger Logger
        {
            get
            {
                return _logger;
            }
            set
            {
                _logger = value;
            }
        }

        /// <summary>
        /// Gets or sets formatter.
        /// </summary>
        protected IFormatter Formatter
        {
            get
            {
                return _formatter;
            }
            set
            {
                _formatter = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher controller.
        /// </summary>
        protected FileWatcherController FileWatcherController
        {
            get
            {
                return _fileWatcherController;
            }
            set
            {
                _fileWatcherController = value;
            }
        }

        /// <summary>
        /// Subscribes to all file watcher controller events.
        /// </summary>
        private void SubscribeToFileWatcherControllerEvents()
        {
            _fileWatcherController.BufferError +=
                new EventHandler<FileWatcherBufferErrorEventArgs>(OnBufferError);

            _fileWatcherController.ConfigurationChanged +=
                new EventHandler<EventArgs>(OnConfigurationChanged);

            _fileWatcherController.FileWatcherPathError +=
                new EventHandler<FileWatcherPathErrorEventArgs>(OnFileWatcherPathError);

            _fileWatcherController.FileWatcherStarted +=
                new EventHandler<FileWatcherStartedEventArgs>(OnFileWatcherStarted);

            _fileWatcherController.FileWatcherStarting +=
                new EventHandler<FileWatcherStartingEventArgs>(OnFileWatcherStarting);

            _fileWatcherController.FileWatcherStopped +=
                new EventHandler<FileWatcherStoppedEventArgs>(OnFileWatcherStopped);

            _fileWatcherController.ProcessError +=
                new EventHandler<ProcessErrorEventArgs>(OnProcessError);

            _fileWatcherController.ProcessErrorData +=
                new EventHandler<ProcessDataEventArgs>(OnProcessErrorData);

            _fileWatcherController.ProcessExited +=
                new EventHandler<ProcessExitEventArgs>(OnProcessExited);

            _fileWatcherController.ProcessOutputData +=
                new EventHandler<ProcessDataEventArgs>(OnProcessOutputData);

            _fileWatcherController.ProcessStarted +=
                new EventHandler<ControllerProcessStartedEventArgs>(OnProcessStarted);

            _fileWatcherController.SystemChanged +=
                new EventHandler<FileWatcherEventArgs>(OnSystemChanged);

            _fileWatcherController.ControllerStopped +=
                new EventHandler<EventArgs>(OnControllerStopped);

            _fileWatcherController.FileWatcherSearchError +=
                new EventHandler<FileWatcherSearchErrorEventArgs>(OnFileWatcherSearchError);

            _fileWatcherController.FileWatcherSearchProgress +=
                new EventHandler<FileWatcherSearchProgressEventArgs>(OnFileWatcherSearchProgress);

            _fileWatcherController.ServiceCalled += 
                new EventHandler<ServiceCalledEventArgs>(OnServiceCalled);

            _fileWatcherController.ServiceError += 
                new EventHandler<ServiceErrorEventArgs>(OnServiceError);

            _fileWatcherController.ServiceBeginCall +=
                new EventHandler<ServiceBeginCallEventArgs>(OnServiceBeginCall);

            _fileWatcherController.ServiceProxyCreationError +=
                new EventHandler<ServiceProxyCreationErrorEventArgs>(OnServiceProxyCreationError);

            _fileWatcherController.ProcessCanceled +=
                new EventHandler<ProcessCanceledEventArgs>(OnProcessCanceled);

            _fileWatcherController.FileWatcherRecycle +=
                new EventHandler<FileWatcherRecycledEventArgs>(OnFileWatcherRecycle);
        }

        /// <summary>
        /// Log message logger.
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Log message formatter.
        /// </summary>
        private IFormatter _formatter;

        /// <summary>
        /// File watcher controller.
        /// </summary>
        private FileWatcherController _fileWatcherController;
    }
}