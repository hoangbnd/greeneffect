using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class UserApiModel 
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Datetime { get; set; }
        public int LocTheo { get; set; }
        public int IdenObj { get; set; }
    }
}