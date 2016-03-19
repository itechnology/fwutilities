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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.ServiceModel;
using System.Globalization;
using System.Collections.Generic;
using System.Security.Cryptography;
using FileWatcherUtilities.FileWatcherServiceContract;

namespace FileWatcherUtilities.SampleFileWatcherStreamingService
{
    /// <summary>
    /// Implements the IFileWatcherStreamingService service contract.
    /// </summary>
    public class FileWatcherStreamingService : IFileWatcherStreamingService
    {
        #region IFileWatcherStreamingService Members

        /// <summary>
        /// Called when system is changed.
        /// </summary>
        /// <param name="request">Infomation about the system change.</param>
        /// <returns>Information about the handling of the system change.</returns>
        /// <remarks>This method can fail fail by throwing an DefaultFault exception.</remarks>
        [DebuggerStepThrough]
        public SystemChangedRespMC SystemChangedStreaming(SystemChangedReqMC request)
        {
            const string StatusReceiving = "Receiving";
            const string StatusCompleted = "Completed";
            const string StatusFailed = "Failed   ";

            try
            {
                AddProgressBar(request.Id);

                using (FileStream createdFile = File.Create(Environment.CurrentDirectory + @"\" + Path.GetFileName(request.FullPath)))
                {
                    const int Length = 4096;
                    byte[] buffer = new byte[Length];
                    int bytesRead = request.FileStream.Read(buffer, 0, Length);
                    long totalBytesRead = bytesRead;

                    while (bytesRead > 0)
                    {
                        DrawFileProgressBar(GetProgressBarRow(request.Id), Path.GetFileName(request.FullPath), (int)totalBytesRead, (int)request.FileStreamLength, StatusReceiving);
                        createdFile.Write(buffer, 0, bytesRead);
                        bytesRead = request.FileStream.Read(buffer, 0, Length);
                        totalBytesRead = totalBytesRead + bytesRead;
                    }

                    request.FileStream.Close();
                }

                if (request.Checksum == CalculateMD5Sum(Environment.CurrentDirectory + @"\" + Path.GetFileName(request.FullPath)))
                {
                    DrawFileProgressBar(GetProgressBarRow(request.Id), Path.GetFileName(request.FullPath), (int)request.FileStreamLength, (int)request.FileStreamLength, StatusCompleted);
                }
                else
                {
                    DrawFileProgressBar(GetProgressBarRow(request.Id), Path.GetFileName(request.FullPath), (int)request.FileStreamLength, (int)request.FileStreamLength, StatusFailed);
                }

                RemoveProgressBar(request.Id);

                SystemChangedRespMC systemChangedRespMC = new SystemChangedRespMC();
                systemChangedRespMC.Message = "Request handled.";

                return systemChangedRespMC;
            }
            catch (Exception ex)
            {
                int row;
                if (TryGetProgressBarRow(request.Id, out row))
                {
                    DrawFileProgressBar(row, Path.GetFileName(request.FullPath), (int)request.FileStreamLength, (int)request.FileStreamLength, StatusFailed);
                    RemoveProgressBar(request.Id);
                }

                DefaultFault defaultFault = new DefaultFault();
                defaultFault.Message = ex.Message;
                defaultFault.Id = Guid.NewGuid();

                throw new FaultException<DefaultFault>(defaultFault);
            }
        }

        #endregion

        /// <summary>
        /// Calculates MD5sum.
        /// </summary>
        /// <param name="fullPath">Path of the file.</param>
        /// <returns>MD5sum.</returns>
        private static string CalculateMD5Sum(string fullPath)
        {
            using (FileStream fileStream = File.OpenRead(fullPath))
            {
                byte[] output;

                using (MD5 md5 = MD5.Create())
                {
                    output = md5.ComputeHash(fileStream);
                }

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < output.Length; i++)
                {
                    stringBuilder.Append(output[i].ToString("x2", CultureInfo.InvariantCulture));
                }

                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Draws progess bar to console.
        /// </summary>
        /// <param name="index">Row index.</param>
        /// <param name="fileName">File name.</param>
        /// <param name="value">Current value of the progess bar.</param>
        /// <param name="maxValue">Maximum value of the progess bar.</param>
        /// <param name="status">File status.</param>
        private static void DrawFileProgressBar(int index,
                                                string fileName,
                                                int value,
                                                int maxValue,
                                                string status)
        {
            const int MaxFileNameLength = 45;
            const string ProgressBarStart = "[";
            const string ProgressBarEnd = "]";
            const double ProgressBarSteps = 10.0d;
            const string FileNameEnd = "... ";
            const string Space = " ";
            const string PercentageString = "100.00%";
            const string PercentageFormat = "{0:.00}%";

            lock (_lockProgressBarDraw)
            {
                Console.CursorTop = index;

                // Reset cursor position.
                Console.CursorLeft = 0;

                // Print file name.
                if (fileName.Length > MaxFileNameLength - Space.Length)
                {
                    Console.Write(fileName.Remove(MaxFileNameLength - FileNameEnd.Length) + FileNameEnd);
                }
                else
                {
                    Console.Write(fileName);
                }

                // Begin progress bar.
                Console.CursorLeft = MaxFileNameLength;
                Console.Write(ProgressBarStart);

                // Calculate current progress bar value.
                int currentProgressBarValue = (int)Math.Truncate((((double)value / maxValue) * ProgressBarSteps));

                // Set cursor position to inside progress bar.
                int cursorPosition = MaxFileNameLength + ProgressBarStart.Length;

                // Set background color to some visible color.
                Console.BackgroundColor = ConsoleColor.Gray;

                for (int i = 0; i < currentProgressBarValue; i++)
                {
                    Console.CursorLeft = cursorPosition++;
                    Console.Write(Space);
                }

                // Reset background color.
                Console.BackgroundColor = ConsoleColor.Black;

                // End progress bar.
                Console.CursorLeft = MaxFileNameLength + ProgressBarStart.Length + (int)ProgressBarSteps;
                Console.Write(ProgressBarEnd);

                // Calculate percentage.
                Console.CursorLeft = MaxFileNameLength + ProgressBarStart.Length + (int)ProgressBarSteps + ProgressBarEnd.Length + Space.Length;
                Console.Write(PercentageFormat, ((double)value / maxValue) * 100);
                Console.CursorLeft = MaxFileNameLength + ProgressBarStart.Length + (int)ProgressBarSteps + ProgressBarEnd.Length + Space.Length + PercentageString.Length + Space.Length + Space.Length;
                Console.Write(status);
            }
        }

        /// <summary>
        /// Adds progess bar.
        /// </summary>
        /// <param name="id">Id of the progess bar.</param>
        /// <exception cref="ArgumentException">Id is invalid.</exception>
        private static void AddProgressBar(Guid id)
        {
            lock (_lockProgressBarRows)
            {
                if (_progressBarRows.ContainsKey(id))
                {
                    throw new ArgumentException("Invalid id.");
                }

                _progressBarRows.Add(id, _rowCount);
                _rowCount = _rowCount + 1;
            }
        }

        /// <summary>
        /// Removes progess bar.
        /// </summary>
        /// <param name="id">Id of the progess bar.</param>
        /// <exception cref="ArgumentException">Id is invalid.</exception>
        private static void RemoveProgressBar(Guid id)
        {
            lock (_lockProgressBarRows)
            {
                if (_progressBarRows.ContainsKey(id))
                {
                    _progressBarRows.Remove(id);
                }
            }
        }

        /// <summary>
        /// Retuns progress bar row by id.
        /// </summary>
        /// <param name="id">Id of the progess bar.</param>
        /// <returns>Row number.</returns>
        private static int GetProgressBarRow(Guid id)
        {
            int row;

            lock (_lockProgressBarRows)
            {
                row = _progressBarRows[id];
            }

            return row;
        }

        /// <summary>
        /// Retuns progress bar row by id.
        /// </summary>
        /// <param name="id">Id of the progess bar.</param>
        /// <param name="row">Row number</param>
        /// <returns>True if row is found.</returns>
        private static bool TryGetProgressBarRow(Guid id, out int row)
        {
            row = -1;

            lock (_lockProgressBarRows)
            {
                if (_progressBarRows.ContainsKey(id))
                {
                    row = _progressBarRows[id];
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Contains progess bars by row.
        /// </summary>
        private static readonly Dictionary<Guid, int> _progressBarRows = new Dictionary<Guid, int>();

        /// <summary>
        /// Lock object for progress bar rows.
        /// </summary>
        private static readonly object _lockProgressBarRows = new object();

        /// <summary>
        /// Console rows.
        /// </summary>
        private static int _rowCount = 3;

        /// <summary>
        /// Lock object for progess bar draw.
        /// </summary>
        private static readonly object _lockProgressBarDraw = new object();
    }
}