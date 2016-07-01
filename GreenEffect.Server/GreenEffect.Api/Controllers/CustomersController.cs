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
        private readonly ICustomersServices _CustomersSevices;

        public CustomersController(ICustomersServices CustomersSevices)
        {
            _CustomersSevices = CustomersSevices;
        }

        public JsonModel<CustomersApiModel> Get(int id)
        {
            var CustomersResult = _CustomersSevices.GetById(id);

            if (CustomersResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<CustomersApiModel>()
                {
                    Data = new CustomersApiModel()
                    {
                        Id = CustomersResult.Result.Id,
                        CustomersId = CustomersResult.Result.CustomersId,
                        CustomersName = CustomersResult.Result.CustomersName,
                        Adress = CustomersResult.Result.Adress,
                        Phone = CustomersResult.Result.Phone,
                        IdenRoute = CustomersResult.Result.IdenRoute,
                        IdenUser=CustomersResult.Result.IdenUser,
                        IdenCustomers=CustomersResult.Result.IdenCustomers,
                        Datetime = CustomersResult.Result.Datetime,
                    },
                    IsSuccessful = true,
                    Messenger = ""
                };
            }


            return new JsonModel<CustomersApiModel>()
            {
                IsSuccessful = false,
                Messenger = CustomersResult.RuleViolations[0].ErrorMessage
            }; ;
        }     
    }
}