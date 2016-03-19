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
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Provides data for FileWatcherSearchProgress event.
    /// </summary>
    public class FileWatcherSearchProgressEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the FileWatcherSearchProgressEventArgs class.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <param name="directoryFullPath">Directory full path.</param>
        /// <param name="displayEvent">True if file watcher search progress event should be displayed.</param>
        /// <param name="logEvent">True if file watcher search progress event should be logged.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentNullException">directoryFullPath is null.</exception>
        public FileWatcherSearchProgressEventArgs(string daemonName,
                                                  string directoryFullPath,
                                                  bool displayEvent,
                                                  bool logEvent)
        {
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (directoryFullPath == null)
            {
                throw new ArgumentNullException("directoryFullPath",
                                                Resources.ArgumentNullException);
            }
            _daemonName = daemonName;
            _directoryFullPath = directoryFullPath;
            _displayEvent = displayEvent;
            _logEvent = logEvent;
        }

        /// <summary>
        /// Gets file watcher daemon name.
        /// </summary>
        public string DaemonName
        {
            get
            {
                return _daemonName;
            }
        }

        /// <summary>
        /// Gets directory full path.
        /// </summary>
        public string DirectoryFullPath
        {
            get
            {
                return _directoryFullPath;
            }
        }

        /// <summary>
        /// Gets the log event value. True if file watcher search progress event should be logged.
        /// </summary>
        public bool LogEvent
        {
            get
            {
                return _logEvent;
            }
        }

        /// <summary>
        /// Gets the display event value. True if file watcher search progress event should be displayed.
        /// </summary>
        public bool DisplayEvent
        {
            get
            {
                return _displayEvent;
            }
        }

        /// <summary>
        /// Contains daemon name.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// Contains directory full path.
        /// </summary>
        private readonly string _directoryFullPath;

        /// <summary>
        /// True if file watcher search progress event should be logged.
        /// </summary>
        private readonly bool _logEvent;

        /// <summary>
        /// True if file watcher search progress event should be displayed.
        /// </summary>
        private readonly bool _displayEvent;
    }
}