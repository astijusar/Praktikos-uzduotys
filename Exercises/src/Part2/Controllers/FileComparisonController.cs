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
        private readonly IResultFilterService _resultFilter;
        private readonly IMapper _mapper;

        public FileComparisonController(IFileReaderService fileReader, IFileComparerService fileComparer,
            IResultFilterService resultFilter, IMapper mapper)
        {
            _fileReader = fileReader;
            _fileComparer = fileComparer;
            _resultFilter = resultFilter;
            _mapper = mapper;
        }

        /// <summary>
        /// Compares ID and value pairs of two files
        /// </summary>
        /// <param name="sourceFile">The source file to compare</param>
        /// <param name="targetFile">The target file to compare against</param>
        /// <param name="param">Additional parameters for result filtering</param>
        /// <returns>Returns the comparison result</returns>
        /// <response code="200">Returns the results of comparison</response>
        /// <response code="422">Returns a model state error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidateFilesAttribute))]
        public async Task<IActionResult> CompareFiles([Required]IFormFile sourceFile, [Required]IFormFile targetFile,
            [FromQuery]ComparisonResultParameters param)
        {
            var sourceFileData = await _fileReader.ReadFile(sourceFile);
            var targetFileData = await _fileReader.ReadFile(targetFile);

            var textParamResults = await _fileComparer
                .CompareFiles(sourceFileData.TextIdValuePairs, targetFileData.TextIdValuePairs);

            var numberParamResults = await _fileComparer
                .CompareFiles(sourceFileData.NumberIdValuePairs, targetFileData.NumberIdValuePairs);

            var textFilteredParamResults = _resultFilter
                .FilterComparisonResults(textParamResults, param.TextParamResultStatus, param.TextParamID);

            var NumberFilteredParamResults = _resultFilter
                .FilterComparisonResults(numberParamResults, param.NumberParamResultStatus, param.NumberParamID);

            var result = new ComparisonResultWithMetadataDto
            {
                SourceFile = _mapper.Map<FileModelDto>(sourceFileData),
                TargetFile = _mapper.Map<FileModelDto>(targetFileData),
                TextParameterComparisonResult = _mapper.Map<List<ComparisonResultDto>>(textFilteredParamResults),
                NumberParameterComparisonResult = _mapper.Map<List<ComparisonResultDto>>(NumberFilteredParamResults)
            };

            return Ok(result);
        }
    }
}
