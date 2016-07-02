using GreenEffect.DomainObject.Customers;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{
    public  class CustomersSevices:ICustomersServices
    {
        private readonly IRepository<Customers> _customersRepository;
        public CustomersSevices(IRepository<Customers> customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public ServiceResult<Customers> GetById(int id)
        {
            try
            {
                Customers customers = _customersRepository.FindById(id);
                return customers != null
                        ? new ServiceResult<Customers>(customers)
                        : new ServiceResult<Customers>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Customers>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
       
        public ServiceResult<ICollection<Customers>> GetAll(string searchCustomersId, string searchCustomersName, string customersAddress, string customersPhone)
        {
            try
            {
                var whCls = new List<Expression<Func<Customers, bool>>>();
                if (!string.IsNullOrEmpty(searchCustomersId))//check dk co hay ko?
                {
                    whCls.Add(c => c.CustomersId.Contains(searchCustomersId));//neu co thi check username chua (Contains) dk, 
                    //neu dk yeu cau bang thi co 2 cach c.UserName == "username" hoac c.UserName.Equals("username")
                }
                if (!string.IsNullOrEmpty(searchCustomersName))
                {
                    whCls.Add(c => c.CustomersName.Contains(searchCustomersName));
                }
                if (!string.IsNullOrEmpty(customersAddress))
                {
                    whCls.Add(c => c.Adress.Contains(customersAddress));
                }
                if (!string.IsNullOrEmpty(customersPhone))
                {
                    whCls.Add(c => c.Phone.Contains(customersPhone));
                }
                var order = "Id desc";//truong sap xep co quy dinh, "Tentruong kieusapxep" 
                //VD: sap xep theo username, kieusapxep co 2 loai "asc"(tang dan) va "desc" giam dan 
                //thi order = "UserName asc"
                var custormes = _customersRepository.FindAll(whCls, order);

                return new ServiceResult<ICollection<Customers>>(custormes);
            }
            catch (Exception ex) {
                return new ServiceResult<ICollection<Customers>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }


        public ServiceResult<Customers> Create(Customers customer)
        {
            try
            {
                _customersRepository.Insert(customer);
                return new ServiceResult<Customers>(customer);
            }
            catch (Exception ex)
            {

                return new ServiceResult<Customers>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }

        public ServiceResult<Customers> Update(Customers customers)
        {
            try
            {
                _customersRepository.Update(customers);
                return new ServiceResult<Customers>(customers);
            }
            catch (Exception ex)
            {

                return new ServiceResult<Customers>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Update data error:" +
                                                                                    ex.Message)
               });
            }
        }

        public ServiceResult<Customers> Delete(Customers customers)
        {
            throw new NotImplementedException();
        }

        public bool Validate(string customersId, string customersName)
        {
            throw new NotImplementedException();
        }


        public object GetCustomersById(int p)
        {
            throw new NotImplementedException();
        }


        //public ServiceResult<Customers> GetByUserNameAndPassword(string userName, string password)
        //{
        //    throw new NotImplementedException();
        //}


        public IRepository<Customers> CustomersRepository { get; set; }


        public bool Validate(string searchCustomersId, string searchCustomersName, string searchCustomersAddress, string searchCustomersPhone)
        {
            throw new NotImplementedException();
        }

    }
}
