using System;
using System.Collections.Generic;
using GreenSign.DomainObject;
using MVCCore;

namespace GreenSign.Services
{
    public interface IUserServices
    {
        ServiceResult<User> GetById(int id);
        ServiceResult<User> GetByGuid(Guid guid);
        ServiceResult<User> GetByUserName(string name);
        ServiceResult<User> GetByEmail(string email);
        ServiceResult<User> GetByUserNameAndPassword(string userName, string password);
        ServiceResult<ICollection<User>> Get(int? brandNameId, int? status, int? userRole, string order, int? limit);
        ServiceResult<ICollection<User>> Get(int? status, int? userRole, string order, int? limit);
        ServiceResult<ICollection<User>> GetInRole(int? status, int? userRole, string order);
        ServiceResult<ICollection<User>> GetSuperMechandisers(int brandMasterId);
        ServiceResult<ICollection<User>> GetMerchandisers(int brandMasterId);
        ServiceResult<ICollection<User>> GetBrandMasters(int userId);
        
        ServiceResult<ICollection<User>> GetAllMechandiserAndSuperMechandiser(int? status, string order, int? limit);
        ServiceResult<IPagedList<User>> GetGreenSignalUser(string keyword, int? status, int? userRole, string order, int pageIndex, int pageSize);
        ServiceResult<IPagedList<User>> GetWarehouseUser(string keyword, int? status, int? userRole, string order, int pageIndex, int pageSize); 
        ServiceResult<IPagedList<User>> GetAll(string keyword, int? status, int? userRole, string order, int pageIndex, int pageSize);
        ServiceResult<User> Create(string userName, string password, string fullName, string avatar, string telephone, string ym, string skype, string email, int[] mechandiserIds, int[] superMechandiserIds, int[] brandMasterIds, int status, int userRole, int? brandNameId, int[] warehouseIds);
        ServiceResult<User> Create(User user);
        ServiceResult<User> Update(User user);
        ServiceResult<User> Delete(User user);

        bool Validate(string userName, string password);
    }
}
