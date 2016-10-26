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
        private readonly IRepository<Product> _productsRepository;
        public ProductsServices(IRepository<Product> productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public ServiceResult<ICollection<Product>> GetByIden(int IdenGroup)
        {
            try
            { 
                var whCls = new List<Expression<Func<Product, bool>>>();
                if (IdenGroup > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.ProductGroupId.Equals(IdenGroup));
                }

                var order = "Id desc";
                var products = _productsRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<Product>>(products);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Product>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
    }
}
