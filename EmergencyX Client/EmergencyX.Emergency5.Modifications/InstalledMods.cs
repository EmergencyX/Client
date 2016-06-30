using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EmergencyX.Emergency5.Modifications
{
	public class InstalledMod : INotifyPropertyChanged
	{

		private string modificationName;
		private bool enabled;
		private string orderingIndex;

		#region Propertys

		public string ModificationName
		{
			get
			{
				return this.modificationName;
			}

			set
			{
				modificationName = value;
				NotifyPropertyChanged();
			}
		}

		public bool Enabled
		{
			get
			{
				return enabled;
			}

			set
			{
				enabled = value;
				NotifyPropertyChanged();
			}
		}

		public string OrderingIndex
		{
			get
			{
				return orderingIndex;
			}

			set
			{
				orderingIndex = value;
				NotifyPropertyChanged();
			}
		}

		#endregion Propertys

		public InstalledMod(string name, bool activ, string order)
		{
			this.ModificationName = name;
			this.Enabled = activ;
			this.OrderingIndex = order;
		}

		#region notifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion notifyPropertyChanged
	}
}
