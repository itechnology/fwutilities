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
using System.IO;
using System.Xml;
using System.Security;
using System.Globalization;
using System.Collections.Generic;
using FileWatcherUtilities.Common;
using FileWatcherUtilities.Logger;
using FileWatcherUtilities.Options;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides presenter builder.
    /// </summary>
    public class PresenterBuilder : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the PresenterBuilder class.
        /// </summary>
        /// <param name="mainView">IMainView.</param>
        /// <param name="logView">ILogView.</param>
        /// <param name="optionsView">IOptionsView.</param>
        /// <param name="propertiesView">IPropertiesView.</param>
        /// <exception cref="ArgumentNullException">mainView is null.</exception>
        /// <exception cref="ArgumentNullException">logView is null.</exception>
        /// <exception cref="ArgumentNullException">optionsView is null.</exception>
        /// <exception cref="ArgumentNullException">propertiesView is null.</exception>
        public PresenterBuilder(IMainView mainView,
                                ILogView logView,
                                IOptionsView optionsView,
                                IPropertiesView propertiesView)
        {
            if (mainView == null)
            {
                throw new ArgumentNullException("mainView",
                                                Resources.ArgumentNullException);
            }
            if (logView == null)
            {
                throw new ArgumentNullException("logView",
                                                Resources.ArgumentNullException);
            }
            if (optionsView == null)
            {
                throw new ArgumentNullException("optionsView",
                                                Resources.ArgumentNullException);
            }
            if (propertiesView == null)
            {
                throw new ArgumentNullException("propertiesView",
                                                Resources.ArgumentNullException);
            }

            _viewType = ViewType.Forms;

            _mainView = mainView;
            _logView = logView;
            _optionsView = optionsView;
            _propertiesView = propertiesView;
        }

        /// <summary>
        /// Initializes a new instance of the PresenterBuilder class.
        /// </summary>
        /// <param name="consoleView">IConsoleView.</param>
        /// <exception cref="ArgumentNullException">consoleView is null.</exception>
        public PresenterBuilder(IConsoleView consoleView)
        {
            if (consoleView == null)
            {
                throw new ArgumentNullException("consoleView",
                                                Resources.ArgumentNullException);
            }

            _viewType = ViewType.Console;

            _consoleView = consoleView;
        }

        /// <summary>
        /// Initializes a new instance of the PresenterBuilder class.
        /// </summary>
        /// <param name="windowsServiceView">IWindowsServiceView.</param>
        /// <exception cref="ArgumentNullException">windowsServiceView is null.</exception>
        public PresenterBuilder(IWindowsServiceView windowsServiceView)
        {
            if (windowsServiceView == null)
            {
                throw new ArgumentNullException("windowsServiceView",
                                                Resources.ArgumentNullException);
            }

            _viewType = ViewType.WindowsService;

            _windowsServiceView = windowsServiceView;
        }

        /// <summary>
        /// Builds presenters and applies configuration.
        /// </summary>
        /// <exception cref="InvalidDataException">File watcher configuration error.</exception>
        /// <exception cref="XmlException">File watcher configuration error.</exception>
        /// <exception cref="UnauthorizedAccessException">Cannot open log file.</exception>
        /// <exception cref="DirectoryNotFoundException">Cannot open log file.</exception>
        /// <exception cref="PathTooLongException">Cannot open log file.</exception>
        /// <exception cref="IOException">Cannot open log file.</exception>
        /// <exception cref="ArgumentException">Cannot open log file.</exception>
        /// <exception cref="SecurityException">Cannot open log file.</exception>
        public void Build()
        {
            CheckDisposed();

            // Build presenter by view type.
            if (_viewType == ViewType.Console)
            {
                BuildConsolePresenters();
            }
            else if (_viewType == ViewType.Forms)
            {
                BuildFormsPresenters();
            }
            else if (_viewType == ViewType.WindowsService)
            {
                BuildWindowsServicePresenter();
            }
        }

        #region IDisposable

        /// <summary>
        /// Implements IDisposable.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">True if disposing.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    if (_logViewPresenter != null)
                    {
                        _logViewPresenter.Dispose();
                        _logViewPresenter = null;
                    }
                    if (_mainViewPresenter != null)
                    {
                        _mainViewPresenter.Dispose();
                        _mainViewPresenter = null;
                    }
                    if (_consoleViewPresenter != null)
                    {
                        _consoleViewPresenter.Dispose();
                        _consoleViewPresenter = null;
                    }
                    if (_windowsServiceViewPresenter != null)
                    {
                        _windowsServiceViewPresenter.Dispose();
                        _windowsServiceViewPresenter = null;
                    }
                    if (_fileWatcherController != null)
                    {
                        _fileWatcherController.Dispose();
                        _fileWatcherController = null;
                    }
                    if (_logger != null)
                    {
                        _logger.Dispose();
                        _logger = null;
                    }
                    _disposed = true;
                }
            }
        }

        #endregion

        /// <summary>
        /// Builds forms presenters.
        /// </summary>
        private void BuildFormsPresenters()
        {
            // NOTE: We need the main thread to call these for synchronization.          
            SetLogger();
            SetFileWatcherContoller();
            SetApplicationOptionsController();
            ApplyApplicationOptions();

            BuildOptionsViewPresenter();
            BuildPropertiesViewPresenter();
            BuildMainViewPresenter();
            BuildLogViewPresenter();

            // If auto startup is on start all here.
            if (_applicationOptionsController.ApplicationOptions.AutoStartup)
            {
                if (_fileWatcherController.CanStartAllFileWatchers())
                {
                    _fileWatcherController.StartAllFileWatchers();
                }
            }
        }

        /// <summary>
        /// Sets logger.
        /// </summary>
        private void SetLogger()
        {
            // Exits if error occures.
            _logger = SetLogger(ConfigurationWrapper.LogFilePath);
        }

        /// <summary>
        /// Applies application options to file watcher controller.
        /// </summary>
        private void ApplyApplicationOptions()
        {
            // Set options to file watcher controller.
            _fileWatcherController.SynchronousExecution =
                _applicationOptionsController.ApplicationOptions.SynchronousExecution;
            _fileWatcherController.RunQueuedProcesses =
                _applicationOptionsController.ApplicationOptions.RunQueuedProcesses;
            _fileWatcherController.ProcessBatchSize =
                _applicationOptionsController.ApplicationOptions.ProcessBatchSize;
        }

        /// <summary>
        /// Loads application options and sets controller.
        /// </summary>
        private void SetApplicationOptionsController()
        {
            // Exits if error occures.
            ApplicationOptions applicationOptions =
                LoadApplicationOptions(ConfigurationWrapper.XmlApplicationOptionsFilePath,
                                       ConfigurationWrapper.XmlSchemaApplicationOptionsFilePath);

            _applicationOptionsController =
                new ApplicationOptionsController(_fileWatcherController,
                                                 ConfigurationWrapper.XmlApplicationOptionsFilePath,
                                                 ConfigurationWrapper.XmlSchemaApplicationOptionsFilePath,
                                                 applicationOptions);
        }

        /// <summary>
        /// Loads file watcher configuration and sets controller.
        /// </summary>
        private void SetFileWatcherContoller()
        {
            // Exits if error occures.
            Dictionary<string, FileWatcherConfigurationSet> configurationDictionary =
                LoadConfiguration(ConfigurationWrapper.XmlConfigurationFilePath,
                                  ConfigurationWrapper.XmlSchemaConfigurationFilePath);

            _fileWatcherController = new FileWatcherController(configurationDictionary);

            // Set queue trim interval.
            _fileWatcherController.SetQueueTrimInterval(ConfigurationWrapper.QueueTrimInterval);
        }

        /// <summary>
        /// Builds options view presenter.
        /// </summary>
        private void BuildOptionsViewPresenter()
        {
            new OptionsViewPresenter(_optionsView,
                                     _mainView,
                                     _fileWatcherController,
                                     _applicationOptionsController);
        }

        /// <summary>
        /// Builds properties view presenter.
        /// </summary>
        private void BuildPropertiesViewPresenter()
        {
            new PropertiesViewPresenter(_propertiesView,
                                        _mainView,
                                        _fileWatcherController,
                                        ConfigurationWrapper.XmlConfigurationFilePath,
                                        ConfigurationWrapper.XmlSchemaConfigurationFilePath);
        }

        /// <summary>
        /// Builds main view presenter.
        /// </summary>
        private void BuildMainViewPresenter()
        {
            _mainViewPresenter = new MainViewPresenter(_mainView,
                                                      _fileWatcherController,
                                                      _logger,
                                                      new DefaultFormatter(),
                                                      ConfigurationWrapper.XmlConfigurationFilePath,
                                                      ConfigurationWrapper.XmlSchemaConfigurationFilePath,
                                                      ConfigurationWrapper.UpdateInterval);
        }

        /// <summary>
        /// Builds log view presenter.
        /// </summary>
        private void BuildLogViewPresenter()
        {
            _logViewPresenter = new LogViewPresenter(_logView,
                                                    _mainView,
                                                    _applicationOptionsController,
                                                    _fileWatcherController,
                                                    new DefaultFormatter(),
                                                    ConfigurationWrapper.LogUpdateInterval,
                                                    _applicationOptionsController.ApplicationOptions.LogMessages);
        }

        /// <summary>
        /// Builds console view presenter.
        /// </summary>
        private void BuildConsoleViewPresenter()
        {
            _consoleViewPresenter = new ConsoleViewPresenter(_consoleView,
                                                            _fileWatcherController,
                                                            _logger,
                                                            new DefaultFormatter(),
                                                            ConfigurationWrapper.XmlConfigurationFilePath,
                                                            ConfigurationWrapper.XmlSchemaConfigurationFilePath);
        }

        /// <summary>
        /// Builds windows service view presenter.
        /// </summary>
        private void BuildWindowsServiceViewPresenter()
        {
            _windowsServiceViewPresenter = new WindowsServiceViewPresenter(_windowsServiceView,
                                                                          _fileWatcherController,
                                                                          _logger,
                                                                          new DefaultFormatter());
        }

        /// <summary>
        /// Builds console presenters.
        /// </summary>
        private void BuildConsolePresenters()
        {
            SetLogger();
            SetFileWatcherContoller();
            BuildConsoleViewPresenter();
        }

        /// <summary>
        /// Builds windows service presenter.
        /// </summary>
        private void BuildWindowsServicePresenter()
        {
            SetLogger();
            SetFileWatcherContoller();
            BuildWindowsServiceViewPresenter();
        }

        /// <summary>
        /// Checks configuration file paths.
        /// </summary>
        /// <param name="xmlFilePath">Path of the XML file.</param>
        /// <param name="xmlSchemaFilePath">Path of the XML Schema file.</param>
        /// <returns>True if files exists.</returns>
        private static bool CheckFiles(string xmlFilePath,
                                       string xmlSchemaFilePath)
        {
            return (File.Exists(xmlFilePath) && File.Exists(xmlSchemaFilePath));
        }

        /// <summary>
        /// Sets logger or exits application.
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <returns>Return logger.</returns>
        private static ILogger SetLogger(string logFilePath)
        {
            DefaultLogger tempDefaultLogger = null;

            try
            {
                tempDefaultLogger = new DefaultLogger(logFilePath);
                tempDefaultLogger.Open(true);
                DefaultLogger defaultLogger = tempDefaultLogger;
                tempDefaultLogger = null;
                return defaultLogger;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(@Resources.MessageCannotOpenLogFile, ex);
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new DirectoryNotFoundException(@Resources.MessageCannotOpenLogFile, ex);
            }
            catch (PathTooLongException ex)
            {
                throw new PathTooLongException(@Resources.MessageCannotOpenLogFile, ex);
            }
            catch (IOException ex)
            {
                throw new IOException(@Resources.MessageCannotOpenLogFile, ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(@Resources.MessageCannotOpenLogFile, ex);
            }
            catch (SecurityException ex)
            {
                throw new SecurityException(@Resources.MessageCannotOpenLogFile, ex);
            }
            finally
            {
                if (tempDefaultLogger != null)
                {
                    tempDefaultLogger.Dispose();
                }
            }
        }

        /// <summary>
        /// Loads application options from files or returns default values.
        /// </summary>
        /// <param name="xmlApplicationOptionsFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaApplicationOptionsFilePath">Path of the configuration XML Schema file.</param>
        /// <returns>Application options.</returns>
        /// <exception cref="InvalidDataException">Invalid data.</exception>
        /// <exception cref="XmlException">Error occured when parsing the XML.</exception>
        private static ApplicationOptions LoadApplicationOptions(string xmlApplicationOptionsFilePath,
                                                                 string xmlSchemaApplicationOptionsFilePath)
        {
            ApplicationOptions tempApplicationOptions = new ApplicationOptions();

            try
            {
                // Check files.
                if (CheckFiles(xmlApplicationOptionsFilePath,
                               xmlSchemaApplicationOptionsFilePath))
                {
                    // Load application options.
                    tempApplicationOptions =
                        XmlOptionsLoader.LoadOptions(xmlApplicationOptionsFilePath,
                                                     xmlSchemaApplicationOptionsFilePath);
                }
            }
            catch (InvalidDataException ex)
            {
                throw new InvalidDataException(String.Format(CultureInfo.CurrentCulture,
                                               @Resources.MessageFileWatcherConfigurationError,
                                               @ex.Message), ex);
            }
            catch (XmlException ex)
            {
                throw new XmlException(String.Format(CultureInfo.CurrentCulture,
                                       @Resources.MessageFileWatcherConfigurationError,
                                       @ex.Message), ex);
            }
            return tempApplicationOptions;
        }

        /// <summary>
        /// Loads configuration dictionary from files or returns empty configuration.
        /// </summary>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <returns>Configuration dictionary.</returns>
        /// <exception cref="InvalidDataException">Invalid data.</exception>
        /// <exception cref="XmlException">Error occured when parsing the XML.</exception>
        private static Dictionary<string, FileWatcherConfigurationSet> LoadConfiguration(string xmlConfigFilePath,
                                                                                         string xmlSchemaConfigFilePath)
        {
            Dictionary<string, FileWatcherConfigurationSet> tempConfigurationDictionary =
                new Dictionary<string, FileWatcherConfigurationSet>();

            try
            {
                // Check files.
                if (CheckFiles(xmlConfigFilePath,
                               xmlSchemaConfigFilePath))
                {
                    // Load configuration.
                    tempConfigurationDictionary =
                        XmlConfigurationLoader.LoadConfiguration(xmlConfigFilePath,
                                                                 xmlSchemaConfigFilePath);
                }
            }
            catch (InvalidDataException ex)
            {
                throw new InvalidDataException(String.Format(CultureInfo.CurrentCulture,
                                               @Resources.MessageFileWatcherConfigurationError,
                                               @ex.Message), ex);
            }
            catch (XmlException ex)
            {
                throw new XmlException(String.Format(CultureInfo.CurrentCulture,
                                       @Resources.MessageFileWatcherConfigurationError,
                                       @ex.Message), ex);
            }
            return tempConfigurationDictionary;
        }

        /// <summary>
        /// Check if this instance is disposed.
        /// </summary>
        private void CheckDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(Resources.ObjectNamePresenterBuilder);
            }
        }

        /// <summary>
        /// ViewType enumeration.
        /// </summary>
        private enum ViewType
        {
            /// <summary>
            /// Forms view.
            /// </summary>
            Forms,
            /// <summary>
            /// Console view.
            /// </summary>
            Console,
            /// <summary>
            /// Windows service view.
            /// </summary>
            WindowsService
        }

        /// <summary>
        /// True if disposed.
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Logger.
        /// </summary>
        private ILogger _logger;

        /// <summary>
        /// Presenter view type.
        /// </summary>
        private readonly ViewType _viewType;

        /// <summary>
        /// Main view.
        /// </summary>
        private readonly IMainView _mainView;

        /// <summary>
        /// Log view.
        /// </summary>
        private readonly ILogView _logView;

        /// <summary>
        /// Options view.
        /// </summary>
        private readonly IOptionsView _optionsView;

        /// <summary>
        /// Properties view.
        /// </summary>
        private readonly IPropertiesView _propertiesView;

        /// <summary>
        /// Console view.
        /// </summary>
        private readonly IConsoleView _consoleView;

        /// <summary>
        /// Windows service view.
        /// </summary>
        private readonly IWindowsServiceView _windowsServiceView;

        /// <summary>
        /// Application options controller.
        /// </summary>
        private ApplicationOptionsController _applicationOptionsController;

        /// <summary>
        /// File watcher controller.
        /// </summary>
        private FileWatcherController _fileWatcherController;

        /// <summary>
        /// Log view presenter.
        /// </summary>
        private LogViewPresenter _logViewPresenter;

        /// <summary>
        /// Main view presenter.
        /// </summary>
        private MainViewPresenter _mainViewPresenter;

        /// <summary>
        /// Console view presenter.
        /// </summary>
        private ConsoleViewPresenter _consoleViewPresenter;

        /// <summary>
        /// Windows service view presenter.
        /// </summary>
        private WindowsServiceViewPresenter _windowsServiceViewPresenter;
    }
}