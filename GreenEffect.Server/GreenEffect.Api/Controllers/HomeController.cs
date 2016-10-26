using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GreenEffect.Api.Models;
using GreenEffect.Services.Interface;
namespace GreenEffect.Api.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
