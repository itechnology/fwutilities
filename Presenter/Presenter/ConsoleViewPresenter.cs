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
using System.Text;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using FileWatcherUtilities.Logger;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides console view presenter.
    /// </summary>
    public class ConsoleViewPresenter : MainViewPresenterBase
    {
        /// <summary>
        /// Initializes a new instance of the ConsoleViewPresenter class.
        /// </summary>
        /// <param name="consoleView">Console view.</param>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="logger">Logger.</param>
        /// <param name="formatter">Log formatter.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        public ConsoleViewPresenter(IConsoleView consoleView,
                                    FileWatcherController fileWatcherController,
                                    ILogger logger,
                                    IFormatter formatter,
                                    string xmlConfigFilePath,
                                    string xmlSchemaConfigFilePath)
            : base(fileWatcherController, 
                   logger,
                   formatter)
        {
            if (consoleView == null)
            {
                throw new ArgumentNullException("consoleView",
                                                Resources.ArgumentNullException);
            }
            if (xmlConfigFilePath == null)
            {
                throw new ArgumentNullException("xmlConfigFilePath",
                                                Resources.ArgumentNullException);
            }
            if (xmlSchemaConfigFilePath == null)
            {
                throw new ArgumentNullException("xmlSchemaConfigFilePath",
                                                Resources.ArgumentNullException);
            }
            _consoleView = consoleView;
            _xmlConfigFilePath = xmlConfigFilePath;
            _xmlSchemaConfigFilePath = xmlSchemaConfigFilePath;
            SubscribeToConsoleViewEvents();
        }

        #region Console view events

        /// <summary>
        /// Handles console started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected void OnConsoleStarted(object sender, 
                                        ConsoleStartedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            PrintApplicationInfo();
            if (ProcessCommandLineArguments(e.Args))
            {
                RunApplication();
            }
            else
            {
                _consoleView.EchoReadLine(String.Empty);
            }
        }

        #endregion

        /// <summary>
        /// Runs the application.
        /// </summary>
        private void RunApplication()
        {
            base.WriteApplicationStartedMessage();
            if (FileWatcherController.CanStartAllFileWatchers())
            {
                StartAllFileWatchers();
                _consoleView.EchoReadLine(String.Empty);
                StopAllFileWatchers();
                WaitForControllerStopped();
            }
            else
            {
                _consoleView.EchoLine(@Resources.MessageNoDaemonsToStart);
                _consoleView.EchoReadLine(@Resources.MessagePressEnterToExit);
            }
            base.WriteApplicationStoppedMessage();
            Dispose();
        }

        #region Command line arguments

        /// <summary>
        /// Processes command line arguments.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        /// <returns>Returns true if to start main application.</returns>
        private bool ProcessCommandLineArguments(Collection<string> args)
        {
            const int MaxArguments = 3;

            if (args.Count == 0)
            {
                FileWatcherController.SynchronousExecution = true;
                return true;
            }
            if (args.Count > MaxArguments)
            {
                return false;
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (string item in args)
            {
                stringBuilder.Append(item);
            }

            string arguments = stringBuilder.ToString();

            if (arguments.Length > 9)
            {
                PrintUsage();
                return false;
            }
            if (String.Compare(arguments, @"/?", StringComparison.Ordinal) == 0)
            {
                PrintUsage();
                return false;
            }
            if (String.Compare(arguments, @"-a", StringComparison.Ordinal) == 0)
            {
                return true;
            }
            if (String.Compare(arguments, @"-e", StringComparison.Ordinal) == 0)
            {
                WriteExampleDaemonConfiguration();
                return false;
            }
            if (String.Compare(arguments, @"-l", StringComparison.Ordinal) == 0)
            {
                PrintLicense();
                return false;
            }
            if (String.Compare(arguments, @"-r", StringComparison.Ordinal) == 0)
            {
                FileWatcherController.SynchronousExecution = true;
                FileWatcherController.RunQueuedProcesses = true;
                return true;
            }
            if (String.Compare(arguments, @"-a-r", StringComparison.Ordinal) == 0 &&
                args.Count == 2)
            {
                FileWatcherController.RunQueuedProcesses = true;
                return true;
            }
            if (Regex.IsMatch(arguments, @"((-a-b:){1,1}([1-9]){1,2}){1,1}") &&
                args.Count == 2)
            {
                int batchSize = Convert.ToInt32(args[1].Substring(3), CultureInfo.InvariantCulture);
                FileWatcherController.ProcessBatchSize = batchSize;
                return true;
            }
            if (Regex.IsMatch(arguments, @"((-a-r-b:){1,1}([1-9]){1,2}){1,1}") &&
                args.Count == 3)
            {
                int batchSize = Convert.ToInt32(args[2].Substring(3), CultureInfo.InvariantCulture);
                FileWatcherController.ProcessBatchSize = batchSize;
                FileWatcherController.RunQueuedProcesses = true;
                return true;
            }

            PrintUsage();
            return false;
        }

        #endregion Command line arguments

        #region File watcher controller events

        /// <summary>
        /// Handles system changed event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnSystemChanged(object sender,
                                                FileWatcherEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnSystemChanged(sender, e);
            if (e.ConfigurationKeyValuePair.Value.DisplayFileSystemChange)
            {
                _consoleView.EchoLine(String.Format(CultureInfo.CurrentCulture,
                                                    Resources.MessageFileChanged,
                                                    @e.ChangeType,
                                                    @e.FileName));
            }
        }

        /// <summary>
        /// Handles controller stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnControllerStopped(object sender, 
                                                    EventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnControllerStopped(sender, e);
            // Set exit.
            _canExit.Set();
        }

        /// <summary>
        /// Handles buffer error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnBufferError(object sender,
                                              FileWatcherBufferErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnBufferError(sender, e);
            _consoleView.EchoLine(String.Format(CultureInfo.CurrentCulture,
                                               @Resources.MessageBufferError,                                               
                                               e.DaemonName,
                                               e.ErrorEventArgs.GetException().Message));
        }

        /// <summary>
        /// Handles file watcher started event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherStarted(object sender,
                                                     FileWatcherStartedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherStarted(sender, e);
            _consoleView.EchoLine(String.Format(CultureInfo.CurrentCulture,
                                               @Resources.MessageDaemonStarted,
                                               @e.DaemonName));
            // Set can stop all.
            _canStopAll.Set();
        }

        /// <summary>
        /// Handles file watcher stopped event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherStopped(object sender,
                                                     FileWatcherStoppedEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherStopped(sender, e);
            _consoleView.EchoLine(String.Format(CultureInfo.CurrentCulture,
                                               @Resources.MessageDaemonStopped,
                                               @e.DaemonName));
        }

        /// <summary>
        /// Handles file watcher searcher error.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherSearchError(object sender, 
                                                         FileWatcherSearchErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherSearchError(sender, e);
            if (e.DisplayEvent)
            {
                _consoleView.EchoLine(String.Format(CultureInfo.InvariantCulture,
                                                   @Resources.MessageSearchError,
                                                   @e.Exception.Message));
            }
        }

        /// <summary>
        /// Handles file watcher search process event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherSearchProgress(object sender, 
                                                            FileWatcherSearchProgressEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherSearchProgress(sender, e);
            if (e.DisplayEvent)
            {
                _consoleView.EchoLine(String.Format(CultureInfo.InvariantCulture,
                                                   Resources.MessageSearchProgress,
                                                   e.DirectoryFullPath));
            }
        }

        /// <summary>
        /// Handles file watcher path error event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        /// <exception cref="ArgumentNullException">e is null.</exception>
        protected override void OnFileWatcherPathError(object sender,
                                                       FileWatcherPathErrorEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e",
                                                Resources.ArgumentNullException);
            }

            base.OnFileWatcherPathError(sender, e);
            _consoleView.EchoLine(String.Format(CultureInfo.InvariantCulture,
                                               Resources.MessageDaemonPathErrorConsole,
                                               e.DaemonName, e.Path));
            // Set can stop all. Avoid deadlock if all file watchers have invalid path.
            _canStopAll.Set();
        }

        #endregion

        /// <summary>
        /// Subscribes to all console view events.
        /// </summary>
        private void SubscribeToConsoleViewEvents()
        {
            _consoleView.ConsoleStarted +=
                new EventHandler<ConsoleStartedEventArgs>(OnConsoleStarted);
        }

        /// <summary>
        /// Starts file watchers.
        /// </summary>
        private void StartAllFileWatchers()
        {
            _consoleView.EchoLine(@Resources.MessageStartingDaemons);
            FileWatcherController.StartAllFileWatchers();
        }

        /// <summary>
        /// Stops file watchers.
        /// </summary>
        private void StopAllFileWatchers()
        {
            // Avoid deadlock if all file watchers have invalid path by checking IsActive.
            while (!FileWatcherController.CanStopAllFileWatchers() &&
                   FileWatcherController.IsActive())
            {
                // Wait for starting file watchers.
                _canStopAll.WaitOne();
            }
            if (FileWatcherController.CanStopAllFileWatchers())
            {
                FileWatcherController.StopAllFileWatchers();
            }
        }

        /// <summary>
        /// Waits for controller to stop.
        /// </summary>
        private void WaitForControllerStopped()
        {
            if (FileWatcherController.RunQueuedProcesses)
            {
                _consoleView.EchoLine(@Resources.MessageWaitingForQueuedProcesses);
            }
            else
            {
                _consoleView.EchoLine(@Resources.MessageWaitingForRunningProcessesToExit);
            }
            // Wait for file watcher controller stopped event.
            _canExit.WaitOne();
        }

        /// <summary>
        /// Writes example daemon files to current directory.
        /// </summary>
        private void WriteExampleDaemonConfiguration()
        {
            FileWatcherExampleDaemonWriter fileWatcherExampleDaemonWriter =
                new FileWatcherExampleDaemonWriter(_xmlConfigFilePath, 
                                                   _xmlSchemaConfigFilePath);

            _consoleView.EchoLine(@Resources.MessageWritingExampleDaemonXMLConfigurationFiles);
            fileWatcherExampleDaemonWriter.CreateExampleDaemonConfigFiles();
            _consoleView.EchoLine(@Resources.MessageConfigurationFilesWritten);
            _consoleView.EchoLine(String.Empty);
            _consoleView.EchoLine(@Resources.MessagePressEnterToExit);
        }

        /// <summary>
        /// Prints GNU license to console view.
        /// </summary>
        private void PrintLicense()
        {
            GnuLicense gnuLicence = new GnuLicense();
            while (gnuLicence.IsLastPart())
            {
                _consoleView.EchoLine(gnuLicence.NextGnuLicensePart());
                _consoleView.EchoLine(String.Empty);
                _consoleView.EchoLine(@Resources.MessagePressAnyKeyToContinue);
                _consoleView.ReadKey();
            }
            Console.WriteLine(gnuLicence.NextGnuLicensePart());
            _consoleView.EchoLine(String.Empty);
            _consoleView.EchoLine(@Resources.MessagePressEnterToExit);
        }

        /// <summary>
        /// Prints application usage to console view.
        /// </summary>
        private void PrintUsage()
        {
            _consoleView.EchoLine(@Resources.MessageUsage);
            _consoleView.EchoLine(String.Empty);
            _consoleView.EchoLine(@Resources.MessagePressEnterToExit);
        }

        /// <summary>
        /// Prints application info to console view.
        /// </summary>
        private void PrintApplicationInfo()
        {
            _consoleView.EchoLine(String.Format(CultureInfo.CurrentCulture,
                                               @Resources.MessageApplicationInfo,
                                               Application.ProductVersion));
            _consoleView.EchoLine(String.Empty);
        }

        /// <summary>
        /// Auto reset event for can stop all.
        /// </summary>
        private readonly AutoResetEvent _canStopAll = new AutoResetEvent(false);

        /// <summary>
        /// Auto reset event for can exit.
        /// </summary>
        private readonly AutoResetEvent _canExit = new AutoResetEvent(false);

        /// <summary>
        /// Console view.
        /// </summary>
        private readonly IConsoleView _consoleView;

        /// <summary>
        /// Contains path of the XML configuration file.
        /// </summary>
        private readonly string _xmlConfigFilePath;

        /// <summary>
        /// Contains path of the XML Schema configuration file.
        /// </summary>
        private readonly string _xmlSchemaConfigFilePath;
    }
}