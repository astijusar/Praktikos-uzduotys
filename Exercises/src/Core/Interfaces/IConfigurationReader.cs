using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface IConfigurationReader
    {
        public FileModel ReadFromFile(string path);
    }
}
