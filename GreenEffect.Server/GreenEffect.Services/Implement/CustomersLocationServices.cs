using GreenEffect.DomainObject.CustomersLocation;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace GreenEffect.Services.Implement
{
    public class CustomersLocationServices : ICustomersLocationServices
    {
        private readonly IRepository<CustomersLocation> _customerslocationRepository;
        public CustomersLocationServices(IRepository<CustomersLocation> customerslocationRepository)
        {
            _customerslocationRepository = customerslocationRepository;
        }
        public ServiceResult<CustomersLocation> GetById(int id)
        {
            try
            {
                CustomersLocation customersLocation = _customerslocationRepository.FindById(id);
                return customersLocation != null
                        ? new ServiceResult<CustomersLocation>(customersLocation)
                        : new ServiceResult<CustomersLocation>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<CustomersLocation>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<CustomersLocation> GetByUser(int userID)
        {
            try
            {
                var whCls = new List<Expression<Func<CustomersLocation, bool>>>();
                if (userID > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.UserID.Equals(userID));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }
                var customerslocation = _customerslocationRepository.Find(whCls);

                return new ServiceResult<CustomersLocation>(customerslocation);
            }
            catch (Exception ex)
            {
                return new ServiceResult<CustomersLocation>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<CustomersLocation> Create(CustomersLocation customerslocation)
        {
            try
            {
                _customerslocationRepository.Insert(customerslocation);
                return new ServiceResult<CustomersLocation>(customerslocation);
            }
            catch (Exception ex)
            {

                return new ServiceResult<CustomersLocation>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }

    }
}
