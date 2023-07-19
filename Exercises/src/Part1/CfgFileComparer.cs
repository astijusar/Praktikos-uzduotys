using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class CfgFileComparer : IFileComparer
    {
        /// <summary>
        /// Compare two .cfg files
        /// </summary>
        /// <param name="source">Original file that will be used in the comparison</param>
        /// <param name="target">File that will be compared against the source file</param>
        /// <returns>Return an object with comparison results</returns>
        /// <exception cref="Exception">source and target files need to implement the ICfgFile interface</exception>
        public ComparisonResult CompareFiles(IFile source, IFile target)
        {
            ICfgFile sourceCfgFile = source as ICfgFile;
            ICfgFile targetCfgFile = target as ICfgFile;

            if (sourceCfgFile == null && targetCfgFile == null)
            {
                throw new Exception("Given objects need to implement the ICfgFile interface");
            }

            ComparisonResult result = new ComparisonResult();

            foreach (string id in sourceCfgFile.IdValuePairs.Keys)
            {
                if (targetCfgFile.IdValuePairs.ContainsKey(id))
                {
                    var sourceValue = sourceCfgFile.IdValuePairs[id].ToString();
                    var targetValue = targetCfgFile.IdValuePairs[id].ToString();

                    if (sourceValue.Equals(targetValue))
                    {
                        result.ResultEntries.Add(CreateComparisonResultEntry(id, sourceValue, targetValue, ResultStatusEnum.unchanged));
                        result.Unchanged++;
                    }
                    else
                    {
                        result.ResultEntries.Add(CreateComparisonResultEntry(id, sourceValue, targetValue, ResultStatusEnum.modified));
                        result.Modified++;
                    }
                }
                else
                {
                    var sourceValue = sourceCfgFile.IdValuePairs[id].ToString();

                    result.ResultEntries.Add(CreateComparisonResultEntry(id, sourceValue, "", ResultStatusEnum.removed));
                    result.Removed++;
                }
            }

            foreach (string id in targetCfgFile.IdValuePairs.Keys)
            {
                if (!sourceCfgFile.IdValuePairs.ContainsKey(id))
                {
                    var targetValue = targetCfgFile.IdValuePairs[id].ToString();

                    result.ResultEntries.Add(CreateComparisonResultEntry(id, "", targetValue, ResultStatusEnum.removed));
                    result.Added++;
                }
            }

            return result;
        }

        private ComparisonResultEntry CreateComparisonResultEntry(string id, string sourceValue, string targetValue, ResultStatusEnum status)
        {
            return new ComparisonResultEntry 
            {
                ID = id,
                SourceValue = sourceValue,
                TargetValue = targetValue,
                Status = status
            };
        }
    }
}
