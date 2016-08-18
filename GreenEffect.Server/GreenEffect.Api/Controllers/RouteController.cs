﻿using GreenEffect.Services.Interface;
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
        [HttpPost]
        public JsonModel<List<RouteApiModel>> GetByUser()
        {
            var idenUserStr = HttpContext.Current.Request["idenUser"];
            int idenUser;
            if (!int.TryParse(idenUserStr, NumberStyles.Integer, CultureInfo.InvariantCulture, out idenUser))
            {
                idenUser = 0;
            }
            if (idenUser.Equals(0))
            {
                return new JsonModel<List<RouteApiModel>>
                {
                    IsSuccessful = false,
                    Messenger = "Mã nhân viên không đúng. Xin mời đăng nhập lại."
                };
            }
            var listUsers = new List<RouteApiModel>();
            //  get user by username
            var routeResult = _routeService.GetByUser(idenUser);
            if (routeResult.RuleViolations.IsNullOrEmpty())
            {

                listUsers = routeResult.Result.Select(r => new RouteApiModel
                {
                    Id = r.Id,
                    RouteId = r.RouteId,
                    RouteName = r.RouteName,
                    IdenRoute = r.IdenRoute,
                    IdenUser = r.IdenUser,
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