using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PasswordManagerData
{
    public class PasswordManagerContext :   DbContext
    {

        public DbSet<Website> Websites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MasterPassword> MasterPasswords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PasswordManager;");

        public PasswordManagerContext()
        {

        }

        public PasswordManagerContext(DbContextOptions<PasswordManagerContext> option) : base(option)
        {
        }

    }
}
