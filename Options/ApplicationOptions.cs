/******************************************************************************
*    File Watcher Utilities / Options
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
using FileWatcherUtilities.Options.Properties;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.Options
{
    /// <summary>
    /// Provides application options.
    /// </summary>
    public class ApplicationOptions
    {
        /// <summary>
        /// Get or set synchronous execution.
        /// </summary>
        public bool SynchronousExecution
        {
            get
            {
                return _synchronousExecution;
            }
            set
            {
                _synchronousExecution = value;
            }
        }

        /// <summary>
        /// Get or set run queued processes.
        /// </summary>
        public bool RunQueuedProcesses
        {
            get
            {
                return _runQueuedProcesses;
            }
            set
            {
                _runQueuedProcesses = value;
            }
        }

        /// <summary>
        /// Get or set amount of log messages to display.
        /// </summary>
        public int LogMessages
        {
            get
            {
                return _logMessages;
            }
            set
            {
                if (value < MinLogMessages ||
                    value > MaxLogMessages)
                {
                    throw new ArgumentException(Resources.ArgumentExceptionInvalidLogMessageValue,
                                                "value");
                }
                _logMessages = value;
            }
        }

        /// <summary>
        /// Get or set auto startup.
        /// </summary>
        public bool AutoStartup
        {
            get
            {
                return _autoStartup;
            }
            set
            {
                _autoStartup = value;
            }
        }

        /// <summary>
        /// Gets or sets process batch size.
        /// </summary>
        public int ProcessBatchSize
        {
            get
            {
                return _processBatchSize;
            }
            set
            {
                _processBatchSize = value;
            }
        }

        /// <summary>
        /// Maximum number of log messages to display.
        /// </summary>
        private const int MaxLogMessages = 1000;

        /// <summary>
        /// Minumum number of log messages to display.
        /// </summary>
        private const int MinLogMessages = 10;

        /// <summary>
        /// Process batch size.
        /// </summary>
        private int _processBatchSize;

        /// <summary>
        /// Contains auto startup. True if to start file watchers at application startup.
        /// </summary>
        private bool _autoStartup;

        /// <summary>
        /// Contains run queued processes value.
        /// </summary>
        private bool _runQueuedProcesses;

        /// <summary>
        /// Contains synchronous execution value.
        /// </summary>
        private bool _synchronousExecution;

        /// <summary>
        /// Contains amount of log messages to display. Default is "100".
        /// </summary>
        private int _logMessages = 100;
    }
}