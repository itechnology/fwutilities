/**********************************************************************************
*   File Watcher Utilities / File Watcher Service Contract
*    Copyright (c) 2006-2013 Jussi Hiltunen
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
using System.Runtime.Serialization;
using FileWatcherUtilities.FileWatcherServiceContract.Properties;

namespace FileWatcherUtilities.FileWatcherServiceContract
{
    /// <summary>
    /// Default fault datacontract.
    /// </summary>
    [DataContract]
    public class DefaultFault : IExtensibleDataObject
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

        /// <summary>
        /// Gets or sets error code.
        /// </summary>
        [DataMember]
        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        /// <summary>
        /// Gets or sets severity.
        /// </summary>
        [DataMember]
        public string Severity
        {
            get
            {
                return _severity;
            }
            set
            {
                _severity = value;
            }
        }

        /// <summary>
        /// Gets or sets fault identifier.
        /// </summary>
        [DataMember]
        public Guid Id
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
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
        /// Identifies fault.
        /// </summary>
        private Guid _guid;

        /// <summary>
        /// Message.
        /// </summary>
        private string _message = String.Empty;

        /// <summary>
        /// Severity.
        /// </summary>
        private string _severity = Resources.SeverityError;

        /// <summary>
        /// Error code.
        /// </summary>
        private string _errorCode = String.Empty;
    }
}