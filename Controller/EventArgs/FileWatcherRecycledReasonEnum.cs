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

namespace FileWatcherUtilities.Controller
{
    /// <summary>
    /// File system watcher recycled reasons.
    /// </summary>
    public enum RecycleReason
    {
        /// <summary>
        /// Default value.
        /// </summary>
        None = 0,
        /// <summary>
        /// File system watcher was recycled because directory was not found.
        /// </summary>
        DirectoryNotFound = 1,
        /// <summary>
        /// File system watcher was recycled because of an error.
        /// </summary>
        Error = 2,
        /// <summary>
        /// File system watcher was recycled.
        /// </summary>
        Recycle = 3
    }
}