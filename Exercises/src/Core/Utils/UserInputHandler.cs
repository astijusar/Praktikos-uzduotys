using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils
{
    public static class UserInputHandler
    {
        /// <summary>
        /// Gets any user key input
        /// </summary>
        /// <param name="message">A message to be displayed before input</param>
        public static void AnyKeyInput(string message)
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Get user inputted line
        /// </summary>
        /// <param name="message">An optional message written before the input</param>
        /// <returns>The user inputted line</returns>
        public static string GetLineInput(string message = null)
        {
            if (message != null)
            {
                Console.WriteLine(message);
            }

            return Console.ReadLine();
        }

        /// <summary>
        /// Gets user number input and validates it
        /// </summary>
        /// <param name="choiceMax">Maximum allowed number to choose from</param>
        /// <returns>The number that the user chose</returns>
        public static uint GetUserMenuChoice(int choiceMax)
        {
            var choice = GetInput();

            while (choice < 1 || choice > choiceMax)
            {
                Console.WriteLine("Please try again");

                choice = GetInput();
            }

            return choice;
        }

        private static uint GetInput()
        {
            Console.Write("> ");
            uint.TryParse(Console.ReadLine(), out var choice);

            return choice;
        }
    }
}
