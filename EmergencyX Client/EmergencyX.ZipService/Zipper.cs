using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace EmergencyX.ZipService
{
	/// <summary>
	///  Contains all the files related with packing a file
	/// </summary>
	public class Zipper
	{
		private string zipFileName;
		private string savePath;

		#region getterAndSetter
		public string getZipFileName()
		{
			return this.zipFileName;
		}

		public void setZipFileName(string zipFileName)
		{
			this.zipFileName = zipFileName;
		}

		public string getSavePath()
		{
			return this.savePath;
		}

		public void setSavePath(string savePath)
		{
			this.savePath = savePath;
		}

		#endregion getterAndSetter

		/// <summary>
		/// Adds files into a ZipArchiv
		/// </summary>
		public void packModZip()
		{
			// The user should have all files for a mod in on direcotry
			//
			ZipFile.CreateFromDirectory(getZipFileName(), getSavePath());
		}
	}
}
