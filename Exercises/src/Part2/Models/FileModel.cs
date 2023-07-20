using System.Collections.Generic;
using System.Collections;

namespace Part2.Models
{
    public class FileModel
    {
        public string Name { get; set; }
        public List<KeyValuePair<string, string>> TextIdValuePairs { get; set; }
        public List<KeyValuePair<string, string>> NumberIdValuePairs { get; set; }

        public FileModel()
        {
            TextIdValuePairs = new List<KeyValuePair<string, string>>();
            NumberIdValuePairs = new List<KeyValuePair<string, string>>();
        }
    }
}