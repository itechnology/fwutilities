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
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
using FileWatcherUtilities.Options.Properties;

namespace FileWatcherUtilities.Options
{
    public static class XmlOptionsLoader
    {
        /// <summary>
        /// Loads application options from XML file.
        /// </summary>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <returns>Application options.</returns>
        /// <exception cref="FileNotFoundException">XML file not found.</exception>
        /// <exception cref="FileNotFoundException">XML Schema file not found.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        /// <exception cref="InvalidDataException">Invalid data.</exception>
        /// <exception cref="XmlException">Error occured when parsing the XML.</exception>
        /// <remarks>Throws all exceptions defined in ApplicationOptions.</remarks>
        public static ApplicationOptions LoadOptions(string xmlConfigFilePath,
                                                     string xmlSchemaConfigFilePath)
        {
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

            if (!File.Exists(xmlConfigFilePath))
            {
                throw new FileNotFoundException(Resources.FileNotFoundException,
                                                xmlConfigFilePath);
            }

            if (!File.Exists(xmlSchemaConfigFilePath))
            {
                throw new FileNotFoundException(Resources.FileNotFoundException,
                                                xmlSchemaConfigFilePath);
            }

            ValidateConfigSets(xmlConfigFilePath,
                               xmlSchemaConfigFilePath);

            if (HasValidationError)
            {
                throw new InvalidDataException(GetValidationError);
            }
            return LoadOptionsToApplicationOptions(xmlConfigFilePath,
                                                   xmlSchemaConfigFilePath);
        }

        /// <summary>
        /// Returns the last validation error of the file watcher XML configuration file.
        /// </summary>
        private static string GetValidationError
        {
            get
            {
                return _validationError;
            }
        }

        /// <summary>
        /// Returns true if file watcher XML configuration file contains validation errors.
        /// </summary>
        private static bool HasValidationError
        {
            get
            {
                return _hasValidationError;
            }
        }

        /// <summary>
        /// Loads configuration from XML files.
        /// </summary>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <returns>Application options.</returns>
        /// <remarks>Throws all exceptions defined in ApplicationOptions.</remarks>
        private static ApplicationOptions LoadOptionsToApplicationOptions(string xmlConfigFilePath,
                                                                          string xmlSchemaConfigFilePath)
        {
            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.InvariantCulture;

                dataSet.ReadXmlSchema(xmlSchemaConfigFilePath);
                dataSet.ReadXml(xmlConfigFilePath);

                ApplicationOptions applicationOptions = new ApplicationOptions();

                // Load application options.
                foreach (DataRow dataRow in dataSet.Tables[Resources.TableName].Rows)
                {
                    applicationOptions.AutoStartup =
                        (bool)dataRow[Resources.ColumnAutoStartup];

                    applicationOptions.LogMessages =
                        (int)dataRow[Resources.ColumnLogMessages];

                    applicationOptions.ProcessBatchSize =
                        (int)dataRow[Resources.ColumnProcessBatchSize];

                    applicationOptions.RunQueuedProcesses =
                        (bool)dataRow[Resources.ColumnRunQueuedProcesses];

                    applicationOptions.SynchronousExecution =
                        (bool)dataRow[Resources.ColumnSynchronousExecution];
                }

                return applicationOptions;
            }
        }

        /// <summary>
        /// Validates application options file against XML Schema.
        /// </summary>
        /// <exception cref="XmlException">Error occured when parsing the XML.</exception>
        private static void ValidateConfigSets(string xmlConfigFilePath,
                                               string xmlSchemaConfigFilePath)
        {
            // Reset validation information.
            _validationError = String.Empty;
            _hasValidationError = false;

            XmlValidator xmlValidator = new XmlValidator(xmlConfigFilePath,
                                                         xmlSchemaConfigFilePath);
            xmlValidator.Validation +=
                new EventHandler<ValidationEventArgs>(OnValidationEvent);
            xmlValidator.Validate();
        }

        /// <summary>
        /// Handles validation event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private static void OnValidationEvent(object sender,
                                              ValidationEventArgs e)
        {
            _validationError = e.Message;
            _hasValidationError = true;
        }

        /// <summary>
        /// Contains last validation error message.
        /// </summary>
        private static string _validationError = String.Empty;

        /// <summary>
        /// True if XML file contains validation errors.
        /// </summary>
        private static bool _hasValidationError;
    }
}