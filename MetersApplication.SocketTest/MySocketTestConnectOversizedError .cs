using System;
using MetersApplication.Core;
using MetersApplication.ProtocolBase;
using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Factory;

namespace MetersApplication.SocketTest
{
    public class MySocketTestConnectOversizedError : IExchangeDataComponent
    {
        private ProtocolValidator Validator { get; set; }

        public MySocketTestConnectOversizedError()
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
            FrameFactory.Create(frame, MetersOperationsConstants.RESPONSE_CONNECT, 0x00, 0x00);
            return 6;
        }

        public void Dispose()
        {
        }
    }
}
