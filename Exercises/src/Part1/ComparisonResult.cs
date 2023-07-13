using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class ComparisonResult
    {
        public List<ComparisonResultEntry> results { get; set; } = new List<ComparisonResultEntry>();
        public int unchanged { get; set; } = 0;
        public int modified { get; set; } = 0;
        public int removed { get; set; } = 0;
        public int added { get; set; } = 0;
    }
}
