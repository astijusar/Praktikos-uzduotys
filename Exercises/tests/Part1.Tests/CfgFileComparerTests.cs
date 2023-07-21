using FluentAssertions;
using Part1.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Part1.Tests
{
    public class CfgFileComparerTests
    {
        private readonly IFileComparer _cfgFileComparer;

        public CfgFileComparerTests()
        {
            _cfgFileComparer = new CfgFileComparer();
        }

        [Fact]
        public void CompareFiles_ShouldReturnUnchangedPairs()
        {
            // Arrange
            var sourceFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            var targetFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            // Act
            var result = _cfgFileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);

            foreach (var res in result.ResultEntries)
            {
                res.Status.Should().Be(ResultStatusEnum.unchanged);
            }

            result.Unchanged.Should().Be(3);
            result.Modified.Should().Be(0);
            result.Removed.Should().Be(0);
            result.Added.Should().Be(0);
        }

        [Fact]
        public void CompareFiles_ShouldReturnModifiedPairs()
        {
            // Arrange
            var sourceFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            var targetFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value5" }
                }
            };

            // Act
            var result = _cfgFileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);
            result.ResultEntries.Should().ContainSingle(r => r.Status.Equals(ResultStatusEnum.modified));

            result.Unchanged.Should().Be(2);
            result.Modified.Should().Be(1);
            result.Removed.Should().Be(0);
            result.Added.Should().Be(0);
        }

        [Fact]
        public void CompareFiles_ShouldReturnRemovedPairs()
        {
            // Setup
            var sourceFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            var targetFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" }
                }
            };

            // Act
            var result = _cfgFileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);
            result.ResultEntries.Should().ContainSingle(r => r.Status.Equals(ResultStatusEnum.removed));

            result.Unchanged.Should().Be(2);
            result.Modified.Should().Be(0);
            result.Removed.Should().Be(1);
            result.Added.Should().Be(0);
        }

        [Fact]
        public void CompareFiles_ShouldReturnAddedPairs()
        {
            // Setup
            var sourceFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                }
            };

            var targetFile = new CfgFile
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            // Act
            var result = _cfgFileComparer.CompareFiles(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);
            result.ResultEntries.Should().ContainSingle(r => r.Status.Equals(ResultStatusEnum.added));

            result.Unchanged.Should().Be(2);
            result.Modified.Should().Be(0);
            result.Removed.Should().Be(0);
            result.Added.Should().Be(1);
        }
    }
}
