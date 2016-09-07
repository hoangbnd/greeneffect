using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Route;
namespace GreenEffect.Api.Controllers
{
    public class RouteController : ApiController
    {   
        private readonly IRouteSevice _routeService;

        public RouteController(IRouteSevice routeServices)
        {
            _routeService = routeServices;
        }
        public JsonModel<List<RouteApiModel>> GetByUser(int IdenUser)
        {
            var listUsers = new List<RouteApiModel>();
            //  get user by username
            var routeResult = _routeService.GetByUser(IdenUser);
            if (routeResult.RuleViolations.IsNullOrEmpty())
            {

                listUsers = routeResult.Result.Select(r => new RouteApiModel
                {
                    Id = r.Id,
                    RouteCode = r.RouteCode,
                    RouteName = r.RouteName,
                    RouteID = r.RouteID,
                    UserID = r.UserID,
                    DateTime = r.DateTime
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<RouteApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<RouteApiModel>>
            {
                IsSuccessful = false,
                Messenger = routeResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}