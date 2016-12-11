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
    public class ProductGroupController:ApiController
    {
        private readonly IProductsGroupServices _productsGroupService;

        public ProductGroupController(IProductsGroupServices productsGroupService)
        {
            _productsGroupService = productsGroupService;
        }
        public JsonModel<List<ProductGroupApiModel>> GetAll(string groupname, int pageIndex, int pageSize)
        {
            var productsgroup = _productsGroupService.GetAll(groupname, pageIndex, pageSize);
            if (productsgroup.RuleViolations.IsNullOrEmpty())
            {
                var listProductsGroup = productsgroup.Result.Select(g => new ProductGroupApiModel
                {
                    Id = g.Id,
                    GroupCode = g.GroupCode,
                    GroupName = g.GroupName,
                    Disable = g.Disable,
                    Datetime=g.Datetime,

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<ProductGroupApiModel>>
                {
                    IsSuccessful = true,
                    Data = listProductsGroup
                };
            }
            return new JsonModel<List<ProductGroupApiModel>>
            {
                IsSuccessful = false,
                Message = productsgroup.RuleViolations[0].ErrorMessage
            };
        }
    
    }
}