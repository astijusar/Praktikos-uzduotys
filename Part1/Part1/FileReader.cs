using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part1
{
    public static class FileReader
    {
        public static CfgFile ReadFile(string path)
        {
            CfgFile file = new CfgFile();
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

                            if (keyValue[0] == "") continue;

                            if (!int.TryParse(keyValue[0], out _))
                            {
                                file.Information.Add(pair);
                            }
                            else
                            {
                                if (keyValue.Length == 1)
                                {
                                    file.Data.Add(keyValue[0], null);
                                }
                                else
                                {
                                    file.Data.Add(keyValue[0], keyValue[1]);
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

            return file;
        }
    }
}
