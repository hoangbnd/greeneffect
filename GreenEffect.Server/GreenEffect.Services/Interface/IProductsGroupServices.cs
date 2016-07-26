using GreenEffect.DomainObject.ProductsGroup;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface IProductsGroupServices
    {
        ServiceResult<ICollection<ProductsGroup>> GetALL(string groupname);
    }
}
