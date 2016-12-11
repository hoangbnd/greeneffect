using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Message;
using MVCCore;
namespace GreenEffect.Services.Interface
{
    public interface IMessageServices
    {
        ServiceResult<ICollection<Message>> GetAll(int userId);
        ServiceResult<Message> Delete(Message message);
        ServiceResult<Message> GetById(int id);
        ServiceResult<List<Message>> Create(List<Message> messages);
    }
}
