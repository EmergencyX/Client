using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace EmergencyX_Client
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		public MainWindow()
		{
			InitializeComponent();			
		}

		private void settingsClick(object sender, RoutedEventArgs e)
		{
			Settings emergencyXSettings = new Settings();
			emergencyXSettings.Show();
		}

		private void mainWindowLoaded(object sender, RoutedEventArgs e)
		{

			//if Emergency 5 is installed and no mods are installed the text "Emergency is installed" should be displayed
			//if its insatlled and there are mods the mods should be displayed 
			//otherwise there should be the text "Emergency is not installed"
			//
			EmergencyInstallation myEmergencyInstallation = new EmergencyInstallation();
			string appDataModificationsJsonFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Promotion Software GmbH\EMERGENCY 5\mods\mods_user_settings.json";
			if(File.Exists(appDataModificationsJsonFile))
			{ 
				MessageBox.Show(appDataModificationsJsonFile);
			}

			if (EmergencyInstallation.getIsEmergencyInstalled() && myEmergencyInstallation.verifyEmergencyInstallation(myEmergencyInstallation.getEmergencyInstallationPath()))
			{
				ModTools modTools = new ModTools(appDataModificationsJsonFile);
				liModListBox.Visibility = Visibility.Visible;

				Dictionary<int, string> modifications = modTools.getModifications();

				foreach (var key in modifications)
				{
					ListBoxItem item = new ListBoxItem();
					item.Content = key.Value;
					liModListBox.Items.Add(item);
				}

			}
			else
			{
				lblIsEmergencyInstalled.Content = Properties.Resources.emergencyIsNotInstalled;
				liModListBox.IsEnabled = false;
				liModListBox.Visibility = Visibility.Hidden;

			}
		}
	}
}
