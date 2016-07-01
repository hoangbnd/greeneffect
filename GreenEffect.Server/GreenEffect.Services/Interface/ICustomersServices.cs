using GreenEffect.DomainObject.Customers;
using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersServices
    {
        ServiceResult<ICollection<Customers>> GetAll(string searchCustomersID);
        ServiceResult<Customers> GetById(int id);
     //   ServiceResult<Customers> GetByCustormers(string searchCustomersID, string searchCustomersName);
        ServiceResult<Customers> Create(Customers customers);
        ServiceResult<Customers> Update(Customers customers);
        ServiceResult<Customers> Delete(Customers customers);

        bool Validate(string searchCustomersID, string searchCustomersName, string searchCustomersAddress, string searchCustomersPhone);

        object GetCustomersById(int p);
    }
}
