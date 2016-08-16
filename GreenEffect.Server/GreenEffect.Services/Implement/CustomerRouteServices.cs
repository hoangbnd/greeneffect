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
        public ServiceResult<ICollection<CustomersRoutes>> GetByRoute(int RouteID)
        {
            try
            {
                var whCls = new List<Expression<Func<CustomersRoutes, bool>>>();
                if (RouteID > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.RouteID.Equals(RouteID));
                }

                var order = "Id desc";
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