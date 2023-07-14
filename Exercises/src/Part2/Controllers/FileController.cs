using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2.Filters.ActionFilters;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("cfg/compare")]
        [ServiceFilter(typeof(ValidateFilesAttribute))]
        public IActionResult CompareFiles(IFormFile sourceFile, IFormFile targetFile)
        {
            return Ok();
        }
    }
}
