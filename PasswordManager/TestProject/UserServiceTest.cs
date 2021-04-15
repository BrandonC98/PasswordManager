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

            //var option = new DbContextOptionsBuilder<PasswordManagerContext>().UseInMemoryDatabase(databaseName: "Example_DB")
            //    .Options;

        }

    }
}
