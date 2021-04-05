using NUnit.Framework;
using PasswordManager;
using System;
using System.Text;

namespace TestProject
{
    public class HashTest
    {
        
        [TestCase("Password123", "Password1234", false)]
        [TestCase("Password123", "Password12", false)]
        [TestCase("Password123", "Password122",false)]
        [TestCase("Password123", "password123", false)]
        [TestCase("Password123", "Password123_", false)]
        [TestCase("Password123", "!Password123", false)]
        public void SimmilarButNotExactPasswordsWillFail(string password1, string password2, bool expected)
        {

            var salt = Hash.GenerateSalt(20);
            var hashedPassword1 = Hash.GenerateHash(Encoding.ASCII.GetBytes(password1), salt, 10000, 10);
            var hashedPassword2 = Hash.GenerateHash(Encoding.ASCII.GetBytes(password2), salt, 10000, 10);

            Assert.AreEqual(expected, Hash.CompareHash(hashedPassword1, hashedPassword2));

        }


        [TestCase("Password123", "Password123", true)]
        [TestCase("HelloWorld", "HelloWorld", true)]
        public void TheSamePasswordWillReturnTrue(string password1, string password2, bool expected)
        {

            var salt = Hash.GenerateSalt(20);
            var hashedPassword1 = Hash.GenerateHash(Encoding.ASCII.GetBytes(password1), salt, 10000, 10);
            var hashedPassword2 = Hash.GenerateHash(Encoding.ASCII.GetBytes(password2), salt, 10000, 10);

            Assert.AreEqual(expected, Hash.CompareHash(hashedPassword1, hashedPassword2));

        }

        [TestCase(20, 21, false)]
        [TestCase(20, 19, false)]
        [TestCase(20, 20, true)]
        public void DifferentHashLengthsWillFail(int hashLength1, int hashLength2, bool expected)
        {
            var password = "Password123";
            var salt = Hash.GenerateSalt(20);
            var hashedPassword1 = Hash.GenerateHash(Encoding.ASCII.GetBytes(password), salt, 10000, hashLength1);
            var hashedPassword2 = Hash.GenerateHash(Encoding.ASCII.GetBytes(password), salt, 10000, hashLength2);

            Assert.AreEqual(expected, Hash.CompareHash(hashedPassword1, hashedPassword2));

        }
    }
}