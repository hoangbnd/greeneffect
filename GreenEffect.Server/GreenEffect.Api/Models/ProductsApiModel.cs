﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class ProductsApiModel
    { 
        public int Id { get; set; }
        public string ProductsID { get; set; }
        public string ProductsName { get; set; }
        public decimal UnitPrice { get; set; }
        public int IdenProductsGroup { get; set; }
        public int IdenProducts { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }
        public bool IsSuccessful { get; set; }
        public string Messenger { get; set; }
    }
}