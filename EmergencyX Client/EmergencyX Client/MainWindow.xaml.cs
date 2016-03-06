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

			//if Emergency 5 is installed and no mods are installed the text "Emergency is installed" should be displayed
			//if its insatlled and there are mods the mods should be displayed 
			//otherwise there should be the text "Emergency is not installed"
			//

			EmergencyInstallation.setEmergencyInstallationPath(AppConfig.readFromAppConfig("emergencyInstallationPath"));

			if(EmergencyInstallation.getIsEmergenyInstalled() == true) {
				isEmergencyInstalled.Content = Properties.Resources.emergencyIsInstalled;
			} else {
				isEmergencyInstalled.Content = Properties.Resources.emergencyIsNotInstalled;
			}
			
		}

		private void settingsClick(object sender, RoutedEventArgs e)
		{
			Settings emergencyXSettings = new Settings();
			emergencyXSettings.Show();
		}
	}
}
