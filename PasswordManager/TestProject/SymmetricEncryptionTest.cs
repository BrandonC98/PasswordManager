using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PasswordManager;

namespace TestProject
{
    class SymmetricEncryptionTest
    {

        [TestCase("MasterPassword")]
        [TestCase("M")]
        [TestCase("Password123")]
        [TestCase("Password123!?")]
        [TestCase("%$$!245:{?_=+)(/")]
        [TestCase("123456789801234567890123456789012345678901234567890123456789012345678901234567890123456788901234567890")]
        public void NoMatterTheKeyTheDataCanBeEncrypted(string MasterPassword)
        {

            var salt = Hash.GenerateSalt(20);
            var key = Hash.GenerateHash(Encoding.ASCII.GetBytes(MasterPassword), salt, 1000, 16);

            var text = "JustSomePlainText";
            var encrpytedText = SymmetricEncryption.Encrypt(Convert.ToBase64String(key), text);
            var plainText = SymmetricEncryption.Decrypt(Convert.ToBase64String(key), encrpytedText);
            Assert.AreEqual(text, plainText);
        }

        [Test]
        public void PlainTextIsDifferentFromEncrypted()
        {

            var salt = Hash.GenerateSalt(20);
            var key = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password123"), salt, 1000, 16);

            var text = "JustSomePlainText";
            var encrpytedText = SymmetricEncryption.Encrypt(Convert.ToBase64String(key), text);

            Assert.AreEqual(true, text != encrpytedText);

        }

        [Test]
        public void TheWrongKeyWillNotDecryptTheData()
        {

            var salt = Hash.GenerateSalt(20);
            var key1 = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password1"), salt, 1000, 16);
            var key2 = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password2"), salt, 1000, 16);

            var text = "JustSomePlainText";
            var encrpytedText = SymmetricEncryption.Encrypt(Convert.ToBase64String(key1), text);
            Assert.Throws<System.Security.Cryptography.CryptographicException>(() => SymmetricEncryption.Decrypt(Convert.ToBase64String(key2), encrpytedText));
        }

    }
}
