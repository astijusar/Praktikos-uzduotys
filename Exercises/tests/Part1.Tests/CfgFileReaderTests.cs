using FluentAssertions;
using Moq;
using Part1.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Part1.Tests
{
    public class CfgFileReaderTests
    {
        private readonly IFileReader _cfgFileReader;

        public CfgFileReaderTests()
        {
            _cfgFileReader = new CfgFileReader();
        }

        [Fact]
        public void ReadFile_ValidFile_ReturnsFileModel()
        {
            // Arrange
            var testFile = "FMB920-default.cfg";

            // Act
            var result = _cfgFileReader.ReadFile(testFile);

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
            var action = new Action(() => _cfgFileReader.ReadFile(testFile));

            // Assert
            action.Should().Throw<FileNotFoundException>();
        }

        [Fact]
        public void ReadFile_NoDirectoryFound_ThrowsDirectoryNotFoundException()
        {
            // Arrange
            var testFile = "randomDir/test.cfg";

            // Act
            var action = new Action(() => _cfgFileReader.ReadFile(testFile));

            // Assert
            action.Should().Throw<DirectoryNotFoundException>();
        }
    }
}
