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
using FileWatcherUtilities.Options.Properties;

namespace FileWatcherUtilities.Options
{
    /// <summary>
    /// Provides data for ApplicationOptionsChanged event.
    /// </summary>
    public class ApplicationOptionsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationOptionsChangedEventArgs class.
        /// </summary>
        /// <param name="applicationOptions">ApplicationOptions.</param>
        /// <exception cref="ArgumentNullException">applicationOptions is null.</exception>
        public ApplicationOptionsChangedEventArgs(ApplicationOptions applicationOptions)
        {
            if (applicationOptions == null)
            {
                throw new ArgumentNullException("applicationOptions",
                                                Resources.ArgumentNullException);
            }
            _applicationOptions = applicationOptions;
        }

        /// <summary>
        /// Gets application options.
        /// </summary>
        public ApplicationOptions  ApplicationOptions 
        {
            get
            {
                return _applicationOptions;
            }
        }

        /// <summary>
        /// Application options.
        /// </summary>
        private readonly ApplicationOptions _applicationOptions;
    }
}
