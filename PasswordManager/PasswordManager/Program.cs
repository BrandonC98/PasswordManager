using System;
using System.Security.Cryptography;
using System.Text;

using System.Net;
using System.IO;

namespace PasswordManager
{

    class Program
    {

        static void Main(string[] args)
        {
            var salt = Hash.GenerateSalt(20);

            var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password1"), salt, 1000, 16);
            Console.WriteLine(Convert.ToBase64String(hashPassword));

            var hashPassword2 = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password1"), salt, 1000, 16);

            Console.WriteLine("Password 1: " + Convert.ToBase64String(hashPassword));
            Console.WriteLine("Password 2: " + Convert.ToBase64String(hashPassword2));


            if (Hash.CompareHash(hashPassword, hashPassword2)) Console.WriteLine("Login Successful");
            else Console.WriteLine("Failed login");

        }

    }
}
