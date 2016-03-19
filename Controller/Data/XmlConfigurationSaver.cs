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
using System.Data;
using System.Collections.Generic;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Saves configuration to XML files.
    /// </summary>
    public static class XmlConfigurationSaver
    {
        /// <summary>
        /// Stores given file watcher configurations sets in XML file and creates XML Schema.
        /// </summary>
        /// <param name="configurationDictionary">File watcher configuration sets to store.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <exception cref="ArgumentNullException">fileWatcherConfigSets is null.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        public static void SaveConfigurationSets(Dictionary<string, FileWatcherConfigurationSet> configurationDictionary,
                                                 string xmlConfigFilePath,
                                                 string xmlSchemaConfigFilePath)
        {
            if (configurationDictionary == null)
            {
                throw new ArgumentNullException("configurationDictionary",
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

            DataSet dataSet = null;

            try
            {
                // Create dataset and set rows and columns.
                dataSet = BuildConfigurationDataSet(configurationDictionary);

                // Write configuration files.
                dataSet.WriteXmlSchema(xmlSchemaConfigFilePath);
                dataSet.WriteXml(xmlConfigFilePath);
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }
        }

        /// <summary>
        /// Stores given file watcher configurations sets in XML file.
        /// </summary>
        /// <param name="configurationDictionary">File watcher configuration sets to store.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <exception cref="ArgumentNullException">fileWatcherConfigSets is null.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        public static void SaveConfigurationSets(Dictionary<string, FileWatcherConfigurationSet> configurationDictionary,
                                                 string xmlConfigFilePath)
        {
            if (configurationDictionary == null)
            {
                throw new ArgumentNullException("configurationDictionary",
                                                Resources.ArgumentNullException);
            }
            if (xmlConfigFilePath == null)
            {
                throw new ArgumentNullException("xmlConfigFilePath",
                                                Resources.ArgumentNullException);
            }
            
            DataSet dataSet = null;

            try
            {
                // Create dataset and set rows and columns.
                dataSet = BuildConfigurationDataSet(configurationDictionary);

                // Write configuration file.
                dataSet.WriteXml(xmlConfigFilePath);
            }
            finally
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }
            }            
        }

        /// <summary>
        /// Creates configuration DataSet.
        /// </summary>
        /// <param name="configurationDictionary">File watcher configuration sets to store.</param>
        /// <returns>Configuration DataSet.</returns>
        private static DataSet BuildConfigurationDataSet(Dictionary<string, FileWatcherConfigurationSet> configurationDictionary)
        {
            DataSet dataSet = null;

            try
            {
                // Create dataset and set columns.
                dataSet = DataSetBuilder.CreateDataSet();

                // Store configuration values to dataset.
                foreach (KeyValuePair<string, FileWatcherConfigurationSet> configuration in configurationDictionary)
                {
                    // Create new datarow.
                    DataRow dataRow = dataSet.Tables[Resources.TableName].NewRow();

                    // Add values to datarow.
                    dataRow[Resources.ColumnDaemonName] =
                        configuration.Key;

                    dataRow[Resources.ColumnStartDaemon] =
                        configuration.Value.StartDaemon;

                    dataRow[Resources.ColumnDisplayFileSystemChange] =
                        configuration.Value.DisplayFileSystemChange;

                    dataRow[Resources.ColumnLogFileSystemChange] =
                        configuration.Value.LogFileSystemChange;

                    dataRow[Resources.ColumnLogFileSystemSearchError] =
                        configuration.Value.LogFileSystemSearchError;

                    dataRow[Resources.ColumnDisplayFileSystemSearchError] =
                        configuration.Value.DisplayFileSystemSearchError;

                    dataRow[Resources.ColumnLogFileSystemSearchProgress] =
                        configuration.Value.LogFileSystemSearchProgress;

                    dataRow[Resources.ColumnDisplayFileSystemSearchProgess] =
                        configuration.Value.DisplayFileSystemSearchProgress;

                    dataRow[Resources.ColumnPath] =
                        configuration.Value.Path;

                    dataRow[Resources.ColumnFilter] =
                        configuration.Value.Filter;

                    dataRow[Resources.ColumnGenerateFileSystemEventsAtStartup] =
                          configuration.Value.GenerateFileSystemEventsAtStartup;

                    dataRow[Resources.ColumnGenereatedEventFileNameRegularExpressionFilter] =
                        configuration.Value.GeneratedEventFileNameRegularExpressionFilter;

                    dataRow[Resources.ColumnChangedRegularExpressionFilter] =
                        configuration.Value.ChangedRegularExpressionFilter;

                    dataRow[Resources.ColumnCreatedRegularExpressionFilter] =
                        configuration.Value.CreatedRegularExpressionFilter;

                    dataRow[Resources.ColumnDeletedRegularExpressionFilter] =
                        configuration.Value.DeletedRegularExpressionFilter;

                    dataRow[Resources.ColumnRenamedRegularExpressionFilter] =
                        configuration.Value.RenamedRegularExpressionFilter;

                    dataRow[Resources.ColumnIncludeSubdirectories] =
                        configuration.Value.IncludeSubdirectories;

                    dataRow[Resources.ColumnInternalBufferSize] =
                        configuration.Value.InternalBufferSize;

                    dataRow[Resources.ColumnSubscribeToBufferErrorEvent] =
                        configuration.Value.SubscribeToBufferErrorEvent;

                    dataRow[Resources.ColumnSubscribeToChangedEvent] =
                        configuration.Value.SubscribeToChangedEvent;

                    dataRow[Resources.ColumnSubscribeToCreatedEvent] =
                        configuration.Value.SubscribeToCreatedEvent;

                    dataRow[Resources.ColumnSubscribeToDeletedEvent] =
                        configuration.Value.SubscribeToDeletedEvent;

                    dataRow[Resources.ColumnSubscribeToRenamedEvent] =
                        configuration.Value.SubscribeToRenamedEvent;

                    dataRow[Resources.ColumnNotifyAttribute] =
                        configuration.Value.NotifyFilterAttributes;

                    dataRow[Resources.ColumnNotifyCreationTime] =
                        configuration.Value.NotifyFilterCreationTime;

                    dataRow[Resources.ColumnNotifyDirectoryName] =
                        configuration.Value.NotifyFilterDirectoryName;

                    dataRow[Resources.ColumnNotifyFileName] =
                        configuration.Value.NotifyFilterFileName;

                    dataRow[Resources.ColumnNotifyLastAccess] =
                        configuration.Value.NotifyFilterLastAccess;

                    dataRow[Resources.ColumnNotifyLastWrite] =
                        configuration.Value.NotifyFilterLastWrite;

                    dataRow[Resources.ColumnNotifySecurity] =
                        configuration.Value.NotifyFilterSecurity;

                    dataRow[Resources.ColumnNotifySize] =
                        configuration.Value.NotifyFilterSize;

                    dataRow[Resources.ColumnStartProcess] =
                        configuration.Value.StartProcess;

                    dataRow[Resources.ColumnLogProcessStart] =
                        configuration.Value.LogProcessStart;

                    dataRow[Resources.ColumnLogProcessEnd] =
                        configuration.Value.LogProcessEnd;

                    dataRow[Resources.ColumnProcessFileName] =
                        configuration.Value.ProcessFileName;

                    dataRow[Resources.ColumnProcessArguments] =
                        configuration.Value.ProcessArguments;

                    dataRow[Resources.ColumnProcessUseFileNameAsArgument] =
                        configuration.Value.ProcessUseFileNameAsArgument;

                    dataRow[Resources.ColumnProcessArgumentsFileNameEscapeString] =
                        configuration.Value.ProcessArgumentsFileNameEscapeString;

                    dataRow[Resources.ColumnProcessUseOldFileNameAsArgument] =
                        configuration.Value.ProcessUseOldFileNameAsArgument;

                    dataRow[Resources.ColumnProcessArgumentsOldFileNameEscapeString] =
                        configuration.Value.ProcessArgumentsOldFileNameEscapeString;

                    dataRow[Resources.ColumnProcessUseChangeTypeAsArgument] =
                        configuration.Value.ProcessUseChangeTypeAsArgument;

                    dataRow[Resources.ColumnProcessArgumentsChangeTypeEscapeString] =
                        configuration.Value.ProcessArgumentsChangeTypeEscapeString;

                    dataRow[Resources.ColumnProcessVerb] =
                        configuration.Value.ProcessVerb;

                    dataRow[Resources.ColumnProcessWorkingDirectory] =
                        configuration.Value.ProcessWorkingDirectory;

                    dataRow[Resources.ColumnProcessUseShellExecute] =
                        configuration.Value.ProcessUseShellExecute;

                    dataRow[Resources.ColumnProcessCreateNoWindow] =
                        configuration.Value.ProcessCreateNoWindow;

                    dataRow[Resources.ColumnProcessWindowStyle] =
                        configuration.Value.ProcessWindowStyle;

                    dataRow[Resources.ColumnProcessSynchronizedExecution] =
                        configuration.Value.ProcessSynchronizedExecution;

                    dataRow[Resources.ColumnProcessMaxWaitTime] =
                        configuration.Value.ProcessMaxWaitTime;

                    dataRow[Resources.ColumnProcessRedirectStandardError] =
                        configuration.Value.ProcessRedirectStandardError;

                    dataRow[Resources.ColumnProcessRedirectStandardOutput] =
                        configuration.Value.ProcessRedirectStandardOutput;

                    dataRow[Resources.ColumnProcessLoadUserProfile] =
                        configuration.Value.ProcessLoadUserProfile;

                    dataRow[Resources.ColumnProcessDomain] =
                        configuration.Value.ProcessDomain;

                    dataRow[Resources.ColumnProcessUserName] =
                        configuration.Value.ProcessUserName;

                    dataRow[Resources.ColumnProcessPassword] =
                        configuration.Value.ProcessPassword;

                    dataRow[Resources.ColumnProcessBatchSize] =
                        configuration.Value.ProcessBatchSize;

                    dataRow[Resources.ColumnProcessFileMustExist] =
                        configuration.Value.ProcessFileMustExist;

                    dataRow[Resources.ColumnProcessDelay] =
                        configuration.Value.ProcessDelay;

                    dataRow[Resources.ColumnProcessLockFile] =
                        configuration.Value.ProcessLockFile;

                    dataRow[Resources.ColumnProcessLockFileLastWriteDelay] =
                        configuration.Value.ProcessLockFileLastWriteDelay;

                    dataRow[Resources.ColumnProcessLockFileRetries] =
                        configuration.Value.ProcessLockFileRetries;

                    dataRow[Resources.ColumnProcessLockFileRetriesQueueLimit] =
                        configuration.Value.ProcessLockFileRetriesQueueLimit;

                    dataRow[Resources.ColumnCallService] =
                        configuration.Value.CallService;

                    dataRow[Resources.ColumnStreamFile] =
                        configuration.Value.StreamFile;

                    dataRow[Resources.ColumnTryRenameFile] =
                        configuration.Value.TryRenameFile;

                    dataRow[Resources.ColumnTryRenameFileRetries] =
                        configuration.Value.TryRenameFileRetries;

                    dataRow[Resources.ColumnFilteredMode] =
                        configuration.Value.FilteredMode;

                    dataRow[Resources.ColumnFilteredModeFilterTimeout] =
                        configuration.Value.FilteredModeFilterTimeout;

                    dataRow[Resources.ColumnPollDirectory] =
                        configuration.Value.PollDirectory;

                    dataRow[Resources.ColumnDirectoryPollInterval] =
                        configuration.Value.DirectoryPollInterval;

                    dataRow[Resources.ColumnRecycleFileWatcher] =
                        configuration.Value.RecycleFileWatcher;

                    dataRow[Resources.ColumnRecycleInterval] =
                        configuration.Value.RecycleInterval;

                    // Add row to dataset.
                    dataSet.Tables[Resources.TableName].Rows.Add(dataRow);
                }

                // Accept changes.
                dataSet.AcceptChanges();

                return dataSet;
            }
            catch
            {
                if (dataSet != null)
                {
                    dataSet.Dispose();
                }

                throw;
            }
        }
    }
}