﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Core
{
    public class ConfigurationReader : IConfigurationReader
    {
        public FileModel ReadFromFile(string path)
        {
            var fileName = Path.GetFileName(path);
            using var stream = File.OpenRead(path);
            using var zippedStream = new GZipStream(stream, CompressionMode.Decompress);

            return ReadFromStream(zippedStream, fileName);
        }

        public FileModel ReadFromStream(Stream stream, string fileName)
        {
            var fileModel = new FileModel
            {
                Name = fileName
            };

            using var streamReader = new StreamReader(stream);

            var line = streamReader.ReadToEnd();
            var pairs = line.Split(";");

            foreach (var pair in pairs)
            {
                var keyValue = pair.Split(":");
                var id = keyValue[0];
                var value = keyValue.Length > 1 ? keyValue[1] : null;

                if (id == "") continue;

                if (!int.TryParse(id, out _))
                {
                    fileModel.Metadata.Add(pair);
                }
                else
                {
                    fileModel.IdValuePairs.Add(id, value);
                }
            }

            return fileModel;
        }
    }
}
