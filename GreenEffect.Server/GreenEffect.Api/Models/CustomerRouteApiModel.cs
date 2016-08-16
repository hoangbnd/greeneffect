using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class CustomerRouteApiModel
    {
        public int Id { get; set; }
        public string CustomersCode { get; set; }
        public string CustomersName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int UserID { get; set; }
        public int CustomersID { get; set; }
        public int RouteID { get; set; }
        public int CustomersRoutesID { get; set; }
        public DateTime DateTime { get; set; }
    }
}