using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public static class ConsoleWriter
    {
        private const int tableWidth = 50;

        public static void WriteFileInformation(CfgFile file)
        {
            Console.WriteLine(new string('-', tableWidth));

            Console.WriteLine($"| File name: {file.Name}".PadRight(tableWidth - 1) + "|");

            Console.WriteLine(new string('-', tableWidth));

            foreach (var ln in file.Information)
            {
                string line = $"| {ln}".PadRight(tableWidth - 1) + "|";
                Console.WriteLine(line);
            }

            Console.WriteLine(new string('-', tableWidth));

            Console.WriteLine();
        }
    }
}
