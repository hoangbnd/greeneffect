using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class ProductApiModel
    { 
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductGroupId { get; set; }
        public int Disable { get; set; }
        public DateTime Datetime { get; set; }
    }

    public class SearchProductApiModel
    {
        public string Keyword { get; set; }
        public bool ByGroup { get; set; }
        public ICollection<ProductApiModel> Products { get; set; }
        public ICollection<ProductGroupApiModel> Groups { get; set; }
        public int PageIndex { get; set; }
    }
}