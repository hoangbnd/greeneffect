﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class CustomerRouteApiModel
    {
        public int Id { get; set; } 
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int RouteId { get; set; }
        public DateTime DateTime { get; set; }
    }
}