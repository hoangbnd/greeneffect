using GreenEffect.DomainObject.AuthorityObject;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Repository.EF.Configuration
{
    public class AuthorityObjectConfigurations: EntityTypeConfiguration<AuthorityObject>
    {
        public AuthorityObjectConfigurations()
        {
            ToTable("Object");
            HasKey(o => o.Id); 
            Property(o => o.ObjectName);
            Property(o => o.ObjectImages);        
            Property(o => o.ObjectUser);
            Property(o => o.IdenObject);
            Property(o => o.ObjectSystem);
            Property(o => o.Datetime);
        }
    }
}
