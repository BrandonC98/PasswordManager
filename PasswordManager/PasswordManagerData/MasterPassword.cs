using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData
{
    public partial class MasterPassword
    {

        public MasterPassword() {   }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Hash { get; set; }
        public int Iterations { get; set; }

    }
}
