using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.CustomersLocation;
namespace GreenEffect.Api.Controllers
{
    public class CustomersLocaController
    {
        private readonly ICustomersLocationServices _customerslocationSevices;

        public CustomersLocaController(ICustomersLocationServices customerslocationSevices)
        {
            _customerslocationSevices = customerslocationSevices;
        }
        public JsonModel<CustomersLocationApiModel> Get(int id)
        {
            var customerslocationResult = _customerslocationSevices.GetById(id);

            if (customerslocationResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<CustomersLocationApiModel>()
                {
                    Data = new CustomersLocationApiModel()
                    {
                        Id = customerslocationResult.Result.Id,
                        CustomersID = customerslocationResult.Result.CustomersID,
                        CustomersLocationID = customerslocationResult.Result.CustomersLocationID,
                        LocationName = customerslocationResult.Result.LocationName,
                        Description = customerslocationResult.Result.Description,
                        Longitude = customerslocationResult.Result.LocationName,
                        Latitude = customerslocationResult.Result.Latitude,
                        UserID = customerslocationResult.Result.UserID,
                        Disable = customerslocationResult.Result.Disable,
                        DateTime = customerslocationResult.Result.DateTime
                    },
                    IsSuccessful = true,
                    Messenger = ""
                };
            }


            return new JsonModel<CustomersLocationApiModel>()
            {
                IsSuccessful = false,
                Messenger = customerslocationResult.RuleViolations[0].ErrorMessage
            }; ;
        }
        public JsonModel<CustomersLocationApiModel> GetByUser(int userID)
        {
            var listUsers = new CustomersLocationApiModel();
            //  get user by username
            var customerslocationResult = _customerslocationSevices.GetByUser(userID);
            if (customerslocationResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = new CustomersLocationApiModel
                {
                    Id = customerslocationResult.Result.Id,
                    CustomersID = customerslocationResult.Result.CustomersID,
                    CustomersLocationID = customerslocationResult.Result.CustomersLocationID,
                    LocationName = customerslocationResult.Result.LocationName,
                    Description = customerslocationResult.Result.Description,
                    Longitude = customerslocationResult.Result.LocationName,
                    Latitude = customerslocationResult.Result.Latitude,
                    UserID = customerslocationResult.Result.UserID,
                    Disable = customerslocationResult.Result.Disable,
                    DateTime = customerslocationResult.Result.DateTime

                };
                return new JsonModel<CustomersLocationApiModel>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<CustomersLocationApiModel>
            {
                IsSuccessful = false,
                Messenger = customerslocationResult.RuleViolations[0].ErrorMessage
            };
        }
        // tao moi 1 ban ghi len sql
        [HttpPost]
        public JsonModel<CustomersLocationApiModel> Create(CustomersLocationApiModel model)
        {

            if (model.UserID > 0 || !string.IsNullOrEmpty(model.LocationName) || model.CustomersID > 0)//check null
            {

                var customerslocation = new CustomersLocation
                {
                    Id = model.Id,
                    CustomersLocationID = model.CustomersLocationID,
                    LocationName = model.LocationName,
                    Description = model.Description,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    CustomersID = model.CustomersID,
                    UserID = model.UserID,
                    Disable = model.Disable,
                    DateTime = DateTime.Now
                };

                var customersResult = _customerslocationSevices.Create(customerslocation);
                if (customersResult.RuleViolations.IsNullOrEmpty())
                {
                    return new JsonModel<CustomersLocationApiModel>
                    {
                        IsSuccessful = true,
                        Data = new CustomersLocationApiModel
                        {
                            Id = customersResult.Result.Id,
                            CustomersLocationID = customersResult.Result.CustomersLocationID,
                            LocationName = customersResult.Result.LocationName,
                            Description = customersResult.Result.Description,
                            Longitude = customersResult.Result.Longitude,
                            CustomersID = customersResult.Result.CustomersID,
                            Latitude = customersResult.Result.Latitude,
                            UserID = customersResult.Result.UserID,
                            Disable = customersResult.Result.Disable,
                            DateTime = customersResult.Result.DateTime
                        }
                    };
                }

                return new JsonModel<CustomersLocationApiModel>
                {
                    IsSuccessful = false,
                    Messenger = customersResult.RuleViolations[0].ErrorMessage
                };

            }
            else
            {
                return new JsonModel<CustomersLocationApiModel>
                {
                    IsSuccessful = false,
                    Messenger = "Not empty UserID or LocationName or CustomersID"
                };
            }
        }
    }
}