using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class CustomersImagesApiModel
    {
        public int Id { get; set; }
        public int CustomersID { get; set; }
        public int CustomersImagesID { get; set; }
        public int UserID { get; set; }
        public string Images { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsSuccessful { get; set; }
        public string Messenger { get; set; }
    }
}