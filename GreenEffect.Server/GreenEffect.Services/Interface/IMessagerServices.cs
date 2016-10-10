using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Messager;
using MVCCore;
namespace GreenEffect.Services.Interface
{
    public interface IMessagerServices
    {
        ServiceResult<ICollection<Messager>> GetAll(int IdenUser);
        ServiceResult<Messager> Delete(Messager messager);
        ServiceResult<Messager> GetById(int id);
    }
}
