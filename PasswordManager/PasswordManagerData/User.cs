using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData
{
    public partial class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        //public List<AccessHistory> AccessHistories { get; set; }
        public ICollection<Website> Websites { get; set; }

    }
}
