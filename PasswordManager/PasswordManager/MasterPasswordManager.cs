using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData;

namespace PasswordManager
{
    public class MasterPasswordManager
    {

        public static MasterPassword Retrieve(int id)
        {
            using (var db = new PasswordManagerContext())
            {
                return db.MasterPasswords.Find(id); 
            }

        }

        public static MasterPassword RetrieveByUserId(int userId)
        {
            using (var db = new PasswordManagerContext())
            {
                return db.MasterPasswords.Where(mp => mp.UserId == userId).FirstOrDefault();
            }

        }

        public static void Create(int UserId, byte[] salt, byte[] hashPassword, int iterations = 1000)
        {

            using (var db = new PasswordManagerContext())
            {

                db.MasterPasswords.Add(new MasterPassword() 
                {
                    Hash = hashPassword,
                    Salt = salt,
                    Iterations = iterations,
                    UserId = UserId
                });

                db.SaveChanges();

            }

        } 

        public static void Delete(int id)
        {

            using (var db = new PasswordManagerContext())
            {

                db.RemoveRange(db.MasterPasswords.Find(id));
                db.SaveChanges();

            }

        }

        public static void Update(int id, byte[] hash, byte[] salt, int iteration = 1000)
        {
            
            using (var db = new PasswordManagerContext())
            {

                var mPassword = db.MasterPasswords.Find(id);
                mPassword.Hash = hash; 
                mPassword.Salt = salt;
                mPassword.Iterations = iteration;
                db.SaveChanges();
            }

        }
    }
}
