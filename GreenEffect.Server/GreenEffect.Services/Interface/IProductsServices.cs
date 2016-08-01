using GreenEffect.DomainObject.Products;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface IProductsServices
    {
        ServiceResult<ICollection<Products>> GetByIden(int IdenGroup); 
    }
}
