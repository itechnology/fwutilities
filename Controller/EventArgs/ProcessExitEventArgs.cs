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
    /// Provides data for ProcessExit event.
    /// </summary>
    public class ProcessExitEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ProcessExitEventArgs class.
        /// </summary>
        /// <param name="daemonName">The name of the daemon which was running the process.</param>
        /// <param name="processId">Process Id.</param>
        /// <param name="exitCode">Process exit code.</param>
        /// <param name="processExitTime">Process exit time.</param>
        /// <param name="logEvent">True if process exit event should be logged.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        public ProcessExitEventArgs(string daemonName,
                                    int processId,
                                    int exitCode,
                                    DateTime processExitTime,
                                    bool logEvent)
        {
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            _daemonName = daemonName;
            _processId = processId;
            _exitCode = exitCode;
            _processExitTime = processExitTime;
            _logEvent = logEvent;
        }

        /// <summary>
        /// Gets the name of the daemon which was running the process.
        /// </summary>
        public string DaemonName
        {
            get
            {
                return _daemonName;
            }
        }

        /// <summary>
        /// Gets the process Id.
        /// </summary>
        public int ProcessId
        {
            get
            {
                return _processId;
            }
        }

        /// <summary>
        /// Gets the process exit code.
        /// </summary>
        public int ExitCode
        {
            get
            {
                return _exitCode;
            }
        }

        /// <summary>
        /// Gets the process exit time.
        /// </summary>
        public DateTime ProcessExitTime
        {
            get
            {
                return _processExitTime;
            }
        }

        /// <summary>
        /// True if process exit event should be logged.
        /// </summary>
        public bool LogEvent
        {
            get
            {
                return _logEvent;
            }
        }

        /// <summary>
        /// Contains the name of the daemon which was running the process.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// Contains process Id.
        /// </summary>
        private readonly int _processId;

        /// <summary>
        /// Contains process exit time.
        /// </summary>
        private readonly DateTime _processExitTime;

        /// <summary>
        /// Contains process exit code.
        /// </summary>
        private readonly int _exitCode;

        /// <summary>
        /// True if process exit event should be logged.
        /// </summary>
        private readonly bool _logEvent;
    }
}