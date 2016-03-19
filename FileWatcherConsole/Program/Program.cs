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
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Security.Permissions;
using FileWatcherUtilities.Presenter;
using FileWatcherUtilities.FileWatcherConsole.Properties;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.FileWatcherConsole
{
    /// <summary>
    /// Main class of the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                ConsoleView consoleView = new ConsoleView();
                _presenterBuilder = new PresenterBuilder(consoleView);
                _presenterBuilder.Build();
                // Start application.
                consoleView.Start(args);
            }
            catch (InvalidDataException ex)
            {
                Console.Clear();
                Console.WriteLine(Application.ProductName + Resources.Space + Application.ProductVersion);
                Console.WriteLine(String.Empty);
                Console.WriteLine(@ex.Message);
                Console.WriteLine(String.Empty);
                Console.WriteLine(Resources.MessagePressAnyKeyToContinue);
                Console.ReadKey();
                Environment.Exit(255);
            }
            catch (XmlException ex)
            {
                Console.Clear();
                Console.WriteLine(Application.ProductName + Resources.Space + Application.ProductVersion);
                Console.WriteLine(String.Empty);
                Console.WriteLine(@ex.Message);
                Console.WriteLine(String.Empty);
                Console.WriteLine(Resources.MessagePressAnyKeyToContinue);
                Console.ReadKey();
                Environment.Exit(255);
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine(Application.ProductName + Resources.Space + Application.ProductVersion);
                Console.WriteLine(String.Empty);
                Console.WriteLine(@Resources.MessageUnexpectedError,
                                  @ex.Message);
                Console.WriteLine(String.Empty);
                Console.WriteLine(Resources.MessagePressAnyKeyToContinue);
                Console.ReadKey();
                Environment.Exit(255);
            }
            finally
            {
                if (_presenterBuilder != null)
                {
                    _presenterBuilder.Dispose();
                }
            }
        }

        /// <summary>
        /// Presenter builder.
        /// </summary>
        private static PresenterBuilder _presenterBuilder;
    }
}