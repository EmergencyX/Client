using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace EmergencyX.Emergency5.Modifications
{
	/// <summary>
	/// A collection of useful methods related to mods
	/// </summary>
	public class ModTools
	{
		private List<InstalledMod> installedModifications { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		#region getterAndSetter

		public void setInstalledModifications(string jsonFilePath)
		{
			this.installedModifications = new List<InstalledMod>();

			Dictionary<string, Dictionary<string, string>> mods = new Dictionary<string, Dictionary<string, string>>();
			mods = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(File.ReadAllText(jsonFilePath));

			foreach (var mod in mods)
			{
				// grab the mod name
				//
				string modName = mod.Key;

				//grab all options in foreach and then add them to a new InstalledMods object
				//
				string[] modOption = new string[2];

				//counter for stringarray
				int i = 0;
				foreach (var option in mod.Value)
				{
					modOption[i] = option.Value;
					i++;
				}

				InstalledMod currentMod = new InstalledMod(modName, modOption[0], modOption[1]);
				this.installedModifications.Add(currentMod);

			}
			NotifyPropertyChanged();
		}

		public List<InstalledMod> getInstalledModifications()
		{
			return this.installedModifications;
		}

		#endregion getterAndSetter


		// Implement NotifyPropertyChanged
		//
		public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Constructor
		//
		public ModTools(string jsonFile)
		{
			setInstalledModifications(jsonFile);
		}

		// static Methodes down there...
		//


		/// <summary>
		/// Activates inactiv mods or deactivates activ mods
		/// </summary>
		/// <param name="modIndex">The index displayed in the listbox</param>
		/// <param name="installed">A list of all installed Emergency 5 Mods</param>
		/// <returns></returns>
		public static bool modifyModActivityState(int modIndex, List<InstalledMod> installed)
		{
			if (installed[modIndex].Enabled == "false")
			{
				installed[modIndex].Enabled = "true";
			}
			else if (installed[modIndex].Enabled == "true")
			{
				installed[modIndex].Enabled = "false";
			}
			else
			{
				// should only been reached if some  unexpected things are set as status
				//
				return false;
			}

			// return true if all is nice :-)
			//

			return true;
		}

		/// <summary>
		/// Re-writes the modification json file
		/// </summary>
		/// <param name="installed">a list of installed mods</param>
		/// <param name="jsonFilePath">the path to the emergeny mod settings file</param>
		/// <returns>true or false</returns>
		public static bool writeJsonModFile(List<InstalledMod> installed, string jsonFilePath)
		{
			// Reformate List into  Dictonary
			//	
			Dictionary<string, Dictionary<string, string>> mods = new Dictionary<string, Dictionary<string, string>>();

			foreach (var mod in installed)
			{
				Dictionary<string, string> optionStorage = new Dictionary<string, string>();
				string[] options = new string[2];

				//Getting mod Options and preparing them for saving
				//
				options[0] = mod.Enabled;
				options[1] = mod.OrderingIndex;

				optionStorage.Add("Enabled", options[0]);
				optionStorage.Add("OrderingIndex", options[1]);

				//Adding the mods to our Dictonary
				//
				mods.Add(mod.ModificationName, optionStorage);
			}

			// Creating JSON Data
			//
			string jsonModData = JsonConvert.SerializeObject(mods, Formatting.Indented);

			// Save to file
			//
			File.WriteAllText(@jsonFilePath, jsonModData);

			return true;
		}


	}
}
