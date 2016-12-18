using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class OrderApiModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public ICollection<OrderItemApiModel> OrderItems { get; set; }
        public string Images { get; set; }
        public ICollection<MultipartFileData> Files2 { get; set; }
        public ICollection<HttpPostedFileBase> Files { get; set; }
        public DateTime Datetime { get; set; }
        public int Disable { get; set; }
    }
}