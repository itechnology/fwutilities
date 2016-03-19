/******************************************************************************
*    File Watcher Utilities / Presenter
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

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Interface for main view.
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// Occures when start is selected.
        /// </summary>
        event EventHandler<EventArgs> Start;

        /// <summary>
        /// Occures when stop is selected.
        /// </summary>
        event EventHandler<EventArgs> Stop;
        
        /// <summary>
        /// Occures when stop all is selected.
        /// </summary>
        event EventHandler<EventArgs> StopAll;

        /// <summary>
        /// Occures when start all is selected.
        /// </summary>
        event EventHandler<EventArgs> StartAll;

        /// <summary>
        /// Occures when delete is selected.
        /// </summary>
        event EventHandler<EventArgs> Delete;

        /// <summary>
        /// Occures when exit is selected.
        /// </summary>
        event EventHandler<EventArgs> Exit;

        /// <summary>
        /// Occures when list initializes.
        /// </summary>
        event EventHandler<EventArgs> ListInitialize;

        /// <summary>
        /// Occures when daemon is selected.
        /// </summary>
        event EventHandler<DaemonSelectedEventArgs> DaemonSelected;

        /// <summary>
        /// Occures when viewing new.
        /// </summary>
        event EventHandler<EventArgs> ViewNew;

        /// <summary>
        /// Occures when viewing properties.
        /// </summary>
        event EventHandler<EventArgs> ViewProperties;

        /// <summary>
        /// Occures when viewing options.
        /// </summary>
        event EventHandler<EventArgs> ViewOptions;

        /// <summary>
        /// Occures when viewing about.
        /// </summary>
        event EventHandler<EventArgs> ViewAbout;

        /// <summary>
        /// Occures when viewing help.
        /// </summary>
        event EventHandler<EventArgs> ViewHelp;

        /// <summary>
        /// Occures when viewing license.
        /// </summary>
        event EventHandler<EventArgs> ViewLicense;

        /// <summary>
        /// Occures when viewing log.
        /// </summary>
        event EventHandler<EventArgs> ViewLog;

        /// <summary>
        /// Gets or sets new enabled.
        /// </summary>
        bool NewEnabled { get; set; }

        /// <summary>
        /// Gets or sets start enabled.
        /// </summary>
        bool StartEnabled { get; set; }

        /// <summary>
        /// Gets or sets stop enabled.
        /// </summary>
        bool StopEnabled { get; set; }

        /// <summary>
        /// Gets or sets start all enabled.
        /// </summary>
        bool StartAllEnabled { get; set; }

        /// <summary>
        /// Gets or sets stop all enabled.
        /// </summary>
        bool StopAllEnabled { get; set; }

        /// <summary>
        /// Gets or sets delete enabled.
        /// </summary>
        bool DeleteEnabled { get; set; }

        /// <summary>
        /// Gets or sets properties enabled.
        /// </summary>
        bool PropertiesEnabled { get; set; }

        /// <summary>
        /// Gets or sets exit enabled.
        /// </summary>
        bool ExitEnabled { get; set; }
 
        /// <summary>
        /// Updates file watcher list.
        /// </summary>
        /// <param name="fileWatcherSortedDictionary">List of file watcher info.</param>
        void UpdateList(SortedDictionary<string, FileWatcherInfo> fileWatcherSortedDictionary);

        /// <summary>
        /// Updates number of running processes.
        /// </summary>
        /// <param name="runningProcesses">Number of running processes.</param>
        void UpdateRunningProcesses(int runningProcesses);
        
        /// <summary>
        /// Updates number of running file watchers.
        /// </summary>
        /// <param name="runningFileWatchers">Number of running file watchers.</param>
        void UpdateRunningFileWatchers(int runningFileWatchers);

        /// <summary>
        /// Updates number of processes to run.
        /// </summary>
        /// <param name="processesToRun">Number of processes to run.</param>
        void UpdateProcessesToRun(int processesToRun);

        /// <summary>
        /// Shows file watcher path error message.
        /// </summary>
        /// <param name="message">message.</param>
        void ShowFileWatcherPathErrorMessage(string message);
    }
}