/******************************************************************************
*    File Watcher Utilities / File Watcher Windows Service
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

using System.ServiceProcess;
using FileWatcherUtilities.Presenter;
using FileWatcherUtilities.FileWatcherWindowsService.Properties;

namespace FileWatcherUtilities.FileWatcherWindowsService
{
    /// <summary>
    /// File watcher utilities windows service.
    /// </summary>
    public class FileWatcherUtilitiesWindowsService : ServiceBase
    {
        /// <summary>
        /// Creates new FileWatcherUtilitiesWindowsService.
        /// </summary>
        public FileWatcherUtilitiesWindowsService()
        {
            CanShutdown = true;
            ServiceName = Resources.ServiceName;
        }

        /// <summary>
        /// Handles OnStart event.
        /// </summary>
        /// <param name="args">Windows service arguments.</param>
        protected override void OnStart(string[] args)
        {
            _windowsServiceView = new WindowsServiceView();
            _presenterBuilder = new PresenterBuilder(_windowsServiceView);
            _presenterBuilder.Build();
            // Start application.
            _windowsServiceView.Start(args);
            // Set started to true;
            _isStarted = true;
        }

        /// <summary>
        /// Handles OnStop event.
        /// </summary>
        /// <remarks>This method will blocks until all the processes have exited.</remarks>
        protected override void OnStop()
        {
            if (_isStarted)
            {
                _windowsServiceView.Stop();
                _isStarted = false;
            }
            if (_presenterBuilder != null)
            {
                _presenterBuilder.Dispose();
                _presenterBuilder = null;
            }
        }

        /// <summary>
        /// Handles OnShutdown event.
        /// </summary>
        protected override void OnShutdown()
        {
            if (_isStarted)
            {
                OnStop();
            }
        }

        /// <summary>
        /// Presenter builder.
        /// </summary>
        private PresenterBuilder _presenterBuilder;

        /// <summary>
        /// Windows service view.
        /// </summary>
        private WindowsServiceView _windowsServiceView;

        /// <summary>
        /// True if application was started.
        /// </summary>
        private bool _isStarted;
    }
}