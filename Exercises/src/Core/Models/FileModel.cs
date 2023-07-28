using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public List<string> Metadata { get; set; }
        public Hashtable IdValuePairs { get; set; }

        public FileModel()
        {
            Metadata = new List<string>();
            IdValuePairs = new Hashtable();
        }
    }
}
