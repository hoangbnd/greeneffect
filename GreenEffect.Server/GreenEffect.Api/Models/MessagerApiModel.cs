using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class MessagerApiModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FromUser { get; set; }
        public string Messager { get; set; }
        public DateTime DateTime { get; set; }
    }
}