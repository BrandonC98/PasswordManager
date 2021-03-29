using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData
{
    public partial class Website
    {

        public Website()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }



    }
}
