using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace IntelDrawingDataBackend.Util
{
    // 字符串加密，参考文章：https://blog.csdn.net/heike_Ch/article/details/141252591
    public class StringEncryptor
    {
        private readonly byte[] key;
        private readonly byte[] iv;

        public StringEncryptor(string keyString, string ivString)
        {
            key = Encoding.UTF8.GetBytes(keyString);
            iv = Encoding.UTF8.GetBytes(ivString);
        }

        public string Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream();
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                using (var streamWriter = new StreamWriter(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
            }

            return Convert.ToBase64String(memoryStream.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.IV = iv;

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }
    }
}
