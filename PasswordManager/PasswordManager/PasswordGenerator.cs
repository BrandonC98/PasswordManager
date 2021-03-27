using System;
using System.Security.Cryptography;

namespace PasswordManager
{
    public class PasswordGenerator
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
        
        public static string Generate(int Length, bool uppercase, bool numbers, bool symbol)
        {

            Byte[] arr = new byte[Length];

            rngCsp.GetBytes(arr);

            string password = "";

            for(int i = 0; i < Length; i++)
            {

                password += GenerateValidChar(uppercase, numbers, symbol);

            }

            return password;

        }

        private static char GenerateValidChar(bool includeUppercase, bool includeDigits, bool includeSymbols)
        {

            var singleByte = new byte[1];

            do
            {
                rngCsp.GetBytes(singleByte);

            } while (!IsByteValid(singleByte[0]));

            char character = Convert.ToChar(singleByte[0]);

            //using recursion to find a valid character
            if (Char.IsUpper(character) && !includeUppercase) character = GenerateValidChar(includeUppercase, includeDigits, includeSymbols);
            else if (Char.IsDigit(character) && !includeDigits) character = GenerateValidChar(includeUppercase, includeDigits, includeSymbols);
            else if (Char.IsSymbol(character) && !includeSymbols) character = GenerateValidChar(includeUppercase, includeDigits, includeSymbols);
            else if (Char.IsControl(character)) character = GenerateValidChar(includeUppercase, includeDigits, includeSymbols);
            else if (Char.IsSeparator(character)) character = GenerateValidChar(includeUppercase, includeDigits, includeSymbols);
            else if (Char.IsPunctuation(character)) character = GenerateValidChar(includeUppercase, includeDigits, includeSymbols);

            return character;

        }

        private static bool IsByteValid(Byte generatedByte)
        {

            if (generatedByte > 40 && generatedByte < 127) return true;
            else return false;

        }

    }
}
