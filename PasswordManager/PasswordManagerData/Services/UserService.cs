using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData.Services
{
    public class UserService : IUserService
    {

        private readonly PasswordManagerContext _context;

        public UserService()
        {

            _context = new PasswordManagerContext();

        }

        public UserService(PasswordManagerContext context)
        {

            _context = context;

        }

        public bool Exist(string email)
        {

            return _context.Users.Any(u => u.EmailAddress == email);

        }

        public void CreateUser(User u)
        {
            _context.Add(u);
            _context.SaveChanges();
        }

        public User GetUserById(int userId)
        {

            var user = _context.Users.Find(userId);

            if (user == null) throw new NullReferenceException("No user of this Id was found");

            return user;

        }

        public User GetByEmail(string email)
        {

            var user = _context.Users.Where(u => u.EmailAddress == email).FirstOrDefault();

            if (user == null) throw new NullReferenceException("No user of with this Email was found");

            return user;

        }

        public void RemoveUser(int id)
        {
            _context.RemoveRange(_context.Users.Find(id));
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();

        }
    }
}
