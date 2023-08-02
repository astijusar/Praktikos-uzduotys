using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IResultFilter
    {
        List<ComparisonResultEntry> Filter(List<ComparisonResultEntry> results, ResultFilterParameters parameters);
    }
}
