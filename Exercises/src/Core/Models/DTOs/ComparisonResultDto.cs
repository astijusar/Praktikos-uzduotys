using System.Collections.Generic;
using Core.Enums;

namespace Core.Models.DTOs
{
    public class ComparisonResultDto
    {
        public string Id { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public ResultStatus Status { get; set; }
    }
}
