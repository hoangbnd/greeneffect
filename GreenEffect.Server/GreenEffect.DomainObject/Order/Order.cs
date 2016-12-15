using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
namespace GreenEffect.DomainObject.Order
{
    public class Order:BaseEntity
    {
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }   
        public string Images { get; set; }
        public DateTime Datetime { get; set; }
        public int Disable { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
