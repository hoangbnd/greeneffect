using GreenEffect.DomainObject.CustomersRoutes;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersRoutesServices
    {
        ServiceResult<ICollection<CustomersRoutes>> GetByRoute(int RouteID);
    }
}
