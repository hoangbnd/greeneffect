using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomerImageConfigurations : EntityTypeConfiguration<CustomersImages>
    {
        public CustomerImageConfigurations()
        {
            ToTable("CustomersImages");
            HasKey(o => o.Id);
            Property(o => o.CustomersId);
            Property(o => o.Images);
            Property(o => o.UserId);
            Property(o => o.DateTime);
         }
    }
}
