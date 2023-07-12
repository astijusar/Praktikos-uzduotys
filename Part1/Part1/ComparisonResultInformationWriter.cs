using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public static class ComparisonResultInformationWriter
    {
        private const int tableWidth = 160;

        public static void WriteResultInformation(List<ComparisonResultEntry> results)
        {
            int columnWidth = tableWidth / 4;

            if (results.Count == 0)
            {
                Console.WriteLine("There are no results");
                Console.WriteLine();
                return;
            }

            Console.WriteLine(new string('-', tableWidth));
            string line = "| ID".PadRight(columnWidth) + "| Source Value".PadRight(columnWidth)
                + "| Target Value".PadRight(columnWidth) + "| Status".PadRight(columnWidth) + "|";
            Console.WriteLine(line);
            Console.WriteLine(new string('-', tableWidth));

            foreach (var row in results)
            {
                Console.ForegroundColor = ConsoleColor.Black;

                if (row.Status == ResultStatusEnum.unchanged)
                    Console.BackgroundColor = ConsoleColor.Gray;
                else if (row.Status == ResultStatusEnum.modified)
                    Console.BackgroundColor = ConsoleColor.Yellow;
                else if (row.Status == ResultStatusEnum.removed)
                    Console.BackgroundColor = ConsoleColor.Red;
                else
                    Console.BackgroundColor = ConsoleColor.Green;

                line = $"| {row.ID}".PadRight(columnWidth) + $"| {row.SourceValue}".PadRight(columnWidth)
                    + $"| {row.TargetValue}".PadRight(columnWidth) + $"| {row.Status}".PadRight(columnWidth) + "|";

                Console.WriteLine(line);
                Console.Write(new string('-', tableWidth));

                Console.ResetColor();

                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        public static void WriteResultSummaryInformation(ComparisonResult result)
        {
            Console.WriteLine($"U:{result.unchanged} M:{result.modified} R:{result.removed} A:{result.added}");
            Console.WriteLine();
        }
    }
}
