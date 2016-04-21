using System;

namespace MetersApplication.DataModel
{
    public class MetersRequest
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public DateTime InitialTime { get; set; }
        public DateTime CurrentTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
