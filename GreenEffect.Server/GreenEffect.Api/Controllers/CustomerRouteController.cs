using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.CustomersRoutes;
namespace GreenEffect.Api.Controllers
{
    public class CustomerRouteController : ApiController
    {
        private readonly ICustomersRoutesServices _customerRouteService;

        public CustomerRouteController(ICustomersRoutesServices customerRouteService)
        {
            _customerRouteService = customerRouteService;
        }
        public JsonModel<List<CustomerRouteApiModel>> GetByRoute(int RouteID)
        {
            var listUsers = new List<CustomerRouteApiModel>();
            //  get user by username
            var customerrouteResult = _customerRouteService.GetByRoute(RouteID);
            if (customerrouteResult.RuleViolations.IsNullOrEmpty())
            {

                listUsers = customerrouteResult.Result.Select(r => new CustomerRouteApiModel
                { 
                    Id = r.Id,
                    CustomersCode = r.CustomersCode,
                    CustomersName = r.CustomersName,
                    Adress = r.Adress,
                    Phone = r.Phone,
                    UserID = r.UserID,
                    CustomersID = r.CustomersID,
                    CustomersRoutesID = r.CustomersRoutesID,
                    RouteID = r.RouteID,
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
                Messenger = customerrouteResult.RuleViolations[0].ErrorMessage
            };
        }
    
    }
}