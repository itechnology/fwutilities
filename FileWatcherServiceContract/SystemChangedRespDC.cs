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

using System.Runtime.Serialization;

namespace FileWatcherUtilities.FileWatcherServiceContract
{
    /// <summary>
    /// SystemChanged response data contract.
    /// </summary>
    [DataContract]
    public class SystemChangedRespDC : IExtensibleDataObject, IResponse
    {
        /// <summary>
        /// Gets or sets message.
        /// </summary>
        [DataMember]
        public string Message 
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
            }
        }

        #region IExtensibleDataObject Members

        /// <summary>
        /// Gets or sets extension data.
        /// </summary>
        public ExtensionDataObject ExtensionData
        {
            get
            {
                return _extensionDataObject;
            }
            set
            {
                _extensionDataObject = value;
            }
        }

        #endregion

        /// <summary>
        /// Contains extension data.
        /// </summary>
        private ExtensionDataObject _extensionDataObject;

        /// <summary>
        /// Message.
        /// </summary>
        private string _message;
    }
}