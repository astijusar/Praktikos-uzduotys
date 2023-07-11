﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1.Interfaces
{
    public interface ICfgFile : IFile
    {
        public List<string> Information { get; set; }
        public Hashtable Data { get; set; }
    }
}
