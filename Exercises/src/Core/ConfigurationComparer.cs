using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Interfaces;
using Core.Models;

namespace Core
{
    public class ConfigurationComparer : IConfigurationComparer
    {
        /// <summary>
        /// Compares two configuration file data
        /// </summary>
        /// <param name="source">The source file to compare</param>
        /// <param name="target">The target file to compare against</param>
        /// <returns>Returns the comparison result</returns>
        public ComparisonResult Compare(FileModel source, FileModel target)
        {
            var result = new ComparisonResult();

            foreach (string id in source.IdValuePairs.Keys)
            {
                if (target.IdValuePairs.ContainsKey(id))
                {
                    var sourceValue = source.IdValuePairs[id].ToString();
                    var targetValue = target.IdValuePairs[id].ToString();

                    if (sourceValue.Equals(targetValue))
                    {
                        result.ResultEntries.Add(CreateComparisonResultEntry(id, sourceValue, targetValue, ResultStatus.Unchanged));
                        result.Unchanged++;
                    }
                    else
                    {
                        result.ResultEntries.Add(CreateComparisonResultEntry(id, sourceValue, targetValue, ResultStatus.Modified));
                        result.Modified++;
                    }
                }
                else
                {
                    var sourceValue = source.IdValuePairs[id].ToString();

                    result.ResultEntries.Add(CreateComparisonResultEntry(id, sourceValue, "", ResultStatus.Removed));
                    result.Removed++;
                }
            }

            foreach (string id in target.IdValuePairs.Keys)
            {
                if (source.IdValuePairs.ContainsKey(id)) continue;

                var targetValue = target.IdValuePairs[id].ToString();

                result.ResultEntries.Add(CreateComparisonResultEntry(id, "", targetValue, ResultStatus.Added));
                result.Added++;
            }

            return result;
        }

        private ComparisonResultEntry CreateComparisonResultEntry(string id, string sourceValue, string targetValue, ResultStatus status)
        {
            return new ComparisonResultEntry
            {
                Id = id,
                SourceValue = sourceValue,
                TargetValue = targetValue,
                Status = status
            };
        }
    }
}
