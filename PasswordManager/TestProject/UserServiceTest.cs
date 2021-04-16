using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PasswordManagerData;
using PasswordManagerData.Services;
using Microsoft.EntityFrameworkCore;


namespace TestProject
{
    public class UserServiceTest
    {

        private UserService _sut;
        private PasswordManagerContext _context;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

            var option = new DbContextOptionsBuilder<PasswordManagerContext>().UseInMemoryDatabase(databaseName: "InMemoryDb")
                .Options;

            _context = new PasswordManagerContext(option);
            _sut = new UserService(_context);

            _sut.CreateUser(new User()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "JohnDoe@Testing.com",

            });

            _sut.CreateUser(new User()
            {
                FirstName = "Jane",
                LastName = "Doe",
                EmailAddress = "JaneDoe@Testing.com",

            });
                        
        }

        [Test]
        [Category("Service Test")]
        [Category("Happy Path")]
        public void GivenANewUser_CreateUserAddItToDataBase()
        {

            var numberOfUserBefore = _context.Users.Count();

            var newUser = new User
            {

                FirstName = "James",
                LastName = "Smith",
                EmailAddress = "JamesSmith@Testing.com"

            };

            _sut.CreateUser(newUser);

            var userInDb = _sut.GetByEmail("JamesSmith@Testing.com");

            Assert.That(_context.Users.Count(), Is.EqualTo(numberOfUserBefore + 1));
            Assert.That(userInDb.EmailAddress, Is.EqualTo("JamesSmith@Testing.com"));

            _context.Users.Remove(userInDb);
            _context.SaveChanges();

        }

        [Test]
        [Category("Service Test")]
        [Category("Happy Path")]
        public void ReturnsAValidUser_WhenGetUserByIdIsCalled()
        {
            var user = _context.Users.
                Where(u => u.EmailAddress == "JohnDoe@Testing.com").FirstOrDefault();
            var result = _sut.GetUserById(user.Id);
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
            Assert.That(result.EmailAddress, Is.EqualTo("JohnDoe@Testing.com"));

        }

        [Test]
        [Category("Service Test")]
        [Category("Unhappy Path")]
        public void ReturnsAErrorUser_WhenGetUserByIdIsCalled()
        {


            Assert.That(() => _sut.GetUserById(-1), Throws.TypeOf<NullReferenceException>());

        }


        [Test]
        [Category("Service Test")]
        [Category("Happy Path")]
        public void ReturnsTrue_WhenAUserIsCurrentlyInTheDataBase()
        {

            var result = _sut.Exist("JohnDoe@Testing.com");

            Assert.That(result, Is.True);

        }

        [Test]
        [Category("Service Test")]
        [Category("Happy Path")]
        public void ReturnsAValidUser_WhenGetByEmailIsCalled()
        {

            var result = _sut.GetByEmail("JohnDoe@Testing.com");
            Assert.That(result.FirstName, Is.EqualTo("John"));
            Assert.That(result.LastName, Is.EqualTo("Doe"));
            Assert.That(result.EmailAddress, Is.EqualTo("JohnDoe@Testing.com"));

        }

        [Test]
        [Category("Service Test")]
        [Category("Happy Path")]
        public void GivenAUserId_RemoveItFromTheDataBase()
        {

            var user = _context.Users.
                Where(u => u.EmailAddress == "JohnDoe@Testing.com").
                FirstOrDefault();

            Assert.That(_context.Users, Does.Contain(user),
                "Test user Isn't in the database. it should be added in the OneTimeSetUp");

            _sut.RemoveUser(user.Id);

            Assert.That(_context.Users, Does.Not.Contain(user));

            //clean up
            _sut.CreateUser(new User()
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "JohnDoe@Testing.com",

            });
        }


    }
}
