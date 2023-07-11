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
        private IUserInputHandler userInputHandler;

        public MenuManager(IFile sourceFile, IFile targetFile, ComparisonResult result, IFileInformationWriter writer, IUserInputHandler userInputHandler)
        {
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.result = result;
            this.writer = writer;
            this.userInputHandler = userInputHandler;
        }

        public void MainMenu()
        {
            Console.Clear();
            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            //writer.WriteMenu();

            uint choice = userInputHandler.GetUserMenuChoice(3);
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
            writer.WriteResultSummaryInformation(result);

            Console.WriteLine("Press any key to go back");
            Console.ReadKey();

            MainMenu();
        }

        private void FullResultMenu()
        {
            Console.Clear();

            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            writer.WriteResultInformation(result.results);

            Console.WriteLine();
            Console.WriteLine("[1] Filter by ID or status");
            Console.WriteLine("[2] Return");
            Console.WriteLine();

            uint choice = userInputHandler.GetUserMenuChoice(2);
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
            Console.WriteLine("Enter status to filter by (leave empty to not filter):");
            var status = Console.ReadLine();

            var filteredByStatus = result.results;
            if (!string.IsNullOrEmpty(status))
            {
                filteredByStatus = result.results.Where(r => r.Status.ToString().Equals(status)).ToList();
            }

            Console.Clear();
            Console.WriteLine("Enter ID to filter by (leave empty to not filter):");

            var id = Console.ReadLine();
            var filteredById = filteredByStatus;
            if (!string.IsNullOrEmpty(id))
            {
                filteredById = filteredByStatus.Where(r => r.ID.StartsWith(id)).ToList();
            }

            Console.Clear();
            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            writer.WriteResultInformation(filteredById);

            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MainMenu();
        }
    }
}
