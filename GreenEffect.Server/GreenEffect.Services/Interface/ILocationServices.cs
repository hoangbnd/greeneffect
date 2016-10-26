using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Interface
{
    public interface ILocationServices
    {
        ServiceResult<ICollection<Location>> GetByUser(int userId);
        ServiceResult<Location> GetById(int id);
        ServiceResult<Location> Create(Location customerslocation);
    }
}
