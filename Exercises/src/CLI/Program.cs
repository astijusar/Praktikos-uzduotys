using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Core;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Configuration;

namespace CLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 170;

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            const string sourceFilePath = "FMB920-default.cfg";
            const string targetFilePath = "FMB920-modified.cfg";

            //string sourceFilePath = UserInputHandler.GetLineInput("Enter source file path:");
            //string targetFilePath = UserInputHandler.GetLineInput("Enter target file path:");

            Console.Clear();

            var validator = new FileValidator(config);

            try
            {
                validator.Validate(sourceFilePath);
                validator.Validate(targetFilePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }

            IConfigurationReader cfgReader = new ConfigurationReader();

            var sourceFile = cfgReader.ReadFromFile(sourceFilePath);
            var targetFile = cfgReader.ReadFromFile(targetFilePath);

            IConfigurationComparer cfgComparer = new ConfigurationComparer();

            var result = cfgComparer.Compare(sourceFile, targetFile);

            IResultFilter filter = new ResultFilter();

            var menuManager = new MenuManager(sourceFile, targetFile, filter, result);

            menuManager.StartNavigation();
        }
    }
}
