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
	/// Contains all the things related with unpacking a File
	/// </summary>
	public class Unzipper
	{

		private string zipFileName;
		
		public string getZipFileName()
		{
			return this.zipFileName;
		}

		public void setZipFileName(string zipFileName)
		{
			this.zipFileName = zipFileName;
		}

		public void unpackModZip(string targetPath)
		{
			
			try { 
				ZipArchive myArchiv = ZipFile.Open(this.zipFileName, ZipArchiveMode.Read);
				myArchiv.ExtractToDirectory(targetPath);
				myArchiv.Dispose();
			} 
			catch (Exception e)
			{
				
			}

			try
			{
				//File
			}
			catch (Exception ex)
			{

			}

		}
	
	}
}
