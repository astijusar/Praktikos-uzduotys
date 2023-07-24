using Part2.Models;
using Part2.Models.Enums;
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
        /// <param name="status">Result status enum to filter by</param>
        /// <param name="id">A value that filters ids that start with it</param>
        /// <returns>A filtered comparison result list</returns>
        public List<ComparisonResult> FilterComparisonResults(List<ComparisonResult> results, ResultStatusEnum? status, string id)
        {
            var filteredResults = results;

            if (id != null)
            {
                filteredResults = filteredResults.Where(r => r.ID.StartsWith(id.ToLower())).ToList();
            }

            if (status != null)
            {
                filteredResults = filteredResults.Where(r => r.Status.Equals(status)).ToList();
            }

            return filteredResults;
        }
    }
}
