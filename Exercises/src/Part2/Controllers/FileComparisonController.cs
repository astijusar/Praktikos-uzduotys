using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2.Filters.ActionFilters;
using Part2.Models;
using Part2.Services;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileComparisonController : ControllerBase
    {
        private readonly IFileReader _fileReader;

        public FileComparisonController(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateFilesAttribute))]
        public IActionResult CompareFiles(IFormFile sourceFile, IFormFile targetFile)
        {
            if (sourceFile == null)
            {
                ModelState.AddModelError("sourceFile", "Source file should be not null");
            }

            if (targetFile == null)
            {
                ModelState.AddModelError("targetFile", "Target file should be not null");
            }

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var sourceFileData = _fileReader.ReadFile(sourceFile);
            var targetFileData = _fileReader.ReadFile(targetFile);

            return Ok();
        }
    }
}
