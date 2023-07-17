using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Part2.Services.Interfaces;
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

        /// <summary>
        /// Checks if the file has a valid size
        /// </summary>
        /// <param name="file">IFormFile to check</param>
        /// <returns>A boolean value</returns>
        public bool IsValidFileSize(IFormFile file)
        {
            var fileSizeLimit = _configuration["FileUploadSettings:SizeLimit"];

            if (fileSizeLimit == null)
            {
                return true;
            }

            return file.Length < int.Parse(fileSizeLimit);
        }

        /// <summary>
        /// Checks if the file extension is valid and allowed
        /// </summary>
        /// <param name="file">IFormFile to check</param>
        /// <returns>A boolean value</returns>
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
