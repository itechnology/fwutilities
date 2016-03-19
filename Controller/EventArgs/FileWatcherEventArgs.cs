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
using System.Collections.Generic;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Provides data for SystemChanged event.
    /// </summary>
    public class FileWatcherEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the FileWatcherEventArgs class.
        /// </summary>
        /// <param name="configurationKeyValuePair">Configuration KeyValuePair.</param>
        /// <param name="changeType">Type of the file system change.</param>
        /// <param name="fileName">Name of the changed file or directory.</param>
        /// <param name="fullPath">Full path of the changed file or directory.</param>
        /// <param name="oldFullPath">Old full path of the changed file or directory.</param>
        /// <exception cref="ArgumentNullException">configurationKeyValuePair is null.</exception>
        /// <exception cref="ArgumentNullException">changeType is null.</exception>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        /// <exception cref="ArgumentNullException">fullPath is null.</exception>
        /// <exception cref="ArgumentNullException">oldFullPath is null.</exception>
        public FileWatcherEventArgs(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair,
                                    string changeType,
                                    string fileName,
                                    string fullPath,
                                    string oldFullPath)
        {
            if (configurationKeyValuePair.Value == null)
            {
                throw new ArgumentNullException("configurationKeyValuePair",
                                                Resources.ArgumentNullException);
            }
            if (changeType == null)
            {
                throw new ArgumentNullException("changeType",
                                                Resources.ArgumentNullException);
            }
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName",
                                                Resources.ArgumentNullException);
            }
            if (fullPath == null)
            {
                throw new ArgumentNullException("fullPath",
                                                Resources.ArgumentNullException);
            }
            if (oldFullPath == null)
            {
                throw new ArgumentNullException("oldFullPath",
                                                Resources.ArgumentNullException);
            }
            _configurationKeyValuePair = configurationKeyValuePair;
            _changeType = changeType;
            _fileName = fileName;
            _fullPath = fullPath;
            _oldFullPath = oldFullPath;
            _guid = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the FileWatcherEventArgs class.
        /// </summary>
        /// <param name="configurationKeyValuePair">Configuration KeyValuePair.</param>
        /// <param name="changeType">Type of the file system change.</param>
        /// <param name="fileName">Name of the changed file or directory.</param>
        /// <param name="fullPath">Full path of the changed file or directory.</param>
        /// <param name="oldFullPath">Old full path of the changed file or directory.</param>
        /// <param name="id">Id of the event.</param>
        /// <exception cref="ArgumentNullException">configurationKeyValuePair is null.</exception>
        /// <exception cref="ArgumentNullException">changeType is null.</exception>
        /// <exception cref="ArgumentNullException">fileName is null.</exception>
        /// <exception cref="ArgumentNullException">fullPath is null.</exception>
        /// <exception cref="ArgumentNullException">oldFullPath is null.</exception>
        /// <exception cref="ArgumentException">Id is empty.</exception>
        public FileWatcherEventArgs(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair,
                                    string changeType,
                                    string fileName,
                                    string fullPath,
                                    string oldFullPath,
                                    Guid id)
        {
            if (configurationKeyValuePair.Value == null)
            {
                throw new ArgumentNullException("configurationKeyValuePair",
                                                Resources.ArgumentNullException);
            }
            if (changeType == null)
            {
                throw new ArgumentNullException("changeType",
                                                Resources.ArgumentNullException);
            }
            if (fileName == null)
            {
                throw new ArgumentNullException("fileName",
                                                Resources.ArgumentNullException);
            }
            if (fullPath == null)
            {
                throw new ArgumentNullException("fullPath",
                                                Resources.ArgumentNullException);
            }
            if (oldFullPath == null)
            {
                throw new ArgumentNullException("oldFullPath",
                                                Resources.ArgumentNullException);
            }
            if (id == Guid.Empty)
            {
                throw new ArgumentException(Resources.ArgumentExceptionIdIsEmpty,
                                            "id");
            }
            _configurationKeyValuePair = configurationKeyValuePair;
            _changeType = changeType;
            _fileName = fileName;
            _fullPath = fullPath;
            _oldFullPath = oldFullPath;
            _guid = id;
        }

        /// <summary>
        /// Gets the configurationKeyValuePair.
        /// </summary>
        public KeyValuePair<string, FileWatcherConfigurationSet> ConfigurationKeyValuePair
        {
            get
            {
                return _configurationKeyValuePair;
            }
        }

        /// <summary>
        /// Gets the name of the changed file or directory (relative path to directory being watched).
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }
        }

        /// <summary>
        /// Gets the full path of the changed file or directory.
        /// </summary>
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
        }

        /// <summary>
        /// Gets the old full path of the changed file or directory.
        /// </summary>
        public string OldFullPath
        {
            get
            {
                return _oldFullPath;
            }
        }

        /// <summary>
        /// Gets the change type of the file or directory.
        /// </summary>
        public string ChangeType
        {
            get
            {
                return _changeType;
            }
        }

        /// <summary>
        /// Gets the date and time of the file or directory change. Uses local time.
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
        }

        /// <summary>
        /// Identifies event.
        /// </summary>
        public Guid Id
        {
            get
            {
                return _guid;
            }
        }

        /// <summary>
        /// Identifies event.
        /// </summary>
        private readonly Guid _guid;

        /// <summary>
        /// Contains configurationKeyValuePair.
        /// </summary>
        private readonly KeyValuePair<string, FileWatcherConfigurationSet> _configurationKeyValuePair;

        /// <summary>
        /// Contains the date and time of the file system change.
        /// </summary>
        private readonly DateTime _dateTime = DateTime.Now;

        /// <summary>
        /// Contains the type of file system change.
        /// </summary>
        private readonly string _changeType;

        /// <summary>
        /// Contains the name of the changed file or directory.
        /// </summary>
        private readonly string _fileName;

        /// <summary>
        /// Contains the full path of the changed file or directory.
        /// </summary>
        private readonly string _fullPath;

        /// <summary>
        /// Contains the old full path of the changed file or directory.
        /// </summary>
        private readonly string _oldFullPath;
    }
}