using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace EmergencyX_Client
{
	public class EmergencyInstallation
	{
		private static bool isEmergencyInstalled;
		private string emergencyInstallationPath;
	
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
		
		//Constructor
		public EmergencyInstallation() 
		{
			this.setEmergencyInstallationPath(AppConfig.readFromAppConfig("emergencyInstallationPath"));
			EmergencyInstallation.setIsEmergencyInstalled(this.getEmergencyInstallationPath());

		}

	}
}
