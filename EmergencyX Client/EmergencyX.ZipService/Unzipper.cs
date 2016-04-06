using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace EmergencyX.ZipService
{
	/// <summary>
	/// Contains all the things related with unpacking a file
	/// </summary>
	public class Unzipper
	{

		private string zipFileName;

		#region getterAndSetter

		public string getZipFileName()
		{
			return this.zipFileName;
		}

		public void setZipFileName(string zipFileName)
		{
			this.zipFileName = zipFileName;
		}

		#endregion getterAndSetter

		/// <summary>
		/// Upacks a ModZip
		/// </summary>
		/// <param name="targetPath"></param>
		public void unpackModZip(string targetPath)
		{
			//First unpack
			//
			ZipArchive myArchiv = ZipFile.Open(getZipFileName(), ZipArchiveMode.Read);
			myArchiv.ExtractToDirectory(targetPath);
			myArchiv.Dispose();
			
			//Then clean up (-> delete the file)
			//
			File.Delete(this.getZipFileName());

		}
	
	}
}
