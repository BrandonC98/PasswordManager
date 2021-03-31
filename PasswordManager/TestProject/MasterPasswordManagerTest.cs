using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PasswordManager;
using PasswordManagerData;

namespace TestProject
{
    class MasterPasswordManagerTest
    {

        private MasterPasswordManager _mPasswordManager;
        private MasterPassword testMPassword;
        private User testUser;

        [SetUp]
        public void Setup()
        {
            _mPasswordManager = new MasterPasswordManager();

            using (var db = new PasswordManagerContext())
            {

                var selectedUser =
                    from u in db.users
                    where u.EmailAddress == "UnitTest@Testing.co.uk"
                    select u;

                db.users.RemoveRange(selectedUser);
                db.SaveChanges();

                db.users.Add(new User() { FirstName = "Unit", LastName = "Test", EmailAddress = "UnitTest@Testing.co.uk" });
                db.SaveChanges();

                testUser = db.users.Where(u => u.EmailAddress == "UnitTest@Testing.co.uk").FirstOrDefault();
                
              
                
                    var salt = Hash.GenerateSalt(20);
                    var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password123"), salt, 1000, 16);

                    db.MasterPasswords.Add(new MasterPassword() { Hash = hashPassword, Salt = salt, Iterations = 1000, User = testUser });
                    db.SaveChanges();
                    testMPassword = db.MasterPasswords.Where(mp => mp.UserId == testUser.Id).FirstOrDefault();
                

            }
        }

        [Test]
        public void WhenAPasswordIsRetrivedItIsTheCorrectPassword()
        {

            using (var db = new PasswordManagerContext())
            {
                var expectedPassword = db.MasterPasswords.Find(testMPassword.Id);
                var actualPassword = _mPasswordManager.Retrieve(testMPassword.Id);

                Assert.AreEqual(expectedPassword.Hash, actualPassword.Hash);

            }

        }

        [Test]
        public void WhenAPasswordIsCreatedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {
                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == testUser.Id));
                db.SaveChanges();
                var salt = Hash.GenerateSalt(20);
                var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("ThisIsAStrongPassword123"), salt, 1000, 16);
                var numberOfUsersBefore = db.MasterPasswords.Count();
                _mPasswordManager.Create(testUser.Id, salt, hashPassword, 1000);
                var numberOfUsersAfter = db.MasterPasswords.Count();

                Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);

            }

        }

        [Test]
        public void WhenAPasswordIsDeletedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {

                var numberOfPasswordsBefore = db.MasterPasswords.Count();
                _mPasswordManager.Delete(db.MasterPasswords.Where(mp => mp.UserId == testUser.Id).FirstOrDefault().Id);
                var numberOfPasswordsAfter = db.MasterPasswords.Count();

                Assert.AreEqual(numberOfPasswordsBefore - 1, numberOfPasswordsAfter);

            }

        }

        [Test]
        public void WhenAPasswordIsUpdatedTheDatabaseWillShowTheChange()
        {

            using (var db = new PasswordManagerContext())
            {
                var oldHash = testMPassword.Hash;
                var salt = Hash.GenerateSalt(20);
                var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("ThisIsAStrongPassword123"), salt, 1000, 16);
                _mPasswordManager.Update(testMPassword.Id, hashPassword, salt);
                Assert.AreEqual(false, Hash.CompareHash(db.MasterPasswords.Find(testMPassword.Id).Hash, oldHash));
            }

        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new PasswordManagerContext())
            {

                var selectedUser =
                    from u in db.users
                    where u.EmailAddress == "UnitTest@Testing.co.uk"
                    select u;

                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == testUser.Id));
                db.users.RemoveRange(selectedUser);
                db.SaveChanges();

            }
        }

    }
}