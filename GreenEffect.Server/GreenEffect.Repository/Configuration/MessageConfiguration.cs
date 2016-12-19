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
            ToTable("Message");
            HasKey(o => o.Id); 
            Property(o => o.FromId);
            Property(o => o.ToId);
            Property(o => o.IsRead);
            Property(o => o.Title);
            Property(o => o.Content);
            Property(o => o.DateTime);

            HasRequired(o => o.User).WithMany(m => m.Messages).HasForeignKey(o => o.FromId);
        }

    }
}
