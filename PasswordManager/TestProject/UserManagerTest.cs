using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PasswordManager;
using PasswordManagerData;

namespace TestProject
{
    class UserManagerTest
    {

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

            }
        }

        [Test]
        public void WhenAUserIsDeletedTheDatabaseIsUpdated()
        {
            var userManager = new UserManager();

            using (var db = new PasswordManagerContext())
            {

                var numberOfUsersBefore = db.Users.Count();
                userManager.Delete(_testUser.Id);
                var numberOfUsersAfter = db.Users.Count();

                Assert.AreEqual(numberOfUsersBefore - 1, numberOfUsersAfter);

            }

        }

        [Test]
        public void WhenAUserIsCreatedTheDatabaseIsUpdated()
        {
            var userManager = new UserManager();

            using (var db = new PasswordManagerContext())
            {
                var numberOfUsersBefore = db.Users.Count();
                userManager.Create("Brandon", "Campbell", "MyEmail@Emails.com");
                var numberOfUsersAfter = db.Users.Count();

                Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);

            }

        }

        [Test]
        public void WhenAUserIsRetrivedItIsTheCorrectUser()
        {
            var userManager = new UserManager();

            using (var db = new PasswordManagerContext())
            {
                var expectedUser = db.Users.Find(_testUser.Id);
                var user = userManager.Retrieve(_testUser.Id);

                Assert.AreEqual(expectedUser.Id, user.Id);

            }

        }

        [Test]
        public void WhenAUserIsRetrivedByEmailItIsTheCorrectUser()
        {
            var userManager = new UserManager();

            using (var db = new PasswordManagerContext())
            {
                var expectedUser = db.Users.Find(_testUser.Id);
                var user = userManager.Retrieve(_testUser.EmailAddress);

                Assert.AreEqual(expectedUser.Id, user.Id);

            }

        }

        [Test]
        public void WhenCheckedToSeeIfAUserExistsItsAlwayCorrect()
        {
            var userManager = new UserManager();

            using (var db = new PasswordManagerContext())
            {
                var expectedUser = db.Users.Find(_testUser.Id);
                var answer = userManager.Exist(_testUser.EmailAddress);

                Assert.AreEqual(true, answer);

            }

        }

        [Test]
        public void WhenAUserIsUpdatedTheDatabaseWillShowTheChange()
        {

            var userManager = new UserManager();

            using (var db = new PasswordManagerContext())
            {
                userManager.Update(_testUser.Id, newLastName: "Smith");
                Assert.AreEqual("Smith", db.Users.Find(_testUser.Id).LastName);

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

                db.Users.RemoveRange(selectedUser);
                db.SaveChanges();

                db.Users.RemoveRange(db.Users.Where(u => u.EmailAddress == "MyEmail@Emails.com"));
                db.SaveChanges();

            }
        }

    }
}
