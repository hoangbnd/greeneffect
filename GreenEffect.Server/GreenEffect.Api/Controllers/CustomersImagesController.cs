using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GreenEffect.DomainObject.Customer;
using MVCCore;
using GreenEffect.Services.Interface;
namespace GreenEffect.Api.Controllers
{
    public class CustomersImagesController:ApiController
    {
        private const string ImagesFolder = "";
        private readonly ICustomersImagesServices _customerImagesService;

        public CustomersImagesController(ICustomersImagesServices customerImagesService)
        {
            _customerImagesService = customerImagesService;
        }
        [HttpPost]
        public async Task<HttpResponseMessage> PostImages()
        {
            var diskFolder = ConfigurationManager.AppSettings["AssetsMapPath"];
            var avatarUrl = "";
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var customerId = 0;
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                
                await Request.Content.ReadAsMultipartAsync(provider);
                foreach (var key in provider.FormData.AllKeys)
                {
                    var strings = provider.FormData.GetValues(key);
                    if (strings != null)
                        foreach (var val in strings)
                        {
                            if (key.ToLower() == "customerid")
                            {
                                customerId = Int32.Parse(val);
                            }
                            
                        }
                }

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
                var images = new CustomersImages();
                images.Images = avatarUrl;
                images.CustomersId = customerId;
                //images.CustomersImagesID = model.CustomersImagesID;
                //images.UserCode = model.UserCode;
                images.DateTime = DateTime.Now;
                var customersImagesResult = _customerImagesService.Create(images);
                if (customersImagesResult != null && customersImagesResult.RuleViolations.IsNullOrEmpty())
                {

                    return new HttpResponseMessage
                    {
                        Content = new StringContent("Upload successful")
                    };
                   
                }

                return new HttpResponseMessage
                {
                    Content = new StringContent("Upload error")
                };
                
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

    }
}