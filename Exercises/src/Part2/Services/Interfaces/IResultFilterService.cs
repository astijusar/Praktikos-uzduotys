using Part1;
using System.Collections.Generic;

namespace Part2.Services.Interfaces
{
    public interface IResultFilterService
    {
        public List<ComparisonResultEntry> FilterComparisonResults(List<ComparisonResultEntry> results, ResultStatusEnum? status, string id);
    }
}
