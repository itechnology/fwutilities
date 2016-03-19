/******************************************************************************
*    File Watcher Utilities / Logger
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
using System.IO;
using System.Security;
using System.Security.Permissions;
using FileWatcherUtilities.Logger.Properties;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.Logger
{
    /// <summary>
    /// Default writer of log messages.
    /// </summary>
    public class DefaultLogger : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the DefaultLogger class.
        /// </summary>
        /// <param name="logFilePath">Path of the log file.</param>
        /// <exception cref="ArgumentException">logFilePath is null or empty.</exception>
        public DefaultLogger(string logFilePath)
        {
            if (String.IsNullOrEmpty(logFilePath))
            {
                throw new ArgumentException(Resources.ArgumentExceptionInvalidPath,
                                            "logFilePath");
            }
            _logFilePath = logFilePath;
        }

        #region ILogger Members

        /// <summary>
        /// Opens log file.
        /// </summary>
        /// <param name="append">True to append to current log file.</param>
        /// <exception cref="UnauthorizedAccessException">Access is denied.</exception>
        /// <exception cref="ArgumentException">Invalid log file.</exception>
        /// <exception cref="DirectoryNotFoundException">Directory is not found.</exception>
        /// <exception cref="IOException">Invalid syntax of file name.</exception>
        /// <exception cref="PathTooLongException">Path is too long.</exception>
        /// <exception cref="SecurityException">Security exception.</exception>
        public void Open(bool append)
        {
            lock (_lockWrite)
            {
                _streamWriter = new StreamWriter(_logFilePath,
                                                 append);
            }
        }

        /// <summary>
        /// Closes log file.
        /// </summary>
        public void Close()
        {
            lock (_lockWrite)
            {
                if (_streamWriter != null)
                {
                    _streamWriter.Close();
                }
            }
        }

        /// <summary>
        /// Writes message to log file.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <exception cref="ObjectDisposedException">Object disposed.</exception>
        /// <exception cref="IOException">IOException.</exception>
        public void Log(string message)
        {
            lock (_lockWrite)
            {
                // Rotate log after 10 megabytes.
                if (_streamWriter.BaseStream.Length >= 10485760)
                {
                    _streamWriter.Close();

                    File.Move(_logFilePath,
                              Path.Combine(Path.GetDirectoryName(_logFilePath),
                                           Path.GetFileNameWithoutExtension(_logFilePath) +
                                           "_" +
                                           Guid.NewGuid().ToString("N") +
                                           Path.GetExtension(_logFilePath)));
                    _streamWriter = new StreamWriter(_logFilePath);
                }
                _streamWriter.WriteLine(message);
                _streamWriter.Flush();
            }
        }

        /// <summary>
        /// Implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">True if disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (_lockWrite)
                {
                    if (_streamWriter != null)
                    {
                        _streamWriter.Dispose();
                        _streamWriter = null;
                    }
                }
            }
        }

        /// <summary>
        /// Lock object.
        /// </summary>
        private readonly object _lockWrite = new object();

        /// <summary>
        /// Contains log file path.
        /// </summary>
        private readonly string _logFilePath;

        /// <summary>
        /// Contains stream writer for log writing.
        /// </summary>
        private StreamWriter _streamWriter;
    }
}