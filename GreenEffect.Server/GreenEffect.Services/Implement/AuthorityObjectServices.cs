using GreenEffect.DomainObject.AuthorityObject;
using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCCore;
using MVCCore.Data;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{ 
    public class AuthorityObjectServices : IAuthorityObjectServices
    {
        private readonly IRepository<AuthorityObject> _repository;
        public AuthorityObjectServices(IRepository<AuthorityObject> repository)
        {
            _repository = repository;
        }
        public ServiceResult<ICollection<AuthorityObject>> GetAll(int idenUser)
        {
            var whCls = new List<Expression<Func<AuthorityObject, bool>>>();
            string prefixMatch = idenUser + ",";
            string suffixMatch = "," + idenUser;
            string innerMatch = "," + idenUser + ",";
            string idenUserStr = idenUser.ToString();
            whCls.Add(a => a.ObjectUser.StartsWith(prefixMatch) || a.ObjectUser.EndsWith(suffixMatch) ||
                a.ObjectUser.Contains(innerMatch) || (!a.ObjectUser.Contains(",") && a.ObjectUser.Equals(idenUserStr)));
            try
            {
                var authorityObjects = _repository.FindAll(whCls);
                return new ServiceResult<ICollection<AuthorityObject>>(authorityObjects);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<AuthorityObject>>();
            }
        }
    }
}
