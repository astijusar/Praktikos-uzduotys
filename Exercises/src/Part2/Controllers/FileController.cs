using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [HttpPost("compare")]
        public IActionResult CompareFiles(IFormFile sourceFile, IFormFile targetFile)
        {
            return Ok();
        }
    }
}
