using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Messager;

namespace GreenEffect.Api.Controllers
{
    public class MessagerController : ApiController
    {
        private readonly IMessagerServices _messagerServices;

        public MessagerController(IMessagerServices messagerServices)
        {
            _messagerServices = messagerServices;
        }
        public JsonModel<List<MessagerApiModel>> GetAll(int idenUser)
        {
            var listmess = new List<MessagerApiModel>();
            var messager = _messagerServices.GetAll(idenUser);
            if (messager.RuleViolations.IsNullOrEmpty())
            {

                listmess = messager.Result.Select(o => new MessagerApiModel
                {
                    Id = o.Id,
                    UserID = o.UserID,
                    UserName = o.UserName,
                    FromUser = o.FromUser,
                    Messager = o.Message,
                    DateTime = o.DateTime,

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<MessagerApiModel>>
                {
                    IsSuccessful = true,
                    Data = listmess
                };
            }
            return new JsonModel<List<MessagerApiModel>>
            {
                IsSuccessful = false,
                Messenger = messager.RuleViolations[0].ErrorMessage
            };
        }
    }
}