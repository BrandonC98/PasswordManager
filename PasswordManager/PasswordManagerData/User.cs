﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManagerData
{
    public partial class User
    {

        public User()
        {
            Websites = new HashSet<Website>();
            AccessHistories = new HashSet<AccessHistory>();
            Blacklists = new HashSet<Blacklist>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public virtual MasterPassword MasterPassword { get; set; }
        public virtual ICollection<Website> Websites { get; set; }
        public virtual ICollection<AccessHistory> AccessHistories { get; set; }
        public virtual ICollection<Blacklist> Blacklists { get; set; } 

    }
}

