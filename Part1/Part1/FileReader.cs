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
        public static Hashtable ReadFile(string path)
        {
            Hashtable data = new Hashtable();

            using (Stream fileStream = File.OpenRead(path), zippedStream = new GZipStream(fileStream, CompressionMode.Decompress))
            {
                using (StreamReader reader = new StreamReader(zippedStream))
                {
                    var line = reader.ReadToEnd();
                    var pairs = line.Split(";");

                    foreach (var pair in pairs)
                    {
                        var keyValue = pair.Split(":");

                        if (keyValue.Length == 1)
                        {
                            data.Add(keyValue[0], null);
                        }
                        else
                        {
                            data.Add(keyValue[0], keyValue[1]);
                        }
                    }
                }
            }

            return data;
        }
    }
}
