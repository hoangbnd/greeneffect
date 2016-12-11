using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customer");
            HasKey(c => c.Id); 
            Property(c => c.CustomerCode);
            Property(c => c.CustomerName);
            Property(c => c.Address);
            Property(c => c.Phone);
            Property(c => c.RouteId);
            Property(c => c.UserId);
            Property(c => c.DateTime);
        }
    }
}
