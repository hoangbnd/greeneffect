using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
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

        [HttpPost]
        public JsonModel<List<RouteApiModel>> GetByUser(RouteApiModel model)
        {
            if (model == null || model.UserId == 0)
            {
                return new JsonModel<List<RouteApiModel>>
                {
                    IsSuccessful = false,
                    Message = "Mã nhân viên không đúng. Xin mời đăng nhập lại."
                };
            }
            //  get user by username
            var routeResult = _routeService.GetByUser(model.UserId);
            if (routeResult.RuleViolations.IsNullOrEmpty())
            {
                var listUsers = routeResult.Result.Select(r => new RouteApiModel
                {
                    Id = r.Id,
                    RouteCode = r.RouteCode,
                    RouteName = r.RouteName,
                    UserId = r.UserId,
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
                Message = routeResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}