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
    public class ProductsController:ApiController
    {
         private readonly IProductsServices _productsSevices;

         public ProductsController(IProductsServices productsSevices)
        {
            _productsSevices = productsSevices;
        }
         public JsonModel<List<ProductsApiModel>> GetByIden(int IdenGroup)
         {
             var listUsers = new List<ProductsApiModel>();
             //  get user by productsgroup
             var productsResult = _productsSevices.GetByIden(IdenGroup);
             if (productsResult.RuleViolations.IsNullOrEmpty())
             {
                 listUsers = productsResult.Result.Select(c => new ProductsApiModel
                 {
                     Id = c.Id,
                     ProductsCode = c.ProductCode,
                     ProductsName = c.ProductName,
                     UnitPrice = c.UnitPrice,
                     
                     ProductsGroupId = c.ProductGroupId,
                     Datetime = c.Datetime,
                     Disable = c.Disable

                 }).OrderByDescending(i => i.Id).ToList();
                 return new JsonModel<List<ProductsApiModel>>
                 {
                     IsSuccessful = true,
                     Data = listUsers
                 };
             }
             return new JsonModel<List<ProductsApiModel>>
             {
                 IsSuccessful = false,
                 Message = productsResult.RuleViolations[0].ErrorMessage
             };
         }
    }
}