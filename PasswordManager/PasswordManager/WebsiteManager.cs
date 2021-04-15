using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData;
using PasswordManagerData.Services;

namespace PasswordManager
{
    public class WebsiteManager
    {

        private IWebsiteService _service;

        public WebsiteManager()
        {

            _service = new WebsiteService();

        }

        public WebsiteManager(IWebsiteService service)
        {

            _service = service;

        }

        public void Create(int UserId, string WebsiteName, string encryptedPassword, string Username = null, string Url = null)
        {
            
            using(var db = new PasswordManagerContext())
            {

                if (Username == null) Username = "Unknown";
                if (Url == null) Url = "Unknown";

                var website = new Website()
                {
                    UserId = UserId,
                    Name = WebsiteName,
                    Password = encryptedPassword,
                    Username = Username,
                    Url = Url
                };

                _service.Create(website);


            }

        }

        public void Delete(int id)
        {

            _service.Delete(id);

        }

        public Website Retrieve(int id)
        {

            return _service.GetWebsiteById(id);

        }

        public void Update(int id, string websiteName = null, string encryptedPassword = null, string username = null, string url = null)
        {

            var website = _service.GetWebsiteById(id);

            if (websiteName != null) website.Name = websiteName;
            if (encryptedPassword != null) website.Password = encryptedPassword;
            if (username != null) website.Username = username;
            if (url != null) website.Url = url;
            _service.SaveChanges();

        }

        public string DecryptPasswordForWebsite(int websiteId, byte[] key)
        {

                var encryptedPassword = _service.GetWebsiteById(websiteId).Password;
                return SymmetricEncryption.Decrypt(Convert.ToBase64String(key), encryptedPassword);           

        }

        public List<Website> GetAll(int userId)
        {

            using (var db = new PasswordManagerContext())
            {

                return db.Websites.Where(w => w.UserId == userId).ToList();

            }

        }

    }
}
