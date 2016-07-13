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
                        CustomersId = customersResult.Result.CustomersId,
                        CustomersName = customersResult.Result.CustomersName,
                        Adress = customersResult.Result.Adress,
                        Phone = customersResult.Result.Phone,
                        IdenRoute = customersResult.Result.IdenRoute,
                        IdenUser=customersResult.Result.IdenUser,
                        IdenCustomers=customersResult.Result.IdenCustomers,
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
                    CustomersId = c.CustomersId,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    IdenRoute = c.IdenRoute,
                    IdenUser = c.IdenUser,
                    IdenCustomers = c.IdenCustomers,
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

        public JsonModel<List<CustomersApiModel>> Get(string searchCustomersId, string searchCustomersName, string customersAddress, string customersPhone)
        {
            var listUsers = new List<CustomersApiModel>();
            // get customers by ID,NAME,ADRESS,PHONE
            var customersResult = _customersSevices.GetAll(searchCustomersId, searchCustomersName, customersAddress, customersPhone);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = customersResult.Result.Select(c => new CustomersApiModel
                {
                    Id = c.Id,
                    CustomersId = c.CustomersId,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    IdenCustomers = c.IdenCustomers,
                    IdenRoute = c.IdenRoute,
                    IdenUser = c.IdenUser
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

        public JsonModel<List<CustomersApiModel>> GetByIden(int IdenRoute)
        {
            var listUsers = new List<CustomersApiModel>();
            //  get user by username
            var customersResult = _customersSevices.GetByIden(IdenRoute);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = customersResult.Result.Select(c => new CustomersApiModel
                {
                    Id = c.Id,
                    CustomersId = c.CustomersId,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    IdenCustomers = c.IdenCustomers,
                    IdenRoute = c.IdenRoute,
                    IdenUser = c.IdenUser
                   
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

        public JsonModel<List<CustomersApiModel>> GetByUser(int IdenUser)
        {
            var listUsers = new List<CustomersApiModel>();
            //  get user by username
            var customersResult = _customersSevices.GetByIden(IdenUser);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {

                listUsers = customersResult.Result.Select(c => new CustomersApiModel
                {
                    Id = c.Id,
                    CustomersId = c.CustomersId,
                    CustomersName = c.CustomersName,
                    Adress = c.Adress,
                    Phone = c.Phone,
                    IdenCustomers = c.IdenCustomers,
                    IdenRoute = c.IdenRoute,
                    IdenUser = c.IdenUser

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

            if (!string.IsNullOrEmpty(model.CustomersId) || !string.IsNullOrEmpty(model.CustomersName))//check null
            {

                var customer = new Customers
                {
                    CustomersId = model.CustomersId,
                    CustomersName = model.CustomersName,
                    Adress = model.Adress,
                    Phone = model.Phone,
                    IdenCustomers = model.IdenCustomers,
                    IdenUser = model.IdenUser,
                    IdenRoute = model.IdenRoute,
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
                            CustomersId = customersResult.Result.CustomersId,
                            CustomersName = customersResult.Result.CustomersName,
                            Adress = customersResult.Result.Adress,
                            Phone = customersResult.Result.Phone,
                            IdenCustomers = customersResult.Result.IdenCustomers,
                            IdenRoute = customersResult.Result.IdenRoute,
                            IdenUser = customersResult.Result.IdenUser,

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
                    Messenger = "Not empty CustomersID or CustomerName"
                };
            }
        }
    }
}