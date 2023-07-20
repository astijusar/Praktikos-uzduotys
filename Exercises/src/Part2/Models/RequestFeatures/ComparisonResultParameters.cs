using Part2.Models.Enums;

namespace Part2.Models.RequestFeatures
{
    public class ComparisonResultParameters
    {
        public ResultStatusEnum? TextParamResultStatus { get; set; }
        public string TextParamID { get; set; }

        public ResultStatusEnum? NumberParamResultStatus { get; set; }
        public string NumberParamID { get; set; }
    }
}
