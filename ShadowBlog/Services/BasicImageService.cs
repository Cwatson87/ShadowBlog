using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ShadowBlog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ShadowBlog.Services
{
    public class BasicImageService : IImageService
    {
        private readonly IConfiguration _configuration;

        public BasicImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PleasecallMe(string number)
        {

        }


        public async Task<byte[]> EncodeImageAsync(IFormFile file)
        {
            if (file is null)
            {
                return null;
            }

            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }

        public async Task<byte[]> EncodeImageAsync(string fileName)
        {
            var file = $"{Directory.GetCurrentDirectory()}/wwwroot/img/{fileName}";
            return await File.ReadAllBytesAsync(file);
        }

        public string ContentType(IFormFile file)
        {
            return file?.ContentType;
        }

        public string DecodeImage(byte[] data, string type)
        {
            if (data is null || type is null) return null;
            return $"data:{type};base64,{Convert.ToBase64String(data)}";
        }

        private bool ValidType(IFormFile file)
        {
            //TODO: Move the acceptable image list out to the appSettings.json file and then use IConfiguration to grab the list
            var acceptableTypes = new List<string>();
            acceptableTypes.Add("jpg");
            acceptableTypes.Add("jpeg");
            acceptableTypes.Add("gif");
            acceptableTypes.Add("bmp");
            acceptableTypes.Add("png");

             var fileContentType = ContentType(file).Split("/")[1];
              var position = acceptableTypes.IndexOf(fileContentType);

            return position > 0;
        }

        private bool ValidSize(IFormFile file)
        {
            const int maxFileSize = 2 * 1024 * 1024;
            return Size(file) < maxFileSize;
        }

        public bool ValidImage(IFormFile file)
        {
            return ValidType(file) && ValidSize(file);
        }

        private int Size(IFormFile file)
        {
            return Convert.ToInt32(file?.Length);
        }
    }
}