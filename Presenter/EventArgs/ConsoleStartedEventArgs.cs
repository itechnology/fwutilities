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
using System.Collections.ObjectModel;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides data for ConsoleStarted event.
    /// </summary>
    public class ConsoleStartedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ConsoleStartedEventArgs class.
        /// </summary>
        /// <param name="args">Console args.</param>
        /// <exception cref="ArgumentNullException">args is null.</exception>
        public ConsoleStartedEventArgs(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args",
                                                Resources.ArgumentNullException);
            }
            for (int i = 0; i < args.Length; i++)
            {
                _args.Add(args[i]);
            }
        }

        /// <summary>
        /// Console args.
        /// </summary>
        private readonly Collection<string> _args = new Collection<string>();

        /// <summary>
        /// Gets console args.
        /// </summary>
        public Collection<string> Args
        {
            get
            {
                return _args;
            }
        }
    }
}