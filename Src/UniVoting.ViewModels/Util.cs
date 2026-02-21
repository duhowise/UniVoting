using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Avalonia.Media.Imaging;

namespace UniVoting.ViewModels;

internal static class Util
{
    public static Bitmap ByteToImageSource(byte[] bytes)
    {
        using var ms = new MemoryStream(bytes);
        return new Bitmap(ms);
    }

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
}
