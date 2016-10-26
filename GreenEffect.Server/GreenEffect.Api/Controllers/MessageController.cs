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
        public JsonModel<List<MessageApiModel>> GetAll(int idenUser)
        {
            var listmess = new List<MessageApiModel>();
            var messager = _messageServices.GetAll(idenUser);
            if (messager.RuleViolations.IsNullOrEmpty())
            {

                listmess = messager.Result.Select(o => new MessageApiModel
                {
                    Id = o.Id,
                    FromId = o.FromId,
                    ToId = o.ToId,
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
           
            var MessagerRs = _messageServices.GetById(model.Id);
            if (MessagerRs.RuleViolations.IsNullOrEmpty())
            {
                var mess = MessagerRs.Result;
                var deleteResult = _messageServices.Delete(mess);
                //kiemtra xoa
                if (deleteResult.RuleViolations.IsNullOrEmpty())
                {
                    //neu xoa thanh cong thi tra ve du lieu con lai
                    return new JsonModel<MessageApiModel>
                    {
                        IsSuccessful = true,
                        Data = new MessageApiModel
                        {
                            Id = mess.Id,
                            FromId = mess.FromId,
                            ToId = mess.ToId,
                            IsRead = mess.IsRead,
                            Content = mess.Content            
                        }
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
                Message = MessagerRs.RuleViolations[0].ErrorMessage
            };
        }

    }
}