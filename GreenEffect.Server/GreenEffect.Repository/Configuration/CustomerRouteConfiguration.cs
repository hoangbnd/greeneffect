using GreenEffect.DomainObject.CustomersRoutes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomerRouteConfiguration : EntityTypeConfiguration<CustomersRoutes>
    {
        public CustomerRouteConfiguration()
        {
            ToTable("CustomersRoutes");
            HasKey(r => r.Id);
            Property(r => r.IdenCustomers);
            Property(r => r.IdenCustomersRoutes);
            Property(r => r.IdenRoute);
            Property(r => r.DateTime);
        }
    }
}
