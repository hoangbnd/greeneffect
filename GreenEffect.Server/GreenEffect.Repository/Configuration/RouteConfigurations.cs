using GreenEffect.DomainObject.Route;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Repository.EF.Configuration
{
   
        public class RouteConfigurations : EntityTypeConfiguration<Route>
        {
            public RouteConfigurations()
            {
                ToTable("Routes"); 
                HasKey(r => r.Id);
                Property(r => r.IdenRoute);
                Property(r => r.RouteName);
                Property(r => r.RouteId);
                Property(r => r.IdenRoute);
                Property(r => r.IdenUser);
                Property(r => r.DateTime);
            }
        }
    
}
