using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MetersApplication.DataModel;
using MetersApplication.FileWriter;
using MetersApplication.Protocol;
using MetersApplication.Queue;
using MetersApplication.Socket;
using MetersApplication.Utils;

namespace MetersApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> fileInMemory;
            var parser = new Parser.Parser();
            var fileWriterSerializedData = new FileWriterSerializable("Data.xml");
            var numberOfDaysToRun = int.Parse(ConfigurationManager.AppSettings[PipelineConstants.TIME_ELAPSED]);
            
            // Step 1 - Read information and parse this information that exists in listaDePedidos.txt
            using(var fileReader = new FileReader.FileReader("listaDePedidos.txt"))
            {
                fileInMemory = fileReader.ReadFile();
            }
            var dataParsed = parser.Parse(fileInMemory);

            //Step 2 - Create Queue that will got some information and file writer
            var queue = new MessageQueue(fileWriterSerializedData); 

            //Step 3 - Iterate information and get the measurer information
            while (dataParsed.FirstOrDefault(param => param.EndTime <= DateTime.Now) != null)
            {
                var elements = dataParsed.Where(param => param.CurrentTime <= DateTime.Now);

                if(elements.Count() != 0)
                {
                    //Iterate each meter and get data
                    Parallel.ForEach(elements, (request) =>
                    {
                        request.CurrentTime = request.CurrentTime.AddMinutes(numberOfDaysToRun);

                        var socket = new SocketTcpIp();

                        using (var client = new MetersProtocolClient(socket, request.IP, request.Port))
                        {
                            try
                            {
                                client.Connect();
                                var serialNumber = client.ReadSerialNumber();
                                var dateTime = client.ReadDateTime();
                                var energy = client.ReadEnergyValue(dateTime);
                                client.Disconnect();

                                var metersInformation = new MetersInformationFlatFormat
                                {
                                    SerialNumber = serialNumber,
                                    MeterDateTime = request.InitialTime,
                                    RegisterDateTime = dateTime,
                                    EnergyValue = FormatUtils.Round(energy)
                                };

                                queue.Put(metersInformation);
                            }
                            catch (Exception)
                            {
                                //log exceptions and retry to get data
                            }
                        }
                    });
                }
            }
            
            //Step 4 - Get data saved in file system
            //Flush queue memory data to file
            queue.Flush();

            //Get data in desnormalized format
            var data = fileWriterSerializedData.GetData();

            //Put data in normalized format
            var normalizedData = data.GroupBy(param => new { param.SerialNumber, param.MeterDateTime },
                param => new { param.RegisterDateTime, param.EnergyValue },
                (key, value) => new
                {
                    key.SerialNumber,
                    key.MeterDateTime,
                    Details = value.Select(param => new {
                        MetersRegister = param.RegisterDateTime,
                        Value = param.EnergyValue
                    })
                })
                .Select(param => new MetersInformation {
                    SerialNumber = param.SerialNumber,
                    MeterDateTime = param.MeterDateTime,
                    Details = param.Details.Select(param1 => new MetersDetailInformation{
                        Value = param1.Value,
                        MetersRegister = param1.MetersRegister})
                });

            //Step 5 - Write meters data to csv file
            using(var fileWriter = new FileWriter.FileWriter("Details.csv"))
            {
                fileWriter.Write(normalizedData);
            }
        }
    }
}
