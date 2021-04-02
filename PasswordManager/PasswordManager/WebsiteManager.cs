using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData;

namespace PasswordManager
{
    public class WebsiteManager
    {
        public void Create(int UserId, string WebsiteName, string encryptedPassword, string Username = null, string Url = null)
        {
            
            using(var db = new PasswordManagerContext())
            {

                if (Username == null) Username = "Unknown";
                if (Url == null) Url = "Unknown";

                db.websites.Add(new Website()
                {
                    UserId = UserId,
                    Name = WebsiteName,
                    Password = encryptedPassword,
                    Username = Username,
                    Url = Url
                });
                db.SaveChanges();

            }

        }

        public void Delete(int id)
        {
            
            using(var db = new PasswordManagerContext())
            {

                db.websites.RemoveRange(db.websites.Find(id));
                db.SaveChanges();

            }

        }

        public Website Retrieve(int id)
        {
            using (var db = new PasswordManagerContext())
            {

                return db.websites.Find(id);

            }
        }

        public Website RetrieveByUserId(int userId)
        {
            using (var db = new PasswordManagerContext())
            {

                return db.websites.Where(w => w.UserId == userId).FirstOrDefault();

            }
        }

        public void Update(int id, string websiteName = null, string encryptedPassword = null, string username = null, string url = null)
        {

            using(var db = new PasswordManagerContext())
            {

                var website = db.websites.Find(id);

                if (websiteName != null) website.Name = websiteName;
                if (encryptedPassword != null) website.Password = encryptedPassword;
                if (username != null) website.Username = username;
                if (url != null) website.Url = url;
                db.SaveChanges();

            }

        }

        public List<Website> GetAll()
        {
            
            using (var db = new PasswordManagerContext())
            {

                return db.websites.ToList();

            }

        }

        public List<Website> GetAll(int userId)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.websites.Where(w => w.UserId == userId).ToList();

            }

        }

    }
}
