using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class ProductGroupApiModel
    { 
        public int Id { get; set; }
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }
    }
}