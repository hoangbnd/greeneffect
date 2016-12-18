using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;

namespace GreenEffect.Api.Controllers
{
    public class OrderDataController : ApiController
    {
        private readonly IOrderDataServices _orderdataServices;
        public OrderDataController(IOrderDataServices orderdataServices)
        {
            _orderdataServices = orderdataServices;
        }
        
    }
}