using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.CustomersImages
{
    public class CustomersImages : BaseEntity
    {
        
        public int CustomersID { get; set; }
        public int CustomersImagesID { get; set; }
        public int UserID { get; set; }
        public string Images { get; set; }
        public DateTime DateTime { get; set; }
    }
}
