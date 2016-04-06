using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace EmergencyX.Emergency5.Modifications
{
	/// <summary>
	/// A collection of useful methods related to mods
	/// </summary>
	public class ModTools
	{
		private List<InstalledMod> installedModifications { get; set; }

		#region getterAndSetter

		public void setInstalledModifications(string jsonFilePath)
		{
			this.installedModifications = new List<InstalledMod>();

			Dictionary<string, object> modNames = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(jsonFilePath));

			string helper = null;
			string helperTwo = null;
			string helperThree = null;

			foreach (var mod in modNames)
			{
				helper = mod.Key;

				Dictionary<string, object> modOptions = JsonConvert.DeserializeObject<Dictionary<string, object>>(mod.Value.ToString());
					
				//counter
				int i = 1;
				foreach (var key in modOptions)
				{
					//if i is one the loop has too work whit the modification activation key
					//if if i is two the loop has too handel the ordering index key
					//if its running more then three times there is a problem with the json file
					if (i == 1) 
					{ 
						helperTwo = key.Value.ToString();
					}
					else if(i == 2)
					{ 
						helperThree = key.Value.ToString();
					}
					else {
						throw new JsonException();
					}

					//increase counter
					i++;
				}

				InstalledMod currentMod = new InstalledMod { modificationName = helper, modificationActivationStatus = helperTwo, modificationOrderingIndex = helperThree };
				this.installedModifications.Add(currentMod);

			}
		}

		public List<InstalledMod> getInstalledModifications()
		{
			return this.installedModifications;
		}

		#endregion getterAndSetter

		public ModTools(string jsonFile)
		{
			setInstalledModifications(jsonFile);
		}

		// static Methodes down there...
		//

		public static bool modifyModActivityState(bool status,string mod,List<InstalledMod> installed)
		{
			//WIP!!
			List<int> i = new List<int>();
			 i.Add(installed.FindIndex(delegate (InstalledMod Mods)
						{
							if (Mods.modificationName == mod)
								return true;
							else
								return false;
						})
			);

			
			foreach (int index in i)
			{
				installed[index].modificationActivationStatus = status.ToString();
			}

			// return
			//
			return true;
		}

		
	}
}
