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

        private MasterPassword _testMPassword;
        private User _testUser;

        [SetUp]
        public void Setup()
        {

            using (var db = new PasswordManagerContext())
            {

                var selectedUser =
                    from u in db.Users
                    where u.EmailAddress == "UnitTest@Testing.co.uk"
                    select u;

                    db.Users.RemoveRange(selectedUser);
                    db.SaveChanges();

                    db.Users.Add(new User() { FirstName = "Unit", LastName = "Test", EmailAddress = "UnitTest@Testing.co.uk" });
                    db.SaveChanges();

                    _testUser = db.Users.Where(u => u.EmailAddress == "UnitTest@Testing.co.uk").FirstOrDefault();
                
              
                
                    var salt = Hash.GenerateSalt(20);
                    var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password123"), salt, 1000, 16);

                    db.MasterPasswords.Add(new MasterPassword() { Hash = hashPassword, Salt = salt, Iterations = 1000, UserId = _testUser.Id });
                    db.SaveChanges();
                    _testMPassword = db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault();
                

            }
        }

        [Test]
        public void WhenAPasswordIsRetrivedItIsTheCorrectPassword()
        {

            using (var db = new PasswordManagerContext())
            {
                var expectedPassword = db.MasterPasswords.Find(_testMPassword.Id);
                var actualPassword = MasterPasswordManager.Retrieve(_testMPassword.Id);

                Assert.AreEqual(expectedPassword.Hash, actualPassword.Hash);

            }

        }

        [Test]
        public void WhenAPasswordIsRetrivedByIdItIsTheCorrectPassword()
        {

            using (var db = new PasswordManagerContext())
            {
                var expectedPassword = db.MasterPasswords.Find(_testMPassword.Id);
                var actualPassword = MasterPasswordManager.RetrieveByUserId(_testUser.Id);

                Assert.AreEqual(expectedPassword.Hash, actualPassword.Hash);

            }

        }

        [Test]
        public void WhenAPasswordIsCreatedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {
                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id));
                db.SaveChanges();
                var numberOfUsersBefore = db.MasterPasswords.Count();
                MasterPasswordManager.Create(_testUser.Id, "ThisIsAStrongPassword123");
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
                MasterPasswordManager.Delete(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault().Id);
                var numberOfPasswordsAfter = db.MasterPasswords.Count();

                Assert.AreEqual(numberOfPasswordsBefore - 1, numberOfPasswordsAfter);

            }

        }

        [Test]
        public void WhenAPasswordIsUpdatedTheDatabaseWillShowTheChange()
        {

            using (var db = new PasswordManagerContext())
            {
                var oldHash = _testMPassword.Hash;
                var salt = Hash.GenerateSalt(20);
                var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("ThisIsAStrongPassword123"), salt, 1000, 16);
                MasterPasswordManager.Update(_testMPassword.Id, hashPassword, salt);
                Assert.AreEqual(false, Hash.CompareHash(db.MasterPasswords.Find(_testMPassword.Id).Hash, oldHash));
            }

        }

        [TearDown]
        public void TearDown()
        {
            using (var db = new PasswordManagerContext())
            {

                var selectedUser =
                    from u in db.Users
                    where u.EmailAddress == "UnitTest@Testing.co.uk"
                    select u;

                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id));
                db.Users.RemoveRange(selectedUser);
                db.SaveChanges();

            }
        }

    }
}