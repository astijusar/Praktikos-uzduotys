﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Core
{
    public class ResultFilter : IResultFilter
    {
        /// <summary>
        /// Filters comparison results by given filters
        /// </summary>
        /// <param name="results">A list of results</param>
        /// <param name="parameters">Parameters to filter by</param>
        /// <returns>A filtered list of comparison results</returns>
        public List<ComparisonResultEntry> Filter(List<ComparisonResultEntry> results, ResultFilterParameters parameters)
        {
            var filteredResults = results;

            if (parameters.Id != null)
            {
                filteredResults = filteredResults.Where(r => r.Id.StartsWith(parameters.Id.ToLower())).ToList();
            }

            if (parameters.ResultStatus != null)
            {
                var statusList = parameters.ResultStatus.Split(',').ToList();
                statusList = statusList.Select(s => s.Trim()).ToList();

                filteredResults = filteredResults.Where(r => statusList.Contains(r.Status.ToString().ToLower())).ToList();
            }

            return filteredResults;
        }
    }
}
