using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Part2.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string fileName);
    }
}
