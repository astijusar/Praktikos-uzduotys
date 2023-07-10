using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Part1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string sourceFilePath = "FMB920-default.cfg";
            const string targetFilePath = "FMB920-modified.cfg";

            /*Console.WriteLine("Source file path: ");
            string sourceFilePath = Console.ReadLine();

            Console.WriteLine("Target file path: ");
            string targetFilePath = Console.ReadLine();

            Console.Clear();*/

            CfgFile sourceFile = FileReader.ReadFile(sourceFilePath);
            CfgFile targetFile = FileReader.ReadFile(targetFilePath);

            ConsoleWriter wr = new ConsoleWriter();

            wr.WriteFileInformation(sourceFile);
            wr.WriteFileInformation(targetFile);

            FileComparer comparer = new FileComparer();
            ComparisonResult result = comparer.CompareFiles(sourceFile, targetFile);

            wr.WriteResultInformation(result);
        }
    }
}
