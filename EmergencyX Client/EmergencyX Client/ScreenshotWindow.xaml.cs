using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace EmergencyX_Client
{
	/// <summary>
	/// Interaktionslogik für ScreenshotWindow.xaml
	/// </summary>
	public partial class ScreenshotWindow : Window
	{

		DataPool dataContext
		{
			get { return DataContext as DataPool; }
		}

		public ScreenshotWindow()
		{
			InitializeComponent();

			//foreach (string file in Directory.GetFiles(dataContext.ScreenshotsDir))
			//{
				Stream imageStreamSource = new FileStream(@"C:\Users\yanni\AppData\Roaming\Promotion Software GmbH\EMERGENCY 5\screenshot\screenshot_2016-05-01-13-36-23.tif", FileMode.Open, FileAccess.Read, FileShare.Read);
				TiffBitmapDecoder screenshotDecoder = new TiffBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.None);
				BitmapSource image = screenshotDecoder.Frames[0];

				Image myImage = new Image();
				myImage.Source = image;
				myImage.Stretch = Stretch.None;
				myImage.Margin = new Thickness(4);
				
				//ImageList
			//}
		}
	}
}
