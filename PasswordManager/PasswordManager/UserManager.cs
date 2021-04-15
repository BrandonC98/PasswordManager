using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData;
using PasswordManagerData.Services;

namespace PasswordManager
{
    public class UserManager
    {

        private IUserService _service;

        public UserManager()
        {

            _service = new UserService();

        }

        public UserManager(IUserService service)
        {

            _service = service;

        }

        public void Create(string firstName, string lastName, string email)
        {

                var user = new User()
                {

                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email

                };

                _service.CreateUser(user);



        }

        public User Retrieve(int id)
        {

               return _service.GetUserById(id);                       

        }

        public bool Exist(string email)
        {

            return _service.Exist(email);            

        }

        public User Retrieve(string email)
        {

            return _service.GetByEmail(email);
            
        }

        public void Delete(int id)
        {

            _service.RemoveUser(id);

        }

        public void Update(int id, string newFirstName = null, string newLastName = null, string newEmailAddress = null)
        { 

            var user = _service.GetUserById(id);
            if (newFirstName != null) user.FirstName = newFirstName;
            if (newLastName != null) user.LastName = newLastName;
            if (newEmailAddress != null) user.EmailAddress = newEmailAddress;

            _service.SaveChanges();         

        }

    }


}


