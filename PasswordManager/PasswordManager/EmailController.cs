using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData.Services;

namespace PasswordManager
{
    public class EmailController
    {

        public string ErrorMessage { get; private set; }

        public bool IsValidEmail(string email)
        {

            if (email.Contains("@")) return true;
            else
            {

                ErrorMessage = "Error Email Doesn't contain a @ symbol";
                return false;

            }


        }

        public bool IsEmailInUse(string email)
        {
            var userManager = new UserManager();

            if (!userManager.Exist(email)) return false;
            else
            {

                ErrorMessage = "This Email Address already has an account";
                return true;

            }

        }
    }
}
