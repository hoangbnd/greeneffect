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
                Order user = _orderRepository.FindById(id);
                return user != null
                        ? new ServiceResult<Order>(user)
                        : new ServiceResult<Order>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Order>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<ICollection<Order>> GetByOrder(string OrderName, int disable)
        {
            try
            {
                var whCls = new List<Expression<Func<Order, bool>>>();
                if (!string.IsNullOrEmpty(OrderName))//check dk co hay ko?
                {
                    whCls.Add(c => c.OrderName.Contains(OrderName));
                }
                if (disable == 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Disable.Equals(disable));
                }
                var order = "Id desc";
                var _oder = _orderRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<Order>>(_oder);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Order>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<ICollection<Order>> GetByUser(int IdenUser, int disable)
        {
            try
            {
                var whCls = new List<Expression<Func<Order, bool>>>();
                if (IdenUser>=1)//check dk co hay ko?
                {
                    whCls.Add(c => c.IdenUser.Equals(IdenUser));
                

                }
                if (disable == 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Disable.Equals(disable));
                }
                var order = "Id desc";
                var _oder = _orderRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<Order>>(_oder);
              
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Order>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<ICollection<Order>> GetByRoute(int IdenRouter, int disable)
        {
            try
            {
                var whCls = new List<Expression<Func<Order, bool>>>();
                if (IdenRouter >= 1)//check dk co hay ko?
                {
                    whCls.Add(c => c.IdenRoute.Equals(IdenRouter));
                }
                if ( disable == 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Disable.Equals(disable));
                }
                var order = "Id desc";
                var _oder = _orderRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<Order>>(_oder);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Order>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
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
    }
}
