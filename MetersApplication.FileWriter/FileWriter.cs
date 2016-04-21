using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MetersApplication.Core;
using MetersApplication.DataModel;

namespace MetersApplication.FileWriter
{
    public class FileWriter : IDataWriter
    {
        private string FilePath { get; set; }
        private StreamWriter Writer { get; set; }

        public FileWriter(string filePath)
        {
            this.FilePath = filePath;

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            this.Writer = new StreamWriter(File.Create(this.FilePath));
        }

        public void Write(IEnumerable<MetersInformation> data)
        {
            var sb = new StringBuilder();
            foreach(var info in data)
            {
                sb.AppendLine(string.Format("{0};{1}", info.SerialNumber, info.MeterDateTime.ToString("yyyy-MM-dd HH:mm")));

                foreach(var detail in info.Details)
                {
                    sb.AppendLine(string.Format("{0};{1}", detail.MetersRegister.ToString("yyyy-MM-dd HH:mm:ss"), detail.Value));
                }
            }

            this.Writer.Write(sb.ToString());
        }

        public void Dispose()
        {
            this.Writer.Close();
            this.Writer.Dispose();
        }
    }
}
