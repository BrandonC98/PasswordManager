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
        public static void Create(int UserId, string WebsiteName, string encryptedPassword, string Username = null, string Url = null)
        {
            
            using(var db = new PasswordManagerContext())
            {

                if (Username == null) Username = "Unknown";
                if (Url == null) Url = "Unknown";

                db.Websites.Add(new Website()
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

        public static void Delete(int id)
        {
            
            using(var db = new PasswordManagerContext())
            {

                db.Websites.RemoveRange(db.Websites.Find(id));
                db.SaveChanges();

            }

        }

        public static Website Retrieve(int id)
        {
            using (var db = new PasswordManagerContext())
            {

                return db.Websites.Find(id);

            }
        }

        public static void Update(int id, string websiteName = null, string encryptedPassword = null, string username = null, string url = null)
        {

            using(var db = new PasswordManagerContext())
            {

                var website = db.Websites.Find(id);

                if (websiteName != null) website.Name = websiteName;
                if (encryptedPassword != null) website.Password = encryptedPassword;
                if (username != null) website.Username = username;
                if (url != null) website.Url = url;
                db.SaveChanges();

            }

        }

        public static List<Website> GetAll(int userId)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.Websites.Where(w => w.UserId == userId).ToList();

            }

        }

    }
}
