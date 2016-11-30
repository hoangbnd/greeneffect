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
         public ServiceResult<ICollection<ProductGroup>> GetAll(string groupname)
         {
             try
             {
                 var whCls = new List<Expression<Func<ProductGroup, bool>>>();
                 if (!string.IsNullOrEmpty(groupname))
                 {
                     whCls.Add(c => c.GroupName.Contains(groupname));
                 }
                 var productsgroup = _productsGroupRepository.FindAll(whCls, "Id desc");

                 return new ServiceResult<ICollection<ProductGroup>>(productsgroup);
             }
             catch (Exception ex) 
             {
                 return new ServiceResult<ICollection<ProductGroup>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
             }
         }

       
    }
}
