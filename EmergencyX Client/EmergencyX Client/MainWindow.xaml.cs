﻿using System;
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
			dataContext.Watcher = new FileSystemWatcher();
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

			// FileWatcher for App.config stuff
			//
			dataContext.Watcher.Path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); // Pretty long...
			dataContext.Watcher.Filter = "EmergencyX Client.exe.config";
			dataContext.Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
			dataContext.Watcher.EnableRaisingEvents = true;
			dataContext.Watcher.Changed += this.OnFileChange;

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
			if (liModificationList.SelectedIndex == -1 | (liModificationList.SelectedIndex == dataContext.InstalledMods.Count - 1))
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

		/// <summary>
		/// Runs Emergency 5
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_RunEmergency_Click(object sender, RoutedEventArgs e)
		{
			ModTools.writeJsonModFile(dataContext.InstalledMods, dataContext.AppDataModificationsJsonFile);

			EmergencyInstallation myEmergencyInstallation = new EmergencyInstallation();

			try
			{

				System.Diagnostics.Process.Start(myEmergencyInstallation.getEmergencyInstallationPath() + @"\bin\em5_launcher.exe");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

		/// <summary>
		/// Opens the screenshot window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnScreenshotBrowser_Click(object sender, RoutedEventArgs e)
		{
			dataContext.ScreenshotWindow = new ScreenshotWindow();
			dataContext.ScreenshotWindow.Show();
		}

		#endregion ClickEvents

		// Close the whole application if mainwindow is closed
		//
		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ModTools.writeJsonModFile(dataContext.InstalledMods, dataContext.AppDataModificationsJsonFile);
			App.Current.Shutdown();
		}

		#region HoverForUpDownButtons
		private void btnIncreaseOrderingIndex_MouseEnter(object sender, MouseEventArgs e)
		{
			btnIncreaseOrderingIndex.Foreground = new SolidColorBrush(Color.FromArgb(255, 8, 158, 221));
		}

		private void btnIncreaseOrderingIndex_MouseLeave(object sender, MouseEventArgs e)
		{
			btnIncreaseOrderingIndex.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
		}

		private void btnDecreaseOrderingIndex_MouseEnter(object sender, MouseEventArgs e)
		{
			btnDecreaseOrderingIndex.Foreground = new SolidColorBrush(Color.FromArgb(255, 8, 158, 221));
		}

		private void btnDecreaseOrderingIndex_MouseLeave(object sender, MouseEventArgs e)
		{
			btnDecreaseOrderingIndex.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
		}

		#endregion HoverForUpDownButtons

		/// <summary>
		/// Event Handler for onFileChanged for our FileWatcher which is watching our app config
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFileChange(object sender, FileSystemEventArgs e)
		{
			string usrname = AppConfig.readFromAppConfig("username");
			string appConfLogin = AppConfig.readFromAppConfig("login");
			if ( ( (!usrname.Equals("") || !usrname.Equals(" ") ) && appConfLogin.Equals("success")) )
			{
				this.dataContext.Login.UserName = usrname; // update username to display it in the ui
				this.btnLogin.Visibility = System.Windows.Visibility.Hidden; //hidde login button
				this.btnLogout.Visibility = System.Windows.Visibility.Visible; //show logout button
				this.txbSuccessfullSaved.Text = Properties.Resources.successFullLoggedIn; // notify user that all is nice

				// set login key in app.conig to null
				// reasone for that: if something in the app conf changs, this check obove will not be enabled again
				//
				AppConfig.writeToAppConfig("login", ""); 
			}

			if(appConfLogin.Equals("error")) 
			{
				// Inform user about problem
				//
				MessageBox.Show("L10N", "L10N", MessageBoxButton.OK, MessageBoxImage.Error);

				// set login key in app.conig to null
				// reasone for that: if something in the app conf changs, this check obove will not be enabled again
				//
				AppConfig.writeToAppConfig("login", "");
			}
		}
	}
}