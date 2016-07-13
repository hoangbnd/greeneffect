using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
namespace GreenEffect.DomainObject.CustomersRoutes
{
    public class CustomersRoutes : BaseEntity
    {
        public int IdenCustomers { get; set; }
        public int IdenRoute { get; set; }
        public int IdenCustomersRoutes { get; set; }
        public DateTime DateTime { get; set; }
    }
}
