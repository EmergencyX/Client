using System;
using System.Collections.Generic;
using EmergencyX.Emergency5.Modifications;
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

		public List<InstalledMod> InstalledMods { get; set; }

		public MainWindow()
		{
			InitializeComponent();

		}

		private void settingsClick(object sender, RoutedEventArgs e)
		{
			Settings emergencyXSettings = new Settings();
			emergencyXSettings.Show();
		}

		//some functions in here work better if Loaded Event is used.. Dont know why...
		private void mainWindowLoaded(object sender, RoutedEventArgs e)
		{
			updateSpecificWindowData();
		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		#region updateSpecificWindowData
		private void updateSpecificWindowData()
		{
			//Read Text from description in AssemblyInfo.cs and display it while app is running
			Version.Text = ((System.Reflection.AssemblyDescriptionAttribute)System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false)[0]).Description;

			//if Emergency 5 is installed and no mods are installed the text "Emergency is installed" should be displayed
			//if its insatlled and there are mods the mods should be displayed 
			//otherwise there should be the text "Emergency is not installed"
			//

			EmergencyInstallation myEmergencyInstallation = new EmergencyInstallation();

			//holds the full path to appdata mod 
			string appDataModificationsJsonFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Promotion Software GmbH\EMERGENCY 5\mods\mods_user_settings.json";

			//if somewhere (somehow) an emergency installation is found and this installation could been verified
			if (EmergencyInstallation.getIsEmergencyInstalled() && myEmergencyInstallation.verifyEmergencyInstallation(myEmergencyInstallation.getEmergencyInstallationPath()))
			{
				ModTools mainWindowModTools = new ModTools(appDataModificationsJsonFile);
				InstalledMods = mainWindowModTools.getInstalledModifications();

				DataContext = this;

			}
			else
			{
				// if not display the text "Emergency is not installed" and dis able the list
				lblIsEmergencyInstalled.Content = Properties.Resources.emergencyIsNotInstalled;

			}
		}
		#endregion updateSpecificWindowData

		private void VisibilityChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			updateSpecificWindowData();
		}

		private void ModListContainerMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
				ModListContainer.UnselectAll();
		}

		#region ModListContextMenu

		private void OnOpend(object sender, RoutedEventArgs e)
		{

		}

		private void OnClosed(object sender, RoutedEventArgs e)
		{

		}

		#endregion ModListContextMenu
	}
}
