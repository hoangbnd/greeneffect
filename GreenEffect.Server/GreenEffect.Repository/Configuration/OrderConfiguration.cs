using GreenEffect.DomainObject.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Repository.EF.Configuration
{
    public class OrderConfiguration: EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Order");
            HasKey(o => o.Id); 
            Property(o => o.CustomerId);
            Property(o => o.UserId);
            Property(o => o.Longitude);
            Property(o => o.Latitude);
            Property(o => o.Datetime);
            Property(o => o.Disable);
           
        }
    }
}
