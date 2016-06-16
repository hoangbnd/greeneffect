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
        public string Datetime { get; set; }
        public int FUser { get; set; }
        public int FRoute { get; set; }
        public int IdenObj { get; set; }
 
    }
}
