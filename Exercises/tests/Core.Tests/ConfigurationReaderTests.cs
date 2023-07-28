using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using FluentAssertions;
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
            var testFile = "FMB920-default.cfg";

            // Act
            var result = _configurationReader.ReadFromFile(testFile);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(testFile);
        }

        [Fact]
        public void ReadFile_NoFileFound_ThrowsFileNotFoundException()
        {
            // Arrange
            var testFile = "test.cfg";

            // Act
            var action = new Action(() => _configurationReader.ReadFromFile(testFile));

            // Assert
            action.Should().Throw<FileNotFoundException>();
        }

        [Fact]
        public void ReadFile_NoDirectoryFound_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            var testFile = "randomDir/test.cfg";

            // Act
            var action = new Action(() => _configurationReader.ReadFromFile(testFile));

            // Assert
            action.Should().Throw<DirectoryNotFoundException>();
        }
    }
}
