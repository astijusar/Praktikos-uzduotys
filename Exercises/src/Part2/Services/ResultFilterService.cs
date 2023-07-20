using Part2.Models;
using Part2.Models.Enums;
using Part2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Part2.Services
{
    public class ResultFilterService : IResultFilterService
    {
        public List<ComparisonResult> filterComparisonResults(List<ComparisonResult> results, ResultStatusEnum? status, string id)
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
