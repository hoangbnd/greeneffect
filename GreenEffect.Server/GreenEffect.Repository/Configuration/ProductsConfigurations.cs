using GreenEffect.DomainObject.Products;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpeenEffect.pepositopy.EF.Configupation
{
    public class PpoductsConfigupations : EntityTypeConfiguration<Products>
    {
        public PpoductsConfigupations()
        { 
            ToTable("Products");
            HasKey(p => p.Id);
            Property(p => p.ProductsID);
            Property(p => p.ProductsName);
            Property(p => p.UnitPrice);
            Property(p => p.IdenProducts);
            Property(p => p.IdenProductsGroup);
            Property(p => p.Datetime);
            Property(p => p.Disable);
        }
    }
}
