using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class ConsoleWriter
    {
        public int tableWidth { get; set; } = 50;

        public ConsoleWriter()
        {

        }

        public ConsoleWriter(int tableWidth)
        {
            this.tableWidth = tableWidth;
        }

        public void WriteFileInformation(CfgFile file)
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

        public void WriteResultSummaryInformation(ComparisonResult results)
        {
            Console.WriteLine($"U:{results.unchanged} M:{results.modified} R:{results.removed} A:{results.added}");
            Console.WriteLine();
        }

        public void WriteResult(List<ComparisonResultEntry> results)
        {
            int columnWidth = tableWidth / 4;

            if (results.Count == 0)
            {
                Console.Write("There are no results");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(new string('-', tableWidth));
            }

            Console.ForegroundColor = ConsoleColor.Black;

            foreach (var row in results)
            {
                if (row.Status == ResultStatusEnum.unchanged)
                    Console.BackgroundColor = ConsoleColor.Gray;
                else if (row.Status == ResultStatusEnum.modified)
                    Console.BackgroundColor = ConsoleColor.Yellow;
                else if (row.Status == ResultStatusEnum.removed)
                    Console.BackgroundColor = ConsoleColor.Red;
                else
                    Console.BackgroundColor = ConsoleColor.Green;

                string line = $"| {row.ID}".PadRight(columnWidth) + $"| {row.SourceValue}".PadRight(columnWidth) 
                    + $"| {row.TargetValue}".PadRight(columnWidth) + $"| {row.Status}".PadRight(columnWidth) + "|";

                Console.WriteLine(line);
                Console.WriteLine(new string('-', tableWidth));
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }

        public void WriteMenu()
        {
            Console.WriteLine("[1] Show result summary");
            Console.WriteLine("[2] Show all results");
            Console.WriteLine("[3] Exit");
            Console.WriteLine();
        }
    }
}
