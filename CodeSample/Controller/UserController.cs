using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GreenSign.Api.Models;
using GreenSign.DomainObject;
using GreenSign.Services;
using MVCCore;
using UserModel = GreenSign.Api.Models.UserModel;

namespace GreenSign.Api.Controllers
{
    public class UserController : ApiController
    {
        private const string LinkSignalAvatar = "http://green-signal.com/Assets/Avatar/";
        private const string AvatarFolder = "/Avatar";
        private readonly IUserServices _userServices;
        private readonly IAuthenticationServices _authenticationServices;

        public UserController(IUserServices userServices, IAuthenticationServices authenticationServices)
        {
            _authenticationServices = authenticationServices;
            _userServices = userServices;
        }

        //public UserModelApi Get()
        //{
        //    var user = GetUser("bangvc", "123456");
        //    var currentUser = new UserModelApi

        //    return currentUser;
        //}

        [HttpGet]
        public UserModelApi GetUser(string username, string password)
        {
            var model = new UserModelApi();

            if (username != null)
            {
                username = username.Trim();
            }

            if (_userServices.Validate(username, password))
            {
                ServiceResult<User> userResult = _userServices.GetByUserNameAndPassword(username, password);

                _authenticationServices.SignIn(userResult.Result, true);

                if (userResult.RuleViolations.IsNullOrEmpty())
                {
                    var userModel = new UserModel
                        {
                            Id = userResult.Result.Id,
                            BrandNameId = userResult.Result.BrandNameId,
                            UserGuid = userResult.Result.UserGuid,
                            UserName = userResult.Result.UserName,
                            Password = userResult.Result.Password,
                            Address = userResult.Result.Address,
                            Cellphone = userResult.Result.Cellphone,
                            CreatedDate = userResult.Result.CreatedDate.ToVnDate(),
                            Status = userResult.Result.Status,
                            FullName = userResult.Result.FullName,
                            DateOfBirth = userResult.Result.DateOfBirth,
                            Telephone = userResult.Result.Telephone,
                            Skype = userResult.Result.Skype,
                            Email = userResult.Result.Email,
                            //MechandiserId = userResult.Result.MechandiserId,
                            UserRole = userResult.Result.UserRole,
                            LastIpAddress = userResult.Result.LastIpAddress,
                            LastLoginDateUtc = userResult.Result.LastLoginDateUtc.ToVnDate()
                        };
                    if (!string.IsNullOrEmpty(userResult.Result.Avatar))
                    {
                        userModel.Avatar = LinkSignalAvatar + userResult.Result.Avatar;
                    }
                    if (userResult.Result.ModifiedDate != null)
                    {
                        userModel.ModifiedDate = ((DateTime)userResult.Result.ModifiedDate).ToVnDate();
                    }
                    model.User = userModel;
                    model.IsSuccessful = true;
                    return model;
                }
                model.Messenger = userResult.RuleViolations[0].ErrorMessage;
                return model;
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> PostProfile()
        {
            var diskFolder = ConfigurationManager.AppSettings["AssetsMapPath"];
            var avatarUrl = "";
            var email = "";
            var telephone = "";
            var password = "";
            var fullname = "";
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var userResult = new ServiceResult<User>();
                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                foreach (var key in provider.FormData.AllKeys)
                {
                    var strings = provider.FormData.GetValues(key);
                    if (strings != null)
                        foreach (var val in strings)
                        {
                            if (key.ToLower() == "userid")
                            {
                                userResult = _userServices.GetById(Int32.Parse(val));
                            }
                            if (key.ToLower() == "email")
                            {
                                email = val;
                            }
                            if (key.ToLower() == "password")
                            {
                                password = val;
                            }
                            if (key.ToLower() == "telephone")
                            {
                                telephone = val;
                            }
                            if (key.ToLower() == "fullname")
                            {
                                fullname = val;
                            }
                        }
                }

                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    var fileInfo = new FileInfo(file.LocalFileName);
                    if (fileInfo.Exists)
                    {
                        var guid = Guid.NewGuid();
                        var fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        var imageType = Path.GetExtension(fileName);
                        var imageThumbUrl = guid.ToString().Substring(0, 1) + "/" + guid.ToString().Substring(0, 2) + "/" + guid + imageType;
                        var imageThumbUrlAb = diskFolder + "/" + AvatarFolder + "/" + guid.ToString().Substring(0, 1) + "/" + guid.ToString().Substring(0, 2) + "/" + guid + imageType;
                        Folder.CreateFolder(guid.ToString().Substring(0, 1) + "/" + guid.ToString().Substring(0, 2) + "/",
                                            diskFolder + "/" + AvatarFolder);
                        fileInfo.MoveTo(imageThumbUrlAb);
                        avatarUrl = imageThumbUrl;
                    }
                }
                if (userResult != null && userResult.RuleViolations.IsNullOrEmpty())
                {
                    var user = userResult.Result;
                    if (!string.IsNullOrEmpty(avatarUrl))
                    {
                        user.Avatar = avatarUrl;
                    }
                    if (!string.IsNullOrEmpty(email))
                    {
                        user.Email = email;
                    }
                    if (!string.IsNullOrEmpty(password))
                    {
                        user.Password = password;
                    }
                    if (!string.IsNullOrEmpty(telephone))
                    {
                        user.Telephone = telephone;
                    }
                    if (!string.IsNullOrEmpty(fullname))
                    {
                        user.FullName = fullname;
                    }
                    //update User
                    _userServices.Update(user);
                }

                return new HttpResponseMessage
                {
                    Content = new StringContent("Upload successful")
                };
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        [HttpPost]
        public UserModelApi RegistrationGcm(UserModel userModel)
        {
            var model = new UserModelApi();
            var userResult = _userServices.GetById(userModel.Id);
            if (userResult.RuleViolations.IsNullOrEmpty())
            {
                var user = userResult.Result;
                if (string.IsNullOrEmpty(user.RegId) || user.RegId != userModel.RegId)
                {
                    user.RegId = userModel.RegId;
                    //update User
                    _userServices.Update(user);
                }
                model.IsSuccessful = true;
            }
            else
            {
                model.IsSuccessful = false;
                model.Messenger = userResult.RuleViolations[0].ErrorMessage;
            }

            return model;

        }

        [HttpGet]
        public ListUserModelApi GetPartner(int userId)
        {
            var model = new ListUserModelApi();
            var userRs = _userServices.GetById(userId);
            if (userRs.RuleViolations.IsNullOrEmpty())
            {
                if (userRs.Result.UserRole == UserRole.Mechandiser ||
                    userRs.Result.UserRole == UserRole.SuperMechandiser || userRs.Result.UserRole == UserRole.Admin)
                {
                    model.IsSuccessful = false;
                    return model;
                }
                var lstPartner = new List<UserModel>();
                var spMrdsRs = _userServices.GetSuperMechandisers(userId);
                if (spMrdsRs.RuleViolations.IsNullOrEmpty())
                {
                    foreach (var spMrd in spMrdsRs.Result)
                    {
                        var spMrdModel = new UserModel
                        {
                            Id = spMrd.Id,
                            Address = spMrd.Address,
                            BrandNameId = spMrd.BrandNameId,
                            Cellphone = spMrd.Cellphone,
                            CreatedDate = spMrd.CreatedDate.ToVnDate(),
                            DateOfBirth = spMrd.DateOfBirth,
                            Email = spMrd.Email,
                            FullName = spMrd.FullName,
                            LastIpAddress = spMrd.LastIpAddress,
                            LastLoginDateUtc = spMrd.LastLoginDateUtc.ToVnDate(),
                            ModifiedDate = ((DateTime)spMrd.ModifiedDate).ToVnDate(),
                            Password = spMrd.Password,
                            RegId = spMrd.RegId,
                            Skype = spMrd.Skype,
                            Status = spMrd.Status,
                            Telephone = spMrd.Telephone,
                            UserGuid = spMrd.UserGuid,
                            UserName = spMrd.UserName,
                            UserRole = spMrd.UserRole,
                            YM = spMrd.YM
                        };
                        if (!string.IsNullOrEmpty(spMrd.Avatar))
                        {
                            spMrdModel.Avatar = LinkSignalAvatar + spMrd.Avatar;
                        }
                        if (lstPartner.IndexOf(spMrdModel) == -1)
                        {
                            lstPartner.Add(spMrdModel);
                        }
                    }
                }
                var mrdRs = _userServices.GetMerchandisers(userId);
                if (mrdRs.RuleViolations.IsNullOrEmpty())
                {
                    foreach (var mrd in mrdRs.Result)
                    {
                        var mrdModel = new UserModel
                        {
                            Id = mrd.Id,
                            Address = mrd.Address,
                            BrandNameId = mrd.BrandNameId,
                            Cellphone = mrd.Cellphone,
                            CreatedDate = mrd.CreatedDate.ToVnDate(),
                            DateOfBirth = mrd.DateOfBirth,
                            Email = mrd.Email,
                            FullName = mrd.FullName,
                            LastIpAddress = mrd.LastIpAddress,
                            LastLoginDateUtc = mrd.LastLoginDateUtc.ToVnDate(),
                            ModifiedDate = ((DateTime)mrd.ModifiedDate).ToVnDate(),
                            Password = mrd.Password,
                            RegId = mrd.RegId,
                            Skype = mrd.Skype,
                            Status = mrd.Status,
                            Telephone = mrd.Telephone,
                            UserGuid = mrd.UserGuid,
                            UserName = mrd.UserName,
                            UserRole = mrd.UserRole,
                            YM = mrd.YM
                        };
                        if (!string.IsNullOrEmpty(mrd.Avatar))
                        {
                            mrdModel.Avatar = LinkSignalAvatar + mrd.Avatar;
                        }
                        if (lstPartner.IndexOf(mrdModel) == -1)
                        {
                            lstPartner.Add(mrdModel);
                        }
                    }
                }
                model.IsSuccessful = true;
                model.Users = lstPartner;
                return model;
            }
            model.IsSuccessful = false;
            return model;
        }
    }
}
