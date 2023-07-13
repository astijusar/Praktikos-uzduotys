using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class MenuManager : IMenuManager
    {
        private IFile _sourceFile;
        private IFile _targetFile;
        private IFileInformationWriter _writer;
        private ComparisonResult _result;

        public MenuManager(IFile sourceFile, IFile targetFile, ComparisonResult result, IFileInformationWriter writer)
        {
            _sourceFile = sourceFile;
            _targetFile = targetFile;
            _result = result;
            _writer = writer;
        }

        /// <summary>
        /// Main menu after file path input and file comparison
        /// </summary>
        public void MainMenu()
        {
            Console.Clear();
            _writer.WriteFileInformation(_sourceFile);
            _writer.WriteFileInformation(_targetFile);
            Console.WriteLine("[1] Show result summary");
            Console.WriteLine("[2] Show all results");
            Console.WriteLine("[3] Exit");
            Console.WriteLine();

            uint choice = UserInputHandler.GetUserMenuChoice(3);
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

            _writer.WriteFileInformation(_sourceFile);
            _writer.WriteFileInformation(_targetFile);
            ComparisonResultInformationWriter.WriteResultSummaryInformation(_result);

            UserInputHandler.AnyKeyInput("Press any key to go back...");

            MainMenu();
        }

        /// <summary>
        /// Console menu where a comparison result table is displayed with filtering option
        /// </summary>
        private void FullResultMenu()
        {
            Console.Clear();

            _writer.WriteFileInformation(_sourceFile);
            _writer.WriteFileInformation(_targetFile);
            ComparisonResultInformationWriter.WriteResultInformation(_result.results);

            Console.WriteLine("[1] Filter by ID or status");
            Console.WriteLine("[2] Return");
            Console.WriteLine();

            uint choice = UserInputHandler.GetUserMenuChoice(2);
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

            var filteredByStatus = _result.results;
            if (!string.IsNullOrEmpty(status))
            {
                filteredByStatus = _result.results.Where(r => r.Status.ToString().Equals(status)).ToList();
            }

            Console.Clear();

            var id = UserInputHandler.GetLineInput("Enter ID to filter by(leave empty to not filter):");
            var filteredById = filteredByStatus;
            if (!string.IsNullOrEmpty(id))
            {
                filteredById = filteredByStatus.Where(r => r.ID.StartsWith(id)).ToList();
            }

            Console.Clear();
            _writer.WriteFileInformation(_sourceFile);
            _writer.WriteFileInformation(_targetFile);
            
            if (status != "" || id != "")
            {
                Console.Write("Filtered by: ");
            }
            if (status != "")
            {
                if (id != "")
                {
                    Console.Write($"Status - {status}, ");
                }
                else
                {
                    Console.Write($"Status - {status} \n");
                }
            }
            if (id != "")
            {
                Console.Write($"ID - {id} \n");
            }

            Console.WriteLine();

            ComparisonResultInformationWriter.WriteResultInformation(filteredById);

            UserInputHandler.AnyKeyInput("Press any key to go back...");

            MainMenu();
        }
    }
}
