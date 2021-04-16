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

        [TestCase("Password123", true)]
        [TestCase("321drowssad", false)]
        public void ReturnsTrueIfTheHashMatches(string hash, bool expected)
        {
            var masterPasswordManager = new MasterPasswordManager();

            var actual = masterPasswordManager.CompareHash(Encoding.ASCII.GetBytes(hash), _testUser.Id);

            Assert.AreEqual(expected, actual);

        }

        [TestCase("Password123")]
        [TestCase("Password")]
        [TestCase("+=-_Password_354")]
        public void ReturnsTheCorrectHash(string password)
        {
            var masterPasswordManager = new MasterPasswordManager();
            var hash = Hash.GenerateHash(Encoding.ASCII.GetBytes(password), _testMPassword.Salt, _testMPassword.Iterations, 16);

            masterPasswordManager.CompareHash(Encoding.ASCII.GetBytes(password), _testUser.Id, out byte[] key);

            Assert.AreEqual(true, Hash.CompareHash(hash, key));

        }

        [Test]
        public void WhenAPasswordIsRetrivedByIdItIsTheCorrectPassword()
        {
            var masterPasswordManager = new MasterPasswordManager();

            using (var db = new PasswordManagerContext())
            {
                var expectedPassword = db.MasterPasswords.Find(_testMPassword.Id);
                var actualPassword = masterPasswordManager.RetrieveByUserId(_testUser.Id);

                Assert.AreEqual(expectedPassword.Hash, actualPassword.Hash);

            }

        }

        [Test]
        public void WhenAPasswordIsCreatedTheDatabaseIsUpdated()
        {
            var masterPasswordManager = new MasterPasswordManager();

            using (var db = new PasswordManagerContext())
            {
                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id));
                db.SaveChanges();
                var numberOfUsersBefore = db.MasterPasswords.Count();
                masterPasswordManager.Create(_testUser.Id, "ThisIsAStrongPassword123");
                var numberOfUsersAfter = db.MasterPasswords.Count();

                Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);

            }

        }

        [Test]
        public void WhenAPasswordIsDeletedTheDatabaseIsUpdated()
        {
            var masterPasswordManager = new MasterPasswordManager();

            using (var db = new PasswordManagerContext())
            {

                var numberOfPasswordsBefore = db.MasterPasswords.Count();
                masterPasswordManager.Delete(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault().Id);
                var numberOfPasswordsAfter = db.MasterPasswords.Count();

                Assert.AreEqual(numberOfPasswordsBefore - 1, numberOfPasswordsAfter);

            }

        }

        [Ignore("")]
        [Test]
        public void WhenAPasswordIsUpdatedTheDatabaseWillShowTheChange()
        {
            var masterPasswordManager = new MasterPasswordManager();

            using (var db = new PasswordManagerContext())
            {
                var oldHash = _testMPassword.Hash;
                var salt = Hash.GenerateSalt(20);
                var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("ThisIsAStrongPassword123"), salt, 1000, 16);
                masterPasswordManager.Update(_testMPassword.Id, hashPassword, salt);
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