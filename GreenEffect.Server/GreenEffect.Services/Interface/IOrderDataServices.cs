using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.OrderData;
using MVCCore;
namespace GreenEffect.Services.Interface
{
    public interface IOrderDataServices
    {
        ServiceResult<OrderData> GetById(int id);
        ServiceResult<ICollection<OrderData>> GetByUser(int IdenUser, int disable);
        ServiceResult<ICollection<OrderData>> GetByOrder(string OrderName, int disable);
        ServiceResult<ICollection<OrderData>> GetByRoute(int IdenRouter, int disable);
    }
}
