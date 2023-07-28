using System.Collections.Generic;

namespace Core.Models.DTOs
{
    public class FileModelDto
    {
        public string FileName { get; set; }
        public List<string> Metadata { get; set; }
    }
}
