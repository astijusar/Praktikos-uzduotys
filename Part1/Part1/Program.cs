using System;
using System.Collections;

namespace Part1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string sourceFile = "FMB001-default.cfg";
            const string targetFile = "FMB920-default.cfg";

            /*Console.WriteLine("Source file path: ");
            string sourceFile = Console.ReadLine();

            Console.WriteLine("Target file path: ");
            string targetFile = Console.ReadLine();*/


            Hashtable sourceData = FileReader.ReadFile(sourceFile);
            Hashtable targetData = FileReader.ReadFile(targetFile);

            foreach (DictionaryEntry en in sourceData)
            {
                Console.WriteLine($"ID - {en.Key}, VALUE - {en.Value}");
            }
        }
    }
}
