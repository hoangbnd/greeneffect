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
        private readonly IRepository<OrderData> _orderdataRepository;
        public OrderDataServices(IRepository<OrderData> orderdataRepository)
        {
            _orderdataRepository = orderdataRepository;
        }
        public ServiceResult<OrderData> GetById(int id)
        {
            try
            {
                OrderData orderdata = _orderdataRepository.FindById(id);
                return orderdata != null
                        ? new ServiceResult<OrderData>(orderdata)
                        : new ServiceResult<OrderData>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<OrderData>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<ICollection<OrderData>> GetByOrder(string OrderName, int disable)
        {
            try
            {
                var whCls = new List<Expression<Func<OrderData, bool>>>();
                if (!string.IsNullOrEmpty(OrderName))//check dk co hay ko?
                {
                    whCls.Add(c => c.OrderName.Contains(OrderName));
                }
                if (disable == 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Disable.Equals(disable));
                }
                var order = "Id desc";
                var _oder = _orderdataRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<OrderData>>(_oder);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<OrderData>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<ICollection<OrderData>> GetByUser(int IdenUser, int disable)
        {
            try
            {
                var whCls = new List<Expression<Func<OrderData, bool>>>();
                if (IdenUser >= 1)//check dk co hay ko?
                {
                    whCls.Add(c => c.UserId.Equals(IdenUser));


                }
                if (disable == 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Disable.Equals(disable));
                }
                var order = "Id desc";
                var _oder = _orderdataRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<OrderData>>(_oder);

            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<OrderData>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
        public ServiceResult<ICollection<OrderData>> GetByRoute(int IdenRouter, int disable)
        {
            try
            {
                var whCls = new List<Expression<Func<OrderData, bool>>>();
                if (IdenRouter >= 1)//check dk co hay ko?
                {
                    whCls.Add(c => c.RouteId.Equals(IdenRouter));
                }
                if (disable == 0)//check dk co hay ko?
                {
                    whCls.Add(c => c.Disable.Equals(disable));
                }
                var order = "Id desc";
                var _oder = _orderdataRepository.FindAll(whCls, order);
                return new ServiceResult<ICollection<OrderData>>(_oder);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<OrderData>>(new[] { new RuleViolation("Exception", "Get data error :" + ex.Message) });
            }
        }
    }
}
