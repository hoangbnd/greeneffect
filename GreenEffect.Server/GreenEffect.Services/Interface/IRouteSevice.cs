using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Route;
using MVCCore;
namespace GreenEffect.Services.Interface
{
    public interface IRouteSevice
    { 
      //  ServiceResult<Route> GetById(int id);
        ServiceResult<ICollection<Route>> GetByUser(int IdenUser);
    }
}
