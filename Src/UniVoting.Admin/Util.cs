using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Avalonia.Controls;
using Avalonia.VisualTree;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using AvaloniaBitmap = Avalonia.Media.Imaging.Bitmap;

namespace UniVoting.Admin
{
    public class Util
    {
        public static string GenerateRandomPassword(int length)
        {
            var range = "23456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz".ToCharArray();
            var data = new byte[length];
            RandomNumberGenerator.Fill(data);
            var result = new StringBuilder(length);
            foreach (var b in data)
                result.Append(range[b % range.Length]);
            return result.ToString();
        }

        public static void Clear(Avalonia.Visual myMainWindow)
        {
            foreach (var child in myMainWindow.GetVisualDescendants())
            {
                if (child is TextBox tb)
                    tb.Text = string.Empty;
            }
        }

        public static byte[] ConvertToBytes(Avalonia.Controls.Image image)
        {
            if (image.Source is AvaloniaBitmap bmp)
            {
                using var ms = new MemoryStream();
                bmp.Save(ms);
                return ms.ToArray();
            }
            return Array.Empty<byte>();
        }

        public static AvaloniaBitmap ByteToImageSource(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            return new AvaloniaBitmap(ms);
        }

        public static AvaloniaBitmap BitmapToImageSource(SixLabors.ImageSharp.Image image)
        {
            using var ms = new MemoryStream();
            image.Save(ms, new PngEncoder());
            ms.Position = 0;
            return new AvaloniaBitmap(ms);
        }

        public static SixLabors.ImageSharp.Image ConvertImage(string filePath)
        {
            return SixLabors.ImageSharp.Image.Load(filePath);
        }

        public static SixLabors.ImageSharp.Image ResizeImage(SixLabors.ImageSharp.Image image, int width, int height)
        {
            image.Mutate(x => x.Resize(width, height));
            return image;
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
