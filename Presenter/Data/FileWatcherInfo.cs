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

using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Holds file watcher infomation.
    /// </summary>
    public class FileWatcherInfo
    {
        /// <summary>
        /// Gets or sets file watcher statuts.
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        /// <summary>
        /// Gets or sets number of events.
        /// </summary>
        public int Events
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
            }
        }

        /// <summary>
        /// Gets or sets last event type.
        /// </summary>
        public string LastEventType
        {
            get
            {
                return _lastEventType;
            }
            set
            {
                _lastEventType = value;
            }
        }

        /// <summary>
        /// Gets or sets last event time.
        /// </summary>
        public string LastEventTime
        {
            get
            {
                return _lastEventTime;
            }
            set
            {
                _lastEventTime = value;
            }
        }

        /// <summary>
        /// Gets or sets number of errors.
        /// </summary>
        public int Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
            }
        }

        /// <summary>
        /// Gets or sets file watcher enabled.
        /// </summary>
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        /// <summary>
        /// Resets info.
        /// </summary>
        public void Reset()
        {
            _status = Resources.StateStopped;
            Events = 0;
            LastEventType = Resources.LastEventTypeNone;
            LastEventTime = Resources.LastEventTimeNone;
            Errors = 0;
        }

        /// <summary>
        /// Status of file watcher.
        /// </summary>
        private string _status = Resources.StateStopped;

        /// <summary>
        /// Number of events.
        /// </summary>
        private int _events;

        /// <summary>
        /// Last event type. Default is 'None'.
        /// </summary>
        private string _lastEventType = Resources.LastEventTypeNone;

        /// <summary>
        /// Last event time. Default is 'None'.
        /// </summary>
        private string _lastEventTime = Resources.LastEventTimeNone;
        
        /// <summary>
        /// True if file watcher is enabled.
        /// </summary>
        private bool _enabled;

        /// <summary>
        /// Number of errors.
        /// </summary>
        private int _errors;
    }
}