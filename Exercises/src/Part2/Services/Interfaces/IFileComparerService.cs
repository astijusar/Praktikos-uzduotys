using Part2.Models;
using System.Collections.Generic;

namespace Part2.Services.Interfaces
{
    public interface IFileComparerService
    {
        public List<ComparisonResult> CompareFiles(FileModel sourceFile, FileModel targetFile);
    }
}
