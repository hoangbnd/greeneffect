﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;

namespace GreenEffect.DomainObject.Message
{
    public class Message : BaseEntity
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

        public virtual User.User User { get; set; }


    }
}
