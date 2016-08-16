using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.CustomersImages;
namespace GreenEffect.Repository.EF.Configuration
{
    public class CustomersImagesConfigurations : EntityTypeConfiguration<CustomersImages>
    {
        public CustomersImagesConfigurations()
        {
            ToTable("CustomersImages");
            HasKey(o => o.ID);
            Property(o => o.CustomersID);
            Property(o => o.CustomersImagesID);
            Property(o => o.Images);
            Property(o => o.UserID);
            Property(o => o.DateTime);
         }
    }
}
