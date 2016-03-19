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
    /// Provides data for FileWatcherPathError event.
    /// </summary>
    public class FileWatcherPathErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the FileWatcherPathErrorEventArgs class.
        /// </summary>
        /// <param name="daemonName">Daemon name of the file watcher.</param>
        /// <param name="path">Path of the file watcher to watch.</param>
        /// <exception cref="ArgumentNullException">daemonName is null.</exception>
        /// <exception cref="ArgumentNullException">path is null.</exception>
        public FileWatcherPathErrorEventArgs(string daemonName,
                                             string path)
        {
            if (daemonName == null)
            {
                throw new ArgumentNullException("daemonName",
                                                Resources.ArgumentNullException);
            }
            if (path == null)
            {
                throw new ArgumentNullException("path",
                                                Resources.ArgumentNullException);
            }
            _daemonName = daemonName;
            _path = path;
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
        /// Gets path.
        /// </summary>
        public string Path
        {
            get
            {
                return _path;
            }
        }

        /// <summary>
        /// Contains daemon name.
        /// </summary>
        private readonly string _daemonName;

        /// <summary>
        /// Contains path.
        /// </summary>
        private readonly string _path;
    }
}