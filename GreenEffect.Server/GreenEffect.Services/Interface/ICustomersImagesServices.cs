using MVCCore;
using System.Collections.Generic;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersImagesServices
    {
        ServiceResult<CustomersImages> Create(CustomersImages customersimages);
    }
}
