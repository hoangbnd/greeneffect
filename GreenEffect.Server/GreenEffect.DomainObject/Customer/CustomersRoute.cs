using System;
using MVCCore;

namespace GreenEffect.DomainObject.Customer
{
    public class CustomerRoute : BaseEntity
    {
        public string CustomersCode { get; set; }
        public string CustomersName { get; set; }
        public string Adress { get; set; } 
        public string Phone { get; set; }
        public int UserId { get; set; }
        public int CustomersId { get; set; }
        public int RouteId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
