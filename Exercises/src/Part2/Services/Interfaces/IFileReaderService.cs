using Microsoft.AspNetCore.Http;
using Part2.Models;
using System.Threading.Tasks;

namespace Part2.Services.Interfaces
{
    public interface IFileReaderService
    {
        public Task<FileModel> ReadFile(IFormFile file);
    }
}
