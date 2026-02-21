using System.IO;
using Avalonia.Media.Imaging;

namespace UniVoting.Client
{
    internal class Util
    {
        public static Bitmap ByteToImageSource(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            return new Bitmap(ms);
        }

        public static Bitmap BytesToBitmapImage(byte[] bytes)
        {
            using var ms = new MemoryStream(bytes);
            return new Bitmap(ms);
        }
    }
}
