/******************************************************************************
*    File Watcher Utilities / File Watcher Windows Service
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
using FileWatcherUtilities.Presenter;

namespace FileWatcherUtilities.FileWatcherWindowsService
{
    /// <summary>
    /// Provides console view.
    /// </summary>
    public class WindowsServiceView : IWindowsServiceView
    {
        #region IWindowsServiceView Members

        /// <summary>
        /// Occures when service is started.
        /// </summary>
        public event EventHandler<ServiceStartedEventArgs> ServiceStarted;

        /// <summary>
        /// Occures when service is stopped.
        /// </summary>
        public event EventHandler<ServiceStoppedEventArgs> ServiceStopped;

        #endregion

        /// <summary>
        /// Start service.
        /// </summary>
        /// <param name="args">Service argumets.</param>
        public void Start(string[] args)
        {
            if (ServiceStarted != null)
            {
                ServiceStarted(this,
                               new ServiceStartedEventArgs(args));
            }
        }

        /// <summary>
        /// Stop service.
        /// </summary>
        /// <remarks>This method will blocks until all the processes have exited.</remarks>
        public void Stop()
        {
            if (ServiceStopped != null)
            {
                ServiceStopped(this,
                               new ServiceStoppedEventArgs());
            }
        }
    }
}