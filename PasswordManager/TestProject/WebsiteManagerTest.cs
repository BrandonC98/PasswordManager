using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PasswordManagerData;
using PasswordManager;

namespace TestProject
{
    class WebsiteManagerTest
    {

        private User _testUser;
        private Website _testWebsite;
        private string _encryptedPassword;
        private int _masterPasswordId;

        [SetUp]
        public void Setup()
        {


            using(var db = new PasswordManagerContext())
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
                var testMPassword = db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault();
                _masterPasswordId = testMPassword.Id;

                _encryptedPassword = SymmetricEncryption.Encrypt(Convert.ToBase64String(hashPassword), "YouTubePassword1");

                db.Websites.Add(new Website() { Name = "YouTube", Username = "Username1", Password = _encryptedPassword, Url = $"https://www.youtube.com/", UserId = _testUser.Id });
                db.SaveChanges();
                _testWebsite = db.Websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault();



            };

        }

        [Test]
        public void WhenAWebsiteIsCreatedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {
                var website = db.Websites.Find(_testWebsite.Id);
                db.Websites.RemoveRange(website);
                db.SaveChanges();

                var numberOfWebsitesBefore = db.Websites.Count();
                WebsiteManager.Create(_testUser.Id, "Google", _encryptedPassword, "Username1", $"https://www.youtube.com/");
                var numberOfWebsitesAfter = db.Websites.Count();

                Assert.AreEqual(numberOfWebsitesBefore + 1, numberOfWebsitesAfter);
                _testWebsite = db.Websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault();
            }

        }

        [Test]
        public void WhenAWebsiteIsDeletedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {

                var numberOfWebsitesBefore = db.Websites.Count();
                WebsiteManager.Delete(_testWebsite.Id);
                var numberOfWebsitesAfter = db.Websites.Count();

                Assert.AreEqual(numberOfWebsitesBefore - 1, numberOfWebsitesAfter);
            }

        }

        [Test]
        public void WhenRequestedTheCorrectWebsiteIsReturned()
        {

            using(var db = new PasswordManagerContext())
            {

                var expectedWebsite = db.Websites.Find(_testWebsite.Id);
                var website = WebsiteManager.Retrieve(_testWebsite.Id);

                Assert.AreEqual(expectedWebsite.Id, website.Id);

            }

        }

        [Test]
        public void WhenRetrieveAllIsCalledWillReturnAllWebsitesForThatUser()
        {

            using (var db = new PasswordManagerContext())
            {

                var count = db.Websites.Count(u => u.UserId == _testUser.Id);
                List<Website> websites = WebsiteManager.GetAll(_testUser.Id);

                Assert.AreEqual(count, websites.Count());

            }

        }

        [Test]
        public void WhenUpdatedTheDatabaseWillReflectTheChanges()
        {

            using (var db = new PasswordManagerContext())
            {

                var expectedUsername = "NewUsername";
                WebsiteManager.Update(_testWebsite.Id, username:expectedUsername);

                Assert.AreEqual(expectedUsername, db.Websites.Find(_testWebsite.Id).Username);

            }

        }

        [TearDown]
        public void TearDown()
        {

            using(var db = new PasswordManagerContext())
            {

                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault());
                db.SaveChanges();

                if(db.Websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault() != null)
                {

                    db.Websites.RemoveRange(db.Websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault());
                    db.SaveChanges();


                }

                var user = db.Users.Find(_testUser.Id);
                db.Users.RemoveRange(user);
                db.SaveChanges();


            }

        }

    }
}
