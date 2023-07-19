using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Part1.Interfaces;

namespace Part1
{
    public class CfgFileReader : IFileReader
    {
        /// <summary>
        /// Reads a .cfg file
        /// </summary>
        /// <param name="path">File path</param>
        /// <returns>An object that implements the IFile interface</returns>
        public IFile ReadFile(string path)
        {
            ICfgFile file = new CfgFile();
            file.Name = Path.GetFileName(path);

            try
            {
                using (Stream fileStream = File.OpenRead(path), zippedStream = new GZipStream(fileStream, CompressionMode.Decompress))
                {
                    using (StreamReader reader = new StreamReader(zippedStream))
                    {
                        var line = reader.ReadToEnd();
                        var pairs = line.Split(";");

                        foreach (var pair in pairs)
                        {
                            var keyValue = pair.Split(":");
                            var id = keyValue[0];
                            var value = keyValue.Length > 1 ? keyValue[1] : null;

                            if (id == "") continue;

                            if (!int.TryParse(id, out _))
                            {
                                file.Metadata.Add(pair);
                            }
                            else
                            {
                                if (keyValue.Length == 1)
                                {
                                    file.IdValuePairs.Add(id, value);
                                }
                                else
                                {
                                    file.IdValuePairs.Add(id, value);
                                }
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.Write($"File not found using the given path - {path}");
                Environment.Exit(1);
            }
            catch (DirectoryNotFoundException)
            {
                Console.Write($"Directory not found using the given path - {path}");
                Environment.Exit(1);
            }
            catch (UnauthorizedAccessException)
            {
                Console.Write($"The access to the file is unauthorized: {path}");
                Environment.Exit(1);
            }

            return file;
        }
    }
}
