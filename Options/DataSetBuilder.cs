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
using System.Data;
using System.Globalization;
using FileWatcherUtilities.Options.Properties;

namespace FileWatcherUtilities.Options
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
                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnAutoStartup,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnLogMessages,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnProcessBatchSize,
                    typeof(Int32));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnRunQueuedProcesses,
                    typeof(Boolean));

                dataSet.Tables[Resources.TableName].Columns.Add(Resources.ColumnSynchronousExecution,
                    typeof(Boolean));

                // Set all values to NOT NULL.
                foreach (DataColumn dataColumn in dataSet.Tables[Resources.TableName].Columns)
                {
                    dataColumn.AllowDBNull = false;
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