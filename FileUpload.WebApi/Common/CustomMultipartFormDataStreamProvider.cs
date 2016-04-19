using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FileUpload.WebApi.Common
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string rootPath)
            : base(rootPath)
        {
        }

        public CustomMultipartFormDataStreamProvider(string rootPath, int bufferSize)
            : base(rootPath, bufferSize)
        {
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            var name = Guid.NewGuid().ToString();

            if (!string.IsNullOrWhiteSpace(headers.ContentDisposition.FileName))
            {
                name += Path.GetExtension(headers.ContentDisposition.FileName.Trim('"'));
            }
            return name;
        }
    }
}