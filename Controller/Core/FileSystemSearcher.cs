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
using System.IO;
using System.Security;
using System.Threading;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Providers filtered file system searching for files.
    /// </summary>
    internal sealed class FileSystemSearcher
    {
        /// <summary>
        /// Occurs when file system searcher encountered an error.
        /// </summary>
        public event EventHandler<FileSystemSearcherErrorEventArgs> OnFileSystemSearchError;

        /// <summary>
        /// Occurs when file system searcher advances to new directory.
        /// </summary>
        public event EventHandler<FileSystemSearcherProgressEventArgs> OnFileSystemSearchProgress;

        /// <summary>
        /// Gets the collection of found files that matched the regular expression.
        /// </summary>
        public Collection<FileInfo> FoundFiles
        {
            get
            {
                return _foundFiles;
            }
        }

        /// <summary>
        /// Searches file system.
        /// </summary>
        /// <param name="path">Path to start searching.</param>
        /// <param name="includeSubdirectories">True if to search subdirectories.</param>
        /// <param name="regexFilterFileName">Regular expressin for filtering files. Empty string to disable filtering.</param>
        /// <exception cref="ArgumentException">Invalid regular expression.</exception>
        public void Search(string path,
                           bool includeSubdirectories,
                           string regexFilterFileName)
        {
            if (path == null)
            {
                throw new ArgumentNullException(null,
                                                Resources.ArgumentNullException);
            }
            if (!String.IsNullOrEmpty(regexFilterFileName))
            {
                _internalRegexFilterFileName = new Regex(regexFilterFileName);
            }

            _internalIncludeSubdirectories = includeSubdirectories;

            try
            {
                SearchDirectory(path);
            }
            catch (ArgumentException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (PathTooLongException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (SecurityException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (DirectoryNotFoundException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (IOException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
        }

        /// <summary>
        /// Recursive search of directories and files.
        /// </summary>
        /// <param name="path">Path to search.</param>
        private void SearchDirectory(string path)
        {
            // Get directory.
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            directoryInfo.Refresh();

            // Raise progress event.
            RaiseFileSystemSearchProgessEvent(directoryInfo.FullName);

            try
            {
                // Get files in directory.
                FileInfo[] fileInfos = directoryInfo.GetFiles();

                foreach (FileInfo fileInfo in fileInfos)
                {
                    if (IsMatchFilterFileName(fileInfo.Name))
                    {
                        _foundFiles.Add(fileInfo);
                    }
                }
            }
            catch (DirectoryNotFoundException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (IOException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                RaiseFileSystemSearchErrorEvent(exception);
            }

            // Search subdirectories.
            if (_internalIncludeSubdirectories)
            {
                try
                {
                    // Get directories in current directory.
                    DirectoryInfo[] directories = directoryInfo.GetDirectories();

                    foreach (DirectoryInfo directory in directories)
                    {
                        try
                        {
                            SearchDirectory(directory.FullName);
                        }
                        catch (ArgumentException exception)
                        {
                            RaiseFileSystemSearchErrorEvent(exception);
                        }
                        catch (UnauthorizedAccessException exception)
                        {
                            RaiseFileSystemSearchErrorEvent(exception);
                        }
                        catch (PathTooLongException exception)
                        {
                            RaiseFileSystemSearchErrorEvent(exception);
                        }
                        catch (SecurityException exception)
                        {
                            RaiseFileSystemSearchErrorEvent(exception);
                        }
                        catch (DirectoryNotFoundException exception)
                        {
                            RaiseFileSystemSearchErrorEvent(exception);
                        }
                        catch (IOException exception)
                        {
                            RaiseFileSystemSearchErrorEvent(exception);
                        }
                    }
                }
                catch (DirectoryNotFoundException exception)
                {
                    RaiseFileSystemSearchErrorEvent(exception);
                }
            }
        }

        /// <summary>
        /// Raises search error event.
        /// </summary>
        /// <param name="exception">Occured exception.</param>
        private void RaiseFileSystemSearchErrorEvent(Exception exception)
        {
            EventHandler<FileSystemSearcherErrorEventArgs> handler = OnFileSystemSearchError;
            if (handler != null)
            {
                handler(this,
                        new FileSystemSearcherErrorEventArgs(exception));
            }
        }

        /// <summary>
        /// Raises search progress event.
        /// </summary>
        /// <param name="directoryFullPath">Directory full path.</param>
        private void RaiseFileSystemSearchProgessEvent(string directoryFullPath)
        {
            EventHandler<FileSystemSearcherProgressEventArgs> handler = OnFileSystemSearchProgress;
            if (handler != null)
            {
                handler(this,
                        new FileSystemSearcherProgressEventArgs(directoryFullPath));
            }
        }

        /// <summary>
        /// Matches regular expression filter to full path.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <returns>True if regular expression finds a match in the full path string.</returns>
        /// <remarks>Null regular expression returns always true.</remarks>
        private bool IsMatchFilterFileName(string fileName)
        {
            // No filtering.
            if (_internalRegexFilterFileName == null)
            {
                return true;
            }
            return (_internalRegexFilterFileName.IsMatch(fileName));
        }

        /// <summary>
        /// File name regular expression filter.
        /// </summary>
        private Regex _internalRegexFilterFileName;

        /// <summary>
        /// True if to search subdirectories.
        /// </summary>
        private bool _internalIncludeSubdirectories;

        /// <summary>
        /// Contains collection of found files that match the regex filer.
        /// </summary>
        private readonly Collection<FileInfo> _foundFiles = new Collection<FileInfo>();
    }
}