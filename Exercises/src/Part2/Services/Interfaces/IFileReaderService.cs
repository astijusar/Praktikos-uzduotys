using Microsoft.AspNetCore.Http;
using Part2.Models;

namespace Part2.Services.Interfaces
{
    public interface IFileReaderService
    {
        public FileModel ReadFile(IFormFile file);
    }
}
