using MVCCore;
using System.Collections.Generic;
using GreenEffect.DomainObject.Products;

namespace GreenEffect.Services.Interface
{
    public interface IProductsGroupServices
    {
        ServiceResult<ICollection<ProductGroup>> GetAll(string groupname); 
    }
}
