using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Api.Controllers
{
    public class LocationController : ApiController
    {
        private readonly ILocationServices _locationSevices;

        public LocationController(ILocationServices locationSevices)
        {
            _locationSevices = locationSevices;
        }
        public JsonModel<LocationApiModel> Get(int id)
        {
            var customerslocationResult = _locationSevices.GetById(id);

            if (customerslocationResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<LocationApiModel>
                {
                    Data = new LocationApiModel
                    {
                        Id = customerslocationResult.Result.Id,
                        CustomerId = customerslocationResult.Result.CustomerId,
                        LocationName = customerslocationResult.Result.LocationName,
                        Description = customerslocationResult.Result.Description,
                        Longitude = customerslocationResult.Result.Longitude,
                        Latitude = customerslocationResult.Result.Latitude,
                        Disable = customerslocationResult.Result.Disable,
                        DateTime = customerslocationResult.Result.DateTime
                    },
                    IsSuccessful = true,
                    Message = ""
                };
            }


            return new JsonModel<LocationApiModel>
            {
                IsSuccessful = false,
                Message = customerslocationResult.RuleViolations[0].ErrorMessage
            }; ;
        }
        [HttpPost]
        public JsonModel<List<LocationApiModel>> GetByUser(LocationApiModel model)
        {
            if (model == null)
            {
                return new JsonModel<List<LocationApiModel>>
                {
                    IsSuccessful = false,
                    Message = "Mã nhân viên không đúng. Xin vui long đăng nhập lại."
                };
            }
            //  get user by username
            var locationRs = _locationSevices.GetByUser(model.UserId);
            if (locationRs.RuleViolations.IsNullOrEmpty())
            {
                var locations = locationRs.Result.Select(l => new LocationApiModel
                {
                    Id = l.Id,
                    CustomerId = l.CustomerId,
                    LocationName = l.LocationName,
                    Description = l.Description,
                    Longitude = l.Longitude,
                    Latitude = l.Latitude,
                    Disable = l.Disable,
                    DateTime = l.DateTime,
                    CustomerName = l.Customer.CustomerName,
                    CustomerAddress = l.Customer.Adress,
                    CustomerPhone = l.Customer.Phone,
                    UserId = model.UserId
                }).ToList();
                return new JsonModel<List<LocationApiModel>>
                {
                    IsSuccessful = true,
                    Data = locations
                };
            }
            return new JsonModel<List<LocationApiModel>>
            {
                IsSuccessful = false,
                Message = locationRs.RuleViolations[0].ErrorMessage
            };
        }
        // tao moi 1 ban ghi len sql
        [HttpPost]
        public JsonModel<LocationApiModel> Create(LocationApiModel model)
        {

            if (!string.IsNullOrEmpty(model.LocationName) || model.CustomerId > 0)//check null
            {

                var customerslocation = new Location
                {
                    Id = model.Id,
                    LocationName = model.LocationName,
                    Description = model.Description,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    CustomerId = model.CustomerId,
                    Disable = model.Disable,
                    DateTime = DateTime.Now
                };

                var customersResult = _locationSevices.Create(customerslocation);
                if (customersResult.RuleViolations.IsNullOrEmpty())
                {
                    return new JsonModel<LocationApiModel>
                    {
                        IsSuccessful = true,
                        Data = new LocationApiModel
                        {
                            Id = customersResult.Result.Id,
                            LocationName = customersResult.Result.LocationName,
                            Description = customersResult.Result.Description,
                            Longitude = customersResult.Result.Longitude,
                            CustomerId = customersResult.Result.CustomerId,
                            Latitude = customersResult.Result.Latitude,
                            Disable = customersResult.Result.Disable,
                            DateTime = customersResult.Result.DateTime
                        }
                    };
                }

                return new JsonModel<LocationApiModel>
                {
                    IsSuccessful = false,
                    Message = customersResult.RuleViolations[0].ErrorMessage
                };

            }
            else
            {
                return new JsonModel<LocationApiModel>
                {
                    IsSuccessful = false,
                    Message = "Not empty UserCode or LocationName or CustomersID"
                };
            }
        }
    }
}