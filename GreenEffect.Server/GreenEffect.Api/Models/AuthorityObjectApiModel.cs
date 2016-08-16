using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class AuthorityObjectApiModel
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string ObjectImages { get; set; }
        public string ObjectUser { get; set; }
        public int ObjectID { get; set; }
        public int ObjectSystem { get; set; }
        public DateTime Datetime { get; set; }
    }
}