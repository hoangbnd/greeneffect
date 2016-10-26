using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;

namespace GreenEffect.Api.Controllers
{
    public class ProductsGroupController:ApiController
    {
        private readonly IProductsGroupServices _productsGroupRouteService;

        public ProductsGroupController(IProductsGroupServices ProductsGroupRouteService)
        {
            _productsGroupRouteService = ProductsGroupRouteService;
        }
        public JsonModel<List<ProductsGroupApiModel>> GetALL(string groupname)
        {
            var listProductsGroup = new List<ProductsGroupApiModel>();
            var productsgroup = _productsGroupRouteService.GetALL(groupname);
            if (productsgroup.RuleViolations.IsNullOrEmpty())
            {

                listProductsGroup = productsgroup.Result.Select(g => new ProductsGroupApiModel
                {
                    Id = g.Id,
                    GroupCode = g.GroupCode,
                    GroupName = g.GroupName,
                    Disable = g.Disable,
                    Datetime=g.Datetime,

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<ProductsGroupApiModel>>
                {
                    IsSuccessful = true,
                    Data = listProductsGroup
                };
            }
            return new JsonModel<List<ProductsGroupApiModel>>
            {
                IsSuccessful = false,
                Message = productsgroup.RuleViolations[0].ErrorMessage
            };
        }
    
    }
}