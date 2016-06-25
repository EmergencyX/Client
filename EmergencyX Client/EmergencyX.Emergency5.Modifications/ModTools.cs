using System;
using System.ComponentModel;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public class ModTools : INotifyPropertyChanged
	{
		private SortableObservableCollection<InstalledMod> installedModifications;
		public SortableObservableCollection<InstalledMod> InstalledModifications
		{
			get
			{
				return installedModifications;
			}

			set
			{
				installedModifications = value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region getterAndSetter

		public void setInstalledModifications(string jsonFilePath)
		{
			this.InstalledModifications = new SortableObservableCollection<InstalledMod>();

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
				this.InstalledModifications.Add(currentMod);

			}
			NotifyPropertyChanged();
		}

		public SortableObservableCollection<InstalledMod> getInstalledModifications()
		{
			return this.InstalledModifications;
		}

		#endregion getterAndSetter

		/// <summary>
		/// Adds a modification to our list
		/// </summary>
		/// <param name="modName">The name of the mod</param>
		/// <param name="activ">"true" or "false"</param>
		/// <param name="order">The "OrderingIndex"</param>
		public void addModification(string modName, string activ, string order)
		{
			this.InstalledModifications.Add(new InstalledMod(modName, activ, order));
			this.installedModifications.Sort(x => x.OrderingIndex, ListSortDirection.Ascending);
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

		// Constructor
		//
		public ModTools(string jsonFile)
		{
			setInstalledModifications(jsonFile);
		}

		#region staticMethods


		/// <summary>
		/// Activates inactiv mods or deactivates activ mods
		/// </summary>
		/// <param name="modIndex">The index displayed in the listbox</param>
		/// <param name="installed">A list of all installed Emergency 5 Mods</param>
		/// <returns></returns>
		public static bool modifyModActivityState(int modIndex, SortableObservableCollection<InstalledMod> installed)
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
		/// 
		/// </summary>
		/// <param name="modIndex"></param>
		/// <param name="installed"></param>
		/// <returns></returns>
		public static bool increaseOrderingIndex(int modIndex, SortableObservableCollection<InstalledMod> installed)
		{
			//only change somethint if not the last object was selected
			//
			if(modIndex == installed.Count - 1 )
			{
				return false;
			}
			
			// store the name of the modifications which currently has the new ordering index (mod Index = Ordering Index
			// because of calculation plus on and the list, the indexes and also the Collection counts from zero
			//
			string helperModName = installed.Where(e => e.OrderingIndex.Equals((modIndex + 1).ToString())).FirstOrDefault().ModificationName;

			installed[modIndex].OrderingIndex = (modIndex + 1).ToString();
			installed.Where(e => e.ModificationName.Equals(helperModName)).FirstOrDefault().OrderingIndex = modIndex.ToString();
			installed.Sort(x => x.OrderingIndex, ListSortDirection.Ascending);
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="modIndex"></param>
		/// <param name="installed"></param>
		/// <returns></returns>
		public static bool decreaseOrderingIndex(int modIndex, SortableObservableCollection<InstalledMod> installed)
		{
			//only change somethint if not the last object was selected
			//
			if (modIndex - 1 == -1)
			{
				return false;
			}

			// store the name of the modifications which currently has the new ordering index (mod Index = Ordering Index
			// because of calculation minus on and the list, the indexes and also the Collection counts from zero
			//
			string helperModName = installed.Where(e => e.OrderingIndex.Equals((modIndex - 1).ToString())).FirstOrDefault().ModificationName;

			installed[modIndex].OrderingIndex = (modIndex - 1).ToString();
			installed.Where(e => e.ModificationName.Equals(helperModName)).FirstOrDefault().OrderingIndex = modIndex.ToString();
			installed.Sort(x => x.OrderingIndex, ListSortDirection.Ascending);
			return true;
		}

		/// <summary>
		/// Re-writes the modification json file
		/// </summary>
		/// <param name="installed">a list of installed mods</param>
		/// <param name="jsonFilePath">the path to the emergeny mod settings file</param>
		/// <returns>true or false</returns>
		public static bool writeJsonModFile(SortableObservableCollection<InstalledMod> installed, string jsonFilePath)
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

		#endregion staticMethods

	}
}
