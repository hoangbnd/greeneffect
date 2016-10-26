using GreenEffect.DomainObject.Route;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace GreenEffect.Services.Implement
{
  public  class RouteSevices:IRouteSevice
    {
       private readonly IRepository<Route> _RouteRepository;
       public RouteSevices(IRepository<Route> RouteRepository)
       {
           _RouteRepository = RouteRepository;
       } 

       public ServiceResult<ICollection<Route>> GetByUser(int IdenUser)
       {
           try
           {
               var whCls = new List<Expression<Func<Route, bool>>>();
               if (IdenUser>0)//check dk co hay ko?
               {
                   whCls.Add(r => r.UserId.Equals(IdenUser));//neu co thi check username chua (Contains) dk, 
                   //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
               }
               var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
               //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
               //thi order = "UserName asc"
               var Routes = _RouteRepository.FindAll(whCls, order);

               return new ServiceResult<ICollection<Route>>(Routes);
           }
           catch (Exception ex)
           {
               return new ServiceResult<ICollection<Route>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
           }
       }
    }
}
