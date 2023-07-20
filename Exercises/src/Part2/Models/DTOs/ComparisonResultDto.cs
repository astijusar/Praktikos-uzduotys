using Part1;
using System.Collections.Generic;

namespace Part2.Models.DTOs
{
    public class ComparisonResultDto
    {
        public string ID { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public ResultStatusEnum Status { get; set; }
    }
}
