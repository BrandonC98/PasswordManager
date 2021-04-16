using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData.Services
{
    public class MasterPasswordService : IMasterPasswordService
    {

        private readonly PasswordManagerContext _context;

        public MasterPasswordService()
        {

            _context = new PasswordManagerContext();

        }

        public MasterPasswordService(PasswordManagerContext context)
        {

            _context = context;

        }

        public MasterPassword GetMasterPasswordByUserId(int masterPasswordId)
        {

            var masterPassword = _context.MasterPasswords.Where(mp => mp.UserId == masterPasswordId).FirstOrDefault();

            if (masterPassword == null) throw new NullReferenceException($"No master Password found with a userId of {masterPasswordId}");

            return masterPassword;

        }

        public void Create(byte[] hash, byte[] salt, int iterations, int userId)
        {

            _context.MasterPasswords.Add(new MasterPassword()
            {
                Hash = hash,
                Salt = salt,
                Iterations = iterations,
                UserId = userId
            });

            _context.SaveChanges();

        }

        public void DeleteMasterPasswordById(int id)
        {

            _context.RemoveRange(_context.MasterPasswords.Find(id));
            _context.SaveChanges();

        }

        public void SaveChanges() => _context.SaveChanges();

    }
}
