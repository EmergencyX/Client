using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace EmergencyX_Client
{
	/// <summary>
	/// Class for easy use of the app.config
	/// </summary>
	public class AppConfig
	{

		public static string readFromAppConfig(string key) 
		{
			try
			{
				
				var appConf = ConfigurationManager.AppSettings;

				return (string) appConf[key];
				
			}
			catch (ConfigurationErrorsException)
			{
				System.Windows.MessageBox.Show(Properties.Resources.errErrorOnSettingsRead);
				return null;
			}
		}
		
		public static bool writeToAppConfig(string key, string value)
		{
			try
			{
				//update or insert installation path
				var appConf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var setting = appConf.AppSettings.Settings;

				//does the key already exist?
				if (setting[key] == null)
				{
					setting.Add(key, value);
				}
				else //if not
				{
					setting[key].Value = value;
				}

				//Important, saveing
				appConf.Save(ConfigurationSaveMode.Modified,true);
				ConfigurationManager.RefreshSection(appConf.AppSettings.SectionInformation.Name);

				//return true if all ok
				return true;

			}
			catch (ConfigurationErrorsException)
			{
				System.Windows.MessageBox.Show(Properties.Resources.errErrorOnSettingsSave, "Error");
				return false; // return false if something went wrong
			}
		}
	}
}
