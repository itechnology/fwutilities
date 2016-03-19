/******************************************************************************
*    File Watcher Utilities / File Watcher Console
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
using FileWatcherUtilities.Presenter;

namespace FileWatcherUtilities.FileWatcherConsole
{
    /// <summary>
    /// Provides console view.
    /// </summary>
    public class ConsoleView : IConsoleView
    {
        #region IConsoleView Members

        /// <summary>
        /// Occures when console view is started.
        /// </summary>
        public event EventHandler<ConsoleStartedEventArgs> ConsoleStarted;

        /// <summary>
        /// Writes message.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public void Echo(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Writes line.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public void EchoLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Reads key.
        /// </summary>
        /// <returns>Key.</returns>
        public string ReadKey()
        {
            return Console.ReadKey().ToString();
        }

        /// <summary>
        /// Writes line and then reads key.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <returns>Key.</returns>
        public string EchoReadKey(string message)
        {
            Console.WriteLine(message);
            return Console.ReadKey().ToString();
        }

        /// <summary>
        /// Reads line.
        /// </summary>
        /// <returns>Line.</returns>
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Writes line an then reads line.
        /// </summary>
        /// <param name="message">Message to write.</param>
        /// <returns>Line.</returns>
        public string EchoReadLine(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }

        #endregion

        /// <summary>
        /// Start console view.
        /// </summary>
        /// <param name="args">Console view argumets.</param>
        public void Start(string[] args)
        {
            if (ConsoleStarted != null)
            {
                ConsoleStarted(this, 
                               new ConsoleStartedEventArgs(args));
            }
        }
    }
}