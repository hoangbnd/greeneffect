using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Message;

namespace GreenEffect.Api.Controllers
{
    public class MessageController : ApiController
    {
        private readonly IMessageServices _messageServices;

        public MessageController(IMessageServices messageServices)
        {
            _messageServices = messageServices;
        }
        public JsonModel<List<MessageApiModel>> GetAll(int userId)
        {
            var messager = _messageServices.GetAll(userId);
            if (messager.RuleViolations.IsNullOrEmpty())
            {
                var listmess = messager.Result.Select(o => new MessageApiModel
                {
                    Id = o.Id,
                    FromId = o.FromId,
                    ToIds = new[] { o.ToId },
                    IsRead = o.IsRead,
                    Content = o.Content,
                    DateTime = o.DateTime,

                }).OrderByDescending(i => i.Id).ToList();
                return new JsonModel<List<MessageApiModel>>
                {
                    IsSuccessful = true,
                    Data = listmess
                };
            }
            return new JsonModel<List<MessageApiModel>>
            {
                IsSuccessful = false,
                Message = messager.RuleViolations[0].ErrorMessage
            };
        }
        //Update Password
        [HttpPost]
        public JsonModel<MessageApiModel> Delete(MessageApiModel model)
        {

            var messagerRs = _messageServices.GetById(model.Id);
            if (messagerRs.RuleViolations.IsNullOrEmpty())
            {
                var mess = messagerRs.Result;
                var deleteResult = _messageServices.Delete(mess);
                //kiemtra xoa
                if (deleteResult.RuleViolations.IsNullOrEmpty())
                {
                    //neu xoa thanh cong thi tra ve du lieu con lai
                    return new JsonModel<MessageApiModel>
                    {
                        IsSuccessful = true,
                        Data = null,
                        Message = "Đã xóa thành công"
                    };
                }
                //delete khong thanh cong tra ve loi
                return new JsonModel<MessageApiModel>
                {
                    IsSuccessful = false,
                    Message = deleteResult.RuleViolations[0].ErrorMessage
                };
            }
            //tra ve loi khi khong lay duoc
            return new JsonModel<MessageApiModel>
            {
                IsSuccessful = false,
                Message = messagerRs.RuleViolations[0].ErrorMessage
            };
        }

        [HttpPost]
        public JsonModel<MessageApiModel> Send(MessageApiModel model)
        {
            var msgs = new List<Message>();
            foreach (var toId in model.ToIds)
            {
                var msg = new Message()
                {
                    Content = model.Content,
                    DateTime = DateTime.Now,
                    FromId = model.FromId,
                    ToId = toId,
                    IsRead = false
                };
                msgs.Add(msg);
            }

            var messagerRs = _messageServices.Create(msgs);
            if (messagerRs.RuleViolations.IsNullOrEmpty())
            {
                return new JsonModel<MessageApiModel>
                {
                    IsSuccessful = true,
                    Data = null,
                    Message = "Gửi thành công"
                };
            }
            //tra ve loi khi khong lay duoc
            return new JsonModel<MessageApiModel>
            {
                IsSuccessful = false,
                Message = messagerRs.RuleViolations[0].ErrorMessage
            };
        }
    }
}