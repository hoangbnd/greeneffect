using GreenEffect.DomainObject.Products;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace GreenEffect.Services.Implement
{
   public class ProductsServices:IProductsServices
    {
        private readonly IRepository<Products> _productsRepository;
        public ProductsServices(IRepository<Products> productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public ServiceResult<ICollection<Products>> GetByIden(int IdenGroup)
        {
            try
            { 
                var whCls = new List<Expression<Func<Products, bool>>>();
                if (IdenGroup > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.IdenProductsGroup.Equals(IdenGroup));
                }

                var order = "Id desc";
                var products = _productsRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<Products>>(products);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Products>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
    }
}
