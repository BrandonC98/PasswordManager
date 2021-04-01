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

        public MasterPassword Retrieve(int id)
        {
            using (var db = new PasswordManagerContext())
            {
                return db.MasterPasswords.Find(id); 
            }

        }

        public MasterPassword RetrieveByUserId(int userId)
        {
            using (var db = new PasswordManagerContext())
            {
                return db.MasterPasswords.Where(mp => mp.UserId == userId).FirstOrDefault();
            }

        }

        public void Create(int UserId, byte[] salt, byte[] hashPassword, int iterations = 1000)
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

        public void Delete(int id)
        {

            using (var db = new PasswordManagerContext())
            {

                db.RemoveRange(db.MasterPasswords.Find(id));
                db.SaveChanges();

            }

        }

        public void Update(int id, byte[] hash, byte[] salt, int iteration = 1000)
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
