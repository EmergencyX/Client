﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace EmergencyX_Client
{
	/// <summary>
	/// Interaktionslogik für Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		public Settings()
		{
			InitializeComponent();
			tbxEmergencyPath.Text = AppConfig.readFromAppConfig("emergencyInstallationPath");

			#region ZipOrBrotliMode
			switch (AppConfig.readFromAppConfig("compressionAlgorithm"))
			{
				case "zip":
					rbZip.IsChecked = true;
					rbBrotli.IsChecked = false;
					break;
				case "brotli":
					rbBrotli.IsChecked = true;
					rbZip.IsChecked = false;
					break;
				default:
					rbZip.IsChecked = true;
					rbBrotli.IsChecked = false;
					break;
			}
			#endregion
		}

		/// <summary>
		/// Saves the setting values
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void settingsOnClick(object sender, RoutedEventArgs e)
		{
			//if settings are saved save the settings to our app.config
			//

			// Storeing the Emergency Installation Path
			// 
			bool emergencyInstallationPathSaved = AppConfig.writeToAppConfig("emergencyInstallationPath", tbxEmergencyPath.Text);

			// Storing the Compression Mode, zip or brotli
			//
			string compressionToConfig;
			#region ZipOrBrotliMode
			if(rbBrotli.IsChecked == true)
			{
				compressionToConfig = "brotli";
			}
			else
			{
				compressionToConfig = "zip";
			}
			#endregion

			bool settingUseZipOrBrotli = AppConfig.writeToAppConfig("compressionAlgorithm", compressionToConfig);

			Settings.GetWindow(SettingsWindow).Close();
		}

		/// <summary>
		/// Handels the Click on the "Cancel" button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnCancelSettingsClick(object sender, RoutedEventArgs e)
		{
			EmergencyInstallation emergency = new EmergencyInstallation();
			emergency.setEmergencyInstallationPath(AppConfig.readFromAppConfig("emergencyInstallationPath"));
			Close();
		}

	}
}
