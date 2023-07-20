using Part2.Models;
using Part2.Models.Enums;
using System.Collections.Generic;

namespace Part2.Services.Interfaces
{
    public interface IResultFilterService
    {
        public List<ComparisonResult> filterComparisonResults(List<ComparisonResult> results, ResultStatusEnum? status, string id);
    }
}
