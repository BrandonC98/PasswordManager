using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData;

namespace PasswordManager
{
    public class UserManager
    {

        public static void Create(string firstName, string lastName, string email)
        {

            using(var db = new PasswordManagerContext())
            {

                var user = new User()
                {

                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email

                };

                db.Add(user);
                db.SaveChanges();

            }

        }

        public static User Retrieve(int id)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.Users.Find(id);
            }

        }

        public static bool Exist(string email)
        {


            using(var db = new PasswordManagerContext())
            {

                if (db.Users.Any(u => u.EmailAddress == email)) return true;
                else return false;

            }

        }

        public static User Retrieve(string email)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.Users.Where(u => u.EmailAddress == email).FirstOrDefault();
            }

        }

        public static void Delete(int id)
        {

            using (var db = new PasswordManagerContext())
            {

                db.RemoveRange(db.Users.Find(id));
                db.SaveChanges();

            }

        }

        public static void Update(int id, string newFirstName = null, string newLastName = null, string newEmailAddress = null)
        {

            using(var db = new PasswordManagerContext())
            {

                var user = db.Users.Find(id);
                if (newFirstName != null) user.FirstName = newFirstName;
                if (newLastName != null) user.LastName = newLastName;
                if (newEmailAddress != null) user.EmailAddress = newEmailAddress;
                db.SaveChanges();

            }

        }

    }


}


