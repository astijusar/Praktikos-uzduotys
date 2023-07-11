using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class NavigationManager
    {
        private CfgFile sourceFile;
        private CfgFile targetFile;
        private ConsoleWriter writer;
        private ComparisonResult result;

        public NavigationManager(CfgFile sourceFile, CfgFile targetFile, ComparisonResult result, ConsoleWriter writer)
        {
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.result = result;
            this.writer = writer;
        }

        public void StartNavigation()
        {
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();
            writer.WriteFileInformation(sourceFile);
            writer.WriteFileInformation(targetFile);
            writer.WriteMenu();

            uint choice = GetUserChoice(3);
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
            writer.WriteResult(result.results);

            Console.WriteLine();
            Console.WriteLine("[1] Filter by ID or status");
            Console.WriteLine("[2] Return");
            Console.WriteLine();

            uint choice = GetUserChoice(2);
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
            writer.WriteResult(filteredById);

            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            MainMenu();
        }

        private uint GetUserChoice(int choiceMax)
        {
            uint choice = 0;
            Action getInput = () =>
            {
                Console.Write("> ");
                uint.TryParse(Console.ReadLine(), out choice);
            };

            getInput();

            while (choice < 1 || choice > choiceMax)
            {
                Console.Clear();
                Console.WriteLine("Please try again");
                writer.WriteMenu();

                getInput();
            }

            return choice;
        }
    }
}
