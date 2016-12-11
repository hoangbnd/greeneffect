using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GreenEffect.DomainObject.Customer;

namespace GreenEffect.Services.Implement
{
    public class CustomerSevices : ICustomerServices
    {
        private readonly IRepository<Customer> _customersRepository;
        public CustomerSevices(IRepository<Customer> customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public ServiceResult<Customer> GetById(int id)
        {
            try
            {
                Customer customer = _customersRepository.FindById(id);
                return customer != null
                        ? new ServiceResult<Customer>(customer)
                        : new ServiceResult<Customer>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Customer>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        //
        public ServiceResult<ICollection<Customer>> GetAll(string searchCustomersId, string searchCustomersName, string customersAddress, string customersPhone)
        {
            try
            {
                var whCls = new List<Expression<Func<Customer, bool>>>();
                if (!string.IsNullOrEmpty(searchCustomersId))//check dk co hay ko?
                {
                    whCls.Add(c => c.CustomerCode.Equals(searchCustomersId));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }
                if (!string.IsNullOrEmpty(searchCustomersName))
                {
                    whCls.Add(c => c.CustomerName.Equals(searchCustomersName));
                }
                if (!string.IsNullOrEmpty(customersAddress))
                {
                    whCls.Add(c => c.Address.Equals(customersAddress));
                }
                if (!string.IsNullOrEmpty(customersPhone))
                {
                    whCls.Add(c => c.Phone.Equals(customersPhone));
                }
                var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                //thi order = "UserName asc"
                var custormes = _customersRepository.FindAll(whCls, order);

                return new ServiceResult<ICollection<Customer>>(custormes);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Customer>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }

        public ServiceResult<ICollection<Customer>> GetByUser(int userId)
        {
            try
            {
                var whCls = new List<Expression<Func<Customer, bool>>>();
                if (userId > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.UserId.Equals(userId));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }

                var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                //thi order = "UserName asc"
                var custormes = _customersRepository.FindAll(whCls, order);

                return new ServiceResult<ICollection<Customer>>(custormes);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Customer>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }

        public ServiceResult<ICollection<Customer>> GetByIden(int routeId)
        {
            try
            {
                var whCls = new List<Expression<Func<Customer, bool>>>();
                if (routeId > 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.RouteId.Equals(routeId));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }
                var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                //thi order = "UserName asc"
                var custormes = _customersRepository.FindAll(whCls, order);

                return new ServiceResult<ICollection<Customer>>(custormes);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Customer>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        //Creat Customer
        public ServiceResult<Customer> Create(Customer customer)
        {
            try
            {
                _customersRepository.Insert(customer);
                return new ServiceResult<Customer>(customer);
            }
            catch (Exception ex)
            {

                return new ServiceResult<Customer>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }



        public bool Validate(string customersId, string customersName)
        {
            throw new NotImplementedException();
        }


        public object GetCustomersById(int p)
        {
            throw new NotImplementedException();
        }

        public IRepository<Customer> CustomersRepository { get; set; }
        public bool Validate(string searchCustomersId, string searchCustomersName, string searchCustomersAddress, string searchCustomersPhone)
        {
            throw new NotImplementedException();
        }

    }
}
