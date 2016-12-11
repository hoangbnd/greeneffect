using System;
using MVCCore;

namespace GreenEffect.DomainObject.Order
{
    public class OrderData:BaseEntity
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
        public string RouteCode { get; set; }
        public string RouteName { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string CustomersCode { get; set; }
        public string CustomersName { get; set; }
        public string ObjectName { get; set; }
        public string ProductsCode { get; set; }
        public string ProductsName { get; set; }
        public string UserName { get; set; }
        public DateTime Datetime { get; set; }
        public int Disable { get; set; }
    }
}
