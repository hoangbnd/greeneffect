using GreenEffect.DomainObject.CustomersLocation;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomersLocationConfigurations : EntityTypeConfiguration<CustomersLocation>
    {
        public CustomersLocationConfigurations()
        {
            ToTable("CustomersLocation");
            HasKey(o => o.Id);
            Property(o => o.CustomersID);
            Property(o => o.CustomersLocationID);
            Property(o => o.UserID);
            Property(o => o.LocationName);
            Property(o => o.Description);
            Property(o => o.LocationName);
            Property(o => o.Longitude);
            Property(o => o.DateTime);
            Property(o => o.Disable);
        }
    }
}
