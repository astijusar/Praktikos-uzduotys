using System;
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
    public class ResultFilterTests
    {
        private readonly IResultFilter _filter;

        public ResultFilterTests()
        {
            _filter = new ResultFilter();
        }

        [Fact]
        public void FilterResults_WhenNoFiltersApplied_ReturnsAllResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new()
                {
                    Id = "1",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatus.Unchanged
                },
                new()
                {
                    Id = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatus.Modified
                }
            };

            var param = new ResultFilterParameters();

            var filteredResults = _filter.Filter(results, param);

            filteredResults.Should().HaveCount(2);
        }

        [Fact]
        public void FilterResults_WhenStatusIsSpecified_ReturnsFilteredByStatusResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new()
                {
                    Id = "1",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatus.Unchanged
                },
                new()
                {
                    Id = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatus.Modified
                }
            };

            var param = new ResultFilterParameters()
            {
                ResultStatus = "modified"
            };

            var filteredResults = _filter.Filter(results, param);

            filteredResults.Should().HaveCount(1);
            filteredResults.ElementAt(0).Id.Should().Be("2");
        }

        [Fact]
        public void FilterResults_WhenIdIsSpecified_ReturnsFilteredByIdResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new()
                {
                    Id = "1",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatus.Unchanged
                },
                new()
                {
                    Id = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatus.Modified
                },
                new()
                {
                    Id = "23",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatus.Added
                }
            };

            var param = new ResultFilterParameters
            {
                Id = "2"
            };

            var filteredResults = _filter.Filter(results, param);

            filteredResults.Should().HaveCount(2);
            filteredResults.ElementAt(0).Id.Should().Be("2");
            filteredResults.ElementAt(1).Id.Should().Be("23");
        }

        [Fact]
        public void FilterResults_WhenBothSpecified_ReturnsFilteredByBothResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new()
                {
                    Id = "24",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatus.Unchanged
                },
                new()
                {
                    Id = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatus.Modified
                },
                new()
                {
                    Id = "23",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatus.Added
                }
            };

            var param = new ResultFilterParameters
            {
                Id = "2",
                ResultStatus = "modified"
            };

            var filteredResults = _filter.Filter(results, param);

            filteredResults.Should().HaveCount(1);
            filteredResults.ElementAt(0).Id.Should().Be("2");
        }
    }
}
