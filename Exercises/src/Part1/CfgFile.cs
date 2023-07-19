using Part1.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class CfgFile : ICfgFile
    {
        public string Name { get; set; }
        public List<string> Metadata { get; set; }
        public Hashtable IdValuePairs { get; set; }

        public CfgFile()
        {
            Metadata = new List<string>();
            IdValuePairs = new Hashtable();
        }
    }
}
