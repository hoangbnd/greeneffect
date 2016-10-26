using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using MVCCore;
using GreenEffect.Api.Models;

namespace GreenEffect.Api.Controllers
{
    
    public class CustomerRouteController : ApiController
    {
        private readonly ICustomersRoutesServices _customerRouteService;

        public CustomerRouteController(ICustomersRoutesServices customerRouteService)
        {
            _customerRouteService = customerRouteService;
        }
        public JsonModel<List<CustomerRouteApiModel>> GetByRoute(int routeId)
        {
            var listUsers = new List<CustomerRouteApiModel>();
            //  get user by username
            var customerrouteResult = _customerRouteService.GetByRoute(routeId);
            if (customerrouteResult.RuleViolations.IsNullOrEmpty())
            {

                listUsers = customerrouteResult.Result.Select(r => new CustomerRouteApiModel
                { 
                    Id = r.Id,
                    CustomerCode = r.CustomersCode,
                    CustomerName = r.CustomersName,
                    Adress = r.Adress,
                    Phone = r.Phone,
                    UserId = r.UserId,
                    CustomerId = r.CustomersId,
                    RouteId = r.RouteId,
                    DateTime = r.DateTime,
               
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<CustomerRouteApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<CustomerRouteApiModel>>
            {
                IsSuccessful = false,
                Message = customerrouteResult.RuleViolations[0].ErrorMessage
            };
        }
    
    }
}