using Microsoft.AspNetCore.Http;
using Part2.Models;

namespace Part2.Services.Interfaces
{
    public interface IFileReader
    {
        public FileModel ReadFile(IFormFile file);
    }
}
