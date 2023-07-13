using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public static class ComparisonResultInformationWriter
    {
        private const int MinimumTableWidth = 100;

        /// <summary>
        /// Writes a table to the console with comparison results
        /// </summary>
        /// <param name="results">A list of comparison result entries</param>
        public static void WriteResultInformation(List<ComparisonResultEntry> results)
        {
            if (results.Count == 0)
            {
                Console.WriteLine("There are no results");
                Console.WriteLine();
                return;
            }

            int widestColumn = 0;
            string longestSourceString = results.OrderByDescending(r => r.SourceValue.Length).First().SourceValue;
            string longestTargetString = results.OrderByDescending(r => r.TargetValue.Length).First().TargetValue;

            if (longestSourceString.Length > longestTargetString.Length)
                widestColumn = longestSourceString.Length;
            else if (longestTargetString.Length > longestSourceString.Length)
                widestColumn = longestTargetString.Length;
            else
                widestColumn = longestSourceString.Length;

            widestColumn += 2;

            int columns = typeof(ComparisonResultEntry).GetProperties().Length;
            int currentTableWidth = widestColumn * columns;

            if (currentTableWidth < MinimumTableWidth)
            {
                currentTableWidth = MinimumTableWidth;
                widestColumn = MinimumTableWidth / columns;
            }

            Console.WriteLine(new string('-', currentTableWidth + 1));
            string line = "| ID".PadRight(widestColumn) + "| Source Value".PadRight(widestColumn)
                + "| Target Value".PadRight(widestColumn) + "| Status".PadRight(widestColumn) + "|";
            Console.WriteLine(line);
            Console.WriteLine(new string('-', currentTableWidth + 1));

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

                line = $"| {row.ID}".PadRight(widestColumn) + $"| {row.SourceValue}".PadRight(widestColumn)
                    + $"| {row.TargetValue}".PadRight(widestColumn) + $"| {row.Status}".PadRight(widestColumn) + "|";

                ConsoleColor prevBackgroundColor = Console.BackgroundColor;
                ConsoleColor prevForegroundColor = Console.ForegroundColor;

                Console.Write(line);
                Console.ResetColor();
                Console.WriteLine();

                Console.ForegroundColor = prevForegroundColor;
                Console.BackgroundColor = prevBackgroundColor;
                Console.Write(new string('-', currentTableWidth + 1));
                Console.ResetColor();
                Console.WriteLine();
            }

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Writes a comparison result summary in the console
        /// </summary>
        /// <param name="result">A comparison result object</param>
        public static void WriteResultSummaryInformation(ComparisonResult result)
        {
            Console.WriteLine($"U:{result.unchanged} M:{result.modified} R:{result.removed} A:{result.added}");
            Console.WriteLine();
        }
    }
}
