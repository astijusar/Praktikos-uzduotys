using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using FluentAssertions;
using Moq;
using Xunit;

namespace Core.Tests
{
    public class ConfigurationReaderTests
    {
        private readonly IConfigurationReader _configurationReader;

        public ConfigurationReaderTests()
        {
            _configurationReader = new ConfigurationReader();
        }

        [Fact]
        public void ReadFile_ValidFile_ReturnsFileModel()
        {
            // Arrange
            var fileName = "test1.cfg";
            var fileContent = "1:value1;2:value2;id:metadata1";
            var expectedFileModel = new FileModel
            {
                Name = fileName,
                IdValuePairs =
                {
                    { "1", "value1" },
                    { "2", "value2" }
                },
                Metadata = { "id:metadata1" }
            };

            var filePath = Path.Combine(Path.GetTempPath(), fileName);
            using (var fileStream = File.Create(filePath))
            {
                using (var gzStream = new GZipStream(fileStream, CompressionMode.Compress))
                {
                    using (var writer = new StreamWriter(gzStream))
                    {
                        writer.Write(fileContent);
                    }
                }
            }

            // Act
            var result = _configurationReader.ReadFromFile(filePath);

            // Assert
            result.Should().BeEquivalentTo(expectedFileModel);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void ReadFile_EmptyFile_ReturnsEmptyFileModel()
        {
            // Arrange
            var fileName = "test2.cfg";
            var expectedFileModel = new FileModel
            {
                Name = fileName,
                IdValuePairs = new Hashtable(),
                Metadata = new List<string>()
            };

            var filePath = Path.Combine(Path.GetTempPath(), fileName);
            using (var fileStream = File.Create(filePath)) {}
            
            // Act
            var result = _configurationReader.ReadFromFile(filePath);

            // Assert
            result.Should().BeEquivalentTo(expectedFileModel);

            // Cleanup
            File.Delete(filePath);
        }
    }
}
