using System.IO;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace API.ActionFilters
{
    public class ValidateFilesAttribute : IActionFilter
    {
        private readonly IFileValidator _fileValidator;

        public ValidateFilesAttribute(IFileValidator validator)
        {
            _fileValidator = validator;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// Validate files before action executing
        /// </summary>
        /// <param name="context">Provides the context</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var files = context.ActionArguments
                .Where(f => f.Value is IFormFile)
                .Select(f => new { Name = f.Key, File = (IFormFile)f.Value });

            foreach (var fileData in files)
            {
                try
                {
                    _fileValidator.Validate(fileData.File);
                }
                catch (IOException ex)
                {
                    context.Result = new BadRequestObjectResult(ex.Message);
                }
            }
        }
    }
}
