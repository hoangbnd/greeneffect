using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class LocationApiModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public bool Disable { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
    }
}