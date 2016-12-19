﻿using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.User
{
    public class User:BaseEntity
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDt { get; set; }
        public int Op { get; set; }

        public virtual ICollection<Message.Message> Messages { get; set; }
    }
}
