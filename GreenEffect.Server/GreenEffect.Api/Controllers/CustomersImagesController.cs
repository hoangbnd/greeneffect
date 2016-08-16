using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GreenEffect.Api.Models;
using GreenEffect.DomainObject;
using GreenEffect.Services;
using MVCCore;
using GreenEffect.DomainObject.CustomersImages;
using GreenEffect.Services.Interface;
namespace GreenEffect.Api.Controllers
{
    public class CustomersImagesController:ApiController
    {
        private const string ImagesFolder = "~/Images";
        private readonly ICustomersImagesServices _customerImagesService;

        public CustomersImagesController(ICustomersImagesServices customerImagesService)
        {
            _customerImagesService = customerImagesService;
        }
        [HttpPost]
        public async Task<HttpResponseMessage> PostImages(CustomersImagesApiModel model)
        {
            var diskFolder = ConfigurationManager.AppSettings["AssetsMapPath"];
            var avatarUrl = "";
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var customersImagesResult = new ServiceResult<CustomersImages>();

                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    var fileInfo = new FileInfo(file.LocalFileName);
                    if (fileInfo.Exists)
                    {
                        var guid = Guid.NewGuid();
                        var fileName = file.Headers.ContentDisposition.FileName;
                        if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                        {
                            fileName = fileName.Trim('"');
                        }
                        if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                        {
                            fileName = Path.GetFileName(fileName);
                        }
                        var imageType = Path.GetExtension(fileName);
                        var imageThumbUrl = guid.ToString().Substring(0, 1) + "/" + guid.ToString().Substring(0, 2) + "/" + guid + imageType;
                        var imageThumbUrlAb = diskFolder + "/" + ImagesFolder + "/" + guid.ToString().Substring(0, 1) + "/" + guid.ToString().Substring(0, 2) + "/" + guid + imageType;
                        Folder.CreateFolder(guid.ToString().Substring(0, 1) + "/" + guid.ToString().Substring(0, 2) + "/",
                                            diskFolder + "/" + ImagesFolder);
                        fileInfo.MoveTo(imageThumbUrlAb);
                        avatarUrl = imageThumbUrl;
                    }
                }
                if (customersImagesResult != null && customersImagesResult.RuleViolations.IsNullOrEmpty())
                {
                    var images = customersImagesResult.Result;
                        images.Images = avatarUrl;             
                        images.CustomersID = model.CustomersID;
                        images.CustomersImagesID = model.CustomersImagesID;
                        images.UserID = model.UserID;
                        images.DateTime = DateTime.Now;
                    //update User
                    _customerImagesService.Create(images);
                }


                return new HttpResponseMessage
                {
                    Content = new StringContent("Upload successful")
                };
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}