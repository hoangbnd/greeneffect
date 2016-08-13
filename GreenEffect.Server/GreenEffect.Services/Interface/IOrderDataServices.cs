using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Order;
using MVCCore;
using System.Collections.Generic;
namespace GreenEffect.Services.Interface
{
    public class IOrderDataServices
    {
        ServiceResult<Order> GetById(int id);
        ServiceResult<ICollection<Order>> GetByUser(int IdenUser, int disable);
        ServiceResult<ICollection<Order>> GetByOrder(string OrderName, int disable);
        ServiceResult<ICollection<Order>> GetByRoute(int IdenRouter, int disable);
    }
}
