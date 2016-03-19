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
using System.Security;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using FileWatcherUtilities.Controller.Properties;

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// Provides control for running a process.
    /// </summary>
    internal sealed class ProcessRunner
    {
        /// <summary>
        /// Occurs when process has exited.
        /// </summary>
        public event EventHandler<ProcessExitEventArgs> ProcessExited;

        /// <summary>
        /// Occurs when process has started.
        /// </summary>
        public event EventHandler<ProcessStartedEventArgs> ProcessStarted;

        /// <summary>
        /// Occurs when process has output data.
        /// </summary>
        public event EventHandler<ProcessDataEventArgs> ProcessOutputData;

        /// <summary>
        /// Occurs when process has error data.
        /// </summary>
        public event EventHandler<ProcessDataEventArgs> ProcessErrorData;

        /// <summary>
        /// Initializes a new instance of the ProcessRunner class.
        /// </summary>
        /// <param name="fileWatcherEventArgs">File watcher event args for running a process.</param>
        /// <exception cref="ArgumentNullException">fileWatcherEventArgs is null.</exception>
        public ProcessRunner(FileWatcherEventArgs fileWatcherEventArgs)
        {
            if (fileWatcherEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherEventArgs",
                                                Resources.ArgumentNullException);
            }
            _fileWatcherEventArgs = fileWatcherEventArgs;
        }

        /// <summary>
        /// Starts process.
        /// </summary>
        /// <exception cref="InvalidOperationException">Process start info is invalid.</exception>
        /// <exception cref="ObjectDisposedException">Object disposed.</exception>
        public void StartProcess()
        {
            // Get process start info.
            ProcessStartInfo processStartInfo = GetProcessStartInfo();

            // Check process configuration for errors.
            CheckConfiguration(processStartInfo);

            // Start process
            _process = Process.Start(processStartInfo);

            // Subscribe to events.

            _process.Exited += new EventHandler(OnProcessExit);

            // Set raise event for process exit on.
            _process.EnableRaisingEvents = true;

            if (_fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessRedirectStandardOutput)
            {
                _process.OutputDataReceived +=
                    new DataReceivedEventHandler(OnOutputData);
                _process.BeginOutputReadLine();
            }

            if (_fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessRedirectStandardError)
            {
                _process.ErrorDataReceived +=
                    new DataReceivedEventHandler(OnErrorData);
                _process.BeginErrorReadLine();
            }

            RaiseProcessStartedEvent(this,
                                     new ProcessStartedEventArgs(_fileWatcherEventArgs.ConfigurationKeyValuePair.Key,
                                                                 GetProcessId(),
                                                                 _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessFileName,
                                                                 processStartInfo.Arguments,
                                                                 _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessVerb,
                                                                 GetProcessStartTime(),
                                                                 _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.LogProcessStart));

            // Wait for the same process to exit if specified.
            if (_fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessSynchronizedExecution)
            {
                _process.WaitForExit(
                    _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessMaxWaitTime);
            }
        }

        /// <summary>
        /// Replaces file name escape string with the file name.
        /// </summary>
        /// <param name="arguments">Arguments string.</param>
        /// <param name="e">File watcher event args for running a process.</param>
        /// <returns>Returns arguments with file name.</returns>
        private static string GetArgumentsWithFileName(string arguments,
                                                       FileWatcherEventArgs e)
        {
            if (!String.IsNullOrEmpty(arguments))
            {
                if (!String.IsNullOrEmpty(e.ConfigurationKeyValuePair.Value.ProcessArgumentsFileNameEscapeString))
                {
                    return arguments.Replace(e.ConfigurationKeyValuePair.Value.ProcessArgumentsFileNameEscapeString,
                                             e.FullPath);
                }
                return arguments;
            }
            return String.Empty;
        }

        /// <summary>
        /// Replaces old file name escape string with the old file name.
        /// </summary>
        /// <param name="arguments">Arguments string.</param>
        /// <param name="e">File watcher event args for running a process.</param>
        /// <returns>Returns arguments with old file name.</returns>
        private static string GetArgumentsWithOldFileName(string arguments,
                                                          FileWatcherEventArgs e)
        {
            if (!String.IsNullOrEmpty(arguments))
            {
                if (!String.IsNullOrEmpty(e.ConfigurationKeyValuePair.Value.ProcessArgumentsOldFileNameEscapeString))
                {
                    return arguments.Replace(e.ConfigurationKeyValuePair.Value.ProcessArgumentsOldFileNameEscapeString,
                                             e.OldFullPath);
                }
                return arguments;
            }
            return String.Empty;
        }

        /// <summary>
        /// Replaces change type escape string with the file name.
        /// </summary>
        /// <param name="arguments">Arguments string.</param>
        /// <param name="e">File watcher event args for running a process.</param>
        /// <returns>Returns arguments with change type.</returns>
        private static string GetArgumentsWithChangeType(string arguments,
                                                         FileWatcherEventArgs e)
        {
            if (!String.IsNullOrEmpty(arguments))
            {
                if (!String.IsNullOrEmpty(e.ConfigurationKeyValuePair.Value.ProcessArgumentsChangeTypeEscapeString))
                {
                    return arguments.Replace(e.ConfigurationKeyValuePair.Value.ProcessArgumentsChangeTypeEscapeString,
                                             e.ChangeType);
                }
                return arguments;
            }
            return String.Empty;
        }

        /// <summary>
        /// Returns process window type enumeration value.
        /// </summary>
        /// <param name="processWindowStyle">Process window type.</param>
        /// <returns>Process window type enumeration value.</returns>
        private static ProcessWindowStyle GetWindowStyle(string processWindowStyle)
        {
            const string Hidden = "HIDDEN";
            const string Maximized = "MAXIMIZED";
            const string Minimized = "MINIMIZED";
            const string Normal = "NORMAL";

            processWindowStyle =
                processWindowStyle.ToUpper(CultureInfo.InvariantCulture);

            if (String.Compare(processWindowStyle, Normal, StringComparison.Ordinal) == 0)
            {
                return ProcessWindowStyle.Normal;
            }
            if (String.Compare(processWindowStyle, Hidden, StringComparison.Ordinal) == 0)
            {
                return ProcessWindowStyle.Hidden;
            }
            if (String.Compare(processWindowStyle, Maximized, StringComparison.Ordinal) == 0)
            {
                return ProcessWindowStyle.Maximized;
            }
            if (String.Compare(processWindowStyle, Minimized, StringComparison.Ordinal) == 0)
            {
                return ProcessWindowStyle.Minimized;
            }
            return ProcessWindowStyle.Normal;
        }

        /// <summary>
        /// Returns secure password from string.
        /// </summary>
        /// <param name="password">Password.</param>
        /// <returns>Secure password.</returns>
        private static SecureString GetSecurePassword(string password)
        {
            SecureString securePassword = null;

            try
            {
                if (password.Length > 0)
                {
                    securePassword = new SecureString();

                    foreach (char passwordChar in password)
                    {
                        securePassword.AppendChar(passwordChar);
                    }

                    return securePassword;
                }

                return null;
            }
            catch
            {
                if (securePassword != null)
                {
                    securePassword.Dispose();
                }

                throw;
            }
        }

        /// <summary>
        /// Returns process arguments with replaced values.
        /// </summary>
        /// <param name="e">File watcher event args for running a process.</param>
        /// <returns>Process arguments with replaced values.</returns>
        private static string GetProcessArguments(FileWatcherEventArgs e)
        {
            // If something to replace
            if (!String.IsNullOrEmpty(e.ConfigurationKeyValuePair.Value.ProcessArguments))
            {
                string arguments = e.ConfigurationKeyValuePair.Value.ProcessArguments;

                // Replace escape strings with values.
                if (e.ConfigurationKeyValuePair.Value.ProcessUseChangeTypeAsArgument)
                {
                    arguments = GetArgumentsWithChangeType(arguments,
                                                           e);
                }
                if (e.ConfigurationKeyValuePair.Value.ProcessUseFileNameAsArgument)
                {
                    arguments = GetArgumentsWithFileName(arguments,
                                                         e);
                }
                if (e.ConfigurationKeyValuePair.Value.ProcessUseOldFileNameAsArgument)
                {
                    arguments = GetArgumentsWithOldFileName(arguments,
                                                            e);
                }

                return arguments;
            }
            return e.ConfigurationKeyValuePair.Value.ProcessArguments; // Nothing to replace.
        }

        /// <summary>
        /// Returns process start info created for file watcher event args.
        /// </summary>
        /// <returns>Process start info created for file watcher event args.</returns>
        private ProcessStartInfo GetProcessStartInfo()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();

            processStartInfo.FileName =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessFileName;

            // Get modified argument string (if there is something to replace).
            processStartInfo.Arguments =
                GetProcessArguments(_fileWatcherEventArgs);

            processStartInfo.Verb =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessVerb;

            processStartInfo.UseShellExecute =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessUseShellExecute;

            processStartInfo.WorkingDirectory =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessWorkingDirectory;

            processStartInfo.CreateNoWindow =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessCreateNoWindow;

            processStartInfo.WindowStyle =
                GetWindowStyle(_fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessWindowStyle);

            processStartInfo.RedirectStandardError =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessRedirectStandardError;

            processStartInfo.RedirectStandardOutput =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessRedirectStandardOutput;

            processStartInfo.LoadUserProfile =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessLoadUserProfile;

            processStartInfo.Domain =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessDomain;

            processStartInfo.UserName =
                _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessUserName;

            processStartInfo.Password =
                GetSecurePassword(_fileWatcherEventArgs.ConfigurationKeyValuePair.Value.ProcessPassword);

            return processStartInfo;
        }

        /// <summary>
        /// Checks process start info configuration for configuration errors.
        /// </summary>
        /// <param name="processStartInfo">Process start info to check.</param>
        /// <exception cref="InvalidOperationException">Process start info is invalid.</exception>
        private static void CheckConfiguration(ProcessStartInfo processStartInfo)
        {
            if (String.IsNullOrEmpty(processStartInfo.FileName))
            {
                throw new InvalidOperationException(
                    Resources.InvalidOperationExceptionFileName);
            }
            if (processStartInfo.UseShellExecute)
            {
                if (processStartInfo.RedirectStandardOutput)
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionOutput);
                }
                if (processStartInfo.RedirectStandardError)
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionError);
                }
                if (processStartInfo.RedirectStandardInput)
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionInput);
                }
                if (processStartInfo.Password != null)
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionPassword);
                }
                if (!String.IsNullOrEmpty(processStartInfo.UserName))
                {
                    throw new InvalidOperationException(
                        Resources.InvalidOperationExceptionUserName);
                }
            }
        }

        /// <summary>
        /// Returns process Id.
        /// </summary>
        /// <returns>Process Id or -1 on error.</returns>
        private int GetProcessId()
        {
            try
            {
                return _process.Id;
            }
            catch (PlatformNotSupportedException)
            {
                return -1;
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Returns process exit code.
        /// </summary>
        /// <returns>Process exit code or -1 on error.</returns>
        private int GetExitCode()
        {
            try
            {
                return _process.ExitCode;
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }

        /// <summary>
        /// Return process exit time.
        /// </summary>
        /// <returns>Process exit time or current time on error.</returns>
        private DateTime GetProcessExitTime()
        {
            try
            {
                return _process.ExitTime;
            }
            catch (PlatformNotSupportedException)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Returns process start time.
        /// </summary>
        /// <returns>Process start time or current time on error.</returns>
        private DateTime GetProcessStartTime()
        {
            try
            {
                return _process.StartTime;
            }
            catch (PlatformNotSupportedException)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Handles process exit event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnProcessExit(object sender,
                                   EventArgs e)
        {
            EventHandler<ProcessExitEventArgs> handler = ProcessExited;
            if (handler != null)
            {
                handler(this,
                        new ProcessExitEventArgs(_fileWatcherEventArgs.ConfigurationKeyValuePair.Key,
                                                 GetProcessId(),
                                                 GetExitCode(),
                                                 GetProcessExitTime(),
                                                 _fileWatcherEventArgs.ConfigurationKeyValuePair.Value.LogProcessEnd));
            }
        }

        /// <summary>
        /// Raises process started event. Runs in the main thread.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void RaiseProcessStartedEvent(object sender,
                                              ProcessStartedEventArgs e)
        {
            EventHandler<ProcessStartedEventArgs> handler = ProcessStarted;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        /// <summary>
        /// Handles process output data event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnOutputData(object sender,
                                  DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                EventHandler<ProcessDataEventArgs> handler = ProcessOutputData;
                if (handler != null)
                {
                    handler(this,
                            new ProcessDataEventArgs(_fileWatcherEventArgs.ConfigurationKeyValuePair.Key,
                                                     GetProcessId(),
                                                     e.Data));
                }
            }
        }

        /// <summary>
        /// Handles process error data event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnErrorData(object sender,
                                 DataReceivedEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Data))
            {
                EventHandler<ProcessDataEventArgs> handler = ProcessErrorData;
                if (handler != null)
                {
                    handler(this,
                            new ProcessDataEventArgs(_fileWatcherEventArgs.ConfigurationKeyValuePair.Key,
                                                     GetProcessId(),
                                                     e.Data));
                }
            }
        }

        /// <summary>
        /// Contains file watcher event args for running a process.
        /// </summary>
        private readonly FileWatcherEventArgs _fileWatcherEventArgs;

        /// <summary>
        /// Contains process.
        /// </summary>
        private Process _process;
    }
}