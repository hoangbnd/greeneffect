using System;
using System.Collections.Generic;
using MVCCore;

namespace GreenEffect.DomainObject.Customer
{
    public class Customer : BaseEntity
    {
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public DateTime DateTime { get; set; }

        //public virtual ICollection<Location> Locations { get; set; }
    }
}
