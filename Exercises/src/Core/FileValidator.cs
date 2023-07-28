using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Validate a file when given its path
        /// </summary>
        /// <param name="filePath">File location</param>
        /// <exception cref="IOException">Thrown if file size or extension are invalid</exception>
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

        /// <summary>
        /// Validate a file when given an IFormFile object
        /// </summary>
        /// <param name="file">IForm file object</param>
        /// <exception cref="IOException">Thrown if file size or extension are invalid</exception>
        public void Validate(IFormFile file)
        {
            var fileSizeLimit = _configuration["FileUploadSettings:SizeLimit"];
            var permittedExtensions = _configuration["FileUploadSettings:PermittedExtensions"]?.Split(",");

            if (file.Length > int.Parse(fileSizeLimit))
            {
                throw new IOException("File size exceeds the allowed limit.");
            }

            var fileExtension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
            if (permittedExtensions != null && !permittedExtensions.Contains(fileExtension))
            {
                throw new IOException("File has an invalid extension.");
            }
        }
    }
}
