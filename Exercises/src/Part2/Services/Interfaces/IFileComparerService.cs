using Part2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Part2.Services.Interfaces
{
    public interface IFileComparerService
    {
        public Task<List<ComparisonResult>> CompareFiles(FileModel sourceFile, FileModel targetFile);
    }
}
