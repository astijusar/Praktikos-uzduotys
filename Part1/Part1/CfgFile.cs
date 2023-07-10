using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class CfgFile
    {
        public string Name { get; set; }
        public List<string> Information { get; set; }
        public Hashtable Data { get; set; }

        public CfgFile()
        {
            Information = new List<string>();
            Data = new Hashtable();
        }
    }
}
