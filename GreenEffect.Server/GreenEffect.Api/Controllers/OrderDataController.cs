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
                        CustomersID = orderResult.Result.CustomersID,
                        UserID = orderResult.Result.UserID,
                        ProductsID = orderResult.Result.ProductsID,
                        RouteID = orderResult.Result.RouteID,
                        CustomersRoutesID = orderResult.Result.CustomersRoutesID,
                        CustomersLocationID = orderResult.Result.CustomersLocationID,
                        ObjectID = orderResult.Result.ObjectID,
                        ProductsGroupID = orderResult.Result.ProductsGroupID,
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
                    CustomersID = o.CustomersID,
                    UserID = o.UserID,
                    ProductsID = o.ProductsID,
                    RouteID = o.RouteID,
                    CustomersRoutesID = o.CustomersRoutesID,
                    CustomersLocationID = o.CustomersLocationID,
                    ObjectID = o.ObjectID,
                    ProductsGroupID = o.ProductsGroupID,
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

        public JsonModel<List<OrderDataApiModel>> GetByUser(int UserID, int disable)
        {
            var listOrder = new List<OrderDataApiModel>();
            //  get user by username
            var orderResult = _orderdataServices.GetByUser(UserID, disable);
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
                    CustomersID = o.CustomersID,
                    UserID = o.UserID,
                    ProductsID = o.ProductsID,
                    RouteID = o.RouteID,
                    CustomersRoutesID = o.CustomersRoutesID,
                    CustomersLocationID = o.CustomersLocationID,
                    ObjectID = o.ObjectID,
                    ProductsGroupID = o.ProductsGroupID,
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
        public JsonModel<List<OrderDataApiModel>> GetByRoute(int RouteIDr, int disable)
        {
            var listOrder = new List<OrderDataApiModel>();
            //  get user by username
            var orderResult = _orderdataServices.GetByRoute(RouteIDr, disable);
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
                    CustomersID = o.CustomersID,
                    UserID = o.UserID,
                    ProductsID = o.ProductsID,
                    RouteID = o.RouteID,
                    CustomersRoutesID = o.CustomersRoutesID,
                    CustomersLocationID = o.CustomersLocationID,
                    ObjectID = o.ObjectID,
                    ProductsGroupID = o.ProductsGroupID,
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