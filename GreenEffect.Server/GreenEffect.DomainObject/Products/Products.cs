using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
namespace GreenEffect.DomainObject.Products
{
    public class Products:BaseEntity
    {
        public string ProductsID { get; set; }
        public string ProductsName { get; set; }
        public decimal UnitPrice { get; set; }
        public int IdenProductsGroup { get; set; }
        public int IdenProducts { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }
    }
}
