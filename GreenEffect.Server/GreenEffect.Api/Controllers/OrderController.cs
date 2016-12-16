using GreenEffect.Services.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MVCCore;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject.Order;
using GreenEffect.DomainObject.Products;
using Newtonsoft.Json;

namespace GreenEffect.Api.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [System.Web.Http.HttpPost]
        public JsonModel<OrderApiModel> Create(OrderApiModel model)
        {
            NameValueCollection nvc = HttpContext.Current.Request.Form;
            foreach (var key in nvc.AllKeys)
            {
                var name = HttpContext.Current.Request.Form[key];
            }
            //var model = JsonConvert.DeserializeObject<OrderApiModel>(nvc["model"]); 
            //var model = new OrderApiModel();
            if (model != null)//check null
            {
                var order = new Order
                {
                    CustomerId = model.CustomerId,
                    UserId = model.UserId,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    Datetime = DateTime.Now,
                    Disable = model.Disable
                };
                if (model.OrderItems == null || model.OrderItems.Count <= 0)
                {
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = false,
                        Message = "Vui lòng thêm sản phẩm vào đơn hàng."
                    };
                }
                var orderItems = model.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId, Quantity = item.Quantity
                }).ToList();
                order.OrderItems = orderItems;
                

                var orderResult = _orderServices.Create(order);
                if (orderResult.RuleViolations.IsNullOrEmpty())
                {
                    //order.Images = SaveImages(orderResult.Result.Id, model.Files);
                    order.Images = "";
                    var updateResult = _orderServices.Update(order);
                    //kiem tra ket qua update
                    if (updateResult.RuleViolations.IsNullOrEmpty())
                    {
                        //neu update thanh cong thi tra ve user da duoc cap nhat password
                        return new JsonModel<OrderApiModel>
                        {
                            IsSuccessful = true,
                            Data = null,
                            Message = "Tạo đơn hàng thành công"
                        };
                    }
                    //update khong thanh cong tra ve loi
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = false,
                        Message = updateResult.RuleViolations[0].ErrorMessage
                    };
                }
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Message = orderResult.RuleViolations[0].ErrorMessage
                };
            }
            return new JsonModel<OrderApiModel>
            {
                IsSuccessful = false,
                Message = "Có lỗi trong xử lý. Vui lòng thử lại sau"
            };
        }

        public async Task<JsonModel<OrderApiModel>> Create2()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                var model = new OrderApiModel();
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var key in provider.FormData.AllKeys)
                {
                    var strings = provider.FormData.GetValues(key);

                    if (strings != null)
                        foreach (var val in strings)
                        {
                            if (key.ToLower() == "userid")
                            {
                                model.UserId = int.Parse(val);
                            }
                            if (key.ToLower() == "customerid")
                            {
                                model.CustomerId = int.Parse(val);
                            }
                            if (key.ToLower() == "longitude")
                            {
                                model.Longitude = val;
                            }
                            if (key.ToLower() == "latitude")
                            {
                                model.Latitude = val;
                            }
                            if (key.ToLower() == "orderitems")
                            {

                            }
                        }
                }
                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    model.Files2.Add(file);
                }

                var order = new Order
                {
                    CustomerId = model.CustomerId,
                    UserId = model.UserId,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    Datetime = DateTime.Now,
                    Disable = 0
                };
                if (model.OrderItems == null || model.OrderItems.Count <= 0)
                {
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = false,
                        Message = "Vui lòng thêm sản phẩm vào đơn hàng."
                    };
                }
                var orderItems = model.OrderItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList();
                order.OrderItems = orderItems;


                var orderResult = _orderServices.Create(order);
                if (orderResult.RuleViolations.IsNullOrEmpty())
                {
                    order.Images = SaveImages2(orderResult.Result.Id, model.Files2);
                    var updateResult = _orderServices.Update(order);
                    //kiem tra ket qua update
                    if (updateResult.RuleViolations.IsNullOrEmpty())
                    {
                        //neu update thanh cong thi tra ve user da duoc cap nhat password
                        return new JsonModel<OrderApiModel>
                        {
                            IsSuccessful = true,
                            Data = null,
                            Message = "Tạo đơn hàng thành công"
                        };
                    }
                    //update khong thanh cong tra ve loi
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = false,
                        Message = updateResult.RuleViolations[0].ErrorMessage
                    };
                }
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Message = orderResult.RuleViolations[0].ErrorMessage
                };

            }
            catch (Exception e)
            {
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Message = "Có lỗi trong xử lý. Vui lòng thử lại sau"
                };
            }

        }

        [System.Web.Http.HttpPost]
        public JsonModel<OrderApiModel> UpdateOrder(OrderApiModel model)
        {
            //kiem tra user da ton tai chua
            var orderResult = _orderServices.GetById(model.Id);
            // kiem tra viec lay user
            if (orderResult.RuleViolations.IsNullOrEmpty())
            {
                //neu co thi set password moi
                var order = orderResult.Result;
                order.CustomerId = model.CustomerId;
                order.Datetime = DateTime.Now;
                var updateResult = _orderServices.Update(order);
                //kiem tra ket qua update
                if (updateResult.RuleViolations.IsNullOrEmpty())
                {
                    //neu update thanh cong thi tra ve user da duoc cap nhat password
                    return new JsonModel<OrderApiModel>
                    {
                        IsSuccessful = true,
                        Data = new OrderApiModel
                        {
                            Id = orderResult.Result.Id,
                            CustomerId = orderResult.Result.CustomerId,
                            UserId = orderResult.Result.UserId,
                            Longitude = orderResult.Result.Longitude,
                            Latitude = orderResult.Result.Latitude,
                            Datetime = orderResult.Result.Datetime,
                            Disable = orderResult.Result.Disable
                        }
                    };
                }
                //update khong thanh cong tra ve loi
                return new JsonModel<OrderApiModel>
                {
                    IsSuccessful = false,
                    Message = updateResult.RuleViolations[0].ErrorMessage
                };
            }
            //tra ve loi khi khong lay duoc user
            return new JsonModel<OrderApiModel>
            {
                IsSuccessful = false,
                Message = orderResult.RuleViolations[0].ErrorMessage
            };
        }

        private string SaveImages(int orderId, ICollection<HttpPostedFileBase> files)
        {
            string phyFloorFolder = HttpContext.Current.Server.MapPath("/Images/OrderImages");
            Folder.CreateFolder("", phyFloorFolder);
            var imgPath = "";
            foreach (var file in files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var guid = Guid.NewGuid();
                    Folder.CreateFolder(orderId.ToString(), phyFloorFolder);
                    var phyPathImage = Path.Combine(phyFloorFolder, orderId.ToString(), guid + Path.GetExtension(file.FileName));
                    var pathImage = string.Format("{0}/{1}/{2}{3}", "/Images/OrderImages", orderId, guid,
                        Path.GetExtension(file.FileName));
                    file.SaveAs(phyPathImage);
                    imgPath += pathImage +",";
                }
            }
            return imgPath;
        }

        private string SaveImages2(int orderId, ICollection<MultipartFileData> files)
        {
            string phyFloorFolder = HttpContext.Current.Server.MapPath("/Images/OrderImages");
            Folder.CreateFolder("", phyFloorFolder);
            var imgPath = "";


            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file.LocalFileName);
                if (fileInfo.Exists)
                {
                    var guid = Guid.NewGuid();
                    var fileName = file.Headers.ContentDisposition.FileName;
                    Folder.CreateFolder(orderId.ToString(), phyFloorFolder);

                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    var pathImage = string.Format("{0}/{1}/{2}{3}", "/Images/OrderImages", orderId, guid, fileName);
                    fileInfo.MoveTo(pathImage);
                    imgPath += pathImage + ",";
                }
            }
            return imgPath;

        }
    }
}