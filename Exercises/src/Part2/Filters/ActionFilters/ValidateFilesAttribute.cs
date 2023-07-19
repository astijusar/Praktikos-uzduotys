﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Part2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Part2.Filters.ActionFilters
{
    public class ValidateFilesAttribute : IActionFilter
    {
        private readonly IFileValidatorService _fileValidationService;
        private readonly ILogger<ValidateFilesAttribute> _logger;

        public ValidateFilesAttribute(IFileValidatorService fileValidationService, ILogger<ValidateFilesAttribute> logger)
        {
            _fileValidationService = fileValidationService;
            _logger = logger;
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
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
    }
}
