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
        private MasterPassword _testMPassword;
        private Website _testWebsite;
        string encryptedPassword;
        private WebsiteManager _websiteManager;

        [SetUp]
        public void Setup()
        {

            _websiteManager = new WebsiteManager();

            using(var db = new PasswordManagerContext())
            {

                var selectedUser =
                    from u in db.users
                    where u.EmailAddress == "UnitTest@Testing.co.uk"
                    select u;

                db.users.RemoveRange(selectedUser);
                db.SaveChanges();

                db.users.Add(new User() { FirstName = "Unit", LastName = "Test", EmailAddress = "UnitTest@Testing.co.uk" });
                db.SaveChanges();

                _testUser = db.users.Where(u => u.EmailAddress == "UnitTest@Testing.co.uk").FirstOrDefault();

                var salt = Hash.GenerateSalt(20);
                var hashPassword = Hash.GenerateHash(Encoding.ASCII.GetBytes("Password123"), salt, 1000, 16);

                db.MasterPasswords.Add(new MasterPassword() { Hash = hashPassword, Salt = salt, Iterations = 1000, User = _testUser });
                db.SaveChanges();
                _testMPassword = db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault();

                encryptedPassword = SymmetricEncryption.Encrypt(Convert.ToBase64String(hashPassword), "YouTubePassword1");

                db.websites.Add(new Website() { Name = "YouTube", Username = "Username1", Password = encryptedPassword, Url = $"https://www.youtube.com/", UserId = _testUser.Id });
                db.SaveChanges();
                _testWebsite = db.websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault();



            };

        }

        [Test]
        public void WhenAWebsiteIsCreatedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {
                var website = db.websites.Find(_testWebsite.Id);
                db.websites.RemoveRange(website);
                db.SaveChanges();

                var numberOfWebsitesBefore = db.websites.Count();
                _websiteManager.Create(_testUser.Id, "Google", encryptedPassword, "Username1", $"https://www.youtube.com/");
                var numberOfWebsitesAfter = db.websites.Count();

                Assert.AreEqual(numberOfWebsitesBefore + 1, numberOfWebsitesAfter);
                _testWebsite = db.websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault();
            }

        }

        [Test]
        public void WhenAWebsiteIsDeletedTheDatabaseIsUpdated()
        {

            using (var db = new PasswordManagerContext())
            {

                var numberOfWebsitesBefore = db.websites.Count();
                _websiteManager.Delete(_testWebsite.Id);
                var numberOfWebsitesAfter = db.websites.Count();

                Assert.AreEqual(numberOfWebsitesBefore - 1, numberOfWebsitesAfter);
            }

        }

        [Test]
        public void WhenRequestedTheCorrectWebsiteIsReturned()
        {

            using(var db = new PasswordManagerContext())
            {

                var expectedWebsite = db.websites.Find(_testWebsite.Id);
                var website = _websiteManager.Retrieve(_testWebsite.Id);

                Assert.AreEqual(expectedWebsite.Id, website.Id);

            }

        }

        [Test]
        public void WhenRetrieveAllIsCalledWillReturnAllWebsites()
        {

            using (var db = new PasswordManagerContext())
            {

                var count = db.websites.Count();
                List<Website> websites = _websiteManager.GetAll();

                Assert.AreEqual(count, websites.Count());

            }

        }

        [Test]
        public void WhenUpdatedTheDatabaseWillReflectTheChanges()
        {

            using (var db = new PasswordManagerContext())
            {

                var expectedUsername = "NewUsername";
                _websiteManager.Update(_testWebsite.Id, username:expectedUsername);

                Assert.AreEqual(expectedUsername, db.websites.Find(_testWebsite.Id).Username);

            }

        }

        [TearDown]
        public void TearDown()
        {

            using(var db = new PasswordManagerContext())
            {

                db.MasterPasswords.RemoveRange(db.MasterPasswords.Where(mp => mp.UserId == _testUser.Id).FirstOrDefault());
                db.SaveChanges();

                if(db.websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault() != null)
                {

                    db.websites.RemoveRange(db.websites.Where(w => w.UserId == _testUser.Id).FirstOrDefault());
                    db.SaveChanges();


                }

                var user = db.users.Find(_testUser.Id);
                db.users.RemoveRange(user);
                db.SaveChanges();


            }

        }

    }
}
