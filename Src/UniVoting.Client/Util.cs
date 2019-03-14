// ***********************************************************************
// Assembly         : UniVoting.Client
// Author           : Duho
// Created          : 09-15-2018
//
// Last Modified By : Duho
// Last Modified On : 10-28-2018
// ***********************************************************************
// <copyright file="Util.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace UniVoting.Client
{
    /// <summary>
    /// Class Util.
    /// </summary>
    internal class Util
	{
        /// <summary>
        /// Converts the bytes to image.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap ConvertBytesToImage(byte[] bytes)
		{
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				return new Bitmap(ms);
			}
		}
        /// <summary>
        /// Bytes to image source.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>BitmapImage.</returns>
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
        /// <summary>
        /// Byteses to bitmap image.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>BitmapImage.</returns>
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
