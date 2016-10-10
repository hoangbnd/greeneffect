using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class CustomersApiModel
    { 
        public int Id { get; set; }
        public string CustomersCode { get; set; }
        public string CustomersName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int CustomersID { get; set; }
        public int UserID { get; set; }
        public int RouteID { get; set; }
        public DateTime Datetime { get; set; }
        public bool IsSuccessful { get; set; }
        public string Messenger { get; set; }
    }
}