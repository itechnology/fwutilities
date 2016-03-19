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
using FileWatcherUtilities.Logger;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;
using FileWatcherUtilities.Options;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides log view presenter.
    /// </summary>
    public class LogViewPresenter : LogViewPresenterBase
    {
        /// <summary>
        /// Initializes a new instance of the LogViewPresenterBase class. 
        /// </summary>
        /// <param name="logView">Log view.</param>
        /// <param name="mainView">Main view.</param>
        /// <param name="applicationOptionsController">ApplicationOptionsController.</param>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="viewUpdateInterval">View update interval.</param>
        /// <param name="logMessageSize">Log message size.</param>
        /// <exception cref="ArgumentNullException">Log view is null.</exception>
        /// <exception cref="ArgumentNullException">fileWatcherController is null.</exception>
        /// <exception cref="ArgumentNullException">formatter is null.</exception>
        public LogViewPresenter(ILogView logView,
                                IMainView mainView,
                                ApplicationOptionsController applicationOptionsController,
                                FileWatcherController fileWatcherController,
                                IFormatter formatter,
                                double viewUpdateInterval,
                                int logMessageSize)
            : base(fileWatcherController, formatter, logMessageSize)
        {
            if (logView == null)
            {
                throw new ArgumentNullException("logView",
                                                Resources.ArgumentNullException);
            }
            if (mainView == null)
            {
                throw new ArgumentNullException("mainView",
                                                Resources.ArgumentNullException);
            }
            if (applicationOptionsController == null)
            {
                throw new ArgumentNullException("applicationOptionsController",
                                                Resources.ArgumentNullException);
            }
            if (fileWatcherController == null)
            {
                throw new ArgumentNullException("fileWatcherController",
                                                Resources.ArgumentNullException);
            }
            if (formatter == null)
            {
                throw new ArgumentNullException("formatter",
                                                Resources.ArgumentNullException);
            }

            _logView = logView;
            _mainView = mainView;
            _applicationOptionsController = applicationOptionsController;

            // Check if view interval is less than default value.
            if (viewUpdateInterval < 100)
            {
                _updateTimer.Interval = 100;
            }
            else
            {
                _updateTimer.Interval = viewUpdateInterval;
            }

            SubscribeToLogViewEvents();
            SubscribeToMainViewEvents();
            SubscribeToApplicationOptionsControllerEvents();

            // Subscribe to timer event. (updates view).
            _updateTimer.Elapsed += new ElapsedEventHandler(OnElapsed);

            // Set synchronization context for running events in main thread.
            SetSynchronizationContext();
        }

        /// <summary>
        /// Handles file watcher starting event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected override void OnFileWatcherStarting(object sender,
                                                      FileWatcherStartingEventArgs e)
        {
            base.OnFileWatcherStarting(sender, e);

            // If starting the file watchers then start updating if not already updating.
            lock (_lockUpdateTimer)
            {
                if (!_updateTimer.Enabled)
                {
                    _updateTimer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Subscribes to log view events.
        /// </summary>
        private void SubscribeToLogViewEvents()
        {
            _logView.ViewClosed +=
                new EventHandler<EventArgs>(OnViewClosed);
        }

        /// <summary>
        /// Handles log view closed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnViewClosed(object sender,
                                  EventArgs e)
        {
            StopUpdate();
        }

        /// <summary>
        /// Subscribes to main view events.
        /// </summary>
        private void SubscribeToMainViewEvents()
        {
            _mainView.ViewLog +=
                new EventHandler<EventArgs>(OnViewLog);
        }

        /// <summary>
        /// Handles view log event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnViewLog(object sender,
                               EventArgs e)
        {
            // Start updating log view.
            Update();
        }

        /// <summary>
        /// Subscribes to options view events.
        /// </summary>
        private void SubscribeToApplicationOptionsControllerEvents()
        {
            _applicationOptionsController.ConfigurationChanged +=
                new EventHandler<ApplicationOptionsChangedEventArgs>(OnConfigurationChanged);
        }

        /// <summary>
        /// Handles application options configuraion changed event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnConfigurationChanged(object sender,
                                            ApplicationOptionsChangedEventArgs e)
        {
            // Set log message size.
            LogMessageSize = e.ApplicationOptions.LogMessages;
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
        /// Update log view.
        /// </summary>
        private void Update()
        {
            // If file watcher is active, use delayed update.
            if (FileWatcherController.IsActive())
            {
                lock (_lockUpdateTimer)
                {
                    if (!_updateTimer.Enabled)
                    {
                        _updateTimer.Enabled = true;
                    }
                }
            }
            else
            {
                lock (_lockUpdateTimer)
                {
                    if (_updateTimer.Enabled)
                    {
                        _updateTimer.Enabled = false;
                    }
                }
                _logView.Update(LogMessages());
            }
        }

        /// <summary>
        /// Stops updating log view.
        /// </summary>
        private void StopUpdate()
        {
            lock (_lockUpdateTimer)
            {
                if (_updateTimer.Enabled)
                {
                    _updateTimer.Enabled = false;
                }
                _logView.Update(LogMessages());
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
                lock (_lockUpdateTimer)
                {
                    if (_updateTimer != null)
                    {
                        if (!_updateTimer.Enabled)
                        {
                            return;
                        }
                        _logView.Update(LogMessages());
                    }
                }
            }), null);
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

        /// <summary>
        /// Lock object for update timer.
        /// </summary>
        private readonly object _lockUpdateTimer = new object();

        /// <summary>
        /// Log view.
        /// </summary>
        private readonly ILogView _logView;

        /// <summary>
        /// Main view.
        /// </summary>
        private readonly IMainView _mainView;

        /// <summary>
        /// Application options controller.
        /// </summary>
        private readonly ApplicationOptionsController _applicationOptionsController;

        /// <summary>
        /// Update timer.
        /// </summary>
        private System.Timers.Timer _updateTimer = new System.Timers.Timer();

        /// <summary>
        /// Contains synchronization context for running the events in the main thread.
        /// </summary>
        private SynchronizationContext _synchronizationContext;
    }
}