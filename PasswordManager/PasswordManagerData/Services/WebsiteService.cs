using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData.Services
{
    public class WebsiteService : IWebsiteService
    {
        private readonly PasswordManagerContext _context;

        public void Create(Website website)
        {

            _context.Websites.Add(website);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {

            _context.Websites.RemoveRange(_context.Websites.Find(id));
            _context.SaveChanges();

        }

        public Website GetWebsiteById(int id)
        {

            return _context.Websites.Find(id);

        }

        public void SaveChanges()
        {

            _context.SaveChanges();

        }

        public List<Website> GetAllWebsitesByUserId(int userId)
        {

            return _context.Websites.Where(w => w.UserId == userId).ToList(); 

        }

    }
}
