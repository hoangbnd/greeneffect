using System;
using MVCCore;

namespace GreenEffect.DomainObject.Products
{
    public class ProductGroup: BaseEntity
    { 
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }
    }
}
