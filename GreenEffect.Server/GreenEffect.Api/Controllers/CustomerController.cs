using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Api.Controllers
{

    public class CustomerController : ApiController
    {
        private readonly ICustomerServices _customerSevices;

        public CustomerController(ICustomerServices customerSevices)
        {
            _customerSevices = customerSevices;
        }

        public JsonModel<CustomerApiModel> Get(int id)
        {
            var customersResult = _customerSevices.GetById(id);

            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<CustomerApiModel>()
                {
                    Data = new CustomerApiModel()
                    {
                        Id = customersResult.Result.Id,
                        CustomerCode = customersResult.Result.CustomerCode,
                        CustomerName = customersResult.Result.CustomerName,
                        Address = customersResult.Result.Address,
                        Phone = customersResult.Result.Phone,
                        RouteId = customersResult.Result.RouteId,
                        UserId = customersResult.Result.UserId,
                        Datetime = customersResult.Result.DateTime,
                    },
                    IsSuccessful = true,
                    Message = ""
                };
            }


            return new JsonModel<CustomerApiModel>()
            {
                IsSuccessful = false,
                Message = customersResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<CustomerApiModel>> GetAll()
        {
            var customerResult = _customerSevices.GetAll(null, null, null, null);
            if (customerResult.RuleViolations.IsNullOrEmpty())
            {
                var lstCustomerApi = customerResult.Result.Select(c => new CustomerApiModel
                {
                    Id = c.Id,
                    CustomerCode = c.CustomerCode,
                    CustomerName = c.CustomerName,
                    Address = c.Address,
                    Phone = c.Phone,
                    RouteId = c.RouteId,
                    UserId = c.UserId,
                    Datetime = c.DateTime
                }).ToList();
                return new JsonModel<List<CustomerApiModel>>()
                {
                    Data = lstCustomerApi,
                    IsSuccessful = true,
                    Message = ""
                };
            }


            return new JsonModel<List<CustomerApiModel>>()
            {
                IsSuccessful = false,
                Message = customerResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<CustomerApiModel>> Get(string searchCustomersCode, string searchCustomersName, string customersAddress, string customersPhone)
        {
            var listUsers = new List<CustomerApiModel>();
            // get customers by ID,NAME,ADDRESS,PHONE
            var customersResult = _customerSevices.GetAll(searchCustomersCode, searchCustomersName, customersAddress, customersPhone);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = customersResult.Result.Select(c => new CustomerApiModel
                {
                    Id = c.Id,
                    CustomerCode = c.CustomerCode,
                    CustomerName = c.CustomerName,
                    Address = c.Address,
                    Phone = c.Phone,
                    RouteId = c.RouteId,
                    UserId = c.UserId
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomerApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<CustomerApiModel>>
            {
                IsSuccessful = false,
                Message = customersResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<CustomerApiModel>> GetByIden(int routeId)
        {
            var listUsers = new List<CustomerApiModel>();
            //  get user by username
            var customersResult = _customerSevices.GetByIden(routeId);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = customersResult.Result.Select(c => new CustomerApiModel
                {
                    Id = c.Id,
                    CustomerCode = c.CustomerCode,
                    CustomerName = c.CustomerName,
                    Address = c.Address,
                    Phone = c.Phone,
                    RouteId = c.RouteId,
                    UserId = c.UserId

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomerApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<CustomerApiModel>>
            {
                IsSuccessful = false,
                Message = customersResult.RuleViolations[0].ErrorMessage
            };
        }
        [HttpPost]
        public JsonModel<List<CustomerApiModel>> GetByUser(CustomerApiModel model)
        {
            if (model == null || model.UserId.Equals(0))
            {
                return new JsonModel<List<CustomerApiModel>>
                {
                    IsSuccessful = false,
                    Message = "Mã nhân viên không đúng. Xin mời đăng nhập lại."
                };
            }
            //  get user by username
            var customersResult = _customerSevices.GetByUser(model.UserId);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                var customers = customersResult.Result.Select(c => new CustomerApiModel
                {
                    Id = c.Id,
                    CustomerCode = c.CustomerCode,
                    CustomerName = c.CustomerName,
                    Address = c.Address,
                    Phone = c.Phone,
                    RouteId = c.RouteId,
                    UserId = c.UserId

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomerApiModel>>
                {
                    IsSuccessful = true,
                    Data = customers
                };
            }
            return new JsonModel<List<CustomerApiModel>>
            {
                IsSuccessful = false,
                Message = customersResult.RuleViolations[0].ErrorMessage
            };
        }
        // tao moi 1 ban ghi len sql
        [HttpPost]
        public JsonModel<CustomerApiModel> Create(CustomerApiModel model)
        {

            if (!string.IsNullOrEmpty(model.CustomerCode) || !string.IsNullOrEmpty(model.CustomerName))//check null
            {

                var customer = new Customer
                {
                    CustomerCode = model.CustomerCode,
                    CustomerName = model.CustomerName,
                    Address = model.Address,
                    Phone = model.Phone,
                    UserId = model.UserId,
                    RouteId = model.RouteId,
                    DateTime = DateTime.Now
                };

                var customersResult = _customerSevices.Create(customer);
                if (customersResult.RuleViolations.IsNullOrEmpty())
                {
                    return new JsonModel<CustomerApiModel>
                    {
                        IsSuccessful = true,
                        Data = new CustomerApiModel
                        {
                            Id = customersResult.Result.Id,
                            CustomerCode = customersResult.Result.CustomerCode,
                            CustomerName = customersResult.Result.CustomerName,
                            Address = customersResult.Result.Address,
                            Phone = customersResult.Result.Phone,
                            RouteId = customersResult.Result.RouteId,
                            UserId = customersResult.Result.UserId

                        }
                    };
                }

                return new JsonModel<CustomerApiModel>
                {
                    IsSuccessful = false,
                    Message = customersResult.RuleViolations[0].ErrorMessage
                };

            }
            else
            {
                return new JsonModel<CustomerApiModel>
                {
                    IsSuccessful = false,
                    Message = "Not empty CustomerCode or CustomerName"
                };
            }
        }
    }
}