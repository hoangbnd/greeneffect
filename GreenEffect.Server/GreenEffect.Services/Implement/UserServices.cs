﻿using GreenEffect.DomainObject.User;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Services.Implement
{
    public class UserServices :IUserServices
    {
        private readonly IRepository<User> _userRepository;
        public UserServices(IRepository<User> userRepository) {
            _userRepository = userRepository;
        }

        public ServiceResult<User> GetById(int id)
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

        public ServiceResult<User> GetByUserNameAndPassword(string userName, string password)
        {
            try
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                if (!string.IsNullOrEmpty(userName))//check dk co hay ko?
                {
                    whCls.Add(c => c.UserName.Equals(userName));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }
                if (!string.IsNullOrEmpty(password))
                {
                    whCls.Add(c => c.Password.Equals(password));
                }
                    
                var users = _userRepository.Find(whCls);

                return new ServiceResult<User>(users);
            }
            catch (Exception ex)
            {
                return new ServiceResult<User>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<ICollection<User>> GetAll(string searchUsername)
        {
            try 
            {
                var whCls = new List<Expression<Func<User, bool>>>();
                if (!string.IsNullOrEmpty(searchUsername))//check dk co hay ko?
                {
                    whCls.Add(c => c.UserName.Contains(searchUsername));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }
                
                var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                //thi order = "UserName asc"
                var users = _userRepository.FindAll(whCls, order);
                
                return new ServiceResult<ICollection<User>>(users);
            }
            catch (Exception ex) {
                return new ServiceResult<ICollection<User>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }

        public ServiceResult<User> Create(User user)
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
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }

        public ServiceResult<User> Update(User user)
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

        public ServiceResult<User> Delete(User user)
        {
            throw new NotImplementedException();
        }

        public bool Validate(string userName, string password)
        {
            throw new NotImplementedException();
        }


        public object GetUserById(int p)
        {
            throw new NotImplementedException();
        }


      

        //public ServiceResult<User> GetByUserNameAndPassword(string userName, string password)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
