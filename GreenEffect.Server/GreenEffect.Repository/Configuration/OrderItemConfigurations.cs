using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Order;

namespace GreenEffect.Repository.EF.Configuration
{
    public class OrderItemConfigurations : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemConfigurations()
        {
            ToTable("OrderItem");
            HasKey(o => o.Id);
            Property(o => o.OrderId);
            Property(o => o.ProductId);
            Property(o => o.Quantity);

            HasRequired(o => o.Order).WithMany(i => i.OrderItems).HasForeignKey(o => o.OrderId);
        }
    }
}
