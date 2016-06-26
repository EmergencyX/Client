using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EmergencyX.Emergency5.Modifications;
using System.Windows.Controls;

namespace EmergencyX_Client
{
	public class DataPool : INotifyPropertyChanged
	{
		//Member
		//
		private Login login;
		private ModTools modTools;
		private SortableObservableCollection<InstalledMod> installedMods;
		private string appDataModificationsJsonFile;
		private string modificationsDir;
		private string screenshotsDir;
		private EmergencyInstallation emergencyInstallation;
		private MainWindow mainWindow;
		private ScreenshotWindow screenshotWindow;

		// property changed
		//
		public event PropertyChangedEventHandler PropertyChanged;

		// Property stuff
		//
		public Login Login
		{
			get
			{
				return login;
			}

			set
			{
				login = value;
				NotifyPropertyChanged();
			}
		}
		public ModTools ModTools
		{
			get
			{
				return modTools;
			}

			set
			{
				modTools = value;
				NotifyPropertyChanged();
			}
		}
		public SortableObservableCollection<InstalledMod> InstalledMods
		{
			get
			{
				return installedMods;
			}

			set
			{
				installedMods = value;
				NotifyPropertyChanged();
			}
		}
		public string AppDataModificationsJsonFile
		{
			get
			{
				return appDataModificationsJsonFile;
			}

			set
			{
				appDataModificationsJsonFile = value;
				NotifyPropertyChanged();
			}
		}
		public EmergencyInstallation EmergencyInstallation
		{
			get
			{
				return emergencyInstallation;
			}

			set
			{
				emergencyInstallation = value;
				NotifyPropertyChanged();
			}
		}
		public MainWindow MainWindow
		{
			get
			{
				return mainWindow;
			}

			set
			{
				mainWindow = value;
			}
		}
		public ScreenshotWindow ScreenshotWindow
		{
			get
			{
				return screenshotWindow;
			}

			set
			{
				screenshotWindow = value;
			}
		}
		public string ScreenshotsDir
		{
			get
			{
				return screenshotsDir;
			}

			set
			{
				screenshotsDir = value;
				NotifyPropertyChanged();
			}
		}

		// Constructer
		//
		public DataPool()
		{
			// Define path to Appdata located mods setting file and create a new instance of ModTools with it
			//
			AppDataModificationsJsonFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Promotion Software GmbH\EMERGENCY 5\mods\mods_user_settings.json";
			//Define mod folder path
			//
			modificationsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Promotion Software GmbH\EMERGENCY 5\mods";
			//Define screenshot dir
			//
			ScreenshotsDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Promotion Software GmbH\EMERGENCY 5\screenshot";
		}

		public void loginWithUsernameUpdate(string username, string password, bool remainLoggedIn)
		{

			this.Login.FullLogin(username, password, remainLoggedIn); // do login
			this.Login.UserName = username; // update username to display it in the ui
			this.MainWindow.btnLogin.Visibility = System.Windows.Visibility.Hidden; //hidde login button
			this.MainWindow.btnLogout.Visibility = System.Windows.Visibility.Visible; //show logout button
			this.MainWindow.txbSuccessfullSaved.Text = Properties.Resources.successFullLoggedIn; // notify user that all is nice
		}


		// Implement NotifyPropertyChanged
		//
		public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
