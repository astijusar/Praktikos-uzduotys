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
                //writer.WriteMenu();

                getInput();
            }

            return choice;
        }
    }
}
