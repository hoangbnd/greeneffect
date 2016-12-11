using System.Data.Entity.ModelConfiguration;
using GreenEffect.DomainObject.Products;

namespace GreenEffect.Repository.EF.Configuration
{
    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        { 
            ToTable("Product");
            HasKey(p => p.Id);
            Property(p => p.ProductCode);
            Property(p => p.ProductName);
            Property(p => p.UnitPrice);
            Property(p => p.ProductGroupId);
            Property(p => p.Datetime);
            Property(p => p.Disable);

            HasRequired(p => p.ProductGroup).WithMany().HasForeignKey(p => p.ProductGroupId);
        }
    }
}
