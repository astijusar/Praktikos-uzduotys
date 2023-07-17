using System.Collections.Generic;
using System.Collections;

namespace Part2.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public List<string> Metadata { get; set; }
        public List<KeyValuePair<string, string>> IdValuePairs { get; set; }

        public FileModel()
        {
            Metadata = new List<string>();
            IdValuePairs = new List<KeyValuePair<string, string>>();
        }
    }
}