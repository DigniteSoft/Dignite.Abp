using System.Security.Cryptography;

namespace System.IO
{
    public static class DigniteStreamExtensions
    {
        public static string ToMd5(this Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                stream.Position = 0;
                var hash = md5.ComputeHash(stream);
                var base64String = Convert.ToBase64String(hash);
                return base64String;
            }
        }
    }
}
