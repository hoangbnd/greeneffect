using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomerRouteConfiguration : EntityTypeConfiguration<CustomerRoute>
    {
        public CustomerRouteConfiguration()
        {
            ToTable("viewall_customer");
            HasKey(r => r.Id);
            Property(r => r.CustomersCode);
            Property(r => r.CustomersName);
            Property(r => r.Adress);
            Property(r => r.Phone);
            Property(r => r.CustomersId);
            Property(r => r.UserId);
            Property(r => r.RouteId);
            Property(r => r.DateTime); 
        }
    }
}
