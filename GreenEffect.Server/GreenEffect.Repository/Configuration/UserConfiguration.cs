using GreenEffect.DomainObject.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.Repository.EF.Configuration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration() {
            ToTable("User"); 
            HasKey(u => u.Id);
            Property(u => u.UserName);
            Property(u => u.Password);
            Property(u => u.Datetime);
            Property(u=> u.Op);
        }
    }
}
