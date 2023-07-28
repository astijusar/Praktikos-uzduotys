using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Core.Utils;

namespace CLI
{
    public class MenuManager
    {
        private readonly FileModel _sourceFile;
        private readonly FileModel _targetFile;
        private readonly ComparisonResult _result;

        public MenuManager(FileModel sourceFile, FileModel targetFile, ComparisonResult result)
        {
            _sourceFile = sourceFile;
            _targetFile = targetFile;
            _result = result;
        }

        /// <summary>
        /// Start of the navigation
        /// </summary>
        public void StartNavigation()
        {
            MainMenu();
        }

        /// <summary>
        /// Main menu after file path input and file comparison
        /// </summary>
        private void MainMenu()
        {
            Console.Clear();
            InformationWriter.WriteFileInformation(_sourceFile);
            InformationWriter.WriteFileInformation(_targetFile);
            Console.WriteLine("[1] Show result summary");
            Console.WriteLine("[2] Show all results");
            Console.WriteLine("[3] Exit");
            Console.WriteLine();

            var choice = UserInputHandler.GetUserMenuChoice(3);
            switch (choice)
            {
                case 1:
                    ResultSummaryMenu();
                    break;
                case 2:
                    FullResultMenu();
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Console menu where comparison result summary is displayed
        /// </summary>
        private void ResultSummaryMenu()
        {
            Console.Clear();

            InformationWriter.WriteFileInformation(_sourceFile);
            InformationWriter.WriteFileInformation(_targetFile);
            InformationWriter.WriteResultSummaryInformation(_result);

            UserInputHandler.AnyKeyInput("Press any key to go back...");

            MainMenu();
        }

        /// <summary>
        /// Console menu where a comparison result table and summary is displayed with filtering option
        /// </summary>
        private void FullResultMenu()
        {
            Console.Clear();

            InformationWriter.WriteFileInformation(_sourceFile);
            InformationWriter.WriteFileInformation(_targetFile);
            InformationWriter.WriteResultSummaryInformation(_result);
            InformationWriter.WriteResultInformation(_result.ResultEntries);

            Console.WriteLine("[1] Filter by ID or status");
            Console.WriteLine("[2] Return");
            Console.WriteLine();

            var choice = UserInputHandler.GetUserMenuChoice(2);
            switch (choice)
            {
                case 1:
                    FilterResultsMenu();
                    break;
                case 2:
                    MainMenu();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Console menu where comparison result filters can be chosen
        /// </summary>
        private void FilterResultsMenu()
        {
            Console.Clear();
            var status = UserInputHandler.GetLineInput("Enter status to filter by (leave empty to not filter):");

            var filteredByStatus = _result.ResultEntries;
            if (!string.IsNullOrEmpty(status))
            {
                filteredByStatus = _result.ResultEntries.Where(r => r.Status.ToString().ToLower().Equals(status)).ToList();
            }

            Console.Clear();

            var id = UserInputHandler.GetLineInput("Enter ID to filter by(leave empty to not filter):");
            var filteredById = filteredByStatus;
            if (!string.IsNullOrEmpty(id))
            {
                filteredById = filteredByStatus.Where(r => r.Id.StartsWith(id)).ToList();
            }

            Console.Clear();
            InformationWriter.WriteFileInformation(_sourceFile);
            InformationWriter.WriteFileInformation(_targetFile);
            
            if (status != "" || id != "")
            {
                Console.Write("Filtered by: ");
            }
            if (status != "")
            {
                Console.Write(id != "" ? $"Status - {status}, " : $"Status - {status} \n");
            }
            if (id != "")
            {
                Console.Write($"ID - {id} \n");
            }

            Console.WriteLine();

            InformationWriter.WriteResultInformation(filteredById);

            UserInputHandler.AnyKeyInput("Press any key to go back...");

            MainMenu();
        }
    }
}
