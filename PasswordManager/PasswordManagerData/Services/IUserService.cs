using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData.Services
{
    public interface IUserService
    {

        public void CreateUser(User u);
        public User GetUserById(int userId);
        public User GetByEmail(string email);
        public void SaveChanges();
        public void RemoveUser(int id);

        public bool Exist(string email);

    }
}
