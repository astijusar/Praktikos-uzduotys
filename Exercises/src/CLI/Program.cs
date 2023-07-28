using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Core;
using Core.Interfaces;
using Core.Models;

namespace CLI
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

            IConfigurationReader cfgReader = new ConfigurationReader();

            var sourceFile = cfgReader.ReadFromFile(sourceFilePath);
            var targetFile = cfgReader.ReadFromFile(targetFilePath);

            IConfigurationComparer cfgComparer = new ConfigurationComparer();

            var result = cfgComparer.Compare(sourceFile, targetFile);

            var menuManager = new MenuManager(sourceFile, targetFile, result);

            menuManager.StartNavigation();
        }
    }
}
