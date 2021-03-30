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
            //var salt = Hash.GenerateSalt(20);

            //var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password1"), salt, 1000, 16);
            //Console.WriteLine(Convert.ToBase64String(hashPassword));

            //var hashPassword2 = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password1"), salt, 1000, 16);

            //Console.WriteLine("Password 1: " + Convert.ToBase64String(hashPassword));
            //Console.WriteLine("Password 2: " + Convert.ToBase64String(hashPassword2));


            //if (Hash.CompareHash(hashPassword, hashPassword2)) Console.WriteLine("Login Successful");
            //else Console.WriteLine("Failed login");

            using (var db = new PasswordManagerContext())
            {

                //var bl = new Blacklist() { IpAddress = "999.999.999", Note = "BAD"};
                //var ah = new AccessHistory() { Date = DateTime.Now, IpAddress = "000.000.000"};
                //var mp = new MasterPassword() { Iterations = 50 };

                //var salt = Hash.GenerateSalt(20);

                //var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password1"), salt, 1000, 16);

                //db.users.Add( new User()
                //{
                //    FirstName = "Brandon",
                //    LastName = "Campbell",
                //    EmailAddress = "BC@hotmail.co.uk"
                //});
                //db.SaveChanges();

                //foreach (var i in db.users) Console.WriteLine(i.FirstName);

                var brandon = db.users.First();
                //db.websites.Add( new Website() { Name = "Google", Username = "me123", UserId = brandon.Id});
                //db.websites.Add(new Website() { Name = "Youtube", Username = "You123", UserId = brandon.Id});
                foreach(var web in db.websites)
                {
                    //if (web.UserId == brandon.Id) brandon.Websites.Add(web);

                }
                //db.SaveChanges();
                foreach(var i in brandon.Websites)
                {

                    Console.WriteLine($"My Name is {brandon.FirstName} and I visited {i.Name} and my username is {i.Username}");

                }
               //brandon.MasterPassword = new MasterPassword() { UserId = brandon.Id, Hash = hashPassword, Salt = salt, Iterations = 1000 };
                foreach (var i in db.MasterPasswords) Console.WriteLine(i.Id);
                //brandon.MasterPassword = db.MasterPasswords.Where(m => m.UserId == brandon.Id).FirstOrDefault();
                Console.WriteLine($"My Name is {brandon.FirstName} {brandon.LastName} my Password hash is {Convert.ToBase64String(brandon.MasterPassword.Hash)} and the salt is {Convert.ToBase64String(brandon.MasterPassword.Salt)}");


                //u.AccessHistories.Add(ah);
                //u.Blacklists.Add(bl);
                //u.MasterPassword = mp;
                //u.Websites.Add(w);

                //Console.WriteLine(db.AccessHistories.FirstOrDefault().Id);
                //Console.WriteLine(db.Blacklists.FirstOrDefault().Id);
                //Console.WriteLine(db.users.FirstOrDefault().Id);
                //Console.WriteLine(db.websites.FirstOrDefault().Id);
                //Console.WriteLine(db.MasterPasswords.FirstOrDefault().Id);


            }


        }

    }
}
