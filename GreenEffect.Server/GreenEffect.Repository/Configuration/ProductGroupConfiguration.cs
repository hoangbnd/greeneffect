using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using GreenEffect.DomainObject.Products;

namespace GreenEffect.Repository.EF.Configuration
{
    public class ProductGroupConfiguration : EntityTypeConfiguration<ProductGroup>
    {
        public ProductGroupConfiguration()
        {
            ToTable("ProductGroup");
            HasKey(g => g.Id); 
            Property(g => g.GroupCode);
            Property(g => g.GroupName);
            Property(g => g.Disable);
            Property(g => g.Datetime);
        }
    }
}
