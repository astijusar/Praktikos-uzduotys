using Microsoft.AspNetCore.Http;

namespace Part2.Services
{
    public interface IFileValidationService
    {
        public bool IsValidFileSize(IFormFile file);
        public bool IsValidFileExtension(IFormFile file);
    }
}
