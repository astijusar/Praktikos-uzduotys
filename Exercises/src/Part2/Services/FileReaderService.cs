using Microsoft.AspNetCore.Http;
using Part2.Models;
using Part2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Part2.Services
{
    public class FileReaderService : IFileReaderService
    {
        /// <summary>
        /// Read a file information from IFormFile
        /// </summary>
        /// <param name="file">IFormFile that needs to be read</param>
        /// <returns>A FileModel with its information</returns>
        /// <exception cref="ArgumentException">The file can't be null</exception>
        public FileModel ReadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            string fileContent;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                if (IsGzipFile(fileBytes))
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        using (var reader = new StreamReader(gzipStream))
                        {
                            fileContent = reader.ReadToEnd();
                        }
                    }
                }
                else
                {
                    using (var reader = new StreamReader(memoryStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                }
            }

            var fileModel = new FileModel();
            var pairs = fileContent.Split(';');

            fileModel.Name = file.FileName;

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
                    fileModel.IdValuePairs.Add(new KeyValuePair<string, string>(id, value));
                }
            }

            return fileModel;
        }

        private bool IsGzipFile(byte[] bytes)
        {
            if (bytes.Length >= 2)
            {
                var firstByte = bytes[0];
                var secondByte = bytes[1];

                return firstByte == 0x1F && secondByte == 0x8B;
            }

            return false;
        }
    }
}
