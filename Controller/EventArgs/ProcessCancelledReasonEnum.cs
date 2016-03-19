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
    /// Process canceled reasons.
    /// </summary>
    public enum ProcessCanceledReason
    {
        /// <summary>
        /// File lock test failed.
        /// </summary>
        FileLockTestFailed,
        /// <summary>
        /// Renaming of the fail failed.
        /// </summary>
        RenameFailed,
        /// <summary>
        /// File did't exist.
        /// </summary>
        FileWasMissing
    }
}