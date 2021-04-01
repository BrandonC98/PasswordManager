using System;
using System.Security.Cryptography;
using System.Text;
using PasswordManagerData;
using System.Net;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace PasswordManager
{

    class Program
    {

        static void Main(string[] args)
        {

            using (var db = new PasswordManagerContext())
            {
                Console.WriteLine("Removing");
                var userManager = new UserManager();
                var passwordManager = new MasterPasswordManager();
                var websiteManager = new WebsiteManager();
                foreach(var user in db.users)
                {

                    userManager.Delete(user.Id);
                    
                }

                foreach (var password in db.MasterPasswords)
                {

                    passwordManager.Delete(password.Id);

                }

                foreach (var website in db.websites)
                {

                    websiteManager.Delete(website.Id);

                }

                Console.WriteLine("Removal complete");
            }

        }

    }
}
