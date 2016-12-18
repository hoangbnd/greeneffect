using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
namespace GreenEffect.DomainObject.Products
{
    public class Product:BaseEntity
    { 
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductGroupId { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }

        public virtual ProductGroup ProductGroup { get; set; }
    }
}
