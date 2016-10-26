using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Order;

namespace GreenEffect.Repository.EF.Configuration
{
    public class OrderDataConfigurations : EntityTypeConfiguration<OrderData>
    {
        public OrderDataConfigurations()
        {
            ToTable("viewall_order");
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
            Property(o => o.RouteCode);
            Property(o => o.RouteName);
            Property(o => o.GroupCode);
            Property(o => o.GroupName);
            Property(o => o.Phone);
            Property(o => o.Adress);
            Property(o => o.CustomersCode);
            Property(o => o.CustomersName);
            Property(o => o.ObjectName);
            Property(o => o.ProductsId);
            Property(o => o.ProductsName);
            Property(o => o.UserName);
            Property(o => o.Datetime);
            Property(o => o.Disable);
        }
    }
}
