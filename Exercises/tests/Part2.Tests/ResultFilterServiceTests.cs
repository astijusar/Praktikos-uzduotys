﻿using Part1;
using Part2.Services.Interfaces;
using Part2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Part2.Tests
{
    public class ResultFilterServiceTests
    {
        private readonly IResultFilterService _resultFilterService;

        public ResultFilterServiceTests()
        {
            _resultFilterService = new ResultFilterService();
        }

        [Fact]
        public void FilterResults_WhenNoFiltersApplied_ReturnsAllResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new ComparisonResultEntry
                {
                    ID = "1",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatusEnum.unchanged
                },
                new ComparisonResultEntry
                {
                    ID = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatusEnum.modified
                }
            };

            var filteredResults = _resultFilterService.FilterComparisonResults(results, null, null);

            filteredResults.Should().HaveCount(2);
        }

        [Fact]
        public void FilterResults_WhenStatusIsSpecified_ReturnsFilteredByStatusResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new ComparisonResultEntry
                {
                    ID = "1",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatusEnum.unchanged
                },
                new ComparisonResultEntry
                {
                    ID = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatusEnum.modified
                }
            };

            var status = ResultStatusEnum.modified;

            var filteredResults = _resultFilterService.FilterComparisonResults(results, status, null);

            filteredResults.Should().HaveCount(1);
            filteredResults.ElementAt(0).ID.Should().Be("2");
        }

        [Fact]
        public void FilterResults_WhenIdIsSpecified_ReturnsFilteredByIdResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new ComparisonResultEntry
                {
                    ID = "1",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatusEnum.unchanged
                },
                new ComparisonResultEntry
                {
                    ID = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatusEnum.modified
                },
                new ComparisonResultEntry
                {
                    ID = "23",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatusEnum.added
                }
            };

            var id = "2";

            var filteredResults = _resultFilterService.FilterComparisonResults(results, null, id);

            filteredResults.Should().HaveCount(2);
            filteredResults.ElementAt(0).ID.Should().Be("2");
            filteredResults.ElementAt(1).ID.Should().Be("23");
        }

        [Fact]
        public void FilterResults_WhenBothSpecified_ReturnsFilteredByBothResults()
        {
            var results = new List<ComparisonResultEntry>
            {
                new ComparisonResultEntry
                {
                    ID = "24",
                    SourceValue = "1",
                    TargetValue = "1",
                    Status = ResultStatusEnum.unchanged
                },
                new ComparisonResultEntry
                {
                    ID = "2",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatusEnum.modified
                },
                new ComparisonResultEntry
                {
                    ID = "23",
                    SourceValue = "1",
                    TargetValue = "2",
                    Status = ResultStatusEnum.added
                }
            };

            var status = ResultStatusEnum.modified;
            var id = "2";

            var filteredResults = _resultFilterService.FilterComparisonResults(results, status, id);

            filteredResults.Should().HaveCount(1);
            filteredResults.ElementAt(0).ID.Should().Be("2");
        }
    }
}
