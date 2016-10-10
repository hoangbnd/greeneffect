using GreenEffect.DomainObject.Messager;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{
    public class MessagerServices : IMessagerServices
    {
        private readonly IRepository<Messager> _MessagerRepository;
        public MessagerServices(IRepository<Messager> MessagerRepository)
        {
            _MessagerRepository = MessagerRepository;
        }
        public ServiceResult<Messager> GetById(int id)
        {
            try
            {
                Messager user = _MessagerRepository.FindById(id);
                return user != null
                        ? new ServiceResult<Messager>(user)
                        : new ServiceResult<Messager>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Messager>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<ICollection<Messager>> GetAll(int idenUser)
        {
            var whCls = new List<Expression<Func<Messager, bool>>>();
            string prefixMatch = idenUser + ",";
            string suffixMatch = "," + idenUser;
            string innerMatch = "," + idenUser + ",";
            string idenUserStr = idenUser.ToString();
            whCls.Add(a => a.FromUser.StartsWith(prefixMatch) || a.FromUser.EndsWith(suffixMatch) ||
                a.FromUser.Contains(innerMatch) || (!a.FromUser.Contains(",") && a.FromUser.Equals(idenUserStr)));
            try
            {
                var messager = _MessagerRepository.FindAll(whCls);
                return new ServiceResult<ICollection<Messager>>(messager);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Messager>>();
            }
        }
        public ServiceResult<Messager> Delete(Messager messager)
        {
            try
            {
                _MessagerRepository.Delete(messager);
                return new ServiceResult<Messager>(messager);
            }
            catch (Exception ex)
            {

                return new ServiceResult<Messager>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Update data error:" +
                                                                                    ex.Message)
               });
            }
        }
    }
}
