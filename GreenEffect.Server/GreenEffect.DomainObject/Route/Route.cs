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
           
            public string RouteId { get; set; }
            public string RouteName { get; set; }
            public int IdenRoute { get; set; }
            public int IdenUser { get; set; }
            public DateTime DateTime { get; set; }

        }
}
