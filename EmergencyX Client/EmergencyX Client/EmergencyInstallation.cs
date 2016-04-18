using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace EmergencyX_Client
{
	public class EmergencyInstallation
	{
		private static bool isEmergencyInstalled;
		private string emergencyInstallationPath;

		//Constructor
		public EmergencyInstallation()
		{
			this.setEmergencyInstallationPath(AppConfig.readFromAppConfig("emergencyInstallationPath"));
			#if DEBUG
				setEmergencyInstallationPath(@"D:\Program Files (x86)\Emergency 5");
			#endif
			EmergencyInstallation.setIsEmergencyInstalled(this.getEmergencyInstallationPath());

		}


		// <var isEmergencyInstalled>	
		public static bool getIsEmergencyInstalled()
		{
			return EmergencyInstallation.isEmergencyInstalled;
		}

		public static void setIsEmergencyInstalled(string path)
		{
			if (!path.Length.Equals(0)) // if the is no installation path no emergeny is installed
			{
				EmergencyInstallation.isEmergencyInstalled = true;
			}
			else
			{
				EmergencyInstallation.isEmergencyInstalled = false;
			}
		}
		//</var>


		//<var emergencyInstallationPath>
		public string getEmergencyInstallationPath()
		{
			return this.emergencyInstallationPath;
		}

		public void setEmergencyInstallationPath(string installPath)
		{
			this.emergencyInstallationPath = installPath;
		}

		//</var>

		/// <summary>
		/// This method verifys an may existing emergency Installation
		/// </summary>
		/// <param name="installPath"></param>
		/// <returns></returns>
		public bool verifyEmergencyInstallation(string installPath)
		{

			if (File.Exists(@installPath + @"\uninstall.exe"))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
