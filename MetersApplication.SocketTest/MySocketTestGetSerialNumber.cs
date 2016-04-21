using System;
using MetersApplication.Core;
using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Factory;
using MetersApplication.Utils;

namespace MetersApplication.SocketTest
{
    public class MySocketTestGetSerialNumber : IExchangeDataComponent
    {
        public void Connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] frame)
        {
            return;
        }

        public int Receive(byte[] frame)
        {
            var serialNumber = "SN-100-200";
            var data = ConvertUtils.ConvertTextToByteArrayInHexadecimal(serialNumber);
            FrameFactory.Create(frame, MetersOperationsConstants.RESPONSE_SERIAL_NUMBER, data);
            return (data.Length + 4);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
