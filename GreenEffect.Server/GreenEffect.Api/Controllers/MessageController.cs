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

        [HttpPost]
        public JsonModel<int> Get(MessageApiModel model)
        {
            var rs = new BasePagedModel<MessageApiModel>();
            if (model == null)
            {
                return new JsonModel<int>
                {
                    IsSuccessful = false,
                    Message = "Có lỗi trong quá trình xử lý. Vui lòng đăng nhập lại."
                };
            }
            var messageRs = _messageServices.CountNewNotice(model.UserId);
            if (messageRs.RuleViolations.IsNullOrEmpty())
            {
               return new JsonModel<int>
                {
                    IsSuccessful = true,
                    Data = messageRs.Result
               };
            }
            return new JsonModel<int>
            {
                IsSuccessful = false,
                Message = messageRs.RuleViolations[0].ErrorMessage
            };
        }
        [HttpPost]
        public JsonModel<BasePagedModel<MessageApiModel>> GetAll(MessageApiModel model, int pageIndex)
        {
            var rs = new BasePagedModel<MessageApiModel>();
            if (model == null)
            {
                return new JsonModel<BasePagedModel<MessageApiModel>>
                {
                    IsSuccessful = false,
                    Message = "Có lỗi trong quá trình xử lý. Vui lòng đăng nhập lại."
                };
            }
            var messageRs = _messageServices.GetAll(model.UserId, pageIndex, 20);
            if (messageRs.RuleViolations.IsNullOrEmpty())
            {
                if (messageRs.Result != null)
                {
                    var data = messageRs.Result.Select(o => new MessageApiModel
                    {
                        Id = o.Id,
                        FromId = o.FromId,
                        ToIds = new[] { o.ToId },
                        IsRead = o.IsRead,
                        Title = o.Title,
                        Content = o.Content,
                        DateTime = o.DateTime,

                    }).OrderByDescending(i => i.Id).ToList();
                    rs.Data = data;
                }

                return new JsonModel<BasePagedModel<MessageApiModel>>
                {
                    IsSuccessful = true,
                    Data = rs
                };
            }
            return new JsonModel<BasePagedModel<MessageApiModel>>
            {
                IsSuccessful = false,
                Message = messageRs.RuleViolations[0].ErrorMessage
            };
        }
        [HttpPost]
        public JsonModel<MessageApiModel> Read(MessageApiModel model)
        {

            var messagerRs = _messageServices.GetById(model.Id);
            if (messagerRs.RuleViolations.IsNullOrEmpty())
            {
                var mess = messagerRs.Result;
                mess.IsRead = true;
                var deleteResult = _messageServices.Update(mess);
                //kiemtra xoa
                if (deleteResult.RuleViolations.IsNullOrEmpty())
                {
                    //neu xoa thanh cong thi tra ve du lieu con lai
                    return new JsonModel<MessageApiModel>
                    {
                        IsSuccessful = true,
                        Data = null
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
            var msgs = model.ToIds.Select(toId => new Message()
            {
                Content = model.Content,
                DateTime = DateTime.Now,
                FromId = model.FromId,
                Title = model.Title,
                ToId = toId,
                IsRead = false
            }).ToList();

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