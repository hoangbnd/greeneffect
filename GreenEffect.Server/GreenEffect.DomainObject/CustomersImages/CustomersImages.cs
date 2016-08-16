using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.CustomersImages
{
    public class CustomersImages
    {
        public int ID { get; set; }
        public int CustomersID { get; set; }
        public int CustomersImagesID { get; set; }
        public int UserID { get; set; }
        public string Images { get; set; }
        public DateTime DateTime { get; set; }
    }
}
