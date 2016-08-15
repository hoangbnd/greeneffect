using GreenEffect.DomainObject.Order;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface IOrderServices
    {
        ServiceResult<Order> GetById(int id);
        ServiceResult<Order> Create(Order customers);
        ServiceResult<Order> Update(Order customers);
    }
}
