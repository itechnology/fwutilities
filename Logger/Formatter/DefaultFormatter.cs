/******************************************************************************
*    File Watcher Utilities / Logger
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

#if (!_NET_20)
using System.ServiceModel;
using FileWatcherUtilities.FileWatcherServiceContract;
#endif

using System;
using System.Globalization;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Logger.Properties;

namespace FileWatcherUtilities.Logger
{
    /// <summary>
    /// Default formatter of log messages.
    /// </summary>
    public class DefaultFormatter : IFormatter
    {
        #region IFormatter Members

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentException">Invalid message type.</exception>
        public string Format(string message, MessageType messageType)
        {
            if (message == null)
            {
                message = String.Empty;
            }

            if (messageType == MessageType.Info)
            {
                return String.Format(CultureInfo.CurrentCulture,
                                     message,
                                     DateTime.Now,
                                     Resources.MessageTypeInfo);
            }
            if (messageType == MessageType.Error)
            {
                return String.Format(CultureInfo.CurrentCulture,
                                     message,
                                     DateTime.Now,
                                     Resources.MessageTypeError);
            }
            if (messageType == MessageType.Warn)
            {
                return String.Format(CultureInfo.CurrentCulture,
                                     message,
                                     DateTime.Now,
                                     Resources.MessageTypeWarn);
            }

            throw new ArgumentException(Resources.ArgumentExceptionMessageType,
                                        "messageType");
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherStoppedEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherStoppedEventArgs is null.</exception>
        public string Format(FileWatcherStoppedEventArgs fileWatcherStoppedEventArgs)
        {
            if (fileWatcherStoppedEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherStoppedEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageDaemonStopped,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @fileWatcherStoppedEventArgs.DaemonName);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherStartedEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherStartedEventArgs is null.</exception>
        public string Format(FileWatcherStartedEventArgs fileWatcherStartedEventArgs)
        {
            if (fileWatcherStartedEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherStartedEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageDaemonStarted,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @fileWatcherStartedEventArgs.DaemonName);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherStartingEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherStartingEventArgs is null.</exception>
        public string Format(FileWatcherStartingEventArgs fileWatcherStartingEventArgs)
        {
            if (fileWatcherStartingEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherStartingEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageDaemonStarting,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @fileWatcherStartingEventArgs.DaemonName);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">processErrorEventArgs is null.</exception>
        public string Format(ProcessErrorEventArgs processErrorEventArgs)
        {
            if (processErrorEventArgs == null)
            {
                throw new ArgumentNullException("processErrorEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageProcessError,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 @processErrorEventArgs.DaemonName,
                                 @processErrorEventArgs.Exception.Message);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processExitEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">processExitEventArgs is null.</exception>
        public string Format(ProcessExitEventArgs processExitEventArgs)
        {
            if (processExitEventArgs == null)
            {
                throw new ArgumentNullException("processExitEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageProcessExited,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @processExitEventArgs.ProcessId,
                                 @processExitEventArgs.DaemonName,
                                 @processExitEventArgs.ExitCode);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="controllerProcessStartedEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">processStartedEventArgs is null.</exception>
        public string Format(ControllerProcessStartedEventArgs controllerProcessStartedEventArgs)
        {
            if (controllerProcessStartedEventArgs == null)
            {
                throw new ArgumentNullException("controllerProcessStartedEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageProcessStarted,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @controllerProcessStartedEventArgs.DaemonName,
                                 @controllerProcessStartedEventArgs.FileName,
                                 @controllerProcessStartedEventArgs.ProcessId,
                                 @controllerProcessStartedEventArgs.Arguments,
                                 @controllerProcessStartedEventArgs.Verb,
                                 @controllerProcessStartedEventArgs.QueuedProcesses.ToString(NumberFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processDataEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">processDataEventArgs is null.</exception>
        public string FormatProcessErrorData(ProcessDataEventArgs processDataEventArgs)
        {
            if (processDataEventArgs == null)
            {
                throw new ArgumentNullException("processDataEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageProcessErrorData,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 @processDataEventArgs.DaemonName,
                                 @processDataEventArgs.Data);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processDataEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">processDataEventArgs is null.</exception>
        public string FormatProcessOutputData(ProcessDataEventArgs processDataEventArgs)
        {
            if (processDataEventArgs == null)
            {
                throw new ArgumentNullException("processDataEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageProcessOutputData,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @processDataEventArgs.DaemonName,
                                 @processDataEventArgs.Data);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherEventArgs is null.</exception>
        public string Format(FileWatcherEventArgs fileWatcherEventArgs)
        {
            if (fileWatcherEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherEventArgs",
                                                Resources.ArgumentNullException);
            }

            if (String.Compare(fileWatcherEventArgs.ChangeType, Resources.ChangeTypeSystemGenerated, StringComparison.Ordinal) == 0)
            {
                return String.Format(CultureInfo.CurrentCulture,
                                     @Resources.MessageFileSystemChange,
                                     @DateTime.Now,
                                     @Resources.MessageTypeInfo,
                                     @Resources.MessageSystemGenerated,
                                     @fileWatcherEventArgs.FullPath);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageFileSystemChange,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @fileWatcherEventArgs.ChangeType.ToLower(CultureInfo.CurrentCulture),
                                 @fileWatcherEventArgs.FullPath);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherBufferErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherBufferErrorEventArgs is null.</exception>
        public string Format(FileWatcherBufferErrorEventArgs fileWatcherBufferErrorEventArgs)
        {
            if (fileWatcherBufferErrorEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherBufferErrorEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageBufferError,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 @fileWatcherBufferErrorEventArgs.DaemonName,
                                 @fileWatcherBufferErrorEventArgs.ErrorEventArgs.GetException().Message);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherSearchErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherSearchErrorEventArgs is null.</exception>
        public string Format(FileWatcherSearchErrorEventArgs fileWatcherSearchErrorEventArgs)
        {
            if (fileWatcherSearchErrorEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherSearchErrorEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageSearchError,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 @fileWatcherSearchErrorEventArgs.DaemonName,
                                 @fileWatcherSearchErrorEventArgs.Exception.Message);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherSearchProgressEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherSearchProgressEventArgs is null.</exception>
        public string Format(FileWatcherSearchProgressEventArgs fileWatcherSearchProgressEventArgs)
        {
            if (fileWatcherSearchProgressEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherSearchProgressEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageSearchProgess,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @fileWatcherSearchProgressEventArgs.DaemonName,
                                 @fileWatcherSearchProgressEventArgs.DirectoryFullPath);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherPathErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherPathErrorEventArgs is null.</exception>
        public string Format(FileWatcherPathErrorEventArgs fileWatcherPathErrorEventArgs)
        {
            if (fileWatcherPathErrorEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherPathErrorEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageDaemonPathError,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 fileWatcherPathErrorEventArgs.DaemonName,
                                 fileWatcherPathErrorEventArgs.Path);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceCalledEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">serviceCalledEventArgs is null.</exception>
        public string Format(ServiceCalledEventArgs serviceCalledEventArgs)
        {
            if (serviceCalledEventArgs == null)
            {
                throw new ArgumentNullException("serviceCalledEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageServiceCalled,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @serviceCalledEventArgs.DaemonName,
                                 @serviceCalledEventArgs.Id,
                                 @serviceCalledEventArgs.Message);
        }

#if (!_NET_20)

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">serviceErrorEventArgs is null.</exception>
        public string Format(ServiceErrorEventArgs serviceErrorEventArgs)
        {
            if (serviceErrorEventArgs == null)
            {
                throw new ArgumentNullException("serviceErrorEventArgs",
                                                Resources.ArgumentNullException);
            }

            FaultException<DefaultFault> faultException = serviceErrorEventArgs.Exception as FaultException<DefaultFault>;

            // Format service fault.
            if (faultException != null)
            {
                // Check for null.
                if (faultException.Detail != null)
                {
                    return String.Format(CultureInfo.CurrentCulture,
                                         @Resources.MessageServiceError,
                                         @DateTime.Now,
                                         @Resources.MessageTypeError,
                                         @serviceErrorEventArgs.DaemonName,
                                         @serviceErrorEventArgs.Id,
                                         @faultException.Detail.Severity,
                                         @faultException.Detail.ErrorCode,
                                         @faultException.Detail.Message,
                                         @faultException.Detail.Id);
                }
                // Null-data.
                return String.Format(CultureInfo.CurrentCulture,
                                     @Resources.MessageServiceError,
                                     @DateTime.Now,
                                     @Resources.MessageTypeError,
                                     @serviceErrorEventArgs.DaemonName,
                                     @serviceErrorEventArgs.Id,
                                     FileWatcherServiceContract.Properties.Resources.SeverityError,
                                     @Resources.MessageServiceErrorDataIsMissing,
                                     0,
                                     Guid.Empty);
            }
            // Format other exceptions.
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageServiceError,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 @serviceErrorEventArgs.DaemonName,
                                 @serviceErrorEventArgs.Id,
                                 @serviceErrorEventArgs.Exception.Message);
        }

#else

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        public string Format(ServiceErrorEventArgs serviceErrorEventArgs)
        {
            return String.Empty;
        }

#endif

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceBeginCallEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">serviceBeginCallEventArgs is null.</exception>
        public string Format(ServiceBeginCallEventArgs serviceBeginCallEventArgs)
        {
            if (serviceBeginCallEventArgs == null)
            {
                throw new ArgumentNullException("serviceBeginCallEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageServiceBeginCall,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @serviceBeginCallEventArgs.DaemonName,
                                 @serviceBeginCallEventArgs.Id,
                                 @serviceBeginCallEventArgs.QueuedProcesses.ToString(NumberFormatInfo.InvariantInfo));
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceProxyCreationErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">serviceProxyCreationErrorEventArgs is null.</exception>
        public string Format(ServiceProxyCreationErrorEventArgs serviceProxyCreationErrorEventArgs)
        {
            if (serviceProxyCreationErrorEventArgs == null)
            {
                throw new ArgumentNullException("serviceProxyCreationErrorEventArgs",
                                                Resources.ArgumentNullException);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageServiceProxyCreationError,
                                 @DateTime.Now,
                                 @Resources.MessageTypeError,
                                 @serviceProxyCreationErrorEventArgs.DaemonName,
                                 @serviceProxyCreationErrorEventArgs.Id);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processCanceledEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">processCanceledEventArgs is null.</exception>
        public string Format(ProcessCanceledEventArgs processCanceledEventArgs)
        {
            if (processCanceledEventArgs == null)
            {
                throw new ArgumentNullException("processCanceledEventArgs",
                                                Resources.ArgumentNullException);
            }
            if (processCanceledEventArgs.Reason == ProcessCanceledReason.FileWasMissing)
            {
                return String.Format(CultureInfo.CurrentCulture,
                                     @Resources.MessageProcessCanceled,
                                     @DateTime.Now,
                                     @Resources.MessageTypeWarn,
                                     @processCanceledEventArgs.DaemonName,
                                     @Resources.MessageProcessCanceledReasonFileIsMissing,
                                     @processCanceledEventArgs.FullPath);
            }
            if (processCanceledEventArgs.Reason == ProcessCanceledReason.FileLockTestFailed)
            {
                return String.Format(CultureInfo.CurrentCulture,
                                     @Resources.MessageProcessCanceled,
                                     @DateTime.Now,
                                     @Resources.MessageTypeWarn,
                                     @processCanceledEventArgs.DaemonName,
                                     @Resources.MessageProcessCanceledReasonFileLockTestFailed,
                                     @processCanceledEventArgs.FullPath);
            }
            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageProcessCanceled,
                                 @DateTime.Now,
                                 @Resources.MessageTypeWarn,
                                 @processCanceledEventArgs.DaemonName,
                                 @Resources.MessageProcessCanceledReasonRenameFailed,
                                 @processCanceledEventArgs.FullPath);
        }

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherRecycledEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        /// <exception cref="ArgumentNullException">fileWatcherRecycledEventArgs is null.</exception>
        public string Format(FileWatcherRecycledEventArgs fileWatcherRecycledEventArgs)
        {
            if (fileWatcherRecycledEventArgs == null)
            {
                throw new ArgumentNullException("fileWatcherRecycledEventArgs",
                                                Resources.ArgumentNullException);
            }

            string recycleReason = String.Empty;

            if (fileWatcherRecycledEventArgs.Reason == RecycleReason.DirectoryNotFound)
            {
                recycleReason = Resources.MessageFileWatcherRecycleReasonDirectoryNotFound;
            }
            else if (fileWatcherRecycledEventArgs.Reason == RecycleReason.Error)
            {
                recycleReason = Resources.MessageFileWatcherRecycleReasonError;
            }
            else if (fileWatcherRecycledEventArgs.Reason == RecycleReason.Recycle)
            {
                recycleReason = Resources.MessageFileWatcherRecycleReasonRecycle; 
            }

            return String.Format(CultureInfo.CurrentCulture,
                                 @Resources.MessageFileWatcherRecycle,
                                 @DateTime.Now,
                                 @Resources.MessageTypeInfo,
                                 @fileWatcherRecycledEventArgs.DaemonName,
                                 @recycleReason);
        }

        #endregion
    }
}