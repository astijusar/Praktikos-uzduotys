using System.Collections.Generic;

namespace Core.Models.DTOs
{
    public class ComparisonResultWithMetadataDto
    {
        public FileModelDto SourceFile { get; set; }
        public FileModelDto TargetFile { get; set; }
        public List<ComparisonResultDto> ComparisonResult { get; set; }
    }
}
