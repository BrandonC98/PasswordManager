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
    class UserManagerTest
    {

        private UserManager _userManager;
        private User testUser;

        [SetUp]
        public void Setup()
        {
            _userManager = new UserManager();

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

            }
        }

        [Test]
        public void WhenAUserIsDeletedTheDatabaseIsUpdated()
        {

            using(var db = new PasswordManagerContext())
            {

                var numberOfUsersBefore = db.users.Count();
                _userManager.Delete(testUser.Id);
                var numberOfUsersAfter = db.users.Count();

                Assert.AreEqual(numberOfUsersBefore - 1, numberOfUsersAfter);

            }

        }

        [Test]
        public void WhenAUserIsCreatedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {
                var numberOfUsersBefore = db.users.Count();
                _userManager.Create("Brandon", "Campbell", "MyEmail@Emails.com");
                var numberOfUsersAfter = db.users.Count();

                Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);

            }

        }

        [Test]
        public void WhenAUserIsRetrivedItIsTheCorrectUser()
        {

            using (var db = new PasswordManagerContext())
            {
                var expectedUser = db.users.Find(testUser.Id);
                var user = _userManager.Retrive(testUser.Id);

                Assert.AreEqual(expectedUser.Id, user.Id);

            }

        }

        [Test]
        public void WhenAUserIsUpdatedTheDatabaseWillShowTheChange()
        {

            using (var db = new PasswordManagerContext())
            {
                _userManager.Create("Brandon", "Campbell", "MyEmail@Emails.com");
                var user = db.users.Where(u => u.EmailAddress == "MyEmail@Emails.com").FirstOrDefault();
                _userManager.Update(user.Id, newLastName: "Smith");

                
                Assert.AreEqual("Smith", db.users.Find(user.Id).LastName);

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

                db.users.RemoveRange(selectedUser);
                db.SaveChanges();

                db.users.RemoveRange(db.users.Where(u => u.EmailAddress == "MyEmail@Emails.com"));
                db.SaveChanges();

            }
        }

    }
}
