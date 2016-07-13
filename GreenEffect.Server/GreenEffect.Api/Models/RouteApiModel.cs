using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class RouteApiModel
    {
        public int Id { get; set; }
        public string RouteId { get; set; }
        public string RouteName { get; set; }
        public int IdenRoute { get; set; }
        public int IdenUser { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsSuccessful { get; set; }
        public string Messenger { get; set; }
    }
}