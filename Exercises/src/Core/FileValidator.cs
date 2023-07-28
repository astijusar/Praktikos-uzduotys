using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Core
{
    public class FileValidator : IFileValidator
    {
        private readonly IConfiguration _configuration;

        public FileValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Validate(string filePath)
        {
            var fileSizeLimit = _configuration["FileUploadSettings:SizeLimit"];
            var permittedExtensions = _configuration["FileUploadSettings:PermittedExtensions"]?.Split(",");

            var fileInfo = new FileInfo(filePath);
            var fileExtension = fileInfo.Extension;

            if (fileInfo.Length > int.Parse(fileSizeLimit))
            {
                throw new IOException($"File size exceeds the allowed limit. File path: {filePath}");
            }

            if (permittedExtensions != null && !permittedExtensions.Contains(fileExtension))
            {
                throw new IOException($"File has invalid extension. File path: {filePath}");
            }
        }
    }
}
