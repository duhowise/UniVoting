using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace UniVoting.Client
{
	internal  class Util
	{
		public static Bitmap ConvertBytesToImage(byte[] bytes)
		{
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				return new Bitmap(ms);
			}
		}
		public static BitmapImage ByteToImageSource(byte[] bytes)
		{
			using (MemoryStream memory = new MemoryStream(bytes))
			{
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.StreamSource = memory;
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.EndInit();
				return bitmapimage;

			}

		}
		public static BitmapImage BytesToBitmapImage(byte[] bytes)
		{
			using (MemoryStream memory = new MemoryStream(bytes))
			{
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.StreamSource = memory;
				bitmapimage.EndInit();
				return bitmapimage;
			}
				
		}
	}
}
