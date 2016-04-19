using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FileUpload.WebApi.Common
{
    public class FileUploadHandler
    {
        public async Task<FileUploadResult> Upload(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"App_Data\upload");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var provider = new CustomMultipartFormDataStreamProvider(folder);
            await request.Content.ReadAsMultipartAsync(provider);

            return new FileUploadResult
            {
                FileData = provider.FileData,
                FormData = provider.FormData
            };
        }
    }

    public class FileUploadResult
    {
        public Collection<MultipartFileData> FileData { get; set; }

        public NameValueCollection FormData { get; set; }
    }
}