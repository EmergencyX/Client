using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EmergencyX.Emergency5.Modifications;
using EmergencyX.ZipService;
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

		DataPool dataContext 
		{
			get { return DataContext as DataPool; }
		}

		#region constructor
		//constructor
		//
		public MainWindow()
		{
			// default Initialization Stuff
			//
			InitializeComponent();
			DataContext = new DataPool();

			// start datacontext
			//
			dataContext.ModTools = new ModTools(dataContext.AppDataModificationsJsonFile);
			dataContext.EmergencyInstallation = new EmergencyInstallation();
			dataContext.Login = new Login();
			dataContext.InstalledMods = dataContext.ModTools.getInstalledModifications();
			dataContext.MainWindow = this;

			//initial value for username
			//
			if (AppConfig.readFromAppConfig("username") != "")
				dataContext.Login.UserName = AppConfig.readFromAppConfig("username");
			else
				dataContext.Login.UserName = Properties.Resources.notLoggedIn;

			//hidde success textbox
			//
			txbSuccessfullSaved.Visibility = Visibility.Hidden;

			// check if emergency is installed, if not disable the mod-box (well... because of this datacontext it looks not that nice...
			//
			if (EmergencyInstallation.getIsEmergencyInstalled() && dataContext.EmergencyInstallation.verifyEmergencyInstallation(dataContext.EmergencyInstallation.getEmergencyInstallationPath()))
				lblEmergencyNotInstalled.Visibility = Visibility.Hidden;
			else
				liModificationList.Visibility = Visibility.Hidden;

			//check weather user is logged in or not
			//
			if (AppConfig.readFromAppConfig("rememberMe").Equals("True"))
			{
				try
				{
					dataContext.Login.TokenLogin();
					txbSuccessfullSaved.Text = Properties.Resources.successFullLoggedIn;
					txbSuccessfullSaved.Visibility = Visibility.Visible;
					btnLogin.Visibility = Visibility.Hidden;
					btnLogout.Visibility = Visibility.Visible;

				}
				catch (NotSuccessFullLoggedInException noe)
				{
					MessageBox.Show(Properties.Resources.loginFailed, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}

			}
		}
		#endregion constructor

		//#region updateSpecificWindowData
		//public void updateSpecificWindowData()
		//{
		//	//Read Text from description in AssemblyInfo.cs and display it while app is running
		//	//Version.Text = ((System.Reflection.AssemblyDescriptionAttribute)System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Reflection.AssemblyDescriptionAttribute), false)[0]).Description;

		//	//if Emergency 5 is installed and no mods are installed the text "Emergency is installed" should be displayed
		//	//if its insatlled and there are mods the mods should be displayed 
		//	//otherwise there should be the text "Emergency is not installed"
		//	//

		//	//holds the full path to appdata mod 

		//	//if somewhere (somehow) an emergency installation is found and this installation could been verified
		//	if (EmergencyInstallation.getIsEmergencyInstalled() && myEmergencyInstallation.verifyEmergencyInstallation(myEmergencyInstallation.getEmergencyInstallationPath()))
		//	{
		//		this.InstalledMods = mainWindowModTools.getInstalledModifications();

		//	}
		//	else
		//	{
		//		// if not display the text "Emergency is not installed" and dis able the list
		//		//lblIsEmergencyInstalled.Content = Properties.Resources.emergencyIsNotInstalled;

		//	}
		//}
		//#endregion updateSpecificWindowData

		private void ModListContainerMouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{ 
				liModificationList.UnselectAll();
				if (txbSuccessfullSaved.IsVisible == true) 
				{ 
					txbSuccessfullSaved.Visibility = Visibility.Hidden;
				}
			}
		}

		#region ClickEvents

		/// <summary>
		/// Opens the settings
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSettings_Click(object sender, RoutedEventArgs e)
		{
			Settings emergencyXSettings = new Settings();
			emergencyXSettings.Show();
		}


		/// <summary>
		/// Handels clicks on the  up arrow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnIncreaseOrderingIndex_Click(object sender, RoutedEventArgs e)
		{
			//  if nothing is selected
			//
			if (liModificationList.SelectedIndex == -1 | (liModificationList.SelectedIndex == dataContext.InstalledMods.Count - 1) )
			{
				MessageBox.Show("Übersetzung", Properties.Resources.changeOrderingIndexDialogeTitle, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// store the number of the current selected item to send it into increasing and reselecting
			// increase the ordering index and sort our collection to display the result with the increaseOrderingIndex method
			//
			int currentlySelected = liModificationList.SelectedIndex;
			ModTools.increaseOrderingIndex(currentlySelected, dataContext.InstalledMods);

			// also let it selcted
			liModificationList.SelectedItem = liModificationList.Items[currentlySelected + 1];
		}

		/// <summary>
		/// Handels the clicks on the down arrow
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnDecreaseOrderingIndex_Click(object sender, RoutedEventArgs e)
		{
			//  if nothing is selected
			//
			if (liModificationList.SelectedIndex == -1 | (liModificationList.SelectedIndex - 1 == -1))
			{
				MessageBox.Show("Übersetzung", Properties.Resources.changeOrderingIndexDialogeTitle, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			// store the number of the current selected item to send it into decreasing and reselecting
			// decrease the ordering index and sort our collection to display the result withthe decreaseOrderingIndex method
			//
			int currentlySelected = liModificationList.SelectedIndex;
			ModTools.decreaseOrderingIndex(currentlySelected, dataContext.InstalledMods);

			// also let it selcted
			liModificationList.SelectedItem = liModificationList.Items[currentlySelected - 1];

		}

		private void btnAcivated_Click(object sender, RoutedEventArgs e)
		{
			ModTools.modifyModActivityState(liModificationList.SelectedIndex, dataContext.InstalledMods);

			if (ModTools.writeJsonModFile(dataContext.InstalledMods, dataContext.AppDataModificationsJsonFile))
			{
				txbSuccessfullSaved.Text = Properties.Resources.changesSuccessfullSaved;
				txbSuccessfullSaved.Visibility = Visibility.Visible;
			}
		}

		/// <summary>
		/// Runs Emergency 5
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_RunEmergency_Click(object sender, RoutedEventArgs e)
		{

			EmergencyInstallation myEmergencyInstallation = new EmergencyInstallation();

			try { 
				
				System.Diagnostics.Process.Start(myEmergencyInstallation.getEmergencyInstallationPath() + @"\bin\em5_launcher.exe");
			} catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
			}
		}

		/// <summary>
		/// Starts the login mechanics
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLogin_Click(object sender, RoutedEventArgs e)
		{
			LoginWindow loginWindow = new LoginWindow();
			loginWindow.DataContext = DataContext;
			loginWindow.Show();
		}

		/// <summary>
		/// Starts the logout proccess
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnLogout_Click(object sender, RoutedEventArgs e)
		{

		}

		#endregion ClickEvents

		// Close the whole application if mainwindow is closed
		//
		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			App.Current.Shutdown();
		}

	}
}
