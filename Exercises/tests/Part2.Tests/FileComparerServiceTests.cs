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
    public class FileComparerServiceTests
    {
        private readonly IFileComparerService _fileComparer;

        public FileComparerServiceTests()
        {
            _fileComparer = new FileComparerService();
        }

        [Fact]
        public async void CompareFiles_ShouldReturnUnchangedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                    new KeyValuePair<string, string>("id3", "Value3")
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            var targetFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                    new KeyValuePair<string, string>("id3", "Value3")
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            // Act
            var textResult = await _fileComparer.CompareFiles(sourceFile.TextIdValuePairs, targetFile.TextIdValuePairs);
            var numberResult = await _fileComparer.CompareFiles(sourceFile.NumberIdValuePairs, targetFile.NumberIdValuePairs);

            // Assert
            textResult.Should().HaveCount(3);
            numberResult.Should().HaveCount(3);

            for (var i = 0; i < textResult.Count; i++)
            {
                textResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
                numberResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }
        }

        [Fact]
        public async void CompareFiles_ShouldReturnModifiedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                    new KeyValuePair<string, string>("id3", "Value3")
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            var targetFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                    new KeyValuePair<string, string>("id3", "Value5")
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value5")
                }
            };

            var textResult = await _fileComparer.CompareFiles(sourceFile.TextIdValuePairs, targetFile.TextIdValuePairs);
            var numberResult = await _fileComparer.CompareFiles(sourceFile.NumberIdValuePairs, targetFile.NumberIdValuePairs);

            // Assert
            textResult.Should().HaveCount(3);
            numberResult.Should().HaveCount(3);

            for (var i = 0; i < textResult.Count - 1; i++)
            {
                textResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
                numberResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }

            textResult.ElementAt(textResult.Count - 1).Status.Should().Be(ResultStatusEnum.modified);
            numberResult.ElementAt(numberResult.Count - 1).Status.Should().Be(ResultStatusEnum.modified);
        }

        [Fact]
        public async void CompareFiles_ShouldReturnRemovedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                    new KeyValuePair<string, string>("id3", "Value3")
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value3")
                }
            };

            var targetFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2")
                }
            };

            // Act
            var textResult = await _fileComparer.CompareFiles(sourceFile.TextIdValuePairs, targetFile.TextIdValuePairs);
            var numberResult = await _fileComparer.CompareFiles(sourceFile.NumberIdValuePairs, targetFile.NumberIdValuePairs);

            // Assert
            textResult.Should().HaveCount(3);
            numberResult.Should().HaveCount(3);

            for (var i = 0; i < textResult.Count - 1; i++)
            {
                textResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
                numberResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }

            textResult.ElementAt(textResult.Count - 1).Status.Should().Be(ResultStatusEnum.removed);
            numberResult.ElementAt(numberResult.Count - 1).Status.Should().Be(ResultStatusEnum.removed);
        }

        [Fact]
        public async void CompareFiles_ShouldReturnAddedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2")
                }
            };

            var targetFile = new FileModel
            {
                TextIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("id1", "Value1"),
                    new KeyValuePair<string, string>("id2", "Value2"),
                    new KeyValuePair<string, string>("id3", "Value3"),
                },
                NumberIdValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1", "Value1"),
                    new KeyValuePair<string, string>("2", "Value2"),
                    new KeyValuePair<string, string>("3", "Value5")
                }
            };

            // Act
            var textResult = await _fileComparer.CompareFiles(sourceFile.TextIdValuePairs, targetFile.TextIdValuePairs);
            var numberResult = await _fileComparer.CompareFiles(sourceFile.NumberIdValuePairs, targetFile.NumberIdValuePairs);

            // Assert
            textResult.Should().HaveCount(3);
            numberResult.Should().HaveCount(3);

            for (var i = 0; i < textResult.Count - 1;  i++)
            {
                textResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
                numberResult.ElementAt(i).Status.Should().Be(ResultStatusEnum.unchanged);
            }

            textResult.ElementAt(textResult.Count - 1).Status.Should().Be(ResultStatusEnum.added);
            numberResult.ElementAt(numberResult.Count - 1).Status.Should().Be(ResultStatusEnum.added);
        }
    }
}
