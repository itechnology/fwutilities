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
using System.Collections.Generic;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides example daemon configurations.
    /// </summary>
    public class FileWatcherExampleDaemonWriter
    {
        /// <summary>
        /// Initializes a new instance of the FileWatcherExampleDaemon class.
        /// </summary>
        /// <param name="xmlConfigFilePath">Path of the configuration XML file.</param>
        /// <param name="xmlSchemaConfigFilePath">Path of the configuration XML Schema file.</param>
        /// <exception cref="ArgumentNullException">xmlConfigFilePath is null.</exception>
        /// <exception cref="ArgumentNullException">xmlSchemaConfigFilePath is null.</exception>
        public FileWatcherExampleDaemonWriter(string xmlConfigFilePath,
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
            _xmlConfigFilePath = xmlConfigFilePath;
            _xmlSchemaConfigFilePath = xmlSchemaConfigFilePath;
        }

        /// <summary>
        /// Creates example daemon configuration files.
        /// </summary>
        public void CreateExampleDaemonConfigFiles()
        {
            Dictionary<string, FileWatcherConfigurationSet> configurationDictionary =
                new Dictionary<string, FileWatcherConfigurationSet>();

            configurationDictionary.Add("Default 'notepad.exe' Daemon (monitors: *.txt)",
                                        GetNotepadDaemonConfiguration());
            configurationDictionary.Add("Default 'calc.exe' Daemon (monitors: *.txt)",
                                        GetCalcDaemonConfiguration());
            configurationDictionary.Add("Default 'mspaint.exe' Daemon (monitors: *.txt)", 
                                        GetMSPaintDaemonConfiguration());

            XmlConfigurationSaver.SaveConfigurationSets(configurationDictionary,
                                                        _xmlConfigFilePath, 
                                                        _xmlSchemaConfigFilePath);
        }

        /// <summary>
        /// Creates example notepad daemon.
        /// </summary>
        /// <returns>File watcher configuration set.</returns>
        private static FileWatcherConfigurationSet GetNotepadDaemonConfiguration()
        {
            FileWatcherConfigurationSet fileWatcherConfigurationSet = 
                new FileWatcherConfigurationSet();

            fileWatcherConfigurationSet.StartDaemon = true;
            fileWatcherConfigurationSet.DisplayFileSystemChange = true;
            fileWatcherConfigurationSet.LogFileSystemChange = true;
            fileWatcherConfigurationSet.Path = Environment.CurrentDirectory;
            fileWatcherConfigurationSet.Filter = "*.txt";
            fileWatcherConfigurationSet.IncludeSubdirectories = false;
            fileWatcherConfigurationSet.InternalBufferSize = 8192;
            fileWatcherConfigurationSet.SubscribeToBufferErrorEvent = true;
            fileWatcherConfigurationSet.SubscribeToChangedEvent = true;
            fileWatcherConfigurationSet.SubscribeToCreatedEvent = true;
            fileWatcherConfigurationSet.SubscribeToDeletedEvent = true;
            fileWatcherConfigurationSet.SubscribeToRenamedEvent = true;
            fileWatcherConfigurationSet.NotifyFilterAttributes = false;
            fileWatcherConfigurationSet.NotifyFilterCreationTime = false;
            fileWatcherConfigurationSet.NotifyFilterDirectoryName = true;
            fileWatcherConfigurationSet.NotifyFilterFileName = true;
            fileWatcherConfigurationSet.NotifyFilterLastAccess = false;
            fileWatcherConfigurationSet.NotifyFilterLastWrite = true;
            fileWatcherConfigurationSet.NotifyFilterSecurity = false;
            fileWatcherConfigurationSet.NotifyFilterSize = false;

            fileWatcherConfigurationSet.StartProcess = true;
            fileWatcherConfigurationSet.LogProcessStart = true;
            fileWatcherConfigurationSet.LogProcessEnd = true;
            fileWatcherConfigurationSet.ProcessFileName = "notepad.exe";
            fileWatcherConfigurationSet.ProcessArguments = "[processArgumentsFileName]";
            fileWatcherConfigurationSet.ProcessUseChangeTypeAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsChangeTypeEscapeString = "";
            fileWatcherConfigurationSet.ProcessUseFileNameAsArgument = true;
            fileWatcherConfigurationSet.ProcessArgumentsFileNameEscapeString = "[processArgumentsFileName]";
            fileWatcherConfigurationSet.ProcessUseOldFileNameAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsOldFileNameEscapeString = "";
            fileWatcherConfigurationSet.ProcessVerb = "";
            fileWatcherConfigurationSet.ProcessWorkingDirectory = Environment.CurrentDirectory;
            fileWatcherConfigurationSet.ProcessUseShellExecute = false;
            fileWatcherConfigurationSet.ProcessCreateNoWindow = false;
            fileWatcherConfigurationSet.ProcessWindowStyle = "Normal";
            fileWatcherConfigurationSet.ProcessSynchronizedExecution = false;
            fileWatcherConfigurationSet.ProcessMaxWaitTime = 0;
            fileWatcherConfigurationSet.ProcessRedirectStandardError = false;
            fileWatcherConfigurationSet.ProcessRedirectStandardOutput = false;
            fileWatcherConfigurationSet.ProcessLoadUserProfile = false;
            fileWatcherConfigurationSet.ProcessDomain = "";
            fileWatcherConfigurationSet.ProcessUserName = "";
            fileWatcherConfigurationSet.ProcessPassword = "";

            return fileWatcherConfigurationSet;
        }

        /// <summary>
        /// Creates example calc daemon.
        /// </summary>
        /// <returns>File watcher configuration set.</returns>
        private static FileWatcherConfigurationSet GetCalcDaemonConfiguration()
        {
            FileWatcherConfigurationSet fileWatcherConfigurationSet = 
                new FileWatcherConfigurationSet();

            fileWatcherConfigurationSet.StartDaemon = true;
            fileWatcherConfigurationSet.DisplayFileSystemChange = true;
            fileWatcherConfigurationSet.LogFileSystemChange = true;
            fileWatcherConfigurationSet.Path = Environment.CurrentDirectory;
            fileWatcherConfigurationSet.Filter = "*.txt";
            fileWatcherConfigurationSet.IncludeSubdirectories = false;
            fileWatcherConfigurationSet.InternalBufferSize = 8192;
            fileWatcherConfigurationSet.SubscribeToBufferErrorEvent = true;
            fileWatcherConfigurationSet.SubscribeToChangedEvent = true;
            fileWatcherConfigurationSet.SubscribeToCreatedEvent = true;
            fileWatcherConfigurationSet.SubscribeToDeletedEvent = true;
            fileWatcherConfigurationSet.SubscribeToRenamedEvent = true;
            fileWatcherConfigurationSet.NotifyFilterAttributes = false;
            fileWatcherConfigurationSet.NotifyFilterCreationTime = false;
            fileWatcherConfigurationSet.NotifyFilterDirectoryName = true;
            fileWatcherConfigurationSet.NotifyFilterFileName = true;
            fileWatcherConfigurationSet.NotifyFilterLastAccess = false;
            fileWatcherConfigurationSet.NotifyFilterLastWrite = true;
            fileWatcherConfigurationSet.NotifyFilterSecurity = false;
            fileWatcherConfigurationSet.NotifyFilterSize = false;

            fileWatcherConfigurationSet.StartProcess = true;
            fileWatcherConfigurationSet.LogProcessStart = true;
            fileWatcherConfigurationSet.LogProcessEnd = true;
            fileWatcherConfigurationSet.ProcessFileName = "calc.exe";
            fileWatcherConfigurationSet.ProcessArguments = "";
            fileWatcherConfigurationSet.ProcessUseChangeTypeAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsChangeTypeEscapeString = "";
            fileWatcherConfigurationSet.ProcessUseFileNameAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsFileNameEscapeString = "";
            fileWatcherConfigurationSet.ProcessUseOldFileNameAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsOldFileNameEscapeString = "";
            fileWatcherConfigurationSet.ProcessVerb = "";
            fileWatcherConfigurationSet.ProcessWorkingDirectory = Environment.CurrentDirectory;
            fileWatcherConfigurationSet.ProcessUseShellExecute = false;
            fileWatcherConfigurationSet.ProcessCreateNoWindow = false;
            fileWatcherConfigurationSet.ProcessWindowStyle = "Normal";
            fileWatcherConfigurationSet.ProcessSynchronizedExecution = false;
            fileWatcherConfigurationSet.ProcessMaxWaitTime = 0;
            fileWatcherConfigurationSet.ProcessRedirectStandardError = false;
            fileWatcherConfigurationSet.ProcessRedirectStandardOutput = false;
            fileWatcherConfigurationSet.ProcessLoadUserProfile = false;
            fileWatcherConfigurationSet.ProcessDomain = "";
            fileWatcherConfigurationSet.ProcessUserName = "";
            fileWatcherConfigurationSet.ProcessPassword = "";

            return fileWatcherConfigurationSet;
        }

        /// <summary>
        /// Creates example msPaint daemon.
        /// </summary>
        /// <returns>File watcher configuration set.</returns>
        private static FileWatcherConfigurationSet GetMSPaintDaemonConfiguration()
        {
            FileWatcherConfigurationSet fileWatcherConfigurationSet =
                new FileWatcherConfigurationSet();

            fileWatcherConfigurationSet.StartDaemon = true;
            fileWatcherConfigurationSet.DisplayFileSystemChange = true;
            fileWatcherConfigurationSet.LogFileSystemChange = true;
            fileWatcherConfigurationSet.Path = Environment.CurrentDirectory;
            fileWatcherConfigurationSet.Filter = "*.txt";
            fileWatcherConfigurationSet.IncludeSubdirectories = false;
            fileWatcherConfigurationSet.InternalBufferSize = 8192;
            fileWatcherConfigurationSet.SubscribeToBufferErrorEvent = true;
            fileWatcherConfigurationSet.SubscribeToChangedEvent = true;
            fileWatcherConfigurationSet.SubscribeToCreatedEvent = true;
            fileWatcherConfigurationSet.SubscribeToDeletedEvent = true;
            fileWatcherConfigurationSet.SubscribeToRenamedEvent = true;
            fileWatcherConfigurationSet.NotifyFilterAttributes = false;
            fileWatcherConfigurationSet.NotifyFilterCreationTime = false;
            fileWatcherConfigurationSet.NotifyFilterDirectoryName = true;
            fileWatcherConfigurationSet.NotifyFilterFileName = true;
            fileWatcherConfigurationSet.NotifyFilterLastAccess = false;
            fileWatcherConfigurationSet.NotifyFilterLastWrite = true;
            fileWatcherConfigurationSet.NotifyFilterSecurity = false;
            fileWatcherConfigurationSet.NotifyFilterSize = false;

            fileWatcherConfigurationSet.StartProcess = true;
            fileWatcherConfigurationSet.LogProcessStart = true;
            fileWatcherConfigurationSet.LogProcessEnd = true;
            fileWatcherConfigurationSet.ProcessFileName = "mspaint.exe";
            fileWatcherConfigurationSet.ProcessArguments = "";
            fileWatcherConfigurationSet.ProcessUseChangeTypeAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsChangeTypeEscapeString = "";
            fileWatcherConfigurationSet.ProcessUseFileNameAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsFileNameEscapeString = "";
            fileWatcherConfigurationSet.ProcessUseOldFileNameAsArgument = false;
            fileWatcherConfigurationSet.ProcessArgumentsOldFileNameEscapeString = "";
            fileWatcherConfigurationSet.ProcessVerb = "";
            fileWatcherConfigurationSet.ProcessWorkingDirectory = Environment.CurrentDirectory;
            fileWatcherConfigurationSet.ProcessUseShellExecute = false;
            fileWatcherConfigurationSet.ProcessCreateNoWindow = false;
            fileWatcherConfigurationSet.ProcessWindowStyle = "Normal";
            fileWatcherConfigurationSet.ProcessSynchronizedExecution = false;
            fileWatcherConfigurationSet.ProcessMaxWaitTime = 0;
            fileWatcherConfigurationSet.ProcessRedirectStandardError = false;
            fileWatcherConfigurationSet.ProcessRedirectStandardOutput = false;
            fileWatcherConfigurationSet.ProcessLoadUserProfile = false;
            fileWatcherConfigurationSet.ProcessDomain = "";
            fileWatcherConfigurationSet.ProcessUserName = "";
            fileWatcherConfigurationSet.ProcessPassword = "";

            return fileWatcherConfigurationSet;
        }

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