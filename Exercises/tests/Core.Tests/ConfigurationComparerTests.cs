using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Interfaces;
using Core.Models;
using FluentAssertions;
using Xunit;

namespace Core.Tests
{
    public class ConfigurationComparerTests
    {
        private readonly IConfigurationComparer _configurationComparer;

        public ConfigurationComparerTests()
        {
            _configurationComparer = new ConfigurationComparer();
        }

        [Fact]
        public void CompareFiles_ShouldReturnUnchangedPairs()
        {
            // Arrange
            var sourceFile = new FileModel()
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            var targetFile = new FileModel()
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            // Act
            var result = _configurationComparer.Compare(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);

            foreach (var res in result.ResultEntries)
            {
                res.Status.Should().Be(ResultStatus.Unchanged);
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
            var sourceFile = new FileModel
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value5" }
                }
            };

            // Act
            var result = _configurationComparer.Compare(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);
            result.ResultEntries.Should().ContainSingle(r => r.Status.Equals(ResultStatus.Modified));

            result.Unchanged.Should().Be(2);
            result.Modified.Should().Be(1);
            result.Removed.Should().Be(0);
            result.Added.Should().Be(0);
        }

        [Fact]
        public void CompareFiles_ShouldReturnRemovedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" }
                }
            };

            // Act
            var result = _configurationComparer.Compare(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);
            result.ResultEntries.Should().ContainSingle(r => r.Status.Equals(ResultStatus.Removed));

            result.Unchanged.Should().Be(2);
            result.Modified.Should().Be(0);
            result.Removed.Should().Be(1);
            result.Added.Should().Be(0);
        }

        [Fact]
        public void CompareFiles_ShouldReturnAddedPairs()
        {
            // Setup
            var sourceFile = new FileModel
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                }
            };

            var targetFile = new FileModel
            {
                IdValuePairs = new Hashtable
                {
                    { "1", "Value1" },
                    { "2", "Value2" },
                    { "3", "Value3" }
                }
            };

            // Act
            var result = _configurationComparer.Compare(sourceFile, targetFile);

            // Assert
            result.ResultEntries.Should().HaveCount(3);
            result.ResultEntries.Should().ContainSingle(r => r.Status.Equals(ResultStatus.Added));

            result.Unchanged.Should().Be(2);
            result.Modified.Should().Be(0);
            result.Removed.Should().Be(0);
            result.Added.Should().Be(1);
        }
    }
}
