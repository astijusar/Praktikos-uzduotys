using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class ComparisonResultEntry
    {
        public string ID { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public ResultStatusEnum Status { get; set; }
    }
}
