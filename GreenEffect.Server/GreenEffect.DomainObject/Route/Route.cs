using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
namespace GreenEffect.DomainObject.Route
{  
        public class Route : BaseEntity
        {
           
            public string RouteCode { get; set; }
            public string RouteName { get; set; }
            public int RouteID { get; set; }
            public int UserID { get; set; }
            public DateTime DateTime { get; set; }

        }
}
