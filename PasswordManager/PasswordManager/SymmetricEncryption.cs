using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager
{
    public class SymmetricEncryption
    {

        public static string Encrypt(string key, string plainText)
        {

            Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];


            byte[] encryptedText;

            using (var memoryStream = new MemoryStream())
            {

                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(aes.Key, aes.IV), CryptoStreamMode.Write))
                {

                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {

                        streamWriter.Write(plainText);

                    }

                    encryptedText = memoryStream.ToArray();
                }

            }

            return Convert.ToBase64String(encryptedText);

        }

        public static string Decrypt(string key, string EncryptedText)
        {

            Aes aes = Aes.Create();

            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            using (var memoryStream = new MemoryStream(Convert.FromBase64String(EncryptedText)))
            {

                using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(aes.Key, aes.IV), CryptoStreamMode.Read))
                {

                    using (var streamReader = new StreamReader(cryptoStream))
                    {

                        return streamReader.ReadToEnd();

                    }

                }

            }

        }

    }
}
