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
using System.ServiceModel;
using System.Security.Permissions;
using System.ServiceModel.Description;
using FileWatcherUtilities.FileWatcherServiceContract;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.SampleFileWatcherService
{
    /// <summary>
    /// Sample host.
    /// </summary>
    class Program
    {
        static void Main()
        {
            Uri baseAddress = new Uri("http://localhost:8000/SampleFileWatcherService");

            using (ServiceHost serviceHost = new ServiceHost(typeof(FileWatcherService), baseAddress))
            {
                try
                {
                    serviceHost.AddServiceEndpoint(typeof(IFileWatcherService),
                                                   new WSHttpBinding(),
                                                   "SampleFileWatcherService");

                    ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
                    serviceMetadataBehavior.HttpGetEnabled = true;                   
                    serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);

                    serviceHost.Open();

                    Console.CursorVisible = false;
                    Console.WriteLine(Properties.Resources.MessageServiceIsReady);
                    Console.WriteLine(Properties.Resources.MessagePressEnterToTerminate);
                    Console.ReadLine();
                    
                    serviceHost.Close();
                }
                catch (TimeoutException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
                catch (CommunicationException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
        }
    }
}