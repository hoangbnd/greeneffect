using MVCCore;
using System.Collections.Generic;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Interface
{
    public interface ICustomerServices
    {
        ServiceResult<ICollection<Customer>> GetAll(string searchCustomersId, string searchCustomersName, string customersAddress, string customersPhone);
        ServiceResult<Customer> GetById(int id);
        ServiceResult<ICollection<Customer>> GetByUser(int userId); 
        ServiceResult<Customer> Create(Customer customer);
        ServiceResult<ICollection<Customer>> GetByIden(int routeId);
    }
}
