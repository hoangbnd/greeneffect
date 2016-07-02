using GreenEffect.DomainObject.Customers;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersServices
    {
        ServiceResult<ICollection<Customers>> GetAll(string searchCustomersId, string searchCustomersName, string customersAddress, string customersPhone);
        ServiceResult<Customers> GetById(int id);
     //   ServiceResult<Customers> GetByCustormers(string searchCustomersID, string searchCustomersName);
        ServiceResult<Customers> Create(Customers customers);
        ServiceResult<Customers> Update(Customers customers);
        ServiceResult<Customers> Delete(Customers customers);

        bool Validate(string searchCustomersId, string searchCustomersName, string searchCustomersAddress, string searchCustomersPhone);

        object GetCustomersById(int p);
    }
}
