using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Part1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 160;

            //const string sourceFilePath = "FMB001-default.cfg";
            //const string targetFilePath = "FMB920-modified.cfg";

            Console.WriteLine("Enter source file path: ");
            string sourceFilePath = Console.ReadLine();

            Console.WriteLine("Enter target file path: ");
            string targetFilePath = Console.ReadLine();

            Console.Clear();

            CfgFile sourceFile = FileReader.ReadFile(sourceFilePath);
            CfgFile targetFile = FileReader.ReadFile(targetFilePath);

            ConsoleWriter wr = new ConsoleWriter(150);

            FileComparer comparer = new FileComparer();
            ComparisonResult result = comparer.CompareFiles(sourceFile, targetFile);

            NavigationManager navManager = new NavigationManager(sourceFile, targetFile, result, wr);
            navManager.StartNavigation();
        }
    }
}
