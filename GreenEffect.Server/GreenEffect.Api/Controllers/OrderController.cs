using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Order;
namespace GreenEffect.Api.Controllers
{
    public class OrderController:ApiController
    {
         private readonly IOrderServices _orderServices;

         public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
         public JsonModel<OrderApiModel> Get(int id)
         {
             var orderResult = _orderServices.GetById(id);

             if (orderResult.RuleViolations.IsNullOrEmpty())
             {
                 return new JsonModel<OrderApiModel>()
                 {
                     Data = new OrderApiModel()
                     {
                         Id = orderResult.Result.Id,
                         OrderDate = orderResult.Result.OrderDate,
                         OrderName = orderResult.Result.OrderName,
                         Note = orderResult.Result.Note,
                         Reciever = orderResult.Result.Reciever,
                         ProductsNumber= orderResult.Result.ProductsNumber,
                         UnitPrice = orderResult.Result.UnitPrice,
                         Amount = orderResult.Result.Amount,
                         IdenCustomers = orderResult.Result.IdenCustomers,
                         IdenUser = orderResult.Result.IdenUser,
                         IdenProducts = orderResult.Result.IdenProducts,
                         IdenRoute = orderResult.Result.IdenRoute,
                         IdenCustomersRoutes = orderResult.Result.IdenCustomersRoutes,
                         IdenCustomersLocation = orderResult.Result.IdenCustomersLocation,
                         IdenObject = orderResult.Result.IdenObject,
                         IdenProductsGroup=orderResult.Result.IdenProductsGroup,
                         LocationName = orderResult.Result.LocationName,
                         Description = orderResult.Result.Description,
                         longitude = orderResult.Result.longitude,
                         Latitude = orderResult.Result.Latitude,
                         Datetime = orderResult.Result.Datetime,
                         Disable = orderResult.Result.Disable
                     },
                     IsSuccessful = true,
                     Messenger = ""
                 };
             }


             return new JsonModel<OrderApiModel>()
             {
                 IsSuccessful = false,
                 Messenger = orderResult.RuleViolations[0].ErrorMessage
             }; ;
         }

         public JsonModel<List<OrderApiModel>> GetByOrder(string OrderName)
         {
             var listOrder = new List<OrderApiModel>();
             //  get user by username
             var orderResult = _orderServices.GetByOrder(OrderName);
             if (orderResult.RuleViolations.IsNullOrEmpty())
             {

                 listOrder = orderResult.Result.Select(o => new OrderApiModel
                 {
                     Id = o.Id,
                     OrderDate = o.OrderDate,
                     OrderName = o.OrderName,
                     Note = o.Note,
                     Reciever = o.Reciever,
                     ProductsNumber = o.ProductsNumber,
                     UnitPrice = o.UnitPrice,
                     Amount = o.Amount,
                     IdenCustomers = o.IdenCustomers,
                     IdenUser = o.IdenUser,
                     IdenProducts = o.IdenProducts,
                     IdenRoute = o.IdenRoute,
                     IdenCustomersRoutes = o.IdenCustomersRoutes,
                     IdenCustomersLocation = o.IdenCustomersLocation,
                     IdenObject = o.IdenObject,
                     IdenProductsGroup = o.IdenProductsGroup,
                     LocationName = o.LocationName,
                     Description = o.Description,
                     longitude = o.longitude,
                     Latitude = o.Latitude,
                     Datetime = o.Datetime,
                     Disable = o.Disable

                 }).OrderByDescending(i => i.Id).ToList();
                 return new JsonModel<List<OrderApiModel>>
                 {
                     IsSuccessful = true,
                     Data = listOrder
                 };
             }
             return new JsonModel<List<OrderApiModel>>
             {
                 IsSuccessful = false,
                 Messenger = orderResult.RuleViolations[0].ErrorMessage
             };
         }

         public JsonModel<List<OrderApiModel>> GetByUser(int IdenUser)
         {
             var listOrder = new List<OrderApiModel>();
             //  get user by username
             var orderResult = _orderServices.GetByUser(IdenUser);
             if (orderResult.RuleViolations.IsNullOrEmpty())
             {

                 listOrder = orderResult.Result.Select(o => new OrderApiModel
                 {
                     Id = o.Id,
                     OrderDate = o.OrderDate,
                     OrderName = o.OrderName,
                     Note = o.Note,
                     Reciever = o.Reciever,
                     ProductsNumber = o.ProductsNumber,
                     UnitPrice = o.UnitPrice,
                     Amount = o.Amount,
                     IdenCustomers = o.IdenCustomers,
                     IdenUser = o.IdenUser,
                     IdenProducts = o.IdenProducts,
                     IdenRoute = o.IdenRoute,
                     IdenCustomersRoutes = o.IdenCustomersRoutes,
                     IdenCustomersLocation = o.IdenCustomersLocation,
                     IdenObject = o.IdenObject,
                     IdenProductsGroup = o.IdenProductsGroup,
                     LocationName = o.LocationName,
                     Description = o.Description,
                     longitude = o.longitude,
                     Latitude = o.Latitude,
                     Datetime = o.Datetime,
                     Disable = o.Disable

                 }).OrderByDescending(i => i.Id).ToList();
                 return new JsonModel<List<OrderApiModel>>
                 {
                     IsSuccessful = true,
                     Data = listOrder
                 };
             }
             return new JsonModel<List<OrderApiModel>>
             {
                 IsSuccessful = false,
                 Messenger = orderResult.RuleViolations[0].ErrorMessage
             };
         }
         public JsonModel<List<OrderApiModel>> GetByRoute(int IdenRouter)
         {
             var listOrder = new List<OrderApiModel>();
             //  get user by username
             var orderResult = _orderServices.GetByRoute(IdenRouter);
             if (orderResult.RuleViolations.IsNullOrEmpty())
             {

                 listOrder = orderResult.Result.Select(o => new OrderApiModel
                 {
                     Id = o.Id,
                     OrderDate = o.OrderDate,
                     OrderName = o.OrderName,
                     Note = o.Note,
                     Reciever = o.Reciever,
                     ProductsNumber = o.ProductsNumber,
                     UnitPrice = o.UnitPrice,
                     Amount = o.Amount,
                     IdenCustomers = o.IdenCustomers,
                     IdenUser = o.IdenUser,
                     IdenProducts = o.IdenProducts,
                     IdenRoute = o.IdenRoute,
                     IdenCustomersRoutes = o.IdenCustomersRoutes,
                     IdenCustomersLocation = o.IdenCustomersLocation,
                     IdenObject = o.IdenObject,
                     IdenProductsGroup = o.IdenProductsGroup,
                     LocationName = o.LocationName,
                     Description = o.Description,
                     longitude = o.longitude,
                     Latitude = o.Latitude,
                     Datetime = o.Datetime,
                     Disable = o.Disable

                 }).OrderByDescending(i => i.Id).ToList();
                 return new JsonModel<List<OrderApiModel>>
                 {
                     IsSuccessful = true,
                     Data = listOrder
                 };
             }
             return new JsonModel<List<OrderApiModel>>
             {
                 IsSuccessful = false,
                 Messenger = orderResult.RuleViolations[0].ErrorMessage
             };
         }
    }
}