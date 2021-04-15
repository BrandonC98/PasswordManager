using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData.Services
{
    public interface IMasterPasswordService
    {

        public MasterPassword GetMasterPasswordById(int masterPasswordId);

        public void Create(byte[] hash, byte[] salt, int iterations, int userId);

        public void DeleteMasterPasswordById(int id);

        public void SaveChanges();


    }
}
