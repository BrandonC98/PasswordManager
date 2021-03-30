using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData
{
    public partial class AccessHistory
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime time { get; set; }
        public DateTime Date { get; set; }
        public string IpAddress { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
}