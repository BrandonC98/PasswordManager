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

        public void Create(string firstName, string lastName, string email)
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

        public User Retrieve(int id)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.users.Find(id);
            }

        }

        public bool Exist(string email)
        {


            using(var db = new PasswordManagerContext())
            {

                if (db.users.Any(u => u.EmailAddress == email)) return true;
                else return false;

            }

        }

        public User Retrieve(string email)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.users.Where(u => u.EmailAddress == email).FirstOrDefault();
            }

        }

        public void Delete(int id)
        {

            using (var db = new PasswordManagerContext())
            {

                db.RemoveRange(db.users.Find(id));
                db.SaveChanges();

            }

        }

        public void Update(int id, string newFirstName = null, string newLastName = null, string newEmailAddress = null)
        {

            using(var db = new PasswordManagerContext())
            {

                var user = db.users.Find(id);
                if (newFirstName != null) user.FirstName = newFirstName;
                if (newLastName != null) user.LastName = newLastName;
                if (newEmailAddress != null) user.EmailAddress = newEmailAddress;
                db.SaveChanges();

            }

        }



    }


}


