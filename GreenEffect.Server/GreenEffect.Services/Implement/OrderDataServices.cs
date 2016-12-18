using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using GreenEffect.DomainObject.Order;

namespace GreenEffect.Services.Implement
{
    public class OrderDataServices : IOrderDataServices
    {
        private readonly IRepository<OrderItem> _orderdataRepository;
        public OrderDataServices(IRepository<OrderItem> orderdataRepository)
        {
            _orderdataRepository = orderdataRepository;
        }
        public ServiceResult<OrderItem> GetById(int id)
        {
            try
            {
                OrderItem orderdata = _orderdataRepository.FindById(id);
                return orderdata != null
                        ? new ServiceResult<OrderItem>(orderdata)
                        : new ServiceResult<OrderItem>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<OrderItem>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        
    }
}
