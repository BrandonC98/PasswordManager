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

        public DbSet<Website> websites { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<AccessHistory> AccessHistories { get; set; }
        public DbSet<Blacklist> Blacklists { get; set; }
        public DbSet<MasterPassword> MasterPasswords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PasswordManager;");


    }
}
