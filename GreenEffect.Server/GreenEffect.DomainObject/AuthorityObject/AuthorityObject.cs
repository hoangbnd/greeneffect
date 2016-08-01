using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.DomainObject.AuthorityObject
{
    public class AuthorityObject:BaseEntity 
    {
        public string ObjectName { get; set; }
        public string ObjectImages { get; set; }
        public string ObjectUser { get; set; }
        public int IdenObject { get; set; }
        public int ObjectSystem { get; set; }
        public DateTime Datetime { get; set; }
    }
}
