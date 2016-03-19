/**********************************************************************************
*   File Watcher Utilities / File Watcher Service Contract
*   Copyright (c) 2006-2013 Jussi Hiltunen
*
*   This library is free software; you can redistribute it and/or
*   modify it under the terms of the GNU Lesser General Public
*   License as published by the Free Software Foundation; either
*   version 2.1 of the License, or (at your option) any later version.
*
*   This library is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
*   Lesser General Public License for more details.
*
*   You should have received a copy of the GNU Lesser General Public
*   License along with this library; if not, write to the Free Software
*   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
**********************************************************************************/

using System;
using System.ServiceModel;
using System.Security.Permissions;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.FileWatcherServiceContract
{
    /// <summary>
    /// Defines the file watcher service contract to be used in communication. 
    /// </summary>
    [ServiceContract]
    public interface IFileWatcherService
    {
        /// <summary>
        /// Called when system is changed.
        /// </summary>
        /// <param name="request">Infomation about the system change.</param>
        /// <returns>Information about the handling of the system change.</returns>
        /// <remarks>This method can fail fail by throwing an DefaultFault exception.</remarks>
        [OperationContract]
        [FaultContract(typeof(DefaultFault))]
        SystemChangedRespDC SystemChanged(SystemChangedReqDC request);
    }
}