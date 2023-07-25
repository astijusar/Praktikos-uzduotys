using Part2.Models;
using Part2.Models.Enums;
using Part2.Models.RequestFeatures;
using Part2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Part2.Services
{
    public class ResultFilterService : IResultFilterService
    {
        /// <summary>
        /// Filters comparison results based on the given status and id
        /// </summary>
        /// <param name="results">Comparison results to be filtered</param>
        /// <param name="parameters">Parameters to filter the result by</param>
        /// <returns>A filtered comparison result list</returns>
        public List<ComparisonResult> FilterComparisonResults(List<ComparisonResult> results,
            FileComparisonParameters parameters)
        {
            var filteredResults = results;

            if (parameters.ID != null)
            {
                filteredResults = filteredResults.Where(r => r.ID.StartsWith(parameters.ID.ToLower())).ToList();
            }

            if (parameters.ResultStatus != null)
            {
                var statusList = parameters.ResultStatus.Split(',').ToList();
                statusList = statusList.Select(s => s.Trim()).ToList();

                filteredResults = filteredResults.Where(r => statusList.Contains(r.Status.ToString())).ToList();
            }

            return filteredResults;
        }
    }
}
