using GreenEffect.DomainObject.CustomersRoutes;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{
    public class CustomerRouteServices : ICustomersRoutesServices
    {
        private readonly IRepository<CustomersRoutes> _customersRouteRepository;
        public CustomerRouteServices(IRepository<CustomersRoutes> customersRouteRepository)
        {
            _customersRouteRepository = customersRouteRepository;
        }
        public ServiceResult<ICollection<CustomersRoutes>> GetByRoute(int IdenRoute)
        {
            try
            {
                var whCls = new List<Expression<Func<CustomersRoutes, bool>>>();
                if (IdenRoute > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.IdenRoute.Equals(IdenRoute));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }

                var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                //thi order = "UserName asc"
                var custormesroute = _customersRouteRepository.FindAll(whCls, order);

                return new ServiceResult<ICollection<CustomersRoutes>>(custormesroute);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<CustomersRoutes>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
    }
}