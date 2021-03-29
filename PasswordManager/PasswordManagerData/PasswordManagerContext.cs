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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = PasswordManager;");


    }
}
