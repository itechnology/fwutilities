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
using FileWatcherUtilities.Options.Properties;

namespace FileWatcherUtilities.Options
{
    public static class XmlOptionsSaver
    {
        /// <summary>
        /// Stores given application options in XML file and creates XML Schema.
        /// </summary>
        /// <param name="applicationOptions">Application options to store.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <exception cref="ArgumentNullException">applicationOptions is null.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        public static void SaveApplicationOptions(ApplicationOptions applicationOptions,
                                                  string xmlConfigFilePath,
                                                  string xmlSchemaConfigFilePath)
        {
            if (applicationOptions == null)
            {
                throw new ArgumentNullException("applicationOptions",
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
                dataSet = BuildConfigurationDataSet(applicationOptions);

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
        /// Stores given application options in XML file.
        /// </summary>
        /// <param name="applicationOptions">Application options to store.</param>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <exception cref="ArgumentNullException">applicationOptions is null.</exception>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        public static void SaveApplicationOptions(ApplicationOptions applicationOptions,
                                                  string xmlConfigFilePath)
        {
            if (applicationOptions == null)
            {
                throw new ArgumentNullException("applicationOptions",
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
                dataSet = BuildConfigurationDataSet(applicationOptions);

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
        /// <param name="applicationOptions">Application options to store.</param>
        /// <returns>Configuration DataSet.</returns>
        private static DataSet BuildConfigurationDataSet(ApplicationOptions applicationOptions)
        {
            DataSet dataSet = null;

            try
            {
                // Create dataset and set columns.
                dataSet = DataSetBuilder.CreateDataSet();

                // Create new datarow.
                DataRow dataRow = dataSet.Tables[Resources.TableName].NewRow();

                // Add values to datarow.
                dataRow[Resources.ColumnAutoStartup] = applicationOptions.AutoStartup;
                dataRow[Resources.ColumnLogMessages] = applicationOptions.LogMessages;
                dataRow[Resources.ColumnProcessBatchSize] = applicationOptions.ProcessBatchSize;
                dataRow[Resources.ColumnRunQueuedProcesses] = applicationOptions.RunQueuedProcesses;
                dataRow[Resources.ColumnSynchronousExecution] = applicationOptions.SynchronousExecution;

                // Add row to dataset.
                dataSet.Tables[Resources.TableName].Rows.Add(dataRow);

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