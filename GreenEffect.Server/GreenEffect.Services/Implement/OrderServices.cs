using GreenEffect.DomainObject.Order;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Order> _orderRepository;
        public OrderServices(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public ServiceResult<Order> GetById(int id)
        {
            try
            { 
                Order order = _orderRepository.FindById(id);
                return order != null
                        ? new ServiceResult<Order>(order)
                        : new ServiceResult<Order>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Order>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<Order> Create(Order order)
        {
            try
            {
                _orderRepository.Insert(order);
                return new ServiceResult<Order>(order);
            }
            catch (Exception ex)
            {
                return new ServiceResult<Order>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Insert data error:" +
                                                                                    ex.Message)
               });
            }
        }
        public ServiceResult<Order> Update(Order order)
        {
            try
            {
                _orderRepository.Update(order);
                return new ServiceResult<Order>(order);
            }
            catch (Exception ex)
            {

                return new ServiceResult<Order>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Update data error:" +
                                                                                    ex.Message)
               });
            }
        }
    }
}
