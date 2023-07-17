using Microsoft.AspNetCore.Http;

namespace Part2.Services
{
    public interface IFileValidator
    {
        public bool IsValidFileSize(IFormFile file);
        public bool IsValidFileExtension(IFormFile file);
    }
}
