using GreenEffect.DomainObject.CustomersLocation;
using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersLocationServices
    { 
        ServiceResult<CustomersLocation> GetByUser(int userid);
        ServiceResult<CustomersLocation> GetById(int id);
        ServiceResult<CustomersLocation> Create(CustomersLocation customerslocation);
    }
}
