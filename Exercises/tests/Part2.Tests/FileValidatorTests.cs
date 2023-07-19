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
    public class FileValidatorTests
    {
        private readonly Mock<IConfiguration> _configuration;
        private readonly IFileValidator _fileValidator;
        private readonly Mock<IFormFile> _fileMock;

        public FileValidatorTests()
        {
            _configuration = new Mock<IConfiguration>();
            _configuration.Setup(c => c["FileUploadSettings:SizeLimit"]).Returns("1024");

            var fileExtensionSectionMock = new Mock<IConfigurationSection>();
            fileExtensionSectionMock
               .Setup(x => x.Value)
               .Returns(".cfg");

            _configuration.Setup(c => c.GetSection("FileUploadSettings:PermitedExtensions")).Returns(fileExtensionSectionMock.Object);

            _fileMock = new Mock<IFormFile>();

            _fileValidator = new FileValidator(_configuration.Object);
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
    }
}
