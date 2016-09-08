using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;

namespace GreenEffect.DomainObject.Messager
{
    public class Messager : BaseEntity
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string FromUser { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }

    }
}
