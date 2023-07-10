using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class FileComparer
    {
        public ComparisonResult CompareFiles(CfgFile source, CfgFile target)
        {
            ComparisonResult result = new ComparisonResult();

            foreach (string key in source.Data.Keys)
            {
                if (target.Data.ContainsKey(key))
                {
                    if (source.Data[key].Equals(target.Data[key]))
                    {
                        result.results.Add(new ComparisonResultEntry
                        {
                            ID = key,
                            SourceValue = source.Data[key].ToString(),
                            TargetValue = target.Data[key].ToString(),
                            Status = ResultStatusEnum.unchanged
                        });

                        result.unchanged++;
                    }
                    else
                    {
                        result.results.Add(new ComparisonResultEntry
                        {
                            ID = key,
                            SourceValue = source.Data[key].ToString(),
                            TargetValue = target.Data[key].ToString(),
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
                        SourceValue = source.Data[key].ToString(),
                        TargetValue = "",
                        Status = ResultStatusEnum.removed
                    });

                    result.removed++;
                }
            }

            foreach (string key in target.Data.Keys)
            {
                if (!source.Data.ContainsKey(key))
                {
                    result.results.Add(new ComparisonResultEntry
                    {
                        ID = key,
                        SourceValue = "",
                        TargetValue = target.Data[key].ToString(),
                        Status = ResultStatusEnum.added
                    });

                    result.added++;
                }
            }

            return result;
        }
    }
}
