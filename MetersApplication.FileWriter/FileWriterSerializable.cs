using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using MetersApplication.Core;
using MetersApplication.DataModel;

namespace MetersApplication.FileWriter
{
    public class FileWriterSerializable : IDataWriterSerializable
    {
        private String FilePath { get; set; }

        public FileWriterSerializable(string filePath)
        {
            this.FilePath = filePath;
        }

        public void AppendSerializeObject(List<MetersInformationFlatFormat> data)
        {
            var serializer = new XmlSerializer(data.GetType());

            try
            {
                using(var reader = XmlReader.Create(this.FilePath))
                {
                    var fileData = (List<MetersInformationFlatFormat>)serializer.Deserialize(reader);
                    data = fileData.Concat(data).ToList();
                }
            }
            catch (FileNotFoundException)
            {
            }

            using(var writer = XmlWriter.Create(this.FilePath))
            {
                serializer.Serialize(writer, data);
            } 
        }

        public List<MetersInformationFlatFormat> GetData()
        {
            var serializer = new XmlSerializer(typeof(List<MetersInformationFlatFormat>));

            using(var reader = XmlReader.Create(this.FilePath))
            {
                return (List<MetersInformationFlatFormat>)serializer.Deserialize(reader);
            }
        }
    }
}
