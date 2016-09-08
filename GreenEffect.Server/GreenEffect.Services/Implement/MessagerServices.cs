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
    }
}
