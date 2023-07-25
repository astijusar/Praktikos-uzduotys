using Part2.Models;
using Part2.Models.Enums;
using Part2.Models.RequestFeatures;
using System.Collections.Generic;

namespace Part2.Services.Interfaces
{
    public interface IResultFilterService
    {
        public List<ComparisonResult> FilterComparisonResults(List<ComparisonResult> results, FileComparisonParameters parameters);
    }
}
