using GreenEffect.DomainObject.CustomersImages;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface ICustomersImagesServices
    {
        ServiceResult<CustomersImages> Create(CustomersImages customersimages);
    }
}
