﻿using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.Customers
{
    public class Customers : BaseEntity
    {
        public string CustomersCode { get; set; }
        public string CustomersName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int CustomersID { get; set; }
        public int UserID { get; set; }
        public int RouteID { get; set; }
        public DateTime Datetime { get; set; }
    }
}
