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
        private IFile sourceFile;
        private IFile targetFile;
        private IFileInformationWriter writer;
        private ComparisonResult result;

        public MenuManager(IFile sourceFile, IFile targetFile, ComparisonResult result, IFileInformationWriter writer)
        {
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.result = result;
            this.writer = writer;
        }

        public void MainMenu()
        {
            Console.Clear();
            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
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

        private void ResultSummaryMenu()
        {
            Console.Clear();

            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            ComparisonResultInformationWriter.WriteResultSummaryInformation(result);

            UserInputHandler.AnyKeyInput("Press any key to go back...");

            MainMenu();
        }

        private void FullResultMenu()
        {
            Console.Clear();

            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            ComparisonResultInformationWriter.WriteResultInformation(result.results);

            Console.WriteLine();
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

        private void FilterResultsMenu()
        {
            Console.Clear();
            var status = UserInputHandler.GetLineInput("Enter status to filter by (leave empty to not filter):");

            var filteredByStatus = result.results;
            if (!string.IsNullOrEmpty(status))
            {
                filteredByStatus = result.results.Where(r => r.Status.ToString().Equals(status)).ToList();
            }

            Console.Clear();

            var id = UserInputHandler.GetLineInput("Enter ID to filter by(leave empty to not filter):");
            var filteredById = filteredByStatus;
            if (!string.IsNullOrEmpty(id))
            {
                filteredById = filteredByStatus.Where(r => r.ID.StartsWith(id)).ToList();
            }

            Console.Clear();
            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            
            if (status != "" || id != "")
            {
                Console.Write("Filtered by: ");
            }
            if (status != "")
            {
                Console.Write($"Status - {status} ");

                if (id != "")
                {
                    Console.Write(", ");
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
