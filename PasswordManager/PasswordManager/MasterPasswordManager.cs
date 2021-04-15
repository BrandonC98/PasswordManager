using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PasswordManagerData;
using PasswordManagerData.Services;

namespace PasswordManager
{
    public class MasterPasswordManager
    {

        private IMasterPasswordService _service;

        public MasterPasswordManager()
        {

            _service = new MasterPasswordService();

        }

        public MasterPasswordManager(IMasterPasswordService service)
        {

            _service = service;

        }

        public bool CompareHash(byte[] newPassword, int userId)
        {

            var masterPassword = RetrieveByUserId(userId);

            var key = Hash.GenerateHash(newPassword, masterPassword.Salt, masterPassword.Iterations, 16);

            return Hash.CompareHash(key, masterPassword.Hash);

        }

        public bool CompareHash(byte[] newPassword, int userId, out byte[] key)
        {

            var masterPassword = RetrieveByUserId(userId);

            key = Hash.GenerateHash(newPassword, masterPassword.Salt, masterPassword.Iterations, 16);

            return Hash.CompareHash(key, masterPassword.Hash);

        }

        public MasterPassword RetrieveByUserId(int userId)
        {

            return _service.GetMasterPasswordById(userId);

        }

        public void Create(int UserId, string password)
        {

            var iterations = 1000;
            var salt = Hash.GenerateSalt(20);
            var hash = Hash.GenerateHash(Encoding.ASCII.GetBytes(password), salt, iterations, 16);

            _service.Create(hash, salt, iterations, UserId);           

        }



        public void Delete(int id)
        {

            _service.DeleteMasterPasswordById(id);

        }

        public void Update(int id, byte[] hash, byte[] salt, int iteration = 1000)
        {
            

            var mPassword = _service.GetMasterPasswordById(id);
            mPassword.Hash = hash; 
            mPassword.Salt = salt;
            mPassword.Iterations = iteration;
            _service.SaveChanges();
            

        }
    }
}
