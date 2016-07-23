using GreenEffect.DomainObject.ProductsGroup;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace GreenEffect.Services.Implement
{
    public class ProductsGroupServices : IProductsGroupServices
    {
         private readonly IRepository<ProductsGroup> _productsGroupRepository;
         public ProductsGroupServices(IRepository<ProductsGroup> productsRepository)
        {
            _productsGroupRepository = productsRepository;
        }
         public ServiceResult<ICollection<ProductsGroup>> GetALL(int disable)
         {
             try
             {
                 var whCls = new List<Expression<Func<ProductsGroup, bool>>>();
                 if (disable==0)
                 {
                     whCls.Add(c => c.Disable.Equals(disable));
                 }
                 var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                 //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                 //thi order = "UserName asc"
                 var productsgroup = _productsGroupRepository.FindAll(whCls, order);

                 return new ServiceResult<ICollection<ProductsGroup>>(productsgroup);
             }
             catch (Exception ex)
             {
                 return new ServiceResult<ICollection<ProductsGroup>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
             }
         }

       
    }
}
