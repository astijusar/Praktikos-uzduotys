using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2.Filters.ActionFilters;
using Part2.Models;
using Part2.Models.DTOs;
using Part2.Models.RequestFeatures;
using Part2.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileComparisonController : ControllerBase
    {
        private readonly IFileReader _fileReader;
        private readonly IFileComparer _fileComparer;
        private readonly IMapper _mapper;

        public FileComparisonController(IFileReader fileReader, IFileComparer fileComparer, IMapper mapper)
        {
            _fileReader = fileReader;
            _fileComparer = fileComparer;
            _mapper = mapper;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateFilesAttribute))]
        public IActionResult CompareFiles(IFormFile sourceFile, IFormFile targetFile, [FromQuery] FileComparisonParameters parameters)
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

            var comparisonResults = _fileComparer.CompareFiles(sourceFileData, targetFileData);

            var filteredResults = comparisonResults;
            if (parameters.ID != null)
            {
                filteredResults = filteredResults.Where(r => r.ID.StartsWith(parameters.ID)).ToList();
            }

            if (parameters.ResultStatus != null)
            {
                filteredResults = filteredResults.Where(r => r.Status.Equals(parameters.ResultStatus)).ToList();
            }

            var result = new ComparisonResultWithMetadataDto
            {
                SourceFile = _mapper.Map<FileModelDto>(sourceFileData),
                TargetFile = _mapper.Map<FileModelDto>(targetFileData),
                ComparisonResult = _mapper.Map<List<ComparisonResultDto>>(filteredResults)
            };

            return Ok(result);
        }
    }
}
