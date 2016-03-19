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
    /// Provides data for ProcessData event.
    /// </summary>
    public class ProcessDataEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ProcessDataEventArgs class.
        /// </summary>
        /// <param name="daemonName">The name of the daemon which is running the process.</param>
        /// <param name="processId">Process Id.</param>
        /// <param name="data">Process data.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentNullException">data is null.</exception>
        public ProcessDataEventArgs(string daemonName,
                                    int processId,
                                    string data)
        {
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (data == null)
            {
                throw new ArgumentNullException("data",
                                                Resources.ArgumentNullException);
            }
            _daemonName = daemonName;
            _processId = processId;
            _data = data;
        }

        /// <summary>
        /// Gets the name of the daemon which is running the process.
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
        /// Gets the process data.
        /// </summary>
        public string Data
        {
            get
            {
                return _data;
            }
        }

        /// <summary>
        /// Contains daemon name.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// Contains process Id.
        /// </summary>
        private readonly int _processId;

        /// <summary>
        /// Contains process data.
        /// </summary>
        private readonly string _data;
    }
}