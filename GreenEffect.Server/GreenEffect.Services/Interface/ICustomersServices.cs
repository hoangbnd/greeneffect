using GreenEffect.DomainObject.Customers;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersServices
    {
        ServiceResult<ICollection<Customers>> GetAll(string searchCustomersId, string searchCustomersName, string customersAddress, string customersPhone);
        ServiceResult<Customers> GetById(int id);
        ServiceResult<ICollection<Customers>> GetByUser(int IdenUser); 
        ServiceResult<Customers> Create(Customers customers);
        ServiceResult<ICollection<Customers>> GetByIden(int IdenRouter);
    }
}
