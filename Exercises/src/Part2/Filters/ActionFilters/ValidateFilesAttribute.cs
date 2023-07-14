using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Part2.Services;
using System.Collections.Generic;
using System.Linq;

namespace Part2.Filters.ActionFilters
{
    public class ValidateFilesAttribute : IActionFilter
    {
        private readonly IFileValidationService _fileValidationService;

        public ValidateFilesAttribute(IFileValidationService fileValidationService)
        {
            _fileValidationService = fileValidationService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var files = context.ActionArguments
                .Where(f => f.Value is IFormFile)
                .Select(f => new { Name = f.Key, File = (IFormFile)f.Value });

            foreach (var fileData in files)
            {
                if (!_fileValidationService.IsValidFileSize(fileData.File))
                {
                    context.ModelState.AddModelError(fileData.Name, "Invalid file size.");
                }

                if (!_fileValidationService.IsValidFileExtension(fileData.File))
                {
                    context.ModelState.AddModelError(fileData.Name, "Invalid file extension");
                }
            }

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
