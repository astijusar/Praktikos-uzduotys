using Part1.Interfaces;
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
            Console.WindowWidth = 170;

            const string sourceFilePath = "FMB920-default.cfg";
            const string targetFilePath = "FMB920-modified.cfg";

            /*Console.WriteLine("Enter source file path: ");
            string sourceFilePath = Console.ReadLine();

            Console.WriteLine("Enter target file path: ");
            string targetFilePath = Console.ReadLine();

            Console.Clear();*/

            IFileReader cfgFileReader = new CfgFileReader();

            ICfgFile sourceFile = (ICfgFile)cfgFileReader.ReadFile(sourceFilePath);
            ICfgFile targetFile = (ICfgFile)cfgFileReader.ReadFile(targetFilePath);

            IFileInformationWriter cfgConsoleWriter = new CfgFileInformationWriter(160);
            IFileComparisonMethod comparisonMethod = new CfgFileComparisonMethod();

            FileComparer comparer = new FileComparer(comparisonMethod);
            ComparisonResult result = comparer.CompareFiles(sourceFile, targetFile);

            IUserInputHandler userInputHandler = new UserInputHandler();
            IMenuManager menuManager = new MenuManager(sourceFile, targetFile, result, cfgConsoleWriter, userInputHandler);
            INavigationManager navManager = new NavigationManager(menuManager);

            navManager.StartNavigation();
        }
    }
}
