using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreenEffect.Api.Models
{
    public class JsonModel<T> 
    {
        public bool IsSuccessful { get; set; }
        public string Messenger { get; set; }
        public T Data { get; set; }
    }
}