using GreenEffect.DomainObject.Message;
using GreenEffect.Services.Interface;
using MVCCore;
using MVCCore.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GreenEffect.Services.Implement
{
    public class MessageServices : IMessageServices
    {
        private readonly IRepository<Message> _messageRepository;
        public MessageServices(IRepository<Message> messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public ServiceResult<Message> Update(Message message)
        {
            try
            {
                _messageRepository.Update(message);
                return new ServiceResult<Message>(message);
            }
            catch (Exception ex)
            {
                return new ServiceResult<Message>(new[] { new RuleViolation("Ex", "Update data error:" + ex.Message) });
            }
        }

        public ServiceResult<Message> GetById(int id)
        {
            try
            {
                Message user = _messageRepository.FindById(id);
                return user != null
                        ? new ServiceResult<Message>(user)
                        : new ServiceResult<Message>(new[] { new RuleViolation("ErrorMsg", "Have no user with id is {0} ".FormatWith(id)) });
            }
            catch (Exception e)
            {
                return new ServiceResult<Message>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        public ServiceResult<List<Message>> Create(List<Message> messages)
        {
            try
            {
                _messageRepository.Insert(messages);
                return new ServiceResult<List<Message>>(messages);
            }
            catch (Exception e)
            {
                return new ServiceResult<List<Message>>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        public ServiceResult<int> CountNewNotice(int userId)
        {
            var whCls = new List<Expression<Func<Message, bool>>> { a => !a.IsRead, a => a.ToId == userId };
            try
            {
                var messages = _messageRepository.Count(whCls);
                return new ServiceResult<int>(messages);
            }
            catch (Exception e)
            {
                return new ServiceResult<int>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        public ServiceResult<PagedList<Message>> GetAll(int idenUser, int pageIndex, int pageSize)
        {
            var whCls = new List<Expression<Func<Message, bool>>> { a => a.ToId == idenUser };
            try
            {
                var messages = _messageRepository.Paging(whCls, "Id desc", pageIndex, pageSize);
                return new ServiceResult<PagedList<Message>>(messages);
            }
            catch (Exception e)
            {
                return new ServiceResult<PagedList<Message>>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        public ServiceResult<ICollection<Message>> GetAll(int idenUser)
        {
            var whCls = new List<Expression<Func<Message, bool>>> { a => a.ToId == idenUser };
            try
            {
                var messages = _messageRepository.FindAll(whCls, 50, "Id desc");
                return new ServiceResult<ICollection<Message>>(messages);
            }
            catch (Exception e)
            {
                return new ServiceResult<ICollection<Message>>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }
        public ServiceResult<Message> Delete(Message message)
        {
            try
            {
                _messageRepository.Delete(message);
                return new ServiceResult<Message>(message);
            }
            catch (Exception ex)
            {
                return new ServiceResult<Message>(new[] { new RuleViolation("Ex", "Update data error:" + ex.Message) });
            }
        }
    }
}
