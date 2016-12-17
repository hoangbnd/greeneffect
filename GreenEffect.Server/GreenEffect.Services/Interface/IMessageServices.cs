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
        ServiceResult<int> CountNewNotice(int userId);
        ServiceResult<PagedList<Message>> GetAll(int userId, int pageIndex, int pageSize);
        ServiceResult<Message> Delete(Message message);
        ServiceResult<Message> Update(Message message);
        ServiceResult<Message> GetById(int id);
        ServiceResult<List<Message>> Create(List<Message> messages);
    }
}
