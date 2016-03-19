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

using System.ServiceModel;

namespace FileWatcherUtilities.FileWatcherServiceContract
{
    /// <summary>
    /// SystemChanged response message contract.
    /// </summary>
    [MessageContract]
    public class SystemChangedRespMC : IResponse
    {
        /// <summary>
        /// Gets or sets message.
        /// </summary>
        [MessageHeader(MustUnderstand=true)]
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
        /// Message.
        /// </summary>
        private string _message;
    }
}