/******************************************************************************
*    File Watcher Utilities / File Watcher Controller
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
using System.Collections.Generic;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Loads configuration from XML files.
    /// </summary>
    public static class XmlConfigurationLoader
    {
        /// <summary>
        /// Loads file watcher configuration from XML file.
        /// </summary>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <returns>Configuration dictionary.</returns>
        /// <exception cref="FileNotFoundException">XML file not found.</exception>
        /// <exception cref="FileNotFoundException">XML Schema file not found.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">Daemon name is null.</exception>
        /// <exception cref="ArgumentException">Duplicate daemon name.</exception>
        /// <exception cref="InvalidDataException">Invalid data.</exception>
        /// <exception cref="XmlException">Error occured when parsing the XML.</exception>
        /// <remarks>Throws all exceptions defined in FileWatcherConfigurationSet.</remarks>
        public static Dictionary<string, FileWatcherConfigurationSet> LoadConfiguration(string xmlConfigFilePath,
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
            return LoadConfigurationToDictionary(xmlConfigFilePath,
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
        /// <returns>Configuration dictionary.</returns>
        /// <exception cref="ArgumentNullException">Dictionary key (daemonName) is null.</exception>
        /// <exception cref="ArgumentException">Duplicate key (daemonName) in configuration.</exception>
        /// <remarks>Throws all exceptions defined in FileWatcherConfigurationSet.</remarks>
        private static Dictionary<string, FileWatcherConfigurationSet> LoadConfigurationToDictionary(string xmlConfigFilePath,
                                                                                                     string xmlSchemaConfigFilePath)
        {
            Dictionary<string, FileWatcherConfigurationSet> configurationDictionary =
                    new Dictionary<string, FileWatcherConfigurationSet>();

            using (DataSet dataSet = new DataSet())
            {
                dataSet.Locale = CultureInfo.InvariantCulture;

                dataSet.ReadXmlSchema(xmlSchemaConfigFilePath);
                dataSet.ReadXml(xmlConfigFilePath);

                // Load configuration sets to dictionary.
                foreach (DataRow dataRow in dataSet.Tables[Resources.TableName].Rows)
                {
                    FileWatcherConfigurationSet fileWatcherConfigSet =
                        new FileWatcherConfigurationSet();

                    fileWatcherConfigSet.StartDaemon =
                        (bool)dataRow[Resources.ColumnStartDaemon];

                    fileWatcherConfigSet.DisplayFileSystemChange =
                        (bool)dataRow[Resources.ColumnDisplayFileSystemChange];

                    fileWatcherConfigSet.LogFileSystemChange =
                        (bool)dataRow[Resources.ColumnLogFileSystemChange];

                    fileWatcherConfigSet.LogFileSystemSearchError =
                        (bool)dataRow[Resources.ColumnLogFileSystemSearchError];

                    fileWatcherConfigSet.DisplayFileSystemSearchError =
                        (bool)dataRow[Resources.ColumnDisplayFileSystemSearchError];

                    fileWatcherConfigSet.LogFileSystemSearchProgress =
                        (bool)dataRow[Resources.ColumnLogFileSystemSearchProgress];

                    fileWatcherConfigSet.DisplayFileSystemSearchProgress =
                        (bool)dataRow[Resources.ColumnDisplayFileSystemSearchProgess];

                    fileWatcherConfigSet.Path =
                        (string)dataRow[Resources.ColumnPath];

                    fileWatcherConfigSet.Filter =
                        (string)dataRow[Resources.ColumnFilter];

                    fileWatcherConfigSet.GenerateFileSystemEventsAtStartup =
                        (bool)dataRow[Resources.ColumnGenerateFileSystemEventsAtStartup];

                    fileWatcherConfigSet.GeneratedEventFileNameRegularExpressionFilter =
                        (string)dataRow[Resources.ColumnGenereatedEventFileNameRegularExpressionFilter];

                    fileWatcherConfigSet.ChangedRegularExpressionFilter =
                        (string)dataRow[Resources.ColumnChangedRegularExpressionFilter];

                    fileWatcherConfigSet.CreatedRegularExpressionFilter =
                        (string)dataRow[Resources.ColumnCreatedRegularExpressionFilter];

                    fileWatcherConfigSet.DeletedRegularExpressionFilter =
                        (string)dataRow[Resources.ColumnDeletedRegularExpressionFilter];

                    fileWatcherConfigSet.RenamedRegularExpressionFilter =
                        (string)dataRow[Resources.ColumnRenamedRegularExpressionFilter];

                    fileWatcherConfigSet.IncludeSubdirectories =
                        (bool)dataRow[Resources.ColumnIncludeSubdirectories];

                    fileWatcherConfigSet.InternalBufferSize =
                        (int)dataRow[Resources.ColumnInternalBufferSize];

                    fileWatcherConfigSet.SubscribeToBufferErrorEvent =
                        (bool)dataRow[Resources.ColumnSubscribeToBufferErrorEvent];

                    fileWatcherConfigSet.SubscribeToChangedEvent =
                        (bool)dataRow[Resources.ColumnSubscribeToChangedEvent];

                    fileWatcherConfigSet.SubscribeToCreatedEvent =
                        (bool)dataRow[Resources.ColumnSubscribeToCreatedEvent];

                    fileWatcherConfigSet.SubscribeToDeletedEvent =
                        (bool)dataRow[Resources.ColumnSubscribeToDeletedEvent];

                    fileWatcherConfigSet.SubscribeToRenamedEvent =
                        (bool)dataRow[Resources.ColumnSubscribeToRenamedEvent];

                    fileWatcherConfigSet.NotifyFilterAttributes =
                        (bool)dataRow[Resources.ColumnNotifyAttribute];

                    fileWatcherConfigSet.NotifyFilterCreationTime =
                        (bool)dataRow[Resources.ColumnNotifyCreationTime];

                    fileWatcherConfigSet.NotifyFilterDirectoryName =
                        (bool)dataRow[Resources.ColumnNotifyDirectoryName];

                    fileWatcherConfigSet.NotifyFilterFileName =
                        (bool)dataRow[Resources.ColumnNotifyFileName];

                    fileWatcherConfigSet.NotifyFilterLastAccess =
                        (bool)dataRow[Resources.ColumnNotifyLastAccess];

                    fileWatcherConfigSet.NotifyFilterLastWrite =
                        (bool)dataRow[Resources.ColumnNotifyLastWrite];

                    fileWatcherConfigSet.NotifyFilterSecurity =
                        (bool)dataRow[Resources.ColumnNotifySecurity];

                    fileWatcherConfigSet.NotifyFilterSize =
                        (bool)dataRow[Resources.ColumnNotifySize];

                    fileWatcherConfigSet.StartProcess =
                        (bool)dataRow[Resources.ColumnStartProcess];

                    fileWatcherConfigSet.LogProcessStart =
                        (bool)dataRow[Resources.ColumnLogProcessStart];

                    fileWatcherConfigSet.LogProcessEnd =
                        (bool)dataRow[Resources.ColumnLogProcessEnd];

                    fileWatcherConfigSet.ProcessFileName =
                        (string)dataRow[Resources.ColumnProcessFileName];

                    fileWatcherConfigSet.ProcessArguments =
                        (string)dataRow[Resources.ColumnProcessArguments];

                    fileWatcherConfigSet.ProcessUseFileNameAsArgument =
                        (bool)dataRow[Resources.ColumnProcessUseFileNameAsArgument];

                    fileWatcherConfigSet.ProcessArgumentsFileNameEscapeString =
                        (string)dataRow[Resources.ColumnProcessArgumentsFileNameEscapeString];

                    fileWatcherConfigSet.ProcessUseOldFileNameAsArgument =
                        (bool)dataRow[Resources.ColumnProcessUseOldFileNameAsArgument];

                    fileWatcherConfigSet.ProcessArgumentsOldFileNameEscapeString =
                        (string)dataRow[Resources.ColumnProcessArgumentsOldFileNameEscapeString];

                    fileWatcherConfigSet.ProcessUseChangeTypeAsArgument =
                        (bool)dataRow[Resources.ColumnProcessUseChangeTypeAsArgument];

                    fileWatcherConfigSet.ProcessArgumentsChangeTypeEscapeString =
                        (string)dataRow[Resources.ColumnProcessArgumentsChangeTypeEscapeString];

                    fileWatcherConfigSet.ProcessVerb =
                        (string)dataRow[Resources.ColumnProcessVerb];

                    fileWatcherConfigSet.ProcessWorkingDirectory =
                        (string)dataRow[Resources.ColumnProcessWorkingDirectory];

                    fileWatcherConfigSet.ProcessUseShellExecute =
                        (bool)dataRow[Resources.ColumnProcessUseShellExecute];

                    fileWatcherConfigSet.ProcessCreateNoWindow =
                        (bool)dataRow[Resources.ColumnProcessCreateNoWindow];

                    fileWatcherConfigSet.ProcessWindowStyle =
                        (string)dataRow[Resources.ColumnProcessWindowStyle];

                    fileWatcherConfigSet.ProcessSynchronizedExecution =
                        (bool)dataRow[Resources.ColumnProcessSynchronizedExecution];

                    fileWatcherConfigSet.ProcessMaxWaitTime =
                        (int)dataRow[Resources.ColumnProcessMaxWaitTime];

                    fileWatcherConfigSet.ProcessRedirectStandardError =
                        (bool)dataRow[Resources.ColumnProcessRedirectStandardError];

                    fileWatcherConfigSet.ProcessRedirectStandardOutput =
                        (bool)dataRow[Resources.ColumnProcessRedirectStandardOutput];

                    fileWatcherConfigSet.ProcessLoadUserProfile =
                        (bool)dataRow[Resources.ColumnProcessLoadUserProfile];

                    fileWatcherConfigSet.ProcessDomain =
                        (string)dataRow[Resources.ColumnProcessDomain];

                    fileWatcherConfigSet.ProcessUserName =
                        (string)dataRow[Resources.ColumnProcessUserName];

                    fileWatcherConfigSet.ProcessPassword =
                        (string)dataRow[Resources.ColumnProcessPassword];

                    fileWatcherConfigSet.ProcessBatchSize =
                        (int)dataRow[Resources.ColumnProcessBatchSize];

                    fileWatcherConfigSet.ProcessFileMustExist =
                        (bool)dataRow[Resources.ColumnProcessFileMustExist];

                    fileWatcherConfigSet.ProcessDelay =
                        (int)dataRow[Resources.ColumnProcessDelay];

                    fileWatcherConfigSet.ProcessLockFile =
                        (bool)dataRow[Resources.ColumnProcessLockFile];

                    fileWatcherConfigSet.ProcessLockFileLastWriteDelay =
                        (int)dataRow[Resources.ColumnProcessLockFileLastWriteDelay];

                    fileWatcherConfigSet.ProcessLockFileRetries =
                        (int)dataRow[Resources.ColumnProcessLockFileRetries];

                    fileWatcherConfigSet.ProcessLockFileRetriesQueueLimit =
                        (int)dataRow[Resources.ColumnProcessLockFileRetriesQueueLimit];

                    fileWatcherConfigSet.CallService =
                        (bool)dataRow[Resources.ColumnCallService];

                    fileWatcherConfigSet.StreamFile =
                        (bool)dataRow[Resources.ColumnStreamFile];

                    fileWatcherConfigSet.TryRenameFile =
                        (bool)dataRow[Resources.ColumnTryRenameFile];

                    fileWatcherConfigSet.TryRenameFileRetries =
                        (int)dataRow[Resources.ColumnTryRenameFileRetries];

                    fileWatcherConfigSet.FilteredMode =
                        (bool)dataRow[Resources.ColumnFilteredMode];

                    fileWatcherConfigSet.FilteredModeFilterTimeout =
                        (int)dataRow[Resources.ColumnFilteredModeFilterTimeout];

                    fileWatcherConfigSet.PollDirectory =
                        (bool)dataRow[Resources.ColumnPollDirectory];
                    
                    fileWatcherConfigSet.DirectoryPollInterval =
                        (int)dataRow[Resources.ColumnDirectoryPollInterval];

                    fileWatcherConfigSet.RecycleFileWatcher =
                        (bool)dataRow[Resources.ColumnRecycleFileWatcher];

                    fileWatcherConfigSet.RecycleInterval =
                        (int)dataRow[Resources.ColumnRecycleInterval];

                    try // Catch dictionary related exceptions.
                    {
                        configurationDictionary.Add((string)dataRow[Resources.ColumnDaemonName],
                                                    fileWatcherConfigSet);
                    }
                    catch (ArgumentNullException exception)
                    {
                        throw new ArgumentNullException(Resources.ArgumentExceptionDaemonNameIsNull,
                                                        exception);
                    }
                    catch (ArgumentException exception)
                    {
                        throw new ArgumentException(Resources.ArgumentExceptionDuplicateDaemonName,
                                                    exception);
                    }
                }
            }

            return configurationDictionary;
        }

        /// <summary>
        /// Validates file watcher XML configuration file against XML Schema.
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