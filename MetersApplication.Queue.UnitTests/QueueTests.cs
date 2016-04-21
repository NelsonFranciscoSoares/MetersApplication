using System;
using System.Threading;
using MetersApplication.DataModel;
using MetersApplication.FileWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetersApplication.Queue.UnitTests
{
    [TestClass]
    public class QueueTests
    {
        private MessageQueue Queue { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.Queue = new MessageQueue(new FileWriterSerializable("SerializedFile.xml"));
        }

        [TestMethod]
        public void GenerateSerializedFile()
        {
            for (var i = 0; i < 193; i++)
            {
                var obj = new MetersInformationFlatFormat
                {
                    SerialNumber = (i + 1).ToString(),
                    MeterDateTime = DateTime.Now,
                    RegisterDateTime = DateTime.Now,
                    EnergyValue = (10 + i)
                };

                this.Queue.Put(obj);

                Thread.Sleep(1000);
                
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.Queue.Flush();
        }
    }
}
