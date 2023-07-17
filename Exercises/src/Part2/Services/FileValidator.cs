using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;

namespace Part2.Services
{
    public class FileValidator : IFileValidator
    {
        private readonly IConfiguration _configuration;

        public FileValidator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidFileSize(IFormFile file)
        {
            var fileSizeLimit = _configuration["FileUploadSettings:SizeLimit"];

            if (fileSizeLimit == null)
            {
                return true;
            }

            return file.Length < int.Parse(fileSizeLimit);
        }

        public bool IsValidFileExtension(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            var permittedExtensions = _configuration.GetSection("FileUploadSettings:PermitedExtensions").Get<string[]>();

            if (permittedExtensions == null)
            {
                return true;
            }

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }

            return true;
        }
    }
}
