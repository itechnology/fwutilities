/******************************************************************************
*    File Watcher Utilities / Presenter
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

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides Gnu license.
    /// </summary>
    public class GnuLicense
    {
        /// <summary>
        /// Returns next part of the GNU license.
        /// </summary>
        /// <returns>Next part of the GNU license.</returns>
        public string NextGnuLicensePart()
        {
            string gnuTemp;

            if (_gnu.Length >= ReadSize)
            {
                gnuTemp = _gnu.Substring(0,
                                        ReadSize);

                _gnu = _gnu.Substring(ReadSize);

                int newLine = _gnu.IndexOf("\n\r", StringComparison.Ordinal);

                gnuTemp = gnuTemp +
                          _gnu.Substring(0,
                                        newLine);

                _gnu = _gnu.Substring(newLine);
                return gnuTemp;
            }

            gnuTemp = _gnu;
            _gnu = String.Empty;
            return gnuTemp;
        }

        /// <summary>
        /// Returns true, if the curren part is the last part of the license.
        /// </summary>
        /// <returns>True, if the current part is the last part of the license.</returns>
        public bool IsLastPart()
        {
            if (_gnu.Length >= ReadSize &&
                _gnu.Length != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Contains GNU license text.
        /// </summary>
        private string _gnu = ResourceGNU.COPYING;

        /// <summary>
        /// Read size of the GNU license.
        /// </summary>
        private const int ReadSize = 1000;
    }
}