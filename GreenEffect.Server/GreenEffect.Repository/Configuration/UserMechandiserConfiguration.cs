using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenSign.Repository.EF.Configuration
{
    using DomainObject;
    public class UserMechandiserConfiguration : EntityTypeConfiguration<UserMechandiser>
    {
        public UserMechandiserConfiguration()
        {
            HasKey(u => u.Id);
            Property(u => u.BrandMasterId);
            Property(u => u.MechandiserId);

            ToTable("UserMechandiser");
        }
    }
}
