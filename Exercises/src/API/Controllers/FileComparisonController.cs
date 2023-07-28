using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;
using API.ActionFilters;
using API.Services;
using Core.Interfaces;
using Core.Models;
using Core.Models.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileComparisonController : ControllerBase
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IConfigurationReader _configurationReader;
        private readonly IConfigurationComparer _configurationComparer;
        private readonly IResultFilter _resultFilter;
        private readonly IMapper _mapper;

        public FileComparisonController(IFileStorageService storageService, IConfigurationReader reader,
            IConfigurationComparer comparer, IResultFilter filter, IMapper mapper)
        {
            _fileStorageService = storageService;
            _configurationComparer = comparer;
            _configurationReader = reader;
            _resultFilter = filter;
            _mapper = mapper;
        }

        /// <summary>
        /// Compares ID and value pairs of two files
        /// </summary>
        /// <param name="sourceFile">The source file to compare</param>
        /// <param name="targetFile">The target file to compare against</param>
        /// <param name="parameters">Additional parameters for result filtering separated by a comma</param>
        /// <returns>Returns the comparison result</returns>
        /// <response code="200">Returns the results of comparison</response>
        /// <response code="422">Returns a model state error</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidateFilesAttribute))]
        public async Task<IActionResult> CompareFiles([Required]IFormFile sourceFile, [Required]IFormFile targetFile,
            [FromQuery]ResultFilterParameters parameters)
        {
            var sourceFilePath = await _fileStorageService.SaveFileAsync(sourceFile);
            var targetFilePath = await _fileStorageService.SaveFileAsync(targetFile);

            var sourceFileData = _configurationReader.ReadFromFile(sourceFilePath);
            sourceFileData.Name = sourceFile.FileName;

            var targetFileData = _configurationReader.ReadFromFile(targetFilePath);
            targetFileData.Name = targetFile.FileName;

            var comparisonResults = _configurationComparer.Compare(sourceFileData, targetFileData).ResultEntries;

            var filteredResults = _resultFilter.Filter(comparisonResults, parameters);

            _fileStorageService.DeleteFile(sourceFilePath);
            _fileStorageService.DeleteFile(targetFilePath);

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
