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

            foreach (string key in sourceCfgFile.Data.Keys)
            {
                if (targetCfgFile.Data.ContainsKey(key))
                {
                    if (sourceCfgFile.Data[key].Equals(targetCfgFile.Data[key]))
                    {
                        result.results.Add(new ComparisonResultEntry
                        {
                            ID = key,
                            SourceValue = sourceCfgFile.Data[key].ToString(),
                            TargetValue = targetCfgFile.Data[key].ToString(),
                            Status = ResultStatusEnum.unchanged
                        });

                        result.unchanged++;
                    }
                    else
                    {
                        result.results.Add(new ComparisonResultEntry
                        {
                            ID = key,
                            SourceValue = sourceCfgFile.Data[key].ToString(),
                            TargetValue = targetCfgFile.Data[key].ToString(),
                            Status = ResultStatusEnum.modified
                        });

                        result.modified++;
                    }
                }
                else
                {
                    result.results.Add(new ComparisonResultEntry
                    {
                        ID = key,
                        SourceValue = sourceCfgFile.Data[key].ToString(),
                        TargetValue = "",
                        Status = ResultStatusEnum.removed
                    });

                    result.removed++;
                }
            }

            foreach (string key in targetCfgFile.Data.Keys)
            {
                if (!sourceCfgFile.Data.ContainsKey(key))
                {
                    result.results.Add(new ComparisonResultEntry
                    {
                        ID = key,
                        SourceValue = "",
                        TargetValue = targetCfgFile.Data[key].ToString(),
                        Status = ResultStatusEnum.added
                    });

                    result.added++;
                }
            }

            return result;
        }
    }
}
