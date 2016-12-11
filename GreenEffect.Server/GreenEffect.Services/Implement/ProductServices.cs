using GreenEffect.DomainObject.Products;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace GreenEffect.Services.Implement
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository<Product> _productRepository;
        public ProductServices(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public ServiceResult<ICollection<Product>> GetById(int groupId)
        {
            try
            {
                var whCls = new List<Expression<Func<Product, bool>>>();
                if (groupId > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.ProductGroupId.Equals(groupId));
                }

                var order = "Id desc";
                var products = _productRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<Product>>(products);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Product>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }

        public ServiceResult<IPagedList<Product>> SearchProduct(string keyword, int pageIndex, int pageSize)
        {
            try
            {
                var whCls = new List<Expression<Func<Product, bool>>>();
                if (!string.IsNullOrEmpty(keyword))//check dk co hay ko?
                {
                    whCls.Add(c => c.ProductCode.Contains(keyword) || c.ProductName.Contains(keyword));
                }
                var products = _productRepository.Paging(whCls, "Id desc", pageIndex, pageSize);
                return new ServiceResult<IPagedList<Product>>(products);
            }
            catch (Exception ex)
            {
                return new ServiceResult<IPagedList<Product>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }

        public ServiceResult<IPagedList<Product>> SearchProductByGroup(string keyword, int pageIndex, int pageSize)
        {
            try
            {
                var whCls = new List<Expression<Func<Product, bool>>>();
                if (!string.IsNullOrEmpty(keyword))//check dk co hay ko?
                {
                    whCls.Add(c => c.ProductGroup.GroupName.Contains(keyword) || c.ProductGroup.GroupCode.Contains(keyword));
                }
                var products = _productRepository.Paging(whCls, "Id desc", pageIndex, pageSize);
                return new ServiceResult<IPagedList<Product>>(products);
            }
            catch (Exception ex)
            {
                return new ServiceResult<IPagedList<Product>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
    }
}
