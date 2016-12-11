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
                return  new ServiceResult<List<Message>>(messages);
            }
            catch (Exception e)
            {
                return new ServiceResult<List<Message>>(new[] { new RuleViolation("Exception", "Get data error :" + e.Message) });
            }
        }

        public ServiceResult<ICollection<Message>> GetAll(int idenUser)
        {
            var whCls = new List<Expression<Func<Message, bool>>>();
            whCls.Add(a => a.ToId == idenUser );
            try
            {
                var messager = _messageRepository.FindAll(whCls);
                return new ServiceResult<ICollection<Message>>(messager);
            }
            catch (Exception ex)
            {
                return new ServiceResult<ICollection<Message>>();
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

                return new ServiceResult<Message>(new[]
                                                              {
                                                                  new RuleViolation("Ex",
                                                                                    "Update data error:" +
                                                                                    ex.Message)
               });
            }
        }
    }
}
