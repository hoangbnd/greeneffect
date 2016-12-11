using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Products;
namespace GreenEffect.Api.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IProductServices _productSevices;
        private readonly IProductsGroupServices _groupServices;

        public ProductController(IProductServices productSevices, IProductsGroupServices groupServices)
        {
            _productSevices = productSevices;
            _groupServices = groupServices;
        }

        public JsonModel<List<ProductApiModel>> GetById(int groupId)
        {
            //  get user by productsgroup
            var productsResult = _productSevices.GetById(groupId);
            if (productsResult.RuleViolations.IsNullOrEmpty())
            {
                var productsApiModels = productsResult.Result.Select(c => new ProductApiModel
                {
                    Id = c.Id,
                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    UnitPrice = c.UnitPrice,
                    ProductGroupId = c.ProductGroupId,
                    Datetime = c.Datetime,
                    Disable = c.Disable
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<ProductApiModel>>
                {
                    IsSuccessful = true,
                    Data = productsApiModels
                };
            }
            return new JsonModel<List<ProductApiModel>>
            {
                IsSuccessful = false,
                Message = productsResult.RuleViolations[0].ErrorMessage
            };
        }
        [HttpPost]
        public JsonModel<SearchProductApiModel> GetProduct(SearchProductApiModel model)
        {
            if (string.IsNullOrEmpty(model.Keyword))
                return new JsonModel<SearchProductApiModel>
                {
                    IsSuccessful = false,
                    Message = "Chưa nhập từ khóa"
                };
            ServiceResult<IPagedList<Product>> productResult;
            var searchRs = new SearchProductApiModel();
            searchRs.Keyword = model.Keyword;
            searchRs.ByGroup = model.ByGroup;
            searchRs.PageIndex = model.PageIndex;
            if (model.ByGroup)
            {
                productResult = _productSevices.SearchProductByGroup(model.Keyword, model.PageIndex, 20);
                var groupResult = _groupServices.GetAll(model.Keyword, model.PageIndex, 20);
                if (groupResult.RuleViolations.IsNullOrEmpty())
                {
                    var groupsApiModels = groupResult.Result.Select(c => new ProductGroupApiModel
                    {
                        Id = c.Id,
                        GroupCode = c.GroupCode,
                        GroupName = c.GroupName,
                        Datetime = c.Datetime,
                        Disable = c.Disable
                    }).OrderByDescending(i => i.Id).ToList();
                    searchRs.Groups = groupsApiModels;
                }
                else
                {
                    return new JsonModel<SearchProductApiModel>
                    {
                        IsSuccessful = false,
                        Message = productResult.RuleViolations[0].ErrorMessage
                    };
                }
            }
            else
            {
                productResult = _productSevices.SearchProduct(model.Keyword, model.PageIndex, 20);
            }
            if (productResult.RuleViolations.IsNullOrEmpty())
            {
                var productsApiModels = productResult.Result.Select(c => new ProductApiModel
                {
                    Id = c.Id,
                    ProductCode = c.ProductCode,
                    ProductName = c.ProductName,
                    UnitPrice = c.UnitPrice,
                    ProductGroupId = c.ProductGroupId,
                    Datetime = c.Datetime,
                    Disable = c.Disable
                }).OrderByDescending(i => i.Id).ToList();
                searchRs.Products = productsApiModels;
                return new JsonModel<SearchProductApiModel>
                {
                    IsSuccessful = true,
                    Data = searchRs
                };
            }
            return new JsonModel<SearchProductApiModel>
            {
                IsSuccessful = false,
                Message = productResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}