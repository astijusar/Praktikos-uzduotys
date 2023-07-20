using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Part1.Interfaces;
using Part2.Filters.ActionFilters;
using Part2.Models;
using Part2.Models.DTOs;
using Part2.Models.RequestFeatures;
using Part2.Services.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Part2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileComparisonController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IFileReader _cfgFileReader;
        private readonly IFileComparer _cfgFileComparer;
        private readonly IResultFilterService _resultFilterService;
        private readonly IMapper _mapper;

        public FileComparisonController(IFileStorageService storageService, IFileReader cfgFileReader,
            IFileComparer cfgFileComparer, IResultFilterService resultFilterService, IMapper mapper)
        {
            _fileStorageService = storageService;
            _cfgFileReader = cfgFileReader;
            _cfgFileComparer = cfgFileComparer;
            _resultFilterService = resultFilterService;
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
            [FromQuery]FileComparisonParameters param)
        {
            var sourceFilePath = await _fileStorageService.SaveFileAsync(sourceFile);
            var targetFilePath = await _fileStorageService.SaveFileAsync(targetFile);

            var sourceFileData = _cfgFileReader.ReadFile(sourceFilePath);
            var targetFileData = _cfgFileReader.ReadFile(targetFilePath);

            var comparisonResults = _cfgFileComparer.CompareFiles(sourceFileData, targetFileData).ResultEntries;

            var filteredComparisonResults = _resultFilterService.FilterComparisonResults(comparisonResults, param.ResultStatus, param.ID);

            var resultDto = new ComparisonResultWithMetadataDto
            {
                SourceFile = _mapper.Map<FileModelDto>(sourceFileData),
                TargetFile = _mapper.Map<FileModelDto>(targetFileData),
                ComparisonResult = _mapper.Map<List<ComparisonResultDto>>(filteredComparisonResults)
            };

            _fileStorageService.DeleteFile(sourceFilePath);
            _fileStorageService.DeleteFile(targetFilePath);

            return Ok(resultDto);
        }
    }
}
