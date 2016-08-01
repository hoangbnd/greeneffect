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

        //Tim kiem du lieu
        //public JsonModel<List<UserApiModel>> Get(string searchUsername, string searchPassword)
        //{
        //    var listUsers = new List<UserApiModel>();
        //    //  get user by username
        //    var userResult = _userServices.GetAll(searchUsername, searchPassword);
        //    if (userResult.RuleViolations.IsNullOrEmpty())
        //    {

        //        listUsers = userResult.Result.Select(u => new UserApiModel
        //        {
        //            Id = u.Id,
        //            UserName = u.UserName,
        //            Password = u.Password
        //        }).OrderByDescending(i => i.Id).ToList();
        //        return new JsonModel<List<UserApiModel>>
        //        {
        //            IsSuccessful = true,
        //            Data = listUsers
        //        };
        //    }
        //    return new JsonModel<List<UserApiModel>>
        //    {
        //        IsSuccessful = false,
        //        Messenger = userResult.RuleViolations[0].ErrorMessage
        //    };
        //}
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
                        Op = userResult.Result.Op,
                        IdenObj = userResult.Result.IdenObj,
                        Datetime = userResult.Result.Datetime,
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
        // Get by UserName Password

        public JsonModel<UserApiModel> Login(string userName, string password)
        {
            var listUsers = new UserApiModel();
            //  get user by username
            var customersResult = _userServices.GetByUserNameAndPassword(userName, password);
            if (customersResult.RuleViolations.IsNullOrEmpty())
            {
                listUsers = new UserApiModel
                {
                    Id = customersResult.Result.Id,
                    UserName = customersResult.Result.UserName,
                    Password = customersResult.Result.Password,
                    IdenObj = customersResult.Result.IdenObj,
                    Op = customersResult.Result.Op,
                    Datetime = customersResult.Result.Datetime
                };
                return new JsonModel<UserApiModel>
                {
                    IsSuccessful = true,
                    Data = listUsers
                };
            }
            return new JsonModel<UserApiModel>
            {
                IsSuccessful = false,
                Messenger = customersResult.RuleViolations[0].ErrorMessage
            };
        }
        //Update Password
        [HttpPost]
        public JsonModel<UserApiModel> UpdatePassword(UserApiModel model)
        {
            //kiem tra user da ton tai chua
            var userRs = _userServices.GetById(model.Id);
            // kiem tra viec lay user
            if (userRs.RuleViolations.IsNullOrEmpty())
            {
                //neu co thi set password moi
                var user = userRs.Result;
                user.Password = model.Password;
                user.Datetime = DateTime.Now;
                var updateResult = _userServices.Update(user);
                //kiem tra ket qua update
                if (updateResult.RuleViolations.IsNullOrEmpty())
                {
                    //neu update thanh cong thi tra ve user da duoc cap nhat password
                    return new JsonModel<UserApiModel>
                    {
                        IsSuccessful = true,
                        Data = new UserApiModel
                        {
                            Id = user.Id,
                            UserName = user.UserName,
                            Password = user.Password,
                            Datetime = user.Datetime
                        }
                    };
                }
                //update khong thanh cong tra ve loi
                return new JsonModel<UserApiModel>
                {
                    IsSuccessful = false,
                    Messenger = updateResult.RuleViolations[0].ErrorMessage
                };
            }
            //tra ve loi khi khong lay duoc user
            return new JsonModel<UserApiModel>
            {
                IsSuccessful = false,
                Messenger = userRs.RuleViolations[0].ErrorMessage
            };
        }

      
        //tao moi 1 ban ghi len sql
        //[HttpPost]
        //public JsonModel<UserApiModel> Create(UserApiModel model)
        //{
        //    var user = new User
        //    {
        //        UserName = model.UserName,
        //        Password = model.Password
        //    };
        //    var userResult = _userServices.Create(user);
        //    if (userResult.RuleViolations.IsNullOrEmpty())
        //    {
        //        return new JsonModel<UserApiModel>
        //        {
        //            IsSuccessful = true,
        //            Data = new UserApiModel
        //            {
        //                Id = userResult.Result.Id,
        //                UserName = userResult.Result.UserName,
        //                Password = userResult.Result.Password
        //            }
        //        };
        //    }
        //    return new JsonModel<UserApiModel>
        //    {
        //        IsSuccessful = false,
        //        Messenger = userResult.RuleViolations[0].ErrorMessage
        //    };
        //}
    }
}