using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.Customers
{
    public class Customers : BaseEntity
    {
        public string CustomersId { get; set; } 
        public string CustomersName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int IdenCustomers { get; set; }
        public int IdenUser { get; set; }
        public int IdenRoute { get; set; }
        public DateTime Datetime { get; set; }
    }
}
