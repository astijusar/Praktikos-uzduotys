using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part2.Filters.ActionFilters;
using Part2.Models;
using Part2.Models.DTOs;
using Part2.Models.RequestFeatures;
using Part2.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileComparisonController : ControllerBase
    {
        private readonly IFileReaderService _fileReader;
        private readonly IFileComparerService _fileComparer;
        private readonly IMapper _mapper;

        public FileComparisonController(IFileReaderService fileReader, IFileComparerService fileComparer, IMapper mapper)
        {
            _fileReader = fileReader;
            _fileComparer = fileComparer;
            _mapper = mapper;
        }

        /// <summary>
        /// Compares ID and value pairs of two files
        /// </summary>
        /// <param name="sourceFile">The source file to compare</param>
        /// <param name="targetFile">The target file to compare against</param>
        /// <param name="parameters">Additional parameters for result filtering</param>
        /// <returns>Returns the comparison result</returns>
        /// <response code="200">Returns the results of comparison</response>
        /// <response code="422">Returns a model state error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidateFilesAttribute))]
        public async Task<IActionResult> CompareFiles([Required]IFormFile sourceFile, [Required]IFormFile targetFile,
            [FromQuery]FileComparisonParameters parameters)
        {
            var sourceFileData = await _fileReader.ReadFile(sourceFile);
            var targetFileData = await _fileReader.ReadFile(targetFile);

            var comparisonResults = await _fileComparer.CompareFiles(sourceFileData, targetFileData);

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
