using GreenEffect.DomainObject.Messager;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Repository.EF.Configuration
{
    public class MessagerConfiguration : EntityTypeConfiguration<Messager>
    {
        public MessagerConfiguration()
        {
            ToTable("viewall_messager");
            HasKey(o => o.Id); 
            Property(o => o.UserID);
            Property(o => o.UserName);
            Property(o => o.FromUser);
            Property(o => o.Message);
            Property(o => o.DateTime);
         }

    }
}
