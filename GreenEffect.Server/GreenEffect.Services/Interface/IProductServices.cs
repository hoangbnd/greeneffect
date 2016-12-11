using GreenEffect.DomainObject.Products;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface IProductServices
    {
        ServiceResult<ICollection<Product>> GetById(int groupId);

        ServiceResult<IPagedList<Product>> SearchProduct(string keyword, int pageIndex, int pageSize);

        ServiceResult<IPagedList<Product>> SearchProductByGroup(string keyword, int pageIndex, int pageSize);
    }
}
