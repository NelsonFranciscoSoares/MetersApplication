using System;

namespace MetersApplication.Core
{
    public interface IExchangeDataComponent : IDisposable
    {
       void Connect(string ip, int port);
       void Disconnect();
       void Send(byte[] frame);
       int Receive(byte[] frame);
    }
}
