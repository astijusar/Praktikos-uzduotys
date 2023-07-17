using Part2.Models.Enums;

namespace Part2.Models.RequestFeatures
{
    public class FileComparisonParameters
    {
        public ResultStatusEnum? ResultStatus { get; set; }
        public string ID { get; set; }
    }
}
