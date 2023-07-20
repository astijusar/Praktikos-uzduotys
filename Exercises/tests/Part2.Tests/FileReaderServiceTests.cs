using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Part2.Services;
using Part2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using Xunit;

namespace Part2.Tests
{
    public class FileReaderServiceTests
    {
        private readonly IFileReaderService _fileReader;

        public FileReaderServiceTests()
        {
            _fileReader = new FileReaderService();
        }

        [Fact]
        public async void ReadFile_ValidGzipFile_ReturnsFileModelWithDecompressedContent()
        {
            // Arrange
            var fileName = "test.txt";
            var gzipBytes = CompressStringToGzipBytes("id1:value1;id2:value2;001:1;002:0;");
            var formFile = CreateFormFile(gzipBytes, fileName);

            // Act
            var result = await _fileReader.ReadFile(formFile);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(fileName);

            result.TextIdValuePairs.Should().HaveCount(2);
            result.TextIdValuePairs.Should().Contain(new KeyValuePair<string, string>("id1", "value1"));
            result.TextIdValuePairs.Should().Contain(new KeyValuePair<string, string>("id2", "value2"));

            result.NumberIdValuePairs.Should().HaveCount(2);
            result.NumberIdValuePairs.Should().Contain(new KeyValuePair<string, string>("001", "1"));
            result.NumberIdValuePairs.Should().Contain(new KeyValuePair<string, string>("002", "0"));
        }

        [Fact]
        public async void ReadFile_ValidFile_ReturnsFileModel()
        {
            // Arrange
            var fileName = "test.cfg";
            var fileContent = "id1:value1;id2:value2;001:1;002:0;";
            var fileBytes = System.Text.Encoding.UTF8.GetBytes(fileContent);

            var formFile = CreateFormFile(fileBytes, fileName);

            // Act
            var result = await _fileReader.ReadFile(formFile);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(fileName);

            result.TextIdValuePairs.Should().HaveCount(2);
            result.TextIdValuePairs.Should().Contain(new KeyValuePair<string, string>("id1", "value1"));
            result.TextIdValuePairs.Should().Contain(new KeyValuePair<string, string>("id2", "value2"));

            result.NumberIdValuePairs.Should().HaveCount(2);
            result.NumberIdValuePairs.Should().Contain(new KeyValuePair<string, string>("001", "1"));
            result.NumberIdValuePairs.Should().Contain(new KeyValuePair<string, string>("002", "0"));
        }

        [Fact]
        public void ReadFile_NullFile_ThrowsArgumentException()
        {
            // Arrange
            IFormFile file = null;

            // Act
            var action = new Func<Task>(async () => await _fileReader.ReadFile(file));

            // Assert
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Invalid file.");
        }

        [Fact]
        public void ReadFile_EmptyFile_ThrowsArgumentException()
        {
            // Arrange
            var formFile = new FormFile(Stream.Null, 0, 0, "test.txt", "test.txt");

            // Act
            var action = new Func<Task>(async () => await _fileReader.ReadFile(formFile));

            // Act and Assert
            action.Should().ThrowAsync<ArgumentException>();
        }

        private byte[] CompressStringToGzipBytes(string content)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Compress))
                using (var writer = new StreamWriter(gzipStream))
                {
                    writer.Write(content);
                }

                return memoryStream.ToArray();
            }
        }

        private IFormFile CreateFormFile(byte[] content, string fileName)
        {
            var stream = new MemoryStream(content);
            return new FormFile(stream, 0, content.Length, "data", fileName);
        }
    }
}
