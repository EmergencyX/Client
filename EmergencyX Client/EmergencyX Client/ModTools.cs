using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace EmergencyX_Client
{
/// <summary>
/// A collection of useful methods related to mods
/// </summary>
	public class ModTools
	{
		private Dictionary<int, string> modifications;
		//Dictionary<string, string> options;
		
		public Dictionary<int,string> getModifications()
		{
			return this.modifications;
		}

		public void setModifications(string jsonFilePath)
		{
			this.modifications = new Dictionary<int, string>();
			
			//first read all "top-level" key which are the mod names
			//each of them contains as value is options eg. "Enabled" or OderingIndex also in JSON Noation
			Dictionary<string, object> modNames = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText(jsonFilePath));

			//go through all mods
			int counter = 0;
			

			foreach (var mod in modNames)
			{
				this.modifications.Add(counter, mod.Key);

				/* **** The follwing code is uncommented because it is currently not needed, but could maybe helpfull as example ****
				 * 
				Dictionary<string, object> modOptions = JsonConvert.DeserializeObject<Dictionary<string, object>>(mod.Value.ToString());

				//go also through all options
				int count = 0;
				foreach (var opt in modOptions) 
				{
					this.options.Add(mod.Key + count.ToString(),)
				}
				*/

				counter++;
			} //end foreache
			
		} //end setModifications;
		
		public ModTools(string jsonFile)
		{
			setModifications(jsonFile);
		}

	}
}
