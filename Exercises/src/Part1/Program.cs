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

            //string sourceFilePath = UserInputHandler.GetLineInput("Enter source file path:");
            //string targetFilePath = UserInputHandler.GetLineInput("Enter target file path:");

            Console.Clear();

            IFileReader cfgFileReader = new CfgFileReader();

            IFile sourceFile = cfgFileReader.ReadFile(sourceFilePath);
            IFile targetFile = cfgFileReader.ReadFile(targetFilePath);

            IFileInformationWriter cfgConsoleWriter = new CfgFileInformationWriter();
            IFileComparer comparer = new CfgFileComparer();

            ComparisonResult result = comparer.CompareFiles(sourceFile, targetFile);

            IMenuManager menuManager = new MenuManager(sourceFile, targetFile, result, cfgConsoleWriter);
            INavigationManager navManager = new NavigationManager(menuManager);

            navManager.StartNavigation();
        }
    }
}
