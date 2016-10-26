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
                        CustomersID = orderResult.Result.CustomersId,
                        UserID = orderResult.Result.UserId,
                        ProductsID = orderResult.Result.ProductsId,
                        RouteID = orderResult.Result.RouteId,
                        CustomersRoutesID = orderResult.Result.CustomersRoutesId,
                        CustomersLocationID = orderResult.Result.CustomersLocationId,
                        ObjectID = orderResult.Result.ObjectId,
                        ProductsGroupID = orderResult.Result.ProductsGroupId,
                        LocationName = orderResult.Result.LocationName,
                        Description = orderResult.Result.Description,
                        Longitude = orderResult.Result.Longitude,
                        Latitude = orderResult.Result.Latitude,
                        Datetime = orderResult.Result.Datetime,
                        Disable = orderResult.Result.Disable
                    },
                    IsSuccessful = true,
                    Message = ""
                };
            }


            return new JsonModel<OrderDataApiModel>()
            {
                IsSuccessful = false,
                Message = orderResult.RuleViolations[0].ErrorMessage
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
                    CustomersID = o.CustomersId,
                    UserID = o.UserId,
                    ProductsID = o.ProductsId,
                    RouteID = o.RouteId,
                    CustomersRoutesID = o.CustomersRoutesId,
                    CustomersLocationID = o.CustomersLocationId,
                    ObjectID = o.ObjectId,
                    ProductsGroupID = o.ProductsGroupId,
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
                Message = orderResult.RuleViolations[0].ErrorMessage
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
                    CustomersID = o.CustomersId,
                    UserID = o.UserId,
                    ProductsID = o.ProductsId,
                    RouteID = o.RouteId,
                    CustomersRoutesID = o.CustomersRoutesId,
                    CustomersLocationID = o.CustomersLocationId,
                    ObjectID = o.ObjectId,
                    ProductsGroupID = o.ProductsGroupId,
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
                Message = orderResult.RuleViolations[0].ErrorMessage
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
                    CustomersID = o.CustomersId,
                    UserID = o.UserId,
                    ProductsID = o.ProductsId,
                    RouteID = o.RouteId,
                    CustomersRoutesID = o.CustomersRoutesId,
                    CustomersLocationID = o.CustomersLocationId,
                    ObjectID = o.ObjectId,
                    ProductsGroupID = o.ProductsGroupId,
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
                Message = orderResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}