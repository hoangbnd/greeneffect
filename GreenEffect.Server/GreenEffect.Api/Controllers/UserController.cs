using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
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

        // GET api/user/5
        public JsonModel<UserApiModel> Get(int id)
        {
            var userResult = _userServices.GetById(id);

            if (userResult.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<UserApiModel>
                {
                    Data = new UserApiModel
                    {
                        Id = userResult.Result.Id,
                        UserName = userResult.Result.UserName,
                        Password = userResult.Result.Password,
                        Op = userResult.Result.Op,
                        Datetime = userResult.Result.LastLoginDt
                    },
                    IsSuccessful = true,
                    Message = ""
                };
            }


            return new JsonModel<UserApiModel>()
            {
                IsSuccessful = false,
                Message = userResult.RuleViolations[0].ErrorMessage
            }; ;
        }
        // Get by UserName Password
        [HttpPost]
        public JsonModel<UserApiModel> Login(UserApiModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
            {
                return new JsonModel<UserApiModel>
                {
                    IsSuccessful = false,
                    Message = "Hãy nhập đầy đủ tên đăng nhập và mật khẩu"
                };
            }
            //  get user by username
            var userResult = _userServices.GetByUserNameAndPassword(model.UserName, model.Password);
            if (userResult.RuleViolations.IsNullOrEmpty())
            {
                if (userResult.Result == null)
                {
                    return new JsonModel<UserApiModel>
                    {
                        IsSuccessful = false,
                        Message = "Tên đăng nhập hoặc mật khẩu chưa đúng. Vui lòng nhập lại "
                    };
                }
                var user = userResult.Result;
                user.LastLoginDt = DateTime.Now;
                _userServices.Update(user);
                var listUsers = new UserApiModel
                {
                    Id = userResult.Result.Id,
                    UserName = userResult.Result.UserName,
                    Password = userResult.Result.Password,
                    Op = userResult.Result.Op,
                    Datetime = userResult.Result.LastLoginDt
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
                Message = userResult.RuleViolations[0].ErrorMessage
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
                user.LastLoginDt = DateTime.Now;
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
                            Datetime = user.LastLoginDt
                        }
                    };
                }
                //update khong thanh cong tra ve loi
                return new JsonModel<UserApiModel>
                {
                    IsSuccessful = false,
                    Message = updateResult.RuleViolations[0].ErrorMessage
                };
            }
            //tra ve loi khi khong lay duoc user
            return new JsonModel<UserApiModel>
            {
                IsSuccessful = false,
                Message = userRs.RuleViolations[0].ErrorMessage
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
        //        Message = userResult.RuleViolations[0].ErrorMessage
        //    };
        //}
    }
}