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
using FileWatcherUtilities.Controller;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Interface for properties view.
    /// </summary>
    public interface IPropertiesView
    {
        /// <summary>
        /// Occures when configuration is ready to be saved.
        /// </summary>
        event EventHandler<EventArgs> Save;

        /// <summary>
        /// Display file watcher configuration on the view.
        /// </summary>
        /// <param name="configurationKeyValuePair">File watcher configuration to display.</param>
        void ViewFileWatcherConfiguration(KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair);

        /// <summary>
        /// Returns new or modified file watcher configuration set.
        /// </summary>
        /// <returns>New or modified file watcher configuration set.</returns>
        KeyValuePair<string, FileWatcherConfigurationSet> NewConfigurationKeyValuePair();        
        
        /// <summary>
        /// Shows view.
        /// </summary>
        void ShowView();
        
        /// <summary>
        /// Hides view.
        /// </summary>
        void HideView();

        /// <summary>
        /// Shows error message.
        /// </summary>
        /// <param name="message">Error message to display.</param>
        void ShowError(string message);
    }
}