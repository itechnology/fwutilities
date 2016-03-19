/**********************************************************************************
*   File Watcher Utilities / File Watcher Service Contract
*   Copyright (c) 2006-2013 Jussi Hiltunen
*
*   This library is free software; you can redistribute it and/or
*   modify it under the terms of the GNU Lesser General Public
*   License as published by the Free Software Foundation; either
*   version 2.1 of the License, or (at your option) any later version.
*
*   This library is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
*   Lesser General Public License for more details.
*
*   You should have received a copy of the GNU Lesser General Public
*   License along with this library; if not, write to the Free Software
*   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
**********************************************************************************/

using System;
using System.IO;
using System.ServiceModel;

namespace FileWatcherUtilities.FileWatcherServiceContract
{
    /// <summary>
    /// SystemChanged request messagecontract.
    /// </summary>
    [MessageContract]
    public class SystemChangedReqMC
    {
        /// <summary>
        /// Gets or sets the machine name that send the message.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string MachineName
        {
            get
            {
                return _machineName;
            }
            set
            {
                _machineName = value;
            }
        }

        /// <summary>
        /// Gets or sets the daemon name of the file watcher.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string DaemonName
        {
            get
            {
                return _daemonName;
            }
            set
            {
                _daemonName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the changed file or directory (relative path to directory being watched).
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        /// <summary>
        /// Gets or sets the full path of the changed file or directory.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string FullPath
        {
            get
            {
                return _fullPath;
            }
            set
            {
                _fullPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the old full path of the changed file or directory.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string OldFullPath
        {
            get
            {
                return _oldFullPath;
            }
            set
            {
                _oldFullPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the change type of the file or directory.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string ChangeType
        {
            get
            {
                return _changeType;
            }
            set
            {
                _changeType = value;
            }
        }

        /// <summary>
        /// Gets or sets the date and time of the file or directory change. Uses local time.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public DateTime DateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
            }
        }

        /// <summary>
        /// Gets or sets event identifier.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public Guid Id
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }

        /// <summary>
        /// Gets or sets file stream length.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public long FileStreamLength
        {
            get
            {
                return _fileStreamLength;
            }
            set
            {
                _fileStreamLength = value;
            }
        }

        /// <summary>
        /// Gets or sets file checksum.
        /// </summary>
        [MessageHeader(MustUnderstand = true)]
        public string Checksum
        {
            get
            {
                return _checksum;
            }
            set
            {
                _checksum = value;
            }
        }

        /// <summary>
        /// Gets or sets streamed file.
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public Stream FileStream
        {
            get
            {
                return _fileStream;
            }
            set
            {
                _fileStream = value;
            }
        }

        /// <summary>
        /// Length of the streamed file.
        /// </summary>
        private long _fileStreamLength;

        /// <summary>
        /// Streamed file.
        /// </summary>
        private Stream _fileStream;

        /// <summary>
        /// Contains the machine name that send the message.
        /// </summary>
        private string _machineName;

        /// <summary>
        /// Containts the daemon name of the file watcher.
        /// </summary>
        private string _daemonName;

        /// <summary>
        /// Identifies event.
        /// </summary>
        private Guid _guid;

        /// <summary>
        /// Contains the date and time of the file system change.
        /// </summary>
        private DateTime _dateTime;

        /// <summary>
        /// Contains the type of file system change.
        /// </summary>
        private string _changeType;

        /// <summary>
        /// Contains the name of the changed file or directory.
        /// </summary>
        private string _fileName;

        /// <summary>
        /// Contains the full path of the changed file or directory.
        /// </summary>
        private string _fullPath;

        /// <summary>
        /// Contains the old full path of the changed file or directory.
        /// </summary>
        private string _oldFullPath;

        /// <summary>
        /// Constains the checksum of the streamed file.
        /// </summary>
        private string _checksum;
    }
}