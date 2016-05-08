using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GreenSign.DomainObject;
using MVCCore;
using MVCCore.Data;

namespace GreenSign.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRelationToBrandMasterServices _relationToBrandMasterServices;
        private readonly IVendorServices _vendorServices;
        public UserServices(IRepository<User> userRepository,
            IRelationToBrandMasterServices relationToBrandMasterServices, IVendorServices vendorServices)
        {
            _userRepository = userRepository;
            _relationToBrandMasterServices = relationToBrandMasterServices;
            _vendorServices = vendorServices;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ServiceResult<User> GetById(int id)
        {
            try
            {
                User user = _userRepository.FindById(id);
                return user != null
                        ? new ServiceResult<User>(user)
                        : new ServiceResult<User>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ServiceResult<User> GetByGuid(Guid guid)
        {
            try
            {
                User user = _userRepository.Find(new List<Expression<Func<User, bool>>> { u => u.UserGuid == guid });
                return user != null
                        ? new ServiceResult<User>(user)
                        : new ServiceResult<User>(new[] { new RuleViolation("ErrorMsg", "Have no user with id guid {0} ".FormatWith(guid.ToString())) });
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual ServiceResult<User> GetByUserName(string name)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>
                                {   
                                    p => p.UserName.ToLower() == name.ToLower()
                                };
                whCls.Add(p => !p.Deleted);
                User user = _userRepository.Find(whCls);
                return user != null
                        ? new ServiceResult<User>(user)
                        : new ServiceResult<User>(new[] { new RuleViolation("Error Message", "Data is empty") });
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        public ServiceResult<User> GetByEmail(string email)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>
                                {   
                                    p => p.UserName.ToLower() == email.ToLower()
                                };
                whCls.Add(p => !p.Deleted);
                User user = _userRepository.Find(whCls);
                return user != null
                        ? new ServiceResult<User>(user)
                        : new ServiceResult<User>(new[] { new RuleViolation("Error Message", "Data is empty") });
            }
            catch (Exception e)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ServiceResult<User> GetByUserNameAndPassword(string userName, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    var whCls = new List<Expression<Func<User, bool>>>
                                    {
                                        c=>c.UserName==userName,
                                        c=>c.Password==password
                                    };
                    whCls.Add(c => !c.Deleted);
                    var user = _userRepository.Find(whCls);
                    if (user != null)
                    {
                        return new ServiceResult<User>(user);
                    }
                    return new ServiceResult<User>(new[] { new RuleViolation("Ex", "User name or password are not correct") });
                }
                return new ServiceResult<User>(new[] { new RuleViolation("Ex", "User name and password are not empty") });
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Ex", ex.Message) });
            }
        }

        public ServiceResult<ICollection<User>> Get(int? brandNameId, int? status, int? userRole, string order, int? limit)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                whCls.Add(c => !c.Deleted);
                if (brandNameId != null)
                {
                    whCls.Add(c => c.BrandNameId == brandNameId);
                }
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                if (userRole != null)
                {
                    whCls.Add(c => c.UserRole == (UserRole)userRole);
                }
                ICollection<User> users = limit != null
                                ? _userRepository.FindAll(whCls, (int)limit, order)
                                : _userRepository.FindAll(whCls, order);
                //return users.Count > 0
                //        ? new ServiceResult<ICollection<User>>(users)
                //        : new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error Message", "Data is empty") });
                return new ServiceResult<ICollection<User>>(users);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mechandiserId"></param>
        /// <param name="status"></param>
        /// <param name="userRole"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ServiceResult<ICollection<User>> Get(int? status, int? userRole, string order, int? limit)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                whCls.Add(c => !c.Deleted);
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                if (userRole != null)
                {
                    whCls.Add(c => c.UserRole == (UserRole)userRole);
                }
                ICollection<User> users = limit != null
                                ? _userRepository.FindAll(whCls, (int)limit, order)
                                : _userRepository.FindAll(whCls, order);
                //return users.Count > 0
                //        ? new ServiceResult<ICollection<User>>(users)
                //        : new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error Message", "Data is empty") });
                return new ServiceResult<ICollection<User>>(users);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="userRole"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public ServiceResult<ICollection<User>> GetInRole(int? status, int? userRole, string order)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                whCls.Add(c => !c.Deleted);
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                if (userRole != null)
                {
                    whCls.Add(c => c.UserRole == (UserRole)userRole);
                }
                ICollection<User> users = _userRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<User>>(users);

            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMasterId"></param>
        /// <returns></returns>
        public ServiceResult<ICollection<User>> GetSuperMechandisers(int brandMasterId)
        {

            var whCls = new List<Expression<Func<User, bool>>>();
            whCls.Add(u => !u.Deleted);
            try
            {
                if (brandMasterId <= 0)
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "BrandMasterId must more than 0") });
                }
                var bmRs = GetById(brandMasterId);
                if (!bmRs.RuleViolations.IsNullOrEmpty())
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "Not found user with id = {0}".FormatWith(brandMasterId)) });
                }
                if (bmRs.Result.UserRole != UserRole.BrandMaster)
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "User with id = {0} isn't brand master".FormatWith(brandMasterId)) });
                }
                whCls.Add(u => u.RelationToUser.Where(r => r.BrandMasterId == brandMasterId).Select(r => r.UserId).Contains(u.Id));
                whCls.Add(u => u.UserRole == UserRole.SuperMechandiser || u.UserRole == UserRole.Admin);
                var superMerchandisers = _userRepository.FindAll(whCls, "Id desc");
                return new ServiceResult<ICollection<User>>(superMerchandisers);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="brandMasterId"></param>
        /// <returns></returns>
        public ServiceResult<ICollection<User>> GetMerchandisers(int brandMasterId)
        {
            var whCls = new List<Expression<Func<User, bool>>>();
            whCls.Add(u => !u.Deleted);
            try
            {
                if (brandMasterId <= 0)
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "BrandMasterId must more than 0") });
                }
                var bmRs = GetById(brandMasterId);
                if (!bmRs.RuleViolations.IsNullOrEmpty())
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "Not found user with id = {0}".FormatWith(brandMasterId)) });
                }
                if (bmRs.Result.UserRole != UserRole.BrandMaster)
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "User with id = {0} isn't brand master".FormatWith(brandMasterId)) });
                }
                whCls.Add(u => u.RelationToUser.Where(r => r.BrandMasterId == brandMasterId).Select(r => r.UserId).Contains(u.Id));
                whCls.Add(u => u.UserRole == UserRole.Mechandiser);
                var merchandisers = _userRepository.FindAll(whCls, "Id desc");
                return new ServiceResult<ICollection<User>>(merchandisers);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ServiceResult<ICollection<User>> GetBrandMasters(int userId)
        {
            var whCls = new List<Expression<Func<User, bool>>>();
            whCls.Add(u => !u.Deleted);
            try
            {
                if (userId <= 0)
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "BrandMasterId must more than 0") });
                }
                var bmRs = GetById(userId);
                if (!bmRs.RuleViolations.IsNullOrEmpty())
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "Not found user with id = {0}".FormatWith(userId)) });
                }
                if (bmRs.Result.UserRole == UserRole.BrandMaster)
                {
                    return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Error", "User with id = {0} is brand master, so can't find relation with other user".FormatWith(userId)) });
                }
                whCls.Add(u => u.RelationToBrandMaster.Where(r => r.UserId == userId).Select(r => r.BrandMasterId).Contains(u.Id));
                whCls.Add(u => u.UserRole == UserRole.BrandMaster);
                var brandMasters = _userRepository.FindAll(whCls, "Id desc");
                return new ServiceResult<ICollection<User>>(brandMasters);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        public ServiceResult<ICollection<User>> GetAllMechandiserAndSuperMechandiser(int? status, string order, int? limit)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>
                {
                    c => c.UserRole == UserRole.Admin || 
                         c.UserRole == UserRole.SuperMechandiser||
                         c.UserRole == UserRole.Mechandiser
                };
                whCls.Add(c => !c.Deleted);
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                var users = limit != null
                                ? _userRepository.FindAll(whCls, (int)limit, order)
                                : _userRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<User>>(users);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        public ServiceResult<IPagedList<User>> GetGreenSignalUser(string keyword, int? status, int? userRole, string order, int pageIndex, int pageSize)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                whCls.Add(c => !c.Deleted);
                //if (mechandiserId != null)
                //{
                //    whCls.Add(c => c.MechandiserId == mechandiserId);
                //}
                if (!string.IsNullOrEmpty(keyword))
                {
                    whCls.Add(c => c.UserName.Contains(keyword));
                }
                if (userRole != null)
                {
                    whCls.Add(c => c.UserRole == (UserRole)userRole);
                }
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                PagedList<User> users = _userRepository.Paging(whCls, order, pageIndex, pageSize);
                return new ServiceResult<IPagedList<User>>(users);

            }
            catch (Exception e)
            {
                return new ServiceResult<IPagedList<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }

        public ServiceResult<IPagedList<User>> GetWarehouseUser(string keyword, int? status, int? userRole, string order, int pageIndex, int pageSize)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>
                {
                    c => c.IsWarehouseAccount() || c.IsAdmin()
                };
                whCls.Add(c => !c.Deleted);
                //if (mechandiserId != null)
                //{
                //    whCls.Add(c => c.MechandiserId == mechandiserId);
                //}
                if (!string.IsNullOrEmpty(keyword))
                {
                    whCls.Add(c => c.UserName.Contains(keyword));
                }
                if (userRole != null)
                {
                    whCls.Add(c => c.UserRole == (UserRole)userRole);
                }
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                PagedList<User> users = _userRepository.Paging(whCls, order, pageIndex, pageSize);
                return new ServiceResult<IPagedList<User>>(users);

            }
            catch (Exception e)
            {
                return new ServiceResult<IPagedList<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="role"></param>
        /// <param name="order"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ServiceResult<IPagedList<User>> GetAll(string keyword, int? status, int? role, string order, int pageIndex, int pageSize)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                whCls.Add(c => !c.Deleted);
                //if (mechandiserId != null)
                //{
                //    whCls.Add(c => c.MechandiserId == mechandiserId);
                //}
                if (!string.IsNullOrEmpty(keyword))
                {
                    whCls.Add(c => c.UserName.Contains(keyword));
                }
                if (role != null)
                {
                    whCls.Add(c => c.UserRole == (UserRole)role);
                }
                if (status != null)
                {
                    whCls.Add(c => c.Status == (UserStatus)status);
                }
                PagedList<User> users = _userRepository.Paging(whCls, order, pageIndex, pageSize);
                return new ServiceResult<IPagedList<User>>(users);

            }
            catch (Exception e)
            {
                return new ServiceResult<IPagedList<User>>(new[] { new RuleViolation("Exception", e.Message) });
            }
        }
 
        public virtual ServiceResult<User> Create(string userName, string password, string fullName, string avatar, string telephone, string ym, string skype, string email, int[] mechandiserIds, int[] superMechandiserIds, int[] brandMasterIds, int status, int userRole, int? brandNameId, int[] warehouseIds)
        {
            var userResult = GetByUserName(userName);
            if (userResult.Result == null)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    return new ServiceResult<User>(new[] { new RuleViolation("Error Message", "UserName is not empty") });
                }
                if (string.IsNullOrEmpty(password))
                {
                    return new ServiceResult<User>(new[] { new RuleViolation("Error Message", "Password is not empty") });
                }
                if (string.IsNullOrEmpty(email))
                {
                    return new ServiceResult<User>(new[] { new RuleViolation("Error Message", "Email is not empty") });
                }
                try
                {
                    var user = new User
                                   {
                                       UserName = userName,
                                       UserGuid = Guid.NewGuid(),
                                       Password = password,
                                       Status = (UserStatus)status,
                                       UserRole = (UserRole)userRole,
                                       FullName = fullName,
                                       Avatar = avatar,
                                       Telephone = telephone,
                                       YM = ym,
                                       Skype = skype,
                                       Email = email,
                                       CreatedDate = DateTime.Now,
                                       ModifiedDate = DateTime.Now,
                                       LastLoginDateUtc = DateTime.Now,
                                       DateOfBirth = null,
                                       Address = "",
                                       LastIpAddress = "",
                                       Cellphone = "",
                                       BrandNameId = brandNameId,
                                       //WarehouseId = warehouseId
                                   };
                    if (!warehouseIds.IsNullOrEmpty() && warehouseIds.Any())
                    {
                        foreach (var whId in warehouseIds)
                        {
                            var whRs = _vendorServices.GetById(whId);
                            if (whRs.RuleViolations.IsNullOrEmpty())
                            {
                                user.Warehouses.Add(whRs.Result);
                            }
                        }
                    }

                    _userRepository.Insert(user);
                    var newUser = GetByUserNameAndPassword(userName, password).Result;
                    if (userRole == (int)UserRole.BrandMaster)
                    {
                        if (superMechandiserIds != null && superMechandiserIds.Any())
                        {
                            foreach (var spMrdId in superMechandiserIds)
                            {
                                var r = new RelationToBrandMaster
                                {
                                    BrandMasterId = newUser.Id,
                                    UserId = spMrdId
                                };
                                _relationToBrandMasterServices.Create(r);
                            }
                        }
                        if (mechandiserIds != null && mechandiserIds.Any())
                        {
                            foreach (var mrdId in mechandiserIds)
                            {
                                var r = new RelationToBrandMaster
                                {
                                    BrandMasterId = newUser.Id,
                                    UserId = mrdId
                                };
                                _relationToBrandMasterServices.Create(r);
                            }
                        }
                    }
                    else
                    {
                        if (brandMasterIds != null && brandMasterIds.Any())
                        {
                            foreach (var bmId in brandMasterIds)
                            {
                                var r = new RelationToBrandMaster
                                {
                                    BrandMasterId = bmId,
                                    UserId = newUser.Id
                                };
                                _relationToBrandMasterServices.Create(r);
                            }
                        }
                    }
                    return new ServiceResult<User>(user);

                }
                catch (Exception e)
                {
                    return new ServiceResult<User>(new[] { new RuleViolation("Exception", e.Message) });
                }
            }
            return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Username's already existed") });
        }
        public virtual ServiceResult<User> Create(User user)
        {
            try
            {
                _userRepository.Insert(user);
                return new ServiceResult<User>(user);
            }
            catch (Exception ex)
            {

                return new ServiceResult<User>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Update data error:" +
                                                                                    ex.Message)
                                                              });
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual ServiceResult<User> Update(User user)
        {
            try
            {
                _userRepository.Update(user);
                return new ServiceResult<User>(user);
            }
            catch (Exception ex)
            {

                return new ServiceResult<User>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Update data error:" +
                                                                                    ex.Message)
                                                              });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual ServiceResult<User> Delete(User user)
        {
            try
            {
                if (user.Comments.Count > 0)
                {
                    return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Can't delete user because it has comment") });
                }
                if (user.BrandMasterOrders.Count > 0 || user.MechandiserOrders.Count > 0)
                {
                    return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Can't delete user because it has order") });
                }
                user.Deleted = true;
                _userRepository.Update(user);
                return new ServiceResult<User>(user);
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Delete data error:" + ex.Message) });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Validate(string userName, string password)
        {
            User user = GetByUserName(userName).Result;

            if (user == null || user.Status == UserStatus.IsDeleted || user.Status != UserStatus.IsPublish)
                return false;

            //string pwd = password.Hash();
            string pwd = password;

            bool isValid = pwd == user.Password;

            //save last login date
            if (isValid)
            {
                user.LastLoginDateUtc = DateTime.UtcNow;
                _userRepository.Update(user);
            }

            return isValid;
        }
    }
}
