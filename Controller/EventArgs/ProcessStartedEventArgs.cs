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
    /// Provides data for ProcessStarted event.
    /// </summary>
    public class ProcessStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ProcessStartedEventArgs class.
        /// </summary>
        /// <param name="daemonName">The name of the daemon which started the process.</param>
        /// <param name="processId">Process Id.</param>
        /// <param name="fileName">Path of the process executable file.</param>
        /// <param name="arguments">Process arguments.</param>
        /// <param name="verb">Process verb.</param>
        /// <param name="processStartTime">Process start time.</param>
        /// <param name="logEvent">True if start event should be logged.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        /// <exception cref="ArgumentNullException">arguments is null.</exception>
        /// <exception cref="ArgumentNullException">verb is null.</exception>
        public ProcessStartedEventArgs(string daemonName,
                                       int processId,
                                       string fileName,
                                       string arguments,
                                       string verb,
                                       DateTime processStartTime,
                                       bool logEvent)
        {
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName",
                                                Resources.ArgumentNullException);
            }
            if (arguments == null)
            {
                throw new ArgumentNullException("arguments",
                                                Resources.ArgumentNullException);
            }
            if (verb == null)
            {
                throw new ArgumentNullException("verb",
                                                Resources.ArgumentNullException);
            }
            _daemonName = daemonName;
            _processId = processId;
            _fileName = fileName;
            _arguments = arguments;
            _verb = verb;
            _processStartTime = processStartTime;
            _logEvent = logEvent;
        }

        /// <summary>
        /// Gets the name of the daemon which started the process.
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
        /// Gets the path of the process executable file.
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        /// <summary>
        /// Gets the process arguments.
        /// </summary>
        public string Arguments
        {
            get
            {
                return _arguments;
            }
        }

        /// <summary>
        /// Gets the process verb.
        /// </summary>
        public string Verb
        {
            get
            {
                return _verb;
            }
        }

        /// <summary>
        /// Gets the process start time.
        /// </summary>
        public DateTime ProcessStartTime
        {
            get
            {
                return _processStartTime;
            }
        }

        /// <summary>
        /// Gets the log event value. True if start event should be logged.
        /// </summary>
        public bool LogEvent
        {
            get
            {
                return _logEvent;
            }
        }

        /// <summary>
        /// Contains the name of the daemon which started the process.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// Contains process Id.
        /// </summary>
        private readonly int _processId;

        /// <summary>
        /// Contains process start time.
        /// </summary>
        private readonly DateTime _processStartTime;

        /// <summary>
        /// Contains the path of the process executable file.
        /// </summary>
        private readonly string _fileName;

        /// <summary>
        /// Contains process arguments.
        /// </summary>
        private readonly string _arguments;

        /// <summary>
        /// Contains process verb.
        /// </summary>
        private readonly string _verb;

        /// <summary>
        /// True if the start event should be logged.
        /// </summary>
        private readonly bool _logEvent;
    }
}