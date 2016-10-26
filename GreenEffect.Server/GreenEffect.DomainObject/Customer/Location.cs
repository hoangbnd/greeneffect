using System;
using MVCCore;

namespace GreenEffect.DomainObject.Customer
{
    public class Location: BaseEntity
    {
        public int CustomerId { get; set; }
        public string LocationName { get; set; }
        public string Description { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime DateTime { get; set; }
        public bool Disable { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
