using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Microsoft.VisualBasic.Devices;

namespace EmergencyX_Client
{
	public static class EmergencyInstallation
	{
		private static bool isEmergencyInstalled;
		private static string emergencyInstallationPath;
	
		// <var isEmergencyInstalled>	
		public static bool getIsEmergenyInstalled() 
		{
			return EmergencyInstallation.isEmergencyInstalled;
		}
		
		public static void setIsEmergencyInstalled()
		{
			if (EmergencyInstallation.emergencyInstallationPath != null) // if the is no installation path no emergeny is installed
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
		public static string getEmergencyInstallationPath()
		{
			return EmergencyInstallation.emergencyInstallationPath;
		}

		public static void setEmergencyInstallationPath(string installPath)
		{
			EmergencyInstallation.emergencyInstallationPath = installPath;		
		}

		//</var>
		

	}
}
