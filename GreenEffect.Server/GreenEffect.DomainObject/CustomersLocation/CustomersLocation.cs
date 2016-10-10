using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.CustomersLocation
{
    public class CustomersLocation
    {
        public int Id { get; set; }
        public int CustomersID { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int UserID { get; set; }
        public int CustomersLocationID { get; set; }
        public DateTime DateTime { get; set; }
        public int Disable { get; set; }
    }
}
