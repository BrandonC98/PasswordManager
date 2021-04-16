using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PasswordManagerData;
using PasswordManagerData.Services;
using Microsoft.EntityFrameworkCore;
using PasswordManager;
using Moq;

namespace TestProject
{
    public class MasterPasswordServiceTest
    {

        private MasterPasswordService _sut;
        private PasswordManagerContext _context;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            var option = new DbContextOptionsBuilder<PasswordManagerContext>().UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            _context = new PasswordManagerContext(option);
            _sut = new MasterPasswordService(_context);

            _context.Users.Add(new User()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "JohnDoe@Testing.com",

            });
            _context.SaveChanges();
            _context.Users.Add(new User()
            {
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "JaneDoe@Testing.com",

            });
            _context.SaveChanges();

            var salt1 = Hash.GenerateSalt(20);
            var hashPassword1 = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password123"), salt1, 1000, 16);

            var salt2 = Hash.GenerateSalt(20);
            var hashPassword2 = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password!_"), salt2, 1000, 16);
            _sut.Create(hashPassword1, salt1, 1000, _context.Users.Where(u => u.EmailAddress == "JohnDoe@Testing.com").FirstOrDefault().Id);
            _sut.Create(hashPassword2, salt2, 1000, _context.Users.Where(u => u.EmailAddress == "JaneDoe@Testing.com").FirstOrDefault().Id);

        }

        [Test]
        [Category("Happy Path")]
        [Category("Service Test")]
        public void ReturnsAValidMasterPassword_WhenGetMasterPasswordByIdIsCalled()
        {
            var userId = _context.Users.Where(u => u.EmailAddress == "JohnDoe@Testing.com").FirstOrDefault().Id;
            var masterPassword = _context.MasterPasswords.Where(mp => mp.UserId == userId).FirstOrDefault();

            var result = _sut.GetMasterPasswordById(masterPassword.Id);

            Assert.That(Hash.CompareHash(result.Hash, masterPassword.Hash));

        }

    }
}
