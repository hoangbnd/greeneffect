using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.AuthorityObject;
namespace GreenEffect.Api.Controllers
{
    
    public class AuthorityObjectController : ApiController
    {
        private readonly IAuthorityObjectServices _authorityObjServices;

        public AuthorityObjectController( IAuthorityObjectServices authorityObjServices)
        {
            _authorityObjServices = authorityObjServices;
        }
        public JsonModel<List<AuthorityObjectApiModel>> GetObjByUser(int idenUser)
        {
            var listObjs = new List<AuthorityObjectApiModel>();
            var authoObjs = _authorityObjServices.GetAll(idenUser);
            if (authoObjs.RuleViolations.IsNullOrEmpty())
            { 

                listObjs = authoObjs.Result.Select(o => new AuthorityObjectApiModel
                {
                    Id = o.Id,
                    ObjectName = o.ObjectName,
                    ObjectImages = o.ObjectImages,
                    ObjectUser = o.ObjectUser,
                    ObjectSystem = o.ObjectSystem,
                    ObjectId = o.ObjectId,
                    Datetime = o.Datetime


                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<AuthorityObjectApiModel>>
                {
                    IsSuccessful = true,
                    Data = listObjs
                };
            }
            return new JsonModel<List<AuthorityObjectApiModel>>
            {
                IsSuccessful = false,
                Message = authoObjs.RuleViolations[0].ErrorMessage
            };
        }

    }
}