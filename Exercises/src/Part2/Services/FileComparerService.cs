using Part2.Models;
using Part2.Models.Enums;
using Part2.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Part2.Services
{
    public class FileComparerService : IFileComparerService
    {
        /// <summary>
        /// Compares ID and value pairs of two files
        /// </summary>
        /// <param name="sourceFile">The source file to compare</param>
        /// <param name="targetFile">The target file to compare against</param>
        /// <returns>Returns the comparison result list</returns>
        public Task<List<ComparisonResult>> CompareFiles(FileModel sourceFile, FileModel targetFile)
        {
            var comparisonResult = new List<ComparisonResult>();

            // Unchanged pairs
            var unchangedPairs = sourceFile.IdValuePairs
                .Join(targetFile.IdValuePairs, s => s.Key, t => t.Key, (s, t) => new { Source = s, Target = t })
                .Where(pair => pair.Source.Value == pair.Target.Value)
                .ToList();

            // Modified pairs
            var modifiedPairs = sourceFile.IdValuePairs
                .Join(targetFile.IdValuePairs, s => s.Key, t => t.Key, (s, t) => new { Source = s, Target = t })
                .Where(pair => pair.Source.Value != pair.Target.Value)
                .ToList();

            // Removed pairs
            var removedPairs = sourceFile.IdValuePairs
                .Where(pair => !targetFile.IdValuePairs.Any(t => t.Key == pair.Key))
                .ToList();

            // Added pairs
            var addedPairs = targetFile.IdValuePairs
                .Where(pair => !sourceFile.IdValuePairs.Any(s => s.Key == pair.Key))
                .ToList();


            comparisonResult.AddRange(unchangedPairs.Select(pair => new ComparisonResult
            {
                ID = pair.Source.Key,
                SourceValue = pair.Source.Value,
                TargetValue = pair.Target.Value,
                Status = ResultStatusEnum.unchanged
            }));

            comparisonResult.AddRange(modifiedPairs.Select(pair => new ComparisonResult
            {
                ID = pair.Source.Key,
                SourceValue = pair.Source.Value,
                TargetValue = pair.Target.Value,
                Status = ResultStatusEnum.modified
            }));

            comparisonResult.AddRange(removedPairs.Select(pair => new ComparisonResult
            {
                ID = pair.Key,
                SourceValue = pair.Value,
                TargetValue = null,
                Status = ResultStatusEnum.removed
            }));

            comparisonResult.AddRange(addedPairs.Select(pair => new ComparisonResult
            {
                ID = pair.Key,
                SourceValue = null,
                TargetValue = pair.Value,
                Status = ResultStatusEnum.added
            }));

            return Task.FromResult(comparisonResult);
        }
    }
}
