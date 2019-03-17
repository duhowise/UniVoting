﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace UniVoting.Admin
{
	public class Util
	{
		public static string GenerateRandomPassword(int length)
		{
		   
			var range = "23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz".ToCharArray();
		    var data = new byte[1];
		    using (var crypto = new RNGCryptoServiceProvider())
		    {
		        crypto.GetBytes(data);
		        data = new byte[length];
		        crypto.GetBytes(data);
		    }
		    var result = new StringBuilder(length);
		    foreach (var b in data)
		    {
		        result.Append(range[b % (range.Length)]);
		    }
			return result.ToString();
		}
		public static void Clear(Visual myMainWindow)
		{
			var childrenCount = VisualTreeHelper.GetChildrenCount(myMainWindow);
			for (int i = 0; i < childrenCount; i++)
			{
				var visualChild = (Visual)VisualTreeHelper.GetChild(myMainWindow, i);
				if (visualChild is TextBox tb)
				{
                    tb.Text = string.Empty;
				}
                if (visualChild is PasswordBox tb1)
				{
                    tb1.Password =string.Empty;
				}
				Clear(visualChild);
			}

		}
		public static System.Drawing.Image ConvertImage(BitmapImage img)
		{
			var ms = new MemoryStream();
			var bbe = new BmpBitmapEncoder();
			bbe.Frames.Add(BitmapFrame.Create(img.UriSource));
			bbe.Save(ms);
			var img2 = System.Drawing.Image.FromStream(ms);
			return img2;
		}
		public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
		{
			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var imageAttr = new ImageAttributes())
				{
					imageAttr.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
				}
			}
			//destImage
			return destImage;
		}
		public static Bitmap ConvertBytesToImage(byte[] bytes)
		{
			using (MemoryStream ms = new MemoryStream(bytes))
			{
			   return new Bitmap(ms);
			}
		}
		public static byte[] ConvertToBytes(Image image)
		{
			byte[] buffer;
			var bitmap = image.Source as BitmapSource;
			var encoder = new PngBitmapEncoder();
			if (bitmap != null) encoder.Frames.Add(BitmapFrame.Create(bitmap));
			using (var stream = new MemoryStream())
			{
				encoder.Save(stream);
				buffer = stream.ToArray();
			}
			return buffer;
		}
	   public static BitmapImage BitmapToImageSource(Bitmap bitmap)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, ImageFormat.Bmp);
				memory.Position = 0;
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.StreamSource = memory;
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.EndInit();
				return bitmapimage;
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
		public static byte[] GetSaltedPasswordHash(string username, string password)
		{
			byte[] pwdBytes = Encoding.UTF8.GetBytes(password);
			byte[] salt = Encoding.UTF8.GetBytes(username);
			byte[] saltedPassword = new byte[pwdBytes.Length + salt.Length];

			Buffer.BlockCopy(pwdBytes, 0, saltedPassword, 0, pwdBytes.Length);
			Buffer.BlockCopy(salt, 0, saltedPassword, pwdBytes.Length, salt.Length);

			SHA1 sha = SHA1.Create();

			return sha.ComputeHash(saltedPassword);
		}
	}
}