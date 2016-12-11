using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GreenEffect.DomainObject.Products;

namespace GreenEffect.Services.Implement
{
    public class ProductsGroupServices : IProductsGroupServices
    {
         private readonly IRepository<ProductGroup> _productsGroupRepository;
         public ProductsGroupServices(IRepository<ProductGroup> productsRepository)
        {
            _productsGroupRepository = productsRepository;
        }
         public ServiceResult<IPagedList<ProductGroup>> GetAll(string keyword, int pageIndex, int pageSize)
         {
             try
             {
                 var whCls = new List<Expression<Func<ProductGroup, bool>>>();
                 if (!string.IsNullOrEmpty(keyword))
                 {
                     whCls.Add(c => c.GroupName.Contains(keyword) || c.GroupCode.Contains(keyword));
                 }
                 var productsgroup = _productsGroupRepository.Paging(whCls, "Id desc", pageIndex, pageSize);

                 return new ServiceResult<IPagedList<ProductGroup>>(productsgroup);
             }
             catch (Exception ex) 
             {
                 return new ServiceResult<IPagedList<ProductGroup>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
             }
         }

       
    }
}
