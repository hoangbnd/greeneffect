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
        ServiceResult<ICollection<Customers>> GetAll(string searchCustomersID, string searchCustomersName, string searchAdress, string searchPhone);
        ServiceResult<Customers> GetById(int id);
        ServiceResult<Customers> GetByCustormers(string userName, string password);
        ServiceResult<Customers> Create(Customers customers);
        ServiceResult<Customers> Update(Customers customers);
        ServiceResult<Customers> Delete(Customers customers);

        bool Validate(string userName, string password);

        object GetUserById(int p);
    }
}
