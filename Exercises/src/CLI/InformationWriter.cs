using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Models;
using static System.Console;

namespace CLI
{
    public static class InformationWriter
    {
        private const int TableWidth = 50;
        private const int MinimumResultTableWidth = 100;

        /// <summary>
        /// Writes the .cfg file information to the console
        /// </summary>
        /// <param name="file">A file object</param>
        /// <exception cref="Exception">The given file object needs to implement the ICfgFile interface</exception>
        public static void WriteFileInformation(FileModel file)
        {
            WriteLine(new string('-', TableWidth));

            WriteLine($"| File name: {file.Name}".PadRight(TableWidth - 1) + "|");

            WriteLine(new string('-', TableWidth));

            foreach (var ln in file.Metadata)
            {
                string line = $"| {ln}".PadRight(TableWidth - 1) + "|";
                WriteLine(line);
            }

            WriteLine(new string('-', TableWidth));

            WriteLine();
        }

        /// <summary>
        /// Writes a table to the console with comparison results
        /// </summary>
        /// <param name="results">A list of comparison result entries</param>
        public static void WriteResultInformation(List<ComparisonResultEntry> results)
        {
            if (results.Count == 0)
            {
                WriteLine("There are no results");
                WriteLine();
                return;
            }

            var widestColumn = 0;
            var longestSourceString = results.OrderByDescending(r => r.SourceValue.Length).First().SourceValue;
            var longestTargetString = results.OrderByDescending(r => r.TargetValue.Length).First().TargetValue;

            if (longestSourceString.Length > longestTargetString.Length)
                widestColumn = longestSourceString.Length;
            else if (longestTargetString.Length > longestSourceString.Length)
                widestColumn = longestTargetString.Length;
            else
                widestColumn = longestSourceString.Length;

            widestColumn += 2;

            var columns = typeof(ComparisonResultEntry).GetProperties().Length;
            var currentTableWidth = widestColumn * columns;

            if (currentTableWidth < MinimumResultTableWidth)
            {
                currentTableWidth = MinimumResultTableWidth;
                widestColumn = MinimumResultTableWidth / columns;
            }

            WriteLine(new string('-', currentTableWidth + 1));
            var line = "| ID".PadRight(widestColumn) + "| Source Value".PadRight(widestColumn)
                + "| Target Value".PadRight(widestColumn) + "| Status".PadRight(widestColumn) + "|";
            WriteLine(line);
            WriteLine(new string('-', currentTableWidth + 1));

            foreach (var row in results)
            {
                ForegroundColor = ConsoleColor.Black;

                BackgroundColor = row.Status switch
                {
                    ResultStatus.Unchanged => ConsoleColor.Gray,
                    ResultStatus.Modified => ConsoleColor.Yellow,
                    ResultStatus.Removed => ConsoleColor.Red,
                    _ => ConsoleColor.Green
                };

                line = $"| {row.Id}".PadRight(widestColumn) + $"| {row.SourceValue}".PadRight(widestColumn)
                                                            + $"| {row.TargetValue}".PadRight(widestColumn) + $"| {row.Status}".PadRight(widestColumn) + "|";

                var prevBackgroundColor = BackgroundColor;
                var prevForegroundColor = ForegroundColor;

                Write(line);
                ResetColor();
                WriteLine();

                ForegroundColor = prevForegroundColor;
                BackgroundColor = prevBackgroundColor;
                Write(new string('-', currentTableWidth + 1));
                ResetColor();
                WriteLine();
            }

            ResetColor();
            WriteLine();
        }

        /// <summary>
        /// Writes a comparison result summary in the console
        /// </summary>
        /// <param name="result">A comparison result object</param>
        public static void WriteResultSummaryInformation(ComparisonResult result)
        {
            WriteLine($"U:{result.Unchanged} M:{result.Modified} R:{result.Removed} A:{result.Added}");
            WriteLine();
        }
    }
}
