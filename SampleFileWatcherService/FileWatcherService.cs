/******************************************************************************
*    File Watcher Utilities / Sample File Watcher Service
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
using System.Diagnostics;
using FileWatcherUtilities.FileWatcherServiceContract;

namespace FileWatcherUtilities.SampleFileWatcherService
{
    /// <summary>
    /// Implements the IFileWatcherService service contract.
    /// </summary>
    public class FileWatcherService : IFileWatcherService
    {
        #region IFileWatcherService Members

        /// <summary>
        /// Called when system is changed.
        /// </summary>
        /// <param name="request">Infomation about the system change.</param>
        /// <returns>Information about the handling of the system change.</returns>
        /// <remarks>This method can fail fail by throwing an DefaultFault exception.</remarks>
        [DebuggerStepThrough]
        public SystemChangedRespDC SystemChanged(SystemChangedReqDC request)
        {
            SystemChangedRespDC systemChangedRespDC = new SystemChangedRespDC();

            Console.WriteLine(Properties.Resources.MessageHandlingRequest,
                              request.Id,
                              request.DaemonName);
            systemChangedRespDC.Message = "Request handled.";
            return systemChangedRespDC;
        }

        #endregion 
    }
}