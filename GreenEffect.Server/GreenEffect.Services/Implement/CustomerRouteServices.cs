using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Implement
{
    public class CustomerRouteServices : ICustomersRoutesServices
    {
        private readonly IRepository<CustomerRoute> _customersRouteRepository;
        public CustomerRouteServices(IRepository<CustomerRoute> customersRouteRepository)
        { 
            _customersRouteRepository = customersRouteRepository;
        }
        public ServiceResult<ICollection<CustomerRoute>> GetByRoute(int RouteID)
        {
            try
            {
                var whCls = new List<Expression<Func<CustomerRoute, bool>>>();
                if (RouteID > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.RouteId.Equals(RouteID));
                }

                var order = "Id desc";
                var custormesroute = _customersRouteRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<CustomerRoute>>(custormesroute);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<CustomerRoute>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
    }
}