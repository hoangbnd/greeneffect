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
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
       
        [HttpPost]
        public JsonModel<OrderApiModel> Create(OrderApiModel model)
        {
            if (!string.IsNullOrEmpty(model.OrderName))//check null
            {
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderName = model.OrderName,
                    Note = model.Note,
                    Reciever = model.Reciever,
                    Description = model.Description,
                    ProductsNumber = model.ProductsNumber,
                    UnitPrice = model.UnitPrice,
                    Amount = model.Amount,
                    CustomersID = model.CustomersID,
                    UserID = model.UserID,
                    ProductsID = model.ProductsID,
                    RouteID = model.RouteID,
                    CustomersRoutesID = model.CustomersRoutesID,
                    CustomersLocationID = model.CustomersLocationID,
                    ObjectID = model.ObjectID,
                    ProductsGroupID = model.ProductsGroupID,
                    LocationName = model.LocationName,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    Datetime = DateTime.Now,
                    Disable = model.Disable
                };
                var orderResult = _orderServices.Create(order);
                if (orderResult.RuleViolations.IsNullOrEmpty())
                {
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = true,
                        Data = new OrderApiModel
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
                        }
                    };
                }
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Messenger = orderResult.RuleViolations[0].ErrorMessage
                };

            }
            else
            {
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Messenger = "Not empty OrderName"
                };
            }
        }
        [HttpPost]
        public JsonModel<OrderApiModel> UpdateOrder(OrderApiModel model)
        {
            //kiem tra user da ton tai chua
            var orderResult = _orderServices.GetById(model.Id);
            // kiem tra viec lay user
            if (orderResult.RuleViolations.IsNullOrEmpty())
            {
                //neu co thi set password moi
                var order = orderResult.Result;
                order.CustomersID = model.CustomersID;
                order.ProductsID = model.ProductsID;
                order.ProductsGroupID = model.ProductsGroupID;
                order.Note = model.Note;
                order.Reciever = model.Reciever;
                order.ProductsNumber = model.ProductsNumber;
                order.UnitPrice = model.UnitPrice;
                order.Amount = model.Amount;
                order.Datetime = DateTime.Now;
                var updateResult = _orderServices.Update(order);
                //kiem tra ket qua update
                if (updateResult.RuleViolations.IsNullOrEmpty())
                {
                    //neu update thanh cong thi tra ve user da duoc cap nhat password
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = true,
                        Data = new OrderApiModel
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
                        }
                    };
                }
                //update khong thanh cong tra ve loi
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Messenger = updateResult.RuleViolations[0].ErrorMessage
                };
            }
            //tra ve loi khi khong lay duoc user
            return new JsonModel<OrderApiModel>
            {
                IsSuccessful = false,
                Messenger = orderResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}