using Part2.Models.Enums;

namespace Part2.Models
{
    public class ComparisonResultEntry
    {
        public string ID { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public ResultStatusEnum Status { get; set; }
    }
}
