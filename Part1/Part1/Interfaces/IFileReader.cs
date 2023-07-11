using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.Interfaces
{
    public interface IFileReader
    {
        public IFile ReadFile(string path);
    }
}
