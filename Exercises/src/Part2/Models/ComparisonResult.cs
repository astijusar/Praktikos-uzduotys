using System.Collections.Generic;

namespace Part2.Models
{
    public class ComparisonResult
    {
        public List<ComparisonResultEntry> resultEntries { get; set; } = new List<ComparisonResultEntry>();
        public int unchanged { get; set; } = 0;
        public int modified { get; set; } = 0;
        public int removed { get; set; } = 0;
        public int added { get; set; } = 0;
    }
}
