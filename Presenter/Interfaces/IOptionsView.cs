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
using FileWatcherUtilities.Options;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Interface for options view.
    /// </summary>
    public interface IOptionsView
    {
        /// <summary>
        /// Occures when application options are ready to be saved.
        /// </summary>
        event EventHandler<EventArgs> Save;

        /// <summary>
        /// Display application options on the view.
        /// </summary>
        /// <param name="applicationOptions">Application options to display.</param>
        void ViewApplicationOptions(ApplicationOptions applicationOptions);

        /// <summary>
        /// Returns modified application options.
        /// </summary>
        /// <returns>Modified application options.</returns>
        ApplicationOptions NewApplicationOptions();

        /// <summary>
        /// Shows view.
        /// </summary>
        void ShowView();

        /// <summary>
        /// Hides view.
        /// </summary>
        void HideView();

        /// <summary>
        /// Sets or gets synchronous execution enabled.
        /// </summary>
        bool SynchronousExecutionEnabled { get; set; }
    }
}