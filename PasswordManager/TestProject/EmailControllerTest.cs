using NUnit.Framework;
using PasswordManager;
using System;
using System.Linq;
using System.Text;
using PasswordManagerData;

namespace TestProject
{
    class EmailControllerTest
    {

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


            }
        }

        [TestCase("emailAddress", false)]
        [TestCase("emailAddress@hotmail.co.uk", true)]
        public void CorrectlyChecksIfAEmailIsOfTheValidFormat(string email, bool expected)
        {

            var emailController = new EmailController();

            var actual = emailController.IsValidEmail(email);

            Assert.AreEqual(expected, actual);

        }

        [TestCase("UnitTest@Testing.co.uk", true)]
        [TestCase("ShouldntPass@Testing.co.uk", false)]
        public void CorrectlyChecksIfAEmailIsIsStored(string email, bool expected)
        {

            var emailController = new EmailController();

            var actual = emailController.IsEmailInUse(email);

            Assert.AreEqual(expected, actual);

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

            }
        }

    }
}
