using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Repository.EF.Configuration
{
    public class LocationConfiguration : EntityTypeConfiguration<Location>
    {
        public LocationConfiguration()
        {
            ToTable("Location");
            HasKey(o => o.Id);
            Property(o => o.CustomerId);
            Property(o => o.LocationName);
            Property(o => o.Description);
            Property(o => o.LocationName);
            Property(o => o.Longitude);
            Property(o => o.DateTime);
            Property(o => o.Disable);

            HasRequired(o => o.Customer).WithMany().HasForeignKey(o => o.CustomerId);
        }
    }
}
