using Part1.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class FileComparer
    {
        private readonly IFileComparisonMethod comparisonMethod;

        public FileComparer(IFileComparisonMethod comparisonMethod)
        {
            this.comparisonMethod = comparisonMethod;
        }

        public ComparisonResult CompareFiles(IFile source, IFile target)
        {
            return comparisonMethod.CompareFiles(source, target);
        }
    }
}
