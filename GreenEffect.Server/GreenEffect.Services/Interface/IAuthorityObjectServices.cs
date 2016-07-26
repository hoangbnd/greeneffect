using GreenEffect.DomainObject.AuthorityObject;
using MVCCore;
using System.Collections.Generic;

namespace GreenEffect.Services.Interface
{
    public interface IAuthorityObjectServices
    {
        ServiceResult<ICollection<AuthorityObject>> GetAll(int IdenUser);
    }
}
