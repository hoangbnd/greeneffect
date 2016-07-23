using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.ProductsGroup;
namespace GreenEffect.Api.Controllers
{
    public class ProductsGroupController:ApiController
    {
        private readonly IProductsGroupServices _productsGroupRouteService;

        public ProductsGroupController(IProductsGroupServices ProductsGroupRouteService)
        {
            _productsGroupRouteService = ProductsGroupRouteService;
        }
        public JsonModel<List<ProductsGroupApiModel>> GetALL(int disable)
        {
            var listUsers = new List<ProductsGroupApiModel>();
            var productsgroup = _productsGroupRouteService.GetALL(disable);
            if (productsgroup.RuleViolations.IsNullOrEmpty())
            {

                listUsers = productsgroup.Result.Select(g => new ProductsGroupApiModel
                {
                    Id = g.Id,
                    GroupID = g.GroupID,
                    GroupName = g.GroupName,
                    IdenProductsGroup = g.IdenProductsGroup,
                    Disable = g.Disable,
                    Datetime=g.Datetime,

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<ProductsGroupApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<ProductsGroupApiModel>>
            {
                IsSuccessful = false,
                Messenger = productsgroup.RuleViolations[0].ErrorMessage
            };
        }
    
    }
}