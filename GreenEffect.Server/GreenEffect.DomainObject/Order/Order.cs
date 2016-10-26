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
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public string Note { get; set; }
        public string Reciever { get; set; }
        public decimal ProductsNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public int CustomersId { get; set; }
        public int UserId { get; set; }
        public int ProductsId { get; set; }
        public int RouteId { get; set; }
        public int CustomersRoutesId { get; set; }
        public int CustomersLocationId { get; set; }
        public int ObjectId { get; set; }       
        public int ProductsGroupId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }   
        public DateTime Datetime { get; set; }
        public int Disable { get; set; }
    }
}
