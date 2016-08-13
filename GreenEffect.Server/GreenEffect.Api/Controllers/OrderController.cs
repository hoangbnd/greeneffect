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


            return new JsonModel<OrderApiModel>()
            {
                IsSuccessful = false,
                Messenger = orderResult.RuleViolations[0].ErrorMessage
            }; ;
        }

        public JsonModel<List<OrderApiModel>> GetByOrder(string OrderName, int disable)
        {
            var listOrder = new List<OrderApiModel>();
            //  get user by username
            var orderResult = _orderServices.GetByOrder(OrderName, disable);
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
                    Longitude = o.Longitude,
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

        public JsonModel<List<OrderApiModel>> GetByUser(int IdenUser, int disable)
        {
            var listOrder = new List<OrderApiModel>();
            //  get user by username
            var orderResult = _orderServices.GetByUser(IdenUser, disable);
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
                    Longitude = o.Longitude,
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
        public JsonModel<List<OrderApiModel>> GetByRoute(int IdenRouter, int disable)
        {
            var listOrder = new List<OrderApiModel>();
            //  get user by username
            var orderResult = _orderServices.GetByRoute(IdenRouter, disable);
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
                    Longitude = o.Longitude,
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
                    IdenCustomers = model.IdenCustomers,
                    IdenUser = model.IdenUser,
                    IdenProducts = model.IdenProducts,
                    IdenRoute = model.IdenRoute,
                    IdenCustomersRoutes = model.IdenCustomersRoutes,
                    IdenCustomersLocation = model.IdenCustomersLocation,
                    IdenObject = model.IdenObject,
                    IdenProductsGroup = model.IdenProductsGroup,
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
                order.IdenCustomers = model.IdenCustomers;
                order.IdenProducts = model.IdenProducts;
                order.IdenProductsGroup = model.IdenProductsGroup;
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