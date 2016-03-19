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

using FileWatcherUtilities.Controller;

namespace FileWatcherUtilities.Logger
{
    public interface IFormatter
    {
        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="messageType">Type of the message.</param>
        /// <returns>Formatted message.</returns>
        string Format(string message, MessageType messageType);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherStoppedEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherStoppedEventArgs fileWatcherStoppedEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherStartedEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherStartedEventArgs fileWatcherStartedEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherStartingEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherStartingEventArgs fileWatcherStartingEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ProcessErrorEventArgs processErrorEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processExitEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ProcessExitEventArgs processExitEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="controllerProcessStartedEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ControllerProcessStartedEventArgs controllerProcessStartedEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processDataEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string FormatProcessErrorData(ProcessDataEventArgs processDataEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processDataEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string FormatProcessOutputData(ProcessDataEventArgs processDataEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherEventArgs fileWatcherEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherBufferErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherBufferErrorEventArgs fileWatcherBufferErrorEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherSearchErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherSearchErrorEventArgs fileWatcherSearchErrorEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherSearchProgressEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherSearchProgressEventArgs fileWatcherSearchProgressEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherPathErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherPathErrorEventArgs fileWatcherPathErrorEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceCalledEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ServiceCalledEventArgs serviceCalledEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ServiceErrorEventArgs serviceErrorEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceBeginCallEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ServiceBeginCallEventArgs serviceBeginCallEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="serviceProxyCreationErrorEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ServiceProxyCreationErrorEventArgs serviceProxyCreationErrorEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="processCanceledEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(ProcessCanceledEventArgs processCanceledEventArgs);

        /// <summary>
        /// Formats message.
        /// </summary>
        /// <param name="fileWatcherRecycledEventArgs">Message.</param>
        /// <returns>Formatted message.</returns>
        string Format(FileWatcherRecycledEventArgs fileWatcherRecycledEventArgs);
    }
}