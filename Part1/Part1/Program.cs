using System;
using System.Collections;

namespace Part1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string sourceFilePath = "FMB001-default.cfg";
            const string targetFilePath = "FMB920-default.cfg";

            /*Console.WriteLine("Source file path: ");
            string sourceFilePath = Console.ReadLine();

            Console.WriteLine("Target file path: ");
            string targetFilePath = Console.ReadLine();*/

            CfgFile sourceFile = FileReader.ReadFile(sourceFilePath);
            CfgFile targetFile = FileReader.ReadFile(targetFilePath);

            ConsoleWriter.WriteFileInformation(sourceFile);
            ConsoleWriter.WriteFileInformation(targetFile);
        }
    }
}
