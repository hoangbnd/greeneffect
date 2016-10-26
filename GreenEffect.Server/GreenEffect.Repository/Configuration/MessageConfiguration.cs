using GreenEffect.DomainObject.Message;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Repository.EF.Configuration
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            ToTable("viewall_messager");
            HasKey(o => o.Id); 
            Property(o => o.FromId);
            Property(o => o.ToId);
            Property(o => o.IsRead);
            Property(o => o.Content);
            Property(o => o.DateTime);
         }

    }
}
