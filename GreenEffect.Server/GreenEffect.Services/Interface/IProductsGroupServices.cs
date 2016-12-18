using MVCCore;
using System.Collections.Generic;
using GreenEffect.DomainObject.Products;

namespace GreenEffect.Services.Interface
{
    public interface IProductsGroupServices
    {
        ServiceResult<IPagedList<ProductGroup>> GetAll(string keyword, int pageIndex, int pageSize); 
    }
}
