using FluentAssertions;
using Part2.Models;
using Part2.Models.Enums;
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
    public class FileComparerTests
    {
        private readonly IFileComparer _fileComparer;

        public FileComparerTests()
        {
            _fileComparer = new FileComparer();
        }

        [Fact]
        public void CompareFiles_ShouldReturnUnchangedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            // Act
            var result = _fileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.Should().HaveCount(3);

            foreach (var res in result)
            {
                res.Status.Should().Be(ResultStatusEnum.unchanged);
            }
        }

        [Fact]
        public void CompareFiles_ShouldReturnModifiedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value5")
                }
            };

            // Act
            var result = _fileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.Should().HaveCount(3);

            for (var i = 0; i < result.Count - 1; i++)
            {
                result.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }

            result.ElementAt(2).Status.Should().Be(ResultStatusEnum.modified);
        }

        [Fact]
        public void CompareFiles_ShouldReturnRemovedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2")
                }
            };

            // Act
            var result = _fileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.Should().HaveCount(3);

            for (var i = 0; i < result.Count - 1; i++)
            {
                result.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }

            result.ElementAt(result.Count - 1).Status.Should().Be(ResultStatusEnum.removed);
        }

        [Fact]
        public void CompareFiles_ShouldReturnAddedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2")
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value5")
                }
            };

            // Act
            var result = _fileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.Should().HaveCount(3);

            for (var i = 0; i < result.Count - 1;  i++)
            {
                result.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }

            result.ElementAt(result.Count - 1).Status.Should().Be(ResultStatusEnum.added);
        }
    }
}
