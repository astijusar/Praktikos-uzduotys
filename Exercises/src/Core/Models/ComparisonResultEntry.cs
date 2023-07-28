using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Models
{
    public class ComparisonResultEntry
    {
        public string Id { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public ResultStatus Status { get; set; }
    }
}
