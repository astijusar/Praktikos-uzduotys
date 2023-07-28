using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace API.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string filePath);
    }
}
