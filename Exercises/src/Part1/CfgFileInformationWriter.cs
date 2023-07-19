using Part1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public class CfgFileInformationWriter : IFileInformationWriter
    {
        private const int tableWidth = 50;

        /// <summary>
        /// Writes the .cfg file information to the console
        /// </summary>
        /// <param name="file">A file object</param>
        /// <exception cref="Exception">The given file object needs to implement the ICfgFile interface</exception>
        public void WriteFileInformation(IFile file)
        {
            ICfgFile cfgFile = file as ICfgFile;

            if (cfgFile == null)
            {
                throw new Exception("Given object needs to implement the ICfgFile interface");
            }

            Console.WriteLine(new string('-', tableWidth));

            Console.WriteLine($"| File name: {file.Name}".PadRight(tableWidth - 1) + "|");

            Console.WriteLine(new string('-', tableWidth));

            foreach (var ln in cfgFile.Metadata)
            {
                string line = $"| {ln}".PadRight(tableWidth - 1) + "|";
                Console.WriteLine(line);
            }

            Console.WriteLine(new string('-', tableWidth));

            Console.WriteLine();
        }
    }
}
