﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class ComparisonResult
    {
        public List<ComparisonResultEntry> ResultEntries { get; set; } = new List<ComparisonResultEntry>();
        public int Unchanged { get; set; } = 0;
        public int Modified { get; set; } = 0;
        public int Removed { get; set; } = 0;
        public int Added { get; set; } = 0;
    }
}
