using System;
using System.Security.Cryptography;
using System.Text;

namespace MvcSolution
{
    public class CryptoService
    {
        public static string Md5Encrypt(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var keyBytes = Encoding.UTF8.GetBytes(AppContext.Md5Key);

            var des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMd5(keyBytes);
            des.Mode = CipherMode.ECB;
            var encrypted = des.CreateEncryptor().TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(encrypted);
        }

        public static string Md5Decrypt(string encodedString)
        {
            var encodedBytes = Convert.FromBase64String(encodedString);
            var keyBytes = Encoding.UTF8.GetBytes(AppContext.Md5Key);

            var des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMd5(keyBytes);
            des.Mode = CipherMode.ECB;
            var decrypted = des.CreateDecryptor().TransformFinalBlock(encodedBytes, 0, encodedBytes.Length);
            return Encoding.Default.GetString(decrypted);
        }

        public static string Md5HashEncrypt(string input)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(result);
        }

        public static string Sha1HashEncrypt(string input)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var result = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(result);
        }

        #region private methods

        private static byte[] MakeMd5(byte[] original)
        {
            using (var hashmd5 = new MD5CryptoServiceProvider())
            {
                byte[] keyhash = hashmd5.ComputeHash(original);
                return keyhash;
            }
        }

        #endregion
    }
}
