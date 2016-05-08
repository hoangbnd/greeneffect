using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.User;
namespace GreenEffect.Api.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        public JsonModel<List<UserApiModel>> Get(string searchUsername, string searchPassword)
        {
            var listUsers = new List<UserApiModel>();

            var userResult = _userServices.GetAll(searchUsername, searchPassword);
            if (userResult.RuleViolations.IsNullOrEmpty()) {
                
                listUsers = userResult.Result.Select(u => new UserApiModel
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Password = u.Password
                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<UserApiModel>>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<List<UserApiModel>>
            {
                IsSuccessful = false,
                Messenger = userResult.RuleViolations[0].ErrorMessage
            };
        }
        // GET api/user/5
        public JsonModel<UserApiModel> Get(int id)
        {
            var userResult = _userServices.GetById(id);
            if (userResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<UserApiModel>()
                {
                    Data = new UserApiModel()
                    {
                        Id = userResult.Result.Id,
                        UserName = userResult.Result.UserName,
                        Password = userResult.Result.Password,
                    },
                    IsSuccessful = true,
                    Messenger = ""
                };
            }


            return new JsonModel<UserApiModel>()
                {
                    IsSuccessful = false,
                    Messenger = userResult.RuleViolations[0].ErrorMessage
                }; ;
        }
        [HttpPost]
        public JsonModel<UserApiModel> Create(UserApiModel model)
        {
            var user = new User
            {
                UserName = model.UserName,
                Password = model.Password
            };
            var userResult = _userServices.Create(user);
            if (userResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<UserApiModel>
                {
                    IsSuccessful = true,
                    Data = new UserApiModel
                    {
                        Id = userResult.Result.Id,
                        UserName = userResult.Result.UserName,
                        Password = userResult.Result.Password
                    }
                };
            }
            return new JsonModel<UserApiModel>
            {
                IsSuccessful = false,
                Messenger = userResult.RuleViolations[0].ErrorMessage
            };
        }
    }
}