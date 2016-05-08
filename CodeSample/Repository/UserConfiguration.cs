
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenSign.Repository.EF.Configuration
{
    using System.Data.Entity.ModelConfiguration;
    using DomainObject;

    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(u => u.Id);
            Property(u => u.UserName);
            Property(u => u.Password);
            Property(u => u.FullName);
            Property(u => u.Avatar);
            Property(u => u.DateOfBirth);
            Property(u => u.Address);
            Property(u => u.Telephone);
            Property(u => u.Cellphone);
            Property(u => u.YM);
            Property(u => u.Email);
            Property(u => u.Status).HasColumnName("Status").IsRequired();
            Property(u => u.UserRole).HasColumnName("UserRole").IsRequired();
            Property(u => u.CreatedDate);
            Property(u => u.ModifiedDate);
            Property(u => u.LastLoginDateUtc);
            Property(u => u.RegId);
            Property(u => u.BrandNameId);
            //Property(u => u.WarehouseId);
            Property(u => u.Deleted);
            //HasRequired(u => u.Warehouse).WithMany(v => v.Users).HasForeignKey(u => u.WarehouseId);
            HasRequired(u => u.BrandName).WithMany(b => b.BrandMasters).HasForeignKey(u => u.BrandNameId);
            HasMany(u => u.Warehouses)
                .WithMany(v => v.Users)
                .Map(m =>
                    m.ToTable("User_Warehouse_Mapping") 
                    .MapLeftKey("UserId")
                    .MapRightKey("WarehouseId")
                );

           
        }
    }
}
