﻿using System;
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
			
			try { 
				ZipArchive myArchiv = ZipFile.Open(getZipFileName(), ZipArchiveMode.Read);
				myArchiv.ExtractToDirectory(targetPath);
				myArchiv.Dispose();
			} 
			catch (Exception e)
			{
				//Error Handling
			}

			try
			{
				File.Delete(this.getZipFileName());
			}
			catch (Exception ex)
			{
				//Error Handling
			}

		}
	
	}
}
