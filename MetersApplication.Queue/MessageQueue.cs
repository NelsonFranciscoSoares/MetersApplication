using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MetersApplication.Core;
using MetersApplication.DataModel;

namespace MetersApplication.Queue
{
    public class MessageQueue : IQueue
    {
        private BlockingCollection<MetersInformationFlatFormat> Queue { get; set; }
        private const int DIMENSION = 1000;
        private IDataWriterSerializable DataWriter { get; set; }

        public MessageQueue(IDataWriterSerializable dataWriter)
        {
            this.Queue = new BlockingCollection<MetersInformationFlatFormat>();
            this.DataWriter = dataWriter;
        }

        public void Put(MetersInformationFlatFormat data)
        {
            this.Queue.Add(data);

            if (this.Queue.Count == DIMENSION)
            {
                Task.Run(() =>
                {
                    var dataQueue = DequeueData(false);
                    this.DataWriter.AppendSerializeObject(dataQueue);
                });
            }
        }

        private List<MetersInformationFlatFormat> DequeueData(bool dequeueAll)
        {
            var queueCount = this.Queue.Count;

            var list = new List<MetersInformationFlatFormat>();

            for (var i = 0; i < ((dequeueAll == true) ? queueCount : DIMENSION); i++)
            {
                list.Add(this.Queue.Take());
            }

            return list;
        }

        public void Flush()
        {
            this.DataWriter.AppendSerializeObject(DequeueData(true));
        }
    }
}
