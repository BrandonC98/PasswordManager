using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData.Services
{
    public interface IWebsiteService
    {

        public void Create(Website website);

        public void Delete(int id);
        public Website GetWebsiteById(int id);
        public void SaveChanges();
        public List<Website> GetAllWebsitesByUserId(int userId);


    }
}
