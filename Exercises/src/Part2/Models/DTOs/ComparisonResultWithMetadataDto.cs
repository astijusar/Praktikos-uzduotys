using System.Collections.Generic;

namespace Part2.Models.DTOs
{
    public class ComparisonResultWithMetadataDto
    {
        public FileModelDto SourceFile { get; set; }
        public FileModelDto TargetFile { get; set; }
        public List<ComparisonResultDto> ComparisonResult { get; set; }
    }
}
