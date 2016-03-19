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
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Options.Properties;

namespace FileWatcherUtilities.Options
{
    /// <summary>
    /// Provides controller for application options.
    /// </summary>
    public class ApplicationOptionsController
    {
        /// <summary>
        /// Occures when application options has changed.
        /// </summary>
        public event EventHandler<ApplicationOptionsChangedEventArgs> ConfigurationChanged;

        /// <summary>
        /// Initializes a new instance of the ApplicationOptionsController class. 
        /// </summary>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="xmlApplicationOptionsFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaApplicationOptionsFilePath">Path of the configuration XML Schema file.</param>
        /// <param name="applicationOptions">Application options.</param>
        /// <exception cref="ArgumentNullException">fileWatcherController is null.</exception>
        /// <exception cref="ArgumentNullException">xmlApplicationOptionsFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaApplicationOptionsFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">applicationOptions is null.</exception>
        public ApplicationOptionsController(FileWatcherController fileWatcherController,
                                            string xmlApplicationOptionsFilePath,
                                            string xmlSchemaApplicationOptionsFilePath,
                                            ApplicationOptions applicationOptions)
        {
            if (fileWatcherController == null)
            {
                throw new ArgumentNullException("fileWatcherController",
                                                Resources.ArgumentNullException);
            }
            if (xmlApplicationOptionsFilePath == null)
            {
                throw new ArgumentNullException("xmlApplicationOptionsFilePath",
                                                Resources.ArgumentNullException);
            }
            if (xmlSchemaApplicationOptionsFilePath == null)
            {
                throw new ArgumentNullException("xmlSchemaApplicationOptionsFilePath",
                                                Resources.ArgumentNullException);
            }
            if (applicationOptions == null)
            {
                throw new ArgumentNullException("applicationOptions",
                                                Resources.ArgumentNullException);
            }

            _fileWatcherController = fileWatcherController;
            _xmlApplicationOptionsFilePath = xmlApplicationOptionsFilePath;
            _xmlSchemaApplicationOptionsFilePath = xmlSchemaApplicationOptionsFilePath;
            _internalApplicationOptions = applicationOptions;
        }

        /// <summary>
        /// Gets application options.
        /// </summary>
        public ApplicationOptions ApplicationOptions
        {
            get
            {
                return _internalApplicationOptions;
            }
        }

        /// <summary>
        /// Saves application options.
        /// </summary>
        /// <param name="applicationOptions">Application options.</param>
        /// <exception cref="ArgumentNullException">applicationOptions is null.</exception>
        public void Save(ApplicationOptions applicationOptions)
        {
            if (applicationOptions == null)
            {
                throw new ArgumentNullException("applicationOptions",
                                                Resources.ArgumentNullException);
            }

            // Set options to file watcher controller.
            if (!_fileWatcherController.IsActive())
            {
                _fileWatcherController.SynchronousExecution = applicationOptions.SynchronousExecution;
            }

            _fileWatcherController.RunQueuedProcesses = applicationOptions.RunQueuedProcesses;
            _fileWatcherController.ProcessBatchSize = applicationOptions.ProcessBatchSize;

            XmlOptionsSaver.SaveApplicationOptions(applicationOptions,
                                                   _xmlApplicationOptionsFilePath,
                                                   _xmlSchemaApplicationOptionsFilePath);


            // Store new application options.
            _internalApplicationOptions = applicationOptions;

            // Raise configuration changed event.
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged(this, 
                    new ApplicationOptionsChangedEventArgs(applicationOptions));
            }
        }

        /// <summary>
        /// File watcher controller.
        /// </summary>
        private readonly FileWatcherController _fileWatcherController;

        /// <summary>
        /// Application options.
        /// </summary>
        private ApplicationOptions _internalApplicationOptions;

        /// <summary>
        /// Contains path of the XML application options file.
        /// </summary>
        private readonly string _xmlApplicationOptionsFilePath;

        /// <summary>
        /// Contains path of the XML application options file.
        /// </summary>
        private readonly string _xmlSchemaApplicationOptionsFilePath;
    }
}