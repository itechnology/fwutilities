/******************************************************************************
*    File Watcher Utilities / File Watcher Simple
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
using System.IO;
using System.Xml;
using System.Threading;
using System.Windows.Forms;
using System.Security.Permissions;
using FileWatcherUtilities.Presenter;
using FileWatcherUtilities.FileWatcherSimple.Properties;

[assembly: CLSCompliant(true)]

namespace FileWatcherUtilities.FileWatcherSimple
{
    /// <summary>
    /// Application class.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Subscribe to thread exception.
            Application.ThreadException +=
                new ThreadExceptionEventHandler(ApplicationThreadException);

            try
            {
                Application.Run(BuildApplicationForms());
            }
            catch (Exception ex) // Some unexpected error.
            {
                MessageBox.Show(Resources.MessageUnexpectedError +
                                ex.Message,
                                Resources.MessageBoxCaptionApplication,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                0);
            }
            finally
            {
                Dispose();
            }
        }

        /// <summary>
        /// Builds application forms.
        /// </summary>
        /// <returns>Main form.</returns>
        private static FormMain BuildApplicationForms()
        {
            _formMain = new FormMain();
            _formOptions = new FormOptions();
            _formProperties = new FormProperties();

            _presenterBuilder = new PresenterBuilder(_formMain,
                                                     _formMain,
                                                     _formOptions,
                                                     _formProperties);
             
            try
            {
                // Builds presenters and applies configuration.
                _presenterBuilder.Build();
            }
            catch (InvalidDataException ex)
            {
                MessageBox.Show(ex.Message,
                                Resources.MessageBoxCaptionApplication,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                0);
                Dispose();
                Environment.Exit(255);
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message,
                                Resources.MessageBoxCaptionApplication,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error,
                                MessageBoxDefaultButton.Button1,
                                0);
                Dispose();
                Environment.Exit(255);
            }

            return _formMain;
        }

        /// <summary>
        /// Thread exception event handler.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event data.</param>
        private static void ApplicationThreadException(object sender,
                                                       ThreadExceptionEventArgs e)
        {
            MessageBox.Show(Resources.MessageUnexpectedError +
                            e.Exception.Message,
                            Resources.MessageBoxCaptionApplication,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1,
                            0);
            Dispose();
            Environment.Exit(255);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        private static void Dispose()
        {
            if (_presenterBuilder != null)
            {
                _presenterBuilder.Dispose();
                _presenterBuilder = null;
            }
            if (_formMain != null)
            {
                _formMain.Dispose();
                _formMain = null;
            }
            if (_formOptions != null)
            {
                _formOptions.Dispose();
                _formOptions = null;
            }
            if (_formProperties != null)
            {
                _formProperties.Dispose();
                _formProperties = null;
            }
        }

        /// <summary>
        /// Presenter builder.
        /// </summary>
        private static PresenterBuilder _presenterBuilder;

        /// <summary>
        /// Form main.
        /// </summary>
        private static FormMain _formMain;

        /// <summary>
        /// Form options.
        /// </summary>
        private static FormOptions _formOptions;

        /// <summary>
        /// Form properties.
        /// </summary>
        private static FormProperties _formProperties;
    }
}