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
using FileWatcherUtilities.Options;
using FileWatcherUtilities.Controller;
using FileWatcherUtilities.Presenter.Properties;

namespace FileWatcherUtilities.Presenter
{
    /// <summary>
    /// Provides options view presenter.
    /// </summary>
    public class OptionsViewPresenter
    {
        /// <summary>
        /// Initializes a new instance of the OptionsViewPresenter class.
        /// </summary>
        /// <param name="optionsView">Options view.</param>
        /// <param name="mainView">Main view.</param>
        /// <param name="fileWatcherController">FileWatcherController.</param>
        /// <param name="applicationOptionsController">ApplicationOptionsController.</param>
        /// <exception cref="ArgumentNullException">fileWatcherController is null.</exception>
        public OptionsViewPresenter(IOptionsView optionsView,
                                    IMainView mainView,
                                    FileWatcherController fileWatcherController,
                                    ApplicationOptionsController applicationOptionsController)
        {
            if (mainView == null)
            {
                throw new ArgumentNullException("mainView",
                                                Resources.ArgumentNullException);
            }
            if (optionsView == null)
            {
                throw new ArgumentNullException("optionsView",
                                                Resources.ArgumentNullException);
            }
            if (fileWatcherController == null)
            {
                throw new ArgumentNullException("fileWatcherController",
                                                Resources.ArgumentNullException);
            }
            if (applicationOptionsController == null)
            {
                throw new ArgumentNullException("applicationOptionsController",
                                                Resources.ArgumentNullException);
            }

            _optionsView = optionsView;
            _mainView = mainView;
            _fileWatcherController = fileWatcherController;
            _applicationOptionsController = applicationOptionsController;

            SubscribeToMainViewEvents();
            SubscribeToOptionsViewEvents();
        }

        /// <summary>
        /// Subscribes to main view events.
        /// </summary>
        private void SubscribeToMainViewEvents()
        {
            _mainView.ViewOptions += 
                new EventHandler<EventArgs>(OnViewOptions);
        }

        /// <summary>
        /// Subscribes to options view events.
        /// </summary>
        private void SubscribeToOptionsViewEvents()
        {
            _optionsView.Save += 
                new EventHandler<EventArgs>(OnSave);
        }

        /// <summary>
        /// Handles save event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnSave(object sender, 
                            EventArgs e)
        {
            // Save application options.
            _applicationOptionsController.Save(_optionsView.NewApplicationOptions());

            // Hide view.
            _optionsView.HideView();
        }

        /// <summary>
        /// Handles view optiosn.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void OnViewOptions(object sender, 
                                   EventArgs e)
        {
            _optionsView.SynchronousExecutionEnabled = !_fileWatcherController.IsActive();
            _optionsView.ViewApplicationOptions(_applicationOptionsController.ApplicationOptions);
            _optionsView.ShowView();
        }

        /// <summary>
        /// Application options controller.
        /// </summary>
        private readonly ApplicationOptionsController _applicationOptionsController;

        /// <summary>
        /// File watcher controller.
        /// </summary>
        private readonly FileWatcherController _fileWatcherController;

        /// <summary>
        /// Options view.
        /// </summary>
        private readonly IOptionsView _optionsView;

        /// <summary>
        /// Main view.
        /// </summary>
        private readonly IMainView _mainView;
    }
}