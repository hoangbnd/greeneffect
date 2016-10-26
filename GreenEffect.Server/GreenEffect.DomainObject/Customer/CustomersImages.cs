using System;
using MVCCore;

namespace GreenEffect.DomainObject.Customer
{
    public class CustomersImages : BaseEntity
    {
        
        public int CustomersId { get; set; }
        public int UserId { get; set; }
        public string Images { get; set; }
        public DateTime DateTime { get; set; }
    }
}
