using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class UserInputHandler : IUserInputHandler
    {
        public uint GetUserMenuChoice(int choiceMax)
        {
            uint choice = GetInput();

            while (choice < 1 || choice > choiceMax)
            {
                Console.WriteLine("Please try again");

                choice = GetInput();
            }

            return choice;
        }

        private uint GetInput()
        {
            uint choice = 0;

            Console.Write("> ");
            uint.TryParse(Console.ReadLine(), out choice);

            return choice;
        }
    }
}
