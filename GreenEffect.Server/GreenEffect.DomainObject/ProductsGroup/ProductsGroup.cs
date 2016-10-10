﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
namespace GreenEffect.DomainObject.ProductsGroup
{
    public class ProductsGroup: BaseEntity
    { 
        public string GroupCode { get; set; }
        public string GroupName { get; set; }
        public int ProductsGroupID { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }
    }
}
