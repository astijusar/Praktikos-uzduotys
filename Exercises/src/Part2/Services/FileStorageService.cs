using Microsoft.AspNetCore.Http;
using Part2.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Part2.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileStorageService(IConfiguration configuration, IWebHostEnvironment enviroment)
        {
            _configuration = configuration;
            _hostingEnvironment = enviroment;
        }

        /// <summary>
        /// Saves file to a temp file directory asynchronously
        /// </summary>
        /// <param name="file">File to be saved</param>
        /// <returns>The path to the saved file</returns>
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var fileStoragePath = _configuration["FileUploadSettings:StoragePath"];
            var rootDir = _hostingEnvironment.ContentRootPath;

            var filePath = Path.Combine(rootDir, fileStoragePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        /// <summary>
        /// Deletes the file from a given path
        /// </summary>
        /// <param name="filePath">Path to the file to delete</param>
        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
