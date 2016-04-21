using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MetersApplication.FileReader
{
    public class FileReader : Core.IDataReader
    {
        private string FilePath { get; set; }
        private StreamReader Reader { get; set; }

        public FileReader(string filePath)
        {
            this.FilePath = filePath;
            this.Reader = new StreamReader(filePath);
        }
        
        public Dictionary<int, string> ReadFile()
        {
            var line = string.Empty;
            var index = 0;
            var dictionary = new Dictionary<int, string>();

            while ((line = this.Reader.ReadLine()) != null)
            {
                dictionary.Add(index++, line);
            }

            return dictionary;
        }

        public void Dispose()
        {
            this.Reader.Dispose();
        }
    }
}
