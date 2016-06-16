using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class CustomersApiModel
    {
        public int Id { get; set; }
        public string CustomersId { get; set; }
        public string CustomersName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int IdenObj { get; set; }
        public int FUser { get; set; }
        public int FRoute { get; set; }
    }
}