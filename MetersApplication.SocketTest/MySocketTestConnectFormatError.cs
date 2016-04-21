using System;
using MetersApplication.Core;
using MetersApplication.ProtocolBase;
using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Factory;

namespace MetersApplication.SocketTest
{
    public class MySocketTestConnectFormatError : IExchangeDataComponent
    {
        private ProtocolValidator Validator { get; set; }

        public MySocketTestConnectFormatError()
        {
            this.Validator = new ProtocolValidator();
        }

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
            FrameFactory.Create(frame, MetersOperationsConstants.RESPONSE_DISCONNECT, 0x00, 0x00);
            return 5;
        }

        public void Dispose()
        {
        }
    }
}
