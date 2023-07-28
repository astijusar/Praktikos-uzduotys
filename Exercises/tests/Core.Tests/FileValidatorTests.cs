using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Core.Tests
{
    public class FileValidatorTests
    {
        private readonly IFileValidator _fileValidator;
        private readonly Mock<IFormFile> _fileMock;

        public FileValidatorTests()
        {
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(c => c["FileUploadSettings:SizeLimit"]).Returns("1024");
            configurationMock.Setup(c => c["FileUploadSettings:PermittedExtensions"]).Returns(".cfg");

            _fileMock = new Mock<IFormFile>();

            _fileValidator = new FileValidator(configurationMock.Object);
        }

        [Fact]
        public void validateIFormFile_InvalidSize_ThrowsIOException()
        {
            // Arrange
            _fileMock.Setup(f => f.Length).Returns(2000);

            // Act
            var action = new Action(() => _fileValidator.Validate(_fileMock.Object));

            // Assert
            action.Should().Throw<IOException>();
        }

        [Fact]
        public void validateIFormFile_InvalidExtension_ThrowsIOException()
        {
            // Arrange
            _fileMock.Setup(f => f.FileName).Returns("test.txt");

            // Act
            var action = new Action(() => _fileValidator.Validate(_fileMock.Object));

            // Assert
            action.Should().Throw<IOException>();
        }
    }
}
