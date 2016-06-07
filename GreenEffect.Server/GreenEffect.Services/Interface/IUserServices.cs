using GreenEffect.DomainObject.User;
using MVCCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenEffect.Services.Interface
{
    public interface IUserServices
    {
        ServiceResult<ICollection<User>> GetAll(string searchUsername, string searchPassword, string datetime);
        ServiceResult<User> GetById(int id);
        ServiceResult<User> GetByUserNameAndPassword(string userName, string password,string datetime,int loctheo);
        ServiceResult<User> Create(User user);
        ServiceResult<User> Update(User user);
        ServiceResult<User> Delete(User user);

        bool Validate(string userName, string password);

        object GetUserById(int p);
    }
}
