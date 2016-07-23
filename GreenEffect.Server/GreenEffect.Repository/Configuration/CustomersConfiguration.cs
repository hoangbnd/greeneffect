using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customers;
namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomersConfiguration : EntityTypeConfiguration<Customers>
    {
        public CustomersConfiguration()
        {
            ToTable("Customers");
            HasKey(c => c.Id);
            Property(c => c.CustomersId);
            Property(c => c.CustomersName);
            Property(c => c.Adress);
            Property(c => c.Phone);
            Property(c => c.IdenRoute);
            Property(c => c.IdenUser);
            Property(c => c.IdenCustomers);
            Property(c => c.Datetime);
        }
    }
}
