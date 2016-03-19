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

#if (!_NET_20)

using System;
using System.IO;
using System.ServiceModel;
using FileWatcherUtilities.FileWatcherServiceContract;

namespace FileWatcherUtilities.Controller
{
    internal sealed class FileWatcherStreamingServiceProxy : ClientBase<IFileWatcherStreamingService>, IFileWatcherStreamingService
    {
        public FileWatcherStreamingServiceProxy()
        {
        }

        public FileWatcherStreamingServiceProxy(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public FileWatcherStreamingServiceProxy(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public FileWatcherStreamingServiceProxy(string endpointConfigurationName, EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public FileWatcherStreamingServiceProxy(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        SystemChangedRespMC IFileWatcherStreamingService.SystemChangedStreaming(SystemChangedReqMC request)
        {
            return Channel.SystemChangedStreaming(request);
        }

        public string SystemChangedStreaming(string changeType, string checksum, string daemonName, DateTime dateTime, string fileName, long fileStreamLength, string fullPath, Guid id, string machineName, string oldFullPath, Stream fileStream)
        {
            SystemChangedReqMC inValue = new SystemChangedReqMC();
            inValue.ChangeType = changeType;
            inValue.Checksum = checksum;
            inValue.DaemonName = daemonName;
            inValue.DateTime = dateTime;
            inValue.FileName = fileName;
            inValue.FileStreamLength = fileStreamLength;
            inValue.FullPath = fullPath;
            inValue.Id = id;
            inValue.MachineName = machineName;
            inValue.OldFullPath = oldFullPath;
            inValue.FileStream = fileStream;
            SystemChangedRespMC retVal = ((IFileWatcherStreamingService)(this)).SystemChangedStreaming(inValue);
            return retVal.Message;
        }
    }
}

#endif