using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Order;
using MVCCore;
namespace GreenEffect.Services.Interface
{
    public interface IOrderDataServices
    {
        ServiceResult<OrderItem> GetById(int id);
    }
}
