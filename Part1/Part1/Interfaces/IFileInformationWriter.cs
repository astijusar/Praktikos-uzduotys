using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.Interfaces
{
    public interface IFileInformationWriter
    {
        void WriteFileInformation(IFile file);
        void WriteResultSummaryInformation(ComparisonResult result);
        void WriteResultInformation(List<ComparisonResultEntry> results);
    }
}
