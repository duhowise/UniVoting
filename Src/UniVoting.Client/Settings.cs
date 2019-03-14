// ***********************************************************************
// Assembly         : UniVoting.Client
// Author           : Duho
// Created          : 09-15-2018
//
// Last Modified By : Duho
// Last Modified On : 03-09-2019
// ***********************************************************************
// <copyright file="Settings.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace UniVoting.Client {


    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a ElectionConfiguration's value is changed.
    //  The PropertyChanged event is raised after a ElectionConfiguration's value is changed.
    //  The SettingsLoaded event is raised after the ElectionConfiguration values are loaded.
    //  The SettingsSaving event is raised before the ElectionConfiguration values are saved.
    /// <summary>
    /// Class Settings. This class cannot be inherited.
    /// </summary>
    internal sealed partial class Settings {

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //
        }

        /// <summary>
        /// Settings the changing event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Configuration.SettingChangingEventArgs"/> instance containing the event data.</param>
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }

        /// <summary>
        /// Settingses the saving event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
