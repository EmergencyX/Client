using System;
using System.Reflection;
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

		private string oldEmergencyPath { get; set; }

		public Settings()
		{
			InitializeComponent();
			tbxEmergencyPath.Text = AppConfig.readFromAppConfig("emergencyInstallationPath");
			this.oldEmergencyPath = tbxEmergencyPath.Text;
			
			#if DEBUG
				tbxEmergencyPath.Text = @"D:\Program Files (x86)\Emergency 5";
				this.oldEmergencyPath = @"D:\Program Files (x86)\Emergency 5";
			#endif

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
			if (rbBrotli.IsChecked == true)
			{
				compressionToConfig = "brotli";
			}
			else
			{
				compressionToConfig = "zip";
			}
			#endregion

			bool settingUseZipOrBrotli = AppConfig.writeToAppConfig("compressionAlgorithm", compressionToConfig);


			// if the Installation path of emergency has changed our Emergency X has to be restarted (for reasons...)
			//

			if (oldEmergencyPath != tbxEmergencyPath.Text)
			{
				// Inform the user and restart the Application
				//
				MessageBox.Show(Properties.Resources.clientRestartNeeded, Properties.Resources.clientRestartNeededTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk);
				System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
				Application.Current.Shutdown();
			}
			else
			{
				// After clean up close the window
				//
				Settings.GetWindow(SettingsWindow).Close();
			}
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
