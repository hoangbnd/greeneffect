using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Implement
{
    public class LocationServices : ILocationServices
    {
        private readonly IRepository<Location> _customerslocationRepository;
        public LocationServices(IRepository<Location> customerslocationRepository)
        {
            _customerslocationRepository = customerslocationRepository;
        }
        public ServiceResult<Location> GetById(int id)
        {
            try
            {
                Location location = _customerslocationRepository.FindById(id);
                return location != null
                        ? new ServiceResult<Location>(location)
                        : new ServiceResult<Location>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Location>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<ICollection<Location>> GetByUser(int userId)
        {
            try
            {
                var whCls = new List<Expression<Func<Location, bool>>>();
                if (userId > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Customer != null && c.Customer.UserId.Equals(userId));
                }
                var customerslocation = _customerslocationRepository.FindAll(whCls);

                return new ServiceResult<ICollection<Location>>(customerslocation);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Location>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<Location> Create(Location customerslocation)
        {
            try
            {
                _customerslocationRepository.Insert(customerslocation);
                return new ServiceResult<Location>(customerslocation);
            }
            catch (Exception ex)
            {

                return new ServiceResult<Location>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }

    }
}
