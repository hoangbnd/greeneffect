﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class OrderDataApiModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public string Note { get; set; }
        public string Reciever { get; set; }
        public decimal ProductsNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public int IdenCustomers { get; set; }
        public int IdenUser { get; set; }
        public int IdenProducts { get; set; }
        public int IdenRoute { get; set; }
        public int IdenCustomersRoutes { get; set; }
        public int IdenCustomersLocation { get; set; }
        public int IdenObject { get; set; }
        public int IdenProductsGroup { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string RouteId { get; set; }
        public string RouteName { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string CustomersId { get; set; }
        public string CustomersName { get; set; }
        public string ObjectName { get; set; }
        public string ProductsID { get; set; }
        public string ProductsName { get; set; }
        public string UserName { get; set; }
        public DateTime Datetime { get; set; }
        public int Disable { get; set; }
    }
}