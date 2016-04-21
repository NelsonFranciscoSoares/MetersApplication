using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using MetersApplication.Core;
using MetersApplication.DataModel;

namespace MetersApplication.Parser
{
    public class Parser : IParser
    {
        private int Duration { get; set; }

        public Parser() 
        {
            this.Duration = Int32.Parse(ConfigurationManager.AppSettings[ParserConstants.METERS_DURATION_TIME]);
        }

        public IEnumerable<MetersRequest> Parse(Dictionary<int, string> inputData)
        {
 	        var outputData = new List<MetersRequest>();

            foreach(var measurerData in inputData)
            {
                var dataSplitted = measurerData.Value.Split(';');

                var measurerRequest = new MetersRequest
                {
                    IP = dataSplitted[0],
                    Port = int.Parse(dataSplitted[1]),
                    InitialTime = DateTime.ParseExact(dataSplitted[2],"yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    CurrentTime = DateTime.Parse(dataSplitted[2]),
                };

                CalculateEndTime(measurerRequest);

                outputData.Add(measurerRequest);
            }

            return outputData;
        }

        private void CalculateEndTime(MetersRequest data)
        {
            data.EndTime = data.InitialTime.AddDays(this.Duration);
        }
    }
}
