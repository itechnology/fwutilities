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
using System.Globalization;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Builds Dataset.
    /// </summary>
    public static class DataSetBuilder
    {
        /// <summary>
        /// Returns dataset with configuration table.
        /// </summary>
        /// <returns>Dataset with configuration table.</returns>
        public static DataSet CreateDataSet()
        {
            DataSet dataSet = null;

            try
            {
                dataSet = new DataSet();

                dataSet.Locale = CultureInfo.InvariantCulture;
                dataSet.DataSetName = Resources.DatasetName;

                dataSet.Tables.Add(Resources.TableName);

                // Set dataset columns.
                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnDaemonName,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnStartDaemon,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnDisplayFileSystemChange,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnLogFileSystemChange,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnLogFileSystemSearchError,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnDisplayFileSystemSearchError,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnLogFileSystemSearchProgress,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnDisplayFileSystemSearchProgess,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnPath,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnFilter,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnGenerateFileSystemEventsAtStartup,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnGenereatedEventFileNameRegularExpressionFilter,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnChangedRegularExpressionFilter,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnCreatedRegularExpressionFilter,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnDeletedRegularExpressionFilter,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnRenamedRegularExpressionFilter,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnIncludeSubdirectories,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnInternalBufferSize,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnSubscribeToBufferErrorEvent,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnSubscribeToChangedEvent,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnSubscribeToCreatedEvent,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnSubscribeToDeletedEvent,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnSubscribeToRenamedEvent,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifyAttribute,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifyCreationTime,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifyDirectoryName,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifyFileName,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifyLastAccess,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifyLastWrite,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifySecurity,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnNotifySize,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnStartProcess,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnLogProcessStart,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnLogProcessEnd,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessFileName,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessArguments,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessUseFileNameAsArgument,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessArgumentsFileNameEscapeString,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessUseOldFileNameAsArgument,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessArgumentsOldFileNameEscapeString,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessUseChangeTypeAsArgument,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessArgumentsChangeTypeEscapeString,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessVerb,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessWorkingDirectory,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessUseShellExecute,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessCreateNoWindow,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessWindowStyle,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessSynchronizedExecution,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessMaxWaitTime,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessRedirectStandardError,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessRedirectStandardOutput,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessLoadUserProfile,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessDomain,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessUserName,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessPassword,
                    typeof(String));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessBatchSize,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessFileMustExist,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessDelay,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessLockFile,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessLockFileLastWriteDelay,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessLockFileRetries,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessLockFileRetriesQueueLimit,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnCallService,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnStreamFile,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnTryRenameFile,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnTryRenameFileRetries,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnFilteredMode,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnFilteredModeFilterTimeout,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnPollDirectory,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnDirectoryPollInterval,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnRecycleFileWatcher,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnRecycleInterval,
                    typeof(Int32));

                // Set all values to NOT NULL.
                foreach (DataColumn dataColumn in dataSet.Tables[Resources.TableName].Columns)
                {
                    dataColumn.AllowDBNull = false;
                }

                // Set max length for process arguments.
                dataSet.Tables[Resources.TableName].Columns[Resources.ColumnProcessArguments].MaxLength =
                    ArgumentsMaxLength;

                // Set max length fot system paths.
                dataSet.Tables[Resources.TableName].Columns[Resources.ColumnPath].MaxLength =
                    PathMaxLength;

                dataSet.Tables[Resources.TableName].Columns[Resources.ColumnProcessWorkingDirectory].MaxLength =
                    PathMaxLength;

                dataSet.Tables[Resources.TableName].Columns[Resources.ColumnProcessFileName].MaxLength =
                    PathMaxLength;

                // Daemon name must be UNIQUE (identifies the file watcher).
                dataSet.Tables[Resources.TableName].Columns[Resources.ColumnDaemonName].Unique = true;

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

        /// <summary>
        /// Max length of path (windows system).
        /// </summary>
        private const int PathMaxLength = 247;

        /// <summary>
        /// Max length of process arguments string.
        /// </summary>
        private const int ArgumentsMaxLength = 2003;
    }
}