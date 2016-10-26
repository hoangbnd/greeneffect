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
            ToTable("CreateOrder");
            HasKey(o => o.Id); 
            Property(o => o.OrderDate);
            Property(o => o.OrderName);
            Property(o => o.Note);
            Property(o => o.Reciever);
            Property(o => o.ProductsNumber);
            Property(o => o.UnitPrice);
            Property(o => o.Amount);
            Property(o => o.CustomersId);
            Property(o => o.UserId);
            Property(o => o.ProductsId);
            Property(o => o.RouteId);
            Property(o => o.CustomersRoutesId);
            Property(o => o.CustomersLocationId);
            Property(o => o.ObjectId);
            Property(o => o.ProductsGroupId);
            Property(o => o.LocationName);
            Property(o => o.Description);
            Property(o => o.Longitude);
            Property(o => o.Latitude);
            Property(o => o.Datetime);
            Property(o => o.Disable);
        }
    }
}
