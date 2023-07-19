using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Part2.Services;
using Part2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Part2.Tests
{
    public class FileValidatorServiceTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IFormFile> _fileMock;
        private readonly IFileValidatorService _fileValidator;

        public FileValidatorServiceTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["FileUploadSettings:SizeLimit"]).Returns("1024");
            _configurationMock.Setup(c => c["FileUploadSettings:PermitedExtensions"]).Returns(".cfg");

            _fileMock = new Mock<IFormFile>();

            _fileValidator = new FileValidatorService(_configurationMock.Object);
        }

        [Fact]
        public void IsValidFileSize_ValidSize_ReturnsTrue()
        {
            // Arrange
            _fileMock.Setup(f => f.Length).Returns(500);

            // Act
            var result = _fileValidator.IsValidFileSize(_fileMock.Object);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidFileSize_InvalidSize_ReturnsFalse()
        {
            // Arrange
            _fileMock.Setup(f => f.Length).Returns(2000);

            // Act
            var result = _fileValidator.IsValidFileSize(_fileMock.Object);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidFileSize_NotConfigured_ReturnsTrue()
        {
            // Arrange
            _fileMock.Setup(f => f.Length).Returns(2000);
            _configurationMock.Setup(c => c["FileUploadSettings:SizeLimit"]).Returns((string)null);

            // Act
            var result = _fileValidator.IsValidFileSize(_fileMock.Object);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidExtension_ValidExtension_ReturnsTrue()
        {
            // Arrange
            _fileMock.Setup(f => f.FileName).Returns("test.cfg");

            // Act
            var result = _fileValidator.IsValidFileExtension(_fileMock.Object);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void IsValidExtension_InvalidExtension_ReturnsFalse()
        {
            // Arrange
            _fileMock.Setup(f => f.FileName).Returns("test.csv");

            // Act
            var result = _fileValidator.IsValidFileExtension(_fileMock.Object);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void IsValidExtension_NotConfigured_ReturnsTrue()
        {
            // Arrange
            _fileMock.Setup(f => f.FileName).Returns("test.csv");
            _configurationMock.Setup(c => c["FileUploadSettings:PermitedExtensions"]).Returns((string)null);

            // Act
            var result = _fileValidator.IsValidFileExtension(_fileMock.Object);

            // Assert
            result.Should().BeTrue();
        }
    }
}
