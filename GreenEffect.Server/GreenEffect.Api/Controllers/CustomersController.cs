using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Customers;
namespace GreenEffect.Api.Controllers
{
    public class CustomersController:ApiController
    {
        private readonly ICustomersServices _customersSevices;

        public CustomersController(ICustomersServices customersSevices)
        {
            _customersSevices = customersSevices;
        }

        public JsonModel<CustomersApiModel> Get(int id)
        {
            var customersResult = _customersSevices.GetById(id);

            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<CustomersApiModel>()
                {
                    Data = new CustomersApiModel()
                    {
                        Id = customersResult.Result.Id,
                        CustomersCode = customersResult.Result.CustomersCode,
                        CustomersName = customersResult.Result.CustomersName,
                        Adress = customersResult.Result.Adress,
                        Phone = customersResult.Result.Phone,
                        RouteID = customersResult.Result.RouteID,
                        UserID=customersResult.Result.UserID,
                        CustomersID=customersResult.Result.CustomersID,
                        Datetime = customersResult.Result.Datetime,
                    },
                    IsSuccessful = true,
                    Messenger = ""
                };
            }


            return new JsonModel<CustomersApiModel>()
            {
                IsSuccessful = false,
                Messenger = customersResult.RuleViolations[0].ErrorMessage
            }; 
        }

        public JsonModel<List<CustomersApiModel>> GetAll()
        {
            var customerResult = _customersSevices.GetAll(null, null, null, null);
            if (customerResult.RuleViolations.IsNullOrEmpty())
            {
                var lstCustomerApi = customerResult.Result.Select(c => new CustomersApiModel
                {
                    Id = c.Id,
                    CustomersCode = c.CustomersCode,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    RouteID = c.RouteID,
                    UserID = c.UserID,
                    CustomersID = c.CustomersID,
                    Datetime = c.Datetime
                }).ToList();
                return new JsonModel<List<CustomersApiModel>>()
                {
                    Data = lstCustomerApi,
                    IsSuccessful = true,
                    Messenger = ""
                };
            }


            return new JsonModel<List<CustomersApiModel>>()
            {
                IsSuccessful = false,
                Messenger = customerResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<CustomersApiModel>> Get(string searchCustomersCode, string searchCustomersName, string customersAddress, string customersPhone)
        {
            var listUsers = new List<CustomersApiModel>();
            // get customers by ID,NAME,ADRESS,PHONE
            var customersResult = _customersSevices.GetAll(searchCustomersCode, searchCustomersName, customersAddress, customersPhone);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = customersResult.Result.Select(c => new CustomersApiModel
                {
                    Id = c.Id,
                    CustomersCode = c.CustomersCode,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    CustomersID = c.CustomersID,
                    RouteID = c.RouteID,
                    UserID = c.UserID
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomersApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<CustomersApiModel>>
            {
                IsSuccessful = false,
                Messenger = customersResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<CustomersApiModel>> GetByIden(int RouteID)
        {
            var listUsers = new List<CustomersApiModel>();
            //  get user by username
            var customersResult = _customersSevices.GetByIden(RouteID);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = customersResult.Result.Select(c => new CustomersApiModel
                { 
                    Id = c.Id,
                    CustomersCode = c.CustomersCode,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    CustomersID = c.CustomersID,
                    RouteID = c.RouteID,
                    UserID = c.UserID
                   
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomersApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<CustomersApiModel>>
            {
                IsSuccessful = false,
                Messenger = customersResult.RuleViolations[0].ErrorMessage
            };
        }

        public JsonModel<List<CustomersApiModel>> GetByUser(int UserID)
        {
            var listUsers = new List<CustomersApiModel>();
            //  get user by username
            var customersResult = _customersSevices.GetByUser(UserID);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {

                listUsers = customersResult.Result.Select(c => new CustomersApiModel
                {
                    Id = c.Id,
                    CustomersCode = c.CustomersCode,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    CustomersID = c.CustomersID,
                    RouteID = c.RouteID,
                    UserID = c.UserID

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomersApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<CustomersApiModel>>
            {
                IsSuccessful = false,
                Messenger = customersResult.RuleViolations[0].ErrorMessage
            };
        }
       // tao moi 1 ban ghi len sql
        [HttpPost]
        public JsonModel<CustomersApiModel> Create(CustomersApiModel model)
        {

            if (!string.IsNullOrEmpty(model.CustomersCode) || !string.IsNullOrEmpty(model.CustomersName))//check null
            {

                var customer = new Customers
                {
                    CustomersCode = model.CustomersCode,
                    CustomersName = model.CustomersName,
                    Adress = model.Adress,
                    Phone = model.Phone,
                    CustomersID = model.CustomersID,
                    UserID = model.UserID,
                    RouteID = model.RouteID,
                    Datetime = DateTime.Now
                };

                var customersResult = _customersSevices.Create(customer);
                if (customersResult.RuleViolations.IsNullOrEmpty())
                {
                    return new JsonModel<CustomersApiModel>
                    {
                        IsSuccessful = true,
                        Data = new CustomersApiModel
                        {
                            Id = customersResult.Result.Id,
                            CustomersCode = customersResult.Result.CustomersCode,
                            CustomersName = customersResult.Result.CustomersName,
                            Adress = customersResult.Result.Adress,
                            Phone = customersResult.Result.Phone,
                            CustomersID = customersResult.Result.CustomersID,
                            RouteID = customersResult.Result.RouteID,
                            UserID = customersResult.Result.UserID,

                        }
                    };
                }

                return new JsonModel<CustomersApiModel>
                {
                    IsSuccessful = false,
                    Messenger = customersResult.RuleViolations[0].ErrorMessage
                };

            }
            else
            {
                return new JsonModel<CustomersApiModel>
                {
                    IsSuccessful = false,
                    Messenger = "Not empty CustomersCode or CustomerName"
                };
            }
        }
    }
}