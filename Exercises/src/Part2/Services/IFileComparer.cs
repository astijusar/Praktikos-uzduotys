﻿using Part2.Models;
using System.Collections.Generic;

namespace Part2.Services
{
    public interface IFileComparer
    {
        public List<ComparisonResult> CompareFiles(FileModel sourceFile, FileModel targetFile);
    }
}
