/******************************************************************************
*    File Watcher Utilities / Common
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
using System.Configuration;
using System.Globalization;
using System.Security.Permissions;
using FileWatcherUtilities.Common.Properties;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.Common
{
    /// <summary>
    /// Provides basic application configuration.
    /// </summary>
    public static class ConfigurationWrapper
    {        
        /// <summary>
        /// Gets XML configuration file path.
        /// </summary>
        public static string XmlConfigurationFilePath 
        { 
            get
            {
                string value = 
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldXmlConfigurationFilePath];

                if (String.IsNullOrEmpty(value))
                {
                    return Resources.XmlConfigFileName;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets XML configuration file path.
        /// </summary>
        public static string XmlApplicationOptionsFilePath
        {
            get
            {
                string value =
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldXmlApplicationOptionsFilePath];

                if (String.IsNullOrEmpty(value))
                {
                    return Resources.XmlApplicationOptionsFileName;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets XML Schema configuration file path.
        /// </summary>
        public static string XmlSchemaApplicationOptionsFilePath
        {
            get
            {
                string value =
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldXmlSchemaApplicationOptionsFilePath];

                if (String.IsNullOrEmpty(value))
                {
                    return Resources.XmlSchemaApplicationOptionsFileName;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets log file path.
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                string value = 
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldLogFilePath];

                if (String.IsNullOrEmpty(value))
                {
                    return Resources.LogFileName;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets XML Schema configuration file path.
        /// </summary>
        public static string XmlSchemaConfigurationFilePath
        {
            get
            {
                string value =
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldXmlSchemaConfigurationFilePath];

                if (String.IsNullOrEmpty(value))
                {
                     return Resources.XmlSchemaFileName;
                }
                return value;
            }
        }

        /// <summary>
        /// Gets file watcher display update interval. Default is 1000.
        /// </summary>
        public static double UpdateInterval
        {
            get
            {
                string value = 
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldUpdateInterval];

                double updateInterval = 1000;

                try
                {
                    updateInterval = Convert.ToDouble(value,
                                                      NumberFormatInfo.InvariantInfo);
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }

                return updateInterval;
            }
        }

        /// <summary>
        /// Gets log view update interval. Default is 300.
        /// </summary>
        public static double LogUpdateInterval
        {
            get
            {
                string value = 
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldLogUpdateInterval];

                double logUpdateInterval = 300;

                try
                {
                    logUpdateInterval = Convert.ToDouble(value,
                                                         NumberFormatInfo.InvariantInfo);
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }

                return logUpdateInterval;
            }
        }

        /// <summary>
        /// Gets queue trim interval. Default is 5.
        /// </summary>
        public static int QueueTrimInterval
        {
            get
            {
                string value =
                    ConfigurationManager.AppSettings[Resources.ConfigurationFieldQueueTrimInterval];

                int queueTrimInterval = 5;

                try
                {
                    queueTrimInterval = Convert.ToInt32(value,
                                                        NumberFormatInfo.InvariantInfo);
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }

                return queueTrimInterval;
            }
        }
    }
}