using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FileUpload.WebApi.Common;
using FileUpload.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FileUpload.WebApi.Controllers
{
    [RoutePrefix("api/files")]
    public class FilesController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {
            var fileUploadHandler = new FileUploadHandler();
            var result = await fileUploadHandler.Upload(Request);

            var filePath = GetFilePath(result);
            var form = ParseForm(result);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(string.Format("Path:{0}, Title:{1}, Content:{2}", filePath, form.Title, form.Content));
            return response;
        }

        private string GetFilePath(FileUploadResult result)
        {
            return result.FileData.Select(n => n.LocalFileName).FirstOrDefault();
        }

        private UploadForm ParseForm(FileUploadResult result)
        {
            var formJson = result.FormData.Get("form");
            var setting = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var form = JsonConvert.DeserializeObject<UploadForm>(formJson, setting);
            return form;
        }
    }
}
