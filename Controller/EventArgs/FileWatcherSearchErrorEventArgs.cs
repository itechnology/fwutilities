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
    /// Provides data for FileWatcherSearchError event.
    /// </summary>
    public class FileWatcherSearchErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the FileWatcherSearchErrorEventArgs class.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <param name="exception">Exception.</param>
        /// <param name="displayEvent">True if file watcher search error event should be displayed.</param>
        /// <param name="logEvent">True if file watcher search error event should be logged.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentNullException">exception is null.</exception>
        public FileWatcherSearchErrorEventArgs(string daemonName,
                                               Exception exception,
                                               bool displayEvent,
                                               bool logEvent)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception",
                                                Resources.ArgumentNullException);
            }
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            _daemonName = daemonName;
            _exception = exception;
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
        /// Gets exception.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }

        /// <summary>
        /// Gets the log event value. True if file watcher search error event should be logged.
        /// </summary>
        public bool LogEvent
        {
            get
            {
                return _logEvent;
            }
        }

        /// <summary>
        /// Gets the display event value. True if file watcher search error event should be displayed.
        /// </summary>
        public bool DisplayEvent
        {
            get
            {
                return _displayEvent;
            }
        }

        /// <summary>
        /// Contains exception.
        /// </summary>
        private readonly Exception _exception;

        /// <summary>
        /// Contains daemon name.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// True if file watcher search error event should be logged.
        /// </summary>
        private readonly bool _logEvent;

        /// <summary>
        /// True if file watcher search error event should be displayed.
        /// </summary>
        private readonly bool _displayEvent;
    }
}