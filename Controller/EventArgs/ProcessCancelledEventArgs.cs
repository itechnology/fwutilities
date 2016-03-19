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
    /// Provides data for ProcessCanceledEventArgs event.
    /// </summary>
    public class ProcessCanceledEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ProcessCanceledEventArgs class.
        /// </summary>
        /// <param name="daemonName">The name of the daemon.</param>
        /// <param name="reason">Reason of the cancellation.</param>
        /// <param name="fullPath">Full path.</param>
        /// <param name="queuedProcesses">Number of queued processes on the controller.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentException">queued processes value cannot be less than zero.</exception>
        public ProcessCanceledEventArgs(string daemonName,
                                        ProcessCanceledReason reason,
                                        string fullPath,
                                        int queuedProcesses)
        {
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (fullPath == null)
            {
                throw new ArgumentNullException("fullPath",
                                                Resources.ArgumentNullException);           
            }
            if (queuedProcesses < 0)
            {
                throw new ArgumentException(Resources.ArgumentExceptionValueCannotBeLessThanZero,
                                            "queuedProcesses");
            }
            _daemonName = daemonName;
            _reason = reason;
            _fullPath = fullPath;
            _queuedProcesses = queuedProcesses;
        }

        /// <summary>
        /// Gets the name of the daemon.
        /// </summary>
        public string DaemonName
        {
            get
            {
                return _daemonName;
            }
        }

        /// <summary>
        /// Gets the reason.
        /// </summary>
        public ProcessCanceledReason Reason
        {
            get
            {
                return _reason;
            }
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
        }

        /// <summary>
        /// Gets the number of queued processes on the controller.
        /// </summary>
        public int QueuedProcesses
        {
            get
            {
                return _queuedProcesses;
            }
        }

        /// <summary>
        /// Contains daemon name.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// Contains reason.
        /// </summary>
        private readonly ProcessCanceledReason _reason;

        /// <summary>
        /// Contains full path.
        /// </summary>
        private readonly string _fullPath;

        /// <summary>
        /// Number of queued processes on the controller.
        /// </summary>
        private readonly int _queuedProcesses;
    }
}