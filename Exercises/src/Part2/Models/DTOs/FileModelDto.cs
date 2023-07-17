using System.Collections.Generic;

namespace Part2.Models.DTOs
{
    public class FileModelDto
    {
        public string FileName { get; set; }
        public List<string> Metadata { get; set; }
    }
}
