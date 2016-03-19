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
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;
using System.Collections.Generic;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides main view presenter.
    /// </summary>
    public class PropertiesViewPresenter
    {
        /// <summary>
        /// Initializes a new instance of the PropertiesViewPresenter class.
        /// </summary>
        /// <param name="propertiesView">Properties view.</param>
        /// <param name="mainView">Main view.</param>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <exception cref="ArgumentNullException">propertiesView is null.</exception>
        /// <exception cref="ArgumentNullException">mainView is null.</exception>
        /// <exception cref="ArgumentNullException">fileWatcherController is null.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        public PropertiesViewPresenter(IPropertiesView propertiesView,
                                       IMainView mainView,
                                       FileWatcherController fileWatcherController,
                                       string xmlConfigFilePath,
                                       string xmlSchemaConfigFilePath)
        {
            if (propertiesView == null)
            {
                throw new ArgumentNullException("propertiesView",
                                                Resources.ArgumentNullException);
            }
            if (mainView == null)
            {
                throw new ArgumentNullException("mainView",
                                                Resources.ArgumentNullException);
            }
            if (fileWatcherController == null)
            {
                throw new ArgumentNullException("fileWatcherController",
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
            _propertiesView = propertiesView;
            _mainView = mainView;
            _fileWatcherController = fileWatcherController;
            _xmlConfigFilePath = xmlConfigFilePath;
            _xmlSchemaConfigFilePath = xmlSchemaConfigFilePath;

            SubscribeToMainViewEvents();
            SubscribeToPropertiesViewEvents();
        }

        /// <summary>
        /// Gets or sets selected daemon.
        /// </summary>
        private string SelectedDaemon
        {
            get
            {
                return _selectedDaemon;
            }
            set
            {
                _selectedDaemon = value;
            }
        }

        /// <summary>
        /// Gets or sets orginal daemon name.
        /// </summary>
        private string OriginalDaemonName
        {
            get
            {
                return _originalDaemonName;
            }
            set
            {
                _originalDaemonName = value;
            }
        }

        /// <summary>
        /// Subscribes to main view events.
        /// </summary>
        private void SubscribeToMainViewEvents()
        {
            _mainView.DaemonSelected +=
                new EventHandler<DaemonSelectedEventArgs>(OnDaemonSelected);

            _mainView.ViewNew +=
                new EventHandler<EventArgs>(OnViewNew);

            _mainView.ViewProperties +=
                new EventHandler<EventArgs>(OnViewProperties);
        }

        /// <summary>
        /// Handles daemon selected event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnDaemonSelected(object sender,
                                      DaemonSelectedEventArgs e)
        {
            SelectedDaemon = e.DaemonName;
        }

        /// <summary>
        /// Handles view new event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnViewNew(object sender,
                               EventArgs e)
        {
            // No daemon.
            OriginalDaemonName = null;

            // Set blank configuration.
            _propertiesView.ViewFileWatcherConfiguration(
                new KeyValuePair<string, FileWatcherConfigurationSet>(String.Empty,
                                                                      new FileWatcherConfigurationSet()));
            // Show view.
            _propertiesView.ShowView();
        }

        /// <summary>
        /// Subscribes to properties view events.
        /// </summary>
        private void SubscribeToPropertiesViewEvents()
        {
            _propertiesView.Save +=
                new EventHandler<EventArgs>(OnSave);
        }

        /// <summary>
        /// Handles on save event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnSave(object sender,
                            EventArgs e)
        {
            bool success = false;

            // If not a new one.
            if (OriginalDaemonName != null)
            {
                try
                {
                    // Update configuration.
                    _fileWatcherController.UpdateFileWatcher(OriginalDaemonName,
                                                            _propertiesView.NewConfigurationKeyValuePair());
                    success = true;
                }
                catch (ArgumentException ex)
                {
                    // Show error in view.
                    _propertiesView.ShowError(ex.Message);
                }
            }
            else
            {
                try
                {
                    // Try to add new configuration.
                    _fileWatcherController.AddFileWatcher(_propertiesView.NewConfigurationKeyValuePair());
                    success = true;
                }
                catch (ArgumentException ex)
                {
                    // Show error in view.
                    _propertiesView.ShowError(ex.Message);
                }
            }

            // If operation was success.
            if (success)
            {
                // Save configuration.
                XmlConfigurationSaver.SaveConfigurationSets(_fileWatcherController.FileWatcherConfiguration,
                                                            _xmlConfigFilePath,
                                                            _xmlSchemaConfigFilePath);
                // Hide view.
                _propertiesView.HideView();
            }
        }

        /// <summary>
        /// Handles view properties event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnViewProperties(object sender,
                                      EventArgs e)
        {
            // Store viewed daemon name.
            OriginalDaemonName = SelectedDaemon;

            // Load configuration to view.
            Dictionary<string, FileWatcherConfigurationSet> configurationDictionary =
                _fileWatcherController.FileWatcherConfiguration;

            KeyValuePair<string, FileWatcherConfigurationSet> configurationKeyValuePair =
                new KeyValuePair<string, FileWatcherConfigurationSet>(
                    OriginalDaemonName, configurationDictionary[OriginalDaemonName]);

            _propertiesView.ViewFileWatcherConfiguration(configurationKeyValuePair);

            // Show view.
            _propertiesView.ShowView();
        }

        /// <summary>
        /// Selected daemon name
        /// </summary>
        private string _selectedDaemon;

        /// <summary>
        /// Original daemon name.
        /// </summary>
        private string _originalDaemonName;

        /// <summary>
        /// Properties view.
        /// </summary>
        private readonly IPropertiesView _propertiesView;

        /// <summary>
        /// Main view.
        /// </summary>
        private readonly IMainView _mainView;

        /// <summary>
        /// File watcher controller.
        /// </summary>
        private readonly FileWatcherController _fileWatcherController;

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