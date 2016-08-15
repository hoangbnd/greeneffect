using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.OrderData;

namespace GreenEffect.Api.Controllers
{
    public class OrderDataController : ApiController
    {
        private readonly IOrderDataServices _orderdataServices;
        public OrderDataController(IOrderDataServices orderdataServices)
        {
            _orderdataServices = orderdataServices;
        }
        public JsonModel<OrderDataApiModel> Get(int id)
        {
            var orderResult = _orderdataServices.GetById(id);

            if (orderResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<OrderDataApiModel>()
                {
                    Data = new OrderDataApiModel()
                    {
                        Id = orderResult.Result.Id,
                        OrderDate = orderResult.Result.OrderDate,
                        OrderName = orderResult.Result.OrderName,
                        Note = orderResult.Result.Note,
                        Reciever = orderResult.Result.Reciever,
                        ProductsNumber = orderResult.Result.ProductsNumber,
                        UnitPrice = orderResult.Result.UnitPrice,
                        Amount = orderResult.Result.Amount,
                        IdenCustomers = orderResult.Result.IdenCustomers,
                        IdenUser = orderResult.Result.IdenUser,
                        IdenProducts = orderResult.Result.IdenProducts,
                        IdenRoute = orderResult.Result.IdenRoute,
                        IdenCustomersRoutes = orderResult.Result.IdenCustomersRoutes,
                        IdenCustomersLocation = orderResult.Result.IdenCustomersLocation,
                        IdenObject = orderResult.Result.IdenObject,
                        IdenProductsGroup = orderResult.Result.IdenProductsGroup,
                        LocationName = orderResult.Result.LocationName,
                        Description = orderResult.Result.Description,
                        Longitude = orderResult.Result.Longitude,
                        Latitude = orderResult.Result.Latitude,
                        Datetime = orderResult.Result.Datetime,
                        Disable = orderResult.Result.Disable
                    },
                    IsSuccessful = true,
                    Messenger = ""
                };
            }


            return new JsonModel<OrderDataApiModel>()
            {
                IsSuccessful = false,
                Messenger = orderResult.RuleViolations[0].ErrorMessage
            }; ;
        }

        public JsonModel<List<OrderDataApiModel>> GetByOrder(string OrderName, int disable)
        {
            var listOrder = new List<OrderDataApiModel>();
            //  get user by username
            var orderResult = _orderdataServices.GetByOrder(OrderName, disable);
            if (orderResult.RuleViolations.IsNullOrEmpty())
            {

                listOrder = orderResult.Result.Select(o => new OrderDataApiModel
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
                    Longitude = o.Longitude,
                    Latitude = o.Latitude,
                    Datetime = o.Datetime,
                    Disable = o.Disable

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<OrderDataApiModel>>
                {
                    IsSuccessful = true,
                    Data = listOrder
                };
            }
            return new JsonModel<List<OrderDataApiModel>>
            {
                IsSuccessful = false,
                Messenger = orderResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<OrderDataApiModel>> GetByUser(int IdenUser, int disable)
        {
            var listOrder = new List<OrderDataApiModel>();
            //  get user by username
            var orderResult = _orderdataServices.GetByUser(IdenUser, disable);
            if (orderResult.RuleViolations.IsNullOrEmpty())
            {

                listOrder = orderResult.Result.Select(o => new OrderDataApiModel
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
                    Longitude = o.Longitude,
                    Latitude = o.Latitude,
                    Datetime = o.Datetime,
                    Disable = o.Disable

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<OrderDataApiModel>>
                {
                    IsSuccessful = true,
                    Data = listOrder
                };
            }
            return new JsonModel<List<OrderDataApiModel>>
            {
                IsSuccessful = false,
                Messenger = orderResult.RuleViolations[0].ErrorMessage
            };
        }
        public JsonModel<List<OrderDataApiModel>> GetByRoute(int IdenRouter, int disable)
        {
            var listOrder = new List<OrderDataApiModel>();
            //  get user by username
            var orderResult = _orderdataServices.GetByRoute(IdenRouter, disable);
            if (orderResult.RuleViolations.IsNullOrEmpty())
            {

                listOrder = orderResult.Result.Select(o => new OrderDataApiModel
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
                    Longitude = o.Longitude,
                    Latitude = o.Latitude,
                    Datetime = o.Datetime,
                    Disable = o.Disable

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<OrderDataApiModel>>
                {
                    IsSuccessful = true,
                    Data = listOrder
                };
            }
            return new JsonModel<List<OrderDataApiModel>>
            {
                IsSuccessful = false,
                Messenger = orderResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}