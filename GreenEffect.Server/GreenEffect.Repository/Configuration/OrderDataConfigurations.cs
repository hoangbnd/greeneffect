using GreenEffect.DomainObject.OrderData;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Property(o => o.CustomersID);
            Property(o => o.UserID);
            Property(o => o.ProductsID);
            Property(o => o.RouteID);
            Property(o => o.CustomersRoutesID);
            Property(o => o.CustomersLocationID);
            Property(o => o.ObjectID);
            Property(o => o.ProductsGroupID);
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
            Property(o => o.ProductsID);
            Property(o => o.ProductsName);
            Property(o => o.UserName);
            Property(o => o.Datetime);
            Property(o => o.Disable);
        }
    }
}
