using GreenEffect.DomainObject.Order;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface IOrderServices
    {
        ServiceResult<Order> GetById(int id);
        ServiceResult<ICollection<Order>> GetByUser(int IdenUser);
        ServiceResult<ICollection<Order>> GetByOrder(string OrderName);
        ServiceResult<Order> Create(Order customers);
        ServiceResult<ICollection<Order>> GetByRoute(int IdenRouter);
    }
}
