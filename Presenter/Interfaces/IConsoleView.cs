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

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Interface for console view.
    /// </summary>
    public interface IConsoleView
    {
        /// <summary>
        /// Start console view.
        /// </summary>
        event EventHandler<ConsoleStartedEventArgs> ConsoleStarted;

        /// <summary>
        /// Writes message.
        /// </summary>
        /// <param name="message">Message to write.</param>
        void Echo(string message);

        /// <summary>
        /// Writes line.
        /// </summary>
        /// <param name="message">Message to write.</param>
        void EchoLine(string message);

        /// <summary>
        /// Reads key.
        /// </summary>
        /// <returns>Key.</returns>
        string ReadKey();

        /// <summary>
        /// Writes line and then reads key.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <returns>Key.</returns>
        string EchoReadKey(string message);

        /// <summary>
        /// Reads line.
        /// </summary>
        /// <returns>Line.</returns>
        string ReadLine();

        /// <summary>
        /// Writes line an then reads line.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <returns>Line.</returns>
        string EchoReadLine(string message);
    }
}