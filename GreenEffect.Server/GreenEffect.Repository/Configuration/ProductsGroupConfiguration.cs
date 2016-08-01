using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.ProductsGroup;
using System.Data.Entity.ModelConfiguration;
namespace GreenEffect.Repository.EF.Configuration
{
    public class ProductsGroupConfiguration : EntityTypeConfiguration<ProductsGroup>
    {
        public ProductsGroupConfiguration()
        {
            ToTable("ProductsGroup");
            HasKey(g => g.Id); 
            Property(g => g.GroupID);
            Property(g => g.GroupName);
            Property(g => g.IdenProductsGroup);
            Property(g => g.Disable);
            Property(g => g.Datetime);
        }
    }
}
